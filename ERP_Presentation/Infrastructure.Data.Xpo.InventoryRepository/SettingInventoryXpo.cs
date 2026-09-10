//'*************************************************************
//' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 18/09/2014
//'
//' Copyright        : (c) . All rights reserved.
//'*************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using DevExpress.Xpo;
using Infrastructure.CrossCutting.Resources;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.SettingInventory")]
    public class SettingInventoryXpo : XPLiteObject
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

        int fOperatingUnitId;
        [Persistent("OperatingUnitId")]
        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
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

        bool fIVACost;
        public bool IVACost
        {
            get { return fIVACost; }
            set { SetPropertyValue<bool>("IVACost", ref fIVACost, value); }
        }

        bool fCommitmentBudgetInterface;
        public bool CommitmentBudgetInterface
        {
            get { return fCommitmentBudgetInterface; }
            set { SetPropertyValue<bool>("CommitmentBudgetInterface", ref fCommitmentBudgetInterface, value); }
        }

        byte fPurchaseOrderInterface;
        public byte PurchaseOrderInterface
        {
            get { return fPurchaseOrderInterface; }
            set { SetPropertyValue<byte>("PurchaseOrderInterface", ref fPurchaseOrderInterface, value); }
        }

        string fPurchaseOrderURL;
        public string PurchaseOrderURL
        {
            get { return fPurchaseOrderURL; }
            set { SetPropertyValue<string>("PurchaseOrderURL", ref fPurchaseOrderURL, value); }
        }

        string fPurchaseOrderIdentifier;
        public string PurchaseOrderIdentifier
        {
            get { return fPurchaseOrderIdentifier; }
            set { SetPropertyValue<string>("PurchaseOrderIdentifier", ref fPurchaseOrderIdentifier, value); }
        }

        string fPurchaseOrderUser;
        public string PurchaseOrderUser
        {
            get { return fPurchaseOrderUser; }
            set { SetPropertyValue<string>("PurchaseOrderUser", ref fPurchaseOrderUser, value); }
        }

        string fPurchaseOrderPass;
        public string PurchaseOrderPass
        {
            get { return fPurchaseOrderPass; }
            set { SetPropertyValue<string>("PurchaseOrderPass", ref fPurchaseOrderPass, value); }
        }

        byte fTaxRegistration;
        public byte TaxRegistration
        {
            get { return fTaxRegistration; }
            set { SetPropertyValue<byte>("TaxRegistration", ref fTaxRegistration, value); }
        }

        #endregion

        #region Builders

        public SettingInventoryXpo(Session session) : base(session)
        {
        }

        public SettingInventoryXpo()
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
