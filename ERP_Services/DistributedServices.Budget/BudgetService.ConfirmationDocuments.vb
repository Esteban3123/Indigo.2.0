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
    ''' metodo para confirmar documentos de presupuesto ingresos
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    Public Function ConfirmDocuments(listDocuments As List(Of Tuple(Of Integer, Integer, EBudgetDocumentType)), audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBudgetServiceConfirmationDocuments.ConfirmDocuments
        Using service As IConfirmationDocumentsAdminService = Container.Current.Resolve(Of IConfirmationDocumentsAdminService)()
            Return service.ConfirmDocuments(listDocuments, audit)
        End Using
    End Function

    ''' <summary>
    ''' metodo para confirmar documentos de presupuesto gastos
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    Public Function ConfirmDocumentsExpense(listDocuments As List(Of Tuple(Of Integer, Integer, EBudgetDocumentTypeExpense)), audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBudgetServiceConfirmationDocuments.ConfirmDocumentsExpense
        Using service As IConfirmationDocumentsAdminService = Container.Current.Resolve(Of IConfirmationDocumentsAdminService)()
            Return service.ConfirmDocumentsExpense(listDocuments, audit)
        End Using
    End Function

End Class