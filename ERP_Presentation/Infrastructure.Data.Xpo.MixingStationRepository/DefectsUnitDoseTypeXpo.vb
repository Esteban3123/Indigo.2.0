'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 01/09/2022
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo


<Persistent("MixingStation.DefectsUnitDoseType")>
Partial Public Class DefectsUnitDoseTypeXpo

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

    Dim _Id As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", _Id, value)
        End Set
    End Property

    Dim fId_Defects As DefectsXpo
    <Association("DefectsUnitDoseTypeReferencesDefects")>
    Public Property Id_Defects() As DefectsXpo
        Get
            Return fId_Defects
        End Get
        Set(ByVal value As DefectsXpo)
            SetPropertyValue(Of DefectsXpo)("Id_DefectsClassificationItem", fId_Defects, value)
        End Set
    End Property

    Dim fId_UnitDoseType As MixinStationUnitDoseTypeXpo
    <Association("ProductionLineUnitDoseTypeReferencesUnitDoseType")>
    Public Property Id_UnitDoseType() As MixinStationUnitDoseTypeXpo
        Get
            Return fId_UnitDoseType
        End Get
        Set(ByVal value As MixinStationUnitDoseTypeXpo)
            SetPropertyValue(Of MixinStationUnitDoseTypeXpo)("Id_UnitDoseType", fId_UnitDoseType, value)
        End Set
    End Property
End Class
