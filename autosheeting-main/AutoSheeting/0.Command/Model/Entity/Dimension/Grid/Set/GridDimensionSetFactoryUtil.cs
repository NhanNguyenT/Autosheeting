using Autodesk.Revit.DB;
using Model.Data;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public static class GridDimensionSetFactoryUtil
    { 
        private static RevitData revitData => RevitData.Instance;
        public static View GetView(this GridDimensionSetFactory q)
        {
            return revitData.Document.ActiveView;
        }

        public static XYZ GetOrigin(this GridDimensionSetFactory q)
        {
            var view = q.View;
            var cropBox = view.CropBox;
            var minPoint = cropBox.Min;
            var maxPoint = cropBox.Max;

            var origin = new XYZ(maxPoint.X, minPoint.Y, minPoint.Z);

            return origin;
        }

        public static GridDimensionFactory GetBasisX_Factory(this GridDimensionSetFactory q)
        {
            var factory = new GridDimensionFactory
            {
                Direction = XYZ.BasisX,
                Origin= q.Origin,
                view= q.View,
            };
            return factory;
        }
        public static GridDimensionFactory GetBasisY_Factory(this GridDimensionSetFactory q)
        {
            var factory = new GridDimensionFactory
            {
                Direction = XYZ.BasisY,
                Origin = q.Origin,
                view = q.View,
            };
            return factory;
        }
        //do
        public static void Do(this GridDimensionSetFactory q)
        {
            var doc = revitData.Document;
            using (var transaction = new Transaction(doc, "Create dimension"))
            {

                transaction.Start();

                var basisX_Factory = q.BasisX_Factory;
                basisX_Factory.Do();

                var basisY_Factory = q.BasisX_Factory;
                basisY_Factory.Do();

                transaction.Commit();

            }
        }
    }
}

