Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

Partial Public Class Budget_BudgetModification
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

