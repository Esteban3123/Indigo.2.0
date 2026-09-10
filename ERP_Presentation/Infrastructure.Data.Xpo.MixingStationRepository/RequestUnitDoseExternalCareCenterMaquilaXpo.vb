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

<Persistent("MixingStation.RequestUnitDoseExternalCareCenterMaquila")>
Partial Public Class RequestUnitDoseExternalCareCenterMaquilaXpo
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

    Dim fRequestUnitDoseExternalCareCenterId As RequestUnitDoseExternalCareCenterXpo
    <Association("MaquilaReferencesCMConfigurationRequestUnitDoseExternalCareCenter")>
    Public Property RequestUnitDoseExternalCareCenterId() As RequestUnitDoseExternalCareCenterXpo
        Get
            Return fRequestUnitDoseExternalCareCenterId
        End Get
        Set(ByVal value As RequestUnitDoseExternalCareCenterXpo)
            SetPropertyValue(Of RequestUnitDoseExternalCareCenterXpo)("RequestUnitDoseExternalCareCenterId", fRequestUnitDoseExternalCareCenterId, value)
        End Set
    End Property

    Dim fType As Integer
    Public Property Type() As Integer
        Get
            Return fType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Type", fType, value)
        End Set
    End Property

    Dim fATCId As MixingStationATCXpo
    <Association("MaquilaReferencesATC")>
    Public Property ATCId() As MixingStationATCXpo
        Get
            Return fATCId
        End Get
        Set(ByVal value As MixingStationATCXpo)
            SetPropertyValue(Of MixingStationATCXpo)("ATCId", fATCId, value)
        End Set
    End Property

    Dim fPackageId As MixinStationPackageXpo
    <Association("MaquilaReferencesPackage")>
    Public Property PackageId() As MixinStationPackageXpo
        Get
            Return fPackageId
        End Get
        Set(ByVal value As MixinStationPackageXpo)
            SetPropertyValue(Of MixinStationPackageXpo)("PackageId", fPackageId, value)
        End Set
    End Property

    Dim fUnitDoseTypeId As MixinStationUnitDoseTypeXpo
    <Association("MaquilaReferencesUnitDoseType")>
    Public Property UnitDoseTypeId() As MixinStationUnitDoseTypeXpo
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As MixinStationUnitDoseTypeXpo)
            SetPropertyValue(Of MixinStationUnitDoseTypeXpo)("UnitDoseTypeId", fUnitDoseTypeId, value)
        End Set
    End Property

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property

End Class