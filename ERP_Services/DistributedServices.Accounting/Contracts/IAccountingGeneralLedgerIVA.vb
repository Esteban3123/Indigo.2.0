'***********************************************************************
' Assembly         : DistributedServices.Accounting
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
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
Public Interface IAccountingGeneralLedgerIVA

#Region "Methods"

    ''' <summary>
    ''' obtiene el iva por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetGeneralLedgerIVAByCode(code As String) As GeneralLedgerIVA

    ''' <summary>
    ''' obtiene el iva por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetGeneralLedgerIVAById(id As Integer, Optional auditParam As AuditMessage = Nothing) As ActionResult(Of GeneralLedgerIVA)

    ''' <summary>
    ''' Graba un Iva
    ''' </summary>
    ''' <param name="doc">Iva a grabar</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function SaveGeneralLedgerIVA(ByVal doc As GeneralLedgerIVA) As ActionResult(Of GeneralLedgerIVA)

    ''' <summary>
    ''' Actualiza el estado de un Iva
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateGeneralLedgerIva(ByVal code As String, ByVal state As Boolean) As ActionResult(Of GeneralLedgerIVA)

    ''' <summary>
    ''' Elimina un Iva
    ''' </summary>
    ''' <param name="doc">IVA a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function DeleteGeneralLedgerIVA(ByVal doc As GeneralLedgerIVA) As ActionResult

#End Region

End Interface
