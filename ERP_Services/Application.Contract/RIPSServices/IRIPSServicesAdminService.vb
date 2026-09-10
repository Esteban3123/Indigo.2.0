'************************************************************
' Assembly         : Application.Contract
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 2023-12-12
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IRIPSServicesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los servicios RIPS
    ''' </summary>
    ''' <returns>Lista de servicios RIPS</returns>
    Function ListAllRIPSServices() As List(Of RIPSServices)

    ''' <summary>
    ''' Obtiene un servicio RIPS por ID.
    ''' </summary>
    ''' <param name="id">ID del grupo de servicio</param>
    ''' <returns>Grupo de servicio RIPS</returns>
    Function GetRIPSServiceById(ByVal id As Integer) As ActionResult(Of RIPSServices)

    ''' <summary>
    ''' Obtiene un servicio RIPS por su código.
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio</param>
    ''' <param name="audit"></param>
    ''' <returns>Grupo de servicios RIPS</returns>
    Function GetRIPSServiceByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of RIPSServices)

    ''' <summary>
    ''' Guarda un servicio RIPS.
    ''' </summary>
    ''' <param name="Service">Instancia de un grupo de servicios RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="idSequense"></param>
    ''' <returns>Sservicio RIPS guardado</returns>
    Function SaveRIPSService(ByVal Service As RIPSServices, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of RIPSServices)


    'Function ChangeStateRIPSServices(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of RIPSServices)

    ''' <summary>
    ''' Elimina el registro de un servicio RIPS
    ''' </summary>
    ''' <param name="Service">Instancia de un servicio RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>ActionResult</returns>
    Function DeleteRIPSService(ByVal Service As RIPSServices, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Cambia el estado del servicio RIPS entre -activo- o -inactivo-
    ''' </summary>
    ''' <param name="code">Código del servicio RIPS</param>
    ''' <param name="state">Nuevo estado</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>Servicio RIPS guardado</returns>
    Function ChangeStateRIPSService(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of RIPSServices)


End Interface
