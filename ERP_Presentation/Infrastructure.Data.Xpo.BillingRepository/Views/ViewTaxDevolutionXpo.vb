Imports DevExpress.Xpo

<Persistent("Billing.ViewTaxDevolution")>
Public Class ViewTaxDevolutionXpo
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

    Dim fRevenueControlDetailId As Integer
    Public Property RevenueControlDetailId() As Integer
        Get
            Return fRevenueControlDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RevenueControlDetailId", fRevenueControlDetailId, value)
        End Set
    End Property

    Dim fTaxValue As Decimal
    Public Property TaxValue() As Decimal
        Get
            Return fTaxValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TaxValue", fTaxValue, value)
        End Set
    End Property

    Dim fPaymentMethodType As Byte
    Public Property PaymentMethodType() As Byte
        Get
            Return fPaymentMethodType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PaymentMethodType", fPaymentMethodType, value)
        End Set
    End Property

    Dim fPaymentMethodTypeName As String
    Public Property PaymentMethodTypeName() As String
        Get
            Return fPaymentMethodTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PaymentMethodTypeName", fPaymentMethodTypeName, value)
        End Set
    End Property

    Dim fValueWithTaxDevolution As Decimal
    Public Property ValueWithTaxDevolution() As Decimal
        Get
            Return fValueWithTaxDevolution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueWithTaxDevolution", fValueWithTaxDevolution, value)
        End Set
    End Property


    Dim fValueWithTaxDevolutionOfficialCurrency As Decimal
    Public Property ValueWithTaxDevolutionOfficialCurrency() As Decimal
        Get
            Return fValueWithTaxDevolutionOfficialCurrency
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueWithTaxDevolutionOfficialCurrency", fValueWithTaxDevolutionOfficialCurrency, value)
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