Imports Application.MixingStation
Imports DistributedServices.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class MixingStationService
    Implements IMixingStationServiceDilutionFactors

    ''' <summary>
    ''' guardar o actualizar
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveDilutionFactorsRepository(ListDilutionFactors As List(Of DilutionFactors), audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As Task(Of ActionResult) Implements IMixingStationServiceDilutionFactors.SaveDilutionFactorsRepositoryAsync
        Using service As IDilutionFactorsAdminService = Container.Current.Resolve(Of IDilutionFactorsAdminService)()
            Return Await service.SaveDilutionFactorsRepositoryAsync(ListDilutionFactors, audit, idSequence)
        End Using
    End Function


    ''' <summary>
    ''' obtienen el registro por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Function GetDilutionFactorsByCode(Code As String) As ActionResult(Of DilutionFactors) Implements IMixingStationServiceDilutionFactors.GetDilutionFactorsByCode
        Using service As IDilutionFactorsAdminService = Container.Current.Resolve(Of IDilutionFactorsAdminService)()
            Return service.GetDilutionFactorsByCode(Code)
        End Using
    End Function
End Class
