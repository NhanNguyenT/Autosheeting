using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity.ViewPlan2PortCreationNS
{
    public  class ViewSheetCreation
    {
        private FamilySymbol? titleBlockType;
        public FamilySymbol TitleBlockType
        {
            get => this.titleBlockType ??= this.GetTitleBlockType();
            set => this.titleBlockType = value;
        }
        public string? Name { get; set; }

        private Func<string?>? getName ;

        public Func<string?> GetName
        {
            get => this.getName ??= () => this.Name;
            set => this.getName = value;
        }


        public string? SheetNumber { get; set; }

        private ViewSheet? viewSheet;

        public ViewSheet? ViewSheet
        {
            get => this.viewSheet ??= this.GetViewSheet();
            set => this.viewSheet = value;

        }
        private FamilyInstance? titleBlock;
        public FamilyInstance TitleBlock => this.titleBlock ??= this.GetTitleBlock();
    }
}

