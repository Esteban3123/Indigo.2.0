'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsAccountPayableTransfer

    ''' <summary>
    ''' Guarda o Actualiza un traslado de factura
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAccountPayableTransfer(AccountPayableTransfer As Domain.Entities.AccountPayableTransfer, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableTransfer)

    ''' <summary>
    ''' Elimina un traslado de factura
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteAccountPayableTransfer(AccountPayableTransfer As Domain.Entities.AccountPayableTransfer, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Anula un traslado de factura
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function AnnularAccountPayableTransfer(AccountPayableTransfer As Domain.Entities.AccountPayableTransfer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableTransfer)

    ''' <summary>
    ''' Obtiene un traslado de factura
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountPayableTransfer(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableTransfer)

    ''' <summary>
    ''' Obtiene un traslado de factura
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountPayableTransferById(id As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableTransfer)

    ''' <summary>
    ''' Funcion para Aceptar o rechazar los traslados
    ''' </summary>
    ''' <param name="ListIDDetailTranfer">Lista de Id de detalle de traslado a actualizar</param>
    ''' <param name="IDTarget">Id de unidad de radicacion de Destino</param>
    ''' <param name="RejectionReasonID">Id del motivo de rechazo</param>
    ''' <param name="RejectionDescription">Descipcion del rechazo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveAcceptanceTranfer(ListIDDetailTranfer As List(Of Integer), IDTarget As Integer, RejectionReasonID As Integer?, RejectionDescription As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult

End Interface
