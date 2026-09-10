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
Public Interface IInteropCostServiceDirectDistributionSecondary
    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDirectDistributionSecondary(code As String, audit As AuditMessage) As Domain.Entities.DirectDistributionSecondary

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDirectDistributionSecondaryById(id As Integer) As DirectDistributionSecondary

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    <OperationContract()>
    Function SaveDirectDistributionSecondary(DirectDistributionSecondary As Domain.Entities.DirectDistributionSecondary, ListUpdateIds As List(Of Integer), idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DirectDistributionSecondary)

    ''' <summary>
    ''' Obtiene el producido de los centros de produccion logisticos
    ''' </summary>
    ''' <param name="ProductionCenterId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetDataImportLogisticProductionCenterById(ByVal ProductionCenterId As Integer) As ActionResult(Of List(Of LogisticsProductionCenterRecordDetail))

End Interface
