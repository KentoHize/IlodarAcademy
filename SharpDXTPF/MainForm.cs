using Aritiafel.IlodarAcademy.SharpDX;
using Aritiafel.IlodarAcademy;

namespace SharpDXTPF
{
    public partial class MainForm : Form
    {
        SharpDXEngine sde;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Ar3DModel model = new Ar3DModel();
            model.Vertices = new ArVertex[3];
            model.Vertices[0] = new ArVertex(0, 0, 0, Color.White);
            model.Vertices[1] = new ArVertex(0, 6, 0, Color.White);
            model.Vertices[2] = new ArVertex(5, 10, 0, Color.White);
            Ar3DArea area = new Ar3DArea();
            area.Models = new Ar3DModel[] { model };

            sde = new SharpDXEngine();
            sde.Load(area);
            sde.LoadPipeline(pibMain.ClientSize.Width, pibMain.ClientSize.Height, pibMain.Handle);
            sde.LoadAssets();
            //sde.LoadAssets2(area);
            sde.Render();
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            sde.Render();
        }
    }
}