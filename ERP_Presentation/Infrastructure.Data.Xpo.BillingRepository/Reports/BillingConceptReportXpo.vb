Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.BillingConcept")>
Public Class BillingConceptReportXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)>
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
    <Size(20)>
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
    <Size(20)>
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

#End Region

#Region "Navigation"

    <Association("Billing_ServiceOrderDetailReferencesContract_IPSServiceGroup", GetType(ServiceOrderDetailXpo))>
    Public ReadOnly Property Billing_ServiceOrderDetails() As XPCollection(Of ServiceOrderDetailXpo)
        Get
            Return GetCollection(Of ServiceOrderDetailXpo)("Billing_ServiceOrderDetails")
        End Get
    End Property

    <Association("Billing_ServiceOrderDetailSurgicalReferencesContract_IPSServiceGroup", GetType(ServiceOrderDetailSurgicalXpo))>
    Public ReadOnly Property Billing_ServiceOrderDetailSurgicals() As XPCollection(Of ServiceOrderDetailSurgicalXpo)
        Get
            Return GetCollection(Of ServiceOrderDetailSurgicalXpo)("Billing_ServiceOrderDetailSurgicals")
        End Get
    End Property

    <Association("Contract_CUPSEntityReferencesContract_IPSServiceGroup", GetType(CUPSEntityXpo))>
    Public ReadOnly Property Contract_CUPSEntitys() As XPCollection(Of CUPSEntityXpo)
        Get
            Return GetCollection(Of CUPSEntityXpo)("Contract_CUPSEntitys")
        End Get
    End Property

    <Association("BasicBillingDetail_References_BillingConcept", GetType(BasicBillingDetailReportXpo))>
    Public ReadOnly Property BasicBillingDetails() As XPCollection(Of BasicBillingDetailReportXpo)
        Get
            Return GetCollection(Of BasicBillingDetailReportXpo)("BasicBillingDetails")
        End Get
    End Property

    <Association("BasicBillingDetailServicesProvided_References_BillingConcept", GetType(BasicBillingDetailReportXpo))>
    Public ReadOnly Property BasicBillingServicesProvided() As XPCollection(Of BasicBillingDetailReportXpo)
        Get
            Return GetCollection(Of BasicBillingDetailReportXpo)("BasicBillingServicesProvided")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class