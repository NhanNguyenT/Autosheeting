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
    public static class WallDimensionSetFactoryUtil
    { 
       private static RevitData revitData => RevitData .Instance;

        public static View GetView(this WallDimensionSetFactory q)
        {
            return revitData.Document.ActiveView;
        }

        public static XYZ GetOrigin(this WallDimensionSetFactory q)
        {
            var view = q.view!;
            var boundingbox = q.Wall!.get_BoundingBox(q.View!);
            var minpoint = boundingbox.Min;
            var maxpoint = boundingbox.Max;

            var origin = new XYZ(maxpoint.X, maxpoint.Y, maxpoint.Z);

            var offset = 800.0.milimeter2Feet();

            return origin + view.RightDirection + offset - view.UpDirection + offset;
        }

        public static View GetWidthFactory(this WallDimensionSetFactory q)
        {
            var factory = new WallDimensionSetFactory
            {
                Wall = q.Wall,
                Direction = q.Wall!.Orientation,
                Origin = q.Origin,
            };
            return factory;
        }
        public static View GetLengthFactory(this WallDimensionSetFactory q)
        {
            var factory = new WallDimensionSetFactory
            {
                Wall = q.Wall,
                Direction = ((q.Wall!.Location as LocationCurve)!.Curve as Line)!.Direction,
                Origin = q.Origin,
            };
            return factory;
        }
        public static void Do(this WallDimensionSetFactory q)
        {
            var doc = revitData.Document;
            using (var transaction = new Transaction(doc, "Create dimension"))
            {

                transaction.Start();

                var widthFactory = q.WithFactory;
                widthFactory.Do();

                var lengthFactory = q.LengthFactory;
                lengthFactory.Do();

                transaction.Commit();

            }
        }
    }
}

