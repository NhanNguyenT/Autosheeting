using Autodesk.Revit.DB;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Model.Data;

namespace Model.Entity.ViewPlan2PortCreationNS
{
    public static class ViewPlan2PortCreationUtil
    {
        private static ViewPlan2PortCreation_Data data=> ViewPlan2PortCreation_Data.Instance;
        private static RevitData revitData => RevitData.Instance;

         public static XYZ  GetLocationPoint(this ViewPlan2PortCreation q) 
        {
            var viewSheetCreation = q.ViewSheetCreation;
            var titleBlock = viewSheetCreation.TitleBlock!;
            var viewSheet = viewSheetCreation.ViewSheet!;
            var bb = titleBlock .get_BoundingBox(viewSheet);
            return (bb.Min + bb.Max) * 0.5;
        }
         public static Viewport GetViewport(this ViewPlan2PortCreation q) 
        {
            var doc = revitData.Document;
            var viewSheetCreation = q.ViewSheetCreation;
            var viewSheet = viewSheetCreation.ViewSheet!;
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

                //var viewSheet = q.ViewSheet!;
                try
                { 
                    var viewport = q.Viewport; 
                }
                 catch
                {

                }
                transaction.Commit();

            }
        }
    }
}