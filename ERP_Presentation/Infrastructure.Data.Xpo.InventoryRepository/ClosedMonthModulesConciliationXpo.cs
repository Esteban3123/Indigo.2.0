using System;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.ClosedMonthModulesConciliation")]
    public class ClosedMonthModulesConciliationXpo : XPLiteObject
    {
        #region Members

        int fId;
        [Key(true)]
        [Persistent("Id")]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fYear;
        [Persistent("Year")]
        public int Year
        {
            get { return fYear; }
            set { SetPropertyValue<int>("Year", ref fYear, value); }
        }

        int fMonth;
        [Persistent("Month")]
        public int Month
        {
            get { return fMonth; }
            set { SetPropertyValue<int>("Month", ref fMonth, value); }
        }

        string fEntityName;
        [Persistent("EntityName")]
        public string EntityName
        {
            get { return fEntityName; }
            set { SetPropertyValue<string>("EntityName", ref fEntityName, value); }
        }

        int fMainAccountId;
        [Persistent("MainAccountId")]
        public int MainAccountId
        {
            get { return fMainAccountId; }
            set { SetPropertyValue<int>("MainAccountId", ref fMainAccountId, value); }
        }

        decimal fTotalDebitAccounting;
        [Persistent("TotalDebitAccounting")]
        public decimal TotalDebitAccounting
        {
            get { return fTotalDebitAccounting; }
            set { SetPropertyValue<decimal>("TotalDebitAccounting", ref fTotalDebitAccounting, value); }
        }

        decimal fTotalCreditAccounting;
        [Persistent("TotalCreditAccounting")]
        public decimal TotalCreditAccounting
        {
            get { return fTotalCreditAccounting; }
            set { SetPropertyValue<decimal>("TotalCreditAccounting", ref fTotalCreditAccounting, value); }
        }

        decimal fTotalDebitInventory;
        [Persistent("TotalDebitInventory")]
        public decimal TotalDebitInventory
        {
            get { return fTotalDebitInventory; }
            set { SetPropertyValue<decimal>("TotalDebitInventory", ref fTotalDebitInventory, value); }
        }

        decimal fTotalCreditInventory;
        [Persistent("TotalCreditInventory")]
        public decimal TotalCreditInventory
        {
            get { return fTotalCreditInventory; }
            set { SetPropertyValue<decimal>("TotalCreditInventory", ref fTotalCreditInventory, value); }
        }

        #endregion

        #region Builders

        public ClosedMonthModulesConciliationXpo(Session session) : base(session)
        {
        }

        public ClosedMonthModulesConciliationXpo()
            : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
