'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 10/02/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.RequestUnitDoseInventory")>
Partial Public Class RequestUnitDoseInventoryXpo
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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fCMConfigurationId As MixinStationCMConfigXpo
    <Association("RequestUnitDoseInventoryReferencesCMConfiguration")>
    Public Property CMConfigurationId() As MixinStationCMConfigXpo
        Get
            Return fCMConfigurationId
        End Get
        Set(ByVal value As MixinStationCMConfigXpo)
            SetPropertyValue(Of MixinStationCMConfigXpo)("CMConfigurationId", fCMConfigurationId, value)
        End Set
    End Property

    Dim fWarehouseId As MixingStationWarehouseXpo
    <Association("RequestUnitDoseInventoryReferencesWarehouse")>
    Public Property WarehouseId() As MixingStationWarehouseXpo
        Get
            Return fWarehouseId
        End Get
        Set(ByVal value As MixingStationWarehouseXpo)
            SetPropertyValue(Of MixingStationWarehouseXpo)("WarehouseId", fWarehouseId, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fStatus As Integer
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 1, 'Registrado', IIF(Status = 2, 'Confirmado', 'Anulado'))")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <Association("RequestUnitDoseInventoryDetailReferencesCMConfigurationRequestUnitDoseInventory", GetType(RequestUnitDoseInventoryDetailXpo))>
    Public ReadOnly Property RequestUnitDoseInventoryDetailXpo() As XPCollection(Of RequestUnitDoseInventoryDetailXpo)
        Get
            Return GetCollection(Of RequestUnitDoseInventoryDetailXpo)("RequestUnitDoseInventoryDetailXpo")
        End Get
    End Property

End Class