//'*************************************************************
//' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 05-04-2014
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

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.ProductSubGroup")]
    public class ProductSubGroupXpo : XPLiteObject
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

        string fCode;
        [Size(20)]
        [Persistent("Code")]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fName;
        [Size(100)]
        [Persistent("Name")]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        byte fStatus;
        [Persistent("Status")]
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        [Size(50)]
        [PersistentAlias("concat(Code,' - ',Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }
        [Association(@"ProductSubGroup", typeof(InventoryProductXpo))]
        public XPCollection<InventoryProductXpo> InventoryProductXpo { get { return GetCollection<InventoryProductXpo>("InventoryProductXpo"); } }

        bool fHandlesBatch;
        [Persistent("HandlesBatch")]
        public bool HandlesBatch
        {
            get { return fHandlesBatch; }
            set { SetPropertyValue<bool>("HandlesBatch", ref fHandlesBatch, value); }
        }

        bool fHandlesExpiry;
        [Persistent("HandlesExpiry")]
        public bool HandlesExpiry
        {
            get { return fHandlesExpiry; }
            set { SetPropertyValue<bool>("HandlesExpiry", ref fHandlesExpiry, value); }
        }

        bool fSelectOption;
        [NonPersistent()]
        public bool SelectOption
        {
            get { return fSelectOption; }
            set { SetPropertyValue<bool>("SelectOption", ref fSelectOption, value); }
        }

        #endregion

        #region Builders

        public ProductSubGroupXpo(Session session) : base(session)
        {
        }

        public ProductSubGroupXpo()
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
