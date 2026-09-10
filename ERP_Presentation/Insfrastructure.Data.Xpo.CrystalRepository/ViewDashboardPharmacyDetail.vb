Imports DevExpress.Xpo

<Persistent("dbo.ViewDashboardPharmacyDetail")> _
Partial Public Class ViewDashboardPharmacyDetail
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fFilaSeleccionada As Integer
    Public Property FilaSeleccionada() As Integer
        Get
            Return fFilaSeleccionada
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FilaSeleccionada", fFilaSeleccionada, value)
        End Set
    End Property

    Dim fOpcion As String
    Public Property Opcion() As String
        Get
            Return fOpcion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Opcion", fOpcion, value)
        End Set
    End Property

    Dim fOpcionAnulado As String
    Public Property OpcionAnulado() As String
        Get
            Return fOpcionAnulado
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OpcionAnulado", fOpcionAnulado, value)
        End Set
    End Property

    Dim fProcedimiento As String
    Public Property Procedimiento() As String
        Get
            Return fProcedimiento
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Procedimiento", fProcedimiento, value)
        End Set
    End Property

    Dim fMarcarOpcion As Boolean
    Public Property MarcarOpcion() As Boolean
        Get
            Return fMarcarOpcion
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("MarcarOpcion", fMarcarOpcion, value)
        End Set
    End Property

    Dim fConsecutivo As Decimal
    Public Property Consecutivo() As Decimal
        Get
            Return fConsecutivo
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Consecutivo", fConsecutivo, value)
        End Set
    End Property

    Dim fIngreso As String
    Public Property Ingreso() As String
        Get
            Return fIngreso
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Ingreso", fIngreso, value)
        End Set
    End Property

    Dim fNUMEFOLIO As String
    Public Property NUMEFOLIO() As String
        Get
            Return fNUMEFOLIO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMEFOLIO", fNUMEFOLIO, value)
        End Set
    End Property

    Dim fEntidad As String
    Public Property Entidad() As String
        Get
            Return fEntidad
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Entidad", fEntidad, value)
        End Set
    End Property

    Dim fCodigoEntidad As String
    Public Property CodigoEntidad() As String
        Get
            Return fCodigoEntidad
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoEntidad", fCodigoEntidad, value)
        End Set
    End Property

    Dim fCodigoContrato As String
    Public Property CodigoContrato() As String
        Get
            Return fCodigoContrato
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoContrato", fCodigoContrato, value)
        End Set
    End Property

    Dim fCodigoPlan As String
    Public Property CodigoPlan() As String
        Get
            Return fCodigoPlan
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoPlan", fCodigoPlan, value)
        End Set
    End Property

    Dim fContratoPlan As String
    Public Property ContratoPlan() As String
        Get
            Return fContratoPlan
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContratoPlan", fContratoPlan, value)
        End Set
    End Property

    Dim fCodigoPaciente As String
    Public Property CodigoPaciente() As String
        Get
            Return fCodigoPaciente
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoPaciente", fCodigoPaciente, value)
        End Set
    End Property

    Dim fMedico As String
    Public Property Medico() As String
        Get
            Return fMedico
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Medico", fMedico, value)
        End Set
    End Property

    Dim fNitMedico As String
    Public Property NitMedico() As String
        Get
            Return fNitMedico
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NitMedico", fNitMedico, value)
        End Set
    End Property

    Dim fEspecialidad As String
    Public Property Especialidad() As String
        Get
            Return fEspecialidad
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Especialidad", fEspecialidad, value)
        End Set
    End Property

    Dim fProducto As String
    Public Property Producto() As String
        Get
            Return fProducto
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Producto", fProducto, value)
        End Set
    End Property

    Dim fNombrePaqueteEnf As String
    Public Property NombrePaqueteEnf() As String
        Get
            Return fNombrePaqueteEnf
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NombrePaqueteEnf", fNombrePaqueteEnf, value)
        End Set
    End Property

    Dim fTipo As String
    Public Property Tipo() As String
        Get
            Return fTipo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Tipo", fTipo, value)
        End Set
    End Property

    Dim fTipoAnterior As String
    Public Property TipoAnterior() As String
        Get
            Return fTipoAnterior
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TipoAnterior", fTipoAnterior, value)
        End Set
    End Property

    Dim fCantidadPaqueteEnf As Integer
    Public Property CantidadPaqueteEnf() As Integer
        Get
            Return fCantidadPaqueteEnf
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CantidadPaqueteEnf", fCantidadPaqueteEnf, value)
        End Set
    End Property

    Dim fCantidadSolicitada As Integer
    Public Property CantidadSolicitada() As Integer
        Get
            Return fCantidadSolicitada
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CantidadSolicitada", fCantidadSolicitada, value)
        End Set
    End Property

    Dim fCantidadEntregada As Integer
    Public Property CantidadEntregada() As Integer
        Get
            Return fCantidadEntregada
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CantidadEntregada", fCantidadEntregada, value)
        End Set
    End Property

    Dim fCantidadPendiente As Integer
    Public Property CantidadPendiente() As Integer
        Get
            Return fCantidadPendiente
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CantidadPendiente", fCantidadPendiente, value)
        End Set
    End Property

    Dim fUnico As Boolean
    Public Property Unico() As Boolean
        Get
            Return fUnico
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Unico", fUnico, value)
        End Set
    End Property

    Dim fNOPOS As Boolean
    Public Property NOPOS() As Boolean
        Get
            Return fNOPOS
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("NOPOS", fNOPOS, value)
        End Set
    End Property

    Dim fPBS As String
    Public Property PBS() As String
        Get
            Return fPBS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PBS", fPBS, value)
        End Set
    End Property

    Dim fUNIRS As String
    Public Property UNIRS() As String
        Get
            Return fUNIRS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UNIRS", fUNIRS, value)
        End Set
    End Property

    Dim fUnidadMedida As String
    Public Property UnidadMedida() As String
        Get
            Return fUnidadMedida
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnidadMedida", fUnidadMedida, value)
        End Set
    End Property

    Dim fIDETIPHIS As String
    Public Property IDETIPHIS() As String
        Get
            Return fIDETIPHIS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IDETIPHIS", fIDETIPHIS, value)
        End Set
    End Property

    Dim fEstado As Char
    Public Property Estado() As Char
        Get
            Return fEstado
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Estado", fEstado, value)
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

    Dim fExtramural As Boolean
    Public Property Extramural() As Boolean
        Get
            Return fExtramural
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Extramural", fExtramural, value)
        End Set
    End Property

    Dim fCustodia As Boolean
    Public Property Custodia() As Boolean
        Get
            Return fCustodia
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Custodia", fCustodia, value)
        End Set
    End Property

    Dim fADMINISTRACION As String
    Public Property ADMINISTRACION() As String
        Get
            Return fADMINISTRACION
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ADMINISTRACION", fADMINISTRACION, value)
        End Set
    End Property

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fRoutedTo As String
    Public Property RoutedTo() As String
        Get
            Return fRoutedTo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RoutedTo", fRoutedTo, value)
        End Set
    End Property

    Dim fCodProducto As String
    Public Property CodProducto() As String
        Get
            Return fCodProducto
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodProducto", fCodProducto, value)
        End Set
    End Property

    Dim fCantidadPrescrita As Integer?
    Public Property CantidadPrescrita() As Integer?
        Get
            Return fCantidadPrescrita
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CantidadPrescrita", fCantidadPrescrita, value)
        End Set
    End Property

    Dim fDosisPrescrita As Integer?
    Public Property DosisPrescrita() As Integer?
        Get
            Return fDosisPrescrita
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("DosisPrescrita", fDosisPrescrita, value)
        End Set
    End Property

    Dim fCantidadAutorizada As Integer?
    Public Property CantidadAutorizada() As Integer?
        Get
            Return fCantidadAutorizada
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CantidadAutorizada", fCantidadAutorizada, value)
        End Set
    End Property

    Dim fNumeroNoPBS As String
    Public Property NumeroNoPBS() As String
        Get
            Return fNumeroNoPBS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumeroNoPBS", fNumeroNoPBS, value)
        End Set
    End Property

    Dim fColorAlertaPBS As Integer?
    Public Property ColorAlertaPBS() As Integer?
        Get
            Return fColorAlertaPBS
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ColorAlertaPBS", fColorAlertaPBS, value)
        End Set
    End Property

    Dim fSaldoAutorizadoMipres As Integer?
    Public Property SaldoAutorizadoMipres() As Integer?
        Get
            Return fSaldoAutorizadoMipres
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("SaldoAutorizadoMipres", fSaldoAutorizadoMipres, value)
        End Set
    End Property

    Dim fCantidadDispensada As Integer
    Public Property CantidadDispensada() As Integer
        Get
            Return fCantidadDispensada
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CantidadDispensada", fCantidadDispensada, value)
        End Set
    End Property

    Dim fAllowBillingWithoutAuthorization As Boolean?
    Public Property AllowBillingWithoutAuthorization() As Boolean?
        Get
            Return fAllowBillingWithoutAuthorization
        End Get
        Set(ByVal value As Boolean?)
            SetPropertyValue(Of Boolean?)("AllowBillingWithoutAuthorization", fAllowBillingWithoutAuthorization, value)
        End Set
    End Property

    Dim fSENDTO As Byte
    Public Property SENDTO() As Byte
        Get
            Return fSENDTO
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SENDTO", fSENDTO, value)
        End Set
    End Property

    Dim fNote As String
    Public Property Note() As String
        Get
            Return fNote
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Note", fNote, value)
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

    Dim fObservation As String
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
        End Set
    End Property

    Dim fTotalDose As Decimal?
    Public Property TotalDose() As Decimal?
        Get
            Return fTotalDose
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("TotalDose", fTotalDose, value)
        End Set
    End Property

    Dim fTotalDoseMeasurement As String
    Public Property TotalDoseMeasurement() As String
        Get
            Return fTotalDoseMeasurement
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TotalDoseMeasurement", fTotalDoseMeasurement, value)
        End Set
    End Property
#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class