Imports DevExpress.Xpo

<Persistent("dbo.ADTIPOIDENTIFICA")>
Partial Public Class ADTIPOIDENTIFICAXpo
    Inherits XPLiteObject

    Dim fID As Integer
    <Key()>
    Public Property ID() As Integer
        Get
            Return fID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ID", fID, value)
        End Set
    End Property

    Dim fCODIGO As String
    Public Property CODIGO() As String
        Get
            Return fCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGO", fCODIGO, value)
        End Set
    End Property

    Dim fNOMBRE As String
    Public Property NOMBRE() As String
        Get
            Return fNOMBRE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMBRE", fNOMBRE, value)
        End Set
    End Property

    Dim fSIGLA As String
    Public Property SIGLA() As String
        Get
            Return fSIGLA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SIGLA", fSIGLA, value)
        End Set
    End Property

    <PersistentAlias("Concat(Trim(CODIGO), ' - ', Trim(NOMBRE))")>
    Public ReadOnly Property CodeName As String
        Get
            Return Convert.ToString(EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fESTADO As Boolean
    Public Property ESTADO() As Boolean
        Get
            Return fESTADO
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ESTADO", fESTADO, value)
        End Set
    End Property

    Dim fMaximumLength As Integer?
    Public Property MaximumLength() As Integer?
        Get
            Return fMaximumLength
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("MaximumLength", fMaximumLength, value)
        End Set
    End Property

    Dim fMinimunLength As Integer?
    Public Property MinimunLength() As Integer?
        Get
            Return fMinimunLength
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("MinimunLength", fMinimunLength, value)
        End Set
    End Property

    <Association("ThirdPartyReferencesADTIPOIDENTIFICA", GetType(CommonPersonXpo))>
    Public ReadOnly Property CommonPersonXpo() As XPCollection(Of CommonPersonXpo)
        Get
            Return GetCollection(Of CommonPersonXpo)("CommonPersonXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
