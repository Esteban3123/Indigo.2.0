'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez  
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBudgetServiceConfirmationDocuments
    ''' <summary>
    ''' metodo para confirmar documentos de presupuesto ingresos
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmDocuments(listDocuments As List(Of Tuple(Of Integer, Integer, EBudgetDocumentType)), audit As AuditMessage) As Domain.Base.Entities.ActionResult
    ''' <summary>
    ''' metodo para confirmar documentos de presupuesto gastos
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmDocumentsExpense(listDocuments As List(Of Tuple(Of Integer, Integer, EBudgetDocumentTypeExpense)), audit As AuditMessage) As Domain.Base.Entities.ActionResult
End Interface
