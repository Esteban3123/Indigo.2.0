'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Andres Felipe Quintero Garcia
' Created          : 10-02-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Payroll
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC
#End Region

Partial Public Class PayrollService
    Implements IPayrollElectronicPayrollConcepts

    ''' <summary>
    ''' Función que obtiene el conceptos de nómina electrónica por Code
    ''' </summary>
    ''' <param name="code">Id del Grupo</param>
    ''' <returns>Conceptos de nómina electrónica</returns>
    ''' <remarks></remarks>
    Public Function GetElectronicPayrollConceptsByCode(code As String, session As SessionValues) As ElectronicPayrollConcepts Implements IPayrollElectronicPayrollConcepts.GetElectronicPayrollConceptsByCode
        Using electronicPayrollConceptsPayroll As IElectronicPayrollConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IElectronicPayrollConceptsAdminService)()
            Return electronicPayrollConceptsPayroll.GetElectronicPayrollConceptsByCode(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nómina electrónica
    ''' </summary>
    ''' <returns>Conceptos de nómina electrónica</returns>
    ''' <remarks></remarks>
    Public Function ListAllElectronicPayrollConcepts(session As SessionValues) As List(Of Domain.Payroll.Entities.ElectronicPayrollConcepts) Implements IPayrollElectronicPayrollConcepts.ListAllElectronicPayrollConcepts
        Using ElectronicPayrollConceptsAdminService As IElectronicPayrollConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IElectronicPayrollConceptsAdminService)()
            Return ElectronicPayrollConceptsAdminService.ListAllElectronicPayrollConcepts()
        End Using
    End Function

    ''' <summary>
    ''' Almacena los conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="electronicPayrollConcepts"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveElectronicPayrollConcepts(electronicPayrollConcepts As ElectronicPayrollConcepts, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of ElectronicPayrollConcepts) Implements IPayrollElectronicPayrollConcepts.SaveElectronicPayrollConcepts
        Using electronicPayrollConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IElectronicPayrollConceptsAdminService)()
            Return electronicPayrollConceptsAdminService.SaveElectronicPayrollConcepts(electronicPayrollConcepts, session.AuditMessageWcf, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Función Para Eliminar conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="electronicPayrollConcepts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteElectronicPayrollConcepts(electronicPayrollConcepts As ElectronicPayrollConcepts, session As SessionValues, audit As AuditMessage) As ActionResult Implements IPayrollElectronicPayrollConcepts.DeleteElectronicPayrollConcepts
        Using electronicPayrollConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IElectronicPayrollConceptsAdminService)()
            Return electronicPayrollConceptsAdminService.DeleteElectronicPayrollConcepts(electronicPayrollConcepts, session.AuditMessageWcf)
        End Using
    End Function

    Function ChangeStateElectronicPayrollConcepts(ByVal code As String, ByVal state As Boolean, session As SessionValues, audit As AuditMessage) As ActionResult(Of ElectronicPayrollConcepts) Implements IPayrollService.ChangeStateElectronicPayrollConcepts
        Using ElectronicPayrollConceptsAdmin As IElectronicPayrollConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IElectronicPayrollConceptsAdminService)()
            Return ElectronicPayrollConceptsAdmin.ChangeStateElectronicPayrollConcepts(code, state, audit)
        End Using
    End Function
End Class
