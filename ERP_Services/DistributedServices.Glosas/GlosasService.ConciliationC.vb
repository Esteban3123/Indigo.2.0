'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
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
    ''' Obtiene una cabecera conciliación por código.
    ''' </summary>
    ''' <param name="Id">Id Conciliación Cabecera</param>
    ''' <returns>Objeto Cabecera Conciliación</returns>
    Public Function GetConciliationC(Id As String, session As SessionValues) As ConciliationC Implements IGlosasService.GetConciliationC
        Using conciliationC As IConciliationCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationCAdminService)()
            Return conciliationC.GetConciliationC(Id, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una cabecera conciliación por consecutivo.
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo Conciliación Cabecera</param>
    ''' <returns>Objeto Cabecera Conciliación</returns>
    Public Function GetConciliationCByConsecutive(Consecutive As String, session As SessionValues) As ConciliationC Implements IGlosasService.GetConciliationCByConsecutive
        Using conciliationC As IConciliationCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationCAdminService)()
            Return conciliationC.GetConciliationCByConsecutive(Consecutive, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una cabecera de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionC">Objeto Conciliación</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveConciliationC(ConciliacionC As ConciliationC, session As SessionValues) As Domain.Base.Entities.ActionResult(Of ConciliationC) Implements IGlosasService.SaveConciliationC
        Using conciliationC As IConciliationCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationCAdminService)()
            Return conciliationC.SaveConciliationC(ConciliacionC, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Elimina una cabecera de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionC">Objeto Conciliación</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteConciliationC(ConciliacionC As ConciliationC, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasService.DeleteConciliationC
        Using conciliationC As IConciliationCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationCAdminService)()
            Return conciliationC.DeleteConciliationC(ConciliacionC, session.AuditMessageWcf)
        End Using
    End Function

End Class
