using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.ViewReportDevolutionOfRequests")]
    public class InventoryViewReportDevolutionOfRequests : XPLiteObject
    {
        long fId;
        [Key(true)]
        public long Id
        {
            get { return fId; }
            set { SetPropertyValue<long>("Id", ref fId, value); }
        }
        string fCode;
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        string fTipoDevolucion;
        [Size(16)]
        public string TipoDevolucion
        {
            get { return fTipoDevolucion; }
            set { SetPropertyValue<string>("TipoDevolucion", ref fTipoDevolucion, value); }
        }
        string fProducto;
        [Size(223)]
        public string Producto
        {
            get { return fProducto; }
            set { SetPropertyValue<string>("Producto", ref fProducto, value); }
        }
        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        string fCodeRequestDevolution;
        [Size(20)]
        public string CodeRequestDevolution
        {
            get { return fCodeRequestDevolution; }
            set { SetPropertyValue<string>("CodeRequestDevolution", ref fCodeRequestDevolution, value); }
        }
        string fDescription;
        [Size(SizeAttribute.Unlimited)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }
        string fCreationUser;
        [Size(20)]
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }


        public InventoryViewReportDevolutionOfRequests(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
