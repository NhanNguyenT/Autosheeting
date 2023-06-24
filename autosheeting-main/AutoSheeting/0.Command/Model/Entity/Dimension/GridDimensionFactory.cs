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
        public List<Grid>? Grids { get; set; }

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

