'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Rafael Eduardo Patiño
' Created          : 07-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Funcion para eliminar una clase de convenios
    ''' </summary>
    ''' <param name="KindsAgreements">Obj. clase de convenio a eliminar</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteKindsAgreements(KindsAgreements As Domain.Payroll.Entities.KindsAgreements, session As SessionValues) As Domain.Base.Entities.ActionMessageResult(Of KindsAgreements) Implements IPayrollKindsAgreements.DeleteKindsAgreements
        Using KindsAgreementsAdmin As IKindsAgreementsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IKindsAgreementsAdminService)()
            Return KindsAgreementsAdmin.DeleteKindsAgreements(KindsAgreements, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Funcion para cargar una clase de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKindsAgreements(code As String, session As SessionValues) As Domain.Payroll.Entities.KindsAgreements Implements IPayrollKindsAgreements.GetKindsAgreements
        Using KindsAgreementsAdmin As IKindsAgreementsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IKindsAgreementsAdminService)()
            Return KindsAgreementsAdmin.GetKindsAgreements(code, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Funcion para guardar una clase de convenios
    ''' </summary>
    ''' <param name="KindsAgreements">Objeto clase de convenio</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveKindsAgreements(KindsAgreements As Domain.Payroll.Entities.KindsAgreements, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Payroll.Entities.KindsAgreements) Implements IPayrollKindsAgreements.SaveKindsAgreements
        Using KindsAgreementsAdmin As IKindsAgreementsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IKindsAgreementsAdminService)()
            Return KindsAgreementsAdmin.SaveKindsAgreements(KindsAgreements, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' LIsat las clases de convenios
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListKindsAgreements(session As SessionValues) As List(Of Domain.Payroll.Entities.KindsAgreements) Implements IPayrollKindsAgreements.ListKindsAgreements
        Using KindsAgreementsAdmin As IKindsAgreementsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IKindsAgreementsAdminService)()
            Return KindsAgreementsAdmin.ListKindsAgreements(session.AuditMessageWcf)
        End Using
    End Function
End Class
