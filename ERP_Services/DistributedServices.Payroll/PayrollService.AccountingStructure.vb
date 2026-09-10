'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 22-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    Implements IPayrollAccountingStructure

    ''' <summary>
    ''' Elimina una Estructura Contable de Nómina
    ''' </summary>
    ''' <param name="accountingStructure">accountingStructure</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteAccountingStructure(accountingStructure As AccountingStructure, session As SessionValues) As ActionMessageResult(Of AccountingStructure) Implements IPayrollAccountingStructure.DeleteAccountingStructure
        Using accountingStructureAdmin As IAccountingStructureAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAccountingStructureAdminService)()
            Return accountingStructureAdmin.DeleteAccountingStructure(accountingStructure, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la Estructura Contable de Nómina x Código
    ''' </summary>
    ''' <param name="code">Código Accounting Structure</param>
    ''' <param name="session">Objeto Session</param>
    ''' <param name="desatach"></param>
    ''' <returns>AccountingStructure</returns>
    ''' <remarks></remarks>
    Public Function GetAccountingStructure(code As String, session As SessionValues) As AccountingStructure Implements IPayrollAccountingStructure.GetAccountingStructure
        Using accountingStructureAdmin As IAccountingStructureAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAccountingStructureAdminService)()
            Return accountingStructureAdmin.GetAccountingStructure(code)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la Estructura Contable de Nómina x Id
    ''' </summary>
    ''' <param name="accountingStructureId">Id Accounting Structure</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>AccountingStructure</returns>
    ''' <remarks></remarks>
    Public Function GetAccountingStructureById(accountingStructureId As Integer, session As SessionValues) As AccountingStructure Implements IPayrollAccountingStructure.GetAccountingStructureById
        Using accountingStructureAdmin As IAccountingStructureAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAccountingStructureAdminService)()
            Return accountingStructureAdmin.GetAccountingStructureById(accountingStructureId)
        End Using
    End Function

    ''' <summary>
    ''' Almacena una Estructura Contable de Nómina
    ''' </summary>
    ''' <param name="accountingStructure">accountingStructure</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveAccountingStructure(accountingStructure As AccountingStructure, session As SessionValues) As Boolean Implements IPayrollAccountingStructure.SaveAccountingStructure
        Using accountingStructureAdmin As IAccountingStructureAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAccountingStructureAdminService)()
            Return accountingStructureAdmin.SaveAccountingStructure(accountingStructure, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Lista Toda las Estructuras Contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllAccountingStructure(session As SessionValues) As List(Of AccountingStructure) Implements IPayrollAccountingStructure.ListAllAccountingStructure
        Using accountingStructureAdmin As IAccountingStructureAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAccountingStructureAdminService)()
            Return accountingStructureAdmin.ListAllAccountingStructure()
        End Using
    End Function
End Class
