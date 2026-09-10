'************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 2023-12-12
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IContractServiceRIPSServices

    ''' <summary>
    ''' Lista todos los servicios RIPS
    ''' </summary>
    ''' <returns>Lista de servicios RIPS</returns>
    <OperationContract()>
    Function ListAllRIPSServices() As List(Of RIPSServices)

    ''' <summary>
    ''' Obtiene un servicio RIPS por ID.
    ''' </summary>
    ''' <param name="id">ID del servicio</param>
    ''' <returns>Servicio RIPS</returns>
    <OperationContract()>
    Function GetRIPSServiceById(id As Integer) As ActionResult(Of RIPSServices)

    ''' <summary>
    ''' Obtiene un servicio RIPS por su código.
    ''' </summary>
    ''' <param name="code">Código del servicio</param>
    ''' <param name="audit"></param>
    ''' <returns>Servicios RIPS</returns>
    <OperationContract()>
    Function GetRIPSServiceByCode(code As String, audit As AuditMessage) As ActionResult(Of RIPSServices)

    ''' <summary>
    ''' Guarda un servicios RIPS.
    ''' </summary>
    ''' <param name="Service">Instancia de un servicio RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="idSequense"></param>
    ''' <returns>Servicio RIPS guardado</returns>
    <OperationContract()>
    Function SaveRIPSService(Service As RIPSServices, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of RIPSServices)

    ''' <summary>
    ''' Elimina el registro de un servicios RIPS
    ''' </summary>
    ''' <param name="Service">Instancia de un servicio RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function DeleteRIPSService(Service As RIPSServices, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Cambia el estado del servicio RIPS entre -activo- o -inactivo-
    ''' </summary>
    ''' <param name="code">Código del servicio RIPS</param>
    ''' <param name="state">Nuevo estado</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>Servicio RIPS guardado</returns>
    <OperationContract()>
    Function ChangeStateRIPSService(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of RIPSServices)

End Interface
