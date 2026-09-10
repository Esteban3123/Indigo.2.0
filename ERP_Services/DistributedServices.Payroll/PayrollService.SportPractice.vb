'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Juan Diego Díaz
' Created          : 31-08-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService


    ''' <summary>
    ''' Elimina un Deporte Practicado
    ''' </summary>
    ''' <param name="sportPractice">Deporte Practicado</param>
    ''' <param name="session">Objeto Sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteSportPractice(sportPractice As Domain.Payroll.Entities.SportPractice, session As SessionValues) As ActionResult Implements IPayrollSportPractice.DeleteSportPractice
        Using sportPracticeAdminService As ISportPracticeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISportPracticeAdminService)()
            Return sportPracticeAdminService.DeleteSportPractice(sportPractice, session.AuditMessageWcf)
        End Using
    End Function


    ''' <summary>
    ''' Obtiene un Deporte Practicado
    ''' </summary>
    ''' <param name="code">Código del Deporte Practicado</param>
    ''' <param name="session">Objeto Sesión</param>
    ''' <returns>Practica Deportiva</returns>
    ''' <remarks></remarks>
    Public Function GetSportPractice(code As String, tracking As Boolean, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.SportPractice) Implements IPayrollSportPractice.GetSportPractice
        Using sportPracticeAdminService As ISportPracticeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISportPracticeAdminService)()
            Return sportPracticeAdminService.GetSportPractice(code, tracking, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Almacena un Deporte Practicado
    ''' </summary>
    ''' <param name="sportPractice">Deporte Practicado</param>
    ''' <param name="session">Objeto Sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveSportPractice(sportPractice As Domain.Payroll.Entities.SportPractice, session As SessionValues, idSequense As Int64) As ActionResult(Of Domain.Payroll.Entities.SportPractice) Implements IPayrollSportPractice.SaveSportPractice
        Using sportPracticeAdminService As ISportPracticeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISportPracticeAdminService)()
            Return sportPracticeAdminService.SaveSportPractice(sportPractice, session.AuditMessageWcf, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una Practica Deportiva
    ''' </summary>
    ''' <param name="ID">ID del Deporte Practicado</param>
    ''' <returns>Practica Deportiva</returns>
    ''' <remarks></remarks>
    Public Function GetSportPracticeById(ID As String, tracking As Boolean, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.SportPractice) Implements IPayrollSportPractice.GetSportPracticeById
        Using sportPracticaAdminService As ISportPracticeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISportPracticeAdminService)()
            Return sportPracticaAdminService.GetSportPracticeById(ID, tracking, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code">Código del Deporte Practicado</param>
    ''' <returns>Deporte Practicado</returns>
    ''' <remarks></remarks>
    Public Function ChangeStateSportPractice(code As String, state As Boolean, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Payroll.Entities.SportPractice) Implements IPayrollSportPractice.ChangeStateSportPractice
        Using sportPracticaAdminService As ISportPracticeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISportPracticeAdminService)()
            Return sportPracticaAdminService.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function

End Class
