'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Duván Mejia Cortes
' Created          : 17-08-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Collections.Generic
Imports Domain.Base

Public Interface IRequestPackageDetailStatusRepository
    Inherits IRepository(Of RequestPackageDetailStatus), Inject

    ''' <summary>
    ''' Obtiene una lista, Filtrando Por Ids
    ''' </summary>
    ''' <param name="ids">The identifier.</param>
    ''' <returns></returns>
    Function GetListPackageDetailStatus(ids As List(Of Integer), status As Byte?) As List(Of RequestPackageDetailStatus)

    ''' <summary>
    ''' Entre los IDs indicados, devuelve el conjunto de los que tienen al menos un ítem de clasificación marcado
    ''' en producción o calidad y cuyo <see cref="DefectClassificationItem"/> es crítico.
    ''' Evita cargar el grafo completo de clasificación por cada estado (uso masivo, p. ej. control de calidad).
    ''' </summary>
    ''' <param name="ids">IDs de <see cref="RequestPackageDetailStatus"/> a evaluar.</param>
    Function GetRequestPackageDetailStatusIdsWithCriticalDefect(ids As List(Of Integer)) As HashSet(Of Integer)

    ''' <summary>
    ''' UPDATE directo: aplica el estado de liberación evaluando defecto crítico en la misma sentencia SQL.
    ''' Los ítems con defecto crítico activo quedan en Status=5 / QualityStatus=2; el resto recibe releaseStatus / QualityStatus=1.
    ''' </summary>
    Function BulkUpdateQualityRelease(ids As List(Of Integer), releaseStatus As Byte) As Integer

    ''' <summary>
    ''' UPDATE directo: aplica status y qualityStatus a todos los IDs indicados sin cargar entidades.
    ''' </summary>
    Function BulkUpdateStatus(ids As List(Of Integer), status As Byte, qualityStatus As Byte) As Integer

    ''' <summary>
    ''' Obtiene un objeto de la entidad
    ''' </summary>
    ''' <param name="RequestPackageDetailStatusId"></param>
    ''' <returns></returns>
    Function GetRequestPackageDetailStatusById(RequestPackageDetailStatusId As Integer) As RequestPackageDetailStatus

    ''' <summary>
    ''' Lista de chequeo de la clasificacion de defectos por detail status
    ''' </summary>
    ''' <param name="requestPackageDetailStatusIds"></param>
    ''' <returns></returns>
    Function GetRequestPackageDetailStatusDefectClassification(requestPackageDetailStatusIds As List(Of Integer), unitDoseClass As Integer, Optional Form As Byte = 0) As List(Of DefectClassificationModel)

    ''' <summary>
    ''' Obtiene los agregados de la solicitud
    ''' </summary>
    ''' <param name="requestPackageDetailStatusId"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Function GetPackageDetailStatusRequestInformation(requestPackageDetailStatusId As Integer, Optional tracking As Boolean = True) As RequestPackageDetailStatus

    ''' <summary>
    ''' Obtiene la materia prima validada usada para preparar un medicamento complementario.
    ''' </summary>
    Function GetComplementaryRawMaterialByBatchProductAndAtc(batchCode As String, packageProductId As Integer, atcId As Integer) As CampaignRawMaterial
End Interface
