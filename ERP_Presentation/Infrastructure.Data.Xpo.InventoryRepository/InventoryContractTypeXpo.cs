using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
	[Persistent(@"Inventory.InventoryContractType")]
	public class InventoryContractTypeXpo : XPLiteObject
	{
		public InventoryContractTypeXpo(Session session) : base(session) { }
		public override void AfterConstruction() { base.AfterConstruction(); }

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
		public string Name
		{
			get { return fName; }
			set { SetPropertyValue<string>("Name", ref fName, value); }
		}        
		byte fType;
		public byte Type
		{
			get { return fType; }
			set { SetPropertyValue<byte>("Type", ref fType, value); }
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
        //[NonPersistent]
        //public String TypenName
        //{
        //	get
        //	{
        //		switch (fType)
        //		{
        //			case 1:
        //				return "Fijo";
        //			case 2:
        //				return "Variable";
        //			default:
        //				return string.Empty ;
        //		}
        //	}
        //	set{;}
        //}


        [PersistentAlias("Iif(Type = 1, 'Fijo', Iif(Type = 2, 'Variable', ''))")]
        public string TypenName
        {
            get
            {
                return Convert.ToString(this.EvaluateAlias("TypenName"));
            }
        }

        //[PersistentAlias("concat(concat(concat(concat(TypenName,' - '),Code),' - '),Name)")]
        [PersistentAlias("concat(TypenName,' - ',Code,' - ',Name)")]
        public string CodeName
		{
			get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
		}
		[Association(@"Inventory_InventoryContractReferencesInventory_InventoryContractType", typeof(InventoryContractXpo))]
		public XPCollection<InventoryContractXpo> Inventory_Contract { get { return GetCollection<InventoryContractXpo>("Inventory_Contract"); } }
	}
}
