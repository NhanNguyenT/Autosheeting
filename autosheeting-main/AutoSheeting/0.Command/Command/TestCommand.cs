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
using Autodesk.Revit.UI.Selection;
using Model.Entity;

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public class TestCommand4 : RevitCommand
    {
        private ViewPlan2PortCreation_Data data => ViewPlan2PortCreation_Data.Instance;

        public override void Execute()
        {
            var form = data.Form;
            form.ShowDialog();
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class TestCommand1 : RevitCommand
    {
        public override void Execute()
        {
            //var grid1 = sel.PickElement<Grid>();
            //var grid2 = sel.PickElement<Grid>();

            var gridRef1 = sel.PickObject(ObjectType.Element);
            var gridRef2 = sel.PickObject(ObjectType.Element);
            var gridRef3 = sel.PickObject(ObjectType.Element);

            using (var transaction = new Transaction(doc, "Create dimension"))
            {
                transaction.Start();

                var pickPoint = sel.PickPoint();
                var line = Line.CreateUnbound(pickPoint, pickPoint + XYZ.BasisX);

                var referenceArray = new ReferenceArray();
                referenceArray.Append(gridRef1);
                referenceArray.Append(gridRef2);
                referenceArray.Append(gridRef3);

                doc.Create.NewDimension(doc.ActiveView, line, referenceArray);

                transaction.Commit();

            }
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class TestCommand2 : RevitCommand
    {
        public override void Execute()
        {
            var factory = new GridDimensionFactory
            {
                Grids = sel.PickElement<Grid>().ToList(),
                Origin = sel.PickPoint() 
            };
            factory.Do();
        }
    }
}
 