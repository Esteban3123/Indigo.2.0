'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andres Alarcon
' Created          : 21-02-2025
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

Imports DevExpress.Xpo

<Persistent("MixingStation.LineClearanceCriteria")>
Partial Public Class LineClearanceCriteriaXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim _Id As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", _Id, value)
        End Set
    End Property

    Dim _Code As String
    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", _Code, value)
        End Set
    End Property

    Dim _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", _Name, value)
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

    <PersistentAlias("Iif(State = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    '<Association("ProductionLineUnitDoseTypeReferencesProductionLine", GetType(ProductionLineUnitDoseTypeXpo))>
    'Public ReadOnly Property ProductionLineUnitDoseTypeXpo() As XPCollection(Of ProductionLineUnitDoseTypeXpo)
    '    Get
    '        Return GetCollection(Of ProductionLineUnitDoseTypeXpo)("ProductionLineUnitDoseTypeXpo")
    '    End Get
    'End Property

End Class
