Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Portfolio.VReportExtractAccountReceivable")> _
Partial Public Class VReportExtractAccountReceivableXpo
    Inherits XPLiteObject

    Public Structure ProceduresExtractKeys
        <Persistent("ThirdPartyNit")> _
        Public Property ThirdPartyNit As String

        <Persistent("AccountNumber")> _
        Public Property AccountNumber As String

        <Persistent("MovesCode")> _
        Public Property MovesCode As String

        <Persistent("DocumentNumber")> _
        Public Property DocumentNumber As String

        <Persistent("TypeDocument")> _
        Public Property TypeDocument As String
    End Structure

    Dim fKey As Infrastructure.Data.Xpo.PortfolioRepository.VReportExtractAccountReceivableXpo.ProceduresExtractKeys

    <Persistent, Key(True)> _
    Public Property Key As Infrastructure.Data.Xpo.PortfolioRepository.VReportExtractAccountReceivableXpo.ProceduresExtractKeys
        Get
            Return fKey
        End Get
        Set(value As Infrastructure.Data.Xpo.PortfolioRepository.VReportExtractAccountReceivableXpo.ProceduresExtractKeys)
            fKey = value
        End Set
    End Property

    '<Key(True), PersistentAlias("Concat(ThirdPartyNit,'-',AccountNumber,'-',MovesCode,'-',DocumentNumber,'-',TypeDocument)")> _
    'Public WriteOnly Property Key As String
    '    Set(value As String)
    '        Convert.ToString(EvaluateAlias("Key"))
    '    End Set
    'End Property

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
    Dim fAccountNumber As String
    Public Property AccountNumber() As String
        Get
            Return fAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AccountNumber", fAccountNumber, value)
        End Set
    End Property
    Dim fAccountName As String
    Public Property AccountName() As String
        Get
            Return fAccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AccountName", fAccountName, value)
        End Set
    End Property
    Dim fTypeDocument As String
    Public Property TypeDocument() As String
        Get
            Return fTypeDocument
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TypeDocument", fTypeDocument, value)
        End Set
    End Property
    Dim fDocumentNumber As String
    Public Property DocumentNumber() As String
        Get
            Return fDocumentNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentNumber", fDocumentNumber, value)
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
    Dim fBillValueInitial As Decimal
    Public Property BillValueInitial() As Decimal
        Get
            Return fBillValueInitial
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BillValueInitial", fBillValueInitial, value)
        End Set
    End Property
    Dim fBillCurrentBalance As Decimal
    Public Property BillCurrentBalance() As Decimal
        Get
            Return fBillCurrentBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BillCurrentBalance", fBillCurrentBalance, value)
        End Set
    End Property
    Dim fMovesCode As String
    Public Property MovesCode() As String
        Get
            Return fMovesCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MovesCode", fMovesCode, value)
        End Set
    End Property
    Dim fMovesDate As DateTime
    Public Property MovesDate() As DateTime
        Get
            Return fMovesDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("MovesDate", fMovesDate, value)
        End Set
    End Property
    Dim fMovesDebit As Decimal
    Public Property MovesDebit() As Decimal
        Get
            Return fMovesDebit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MovesDebit", fMovesDebit, value)
        End Set
    End Property
    Dim fMovesCredit As Decimal
    Public Property MovesCredit() As Decimal
        Get
            Return fMovesCredit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MovesCredit", fMovesCredit, value)
        End Set
    End Property
    Dim fVoucherName As String
    Public Property VoucherName() As String
        Get
            Return fVoucherName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VoucherName", fVoucherName, value)
        End Set
    End Property
    Dim fIdCareGroup As Integer?
    Public Property IdCareGroup() As Integer?
        Get
            Return fIdCareGroup
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("IdCareGroup", fIdCareGroup, value)
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
    Dim fNumberName As String
    'columna que devuelve el numero y nombre de la cuenta
    <PersistentAlias("concat(concat(AccountNumber,' - '),AccountName)")>
    Public ReadOnly Property NumberName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NumberName"))
        End Get
    End Property
    Dim fCareGroupCode As String
    Public Property CareGroupCode() As String
        Get
            Return fCareGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupCode", fCareGroupCode, value)
        End Set
    End Property
    Dim fAccountReceivableType As Byte
    Public Property AccountReceivableType() As Byte
        Get
            Return fAccountReceivableType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AccountReceivableType", fAccountReceivableType, value)
        End Set
    End Property

    Dim fCurrencyName As String
    Public Property CurrencyName() As String
        Get
            Return fCurrencyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyName", fCurrencyName, value)
        End Set
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
