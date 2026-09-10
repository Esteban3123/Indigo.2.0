Imports System
Imports DevExpress.Xpo

<Persistent("Security.Roll")> _
Partial Public Class Security_Roll
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
    Dim fRollCode As String
    <Size(3)>
    Public Property RollCode() As String
        Get
            Return fRollCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RollCode", fRollCode, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(60)>
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    'columna que devuelve el codigo y el nombre concatenado
    <Size(120)>
    <PersistentAlias("concat(concat(RollCode,' - '),Description)")>
    Public ReadOnly Property RolCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("RolCodeName"))
        End Get
    End Property
    Dim fRollType As Byte
    Public Property RollType() As Byte
        Get
            Return fRollType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RollType", fRollType, value)
        End Set
    End Property
    <Size(20)>
    <PersistentAlias("IIF(RollType=1,'Global','Por tenant')")>
    Public ReadOnly Property RollTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("RollTypeName"))
        End Get
    End Property
#End Region

#Region "Association"
    <Association("TenantRollXpo_Roll", GetType(EntityTenantRollXpo))>
    Public ReadOnly Property EntityTenantRollXpo() As XPCollection(Of EntityTenantRollXpo)
        Get
            Return GetCollection(Of EntityTenantRollXpo)("EntityTenantRollXpo")
        End Get
    End Property
#End Region

#Region "Builder"
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
