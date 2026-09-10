'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ExpenseTypeRepository
    Inherits GenericRepository(Of RevenueType)
    Implements IExpenseTypeRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region


#Region "Methods"
    ''' <summary>
    ''' Obtiene un tipo de gasto
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetExpenseType(code As String, validityId As Integer, type As Integer, Optional tracking As Boolean = True) As RevenueType Implements IExpenseTypeRepository.GetExpenseType
        Dim RevenueType = From e In _context.RevenueType
                     Where e.Code = code AndAlso e.BudgetaryValidityId = validityId AndAlso e.Type = type
                     Select e
        If RevenueType.Count > 0 Then
            Dim objExpenseType = Nothing
            RevenueType.SingleOrDefault().OriginalValue = (From e In _context.RevenueType.AsNoTracking
                                Where e.Code = code AndAlso e.BudgetaryValidityId = validityId AndAlso e.Type = type
                                Select e).SingleOrDefault
            objExpenseType = RevenueType.SingleOrDefault()
            Return objExpenseType
        Else
            Return New RevenueType()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un tipo de gasto
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetExpenseTypeByValidity(code As String, ValidityId As String, Optional tracking As Boolean = True) As RevenueType Implements IExpenseTypeRepository.GetExpenseTypeByValidity
        'Dim RevenueType = From e In _context.RevenueType.Include("BudgetaryValidity")
        '             Where e.Code = code And e.ValidityId = ValidityId
        '             Select e
        'If RevenueType.Count > 0 Then
        '    Dim objExpenseType = Nothing
        '    RevenueType.SingleOrDefault().OriginalValue = (From e In _context.RevenueType.AsNoTracking
        '                        Where e.Code = code And e.ValidityId = ValidityId
        '                        Select e).SingleOrDefault
        '    objExpenseType = RevenueType.SingleOrDefault()
        '    Return objExpenseType
        'Else
        '    Return New RevenueType()
        'End If
        Return New RevenueType()
    End Function

    ''' <summary>
    ''' Obtiene un tipo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRevenueTypeById(id As Integer) As RevenueType Implements IExpenseTypeRepository.GetRevenueTypeById
        Return (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = id Select rt).FirstOrDefault()
    End Function

#End Region

   
End Class
