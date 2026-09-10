'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 27-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

Partial Class PayrollService

    ''' <summary>
    ''' elimina un objeto parentesco
    ''' </summary>
    ''' <param name="kinship">Parentesco</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteKinship(kinship As Domain.Payroll.Entities.Kinship, session As SessionValues) As ActionMessageResult(Of Kinship) Implements IPayrollKinship.DeleteKinship
        Using kinshipAdmin As IKinshipAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IKinshipAdminService)()
            Return kinshipAdmin.DeleteKinship(kinship, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un parentesco en especifico
    ''' </summary>
    ''' <param name="code">Codigo del parentesco</param>
    ''' <returns>Parentesco</returns>
    ''' <remarks></remarks>
    Public Function GetKinship(code As String, session As SessionValues) As Domain.Payroll.Entities.Kinship Implements IPayrollKinship.GetKinship
        Using kinshipAdmin As IKinshipAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IKinshipAdminService)()
            Return kinshipAdmin.GetKinship(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los parentescos
    ''' </summary>
    ''' <returns>Parentesco</returns>
    ''' <remarks></remarks>
    Public Function ListAllKinship(session As SessionValues) As List(Of Domain.Payroll.Entities.Kinship) Implements IPayrollKinship.ListAllKinship
        Using kinshipAdmin As IKinshipAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IKinshipAdminService)()
            Return kinshipAdmin.ListAllKinship()
        End Using
    End Function

    ''' <summary>
    ''' Graba o actualiza un parentesco
    ''' </summary>
    ''' <param name="kinship">Parentesco</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveKinship(kinship As Domain.Payroll.Entities.Kinship, session As SessionValues) As Boolean Implements IPayrollKinship.SaveKinship
        Using kinshipAdmin As IKinshipAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IKinshipAdminService)()
            Return kinshipAdmin.SaveKinship(kinship, session.AuditMessageWcf)
        End Using
    End Function
End Class
