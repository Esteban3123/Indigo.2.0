Imports DevExpress.Xpo

<Persistent("dbo.ViewDashboardPharmacyDetailDevolution")> _
Partial Public Class ViewDashboardPharmacyDetailDevolution
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

    Dim fConsecutivo As Integer
    Public Property Consecutivo() As Integer
        Get
            Return fConsecutivo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Consecutivo", fConsecutivo, value)
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

    Dim fCODCENATE As String
    Public Property CODCENATE() As String
        Get
            Return fCODCENATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODCENATE", fCODCENATE, value)
        End Set
    End Property

    Dim fUFUCODIGO As String
    Public Property UFUCODIGO() As String
        Get
            Return fUFUCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUCODIGO", fUFUCODIGO, value)
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

    Dim fContratoPlan As String
    Public Property ContratoPlan() As String
        Get
            Return fContratoPlan
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContratoPlan", fContratoPlan, value)
        End Set
    End Property

    Dim fCodigoPacienteDevolucion As String
    Public Property CodigoPacienteDevolucion() As String
        Get
            Return fCodigoPacienteDevolucion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoPacienteDevolucion", fCodigoPacienteDevolucion, value)
        End Set
    End Property

    Dim fCODPROSAL As String
    Public Property CODPROSAL() As String
        Get
            Return fCODPROSAL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODPROSAL", fCODPROSAL, value)
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

    Dim fEspecialidad As String
    Public Property Especialidad() As String
        Get
            Return fEspecialidad
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Especialidad", fEspecialidad, value)
        End Set
    End Property

    Dim fCODPRODUC As String
    Public Property CODPRODUC() As String
        Get
            Return fCODPRODUC
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODPRODUC", fCODPRODUC, value)
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

    Dim fTipo As Char
    Public Property Tipo() As Char
        Get
            Return fTipo
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Tipo", fTipo, value)
        End Set
    End Property

    Dim fCantidadDevuelta As Integer
    Public Property CantidadDevuelta() As Integer
        Get
            Return fCantidadDevuelta
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CantidadDevuelta", fCantidadDevuelta, value)
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

    Dim fFECRESGIS As DateTime
    Public Property FECRESGIS() As DateTime
        Get
            Return fFECRESGIS
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECRESGIS", fFECRESGIS, value)
        End Set
    End Property

    Dim fPROESTADO As Char
    Public Property PROESTADO() As Char
        Get
            Return fPROESTADO
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("PROESTADO", fPROESTADO, value)
        End Set
    End Property

    Dim fCODUSUARI As String
    Public Property CODUSUARI() As String
        Get
            Return fCODUSUARI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODUSUARI", fCODUSUARI, value)
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

    Dim fCantidadFisico As Integer
    Public Property CantidadFisico() As Integer
        Get
            Return fCantidadFisico
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CantidadFisico", fCantidadFisico, value)
        End Set
    End Property

    Dim fFinalProductId As Integer?
    Public Property FinalProductId() As Integer?
        Get
            Return fFinalProductId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("FinalProductId", fFinalProductId, value)
        End Set
    End Property

    Dim fFinalProductCode As String
    Public Property FinalProductCode() As String
        Get
            Return fFinalProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FinalProductCode", fFinalProductCode, value)
        End Set
    End Property

    Dim fFinalProductName As String
    Public Property FinalProductName() As String
        Get
            Return fFinalProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FinalProductName", fFinalProductName, value)
        End Set
    End Property

    Dim fHCDEVMEDDId As Integer
    Public Property HCDEVMEDDId() As Integer
        Get
            Return fHCDEVMEDDId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HCDEVMEDDId", fHCDEVMEDDId, value)
        End Set
    End Property

    Dim fBatchCode As String
    Public Property BatchCode() As String
        Get
            Return fBatchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BatchCode", fBatchCode, value)
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

    Dim fCustody As Boolean
    Public Property Custody() As Boolean
        Get
            Return fCustody
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Custody", fCustody, value)
        End Set
    End Property

#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class

