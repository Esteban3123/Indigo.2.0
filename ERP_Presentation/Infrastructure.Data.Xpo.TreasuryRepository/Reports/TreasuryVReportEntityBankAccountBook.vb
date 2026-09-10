Imports DevExpress.Xpo

<Persistent("Treasury.VReportEntityBankAccountBook")> _
Public Class TreasuryVReportEntityBankAccountBook
    Inherits XPLiteObject

#Region "Properties"

    Dim fRow As String
    <Key(True)>
    Public Property Row() As String
        Get
            Return fRow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Row", fRow, value)
        End Set
    End Property

    Dim fNameVoucher As String
    Public Property NameVoucher() As String
        Get
            Return fNameVoucher
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameVoucher", fNameVoucher, value)
        End Set
    End Property

    Dim fEntityBankAccountId As Integer
    Public Property EntityBankAccountId() As Integer
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property

    Dim fEntityBankAccountCode As String
    Public Property EntityBankAccountCode() As String
        Get
            Return fEntityBankAccountCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityBankAccountCode", fEntityBankAccountCode, value)
        End Set
    End Property

    Dim fEntityBankAccountName As String
    Public Property EntityBankAccountName() As String
        Get
            Return fEntityBankAccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityBankAccountName", fEntityBankAccountName, value)
        End Set
    End Property

    Dim fType As Byte
    Public Property Type() As Byte
        Get
            Return fType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Type", fType, value)
        End Set
    End Property

    Dim fEntityBankAccountNumber As String
    Public Property EntityBankAccountNumber() As String
        Get
            Return fEntityBankAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityBankAccountNumber", fEntityBankAccountNumber, value)
        End Set
    End Property

    Dim fEntityBankStatus As Boolean
    Public Property EntityBankStatus() As Boolean
        Get
            Return fEntityBankStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("EntityBankStatus", fEntityBankStatus, value)
        End Set
    End Property

    Dim fNumber As String
    Public Property Number() As String
        Get
            Return fNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Number", fNumber, value)
        End Set
    End Property

    Dim fThirdPartyNit As String
    Public Property ThirdPartyNit() As String
        Get
            Return fThirdPartyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNit", fThirdPartyNit, value)
        End Set
    End Property

    Dim fThirdPartyName As String
    Public Property ThirdPartyName() As String
        Get
            Return fThirdPartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyName", fThirdPartyName, value)
        End Set
    End Property

    Dim fCode As String
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

    Dim fDetail As String
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property

    Dim fVoucherType As Integer
    Public Property VoucherType() As Integer
        Get
            Return fVoucherType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("VoucherType", fVoucherType, value)
        End Set
    End Property

    Dim fValueDebit As Decimal
    Public Property ValueDebit() As Decimal
        Get
            Return fValueDebit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueDebit", fValueDebit, value)
        End Set
    End Property

    Dim fValueCredit As Decimal
    Public Property ValueCredit() As Decimal
        Get
            Return fValueCredit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueCredit", fValueCredit, value)
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

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
