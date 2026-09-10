Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class MixingStationService
    Implements IMixingStationServiceReadjustments

    ''' <summary>
    ''' guardar o actualizar
    ''' </summary>
    ''' <returns></returns>
    Public Function SaveReadjustmentsRepository(ListReadjustments As List(Of Readjustments), operativeUnitId As Integer, audit As AuditMessage) As ActionResult Implements IMixingStationServiceReadjustments.SaveReadjustmentsRepository
        Using service As IReadjustmentsAdminService = Container.Current.Resolve(Of IReadjustmentsAdminService)()
            Return service.SaveReadjustmentsRepository(ListReadjustments, operativeUnitId, audit)
        End Using
    End Function


    ''' <summary>
    ''' Obtiene las readecuaciones que ha tenido un paquete
    ''' </summary>
    ''' <param name="RequestPackageDetailStatusId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetReadjustmentsByRequestPackageStatus(RequestPackageDetailStatusId As Integer, audit As AuditMessage, Optional tracking As Boolean = False) As List(Of Readjustments) Implements IMixingStationServiceReadjustments.GetReadjustmentsByRequestPackageStatus
        Using service As IReadjustmentsAdminService = Container.Current.Resolve(Of IReadjustmentsAdminService)()
            Return service.GetReadjustmentsByRequestPackageStatus(RequestPackageDetailStatusId, audit, tracking)
        End Using
    End Function

    Public Function SaveReadjustmentsByTechnicalConcept(Readjustment As Readjustments, audit As AuditMessage) As ActionResult Implements IMixingStationServiceReadjustments.SaveReadjustmentsByTechnicalConcept
        Using service As IReadjustmentsAdminService = Container.Current.Resolve(Of IReadjustmentsAdminService)()
            Return service.SaveReadjustmentsByTechnicalConcept(Readjustment, audit)
        End Using
    End Function

    Public Async Function GenerateInventoryAjustmenByReadjusments(ListReadjustmentsIds As List(Of Integer), operatingUnitId As Integer, audit As AuditMessage) As Task(Of ActionResult) Implements IMixingStationServiceReadjustments.GenerateInventoryAjustmenByReadjusments
        Using service As IReadjustmentsAdminService = Container.Current.Resolve(Of IReadjustmentsAdminService)()
            Return Await service.GenerateInventoryAjustmenByReadjusments(ListReadjustmentsIds, operatingUnitId, audit)
        End Using
    End Function


    ''' <summary>
    ''' obtiene un detalle de la solictud por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetRequestMSDToReadjustmentById(id As Integer) As ActionResult(Of RequestMixingStationDetail) Implements IMixingStationServiceReadjustments.GetRequestMSDToReadjustmentById
        Using service As IReadjustmentsAdminService = Container.Current.Resolve(Of IReadjustmentsAdminService)()
            Return service.GetRequestMSDToReadjustmentById(id)
        End Using
    End Function

End Class
