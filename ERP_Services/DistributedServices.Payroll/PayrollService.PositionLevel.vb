'***********************************************************************
' Assembly         : DistributedService.Payroll
' Author           : Cristhian Salazar
' Created          : 07-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base.Entities

#End Region

Partial Class PayrollService

    ''' <summary>
    ''' Obtiene todos los niveles de cargos
    ''' </summary>
    ''' <returns>Lista de niveles de cargos</returns>
    Public Function ListAllPositionLevel(session As SessionValues) As List(Of PositionLevel) Implements IPayrollService.ListAllPositionLevel
        Using PositionLevelAdmin As IPositionLevelAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPositionLevelAdminService)()
            Return PositionLevelAdmin.ListAllPositionLevel()
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un nivel de cargo especifico
    ''' </summary>
    ''' <param name="code">Codigo del nivel del cargo</param>
    ''' <returns>El nivel de cargo del codigo que envien</returns>
    Public Function GetPositionLevel(ByVal code As String, session As SessionValues) As PositionLevel Implements IPayrollService.GetPositionLevel
        Using PositionLevelAdmin As IPositionLevelAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPositionLevelAdminService)()
            Return PositionLevelAdmin.GetPositionLevel(code)
        End Using
    End Function

    ''' <summary>
    ''' Graba un Nivel de Cargo
    ''' </summary>
    ''' <param name="PositionLevel">Objeto de Nivel de Posición</param>
    ''' <param name="audit">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso, 0. si no lo fue</returns>
    Public Function SavePositionLevel(ByVal PositionLevel As PositionLevel, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of PositionLevel) Implements IPayrollService.SavePositionLevel
        Using PositionLevelAdmin As IPositionLevelAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPositionLevelAdminService)()
            Return PositionLevelAdmin.SavePositionLevel(PositionLevel, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un Nivel de Cargo
    ''' </summary>
    ''' <param name="PositionLevel">Objeto de Nivel de Posición</param>
    ''' <param name="session">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso el borrado, 0. si no lo fue</returns>
    Public Function DeletePositionLevel(ByVal PositionLevel As PositionLevel, session As SessionValues, audit As AuditMessage) As ActionResult Implements IPayrollService.DeletePositionLevel
        Using PositionLevelAdmin As IPositionLevelAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPositionLevelAdminService)()
            Return PositionLevelAdmin.DeletePositionLevel(PositionLevel, audit)
        End Using
    End Function


    Function ChangeStatePositionLevel(ByVal code As String, ByVal state As Boolean, session As SessionValues, audit As AuditMessage) As ActionResult(Of PositionLevel) Implements IPayrollService.ChangeStatePositionLevel
        Using PositionLevelAdmin As IPositionLevelAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPositionLevelAdminService)()
            Return PositionLevelAdmin.ChangeStatePositionLevel(code, state, audit)
        End Using
    End Function

End Class
