'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class LevelCategoryRepository
    Inherits GenericRepository(Of LevelCategory)
    Implements ILevelCategoryRepository


    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region


#Region "Methods"
    Public Function GetLevelCategory(level As String, Optional tracking As Boolean = True) As LevelCategory Implements ILevelCategoryRepository.GetLevelCategory
        Dim levelCategory = From e In _context.LevelCategory
                     Where e.Level = level
                     Select e
        If levelCategory.Count > 0 Then
            Dim objBankCity = Nothing
            levelCategory.SingleOrDefault().OriginalValue = (From e In _context.LevelCategory.AsNoTracking
                                Where e.Level = level
                                Select e).SingleOrDefault
            objBankCity = levelCategory.SingleOrDefault()
            Return objBankCity
        Else
            Return New LevelCategory()
        End If
    End Function

    Public Function ListLevelsCategory() As List(Of LevelCategory) Implements ILevelCategoryRepository.ListLevelsCategory
        Dim levelsCategory = From e In _context.LevelCategory
                              Select e

        If levelsCategory.Count > 0 Then
            Return levelsCategory.ToList
        End If
        Return New List(Of LevelCategory)
    End Function
#End Region


    
End Class
