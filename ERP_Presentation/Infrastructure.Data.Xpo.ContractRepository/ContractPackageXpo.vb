'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Diego A. Roldán
' Created          : 2021-08-24
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Contract.ContractPackage")>
Public Class ContractPackageXpo
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

    Dim fCode As String
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fCUPSEntityId As CupsEntityXpo
    <Association("ContractPackagesReferencesCUPS")>
    <Persistent("CUPSEntityId")>
    Public Property CUPSEntityId() As CupsEntityXpo
        Get
            Return fCUPSEntityId
        End Get
        Set(ByVal value As CupsEntityXpo)
            SetPropertyValue("CUPSEntityId", fCUPSEntityId, value)
        End Set
    End Property

    Dim fIPSServiceId As ContractIPSServiceXPO
    <Association("ContractPackagesReferencesIPSService")>
    <Persistent("IPSServiceId")>
    Public Property IPSServiceId() As ContractIPSServiceXPO
        Get
            Return fIPSServiceId
        End Get
        Set(ByVal value As ContractIPSServiceXPO)
            SetPropertyValue(Of ContractIPSServiceXPO)("IPSServiceId", fIPSServiceId, value)
        End Set
    End Property

    Dim fDescription As String
    <Persistent("Description")>
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fStatus As Boolean
    <Persistent("Status")>
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("concat(Code, ' - ', Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <PersistentAlias("Iif(Status, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property


    <Association("ContractPackageReferencesCareGroupPackage", GetType(CareGroupPackage))>
    Public ReadOnly Property CareGroupPackages() As XPCollection(Of CareGroupPackage)
        Get
            Return GetCollection(Of CareGroupPackage)("CareGroupPackages")
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
