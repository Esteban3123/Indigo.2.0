Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.InitialBalanceAdvance")> _
Public Class PaymentsInitialBalanceAdvanceXpo
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
    <Association("PaymentsInitialBalanceAdvanceXpoReferencesPaymentsInitialBalanceXpo")> _
    Public Property InitialBalanceId() As PaymentsInitialBalanceXpo
        Get
            Return fInitialBalanceId
        End Get
        Set(ByVal value As PaymentsInitialBalanceXpo)
            SetPropertyValue(Of PaymentsInitialBalanceXpo)("InitialBalanceId", fInitialBalanceId, value)
        End Set
    End Property
    Dim fSupplierId As Maintenance_Supplier
    <Association("PaymentsInitialBalanceAdvanceXpoReferencesMaintenance_Supplier")> _
    Public Property SupplierId() As Maintenance_Supplier
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("SupplierId", fSupplierId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("PaymentsInitialBalanceAdvanceXpoReferencesCommonThirdPartyXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fSupplierDistributionLinesId As Integer
    Public Property SupplierDistributionLinesId() As Integer
        Get
            Return fSupplierDistributionLinesId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierDistributionLinesId", fSupplierDistributionLinesId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("PaymentsInitialBalanceAdvanceXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpoP
    <Association("PaymentsInitialBalanceAdvanceXpoReferencesPayrollCostCenterXpoP")> _
    Public Property CostCenterId() As PayrollCostCenterXpoP
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpoP)
            SetPropertyValue(Of PayrollCostCenterXpoP)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fAdvancePaymentsId As PaymentsAdvancePaymentsXpo
    <Association("PaymentsInitialBalanceAdvanceXpoReferencesPaymentsAdvancePaymentsXpo")> _
    Public Property AdvancePaymentsId() As PaymentsAdvancePaymentsXpo
        Get
            Return fAdvancePaymentsId
        End Get
        Set(ByVal value As PaymentsAdvancePaymentsXpo)
            SetPropertyValue(Of PaymentsAdvancePaymentsXpo)("AdvancePaymentsId", fAdvancePaymentsId, value)
        End Set
    End Property
    Dim fAdvancePaymentsDate As DateTime
    Public Property AdvancePaymentsDate() As DateTime
        Get
            Return fAdvancePaymentsDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AdvancePaymentsDate", fAdvancePaymentsDate, value)
        End Set
    End Property
    Dim fAdvancePaymentsDescription As String
    <Size(200)> _
    Public Property AdvancePaymentsDescription() As String
        Get
            Return fAdvancePaymentsDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdvancePaymentsDescription", fAdvancePaymentsDescription, value)
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
