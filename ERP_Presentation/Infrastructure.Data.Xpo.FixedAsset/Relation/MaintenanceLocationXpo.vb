Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.Location")> _
Public Class MaintenanceLocationXpo
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
    Dim fIdParent As MaintenanceLocationXpo
    <Association("Maintenance_LocationReferencesMaintenance_Location")> _
    Public Property IdParent() As MaintenanceLocationXpo
        Get
            Return fIdParent
        End Get
        Set(ByVal value As MaintenanceLocationXpo)
            SetPropertyValue(Of MaintenanceLocationXpo)("IdParent", fIdParent, value)
        End Set
    End Property
    Dim fCityId As Integer
    Public Property CityId() As Integer
        Get
            Return fCityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CityId", fCityId, value)
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
    Dim fName As String
    <Size(50)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fIdLocationType As Short
    Public Property IdLocationType() As Short
        Get
            Return fIdLocationType
        End Get
        Set(ByVal value As Short)
            SetPropertyValue(Of Short)("IdLocationType", fIdLocationType, value)
        End Set
    End Property
    Dim fRiskLevel As Char
    Public Property RiskLevel() As Char
        Get
            Return fRiskLevel
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("RiskLevel", fRiskLevel, value)
        End Set
    End Property
    <Association("Maintenance_LocationReferencesMaintenance_Location", GetType(MaintenanceLocationXpo))> _
    Public ReadOnly Property Maintenance_LocationCollection() As XPCollection(Of MaintenanceLocationXpo)
        Get
            Return GetCollection(Of MaintenanceLocationXpo)("Maintenance_LocationCollection")
        End Get
    End Property

    <Association("FixedAssetPurchaseOrderDetailReferencesLocation", GetType(FixedAssetPurchaseOrderEquipmentDetailXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderEquipmentDetailXpo() As XPCollection(Of FixedAssetPurchaseOrderEquipmentDetailXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderEquipmentDetailXpo)("FixedAssetPurchaseOrderEquipmentDetailXpo")
        End Get
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
