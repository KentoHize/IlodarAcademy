using Aritiafel.IlodarAcademy.SharpDX;

namespace SharpDXTPF
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {   
            SharpDXEngine sde = new SharpDXEngine();
            sde.LoadPipeline(pibMain.ClientSize.Width, pibMain.ClientSize.Height, pibMain.Handle);
            sde.LoadAssets();
            sde.PopulateCommandList();
            sde.WaitForPreviousFrame();
        }

    }
}