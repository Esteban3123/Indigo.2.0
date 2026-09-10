'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 2017-07-24
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports System.ComponentModel.DataAnnotations.Schema
Imports DevExpress.Xpo

#End Region

''' <summary>
''' asociacion entre procedureCups y MarketingUnitCups usado en los servicios Xpo
''' </summary>
<Persistent("Billing.ViewListRecognitionEntrance")>
Public Class ViewListRecognitionEntrance
    Inherits XPLiteObject

#Region "Members"

    Dim fCareGroupId As Integer
    <Key(True)>
    Public Property CareGroupId() As Integer
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CareGroupId", fCareGroupId, value)
        End Set
    End Property


    <NotMapped()>
    Public Property StateOperation As Byte = 0

    <NotMapped()>
    Public Property MessageInfo As String


    Dim fCareGroupCodeName As String
    Public Property CareGroupCodeName() As String
        Get
            Return fCareGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupCodeName", fCareGroupCodeName, value)
        End Set
    End Property

    Dim fTotalCareGroup As Decimal
    Public Property TotalCareGroup() As Decimal
        Get
            Return fTotalCareGroup
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalCareGroup", fTotalCareGroup, value)
        End Set
    End Property

    Dim fFolioQuantity As Integer
    Public Property FolioQuantity() As Integer
        Get
            Return fFolioQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FolioQuantity", fFolioQuantity, value)
        End Set
    End Property

    'Dim fFecha As DateTime
    'Public Property Fecha() As DateTime
    '    Get
    '        Return fFecha
    '    End Get
    '    Set(ByVal value As DateTime)
    '        SetPropertyValue(Of DateTime)("Fecha", fFecha, value)
    '    End Set
    'End Property

    Dim fOperativeUnitId As Integer
    Public Property OperativeUnitId() As Integer
        Get
            Return fOperativeUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperativeUnitId", fOperativeUnitId, value)
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

