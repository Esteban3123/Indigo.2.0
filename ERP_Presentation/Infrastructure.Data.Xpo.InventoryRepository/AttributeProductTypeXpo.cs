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
    [Persistent("Inventory.AttributeProductType")]
    public class AttributeProductTypeXpo : XPLiteObject
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

        ProductTypeXpo fProductTypeId;
        [Association("ProductTypeReferencesAttributeProductType")]
        public ProductTypeXpo ProductTypeId
        {
            get { return fProductTypeId; }
            set { SetPropertyValue<ProductTypeXpo>("ProductTypeId", ref fProductTypeId, value); }
        }

        int fDataType;
        [Persistent("DataType")]
        public int DataType
        {
            get { return fDataType; }
            set { SetPropertyValue<int>("DataType", ref fDataType, value); }
        }

        [PersistentAlias("Iif(DataType = 1, 'Fecha', Iif(DataType = 2, 'Número', Iif(DataType = 3, 'Texto', Iif(DataType = 4, 'Lista de Opciones', ''))))")]
        public string DataTypeName
        {
            get
            {
                return Convert.ToString(this.EvaluateAlias("DataTypeName"));
                //switch (fDataType)
                //{
                //    case 1:
                //        return "Fecha";
                //    case 2:
                //        return "Número";
                //    case 3:
                //        return "Texto";
                //    case 4:
                //        return "Lista de Opciones";
                //}
                //return "";
            }
        }

        #endregion

        #region Builders

        public AttributeProductTypeXpo(Session session) : base(session)
        {
        }

        public AttributeProductTypeXpo()
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
