'***********************************************************************
' Assembly         : Domain.Billing
' Author           : Anthony Ocampo
' Created          : 2026-05-06
' Description      : Repositorio cabecera InitialBalanceInvoice (saldos iniciales con RIPS).
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IInitialBalanceInvoiceRepository
    Inherits IRepository(Of InitialBalanceInvoice)

    ''' <summary>
    ''' Obtiene el header InitialBalanceInvoice asociado a una AccountReceivable.
    ''' </summary>
    Function GetByAccountReceivableId(accountReceivableId As Integer) As InitialBalanceInvoice

    ''' <summary>
    ''' Obtiene el header InitialBalanceInvoice por número de factura.
    ''' </summary>
    Function GetByInvoiceNumber(invoiceNumber As String) As InitialBalanceInvoice

    ''' <summary>
    ''' Obtiene el header InitialBalanceInvoice asociado a un Invoice shadow (saldo inicial).
    ''' Su existencia discrimina si un ElectronicDocument corresponde a una factura de saldo inicial.
    ''' </summary>
    Function GetByInvoiceId(invoiceId As Integer) As InitialBalanceInvoice

    ''' <summary>
    ''' Elimina todos los rows InitialBalanceInvoiceDetail asociados a un IBI (idempotencia reupload, ADR-007).
    ''' </summary>
    Sub DeleteDetailsByInitialBalanceInvoiceId(initialBalanceInvoiceId As Integer)

    ''' <summary>
    ''' Inserta un row InitialBalanceInvoiceDetail.
    ''' </summary>
    Sub SaveDetail(detail As InitialBalanceInvoiceDetail)
End Interface
