'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class ValidityRepository
    Inherits GenericRepository(Of BudgetaryValidity)
    Implements IValidityRepository

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
    ''' Obtiene las validaciones de una entidad presupuestal
    ''' </summary>
    ''' <param name="BudgetInstitutionsId">Id de la entidad presupuestal.</param>
    ''' <returns></returns>
    Public Function GetValidityByBudgetBudgetInstitutions(BudgetInstitutionsId As String) As List(Of BudgetaryValidity) Implements IValidityRepository.GetValidityByBudgetInstitutions
        Dim validity = From e In _context.BudgetaryValidity.Include("BudgetaryEntity1")
                       Where e.BudgetaryEntityId = BudgetInstitutionsId
                       Select e
        If validity.Count > 0 Then
            Return validity.ToList
        Else
            Return New List(Of BudgetaryValidity)
        End If
    End Function

    ''' <summary>
    ''' Obtiene una validacion por Id
    ''' </summary>
    ''' <param name="Id">Id de la validacion.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetValidity(Id As String, Optional tracking As Boolean = True) As BudgetaryValidity Implements IValidityRepository.GetValidity
        Dim validity = From e In _context.BudgetaryValidity.Include("BudgetaryEntity1")
                       Where e.Id = Id
                       Select e
        If validity.Count > 0 Then
            Dim objvalidity = Nothing
            validity.SingleOrDefault().OriginalValue = (From e In _context.BudgetaryValidity.AsNoTracking
                                                        Where e.Id = Id
                                                        Select e).SingleOrDefault
            objvalidity = validity.SingleOrDefault()
            Return objvalidity
        Else
            Return New BudgetaryValidity()
        End If
    End Function

    ''' <summary>
    ''' Cerrar la vigencia de ingresos
    ''' </summary>
    ''' <param name="BudgetaryValidityId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_ClosingValidityIncome(BudgetaryValidityId As Integer, CodeUser As String) As SP_ClosingValidityIncome_Result Implements IValidityRepository.SP_ClosingValidityIncome
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ClosingValidityIncome(BudgetaryValidityId, CodeUser).SingleOrDefault()
    End Function

    ''' <summary>
    ''' Cerrar la vigencia de ingresos
    ''' </summary>
    ''' <param name="BudgetaryValidityId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_ClosingValidityExpense(BudgetaryValidityId As Integer, CodeUser As String) As SP_ClosingValidityExpense_Result Implements IValidityRepository.SP_ClosingValidityExpense
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ClosingValidityExpense(BudgetaryValidityId, CodeUser).SingleOrDefault()
    End Function

    ''' <summary>
    ''' Recalcular los valores de la vigencia de gastos
    ''' </summary>
    ''' <param name="BudgetaryValidityId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_RecalculateBalancesExpense(BudgetaryValidityId As Integer, CodeUser As String) As SP_RecalculateBalancesExpense_Result Implements IValidityRepository.SP_RecalculateBalancesExpense
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_RecalculateBalancesExpense(BudgetaryValidityId, CodeUser).SingleOrDefault()
    End Function

    ''' <summary>
    ''' Recalcular los valores de la vigencia de ingresos
    ''' </summary>
    ''' <param name="BudgetaryValidityId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_RecalculateBalancesIncome(BudgetaryValidityId As Integer, CodeUser As String) As SP_RecalculateBalancesIncome_Result Implements IValidityRepository.SP_RecalculateBalancesIncome
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_RecalculateBalancesIncome(BudgetaryValidityId, CodeUser).SingleOrDefault()
    End Function

#End Region


End Class
