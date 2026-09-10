'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Juan Diego Diaz
' Created          : 12-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Entities

Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base

#End Region

Partial Class GlosasService

    ''' <summary>
    ''' Elimina una cabecera de devolución.
    ''' </summary>
    ''' <param name="DevolutionC">Objeto devolución</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteDevolutionC(DevolutionC As GlosaDevolutionsReceptionC, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasService.DeleteDevolutionC
        Using devolution As IDevolutionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionCAdminService)()
            Return devolution.DeleteDevolutionC(DevolutionC, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una cabecera devolución por código.
    ''' </summary>
    ''' <param name="Id">Id Devolución Cabecera</param>
    ''' <returns>Objeto Cabecera Devolución</returns>
    Public Function GetDevolutionC(Id As String, session As SessionValues) As GlosaDevolutionsReceptionC Implements IGlosasService.GetDevolutionC
        Using devolution As IDevolutionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionCAdminService)()
            Return devolution.GetDevolutionnC(Id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una cabecera devolución por consecutivo.
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo Devolución Cabecera</param>
    ''' <returns>Objeto Cabecera Devolución</returns>
    Public Function GetDevolutionCByConsecutive(Consecutive As String, session As SessionValues) As GlosaDevolutionsReceptionC Implements IGlosasService.GetDevolutionCByConsecutive

        Using devolution As IDevolutionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionCAdminService)()
            Return devolution.GetDevolutionCByConsecutive(Consecutive)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para listar todas las cabeceras de devoluciones.
    ''' </summary>
    ''' <returns>Lista de cabeceras devoluciones</returns>
    Public Function ListAllDevolutionC(session As SessionValues) As List(Of GlosaDevolutionsReceptionC) Implements IGlosasService.ListAllDevolutionC
        Using devolution As IDevolutionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionCAdminService)()
            Return devolution.ListAllDevolutionC()
        End Using
    End Function

    ''' <summary>
    ''' Guarda una cabecera de devolución.
    ''' </summary>
    ''' <param name="DevolutionC">Objeto Devolución</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveDevolutionC(DevolutionC As GlosaDevolutionsReceptionC, ListDevolutionD As List(Of GlosaDevolutionsReceptionD), session As SessionValues) As Domain.Base.Entities.ActionResult(Of GlosaDevolutionsReceptionC) Implements IGlosasService.SaveDevolutionC
        Using devolution As IDevolutionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionCAdminService)()
            Return devolution.SaveDevolutionC(DevolutionC, ListDevolutionD, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Confirma la devolución
    ''' </summary>
    ''' <param name="DevolutionC">Objeto Devolución Cabecera</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Action Result</returns>
    Public Function ConfirmConciliationC(DevolutionC As GlosaDevolutionsReceptionC, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasService.ConfirmDevolutionC
        Using devolution As IDevolutionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionCAdminService)()
            Return devolution.ConfirmDevolutionC(DevolutionC, session.AuditMessageWcf)
        End Using
    End Function


End Class
