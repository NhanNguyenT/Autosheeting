using Autodesk.Revit.DB;
using Model.Data;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Utility;

namespace Model.Entity
{
    public static class WallDimensionFactoryUtil
    { 
       private static RevitData revitData => RevitData .Instance;

        public static View GetView(this WallDimensionFactory q)
        {
            return revitData.Document.ActiveView;
        }
        public static void Do(this GridDimensionSetFactory q)
        {
            var doc = revitData.Document;
            //using (var transaction = new Transaction(doc, "Create dimension"))
            //{

            //    transaction.Start();

                var wall = q.Wall!;
                var solid = wall.GetEntElement()!.EntSolid.Solid;

                var direction = q.Direction!;

                var faces = solid.Faces.Cast<PlanarFace>()
                    .Where(x=>x.FaceNormal.IsParallel(direction));
                var references = faces.Select(Face => Reference.ParseFromStableRepresentation(doc,$"{wall.UniqueId}:{Face.Id}:SURFACE"));

                var referenceArray = new ReferenceArray();
                foreach (var reference in references)
                {
                    referenceArray.Append(reference);
                }

                var origin = q.Origin;

                var line = Line.CreateBound(origin, origin + direction);

                var dimension = doc.Create.NewDimension(q.view, line, referenceArray);

            //    transaction.Commit();

            //}
        }
    }
}

