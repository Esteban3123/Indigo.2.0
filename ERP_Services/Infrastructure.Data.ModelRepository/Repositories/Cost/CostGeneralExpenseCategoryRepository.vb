'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/11/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CostGeneralExpenseCategoryRepository
    Inherits GenericRepository(Of CostGeneralExpenseCategory)
    Implements ICostGeneralExpenseCategoryRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostGeneralExpenseCategory(code As String) As CostGeneralExpenseCategory Implements ICostGeneralExpenseCategoryRepository.GetCostGeneralExpenseCategory
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As CostGeneralExpenseCategory In Me._context.CostGeneralExpenseCategory Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.ParentId IsNot Nothing Then
                Dim CostGeneralExpenseCategory = (From st In _context.CostGeneralExpenseCategory.AsNoTracking Where st.Id = res.ParentId Select st).FirstOrDefault
                res.CostGeneralExpensecategoryDescription = CostGeneralExpenseCategory.Code + " - " + CostGeneralExpenseCategory.Name
            End If

            res.OriginalValue = (From d As CostGeneralExpenseCategory In Me._context.CostGeneralExpenseCategory.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New CostGeneralExpenseCategory()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostGeneralExpenseCategoryById(id As Integer) As CostGeneralExpenseCategory Implements ICostGeneralExpenseCategoryRepository.GetCostGeneralExpenseCategoryById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From o In _context.CostGeneralExpenseCategory Where o.Id = id Select o).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From o In _context.CostGeneralExpenseCategory.AsNoTracking() Where o.Id = id Select o).FirstOrDefault()
            Return query
        Else
            Return New CostGeneralExpenseCategory()
        End If
    End Function

End Class