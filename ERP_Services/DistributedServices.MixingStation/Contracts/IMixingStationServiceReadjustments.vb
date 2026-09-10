Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceReadjustments

    ''' <summary>
    ''' Guarda o actualiza registro
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveReadjustmentsRepository(ListReadjustments As List(Of Readjustments), operativeUnitId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene las readecuaciones que ha tenido un paquete
    ''' </summary>
    ''' <param name="RequestPackageDetailStatusId"></param>
    ''' <param name="audit"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReadjustmentsByRequestPackageStatus(RequestPackageDetailStatusId As Integer, audit As AuditMessage, Optional tracking As Boolean = False) As List(Of Readjustments)

    ''' <summary>
    ''' Guarda una readecuación
    ''' </summary>
    ''' <param name="Readjustment"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveReadjustmentsByTechnicalConcept(Readjustment As Readjustments, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Genera los ajustes de inventario cuando no se acepta por readecuación
    ''' </summary>
    ''' <param name="ListReadjustmentsIds"></param>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GenerateInventoryAjustmenByReadjusments(ListReadjustmentsIds As List(Of Integer), operatingUnitId As Integer, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Obtiene un detalle de la solictud por id para buscar readecuaciones que hagn match
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRequestMSDToReadjustmentById(ByVal id As Integer) As ActionResult(Of RequestMixingStationDetail)
End Interface
