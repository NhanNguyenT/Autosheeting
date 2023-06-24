using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity.ViewPlan2PortCreationNS
{
    public class ViewPlan2PortCreation
    {
        public ViewPlan? ViewPlan { get; set; }

        private ViewSheetCreation? viewSheetCreation;
        public ViewSheetCreation ViewSheetCreation => this.viewSheetCreation ??= new ViewSheetCreation
        {
            GetName = () => this.ViewPlan!.Name
        };
               
        private XYZ? locationPoint;

        public XYZ LocationPoint => this.locationPoint ??= this.GetLocationPoint();

        private Viewport? viewport;

        public Viewport Viewport => this.viewport ??= this.GetViewport();

    }
}
