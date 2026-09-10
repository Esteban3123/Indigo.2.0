'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Hector Rodriguez
' Created          : 24-09-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("Payroll.FunctionalUnit")>
Partial Public Class CMFunctionalUnitXpo
    Inherits XPLiteObject

#Region "Members"
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

    Dim _Name As String
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", _Name, value)
        End Set
    End Property

    Dim _Code As String
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", _Code, value)
        End Set
    End Property

    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim _State As Boolean
    <Persistent("State")>
    Public Property State() As Boolean
        Get
            Return _State
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", _State, value)
        End Set
    End Property
#End Region

    #Region "Dependencies"
   <Association("MixingStation_ProductionLineReferencesFunctionalUnit", GetType(MixingStationProductionLineXpo))> _
    Public ReadOnly Property ProductionLine() As XPCollection(Of MixingStationProductionLineXpo)
        Get
            Return GetCollection(Of MixingStationProductionLineXpo)("ProductionLine")
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
