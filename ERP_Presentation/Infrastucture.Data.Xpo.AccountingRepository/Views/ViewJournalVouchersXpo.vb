Imports DevExpress.Xpo

<Persistent("GeneralLedger.ViewJournalVouchers")>
Public Class ViewJournalVouchersXpo
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

    Dim fLegalBookId As Integer
    Public Property LegalBookId() As Integer
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LegalBookId", fLegalBookId, value)
        End Set
    End Property

    Dim fJournalVoucherTypeCode As String
    Public Property JournalVoucherTypeCode() As String
        Get
            Return fJournalVoucherTypeCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("JournalVoucherTypeCode", fJournalVoucherTypeCode, value)
        End Set
    End Property

    Dim fJournalVoucherTypeName As String
    Public Property JournalVoucherTypeName() As String
        Get
            Return fJournalVoucherTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("JournalVoucherTypeName", fJournalVoucherTypeName, value)
        End Set
    End Property

    Dim fConsecutive As Int64
    Public Property Consecutive() As Int64
        Get
            Return fConsecutive
        End Get
        Set(ByVal value As Int64)
            SetPropertyValue(Of Int64)("Consecutive", fConsecutive, value)
        End Set
    End Property

    Dim fVoucherDate As DateTime
    Public Property VoucherDate() As DateTime
        Get
            Return fVoucherDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("VoucherDate", fVoucherDate, value)
        End Set
    End Property

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fEntityCode As String
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property

    Dim fDetail As String
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property

    Dim fStatusName As String
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
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

#End Region

#Region "Builders"

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