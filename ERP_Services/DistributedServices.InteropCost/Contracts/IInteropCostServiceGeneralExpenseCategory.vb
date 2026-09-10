'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/11/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IInteropCostServiceGeneralExpenseCategory

    ''' <summary>
    ''' Guarda una entidad
    ''' </summary>
    <OperationContract()>
    Function SaveGeneralExpenseCategory(GeneralExpenseCategory As GeneralExpenseCategory, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory)

    ''' <summary>
    ''' Elimina una entidad
    ''' </summary>
    <OperationContract()>
    Function DeleteGeneralExpenseCategory(GeneralExpenseCategory As GeneralExpenseCategory, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateGeneralExpenseCategory(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory)

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetGeneralExpenseCategory(code As String, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory)

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetGeneralExpenseCategoryById(id As Integer, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory)

End Interface