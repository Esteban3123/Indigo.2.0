'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IIPSServiceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza una IPSService
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveIPSService(ByVal IPSService As IPSService, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of IPSService)

    ''' <summary>
    ''' Elimina una IPSService
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteIPSService(ByVal IPSService As IPSService, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una IPSService por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetIPSService(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of IPSService)

    ''' <summary>
    ''' Obtiene una IPSService por id
    ''' </summary>
    ''' <returns></returns>
    Function GetIPSServiceById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of IPSService)
    ''' <summary>
    ''' obtiene las homologaciones del servicio IPS
    ''' </summary>
    ''' <param name="idIPSService"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCupsHomologationByIPSServiceId(idIPSService As Integer) As List(Of CupsHomologation)
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateIPSService(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of IPSService)

    ''' <summary>
    ''' metodo para pegar en la rejilla de servicios ips
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CopyAndPasteIPSService(data As List(Of List(Of String)), ServiceManual As Integer) As ActionResult(Of List(Of SurgicalProcedureService), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' obtiene los servicvios ips qx por id ips padre, opcionalmente se puede filtra por una clase en especifico
    ''' </summary>
    ''' <param name="parentId"></param>
    ''' <param name="classService"></param>
    ''' <returns></returns>
    Function GetSurgicalProcedureServiceByParentIPSId(parentId As Integer, Optional classService As EClassService = 0) As ActionResult(Of List(Of IPSService))
End Interface
