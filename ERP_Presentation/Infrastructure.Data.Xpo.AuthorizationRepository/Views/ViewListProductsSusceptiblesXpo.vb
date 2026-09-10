'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/09/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Authorization.ViewListProductsSusceptibles")>
Public Class ViewListProductsSusceptiblesXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fRow As String
    <Key(True)>
    Public Property Row() As String
        Get
            Return fRow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Row", fRow, value)
        End Set
    End Property

    Dim fInventoryProductId As Integer
    Public Property InventoryProductId() As Integer
        Get
            Return fInventoryProductId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InventoryProductId", fInventoryProductId, value)
        End Set
    End Property

    Dim fInventoryProductCode As String
    Public Property InventoryProductCode() As String
        Get
            Return fInventoryProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InventoryProductCode", fInventoryProductCode, value)
        End Set
    End Property

    Dim fInventoryProductName As String
    Public Property InventoryProductName() As String
        Get
            Return fInventoryProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InventoryProductName", fInventoryProductName, value)
        End Set
    End Property

    Dim fInventoryProductCodeName As String
    Public Property InventoryProductCodeName() As String
        Get
            Return fInventoryProductCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InventoryProductCodeName", fInventoryProductCodeName, value)
        End Set
    End Property

    Dim fCareCenterCode As String
    Public Property CareCenterCode() As String
        Get
            Return fCareCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterCode", fCareCenterCode, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
