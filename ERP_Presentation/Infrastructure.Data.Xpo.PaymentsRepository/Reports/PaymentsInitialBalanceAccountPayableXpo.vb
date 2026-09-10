Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.InitialBalanceAccountPayable")> _
Public Class PaymentsInitialBalanceAccountPayableXpo
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
    Dim fInitialBalanceId As PaymentsInitialBalanceXpo
    <Association("PaymentsInitialBalanceAccountPayableXpoReferencesPaymentsInitialBalanceXpo")> _
    Public Property InitialBalanceId() As PaymentsInitialBalanceXpo
        Get
            Return fInitialBalanceId
        End Get
        Set(ByVal value As PaymentsInitialBalanceXpo)
            SetPropertyValue(Of PaymentsInitialBalanceXpo)("InitialBalanceId", fInitialBalanceId, value)
        End Set
    End Property
    Dim fSupplierId As Maintenance_Supplier
    <Association("PaymentsInitialBalanceAccountPayableXpoReferencesMaintenance_Supplier")> _
    Public Property SupplierId() As Maintenance_Supplier
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("SupplierId", fSupplierId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("PaymentsInitialBalanceAccountPayableXpoReferencesCommonThirdPartyXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fSupplierDistributionLinesID As Integer
    Public Property SupplierDistributionLinesID() As Integer
        Get
            Return fSupplierDistributionLinesID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierDistributionLinesID", fSupplierDistributionLinesID, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("PaymentsInitialBalanceAccountPayableXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpoP
    <Association("PaymentsInitialBalanceAccountPayableXpoReferencesPayrollCostCenterXpoP")> _
    Public Property CostCenterId() As PayrollCostCenterXpoP
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpoP)
            SetPropertyValue(Of PayrollCostCenterXpoP)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fAccountPayableId As PaymentsAccountPayable
    <Association("PaymentsInitialBalanceAccountPayableXpoReferencesPaymentsAccountPayable")> _
    Public Property AccountPayableId() As PaymentsAccountPayable
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As PaymentsAccountPayable)
            SetPropertyValue(Of PaymentsAccountPayable)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property
    Dim fBillNumber As String
    <Size(20)> _
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
    Dim fTerm As Integer
    Public Property Term() As Integer
        Get
            Return fTerm
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Term", fTerm, value)
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
    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
