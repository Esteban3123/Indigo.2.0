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
    [Persistent(@"Inventory.RemissionDevolution")]
    public class InventoryRemissionDevolutionReportXpo : XPLiteObject
    {

        #region Properties

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fOperatingUnitId;
        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        DateTime fRemissionDate;
        public DateTime RemissionDate
        {
            get { return fRemissionDate; }
            set { SetPropertyValue<DateTime>("RemissionDate", ref fRemissionDate, value); }
        }

        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        byte fDevolutionType;
        public byte DevolutionType
        {
            get { return fDevolutionType; }
            set { SetPropertyValue<byte>("DevolutionType", ref fDevolutionType, value); }
        }

        InventoryWarehouseReportXpo fWarehouseId;
        [Association(@"Inventory_RemissionDevolutionReportXpoReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }

        InventoryRemissionEntranceReportXpo fRemissionEntranceId;
        [Association(@"Inventory_RemissionDevolutionReportXpoReferencesInventory_RemissionEntrance")]
        public InventoryRemissionEntranceReportXpo RemissionEntranceId
        {
            get { return fRemissionEntranceId; }
            set { SetPropertyValue<InventoryRemissionEntranceReportXpo>("RemissionEntranceId", ref fRemissionEntranceId, value); }
        }

        InventoryRemissionOutputReportXpo fRemissionOutputId;
        [Association(@"Inventory_RemissionDevolutionReferencesInventory_RemissionOutput")]
        public InventoryRemissionOutputReportXpo RemissionOutputId
        {
            get { return fRemissionOutputId; }
            set { SetPropertyValue<InventoryRemissionOutputReportXpo>("RemissionOutputId", ref fRemissionOutputId, value); }
        }

        InventoryConsignmentInventoryRemissionReportXpo fConsignmentInventoryRemissionId;
        [Association(@"Inventory_RemissionDevolutionReportXpoReferencesInventory_ConsignmentInventoryRemission")]
        public InventoryConsignmentInventoryRemissionReportXpo ConsignmentInventoryRemissionId
        {
            get { return fConsignmentInventoryRemissionId; }
            set { SetPropertyValue<InventoryConsignmentInventoryRemissionReportXpo>("ConsignmentInventoryRemissionId", ref fConsignmentInventoryRemissionId, value); }
        }

        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        string fCreationUser;
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

        string fConfirmationUser;
        public string ConfirmationUser
        {
            get { return fConfirmationUser; }
            set { SetPropertyValue<string>("ConfirmationUser", ref fConfirmationUser, value); }
        }

        DateTime fConfirmationDate;
        public DateTime ConfirmationDate
        {
            get { return fConfirmationDate; }
            set { SetPropertyValue<DateTime>("ConfirmationDate", ref fConfirmationDate, value); }
        }

        string fAnnulmentUser;
        public string AnnulmentUser
        {
            get { return fAnnulmentUser; }
            set { SetPropertyValue<string>("AnnulmentUser", ref fAnnulmentUser, value); }
        }

        DateTime fAnnulmentDate;
        public DateTime AnnulmentDate
        {
            get { return fAnnulmentDate; }
            set { SetPropertyValue<DateTime>("AnnulmentDate", ref fAnnulmentDate, value); }
        }

        #endregion

        #region Custom Members

        [PersistentAlias("concat(Code,' - ',Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        [PersistentAlias("iif(DevolutionType = 1, 'Remisión Entrada', DevolutionType = 2, 'Remisión Salida', DevolutionType = 3, 'Remisión Inventario en Consignación', 'N/A')")]
        public string DevolutionTypeName
        {
            get { return Convert.ToString(this.EvaluateAlias("DevolutionTypeName")); }
        }

        [PersistentAlias("iif(Status = 1, 'Registrado', Status = 2, 'Confirmado', Status = 3, 'Anulado', 'N/A')")]
        public string StatusName
        {
            get { return Convert.ToString(this.EvaluateAlias("StatusName")); }
        }

        [PersistentAlias("iif(DevolutionType = 1, RemissionEntranceId.Code, DevolutionType = 2, RemissionOutputId.Code, DevolutionType = 3, ConsignmentInventoryRemissionId.Code, 'N/A')")]
        public string RemissionCode
        {
            get { return Convert.ToString(this.EvaluateAlias("RemissionCode")); }
        }

        [PersistentAlias("iif(DevolutionType = 1, RemissionEntranceId.CurrencyAbbreviation, DevolutionType = 2, RemissionOutputId.CurrencyAbbreviation, DevolutionType = 3, ConsignmentInventoryRemissionId.CurrencyAbbreviation, '')")]
        public string CurrencyAbbreviation
        {
            get { return Convert.ToString(this.EvaluateAlias("CurrencyAbbreviation")); }
        }

        [PersistentAlias("iif(DevolutionType = 1, RemissionEntranceId.CurrencyId, DevolutionType = 2, RemissionOutputId.CurrencyId, DevolutionType = 3, ConsignmentInventoryRemissionId.CurrencyId, 0)")]
        public string CurrencyId
        {
            get { return Convert.ToString(this.EvaluateAlias("CurrencyId")); }
        }

        #endregion

        #region Navigation Properties

        [Association(@"Inventory_RemissionDevolutionDetailReferencesInventory_RemissionDevolution", typeof(InventoryRemissionDevolutionDetailReportXpo))]
        public XPCollection<InventoryRemissionDevolutionDetailReportXpo> Inventory_RemissionDevolutionDetails { get { return GetCollection<InventoryRemissionDevolutionDetailReportXpo>("Inventory_RemissionDevolutionDetails"); } }

        #endregion

        #region Builders

        public InventoryRemissionDevolutionReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion

    }
}
