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
    [Persistent(@"Inventory.ProductSubGroup")]
    public class InventoryProductSubGroupReportXpo : XPLiteObject
    {

        #region "Members"

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        bool fHandlesBatch;
        public bool HandlesBatch
        {
            get { return fHandlesBatch; }
            set { SetPropertyValue<bool>("HandlesBatch", ref fHandlesBatch, value); }
        }
        bool fHandlesExpiry;
        public bool HandlesExpiry
        {
            get { return fHandlesExpiry; }
            set { SetPropertyValue<bool>("HandlesExpiry", ref fHandlesExpiry, value); }
        }
        bool fStatus;
        public bool Status
        {
            get { return fStatus; }
            set { SetPropertyValue<bool>("Status", ref fStatus, value); }
        }
        string fCreationUser;
        [Size(20)]
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }
        DateTime fCreationDate;
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }
        string fModificationUser;
        [Size(20)]
        public string ModificationUser
        {
            get { return fModificationUser; }
            set { SetPropertyValue<string>("ModificationUser", ref fModificationUser, value); }
        }
        DateTime fModificationDate;
        public DateTime ModificationDate
        {
            get { return fModificationDate; }
            set { SetPropertyValue<DateTime>("ModificationDate", ref fModificationDate, value); }
        }
      
        //Propiedad Añadida
        bool fSeleccionado = false;
        [NonPersistent()]
        public bool Seleccionado
        {
            get { return fSeleccionado; }
            set { this.fSeleccionado = value; }
        }

        #endregion

        #region "Custom Members"

        [PersistentAlias("Iif(HandlesBatch, 'Si', 'No')")]
        public string HandlesBatchType{ get { return Convert.ToString(EvaluateAlias("HandlesBatchType")); } }

        
        [PersistentAlias("Iif(HandlesExpiry, 'Si', 'No')")]
        public string HandlesExpiryType { get { return Convert.ToString(EvaluateAlias("HandlesExpiryType")); } }

        #endregion

        [Association(@"InventoryProductReportXpoReferencesInventoryProductSubGroupReportXpo", typeof(InventoryProductReportXpo))]
        public XPCollection<InventoryProductReportXpo> InventoryProductReportXpo { get { return GetCollection<InventoryProductReportXpo>("InventoryProductReportXpo"); } }

        [Association(@"InventortyProdcutSubgroup_InventoryProductRateGenral", typeof(ProductRateGeneralXpo))]
        public XPCollection<ProductRateGeneralXpo> ProductRateGeneralXpo { get { return GetCollection<ProductRateGeneralXpo>("ProductRateGeneralXpo"); } }

        [Association(@"InventortyProdcutSubgroup_WarehouseRestrictedConditionsXpo", typeof(WarehouseRestrictedConditionsXpo))]
        public XPCollection<WarehouseRestrictedConditionsXpo> WarehouseRestrictedConditionsXpo { get { return GetCollection<WarehouseRestrictedConditionsXpo>("WarehouseRestrictedConditionsXpo"); } }

        public InventoryProductSubGroupReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
