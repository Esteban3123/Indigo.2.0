'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andres Alarcon
' Created          : 02-12-2024
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("Inventory.StorageTemperature")>
Partial Public Class MixingStationStorageTemperatureXpo
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

    <PersistentAlias("Concat(Code, ' - ', Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fFrom As Integer
    Public Property From() As Integer
        Get
            Return fFrom
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("From", fFrom, value)
        End Set
    End Property

    Dim fUntil As Integer
    Public Property Until() As Integer
        Get
            Return fUntil
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Until", fUntil, value)
        End Set
    End Property

    Dim fTemperatureUnit As Byte
    Public Property TemperatureUnit() As Byte
        Get
            Return fTemperatureUnit
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TemperatureUnit", fTemperatureUnit, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    <Association("StabilityTableDetailDilutionreferencesStorageTemperature", GetType(StabilityTableDetailDilutionXpo))>
    Public ReadOnly Property StabilityTableDetailDilutionXpo() As XPCollection(Of StabilityTableDetailDilutionXpo)
        Get
            Return GetCollection(Of StabilityTableDetailDilutionXpo)("StabilityTableDetailDilutionXpo")
        End Get
    End Property

    <Association("StabilityTableDetailReconstitutionreferencesStorageTemperature", GetType(StabilityTableDetailReconstitutionXpo))>
    Public ReadOnly Property StabilityTableDetailReconstitutionXpo() As XPCollection(Of StabilityTableDetailReconstitutionXpo)
        Get
            Return GetCollection(Of StabilityTableDetailReconstitutionXpo)("StabilityTableDetailReconstitutionXpo")
        End Get
    End Property
End Class