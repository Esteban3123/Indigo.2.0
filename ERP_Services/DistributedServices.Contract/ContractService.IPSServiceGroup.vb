'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Contract
Imports Microsoft.Practices.Unity

Partial Class ContractService
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Function ChangeStateIPSServiceGroup(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingConcept) Implements IContractServiceIPSServiceGroup.ChangeStateIPSServiceGroup
        Using service As IIPSServiceGroupAdminService = Container.Current.Resolve(Of IIPSServiceGroupAdminService)()
            Return service.ChangeStateIPSServiceGroup(code, state, audit)
        End Using
        'Return Me._iPSServiceGroupAdminService.ChangeStateIPSServiceGroup(code, state, audit)
    End Function

    ''' <summary>
    ''' Elimina una IPSServiceGroup
    ''' </summary>
    ''' <param name="IPSServiceGroup"></param>
    ''' <returns></returns>
    Public Function DeleteIPSServiceGroup(IPSServiceGroup As Domain.Entities.BillingConcept, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractServiceIPSServiceGroup.DeleteIPSServiceGroup
        Using service As IIPSServiceGroupAdminService = Container.Current.Resolve(Of IIPSServiceGroupAdminService)()
            Return service.DeleteIPSServiceGroup(IPSServiceGroup, audit)
        End Using
        'Return Me._iPSServiceGroupAdminService.DeleteIPSServiceGroup(IPSServiceGroup, audit)
    End Function

    ''' <summary>
    ''' Obtiene una IPSServiceGroup por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetIPSServiceGroup(code As String, audit As AuditMessage) As Domain.Entities.BillingConcept Implements IContractServiceIPSServiceGroup.GetIPSServiceGroup
        Using service As IIPSServiceGroupAdminService = Container.Current.Resolve(Of IIPSServiceGroupAdminService)()

            Return service.GetIPSServiceGroup(code, audit)
        End Using
        'Return Me._iPSServiceGroupAdminService.GetIPSServiceGroup(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene una IPSServiceGroup por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetIPSServiceGroupById(id As Integer) As Domain.Entities.BillingConcept Implements IContractServiceIPSServiceGroup.GetIPSServiceGroupById
        Using service As IIPSServiceGroupAdminService = Container.Current.Resolve(Of IIPSServiceGroupAdminService)()
            Return service.GetIPSServiceGroupById(id)
        End Using
        'Return Me._iPSServiceGroupAdminService.GetIPSServiceGroupById(id)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una IPSServiceGroup
    ''' </summary>
    ''' <param name="IPSServiceGroup"></param>
    ''' <returns></returns>
    Public Function SaveIPSServiceGroup(IPSServiceGroup As Domain.Entities.BillingConcept, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingConcept) Implements IContractServiceIPSServiceGroup.SaveIPSServiceGroup
        Using service As IIPSServiceGroupAdminService = Container.Current.Resolve(Of IIPSServiceGroupAdminService)()
            Return service.SaveIPSServiceGroup(IPSServiceGroup, audit, idSequense)
        End Using
        'Return Me._iPSServiceGroupAdminService.SaveIPSServiceGroup(IPSServiceGroup, audit, idSequense)
    End Function

    ''' <summary>
    ''' Copia y pega los centros de costo por sucursal y unidad funcional
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function CopyAndPasteBillingConceptCostCenter(data As List(Of List(Of String))) As ActionResult(Of List(Of BillingConceptCostCenter)) Implements IContractServiceIPSServiceGroup.CopyAndPasteBillingConceptCostCenter
        Using service As IIPSServiceGroupAdminService = Container.Current.Resolve(Of IIPSServiceGroupAdminService)()
            Return service.CopyAndPasteBillingConceptCostCenter(data)
        End Using
    End Function
End Class
