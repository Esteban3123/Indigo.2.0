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

Public Class PortfolioMassiveConfirmAdminService
    Implements IPortfolioMassiveConfirmAdminService

    Private _portfolioNoteAdminService As IPortfolioNoteAdminService
    Private _portfolioNoteRepository As IPortfolioNoteRepository
    Private _portfolioTransferRepository As IPortfolioTransferRepository
    Private _portfolioTransfersAdminService As IPortfolioTransfersAdminService
    Private _accountReceivableDocumentRepository As IAccountReceivableDocumentRepository
    Private _accountReceivableDocumentAdminService As IAccountReceivableDocumentAdminService

    Public Sub New(portfolioNoteAdminService As IPortfolioNoteAdminService, portfolioNoteRepository As IPortfolioNoteRepository, portfolioTransferRepository As IPortfolioTransferRepository,
                   portfolioTransfersAdminService As IPortfolioTransfersAdminService, accountReceivableDocumentRepository As IAccountReceivableDocumentRepository,
                   accountReceivableDocumentAdminService As IAccountReceivableDocumentAdminService)
        _portfolioNoteAdminService = portfolioNoteAdminService
        _portfolioNoteRepository = portfolioNoteRepository
        _portfolioTransferRepository = portfolioTransferRepository
        _portfolioTransfersAdminService = portfolioTransfersAdminService
        _accountReceivableDocumentRepository = accountReceivableDocumentRepository
        _accountReceivableDocumentAdminService = accountReceivableDocumentAdminService
    End Sub

    Public Function ConfirmPortfolioDocument(processId As Integer, code As String, audit As AuditMessage, session As SessionValues) As ActionResult(Of Tuple(Of String, Integer)) Implements IPortfolioMassiveConfirmAdminService.ConfirmPortfolioDocument
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing

        Select Case processId
            Case 686 'Notas debito credito
                Return ConfirmPortfolioNotes(code, audit, session)
            Case 687 'Cruce de anticipos vs cxc
                Return ConfirmPortfolioTransfers(code, audit)
            Case 1522 'Cuentas por cobrar
                Return ConfirmAccountReceivableDocument(code, audit)
            Case Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2)
                Return New ActionResult(Of Tuple(Of String, Integer)) With {.ObjectEmbbeded = objectEmbbeded}
        End Select
    End Function

    Public Function ConfirmPortfolioDocuments(processId As Integer, listDocuments As List(Of String), audit As AuditMessage, session As SessionValues) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IPortfolioMassiveConfirmAdminService.ConfirmPortfolioDocuments
        Select Case processId
            Case 686 'Notas debito credito
                Return ConfirmPortfolioNotes(listDocuments, audit, session)
            Case 687 'Cruce de anticipos vs cxc
                Return ConfirmPortfolioTransfers(listDocuments, audit)
            Case 1522 'Cuentas por cobrar
                Return ConfirmAccountReceivableDocument(listDocuments, audit)
            Case Else
                Dim listResult As New List(Of Tuple(Of String, Integer))
                listResult.Add(New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2))
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult}
        End Select
    End Function

    Private Function ConfirmPortfolioNotes(code As String, audit As AuditMessage, session As SessionValues) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _portfolioNoteRepository.ListPortfolioNoteMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            document.Status = 2
            Dim result = _portfolioNoteAdminService.SavePortfolioNote(document, audit, session)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente la nota de cuentas por cobrar " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se confirmo la nota de cuentas por cobrar " & code, 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmPortfolioNotes(listDocuments As List(Of String), audit As AuditMessage, session As SessionValues) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listResult As New List(Of Tuple(Of String, Integer))        
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmPortfolioNotes(code, audit, session)

                                  listResult.Add(result.ObjectEmbbeded)
                                  If result.StateResult Then
                                      listDocumentsConfirmReturn.Add(code)
                                  End If
                              End Sub)
        
        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult, .MessageResult = listDocumentsConfirmReturn}
    End Function

    Private Function ConfirmPortfolioTransfers(code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _portfolioTransferRepository.ListPortfolioTransferMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            Dim result = _portfolioTransfersAdminService.SavePortfolioTransfer(document, audit)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente el cruce de cuentas " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se confirmo el cruce de cuentas " & code, 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmPortfolioTransfers(listDocuments As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listResult As New List(Of Tuple(Of String, Integer))        
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmPortfolioTransfers(code, audit)

                                  listResult.Add(result.ObjectEmbbeded)
                                  If result.StateResult Then
                                      listDocumentsConfirmReturn.Add(code)
                                  End If
                              End Sub)
        
        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult, .MessageResult = listDocumentsConfirmReturn}
    End Function

    Private Function ConfirmAccountReceivableDocument(code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _accountReceivableDocumentRepository.ListAccountReceivableDocumentMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            Dim result = _accountReceivableDocumentAdminService.SaveAccountReceivableDocument(document, audit)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente la cuenta por cobrar " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)(String.Concat("No se confirmo la cuenta por cobrar '", code, "' por: ", result.Message), 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmAccountReceivableDocument(listDocuments As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listResult As New List(Of Tuple(Of String, Integer))        
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmAccountReceivableDocument(code, audit)

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
                _portfolioNoteAdminService.Dispose()
                _portfolioTransfersAdminService.Dispose()
                _accountReceivableDocumentAdminService.Dispose()
            End If
            _portfolioNoteAdminService = Nothing
            _portfolioNoteRepository = Nothing
            _portfolioTransferRepository = Nothing
            _portfolioTransfersAdminService = Nothing
            _accountReceivableDocumentRepository = Nothing
            _accountReceivableDocumentAdminService = Nothing
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