Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.PaymentsRepository

<Persistent("FixedAsset.FixedAssetPurchaseOrder")> _
Public Class FixedAssetPurchaseOrderXpo
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

    Dim fPurchaseOrderDate As Date
    Public Property PurchaseOrderDate() As Date
        Get
            Return fPurchaseOrderDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("PurchaseOrderDate", fPurchaseOrderDate, value)
        End Set
    End Property

    Dim fDeliverDate As Date
    Public Property DeliverDate() As Date
        Get
            Return fDeliverDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("DeliverDate", fDeliverDate, value)
        End Set
    End Property

    Dim fSupplierDistributionLineId As CommonSupplierDistributionLineXpo
    <Association("FixedAsset_FixedAssetPurchaseOrderXpo_References_Common_SupplierDistributionLineXpo")>
    Public Property SupplierDistributionLineId() As CommonSupplierDistributionLineXpo
        Get
            Return fSupplierDistributionLineId
        End Get
        Set(ByVal value As CommonSupplierDistributionLineXpo)
            SetPropertyValue(Of CommonSupplierDistributionLineXpo)("SupplierDistributionLineId", fSupplierDistributionLineId, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferenceFixedAssetPurchaseOrderXpo")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation"))
        End Get
    End Property

    'Dim fCurrency As CommonCurrencyXpo
    '<Association("FixedAsset_FixedAssetPurchaseOrderXpo_References_Common_CurrencyXpo")>
    'Public Property Currency() As CommonCurrencyXpo
    '    Get
    '        Return fCurrency
    '    End Get
    '    Set(ByVal value As CommonCurrencyXpo)
    '        SetPropertyValue(Of CommonCurrencyXpo)("Currency", fCurrency, value)
    '    End Set
    'End Property

    '<PersistentAlias("Currency.Id")>
    'Public ReadOnly Property CurrencyId As Integer
    '    Get
    '        Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
    '    End Get
    'End Property

    '<PersistentAlias("Currency.Abbreviation")>
    'Public ReadOnly Property CurrencyAbbreviation As String
    '    Get
    '        Return Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))
    '    End Get
    'End Property

    Dim fDetail As String
    <Size(300)> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
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

    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', '')))")>
    Public ReadOnly Property StatusName As String
        Get
            'Select Case fStatus
            '    Case 1
            '        Return "Sin Confirmar"
            '    Case 2
            '        Return "Confirmado"
            '    Case 3
            '        Return "Anulado"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
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

    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property

    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property

    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property

    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property

    <Association("FixedAssetPurchaseOrderReferences", GetType(FixedAssetPurchaseOrderEquipmentXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderEquipmentXpo() As XPCollection(Of FixedAssetPurchaseOrderEquipmentXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderEquipmentXpo)("FixedAssetPurchaseOrderEquipmentXpo")
        End Get
    End Property

    <Association("PurchaseOrderItemReferencePurchaseOrder", GetType(FixedAssetPurchaseOrderItemXpo))>
    Public ReadOnly Property FixedAssetPurchaseOrderItemXpo() As XPCollection(Of FixedAssetPurchaseOrderItemXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderItemXpo)("FixedAssetPurchaseOrderItemXpo")
        End Get
    End Property

    <PersistentAlias("FixedAssetPurchaseOrderItemXpo.Sum(TotalValue)")>
    Public ReadOnly Property TotalValue() As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("TotalValue"))
        End Get
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

