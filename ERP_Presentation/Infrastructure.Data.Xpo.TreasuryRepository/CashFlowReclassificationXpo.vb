Imports DevExpress.Xpo

<Persistent("Treasury.CashFlowReclassification")>
Public Class CashFlowReclassificationXpo
    Inherits XPLiteObject
#Region "Members"
    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    <Persistent("DocumentDate")>
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fDocumentType As Byte
    <Persistent("DocumentType")>
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
        End Set
    End Property
    <PersistentAlias("Iif(DocumentType = 1, 'Recibos de caja', DocumentType = 2, 'Comprobantes de egreso', DocumentType = 4, 'Notas', DocumentType = 5, 'Cruce de cuentas','')")>
    Public ReadOnly Property DocumentName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentName"))
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
