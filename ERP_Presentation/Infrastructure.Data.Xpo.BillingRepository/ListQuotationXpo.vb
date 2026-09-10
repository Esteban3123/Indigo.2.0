Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel


<Persistent("Billing.Quotation")> _
Public Class ListQuotationXpo
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
    

    Dim fDate As DateTime
    Public Property QuoteDate() As DateTime
        Get
            Return fDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("QuoteDate", fDate, value)
        End Set
    End Property

    Private fliquidationAuthorization As String
    <Size(100)> _
    Public Property LiquidationAuthorization() As String
        Get
            Return fliquidationAuthorization
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LiquidationAuthorization", fliquidationAuthorization, value)
        End Set
    End Property

    Private ffunctionalUnitId As Integer
    Public Property FunctionalUnitId() As Integer
        Get
            Return ffunctionalUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FunctionalUnitId", ffunctionalUnitId, value)
        End Set
    End Property

    Private fquoteType As Byte
    Public Property QuoteType() As Byte
        Get
            Return fquoteType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("QuoteType", fquoteType, value)
        End Set
    End Property

    Private fdescription As String
    <Size(300)> _
    Public Property Description() As String
        Get
            Return fdescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fdescription, value)
        End Set
    End Property

    Private fstatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fstatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fstatus, value)
        End Set
    End Property

    <PersistentAlias("iif(Status=True, 'Activo', 'Inactivo')")>
    Public ReadOnly Property Estado() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("Estado"))
        End Get
    End Property


    Private fcreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fcreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fcreationUser, value)
        End Set
    End Property

    Private fcreationDate As Date
    Public Property CreationDate() As Date
        Get
            Return fcreationDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("CreationDate", fcreationDate, value)
        End Set
    End Property

    Private fmodificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fmodificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fmodificationUser, value)
        End Set
    End Property

    Private fmodificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fmodificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fmodificationDate, value)
        End Set
    End Property
    
#End Region
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub


End Class
