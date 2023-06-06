using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Model.Form;
using System;
using System.Linq;
using System.Collections.Generic;
using Utility;
using Model.Entity.ViewPlan2PortCreationNS;
using Model.Data;

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public class TestCommand : RevitCommand
    {
        public override void Execute()
        {
            var viewSheet = (doc.ActiveView as ViewSheet)!;
            var viewPlan = 523483.GetElement<ViewPlan>(); 
            
            using (var transaction = new Transaction(doc, "Apply view plan"))
            {
                transaction.Start();

                var titelBlock = new FilteredElementCollector(doc, viewSheet.Id).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_TitleBlocks)
                    .Cast<FamilyInstance>().First();
                var bb = titelBlock.get_BoundingBox(viewSheet);
                var centerPoint = (bb.Min + bb.Max) * 0.5;
               
                Viewport.Create(doc,viewSheet.Id,viewPlan.Id,centerPoint);
                
                transaction.Commit();

            }
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class TestCommand1 : RevitCommand
    {
        public override void Execute()
        {
      

            using (var transaction = new Transaction(doc, "Creat sheet"))
            {
                transaction.Start();

                var titelBlock = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_TitleBlocks)
                 .Cast<FamilySymbol>().First();
                var viewSheet = ViewSheet.Create(doc, titelBlock.Id);
                viewSheet.Name = " Mặt Bằng Bản Vẽ ";
                viewSheet.SheetNumber = "S102";
                transaction.Commit();

            }
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class TestCommand3 : RevitCommand
    {
        public override void Execute()
        {
            var creation = new ViewPlan2PortCreation
            {
                ViewPlan = 523483.GetElement<ViewPlan>(),
                //ViewSheet = (doc.ActiveView as ViewSheet)!,
            };
            creation.Do();
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class TestCommand4 : RevitCommand
    {
        public override void Execute()
        {
            var creation = new ViewPlan2PortCreation
            {
                ViewPlan = 523483.GetElement<ViewPlan>(),
                //ViewSheet = (doc.ActiveView as ViewSheet)!,
            };
            creation.Do();
        }
    }
}
