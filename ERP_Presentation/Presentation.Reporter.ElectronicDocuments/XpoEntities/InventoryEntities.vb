'***********************************************************************
' Assembly         : Presentation.Reporter.ElectronicDocuments
' Entidades XPO para Inventory
' Adaptado para .NET 8
'***********************************************************************

Imports DevExpress.Xpo

Namespace XpoEntities

    ' Nota: InvoiceXpo y BillingAuthorizationXpo están definidos en BillingEntities.vb

#Region "InventoryDocumentInvoiceProductSalesDetailReportXpo"

    <Persistent("Inventory.DocumentInvoiceProductSalesDetail")>
    Public Class InventoryDocumentInvoiceProductSalesDetailReportXpo
        Inherits XPLiteObject

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

        Dim fDocumentInvoiceProductSalesId As DocumentInvoiceProductSalesXpo
        <Association("DocumentInvoiceProductSalesDetail_References_DocumentInvoiceProductSales")>
        Public Property DocumentInvoiceProductSalesId() As DocumentInvoiceProductSalesXpo
            Get
                Return fDocumentInvoiceProductSalesId
            End Get
            Set(ByVal value As DocumentInvoiceProductSalesXpo)
                SetPropertyValue(Of DocumentInvoiceProductSalesXpo)("DocumentInvoiceProductSalesId", fDocumentInvoiceProductSalesId, value)
            End Set
        End Property

        Public Property Code() As String
        Public Property Name() As String
        Public Property Quantity() As Integer
        Public Property Price() As Decimal
        Public Property Value() As Decimal
        Public Property PercentageIVA() As Decimal
        Public Property PercentageDiscount() As Decimal

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "DocumentInvoiceProductSalesXpo"

    <Persistent("Inventory.DocumentInvoiceProductSales")>
    Public Class DocumentInvoiceProductSalesXpo
        Inherits XPLiteObject

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

        Public Property Code() As String
        Public Property DocumentDate() As String
        Public Property CreationUser() As String
        Public Property Status() As Byte

        Dim fInvoiceId As InvoiceXpo
        Public Property InvoiceId() As InvoiceXpo
            Get
                Return fInvoiceId
            End Get
            Set(ByVal value As InvoiceXpo)
                SetPropertyValue(Of InvoiceXpo)("InvoiceId", fInvoiceId, value)
            End Set
        End Property

        Dim fBillingAuthorizationId As BillingAuthorizationXpo
        Public Property BillingAuthorizationId() As BillingAuthorizationXpo
            Get
                Return fBillingAuthorizationId
            End Get
            Set(ByVal value As BillingAuthorizationXpo)
                SetPropertyValue(Of BillingAuthorizationXpo)("BillingAuthorizationId", fBillingAuthorizationId, value)
            End Set
        End Property

        Dim fConditionSalesId As Integer?
        Public Property ConditionSalesId() As Integer?
            Get
                Return fConditionSalesId
            End Get
            Set(ByVal value As Integer?)
                SetPropertyValue(Of Integer?)("ConditionSalesId", fConditionSalesId, value)
            End Set
        End Property

        <Association("DocumentInvoiceProductSalesDetail_References_DocumentInvoiceProductSales", GetType(InventoryDocumentInvoiceProductSalesDetailReportXpo))>
        Public ReadOnly Property Details() As XPCollection(Of InventoryDocumentInvoiceProductSalesDetailReportXpo)
            Get
                Return GetCollection(Of InventoryDocumentInvoiceProductSalesDetailReportXpo)("Details")
            End Get
        End Property

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "BillingElectronicDocumentReportXpo"

    <Persistent("Billing.ElectronicDocument")>
    Public Class BillingElectronicDocumentReportXpo
        Inherits XPLiteObject

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

        Public Property EntityId() As String
        Public Property EntityName() As String
        Public Property Status() As Integer
        Public Property StatusName() As String
        Public Property ValidationDate() As DateTime?
        Public Property ShippingDate() As DateTime?

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

End Namespace
