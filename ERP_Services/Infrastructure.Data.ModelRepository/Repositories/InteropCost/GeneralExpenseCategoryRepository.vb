'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/11/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources

Public Class GeneralExpenseCategoryRepository
    Inherits GenericRepository(Of GeneralExpenseCategory)
    Implements IGeneralExpenseCategoryRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetGeneralExpenseCategory(code As String) As GeneralExpenseCategory Implements IGeneralExpenseCategoryRepository.GetGeneralExpenseCategory
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As GeneralExpenseCategory In Me._context.GeneralExpenseCategory Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.ParentId IsNot Nothing Then
                Dim GeneralExpenseCategory = (From st In _context.GeneralExpenseCategory.AsNoTracking Where st.Id = res.ParentId Select st).FirstOrDefault
                res.GeneralExpensecategoryDescription = GeneralExpenseCategory.Code + " - " + GeneralExpenseCategory.Name
            End If

            res.OriginalValue = (From d As GeneralExpenseCategory In Me._context.GeneralExpenseCategory.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New GeneralExpenseCategory()
        End If
    End Function

    Public Function GetGeneralExpenseCategoryById(id As Integer) As GeneralExpenseCategory Implements IGeneralExpenseCategoryRepository.GetGeneralExpenseCategoryById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From o In _context.GeneralExpenseCategory Where o.Id = id Select o).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From o In _context.GeneralExpenseCategory.AsNoTracking() Where o.Id = id Select o).FirstOrDefault()
            Return query
        Else
            Return New GeneralExpenseCategory()
        End If
    End Function

End Class
