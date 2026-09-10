'***********************************************************************
' Assembly         : DistributedServices.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 15-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ICostServiceCostDistributionIntermediate

    ''' <summary>
    ''' Guarda una distribución intermedia
    ''' </summary>
    <OperationContract()>
    Function SaveDistributionIntermediate(distributionIntermediate As CostDistributionIntermediate, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate)

    ''' <summary>
    ''' Elimina una distribución intermedia
    ''' </summary>
    <OperationContract()>
    Function DeleteDistributionIntermediate(distributionIntermediate As CostDistributionIntermediate, audit As AuditMessage) As ActionResult

    ' ''' <summary>
    ' ''' Actualiza el estado del registro
    ' ''' </summary>
    <OperationContract()>
    Function UpdateStateDistributionIntermediate(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate)

    ''' <summary>
    ''' Confirma una distribución intermedia
    ''' </summary>
    <OperationContract()>
    Function ConfirmDistributionIntermediate(code As String, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate)

    ''' <summary>
    ''' Anula una distribución intermedia
    ''' </summary>
    <OperationContract()>
    Function AnnulDistributionIntermediate(code As String, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate)

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por codigo
    ''' </summary>
    <OperationContract()>
    Function GetDistributionIntermediate(code As String, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate)

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por id
    ''' </summary>
    <OperationContract()>
    Function GetDistributionIntermediateById(id As Integer) As CostDistributionIntermediate

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    <OperationContract()>
    Function ListPeriodWithDataByMaximumPeriodDistributionIntermediate(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Lista las distribuciones Intermedia por año y mes
    ''' </summary>
    <OperationContract()>
    Function ListDistributionIntermediateByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionIntermediate)

    ''' <summary>
    ''' Calcula la distribución intermedia basándose en las bases de distribución configuradas
    ''' </summary>
    <OperationContract()>
    Function CalculateDistributionIntermediate(costIntermediateDistributionId As Integer, year As Integer, month As Integer) As ActionResult(Of List(Of CostDistributionIntermediateDetail))

End Interface