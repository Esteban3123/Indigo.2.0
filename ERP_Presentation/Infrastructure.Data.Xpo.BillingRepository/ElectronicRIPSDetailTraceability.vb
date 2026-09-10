Imports DevExpress.Xpo

<Persistent("Billing.ElectronicsRIPSDetail")>
Public Class ElectronicRIPSDetailTraceability
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fElectronicsRIPSId As Integer
    Public Property ElectronicsRIPSId() As Integer
        Get
            Return fElectronicsRIPSId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ElectronicsRIPSId", fElectronicsRIPSId, value)
        End Set
    End Property

    Dim fMessageCode As String
    Public Property MessageCode() As String
        Get
            Return fMessageCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MessageCode", fMessageCode, value)
        End Set
    End Property

    Dim fMessage As String
    Public Property Message() As String
        Get
            Return fMessage
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Message", fMessage, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fPath As String
    Public Property Path() As String
        Get
            Return fPath
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Path", fPath, value)
        End Set
    End Property

    Dim fTypeMessage As String
    Public Property TypeMessage() As String
        Get
            Return fTypeMessage
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TypeMessage", fTypeMessage, value)
        End Set
    End Property

    Dim fSourcePath As String
    Public Property SourcePath() As String
        Get
            Return fSourcePath
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SourcePath", fSourcePath, value)
        End Set
    End Property
#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class
