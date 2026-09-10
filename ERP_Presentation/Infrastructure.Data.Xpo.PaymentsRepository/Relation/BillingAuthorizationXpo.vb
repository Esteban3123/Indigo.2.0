Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.BillingAuthorization")>
Public Class BillingAuthorizationXpo
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

    <PersistentAlias("concat(Code,' - ',Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fResolutionNumber As String
    <Size(50)>
    Public Property ResolutionNumber() As String
        Get
            Return fResolutionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResolutionNumber", fResolutionNumber, value)
        End Set
    End Property

    Dim fResolutionDate As DateTime
    Public Property ResolutionDate() As DateTime
        Get
            Return fResolutionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ResolutionDate", fResolutionDate, value)
        End Set
    End Property

    Dim fInvoicePrefix As String
    <Size(5)>
    Public Property InvoicePrefix() As String
        Get
            Return fInvoicePrefix
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoicePrefix", fInvoicePrefix, value)
        End Set
    End Property

    Dim fInitialInvoice As Long
    Public Property InitialInvoice() As Long
        Get
            Return fInitialInvoice
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("InitialInvoice", fInitialInvoice, value)
        End Set
    End Property

    Dim fFinalInvoice As Long
    Public Property FinalInvoice() As Long
        Get
            Return fFinalInvoice
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("FinalInvoice", fFinalInvoice, value)
        End Set
    End Property

    Dim fInvoiceType As Byte
    Public Property InvoiceType() As Byte
        Get
            Return fInvoiceType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("InvoiceType", fInvoiceType, value)
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

    Dim fTechnicalKey As String
    <Size(50)>
    Public Property TechnicalKey() As String
        Get
            Return fTechnicalKey
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TechnicalKey", fTechnicalKey, value)
        End Set
    End Property

    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fFinalDate As DateTime
    Public Property FinalDate() As DateTime
        Get
            Return fFinalDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FinalDate", fFinalDate, value)
        End Set
    End Property


#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class