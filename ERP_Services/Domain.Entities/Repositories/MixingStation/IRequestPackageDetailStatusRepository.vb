'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Duván Mejia Cortes
' Created          : 17-08-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

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
End Interface
