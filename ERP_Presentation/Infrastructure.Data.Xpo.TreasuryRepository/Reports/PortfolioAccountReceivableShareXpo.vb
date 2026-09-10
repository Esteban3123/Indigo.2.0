Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.AccountReceivableShare")> _
Public Class PortfolioAccountReceivableShareXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fAccountReceivableId As PortfolioAccountReceivableReportXpo
    <Association("PortfolioAccountReceivableShareXpoReferencesPortfolioAccountReceivableReportXpo")> _
    Public Property AccountReceivableId() As PortfolioAccountReceivableReportXpo
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As PortfolioAccountReceivableReportXpo)
            SetPropertyValue(Of PortfolioAccountReceivableReportXpo)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property
    Dim fNumber As Integer
    Public Property Number() As Integer
        Get
            Return fNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Number", fNumber, value)
        End Set
    End Property
    Dim fExpiredDate As DateTime
    Public Property ExpiredDate() As DateTime
        Get
            Return fExpiredDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ExpiredDate", fExpiredDate, value)
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
    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property
    Dim fDebitValue As Decimal
    Public Property DebitValue() As Decimal
        Get
            Return fDebitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitValue", fDebitValue, value)
        End Set
    End Property
    Dim fCreditValue As Decimal
    Public Property CreditValue() As Decimal
        Get
            Return fCreditValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValue", fCreditValue, value)
        End Set
    End Property
    Dim fTransferValue As Decimal
    Public Property TransferValue() As Decimal
        Get
            Return fTransferValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TransferValue", fTransferValue, value)
        End Set
    End Property
    Dim fPaymentValue As Decimal
    Public Property PaymentValue() As Decimal
        Get
            Return fPaymentValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PaymentValue", fPaymentValue, value)
        End Set
    End Property
    Dim fCrossingValue As Decimal
    Public Property CrossingValue() As Decimal
        Get
            Return fCrossingValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CrossingValue", fCrossingValue, value)
        End Set
    End Property
    Dim fInterestPaymentDate As DateTime
    Public Property InterestPaymentDate() As DateTime
        Get
            Return fInterestPaymentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InterestPaymentDate", fInterestPaymentDate, value)
        End Set
    End Property
    Dim fInterestValue As Decimal
    Public Property InterestValue() As Decimal
        Get
            Return fInterestValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InterestValue", fInterestValue, value)
        End Set
    End Property
    Dim fSurchargesValue As Decimal
    Public Property SurchargesValue() As Decimal
        Get
            Return fSurchargesValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SurchargesValue", fSurchargesValue, value)
        End Set
    End Property
    Dim fCapitalRepaymentAgreement As Decimal
    Public Property CapitalRepaymentAgreement() As Decimal
        Get
            Return fCapitalRepaymentAgreement
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CapitalRepaymentAgreement", fCapitalRepaymentAgreement, value)
        End Set
    End Property
    Dim fFinancialInterest As Decimal
    Public Property FinancialInterest() As Decimal
        Get
            Return fFinancialInterest
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FinancialInterest", fFinancialInterest, value)
        End Set
    End Property
    Dim fRepaymentAgreementInterest As Decimal
    Public Property RepaymentAgreementInterest() As Decimal
        Get
            Return fRepaymentAgreementInterest
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RepaymentAgreementInterest", fRepaymentAgreementInterest, value)
        End Set
    End Property
    <Association("PortfolioTransferDetailAccountShareXpoReferencesPortfolioAccountReceivableShareXpo", GetType(PortfolioTransferDetailAccountShareXpo))> _
    Public ReadOnly Property PortfolioTransferDetailAccountShareXpo() As XPCollection(Of PortfolioTransferDetailAccountShareXpo)
        Get
            Return GetCollection(Of PortfolioTransferDetailAccountShareXpo)("PortfolioTransferDetailAccountShareXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
