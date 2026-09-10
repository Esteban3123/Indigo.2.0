Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel


<Persistent("FixedAsset.FixedAssetPurchaseOrderEquipmentDetail")> _
Public Class FixedAssetPurchaseOrderEquipmentDetailXpo
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
    Dim fIdFixedAssetPurchaseOrderEquipment As FixedAssetPurchaseOrderEquipmentXpo
    <Association("FixedAssetPurchaseOrderEquipmentDetailReferences")> _
    Public Property IdFixedAssetPurchaseOrderEquipment() As FixedAssetPurchaseOrderEquipmentXpo
        Get
            Return fIdFixedAssetPurchaseOrderEquipment
        End Get
        Set(ByVal value As FixedAssetPurchaseOrderEquipmentXpo)
            SetPropertyValue(Of FixedAssetPurchaseOrderEquipmentXpo)("IdFixedAssetPurchaseOrderEquipment", fIdFixedAssetPurchaseOrderEquipment, value)
        End Set
    End Property
    Dim fIdEquipment As Integer
    Public Property IdEquipment() As Integer
        Get
            Return fIdEquipment
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdEquipment", fIdEquipment, value)
        End Set
    End Property
    Dim fLicensePlate As String
    <Size(50)> _
    Public Property LicensePlate() As String
        Get
            Return fLicensePlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LicensePlate", fLicensePlate, value)
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
    Dim fIdResponsible As FixedAssetResponsibleXpo
    <Association("FixedAssetResponsibleReferences")> _
    Public Property IdResponsible() As FixedAssetResponsibleXpo
        Get
            Return fIdResponsible
        End Get
        Set(ByVal value As FixedAssetResponsibleXpo)
            SetPropertyValue(Of FixedAssetResponsibleXpo)("IdResponsible", fIdResponsible, value)
        End Set
    End Property
    Dim fIdFunctionalUnit As PayrollFunctionalUnitXpo
    <Association("FixedAssetPurchaseOrderDetailReferencesFunctionalUnit")> _
    Public Property IdFunctionalUnit() As PayrollFunctionalUnitXpo
        Get
            Return fIdFunctionalUnit
        End Get
        Set(ByVal value As PayrollFunctionalUnitXpo)
            SetPropertyValue(Of PayrollFunctionalUnitXpo)("IdFunctionalUnit", fIdFunctionalUnit, value)
        End Set
    End Property
    Dim fIdLocation As MaintenanceLocationXpo
    <Association("FixedAssetPurchaseOrderDetailReferencesLocation")> _
    Public Property IdLocation() As MaintenanceLocationXpo
        Get
            Return fIdLocation
        End Get
        Set(ByVal value As MaintenanceLocationXpo)
            SetPropertyValue(Of MaintenanceLocationXpo)("IdLocation", fIdLocation, value)
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
    Dim fComponentDepreciate As Boolean
    Public Property ComponentDepreciate() As Boolean
        Get
            Return fComponentDepreciate
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ComponentDepreciate", fComponentDepreciate, value)
        End Set
    End Property

    <Association("FixedAssetPurchaseOrderPartsAccesoriesConsumiblesReferences", GetType(FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo() As XPCollection(Of FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo)("FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo")
        End Get
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class


