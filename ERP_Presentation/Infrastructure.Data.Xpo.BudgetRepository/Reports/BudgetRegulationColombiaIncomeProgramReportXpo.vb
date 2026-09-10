Imports DevExpress.Xpo

Public Class BudgetRegulationColombiaIncomeProgramHeaderReportXpo
    Public Property Letra As String
    Public Property CodigoCHIP As String
    Public Property Periodo As String
    Public Property Anio As String
    Public Property NombreFormulario As String
End Class

#Region "Encabezado"
<Persistent("Budget.BudgetaryValidity")>
Public Class BudgetRegulationColombiaIncomeProgramBudgetaryValidityXpo
    Inherits XPLiteObject
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
    Dim fIncomeMonth As Integer
    Public Property IncomeMonth() As Integer
        Get
            Return fIncomeMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IncomeMonth", fIncomeMonth, value)
        End Set
    End Property
    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property


    Dim fBudgetaryEntityId As BudgetRegulationColombiaIncomeProgramBudgetaryEntityXpo
    <Association("BudgetRegulationColombiaIncomeProgramBudgetaryValidityXpo_BudgetBudgetaryEntityXpo")>
    Public Property BudgetaryEntityId() As BudgetRegulationColombiaIncomeProgramBudgetaryEntityXpo
        Get
            Return fBudgetaryEntityId
        End Get
        Set(ByVal value As BudgetRegulationColombiaIncomeProgramBudgetaryEntityXpo)
            SetPropertyValue(Of BudgetRegulationColombiaIncomeProgramBudgetaryEntityXpo)("BudgetaryEntityId", fBudgetaryEntityId, value)
        End Set
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

<Persistent("Budget.BudgetaryEntity")>
Public Class BudgetRegulationColombiaIncomeProgramBudgetaryEntityXpo
    Inherits XPLiteObject

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
    <Size(20)>
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    <Association("BudgetRegulationColombiaIncomeProgramBudgetaryValidityXpo_BudgetBudgetaryEntityXpo", GetType(BudgetRegulationColombiaIncomeProgramBudgetaryValidityXpo))>
    Public ReadOnly Property BudgetBudgetaryValidityXpo() As XPCollection(Of BudgetRegulationColombiaIncomeProgramBudgetaryValidityXpo)
        Get
            Return GetCollection(Of BudgetRegulationColombiaIncomeProgramBudgetaryValidityXpo)("BudgetRegulationColombiaIncomeProgramBudgetaryValidityXpo")
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

#End Region
