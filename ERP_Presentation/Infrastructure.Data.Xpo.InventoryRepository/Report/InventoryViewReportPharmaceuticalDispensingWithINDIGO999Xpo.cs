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
     [Persistent("Inventory.ViewPharmaceuticalDispensing")]
    public class InventoryViewReportPharmaceuticalDispensingWithINDIGO999Xpo : XPLiteObject
    {
        string fid;
        [Key(true)]
        public string id
        {
            get { return fid; }
            set { SetPropertyValue<string>("id", ref fid, value); }
        }

        int fType;
        public int Type
        {
            get { return fType; }
            set { SetPropertyValue<int>("Type", ref fType, value); }
        }

        int fIdPharmaceuticalDispensing;
         public int IdPharmaceuticalDispensing
         {
             get { return fIdPharmaceuticalDispensing; }
             set { SetPropertyValue<int>("IdPharmaceuticalDispensing", ref fIdPharmaceuticalDispensing, value); }
         }
         string fCodigo;
         [Size(20)]
         public string Codigo
         {
             get { return fCodigo; }
             set { SetPropertyValue<string>("Codigo", ref fCodigo, value); }
         }
         string fUnidadOperativa;
         public string UnidadOperativa
         {
             get { return fUnidadOperativa; }
             set { SetPropertyValue<string>("UnidadOperativa", ref fUnidadOperativa, value); }
         }
         string fIngreso;
         [Size(10)]
         public string Ingreso
         {
             get { return fIngreso; }
             set { SetPropertyValue<string>("Ingreso", ref fIngreso, value); }
         }
         string fNombreCompletoPaciente;
         [Size(250)]
         public string NombreCompletoPaciente
         {
             get { return fNombreCompletoPaciente; }
             set { SetPropertyValue<string>("NombreCompletoPaciente", ref fNombreCompletoPaciente, value); }
         }
         DateTime fFechaDispensacion;
         public DateTime FechaDispensacion
         {
             get { return fFechaDispensacion; }
             set { SetPropertyValue<DateTime>("FechaDispensacion", ref fFechaDispensacion, value); }
         }
         string fAfectaInventario;
         [Size(2)]
         public string AfectaInventario
         {
             get { return fAfectaInventario; }
             set { SetPropertyValue<string>("AfectaInventario", ref fAfectaInventario, value); }
         }
         string fEstado;
         [Size(10)]
         public string Estado
         {
             get { return fEstado; }
             set { SetPropertyValue<string>("Estado", ref fEstado, value); }
         }
         string fUsuarioCreado;
         [Size(20)]
         public string UsuarioCreado
         {
             get { return fUsuarioCreado; }
             set { SetPropertyValue<string>("UsuarioCreado", ref fUsuarioCreado, value); }
         }
         DateTime fFechaCreacion;
         public DateTime FechaCreacion
         {
             get { return fFechaCreacion; }
             set { SetPropertyValue<DateTime>("FechaCreacion", ref fFechaCreacion, value); }
         }
         string fIdProducto;
         public string IdProducto
         {
             get { return fIdProducto; }
             set { SetPropertyValue<string>("IdProducto", ref fIdProducto, value); }
         }
        string fProductId;
        public string ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<string>("ProductId", ref fProductId, value); }
        }
        string fProducto;
         [Size(200)]
         public string Producto
         {
             get { return fProducto; }
             set { SetPropertyValue<string>("Producto", ref fProducto, value); }
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
         int fCantidad;
         public int Cantidad
         {
             get { return fCantidad; }
             set { SetPropertyValue<int>("Cantidad", ref fCantidad, value); }
         }
         string fUnidadFuncional;
         [Size(50)]
         public string UnidadFuncional
         {
             get { return fUnidadFuncional; }
             set { SetPropertyValue<string>("UnidadFuncional", ref fUnidadFuncional, value); }
         }


        public InventoryViewReportPharmaceuticalDispensingWithINDIGO999Xpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
