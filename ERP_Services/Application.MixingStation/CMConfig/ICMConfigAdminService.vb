'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 06-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ICMConfigAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene el medicamento de produccion por ID
    ''' </summary>
    Function GetMedicineProductionByID(id As Integer) As MedicinesProduction
    ''' <summary>
    ''' Elimina los medicamentos para producción
    ''' </summary>
    ''' <param name="listIds"></param>
    ''' <param name="TransactionalContainer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function DeleteMedicinesProduction(listIds As List(Of Integer), TransactionalContainer As String, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Permite la importación de medicamentos para producción
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="CMConfigurationId"></param>
    ''' <returns></returns>
    Function ImportMedicinesProduction(data As List(Of List(Of String)), CMConfigurationId As Integer) As ActionResult(Of List(Of SP_ImportMedicinesProduction_Result))

    ''' <summary>
    ''' Guarda o Actualiza una entidad
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveMedicinesProduction(ByVal MedicinesProduction As MedicinesProduction, ByVal audit As AuditMessage) As ActionResult(Of MedicinesProduction)

    ''' <summary>
    ''' Lista todos los parámetros de configuración de central de mezclas
    ''' </summary>
    ''' <returns>Lista de turnos</returns>
    Function ListAllCMConfig(audit As AuditMessage) As List(Of CMConfiguration)

    ''' <summary>
    ''' Guarda la central de mezclas
    ''' </summary>
    ''' <param name="cmConfig">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function SaveCMConfig(ByVal cmConfig As CMConfiguration, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CMConfiguration)

    ''' <summary>
    ''' Actualiza los parámetros de configuración de central de mezclas
    ''' </summary>
    ''' <param name="state">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function UpdateStateCMConfig(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CMConfiguration)

    ''' <summary>
    ''' Obtiene los parámetros de central de mezclas por Id
    ''' </summary>
    ''' <param name="audit">The identifier.</param>
    Function GetCMConfigAsync(ByVal code As String, ByVal audit As AuditMessage) As Task(Of ActionResult(Of CMConfiguration))

    ''' <summary>
    ''' Consulta unidades funcionales de centros de atencion de una central de mezcla
    ''' </summary>
    ''' <param name="mixingStationId"></param>
    ''' <param name="listCenterLine"></param>
    ''' <returns></returns>
    Function ListCMCenterLineUnit(ByVal mixingStationId As Integer, ByVal listCenterLine As List(Of Tuple(Of String, Integer, Boolean))) As ActionResult(Of List(Of SP_CMCenterLineUnit_Result))

End Interface