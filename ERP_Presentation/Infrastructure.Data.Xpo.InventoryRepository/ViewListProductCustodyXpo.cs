


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.ViewListProductCustody")]
    public class ViewListProductCustodyXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }


        string fProductCode;
        public string ProductCode
        {
            get { return fProductCode; }
            set { SetPropertyValue<string>("ProductCode", ref fProductCode, value); }
        }

        string fProductName;
        public string ProductName
        {
            get { return fProductName; }
            set { SetPropertyValue<string>("ProductName", ref fProductName, value); }
        }


        string fPharmaceuticalForm;
        public string PharmaceuticalForm
        {
            get { return fPharmaceuticalForm; }
            set { SetPropertyValue<string>("PharmaceuticalForm", ref fPharmaceuticalForm, value); }
        }


        string fBatchCode;
        public string BatchCode
        {
            get { return fBatchCode; }
            set { SetPropertyValue<string>("BatchCode", ref fBatchCode, value); }
        }

        DateTime fExpirationDate;
        public DateTime ExpirationDate
        {
            get { return fExpirationDate; }
            set { SetPropertyValue<DateTime>("ExpirationDate", ref fExpirationDate, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        string fAdmissionNumber;
        public string AdmissionNumber
        {
            get { return fAdmissionNumber; }
            set { SetPropertyValue<string>("AdmissionNumber", ref fAdmissionNumber, value); }
        }

        string fPaciente;
        public string Paciente
        {
            get { return fPaciente; }
            set { SetPropertyValue<string>("Paciente", ref fPaciente, value); }
        }

        string fWarehouseCode;
        public string WarehouseCode
        {
            get { return fWarehouseCode; }
            set { SetPropertyValue<string>("WarehouseCode", ref fWarehouseCode, value); }
        }

        string fWareHouseName;
        public string WareHouseName
        {
            get { return fWareHouseName; }
            set { SetPropertyValue<string>("WareHouseName", ref fWareHouseName, value); }
        }

        public ViewListProductCustodyXpo(Session session) : base(session) { }

    }
}
