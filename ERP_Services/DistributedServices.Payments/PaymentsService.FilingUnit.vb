'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class PaymentsService

    ''' <summary>
    ''' Elimina una unidad de radicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteFilingUnit(FilingUnit As Domain.Entities.FilingUnit, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPaymentsFilingUnit.DeleteFilingUnit
        Using service As IFilingUnitAdminService = Container.Current.Resolve(Of IFilingUnitAdminService)()
            Return service.DeleteFilingUnit(FilingUnit, audit)
        End Using
        'Return Me._filingUnitAdminService.DeleteFilingUnit(FilingUnit, audit)
    End Function

    ''' <summary>
    ''' Obtiene una unidad de radicacion
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnit(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FilingUnit) Implements IPaymentsFilingUnit.GetFilingUnit
        Using service As IFilingUnitAdminService = Container.Current.Resolve(Of IFilingUnitAdminService)()
            Return service.GetFilingUnit(code, audit)
        End Using
        'Return Me._filingUnitAdminService.GetFilingUnit(code, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una unidad de radicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFilingUnit(FilingUnit As Domain.Entities.FilingUnit, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FilingUnit) Implements IPaymentsFilingUnit.SaveFilingUnit
        Using service As IFilingUnitAdminService = Container.Current.Resolve(Of IFilingUnitAdminService)()
            Return service.SaveFilingUnit(FilingUnit, audit, idSequense)
        End Using
        'Return Me._filingUnitAdminService.SaveFilingUnit(FilingUnit, audit, idSequense)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateFilingUnit(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FilingUnit) Implements IPaymentsFilingUnit.ChangeStateFilingUnit
        Using service As IFilingUnitAdminService = Container.Current.Resolve(Of IFilingUnitAdminService)()
            Return service.ChangeState(code, state, audit)
        End Using
        'Return Me._filingUnitAdminService.ChangeState(code, state, audit)
    End Function

    ''' <summary>
    ''' Consulta una unidad de radicacion
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnitById(id As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FilingUnit) Implements IPaymentsFilingUnit.GetFilingUnitById
        Using service As IFilingUnitAdminService = Container.Current.Resolve(Of IFilingUnitAdminService)()
            Return service.GetFilingUnitById(id, audit)
        End Using
        'Return Me._filingUnitAdminService.GetFilingUnitById(id, audit)
    End Function

    ''' <summary>
    ''' Obtiene las unidades de radicacion
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnitByUser(userCode As String) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.FilingUnit)) Implements IPaymentsFilingUnit.GetFilingUnitByUser
        Using service As IFilingUnitAdminService = Container.Current.Resolve(Of IFilingUnitAdminService)()
            Return service.GetFilingUnitByUser(userCode)
        End Using
        'Return Me._filingUnitAdminService.GetFilingUnitByUser(userCode)
    End Function
    ''' <summary>
    ''' Obtiene las unidades de radicacion que tiene permiso un usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnitByUserPermission(userCode As String) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.FilingUnitUser)) Implements IPaymentsFilingUnit.GetFilingUnitByUserPermission
        Using service As IFilingUnitAdminService = Container.Current.Resolve(Of IFilingUnitAdminService)()
            Return service.GetFilingUnitByUserPermission(userCode)
        End Using
        'Return Me._filingUnitAdminService.GetFilingUnitByUserPermission(userCode)
    End Function

End Class
