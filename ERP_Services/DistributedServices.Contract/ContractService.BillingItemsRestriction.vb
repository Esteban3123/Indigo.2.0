'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/10/2014
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
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateBillingItemsRestriction(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingItemsRestriction) Implements IContractServiceBillingItemsRestriction.ChangeStateBillingItemsRestriction
        Using service As IBillingItemsRestrictionAdminService = Container.Current.Resolve(Of IBillingItemsRestrictionAdminService)()
            Return service.ChangeStateBillingItemsRestriction(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' elimina la entidad
    ''' </summary>
    ''' <param name="BillingItemsRestriction"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteBillingItemsRestriction(BillingItemsRestriction As Domain.Entities.BillingItemsRestriction, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractServiceBillingItemsRestriction.DeleteBillingItemsRestriction
        Using service As IBillingItemsRestrictionAdminService = Container.Current.Resolve(Of IBillingItemsRestrictionAdminService)()
            Return service.DeleteBillingItemsRestriction(BillingItemsRestriction, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetBillingItemsRestriction(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingItemsRestriction) Implements IContractServiceBillingItemsRestriction.GetBillingItemsRestriction
        Using service As IBillingItemsRestrictionAdminService = Container.Current.Resolve(Of IBillingItemsRestrictionAdminService)()
            Return service.GetBillingItemsRestriction(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene la entidad por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetBillingItemsRestrictionById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingItemsRestriction) Implements IContractServiceBillingItemsRestriction.GetBillingItemsRestrictionById
        Using service As IBillingItemsRestrictionAdminService = Container.Current.Resolve(Of IBillingItemsRestrictionAdminService)()
            Return service.GetBillingItemsRestrictionById(id, audit)
        End Using
    End Function

    ''' <summary>
    ''' guarda la entidad
    ''' </summary>
    ''' <param name="BillingItemsRestriction"></param>
    ''' <param name="ListBillingItemsRestrictionDetail"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveBillingItemsRestriction(BillingItemsRestriction As Domain.Entities.BillingItemsRestriction, ListBillingItemsRestrictionDetail As List(Of BillingItemsRestrictionDetail), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingItemsRestriction) Implements IContractServiceBillingItemsRestriction.SaveBillingItemsRestriction
        Using service As IBillingItemsRestrictionAdminService = Container.Current.Resolve(Of IBillingItemsRestrictionAdminService)()
            Return service.SaveBillingItemsRestriction(BillingItemsRestriction, ListBillingItemsRestrictionDetail, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' obtiene detalles por id de cabecera
    ''' </summary>
    ''' <param name="idHeader"></param>
    ''' <returns></returns>
    Public Function GetItemsRestrictionDetailByIdHeader(idHeader As Integer) As ActionResult(Of List(Of BillingItemsRestrictionDetail)) Implements IContractServiceBillingItemsRestriction.GetItemsRestrictionDetailByIdHeader
        Using service As IBillingItemsRestrictionAdminService = Container.Current.Resolve(Of IBillingItemsRestrictionAdminService)()
            Return service.GetItemsRestrictionDetailByIdHeader(idHeader)
        End Using
    End Function

End Class
