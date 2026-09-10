'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IInteropCostServiceGeneralExpense

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    <OperationContract()>
    Function SaveGeneralExpense(generalExpense As GeneralExpense, idSequence As Int64, audit As AuditMessage) As ActionResult(Of GeneralExpense)

    <OperationContract()>
    Function ListGeneralExpenseByStatus(status As Boolean) As List(Of GeneralExpense)

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    <OperationContract()>
    Function DeleteGeneralExpense(generalExpense As GeneralExpense, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Gets the general expense by main account identifier.
    ''' </summary>
    <OperationContract()>
    Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As GeneralExpense

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    <OperationContract()>
    Function GetGeneralExpense(code As String, audit As AuditMessage) As ActionResult(Of GeneralExpense)

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    <OperationContract()>
    Function GetGeneralExpenseById(id As Integer) As GeneralExpense

    <OperationContract()>
    Function UpdateStateGeneralExpense(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.GeneralExpense)
End Interface