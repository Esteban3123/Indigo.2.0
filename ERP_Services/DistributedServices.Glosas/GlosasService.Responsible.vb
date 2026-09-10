'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Julian Cardozo
' Created          : 06-04-2013
'
' Last Modified By : Julian Cardozo
' Last Modified On : 06-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Entities

Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region
Partial Class GlosasService

#Region "Responsible"

    Public Function DeleteResponsible(Responsible As Domain.Entities.Responsible, session As SessionValues) As ActionResult Implements IGlosasService.DeleteResponsible
        Using ResponsibleAdmin As IResponsibleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResponsibleAdminService)()
            Return ResponsibleAdmin.DeleteResponsible(Responsible, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetResponsible(codeResponsible As String, session As SessionValues) As Domain.Entities.Responsible Implements IGlosasService.GetResponsible
        Using ResponsibleAdmin As IResponsibleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResponsibleAdminService)()
            Return ResponsibleAdmin.GetResponsible(codeResponsible, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetResponsibleByCodeERPO(codeResponsible As String, session As SessionValues) As Domain.Entities.Responsible Implements IGlosasService.GetResponsibleByCodeERP
        Using ResponsibleAdmin As IResponsibleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResponsibleAdminService)()
            Return ResponsibleAdmin.GetResponsibleByCodeERP(codeResponsible, session.AuditMessageWcf)
        End Using
    End Function

    Public Function ListResponsibleAll(session As SessionValues) As List(Of Domain.Entities.ResponsibleAll) Implements IGlosasService.ListResponsibleAll
        Using ResponsibleAdmin As IResponsibleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResponsibleAdminService)()
            Return ResponsibleAdmin.ListResponsibleAll(session.AuditMessageWcf)
        End Using
    End Function

    Public Function SaveResponsible(Responsible As Domain.Entities.Responsible, session As SessionValues) As ActionResult(Of Responsible) Implements IGlosasService.SaveResponsible
        Using ResponsibleAdmin As IResponsibleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResponsibleAdminService)()
            Return ResponsibleAdmin.SaveResponsible(Responsible, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Función que retorna una lista de responsables
    ''' para reasignar 
    ''' </summary>
    ''' <param name="IdResponsible">Id Responsable</param>
    ''' <param name="session">variable sesión</param>
    ''' <returns>Lista de responsables por movimientos</returns>
    Public Function listAllResponsiblesTransfer(IdResponsible As String, session As SessionValues) As List(Of ResponsibleMovements) Implements IGlosasService.listAllResponsiblesTransfer
        Using ResponsibleAdmin As IResponsibleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResponsibleAdminService)()
            Return ResponsibleAdmin.listAllResponsiblesTransfer(IdResponsible)
        End Using
    End Function

#End Region

End Class
