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
<Persistent("Contract.CupsSubgroup")> _
Public Class CupsSubGroupXpo
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

    Dim fDescription As String
    <Size(300)> _
    <Persistent("Description")> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    'columna que devuelve el nit y el nombre concatenado
    <Size(50)>
    <PersistentAlias("concat(Code,' - ',Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fCupsGroupId As CupsGroupXpo
    <Association("CupSubGroupReferencesCupGroup")> _
    Public Property CupsGroupId() As CupsGroupXpo
        Get
            Return fCupsGroupId
        End Get
        Set(ByVal value As CupsGroupXpo)
            SetPropertyValue(Of CupsGroupXpo)("CupsGroupId", fCupsGroupId, value)
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

    Dim fSelectOption As Boolean
    <NonPersistent()> _
    Public Property SelectOption As Boolean
        Get
            Return fSelectOption
        End Get
        Set(value As Boolean)
            fSelectOption = value
        End Set
    End Property

    <Association("CupsEntityReferencesCupSubGroup", GetType(CupsEntityXpo))> _
    Public ReadOnly Property CupsEntityXpo() As XPCollection(Of CupsEntityXpo)
        Get
            Return GetCollection(Of CupsEntityXpo)("CupsEntityXpo")
        End Get
    End Property

    <Association("DefinitionRateDetailReferencesSubGroup", GetType(DefinitionRateDetailXpo))>
    Public ReadOnly Property DefinitionRateDetailXpo() As XPCollection(Of DefinitionRateDetailXpo)
        Get
            Return GetCollection(Of DefinitionRateDetailXpo)("DefinitionRateDetailXpo")
        End Get
    End Property

    <Association("CupsEntityDescriptionReferencesSubgroup", GetType(CUPSEntityContractDescriptionsXpo))>
    Public ReadOnly Property CUPSEntityContractDescriptionsXpo() As XPCollection(Of CUPSEntityContractDescriptionsXpo)
        Get
            Return GetCollection(Of CUPSEntityContractDescriptionsXpo)("CUPSEntityContractDescriptionsXpo")
        End Get
    End Property

    Dim fIdRisGrImage As Integer
    <Persistent("IdRisGrImage")> _
    Public Property IdRisGrImage() As Integer
        Get
            Return fIdRisGrImage
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRisGrImage", fIdRisGrImage, value)
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
