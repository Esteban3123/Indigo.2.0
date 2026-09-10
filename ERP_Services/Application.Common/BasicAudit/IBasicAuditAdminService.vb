'***********************************************************************
' Assembly         : Application.Common
' Author           : Juan Diego Diaz
' Created          : 25-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities

Public Interface IBasicAuditAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función que obtiene una lista de auditoria basica.
    ''' </summary>
    ''' <returns>Lista de Auditoria Basica</returns>
    Function ListAllBasicAudit() As List(Of BasicAudit)
    ''' <summary>
    ''' Consulta un registro de auditoria especifico.
    ''' </summary>
    ''' <param name="code">El Id de la auditoria basica</param>
    ''' <returns>Objeto Auditoria Basica</returns>
    Function GetBasicAuditById(ByVal code As String) As BasicAudit
    ''' <summary>
    ''' Consulta registros de auditoria especifico.
    ''' </summary>
    ''' <param name="entity">Nombre de la entidad</param>
    ''' <returns>Lista de Auditoria Basica</returns>
    Function ListBasicAuditByEntity(ByVal entity As String) As List(Of BasicAudit)
    ''' <summary>
    ''' Consulta registros de auditoria especifico.
    ''' </summary>
    ''' <param name="code">El Id del usuario</param>
    ''' <returns>Lista de Auditoria Basica</returns>
    Function ListBasicAuditByIdUsuario(ByVal code As String) As List(Of BasicAudit)
    ''' <summary>
    ''' Función para obtener registros especificos de auditoria basica según IdForm y IdEntity
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdEntity">Id del registro</param>
    ''' <returns>Lista Auditoria Basica</returns>
    Function ListBasicAuditByIdIdFormAndIdEntity(IdForm As String, IdEntity As String) As List(Of BasicAudit)
    ''' <summary>
    ''' Función para obtener registros eliminados de auditoria basica según formulario.
    ''' </summary>
    ''' <param name="Tag">Tag del formulario</param>
    ''' <returns>Lista Auditoria Basica</returns>
    Function ListBasicAuditByTag(Tag As String) As List(Of BasicAudit)
    ''' <summary>
    ''' Función para guardar la auditoria basica
    ''' </summary>
    ''' <param name="Id">Id de la entidad</param>
    ''' <param name="NameEntity">Nombre de la entidad</param>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    Function SaveBasicAudit(Id As String, NameEntity As String, Company As String, audit As AuditMessage, ActionAudit As ActionsAudit, Optional Parameters As String = "", Optional ReportName As String = "", Optional ByVal count As Integer = 1) As ActionResult(Of BasicAudit)

End Interface
