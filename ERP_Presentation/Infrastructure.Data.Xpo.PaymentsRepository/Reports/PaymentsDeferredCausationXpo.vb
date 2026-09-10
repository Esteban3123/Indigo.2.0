Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.DeferredCausation")> _
Public Class PaymentsDeferredCausationXpo
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
    Dim fIdAccountPayable As PaymentsAccountPayable
    <Association("PaymentsDeferredCausationXpoReferencesPaymentsAccountPayable")> _
    Public Property IdAccountPayable() As PaymentsAccountPayable
        Get
            Return fIdAccountPayable
        End Get
        Set(ByVal value As PaymentsAccountPayable)
            SetPropertyValue(Of PaymentsAccountPayable)("IdAccountPayable", fIdAccountPayable, value)
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
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("PaymentsDeferredCausationXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fPeriodsNumber As Integer
    Public Property PeriodsNumber() As Integer
        Get
            Return fPeriodsNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PeriodsNumber", fPeriodsNumber, value)
        End Set
    End Property
    Dim fTypeDistribution As Byte
    Public Property TypeDistribution() As Byte
        Get
            Return fTypeDistribution
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypeDistribution", fTypeDistribution, value)
        End Set
    End Property
    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property
    Dim fEndDate As DateTime
    Public Property EndDate() As DateTime
        Get
            Return fEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDate", fEndDate, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("PaymentsDeferredCausationXpoReferencesCommonThirdPartyXpo")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdCostCenter As PayrollCostCenterXpoP
    <Association("PaymentsDeferredCausationXpoReferencesPayrollCostCenterXpoP")> _
    Public Property IdCostCenter() As PayrollCostCenterXpoP
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As PayrollCostCenterXpoP)
            SetPropertyValue(Of PayrollCostCenterXpoP)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fValueCreditPeriod As Decimal
    Public Property ValueCreditPeriod() As Decimal
        Get
            Return fValueCreditPeriod
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueCreditPeriod", fValueCreditPeriod, value)
        End Set
    End Property
    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property
    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property
    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property
    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property
    <Association("PaymentsDeferredCausationShareXpoReferencesPayments_DeferredCausation", GetType(PaymentsDeferredCausationShareXpo))> _
    Public ReadOnly Property PaymentsDeferredCausationShareXpo() As XPCollection(Of PaymentsDeferredCausationShareXpo)
        Get
            Return GetCollection(Of PaymentsDeferredCausationShareXpo)("PaymentsDeferredCausationShareXpo")
        End Get
    End Property
    <Association("PaymentsDeferredCausationDetailsXpoReferencesPayments_DeferredCausation", GetType(PaymentsDeferredCausationDetailsXpo))> _
    Public ReadOnly Property PaymentsDeferredCausationDetailsXpo() As XPCollection(Of PaymentsDeferredCausationDetailsXpo)
        Get
            Return GetCollection(Of PaymentsDeferredCausationDetailsXpo)("PaymentsDeferredCausationDetailsXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
