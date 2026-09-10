'***********************************************************************
' Assembly         : DistributedServices.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/11/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface ICostServiceCostGeneralExpenseCategory

    ''' <summary>
    ''' Guarda una entidad
    ''' </summary>
    <OperationContract()>
    Function SaveCostGeneralExpenseCategory(CostGeneralExpenseCategory As Domain.Entities.CostGeneralExpenseCategory, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpenseCategory)

    ''' <summary>
    ''' Elimina una entidad
    ''' </summary>
    <OperationContract()>
    Function DeleteCostGeneralExpenseCategory(CostGeneralExpenseCategory As Domain.Entities.CostGeneralExpenseCategory, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateCostGeneralExpenseCategory(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpenseCategory)

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCostGeneralExpenseCategory(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpenseCategory)

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCostGeneralExpenseCategoryById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpenseCategory)

End Interface
