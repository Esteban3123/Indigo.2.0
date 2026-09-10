'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Cristian Camilo Bahamon
' Created          : 2022-07-29
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC

#End Region

Partial Class BillingService

    Public Function ListAllJustificationControl() As List(Of BillingJustificationControl) Implements IBillingServiceJustificationControl.ListAllJustificationControl
        Using service As IBillingJustificationControlAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IBillingJustificationControlAdminService)()
            Return service.ListAllJustificationControl()
        End Using
    End Function

    Public Function DeleteJustificationControl(justificationControl As BillingJustificationControl, audit As AuditMessage) As ActionResult Implements IBillingServiceJustificationControl.DeleteJustificationControl
        Using service As IBillingJustificationControlAdminService = Container.Current.Resolve(Of IBillingJustificationControlAdminService)()
            Return service.DeleteJustificationControl(justificationControl, audit)
        End Using
    End Function


    ''' <summary>
    ''' Guarda o actualiza una justificacion
    ''' </summary>
    ''' <param name="billingAuthorization"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveJustificationControl(justificationControl As BillingJustificationControl, audit As AuditMessage, Optional idSequense As Int64 = 0) As ActionResult(Of BillingJustificationControl) Implements IBillingServiceJustificationControl.SaveJustificationControl
        Using service As IBillingJustificationControlAdminService = Container.Current.Resolve(Of IBillingJustificationControlAdminService)()
            Return service.SaveJustificationControl(justificationControl, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una justificacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillingJustification(code As String, audit As AuditMessage) As ActionResult(Of BillingJustificationControl) Implements IBillingServiceJustificationControl.GetBillingJustification
        Using service As IBillingJustificationControlAdminService = Container.Current.Resolve(Of IBillingJustificationControlAdminService)()
            Return service.GetBillingJustification(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una justificacon por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillingJustificationById(id As Integer) As ActionResult(Of BillingJustificationControl) Implements IBillingServiceJustificationControl.GetBillingJustificationById
        Using service As IBillingJustificationControlAdminService = Container.Current.Resolve(Of IBillingJustificationControlAdminService)()
            Return service.GetBillingJustificationById(id)
        End Using
    End Function

End Class
