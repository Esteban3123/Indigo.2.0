Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Treasury.CrossingAccountDetailOtherConcept")> _
Public Class TreasuryCrossingAccountDetailOtherConceptXpo
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
    <Association("TreasuryCrossingAccountDetailOtherConceptXpoReferencesTreasuryCrossingAccountXpo")> _
    Public Property CrossingAccountId() As TreasuryCrossingAccountXpo
        Get
            Return fCrossingAccountId
        End Get
        Set(ByVal value As TreasuryCrossingAccountXpo)
            SetPropertyValue(Of TreasuryCrossingAccountXpo)("CrossingAccountId", fCrossingAccountId, value)
        End Set
    End Property
    Dim fTreasuryNoteConceptId As NoteConceptsXpo
    <Association("TreasuryCrossingAccountDetailOtherConceptXpoReferencesNoteConceptsXpo")> _
    Public Property TreasuryNoteConceptId() As NoteConceptsXpo
        Get
            Return fTreasuryNoteConceptId
        End Get
        Set(ByVal value As NoteConceptsXpo)
            SetPropertyValue(Of NoteConceptsXpo)("TreasuryNoteConceptId", fTreasuryNoteConceptId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("TreasuryCrossingAccountDetailOtherConceptXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyReportXpo
    <Association("TreasuryCrossingAccountDetailOtherConceptXpoReferencesCommonThirdPartyReportXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyReportXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("TreasuryCrossingAccountDetailOtherConceptXpoReferencesPayrollCostCenterXpo")> _
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
    Dim fDetail As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
