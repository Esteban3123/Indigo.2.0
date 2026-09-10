'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : giovanny Plazas Lozano
' Created          : 25/02/2022
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewProcessSetting")>
Partial Public Class ViewProcessSettingXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

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

    Dim fProductCodeName As String
    Public Property ProductCodeName() As String
        Get
            Return fProductCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductCodeName", fProductCodeName, value)
        End Set
    End Property

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fProductId As Integer?
    Public Property ProductId() As Integer?
        Get
            Return fProductId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ProductId", fProductId, value)
        End Set
    End Property

    Dim fCodeId As Integer?
    Public Property CodeId() As Integer?
        Get
            Return fCodeId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CodeId", fCodeId, value)
        End Set
    End Property

    Dim fMovementDate As DateTime?
    Public Property MovementDate() As DateTime?
        Get
            Return fMovementDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("MovementDate", fMovementDate, value)
        End Set
    End Property

    Dim fMovementTypeName As String
    Public Property MovementTypeName() As String
        Get
            Return fMovementTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MovementTypeName", fMovementTypeName, value)
        End Set
    End Property

    Dim fUnitMeasurement As String
    Public Property UnitMeasurement() As String
        Get
            Return fUnitMeasurement
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitMeasurement", fUnitMeasurement, value)
        End Set
    End Property

    Dim fUnitMeasurementId As Integer
    Public Property UnitMeasurementId() As Integer
        Get
            Return fUnitMeasurementId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitMeasurementId", fUnitMeasurementId, value)
        End Set
    End Property

    Dim fQuantityUnitMeasurement As Decimal
    Public Property QuantityUnitMeasurement() As Decimal
        Get
            Return fQuantityUnitMeasurement
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("QuantityUnitMeasurement", fQuantityUnitMeasurement, value)
        End Set
    End Property

    Dim fQuantityUsed As Decimal
    Public Property QuantityUsed() As Decimal
        Get
            Return fQuantityUsed
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("QuantityUsed", fQuantityUsed, value)
        End Set
    End Property

    Dim fCurrentQuantity As Decimal?
    Public Property CurrentQuantity() As Decimal?
        Get
            Return fCurrentQuantity
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("CurrentQuantity", fCurrentQuantity, value)
        End Set
    End Property

    Dim fDeliveredQuantity As Decimal
    Public Property DeliveredQuantity() As Decimal
        Get
            Return fDeliveredQuantity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DeliveredQuantity", fDeliveredQuantity, value)
        End Set
    End Property

    Dim fQuantityUsedCampaign As Decimal
    Public Property QuantityUsedCampaign() As Decimal
        Get
            Return fQuantityUsedCampaign
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("QuantityUsedCampaign", fQuantityUsedCampaign, value)
        End Set
    End Property

    Dim fQuantityDevolution As Decimal
    Public Property QuantityDevolution() As Decimal
        Get
            Return fQuantityDevolution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("QuantityDevolution", fQuantityDevolution, value)
        End Set
    End Property

    Dim fCampaignBalance As Decimal?
    Public Property CampaignBalance() As Decimal?
        Get
            Return fCampaignBalance
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("CampaignBalance", fCampaignBalance, value)
        End Set
    End Property

    Dim fQuantityHarnessed As Decimal?
    Public Property QuantityHarnessed() As Decimal?
        Get
            Return fQuantityHarnessed
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("QuantityHarnessed", fQuantityHarnessed, value)
        End Set
    End Property

    Dim fQuantityDevolutionUnitMeasurement As Decimal?
    Public Property QuantityDevolutionUnitMeasurement() As Decimal?
        Get
            Return fQuantityDevolutionUnitMeasurement
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("QuantityDevolutionUnitMeasurement", fQuantityDevolutionUnitMeasurement, value)
        End Set
    End Property

    Dim fDirectMPQuantity As Decimal?
    Public Property DirectMPQuantity() As Decimal?
        Get
            Return fDirectMPQuantity
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("DirectMPQuantity", fDirectMPQuantity, value)
        End Set
    End Property

    Dim fIndirectMPQuantity As Decimal?
    Public Property IndirectMPQuantity() As Decimal?
        Get
            Return fIndirectMPQuantity
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("IndirectMPQuantity", fIndirectMPQuantity, value)
        End Set
    End Property

    Dim fQuantityRemaining As Decimal?
    Public Property QuantityRemaining() As Decimal?
        Get
            Return fQuantityRemaining
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("QuantityRemaining", fQuantityRemaining, value)
        End Set
    End Property

    Dim fRawMaterialQuantityBalance As Decimal?
    Public Property RawMaterialQuantityBalance() As Decimal?
        Get
            Return fRawMaterialQuantityBalance
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("RawMaterialQuantityBalance", fRawMaterialQuantityBalance, value)
        End Set
    End Property

    Dim fUserMovementDescription As String
    Public Property UserMovementDescription() As String
        Get
            Return fUserMovementDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserMovementDescription", fUserMovementDescription, value)
        End Set
    End Property

    Dim fCKQuantity As Decimal?
    Public Property CKQuantity() As Decimal?
        Get
            Return fCKQuantity
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("CKQuantity", fCKQuantity, value)
        End Set
    End Property

    Dim fBatchSerialId As Integer?
    Public Property BatchSerialId() As Integer?
        Get
            Return fBatchSerialId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("BatchSerialId", fBatchSerialId, value)
        End Set
    End Property

    Dim fBatchCode As String
    Public Property BatchCode() As String
        Get
            Return fBatchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BatchCode", fBatchCode, value)
        End Set
    End Property

    Dim fIsMain As Boolean
    Public Property IsMain() As Boolean
        Get
            Return fIsMain
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsMain", fIsMain, value)
        End Set
    End Property

    Dim fAllowsRemanent As Byte?
    Public Property AllowsRemanent() As Byte?
        Get
            Return fAllowsRemanent
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("AllowsRemanent", fAllowsRemanent, value)
        End Set
    End Property

    Dim fItemType As Integer?
    Public Property ItemType() As Integer?
        Get
            Return fItemType
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ItemType", fItemType, value)
        End Set
    End Property

    Dim fItemTypeName As String
    Public Property ItemTypeName() As String
        Get
            Return fItemTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemTypeName", fItemTypeName, value)
        End Set
    End Property


    Dim fIsSelected As Boolean

    <PersistentAlias("Is_Selected")>
    Public Property IsSelected() As Boolean
        Get
            Return fIsSelected
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsSelected", fIsSelected, value)
        End Set
    End Property
End Class