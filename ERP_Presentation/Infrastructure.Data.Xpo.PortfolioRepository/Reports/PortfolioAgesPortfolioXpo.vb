Imports DevExpress.Xpo

<Persistent("Portfolio.AgesPortfolio")> _
Public Class PortfolioAgesPortfolioXpo
    Inherits XPLiteObject

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

    Dim fSettingPortfolioId As PortfolioSettingPortfolioXpo
    <Association("PortfolioAgesPortfolioXpoReferencesPortfolioSettingPortfolioXpo")>
    Public Property SettingPortfolioId() As PortfolioSettingPortfolioXpo
        Get
            Return fSettingPortfolioId
        End Get
        Set(ByVal value As PortfolioSettingPortfolioXpo)
            SetPropertyValue(Of PortfolioSettingPortfolioXpo)("SettingPortfolioId", fSettingPortfolioId, value)
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

    Dim fInitialRange As Integer
    Public Property InitialRange() As Integer
        Get
            Return fInitialRange
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InitialRange", fInitialRange, value)
        End Set
    End Property

    Dim fEndRange As Integer
    Public Property EndRange() As Integer
        Get
            Return fEndRange
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EndRange", fEndRange, value)
        End Set
    End Property

    Dim fDeteriorationPercentage As Decimal
    Public Property DeteriorationPercentage() As Decimal
        Get
            Return fDeteriorationPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DeteriorationPercentage", fDeteriorationPercentage, value)
        End Set
    End Property

    Dim fProvisionPercentage As Decimal
    Public Property ProvisionPercentage() As Decimal
        Get
            Return fProvisionPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ProvisionPercentage", fProvisionPercentage, value)
        End Set
    End Property

    Dim fColor As Integer
    Public Property Color() As Integer
        Get
            Return fColor
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Color", fColor, value)
        End Set
    End Property

    Dim fCreationUser As String
    <Size(20)>
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
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

    Dim fModificationUser As String
    <Size(20)>
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
