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
Public Interface IPortfolioServicePortfolioInitialBalance
    ''' <summary>
    ''' metodo para obtener el saldo inicial por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioInitialBalanceByCode(code As String, audit As AuditMessage) As Domain.Entities.PortfolioInitialBalance
    ''' <summary>
    ''' metodo para obtener el saldo inicial por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioInitialBalanceById(id As Integer) As PortfolioInitialBalance
    ''' <summary>
    ''' metodo para obtener las facturas del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of PortfolioInitialBalanceAccountReceivable)
    ''' <summary>
    ''' metodo para obtener los anticipos del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of PortfolioInitialBalanceAdvance)
    ''' <summary>
    ''' guardar saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SavePortfolioInitialBalance(PortfolioInitialBalance As Domain.Entities.PortfolioInitialBalance, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioInitialBalance)
    ''' <summary>
    ''' confirmar el saldo incial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmPortfolioInitialBalance(idPortfolioInitialBalance As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of String)
    ''' <summary>
    ''' metodo para guardar y confirmar el saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAndConfirmPortfolioInitialBalance(PortfolioInitialBalance As Domain.Entities.PortfolioInitialBalance, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of String)
    ''' <summary>
    ''' metodo para establecer las facturas que se pegaron en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SetBillsCopyPaste(data As List(Of List(Of String)), companyType As Integer) As ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable))
    ''' <summary>
    ''' metodo para establecer los anticipos en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SetAdvanceCopyPaste(data As List(Of List(Of String))) As ActionResult(Of List(Of PortfolioInitialBalanceAdvance))

    ''' <summary>
    ''' metodo para validar la carga del archivo para los saldo iniciales
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ValidateFileBillsInitialBalance(data As List(Of ImportFileRow), companyType As Integer) As ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable))
End Interface
