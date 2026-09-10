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
Public Interface IInteropCostServiceDistributionSecondary

    ''' <summary>
    ''' Guarda una distribucion secundaria
    ''' </summary>
    <OperationContract()>
    Function SaveDistributionSecondary(distributionSecondary As DistributionSecondary, idSequence As Int64, audit As AuditMessage) As ActionResult(Of DistributionSecondary)

    ''' <summary>
    ''' Elimina una distribucion secundaria
    ''' </summary>
    <OperationContract()>
    Function DeleteDistributionSecondary(distributionSecondary As DistributionSecondary, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Actualiza una distribucion secundaria
    ''' </summary>
    <OperationContract()>
    Function UpdateStateDistributionSecondary(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionSecondary)

    ''' <summary>
    ''' Obtiene una distribucion secundaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDistributionSecondary(code As String, audit As AuditMessage) As ActionResult(Of DistributionSecondary)

    ''' <summary>
    ''' Obtiene una distribucion secundaria por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDistributionSecondaryById(id As Integer) As DistributionSecondary

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    <OperationContract()>
    Function ListPeriodWithDataByMaximumPeriodDistributionSecondary(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Lista las distribuciones secundarias por año y mes
    ''' </summary>
    <OperationContract()>
    Function ListDistributionSecondaryByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of DistributionSecondary)

    <OperationContract()>
    Function SP_CopyPasteSecondaryDistribution(data As List(Of List(Of String)), DistributionSecondaryId As Integer, InitialDistribution As Decimal) As ActionResult(Of List(Of DirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer)))

End Interface
