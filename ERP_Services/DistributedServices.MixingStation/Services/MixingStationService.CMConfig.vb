'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 06-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports DistributedServices.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC
Imports Microsoft.Practices.Unity
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceCMConfig

    ''' <summary>
    ''' Obtiene el medicamento de produccion por ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetMedicineProductionByID(id As Integer) As MedicinesProduction Implements IMixingStationServiceCMConfig.GetMedicineProductionByID
        Using service As ICMConfigAdminService = Container.Current.Resolve(Of ICMConfigAdminService)()
            Return service.GetMedicineProductionByID(id)
        End Using
    End Function

    ''' <summary>
    ''' Trae los parámetros de central de mezclas
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListAllCMConfig(audit As AuditMessage) As List(Of CMConfiguration) Implements IMixingStationServiceCMConfig.ListAllCMConfig
        Using service As ICMConfigAdminService = Container.Current.Resolve(Of ICMConfigAdminService)()
            Return service.ListAllCMConfig(audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda los parametros de central de mezclas
    ''' </summary>
    ''' <param name="cmConfig"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveCMConfig(cmConfig As CMConfiguration, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CMConfiguration) Implements IMixingStationServiceCMConfig.SaveCMConfig
        Using service As ICMConfigAdminService = Container.Current.Resolve(Of ICMConfigAdminService)()
            Return service.SaveCMConfig(cmConfig, audit, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza el estado de un turno
    ''' </summary>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function UpdateStateCMConfig(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CMConfiguration) Implements IMixingStationServiceCMConfig.UpdateStateCMConfig
        Using service As ICMConfigAdminService = Container.Current.Resolve(Of ICMConfigAdminService)()
            Return service.UpdateStateCMConfig(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un turno por id
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function GetCMConfig(ByVal code As String, audit As AuditMessage) As Task(Of ActionResult(Of CMConfiguration)) Implements IMixingStationServiceCMConfig.GetCMConfigAsync
        Using service As ICMConfigAdminService = Container.Current.Resolve(Of ICMConfigAdminService)()
            Return Await service.GetCMConfigAsync(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Consulta unidades funcionales de centros de atencion de una central de mezcla
    ''' </summary>
    ''' <param name="mixingStationId"></param>
    ''' <param name="listCenterLine"></param>
    ''' <returns></returns>
    Public Function ListCMCenterLineUnit(ByVal mixingStationId As Integer, ByVal listCenterLine As List(Of Tuple(Of String, Integer, Boolean))) As ActionResult(Of List(Of SP_CMCenterLineUnit_Result)) Implements IMixingStationServiceCMConfig.ListCMCenterLineUnit
        Using service As ICMConfigAdminService = Container.Current.Resolve(Of ICMConfigAdminService)()
            Return service.ListCMCenterLineUnit(mixingStationId, listCenterLine)
        End Using
    End Function

    Public Function SaveMedicinesProduction(MedicinesProduction As MedicinesProduction, audit As AuditMessage) As ActionResult(Of MedicinesProduction) Implements IMixingStationServiceCMConfig.SaveMedicinesProduction
        Using service As ICMConfigAdminService = Container.Current.Resolve(Of ICMConfigAdminService)()
            Return service.SaveMedicinesProduction(MedicinesProduction, audit)
        End Using
    End Function

    Public Function ImportMedicinesProduction(data As List(Of List(Of String)), CMConfigurationId As Integer) As ActionResult(Of List(Of SP_ImportMedicinesProduction_Result)) Implements IMixingStationServiceCMConfig.ImportMedicinesProduction
        Using service As ICMConfigAdminService = Container.Current.Resolve(Of ICMConfigAdminService)()
            Return service.ImportMedicinesProduction(data, CMConfigurationId)
        End Using
    End Function

    Public Function DeleteMedicinesProduction(listIds As List(Of Integer), TransactionalContainer As String, audit As AuditMessage) As ActionResult Implements IMixingStationServiceCMConfig.DeleteMedicinesProduction
        Using service As ICMConfigAdminService = Container.Current.Resolve(Of ICMConfigAdminService)()
            Return service.DeleteMedicinesProduction(listIds, TransactionalContainer, audit)
        End Using
    End Function
End Class
