'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 2024-12-11
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewStabilityTableDetail")>
Partial Public Class ViewStabilityTableDetailXpo
    Inherits XPLiteObject

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fStabilityTableId As Integer
    Public Property StabilityTableId() As Integer
        Get
            Return fStabilityTableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("StabilityTableId", fStabilityTableId, value)
        End Set
    End Property

    Dim fAtcIdMainMedicine As Integer
    Public Property AtcIdMainMedicine() As Integer
        Get
            Return fAtcIdMainMedicine
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AtcIdMainMedicine", fAtcIdMainMedicine, value)
        End Set
    End Property

    Dim fProductIdMainMedicine As Integer
    Public Property ProductIdMainMedicine() As Integer
        Get
            Return fProductIdMainMedicine
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductIdMainMedicine", fProductIdMainMedicine, value)
        End Set
    End Property

    Dim fUnitDoseTypeId As Integer
    Public Property UnitDoseTypeId() As Integer
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseTypeId", fUnitDoseTypeId, value)
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

    Dim fStabilityMainMedicine As Integer
    Public Property StabilityMainMedicine() As Integer
        Get
            Return fStabilityMainMedicine
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("StabilityMainMedicine", fStabilityMainMedicine, value)
        End Set
    End Property

    Dim fAtcIdReconstituent As Integer?
    Public Property AtcIdReconstituent() As Integer?
        Get
            Return fAtcIdReconstituent
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("AtcIdReconstituent", fAtcIdReconstituent, value)
        End Set
    End Property

    Dim fStabilityReconstituent As Integer?
    Public Property StabilityReconstituent() As Integer?
        Get
            Return fStabilityReconstituent
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("StabilityReconstituent", fStabilityReconstituent, value)
        End Set
    End Property

    Dim fAtcIdVehicle As Integer?
    Public Property AtcIdVehicle() As Integer?
        Get
            Return fAtcIdVehicle
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("AtcIdVehicle", fAtcIdVehicle, value)
        End Set
    End Property

    Dim fStabilityVehicle As Integer?
    Public Property StabilityVehicle() As Integer?
        Get
            Return fStabilityVehicle
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("StabilityVehicle", fStabilityVehicle, value)
        End Set
    End Property

#Region "Builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
#End Region

End Class