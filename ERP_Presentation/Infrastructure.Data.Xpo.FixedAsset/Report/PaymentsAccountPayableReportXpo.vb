Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.AccountPayable")> _
Public Class PaymentsAccountPayableReportXpo
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
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fNumberFiling As Long
    Public Property NumberFiling() As Long
        Get
            Return fNumberFiling
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("NumberFiling", fNumberFiling, value)
        End Set
    End Property
    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property
    Dim fEntityCode As String
    <Size(20)> _
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property
    Dim fEntityName As String
    <Size(250)> _
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property
    Dim fIdSupplier As Integer
    Public Property IdSupplier() As Integer
        Get
            Return fIdSupplier
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdSupplier", fIdSupplier, value)
        End Set
    End Property
    Dim fIdThirdParty As Integer
    Public Property IdThirdParty() As Integer
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdAccount As Integer
    Public Property IdAccount() As Integer
        Get
            Return fIdAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdAccount", fIdAccount, value)
        End Set
    End Property
    Dim fIdCostCenter As Integer
    Public Property IdCostCenter() As Integer
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fBillNumber As String
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
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fServicePeriodDate As DateTime
    Public Property ServicePeriodDate() As DateTime
        Get
            Return fServicePeriodDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ServicePeriodDate", fServicePeriodDate, value)
        End Set
    End Property
    Dim fFilingUnitId As Integer
    Public Property FilingUnitId() As Integer
        Get
            Return fFilingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FilingUnitId", fFilingUnitId, value)
        End Set
    End Property
    Dim fSupplierTypeId As Integer
    Public Property SupplierTypeId() As Integer
        Get
            Return fSupplierTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierTypeId", fSupplierTypeId, value)
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
    Dim fExpirationDate As DateTime
    Public Property ExpirationDate() As DateTime
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ExpirationDate", fExpirationDate, value)
        End Set
    End Property
    Dim fComents As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property Coments() As String
        Get
            Return fComents
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Coments", fComents, value)
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
    Dim fInitialBalance As Boolean
    Public Property InitialBalance() As Boolean
        Get
            Return fInitialBalance
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("InitialBalance", fInitialBalance, value)
        End Set
    End Property
    Dim fIdInitialBalance As Integer
    Public Property IdInitialBalance() As Integer
        Get
            Return fIdInitialBalance
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdInitialBalance", fIdInitialBalance, value)
        End Set
    End Property
    Dim fPreviousBudget As Boolean
    Public Property PreviousBudget() As Boolean
        Get
            Return fPreviousBudget
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PreviousBudget", fPreviousBudget, value)
        End Set
    End Property
    Dim fShares As Integer
    Public Property Shares() As Integer
        Get
            Return fShares
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Shares", fShares, value)
        End Set
    End Property
    Dim fInvoiceValue As Decimal
    Public Property InvoiceValue() As Decimal
        Get
            Return fInvoiceValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InvoiceValue", fInvoiceValue, value)
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
    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property
    Dim fIdOperatingUnit As Integer
    Public Property IdOperatingUnit() As Integer
        Get
            Return fIdOperatingUnit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdOperatingUnit", fIdOperatingUnit, value)
        End Set
    End Property
    Dim fIdSuppliersDistributionLines As Integer
    Public Property IdSuppliersDistributionLines() As Integer
        Get
            Return fIdSuppliersDistributionLines
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdSuppliersDistributionLines", fIdSuppliersDistributionLines, value)
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
    <Association("FixedAsset_FixedAssetEntryReferencesPayments_AccountPayable", GetType(FixedAssetFixedAssetEntryReportXpo))>
    Public ReadOnly Property FixedAssetFixedAssetEntryReportXpo() As XPCollection(Of FixedAssetFixedAssetEntryReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryReportXpo)("FixedAssetFixedAssetEntryReportXpo")
        End Get
    End Property
    <Association("PaymentsAccountPayableDocumentSupport_References_PaymentsAccountPayable", GetType(AccountPayableDocumentSupportXpo))>
    Public ReadOnly Property AccountPayableDocumentSupport() As XPCollection(Of AccountPayableDocumentSupportXpo)
        Get
            Return GetCollection(Of AccountPayableDocumentSupportXpo)("AccountPayableDocumentSupport")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
