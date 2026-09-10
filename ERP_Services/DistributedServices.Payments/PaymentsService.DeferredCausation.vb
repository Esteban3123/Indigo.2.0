'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Public Class PaymentsService

    ''' <summary>
    ''' Elimina una causacion diferida
    ''' </summary>
    ''' <param name="deferredCausation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDeferredCausation(deferredCausation As Domain.Entities.DeferredCausation, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPaymentsDeferredCausation.DeleteDeferredCausation
        Using service As IDeferredCausationAdminService = Container.Current.Resolve(Of IDeferredCausationAdminService)()
            Return service.DeleteDeferredCausation(deferredCausation, audit)
        End Using
        'Return Me._deferredCausationAdminService.DeleteDeferredCausation(deferredCausation, audit)
    End Function

    ''' <summary>
    ''' Obtiene el listado de causaciones diferidas
    ''' </summary>
    ''' <param name="idAccountPayable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationByIdAccountPayable(idAccountPayable As Integer, audit As AuditMessage) As List(Of Domain.Entities.DeferredCausation) Implements IPaymentsDeferredCausation.GetDeferredCausationByIdAccountPayable
        Using service As IDeferredCausationAdminService = Container.Current.Resolve(Of IDeferredCausationAdminService)()
            Return service.GetDeferredCausationByIdAccountPayable(idAccountPayable, audit)
        End Using
        'Return Me._deferredCausationAdminService.GetDeferredCausationByIdAccountPayable(idAccountPayable, audit)
    End Function

    ''' <summary>
    ''' Obtiene un listado de causaciones diferidas por el codigo de la cuenta por pagar
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationByAccountPayableCode(code As String, audit As AuditMessage) As List(Of Domain.Entities.DeferredCausation) Implements IPaymentsDeferredCausation.GetDeferredCausationByAccountPayableCode
        Using service As IDeferredCausationAdminService = Container.Current.Resolve(Of IDeferredCausationAdminService)()
            Return service.GetDeferredCausationByAccountPayableCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza la causacion diferida
    ''' </summary>
    ''' <param name="deferredCausation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveDeferredCausation(deferredCausation As Domain.Entities.DeferredCausation, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DeferredCausation) Implements IPaymentsDeferredCausation.SaveDeferredCausation
        Using service As IDeferredCausationAdminService = Container.Current.Resolve(Of IDeferredCausationAdminService)()
            Return service.SaveDeferredCausation(deferredCausation, audit, idSequense)
        End Using
        'Return Me._deferredCausationAdminService.SaveDeferredCausation(deferredCausation, audit, idSequense)
    End Function

End Class
