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
Public Interface ICostServiceCostGeneralExpense

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    <OperationContract()>
    Function SaveGeneralExpense(generalExpense As Domain.Entities.CostGeneralExpense, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpense)

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    <OperationContract()>
    Function DeleteGeneralExpense(generalExpense As Domain.Entities.CostGeneralExpense, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Gets the general expense by main account identifier.
    ''' </summary>
    <OperationContract()>
    Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As CostGeneralExpense

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    <OperationContract()>
    Function GetGeneralExpense(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpense)

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    <OperationContract()>
    Function GetGeneralExpenseById(id As Integer) As CostGeneralExpense

    ''' <summary>
    ''' Actualiza el estado
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function UpdateStateCostGeneralExpense(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpense)

    ''' <summary>
    ''' Importa detalles al elemento de costo
    ''' </summary>
    ''' <param name="DistributionType"></param>
    ''' <param name="MeasurementUnit"></param>
    ''' <param name="ListDistributionBaseDetail"></param>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportDetailsToCostDistributionBase(DistributionType As Byte, MeasurementUnit As Byte, ListDistributionBaseDetail As List(Of CostDistributionBaseDetail), Data As List(Of List(Of String))) As ActionResult(Of List(Of CostDistributionBaseDetail))

End Interface
