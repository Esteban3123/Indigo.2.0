Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.RadicateInvoiceC")> _
Public Class PortfolioRadicateInvoiceCReportXpo
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
    Dim fRadicatedConsecutive As Integer
    Public Property RadicatedConsecutive() As Integer
        Get
            Return fRadicatedConsecutive
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RadicatedConsecutive", fRadicatedConsecutive, value)
        End Set
    End Property
    Dim fCustomerId As CommonCustomerReportXpo
    <Association("PortfolioRadicateInvoiceCReportXpoReferencesCommonCustomerReportXpo")> _
    Public Property CustomerId() As CommonCustomerReportXpo
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As CommonCustomerReportXpo)
            SetPropertyValue(Of CommonCustomerReportXpo)("CustomerId", fCustomerId, value)
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
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
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
    <PersistentAlias("Iif(State = '1', 'Sin Confirmar', State = '2', 'Confirmado', State = '4', 'Anulado', '')")>
    Public ReadOnly Property StateName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StateName"))
        End Get
    End Property
    Dim fRadicatedUser As Integer
    Public Property RadicatedUser() As Integer
        Get
            Return fRadicatedUser
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RadicatedUser", fRadicatedUser, value)
        End Set
    End Property
    Dim fComment As String
    <Size(250)> _
    Public Property Comment() As String
        Get
            Return fComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comment", fComment, value)
        End Set
    End Property
    Dim fRecognitionId As Integer
    Public Property RecognitionId() As Integer
        Get
            Return fRecognitionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RecognitionId", fRecognitionId, value)
        End Set
    End Property
    Dim fConfirmDateSystem As DateTime
    Public Property ConfirmDateSystem() As DateTime
        Get
            Return fConfirmDateSystem
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmDateSystem", fConfirmDateSystem, value)
        End Set
    End Property
    Dim fConfirmDate As DateTime
    Public Property ConfirmDate() As DateTime
        Get
            Return fConfirmDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmDate", fConfirmDate, value)
        End Set
    End Property
    Dim fConfirmUser As Integer
    Public Property ConfirmUser() As Integer
        Get
            Return fConfirmUser
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConfirmUser", fConfirmUser, value)
        End Set
    End Property
    Dim fConfirmComment As String
    <Size(250)> _
    Public Property ConfirmComment() As String
        Get
            Return fConfirmComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmComment", fConfirmComment, value)
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
    <Association("PortfolioRadicateInvoiceDReportXpoReferencesPortfolioRadicateInvoiceCReportXpo", GetType(PortfolioRadicateInvoiceDReportXpo))> _
    Public ReadOnly Property PortfolioRadicateInvoiceDReportXpo() As XPCollection(Of PortfolioRadicateInvoiceDReportXpo)
        Get
            Return GetCollection(Of PortfolioRadicateInvoiceDReportXpo)("PortfolioRadicateInvoiceDReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
