'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ICostServiceCostDistributionDirectCost

    ''' <summary>
    ''' Guarda un gasto directo
    ''' </summary>
    <OperationContract()>
    Function SaveDistributionDirectCost(distributionDirectCost As Domain.Entities.CostDistributionDirectCost, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionDirectCost)

    ''' <summary>
    ''' Elimina un gasto directo
    ''' </summary>
    <OperationContract()>
    Function DeleteDistributionDirectCost(distributionDirectCost As Domain.Entities.CostDistributionDirectCost, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ' ''' <summary>
    ' ''' Actualiza el estado
    ' ''' </summary>
    <OperationContract()>
    Function UpdateStateDistributionDirectCost(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionDirectCost)

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    <OperationContract()>
    Function GetDistributionDirectCost(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionDirectCost)

    ''' <summary>
    ''' Obtiene un gasto directo por id
    ''' </summary>
    <OperationContract()>
    Function GetDistributionDirectCostById(id As Integer) As CostDistributionDirectCost

    ''' <summary>
    ''' Lista los gastos directos por año y mes
    ''' </summary>
    <OperationContract()>
    Function ListDistributionDirectCostByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionDirectCost)

    ''' <summary>
    ''' Obtiene el valor contable por contenedor, número de cuenta contable, año y mes
    ''' </summary>
    <OperationContract()>
    Function GetMainAccountValueByNumberAccountYearAndMotn(ByVal mainAccountId As Integer, ByVal year As String, ByVal month As Integer) As Decimal

    <OperationContract()>
    Function CalculateCostDistribution(CostGeneralExpenseId As Integer, ByVal value As Decimal, ByVal year As Int32, ByVal month As Int32, ByVal containerName As String) As ActionResult(Of List(Of CostDistributionDirectCostDetail))

    ''' <summary>
    ''' Desconfirma la distribución del elemento del costo
    ''' </summary>
    <OperationContract()>
    Function DisconfirmDistributionDirectCost(distributionDirectCost As Domain.Entities.CostDistributionDirectCost, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionDirectCost)

    ''' <summary>
    ''' Reversa un documento de provision
    ''' </summary>
    <OperationContract()>
    Function ReverseProvisionDocument(distributionDirectCostId As Integer, audit As AuditMessage) As ActionResult(Of CostDistributionDirectCost)

    ''' <summary>
    ''' Copy And Paste Detalles de la distribucion de elementos del costo
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function CopyAndPasteCostDistributionDirectCostDetail(GeneralExpenseId As Integer, data As List(Of List(Of String))) As ActionResult(Of List(Of CostDistributionDirectCostDetail))

End Interface