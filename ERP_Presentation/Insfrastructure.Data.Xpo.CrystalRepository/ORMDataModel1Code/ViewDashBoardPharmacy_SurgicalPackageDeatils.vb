Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel


<Persistent("dbo.ViewDashBoardPharmacy_SurgicalPackageDeatils")>
    Partial Public Class ViewDashBoardPharmacy_SurgicalPackageDeatils
        Inherits XPLiteObject
        Dim fId As String
        <Key()>
        <Size(50)>
        Public Property Id() As String
            Get
                Return fId
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Id), fId, value)
            End Set
        End Property
        Dim fIngreso As String
        <Size(10)>
        Public Property Ingreso() As String
            Get
                Return fIngreso
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Ingreso), fIngreso, value)
            End Set
        End Property
        Dim fEntidad As String
        <Size(300)>
        Public Property Entidad() As String
            Get
                Return fEntidad
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Entidad), fEntidad, value)
            End Set
        End Property
        Dim fCodigoEntidad As String
        <Size(20)>
        Public Property CodigoEntidad() As String
            Get
                Return fCodigoEntidad
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(CodigoEntidad), fCodigoEntidad, value)
            End Set
        End Property
        Dim fCodigoContrato As String
        <Size(6)>
        Public Property CodigoContrato() As String
            Get
                Return fCodigoContrato
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(CodigoContrato), fCodigoContrato, value)
            End Set
        End Property
        Dim fCodigoPlan As String
        <Size(2)>
        Public Property CodigoPlan() As String
            Get
                Return fCodigoPlan
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(CodigoPlan), fCodigoPlan, value)
            End Set
        End Property
        Dim fContratoPlan As String
    Public Property ContratoPlan() As String
        Get
            Return fContratoPlan
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)(NameOf(ContratoPlan), fContratoPlan, value)
        End Set
    End Property
    Dim fCodProducto As String
    <Size(278)>
    Public Property CodProducto() As String
        Get
            Return fCodProducto
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)(NameOf(CodProducto), fCodProducto, value)
        End Set
    End Property
    Dim fProducto As String
        <Size(278)>
        Public Property Producto() As String
            Get
                Return fProducto
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Producto), fProducto, value)
            End Set
        End Property
        Dim fTipo As Char
        Public Property Tipo() As Char
            Get
                Return fTipo
            End Get
            Set(ByVal value As Char)
                SetPropertyValue(Of Char)(NameOf(Tipo), fTipo, value)
            End Set
        End Property
        Dim fCantidadSolicitada As Integer
    Public Property CantidadSolicitada() As Integer
        Get
            Return fCantidadSolicitada
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)(NameOf(CantidadSolicitada), fCantidadSolicitada, value)
        End Set
    End Property
    Dim fCantidadPaqueteEnf As Integer
    Public Property CantidadPaqueteEnf() As Integer
        Get
            Return fCantidadPaqueteEnf
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)(NameOf(CantidadPaqueteEnf), fCantidadPaqueteEnf, value)
        End Set
    End Property
    Dim fCantidadEntregada As Integer
        Public Property CantidadEntregada() As Integer
            Get
                Return fCantidadEntregada
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)(NameOf(CantidadEntregada), fCantidadEntregada, value)
            End Set
        End Property
        Dim fCantidadPendiente As Integer
        Public Property CantidadPendiente() As Integer
            Get
                Return fCantidadPendiente
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)(NameOf(CantidadPendiente), fCantidadPendiente, value)
            End Set
        End Property
        Dim fUnico As Boolean
        Public Property Unico() As Boolean
            Get
                Return fUnico
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)(NameOf(Unico), fUnico, value)
            End Set
        End Property
        Dim fNOPOS As Boolean
        Public Property NOPOS() As Boolean
            Get
                Return fNOPOS
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)(NameOf(NOPOS), fNOPOS, value)
            End Set
        End Property
        Dim fUnidadMedida As String
        <Size(3)>
        Public Property UnidadMedida() As String
            Get
                Return fUnidadMedida
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(UnidadMedida), fUnidadMedida, value)
            End Set
        End Property
        Dim fMedico As String
        <Size(83)>
        Public Property Medico() As String
            Get
                Return fMedico
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Medico), fMedico, value)
            End Set
        End Property
        Dim fNitMedico As String
        <Size(15)>
        Public Property NitMedico() As String
            Get
                Return fNitMedico
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(NitMedico), fNitMedico, value)
            End Set
        End Property
        Dim fFilaSeleccionada As Integer
        Public Property FilaSeleccionada() As Integer
            Get
                Return fFilaSeleccionada
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)(NameOf(FilaSeleccionada), fFilaSeleccionada, value)
            End Set
        End Property
        Dim fIDETIPHIS As String
        <Size(9)>
        Public Property IDETIPHIS() As String
            Get
                Return fIDETIPHIS
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(IDETIPHIS), fIDETIPHIS, value)
            End Set
        End Property
        Dim fNUMEFOLIO As String
        <Size(10)>
        Public Property NUMEFOLIO() As String
            Get
                Return fNUMEFOLIO
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(NUMEFOLIO), fNUMEFOLIO, value)
            End Set
        End Property
        Dim fOpcion As String
        <Size(1)>
        Public Property Opcion() As String
            Get
                Return fOpcion
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Opcion), fOpcion, value)
            End Set
        End Property
        Dim fOpcionAnulado As String
        <Size(1)>
        Public Property OpcionAnulado() As String
            Get
                Return fOpcionAnulado
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(OpcionAnulado), fOpcionAnulado, value)
            End Set
        End Property
        Dim fProcedimiento As String
        <Size(1)>
        Public Property Procedimiento() As String
            Get
                Return fProcedimiento
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Procedimiento), fProcedimiento, value)
            End Set
        End Property
        Dim fMarcarOpcion As Boolean
        Public Property MarcarOpcion() As Boolean
            Get
                Return fMarcarOpcion
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)(NameOf(MarcarOpcion), fMarcarOpcion, value)
            End Set
        End Property
        Dim fCodigoPaciente As String
        <Size(15)>
        Public Property CodigoPaciente() As String
            Get
                Return fCodigoPaciente
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(CodigoPaciente), fCodigoPaciente, value)
            End Set
        End Property
        Dim fConsecutivo As Decimal
        Public Property Consecutivo() As Decimal
            Get
                Return fConsecutivo
            End Get
            Set(ByVal value As Decimal)
                SetPropertyValue(Of Decimal)(NameOf(Consecutivo), fConsecutivo, value)
            End Set
        End Property
        Dim fEstado As Char
        Public Property Estado() As Char
            Get
                Return fEstado
            End Get
            Set(ByVal value As Char)
                SetPropertyValue(Of Char)(NameOf(Estado), fEstado, value)
            End Set
        End Property
        Dim fEspecialidad As String
        <Size(66)>
        Public Property Especialidad() As String
            Get
                Return fEspecialidad
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Especialidad), fEspecialidad, value)
            End Set
        End Property
        Dim fServicioIPS As String
        <Size(323)>
        Public Property ServicioIPS() As String
            Get
                Return fServicioIPS
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(ServicioIPS), fServicioIPS, value)
            End Set
        End Property
        Dim fIDAGEPROGQX As Integer
    Public Property IDAGEPROGQX() As Integer
        Get
            Return fIDAGEPROGQX
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)(NameOf(IDAGEPROGQX), fIDAGEPROGQX, value)
        End Set
    End Property
    Dim fPackageCodeName As String
    Public Property PackageCodeName() As String
        Get
            Return fPackageCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackageCodeName", fPackageCodeName, value)
        End Set
    End Property

    Dim fPackageId As Integer?
    Public Property PackageId() As Integer?
        Get
            Return fPackageId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("PackageId", fPackageId, value)
        End Set
    End Property

    Dim fTIPPRODUC As Byte
    Public Property TIPPRODUC() As Byte
        Get
            Return fTIPPRODUC
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TIPPRODUC", fTIPPRODUC, value)
        End Set
    End Property

    Dim fIdCostCenter As Integer?
    Public Property IdCostCenter() As Integer?
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
    End Class


