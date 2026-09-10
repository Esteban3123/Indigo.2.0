#region "Imports"

using DevExpress.Xpo;
using System;

#endregion

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewClosedMonthConciliationMovements")]
    public class ViewClosedMonthConciliationMovementsXpo : XPLiteObject
    {
        #region "Members"

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fYear;
        public int Year
        {
            get { return fYear; }
            set { SetPropertyValue<int>("Year", ref fYear, value); }
        }

        int fMonth;
        public int Month
        {
            get { return fMonth; }
            set { SetPropertyValue<int>("Month", ref fMonth, value); }
        }

        string fEntityName;
        public string EntityName
        {
            get { return fEntityName; }
            set { SetPropertyValue<string>("EntityName", ref fEntityName, value); }
        }

        string fAccountNumber;
        public string AccountNumber
        {
            get { return fAccountNumber; }
            set { SetPropertyValue<string>("AccountNumber", ref fAccountNumber, value); }
        }

        decimal fTotalDebitAccounting;
        public decimal TotalDebitAccounting
        {
            get { return fTotalDebitAccounting; }
            set { SetPropertyValue<decimal>("TotalDebitAccounting", ref fTotalDebitAccounting, value); }
        }

        decimal fTotalCreditAccounting;
        public decimal TotalCreditAccounting
        {
            get { return fTotalCreditAccounting; }
            set { SetPropertyValue<decimal>("TotalCreditAccounting", ref fTotalCreditAccounting, value); }
        }

        decimal fTotalDebitInventory;
        public decimal TotalDebitInventory
        {
            get { return fTotalDebitInventory; }
            set { SetPropertyValue<decimal>("TotalDebitInventory", ref fTotalDebitInventory, value); }
        }

        decimal fTotalCreditInventory;
        public decimal TotalCreditInventory
        {
            get { return fTotalCreditInventory; }
            set { SetPropertyValue<decimal>("TotalCreditInventory", ref fTotalCreditInventory, value); }
        }

        decimal fDifferenceDebit;
        public decimal DifferenceDebit
        {
            get { return fDifferenceDebit; }
            set { SetPropertyValue<decimal>("DifferenceDebit", ref fDifferenceDebit, value); }
        }

        decimal fDifferenceCredit;
        public decimal DifferenceCredit
        {
            get { return fDifferenceCredit; }
            set { SetPropertyValue<decimal>("DifferenceCredit", ref fDifferenceCredit, value); }
        }

        #endregion

        #region Builders

        public ViewClosedMonthConciliationMovementsXpo(Session session) : base(session)
        {
        }

        public ViewClosedMonthConciliationMovementsXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
