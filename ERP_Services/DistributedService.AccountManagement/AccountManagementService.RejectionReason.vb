'************************************************************
' Assembly         : Distribuited Services
' Author           : Andrés Steven Rojas
' Created          : 07-01-2025
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "imports"
Imports Application.AccountManagement
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
#End Region

Partial Class AccountManagementService
    Implements IAccountManagementServiceRejectionReason

    ''' <summary>
    ''' Lista los motivos de rechazo por usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function ListRejectionReasonByUserCode(userCode As String) As List(Of RejectionReason) Implements IAccountManagementServiceRejectionReason.ListRejectionReasonByUserCode
        Using service As IRejectionReasonAdminService = Container.Current.Resolve(Of IRejectionReasonAdminService)()
            Return service.ListRejectionReasonByUserCode(userCode)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene el Motivo de Rechazo por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetRejectionReasonById(id As Integer) As ActionResult(Of RejectionReason) Implements IAccountManagementServiceRejectionReason.GetRejectionReasonById
        Using service As IRejectionReasonAdminService = Container.Current.Resolve(Of IRejectionReasonAdminService)()
            Return service.GetRejectionReasonById(id)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene el Motivo de Rechazo por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetRejectionReasonByCode(code As String) As ActionResult(Of RejectionReason) Implements IAccountManagementServiceRejectionReason.GetRejectionReasonByCode
        Using service As IRejectionReasonAdminService = Container.Current.Resolve(Of IRejectionReasonAdminService)()
            Return service.GetRejectionReasonByCode(code)
        End Using
    End Function
    ''' <summary>
    ''' Guarda o actualiza el motivo de rechazo
    ''' </summary>
    ''' <param name="rejectionReason"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSecuence"></param>
    ''' <returns></returns>
    Public Function SaveRejectionReason(rejectionReason As RejectionReason, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of RejectionReason) Implements IAccountManagementServiceRejectionReason.SaveRejectionReason
        Using service As IRejectionReasonAdminService = Container.Current.Resolve(Of IRejectionReasonAdminService)()
            Return service.SaveRejectionReason(rejectionReason, audit, idSecuence)
        End Using
    End Function
    ''' <summary>
    ''' Elimina el motivo de rechazo
    ''' </summary>
    ''' <param name="rejectionReason"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteRejectionReason(rejectionReason As RejectionReason, audit As AuditMessage) As ActionResult Implements IAccountManagementServiceRejectionReason.DeleteRejectionReason
        Using service As IRejectionReasonAdminService = Container.Current.Resolve(Of IRejectionReasonAdminService)()
            Return service.DeleteRejectionReason(rejectionReason, audit)
        End Using
    End Function
    ''' <summary>
    ''' Activa o inactiva el Motivo de Rechazo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateRejectionReason(code As String, audit As AuditMessage) As ActionResult(Of RejectionReason) Implements IAccountManagementServiceRejectionReason.ChangeStateRejectionReason
        Using service As IRejectionReasonAdminService = Container.Current.Resolve(Of IRejectionReasonAdminService)()
            Return service.ChangeStateRejectionReason(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene todas las razones de rechazo activas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllRejectionReasons() As ActionResult(Of List(Of RejectionReason)) Implements IAccountManagementServiceRejectionReason.ListAllRejectionReasons
        Using service As IRejectionReasonAdminService = Container.Current.Resolve(Of IRejectionReasonAdminService)()
            Return service.ListAllRejectionReasons()
        End Using
    End Function
End Class
