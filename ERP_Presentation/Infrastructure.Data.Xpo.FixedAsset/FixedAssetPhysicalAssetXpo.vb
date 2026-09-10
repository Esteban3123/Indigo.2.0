Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPhysicalAsset")> _
Public Class FixedAssetPhysicalAssetXpo
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

    Dim fItemId As FixedAssetEquipmentXpo
    <Association("PhysicalReferenceItem")>
    Public Property ItemId() As FixedAssetEquipmentXpo
        Get
            Return fItemId
        End Get
        Set(ByVal value As FixedAssetEquipmentXpo)
            SetPropertyValue(Of FixedAssetEquipmentXpo)("ItemId", fItemId, value)
        End Set
    End Property

    <PersistentAlias("Concat(Plate, ' - ', ItemId.Description)")>
    Public ReadOnly Property PlateName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("PlateName"))
        End Get
    End Property

    Dim fMainAccountId As PUCServiceXpo
    <Association("PhysicalReferenceMainAccount")> _
    Public Property MainAccountId() As PUCServiceXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As PUCServiceXpo)
            SetPropertyValue(Of PUCServiceXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fSerie As String
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
        End Set
    End Property

    Dim fPlate As String
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
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

    Dim fHistoricalValue As Decimal
    Public Property HistoricalValue() As Decimal
        Get
            Return fHistoricalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HistoricalValue", fHistoricalValue, value)
        End Set
    End Property

    Dim fFinancialDiscount As Decimal
    Public Property FinancialDiscount() As Decimal
        Get
            Return fFinancialDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FinancialDiscount", fFinancialDiscount, value)
        End Set
    End Property

    Dim fLocationId As FixedAssetFixedAssetLocationXpo
    <Association("PhysicalReferenceLocation")> _
    Public Property LocationId() As FixedAssetFixedAssetLocationXpo
        Get
            Return fLocationId
        End Get
        Set(ByVal value As FixedAssetFixedAssetLocationXpo)
            SetPropertyValue(Of FixedAssetFixedAssetLocationXpo)("LocationId", fLocationId, value)
        End Set
    End Property

    Dim fPolicyId As FixedAssetPolizaXpo
    <Association("PhysicalReferencePolicy")> _
    Public Property PolicyId() As FixedAssetPolizaXpo
        Get
            Return fPolicyId
        End Get
        Set(ByVal value As FixedAssetPolizaXpo)
            SetPropertyValue(Of FixedAssetPolizaXpo)("PolicyId", fPolicyId, value)
        End Set
    End Property

    Dim fResponsibleId As FixedAssetResponsibleXpo
    <Association("PhysicalReferenceResponsible")> _
    Public Property ResponsibleId() As FixedAssetResponsibleXpo
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleXpo)
            SetPropertyValue(Of FixedAssetResponsibleXpo)("ResponsibleId", fResponsibleId, value)
        End Set
    End Property

    Dim fSupplierId As Maintenance_Supplier
    <Association("PhysicalReferenceSupplier")> _
    Public Property SupplierId() As Maintenance_Supplier
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("SupplierId", fSupplierId, value)
        End Set
    End Property

    Dim fTrademarkId As FixedAssetTrademarkXpo
    <Association("PhysicalReferenceTrademark")> _
    Public Property TrademarkId() As FixedAssetTrademarkXpo
        Get
            Return fTrademarkId
        End Get
        Set(ByVal value As FixedAssetTrademarkXpo)
            SetPropertyValue(Of FixedAssetTrademarkXpo)("TrademarkId", fTrademarkId, value)
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

    Dim fSelectOption As Boolean
    <NonPersistent()>
    Public Property SelectOption() As Boolean
        Get
            Return fSelectOption
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SelectOption", fSelectOption, value)
        End Set
    End Property

    Dim fNumberContractLeasing As String
    Public Property NumberContractLeasing() As String
        Get
            Return fNumberContractLeasing
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumberContractLeasing", fNumberContractLeasing, value)
        End Set
    End Property

    Dim fInitialDateLeasing As Date
    Public Property InitialDateLeasing() As Date
        Get
            Return fInitialDateLeasing
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("InitialDateLeasing", fInitialDateLeasing, value)
        End Set
    End Property

    Dim fEndDateLeasing As Date
    Public Property EndDateLeasing() As Date
        Get
            Return fEndDateLeasing
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("EndDateLeasing", fEndDateLeasing, value)
        End Set
    End Property

    Dim fAdquisitionType As Byte
    Public Property AdquisitionType() As Byte
        Get
            Return fAdquisitionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AdquisitionType", fAdquisitionType, value)
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

    <Size(200)>
    <PersistentAlias("concat(concat(Plate,' - '),ItemId.CodeDescription)")>
    Public ReadOnly Property AssetDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("AssetDescription"))
        End Get
    End Property

    Dim fHasHighTech As Boolean
    Public Property HasHighTech() As Boolean
        Get
            Return fHasHighTech
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HasHighTech", fHasHighTech, value)
        End Set
    End Property


    <Association("FixedAssetPhysicalAssetPartsReferences", GetType(FixedAssetPhysicalAssetPartsXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetPartsXpo() As XPCollection(Of FixedAssetPhysicalAssetPartsXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetPartsXpo)("FixedAssetPhysicalAssetPartsXpo")
        End Get
    End Property

    <Association("FixedAssetPhysicalAssetReferenceFixedAssetPhysicalAssetDetailBook", GetType(FixedAssetPhysicalAssetDetailBookXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetDetailBookXpo() As XPCollection(Of FixedAssetPhysicalAssetDetailBookXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetDetailBookXpo)("FixedAssetPhysicalAssetDetailBookXpo")
        End Get
    End Property

    <Association("DepreciationDetailReferencePhysical", GetType(FixedAssetDepreciationDetailXpo))>
    Public ReadOnly Property FixedAssetDepreciationDetailXpo() As XPCollection(Of FixedAssetDepreciationDetailXpo)
        Get
            Return GetCollection(Of FixedAssetDepreciationDetailXpo)("FixedAssetDepreciationDetailXpo")
        End Get
    End Property

    <Association("FixedAssetPhysicalAssetReferenceDeteriorationIndicationByPhysicalAsset", GetType(DeteriorationIndicationByPhysicalAssetXpo))>
    Public ReadOnly Property DeteriorationIndicationByPhysicalAssetXpo() As XPCollection(Of DeteriorationIndicationByPhysicalAssetXpo)
        Get
            Return GetCollection(Of DeteriorationIndicationByPhysicalAssetXpo)("DeteriorationIndicationByPhysicalAssetXpo")
        End Get
    End Property

    <Association("FixedAssetPhysicalAsset_Reference_FixedAssetPhysicalAssetAccessory", GetType(FixedAssetPhysicalAssetAccessoryXpo))>
    Public ReadOnly Property FixedAssetPhysicalAssetAccessoryXpo() As XPCollection(Of FixedAssetPhysicalAssetAccessoryXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetAccessoryXpo)("FixedAssetPhysicalAssetAccessoryXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

