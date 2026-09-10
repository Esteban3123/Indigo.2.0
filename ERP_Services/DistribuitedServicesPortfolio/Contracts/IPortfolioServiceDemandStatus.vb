'***********************************************************************
' Assembly         : DistributedServices.PortfolioService
' Author           : Hector Rodriguez Rubiano
' Created          : 09-08-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface IPortfolioServiceDemandStatus

    ''' <summary>
    ''' Funcion para eliminar un estado de demanda
    ''' </summary>
    ''' <param name="DemandStatus"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteDemandStatus(DemandStatus As Domain.Entities.DemandStatus, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Función que obtiene un estado de demanda por Código
    ''' </summary>
    ''' <param name="Code">Código del estado de demanda</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetDemandStatusByCode(Code As String) As Domain.Entities.DemandStatus

    ''' <summary>
    ''' Función que obtiene todos los estados de demanda
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllDemandStatus(audit As AuditMessage) As List(Of Domain.Entities.DemandStatus)

    ''' <summary>
    ''' Función para almacenar un estado de demanda
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveDemandStatus(DemandStatus As Domain.Entities.DemandStatus, idSequense As Int64, audit As AuditMessage) As ActionResult(Of DemandStatus)

    ''' <summary>
    ''' Funcion para actualizar el estado de un estado de demanda
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeDemandStatus(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of DemandStatus)

End Interface

