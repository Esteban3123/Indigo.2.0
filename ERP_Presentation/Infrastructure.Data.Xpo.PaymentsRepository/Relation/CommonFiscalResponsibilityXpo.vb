#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Common.FiscalResponsibility")>
Partial Public Class CommonFiscalResponsibilityXpo
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

    Dim fCode As String
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    <Association("ThirdPartyFiscalResponsibilityReferencesFiscalResponsibility", GetType(CommonThirdPartyFiscalResponsibilityXpo))>
    Public ReadOnly Property ThirdPartyFiscalResponsibilityReferencesFiscalResponsibilityXpoCollection() As XPCollection(Of CommonThirdPartyFiscalResponsibilityXpo)
        Get
            Return GetCollection(Of CommonThirdPartyFiscalResponsibilityXpo)("ThirdPartyFiscalResponsibilityReferencesFiscalResponsibilityXpoCollection")
        End Get
    End Property



#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

#End Region

End Class

