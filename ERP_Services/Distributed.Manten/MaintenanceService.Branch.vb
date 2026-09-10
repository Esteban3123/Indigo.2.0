Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class MaintanceService

    Public Function DeleteBranch(Empresa As String, Branch As Domain.Maintenance.Entities.Branch, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IBranchService.DeleteBranch
        Using BranchAdmin As IBranchAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IBranchAdminService)()
            Return BranchAdmin.DeleteBranch(Branch, audit)
        End Using
    End Function

    Public Function GetBranch(Empresa As String, codeBranch As String) As Domain.Maintenance.Entities.Branch Implements IBranchService.GetBranch
        Using BranchAdmin As IBranchAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IBranchAdminService)()
            Return BranchAdmin.GetBranch(codeBranch)
        End Using
    End Function

    Public Function ListAllBranch(Empresa As String) As List(Of Domain.Maintenance.Entities.Branch) Implements IBranchService.ListAllBranch
        Using BranchAdmin As IBranchAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IBranchAdminService)()
            Return BranchAdmin.ListAllBranch()
        End Using
    End Function

    Public Function SaveBranch(Branch As Domain.Maintenance.Entities.Branch, session As SessionValues) As ActionResult(Of Domain.Maintenance.Entities.Branch) Implements IBranchService.SaveBranch
        Using AccesoryAdmin As IBranchAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBranchAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return AccesoryAdmin.SaveBranch(Branch, audit, idSequense)
        End Using
    End Function

    'Public Function ListAllCostCenter(Empresa As String) As List(Of Domain.Maintenance.Entities.CostCenter) Implements IBranchService.ListAllCostCenter
    '    Using BranchAdmin As IBranchAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IBranchAdminService)()
    '    Return BranchAdmin.ListAllCostCenter
    'End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function Change_StateBranch(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Maintenance.Entities.Branch) Implements IBranchService.Change_StateBranch
        Using BranchAdmin As IBranchAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBranchAdminService)()
            Return BranchAdmin.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
