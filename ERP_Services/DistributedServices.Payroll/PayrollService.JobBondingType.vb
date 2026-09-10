'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

Partial Class PayrollService

    Implements IPayrollJobBondingType

    ''' <summary>
    ''' Elimina un Tipo de Vinculación Laboral
    ''' </summary>
    ''' <param name="jobBondingType">Tipo de Vinculación Laboral</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteJobBondingType(jobBondingType As Domain.Payroll.Entities.JobBondingType, session As SessionValues) As ActionMessageResult(Of JobBondingType) Implements IPayrollJobBondingType.DeleteJobBondingType
        Using jobBondingTypeAdminService As IJobBondingTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IJobBondingTypeAdminService)()
            Return jobBondingTypeAdminService.DeleteJobBondingType(jobBondingType, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Tipo de Vinculación Laboral
    ''' </summary>
    ''' <param name="code">Código del Tipo de Vinculación Laboral</param>
    ''' <returns>Tipo de Vinculación Laboral</returns>
    ''' <remarks></remarks>
    Public Function GetJobBondingType(code As String, session As SessionValues) As Domain.Payroll.Entities.JobBondingType Implements IPayrollJobBondingType.GetJobBondingType
        Using jobBondingTypeAdminService As IJobBondingTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IJobBondingTypeAdminService)()
            Return jobBondingTypeAdminService.GetJobBondingType(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los Tipos de Vinculación Laboral
    ''' </summary>
    ''' <returns>Tipos de Vinculación Laboral</returns>
    ''' <remarks></remarks>
    Public Function ListAllJobBondingType(session As SessionValues) As List(Of Domain.Payroll.Entities.JobBondingType) Implements IPayrollJobBondingType.ListAllJobBondingType
        Using jobBondingTypeAdminService As IJobBondingTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IJobBondingTypeAdminService)()
            Return jobBondingTypeAdminService.ListAllJobBondingType()
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza un Tipo de Vinculación Laboral
    ''' </summary>
    ''' <param name="jobBondingType">Tipo de Vinculación Laboral</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveJobBondingType(jobBondingType As Domain.Payroll.Entities.JobBondingType, session As SessionValues) As Boolean Implements IPayrollJobBondingType.SaveJobBondingType
        Using jobBondingTypeAdminService As IJobBondingTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IJobBondingTypeAdminService)()
            Return jobBondingTypeAdminService.SaveJobBondingType(jobBondingType, session.AuditMessageWcf)
        End Using
    End Function
End Class
