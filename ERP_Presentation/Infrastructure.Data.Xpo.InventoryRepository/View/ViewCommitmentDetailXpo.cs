#region "Imports"

using System;
using DevExpress.Xpo;

#endregion

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewCommitmentDetail")]
    public partial class ViewCommitmentDetailXpo : XPLiteObject
    {
        #region "Members"

        string fUUID;
        [Key(true)]
        public string UUID
        {
            get { return fUUID; }
            set { SetPropertyValue<string>("UUID", ref fUUID, value); }
        }

        string fEntityName;
        public string EntityName
        {
            get { return fEntityName; }
            set { SetPropertyValue<string>("EntityName", ref fEntityName, value); }
        }

        string fEntityCode;
        public string EntityCode
        {
            get { return fEntityCode; }
            set { SetPropertyValue<string>("EntityCode", ref fEntityCode, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fDocument;
        public string Document
        {
            get { return fDocument; }
            set { SetPropertyValue<string>("Document", ref fDocument, value); }
        }

        int fCommitmentDetailId;
        public int CommitmentDetailId
        {
            get { return fCommitmentDetailId; }
            set { SetPropertyValue<int>("CommitmentDetailId", ref fCommitmentDetailId, value); }
        }

        string fCategoryCodeName;
        public string CategoryCodeName
        {
            get { return fCategoryCodeName; }
            set { SetPropertyValue<string>("CategoryCodeName", ref fCategoryCodeName, value); }
        }

        string fFinancialSourceCodeName;
        public string FinancialSourceCodeName
        {
            get { return fFinancialSourceCodeName; }
            set { SetPropertyValue<string>("FinancialSourceCodeName", ref fFinancialSourceCodeName, value); }
        }

        string fRevenueTypeCodeName;
        public string RevenueTypeCodeName
        {
            get { return fRevenueTypeCodeName; }
            set { SetPropertyValue<string>("RevenueTypeCodeName", ref fRevenueTypeCodeName, value); }
        }

        decimal fBalance;
        public decimal Balance
        {
            get { return fBalance; }
            set { SetPropertyValue<decimal>("Balance", ref fBalance, value); }
        }

        #endregion

        #region Builders

        public ViewCommitmentDetailXpo(Session session) : base(session)
        {
        }

        public ViewCommitmentDetailXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
