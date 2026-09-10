'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IAccountPayableTransferAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un traslado de factura
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveAccountPayableTransfer(ByVal AccountPayableTransfer As AccountPayableTransfer, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AccountPayableTransfer)

    ''' <summary>
    ''' Elimina un traslado de factura
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteAccountPayableTransfer(ByVal AccountPayableTransfer As AccountPayableTransfer, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Anula un traslado de factura
    ''' </summary>
    ''' <param name="AccountPayableTransfer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AnnularAccountPayableTransfer(ByVal AccountPayableTransfer As AccountPayableTransfer, ByVal audit As AuditMessage) As ActionResult(Of AccountPayableTransfer)

    ''' <summary>
    ''' Obtiene un traslado de factura
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetAccountPayableTransfer(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of AccountPayableTransfer)

    ''' <summary>
    ''' Obtiene un traslado de factura
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableTransferById(id As String, ByVal audit As AuditMessage) As ActionResult(Of AccountPayableTransfer)

    ''' <summary>
    ''' Funcion para Aceptar o rechazar los traslados
    ''' </summary>
    ''' <param name="ListIDDetailTranfer">Lista de Id de detalle de traslado a actualizar</param>
    ''' <param name="IDTarget">Id de unidad de radicacion de Destino</param>
    ''' <param name="RejectionReasonID">Id del motivo de rechazo</param>
    ''' <param name="RejectionDescription">Descipcion del rechazo</param>
    ''' <param name="audit">auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAcceptanceTranfer(ListIDDetailTranfer As List(Of Integer), IDTarget As Integer, RejectionReasonID As Integer?, RejectionDescription As String, audit As AuditMessage) As ActionResult

End Interface
