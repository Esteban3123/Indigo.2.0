'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractIPSService

    ''' <summary>
    ''' Guarda o Actualiza la entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveIPSService(IPSService As Domain.Entities.IPSService, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.IPSService)

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteIPSService(IPSService As Domain.Entities.IPSService, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetIPSService(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.IPSService)

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetIPSServiceById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.IPSService)
    ''' <summary>
    ''' obtiene las homologaciones del servicio IPS
    ''' </summary>
    ''' <param name="idIPSService"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetCupsHomologationByIPSServiceId(idIPSService As Integer) As List(Of CupsHomologation)
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateIPSService(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.IPSService)

    ''' <summary>
    ''' metodo para pegar en la rejilla del form de servicio ips
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function CopyAndPasteIPSService(data As List(Of List(Of String)), ServiceManual As Integer) As ActionResult(Of List(Of SurgicalProcedureService), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' obtiene los servicvios ips qx por id ips padre, opcionalmente se puede filtra por una clase en especifico
    ''' </summary>
    ''' <param name="parentId"></param>
    ''' <param name="classService"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSurgicalProcedureServiceByParentIPSId(parentId As Integer, Optional classService As EClassService = 0) As ActionResult(Of List(Of IPSService))

End Interface
