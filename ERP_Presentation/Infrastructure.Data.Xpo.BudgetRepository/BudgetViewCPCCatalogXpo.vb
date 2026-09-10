#Region "imports"
Imports DevExpress.Xpo
#End Region
''' <summary>
'''Autor: HECTOR RODRIGUEZ RUBIANO
'''Proposito: CPCCatalog activos sin hijos
''' </summary>
<Persistent("Budget.ViewListCPCCatalog")>
Public Class BudgetViewCPCCatalogXpo
    Inherits XPLiteObject

#Region "Members"
    Dim fId As Integer
    <Key()>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

#End Region

#Region "Custom Members"
    <PersistentAlias("concat(Code,' - ',Name)")>
    Public ReadOnly Property NameCode() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NameCode"))
        End Get
    End Property
#End Region

#Region "builder"
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

