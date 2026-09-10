'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports System.ComponentModel

Partial Class PayrollService

    ''' <summary>
    ''' Elimina un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    Public Function DeleteBank(bank As Domain.Payroll.Entities.Bank, session As SessionValues) As ActionMessageResult(Of Bank) Implements IPayrollBank.DeleteBank
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.DeleteBank(bank, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Banco especifico
    ''' </summary>
    ''' <param name="code">Código de el banco</param>
    ''' <returns> Banco</returns>
    Public Function GetBank(code As String, session As SessionValues) As Domain.Payroll.Entities.Bank Implements IPayrollBank.GetBank
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.GetBank(code, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns>Lista de bancos</returns>
    Public Function ListAllBank(session As SessionValues) As List(Of Domain.Payroll.Entities.Bank) Implements IPayrollBank.ListAllBank
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.ListAllBank()
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    Public Function SaveBank(bank As Domain.Payroll.Entities.Bank, session As SessionValues, idSequence As Long) As ActionResult(Of Bank) Implements IPayrollBank.SaveBank
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.SaveBank(bank, session.AuditMessageWcf, idSequence)
        End Using
    End Function

    Public Function UpdateStateBank(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Bank) Implements IPayrollBank.UpdateStateBank
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.UpdateStateBank(code, state, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un banco por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetBankById(Id As Integer, session As SessionValues) As Bank Implements IPayrollBank.GetBankById
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.GetBankById(Id)
        End Using
    End Function

    Public Function SetCopyPasteOrImportFileBankDetail(session As SessionValues, dataImportFile As List(Of Domain.Base.Entities.ImportFileRow), dataCopyPaste As List(Of List(Of String))) As Domain.Base.Entities.ActionResult(Of List(Of BankDetail)) Implements IPayrollBank.SetCopyPasteOrImportFileBankDetail
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.SetCopyPasteOrImportFileBankDetail(dataImportFile, dataCopyPaste)
        End Using
    End Function


    ''' <summary>
    ''' Sets details BankAutomaticRecornitionRules from Copy and Paste.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetBankAutomaticRecognitionRulesFromCopyandPaste(session As SessionValues, data As List(Of List(Of String))) As ActionResult(Of List(Of BankAutomaticRecognitionRules)) Implements IPayrollBank.SetBankAutomaticRecognitionRulesFromCopyandPaste
        Using service As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return service.SetBankAutomaticRecognitionRulesFromFIle(Nothing, data)
        End Using
    End Function

    ''' <summary>
    ''' Sets details BankAutomaticRecornitionRules from Copy and Paste.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetBankAutomaticRecognitionRulesFromFile(session As SessionValues, data As List(Of ImportFileRow)) As ActionResult(Of List(Of BankAutomaticRecognitionRules)) Implements IPayrollBank.SetBankAutomaticRecognitionRulesFromFile
        Using service As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return service.SetBankAutomaticRecognitionRulesFromFIle(data, Nothing)
        End Using
    End Function


End Class
