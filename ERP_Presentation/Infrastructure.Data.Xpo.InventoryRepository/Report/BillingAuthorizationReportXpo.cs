using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Billing.BillingAuthorization")]
    public class BillingAuthorizationReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        //[Indexed(Name = @"IX_BillingAuthorization", Unique = true)]
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
        [Size(5)]
        public string InvoicePrefix
        {
            get { return fInvoicePrefix; }
            set { SetPropertyValue<string>("InvoicePrefix", ref fInvoicePrefix, value); }
        }
        long fInitialInvoice;
        public long InitialInvoice
        {
            get { return fInitialInvoice; }
            set { SetPropertyValue<long>("InitialInvoice", ref fInitialInvoice, value); }
        }
        long fFinalInvoice;
        public long FinalInvoice
        {
            get { return fFinalInvoice; }
            set { SetPropertyValue<long>("FinalInvoice", ref fFinalInvoice, value); }
        }
        byte fInvoiceType;
        public byte InvoiceType
        {
            get { return fInvoiceType; }
            set { SetPropertyValue<byte>("InvoiceType", ref fInvoiceType, value); }
        }
        long fConsecutive;
        public long Consecutive
        {
            get { return fConsecutive; }
            set { SetPropertyValue<long>("Consecutive", ref fConsecutive, value); }
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
        [Association(@"Inventory_DocumentInvoiceProductSalesReferencesBilling_BillingAuthorization", typeof(InventoryDocumentInvoiceProductSalesReportXpo))]
        public XPCollection<InventoryDocumentInvoiceProductSalesReportXpo> Inventory_DocumentInvoiceProductSaless { get { return GetCollection<InventoryDocumentInvoiceProductSalesReportXpo>("Inventory_DocumentInvoiceProductSaless"); } }

        public BillingAuthorizationReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
