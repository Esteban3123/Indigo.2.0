Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.PortfolioTransferOtherConcept")> _
Public Class PortfolioTransferOtherConceptReportXpo
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
    Dim fPortfolioTransferId As PortfolioTransferReportXpo
    <Association("PortfolioTransferOtherConceptReportXpoReferencesPortfolioTransferReportXpo")> _
    Public Property PortfolioTransferId() As PortfolioTransferReportXpo
        Get
            Return fPortfolioTransferId
        End Get
        Set(ByVal value As PortfolioTransferReportXpo)
            SetPropertyValue(Of PortfolioTransferReportXpo)("PortfolioTransferId", fPortfolioTransferId, value)
        End Set
    End Property
    Dim fPortfolioNoteConceptId As PortfolioNoteConceptReportXpo
    <Association("PortfolioTransferOtherConceptReportXpoReferencesPortfolioNoteConceptReportXpo")> _
    Public Property PortfolioNoteConceptId() As PortfolioNoteConceptReportXpo
        Get
            Return fPortfolioNoteConceptId
        End Get
        Set(ByVal value As PortfolioNoteConceptReportXpo)
            SetPropertyValue(Of PortfolioNoteConceptReportXpo)("PortfolioNoteConceptId", fPortfolioNoteConceptId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("PortfolioTransferOtherConceptReportXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("PortfolioTransferOtherConceptReportXpoReferencesPayrollCostCenterXpo")> _
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
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
