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
Public Interface IBudgetServiceAnnualizedCashFlow

    ''' <summary>
    ''' Guarda o Actualiza el presupuesto inicial
    ''' </summary>
    ''' <param name="ListAnnualizedCashFlow">la entidad</param>
    ''' <param name="state">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function SaveListAnnualizedCashFlowHeader(ListAnnualizedCashFlow As List(Of AnnualizedCashFlow), state As Integer, type As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of AnnualizedCashFlow))

    ''' <summary>
    ''' Obtener los rubros del PAC inicial 
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="CodeCategory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAnnualizedCashFlowByValidityIdAndByCodeCategory(ValidityId As Integer, CodeCategory As String, type As Integer, audit As AuditMessage) As ActionResult(Of List(Of Domain.Entities.AnnualizedCashFlow))
    ''' <summary>
    ''' Obtener todos los registros de PAC inicial por vigencia 
    ''' </summary>
    ''' <param name="ValidityId"></param>
    <OperationContract()> _
    Function GetAnnualizedCashFlowByValidityId(ValidityId As Integer) As List(Of AnnualizedCashFlow)
End Interface
