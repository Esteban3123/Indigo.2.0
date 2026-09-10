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
Public Interface IBudgetServiceBudgetInstitution
    ''' <summary>
    ''' Obtiene una entidad presupuestal por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="session">objeto de auditoria</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBudgetInstitution(code As String, audit As AuditMessage) As BudgetaryEntity

    ''' <summary>
    ''' Guarda o Actualiza una Entidad Presupuestal
    ''' </summary>
    ''' <param name="BudgetaryEntity">la entidad</param>
    ''' <param name="session">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function SaveBudgetInstitution(BudgetaryEntity As BudgetaryEntity, audit As AuditMessage) As ActionResult(Of BudgetaryEntity)

    ''' <summary>
    ''' Elimina una entidad presupuestal
    ''' </summary>
    ''' <param name="BudgetaryEntity">La entidad</param>
    ''' <param name="session">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    <OperationContract()>
    Function DeleteBudgetInstitution(BudgetaryEntity As BudgetaryEntity, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateBudgetInstitution(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BudgetaryEntity)

End Interface

