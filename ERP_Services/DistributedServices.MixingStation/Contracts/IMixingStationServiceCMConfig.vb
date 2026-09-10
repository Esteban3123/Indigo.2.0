'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 06/05/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceCMConfig

    ''' <summary>
    ''' Obtiene el medicamento de produccion por ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMedicineProductionByID(id As Integer) As MedicinesProduction

    ''' <summary>
    ''' Elimina los medicamentos para producción
    ''' </summary>
    ''' <param name="listIds"></param>
    ''' <param name="TransactionalContainer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMedicinesProduction(listIds As List(Of Integer), TransactionalContainer As String, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Permite la importación de medicamentos para producción
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="CMConfigurationId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportMedicinesProduction(data As List(Of List(Of String)), CMConfigurationId As Integer) As ActionResult(Of List(Of SP_ImportMedicinesProduction_Result))

    ''' <summary>
    ''' Lista todos los paràmetros de central de mezclas
    ''' </summary>
    ''' <returns>Lista de turnos</returns>
    <OperationContract()>
    Function ListAllCMConfig(audit As AuditMessage) As List(Of CMConfiguration)

    ''' <summary>
    ''' Guarda cel centro de mezclas
    ''' </summary>
    ''' <param name="cmConfig">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function SaveCMConfig(ByVal cmConfig As CMConfiguration, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CMConfiguration)

    ''' <summary>
    ''' Guarda o Actualiza una entidad
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMedicinesProduction(ByVal MedicinesProduction As MedicinesProduction, ByVal audit As AuditMessage) As ActionResult(Of MedicinesProduction)

    ''' <summary>
    ''' Actualiza los parámetros de central de mezclas
    ''' </summary>
    ''' <param name="state">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function UpdateStateCMConfig(ByVal code As String, ByVal state As Boolean, audit As AuditMessage) As ActionResult(Of CMConfiguration)

    ''' <summary>
    ''' Obtiene los parámetros de central de mezclas por id
    ''' </summary>
    <OperationContract()>
    Function GetCMConfigAsync(ByVal code As String, audit As AuditMessage) As Task(Of ActionResult(Of CMConfiguration))

    ''' <summary>
    ''' Consulta unidades funcionales de centros de atencion de una central de mezcla
    ''' </summary>
    ''' <param name="mixingStationId"></param>
    ''' <param name="listCenterLine"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListCMCenterLineUnit(ByVal mixingStationId As Integer, ByVal listCenterLine As List(Of Tuple(Of String, Integer, Boolean))) As ActionResult(Of List(Of SP_CMCenterLineUnit_Result))

End Interface
