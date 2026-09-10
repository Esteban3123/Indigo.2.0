'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 16-04-2013
'
' Last Modified By : Cristhian Mauricio Salazar
' Last Modified On : 07-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Elimina Fondo
    ''' </summary>
    ''' <returns>True o False</returns>
    Public Function DeleteFunds(Funds As Domain.Payroll.Entities.Fund, session As SessionValues, audit As AuditMessage) As ActionResult Implements IPayrollService.DeleteFunds
        Using FundsAdminService As IFundsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFundsAdminService)()
            Return FundsAdminService.DeleteFunds(Funds, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Fondo en Específico
    ''' </summary>
    ''' <param name="code">Código del Fondo</param>
    ''' <returns>Fondo</returns>
    Public Function GetFunds(code As String, session As SessionValues) As Domain.Payroll.Entities.Fund Implements IPayrollService.GetFunds
        Using FundsAdminService As IFundsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFundsAdminService)()
            Return FundsAdminService.GetFunds(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista Todos los fondos
    ''' </summary>
    ''' <returns>Lista de Fondos</returns>
    Public Function ListAllFunds(session As SessionValues) As List(Of Domain.Payroll.Entities.Fund) Implements IPayrollService.ListAllFunds
        Using FundsAdminService As IFundsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFundsAdminService)()
            Return FundsAdminService.ListAllFunds()
        End Using
    End Function

    ''' <summary>
    ''' Graba o Actualiza un Fondo
    ''' </summary>
    ''' <param name="Funds">Fondo</param>
    ''' <param name="audit">Objeto de Auditoría</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function SaveFunds(Funds As Domain.Payroll.Entities.Fund, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.Fund) Implements IPayrollService.SaveFunds
        Using FundsAdminService As IFundsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFundsAdminService)()
            Return FundsAdminService.SaveFunds(Funds, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tercero atraves del nit
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns>Tercero</returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyByNit(nit As String, session As SessionValues) As Domain.Payroll.Entities.ThirdParty Implements IPayrollFunds.GetThirdPartyByNit
        Using FundsAdminService As IFundsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFundsAdminService)()
            Return FundsAdminService.GetThirdPartyByNit(nit)
        End Using
    End Function

    Function ChangeStateFund(ByVal code As String, ByVal state As Boolean, session As SessionValues, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.Fund) Implements IPayrollFunds.ChangeStateFund
        Using FundsAdminService As IFundsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFundsAdminService)()
            Return FundsAdminService.ChangeStateFund(code, state, audit)
        End Using
    End Function
End Class
