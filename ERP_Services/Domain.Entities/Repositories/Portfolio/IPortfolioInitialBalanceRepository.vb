'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

''' <summary>
''' Resumen materializado de la confirmación set-based del saldo inicial.
''' </summary>
Public Class PortfolioInitialBalanceConfirmationSummary
    Public Property PortfolioInitialBalanceId As Integer
    Public Property Staged As Integer
    Public Property Created As Integer
    Public Property OmittedConflicts As Integer
    Public Property AccountReceivableConflicts As Integer
    Public Property BillingInvoiceConflicts As Integer
    Public Property InvoicesCreated As Integer
    Public Property AccountReceivablesCreated As Integer
    Public Property AccountingCreated As Integer
    Public Property SharesCreated As Integer
    Public Property ElectronicDocumentsCreated As Integer
    Public Property ElectronicsPropertiesCreated As Integer
    Public Property ElectronicsRIPSCreated As Integer
    Public Property InitialBalanceInvoicesCreated As Integer
    Public Property HeaderStatus As Byte
End Class

Public Interface IPortfolioInitialBalanceRepository
    Inherits IRepository(Of PortfolioInitialBalance)
    ''' <summary>
    ''' metodo para obtener el saldo inicial por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioInitialBalanceByCode(code As String) As PortfolioInitialBalance
    ''' <summary>
    ''' metodo para obtener el saldo inicial por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioInitialBalanceById(id As Integer) As PortfolioInitialBalance
    ''' <summary>
    ''' Registra un lote importado de facturas sin recorrer el grafo mediante
    ''' el mecanismo genérico de detección de cambios.
    ''' </summary>
    Sub RegisterAccountReceivableImportBatch(portfolioInitialBalance As PortfolioInitialBalance)
    ''' <summary>
    ''' Persiste masivamente el lote importado previamente registrado.
    ''' </summary>
    Sub CommitAccountReceivableImportBatch()
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
    ''' Ejecuta la confirmación set-based y devuelve su resumen persistido.
    ''' </summary>
    Function ConfirmPortfolioInitialBalanceSetBased(idPortfolioInitialBalance As Integer, auditUser As String, electronicDocumentContainer As String) As PortfolioInitialBalanceConfirmationSummary
End Interface
