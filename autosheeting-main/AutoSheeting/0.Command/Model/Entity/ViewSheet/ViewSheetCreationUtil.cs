using Autodesk.Revit.DB;
using Model.Data;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;


namespace Model.Entity.ViewPlan2PortCreationNS
{
    public static class ViewSheetCreationUtil
    {
        private static ViewPlan2PortCreation_Data data => ViewPlan2PortCreation_Data.Instance;
        private static RevitData revitData => RevitData.Instance;
        public static FamilySymbol GetTitleBlockType(this ViewSheetCreation q)
        {
            return data.TitleBlockTypes.FirstOrDefault();
        }

        public static ViewSheet GetViewSheet(this ViewSheetCreation q)
        {
            var doc = revitData.Document;
        

            var titleBlockType = q.TitleBlockType;

            var viewSheet = ViewSheet.Create(doc, titleBlockType.Id);

            viewSheet.Name = q.Name;
            viewSheet.SheetNumber= q.SheetNumber;

            var name = q.GetName();

            if (name !=null)
            {
                viewSheet.Name = name;
            }

            if (q.SheetNumber != null)
            {
                viewSheet.SheetNumber = q.SheetNumber;  
            }

            return viewSheet;
        }
        public static FamilyInstance GetTitleBlock(this ViewSheetCreation q)
        {
            var doc = revitData.Document;
            var viewSheet = q.ViewSheet!;

            return new FilteredElementCollector(doc, viewSheet.Id).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_TitleBlocks)
                 .Cast<FamilyInstance>().First();
        }
    }
}

