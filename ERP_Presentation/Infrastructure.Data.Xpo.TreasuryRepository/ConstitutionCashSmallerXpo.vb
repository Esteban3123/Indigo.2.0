'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' ciudades bancarias usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.ConstitutionCashSmaller")> _
Public Class ConstitutionCashSmallerXpo
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

    'Dim fDocumentDate As Date
    '<Persistent("DocumentDate")>
    '<ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")>
    '<ModelDefault("EditMask", "dd/MM/yyyy")>
    'Public Property DocumentDate() As Date
    '    Get
    '        Return fDocumentDate.Date
    '    End Get
    '    Set(ByVal value As Date)
    '        SetPropertyValue(Of Date)("DocumentDate", fDocumentDate, value)
    '    End Set
    'End Property

    Dim fDocumentDate As Date
    <Persistent("DocumentDate")>
    Public Property DocumentDate1() As Date
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    <PersistentAlias("GetDate(DocumentDate1)")>
    Public ReadOnly Property DocumentDate() As Date
        Get
            Return Convert.ToDateTime(Me.EvaluateAlias("DocumentDate")).Date
        End Get
    End Property

    Dim fDocumentType As Integer
    <Persistent("DocumentType")> _
    Public Property DocumentType() As Integer
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Dim fCashRegisterSmallerId As CashRegisterXpo
    <Association("ConsitutionCashSmallerReferencesCashRegister")> _
    Public Property CashRegisterSmallerId() As CashRegisterXpo
        Get
            Return fCashRegisterSmallerId
        End Get
        Set(ByVal value As CashRegisterXpo)
            SetPropertyValue(Of CashRegisterXpo)("CashRegisterSmallerId", fCashRegisterSmallerId, value)
        End Set
    End Property

    Dim fSourceType As Integer
    <Persistent("SourceType")> _
    Public Property SourceType() As Integer
        Get
            Return fSourceType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SourceType", fSourceType, value)
        End Set
    End Property

    Dim fCashRegisterId As Integer
    <Persistent("CashRegisterId")> _
    Public Property CashRegisterId() As Integer
        Get
            Return fCashRegisterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CashRegisterId", fCashRegisterId, value)
        End Set
    End Property

    Dim fEntityBankAccountId As Integer
    <Persistent("EntityBankAccountId")> _
    Public Property EntityBankAccountId() As Integer
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property

    Dim fValue As Decimal
    <Persistent("Value")> _
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

    Dim fStatus As Integer
    <Persistent("Status")> _
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
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
