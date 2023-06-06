using Autodesk.Revit.DB;
using Model.Entity;
using Model.Form;
using SingleData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class ViewPlan2PortCreation_Data
    {
        private static ViewPlan2PortCreation_Data? instance;
        public static ViewPlan2PortCreation_Data Instance
        {
            get => instance ??= new ViewPlan2PortCreation_Data();
            set => instance = value;
        }

        //
        private static IOData ioData => IOData.Instance;

        // form
        private ViewPlan2PortCreation_Form? form;
        public ViewPlan2PortCreation_Form Form => this.form ??= new Form.ViewPlan2PortCreation_Form { DataContext = this };

        //
        private List<ViewPlan>? viewPlans;

        public List<ViewPlan> ViewPlans => this.viewPlans ??= this.GetViewPlans();

        // config
        public string ConfigFolder { get; set; } =
#if DEBUG
            @"[DIRECTORY_NAME]\output\Resource";
#elif RELEASE
            Path.Combine(ioData.AssemblyDirectoryPath, "Resource");
#endif

        public string ConfigPath => Path.Combine(this.ConfigFolder, "config.appsetting");

        private CommandConfig? config;
        public CommandConfig Config => this.config ??= ConfigUtil.Get<CommandConfig>(this.ConfigPath);
    }
}
