'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Oscar Stiven Astudillo
' Created          : 2024-02-06
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Contract.CareGroupMixLiquidation")>
Public Class CareGroupMixLiquidationXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCareGroupId As Integer
    <Persistent("CareGroupId")>
    Public Property CareGroupId() As Integer
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CareGroupId", fCareGroupId, value)
        End Set
    End Property

    Dim fUnitDoseTypeId As Integer
    <Persistent("UnitDoseTypeId")>
    Public Property UnitDoseTypeId() As Integer
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseTypeId", fUnitDoseTypeId, value)
        End Set
    End Property

    Dim fTypePayment As Integer
    <Persistent("TypePayment")>
    Public Property TypePayment() As Integer
        Get
            Return fTypePayment
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TypePayment", fTypePayment, value)
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
