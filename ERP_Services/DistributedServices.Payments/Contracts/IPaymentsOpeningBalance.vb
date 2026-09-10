'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsOpeningBalance

    ''' <summary>
    ''' Guarda o Actualiza un saldo inicial
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveOpeningBalance(openingBalance As Domain.Entities.InitialBalance, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.InitialBalance)

    ''' <summary>
    ''' Guarda o Actualiza un saldo inicial
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ConfirmOpeningBalance(openingBalance As Domain.Entities.InitialBalance, modeSaveAndConfirm As Boolean, _idOperativeUnit As Int32, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.InitialBalance)

    ''' <summary>
    ''' Elimina un saldo incial
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteOpeningBalance(openingBalance As Domain.Entities.InitialBalance, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un saldo inicial
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetOpeningBalance(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.InitialBalance)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateOpeningBalance(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.InitialBalance)

    ''' <summary>
    ''' metodo para validar la factura del copyAndPaste
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SetBillsInitialBalance(data As List(Of List(Of String))) As ActionResult(Of List(Of InitialBalanceAccountPayable))

    ''' <summary>
    ''' metodo para validar el anticipo del copyAndPaste
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SetAdvanceInitialBalance(data As List(Of List(Of String))) As ActionResult(Of List(Of InitialBalanceAdvance))

End Interface
