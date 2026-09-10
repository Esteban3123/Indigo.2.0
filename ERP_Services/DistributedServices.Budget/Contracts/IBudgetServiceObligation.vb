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
Public Interface IBudgetServiceObligation
    ''' <summary>
    ''' obtiene una obligacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetObligationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.Obligation
    ''' <summary>
    ''' obtiene una obligacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetObligationById(id As Integer) As Obligation

    ''' <summary>
    ''' metodo para guardar una obligacion
    ''' </summary>
    ''' <param name="Obligation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveObligation(Obligation As Domain.Entities.Obligation, listObligationDetailDelete As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Obligation)
End Interface
