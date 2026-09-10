'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andres Alarcon
' Created          : 07-05-2025
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Inventory.ATCAdministrationRoute")>
Partial Public Class MixingStationATCAdministrationRouteXpo
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

    Dim fATCId As MixingStationATCXpo
    <Association("ATCAdministrationRouteReferencesATC")>
    Public Property ATCId() As MixingStationATCXpo
        Get
            Return fATCId
        End Get
        Set(ByVal value As MixingStationATCXpo)
            SetPropertyValue(Of MixingStationATCXpo)("AtcId", fATCId, value)
        End Set
    End Property

    Dim fAdministrationRouteId As MixingStationAdministrationRouteXpo
    <Association("ATCAdministrationRouteReferencesAdministrationRoute")>
    Public Property AdministrationRouteId() As MixingStationAdministrationRouteXpo
        Get
            Return fAdministrationRouteId
        End Get
        Set(ByVal value As MixingStationAdministrationRouteXpo)
            SetPropertyValue(Of MixingStationAdministrationRouteXpo)("AdministrationRouteId", fAdministrationRouteId, value)
        End Set
    End Property

    <PersistentAlias("concat(concat(AdministrationRouteId.Code,' - '),AdministrationRouteId.Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <PersistentAlias("AdministrationRouteId.Id")>
    Public ReadOnly Property AdministrationRouteIdTmp() As Integer
        Get
            Return Convert.ToInt32(Me.EvaluateAlias("AdministrationRouteIdTmp"))
        End Get
    End Property

End Class