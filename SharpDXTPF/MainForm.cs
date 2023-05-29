using Aritiafel.IlodarAcademy.SharpDX;
using Aritiafel.IlodarAcademy;
using System.Numerics;
using Aritiafel.Organizations.RaeriharUniversity;

namespace SharpDXTPF
{
    public partial class MainForm : Form
    {
        SharpDXEngine sde;
        Ar3DArea area;
        ArViewport viewport;

        public MainForm()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Ar3DModel model = new Ar3DModel();
            model.Planes = new ArPlane[1];
            ArVertex[] vertices = new ArVertex[3];
            vertices[0] = new ArVertex(1000, 1000, 0, Color.Red);
            vertices[2] = new ArVertex(1000, 0, 0, Color.Green);
            vertices[1] = new ArVertex(0, 1000, 0, Color.Blue);
            model.Planes[0] = new ArPlane(vertices);

            Ar3DModel model2 = new Ar3DModel();
            model2.Planes = new ArPlane[1];
            vertices = new ArVertex[3];
            vertices[0] = new ArVertex(0, 0, 0, Color.Red);
            vertices[1] = new ArVertex(1000, -2000, 0, Color.Green);
            vertices[2] = new ArVertex(1000, 0, 0, Color.Blue);
            model2.Planes[0] = new ArPlane(vertices);

            area = new Ar3DArea();
            area.BackgroudColor = Color.Black;
            area.Models = new Ar3DModel[] { model, model2 };
            //area.Models = new Ar3DModel[] { model2 };
            //area.Models = new Ar3DModel[] { model };
            area.ScaleTransform = ArVector3.One;
            area.RotateTransform = ArVector3.Zero;
            area.TranslateTransform = ArVector3.Zero;

            sde = new SharpDXEngine();
            SharpDXSetting setting = new SharpDXSetting
            {
                Handle = pibMain.Handle,
                Viewport = new ArViewport(0, 0, pibMain.Width, pibMain.Height, 0, 20)
            };
            sde.Initialize(setting);
            Upload();
        }

        void Upload()
        {
            SharpDXData data = new SharpDXData
            {
                GraphicData = Ar3DMahine.ProduceDrawingVertices(area),
                BackgroundColor = Color.Black
            };
            sde.Load(data);
            sde.Render();
        }

        private Ar3DModel[] GetBasicGrids(int width, int height)
        {
            Ar3DModel[] result = new Ar3DModel[width * height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Ar3DModel model = new Ar3DModel();
                    model.Planes = new ArPlane[1];
                    ArVertex[] vertices = new ArVertex[4];
                    vertices[0] = new ArVertex(0 + i, 0 + j, 0, Color.Black);
                    vertices[1] = new ArVertex(1 + i, 0 + j, 0, Color.Black);
                    vertices[2] = new ArVertex(1 + i, 1 + j, 0, Color.Black);
                    vertices[3] = new ArVertex(0 + i, 1 + j, 0, Color.Black);
                    model.Planes[0] = new ArPlane(vertices);
                    result[i * height + j] = model;
                }
            }
            return result;
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            area.Models = GetBasicGrids(100, 100);
            area.BackgroudColor = Color.White;
            Upload();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(e.KeyChar.ToString());
            switch (e.KeyChar)
            {
                case 'q':
                    //viewport = new ArViewport(viewport.X, viewport.Y, viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth + 10);
                    area.RotateTransform += new ArVector3(0.1, 0, 0);
                    break;
                case 'e':
                    area.RotateTransform += new ArVector3(-0.1, 0, 0);
                    //viewport = new ArViewport(viewport.X, viewport.Y, viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth - 10);
                    break;
                case 'y':
                    area.RotateTransform += new ArVector3(0, 0.1, 0);
                    break;
                case 'h':
                    area.RotateTransform += new ArVector3(0, -0.1, 0);
                    break;
                case 'g':
                    area.RotateTransform += new ArVector3(0, 0, -0.1);
                    break;
                case 'j':
                    area.RotateTransform += new ArVector3(0, 0, 0.1);
                    break;
                case 'w':
                    //viewport = new ArViewport(viewport.X, viewport.Y - 10, viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth);
                    area.TranslateTransform += new ArVector3(0, 0.1f, 0);
                    break;
                case 's':
                    area.TranslateTransform += new ArVector3(0, -0.1f, 0);
                    //viewport = new ArViewport(viewport.X, viewport.Y + 10, viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth);
                    break;
                case 'a':
                    area.TranslateTransform += new ArVector3(-0.1f, 0, 0);
                    //viewport = new ArViewport(viewport.X - 10, viewport.Y, viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth);
                    break;
                case 'd':
                    area.TranslateTransform += new ArVector3(0.1f, 0, 0);
                    //viewport = new ArViewport(viewport.X + 10, viewport.Y, viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth);
                    break;
                case 'x':
                    //area.Models[0].Vertices[0].Color = Color.FromArgb(area.Models[0].Vertices[0].Color.R + 10);
                    sde.Flush();
                    break;
                case 'z':
                    area.ScaleTransform += new ArVector3(0.1, 0.1, 0.1);
                    break;
                case 'c':
                    if (area.ScaleTransform.X > 0.2)
                        area.ScaleTransform -= new ArVector3(0.1, 0.1, 0.1);
                    break;
            }
            Upload();
            //sde.Render();
        }

        private void pibMain_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show($"Mouse Click({e.X},{e.Y})");
        }
        private void btnScript_Click(object sender, EventArgs e)
        {

        }

        private void btnRenderSquare_Click(object sender, EventArgs e)
        {
            Ar3DModel model = new Ar3DModel();
            model.Planes = new ArPlane[1];

            ArVertex[] vertices = new ArVertex[4];
            //vertices[0] = new ArVertex(1000, 1000, 0, Color.White);
            //vertices[1] = new ArVertex(1000, 0, 0, Color.White);            
            //vertices[3] = new ArVertex(0, 0, 0, Color.White);
            //vertices[2] = new ArVertex(0, 1000, 0, Color.White);

            vertices[0] = new ArVertex(0, 0, 0, Color.White);
            vertices[1] = new ArVertex(1000, 0, 0, Color.White);
            vertices[2] = new ArVertex(1000, 1000, 0, Color.White);
            vertices[3] = new ArVertex(0, 1000, 0, Color.White);
            model.Planes[0] = new ArPlane(vertices);
            area.Models = new Ar3DModel[] { model };
            Upload();
        }
    }
}