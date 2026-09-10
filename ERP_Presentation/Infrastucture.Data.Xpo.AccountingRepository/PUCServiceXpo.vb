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

    Dim fLegalBookId As BookXpo
    <Association("PUCXpoReferencesBookXpo")> _
    Public Property LegalBookId() As BookXpo
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As BookXpo)
            SetPropertyValue(Of BookXpo)("LegalBookId", fLegalBookId, value)
        End Set
    End Property

    Dim fIdAccountClass As AccountClassXpo
    <Association("PUCXpoReferencesAccountClassXpo")> _
    Public Property IdAccountClass() As AccountClassXpo
        Get
            Return fIdAccountClass
        End Get
        Set(ByVal value As AccountClassXpo)
            SetPropertyValue(Of AccountClassXpo)("IdAccountClass", fIdAccountClass, value)
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
    ''' Gets or sets the parent identifier.
    ''' </summary>
    ''' <value>
    ''' The parent identifier.
    ''' </value>
    <Persistent("IdParent")> _
    Public Property IdParent As Integer
        Get
            Return _IdParent
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("IdParent", _IdParent, value)
        End Set
    End Property

    Dim _IdAccountLevel As AccountLevelXpo
    ''' <summary>
    ''' Gets or sets the parent identifier.
    ''' </summary>
    ''' <value>
    ''' The parent identifier.
    ''' </value>
    <Association("PUCXpoReferenceAccountLevelXpo")>
    Public Property IdAccountLevel As AccountLevelXpo
        Get
            Return _IdAccountLevel
        End Get
        Set(ByVal value As AccountLevelXpo)
            SetPropertyValue(Of AccountLevelXpo)("IdAccountLevel", _IdAccountLevel, value)
        End Set
    End Property



    ''' <summary>
    ''' Gets or sets a value indicating whether [handles third].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [handles third]; otherwise, <c>false</c>.
    ''' </value>
    <Persistent("HandlesThirdParty")> _
    Public Property HandlesThirdParty As Boolean
        Get
            Return _HandlesThirdParty
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesThirdParty", _HandlesThirdParty, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [handles center].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [handles center]; otherwise, <c>false</c>.
    ''' </value>
    <Persistent("HandlesCostCenter")> _
    Public Property HandlesCostCenter As Boolean
        Get
            Return _HandlesCostCenter
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesCostCenter", _HandlesCostCenter, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the type retencion.
    ''' </summary>
    ''' <value>
    ''' The type retencion.
    ''' </value>
    <Persistent("RetencionType")> _
    Public Property RetencionType As Integer
        Get
            Return _RetencionType
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("RetencionType", _RetencionType, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [FreelancerCategory].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if FreelancerCategory; otherwise, <c>false</c>.
    ''' </value>
    <Persistent("FreelancerCategory")> _
    Public Property FreelancerCategory As Boolean
        Get
            Return _FreelancerCategory
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("FreelancerCategory", _FreelancerCategory, value)
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

    Dim fAvailability As Integer
    Public Property Availability() As Integer
        Get
            Return fAvailability
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Availability", fAvailability, value)
        End Set
    End Property

    '<PersistentAlias("Iif(Availability = 0, 'Ninguna',Iif(Availability = 1, 'Corriente',Iif(Availability = 2, 'NoCorriente', Iif(Availability = 3, 'Ambas', ''))))")>
    Public ReadOnly Property AvailabilityName As String
        Get
            'Select Case fAvailability
            '    Case 0
            '        Return "Ninguna"
            '    Case 1
            '        Return "Corriente"
            '    Case 2
            '        Return "NoCorriente"
            '    Case 3
            '        Return "Ambas"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("AvailabilityName"))
        End Get
    End Property

    Dim fShowCGN2 As Boolean
    Public Property ShowCGN2() As Boolean
        Get
            Return fShowCGN2
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ShowCGN2", fShowCGN2, value)
        End Set
    End Property

    <PersistentAlias("Iif(Nature = 1, 'Debito', Iif(Nature = 2, 'Credito', IdAccountClass.NatureName))")>
    Public ReadOnly Property NatureName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NatureName"))
        End Get
    End Property

    <PersistentAlias("Iif(RetencionType = 0, 'Ninguna', RetencionType = 1, 'Retefuente', RetencionType = 2, 'ReteIva', RetencionType = 3, 'ReteIca', 'Otra')")>
    Public ReadOnly Property RetencionTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("RetencionTypeName"))
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
