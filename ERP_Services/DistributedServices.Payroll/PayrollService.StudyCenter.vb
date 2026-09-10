'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

Partial Class PayrollService

    Implements IPayrollStudyCenter

    ''' <summary>
    ''' Elimina un Centro de Estudio
    ''' </summary>
    ''' <param name="studyCenter">Centro de Estudio</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteStudyCenter(studyCenter As Domain.Payroll.Entities.StudyCenter, session As SessionValues) As ActionMessageResult(Of StudyCenter) Implements IPayrollStudyCenter.DeleteStudyCenter
        Using studyCenterAdmin As IStudyCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IStudyCenterAdminService)()
            Return studyCenterAdmin.DeleteStudyCenter(studyCenter, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Centro de Estudio
    ''' </summary>
    ''' <param name="code">Código del Centro de Estudio</param>
    ''' <returns>Centro de Estudio</returns>
    ''' <remarks></remarks>
    Public Function GetStudyCenter(code As String, session As SessionValues) As Domain.Payroll.Entities.StudyCenter Implements IPayrollStudyCenter.GetStudyCenter
        Using studyCenterAdmin As IStudyCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IStudyCenterAdminService)()
            Return studyCenterAdmin.GetStudyCenter(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista Todos los Centros de Estudio
    ''' </summary>
    ''' <returns>Centros de Estudio</returns>
    ''' <remarks></remarks>
    Public Function ListAllStudyCenter(session As SessionValues) As List(Of Domain.Payroll.Entities.StudyCenter) Implements IPayrollStudyCenter.ListAllStudyCenter
        Using studyCenterAdmin As IStudyCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IStudyCenterAdminService)()
            Return studyCenterAdmin.ListAllStudyCenter()
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza un Centro de Estudio
    ''' </summary>
    ''' <param name="studyCenter">Centro de Estudio</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveStudyCenter(studyCenter As Domain.Payroll.Entities.StudyCenter, session As SessionValues) As Boolean Implements IPayrollStudyCenter.SaveStudyCenter
        Using studyCenterAdmin As IStudyCenterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IStudyCenterAdminService)()
            Return studyCenterAdmin.SaveStudyCenter(studyCenter, session.AuditMessageWcf)
        End Using
    End Function
End Class
