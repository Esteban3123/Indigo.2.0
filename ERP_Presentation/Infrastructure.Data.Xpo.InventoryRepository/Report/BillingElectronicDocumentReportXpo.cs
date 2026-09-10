using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Billing.ElectronicDocument")]
    public class BillingElectronicDocumentReportXpo : XPLiteObject
    {

        #region Properties

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fEntityId;
        public int EntityId
        {
            get { return fEntityId; }
            set { SetPropertyValue<int>("EntityId", ref fEntityId, value); }
        }

        string fEntityName;
        public string EntityName
        {
            get { return fEntityName; }
            set { SetPropertyValue<string>("EntityName", ref fEntityName, value); }
        }

        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        DateTime? fValidationDate;
        public DateTime? ValidationDate
        {
            get { return fValidationDate; }
            set { SetPropertyValue<DateTime?>("ValidationDate", ref fValidationDate, value); }
        }

        #endregion

        #region Custom Properties

        [PersistentAlias("Iif(Status = 0, 'Erronea', Status = 1, 'Registrada', Status = 2, 'Enviada', Status = 3, 'Valida', Status = 4, 'Invalida', 'Procesando')")]
        public string StatusName
        {
            get { return Convert.ToString(this.EvaluateAlias("StatusName")); }
        }

        #endregion

        #region Builders

        public BillingElectronicDocumentReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion
    }
}
