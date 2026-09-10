
#Region "Imports"
Imports Application.Glosas
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

#End Region

Partial Class GlosasService


    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    '''<param name="code">Id del ImportunityCauses</param>
    ''' <returns></returns>
    Public Function GetImportunityCausesByCode(code As String, session As SessionValues) As ActionResult(Of ImportunityCauses) Implements IGlosasImportunityCauses.GetImportunityCausesByCode
        Using service As IImportunityCausesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IImportunityCausesAdminService)()
            Return service.GetImportunityCausesByCode(code, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    '''<param name="Id">Código del Rubro</param>
    ''' <returns></returns>
    Public Function GetImportunityCausesById(Id As Integer, session As SessionValues) As ActionResult(Of ImportunityCauses) Implements IGlosasImportunityCauses.GetImportunityCausesById
        Using service As IImportunityCausesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IImportunityCausesAdminService)()
            Return service.GetImportunityCausesById(Id, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="ImportunityCauses">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveImportunityCauses(ImportunityCauses As ImportunityCauses, session As SessionValues, idSequence As Int64) As ActionResult(Of ImportunityCauses) Implements IGlosasImportunityCauses.SaveImportunityCauses
        Using service As IImportunityCausesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IImportunityCausesAdminService)()
            Return service.SaveImportunityCauses(ImportunityCauses, session.AuditMessageWcf, idSequence)
        End Using
    End Function
    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateImportunityCauses(code As String, state As Boolean, session As SessionValues) As ActionResult(Of ImportunityCauses) Implements IGlosasImportunityCauses.ChangeStateImportunityCauses
        Using service As IImportunityCausesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IImportunityCausesAdminService)()
            Return service.ChangeStateImportunityCauses(code, state, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Elimina  un registro
    ''' </summary>
    ''' <param name="ImportunityCauses">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function DeleteImportunityCauses(ImportunityCauses As ImportunityCauses, session As SessionValues) As ActionResult(Of ImportunityCauses) Implements IGlosasImportunityCauses.DeleteImportunityCauses
        Using service As IImportunityCausesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IImportunityCausesAdminService)()
            Return service.DeleteImportunityCauses(ImportunityCauses, session.AuditMessageWcf)
        End Using
    End Function


End Class
