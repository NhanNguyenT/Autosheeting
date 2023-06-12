using Autodesk.Revit.DB;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Utility;

namespace Model.Data
{
    public static class ViewPlan2PortCreation_DataUtil
    {
        private static RevitData revitData => RevitData.Instance;

       
        public static void Dispose(this ViewPlan2PortCreation_Data q)
        {
            RevitDataUtil.Dispose();
            ViewPlan2PortCreation_Data.Instance = null;
        }
        //
        public static List<ViewPlan> GetViewPlans(this ViewPlan2PortCreation_Data q) 
        {
            var doc = revitData.Document;
            return new FilteredElementCollector(doc).OfClass(typeof(ViewPlan)).Cast<ViewPlan>().Where(x =>x.ViewType == ViewType.FloorPlan)
                .ToList();
        }
        public static List<FamilySymbol> GetTitleBlockTypes(this ViewPlan2PortCreation_Data q)
        {
            var doc = revitData.Document;
            return new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_TitleBlocks).Cast<FamilySymbol>().ToList();
        }
    }
}
