Imports DevExpress.Xpo

<Persistent("Cost.ViewCostReportDistributionDirectCost")>
Public Class ViewCostReportDistributionDirectCostXpo
    Inherits XPLiteObject

#Region "Properties"

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

    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property

    Dim fMonth As Integer
    Public Property Month() As Integer
        Get
            Return fMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Month", fMonth, value)
        End Set
    End Property

    Dim fGeneralExpenseCodeName As String
    Public Property GeneralExpenseCodeName() As String
        Get
            Return fGeneralExpenseCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GeneralExpenseCodeName", fGeneralExpenseCodeName, value)
        End Set
    End Property

    Dim fThirdPartyNitName As String
    Public Property ThirdPartyNitName() As String
        Get
            Return fThirdPartyNitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNitName", fThirdPartyNitName, value)
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

    Dim fBillNumber As String
    Public Property BillNumber() As String
        Get
            Return fBillNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillNumber", fBillNumber, value)
        End Set
    End Property

    Dim fBillDate As DateTime
    Public Property BillDate() As DateTime
        Get
            Return fBillDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BillDate", fBillDate, value)
        End Set
    End Property

    Dim fServicePeriodDate As DateTime
    Public Property ServicePeriodDate() As DateTime
        Get
            Return fServicePeriodDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ServicePeriodDate", fServicePeriodDate, value)
        End Set
    End Property

    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

    Dim fStatusName As String
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
        End Set
    End Property

    Dim fCreationUser As String
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fCurrencyAbbreviation As String
    Public Property CurrencyAbbreviation() As String
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
        End Set
    End Property

    Dim fCurrencyId As Integer
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fCurrencyName As String
    Public Property CurrencyName() As String
        Get
            Return fCurrencyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyName", fCurrencyName, value)
        End Set
    End Property
#End Region

#Region "Navigations Properties"

    <Association("ViewCostReportDistributionDirectCostDetail_References_ViewCostReportDistributionDirectCost", GetType(ViewCostReportDistributionDirectCostDetailXpo))>
    Public ReadOnly Property CostReportDistributionDirectCostDetail() As XPCollection(Of ViewCostReportDistributionDirectCostDetailXpo)
        Get
            Return GetCollection(Of ViewCostReportDistributionDirectCostDetailXpo)("CostReportDistributionDirectCostDetail")
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
