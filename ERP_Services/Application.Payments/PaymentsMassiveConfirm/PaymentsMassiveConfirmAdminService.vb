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

Public Class PaymentsMassiveConfirmAdminService
    Implements IPaymentsMassiveConfirmAdminService

    Private _accountPayableAdminService As IAccountPayableAdminService
    Private _accountPayableRepository As IAccountPayableRepository
    Private _notesDebitCreditAdminService As INotesDebitCreditAdminService
    Private _notesDebitCreditRepository As INotesDebitCreditRepository
    Private _moneyAdvanceRepository As IMoneyAdvanceRepository
    Private _transfersRepository As ITransfersRepository
    Private _transfersAdminService As ITransfersAdminService

    Public Sub New(accountPayableAdminService As IAccountPayableAdminService, accountPayableRepository As IAccountPayableRepository,
                   notesDebitCreditAdminService As INotesDebitCreditAdminService, notesDebitCreditRepository As INotesDebitCreditRepository, moneyAdvanceRepository As IMoneyAdvanceRepository,
                   transfersRepository As ITransfersRepository, transfersAdminService As ITransfersAdminService)
        _accountPayableAdminService = accountPayableAdminService
        _accountPayableRepository = accountPayableRepository
        _notesDebitCreditAdminService = notesDebitCreditAdminService
        _notesDebitCreditRepository = notesDebitCreditRepository
        _moneyAdvanceRepository = moneyAdvanceRepository
        _transfersRepository = transfersRepository
        _transfersAdminService = transfersAdminService
    End Sub

#Region "Methods"

    Public Function ConfirmPaymentDocument(processId As Integer, code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer)) Implements IPaymentsMassiveConfirmAdminService.ConfirmPaymentDocument
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing

        Select Case processId
            Case 727 'Amortizacion mensual
                objectEmbbeded = New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2)
                Return New ActionResult(Of Tuple(Of String, Integer)) With {.ObjectEmbbeded = objectEmbbeded}
            Case 730 'cuentas por pagar
                Return ConfirmAccountPayable(code, audit)
            Case 731 'Nota debito credito
                Return ConfirmPaymentsNotes(code, audit)
            Case 734 'Traslado de facturas
                objectEmbbeded = New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2)
                Return New ActionResult(Of Tuple(Of String, Integer)) With {.ObjectEmbbeded = objectEmbbeded}
            Case 746 'Cruce de antipos vs cxp
                Return ConfirmTransfers(code, audit)
            Case 1505 'Aceptacion traslado
                objectEmbbeded = New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2)
                Return New ActionResult(Of Tuple(Of String, Integer)) With {.ObjectEmbbeded = objectEmbbeded}
            Case Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2)
                Return New ActionResult(Of Tuple(Of String, Integer)) With {.ObjectEmbbeded = objectEmbbeded}
        End Select
    End Function

    Public Function ConfirmPaymentsDocuments(processId As Integer, listDocuments As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IPaymentsMassiveConfirmAdminService.ConfirmPaymentsDocuments
        Select Case processId
            Case 730 'cuentas por pagar
                Return ConfirmAccountPayable(listDocuments, audit)
            Case 731 'Nota debito credito
                Return ConfirmPaymentsNotes(listDocuments, audit)
            Case 746 'Cruce de antipos vs cxp
                Return ConfirmTransfers(listDocuments, audit)
            Case 727 'Amortizacion mensual
                Dim listResult As New List(Of Tuple(Of String, Integer))
                listResult.Add(New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2))
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult}
            Case 734 'Traslado de facturas
                Dim listResult As New List(Of Tuple(Of String, Integer))
                listResult.Add(New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2))
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult}
            Case 1505 'Aceptacion traslado
                Dim listResult As New List(Of Tuple(Of String, Integer))
                listResult.Add(New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2))
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult}
            Case Else
                Dim listResult As New List(Of Tuple(Of String, Integer))
                listResult.Add(New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2))
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult}
        End Select
    End Function

    Private Function ConfirmAccountPayable(code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim listDocuments = _accountPayableRepository.ListAccountPayableMassiveConfirm(New List(Of String)(New String() {code}))

        If listDocuments IsNot Nothing AndAlso listDocuments.Count > 0 Then
            Dim result = _accountPayableAdminService.ConfirmAccountPayable(listDocuments, audit)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente la cuenta por pagar " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se confirmo la Cuenta por Pagar " & code, 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmAccountPayable(listDocuments As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listAccountPayable As New List(Of AccountPayable)

        listDocuments.ForEach(Sub(code)
                                  listAccountPayable.Add(New AccountPayable() With { .Code = code })
                              End Sub)

        Return _accountPayableAdminService.ConfirmAccountPayable(listAccountPayable, audit, True)
    End Function

    Private Function ConfirmPaymentsNotes(code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _notesDebitCreditRepository.ListPaymentNotesMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            Dim listAccountId As List(Of Integer?) = (From a In document.PaymentNotesAccountPayableAdvance Where a.AccountPayableId IsNot Nothing Select a.AccountPayableId).ToList()
            Dim listAccountPayable = _accountPayableRepository.ListAccountPayableById(listAccountId)
            listAccountPayable.ForEach(Sub(x)
                                           x.Adjustment = (From a In document.PaymentNotesAccountPayableAdvance Where a.AccountPayableId IsNot Nothing AndAlso a.AccountPayableId = x.Id Select a.AdjusmentValue).FirstOrDefault
                                           x.HandlesAddModifyDelete = 2
                                       End Sub)
            Dim listAdvanceId As List(Of Integer?) = (From a In document.PaymentNotesAccountPayableAdvance Where a.AdvancePaymentId IsNot Nothing Select a.AdvancePaymentId).ToList()
            Dim listAdvance = _moneyAdvanceRepository.ListMoneyAdvanceById(listAdvanceId)
            Dim result = _notesDebitCreditAdminService.SavePaymentNotesComplete(document, listAccountPayable, listAdvance, True, audit)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente la nota de cuentas por pagar " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se confirmo la nota de cuentas por pagar " & code, 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmPaymentsNotes(listDocuments As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listResult As New List(Of Tuple(Of String, Integer))        
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmPaymentsNotes(code, audit)

                                  listResult.Add(result.ObjectEmbbeded)
                                  If result.StateResult Then
                                      listDocumentsConfirmReturn.Add(code)
                                  End If
                              End Sub)
        
        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult, .MessageResult = listDocumentsConfirmReturn}
    End Function

    Private Function ConfirmTransfers(code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _transfersRepository.ListPaymentTransferMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            document.Status = 2
            Dim result = _transfersAdminService.ConfirmTransfer(document, audit)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente la radicación de cuentas " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se confirmo la radicación de cuentas " & code, 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmTransfers(listDocuments As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listResult As New List(Of Tuple(Of String, Integer))        
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmTransfers(code, audit)

                                  listResult.Add(result.ObjectEmbbeded)
                                  If result.StateResult Then
                                      listDocumentsConfirmReturn.Add(code)
                                  End If
                              End Sub)
        
        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult, .MessageResult = listDocumentsConfirmReturn}
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _accountPayableAdminService = Nothing
            _accountPayableRepository = Nothing
            _notesDebitCreditAdminService = Nothing
            _notesDebitCreditRepository = Nothing
            _moneyAdvanceRepository = Nothing
            _transfersRepository = Nothing
            _transfersAdminService = Nothing
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