Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.PortfolioNoteDetail")> _
Public Class PortfolioNoteDetailReportXpo
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
    Dim fPortfolioNoteId As PortfolioNoteReportXpo
    <Association("Portfolio_PortfolioNoteDetailReferencesPortfolio_PortfolioNote")> _
    Public Property PortfolioNoteId() As PortfolioNoteReportXpo
        Get
            Return fPortfolioNoteId
        End Get
        Set(ByVal value As PortfolioNoteReportXpo)
            SetPropertyValue(Of PortfolioNoteReportXpo)("PortfolioNoteId", fPortfolioNoteId, value)
        End Set
    End Property
    Dim fPortfolioNoteConceptId As PortfolioNoteConceptReportXpo
    <Association("Portfolio_PortfolioNoteDetailReferencesPortfolio_PortfolioNoteConcept")> _
    Public Property PortfolioNoteConceptId() As PortfolioNoteConceptReportXpo
        Get
            Return fPortfolioNoteConceptId
        End Get
        Set(ByVal value As PortfolioNoteConceptReportXpo)
            SetPropertyValue(Of PortfolioNoteConceptReportXpo)("PortfolioNoteConceptId", fPortfolioNoteConceptId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("PortfolioNoteDetailReportXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("PortfolioNoteDetailReportXpoReferencesCommonThirdPartyXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("PortfolioNoteDetailReportXpoReferencesPayrollCostCenterXpo")> _
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

    <PersistentAlias("Iif(Nature = 1, 'Debito', Nature = 2, 'Credito', '')")>
    Public ReadOnly Property NatureName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NatureName"))
        End Get
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
    Dim fRetentionConceptId As Integer
    Public Property RetentionConceptId() As Integer
        Get
            Return fRetentionConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RetentionConceptId", fRetentionConceptId, value)
        End Set
    End Property
    Dim fBaseValue As Decimal
    Public Property BaseValue() As Decimal
        Get
            Return fBaseValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseValue", fBaseValue, value)
        End Set
    End Property
    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
        End Set
    End Property
    Dim fObservations As String
    <Size(3000)> _
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
