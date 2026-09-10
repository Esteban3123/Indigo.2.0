'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface ICommonERPBank

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns>Lista de bancos</returns>
    <OperationContract()> _
    Function ListAllBank(session As SessionValues) As List(Of Bank)

    ''' <summary>
    ''' Elimina un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteBank(ByVal bank As Bank, session As SessionValues) As ActionMessageResult(Of Bank)

    ''' <summary>
    ''' Guarda o edita un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveBank(ByVal bank As Bank, session As SessionValues, idSequence As Long) As ActionResult(Of Bank)

    <OperationContract()>
    Function UpdateStateBank(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Bank)

    ''' <summary>
    ''' Obtiene un Banco especifico
    ''' </summary>
    ''' <param name="code">Código de el banco</param>
    ''' <returns> Banco</returns>
    <OperationContract()> _
    Function GetBank(ByVal code As String, session As SessionValues) As Bank

    ''' <summary>
    ''' Obtiene un banco por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBankById(ByVal Id As Integer, session As SessionValues) As Bank

End Interface
