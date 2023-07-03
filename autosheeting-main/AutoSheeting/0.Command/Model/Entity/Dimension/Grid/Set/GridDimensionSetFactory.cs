using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class GridDimensionSetFactory
    {
        public View? view;

        public View View
        {
            get => this.view ??= this.GetView();
            set => this.view = value;
        }
        private XYZ? origin;

        public XYZ Origin => this.origin ??= this.GetOrigin();

        private GridDimensionFactory? basisX_Factory;
        public GridDimensionFactory BasisX_Factory =>this.basisX_Factory ??= this.GetBasisX_Factory();

        private GridDimensionFactory? basisY_Factory;
        public GridDimensionFactory BasisY_Factory => this.basisY_Factory ??= this.GetBasisY_Factory();
    }
}

