'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BudgetConceptRepository
    Inherits GenericRepository(Of Concept)
    Implements IBudgetConceptRepository

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
    ''' Obtiene un concepto
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetBudgetConcept(code As String, validityId As Integer, Optional tracking As Boolean = True) As Concept Implements IBudgetConceptRepository.GetBudgetConcept
        Dim Concept = From e In _context.Concept
                     Where e.Code = code AndAlso e.BudgetaryValidityId = validityId
                     Select e
        If Concept.Count > 0 Then
            Dim objBudgetConcept = Nothing
            Concept.SingleOrDefault().OriginalValue = (From e In _context.Concept.AsNoTracking
                                Where e.Code = code AndAlso e.BudgetaryValidityId = validityId
                                Select e).SingleOrDefault
            objBudgetConcept = Concept.SingleOrDefault()
            Return objBudgetConcept
        Else
            Return New Concept()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un concepto
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetBudgetConceptByValidity(code As String, ValidityId As String, Optional tracking As Boolean = True) As Concept Implements IBudgetConceptRepository.GetBudgetConceptByValidity
        'Dim Concept = From e In _context.Concept.Include("BudgetaryValidity")
        '             Where e.Code = code And e.ValidityId = ValidityId
        '             Select e
        'If Concept.Count > 0 Then
        '    Dim objBudgetConcept = Nothing
        '    Concept.SingleOrDefault().OriginalValue = (From e In _context.Concept.AsNoTracking
        '                        Where e.Code = code And e.ValidityId = ValidityId
        '                        Select e).SingleOrDefault
        '    objBudgetConcept = Concept.SingleOrDefault()
        '    Return objBudgetConcept
        'Else
        '    Return New Concept()
        'End If
        Return New Concept()
    End Function

    ''' <summary>
    ''' Obtiene la dependencia por codigo y la vigencia
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="validityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConceptByCodeAndValidityForCopyBase(code As String, validityId As Integer) As Concept Implements IBudgetConceptRepository.GetConceptByCodeAndValidityForCopyBase
        Return (From e In _context.Concept.AsNoTracking Where e.BudgetaryValidityId = validityId AndAlso e.Code = code Select e).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene todas las dependencias para copiarlos y agregarlos a otra vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListConceptForCopyBase(validityId As Integer) As List(Of Concept) Implements IBudgetConceptRepository.GetListConceptForCopyBase
        Dim listConcept = (From e In _context.Concept.AsNoTracking Where e.BudgetaryValidityId = validityId Select e).ToList
        If listConcept.Count > 0 Then
            Return listConcept
        Else
            Return Nothing
        End If
    End Function

#End Region

End Class

