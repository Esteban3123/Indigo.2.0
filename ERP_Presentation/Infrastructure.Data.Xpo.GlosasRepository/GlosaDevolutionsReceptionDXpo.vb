Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Glosas.GlosaDevolutionsReceptionD")> _
Partial Public Class GlosaDevolutionsReceptionDXpo
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
    Dim fGlosaDevolutionsReceptionCId As GlosaDevolutionsReceptionCXpo
    <Association("Glosas_GlosaDevolutionsReceptionDReferencesGlosas_GlosaDevolutionsReceptionC")> _
    Public Property GlosaDevolutionsReceptionCId() As GlosaDevolutionsReceptionCXpo
        Get
            Return fGlosaDevolutionsReceptionCId
        End Get
        Set(ByVal value As GlosaDevolutionsReceptionCXpo)
            SetPropertyValue(Of GlosaDevolutionsReceptionCXpo)("GlosaDevolutionsReceptionCId", fGlosaDevolutionsReceptionCId, value)
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
    Dim fBalanceInvoice As Decimal
    Public Property BalanceInvoice() As Decimal
        Get
            Return fBalanceInvoice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceInvoice", fBalanceInvoice, value)
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
    Dim fPatientName As String
    <Size(200)> _
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientName", fPatientName, value)
        End Set
    End Property
    Dim fIngress As String
    <Size(15)> _
    Public Property Ingress() As String
        Get
            Return fIngress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Ingress", fIngress, value)
        End Set
    End Property
    Dim fUserNameInvoice As String
    <Size(200)> _
    Public Property UserNameInvoice() As String
        Get
            Return fUserNameInvoice
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserNameInvoice", fUserNameInvoice, value)
        End Set
    End Property
    Dim fComment As String
    <Size(500)> _
    Public Property Comment() As String
        Get
            Return fComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comment", fComment, value)
        End Set
    End Property
    Dim fContractCode As String
    <Size(10)> _
    Public Property ContractCode() As String
        Get
            Return fContractCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractCode", fContractCode, value)
        End Set
    End Property
    Dim fContractName As String
    <Size(200)> _
    Public Property ContractName() As String
        Get
            Return fContractName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractName", fContractName, value)
        End Set
    End Property
    Dim fState As Char
    Public Property State() As Char
        Get
            Return fState
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("State", fState, value)
        End Set
    End Property
    <Association("Glosas_GlosaMovementDevolutionsReferencesGlosas_GlosaDevolutionsReceptionD", GetType(GlosaMovementDevolutionsXpo))> _
    Public ReadOnly Property Glosas_GlosaMovementDevolutionsCollection() As XPCollection(Of GlosaMovementDevolutionsXpo)
        Get
            Return GetCollection(Of GlosaMovementDevolutionsXpo)("Glosas_GlosaMovementDevolutionsCollection")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
