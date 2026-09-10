Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.ViewReportPortfolioDeterioratingProvision")> _
Public Class PortfolioViewReportPortfolioDeterioratingProvisionReportXpo
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
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fCourtDate As DateTime
    Public Property CourtDate() As DateTime
        Get
            Return fCourtDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CourtDate", fCourtDate, value)
        End Set
    End Property
    Dim fDocumentType As String
    <Size(9)> _
    Public Property DocumentType() As String
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentType", fDocumentType, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(500)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fAgeRange As String
    <Size(36)> _
    Public Property AgeRange() As String
        Get
            Return fAgeRange
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AgeRange", fAgeRange, value)
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
    Dim fBalanceAccountReceivable As Decimal
    Public Property BalanceAccountReceivable() As Decimal
        Get
            Return fBalanceAccountReceivable
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceAccountReceivable", fBalanceAccountReceivable, value)
        End Set
    End Property
    Dim fConfirmDateAccountReceivable As DateTime
    Public Property ConfirmDateAccountReceivable() As DateTime
        Get
            Return fConfirmDateAccountReceivable
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmDateAccountReceivable", fConfirmDateAccountReceivable, value)
        End Set
    End Property
    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
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


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
