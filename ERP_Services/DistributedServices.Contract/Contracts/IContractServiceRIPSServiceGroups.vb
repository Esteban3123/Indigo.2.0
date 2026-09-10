'************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 2023-12-04
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

<ServiceContract()>
Public Interface IContractServiceRIPSServiceGroups
    ''' <summary>
    ''' Lista todos los grupos de servicios RIPS
    ''' </summary>
    ''' <returns>Lista con grupos de servicios RIPS</returns>
    <OperationContract()>
    Function ListAllRIPSServiceGroups() As List(Of RIPSServiceGroups)

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por ID.
    ''' </summary>
    ''' <param name="id">ID del grupo de servicio</param>
    ''' <returns>Grupo de servicio RIPS</returns>
    <OperationContract()>
    Function GetRIPSServiceGroupById(id As Integer) As ActionResult(Of RIPSServiceGroups)

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por su código.
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio</param>
    ''' <param name="audit"></param>
    ''' <returns>Grupo de servicios RIPS</returns>
    <OperationContract()>
    Function GetRIPSServiceGroupByCode(code As String, audit As AuditMessage) As ActionResult(Of RIPSServiceGroups)

    ''' <summary>
    ''' Guarda un grupo de servicios RIPS.
    ''' </summary>
    ''' <param name="ServiceGroup">Instancia de un grupo de servicios RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="idSequense"></param>
    ''' <returns>Grupo de servicio RIPS guardado</returns>
    <OperationContract()>
    Function SaveRIPSServiceGroup(serviceGroup As RIPSServiceGroups, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of RIPSServiceGroups)

    ''' <summary>
    ''' Cambia el estado del grupo de servicio RIPS entre -activo- o -inactivo-
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio RIPS</param>
    ''' <param name="state">Nuevo estado</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>Grupo de servicio RIPS guardado</returns>
    <OperationContract()>
    Function ChangeStateRIPSServiceGroup(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of RIPSServiceGroups)

    ''' <summary>
    ''' Elimina el registro de un grupo de servicios RIPS
    ''' </summary>
    ''' <param name="ServiceGroup">Instancia de un grupo de servicios RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function DeleteRIPSServiceGroup(serviceGroup As RIPSServiceGroups, audit As AuditMessage) As ActionResult


End Interface
