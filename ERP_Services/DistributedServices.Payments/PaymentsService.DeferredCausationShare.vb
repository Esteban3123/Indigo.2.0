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
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDeferredCausationShare(deferredCausationShare As Domain.Entities.DeferredCausationShare) As Domain.Base.Entities.ActionResult Implements IPaymentsDeferredCausationShare.DeleteDeferredCausationShare
        Using service As IDeferredCausationShareAdminService = Container.Current.Resolve(Of IDeferredCausationShareAdminService)()
            Return service.DeleteDeferredCausationShare(deferredCausationShare)
        End Using
        'Return Me._deferredCausationShare.DeleteDeferredCausationShare(deferredCausationShare)
    End Function

    ''' <summary>
    ''' Obtiene el listado de causaciones diferidas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationShareById(id As Integer) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DeferredCausationShare) Implements IPaymentsDeferredCausationShare.GetDeferredCausationShareById
        Using service As IDeferredCausationShareAdminService = Container.Current.Resolve(Of IDeferredCausationShareAdminService)()
            Return service.GetDeferredCausationShareById(id)
        End Using
        'Return Me._deferredCausationShare.GetDeferredCausationShareById(id)
    End Function

    ''' <summary>
    ''' Guarda o actualiza la causacion diferida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveDeferredCausationShare(deferredCausationShare As Domain.Entities.DeferredCausationShare) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DeferredCausationShare) Implements IPaymentsDeferredCausationShare.SaveDeferredCausationShare
        Using service As IDeferredCausationShareAdminService = Container.Current.Resolve(Of IDeferredCausationShareAdminService)()
            Return service.SaveDeferredCausationShare(deferredCausationShare)
        End Using
        'Return Me._deferredCausationShare.SaveDeferredCausationShare(deferredCausationShare)
    End Function

    ''' <summary>
    ''' Obtiene el listado de las cuotas de causacion que tiene asociado la cxp
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationShareByAccountPayableId(accountPayableId As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.DeferredCausationShare)) Implements IPaymentsDeferredCausationShare.GetDeferredCausationShareByAccountPayableId
        Using service As IDeferredCausationShareAdminService = Container.Current.Resolve(Of IDeferredCausationShareAdminService)()
            Return service.GetDeferredCausationShareByAccountPayableId(accountPayableId)
        End Using
        'Return Me._deferredCausationShare.GetDeferredCausationShareByAccountPayableId(accountPayableId)
    End Function

    ''' <summary>
    ''' Actualiza los valores de las cuotas de la causacion diferida
    ''' </summary>
    ''' <param name="ListDeferredCausationShare"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveListDeferredCausationShare(ListDeferredCausationShare As List(Of Domain.Entities.DeferredCausationShare), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.DeferredCausationShare)) Implements IPaymentsDeferredCausationShare.SaveListDeferredCausationShare
        Using service As IDeferredCausationShareAdminService = Container.Current.Resolve(Of IDeferredCausationShareAdminService)()
            Return service.SaveListDeferredCausationShare(ListDeferredCausationShare, audit)
        End Using
        'Return Me._deferredCausationShare.SaveListDeferredCausationShare(ListDeferredCausationShare, audit)
    End Function

End Class
