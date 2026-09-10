Imports DevExpress.Xpo

<Persistent("Common.ThirdParty")> _
Public Class CommonThirdPartyXpo
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

    Dim fNit As String
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property

    Dim fDigitVerification As String
    Public Property DigitVerification() As String
        Get
            Return fDigitVerification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DigitVerification", fDigitVerification, value)
        End Set
    End Property

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

#End Region

#Region "Navigators"

    <Association("Common_Customer_References_Common_ThirdParty", GetType(Common_CustomerXpo))>
    Public ReadOnly Property Customer() As XPCollection(Of Common_CustomerXpo)
        Get
            Return GetCollection(Of Common_CustomerXpo)("Customer")
        End Get
    End Property

    <Association("Portfolio_Lawyer_References_Common_ThirdParty", GetType(PortfolioLawyerXpo))>
    Public ReadOnly Property Lawyer() As XPCollection(Of PortfolioLawyerXpo)
        Get
            Return GetCollection(Of PortfolioLawyerXpo)("Lawyer")
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

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
