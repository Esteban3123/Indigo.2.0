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
    Implements IPayrollConcept


    ''' <summary>
    ''' Elimina un Concepto
    ''' </summary>
    ''' <param name="concept">Concepto</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteConcept(concept As Domain.Payroll.Entities.Concept, session As SessionValues) As ActionMessageResult(Of Concept) Implements IPayrollConcept.DeleteConcept
        Using conceptAdminService As IConceptAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptAdminService)()
            Return conceptAdminService.DeleteConcept(concept, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Concepto
    ''' </summary>
    ''' <param name="code">Código del Concepto</param>
    ''' <returns>Concepto</returns>
    ''' <remarks></remarks>
    Public Function GetConcept(code As String, session As SessionValues) As Domain.Payroll.Entities.Concept Implements IPayrollConcept.GetConcept
        Using conceptAdminService As IConceptAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptAdminService)()
            Return conceptAdminService.GetConcept(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista Todos los Conceptos
    ''' </summary>
    ''' <returns>Conceptos</returns>
    ''' <remarks></remarks>
    Public Function ListAllConcept(session As SessionValues) As List(Of Domain.Payroll.Entities.Concept) Implements IPayrollConcept.ListAllConcept
        Using conceptAdminService As IConceptAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptAdminService)()
            Return conceptAdminService.ListAllConcept()
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza un Concepto
    ''' </summary>
    ''' <param name="concept">Concepto</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveConcept(concept As Domain.Payroll.Entities.Concept, session As SessionValues) As Boolean Implements IPayrollConcept.SaveConcept
        Using conceptAdminService As IConceptAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptAdminService)()
            Return conceptAdminService.SaveConcept(concept, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una lista de Concepto dependiendo de la lista de class Concept, para el formulario de ScheduleTemplate
    ''' </summary>
    ''' <param name="listClassConcept">Lista de codigos de clase de concepto</param>
    ''' <returns>Lista de Concepto</returns>
    ''' <remarks></remarks>
    Public Function GetConceptByConceptClass(listClassConcept As List(Of String), session As SessionValues) As List(Of Domain.Payroll.Entities.Concept) Implements IPayrollConcept.GetConceptByConceptClass
        Using conceptAdminService As IConceptAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptAdminService)()
            Return conceptAdminService.GetConceptByConceptClass(listClassConcept)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado del concepto según el código
    ''' </summary>
    ''' <param name="Code">Código del concepto</param>
    ''' <param name="state">Nuevo estado del concepto</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateConcept(Code As String, state As Boolean, session As SessionValues) As Boolean Implements IPayrollConcept.ChangeStateConcept
        Using conceptAdminService As IConceptAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptAdminService)()
            Return conceptAdminService.ChangeState(Code, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
