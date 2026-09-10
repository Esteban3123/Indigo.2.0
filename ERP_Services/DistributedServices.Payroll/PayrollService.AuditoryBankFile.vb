'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Obtiene un objeto de Auditoria Archivos de Bancos
    ''' </summary>
    ''' <param name="bankId">Id del Banco</param>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="PayrollDate">Fecha Nómina</param>
    ''' <returns>AuditoryBankFile</returns>
    ''' <remarks></remarks>
    Public Function GetAuditoryBankFile(bankId As String, groupId As String, PayrollDate As Date, session As SessionValues) As List(Of AuditoryBankFile) Implements IPayrollAuditoryBankFile.GetAuditoryBankFile
        Using AuditoryBankFileAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAuditoryBankFileAdminService)()
            Return AuditoryBankFileAdminService.GetAuditoryBankFile(bankId, groupId, PayrollDate)
        End Using
    End Function

    ''' <summary>
    ''' Almacena el Objeto Auditoria Archivos de Bancos
    ''' </summary>
    ''' <param name="AuditoryBankFile">Objeto AuditoryBankFile</param>
    ''' <param name="session">Variable Sesion</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveAuditoryBankFile(auditoryBankFile As AuditoryBankFile, session As SessionValues) As Boolean Implements IPayrollAuditoryBankFile.SaveAuditoryBankFile
        Using AuditoryBankFileAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAuditoryBankFileAdminService)()
            Return AuditoryBankFileAdminService.SaveAuditoryBankFile(auditoryBankFile, session.AuditMessageWcf)
        End Using
    End Function

End Class
