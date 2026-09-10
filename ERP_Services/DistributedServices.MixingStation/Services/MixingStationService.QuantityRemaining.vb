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
    Implements IMixingStationServiceQuantityRemaining

    ''' <summary>
    ''' Guarda o actualizar la tabla de sobrantes
    ''' </summary>
    ''' <param name="ListQuantityRemaining"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveQuantityRemaining(ListQuantityRemaining As List(Of QuantityRemaining), audit As AuditMessage) As ActionResult Implements IMixingStationServiceQuantityRemaining.SaveQuantityRemaining
        Using service As IQuantityRemainingAdminService = Container.Current.Resolve(Of IQuantityRemainingAdminService)()
            Return service.SaveQuantityRemaining(ListQuantityRemaining, audit)
        End Using
    End Function

    ''' <summary>
    ''' consulta todos los registros de la tabla
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllQuantityRemaining() As ActionResult(Of List(Of QuantityRemaining)) Implements IMixingStationServiceQuantityRemaining.GetAllQuantityRemaining
        Using service As IQuantityRemainingAdminService = Container.Current.Resolve(Of IQuantityRemainingAdminService)()
            Return service.GetAllQuantityRemaining()
        End Using
    End Function

    ''' <summary>
    ''' funcion para consultar la tabla de remanentes por almacen de remanente el cual se parametriza por central de mezclas
    ''' </summary>
    ''' <param name="_cMConfigurationId"></param>
    ''' <returns></returns>
    Public Function GetQuantityRemainingByMixingStation(_cMConfigurationId As Integer) As ActionResult(Of List(Of QuantityRemaining)) Implements IMixingStationServiceQuantityRemaining.GetQuantityRemainingByMixingStation
        Using service As IQuantityRemainingAdminService = Container.Current.Resolve(Of IQuantityRemainingAdminService)()
            Return service.GetQuantityRemainingByMixingStation(_cMConfigurationId)
        End Using
    End Function

End Class
