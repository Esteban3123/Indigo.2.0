Imports DevExpress.Xpo

<Persistent("Common.ViewEmployeeExemptIncome ")>
Public Class CommonExemptIncomeDetailXpo
    Inherits XPLiteObject

#Region "Properties"

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

    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fRegisterStatus As String
    Public Property RegisterStatus() As String
        Get
            Return fRegisterStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RegisterStatus", fRegisterStatus, value)
        End Set
    End Property

    Dim fVoucherType As String
    Public Property VoucherType() As String
        Get
            Return fVoucherType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VoucherType", fVoucherType, value)
        End Set
    End Property

    Dim fVoucherCode As String
    Public Property VoucherCode() As String
        Get
            Return fVoucherCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VoucherCode", fVoucherCode, value)
        End Set
    End Property

    Dim fDateLiquidation As Date
    Public Property DateLiquidation() As Date
        Get
            Return fDateLiquidation
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("DateLiquidation", fDateLiquidation, value)
        End Set
    End Property

    Dim fMonthlyIncome As Decimal
    Public Property MonthlyIncome() As Decimal
        Get
            Return fMonthlyIncome
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MonthlyIncome", fMonthlyIncome, value)
        End Set
    End Property

    Dim fExemptIncomeValue As Decimal
    Public Property ExemptIncomeValue() As Decimal
        Get
            Return fExemptIncomeValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ExemptIncomeValue", fExemptIncomeValue, value)
        End Set
    End Property

    Dim fYearLiquidated
    Public Property YearLiquidated() As Integer
        Get
            Return fYearLiquidated
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Decimal)("YearLiquidated", fYearLiquidated, value)
        End Set
    End Property

    Dim fIsNew
    Public Property IsNew() As Boolean
        Get
            Return fIsNew
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("IsNew", fIsNew, value)
        End Set
    End Property

    Dim fComments
    Public Property Comments() As String
        Get
            Return fComments
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Comments", fComments, value)
        End Set
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
