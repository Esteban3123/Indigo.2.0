Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources

<Persistent("Portfolio.ViewListPortfolioTransferDetail")> _
Public Class ViewListPortfolioTransferDetailXpo
    Inherits XPLiteObject
#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region

#Region "Members"

    Dim fPortfolioTransferDetailId As Integer
    <Key(True)>
    Public Property PortfolioTransferDetailId() As Integer
        Get
            Return fPortfolioTransferDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PortfolioTransferDetailId", fPortfolioTransferDetailId, value)
        End Set
    End Property

    Dim fPortfolioTransferId As Integer
    Public Property PortfolioTransferId() As Integer
        Get
            Return fPortfolioTransferId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PortfolioTransferId", fPortfolioTransferId, value)
        End Set
    End Property

    Dim fAccountReceivableId As Integer
    Public Property AccountReceivableId() As Integer
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property

    Dim fMainAccountId As Integer
    Public Property MainAccountId() As Integer
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MainAccountId", fMainAccountId, value)
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

    Dim fValueBill As Decimal
    Public Property ValueBill() As Decimal
        Get
            Return fValueBill
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueBill", fValueBill, value)
        End Set
    End Property

    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property

    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property

    Dim fCodeNameMainAccount As String
    Public Property CodeNameMainAccount() As String
        Get
            Return fCodeNameMainAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeNameMainAccount", fCodeNameMainAccount, value)
        End Set
    End Property

    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
        End Set
    End Property

    Dim fPortfolioStatusName As String
    Public Property PortfolioStatusName() As String
        Get
            Return fPortfolioStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PortfolioStatusName", fPortfolioStatusName, value)
        End Set
    End Property

    Dim fCurrencyAbbreviation As String
    Public Property CurrencyAbbreviation() As String
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
        End Set
    End Property
    Dim fNamePatient As String
    Public Property Patient() As String
        Get
            Return fNamePatient
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Patient", fNamePatient, value)
        End Set
    End Property
#End Region
End Class
