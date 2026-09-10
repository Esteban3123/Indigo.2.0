'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ProductionLineUnitDoseType")>
Partial Public Class ProductionLineUnitDoseTypeXpo
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

    Dim fId_ProductionLine As MixingStationProductionLineXpo
    <Association("ProductionLineUnitDoseTypeReferencesProductionLine")>
    Public Property Id_ProductionLine() As MixingStationProductionLineXpo
        Get
            Return fId_ProductionLine
        End Get
        Set(ByVal value As MixingStationProductionLineXpo)
            SetPropertyValue(Of MixingStationProductionLineXpo)("Id_ProductionLine", fId_ProductionLine, value)
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
