#Region "Imports"

Imports System.Data
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

#End Region

Public Class MReports
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        _indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

#Region "Income"

    Public Async Function GetReportListDocumentIncome(criterias As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetReportListDocumentIncomeAsync(criterias, Me._indigo)
    End Function

    Public Async Function GetListReportBudgetExecutionIncome(criterias As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetListReportBudgetExecutionIncomeAsync(criterias, Me._indigo)
    End Function

    Public Async Function GetListReportMonthlyIncomeExecution(criterias As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetReportMonthlyIncomeExecutionAsync(criterias, Me._indigo)
    End Function

    Public Async Function GetListReportIncomeRecordBook(criterias As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetReportIncomeRecordBookAsync(criterias, Me._indigo)
    End Function

    Public Async Function GenerateArchiveReportIncomeProgramming(criterias As Dictionary(Of String, String)) As Task(Of StringBuilder)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GenerateArchiveReportIncomeProgrammingAsync(criterias, Me._indigo)
    End Function

#End Region

#Region "Expense"

    Public Async Function GetReportBudgetExecutionByCategoryThird(criterias As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetReportBudgetExecutionByCategoryThirdAsync(criterias, Me._indigo)
    End Function

    Public Async Function GetReportListDocumentExpense(criterias As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetReportListDocumentExpenseAsync(criterias, Me._indigo)
    End Function

    Public Async Function GetListReportBudgetExecutionExpense(criterias As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetListReportBudgetExecutionExpenseAsync(criterias, Me._indigo)
    End Function

    Public Async Function GetListReportMonthlyExpenseExecution(criterias As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetReportMonthlyExpenseExecutionAsync(criterias, Me._indigo)
    End Function

    Public Async Function GetListReportExpenseRecordBook(criterias As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetReportExpenseRecordBookAsync(criterias, Me._indigo)
    End Function

    Public Async Function GetExpendituresProgrammingForTerritorialPublicEstablisments(criterias As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetExpendituresProgrammingForTerritorialPublicEstablismentsAsync(criterias, Me._indigo)
    End Function

    Public Async Function GenerateArchiveReportExpendituresProgrammingForTerritorialPublicEstablisments(criterias As Dictionary(Of String, String)) As Task(Of StringBuilder)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GenerateArchiveReportExpendituresProgrammingForTerritorialPublicEstablismentsAsync(criterias, Me._indigo)
    End Function

    Public Async Function GetStringBuilderBudgetExecutionOfExpensesForTerritorialPublicEntitiesAsync(criterias As Dictionary(Of String, String)) As Task(Of StringBuilder)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetStringBuilderBudgetExecutionOfExpensesForTerritorialPublicEntitiesAsync(criterias, Me._indigo)
    End Function

#End Region

#End Region


#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
