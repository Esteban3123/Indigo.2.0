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

<Persistent("MixingStation.TransportationAssistant")>
Partial Public Class TransportationAssistantXpo
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

    Dim fAssistantType As Integer
    Public Property AssistantType() As Integer
        Get
            Return fAssistantType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AssistantType", fAssistantType, value)
        End Set
    End Property

    Dim fIdentificationNumber As String
    Public Property IdentificationNumber() As String
        Get
            Return fIdentificationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdentificationNumber", fIdentificationNumber, value)
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

    Dim fLastName As String
    Public Property LastName() As String
        Get
            Return fLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LastName", fLastName, value)
        End Set
    End Property

    <PersistentAlias("CONCAT(Name, ' ', LastName)")>
    Public ReadOnly Property FullName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("FullName"))
        End Get
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

    <PersistentAlias("Iif(AssistantType = 1, 'Interno', 'Contratado')")>
    Public ReadOnly Property AssistantTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("AssistantTypeName"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

End Class