Imports Infrastructure.CrossCutting.IOC
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Elimina una unidad funcional
    ''' </summary>
    ''' <param name="FunctionalUnit">Unidad Funcional</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteFunctionalUnit(FunctionalUnit As Domain.Payroll.Entities.FunctionalUnit, session As SessionValues) As ActionMessageResult(Of Domain.Payroll.Entities.FunctionalUnit) Implements IPayrollFunctionalUnit.DeleteFunctionalUnit
        Using functionalUnitAdminService As IFunctionalUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFunctionalUnitAdminService)()
            Return functionalUnitAdminService.DeleteFunctionalUnit(FunctionalUnit, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una unidad funcional
    ''' </summary>
    ''' <param name="id">id de la unidad funcional</param>
    ''' <returns>Unidad funcional</returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnitById(id As String, session As SessionValues) As Domain.Payroll.Entities.FunctionalUnit Implements IPayrollFunctionalUnit.GetFunctionalUnitById
        Using functionalUnitAdminService As IFunctionalUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFunctionalUnitAdminService)()
            Return functionalUnitAdminService.GetFunctionalUnitById(id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una unidad funcional
    ''' </summary>
    ''' <param name="code">Codigo de la unidad funcional</param>
    ''' <returns>Unidad funcional</returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnit(code As String, session As SessionValues) As Domain.Payroll.Entities.FunctionalUnit Implements IPayrollFunctionalUnit.GetFunctionalUnit
        Using functionalUnitAdminService As IFunctionalUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFunctionalUnitAdminService)()
            Return functionalUnitAdminService.GetFunctionalUnit(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos las unidades funcionales
    ''' </summary>
    ''' <returns>Lista las unidades funcionales</returns>
    ''' <remarks></remarks>
    Public Function ListAllFunctionalUnit(session As SessionValues) As List(Of Domain.Payroll.Entities.FunctionalUnit) Implements IPayrollFunctionalUnit.ListAllFunctionalUnit
        Using functionalUnitAdminService As IFunctionalUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFunctionalUnitAdminService)()
            Return functionalUnitAdminService.ListAllFunctionalUnit()
        End Using
    End Function

    ''' <summary>
    ''' Graba o actualiza una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnit">unidad funcional a guardar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function SaveFunctionalUnit(functionalUnit As Domain.Payroll.Entities.FunctionalUnit, session As SessionValues, idSequence As Long) As ActionResult(Of Domain.Payroll.Entities.FunctionalUnit) Implements IPayrollFunctionalUnit.SaveFunctionalUnit
        Using functionalUnitAdminService As IFunctionalUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFunctionalUnitAdminService)()
            Return functionalUnitAdminService.SaveFunctionalUnit(functionalUnit, session.AuditMessageWcf, idSequence)
        End Using
    End Function

    Public Function UpdateStateFunctionalUnit(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.FunctionalUnit) Implements IPayrollFunctionalUnit.UpdateStateFunctionalUnit
        Using functionalUnitAdminService As IFunctionalUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFunctionalUnitAdminService)()
            Return functionalUnitAdminService.UpdateStateFunctionalUnit(code, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
