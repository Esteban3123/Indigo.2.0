Imports DevExpress.Xpo

<Persistent("dbo.ViewServicesProceduresConsultation")>
Partial Public Class ViewServicesProceduresConsultation
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key()>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
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

    Dim fRow As Integer
    Public Property Row() As Integer
        Get
            Return fRow
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Row", fRow, value)
        End Set
    End Property

    Dim fCODSERIPS As String
    Public Property CODSERIPS() As String
        Get
            Return fCODSERIPS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODSERIPS", fCODSERIPS, value)
        End Set
    End Property

    Dim fSeleccione As Boolean
    Public Property Seleccione() As Boolean
        Get
            Return fSeleccione
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Seleccione", fSeleccione, value)
        End Set
    End Property

    Dim fUnidadFuncional As String
    Public Property UnidadFuncional() As String
        Get
            Return fUnidadFuncional
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnidadFuncional", fUnidadFuncional, value)
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

    Dim fObservacion As String
    Public Property Observacion() As String
        Get
            Return fObservacion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observacion", fObservacion, value)
        End Set
    End Property

    Dim fFolioSolicitud As String
    Public Property FolioSolicitud() As String
        Get
            Return fFolioSolicitud
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FolioSolicitud", fFolioSolicitud, value)
        End Set
    End Property

    Dim fMedicoSolicitud As String
    Public Property MedicoSolicitud() As String
        Get
            Return fMedicoSolicitud
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MedicoSolicitud", fMedicoSolicitud, value)
        End Set
    End Property

    Dim fFechaSolicitud As Date?
    Public Property FechaSolicitud() As Date?
        Get
            Return fFechaSolicitud
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("FechaSolicitud", fFechaSolicitud, value)
        End Set
    End Property

    Dim fFolioRealizado As String
    Public Property FolioRealizado() As String
        Get
            Return fFolioRealizado
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FolioRealizado", fFolioRealizado, value)
        End Set
    End Property

    Dim fMedicoRealizado As String
    Public Property MedicoRealizado() As String
        Get
            Return fMedicoRealizado
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MedicoRealizado", fMedicoRealizado, value)
        End Set
    End Property

    Dim fFechaRealizado As Date?
    Public Property FechaRealizado() As Date?
        Get
            Return fFechaRealizado
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("FechaRealizado", fFechaRealizado, value)
        End Set
    End Property

    Dim fInterpretacion As String
    Public Property Interpretacion() As String
        Get
            Return fInterpretacion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Interpretacion", fInterpretacion, value)
        End Set
    End Property

    Dim fCODESPEC1 As String
    Public Property CODESPEC1() As String
        Get
            Return fCODESPEC1
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODESPEC1", fCODESPEC1, value)
        End Set
    End Property

    Dim fCANSERIPS As Integer
    Public Property CANSERIPS() As Integer
        Get
            Return fCANSERIPS
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CANSERIPS", fCANSERIPS, value)
        End Set
    End Property

    Dim fNUMINGRES As String
    Public Property NUMINGRES() As String
        Get
            Return fNUMINGRES
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMINGRES", fNUMINGRES, value)
        End Set
    End Property

    Dim fIPCODPACI As String
    Public Property IPCODPACI() As String
        Get
            Return fIPCODPACI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPCODPACI", fIPCODPACI, value)
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

    Dim fNitMedico As String
    Public Property NitMedico() As String
        Get
            Return fNitMedico
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NitMedico", fNitMedico, value)
        End Set
    End Property

    Dim fGENSERVICEORDER As Integer?
    Public Property GENSERVICEORDER() As Integer?
        Get
            Return fGENSERVICEORDER
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("GENSERVICEORDER", fGENSERVICEORDER, value)
        End Set
    End Property

    <PersistentAlias("Iif(GENSERVICEORDER IS NOT NULL, 1, 0)")>
    Public ReadOnly Property IsGenerated As Byte
        Get
            Return Convert.ToByte(Me.EvaluateAlias("IsGenerated"))
        End Get
    End Property

    Dim fFechaRealizacion As DateTime?
    Public Property FechaRealizacion() As DateTime?
        Get
            Return fFechaRealizacion
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("FechaRealizacion", fFechaRealizacion, value)
        End Set
    End Property

    Dim fRealizo As Byte
    Public Property Realizo() As Byte
        Get
            Return fRealizo
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Realizo", fRealizo, value)
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

    Dim fDESSERIPS As String
    Public Property DESSERIPS() As String
        Get
            Return fDESSERIPS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DESSERIPS", fDESSERIPS, value)
        End Set
    End Property

    Dim fCUPSEntityContractDescriptionId As Integer
    Public Property CUPSEntityContractDescriptionId() As Integer
        Get
            Return fCUPSEntityContractDescriptionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CUPSEntityContractDescriptionId", fCUPSEntityContractDescriptionId, value)
        End Set
    End Property

    Dim fContractDescriptionId As Integer
    Public Property ContractDescriptionId() As Integer
        Get
            Return fContractDescriptionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractDescriptionId", fContractDescriptionId, value)
        End Set
    End Property

    Dim fContractDescriptionCodeName As String
    Public Property ContractDescriptionCodeName() As String
        Get
            Return fContractDescriptionCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractDescriptionCodeName", fContractDescriptionCodeName, value)
        End Set
    End Property

    Dim fACJustificationId As Integer?
    Public Property ACJustificationId() As Integer?
        Get
            Return fACJustificationId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ACJustificationId", fACJustificationId, value)
        End Set
    End Property

    Dim fJustificationId As Integer?
    Public Property JustificationId() As Integer?
        Get
            Return fJustificationId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("JustificationId", fJustificationId, value)
        End Set
    End Property

    Dim fJustificationCodeName As String
    Public Property JustificationCodeName() As String
        Get
            Return fJustificationCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("JustificationCodeName", fJustificationCodeName, value)
        End Set
    End Property

    Dim fSkipLiquidation As Boolean
    Public Property SkipLiquidation() As Boolean
        Get
            Return fSkipLiquidation
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SkipLiquidation", fSkipLiquidation, value)
        End Set
    End Property

    Dim fACCreationUser As String
    Public Property ACCreationUser() As String
        Get
            Return fACCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ACCreationUser", fACCreationUser, value)
        End Set
    End Property

    Dim fACCreationDate As DateTime?
    Public Property ACCreationDate() As DateTime?
        Get
            Return fACCreationDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ACCreationDate", fACCreationDate, value)
        End Set
    End Property

    Dim fExistsInViewReviews As Boolean
    Public Property ExistsInViewReviews() As Boolean
        Get
            Return fExistsInViewReviews
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ExistsInViewReviews", fExistsInViewReviews, value)
        End Set
    End Property
#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class