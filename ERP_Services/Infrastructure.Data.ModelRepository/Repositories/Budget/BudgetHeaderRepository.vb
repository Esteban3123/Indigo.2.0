'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Public Class BudgetHeaderRepository
    Inherits GenericRepository(Of BudgetHeader)
    Implements IBudgetHeaderRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="IdBudget"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetHeaderByIdForBudgetTransfer(IdBudget As Integer) As BudgetHeader Implements IBudgetHeaderRepository.GetBudgetHeaderByIdForBudgetTransfer
        If IdBudget = 0 Then
            Throw New ArgumentNullException("IdBudget")
        End If
        'Se obtiene el id del header para consultarlo y poder agregarle el detalle nuevo en traslado
        Dim headerId As Integer = (From b In Me._context.Budget.AsNoTracking Where b.Id = IdBudget Select b.BudgetHeaderId).FirstOrDefault
        'Se consulta el budgetHeader con el id sacado anteriormente
        Dim res = (From bh In Me._context.BudgetHeader.Include("Budget") Where bh.Id = headerId Select bh).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return Nothing
        End If
    End Function

End Class
