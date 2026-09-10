'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/09/2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Billing.ViewListCUPSEntityAndInventoryProduct")>
Public Class ViewListCUPSEntityAndInventoryProductXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fRow As Integer
    <Key(True)>
    Public Property Row() As Integer
        Get
            Return fRow
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Row", fRow, value)
        End Set
    End Property

    Dim fId As Integer
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

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fIdentification As Integer
    Public Property Identification() As Integer
        Get
            Return fIdentification
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Identification", fIdentification, value)
        End Set
    End Property

    Dim fIdentificationDescription As String
    Public Property IdentificationDescription() As String
        Get
            Return fIdentificationDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdentificationDescription", fIdentificationDescription, value)
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
