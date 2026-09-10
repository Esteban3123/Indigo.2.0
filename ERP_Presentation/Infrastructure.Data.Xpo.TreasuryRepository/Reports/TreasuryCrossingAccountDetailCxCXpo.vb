Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.CrossingAccountDetailCxC")> _
Public Class TreasuryCrossingAccountDetailCxCXpo
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
    Dim fCrossingAccountId As TreasuryCrossingAccountXpo
    <Association("TreasuryCrossingAccountDetailCxCXpoReferencesTreasuryCrossingAccountXpo")> _
    Public Property CrossingAccountId() As TreasuryCrossingAccountXpo
        Get
            Return fCrossingAccountId
        End Get
        Set(ByVal value As TreasuryCrossingAccountXpo)
            SetPropertyValue(Of TreasuryCrossingAccountXpo)("CrossingAccountId", fCrossingAccountId, value)
        End Set
    End Property
    Dim fAccountReceivableId As PortfolioAccountReceivableReportXpo
    <Association("TreasuryCrossingAccountDetailCxCXpoReferencesPortfolioAccountReceivableReportXpo")> _
    Public Property AccountReceivableId() As PortfolioAccountReceivableReportXpo
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As PortfolioAccountReceivableReportXpo)
            SetPropertyValue(Of PortfolioAccountReceivableReportXpo)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property
    Dim fAccountReceivableAccountingId As PortfolioAccountReceivableAccountingXpo
    <Association("TreasuryCrossingAccountDetailCxCXpoReferencesPortfolioAccountReceivableAccountingXpo")> _
    Public Property AccountReceivableAccountingId() As PortfolioAccountReceivableAccountingXpo
        Get
            Return fAccountReceivableAccountingId
        End Get
        Set(ByVal value As PortfolioAccountReceivableAccountingXpo)
            SetPropertyValue(Of PortfolioAccountReceivableAccountingXpo)("AccountReceivableAccountingId", fAccountReceivableAccountingId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("TreasuryCrossingAccountDetailCxCXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
