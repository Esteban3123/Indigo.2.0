#region "Imports"

using DevExpress.Xpo;
using System;

#endregion

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewListRequestDetail")]
    public partial class ViewListRequestDetailXpo : XPLiteObject
    {
        #region "Members"

        string fViewKey;
        [Key(true)]
        public string ViewKey
        {
            get { return fViewKey; }
            set { SetPropertyValue<string>("ViewKey", ref fViewKey, value); }
        }

        int fEntitySource;
        public int EntitySource
        {
            get { return fEntitySource; }
            set { SetPropertyValue<int>("EntitySource", ref fEntitySource, value); }
        }

        int fId;
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
                                 
        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }

        int? fTargetWarehouseId;
        public int? TargetWarehouseId
        {
            get { return fTargetWarehouseId; }
            set { SetPropertyValue<int?>("TargetWarehouseId", ref fTargetWarehouseId, value); }
        }

        int? fTargetFunctionalUnitId;
        public int? TargetFunctionalUnitId
        {
            get { return fTargetFunctionalUnitId; }
            set { SetPropertyValue<int?>("TargetFunctionalUnitId", ref fTargetFunctionalUnitId, value); }
        }

        string fObservation;
        public string Observation
        {
            get { return fObservation; }
            set { SetPropertyValue<string>("Observation", ref fObservation, value); }
        }

        int fStatus;
        public int Status
        {
            get { return fStatus; }
            set { SetPropertyValue<int>("Status", ref fStatus, value); }
        }

        int fComponentType;
        public int ComponentType
        {
            get { return fComponentType; }
            set { SetPropertyValue<int>("ComponentType", ref fComponentType, value); }
        }

        string fComponentTypeName;
        public string ComponentTypeName
        {
            get { return fComponentTypeName; }
            set { SetPropertyValue<string>("ComponentTypeName", ref fComponentTypeName, value); }
        }

        int fItemId;
        public int ItemId
        {
            get { return fItemId; }
            set { SetPropertyValue<int>("ItemId", ref fItemId, value); }
        }

        string fItemDescription;
        public string ItemDescription
        {
            get { return fItemDescription; }
            set { SetPropertyValue<string>("ItemDescription", ref fItemDescription, value); }
        }

        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }

        int fRequestQuantity;
        public int RequestQuantity
        {
            get { return fRequestQuantity; }
            set { SetPropertyValue<int>("RequestQuantity", ref fRequestQuantity, value); }
        }

        #endregion

        #region "Custom Members"

        bool fActivated;
        [NonPersistent]
        public bool Activated
        {
            get { return fActivated; }
            set { fActivated = value; }
        }

        bool fItemInvalid;
        [NonPersistent]
        public bool ItemInvalid
        {
            get { return fItemInvalid; }
            set { fItemInvalid = value; }
        }

        #endregion

        #region Builders

        public ViewListRequestDetailXpo(Session session) : base(session)
        {
        }

        public ViewListRequestDetailXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
