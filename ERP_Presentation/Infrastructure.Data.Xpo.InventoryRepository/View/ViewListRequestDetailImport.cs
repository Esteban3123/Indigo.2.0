#region "Imports"

using DevExpress.Xpo;
using System;

#endregion

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewListRequestDetailImport")]
    public partial class ViewListRequestDetailImportXpo : XPLiteObject
    {
        #region "Members"

        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        int fInventoryRequestDetailType;
        public int InventoryRequestDetailType
        {
            get { return fInventoryRequestDetailType; }
            set { SetPropertyValue<int>("InventoryRequestDetailType", ref fInventoryRequestDetailType, value); }
        }

        int fRow;
        [Key(true)]
        public int Row
        {
            get { return fRow; }
            set { SetPropertyValue<int>("Row", ref fRow, value); }
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

        int fEntityId;
        public int EntityId
        {
            get { return fEntityId; }
            set { SetPropertyValue<int>("EntityId", ref fEntityId, value); }
        }

        string fSourceCode;
        public string SourceCode
        {
            get { return fSourceCode; }
            set { SetPropertyValue<string>("SourceCode", ref fSourceCode, value); }
        }

        string fSourceCodeName;
        public string SourceCodeName
        {
            get { return fSourceCodeName; }
            set { SetPropertyValue<string>("SourceCodeName", ref fSourceCodeName, value); }
        }

        int fQuantityRequested;
        public int QuantityRequested
        {
            get { return fQuantityRequested; }
            set { SetPropertyValue<int>("QuantityRequested", ref fQuantityRequested, value); }
        }

        int fQuantityDelivered;
        public int QuantityDelivered
        {
            get { return fQuantityDelivered; }
            set { SetPropertyValue<int>("QuantityDelivered", ref fQuantityDelivered, value); }
        }

        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }

        string fDescriptionProduct;
        public string DescriptionProduct
        {
            get { return fDescriptionProduct; }
            set { SetPropertyValue<string>("DescriptionProduct", ref fDescriptionProduct, value); }
        }

        int fTargetWarehouseId;
        public int TargetWarehouseId
        {
            get { return fTargetWarehouseId; }
            set { SetPropertyValue<int>("TargetWarehouseId", ref fTargetWarehouseId, value); }
        }

        int fTargetFunctionalUnitId;
        public int TargetFunctionalUnitId
        {
            get { return fTargetFunctionalUnitId; }
            set { SetPropertyValue<int>("TargetFunctionalUnitId", ref fTargetFunctionalUnitId, value); }
        }

        int fSourceWarehouseId;
        public int SourceWarehouseId
        {
            get { return fSourceWarehouseId; }
            set { SetPropertyValue<int>("SourceWarehouseId", ref fSourceWarehouseId, value); }
        }

        string fCUMSourceCodeName;
        public string CUMSourceCodeName
        {
            get { return fCUMSourceCodeName; }
            set { SetPropertyValue<string>("CUMSourceCodeName", ref fCUMSourceCodeName, value); }
        }

        int fStatus;
        public int Status
        {
            get { return fStatus; }
            set { SetPropertyValue<int>("Status", ref fStatus, value); }
        }

        byte fMovementType;
        public byte MovementType
        {
            get { return fMovementType; }
            set { SetPropertyValue<byte>("MovementType", ref fMovementType, value); }
        }

        #endregion

        #region Builders

        public ViewListRequestDetailImportXpo(Session session) : base(session)
        {
        }

        public ViewListRequestDetailImportXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
