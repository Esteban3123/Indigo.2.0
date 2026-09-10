Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Contract.CareGroup")> _
Public Class CareGroupReportXpo
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
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    'columna que devuelve el codigo y el nombre concatenado
    <Size(120)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodName_GroupCare() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodName_GroupCare"))
        End Get
    End Property

    Dim fCareGroupType As Byte
    Public Property CareGroupType() As Byte
        Get
            Return fCareGroupType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CareGroupType", fCareGroupType, value)
        End Set
    End Property
    Dim fDefaultManual As Byte
    Public Property DefaultManual() As Byte
        Get
            Return fDefaultManual
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DefaultManual", fDefaultManual, value)
        End Set
    End Property
    Dim fContractId As ContractsXpo
    <Association("Contract_CareGroupReferencesContract_Contract")> _
    Public Property ContractId() As ContractsXpo
        Get
            Return fContractId
        End Get
        Set(ByVal value As ContractsXpo)
            SetPropertyValue(Of ContractsXpo)("ContractId", fContractId, value)
        End Set
    End Property
    Dim fLiquidationType As Byte
    Public Property LiquidationType() As Byte
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("LiquidationType", fLiquidationType, value)
        End Set
    End Property
    Dim fBillingPeriod As Byte
    Public Property BillingPeriod() As Byte
        Get
            Return fBillingPeriod
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("BillingPeriod", fBillingPeriod, value)
        End Set
    End Property
    Dim fMaximumIndividualBilling As Decimal
    Public Property MaximumIndividualBilling() As Decimal
        Get
            Return fMaximumIndividualBilling
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MaximumIndividualBilling", fMaximumIndividualBilling, value)
        End Set
    End Property
    Dim fPeriodMaximumBilling As Decimal
    Public Property PeriodMaximumBilling() As Decimal
        Get
            Return fPeriodMaximumBilling
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PeriodMaximumBilling", fPeriodMaximumBilling, value)
        End Set
    End Property
    Dim fRequirementsTemplateId As Integer
    Public Property RequirementsTemplateId() As Integer
        Get
            Return fRequirementsTemplateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequirementsTemplateId", fRequirementsTemplateId, value)
        End Set
    End Property
    Dim fInvoiceDeadlines As Integer
    Public Property InvoiceDeadlines() As Integer
        Get
            Return fInvoiceDeadlines
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceDeadlines", fInvoiceDeadlines, value)
        End Set
    End Property
    Dim fProcedureTemplateId As Integer
    Public Property ProcedureTemplateId() As Integer
        Get
            Return fProcedureTemplateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProcedureTemplateId", fProcedureTemplateId, value)
        End Set
    End Property
    Dim fProductRateId As Integer
    Public Property ProductRateId() As Integer
        Get
            Return fProductRateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductRateId", fProductRateId, value)
        End Set
    End Property
    Dim fConceptToBill As Byte
    Public Property ConceptToBill() As Byte
        Get
            Return fConceptToBill
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptToBill", fConceptToBill, value)
        End Set
    End Property
    Dim fEntityType As Byte
    Public Property EntityType() As Byte
        Get
            Return fEntityType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("EntityType", fEntityType, value)
        End Set
    End Property
    'Dim fAffectedService As Boolean
    'Public Property AffectedService() As Boolean
    '    Get
    '        Return fAffectedService
    '    End Get
    '    Set(ByVal value As Boolean)
    '        SetPropertyValue(Of Boolean)("AffectedService", fAffectedService, value)
    '    End Set
    'End Property
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
    <Association("Billing_ServiceOrderDetailReferencesContract_CareGroup", GetType(OrderServiceDetailXpo))> _
    Public ReadOnly Property Billing_ServiceOrderDetails() As XPCollection(Of OrderServiceDetailXpo)
        Get
            Return GetCollection(Of OrderServiceDetailXpo)("Billing_ServiceOrderDetails")
        End Get
    End Property
    <Association("Billing_EntityCapitatedReferencesContract_CareGroup", GetType(InvoiceEntityCapitatedReportXpo))> _
    Public ReadOnly Property Billing_EntityCapitated() As XPCollection(Of InvoiceEntityCapitatedReportXpo)
        Get
            Return GetCollection(Of InvoiceEntityCapitatedReportXpo)("Billing_EntityCapitated")
        End Get
    End Property
    <Association("Billing_InvoiceReferencesContract_CareGroup", GetType(InvoicesXpo))>
    Public ReadOnly Property Billing_Invoices() As XPCollection(Of InvoicesXpo)
        Get
            Return GetCollection(Of InvoicesXpo)("Billing_Invoices")
        End Get
    End Property
    <Association("Contract_GroupersCareGroupReferencesContract_CareGroup", GetType(GroupersCareGroupXpo))>
    Public ReadOnly Property Contract_GroupersCareGroups() As XPCollection(Of GroupersCareGroupXpo)
        Get
            Return GetCollection(Of GroupersCareGroupXpo)("Contract_GroupersCareGroups")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
