Imports DevExpress.Xpo

<Persistent("dbo.VServicesProceduresReportQx")>
Partial Public Class ViewServicesProceduresReportQx
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

    Dim fFecha As Date
    Public Property Fecha() As Date
        Get
            Return fFecha
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("Fecha", fFecha, value)
        End Set
    End Property

    Dim fFolio As String
    Public Property Folio() As String
        Get
            Return fFolio
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Folio", fFolio, value)
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

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class