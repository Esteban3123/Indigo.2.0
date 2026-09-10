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

    <PersistentAlias("Iif(TypeBook = 1, 'Colgap', 'NIIF')")>
    Public ReadOnly Property TypeBookName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeBookName"))
        End Get
    End Property

    Dim fOfficialCurrencyId As CommonCurrencyXpo
    <Association("BookItemReferenceCurrency")>
    Public Property OfficialCurrencyId() As CommonCurrencyXpo
        Get
            Return fOfficialCurrencyId
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue(Of CommonCurrencyXpo)("OfficialCurrencyId", fOfficialCurrencyId, value)
        End Set
    End Property

    Dim fCodeName As String
    'columna que devuelve el nit y el nombre concatenado
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("FixedAssetItemDetailReferenceLegalBook", GetType(FixedAssetItemDetailXpo))> _
    Public ReadOnly Property FixedAssetItemDetailXpo() As XPCollection(Of FixedAssetItemDetailXpo)
        Get
            Return GetCollection(Of FixedAssetItemDetailXpo)("FixedAssetItemDetailXpo")
        End Get
    End Property

    <Association("RemissionEntranceItemDetailBookReferenceLegalBook", GetType(FixedAssetRemissionEntranceItemDetailBookXpo))> _
    Public ReadOnly Property FixedAssetRemissionEntranceItemDetailBookXpo() As XPCollection(Of FixedAssetRemissionEntranceItemDetailBookXpo)
        Get
            Return GetCollection(Of FixedAssetRemissionEntranceItemDetailBookXpo)("FixedAssetRemissionEntranceItemDetailBookXpo")
        End Get
    End Property

    <Association("RemissionEntranceItemDetailPartBookReferenceLegalBook", GetType(FixedAssetRemissionEntranceItemDetailPartBookXpo))> _
    Public ReadOnly Property FixedAssetRemissionEntranceItemDetailPartBookXpo() As XPCollection(Of FixedAssetRemissionEntranceItemDetailPartBookXpo)
        Get
            Return GetCollection(Of FixedAssetRemissionEntranceItemDetailPartBookXpo)("FixedAssetRemissionEntranceItemDetailPartBookXpo")
        End Get
    End Property

    <Association("BookItemReferencePhysicalAssetBook", GetType(FixedAssetPhysicalAssetDetailBookXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetDetailBookXpo() As XPCollection(Of FixedAssetPhysicalAssetDetailBookXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetDetailBookXpo)("FixedAssetPhysicalAssetDetailBookXpo")
        End Get
    End Property

    <Association("BookItemReferencePhysicalAssetPartsBook", GetType(FixedAssetPhysicalAssetPartsDetailBookXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetPartsDetailBookXpo() As XPCollection(Of FixedAssetPhysicalAssetPartsDetailBookXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetPartsDetailBookXpo)("FixedAssetPhysicalAssetPartsDetailBookXpo")
        End Get
    End Property

    <Association("DepreciationDetailReferenceBook", GetType(FixedAssetDepreciationDetailXpo))>
    Public ReadOnly Property FixedAssetDepreciationDetailXpo() As XPCollection(Of FixedAssetDepreciationDetailXpo)
        Get
            Return GetCollection(Of FixedAssetDepreciationDetailXpo)("FixedAssetDepreciationDetailXpo")
        End Get
    End Property

    <Association("FixedAssetForReclassificationReferenceBook", GetType(ViewListFixedAssetForReclassificationXpo))>
    Public ReadOnly Property ViewListFixedAssetForReclassificationXpo() As XPCollection(Of ViewListFixedAssetForReclassificationXpo)
        Get
            Return GetCollection(Of ViewListFixedAssetForReclassificationXpo)("ViewListFixedAssetForReclassificationXpo")
        End Get
    End Property

    <Association("FixedAssetReclassifiedReferenceBook", GetType(ViewListFixedAssetReclassifiedXpo))>
    Public ReadOnly Property ViewListFixedAssetReclassifiedXpo() As XPCollection(Of ViewListFixedAssetReclassifiedXpo)
        Get
            Return GetCollection(Of ViewListFixedAssetReclassifiedXpo)("ViewListFixedAssetReclassifiedXpo")
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
