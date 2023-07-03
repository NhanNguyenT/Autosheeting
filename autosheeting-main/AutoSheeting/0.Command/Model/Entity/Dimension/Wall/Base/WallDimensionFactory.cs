using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class WallDimensionFactory
    {
     
        public Wall? Wall { get; set;}

        public XYZ? Direction { get; set;}

        public XYZ? Origin { get; set; }

        public View? view;

        public View View
        {
            get => this.view ??= this.GetView();
            set => this.view = value;
        }
    }
}

