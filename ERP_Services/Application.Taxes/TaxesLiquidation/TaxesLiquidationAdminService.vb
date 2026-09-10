'***********************************************************************
' Assembly         : Application.Taxes
' Author           : Carlos Mario Arias Rubiano
' Created          : 29-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Text
Imports System.Data.Entity.Infrastructure

#End Region

Public Class TaxesLiquidationAdminService
    Implements ITaxesLiquidationAdminService

    'Repositorio para la liquidación de impuestos
    Private _taxesLiquidationRepository As ITaxesLiquidationRepository

    Public Sub New(taxesLiquidationRepository As ITaxesLiquidationRepository)
        _taxesLiquidationRepository = taxesLiquidationRepository
    End Sub

    ''' <summary>
    ''' Guarda la liquidación de impuestos
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="CadastralIdentification"></param>
    ''' <param name="Address"></param>
    ''' <param name="OwnerId"></param>
    ''' <param name="PropertyType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveTaxesLiquidation(Year As Integer, CadastralIdentification As String, CadastralIdentification2 As String, Address As String, Address2 As String, OwnerId As Integer, PropertyType As Integer, audit As AuditMessage) As ActionResult(Of Tuple(Of Integer, String)) Implements ITaxesLiquidationAdminService.SaveTaxesLiquidation
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim result = _taxesLiquidationRepository.SP_SaveTaxesLiquidation(Year, CadastralIdentification, CadastralIdentification2, Address, Address2, OwnerId, PropertyType, audit.CodeUser)
                If result.State = 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of Tuple(Of Integer, String)) With {.Message = result.Message, .StatusCode = eStatusResult.WARNING, .StateResult = False}
                End If

                transaction.Complete()
                Return New ActionResult(Of Tuple(Of Integer, String)) With {.Message = result.Message, .StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = New Tuple(Of Integer, String)(result.TaxesLiquidationId, result.MessagesIds)}

            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Tuple(Of Integer, String)) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex), .StateResult = False}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Confirma la liquidacion de impuestos
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Ids"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmTaxesLiquidation(Year As Integer, Ids As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, String, String, String)) Implements ITaxesLiquidationAdminService.ConfirmTaxesLiquidation
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim result = _taxesLiquidationRepository.SP_ConfirmTaxesLiquidation(Year, Ids, audit.CodeUser)
                If result.State = 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of Tuple(Of String, String, String, String)) With {.Message = result.Message, .StatusCode = eStatusResult.WARNING, .StateResult = False}
                End If

                'result.MessagesIds -> Item1
                'result.MessagesInvoiceNumber -> Item2
                'result.MessagesAccountReceivableCode -> Item3
                'result.MessagesJournalVoucher -> Item4

                'Se arma el mensaje que se va a devover
                Dim messages As New StringBuilder
                messages.AppendLine("Se confirmó correctamente")
                'messages.AppendLine("Se generaron facturas con números: " + result.MessagesInvoiceNumber)
                'messages.AppendLine("Se generaron cuentas por cobrar con códigos: " + result.MessagesAccountReceivableCode)
                messages.AppendLine("Se generaron comprobantes contables " + result.MessagesJournalVoucher)

                transaction.Complete()
                Return New ActionResult(Of Tuple(Of String, String, String, String)) With {.Message = messages.ToString, .StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = New Tuple(Of String, String, String, String)(result.MessagesIds, result.MessagesInvoiceNumber, result.MessagesAccountReceivableCode, result.MessagesJournalVoucher)}

            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Tuple(Of String, String, String, String)) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex), .StateResult = False}
            End Try
        End Using
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _taxesLiquidationRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
