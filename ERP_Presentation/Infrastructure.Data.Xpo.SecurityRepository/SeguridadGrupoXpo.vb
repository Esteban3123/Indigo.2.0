Imports System
Imports DevExpress.Xpo

<Persistent("Security.Group")> _
Partial Public Class Security_Group
    Inherits XPLiteObject
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
    <Indexed(Name:="IX_SEGGRUUSU_CODGRUPOU", Unique:=True)> _
    <Size(3)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(60)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property
    'columna que devuelve el codigo y el nombre concatenado
    <Size(120)>
    <PersistentAlias("concat(concat(Code,' - '),Description)")>
    Public ReadOnly Property GroupCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("GroupCodeName"))
        End Get
    End Property
    Dim fGroupType As Byte
    Public Property GroupType() As Byte
        Get
            Return fGroupType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("GroupType", fGroupType, value)
        End Set
    End Property
    <Size(20)>
    <PersistentAlias("IIF(GroupType=1,'Global','Por tenant')")>
    Public ReadOnly Property GroupTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("GroupTypeName"))
        End Get
    End Property

#Region "Association"
    <Association("TenantGroupXpo_Group", GetType(EntityTenantGroupXpo))>
    Public ReadOnly Property EntityTenantRollXpo() As XPCollection(Of EntityTenantGroupXpo)
        Get
            Return GetCollection(Of EntityTenantGroupXpo)("EntityTenantGroupXpo")
        End Get
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
