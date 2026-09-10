'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IConfirmationDocumentsAdminService
    Inherits IDisposable
    ''' <summary>
    ''' metodo para confirmar documentos de presupuesto
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmDocuments(listDocuments As List(Of Tuple(Of Integer, Integer, EBudgetDocumentType)), audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' metodo para confirmar documentos de presupuesto
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmDocumentsExpense(listDocuments As List(Of Tuple(Of Integer, Integer, EBudgetDocumentTypeExpense)), audit As AuditMessage) As ActionResult
End Interface
