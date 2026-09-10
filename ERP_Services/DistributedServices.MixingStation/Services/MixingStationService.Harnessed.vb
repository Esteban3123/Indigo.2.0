'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Giovanny Plazas
' Created          : 18-09-2021
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
    Implements IMixingStationServiceHarnessed

    ''' <summary>
    ''' Guarda o actualizar la tabla de aprovehcamientos
    ''' </summary>
    ''' <param name="ListHarnessed"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveHarnessed(ListHarnessed As List(Of Harnessed), audit As AuditMessage) As ActionResult Implements IMixingStationServiceHarnessed.SaveHarnessed
        Using service As IHarnessedAdminService = Container.Current.Resolve(Of IHarnessedAdminService)()
            Return service.SaveHarnessed(ListHarnessed, audit)
        End Using
    End Function

    ''' <summary>
    ''' consulta todos los registros de la tabla
    ''' </summary>
    ''' <returns></returns>
    Public Function RegisterHarnessed(ListQuantityRemaining As List(Of QuantityRemaining), CampaignDetailId As Integer, audit As AuditMessage) As ActionResult Implements IMixingStationServiceHarnessed.RegisterHarnessed
        Using service As IHarnessedAdminService = Container.Current.Resolve(Of IHarnessedAdminService)()
            Return service.RegisterHarnessed(ListQuantityRemaining, CampaignDetailId, audit)
        End Using
    End Function

End Class
