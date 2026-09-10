'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 14-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IReimbursementResourceRepository
    Inherits IRepository(Of ReimbursementResource)

    ''' <summary>
    ''' obtiene un reintegro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetReimbursementResourceByCode(code As String, BudgetaryValidityId As Integer) As ReimbursementResource

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
    ''' Guarda la modificacion
    ''' </summary>
    ''' <param name="ReimbursementResourceXml"></param>
    ''' <param name="ReimbursementResourceDetailForDeleteXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveReimbursementResource(ReimbursementResourceXml As String, ReimbursementResourceDetailForDeleteXml As String, CodeUser As String) As SP_SaveReimbursementResource_Result

End Interface
