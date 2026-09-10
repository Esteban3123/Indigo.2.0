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
    [Persistent("Inventory.ProductGroup")]
    public class ProductGroupXpo : XPLiteObject
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

        int fGroupClass;
        [Persistent("GroupClass")]
        public int GroupClass
        {
            get { return fGroupClass; }
            set { SetPropertyValue<int>("GroupClass", ref fGroupClass, value); }
        }

        [PersistentAlias("Iif(GroupClass = 1, 'Producto', Iif(GroupClass = 2, 'Servicio', ''))")]
        public string GroupClassName
        {
            get
            {
                return Convert.ToString(this.EvaluateAlias("GroupClassName"));
            }
        }

        int fSubclassCode;
        [Persistent("SubclassCode")]
        public int SubclassCode
        {
            get { return fSubclassCode; }
            set { SetPropertyValue<int>("SubclassCode", ref fSubclassCode, value); }
        }

        [PersistentAlias("Iif(SubclassCode = 1, 'Insumos Hospitalarios', Iif(SubclassCode = 2, 'Material Quirurgico', Iif(SubclassCode = 3, 'Medicamentos', '')))")]
        public string SubclassCodeName
        {
            get
            {
                return Convert.ToString(this.EvaluateAlias("SubclassCodeName"));
            }
        }
        decimal fSecurityPercentage;
        public decimal SecurityPercentage
        {
            get { return fSecurityPercentage; }
            set { SetPropertyValue<decimal>("SecurityPercentage", ref fSecurityPercentage, value); }
        }

        int fDeclarantRetentionAccountPayableConceptId;
        [Persistent("DeclarantRetentionAccountPayableConceptId")]
        public int DeclarantRetentionAccountPayableConceptId
        {
            get { return fDeclarantRetentionAccountPayableConceptId; }
            set { SetPropertyValue<int>("DeclarantRetentionAccountPayableConceptId", ref fDeclarantRetentionAccountPayableConceptId, value); }
        }

        int fNotDeclarantRetentionAccountPayableConceptId;
        [Persistent("NotDeclarantRetentionAccountPayableConceptId")]
        public int NotDeclarantRetentionAccountPayableConceptId
        {
            get { return fNotDeclarantRetentionAccountPayableConceptId; }
            set { SetPropertyValue<int>("NotDeclarantRetentionAccountPayableConceptId", ref fNotDeclarantRetentionAccountPayableConceptId, value); }
        }

        bool fSelectOption;
        [NonPersistent()]
        public bool SelectOption
        {
            get { return fSelectOption; }
            set { SetPropertyValue<bool>("SelectOption", ref fSelectOption, value); }
        }
        #endregion

        #region "Navigation Properties"

        [Association(@"ProductGroup", typeof(InventoryProductXpo))]
        public XPCollection<InventoryProductXpo> InventoryProductXpo { get { return GetCollection<InventoryProductXpo>("InventoryProductXpo"); } }
        #endregion

        #region Builders

        public ProductGroupXpo(Session session) : base(session)
        {
        }

        public ProductGroupXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
