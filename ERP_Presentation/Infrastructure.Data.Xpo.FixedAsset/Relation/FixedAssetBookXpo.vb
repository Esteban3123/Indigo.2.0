'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.AccountingRepository
' Author           : Juan F. Tamayo
' Created          : 2014-01-19
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports DevExpress.Xpo

#End Region

''' <summary>
''' Tipo de documento usado en los servicios Xpo
''' </summary>
<Persistent("GeneralLedger.LegalBook")> _
Public Class FixedAssetBookXpo
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

    <PersistentAlias("Iif(TypeBook = 1, 'Colgap', 'NIIF')")>
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


    <Association("FixedAsserBookItemReferencePhysicalAssetBook", GetType(FixedAssetFixedAssetPhysicalAssetDetailBookXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetDetailBookXpo() As XPCollection(Of FixedAssetFixedAssetPhysicalAssetDetailBookXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetPhysicalAssetDetailBookXpo)("FixedAssetPhysicalAssetDetailBookXpo")
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
