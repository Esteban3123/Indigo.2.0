'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Juan Carlos Bermudez
' Created          : 16/07/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.IOC

Partial Class PayrollService

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function DeleteBlockRecordPayroll(blockRecordPayroll As BlockRecordPayroll, session As SessionValues) As ActionResult Implements IPayrollBlockRecordPayroll.DeleteBlockRecordPayroll
        Using blockRecordPayrollAdminService As IBlockRecordPayrollAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBlockRecordPayrollAdminService)()
            Return blockRecordPayrollAdminService.DeleteBlockRecordPayroll(blockRecordPayroll)
        End Using
    End Function

    ''' <summary>
    ''' Gets the block record treasury by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Function GetBlockRecordPayrollByIdformAndIdRecord(IdForm As String, IdRecord As String, session As SessionValues) As BlockRecordPayroll Implements IPayrollBlockRecordPayroll.GetBlockRecordPayrollByIdformAndIdRecord
        Using blockRecordPayrollAdminService As IBlockRecordPayrollAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBlockRecordPayrollAdminService)()
            Return blockRecordPayrollAdminService.GetBlockRecordPayrollByIdformAndIdRecord(IdForm, IdRecord)
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function SaveBlockRecordPayroll(blockRecordPayroll As BlockRecordPayroll, session As SessionValues) As ActionResult(Of BlockRecordPayroll) Implements IPayrollBlockRecordPayroll.SaveBlockRecordPayroll
        Using blockRecordPayrollAdminService As IBlockRecordPayrollAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBlockRecordPayrollAdminService)()
            Return blockRecordPayrollAdminService.SaveBlockRecordPayroll(blockRecordPayroll)
        End Using
    End Function

End Class
