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
''' Tipos de unidades funcionales agregado al grupo de atención
''' </summary>
<Persistent("Contract.ControlByTypeFunctionalUnit")>
Public Class ControlByTypeFunctionalUnitXpo
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

    Dim fUnitType As Integer
    <Persistent("UnitType")>
    Public Property UnitType() As Integer
        Get
            Return fUnitType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitType", fUnitType, value)
        End Set
    End Property

    Dim fTypeFunctionalUnit As String
    <Persistent("TypeFunctionalUnit")>
    Public Property TypeFunctionalUnit() As String
        Get
            Return fTypeFunctionalUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TypeFunctionalUnit", fTypeFunctionalUnit, value)
        End Set
    End Property

    Dim fMandatoryAuthorization As Boolean
    <Persistent("MandatoryAuthorization")>
    Public Property MandatoryAuthorization() As Boolean
        Get
            Return fMandatoryAuthorization
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("MandatoryAuthorization", fMandatoryAuthorization, value)
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
