Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

#End Region

Public Class AccountingMassiveConfirmAdminService
    Implements IAccountingMassiveConfirmAdminService

    Private _accountingDocumentAdminService As IAccountingDocumentAdminService
    Private _accountingDocumentRepository As IAccountingDocumentRepository

    Public Sub New(accountingDocumentAdminService As IAccountingDocumentAdminService, accountingDocumentRepository As IAccountingDocumentRepository)
        _accountingDocumentAdminService = accountingDocumentAdminService
        _accountingDocumentRepository = accountingDocumentRepository
    End Sub

    Public Function ConfirmAccountingDocument(code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer)) Implements IAccountingMassiveConfirmAdminService.ConfirmAccountingDocument
        Return ConfirmJournalVoucher(code, audit)
    End Function

    Public Function ConfirmAccountingDocuments(listDocuments As List(Of String), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IAccountingMassiveConfirmAdminService.ConfirmAccountingDocuments
        Dim listResult As New List(Of Tuple(Of String, Integer))        
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmJournalVoucher(code, audit)

                                  listResult.Add(result.ObjectEmbbeded)
                                  If result.StateResult Then
                                      listDocumentsConfirmReturn.Add(code)
                                  End If
                              End Sub)
        
        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult, .MessageResult = listDocumentsConfirmReturn}
    End Function

    Private Function ConfirmJournalVoucher(code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _accountingDocumentRepository.ListJournalVouchersMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            document.Status = 2
            Dim result = _accountingDocumentAdminService.SaveAccountingDocument(document, audit)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente el comprobante contable " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se confirmo el comprobante contable " & code, 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _accountingDocumentAdminService.Dispose()
            End If
            _accountingDocumentAdminService = Nothing
            _accountingDocumentRepository = Nothing
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
