Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.BillingConcept")> _
Public Class ConceptReportXpo
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
    '<Indexed(Name:="IX_IPSServiceGroup", Unique:=True)> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fConceptType As Byte
    Public Property ConceptType() As Byte
        Get
            Return fConceptType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptType", fConceptType, value)
        End Set
    End Property
    Dim fObtainCostCenter As Byte
    Public Property ObtainCostCenter() As Byte
        Get
            Return fObtainCostCenter
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ObtainCostCenter", fObtainCostCenter, value)
        End Set
    End Property
    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fAccountingType As Byte
    Public Property AccountingType() As Byte
        Get
            Return fAccountingType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AccountingType", fAccountingType, value)
        End Set
    End Property
    Dim fEntityIncomeAccountId As Integer
    Public Property EntityIncomeAccountId() As Integer
        Get
            Return fEntityIncomeAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityIncomeAccountId", fEntityIncomeAccountId, value)
        End Set
    End Property
    Dim fIndividualIncomeAccountId As Integer
    Public Property IndividualIncomeAccountId() As Integer
        Get
            Return fIndividualIncomeAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IndividualIncomeAccountId", fIndividualIncomeAccountId, value)
        End Set
    End Property
    Dim fDiscountAccountId As Integer
    Public Property DiscountAccountId() As Integer
        Get
            Return fDiscountAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DiscountAccountId", fDiscountAccountId, value)
        End Set
    End Property
    Dim fFeesExpensesAccountId As Integer
    Public Property FeesExpensesAccountId() As Integer
        Get
            Return fFeesExpensesAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FeesExpensesAccountId", fFeesExpensesAccountId, value)
        End Set
    End Property
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
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
    <Association("Billing_BillingConceptAccountReferencesBilling_BillingConcept", GetType(ConceptAccountReportXpo))> _
    Public ReadOnly Property Billing_BillingConceptAccounts() As XPCollection(Of ConceptAccountReportXpo)
        Get
            Return GetCollection(Of ConceptAccountReportXpo)("Billing_BillingConceptAccounts")
        End Get
    End Property
    <Association("Billing_ServiceOrderDetailReferencesBilling_BillingConcept", GetType(OrderServiceDetailXpo))> _
    Public ReadOnly Property Billing_ServiceOrderDetails() As XPCollection(Of OrderServiceDetailXpo)
        Get
            Return GetCollection(Of OrderServiceDetailXpo)("Billing_ServiceOrderDetails")
        End Get
    End Property
    <Association("Billing_ServiceOrderDetailSurgicalReferencesBilling_BillingConcept", GetType(OrderServiceDetailSurgicalXpo))> _
    Public ReadOnly Property Billing_ServiceOrderDetailSurgicals() As XPCollection(Of OrderServiceDetailSurgicalXpo)
        Get
            Return GetCollection(Of OrderServiceDetailSurgicalXpo)("Billing_ServiceOrderDetailSurgicals")
        End Get
    End Property
    <Association("Contract_CUPSEntityReferencesBilling_BillingConcept", GetType(EntityCUPSXpo))> _
    Public ReadOnly Property Contract_CUPSEntitys() As XPCollection(Of EntityCUPSXpo)
        Get
            Return GetCollection(Of EntityCUPSXpo)("Contract_CUPSEntitys")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
