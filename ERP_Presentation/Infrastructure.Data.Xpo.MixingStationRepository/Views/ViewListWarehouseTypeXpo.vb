'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Duván Mejia Cortes 
' Created          : 06/07/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListWarehouseTypes")>
Partial Public Class ViewListWarehouseTypeXpo
    Inherits XPLiteObject

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

    Dim fIdMixingStation As Integer
    Public Property IdMixingStation() As Integer
        Get
            Return fIdMixingStation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdMixingStation", fIdMixingStation, value)
        End Set
    End Property

    Dim fWarehouseType As Integer
    Public Property WarehouseType() As Integer
        Get
            Return fWarehouseType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("WarehouseType", fWarehouseType, value)
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

    Dim fCodeName As String
    Public Property CodeName() As String
        Get
            Return fCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeName", fCodeName, value)
        End Set
    End Property

    Dim fId_Warehouse As Integer
    Public Property Id_Warehouse() As Integer
        Get
            Return fId_Warehouse
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id_Warehouse", fId_Warehouse, value)
        End Set
    End Property

    Dim fWarehouseTypeName As String
    Public Property WarehouseTypeName() As String
        Get
            Return fWarehouseTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("WarehouseTypeName", fWarehouseTypeName, value)
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