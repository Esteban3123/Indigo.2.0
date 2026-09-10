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
<Persistent("Contract.HealthAdministrator")> _
Public Class HealthAdministratorXpo
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

    Dim fCode As String
    <Size(20)> _
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Size(100)> _
    <Persistent("Name")> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fHealthEntityCode As String
    <Size(6)> _
    <Persistent("HealthEntityCode")> _
    Public Property HealthEntityCode() As String
        Get
            Return fHealthEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthEntityCode", fHealthEntityCode, value)
        End Set
    End Property

    Dim fStatus As Boolean
    <Persistent("Status")> _
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    'columna que devuelve el nit y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("HealthAdministratorReferencesThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property


    Dim fCompanyType As CompanyTypeXpo
    <Association("CompanyTypeXpo_RateReferences_HealthAdministratorXpo")>
    <Persistent("EntityType")>
    Public Property CompanyType() As CompanyTypeXpo
        Get
            Return fCompanyType
        End Get
        Set(ByVal value As CompanyTypeXpo)
            SetPropertyValue(Of CompanyTypeXpo)("CompanyType", fCompanyType, value)
        End Set
    End Property

    <PersistentAlias("CompanyType.Id")>
    Public ReadOnly Property EntityType() As Byte
        Get
            Return Convert.ToByte(Me.EvaluateAlias("EntityType"))
        End Get
    End Property

    <Association("ContractReferencesHealthAdministrator", GetType(ContractXpo))> _
    Public ReadOnly Property ContractXpo() As XPCollection(Of ContractXpo)
        Get
            Return GetCollection(Of ContractXpo)("ContractXpo")
        End Get
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
