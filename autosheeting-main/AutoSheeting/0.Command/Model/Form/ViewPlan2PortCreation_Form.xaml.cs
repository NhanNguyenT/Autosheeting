using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model.Data;
using Model.Entity.ViewPlan2PortCreationNS;

namespace Model.Form
{
    /// <summary>
    /// Interaction logic for PM_ProjectUC.xaml
    /// </summary>
    public partial class ViewPlan2PortCreation_Form : System.Windows.Window
    {
        private ViewPlan2PortCreation_Data Data => ViewPlan2PortCreation_Data.Instance;
        public ViewPlan2PortCreation_Form()
        {
            InitializeComponent();
       
        }

        private void run_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
            Data.Creation.Do();
        }
    }
}
