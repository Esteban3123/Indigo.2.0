using DevExpress.Xpo;
using Infrastructure.Data.Xpo.PayrollRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Xpo.InventoryRepository.Relation
{
    [Persistent(@"Payroll.FunctionalUnit")]
    public class PayrollFunctionalUnitXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }


        string fCode;
        [Indexed(Name = "IX_FunctionalUnit", Unique = true)]
        [Size(3)]
        [Persistent("Code")]
        public string Codigo
        {
            get => fCode;
            set => SetPropertyValue<string>("Code", ref fCode, value);
        }

        string fName;
        [Size(50)]
        [Persistent("Name")]
        public string Descripcion
        {
            get => fName;
            set => SetPropertyValue<string>("Name", ref fName, value);
        }

        int fAccountingStructureId;
        public int AccountingStructureId
        {
            get => fAccountingStructureId;
            set => SetPropertyValue<int>("AccountingStructureId", ref fAccountingStructureId, value);
        }

        bool fState;
        public bool State
        {
            get => fState;
            set => SetPropertyValue<bool>("State", ref fState, value);
        }

        byte fUnitType;
        public byte UnitType
        {
            get => fUnitType;
            set => SetPropertyValue<byte>("UnitType", ref fUnitType, value);
        }

        bool fSelectOption;
        [NonPersistent]
        public bool SelectOption
        {
            get => fSelectOption;
            set => fSelectOption = value;
        }

        [Size(50)]
        [PersistentAlias("concat(concat(Codigo,' - '),Descripcion)")]
        public string CodeDescription
        {
            get { return (string)EvaluateAlias("CodeDescription"); }
        }

        [Association("Inventory_PurchaseOrderReferencesCommon_FunctionalUnitRequest", typeof(InventoryPurchaseOrderReportXpo))]
        public XPCollection<InventoryPurchaseOrderReportXpo> PurchaseOrderReferencesCommon_FunctionalUnitRequest => GetCollection<InventoryPurchaseOrderReportXpo>("PurchaseOrderReferencesCommon_FunctionalUnitRequest");

        public PayrollFunctionalUnitXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }



    }
}
