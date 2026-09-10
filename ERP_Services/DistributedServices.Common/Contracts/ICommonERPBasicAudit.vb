'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Juan Diego Diaz Mosquera
' Created          : 25-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface ICommonERPBasicAudit

    ''' <summary>
    ''' Función que obtiene una lista de auditoria basica.
    ''' </summary>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de Auditoria Basica</returns>
    <OperationContract>
    Function ListAllBasicAudit(session As SessionValues) As List(Of BasicAudit)
    ''' <summary>
    ''' Consulta un registro de auditoria especifico.
    ''' </summary>
    ''' <param name="code">El Id de la auditoria basica</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Objeto Auditoria Basica</returns>
    <OperationContract>
    Function GetBasicAuditById(ByVal code As String, session As SessionValues) As BasicAudit
    ''' <summary>
    ''' Consulta registros de auditoria especifico.
    ''' </summary>
    ''' <param name="entity">Nombre de la entidad</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de Auditoria Basica</returns>
    <OperationContract>
    Function ListBasicAuditByEntity(ByVal entity As String, session As SessionValues) As List(Of BasicAudit)
    ''' <summary>
    ''' Consulta registros de auditoria especifico.
    ''' </summary>
    ''' <param name="code">El Id del usuario</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de Auditoria Basica</returns>
    <OperationContract>
    Function ListBasicAuditByIdUsuario(ByVal code As String, session As SessionValues) As List(Of BasicAudit)
    ''' <summary>
    ''' Función para obtener registros especificos de auditoria basica según IdForm y IdEntity
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdEntity">Id del registro</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista Auditoria Basica</returns>
    <OperationContract>
    Function ListBasicAuditByIdIdFormAndIdEntity(IdForm As String, IdEntity As String, session As SessionValues) As List(Of BasicAudit)
    ''' <summary>
    ''' Función para obtener registros eliminados de auditoria basica según formulario.
    ''' </summary>
    ''' <param name="Tag">Tag del formulario</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista Auditoria Basica</returns>
    <OperationContract>
    Function ListBasicAuditByTag(Tag As String, session As SessionValues) As List(Of BasicAudit)
    ''' <summary>
    ''' Función para guardar un registro de auditoria basica
    ''' </summary>
    ''' <param name="Id">Id de la entidad</param>
    ''' <param name="NameEntity">Nombre de la entidad</param>
    ''' <param name="session">Variable de sesión</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function SaveBasicAudit(Id As String, NameEntity As String, Parameters As String, ReportName As String, ActionAudit As ActionsAudit, session As SessionValues, ByVal count As Integer) As ActionResult(Of Domain.Entities.BasicAudit)

End Interface