'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BudgetService

    ''' <summary>
    ''' obtiene una obligacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetObligationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.Obligation Implements IBudgetServiceObligation.GetObligationByCode
        Using service As IObligationAdminService = Container.Current.Resolve(Of IObligationAdminService)()
            Return service.GetObligationByCode(code, BudgetaryValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene una obligacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetObligationById(id As Integer) As Domain.Entities.Obligation Implements IBudgetServiceObligation.GetObligationById
        Using service As IObligationAdminService = Container.Current.Resolve(Of IObligationAdminService)()
            Return service.GetObligationById(id)
        End Using
    End Function

    ''' <summary>
    ''' metodo para guardar una obligacion
    ''' </summary>
    ''' <param name="Obligation"></param>
    ''' <returns></returns>
    Public Function SaveObligation(Obligation As Domain.Entities.Obligation, listObligationDetailDelete As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Obligation) Implements IBudgetServiceObligation.SaveObligation
        Using service As IObligationAdminService = Container.Current.Resolve(Of IObligationAdminService)()
            Return service.SaveObligation(Obligation, listObligationDetailDelete, audit)
        End Using
    End Function

End Class