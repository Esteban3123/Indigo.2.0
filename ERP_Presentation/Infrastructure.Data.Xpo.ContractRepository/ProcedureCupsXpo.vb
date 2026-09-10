'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Contract.ProcedureCups")> _
Public Class ProcedureCupsXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fProceduresTemplateId As Integer
    <Persistent("ProceduresTemplateId")> _
    Public Property ProceduresTemplateId() As Integer
        Get
            Return fProceduresTemplateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProceduresTemplateId", fProceduresTemplateId, value)
        End Set
    End Property

    Dim fCupsId As CupsEntityXpo
    <Association("ProcedureCupsReferencesCupsEntity")> _
    Public Property CupsId() As CupsEntityXpo
        Get
            Return fCupsId
        End Get
        Set(ByVal value As CupsEntityXpo)
            SetPropertyValue(Of CupsEntityXpo)("CupsId", fCupsId, value)
        End Set
    End Property

    Dim fContracted As Boolean
    Public Property Contracted() As Boolean
        Get
            Return fContracted
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Contracted", fContracted, value)
        End Set
    End Property

    Dim fQuoted As Boolean
    Public Property Quoted() As Boolean
        Get
            Return fQuoted
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Quoted", fQuoted, value)
        End Set
    End Property

    Dim fCUPSEntityContractDescriptionId As Integer
    Public Property CUPSEntityContractDescriptionId() As Integer
        Get
            Return fCUPSEntityContractDescriptionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CUPSEntityContractDescriptionId", fCUPSEntityContractDescriptionId, value)
        End Set
    End Property

    Dim fContractDescriptionId As ContractDescriptionsXpo
    <Association("ProcedureCupsReferencesContractDescription")>
    Public Property ContractDescriptionId() As ContractDescriptionsXpo
        Get
            Return fContractDescriptionId
        End Get
        Set(ByVal value As ContractDescriptionsXpo)
            SetPropertyValue(Of ContractDescriptionsXpo)("ContractDescriptionId", fContractDescriptionId, value)
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
