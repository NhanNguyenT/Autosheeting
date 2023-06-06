using Autodesk.Revit.DB;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Model.Entity.ViewPlan2PortCreationNS
{
    public static class ViewPlan2PortCreationUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static ViewSheet GetViewSheet(this ViewPlan2PortCreation q)
        {
            var doc = revitData.Document;
            var titelBlockType = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_TitleBlocks)
                 .Cast<FamilySymbol>().First();
            var viewSheet = ViewSheet.Create(doc, titelBlockType.Id);
            viewSheet.Name = q .ViewPlan!.Name;
            
            return viewSheet;
        }

        public static FamilyInstance GetTitleBlock(this ViewPlan2PortCreation q)
        {
            var doc = revitData.Document;
            var viewSheet = q.ViewSheet!;

            return new FilteredElementCollector(doc, viewSheet.Id).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_TitleBlocks)
                 .Cast<FamilyInstance>().First();
        }
         public static XYZ  GetLocationPoint(this ViewPlan2PortCreation q) 
        {
            var titleBlock = q.TitleBlock!;
            var viewSheet = q.ViewSheet!;
            var bb = titleBlock .get_BoundingBox(viewSheet);
            return (bb.Min + bb.Max) * 0.5;
        }
         public static Viewport GetViewport(this ViewPlan2PortCreation q) 
        {
            var doc = revitData.Document;
            var viewSheet = q.ViewSheet!;
            var viewPlan = q.ViewPlan!;
            var locationPoint = q.LocationPoint!;

            return Viewport.Create (doc, viewSheet.Id , viewPlan.Id, locationPoint); 
        }
         // Method
         public static void Do(this ViewPlan2PortCreation q)
        {
            var doc = revitData.Document;
            using (var transaction = new Transaction(doc, "Apply view plan"))
            {
                transaction.Start();

                var viewport = q.Viewport;
               
                transaction.Commit();

            }
        }
    }
}