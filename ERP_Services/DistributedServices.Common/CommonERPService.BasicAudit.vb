Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC
Imports Application.Common
Imports Domain.Base.Entities


Partial Class CommonERPService
    Implements ICommonERPBasicAudit

    ''' <summary>
    ''' Obtiene un registro de auditoria basica
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Registro de Auditoria Basica</returns>
    Public Function GetBasicAuditById(code As String, session As SessionValues) As Domain.Entities.BasicAudit Implements ICommonERPBasicAudit.GetBasicAuditById
        Using auditBasicAdminServiceAux As IBasicAuditAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBasicAuditAdminService)()
            Return auditBasicAdminServiceAux.GetBasicAuditById(code)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro de auditoria basica
    ''' </summary>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de Registros de Auditoria Basica</returns>
    Public Function ListAllBasicAudit(session As SessionValues) As List(Of Domain.Entities.BasicAudit) Implements ICommonERPBasicAudit.ListAllBasicAudit
        Using auditBasicAdminServiceAux As IBasicAuditAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBasicAuditAdminService)()
            Return auditBasicAdminServiceAux.ListAllBasicAudit
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro de auditoria basica
    ''' </summary>
    ''' <param name="entity">Entidad</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de Registros de Auditoria Basica</returns>
    Public Function ListBasicAuditByEntity(entity As String, session As SessionValues) As List(Of Domain.Entities.BasicAudit) Implements ICommonERPBasicAudit.ListBasicAuditByEntity
        Using auditBasicAdminServiceAux As IBasicAuditAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBasicAuditAdminService)()
            Return auditBasicAdminServiceAux.ListBasicAuditByEntity(entity)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro de auditoria basica
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdEntity">Id de la entidad</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de Registros de Auditoria Basica</returns>
    Public Function ListBasicAuditByIdIdFormAndIdEntity(IdForm As String, IdEntity As String, session As SessionValues) As List(Of Domain.Entities.BasicAudit) Implements ICommonERPBasicAudit.ListBasicAuditByIdIdFormAndIdEntity
        Using auditBasicAdminServiceAux As IBasicAuditAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBasicAuditAdminService)()
            Return auditBasicAdminServiceAux.ListBasicAuditByIdIdFormAndIdEntity(IdForm, IdEntity)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro de auditoria basica
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Registro de Auditoria Basica</returns>
    Public Function ListBasicAuditByIdUsuario(code As String, session As SessionValues) As List(Of Domain.Entities.BasicAudit) Implements ICommonERPBasicAudit.ListBasicAuditByIdUsuario
        Using auditBasicAdminServiceAux As IBasicAuditAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBasicAuditAdminService)()
            Return auditBasicAdminServiceAux.ListBasicAuditByIdUsuario(code)
        End Using
    End Function

    ''' <summary>
    ''' Función para obtener registros eliminados de auditoria basica según formulario.
    ''' </summary>
    ''' <param name="Tag">Tag del formulario</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista Auditoria Basica</returns>
    Public Function ListBasicAuditByTag(Tag As String, session As SessionValues) As List(Of Domain.Entities.BasicAudit) Implements ICommonERPBasicAudit.ListBasicAuditByTag
        Using auditBasicAdminServiceAux As IBasicAuditAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBasicAuditAdminService)()
            Return auditBasicAdminServiceAux.ListBasicAuditByTag(Tag)
        End Using
    End Function


    ''' <summary>
    ''' Función para guardar un registro de auditoria basica
    ''' </summary>
    ''' <param name="Id">Id de la entidad</param>
    ''' <param name="NameEntity">Nombre de la entidad</param>
    ''' <param name="session">Variable de sesión</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveBasicAudit(Id As String, NameEntity As String, Parameters As String, ReportName As String, ActionAudit As ActionsAudit, session As SessionValues, ByVal count As Integer) As ActionResult(Of Domain.Entities.BasicAudit) Implements ICommonERPBasicAudit.SaveBasicAudit
        Using auditBasicAdminServiceAux As IBasicAuditAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBasicAuditAdminService)()
            Return auditBasicAdminServiceAux.SaveBasicAudit(Id, NameEntity, session.IndigoCompany, session.AuditMessageWcf, ActionAudit, Parameters, ReportName, count)
        End Using
    End Function

End Class
