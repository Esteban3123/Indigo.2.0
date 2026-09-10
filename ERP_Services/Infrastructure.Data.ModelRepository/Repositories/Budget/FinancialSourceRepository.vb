'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class FinancialSourceRepository
    Inherits GenericRepository(Of FinancialSource)
    Implements IFinancialSourceRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una fuente de financiacion por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetFinancialSource(code As String, validityId As Integer, Optional tracking As Boolean = True) As FinancialSource Implements IFinancialSourceRepository.GetFinancialSource
        Dim financialSource = From e In _context.FinancialSource
                     Where e.Code = code AndAlso e.BudgetaryValidityId = validityId
                     Select e
        If financialSource.Count > 0 Then
            Dim objBankCity = Nothing
            financialSource.SingleOrDefault().OriginalValue = (From e In _context.FinancialSource.AsNoTracking
                                Where e.Code = code AndAlso e.BudgetaryValidityId = validityId
                                Select e).SingleOrDefault
            objBankCity = financialSource.SingleOrDefault()
            Return objBankCity
        Else
            Return New FinancialSource()
        End If
    End Function

    ''' <summary>
    ''' Obtiene todos los recursos para copiarlos y agregarlos a otra vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListFinancialSourceForCopyBase(validityId As Integer) As List(Of FinancialSource) Implements IFinancialSourceRepository.GetListFinancialSourceForCopyBase
        Dim listFinancialSource = (From e In _context.FinancialSource.AsNoTracking Where e.BudgetaryValidityId = validityId Select e).ToList
        If listFinancialSource.Count > 0 Then
            Return listFinancialSource
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene la fuente de financiacio por codigo y la vigencia
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="validityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFinancialSourceByCodeAndValidityForCopyBase(code As String, validityId As Integer) As FinancialSource Implements IFinancialSourceRepository.GetFinancialSourceByCodeAndValidityForCopyBase
        Return (From e In _context.FinancialSource.AsNoTracking Where e.BudgetaryValidityId = validityId AndAlso e.Code = code Select e).FirstOrDefault
    End Function

#End Region

End Class
