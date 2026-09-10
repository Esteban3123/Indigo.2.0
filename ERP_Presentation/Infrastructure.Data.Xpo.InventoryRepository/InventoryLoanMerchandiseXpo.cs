using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.LoanMerchandise")]
    public class InventoryLoanMerchandiseXpo : XPLiteObject
    {
        #region Members

        private int fId;

        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        private string fCode;

        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        private DateTime fDocumentDate;

        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }

        private byte fLoanType;

        public byte LoanType
        {
            get { return fLoanType; }
            set { SetPropertyValue<byte>("LoanType", ref fLoanType, value); }
        }

        private WarehouseXpo fWarehouseId;

        [Association(@"Inventory_LoanMerchandise_Warehouse")]
        public WarehouseXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<WarehouseXpo>("WarehouseId", ref fWarehouseId, value); }
        }

        private ThirdPartyXpo fThirdPartyId;

        [Association(@"InventoryLoanMerchandiseXpo_thirdparty")]
        public ThirdPartyXpo ThirdPartyId
        {
            get { return fThirdPartyId; }
            set { SetPropertyValue<ThirdPartyXpo>("ThirdPartyId", ref fThirdPartyId, value); }
        }

        private string fObservation;

        [Size(300)]
        public string Observation
        {
            get { return fObservation; }
            set { SetPropertyValue<string>("Observation", ref fObservation, value); }
        }

        private byte fStatus;

        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        private string fCreationUser;

        [Size(20)]
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }

        private DateTime fCreationDate;

        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }

        private string fModificationUser;

        [Size(20)]
        public string ModificationUser
        {
            get { return fModificationUser; }
            set { SetPropertyValue<string>("ModificationUser", ref fModificationUser, value); }
        }

        private DateTime fModificationDate;

        public DateTime ModificationDate
        {
            get { return fModificationDate; }
            set { SetPropertyValue<DateTime>("ModificationDate", ref fModificationDate, value); }
        }

        private string fConfirmationUser;

        [Size(20)]
        public string ConfirmationUser
        {
            get { return fConfirmationUser; }
            set { SetPropertyValue<string>("ConfirmationUser", ref fConfirmationUser, value); }
        }

        private DateTime fConfirmationDate;

        public DateTime ConfirmationDate
        {
            get { return fConfirmationDate; }
            set { SetPropertyValue<DateTime>("ConfirmationDate", ref fConfirmationDate, value); }
        }

        private string fAnnulmentUser;

        [Size(20)]
        public string AnnulmentUser
        {
            get { return fAnnulmentUser; }
            set { SetPropertyValue<string>("AnnulmentUser", ref fAnnulmentUser, value); }
        }

        private DateTime fAnnulmentDate;

        public DateTime AnnulmentDate
        {
            get { return fAnnulmentDate; }
            set { SetPropertyValue<DateTime>("AnnulmentDate", ref fAnnulmentDate, value); }
        }

        [Association(@"Inventory_LoanMerchandiseDetailReferencesInventory_LoanMerchandise", typeof(InventoryLoanMerchandiseDetailXpo))]
        public XPCollection<InventoryLoanMerchandiseDetailXpo> InventoryLoanMerchandiseDetails { get { return GetCollection<InventoryLoanMerchandiseDetailXpo>("InventoryLoanMerchandiseDetails"); } }

        [Size(200)]
        [PersistentAlias("concat(concat(Code,' - '),ThirdPartyId.CodeName)")]
        public string CodeThirdParty
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeThirdParty")); }
        }

        [PersistentAlias("Iif(Status = 1, 'Registrado', Iif(Status = 2, 'Confirmado', Iif(Status = 3, 'Anulado', '')))")]
        public string StatusName
        {
            get { return Convert.ToString(this.EvaluateAlias("StatusName")); }
        }

        [PersistentAlias("Iif(LoanType = 1, 'Entrada', Iif(LoanType = 2, 'Salida', ''))")]
        public string LoanTypeName
        {
            get { return Convert.ToString(this.EvaluateAlias("LoanTypeName")); }
        }

        #endregion Members

        #region Builders

        public InventoryLoanMerchandiseXpo(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion Builders
    }
}