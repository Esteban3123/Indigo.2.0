Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceDilutionFactors

    ''' <summary>
    ''' Guarda o actualiza registro
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveDilutionFactorsRepositoryAsync(ListDilutionFactors As List(Of DilutionFactors), audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As Task(Of ActionResult)

    ''' <summary>
    ''' obtiene registro por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDilutionFactorsByCode(Code As String) As ActionResult(Of DilutionFactors)
End Interface
