Imports DevExpress.Xpo

<Persistent("Portfolio.Lawyer")>
Public Class PortfolioLawyerXpo
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
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("Portfolio_Lawyer_References_Common_ThirdParty")>
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fType As Integer
    Public Property Type() As Integer
        Get
            Return fType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Type", fType, value)
        End Set
    End Property

    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
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

#End Region

#Region "Navigators"

    <Association("Glosas_TransferJuridicalDebtCollectionC_References_Portfolio_Lawyer", GetType(GlosasTransferJuridicalDebtCollectionCXpo))>
    Public ReadOnly Property TransferJuridicalDebtCollection() As XPCollection(Of GlosasTransferJuridicalDebtCollectionCXpo)
        Get
            Return GetCollection(Of GlosasTransferJuridicalDebtCollectionCXpo)("TransferJuridicalDebtCollection")
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
