using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class GridDimensionFactory
    {
        public XYZ? Direction { get; set; }

        private List<Grid>? grids;

        public List<Grid> Grids
        {
            get => this.grids ??= this.GetGrids();
            set => this.grids = value;
        }

        public XYZ? Origin { get; set; }

        private XYZ? lineDirection;
        public XYZ? LineDirection => this.lineDirection ??= this.GetLineDirection();

        public View? view;

        public View View
        {
            get => this.view ??= this.GetView();
            set => this.view = value;
        }

        private Dimension? partialDimension;

        public Dimension PartialDimension
            => this.partialDimension ??= this.GetPartialDimension();

        //private Dimension totalDimension

        private Dimension? totalDimension;

        public Dimension TotalDimension
            => this.totalDimension ??= this.GetTotalDimension();



    }
}

