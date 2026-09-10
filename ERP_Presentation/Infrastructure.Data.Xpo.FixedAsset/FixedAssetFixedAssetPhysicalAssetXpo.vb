Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPhysicalAsset")> _
Public Class FixedAssetFixedAssetPhysicalAssetXpo
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
    Dim fItemId As FixedAssetFixedAssetItemXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetItem")> _
    Public Property ItemId() As FixedAssetFixedAssetItemXpo
        Get
            Return fItemId
        End Get
        Set(ByVal value As FixedAssetFixedAssetItemXpo)
            SetPropertyValue(Of FixedAssetFixedAssetItemXpo)("ItemId", fItemId, value)
        End Set
    End Property
    Dim fSerie As String
    <Size(50)> _
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
        End Set
    End Property
    Dim fPlate As String
    <Size(50)> _
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
        End Set
    End Property
    Dim fLocationId As FixedAssetFixedAssetLocationXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetLocation")> _
    Public Property LocationId() As FixedAssetFixedAssetLocationXpo
        Get
            Return fLocationId
        End Get
        Set(ByVal value As FixedAssetFixedAssetLocationXpo)
            SetPropertyValue(Of FixedAssetFixedAssetLocationXpo)("LocationId", fLocationId, value)
        End Set
    End Property
    Dim fResponsibleId As FixedAssetResponsibleXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetResponsible")> _
    Public Property ResponsibleId() As FixedAssetResponsibleXpo
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleXpo)
            SetPropertyValue(Of FixedAssetResponsibleXpo)("ResponsibleId", fResponsibleId, value)
        End Set
    End Property
    Dim fSupplierId As Integer
    Public Property SupplierId() As Integer
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierId", fSupplierId, value)
        End Set
    End Property
    Dim fHistoricalValue As Decimal
    Public Property HistoricalValue() As Decimal
        Get
            Return fHistoricalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HistoricalValue", fHistoricalValue, value)
        End Set
    End Property
    Dim fFairValue As Decimal
    Public Property FairValue() As Decimal
        Get
            Return fFairValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FairValue", fFairValue, value)
        End Set
    End Property
    Dim fTrademarkId As FixedAssetTrademarkXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetTrademark")> _
    Public Property TrademarkId() As FixedAssetTrademarkXpo
        Get
            Return fTrademarkId
        End Get
        Set(ByVal value As FixedAssetTrademarkXpo)
            SetPropertyValue(Of FixedAssetTrademarkXpo)("TrademarkId", fTrademarkId, value)
        End Set
    End Property
    Dim fModel As String
    Public Property Model() As String
        Get
            Return fModel
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Model", fModel, value)
        End Set
    End Property
    Dim fPolicyId As Integer
    Public Property PolicyId() As Integer
        Get
            Return fPolicyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PolicyId", fPolicyId, value)
        End Set
    End Property
    Dim fHandlesWarranty As Boolean
    Public Property HandlesWarranty() As Boolean
        Get
            Return fHandlesWarranty
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesWarranty", fHandlesWarranty, value)
        End Set
    End Property
    Dim fWarrantyExpirationDate As DateTime
    Public Property WarrantyExpirationDate() As DateTime
        Get
            Return fWarrantyExpirationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("WarrantyExpirationDate", fWarrantyExpirationDate, value)
        End Set
    End Property
    Dim fAdquisitionDate As DateTime
    Public Property AdquisitionDate() As DateTime
        Get
            Return fAdquisitionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AdquisitionDate", fAdquisitionDate, value)
        End Set
    End Property
    Dim fDepreciate As Boolean
    Public Property Depreciate() As Boolean
        Get
            Return fDepreciate
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Depreciate", fDepreciate, value)
        End Set
    End Property
    Dim fStatusAssetId As FixedAssetStatusAssetXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetStatusAsset")> _
    Public Property StatusAssetId() As FixedAssetStatusAssetXpo
        Get
            Return fStatusAssetId
        End Get
        Set(ByVal value As FixedAssetStatusAssetXpo)
            SetPropertyValue(Of FixedAssetStatusAssetXpo)("StatusAssetId", fStatusAssetId, value)
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

    Dim fHasOutput As Boolean
    Public Property HasOutput() As Boolean
        Get
            Return fHasOutput
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HasOutput", fHasOutput, value)
        End Set
    End Property

    Dim fOutputRefund As Boolean
    Public Property OutputRefund() As Boolean
        Get
            Return fOutputRefund
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("OutputRefund", fOutputRefund, value)
        End Set
    End Property

    <PersistentAlias("Iif(HasOutput = True, False, Status)")> _
    Public ReadOnly Property StatusFull() As Boolean
        Get
            Return CType(EvaluateAlias("StatusFull"), Boolean)
        End Get
    End Property

    <PersistentAlias("Iif(OutputRefund = 1, 'Devuelto', Iif(HasOutput = 1, 'Inactivo', 'Activo'))")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Concat(ItemId.CodeDescription, ' - ', Plate)")>
    Public ReadOnly Property FixedAssetItemCodeNameWithPlate As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("FixedAssetItemCodeNameWithPlate"))
        End Get
    End Property

    <Association("FixedAssetPhysicalAssetReferenceFixedAssetDetailBook", GetType(FixedAssetFixedAssetPhysicalAssetDetailBookXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetDetailBookXpo() As XPCollection(Of FixedAssetFixedAssetPhysicalAssetDetailBookXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetPhysicalAssetDetailBookXpo)("FixedAssetPhysicalAssetDetailBookXpo")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
