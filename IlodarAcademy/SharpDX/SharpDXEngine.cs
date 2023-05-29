using Aritiafel.IlodarAcademy;
using SharpDX.Direct3D;
using SharpDX.Direct3D12;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.D3DCompiler;
using Device = SharpDX.Direct3D12.Device;
using Resource = SharpDX.Direct3D12.Resource;
using ShaderBytecodeDC = SharpDX.D3DCompiler.ShaderBytecode;
using ShaderBytecodeD12 = SharpDX.Direct3D12.ShaderBytecode;
using SharpDX.Mathematics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;

namespace Aritiafel.IlodarAcademy.SharpDX
{

    public class SharpDXEngine
    {
        //public string ShaderSLFile { get; set; } =  Path.Combine(Environment.CurrentDirectory, "shaders.hlsl");
        public string ShaderSLFile { get; set; } = @"C:\Programs\IlodarAcademy\IlodarAcademy\bin\Debug\net7.0\shaders.hlsl";
        public Vertex[] Data { get; private set; }
        public Color4 BackgroundColor { get; private set; }
        public IntPtr Handle { get; set; }
        public SwapEffect SwapEffect { get; private set; }
        public ViewportF viewport { get; private set; }

        Rectangle scissorRect;
        Device device;
        SwapChain3 swapChain;

        CommandAllocator commandAllocator;
        CommandAllocator[] bundleAllocators;
        CommandQueue commandQueue;
        const int FrameCount = 2;
        DescriptorHeap renderTargetViewHeap;
        PipelineState pipelineState;
        int rtvDescriptorSize;
        readonly Resource[] renderTargets = new Resource[FrameCount];
        RootSignature rootSignature;
        GraphicsCommandList commandList;
        GraphicsCommandList[] bundles;
        Resource vertexBuffer;
        VertexBufferView vertexBufferView;

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
            LoadPipeline();
            LoadAssets();
        }

        public void Load(SharpDXData data)
        {
            Data = data.GraphicData.ToSharpDXVerticesArray();
            BackgroundColor = data.BackgroundColor.ToSharpDXColor4();
            //BackgroundColor = Color4.White;
            Flush();
        }

        public void Flush()
        {
            LoadAssets2();
        }

        void LoadPipeline()
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

            rtvDescriptorSize = device.GetDescriptorHandleIncrementSize(DescriptorHeapType.RenderTargetView);

            // Create frame resources.
            var rtvHandle = renderTargetViewHeap.CPUDescriptorHandleForHeapStart;
            for (int n = 0; n < FrameCount; n++)
            {
                renderTargets[n] = swapChain.GetBackBuffer<Resource>(n);
                device.CreateRenderTargetView(renderTargets[n], null, rtvHandle);
                rtvHandle += rtvDescriptorSize;
            }
            commandAllocator = device.CreateCommandAllocator(CommandListType.Direct);
        }

        void LoadAssets2()
        {
            // Create the vertex buffer.
            

            // Define the geometry for a triangle.

            bundles = new GraphicsCommandList[1];
            bundleAllocators = new CommandAllocator[1];

            //for (long i = 0; i < Data.LongLength; i++)
            //{

            float aspectRatio = viewport.Width / viewport.Height;
           // Data = new[]
           //{
           //         new Vertex() {Position=new Vector3(0.0f, 0.25f * aspectRatio, 0.0f ),Color=new Vector4(1.0f, 0.0f, 0.0f, 1.0f ) },
           //         new Vertex() {Position=new Vector3(0.25f, -0.25f * aspectRatio, 0.0f),Color=new Vector4(0.0f, 1.0f, 0.0f, 1.0f) },
           //         new Vertex() {Position=new Vector3(-0.25f, -0.25f * aspectRatio, 0.0f),Color=new Vector4(0.0f, 0.0f, 1.0f, 1.0f ) },
           //         new Vertex() {Position=new Vector3(0.5f, 0.25f * aspectRatio, 0.0f ),Color=new Vector4(1.0f, 0.0f, 0.0f, 1.0f ) },
           //         new Vertex() {Position=new Vector3(0.75f, -0.25f * aspectRatio, 0.0f),Color=new Vector4(0.0f, 1.0f, 0.0f, 1.0f) },
           //         new Vertex() {Position=new Vector3(0.25f, -0.25f * aspectRatio, 0.0f),Color=new Vector4(0.0f, 0.0f, 1.0f, 1.0f ) },
           // };
            int vertexBufferSize = Utilities.SizeOf(Data);
            //upload heap issue
            vertexBuffer = device.CreateCommittedResource(new HeapProperties(HeapType.Upload), HeapFlags.None, ResourceDescription.Buffer(vertexBufferSize), ResourceStates.GenericRead);
            IntPtr pVertexDataBegin = vertexBuffer.Map(0);
            //Fix
            Utilities.Write(pVertexDataBegin, Data, 0, Data.Length);
            
            vertexBuffer.Unmap(0);
            // Initialize the vertex buffer view.
            vertexBufferView = new VertexBufferView();
            vertexBufferView.BufferLocation = vertexBuffer.GPUVirtualAddress;
            vertexBufferView.StrideInBytes = Utilities.SizeOf<Vertex>();
            vertexBufferView.SizeInBytes = vertexBufferSize;
            // Create and record the bundle.                
            bundleAllocators[0] = device.CreateCommandAllocator(CommandListType.Bundle);
            bundles[0] = device.CreateCommandList(0, CommandListType.Bundle, bundleAllocators[0], pipelineState);
            bundles[0].SetGraphicsRootSignature(rootSignature);
            bundles[0].PrimitiveTopology = PrimitiveTopology.TriangleStrip;
            bundles[0].SetVertexBuffer(0, vertexBufferView);
            //bundles[0].DrawInstanced(Data.Length, Data.Length / 3, 0, 0);
            //bundles[0].DrawInstanced(3, Data.Length / 3, 0, 0);
            bundles[0].DrawInstanced(Data.Length, 1, 0, 0);
            bundles[0].Close();
            //}

            // Create synchronization objects.
            fence = device.CreateFence(0, FenceFlags.None);
            fenceValue = 1;

            // Create an event handle to use for frame synchronization.
            fenceEvent = new AutoResetEvent(false);
        }
        void LoadAssets()
        {
            // Create an empty root signature.
            var rootSignatureDesc = new RootSignatureDescription(RootSignatureFlags.AllowInputAssemblerInputLayout);
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

            // Describe and create the graphics pipeline state object (PSO).
            var psoDesc = new GraphicsPipelineStateDescription()
            {
                InputLayout = new InputLayoutDescription(inputElementDescs),
                RootSignature = rootSignature,
                VertexShader = vertexShader,
                PixelShader = pixelShader,
                RasterizerState = RasterizerStateDescription.Default(),
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
            psoDesc.RenderTargetFormats[0] = Format.R8G8B8A8_UNorm;

            pipelineState = device.CreateGraphicsPipelineState(psoDesc);

            // Create the command list.
            // Command lists are created in the recording state, but there is nothing
            // to record yet. The main loop expects it to be closed, so close it now.
            commandList = device.CreateCommandList(CommandListType.Direct, commandAllocator, pipelineState);
            commandList.Close();

            //LoadAssets2();
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
        }
    }

}
