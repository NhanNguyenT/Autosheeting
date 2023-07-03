using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class WallDimensionSetFactory
    {
     
        public Wall? Wall { get; set;}

        private XYZ? origin;
        public XYZ Origin => this.origin ??= this.GetOrigin();

        public View? view;

        public View View
        {
            get => this.view ??= this.GetView();
            set => this.view = value;
        }
        private WallDimensionFactory? withFactory;
        public WallDimensionSetFactory WithFactory => this.withFactory ??= this.GetWidthFactory();

        private WallDimensionFactory? lengthFactory;
        public WallDimensionSetFactory LengthFactory => this.lengthFactory ??= this.GetLengthFactory();


    }
}

