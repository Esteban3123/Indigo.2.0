'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BudgetControlRepository
    Inherits GenericRepository(Of BudgetControl)
    Implements IBudgetControlRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region


#Region "Methods"
    Function GetBudgetControl(Consecutive As String, Process As Integer) As BudgetControl Implements IBudgetControlRepository.GetBudgetControl
        Dim result = (From e In _context.BudgetControl
                     Where e.DocumentNumber = Consecutive And e.DocumentType = Process Select e).FirstOrDefault

        If result IsNot Nothing Then
            Return result
        Else
            Return New BudgetControl
        End If
    End Function
#End Region

End Class