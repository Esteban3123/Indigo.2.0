Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Contract.HealthAdministrator")> _
Public Class AdministratorHealthXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
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
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fThirdPartyId As ThirdsPartyXpo
    <Association("Contract_HealthAdministratorReferencesCommon_ThirdParty")> _
    Public Property ThirdPartyId() As ThirdsPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As ThirdsPartyXpo)
            SetPropertyValue(Of ThirdsPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fEntityType As Byte
    Public Property EntityType() As Byte
        Get
            Return fEntityType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("EntityType", fEntityType, value)
        End Set
    End Property

    Dim fHealthEntityCode As String
    <Size(6)> _
    Public Property HealthEntityCode() As String
        Get
            Return fHealthEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthEntityCode", fHealthEntityCode, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

#End Region

#Region "Navigations"

    <Association("Contract_ContractReferencesContract_HealthAdministrator", GetType(ContractsXpo))> _
    Public ReadOnly Property Contract_Contracts() As XPCollection(Of ContractsXpo)
        Get
            Return GetCollection(Of ContractsXpo)("Contract_Contracts")
        End Get
    End Property

    <Association("Billing_InvoiceReferencesContract_HealthAdministrator", GetType(InvoicesXpo))> _
    Public ReadOnly Property Billing_Invoices() As XPCollection(Of InvoicesXpo)
        Get
            Return GetCollection(Of InvoicesXpo)("Billing_Invoices")
        End Get
    End Property

    <Association("Billing_InvoiceEntityCapitatedDistributionDetailReferencesContract_HealthAdministrator", GetType(InvoiceEntityCapitatedDistributionDetailReportXpo))> _
    Public ReadOnly Property InvoiceEntityCapitatedDistributionDetails() As XPCollection(Of InvoiceEntityCapitatedDistributionDetailReportXpo)
        Get
            Return GetCollection(Of InvoiceEntityCapitatedDistributionDetailReportXpo)("InvoiceEntityCapitatedDistributionDetails")
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

#End Region

End Class
