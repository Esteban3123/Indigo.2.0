Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.ConsignmentDetail")> _
Public Class TreasuryConsignmentDetailXpo
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
    Dim fConsignmentTransferId As TreasuryConsignmentXpo
    <Association("TreasuryConsignmentDetailXpoReferencesTreasuryConsignmentXpo")> _
    Public Property ConsignmentTransferId() As TreasuryConsignmentXpo
        Get
            Return fConsignmentTransferId
        End Get
        Set(ByVal value As TreasuryConsignmentXpo)
            SetPropertyValue(Of TreasuryConsignmentXpo)("ConsignmentTransferId", fConsignmentTransferId, value)
        End Set
    End Property
    Dim fCashRegisterId As TreasuryCashRegistersXpo
    <Association("TreasuryConsignmentDetailXpoReferencesTreasuryCashRegistersXpo")> _
    Public Property CashRegisterId() As TreasuryCashRegistersXpo
        Get
            Return fCashRegisterId
        End Get
        Set(ByVal value As TreasuryCashRegistersXpo)
            SetPropertyValue(Of TreasuryCashRegistersXpo)("CashRegisterId", fCashRegisterId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("TreasuryConsignmentDetailXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("TreasuryConsignmentDetailXpoReferencesPayrollCostCenterXpo")> _
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
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
