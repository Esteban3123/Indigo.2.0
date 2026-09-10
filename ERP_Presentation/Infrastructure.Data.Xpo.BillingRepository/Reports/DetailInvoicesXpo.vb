Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.InvoiceDetail")> _
Public Class DetailInvoicesXpo
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
    Dim fInvoiceId As InvoicesXpo
    <Association("Billing_InvoiceDetailReferencesBilling_Invoice")> _
    Public Property InvoiceId() As InvoicesXpo
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As InvoicesXpo)
            SetPropertyValue(Of InvoicesXpo)("InvoiceId", fInvoiceId, value)
        End Set
    End Property
    Dim fServiceOrderDetailId As OrderServiceDetailXpo
    <Association("Billing_InvoiceDetailReferencesBilling_ServiceOrderDetail")> _
    Public Property ServiceOrderDetailId() As OrderServiceDetailXpo
        Get
            Return fServiceOrderDetailId
        End Get
        Set(ByVal value As OrderServiceDetailXpo)
            SetPropertyValue(Of OrderServiceDetailXpo)("ServiceOrderDetailId", fServiceOrderDetailId, value)
        End Set
    End Property
    Dim fGrandTotalSalesPrice As Decimal
    Public Property GrandTotalSalesPrice() As Decimal
        Get
            Return fGrandTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GrandTotalSalesPrice", fGrandTotalSalesPrice, value)
        End Set
    End Property
    Dim fDistributionType As Byte
    Public Property DistributionType() As Byte
        Get
            Return fDistributionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DistributionType", fDistributionType, value)
        End Set
    End Property
    Dim fThirdPartySalesPrice As Decimal
    Public Property ThirdPartySalesPrice() As Decimal
        Get
            Return fThirdPartySalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartySalesPrice", fThirdPartySalesPrice, value)
        End Set
    End Property
    Dim fThirdPartyPercentage As Decimal
    Public Property ThirdPartyPercentage() As Decimal
        Get
            Return fThirdPartyPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartyPercentage", fThirdPartyPercentage, value)
        End Set
    End Property
    Dim fApplyRecoveryFee As Byte
    Public Property ApplyRecoveryFee() As Byte
        Get
            Return fApplyRecoveryFee
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ApplyRecoveryFee", fApplyRecoveryFee, value)
        End Set
    End Property
    Dim fRecoveryFeeType As Byte
    Public Property RecoveryFeeType() As Byte
        Get
            Return fRecoveryFeeType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RecoveryFeeType", fRecoveryFeeType, value)
        End Set
    End Property
    Dim fSubTotalPatientSalesPrice As Decimal
    Public Property SubTotalPatientSalesPrice() As Decimal
        Get
            Return fSubTotalPatientSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SubTotalPatientSalesPrice", fSubTotalPatientSalesPrice, value)
        End Set
    End Property
    Dim fPatientPercentage As Decimal
    Public Property PatientPercentage() As Decimal
        Get
            Return fPatientPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PatientPercentage", fPatientPercentage, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
