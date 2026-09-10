Imports DevExpress.Xpo

<Persistent("Payments.AccountPayableDocumentSupport")>
Public Class AccountPayableDocumentSupportXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fAccountPayableId As PaymentsAccountPayable
    <Association("PaymentsAccountPayableDocumentSupport_References_PaymentsAccountPayable")>
    Public Property AccountPayableId() As PaymentsAccountPayable
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As PaymentsAccountPayable)
            SetPropertyValue(Of PaymentsAccountPayable)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property

    Dim fDocumentSupportId As PaymentsDocumentSupportXpo
    <Association("PaymentsAccountPayableDocumentSupport_References_DocumentSupport")>
    Public Property DocumentSupportId() As PaymentsDocumentSupportXpo
        Get
            Return fDocumentSupportId
        End Get
        Set(ByVal value As PaymentsDocumentSupportXpo)
            SetPropertyValue(Of PaymentsDocumentSupportXpo)("DocumentSupportId", fDocumentSupportId, value)
        End Set
    End Property

    Dim fInvoicePrefix As String
    Public Property InvoicePrefix() As String
        Get
            Return fInvoicePrefix
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoicePrefix", fInvoicePrefix, value)
        End Set
    End Property

    Dim fConsecutive As Long
    Public Property Consecutive() As Long
        Get
            Return fConsecutive
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Consecutive", fConsecutive, value)
        End Set
    End Property

#End Region

#Region "CustomProperties"

    <PersistentAlias("concat(InvoicePrefix,Consecutive)")>
    Public ReadOnly Property InvoiceNumber() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("InvoiceNumber"))
        End Get
    End Property

#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
