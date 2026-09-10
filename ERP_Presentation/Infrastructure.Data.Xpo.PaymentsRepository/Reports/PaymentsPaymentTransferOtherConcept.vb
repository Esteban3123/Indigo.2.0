Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.PaymentTransferOtherConcept")> _
Public Class PaymentsPaymentTransferOtherConcept
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
    Dim fPaymentTransferId As PaymentsPaymentTransfer
    <Association("Payments_PaymentTransferOtherConceptReferencesPayments_PaymentTransfer")> _
    Public Property PaymentTransferId() As PaymentsPaymentTransfer
        Get
            Return fPaymentTransferId
        End Get
        Set(ByVal value As PaymentsPaymentTransfer)
            SetPropertyValue(Of PaymentsPaymentTransfer)("PaymentTransferId", fPaymentTransferId, value)
        End Set
    End Property
    Dim fAccountPayableConceptNoteId As PaymentsAccountPayableConceptNotesXpo
    <Association("Payments_PaymentTransferOtherConceptReferencesPayments_AccountPayableConceptNotes")> _
    Public Property AccountPayableConceptNoteId() As PaymentsAccountPayableConceptNotesXpo
        Get
            Return fAccountPayableConceptNoteId
        End Get
        Set(ByVal value As PaymentsAccountPayableConceptNotesXpo)
            SetPropertyValue(Of PaymentsAccountPayableConceptNotesXpo)("AccountPayableConceptNoteId", fAccountPayableConceptNoteId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("Payments_PaymentTransferOtherConceptReferencesGeneralLedger_MainAccounts")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdsPartyXpo
    <Association("Payments_PaymentTransferOtherConceptReferencesCommon_ThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdsPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdsPartyXpo)
            SetPropertyValue(Of CommonThirdsPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpoP
    <Association("Payments_PaymentTransferOtherConceptReferencesPayroll_CostCenter")> _
    Public Property CostCenterId() As PayrollCostCenterXpoP
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpoP)
            SetPropertyValue(Of PayrollCostCenterXpoP)("CostCenterId", fCostCenterId, value)
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
