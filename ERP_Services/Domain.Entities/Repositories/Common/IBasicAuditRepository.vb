'************************************************************
' Assembly         : Domain.Common
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Common.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio Auditoria Basica.
''' </summary>
''' <remarks></remarks>
Public Interface IBasicAuditRepository
    Inherits IRepository(Of BasicAudit)

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
    ''' <param name="entity">Tag del formulario</param>
    ''' <returns>Lista Auditoria Basica</returns>
    Function ListBasicAuditByTag(entity As String) As List(Of BasicAudit)

End Interface
