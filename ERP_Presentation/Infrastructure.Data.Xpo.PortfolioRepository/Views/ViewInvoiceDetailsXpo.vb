Imports DevExpress.Xpo

<Persistent("Portfolio.ViewInvoiceDetails")>
Public Class ViewInvoiceDetailsXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key()>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
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

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
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

    Dim fBillingGroupCodeName As String
    Public Property BillingGroupCodeName() As String
        Get
            Return fBillingGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillingGroupCodeName", fBillingGroupCodeName, value)
        End Set
    End Property

    Dim fServiceDate As String
    Public Property ServiceDate() As String
        Get
            Return fServiceDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceDate", fServiceDate, value)
        End Set
    End Property

    Dim fCodeCups As String
    Public Property CodeCups() As String
        Get
            Return fCodeCups
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCups", fCodeCups, value)
        End Set
    End Property

    Dim fCodeName As String
    Public Property CodeName() As String
        Get
            Return fCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeName", fCodeName, value)
        End Set
    End Property

    Dim fAlternativeCodeName As String
    Public Property AlternativeCodeName() As String
        Get
            Return fAlternativeCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AlternativeCodeName", fAlternativeCodeName, value)
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

    Dim fMainAccountNumberName As String
    Public Property MainAccountNumberName() As String
        Get
            Return fMainAccountNumberName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccountNumberName", fMainAccountNumberName, value)
        End Set
    End Property

    Dim fCostCenterId As Integer?
    Public Property CostCenterId() As Integer?
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CostCenterId", fCostCenterId, value)
        End Set
    End Property

    Dim fCostCenterCodeName As String
    Public Property CostCenterCodeName() As String
        Get
            Return fCostCenterCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterCodeName", fCostCenterCodeName, value)
        End Set
    End Property

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fUnitSalesPrice As Decimal
    Public Property UnitSalesPrice() As Decimal
        Get
            Return fUnitSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UnitSalesPrice", fUnitSalesPrice, value)
        End Set
    End Property

    Dim fTotalSalesPrice As Decimal
    Public Property TotalSalesPrice() As Decimal
        Get
            Return fTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalSalesPrice", fTotalSalesPrice, value)
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

    Dim fTaxValueName As String
    Public Property TaxValueName() As String
        Get
            Return fTaxValueName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TaxValueName", fTaxValueName, value)
        End Set
    End Property

    Dim fTaxPercentage As Decimal?
    Public Property TaxPercentage() As Decimal?
        Get
            Return fTaxPercentage
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("TaxPercentage", fTaxPercentage, value)
        End Set
    End Property

    Dim fTaxId As Integer?
    Public Property TaxId() As Integer?
        Get
            Return fTaxId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("TaxId", fTaxId, value)
        End Set
    End Property

    Dim fnoteId As Integer?
    Public Property noteId() As Integer?
        Get
            Return fnoteId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("noteId", fnoteId, value)
        End Set
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
