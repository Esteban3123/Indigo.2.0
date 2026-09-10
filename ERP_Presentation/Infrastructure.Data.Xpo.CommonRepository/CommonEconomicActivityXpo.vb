'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.CommonRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 16-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.EconomicActivity")> _
Partial Public Class CommonEconomicActivity
    Inherits XPLiteObject

    Dim fId As Integer
    <Key()> _
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
    <Size(200)> _
    <Persistent("Name")> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
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

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    Dim fIsIncomeGenerating As Boolean
    Public Property IsIncomeGenerating() As Boolean
        Get
            Return fIsIncomeGenerating
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsIncomeGenerating", fIsIncomeGenerating, value)
        End Set
    End Property

    <PersistentAlias("Iif(IsIncomeGenerating = 1, 'Si', 'No')")>
    Public ReadOnly Property IsIncomeGeneratingName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("IsIncomeGeneratingName"))
        End Get
    End Property


    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("EconomicActivityReferencesCommonThirdPartyEconomicActivitiesXpo", GetType(CommonThirdPartyEconomicActivitiesXpo))>
    Public ReadOnly Property CommonThirdPartyEconomicActivitiesXpo() As XPCollection(Of CommonThirdPartyEconomicActivitiesXpo)
        Get
            Return GetCollection(Of CommonThirdPartyEconomicActivitiesXpo)("CommonThirdPartyEconomicActivitiesXpo")
        End Get
    End Property

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

