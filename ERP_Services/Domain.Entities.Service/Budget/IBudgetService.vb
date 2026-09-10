'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Carlos Ernesto Cordoba
' Created          : 24-11-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IBudgetService
    Inherits IDisposable
    ''' <summary>
    ''' metodo para validar el periodo de presupuesto
    ''' </summary>
    ''' <param name="validityId"></param>
    ''' <param name="documentDate"></param>
    ''' <param name="budgetType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateBudgetPeriod(validityId As Integer, documentDate As DateTime, budgetType As EBudgetType) As ActionResult

    ''' <summary>
    ''' Metodo para validar los datos de presupuesto traslado
    ''' </summary>
    ''' <param name="budgetTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateBudgetTransfer(budgetTransfer As BudgetTransfer) As String

    ''' <summary>
    ''' Metodo para validar los datos de la modificacion del pac
    ''' </summary>
    ''' <param name="annualizedCashFlowModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidatePACModification(annualizedCashFlowModification As AnnualizedCashFlowModification) As String

    ''' <summary>
    ''' Metodo para validar los datos de un traslado del pac
    ''' </summary>
    ''' <param name="annualizedCashFlowTrasnfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidatePACTransfer(annualizedCashFlowTrasnfer As AnnualizedCashFlowTransfer) As String

    ''' <summary>
    ''' Metodo para validar los datos del compromiso
    ''' </summary>
    ''' <param name="commitment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateCommitment(commitment As Commitment) As String

    ''' <summary>
    ''' Metodo para validar los datos de una suspension presupuestal
    ''' </summary>
    ''' <param name="suspension"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateSuspension(suspension As Suspension) As String

    ''' <summary>
    ''' Metodo para validar los datos de un levantamiento de suspencion presupuestal
    ''' </summary>
    ''' <param name="suspensionCancellation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateSuspensionCancellation(suspensionCancellation As SuspensionCancellation) As String

End Interface
