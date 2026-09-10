'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 14-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BudgetService

    ''' <summary>
    ''' obtiene un reintegro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetReimbursementResourceByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.ReimbursementResource Implements IBudgetServiceReimbursementResource.GetReimbursementResourceByCode
        Using service As IReimbursementResourceAdminService = Container.Current.Resolve(Of IReimbursementResourceAdminService)()
            Return service.GetReimbursementResourceByCode(code, BudgetaryValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un reintegro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetReimbursementResourceById(id As Integer) As Domain.Entities.ReimbursementResource Implements IBudgetServiceReimbursementResource.GetReimbursementResourceById
        Using service As IReimbursementResourceAdminService = Container.Current.Resolve(Of IReimbursementResourceAdminService)()
            Return service.GetReimbursementResourceById(id)
        End Using
    End Function

    ''' <summary>
    ''' Metodo para consultar hasta que punto se puede hacer el reintegro
    ''' </summary>
    ''' <param name="paymentOrderId"></param>
    ''' <returns>1 = Obligación, 2 = Compromiso / Reserva, 3 = Presupuesto</returns>
    ''' <remarks></remarks>
    Public Function GetReimbursementUntilByPaymentOrderId(paymentOrderId As Integer) As Integer Implements IBudgetServiceReimbursementResource.GetReimbursementUntilByPaymentOrderId
        Using service As IReimbursementResourceAdminService = Container.Current.Resolve(Of IReimbursementResourceAdminService)()
            Return service.GetReimbursementUntilByPaymentOrderId(paymentOrderId)
        End Using
    End Function

    ''' <summary>
    ''' Guarda y confirma un documento de reintegro
    ''' </summary>
    ''' <param name="reimbursementResource"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveReimbursementResource(reimbursementResource As ReimbursementResource, listReimbursementResourceDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of ReimbursementResource) Implements IBudgetServiceReimbursementResource.SaveReimbursementResource
        Using service As IReimbursementResourceAdminService = Container.Current.Resolve(Of IReimbursementResourceAdminService)()
            Return service.SaveReimbursementResource(reimbursementResource, listReimbursementResourceDetailDelete, audit)
        End Using
    End Function

End Class