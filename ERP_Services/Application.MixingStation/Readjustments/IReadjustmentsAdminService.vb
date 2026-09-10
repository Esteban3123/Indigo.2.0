'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Giovanny Plazas L
' Created          : 2022-08-25
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IReadjustmentsAdminService
    Inherits IDisposable


    ''' <summary>
    ''' Guarda  o actualiza
    ''' </summary>
    ''' <param name="ListReadjustments"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveReadjustmentsRepository(listReadjustments As List(Of Readjustments), operativeUnitId As Integer, audit As AuditMessage) As ActionResult


    ''' <summary>
    ''' Guarda una readecuación
    ''' </summary>
    ''' <param name="Readjustment"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveReadjustmentsByTechnicalConcept(Readjustment As Readjustments, audit As AuditMessage) As ActionResult

    Function GetReadjustmentsByRequestPackageStatus(RequestPackageDetailStatusId As Integer, audit As AuditMessage, Optional tracking As Boolean = False) As List(Of Readjustments)

    ''' <summary>
    ''' Genera los ajustes de inventario cuando no se acepta por readecuación
    ''' </summary>
    ''' <param name="ListReadjustmentsIds"></param>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GenerateInventoryAjustmenByReadjusments(ListReadjustmentsIds As List(Of Integer), operatingUnitId As Integer, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' funcion para consultar un detalle de la solictud con validaciones especificas
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetRequestMSDToReadjustmentById(id As Integer) As ActionResult(Of RequestMixingStationDetail)
End Interface
