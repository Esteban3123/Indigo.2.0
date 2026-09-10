'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class TreasuryMassiveConfirmAdminService
    Implements ITreasuryMassiveConfirmAdminService

    Private _cashReceiptsAdminService As ICashReceiptsAdminService
    Private _voucherTransactionAdminService As IVoucherTransactionAdminService
    Private _voucherTransactionRepository As IVoucherTransactionRepository
    Private _cashReceiptsRepository As ICashReceiptsRepository
    Private _treasuryNoteAdminService As ITreasuryNoteAdminService
    Private _treasuryNoteRepository As ITreasuryNoteRepository
    Private _consignmentTransferRepository As IConsignmentTransferRepository
    Private _consignmentTransferAdminService As IConsignmentTransferAdminService
    Private _refundRepository As IRefundRepository
    Private _refundAdminService As IRefundAdminService

    Public Sub New(cashReceiptsAdminService As ICashReceiptsAdminService, voucherTransactionAdminService As IVoucherTransactionAdminService,
                   voucherTransactionRepository As IVoucherTransactionRepository, cashReceiptsRepository As ICashReceiptsRepository, treasuryNoteRepository As ITreasuryNoteRepository,
                   treasuryNoteAdminService As ITreasuryNoteAdminService, consignmentTransferRepository As IConsignmentTransferRepository, consignmentTransferAdminService As IConsignmentTransferAdminService,
                   refundRepository As IRefundRepository, refundAdminService As IRefundAdminService)
        _cashReceiptsAdminService = cashReceiptsAdminService
        _voucherTransactionAdminService = voucherTransactionAdminService
        _voucherTransactionRepository = voucherTransactionRepository
        _cashReceiptsRepository = cashReceiptsRepository
        _treasuryNoteRepository = treasuryNoteRepository
        _treasuryNoteAdminService = treasuryNoteAdminService
        _consignmentTransferRepository = consignmentTransferRepository
        _consignmentTransferAdminService = consignmentTransferAdminService
        _refundRepository = refundRepository
        _refundAdminService = refundAdminService
    End Sub

    Public Function ConfirmTreasuryDocument(processId As Integer, code As String, audit As AuditMessage, session As SessionValues) As ActionResult(Of Tuple(Of String, Integer)) Implements ITreasuryMassiveConfirmAdminService.ConfirmTreasuryDocument
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing

        Select Case processId
            Case 635 'Recibos de Caja
                Return ConfirmCashReceipts(code, audit, session)
            Case 636 'Comprobante de egreso
                Return ConfirmVoucherTransaction(code, audit)
            Case 637 'Notas
                Return ConfirmTreasuryNote(code, audit)
            Case 638 'Consignaciones
                Return ConfirmConsignment(code, audit)
            Case 639 'Reembolso
                Return ConfirmRefunds(code, audit)
            Case 640 'Cruce de Cuentas
                objectEmbbeded = New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2)
                Return New ActionResult(Of Tuple(Of String, Integer)) With {.ObjectEmbbeded = objectEmbbeded}
            Case 642 'Dispersion de Fondos
                objectEmbbeded = New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2)
                Return New ActionResult(Of Tuple(Of String, Integer)) With {.ObjectEmbbeded = objectEmbbeded}
            Case Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2)
                Return New ActionResult(Of Tuple(Of String, Integer)) With {.ObjectEmbbeded = objectEmbbeded}
        End Select
    End Function

    Public Function ConfirmTreasuryDocuments(processId As Integer, listDocuments As List(Of String), audit As AuditMessage, IndigoSessionValues As SessionValues) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements ITreasuryMassiveConfirmAdminService.ConfirmTreasuryDocuments
        Select Case processId
            Case 635 'Recibos de Caja
                Return ConfirmCashReceipts(listDocuments, audit, IndigoSessionValues)
            Case 636 'Comprobante de egreso
                Return ConfirmVoucherTransaction(listDocuments, audit)
            Case 637 'Notas
                Return ConfirmTreasuryNote(listDocuments, audit)
            Case 638 'Consignaciones
                Return ConfirmConsignment(listDocuments, audit)
            Case 639 'Reembolso
                Return ConfirmRefunds(listDocuments, audit)
            Case 640 'Cruce de Cuentas
                Dim listResult As New List(Of Tuple(Of String, Integer))
                listResult.Add(New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2))
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult}
            Case 642 'Dispersion de Fondos
                Dim listResult As New List(Of Tuple(Of String, Integer))
                listResult.Add(New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2))
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult}
            Case Else
                Dim listResult As New List(Of Tuple(Of String, Integer))
                listResult.Add(New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2))
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult}
        End Select
    End Function

    Private Function ConfirmCashReceipts(code As String, audit As AuditMessage, session As SessionValues) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _cashReceiptsRepository.ListCashReceiptsMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            document.Status = 2
            Dim result = _cashReceiptsAdminService.SaveCashReceipts(document, audit)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente el recibo de caja " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se confirmo el recibo de caja " & code, 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmCashReceipts(listDocuments As List(Of String), audit As AuditMessage, IndigoSessionValues As SessionValues) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listResult As New List(Of Tuple(Of String, Integer))
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmCashReceipts(code, audit, IndigoSessionValues)

                                  listResult.Add(result.ObjectEmbbeded)
                                  If result.StateResult Then
                                      listDocumentsConfirmReturn.Add(code)
                                  End If
                              End Sub)

        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult, .MessageResult = listDocumentsConfirmReturn}
    End Function

    ''' <summary>
    ''' confirmacion de comprobantes de egreso
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''

    Private Function ConfirmVoucherTransaction(code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _voucherTransactionRepository.ListVoucherTransactionMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            document.Value -= document.TaxByMilValue
            Dim result = _voucherTransactionAdminService.SaveVoucherTransaction(document, audit, True)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente el comprobante de egreso " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se confirmo el comprobante de egreso " & code, 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmVoucherTransaction(listDocuments As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listResult As New List(Of Tuple(Of String, Integer))
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmVoucherTransaction(code, audit)

                                  listResult.Add(result.ObjectEmbbeded)
                                  If result.StateResult Then
                                      listDocumentsConfirmReturn.Add(code)
                                  End If
                              End Sub)

        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult, .MessageResult = listDocumentsConfirmReturn}
    End Function

    Private Function ConfirmTreasuryNote(code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _treasuryNoteRepository.ListTreasuryNoteMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            Dim result = _treasuryNoteAdminService.ConfirmTreasuryNote(1, audit, document)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente la nota de tesoreria " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se confirmo la nota de tesoreria " & code, 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmTreasuryNote(listDocuments As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listResult As New List(Of Tuple(Of String, Integer))
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmTreasuryNote(code, audit)

                                  listResult.Add(result.ObjectEmbbeded)
                                  If result.StateResult Then
                                      listDocumentsConfirmReturn.Add(code)
                                  End If
                              End Sub)

        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult, .MessageResult = listDocumentsConfirmReturn}
    End Function

    Private Function ConfirmConsignment(code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _consignmentTransferRepository.ListConsignmentMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            Dim result = _consignmentTransferAdminService.ConfirmConsignmentTransfer(1, audit, document)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente la consignación " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se confirmo la consignación " & code, 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmConsignment(listDocuments As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listResult As New List(Of Tuple(Of String, Integer))
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmConsignment(code, audit)

                                  listResult.Add(result.ObjectEmbbeded)
                                  If result.StateResult Then
                                      listDocumentsConfirmReturn.Add(code)
                                  End If
                              End Sub)

        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult, .MessageResult = listDocumentsConfirmReturn}
    End Function

    Private Function ConfirmRefunds(code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _refundRepository.ListRefundMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            Dim result = _refundAdminService.ConfirmRefund(1, audit, 0, document)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente el reembolso " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se confirmo el reembolso " & code, 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmRefunds(listDocuments As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listResult As New List(Of Tuple(Of String, Integer))
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmRefunds(code, audit)

                                  listResult.Add(result.ObjectEmbbeded)
                                  If result.StateResult Then
                                      listDocumentsConfirmReturn.Add(code)
                                  End If
                              End Sub)

        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult, .MessageResult = listDocumentsConfirmReturn}
    End Function

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _cashReceiptsAdminService.Dispose()
                _voucherTransactionAdminService.Dispose()
                _treasuryNoteAdminService.Dispose()
                _consignmentTransferAdminService.Dispose()
                _refundAdminService.Dispose()
            End If
            _cashReceiptsAdminService = Nothing
            _voucherTransactionAdminService = Nothing
            _voucherTransactionRepository = Nothing
            _cashReceiptsRepository = Nothing
            _treasuryNoteRepository = Nothing
            _treasuryNoteAdminService = Nothing
            _consignmentTransferRepository = Nothing
            _consignmentTransferAdminService = Nothing
            _refundRepository = Nothing
            _refundAdminService = Nothing
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