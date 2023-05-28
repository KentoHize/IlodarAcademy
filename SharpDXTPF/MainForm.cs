using Aritiafel.IlodarAcademy.SharpDX;
using Aritiafel.IlodarAcademy;
using System.Diagnostics;
using System.Reflection;

namespace SharpDXTPF
{
    public partial class MainForm : Form
    {
        SharpDXEngine sde;
        Ar3DArea area;

        public MainForm()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Ar3DModel model = new Ar3DModel();
            model.Vertices = new ArVertex[3];
            model.Vertices[0] = new ArVertex(1, 1, 0, Color.Red);
            model.Vertices[1] = new ArVertex(1, 0, 0, Color.Green);
            model.Vertices[2] = new ArVertex(0, 1, 0, Color.Blue);

            Ar3DModel model2 = new Ar3DModel();
            model2.Vertices = new ArVertex[3];
            model2.Vertices[0] = new ArVertex(0, 0, 0, Color.White);
            model2.Vertices[1] = new ArVertex(-3, -4, 0, Color.Red);
            model2.Vertices[2] = new ArVertex(-3, 0, 0, Color.Green);
            area = new Ar3DArea();
            area.Models = new Ar3DModel[] { model, model2 };
            area.Viewport = new ArViewport(0, 0, pibMain.ClientSize.Width, pibMain.ClientSize.Height);


            sde = new SharpDXEngine();
            sde.Area = area;
            sde.Handle = pibMain.Handle;
            sde.Load();            
            sde.Render();
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            Ar3DModel model2 = new Ar3DModel();
            model2.Vertices = new ArVertex[3];
            var aspectRatio = pibMain.ClientSize.Width / pibMain.ClientSize.Height;
            model2.Vertices[0] = new ArVertex(0, 0.25f * aspectRatio, 0, Color.White);
            model2.Vertices[1] = new ArVertex(0.25f, -0.25f * aspectRatio, 0, Color.Red);
            model2.Vertices[2] = new ArVertex(-0.25f, -0.25f * aspectRatio, 0, Color.Green);
            area.Models = new Ar3DModel[] { model2 };

            sde.Flush();
            sde.Render();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(e.KeyChar.ToString());
            switch (e.KeyChar)
            {
                case 'q':
                    area.Viewport = new ArViewport(area.Viewport.X, area.Viewport.Y, area.Viewport.Width, area.Viewport.Height, area.Viewport.MinDepth, area.Viewport.MaxDepth + 10);
                    break;
                case 'e':
                    area.Viewport = new ArViewport(area.Viewport.X, area.Viewport.Y, area.Viewport.Width, area.Viewport.Height, area.Viewport.MinDepth, area.Viewport.MaxDepth - 10);
                    break;
                case 'y':
                    area.Viewport = new ArViewport(area.Viewport.X, area.Viewport.Y, area.Viewport.Width, area.Viewport.Height - 10, area.Viewport.MinDepth, area.Viewport.MaxDepth);
                    break;
                case 'h':
                    area.Viewport = new ArViewport(area.Viewport.X, area.Viewport.Y, area.Viewport.Width, area.Viewport.Height + 10, area.Viewport.MinDepth, area.Viewport.MaxDepth);
                    break;
                case 'g':
                    area.Viewport = new ArViewport(area.Viewport.X, area.Viewport.Y, area.Viewport.Width - 10, area.Viewport.Height, area.Viewport.MinDepth, area.Viewport.MaxDepth);
                    break;
                case 'j':
                    area.Viewport = new ArViewport(area.Viewport.X, area.Viewport.Y, area.Viewport.Width + 10, area.Viewport.Height, area.Viewport.MinDepth, area.Viewport.MaxDepth);
                    break;
                case 'w':
                    area.Viewport = new ArViewport(area.Viewport.X, area.Viewport.Y - 10, area.Viewport.Width, area.Viewport.Height, area.Viewport.MinDepth, area.Viewport.MaxDepth);
                    break;
                case 's':
                    area.Viewport = new ArViewport(area.Viewport.X, area.Viewport.Y + 10, area.Viewport.Width, area.Viewport.Height, area.Viewport.MinDepth, area.Viewport.MaxDepth);
                    break;
                case 'a':
                    area.Viewport = new ArViewport(area.Viewport.X - 10, area.Viewport.Y, area.Viewport.Width, area.Viewport.Height, area.Viewport.MinDepth, area.Viewport.MaxDepth);
                    break;
                case 'd':
                    area.Viewport = new ArViewport(area.Viewport.X + 10, area.Viewport.Y, area.Viewport.Width, area.Viewport.Height, area.Viewport.MinDepth, area.Viewport.MaxDepth);
                    break;
                case 'x':
                    area.Models[0].Vertices[0].Color = new System.Numerics.Vector4(area.Models[0].Vertices[0].Color.X + 10);
                    sde.Flush();
                    break;
            }
            sde.Render();
        }

        //private void MainForm_KeyDown(object sender, KeyEventArgs e)
        //{
        //    MessageBox.Show("Key Down");
        //}

        private void pibMain_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show($"Mouse Click({e.X},{e.Y})");
        }
        private void btnScript_Click(object sender, EventArgs e)
        {

        }
    }
}