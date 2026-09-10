'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IAccountPayableRejectionReasonAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un rechazo de facrtura
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveAccountPayableRejectionReason(ByVal AccountPayableRejectionReason As AccountPayableRejectionReason, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AccountPayableRejectionReason)

    ''' <summary>
    ''' Elimina un rechazo de factura
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteAccountPayableRejectionReason(ByVal AccountPayableRejectionReason As AccountPayableRejectionReason, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un determinado rechazo de factura
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetAccountPayableRejectionReason(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of AccountPayableRejectionReason)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of AccountPayableRejectionReason)

    ''' <summary>
    ''' Obtiene un rechazo de factura
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableRejectionReasonById(id As String, ByVal audit As AuditMessage) As ActionResult(Of AccountPayableRejectionReason)
    ''' <summary>
    ''' Lista las razones de rechazo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListRejectionReason() As List(Of AccountPayableRejectionReason)

End Interface
