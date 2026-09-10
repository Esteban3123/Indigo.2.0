'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Hector Rodriguez
' Created          : 05/11/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface ITreasuryServiceCashFlowConcept

#Region "Methods"

    ''' <summary>
    ''' metodo para obtener un concepto de flujo de efectivo por codigo
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCashFlowConceptByCode(code As String, audit As AuditMessage) As ActionResult(Of CashFlowConcept)

    ''' <summary>
    ''' Obtiene un concepto de flujo de efectivo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetCashFlowConceptById(id As Integer) As ActionResult(Of CashFlowConcept)

    ''' <summary>
    ''' metodo para guardar un concepto de flujo de efectivo
    ''' </summary>    
    <OperationContract()>
    Function SaveCashFlowConcept(CashFlowConcept As CashFlowConcept, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CashFlowConcept)

    ''' <summary>
    ''' metodo para eliminar un concepto de flujo de efectivo
    ''' </summary>    
    <OperationContract()>
    Function DeleteCashFlowConcept(CashFlowConcept As CashFlowConcept, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateCashFlowConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CashFlowConcept)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="parameters"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListCashFlowStatus(parameters As String, session As Infrastructure.CrossCutting.Base.SessionValues) As System.Data.DataSet

#End Region

End Interface
