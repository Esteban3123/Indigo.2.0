'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Common
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports DistributedServices.Common

Partial Class CommonERPService
    Implements ICommonERPBank

    ''' <summary>
    ''' Elimina un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    Public Function DeleteBank(bank As Bank, session As SessionValues) As ActionMessageResult(Of Bank) Implements ICommonERPBank.DeleteBank
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.DeleteBank(bank, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Banco especifico
    ''' </summary>
    ''' <param name="code">Código de el banco</param>
    ''' <returns> Banco</returns>
    Public Function GetBank(code As String, session As SessionValues) As Bank Implements ICommonERPBank.GetBank
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.GetBank(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns>Lista de bancos</returns>
    Public Function ListAllBank(session As SessionValues) As List(Of Bank) Implements ICommonERPBank.ListAllBank
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.ListAllBank()
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    Public Function SaveBank(bank As Bank, session As SessionValues, idSequence As Long) As ActionResult(Of Bank) Implements ICommonERPBank.SaveBank
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.SaveBank(bank, session.AuditMessageWcf, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un banco por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetBankById(Id As Integer, session As SessionValues) As Bank Implements ICommonERPBank.GetBankById
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.GetBankById(Id)
        End Using
    End Function

    Public Function UpdateStateBank(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Bank) Implements ICommonERPBank.UpdateStateBank
        Using bankAdminService As IBankAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankAdminService)()
            Return bankAdminService.UpdateStateBank(code, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
