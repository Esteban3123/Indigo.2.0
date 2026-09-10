using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.ConsignmentInventoryRemissionDetailControl")]
    public class ConsignmentInventoryRemissionDetailControlXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        ConsignmentInventoryRemissionDetailXpo fConsignmentInventoryRemissionDetail;
        [Association(@"ConsignmentInventoryRemissionDetailXpo_References_ConsignmentInventoryRemissionDetailControlXpo")]
        [Persistent("ConsignmentInventoryRemissionDetailId")]
        public ConsignmentInventoryRemissionDetailXpo ConsignmentInventoryRemissionDetail
        {
            get { return fConsignmentInventoryRemissionDetail; }
            set { SetPropertyValue<ConsignmentInventoryRemissionDetailXpo>("ConsignmentInventoryRemissionDetail", ref fConsignmentInventoryRemissionDetail, value); }
        }

        [PersistentAlias("ConsignmentInventoryRemissionDetail.Id")]
        public int ConsignmentInventoryRemissionDetailId
        {
            get { return Convert.ToInt32(EvaluateAlias("ConsignmentInventoryRemissionDetailId")); }

        }

        int? fBatchSerialId;
        public int? BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<int?>("BatchSerialId", ref fBatchSerialId, value); }
        }

        byte fMovementType;
        public byte MovementType
        {
            get { return fMovementType; }
            set { SetPropertyValue<byte>("MovementType", ref fMovementType, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        decimal fValue;
        public decimal Value
        {
            get { return fValue; }
            set { SetPropertyValue<decimal>("Value", ref fValue, value); }
        }

        int fEntityId;
        public int EntityId
        {
            get { return fEntityId; }
            set { SetPropertyValue<int>("EntityId", ref fEntityId, value); }
        }

        string fEntityCode;
        public string EntityCode
        {
            get { return fEntityCode; }
            set { SetPropertyValue<string>("EntityCode", ref fEntityCode, value); }
        }

        string fEntityName;
        public string EntityName
        {
            get { return fEntityName; }
            set { SetPropertyValue<string>("EntityName", ref fEntityName, value); }
        }

        int fQuantityPendingLegalization;
        public int QuantityPendingLegalization
        {
            get { return fQuantityPendingLegalization; }
            set { SetPropertyValue<int>("QuantityPendingLegalization", ref fQuantityPendingLegalization, value); }
        }

        [PersistentAlias("iif(  EntityName='PharmaceuticalDispensingDevolution',Concat('Dev.Disp.Farmaceutica',' - ',EntityCode)," +
                               "EntityName='PharmaceuticalDispensing',Concat('Disp.Farmaceutica',' - ',EntityCode)," +
                               "EntityName='TransferOrder',Concat('Ord.Traslado',' - ','EntityCode')," +
                               "EntityName='BasicBilling',Concat('Fact.Basica',' - ',EntityCode)," +
                               "EntityName='RemissionDevolution',concat('Dev. Remisión',' - ',EntityCode)," +
                               "EntityName='RemissionEntrance',Concat('Remisión',' - ',EntityCode)," +
                               "EntityName = 'PurchaseOrder',Concat('Ordenes de compra',' - ',EntityCode),'')")]
        public string EntityNameSpanish
        {
            get { return Convert.ToString(EvaluateAlias("EntityNameSpanish")); }

        }

        public ConsignmentInventoryRemissionDetailControlXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
