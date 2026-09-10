''' <summary>
''' clase para mapear la consulta del detalle del presupuesto por rubro
''' </summary>
''' <remarks></remarks>
Public Class BudgetEntry

    Property Id As Integer
    Property CodeBudget As String
    Property NameBudget As String
    Property Resource As String
    Property ResolutionValue As Double
    Property ListBudgetEntry As New List(Of BudgetEntryDetail)



End Class
