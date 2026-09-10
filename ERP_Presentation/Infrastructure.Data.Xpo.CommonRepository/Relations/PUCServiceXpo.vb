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

#Region "Fields"
    ''' <summary>
    ''' Gets or sets the identifier.
    ''' </summary>
    ''' <value>
    ''' The identifier.
    ''' </value>
    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")> _
    Public Property Id As Integer
        Get
            Return fId
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the account code.
    ''' </summary>
    ''' The account code.
    ''' </value>
    Dim fNumber As String
    <Size(20)> _
    <Persistent("Number")> _
    Public Property Number As String
        Get
            Return fNumber
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Number", fNumber, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the name of the account.
    ''' </summary>
    ''' The name of the account.
    ''' </value>
    Dim fName As String
    <Size(100)> _
    <Persistent("Name")> _
    Public Property Name As String
        Get
            Return fName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [handles third].
    ''' </summary>
    '''   <c>true</c> if [handles third]; otherwise, <c>false</c>.
    ''' </value>
    Dim fHandlesThirdParty As Boolean
    <Persistent("HandlesThirdParty")> _
    Public Property HandlesThirdParty As Boolean
        Get
            Return fHandlesThirdParty
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesThirdParty", fHandlesThirdParty, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [handles center].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [handles center]; otherwise, <c>false</c>.
    Dim fHandlesCostCenter As Boolean
    <Persistent("HandlesCostCenter")>
    Public Property HandlesCostCenter As Boolean
        Get
            Return fHandlesCostCenter
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesCostCenter", fHandlesCostCenter, value)
        End Set
    End Property

    Dim fRetencionType As Byte
    <Persistent("RetencionType")>
    Public Property RetencionType As Byte
        Get
            Return fRetencionType
        End Get
        Set(value As Byte)
            SetPropertyValue(Of Byte)("RetencionType", fRetencionType, value)
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

    <Association("DistributionLinesReferencesPUC", GetType(CommonDistibutionLineXpo))>
    Public ReadOnly Property DistributionLine() As XPCollection(Of CommonDistibutionLineXpo)
        Get
            Return GetCollection(Of CommonDistibutionLineXpo)("DistributionLine")
        End Get
    End Property

    <Association("DistributionLinesMainAccountCostProvisionReferencesPUC", GetType(CommonDistibutionLineXpo))>
    Public ReadOnly Property DistributionLineMainAccountCostProvision() As XPCollection(Of CommonDistibutionLineXpo)
        Get
            Return GetCollection(Of CommonDistibutionLineXpo)("DistributionLineMainAccountCostProvision")
        End Get
    End Property

    <Association("ConceptReferencesPUC", GetType(ConceptsAccountPayableXpo))> _
    Public ReadOnly Property ConceptsAccountPayableXpo() As XPCollection(Of ConceptsAccountPayableXpo)
        Get
            Return GetCollection(Of ConceptsAccountPayableXpo)("ConceptsAccountPayableXpo")
        End Get
    End Property

    <Association("ConceptReferencesThreeEightThreeAccountId", GetType(ConceptsAccountPayableXpo))>
    Public ReadOnly Property ConceptsAccountPayableThreeEightThreeAccountIdXpo() As XPCollection(Of ConceptsAccountPayableXpo)
        Get
            Return GetCollection(Of ConceptsAccountPayableXpo)("ConceptsAccountPayableThreeEightThreeAccountIdXpo")
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
