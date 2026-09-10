Imports DevExpress.Xpo

<Persistent("Payments.DocumentSupport")>
Public Class PaymentsDocumentSupportXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
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

    Dim fResolutionNumber As String
    Public Property ResolutionNumber() As String
        Get
            Return fResolutionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResolutionNumber", fResolutionNumber, value)
        End Set
    End Property

    Dim fResolutionDate As DateTime
    Public Property ResolutionDate() As DateTime
        Get
            Return fResolutionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ResolutionDate", fResolutionDate, value)
        End Set
    End Property

    Dim fDocumentType As Byte
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Dim fInitialDocument As Long
    Public Property InitialDocument() As Long
        Get
            Return fInitialDocument
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("InitialDocument", fInitialDocument, value)
        End Set
    End Property

    Dim fFinalDocument As Long
    Public Property FinalDocument() As Long
        Get
            Return fFinalDocument
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("FinalDocument", fFinalDocument, value)
        End Set
    End Property

    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fFinalDate As DateTime
    Public Property FinalDate() As DateTime
        Get
            Return fFinalDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FinalDate", fFinalDate, value)
        End Set
    End Property

#End Region

#Region "CustomProperties"

    <PersistentAlias("concat(Code,' - ',Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <PersistentAlias("Iif(DocumentType = 1, 'POR COMPUTADOR','ELECTRÓNICA')")>
    Public ReadOnly Property DocumentTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentTypeName"))
        End Get
    End Property
#End Region

#Region "NavigationProperties"

    <Association("PaymentsAccountPayableDocumentSupport_References_DocumentSupport", GetType(AccountPayableDocumentSupportXpo))>
    Public ReadOnly Property AccountPayableDocumentSupport() As XPCollection(Of AccountPayableDocumentSupportXpo)
        Get
            Return GetCollection(Of AccountPayableDocumentSupportXpo)("AccountPayableDocumentSupport")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class