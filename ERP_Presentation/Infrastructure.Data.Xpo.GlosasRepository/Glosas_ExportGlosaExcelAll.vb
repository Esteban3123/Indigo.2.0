Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Glosas.ExportGlosaExcelAll")> _
Public Class Glosas_ExportGlosaExcelAll
    Inherits XPLiteObject
    Dim fRow As Integer
    <Key(True)> _
    Public Property Row() As Integer
        Get
            Return fRow
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Row", fRow, value)
        End Set
    End Property
    Dim fId As Integer
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fidInvoice As Integer
    Public Property idInvoice() As Integer
        Get
            Return fidInvoice
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("idInvoice", fidInvoice, value)
        End Set
    End Property
    Dim fCodeMovement As Integer
    Public Property CodeMovement() As Integer
        Get
            Return fCodeMovement
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CodeMovement", fCodeMovement, value)
        End Set
    End Property
    Dim fInvoiceNumber As String
    <Size(50)> _
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property
    Dim fServiceCode As String
    <Size(20)> _
    Public Property ServiceCode() As String
        Get
            Return fServiceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceCode", fServiceCode, value)
        End Set
    End Property
    Dim fServiceName As String
    <Size(250)> _
    Public Property ServiceName() As String
        Get
            Return fServiceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceName", fServiceName, value)
        End Set
    End Property
    Dim fServiceNameQX As String
    <Size(300)> _
    Public Property ServiceNameQX() As String
        Get
            Return fServiceNameQX
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceNameQX", fServiceNameQX, value)
        End Set
    End Property
    Dim fCostCenterCode As String
    <Size(14)> _
    Public Property CostCenterCode() As String
        Get
            Return fCostCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterCode", fCostCenterCode, value)
        End Set
    End Property
    Dim fCostCenterName As String
    <Size(500)> _
    Public Property CostCenterName() As String
        Get
            Return fCostCenterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterName", fCostCenterName, value)
        End Set
    End Property
    Dim fMainGlosa As Boolean
    Public Property MainGlosa() As Boolean
        Get
            Return fMainGlosa
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("MainGlosa", fMainGlosa, value)
        End Set
    End Property
    Dim fCodeGlosa As String
    <Size(50)> _
    Public Property CodeGlosa() As String
        Get
            Return fCodeGlosa
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeGlosa", fCodeGlosa, value)
        End Set
    End Property
    Dim fNameSpecific As String
    <Size(200)> _
    Public Property NameSpecific() As String
        Get
            Return fNameSpecific
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameSpecific", fNameSpecific, value)
        End Set
    End Property
    Dim fRationaleGlosa As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property RationaleGlosa() As String
        Get
            Return fRationaleGlosa
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RationaleGlosa", fRationaleGlosa, value)
        End Set
    End Property
    Dim fValueGlosado As Decimal
    Public Property ValueGlosado() As Decimal
        Get
            Return fValueGlosado
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueGlosado", fValueGlosado, value)
        End Set
    End Property
    Dim fValueAcceptedFirstInstance As Decimal
    Public Property ValueAcceptedFirstInstance() As Decimal
        Get
            Return fValueAcceptedFirstInstance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueAcceptedFirstInstance", fValueAcceptedFirstInstance, value)
        End Set
    End Property
    Dim fValueReiterated As Decimal
    Public Property ValueReiterated() As Decimal
        Get
            Return fValueReiterated
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueReiterated", fValueReiterated, value)
        End Set
    End Property
    Dim fValueReiterationBalance As Decimal
    Public Property ValueReiterationBalance() As Decimal
        Get
            Return fValueReiterationBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueReiterationBalance", fValueReiterationBalance, value)
        End Set
    End Property
    Dim fValueAcceptedSecondInstance As Decimal
    Public Property ValueAcceptedSecondInstance() As Decimal
        Get
            Return fValueAcceptedSecondInstance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueAcceptedSecondInstance", fValueAcceptedSecondInstance, value)
        End Set
    End Property
    Dim fValueAcceptedIPSconciliation As Decimal
    Public Property ValueAcceptedIPSconciliation() As Decimal
        Get
            Return fValueAcceptedIPSconciliation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueAcceptedIPSconciliation", fValueAcceptedIPSconciliation, value)
        End Set
    End Property
    Dim fValueAcceptedEAPBconciliation As Decimal
    Public Property ValueAcceptedEAPBconciliation() As Decimal
        Get
            Return fValueAcceptedEAPBconciliation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueAcceptedEAPBconciliation", fValueAcceptedEAPBconciliation, value)
        End Set
    End Property
    Dim fValuePendingConciliation As Decimal
    Public Property ValuePendingConciliation() As Decimal
        Get
            Return fValuePendingConciliation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValuePendingConciliation", fValuePendingConciliation, value)
        End Set
    End Property
    Dim fRadicatedConsecutive As Integer
    Public Property RadicatedConsecutive() As Integer
        Get
            Return fRadicatedConsecutive
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RadicatedConsecutive", fRadicatedConsecutive, value)
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
    Dim fValuePayments As Decimal
    Public Property ValuePayments() As Decimal
        Get
            Return fValuePayments
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValuePayments", fValuePayments, value)
        End Set
    End Property
    Dim fInvoiceDate As DateTime
    Public Property InvoiceDate() As DateTime
        Get
            Return fInvoiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoiceDate", fInvoiceDate, value)
        End Set
    End Property
    Dim fRadicatedNumber As String
    <Size(50)> _
    Public Property RadicatedNumber() As String
        Get
            Return fRadicatedNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RadicatedNumber", fRadicatedNumber, value)
        End Set
    End Property
    Dim fRadicatedDate As DateTime
    Public Property RadicatedDate() As DateTime
        Get
            Return fRadicatedDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RadicatedDate", fRadicatedDate, value)
        End Set
    End Property
    Dim fCodeConceptEvaluation As String
    <Size(3)> _
    Public Property CodeConceptEvaluation() As String
        Get
            Return fCodeConceptEvaluation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeConceptEvaluation", fCodeConceptEvaluation, value)
        End Set
    End Property
    Dim fNameSpecificEvaluation As String
    <Size(200)> _
    Public Property NameSpecificEvaluation() As String
        Get
            Return fNameSpecificEvaluation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameSpecificEvaluation", fNameSpecificEvaluation, value)
        End Set
    End Property
    Dim fJustificationGlosaText As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property JustificationGlosaText() As String
        Get
            Return fJustificationGlosaText
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("JustificationGlosaText", fJustificationGlosaText, value)
        End Set
    End Property
    Dim fIngressNumber As String
    <Size(15)> _
    Public Property IngressNumber() As String
        Get
            Return fIngressNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IngressNumber", fIngressNumber, value)
        End Set
    End Property
    Dim fIngressDate As DateTime
    Public Property IngressDate() As DateTime
        Get
            Return fIngressDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("IngressDate", fIngressDate, value)
        End Set
    End Property
    Dim fPatientCode As String
    <Size(20)> _
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub


End Class


