'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()> _
Public Interface IAccountReceivableConcept
    ''' <summary>
    ''' metodo para obtener todos los conceptos de cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAllAccountReceivableConcept(audit As AuditMessage) As Object

    ''' <summary>
    ''' metodo para obtener un concepto de cuenta por pagar
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountReceivableConceptByCode(code As String, audit As AuditMessage) As Object

    ''' <summary>
    ''' Obtiene un documento de cuenta x cobrar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountReceivableConceptById(id As Integer) As AccountReceivableConcept

    ''' <summary>
    ''' metodo para guardar un concepto de cuenta por cobrar
    ''' </summary>    
    <OperationContract()>
    Function SaveAccountReceivableConcept(accountReceivableConcept As AccountReceivableConcept, idSequense As Int64, audit As AuditMessage) As ActionResult(Of AccountReceivableConcept)

    ''' <summary>
    ''' metodo para eliminar un concepto de cuenta por cobrar
    ''' </summary>    
    <OperationContract()>
    Function DeleteAccountReceivableConcept(accountReceivableConcept As AccountReceivableConcept, audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountReceivableConcept)
End Interface
