Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Inventory.InventoryProduct")> _
Public Class InventoryProductReportXpo
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

    Dim fProductTypeId As Integer
    Public Property ProductTypeId() As Integer
        Get
            Return fProductTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductTypeId", fProductTypeId, value)
        End Set
    End Property

    Dim fATCId As Integer
    Public Property ATCId() As Integer
        Get
            Return fATCId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ATCId", fATCId, value)
        End Set
    End Property

    Dim fCodeCUM As String
    Public Property CodeCUM() As String
        Get
            Return fCodeCUM
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCUM", fCodeCUM, value)
        End Set
    End Property

    Dim fCodeAlternative As String
    Public Property CodeAlternative() As String
        Get
            Return fCodeAlternative
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAlternative", fCodeAlternative, value)
        End Set
    End Property

    Dim fCodeAlternativeTwo As String
    Public Property CodeAlternativeTwo() As String
        Get
            Return fCodeAlternativeTwo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAlternativeTwo", fCodeAlternativeTwo, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fProductGroupId As Integer
    Public Property ProductGroupId() As Integer
        Get
            Return fProductGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductGroupId", fProductGroupId, value)
        End Set
    End Property

    Dim fProductSubGroupId As Integer
    Public Property ProductSubGroupId() As Integer
        Get
            Return fProductSubGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductSubGroupId", fProductSubGroupId, value)
        End Set
    End Property

    Dim fPackagingUnitId As Integer
    Public Property PackagingUnitId() As Integer
        Get
            Return fPackagingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PackagingUnitId", fPackagingUnitId, value)
        End Set
    End Property

    Dim fManufacturerId As Integer
    Public Property ManufacturerId() As Integer
        Get
            Return fManufacturerId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ManufacturerId", fManufacturerId, value)
        End Set
    End Property

    Dim fIVAId As Integer
    Public Property IVAId() As Integer
        Get
            Return fIVAId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IVAId", fIVAId, value)
        End Set
    End Property

    Dim fPresentation As String
    Public Property Presentation() As String
        Get
            Return fPresentation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Presentation", fPresentation, value)
        End Set
    End Property

    Dim fCodeSICE As String
    Public Property CodeSICE() As String
        Get
            Return fCodeSICE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeSICE", fCodeSICE, value)
        End Set
    End Property

    Dim fHandlesSerial As Boolean
    Public Property HandlesSerial() As Boolean
        Get
            Return fHandlesSerial
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesSerial", fHandlesSerial, value)
        End Set
    End Property

    Dim fHandlesHealthRegistration As Boolean
    Public Property HandlesHealthRegistration() As Boolean
        Get
            Return fHandlesHealthRegistration
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesHealthRegistration", fHandlesHealthRegistration, value)
        End Set
    End Property

    Dim fHealthRegistration As String
    Public Property HealthRegistration() As String
        Get
            Return fHealthRegistration
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthRegistration", fHealthRegistration, value)
        End Set
    End Property

    Dim fExpirationDate As DateTime
    Public Property ExpirationDate() As DateTime
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ExpirationDate", fExpirationDate, value)
        End Set
    End Property

    Dim fBillingGroupId As GroupBillingXpo
    <Association("Inventory_ProductReferencesBilling_BillingGroup")>
    Public Property BillingGroupId() As GroupBillingXpo
        Get
            Return fBillingGroupId
        End Get
        Set(ByVal value As GroupBillingXpo)
            SetPropertyValue(Of GroupBillingXpo)("BillingGroupId", fBillingGroupId, value)
        End Set
    End Property

    Dim fProductControl As Boolean
    Public Property ProductControl() As Boolean
        Get
            Return fProductControl
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ProductControl", fProductControl, value)
        End Set
    End Property

    Dim fProductWithPriceControl As Boolean
    Public Property ProductWithPriceControl() As Boolean
        Get
            Return fProductWithPriceControl
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ProductWithPriceControl", fProductWithPriceControl, value)
        End Set
    End Property

    Dim fPOSProduct As Boolean
    Public Property POSProduct() As Boolean
        Get
            Return fPOSProduct
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("POSProduct", fPOSProduct, value)
        End Set
    End Property

    Dim fAuthorizationByOrderNumber As Integer
    Public Property AuthorizationByOrderNumber() As Integer
        Get
            Return fAuthorizationByOrderNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AuthorizationByOrderNumber", fAuthorizationByOrderNumber, value)
        End Set
    End Property

    Dim fExpirationDay As Integer
    Public Property ExpirationDay() As Integer
        Get
            Return fExpirationDay
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ExpirationDay", fExpirationDay, value)
        End Set
    End Property

    Dim fMaximumControlPeriod As Boolean
    Public Property MaximumControlPeriod() As Boolean
        Get
            Return fMaximumControlPeriod
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("MaximumControlPeriod", fMaximumControlPeriod, value)
        End Set
    End Property

    Dim fControlDays As Integer
    Public Property ControlDays() As Integer
        Get
            Return fControlDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ControlDays", fControlDays, value)
        End Set
    End Property

    Dim fControlOrderQuantity As Boolean
    Public Property ControlOrderQuantity() As Boolean
        Get
            Return fControlOrderQuantity
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ControlOrderQuantity", fControlOrderQuantity, value)
        End Set
    End Property

    Dim fProductOrderAmount As Integer
    Public Property ProductOrderAmount() As Integer
        Get
            Return fProductOrderAmount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductOrderAmount", fProductOrderAmount, value)
        End Set
    End Property

    Dim fLastPurchase As DateTime
    Public Property LastPurchase() As DateTime
        Get
            Return fLastPurchase
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastPurchase", fLastPurchase, value)
        End Set
    End Property

    Dim fLastSale As DateTime
    Public Property LastSale() As DateTime
        Get
            Return fLastSale
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastSale", fLastSale, value)
        End Set
    End Property

    Dim fProductOrigin As Byte
    Public Property ProductOrigin() As Byte
        Get
            Return fProductOrigin
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ProductOrigin", fProductOrigin, value)
        End Set
    End Property

    Dim fMinimumStock As Integer
    Public Property MinimumStock() As Integer
        Get
            Return fMinimumStock
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MinimumStock", fMinimumStock, value)
        End Set
    End Property

    Dim fMaximumStock As Integer
    Public Property MaximumStock() As Integer
        Get
            Return fMaximumStock
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MaximumStock", fMaximumStock, value)
        End Set
    End Property

    Dim fCommissionPercentage As Decimal
    Public Property CommissionPercentage() As Decimal
        Get
            Return fCommissionPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CommissionPercentage", fCommissionPercentage, value)
        End Set
    End Property

    Dim fRepositionPoint As Integer
    Public Property RepositionPoint() As Integer
        Get
            Return fRepositionPoint
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RepositionPoint", fRepositionPoint, value)
        End Set
    End Property

    Dim fResetTime As Integer
    Public Property ResetTime() As Integer
        Get
            Return fResetTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ResetTime", fResetTime, value)
        End Set
    End Property

    Dim fCurrencyType As Byte
    Public Property CurrencyType() As Byte
        Get
            Return fCurrencyType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CurrencyType", fCurrencyType, value)
        End Set
    End Property

    Dim fProductCost As Decimal
    Public Property ProductCost() As Decimal
        Get
            Return fProductCost
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ProductCost", fProductCost, value)
        End Set
    End Property

    Dim fFinalProductCost As Decimal
    Public Property FinalProductCost() As Decimal
        Get
            Return fFinalProductCost
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FinalProductCost", fFinalProductCost, value)
        End Set
    End Property

    Dim fSellingPrice As Decimal
    Public Property SellingPrice() As Decimal
        Get
            Return fSellingPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SellingPrice", fSellingPrice, value)
        End Set
    End Property

    Dim fAllPOSPathologies As Boolean
    Public Property AllPOSPathologies() As Boolean
        Get
            Return fAllPOSPathologies
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllPOSPathologies", fAllPOSPathologies, value)
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

#End Region

#Region "Navigation"

    <Association("Billing_OrderServiceDetailReferencesInventory_InventoryProduct", GetType(OrderServiceDetailXpo))>
    Public ReadOnly Property Billing_OrderServiceDetailInventoryProduct() As XPCollection(Of OrderServiceDetailXpo)
        Get
            Return GetCollection(Of OrderServiceDetailXpo)("Billing_OrderServiceDetailInventoryProduct")
        End Get
    End Property

    <Association("BasicBillingDetail_References_Product", GetType(BasicBillingDetailReportXpo))>
    Public ReadOnly Property BasicBillingDetails() As XPCollection(Of BasicBillingDetailReportXpo)
        Get
            Return GetCollection(Of BasicBillingDetailReportXpo)("BasicBillingDetails")
        End Get
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
