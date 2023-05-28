using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aritiafel.IlodarAcademy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Realm realm;
        public MainWindow()
        {
            InitializeComponent();
            //cavMain.Background = Brushes.Black;

            Viewport3D view3D = new Viewport3D();
            realm = new Realm(view3D);
            realm.CreateSample();

            //grdMain.Children.Add(view3D);
            Grid aGrid = new Grid();
            aGrid.Width = 400;
            aGrid.Height = 400;
            dplMain.Height = grdMain.Height;
            dplMain.Width = grdMain.Width;
            aGrid.Children.Add(view3D);
            //grdMain.Children.Add(aGrid);
            dplMain.Children.Add(aGrid);


            //grdInner.Children.Add(view3D);
            //dplMain.Children.Add(grdInner);
            //vibMain.SetCurrentValue

            //grdMain.Children.Add(view3D);
            //vibMain.Join
            //vibMain.Child.SetValue(this, view3D);
            //ContentPresenter cp = new ContentPresenter();
            //cp.Content = view3D;
         

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //cavMain.Background = Brushes.Black;

            //Canvas cv = this.Content as Canvas;
            //view3DChildren.
            //Content = realm.Viewport;
            //this.reload
            //MessageBox.Show(Content.ToString());
            UpdateLayout();
        }
    }
}
