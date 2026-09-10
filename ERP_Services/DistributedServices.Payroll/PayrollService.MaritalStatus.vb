'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Antony F. Córdoba P.
' Created          : 20-12-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC
Imports Application.Billing
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
Imports Domain.Entities

Partial Public Class PayrollService
    ''' <summary>
    ''' Lista todos los estados civiles
    ''' </summary>
    ''' <returns>Listado de los estados civiles</returns>
    ''' <remarks></remarks>
    Public Function ListAllMaritalStatus(ByVal session As SessionValues) As List(Of MaritalStatus) Implements IPayrollMaritalStatus.ListAllMaritalStatus
        Using service As IMaritalStatusAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMaritalStatusAdminService)()
            Return service.ListAllMaritalStatus()
        End Using
    End Function
    ''' <summary>
    ''' Elimina un Tipo de Estado civil
    ''' </summary>
    ''' <param name="maritalStatus">Tipo de Estado civil</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMaritalStatus(ByVal maritalStatus As MaritalStatus, ByVal audit As AuditMessage, session As SessionValues) As ActionResult Implements IPayrollMaritalStatus.DeleteMaritalStatus
        Using service As IMaritalStatusAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMaritalStatusAdminService)()
            Return service.DeleteMaritalStatus(maritalStatus, audit)
        End Using
    End Function
    ''' <summary>
    ''' Graba un Tipo de Estado civil
    ''' </summary>
    ''' <param name="maritalStatus">Tipo de Estado civil</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMaritalStatus(ByVal maritalStatus As MaritalStatus, ByVal audit As AuditMessage, session As SessionValues, Optional idSequense As Long = 0) As ActionResult(Of MaritalStatus) Implements IPayrollMaritalStatus.SaveMaritalStatus
        Using service As IMaritalStatusAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMaritalStatusAdminService)()
            Return service.SaveMaritalStatus(maritalStatus, audit, idSequense)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene un Tipo de Estado civil por código
    ''' </summary>
    ''' <param name="code">Tipo de Estado civil por código</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMaritalStatusByCode(ByVal code As String, ByVal audit As AuditMessage, session As SessionValues) As ActionResult(Of MaritalStatus) Implements IPayrollMaritalStatus.GetMaritalStatusByCode
        Using service As IMaritalStatusAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMaritalStatusAdminService)()
            Return service.GetMaritalStatusByCode(code, audit)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene un Tipo de Estado civil por ID
    ''' </summary>
    ''' <param name="id">Tipo de Estado civil por ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMaritalStatusById(ByVal id As Integer, session As SessionValues) As ActionResult(Of MaritalStatus) Implements IPayrollMaritalStatus.GetMaritalStatusById
        Using service As IMaritalStatusAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMaritalStatusAdminService)()
            Return service.GetMaritalStatusById(id)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene un Tipo de Estado civil por cultura
    ''' </summary>
    ''' <param name="CultureStatus">Tipo de Estado civil segun la cultura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCultureMaritalStatus(ByVal CultureStatus As String, session As SessionValues) Implements IPayrollMaritalStatus.GetCultureMaritalStatus
        Using service As IMaritalStatusAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMaritalStatusAdminService)()
            Return service.GetCultureMaritalStatus(CultureStatus)
        End Using
    End Function
End Class
