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
    [Persistent("Inventory.ProductType")]
    public class ProductTypeXpo : XPLiteObject
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
        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        int fClass;
        [Persistent("Class")]
        public int Class
        {
            get { return fClass; }
            set { SetPropertyValue<int>("Class", ref fClass, value); }
        }
        
        [PersistentAlias("Iif(Class = 1, 'Grupo', Class = 2, 'Item Medicamento', Class = 3, 'Item Insumo', Class = 4, 'Item Otro', Class=5, 'Item Producción', '')")]
        public string ClassName { get { return Convert.ToString(EvaluateAlias("ClassName")); } }

        byte fTemperatureRange;
        [Persistent("TemperatureRange")]
        public byte TemperatureRange
        {
            get { return fTemperatureRange; }
            set { SetPropertyValue<byte>("TemperatureRange", ref fTemperatureRange, value); }
        }
        //[NonPersistent()]
        //public string ClassName
        //{
        //    get
        //    {
        //        switch (fClass)
        //        { 
        //            case 1:
        //                return ResourceManager.get_GetString("Group");
        //            case 2:
        //                return ResourceManager.get_GetString("MedicationItem");
        //            case 3:
        //                return ResourceManager.get_GetString("InputItem");
        //            case 4:
        //                return ResourceManager.get_GetString("OtherItem");
        //            default:
        //                return string.Empty;
        //        }
        //    }
        //}

        [Association("ProductTypeReferencesAttributeProductType", typeof(AttributeProductTypeXpo))]
        public XPCollection<AttributeProductTypeXpo> AttributeProductTypeXpo
        {
            get { return GetCollection<AttributeProductTypeXpo>("AttributeProductTypeXpo"); }
        }

        [Association("ProductTypeReferencesInventoryProduct", typeof(InventoryProductXpo))]
        public XPCollection<InventoryProductXpo> InventoryProductXpo
        {
            get { return GetCollection<InventoryProductXpo>("InventoryProductXpo"); }
        }

        #endregion

        #region Builders

        public ProductTypeXpo(Session session) : base(session)
        {
        }

        public ProductTypeXpo()
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
