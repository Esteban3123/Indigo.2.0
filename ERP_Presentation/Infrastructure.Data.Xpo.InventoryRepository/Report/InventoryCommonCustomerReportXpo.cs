using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Common.Customer")]
    public class InventoryCommonCustomerReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fNit;
        //[Indexed(Name = @"IX_Customer", Unique = true)]
        [Size(15)]
        public string Nit
        {
            get { return fNit; }
            set { SetPropertyValue<string>("Nit", ref fNit, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fEPSCode;
        [Size(20)]
        public string EPSCode
        {
            get { return fEPSCode; }
            set { SetPropertyValue<string>("EPSCode", ref fEPSCode, value); }
        }
        InventoryCommonThirdPartyXpo fThirdPartyId;
        [Association(@"InventoryCommonCustomerReportXpoReferencesInventoryCommonThirdPartyXpo")]
        public InventoryCommonThirdPartyXpo ThirdPartyId
        {
            get { return fThirdPartyId; }
            set { SetPropertyValue<InventoryCommonThirdPartyXpo>("ThirdPartyId", ref fThirdPartyId, value); }
        }
        bool fState;
        public bool State
        {
            get { return fState; }
            set { SetPropertyValue<bool>("State", ref fState, value); }
        }

        [PersistentAlias("Iif([State] = True, 'Activo', 'Inactivo')")]
        public string StateName
        {
            get { return Convert.ToString(this.EvaluateAlias("StateName")); }
        }

        [Association(@"InventoryRemissionOutputReportXpoReferencesInventoryCommonCustomerReportXpo", typeof(InventoryRemissionOutputReportXpo))]
        public XPCollection<InventoryRemissionOutputReportXpo> InventoryRemissionOutputReportXpo { get { return GetCollection<InventoryRemissionOutputReportXpo>("InventoryRemissionOutputReportXpo"); } }

        public InventoryCommonCustomerReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }

}
