'************************************************************
' Assembly         : Infraestructure.Accounting.AddressServiceXpo
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 05-05-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
#End Region
<Persistent("GeneralLedger.MainAccounts")> _
Public Class PUCServiceXpo
    Inherits XPLiteObject

#Region "Members"
    Private _RetencionType As Integer
    Private _HandlesCostCenter As Boolean
    Private _HandlesThirdParty As Boolean
    Private _IdParent As String
    Private _Name As String
    Private _Number As String
    Private _Id As Integer
    Private _LegalBookId As Integer
    Private _FreelancerCategory As Boolean
#End Region

#Region "Fields"
    ''' <summary>
    ''' Gets or sets the identifier.
    ''' </summary>
    ''' <value>
    ''' The identifier.
    ''' </value>
    <Key(True)>
    <Persistent("Id")> _
    Public Property Id As Integer
        Get
            Return _Id
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("Id", _Id, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the account code.
    ''' </summary>
    ''' <value>
    ''' The account code.
    ''' </value>
    <Persistent("Number")> _
    Public Property Number As String
        Get
            Return _Number
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Number", _Number, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the name of the account.
    ''' </summary>
    ''' <value>
    ''' The name of the account.
    ''' </value>
    <Persistent("Name")> _
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Name", _Name, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets the name of the code.
    ''' </summary>
    ''' <value>
    ''' The name of the code.
    ''' </value>
    <Size(50)> _
    <PersistentAlias("concat(concat(Number,' - '),Name)")>
    Public ReadOnly Property NumberName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NumberName"))
        End Get
    End Property

    Dim fAllowsMovement As Boolean
    Public Property AllowsMovement() As Boolean
        Get
            Return fAllowsMovement
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllowsMovement", fAllowsMovement, value)
        End Set
    End Property

    Private _Status As Boolean
    ''' <summary>
    ''' Gets or sets a value indicating whether [status].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [status]; otherwise, <c>false</c>.
    ''' </value>
    Public Property Status As Boolean
        Get
            Return _Status
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("Status", _Status, value)
        End Set
    End Property

    Dim _HomologationAccountId As Integer
    <NonPersistent()> _
    Public Property HomologationAccountId As Integer
        Get
            Return _HomologationAccountId
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Boolean)("HomologationAccountId", _HomologationAccountId, value)
        End Set
    End Property

    Private _Nature As Integer
    Public Property Nature As Integer
        Get
            Return _Nature
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("Nature", _Nature, value)
        End Set
    End Property

    <Association("PhysicalReferenceMainAccount", GetType(FixedAssetPhysicalAssetXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetXpo() As XPCollection(Of FixedAssetPhysicalAssetXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetXpo)("FixedAssetPhysicalAssetXpo")
        End Get
    End Property

    <Association("ItemCatalogReferenceMainAccount", GetType(FixedAssetEquipmentCatalogXpo))>
    Public ReadOnly Property FixedAssetEquipmentCatalogXpo() As XPCollection(Of FixedAssetEquipmentCatalogXpo)
        Get
            Return GetCollection(Of FixedAssetEquipmentCatalogXpo)("FixedAssetEquipmentCatalogXpo")
        End Get
    End Property

    <Association("DepreciationDetailCostReferenceMainAccount", GetType(FixedAssetDepreciationDetailCostXpo))>
    Public ReadOnly Property FixedAssetDepreciationDetailCostXpo() As XPCollection(Of FixedAssetDepreciationDetailCostXpo)
        Get
            Return GetCollection(Of FixedAssetDepreciationDetailCostXpo)("FixedAssetDepreciationDetailCostXpo")
        End Get
    End Property

    <Association("FixedAssetAmortizationDetailCostReferenceMainAccount", GetType(FixedAssetAmortizationDetailCostXpo))>
    Public ReadOnly Property FixedAssetAmortizationDetailCostXpo() As XPCollection(Of FixedAssetAmortizationDetailCostXpo)
        Get
            Return GetCollection(Of FixedAssetAmortizationDetailCostXpo)("FixedAssetAmortizationDetailCostXpo")
        End Get
    End Property

#End Region

#Region "Constructors"
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
