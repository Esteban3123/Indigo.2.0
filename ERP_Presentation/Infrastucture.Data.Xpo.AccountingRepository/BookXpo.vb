'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.AccountingRepository
' Author           : Juan F. Tamayo
' Created          : 2014-01-19
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Tipo de documento usado en los servicios Xpo
''' </summary>
<Persistent("GeneralLedger.LegalBook")> _
Public Class BookXpo
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
    <Persistent("Description")> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fOfficialBook As Boolean
    Public Property OfficialBook() As Boolean
        Get
            Return fOfficialBook
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("OfficialBook", fOfficialBook, value)
        End Set
    End Property

    Dim fTypeBook As Integer
    Public Property TypeBook() As Integer
        Get
            Return fTypeBook
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TypeBook", fTypeBook, value)
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

    Dim fLastYearClose As Integer
    Public Property LastYearClose() As Integer
        Get
            Return fLastYearClose
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LastYearClose", fLastYearClose, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property OfficialCurrencyId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("OfficialCurrencyId"))
        End Get
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("OfficialCurrencyId")>
    <Association("CurrencyReferenceBookXpo")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property


    <PersistentAlias("CommonCurrency.ISO4217Xpo.CurrencyName")>
    Public ReadOnly Property CurrencyNameISO() As String
        Get
            Return Convert.ToString(EvaluateAlias("CurrencyNameISO"))
        End Get
    End Property


    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Iif(TypeBook = 1, 'COLGAAP', TypeBook = 2, 'NIIF', TypeBook = 3, 'Cuentas Estados Financieros', 'N/A')")>
    Public ReadOnly Property TypeBookName As String
        Get
            'If fTypeBook = 1 Then
            '    Return "Colgap"
            'Else
            '    Return "NIIF"
            'End If
            Return Convert.ToString(Me.EvaluateAlias("TypeBookName"))
        End Get
    End Property

    Dim fCodeName As String
    'columna que devuelve el nit y el nombre concatenado
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("MainAccountsXpoReferencesBookXpo", GetType(MainAccountsXpo))> _
    Public ReadOnly Property MainAccountsXpo() As XPCollection(Of MainAccountsXpo)
        Get
            Return GetCollection(Of MainAccountsXpo)("MainAccountsXpo")
        End Get
    End Property

    <Association("JournalVouchersXpoReferencesBookXpo", GetType(JournalVouchersXpo))> _
    Public ReadOnly Property JournalVouchersXpo() As XPCollection(Of JournalVouchersXpo)
        Get
            Return GetCollection(Of JournalVouchersXpo)("JournalVouchersXpo")
        End Get
    End Property

    <Association("PUCXpoReferencesBookXpo", GetType(PUCServiceXpo))> _
    Public ReadOnly Property PUCServiceXpo() As XPCollection(Of PUCServiceXpo)
        Get
            Return GetCollection(Of PUCServiceXpo)("PUCServiceXpo")
        End Get
    End Property

    <Association("VieBotXpoReferencesBookXpo", GetType(VieBotXpo))> _
    Public ReadOnly Property VieBotXpo() As XPCollection(Of VieBotXpo)
        Get
            Return GetCollection(Of VieBotXpo)("VieBotXpo")
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
