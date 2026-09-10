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
    [Persistent(@"dbo.ViewDashBoardPharmacyReport")]
    public class InventoryDashboardPharmacyReportXpo : XPLiteObject
    {
        public InventoryDashboardPharmacyReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        [Key()]
        public int IdDetalleFarmacia { get; set; }

        string fIngreso;
        [Size(10)]
        public string Ingreso
        {
            get { return fIngreso; }
            set { SetPropertyValue<string>("Ingreso", ref fIngreso, value); }
        }
        string fEntidad;
        [Size(300)]
        public string Entidad
        {
            get { return fEntidad; }
            set { SetPropertyValue<string>("Entidad", ref fEntidad, value); }
        }
        string fCodigoEntidad;
        [Size(20)]
        public string CodigoEntidad
        {
            get { return fCodigoEntidad; }
            set { SetPropertyValue<string>("CodigoEntidad", ref fCodigoEntidad, value); }
        }
        string fCodigoContrato;
        [Size(6)]
        public string CodigoContrato
        {
            get { return fCodigoContrato; }
            set { SetPropertyValue<string>("CodigoContrato", ref fCodigoContrato, value); }
        }
        string fCodigoProducto;
        [Size(20)]
        public string CodigoProducto
        {
            get { return fCodigoProducto; }
            set { SetPropertyValue<string>("CodigoProducto", ref fCodigoProducto, value); }
        }
        string fCodigoPlan;
        [Size(2)]
        public string CodigoPlan
        {
            get { return fCodigoPlan; }
            set { SetPropertyValue<string>("CodigoPlan", ref fCodigoPlan, value); }
        }
        string fContratoPlan;
        public string ContratoPlan
        {
            get { return fContratoPlan; }
            set { SetPropertyValue<string>("ContratoPlan", ref fContratoPlan, value); }
        }
        string fProducto;
        [Size(278)]
        public string Producto
        {
            get { return fProducto; }
            set { SetPropertyValue<string>("Producto", ref fProducto, value); }
        }
        char fTipo;
        public char Tipo
        {
            get { return fTipo; }
            set { SetPropertyValue<char>("Tipo", ref fTipo, value); }
        }
        DateTime fFechaOrden;
        public DateTime FechaOrden
        {
            get { return fFechaOrden; }
            set { SetPropertyValue<DateTime>("FechaOrden", ref fFechaOrden, value); }
        }
        string fNombrePaciente;
        [Size(250)]
        public string NombrePaciente
        {
            get { return fNombrePaciente; }
            set { SetPropertyValue<string>("NombrePaciente", ref fNombrePaciente, value); }
        }
        string fUnidadFuncional;
        [Size(60)]
        public string UnidadFuncional
        {
            get { return fUnidadFuncional; }
            set { SetPropertyValue<string>("UnidadFuncional", ref fUnidadFuncional, value); }
        }
        int fCantidadSolicitada;
        public int CantidadSolicitada
        {
            get { return fCantidadSolicitada; }
            set { SetPropertyValue<int>("CantidadSolicitada", ref fCantidadSolicitada, value); }
        }
        int fCantidadEntregada;
        public int CantidadEntregada
        {
            get { return fCantidadEntregada; }
            set { SetPropertyValue<int>("CantidadEntregada", ref fCantidadEntregada, value); }
        }
        int fCantidadPendiente;
        public int CantidadPendiente
        {
            get { return fCantidadPendiente; }
            set { SetPropertyValue<int>("CantidadPendiente", ref fCantidadPendiente, value); }
        }
        bool fUnico;
        public bool Unico
        {
            get { return fUnico; }
            set { SetPropertyValue<bool>("Unico", ref fUnico, value); }
        }
        bool fNOPOS;
        public bool NOPOS
        {
            get { return fNOPOS; }
            set { SetPropertyValue<bool>("NOPOS", ref fNOPOS, value); }
        }
        string fUnidadMedida;
        [Size(3)]
        public string UnidadMedida
        {
            get { return fUnidadMedida; }
            set { SetPropertyValue<string>("UnidadMedida", ref fUnidadMedida, value); }
        }
        string fNombreMedico;
        [Size(83)]
        public string NombreMedico
        {
            get { return fNombreMedico; }
            set { SetPropertyValue<string>("NombreMedico", ref fNombreMedico, value); }
        }
        string fNitMedico;
        [Size(15)]
        public string NitMedico
        {
            get { return fNitMedico; }
            set { SetPropertyValue<string>("NitMedico", ref fNitMedico, value); }
        }
        int fFilaSeleccionada;
        public int FilaSeleccionada
        {
            get { return fFilaSeleccionada; }
            set { SetPropertyValue<int>("FilaSeleccionada", ref fFilaSeleccionada, value); }
        }
        string fNumeroTarjeta;
        [Size(15)]
        public string NumeroTarjeta
        {
            get { return fNumeroTarjeta; }
            set { SetPropertyValue<string>("NumeroTarjeta", ref fNumeroTarjeta, value); }
        }
        string fIDETIPHIS;
        [Size(9)]
        public string IDETIPHIS
        {
            get { return fIDETIPHIS; }
            set { SetPropertyValue<string>("IDETIPHIS", ref fIDETIPHIS, value); }
        }
        string fNUMEFOLIO;
        [Size(10)]
        public string NUMEFOLIO
        {
            get { return fNUMEFOLIO; }
            set { SetPropertyValue<string>("NUMEFOLIO", ref fNUMEFOLIO, value); }
        }
        string fOpcion;
        [Size(1)]
        public string Opcion
        {
            get { return fOpcion; }
            set { SetPropertyValue<string>("Opcion", ref fOpcion, value); }
        }
        string fOpcionAnulado;
        [Size(1)]
        public string OpcionAnulado
        {
            get { return fOpcionAnulado; }
            set { SetPropertyValue<string>("OpcionAnulado", ref fOpcionAnulado, value); }
        }
        string fProcedimiento;
        [Size(1)]
        public string Procedimiento
        {
            get { return fProcedimiento; }
            set { SetPropertyValue<string>("Procedimiento", ref fProcedimiento, value); }
        }
        bool fMarcarOpcion;
        public bool MarcarOpcion
        {
            get { return fMarcarOpcion; }
            set { SetPropertyValue<bool>("MarcarOpcion", ref fMarcarOpcion, value); }
        }
        string fCodigoPaciente;
        [Size(15)]
        public string CodigoPaciente
        {
            get { return fCodigoPaciente; }
            set { SetPropertyValue<string>("CodigoPaciente", ref fCodigoPaciente, value); }
        }
        decimal fConsecutivoFarmacia;
        public decimal ConsecutivoFarmacia
        {
            get { return fConsecutivoFarmacia; }
            set { SetPropertyValue<decimal>("ConsecutivoFarmacia", ref fConsecutivoFarmacia, value); }
        }
        char fEstado;
        public char Estado
        {
            get { return fEstado; }
            set { SetPropertyValue<char>("Estado", ref fEstado, value); }
        }
        string fEspecialidad;
        [Size(66)]
        public string Especialidad
        {
            get { return fEspecialidad; }
            set { SetPropertyValue<string>("Especialidad", ref fEspecialidad, value); }
        }
        string fCodigoCama;
        [Size(15)]
        public string CodigoCama
        {
            get { return fCodigoCama; }
            set { SetPropertyValue<string>("CodigoCama", ref fCodigoCama, value); }
        }
        string fNotaAdministracion;
        [Size(15)]
        public string NotaAdministracion
        {
            get { return fNotaAdministracion; }
            set { SetPropertyValue<string>("NotaAdministracion", ref fNotaAdministracion, value); }
        }
        string fADMINISTRACION;
        public string ADMINISTRACION
        {
            get { return fADMINISTRACION; }
            set { SetPropertyValue<string>("ADMINISTRACION", ref fADMINISTRACION, value); }
        }
    }
}
