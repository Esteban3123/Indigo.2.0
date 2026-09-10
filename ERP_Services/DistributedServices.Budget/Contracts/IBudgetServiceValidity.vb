'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBudgetServiceValidity

    ''' <summary>
    ''' Obtiene las validaciones de una entidad presupuestal
    ''' </summary>
    ''' <param name="BudgetEntityId">Id de la entidad presupuestal.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetValidityByBudgetEntity(BudgetEntityId As String, audit As AuditMessage) As Object

    ''' <summary>
    ''' Obtiene una validacion por Id
    ''' </summary>
    ''' <param name="Id">Id de la validacion.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetValidity(Id As String, audit As AuditMessage) As BudgetaryValidity

    ''' <summary>
    ''' Guarda o Actualiza una Vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function SaveValidity(BudgetaryValidity As BudgetaryValidity, audit As AuditMessage) As ActionResult(Of BudgetaryValidity)

    ''' <summary>
    ''' Cierra una Vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">la entidad</param>
    ''' <param name="type">Tipo 1. Ingresos 2. Gastos</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function CloseValidity(BudgetaryValidity As BudgetaryValidity, type As Integer, audit As AuditMessage) As ActionResult(Of BudgetaryValidity)

    ''' <summary>
    ''' Recalcula los valores de una Vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">la entidad</param>
    ''' <param name="type">Tipo 1. Ingresos 2. Gastos</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function RecalculateBalances(BudgetaryValidity As BudgetaryValidity, type As Integer, audit As AuditMessage) As ActionResult(Of BudgetaryValidity)

    ''' <summary>
    ''' Elimina una vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    <OperationContract()>
    Function DeleteValidity(BudgetaryValidity As BudgetaryValidity, audit As AuditMessage) As ActionResult

End Interface

