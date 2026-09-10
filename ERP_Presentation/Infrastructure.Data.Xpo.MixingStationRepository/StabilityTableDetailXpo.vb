'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 16-04-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.StabilityTableDetail")>
Partial Public Class StabilityTableDetailXpo
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

    Dim fStabilityTableId As StabilityTableXpo
    <Association("StabilityTableDetailferencesStabilityTable")>
    Public Property StabilityTableId() As StabilityTableXpo
        Get
            Return fStabilityTableId
        End Get
        Set(ByVal value As StabilityTableXpo)
            SetPropertyValue(Of StabilityTableXpo)("StabilityTableId", fStabilityTableId, value)
        End Set
    End Property

    Dim fATCId As MixingStationATCXpo
    <Association("StabilityTableDetailferencesATC")>
    Public Property ATCId() As MixingStationATCXpo
        Get
            Return fATCId
        End Get
        Set(ByVal value As MixingStationATCXpo)
            SetPropertyValue(Of MixingStationATCXpo)("ATCId", fATCId, value)
        End Set
    End Property

    Dim fProductId As MixingStationProductXpo
    <Association("StabilityTableDetailferencesProduct")>
    Public Property ProductId() As MixingStationProductXpo
        Get
            Return fProductId
        End Get
        Set(ByVal value As MixingStationProductXpo)
            SetPropertyValue(Of MixingStationProductXpo)("ProductId", fProductId, value)
        End Set
    End Property

    Dim fHourStabilityProduct As Integer
    Public Property HourStabilityProduct() As Integer
        Get
            Return fHourStabilityProduct
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HourStabilityProduct", fHourStabilityProduct, value)
        End Set
    End Property

    Dim fUnitDoseTypeId As MixinStationUnitDoseTypeXpo
    <Association("StabilityTableDetailferencesUnitDosesType")>
    Public Property UnitDoseTypeId() As MixinStationUnitDoseTypeXpo
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As MixinStationUnitDoseTypeXpo)
            SetPropertyValue(Of MixinStationUnitDoseTypeXpo)("UnitDoseTypeId", fUnitDoseTypeId, value)
        End Set
    End Property

    Dim fAllowableDoses As Integer
    Public Property AllowableDoses() As Integer
        Get
            Return fAllowableDoses
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AllowableDoses", fAllowableDoses, value)
        End Set
    End Property

    Dim fObservations As String
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property

    <Association("StabilityTableDetailDilutionferencesStabilityTableDetail", GetType(StabilityTableDetailDilutionXpo))>
    Public ReadOnly Property StabilityTableDetailDilutionXpo() As XPCollection(Of StabilityTableDetailDilutionXpo)
        Get
            Return GetCollection(Of StabilityTableDetailDilutionXpo)("StabilityTableDetailDilutionXpo")
        End Get
    End Property

    <Association("StabilityTableDetailReconstitutionferencesStabilityTableDetail", GetType(StabilityTableDetailReconstitutionXpo))>
    Public ReadOnly Property StabilityTableDetailReconstitutionXpo() As XPCollection(Of StabilityTableDetailReconstitutionXpo)
        Get
            Return GetCollection(Of StabilityTableDetailReconstitutionXpo)("StabilityTableDetailReconstitutionXpo")
        End Get
    End Property

End Class