using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.ViewPharmaceutical")]
    public class InventoryViewPharmaceuticalReportXpo : XPLiteObject
    {
        int fid;
        [Key(true)]
        public int id
        {
            get { return fid; }
            set { SetPropertyValue<int>("id", ref fid, value); }
        }
        int fIdPharmaceuticalDispensing;
        public int IdPharmaceuticalDispensing
        {
            get { return fIdPharmaceuticalDispensing; }
            set { SetPropertyValue<int>("IdPharmaceuticalDispensing", ref fIdPharmaceuticalDispensing, value); }
        }
        string fConsecutivo;
        public string Consecutivo
        {
            get { return fConsecutivo; }
            set { SetPropertyValue<string>("Consecutivo", ref fConsecutivo, value); }
        }
        DateTime fCreationDate;
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }
        string fIPCODPACI;
        public string IPCODPACI
        {
            get { return fIPCODPACI; }
            set { SetPropertyValue<string>("IPCODPACI", ref fIPCODPACI, value); }
        }
        string fNUMINGRES;
        public string NUMINGRES
        {
            get { return fNUMINGRES; }
            set { SetPropertyValue<string>("NUMINGRES", ref fNUMINGRES, value); }
        }
        int fIdAlmacen;        
        public int IdAlmacen
        {
            get { return fIdAlmacen; }
            set { SetPropertyValue<int>("IdAlmacen", ref fIdAlmacen, value); }
        }
        string fAlmacen;
        public string Almacen
        {
            get { return fAlmacen; }
            set { SetPropertyValue<string>("Almacen", ref fAlmacen, value); }
        }
        string fCreationUser;
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }
        string fFullname;
        public string Fullname
        {
            get { return fFullname; }
            set { SetPropertyValue<string>("Fullname", ref fFullname, value); }
        }
        string fIdProducto;
        public string IdProducto
        {
            get { return fIdProducto; }
            set { SetPropertyValue<string>("IdProducto", ref fIdProducto, value); }
        }
        int fIdProduct;      
        public int IdProduct
        {
            get { return fIdProduct; }
            set { SetPropertyValue<int>("IdProduct", ref fIdProduct, value); }
        }
        string fProducto;
        public string Producto
        {
            get { return fProducto; }
            set { SetPropertyValue<string>("Producto", ref fProducto, value); }
        }
        DateTime fServiceDate;
        public DateTime ServiceDate
        {
            get { return fServiceDate; }
            set { SetPropertyValue<DateTime>("ServiceDate", ref fServiceDate, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        decimal fTotalSalesPrice;
        public decimal TotalSalesPrice
        {
            get { return fTotalSalesPrice; }
            set { SetPropertyValue<decimal>("TotalSalesPrice", ref fTotalSalesPrice, value); }
        }
        decimal fGrandTotalSalesPrice;
        public decimal GrandTotalSalesPrice
        {
            get { return fGrandTotalSalesPrice; }
            set { SetPropertyValue<decimal>("GrandTotalSalesPrice", ref fGrandTotalSalesPrice, value); }
        }
        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        string fCodeAtc;
        public string CodeAtc
        {
            get { return fCodeAtc; }
            set { SetPropertyValue<string>("CodeAtc", ref fCodeAtc, value); }
        }
        string fConcentration;
        public string Concentration
        {
            get { return fConcentration; }
            set { SetPropertyValue<string>("Concentration", ref fConcentration, value); }
        }
        string fNombreCompletoPaciente;
        public string NombreCompletoPaciente
        {
            get { return fNombreCompletoPaciente; }
            set { SetPropertyValue<string>("NombreCompletoPaciente", ref fNombreCompletoPaciente, value); }
        }
        string fIPPRINOMB;
        public string IPPRINOMB
        {
            get { return fIPPRINOMB; }
            set { SetPropertyValue<string>("IPPRINOMB", ref fIPPRINOMB, value); }
        }
        string fIPSEGNOMB;
        public string IPSEGNOMB
        {
            get { return fIPSEGNOMB; }
            set { SetPropertyValue<string>("IPSEGNOMB", ref fIPSEGNOMB, value); }
        }
        string fIPPRIAPEL;
        public string IPPRIAPEL
        {
            get { return fIPPRIAPEL; }
            set { SetPropertyValue<string>("IPPRIAPEL", ref fIPPRIAPEL, value); }
        }
        string fIPSEGAPEL;
        public string IPSEGAPEL
        {
            get { return fIPSEGAPEL; }
            set { SetPropertyValue<string>("IPSEGAPEL", ref fIPSEGAPEL, value); }
        }
        string fNombreMedico;
        public string NombreMedico
        {
            get { return fNombreMedico; }
            set { SetPropertyValue<string>("NombreMedico", ref fNombreMedico, value); }
        }
        string fOrderedHealthProfessionalCode;
        public string OrderedHealthProfessionalCode
        {
            get { return fOrderedHealthProfessionalCode; }
            set { SetPropertyValue<string>("OrderedHealthProfessionalCode", ref fOrderedHealthProfessionalCode, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fRegimen;
        public string Regimen
        {
            get { return fRegimen; }
            set { SetPropertyValue<string>("Regimen", ref fRegimen, value); }
        }
        string fUnidadProducto;
        public string UnidadProducto
        {
            get { return fUnidadProducto; }
            set { SetPropertyValue<string>("UnidadProducto", ref fUnidadProducto, value); }
        }
        string fUnidadFuncional;
        public string UnidadFuncional
        {
            get { return fUnidadFuncional; }
            set { SetPropertyValue<string>("UnidadFuncional", ref fUnidadFuncional, value); }
        }
        public InventoryViewPharmaceuticalReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
