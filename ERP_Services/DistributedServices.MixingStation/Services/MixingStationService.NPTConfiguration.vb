'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 21-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceNPTConfiguration

    ''' <summary>
    ''' consulta todo los reistros
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListAllNPTConfiguration(audit As AuditMessage) As List(Of NPTConfiguration) Implements IMixingStationServiceNPTConfiguration.ListAllNPTConfiguration
        Using service As INPTConfigurationAdminService = Container.Current.Resolve(Of INPTConfigurationAdminService)()
            Return service.ListAllNPTConfiguration(audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda
    ''' </summary>
    ''' <param name="NPTConfiguration"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveNPTConfiguration(NPTConfiguration As NPTConfiguration, audit As AuditMessage) As ActionResult(Of List(Of NPTConfiguration)) Implements IMixingStationServiceNPTConfiguration.SaveNPTConfiguration
        Using service As INPTConfigurationAdminService = Container.Current.Resolve(Of INPTConfigurationAdminService)()
            Return service.SaveNPTConfiguration(NPTConfiguration, audit)
        End Using
    End Function

    ''' <summary>
    ''' elimina 
    ''' </summary>
    ''' <param name="ListNPTConfiguration"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteNPTConfiguration(ListNPTConfiguration As List(Of NPTConfiguration), audit As AuditMessage) As ActionResult(Of List(Of NPTConfiguration)) Implements IMixingStationServiceNPTConfiguration.DeleteNPTConfiguration
        Using service As INPTConfigurationAdminService = Container.Current.Resolve(Of INPTConfigurationAdminService)()
            Return service.DeleteNPTConfiguration(ListNPTConfiguration, audit)
        End Using
    End Function

End Class
