using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Payments.DocumentSupport")]
    public class InventoryDocumentSupportReport : XPLiteObject
    {

        #region Properties

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
        [Size(100)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fResolutionNumber;
        [Size(50)]
        public string ResolutionNumber
        {
            get { return fResolutionNumber; }
            set { SetPropertyValue<string>("ResolutionNumber", ref fResolutionNumber, value); }
        }
        DateTime fResolutionDate;
        public DateTime ResolutionDate
        {
            get { return fResolutionDate; }
            set { SetPropertyValue<DateTime>("ResolutionDate", ref fResolutionDate, value); }
        }
        string fInvoicePrefix;
        [Size(6)]
        public string InvoicePrefix
        {
            get { return fInvoicePrefix; }
            set { SetPropertyValue<string>("InvoicePrefix", ref fInvoicePrefix, value); }
        }
        int fInitialDocument;
        public int InitialDocument
        {
            get { return fInitialDocument; }
            set { SetPropertyValue<int>("InitialDocument", ref fInitialDocument, value); }
        }
        int fFinalDocument;
        public int FinalDocument
        {
            get { return fFinalDocument; }
            set { SetPropertyValue<int>("FinalDocument", ref fFinalDocument, value); }
        }
        byte fDocumentType;
        public byte DocumentType
        {
            get { return fDocumentType; }
            set { SetPropertyValue<byte>("DocumentType", ref fDocumentType, value); }
        }
        int fConsecutive;
        public int Consecutive
        {
            get { return fConsecutive; }
            set { SetPropertyValue<int>("Consecutive", ref fConsecutive, value); }
        }
        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
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
        string fTechnicalKey;
        [Size(500)]
        public string TechnicalKey
        {
            get { return fTechnicalKey; }
            set { SetPropertyValue<string>("TechnicalKey", ref fTechnicalKey, value); }
        }
        DateTime fInitialDate;
        public DateTime InitialDate
        {
            get { return fInitialDate; }
            set { SetPropertyValue<DateTime>("InitialDate", ref fInitialDate, value); }
        }
        DateTime fFinalDate;
        public DateTime FinalDate
        {
            get { return fInitialDate; }
            set { SetPropertyValue<DateTime>("FinalDate", ref fFinalDate, value); }
        }
        #endregion
        #region Custom Properties

        [PersistentAlias("Iif(DocumentType = 1, 'POR COMPUTADOR', 'ELECTRÓNICA')")]
        public string DocumentTypeName
        {
            get { return Convert.ToString(this.EvaluateAlias("DocumentTypeName")); }
        }

        #endregion

        #region Association

        [Association(@"InventoryDocumentSupportReportXpoReferencesInventoryPaymentAccountPayableReport", typeof(InventoryAccountPayableDocumentSupportReportXpo))]
        public XPCollection<InventoryAccountPayableDocumentSupportReportXpo> AccountPayableDocumentSupports { get { return GetCollection<InventoryAccountPayableDocumentSupportReportXpo>("AccountPayableDocumentSupports"); } }

        #endregion

        #region Builders

        public InventoryDocumentSupportReport(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #endregion
    }
}
