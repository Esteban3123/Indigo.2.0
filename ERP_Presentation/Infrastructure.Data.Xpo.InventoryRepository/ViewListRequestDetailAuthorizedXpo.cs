
using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.ViewListRequestDetailAuthorized")]
    public class ViewListRequestDetailAuthorizedXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fInventoryRequestDetailOtherId;
        public int InventoryRequestDetailOtherId
        {
            get { return fInventoryRequestDetailOtherId; }
            set { SetPropertyValue<int>("InventoryRequestDetailOtherId", ref fInventoryRequestDetailOtherId, value); }
        }

        int fOperatingUnitId;
        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
        }

        int fRequestType;
        public int RequestType
        {
            get { return fRequestType; }
            set { SetPropertyValue<int>("RequestType", ref fRequestType, value); }
        }

        int fStatus;
        public int Status
        {
            get { return fStatus; }
            set { SetPropertyValue<int>("Status", ref fStatus, value); }
        }

        string fRequestTypeName;
        public string RequestTypeName
        {
            get { return fRequestTypeName; }
            set { SetPropertyValue<string>("RequestTypeName", ref fRequestTypeName, value); }
        }

        DateTime fConfirmationDate;
        public DateTime ConfirmationDate
        {
            get { return fConfirmationDate; }
            set { SetPropertyValue<DateTime>("ConfirmationDate", ref fConfirmationDate, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fUserConfirmation;
        public string UserConfirmation
        {
            get { return fUserConfirmation; }
            set { SetPropertyValue<string>("UserConfirmation", ref fUserConfirmation, value); }
        }

        string fItemDescription;
        public string ItemDescription
        {
            get { return fItemDescription; }
            set { SetPropertyValue<string>("ItemDescription", ref fItemDescription, value); }
        }

        string fFunctionUnitCodeName;
        public string FunctionUnitCodeName
        {
            get { return fFunctionUnitCodeName; }
            set { SetPropertyValue<string>("FunctionUnitCodeName", ref fFunctionUnitCodeName, value); }
        }

        string fWarehouseCodeName;
        public string WarehouseCodeName
        {
            get { return fWarehouseCodeName; }
            set { SetPropertyValue<string>("WarehouseCodeName", ref fWarehouseCodeName, value); }
        }

        string fMovementTypeName;
        public string MovementTypeName
        {
            get { return fMovementTypeName; }
            set { SetPropertyValue<string>("MovementTypeName", ref fMovementTypeName, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
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

        public ViewListRequestDetailAuthorizedXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
