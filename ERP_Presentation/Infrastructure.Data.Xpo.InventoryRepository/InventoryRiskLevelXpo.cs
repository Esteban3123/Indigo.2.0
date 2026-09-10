//'*************************************************************
//' Assembly         : Infraestructure.Data.Xpo.InventoryRepository
//' Author           : Cristhian Mauricio Salazar
//' Created          : 05-10-2014
//'
//' Copyright        : (c) . All rights reserved.
//'*************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.InventoryRiskLevel")]
    public partial class InventoryRiskLevelXpo : XPLiteObject
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

        [Size(50)]
        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        byte fStatus;
        [Persistent("Status")]
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        [Association("InventoryRiskLevelReferencesInventorySupplie", typeof(InventorySupplieXpo))]
        public XPCollection<InventorySupplieXpo> InventorySupplieXpo
        {
            get { return GetCollection<InventorySupplieXpo>("InventorySupplieXpo"); }
        }
        #endregion

        #region Builders

        public InventoryRiskLevelXpo(Session session) : base(session)
        {
        }

        public InventoryRiskLevelXpo()
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
