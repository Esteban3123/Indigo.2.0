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

<Persistent("Inventory.AdministrationRoute")>
Partial Public Class MixingStationAdministrationRouteXpo
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

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <PersistentAlias("Id")>
    Public ReadOnly Property AdministrationRouteIdTmp() As Integer
        Get
            Return CInt(Convert.ToString(Me.EvaluateAlias("AdministrationRouteIdTmp")))
        End Get
    End Property

    <Association("ExternalPatientPreparation_Reference_AdministrationRoute", GetType(ExternalPatientPreparationXpo))>
    Public ReadOnly Property ExternalPatientPreparationsXpo() As XPCollection(Of ExternalPatientPreparationXpo)
        Get
            Return GetCollection(Of ExternalPatientPreparationXpo)("ExternalPatientPreparationsXpo")
        End Get
    End Property

    <Association("ATCAdministrationRouteReferencesAdministrationRoute", GetType(MixingStationATCAdministrationRouteXpo))>
    Public ReadOnly Property MixingStationATCAdministrationRouteXpo() As XPCollection(Of MixingStationATCAdministrationRouteXpo)
        Get
            Return GetCollection(Of MixingStationATCAdministrationRouteXpo)("MixingStationATCAdministrationRouteXpo")
        End Get
    End Property

    <Association("RequestUnitDoseInventoryDetailReferencesAdministrationRoute", GetType(RequestUnitDoseInventoryDetailXpo))>
    Public ReadOnly Property RequestUnitDoseInventoryDetailXpo() As XPCollection(Of RequestUnitDoseInventoryDetailXpo)
        Get
            Return GetCollection(Of RequestUnitDoseInventoryDetailXpo)("RequestUnitDoseInventoryDetailXpo")
        End Get
    End Property

End Class