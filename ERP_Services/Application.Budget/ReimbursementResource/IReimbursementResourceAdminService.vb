'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 14-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IReimbursementResourceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene un reintegro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetReimbursementResourceByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As ReimbursementResource

    ''' <summary>
    ''' obtiene un reintegro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetReimbursementResourceById(id As Integer) As ReimbursementResource

    ''' <summary>
    ''' Metodo para consultar hasta que punto se puede hacer el reintegro
    ''' </summary>
    ''' <param name="paymentOrderId"></param>
    ''' <returns>1 = Obligación, 2 = Compromiso / Reserva, 3 = Presupuesto</returns>
    ''' <remarks></remarks>
    Function GetReimbursementUntilByPaymentOrderId(paymentOrderId As Integer) As Integer

    ''' <summary>
    ''' Guarda un reintegro
    ''' </summary>
    ''' <param name="reimbursementResource"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveReimbursementResource(reimbursementResource As ReimbursementResource, listReimbursementResourceDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of ReimbursementResource)

End Interface
