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

Partial Class PayrollService

    ''' <summary>
    ''' Elimina un cargo
    ''' </summary>
    ''' <param name="position">Cargo</param>
    ''' <returns></returns>
    Public Function DeletePosition(position As Domain.Payroll.Entities.Position, session As SessionValues) As ActionMessageResult(Of Position) Implements IPayrollPosition.DeletePosition
        Using positionAdminService As IPositionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPositionAdminService)()
            Return positionAdminService.DeletePosition(position, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetPosition(code As String, session As SessionValues) As Domain.Payroll.Entities.Position Implements IPayrollPosition.GetPosition
        Using positionAdminService As IPositionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPositionAdminService)()
            Return positionAdminService.GetPosition(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los cargos
    ''' </summary>
    ''' <returns>Lista de cargos</returns>
    Public Function ListAllPosition(session As SessionValues) As List(Of Domain.Payroll.Entities.Position) Implements IPayrollPosition.ListAllPosition
        Using positionAdminService As IPositionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPositionAdminService)()
            Return positionAdminService.ListAllPosition()
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita un cargo
    ''' </summary>
    ''' <param name="position">Cargo</param>
    ''' <returns></returns>
    Public Function SavePosition(position As Domain.Payroll.Entities.Position, session As SessionValues) As Boolean Implements IPayrollPosition.SavePosition
        Using positionAdminService As IPositionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPositionAdminService)()
            Return positionAdminService.SavePosition(position, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita un cargo
    ''' </summary>
    ''' <param name="position">Cargo</param>
    ''' <returns></returns>
    Public Function ChangeStatePosition(code As String, state As Boolean, session As SessionValues) As Boolean Implements IPayrollPosition.ChangeStatePosition
        Using positionAdminService As IPositionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPositionAdminService)()
            Return positionAdminService.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function


End Class
