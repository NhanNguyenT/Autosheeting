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
    public static class GridDimensionFactoryUtil
    { 
        private static RevitData revitData => RevitData.Instance;

        public static View GetView ( this GridDimensionFactory q)
        {
            return revitData.Document.ActiveView;
        }

        public static List<Grid> GetGrids( this GridDimensionFactory q )
        {
            var dir = q.Direction;
            var view = q.View;
            var doc = revitData.Document;

            var grids = new FilteredElementCollector(doc,view.Id).OfClass(typeof(Grid)).Cast<Grid>()
                .Where(X => (X.Curve as Line )!.Direction.IsParallel(dir)).ToList();

            return grids;

        }

        public static XYZ GetLineDirection( this GridDimensionFactory q )
        {
            var grid = q.Grids.First();
            var griDirection = ( grid.Curve as Line)!.Direction;

            return griDirection.IsParallel(XYZ.BasisX) ? XYZ.BasisY : XYZ.BasisX;
        }

        public static Dimension GetPartialDimension(this GridDimensionFactory q)
        {
            var doc = revitData.Document;   

            var grids = q.Grids;

            var origin = q.Origin;
            var dir = q.LineDirection;
            var line = Line.CreateBound(origin,origin + dir);


            var references = grids.Select(grid => new Reference(grid));

            var referenceArray = new ReferenceArray();
            foreach ( var reference in references)
            {
                referenceArray.Append(reference);

            }

            var dimension = doc.Create.NewDimension(q.view,line,referenceArray );
            return dimension;
        }

        public static Dimension GetTotalDimension(this GridDimensionFactory q)
        {
            var doc = revitData.Document;

            var grids = q.Grids!;

            Grid? startGrid = null;
            Grid? endGrid = null;
            double? maxDistance = null;

            for (int i = 0; i < grids.Count -1; i++)
            {
                var i_grid = grids[i];
                var i_line = (i_grid.Curve as Line)!;

                for (int j = i + 1; j < grids.Count; j++)
                {
                    var j_grid = grids[j];
                    var j_line = (j_grid.Curve as Line)!;
                    var j_startPoint = j_line.GetEndPoint(0);

                    var projectPoint = i_line.GetProjectPoint(j_startPoint);
                    var distance = (j_startPoint - projectPoint).GetLength();

                    if (maxDistance == null || distance > maxDistance)
                    {
                        startGrid= i_grid;
                        endGrid= j_grid;
                        maxDistance= distance;
                    }
                }

            }
            var dir = q.LineDirection;
            var offVector = dir.IsParallel(XYZ.BasisX) ? -XYZ.BasisY : -XYZ.BasisZ;

            var origin = q.Origin + offVector * 1000.0.milimeter2Feet();
            
            var line = Line.CreateBound(origin, origin + dir);
 

            var referencearray = new ReferenceArray();
            referencearray.Append(new Reference(startGrid));
            referencearray.Append(new Reference(endGrid));


            var dimension = doc.Create.NewDimension(q.view, line, referencearray);
            return dimension;
        }

        // do
        public static void Do(this GridDimensionFactory q )
        {
            //var doc = revitData.Document;
            //using (var transaction = new Transaction (doc,"Create dimension"))
            //{
                
            //    transaction.Start();

                var paritalDimension = q.PartialDimension;
                var totalDimension = q.TotalDimension;

                //transaction.Commit();

            //}
        }
    }
}

