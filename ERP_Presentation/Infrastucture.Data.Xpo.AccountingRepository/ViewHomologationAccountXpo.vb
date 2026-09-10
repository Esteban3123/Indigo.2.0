'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.AccountingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/12/2015
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

#Region "Structure"

Public Structure KeyValue

    <Persistent("Id")> _
    Public Property Id As Integer

    <Persistent("OfficalMainAccountId")> _
    Public Property OfficalMainAccountId As Integer

    <Persistent("MainAccountId")> _
    Public Property MainAccountId As Integer

End Structure

#End Region

''' <summary>
''' asociacion entre procedureCups y MarketingUnitCups usado en los servicios Xpo
''' </summary>
<Persistent("GeneralLedger.ViewHomologationAccount")> _
Public Class ViewHomologationAccountXpo
    Inherits XPLiteObject

#Region "Members"

    <Key(), Persistent()> _
    Public Property Key As KeyValue

    Dim fId As Integer
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fOfficalMainAccountId As Integer
    Public Property OfficalMainAccountId() As Integer
        Get
            Return fOfficalMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OfficalMainAccountId", fOfficalMainAccountId, value)
        End Set
    End Property

    Dim fNumberNameOfficialMainAccount As String
    Public Property NumberNameOfficialMainAccount() As String
        Get
            Return fNumberNameOfficialMainAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumberNameOfficialMainAccount", fNumberNameOfficialMainAccount, value)
        End Set
    End Property

    Dim fMainAccountId As Integer
    Public Property MainAccountId() As Integer
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fNumberNameMainAccount As String
    Public Property NumberNameMainAccount() As String
        Get
            Return fNumberNameMainAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumberNameMainAccount", fNumberNameMainAccount, value)
        End Set
    End Property

    Dim fLegalBookId As Integer
    Public Property LegalBookId() As Integer
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LegalBookId", fLegalBookId, value)
        End Set
    End Property

    Dim fHomologationLegalBookId As Integer
    Public Property HomologationLegalBookId() As Integer
        Get
            Return fHomologationLegalBookId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HomologationLegalBookId", fHomologationLegalBookId, value)
        End Set
    End Property

    Dim fHandlesCostCenterOfficialMainAccount As Boolean
    Public Property HandlesCostCenterOfficialMainAccount() As Boolean
        Get
            Return fHandlesCostCenterOfficialMainAccount
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesCostCenterOfficialMainAccount", fHandlesCostCenterOfficialMainAccount, value)
        End Set
    End Property

    Dim fHandlesThirdPartyOfficialMainAccount As Boolean
    Public Property HandlesThirdPartyOfficialMainAccount() As Boolean
        Get
            Return fHandlesThirdPartyOfficialMainAccount
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesThirdPartyOfficialMainAccount", fHandlesThirdPartyOfficialMainAccount, value)
        End Set
    End Property

    Dim fCreationUser As String
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fModificationUser As String
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
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
