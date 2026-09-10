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
    ''' Elimina un traslado de factura
    ''' </summary>
    ''' <param name="AccountPayableTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAccountPayableTransfer(AccountPayableTransfer As Domain.Entities.AccountPayableTransfer, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPaymentsAccountPayableTransfer.DeleteAccountPayableTransfer
        Using service As IAccountPayableTransferAdminService = Container.Current.Resolve(Of IAccountPayableTransferAdminService)()
            Return service.DeleteAccountPayableTransfer(AccountPayableTransfer, audit)
        End Using
        'Return Me._accountPayableTransferAdminService.DeleteAccountPayableTransfer(AccountPayableTransfer, audit)
    End Function

    ''' <summary>
    ''' Obtiene un traslado de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableTransfer(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableTransfer) Implements IPaymentsAccountPayableTransfer.GetAccountPayableTransfer
        Using service As IAccountPayableTransferAdminService = Container.Current.Resolve(Of IAccountPayableTransferAdminService)()
            Return service.GetAccountPayableTransfer(code, audit)
        End Using
        'Return Me._accountPayableTransferAdminService.GetAccountPayableTransfer(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene un traslado de factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableTransferById(id As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableTransfer) Implements IPaymentsAccountPayableTransfer.GetAccountPayableTransferById
        Using service As IAccountPayableTransferAdminService = Container.Current.Resolve(Of IAccountPayableTransferAdminService)()
            Return service.GetAccountPayableTransferById(id, audit)
        End Using
        'Return Me._accountPayableTransferAdminService.GetAccountPayableTransferById(id, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un traslado de factura
    ''' </summary>
    ''' <param name="AccountPayableTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAccountPayableTransfer(AccountPayableTransfer As Domain.Entities.AccountPayableTransfer, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableTransfer) Implements IPaymentsAccountPayableTransfer.SaveAccountPayableTransfer
        Using service As IAccountPayableTransferAdminService = Container.Current.Resolve(Of IAccountPayableTransferAdminService)()
            Return service.SaveAccountPayableTransfer(AccountPayableTransfer, audit, idSequense)
        End Using
        'Return Me._accountPayableTransferAdminService.SaveAccountPayableTransfer(AccountPayableTransfer, audit, idSequense)
    End Function

    ''' <summary>
    ''' Anula un traslado de factura
    ''' </summary>
    ''' <param name="AccountPayableTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AnnularAccountPayableTransfer(AccountPayableTransfer As Domain.Entities.AccountPayableTransfer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableTransfer) Implements IPaymentsAccountPayableTransfer.AnnularAccountPayableTransfer
        Using service As IAccountPayableTransferAdminService = Container.Current.Resolve(Of IAccountPayableTransferAdminService)()
            Return service.AnnularAccountPayableTransfer(AccountPayableTransfer, audit)
        End Using
        'Return Me._accountPayableTransferAdminService.AnnularAccountPayableTransfer(AccountPayableTransfer, audit)
    End Function
    ''' <summary>
    ''' aceptacion de traslado de facturas
    ''' </summary>
    ''' <param name="ListIDDetailTranfer"></param>
    ''' <param name="IDTarget"></param>
    ''' <param name="RejectionReasonID"></param>
    ''' <param name="RejectionDescription"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAcceptanceTranfer(ListIDDetailTranfer As List(Of Integer), IDTarget As Integer, RejectionReasonID As Integer?, RejectionDescription As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPaymentsAccountPayableTransfer.SaveAcceptanceTranfer
        Using service As IAccountPayableTransferAdminService = Container.Current.Resolve(Of IAccountPayableTransferAdminService)()
            Return service.SaveAcceptanceTranfer(ListIDDetailTranfer, IDTarget, RejectionReasonID, RejectionDescription, audit)
        End Using
        'Return Me._accountPayableTransferAdminService.SaveAcceptanceTranfer(ListIDDetailTranfer, IDTarget, RejectionReasonID, RejectionDescription, audit)
    End Function
End Class
