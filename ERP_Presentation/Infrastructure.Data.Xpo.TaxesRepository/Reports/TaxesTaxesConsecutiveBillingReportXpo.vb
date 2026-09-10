Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.TaxesConsecutiveBilling")> _
Public Class TaxesTaxesConsecutiveBillingReportXpo
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
    Dim fName As String
    <Size(200)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fInvoicePrefix As String
    <Size(5)> _
    Public Property InvoicePrefix() As String
        Get
            Return fInvoicePrefix
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoicePrefix", fInvoicePrefix, value)
        End Set
    End Property
    Dim fInitialInvoice As Integer
    Public Property InitialInvoice() As Integer
        Get
            Return fInitialInvoice
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InitialInvoice", fInitialInvoice, value)
        End Set
    End Property
    Dim fFinalInvoice As Integer
    Public Property FinalInvoice() As Integer
        Get
            Return fFinalInvoice
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FinalInvoice", fFinalInvoice, value)
        End Set
    End Property
    Dim fConsecutive As Integer
    Public Property Consecutive() As Integer
        Get
            Return fConsecutive
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Consecutive", fConsecutive, value)
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
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
