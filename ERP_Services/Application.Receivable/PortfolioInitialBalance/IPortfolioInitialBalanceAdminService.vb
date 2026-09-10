'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPortfolioInitialBalanceAdminService
    Inherits IDisposable
    ''' <summary>
    ''' metodo para obtener el saldo inicial por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioInitialBalanceByCode(code As String, audit As AuditMessage) As PortfolioInitialBalance
    ''' <summary>
    ''' metodo para obtener el saldo inicial por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioInitialBalanceById(id As Integer) As PortfolioInitialBalance
    ''' <summary>
    ''' metodo para obtener las facturas del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of PortfolioInitialBalanceAccountReceivable)
    ''' <summary>
    ''' metodo para obtener los anticipos del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of PortfolioInitialBalanceAdvance)
    ''' <summary>
    ''' guardar saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePortfolioInitialBalance(PortfolioInitialBalance As PortfolioInitialBalance, audit As AuditMessage, Optional idSequence As Integer = 0) As ActionResult(Of PortfolioInitialBalance)
    ''' <summary>
    ''' confirmar el saldo incial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmPortfolioInitialBalance(idPortfolioInitialBalance As Integer, audit As AuditMessage) As ActionResult(Of String)
    ''' <summary>
    ''' metodo para guardar y confirmar el saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Function SaveAndConfirmPortfolioInitialBalance(PortfolioInitialBalance As PortfolioInitialBalance, audit As AuditMessage, Optional idSequence As Integer = 0) As ActionResult(Of String)
    ''' <summary>
    ''' metodo para establecer las facturas que se pegaron en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetBillsCopyPaste(data As List(Of List(Of String)), companyType As Integer) As ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable))
    ''' <summary>
    ''' metodo para establecer los anticipos en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetAdvanceCopyPaste(data As List(Of List(Of String))) As ActionResult(Of List(Of PortfolioInitialBalanceAdvance))
    ''' <summary>
    ''' metodo para validar la carga del archivo para los saldo iniciales
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateFileBillsInitialBalance(data As List(Of ImportFileRow), companyType As Integer) As ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable))
End Interface
