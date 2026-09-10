#Region "Imports"
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

<ServiceContract()>
Public Interface IAccountManagementServiceRejectionReason
    ''' <summary>
    ''' Lista los motivos de rechazo por usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListRejectionReasonByUserCode(userCode As String) As List(Of RejectionReason)
    ''' <summary>
    ''' Obtiene el motivo de rechazo por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRejectionReasonById(id As Integer) As ActionResult(Of RejectionReason)
    ''' <summary>
    ''' Obtiene el motivo de rechazo por Código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRejectionReasonByCode(code As String) As ActionResult(Of RejectionReason)
    ''' <summary>
    ''' Guarda o Actualiza el motivo de rechazo
    ''' </summary>
    ''' <param name="rejectionReason"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSecuence"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveRejectionReason(rejectionReason As RejectionReason, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of RejectionReason)
    ''' <summary>
    ''' Elimina el motivo de rechazo
    ''' </summary>
    ''' <param name="rejectionReason"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteRejectionReason(rejectionReason As RejectionReason, audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Activa o Inactiva el motivo de rechazo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateRejectionReason(code As String, audit As AuditMessage) As ActionResult(Of RejectionReason)

    ''' <summary>
    ''' Obtiene todas las razones de rechazo activas
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAllRejectionReasons() As ActionResult(Of List(Of RejectionReason))

End Interface
