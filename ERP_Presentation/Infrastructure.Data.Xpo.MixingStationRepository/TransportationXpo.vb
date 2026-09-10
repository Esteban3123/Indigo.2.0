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

<Persistent("MixingStation.Transportation")>
Partial Public Class TransportationXpo
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

    Dim fTransportationType As Integer
    Public Property TransportationType() As Integer
        Get
            Return fTransportationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TransportationType", fTransportationType, value)
        End Set
    End Property

    Dim fTransportationCode As String
    Public Property TransportationCode() As String
        Get
            Return fTransportationCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TransportationCode", fTransportationCode, value)
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

    <PersistentAlias("Iif(TransportationType = 1, 'Interno', 'Contratado')")>
    Public ReadOnly Property TransportationTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TransportationTypeName"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

End Class