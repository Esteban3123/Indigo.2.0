using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Contract.CUPSEntity")]
    public class CupsEntityXpo : XPLiteObject
    {
        private int fId;

        [Key(true)]
        [Persistent("Id")]
        public int Id
        {
            get
            {
                return fId;
            }
            set
            {
                SetPropertyValue<int>("Id",ref fId, value);
            }
        }

        private string fCode;

        [Size(20)]
        [Persistent("Code")]
        public string Code
        {
            get
            {
                return fCode;
            }
            set
            {
                SetPropertyValue<string>("Code",ref fCode, value);
            }
        }

        private string fDescription;

        [Size(300)]
        [Persistent("Description")]
        public string Description
        {
            get
            {
                return fDescription;
            }
            set
            {
                SetPropertyValue<string>("Description", ref fDescription, value);
            }
        }

        private int fServiceType;

        [Size(20)]
        public int ServiceType
        {
            get
            {
                return fServiceType;
            }
            set
            {
                SetPropertyValue<int>("ServiceType", ref fServiceType, value);
            }
        }

        private string fRIPSCode;

        [Size(20)]
        [Persistent("RIPSCode")]
        public string RIPSCode
        {
            get
            {
                return fRIPSCode;
            }
            set
            {
                SetPropertyValue<string>("RIPSCode",ref fRIPSCode, value);
            }
        }

        private string fRIPSDescription;

        [Size(300)]
        [Persistent("RIPSDescription")]
        public string RIPSDescription
        {
            get
            {
                return fRIPSDescription;
            }
            set
            {
                SetPropertyValue<string>("RIPSDescription",ref fRIPSDescription, value);
            }
        }

        private bool fApplyRIAS;

        public bool ApplyRIAS
        {
            get
            {
                return fApplyRIAS;
            }
            set
            {
                SetPropertyValue<bool>("ApplyRIAS",ref fApplyRIAS, value);
            }
        }

        private bool foxigenService;

        public bool OxigenService
        {
            get
            {
                return foxigenService;
            }
            set
            {
                SetPropertyValue<bool>("OxigenService", ref foxigenService, value);
            }
        }

        // columna que devuelve el nit y el nombre concatenado
        [Size(50)]
        [PersistentAlias("concat(concat(Code,' - '),Description)")]
        public string CodeDescription
        {
            get
            {
                return Convert.ToString(this.EvaluateAlias("CodeDescription"));
            }
        }

        private int fCUPSSubGroupId;

        public int CUPSSubGroupId
        {
            get
            {
                return fCUPSSubGroupId;
            }
            set
            {
                SetPropertyValue<int>("CUPSSubGroupId",ref fCUPSSubGroupId, value);
            }
        }
    

        private bool fStatus;

        [Persistent("Status")]
        public bool Status
        {
            get
            {
                return fStatus;
            }
            set
            {
                SetPropertyValue<bool>("Status",ref fStatus, value);
            }
        }

        private bool fSelectOption;

        [NonPersistent()]
        public bool SelectOption
        {
            get
            {
                return fSelectOption;
            }
            set
            {
                fSelectOption = value;
            }
        }
      
        [Association("ProductRateDetailReferencesCUPS", typeof(ProductRateDetailXpo))]
        public XPCollection<ProductRateDetailXpo> ProductRateDetail
        {
            get
            {
                return GetCollection<ProductRateDetailXpo>("ProductRateDetail");
            }
        }

        public CupsEntityXpo(Session session) : base(session)
        {
        }

        public CupsEntityXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}