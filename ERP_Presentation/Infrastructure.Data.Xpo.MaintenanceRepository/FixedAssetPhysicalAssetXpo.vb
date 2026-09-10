Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPhysicalAsset")> _
Public Class FixedAssetPhysicalAssetXpo
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

    Dim fHasHighTech As Boolean
    Public Property HasHighTech() As Boolean
        Get
            Return fHasHighTech
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HasHighTech", fHasHighTech, value)
        End Set
    End Property

#End Region

#Region "CustomMembers"
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

    Dim fLocationId As FixedAssetFixedAssetLocationXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetLocation")>
    Public Property LocationId() As FixedAssetFixedAssetLocationXpo
        Get
            Return fLocationId
        End Get
        Set(ByVal value As FixedAssetFixedAssetLocationXpo)
            SetPropertyValue(Of FixedAssetFixedAssetLocationXpo)("LocationId", fLocationId, value)
        End Set
    End Property

    <PersistentAlias("Concat(Plate, ' - ', ItemId.Description)")>
    Public ReadOnly Property PlateName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("PlateName"))
        End Get
    End Property

    Dim fTrademarkId As FixedAssetTrademarkXpo
    <Association("PhysicalReferenceTrademark")>
    Public Property TrademarkId() As FixedAssetTrademarkXpo
        Get
            Return fTrademarkId
        End Get
        Set(ByVal value As FixedAssetTrademarkXpo)
            SetPropertyValue(Of FixedAssetTrademarkXpo)("TrademarkId", fTrademarkId, value)
        End Set
    End Property

    <Size(200)>
    <PersistentAlias("concat(concat(Plate,' - '),ItemId.CodeDescription)")>
    Public ReadOnly Property AssetDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("AssetDescription"))
        End Get
    End Property

    <Association("Maintenance_EquipmentRegistrationReferencesFixedAsset_FixedAssetPhysicalAsset", GetType(Maintenance_EquipmentRegistration))>
    Public ReadOnly Property EquipmentRegistration() As XPCollection(Of Maintenance_EquipmentRegistration)
        Get
            Return GetCollection(Of Maintenance_EquipmentRegistration)("EquipmentRegistration")
        End Get
    End Property

    <Association("MaintenanceContractDetailReferences_FixedAssetPhysicalAsset", GetType(MaintenanceContractDetailXpo))>
    Public ReadOnly Property MaintenanceContractDetailPhysicalAssetIdXpo() As XPCollection(Of MaintenanceContractDetailXpo)
        Get
            Return GetCollection(Of MaintenanceContractDetailXpo)("MaintenanceContractDetailPhysicalAssetIdXpo")
        End Get
    End Property

    <Association("Maintenance_ViewWorkOrderReferencesFixedAsset_FixedAssetPhysicalAssetXpo", GetType(ViewWorkOrderReportXpo))>
    Public ReadOnly Property ViewWorkOrderReportXpo() As XPCollection(Of ViewWorkOrderReportXpo)
        Get
            Return GetCollection(Of ViewWorkOrderReportXpo)("ViewWorkOrderReportXpo")
        End Get
    End Property


#End Region

#Region "builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region


End Class

