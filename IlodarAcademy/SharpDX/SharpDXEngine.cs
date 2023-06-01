using SharpDX.Direct3D;
using SharpDX.Direct3D12;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.D3DCompiler;
using Device = SharpDX.Direct3D12.Device;
using Resource = SharpDX.Direct3D12.Resource;
using ShaderBytecodeDC = SharpDX.D3DCompiler.ShaderBytecode;
using ShaderBytecodeD12 = SharpDX.Direct3D12.ShaderBytecode;
using InfoQueue = SharpDX.Direct3D12.InfoQueue;
using Aritiafel.IlodarAcademy.SharpDX;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Aritiafel.IlodarAcademy.SharpDX
{

    public class SharpDXEngine
    {
        struct ConstantBuffer
        {
            public Vector4 data;
            public Vector4 data2;
            public Vector4 data3;
            //public Vector4 data4;
        }

        struct ConstantBuffer2
        {
            public Vector4 data;
        }

        //public string ShaderSLFile { get; set; } =  Path.Combine(Environment.CurrentDirectory, "shaders.hlsl");

        //public string ShaderSLFile { get; set; } = @""
        public string ShaderSLFile { get; set; } = @"C:\Programs\IlodarAcademy\IlodarAcademy\bin\Debug\net7.0\shaders.hlsl";
        public Vertex[][] Data { get; private set; }
        public PrimitiveTopology[] DrawingMethod { get; private set; }
        public Color4 BackgroundColor { get; private set; }
        public IntPtr Handle { get; set; }
        public SwapEffect SwapEffect { get; private set; }
        public ViewportF viewport { get; private set; }        

        Rectangle scissorRect;
        Device device;
        SwapChain3 swapChain;
        InfoQueue iq;

        CommandAllocator commandAllocator;
        CommandAllocator[] bundleAllocators;
        CommandQueue commandQueue;
        const int FrameCount = 2;
        DescriptorHeap renderTargetViewHeap;
        DescriptorHeap constantBufferViewHeap;
        //DescriptorHeap constantBufferViewHeap2;
        PipelineState pipelineState;
        int rtvDescriptorSize;
        readonly Resource[] renderTargets = new Resource[FrameCount];
        RootSignature rootSignature;
        GraphicsCommandList commandList;
        GraphicsCommandList[] bundles;
        Resource vertexBuffer;
        Resource constantBuffer;
        Resource constantBuffer2;
        VertexBufferView vertexBufferView;
        

        ConstantBuffer constantBufferData;
        IntPtr constantBufferPointer;

        ConstantBuffer2 constantBuffer2Data;

        AutoResetEvent fenceEvent;
        Fence fence;
        int fenceValue;
        int frameIndex;
        //Matrix m = Matrix.Identity;

        public void Initialize(SharpDXSetting setting)
        {
            Handle = setting.Handle;
            SwapEffect = (SwapEffect)setting.SwapEffect;
            viewport = new ViewportF(setting.Viewport.X, setting.Viewport.Y,
                    setting.Viewport.Width, setting.Viewport.Height,
                    setting.Viewport.MinDepth, setting.Viewport.MaxDepth);
            LoadPipeline(setting);
            LoadAssets(setting);
        }

        public void Load(SharpDXData data)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            BackgroundColor = data.BackgroundColor.ToSharpDXColor4();
            Data = new Vertex[data.GraphicData.Length][];
            DrawingMethod = new PrimitiveTopology[data.GraphicData.Length];
            for (int i = 0; i < data.GraphicData.Length; i++)
            {
                Data[i] = data.GraphicData[i].Data.ToSharpDXVerticesArray();
                if (data.GraphicData[i].PrimitiveTopology == ArDrawingMethod.LineList)
                    DrawingMethod[i] = PrimitiveTopology.LineList;
                else if (data.GraphicData[i].PrimitiveTopology == ArDrawingMethod.TriangleList)
                    DrawingMethod[i] = PrimitiveTopology.TriangleList;
                else
                    DrawingMethod[i] = PrimitiveTopology.Undefined;
            }
            sw.Stop();
            Debug.WriteLine($"To VerticesArray:{sw.ElapsedMilliseconds}");
            sw.Restart();
            Flush();
            sw.Stop();
            Debug.WriteLine($"Load Assets:{sw.ElapsedMilliseconds}");
        }

        public void Flush()
        {
            LoadAssets2();
        }

        void LoadPipeline(SharpDXSetting setting)
        {
            if (Handle == nint.Zero)
                return;
#if DEBUG
            // Enable the D3D12 debug layer.
            {
                DebugInterface.Get().EnableDebugLayer();
            }
#endif
            
            device = new Device(null, FeatureLevel.Level_11_0);
            using (var factory = new Factory4())
            {
                // Describe and create the command queue.
                var queueDesc = new CommandQueueDescription(CommandListType.Direct);
                commandQueue = device.CreateCommandQueue(queueDesc);
                //Range[] range = new Range[2];
                //range.
                // Describe and create the swap chain.
                var swapChainDesc = new SwapChainDescription()
                {
                    BufferCount = FrameCount,
                    ModeDescription = new ModeDescription((int)viewport.Width, (int)viewport.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                    Usage = Usage.RenderTargetOutput,
                    SwapEffect = SwapEffect,
                    OutputHandle = Handle,
                    //Flags = SwapChainFlags.None,
                    SampleDescription = new SampleDescription(1, 0),
                    IsWindowed = true
                };

                var tempSwapChain = new SwapChain(factory, commandQueue, swapChainDesc);
                swapChain = tempSwapChain.QueryInterface<SwapChain3>();
                tempSwapChain.Dispose();
                //iq = DebugInterface.Get().QueryInterface<InfoQueue>();
                iq = device.QueryInterface<InfoQueue>();
                frameIndex = swapChain.CurrentBackBufferIndex;
            }

            // Create descriptor heaps.
            // Describe and create a render target view (RTV) descriptor heap.
            var rtvHeapDesc = new DescriptorHeapDescription()
            {
                DescriptorCount = FrameCount,
                Flags = DescriptorHeapFlags.None,
                Type = DescriptorHeapType.RenderTargetView
            };
            renderTargetViewHeap = device.CreateDescriptorHeap(rtvHeapDesc);

            var cbvHeapDesc = new DescriptorHeapDescription()
            {
                DescriptorCount = 2,
                Flags = DescriptorHeapFlags.ShaderVisible,
                Type = DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView
            };
            constantBufferViewHeap = device.CreateDescriptorHeap(cbvHeapDesc);
            

            rtvDescriptorSize = device.GetDescriptorHandleIncrementSize(DescriptorHeapType.RenderTargetView);
            //// Create frame resources.
            var rtvHandle = renderTargetViewHeap.CPUDescriptorHandleForHeapStart;
            for (int n = 0; n < FrameCount; n++)
            {
                renderTargets[n] = swapChain.GetBackBuffer<Resource>(n);
                device.CreateRenderTargetView(renderTargets[n], null, rtvHandle);
                rtvHandle += rtvDescriptorSize;
            }
            commandAllocator = device.CreateCommandAllocator(CommandListType.Direct);
        }

        void LoadAssets(SharpDXSetting setting)
        {
            // Create an empty root signature.
            var rootSignatureDesc = new RootSignatureDescription(RootSignatureFlags.AllowInputAssemblerInputLayout,
               // Root Parameters
               new[]
               {
                    new RootParameter(ShaderVisibility.All,
                        new DescriptorRange()
                        {
                            RangeType = DescriptorRangeType.ConstantBufferView,
                            BaseShaderRegister = 0,
                            OffsetInDescriptorsFromTableStart = -1,
                            DescriptorCount = 1
                        },
                         new DescriptorRange()
                        {
                            RangeType = DescriptorRangeType.ConstantBufferView,
                            BaseShaderRegister = 1,
                            OffsetInDescriptorsFromTableStart = -1,
                            DescriptorCount = 1
                        })
               });
            //var rootSignatureDesc = new RootSignatureDescription(RootSignatureFlags.AllowInputAssemblerInputLayout);
            rootSignature = device.CreateRootSignature(rootSignatureDesc.Serialize());

            // Create the pipeline state, which includes compiling and loading shaders.
#if DEBUG
            var vertexShader = new ShaderBytecodeD12(ShaderBytecodeDC.CompileFromFile(ShaderSLFile, "VSMain", "vs_5_0", ShaderFlags.Debug));
#else
            var vertexShader = new ShaderBytecodeD12(ShaderBytecodeDC.CompileFromFile("shaders.hlsl", "VSMain", "vs_5_0"));
#endif

#if DEBUG
            var pixelShader = new ShaderBytecodeD12(ShaderBytecodeDC.CompileFromFile(ShaderSLFile, "PSMain", "ps_5_0", ShaderFlags.Debug));
#else
            var pixelShader = new ShaderBytecodeD12(ShaderBytecodeDC.CompileFromFile("shaders.hlsl", "PSMain", "ps_5_0"));
#endif

            // Define the vertex input layout.
            var inputElementDescs = new[]
            {
                    new InputElement("POSITION",0,Format.R32G32B32_Float,0,0),
                    new InputElement("COLOR",0,Format.R32G32B32A32_Float,12,0)
            };

            //畫所有三角形
            RasterizerStateDescription rasterizerStateDesc = new RasterizerStateDescription()
            {
                CullMode = setting.CullTwoFace ? CullMode.None : CullMode.Front,
                FillMode = FillMode.Solid,
                IsDepthClipEnabled = false,
                IsFrontCounterClockwise = true,
                IsMultisampleEnabled = false,
            };

            // Describe and create the graphics pipeline state object (PSO).
            var psoDesc = new GraphicsPipelineStateDescription()
            {
                InputLayout = new InputLayoutDescription(inputElementDescs),
                RootSignature = rootSignature,
                VertexShader = vertexShader,
                PixelShader = pixelShader,
                RasterizerState = rasterizerStateDesc,
                BlendState = BlendStateDescription.Default(),
                DepthStencilFormat = Format.D32_Float,
                DepthStencilState = new DepthStencilStateDescription() { IsDepthEnabled = false, IsStencilEnabled = false },
                SampleMask = int.MaxValue,
                PrimitiveTopologyType = PrimitiveTopologyType.Triangle,
                RenderTargetCount = 1,
                Flags = PipelineStateFlags.None,
                SampleDescription = new SampleDescription(1, 0),
                StreamOutput = new StreamOutputDescription()
            };

            
            pipelineState = device.CreateGraphicsPipelineState(psoDesc);

            // Create the command list.
            // Command lists are created in the recording state, but there is nothing
            // to record yet. The main loop expects it to be closed, so close it now.
            commandList = device.CreateCommandList(CommandListType.Direct, commandAllocator, pipelineState);
            commandList.Close();

            constantBuffer = device.CreateCommittedResource(new HeapProperties(HeapType.Upload), HeapFlags.None, ResourceDescription.Buffer(256), ResourceStates.GenericRead);
            var cbvDesc = new ConstantBufferViewDescription()
            {
                BufferLocation = constantBuffer.GPUVirtualAddress,
                SizeInBytes = (Utilities.SizeOf<ConstantBuffer>() + 255) & ~255
            };            
            device.CreateConstantBufferView(cbvDesc, constantBufferViewHeap.CPUDescriptorHandleForHeapStart);
            //device.CreateConstantBufferView(cbvDesc, constantBufferViewHeap.CPUDescriptorHandleForHeapStart);

            constantBufferData = new ConstantBuffer
            {
                data = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                data2 = new Vector4(0.5f, 0.5f, 0, 0),
                data3 = new Vector4(0.0f, 1.0f, 0, 1.0f),
                //data4 = new Vector4(1.0f, 0.0f, 0.0f, 1.0f)
            };
            constantBuffer2Data = new ConstantBuffer2
            {
                data = new Vector4(1.0f, 1.0f, 0.0f, 1.0f)
            };
            //var data = new object [] { constantBufferData, constantBuffer2Data };
            
            constantBufferPointer = constantBuffer.Map(0);
            
            Debug.WriteLine($"Constant Buffer Pointer:{string.Format("{0:X}", constantBufferPointer)}");
            Utilities.Write(constantBufferPointer, ref constantBufferData);
            //var cbvDescriptorSize = device.GetDescriptorHandleIncrementSize(DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView);
            //Utilities.Write(constantBufferPointer + 36, ref constantBuffer2Data);
            //     Utilities.Write(constantBufferPointer + cbvDescriptorSize, ref constantBuffer2Data);
            //Utilities.Write(constantBufferPointer + 256 * 4, ref constantBuffer2Data);
            //Utilities.Write(constantBufferPointer + 1024 * 64, ref constantBuffer2Data);
            //Utilities.Write(constantBufferPointer, ref constantBufferData);            
            //Utilities.Write(constantBufferPointer + sizeof(float) * 8, ref constantBuffer2Data);
            //constantBuffer.Unmap(0);

            constantBuffer2 = device.CreateCommittedResource(new HeapProperties(HeapType.Upload), HeapFlags.None, ResourceDescription.Buffer(256), ResourceStates.GenericRead);
            cbvDesc = new ConstantBufferViewDescription()
            {
                BufferLocation = constantBuffer2.GPUVirtualAddress,
                SizeInBytes = (Utilities.SizeOf<ConstantBuffer2>() + 255) & ~255
            };

            //var = new ShaderResourceView()  ConstantBufferViewDescription()


            ////device.CreateConstantBufferView(cbvDesc, constantBufferViewHeap.CPUDescriptorHandleForHeapStart);

            constantBufferPointer = constantBuffer2.Map(0);
            Debug.WriteLine($"Constant Buffer Pointer:{string.Format("{0:X}", constantBufferPointer)}");
            Utilities.Write(constantBufferPointer, ref constantBuffer2Data);
            
            //constantBuffer2.Unmap(0);
        }

        void LoadAssets2()
        {
            // Create the vertex buffer.
            // Define the geometry for a triangle.

            bundles = new GraphicsCommandList[Data.Length];
            bundleAllocators = new CommandAllocator[Data.Length];

            for (int i = 0; i < Data.Length; i++)
            {

                //float aspectRatio = viewport.Width / viewport.Height;
                // Data = new[]
                //{
                //         new Vertex() {Position=new Vector3(0.0f, 0.25f * aspectRatio, 0.0f ),Color=new Vector4(1.0f, 0.0f, 0.0f, 1.0f ) },
                //         new Vertex() {Position=new Vector3(0.25f, -0.25f * aspectRatio, 0.0f),Color=new Vector4(0.0f, 1.0f, 0.0f, 1.0f) },
                //         new Vertex() {Position=new Vector3(-0.25f, -0.25f * aspectRatio, 0.0f),Color=new Vector4(0.0f, 0.0f, 1.0f, 1.0f ) },
                //         new Vertex() {Position=new Vector3(0.5f, 0.25f * aspectRatio, 0.0f ),Color=new Vector4(1.0f, 0.0f, 0.0f, 1.0f ) },
                //         new Vertex() {Position=new Vector3(0.75f, -0.25f * aspectRatio, 0.0f),Color=new Vector4(0.0f, 1.0f, 0.0f, 1.0f) },
                //         new Vertex() {Position=new Vector3(0.25f, -0.25f * aspectRatio, 0.0f),Color=new Vector4(0.0f, 0.0f, 1.0f, 1.0f ) },
                // };
                //Vector4
                
                //upload heap issue
                //if(vertexBuffer == null)
                //{
                    int vertexBufferSize = Utilities.SizeOf(Data[i]);
                    vertexBuffer = device.CreateCommittedResource(new HeapProperties(HeapType.Upload), HeapFlags.None, ResourceDescription.Buffer(vertexBufferSize), ResourceStates.GenericRead);
                    IntPtr pVertexDataBegin = vertexBuffer.Map(0);
                    Utilities.Write(pVertexDataBegin, Data[i], 0, Data[i].Length);
                    vertexBuffer.Unmap(0);
                    vertexBufferView = new VertexBufferView();
                    vertexBufferView.BufferLocation = vertexBuffer.GPUVirtualAddress;
                    vertexBufferView.StrideInBytes = Utilities.SizeOf<Vertex>();
                    vertexBufferView.SizeInBytes = vertexBufferSize;
                //}                
                // Initialize the vertex buffer view.
               
                // Create and record the bundle.                
                bundleAllocators[i] = device.CreateCommandAllocator(CommandListType.Bundle);
                bundles[i] = device.CreateCommandList(0, CommandListType.Bundle, bundleAllocators[i], pipelineState);
                bundles[i].SetGraphicsRootSignature(rootSignature);
                bundles[i].PrimitiveTopology = DrawingMethod[i];
                bundles[i].SetVertexBuffer(0, vertexBufferView);
                bundles[i].DrawInstanced(Data[i].Length, 1, 0, 0);
                bundles[i].Close();
                
            }

            
            // Create synchronization objects.
            fence = device.CreateFence(0, FenceFlags.None);
            fenceValue = 1;

            // Create an event handle to use for frame synchronization.
            fenceEvent = new AutoResetEvent(false);
        }
      

        public void PopulateCommandList()
        {
            // Command list allocators can only be reset when the associated 
            // command lists have finished execution on the GPU; apps should use 
            // fences to determine GPU execution progress.
            commandAllocator.Reset();

            // However, when ExecuteCommandList() is called on a particular command 
            // list, that command list can then be reset at any time and must be before 
            // re-recording.
            commandList.Reset(commandAllocator, pipelineState);


            commandList.SetGraphicsRootSignature(rootSignature);

            //DescriptorHeap[] dh = new DescriptorHeap[] { constantBufferViewHeap, constantBufferViewHeap2 };
            //commandList.SetDescriptorHeaps(dh.Length, dh);
            commandList.SetDescriptorHeaps(new DescriptorHeap[] { constantBufferViewHeap });
            commandList.SetGraphicsRootDescriptorTable(0, constantBufferViewHeap.GPUDescriptorHandleForHeapStart);
            //commandList.SetGraphicsRootDescriptorTable(1, constantBufferViewHeap.GPUDescriptorHandleForHeapStart);
            //commandList.SetGraphicsRootConstantBufferView(0, constantBuffer.GPUVirtualAddress);
            //commandList.SetGraphicsRootConstantBufferView(1, constantBuffer2.GPUVirtualAddress);
            //commandList.SetGraphicsRootDescriptorTable(1, constantBufferViewHeap2.GPUDescriptorHandleForHeapStart);
            //commandList.SetGraphicsRootDescriptorTable(0, constantBufferViewHeap2.GPUDescriptorHandleForHeapStart);

            commandList.SetViewport(viewport);
            //Scrissor自動設定
            scissorRect.X = 0;
            scissorRect.Y = 0;
            scissorRect.Width = (int)viewport.Width + 1;
            scissorRect.Height = (int)viewport.Height + 1;
            commandList.SetScissorRectangles(scissorRect);

            // Indicate that the back buffer will be used as a render target.
            commandList.ResourceBarrierTransition(renderTargets[frameIndex], ResourceStates.Present, ResourceStates.RenderTarget);

            var rtvHandle = renderTargetViewHeap.CPUDescriptorHandleForHeapStart;
            rtvHandle += frameIndex * rtvDescriptorSize;
            commandList.SetRenderTargets(rtvHandle, null);

            // Record commands.
            commandList.ClearRenderTargetView(rtvHandle, BackgroundColor, 0, null);
            for (long i = 0; i < bundles.LongLength; i++)
                commandList.ExecuteBundle(bundles[i]);

            //commandList.PrimitiveTopology = PrimitiveTopology.TriangleStrip;
            //commandList.SetVertexBuffer(0, vertexBufferView);
            //commandList.DrawInstanced(3, 1, 0, 0);
            //Transfrom
            //commandList.ExecuteBundle(bundles[1]);
            // Indicate that the back buffer will now be used to present.
            commandList.ResourceBarrierTransition(renderTargets[frameIndex], ResourceStates.RenderTarget, ResourceStates.Present);

            commandList.Close();
        }


        /// <summary> 
        /// Wait the previous command list to finish executing. 
        /// </summary> 
        void WaitForPreviousFrame()
        {
            // WAITING FOR THE FRAME TO COMPLETE BEFORE CONTINUING IS NOT BEST PRACTICE. 
            // This is code implemented as such for simplicity. 

            int localFence = fenceValue;
            commandQueue.Signal(this.fence, localFence);
            fenceValue++;

            // Wait until the previous frame is finished.
            if (this.fence.CompletedValue < localFence)
            {
                this.fence.SetEventOnCompletion(localFence, fenceEvent.SafeWaitHandle.DangerousGetHandle());
                fenceEvent.WaitOne();
            }

            frameIndex = swapChain.CurrentBackBufferIndex;
        }

        public void Render()
        {
            if (Data == null)
                throw new ArgumentNullException(nameof(Data));
            if (Handle == nint.Zero)
                throw new ArgumentNullException(nameof(Handle));
            // Record all the commands we need to render the scene into the command list.
            PopulateCommandList();

            // Execute the command list.
            commandQueue.ExecuteCommandList(commandList);

            // Present the frame.
            swapChain.Present(1, 0);

            WaitForPreviousFrame();

            int i = 0;
            while (true)
            {
                try
                {
                    i++;
                    Message s = iq.GetMessage(i);                    
                    Debug.WriteLine(iq.GetMessage(i));
                }
                catch
                {
                    break;
                }
            }
            iq.ClearStoredMessages();
        }
    }

}
