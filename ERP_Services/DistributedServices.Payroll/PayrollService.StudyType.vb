'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 27-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService
    Implements IPayrollStudyType

    ''' <summary>
    ''' Elimina un tipo de estudio
    ''' </summary>
    ''' <param name="studyType">Tipo de estudio</param>
    ''' <returns></returns>
    Public Function DeleteStudyType(studyType As Domain.Payroll.Entities.StudyType, session As SessionValues) As ActionMessageResult(Of Domain.Payroll.Entities.StudyType) Implements IPayrollStudyType.DeleteStudyType
        Using studyTypeAdmin As IStudyTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IStudyTypeAdminService)()
            Return studyTypeAdmin.DeleteStudyType(studyType, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tipo de estudio especifico
    ''' </summary>
    ''' <param name="code">Código de el tipo de estudio</param>
    ''' <returns> Tipo de estudio</returns>
    Public Function GetStudyType(code As String, session As SessionValues) As Domain.Payroll.Entities.StudyType Implements IPayrollStudyType.GetStudyType
        Using studyTypeAdmin As IStudyTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IStudyTypeAdminService)()
            Return studyTypeAdmin.GetStudyType(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los tipos de estudio
    ''' </summary>
    ''' <returns>Lista de tipos de estudio</returns>
    Public Function ListAllStudyType(session As SessionValues) As List(Of Domain.Payroll.Entities.StudyType) Implements IPayrollStudyType.ListAllStudyType
        Using studyTypeAdmin As IStudyTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IStudyTypeAdminService)()
            Return studyTypeAdmin.ListAllStudyType()
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita un tipo de estudio
    ''' </summary>
    ''' <param name="studyType">Tipo de estudio</param>
    ''' <returns></returns>
    Public Function SaveStudyType(studyType As Domain.Payroll.Entities.StudyType, session As SessionValues) As Boolean Implements IPayrollStudyType.SaveStudyType
        Using studyTypeAdmin As IStudyTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IStudyTypeAdminService)()
            Return studyTypeAdmin.SaveStudyType(studyType, session.AuditMessageWcf)
        End Using
    End Function

End Class
