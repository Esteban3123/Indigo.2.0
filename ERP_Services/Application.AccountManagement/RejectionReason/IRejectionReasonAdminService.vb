'************************************************************
' Assembly         : Domain.Entities
' Author           : Andrés Steven Rojas
' Created          : 07-01-2025
'
' Copyright        : (c) . All rights reserved.
'************************************************************
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IRejectionReasonAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene los Motivos de Rechazo por código de usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function ListRejectionReasonByUserCode(ByVal userCode As String) As List(Of RejectionReason)
    ''' <summary>
    ''' Obtiene el Motivo de Rechazo por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetRejectionReasonById(ByVal id As Integer) As ActionResult(Of RejectionReason)
    ''' <summary>
    ''' Obtiene el Motivo de Rechazo por Código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Function GetRejectionReasonByCode(ByVal code As String) As ActionResult(Of RejectionReason)
    ''' <summary>
    ''' Guarda o actualiza el motivo de rechazo
    ''' </summary>
    ''' <param name="rejectionReason"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveRejectionReason(rejectionReason As RejectionReason, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of RejectionReason)
    ''' <summary>
    ''' Elimina el motivo de rechazo
    ''' </summary>
    ''' <param name="rejectionReason"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function DeleteRejectionReason(rejectionReason As RejectionReason, audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Activa o Inactiva el motivo de rechazo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ChangeStateRejectionReason(code As String, audit As AuditMessage) As ActionResult(Of RejectionReason)

    ''' <summary>
    ''' Obtiene todas las razones de rechazo activas
    ''' </summary>
    ''' <returns></returns>
    Function ListAllRejectionReasons() As ActionResult(Of List(Of RejectionReason))

End Interface
