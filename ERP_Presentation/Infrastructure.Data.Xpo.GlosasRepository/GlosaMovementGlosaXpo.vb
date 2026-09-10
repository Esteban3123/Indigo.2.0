Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Glosas.GlosaMovementGlosa")> _
Partial Public Class GlosaMovementGlosaXpo
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
    Dim fInvoiceDetailId As Integer
    <Indexed("InvoiceDetailIdQX;CodeGlosa", Name:="IX_GlosaMovementGlosa", Unique:=True)> _
    Public Property InvoiceDetailId() As Integer
        Get
            Return fInvoiceDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceDetailId", fInvoiceDetailId, value)
        End Set
    End Property
    Dim fInvoiceDetailIdQX As Integer
    Public Property InvoiceDetailIdQX() As Integer
        Get
            Return fInvoiceDetailIdQX
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceDetailIdQX", fInvoiceDetailIdQX, value)
        End Set
    End Property
    Dim fInvoiceNumber As String
    <Indexed("ResponsibleId;ResponsibleReiterationId;State", Name:="IX_InvoiceNumberResponsibles")> _
    <Size(50)> _
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property
    Dim fCodeGlosaId As Integer
    Public Property CodeGlosaId() As Integer
        Get
            Return fCodeGlosaId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CodeGlosaId", fCodeGlosaId, value)
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
    Dim fResponsibleId As Integer
    Public Property ResponsibleId() As Integer
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ResponsibleId", fResponsibleId, value)
        End Set
    End Property
    Dim fResponsibleReiterationId As Integer
    Public Property ResponsibleReiterationId() As Integer
        Get
            Return fResponsibleReiterationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ResponsibleReiterationId", fResponsibleReiterationId, value)
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
    Dim fValuePayments As Decimal
    Public Property ValuePayments() As Decimal
        Get
            Return fValuePayments
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValuePayments", fValuePayments, value)
        End Set
    End Property
    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("State", fState, value)
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
    Dim fRationaleDateGlosa As DateTime
    Public Property RationaleDateGlosa() As DateTime
        Get
            Return fRationaleDateGlosa
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RationaleDateGlosa", fRationaleDateGlosa, value)
        End Set
    End Property
    Dim fRationaleReiteration As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property RationaleReiteration() As String
        Get
            Return fRationaleReiteration
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RationaleReiteration", fRationaleReiteration, value)
        End Set
    End Property
    Dim fRationaleDateReiteration As DateTime
    Public Property RationaleDateReiteration() As DateTime
        Get
            Return fRationaleDateReiteration
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RationaleDateReiteration", fRationaleDateReiteration, value)
        End Set
    End Property
    Dim fRationaleConciliation As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property RationaleConciliation() As String
        Get
            Return fRationaleConciliation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RationaleConciliation", fRationaleConciliation, value)
        End Set
    End Property
    Dim fRationaleDateConciliation As DateTime
    Public Property RationaleDateConciliation() As DateTime
        Get
            Return fRationaleDateConciliation
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RationaleDateConciliation", fRationaleDateConciliation, value)
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
    Dim fTypeConcept As Char
    Public Property TypeConcept() As Char
        Get
            Return fTypeConcept
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("TypeConcept", fTypeConcept, value)
        End Set
    End Property
    Dim fTempState As Byte
    Public Property TempState() As Byte
        Get
            Return fTempState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TempState", fTempState, value)
        End Set
    End Property
    Dim fJustificationGlosa As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property JustificationGlosa() As String
        Get
            Return fJustificationGlosa
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("JustificationGlosa", fJustificationGlosa, value)
        End Set
    End Property


    Dim fJustificationGlosatext As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property JustificationGlosaText() As String
        Get
            Return fJustificationGlosatext
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("JustificationGlosaText", fJustificationGlosatext, value)
        End Set
    End Property


    Dim fJustificationReiteration As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property JustificationReiteration() As String
        Get
            Return fJustificationReiteration
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("JustificationReiteration", fJustificationReiteration, value)
        End Set
    End Property

    Dim fJustificationReiterationText As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property JustificationReiterationText() As String
        Get
            Return fJustificationReiterationText
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("JustificationReiterationText", fJustificationReiterationText, value)
        End Set
    End Property


    Dim fIdGlosaEvaluation As Integer
    Public Property IdGlosaEvaluation() As Integer
        Get
            Return fIdGlosaEvaluation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdGlosaEvaluation", fIdGlosaEvaluation, value)
        End Set
    End Property
    Dim fCodeGlosaEvaluation As String
    <Size(50)> _
    Public Property CodeGlosaEvaluation() As String
        Get
            Return fCodeGlosaEvaluation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeGlosaEvaluation", fCodeGlosaEvaluation, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class