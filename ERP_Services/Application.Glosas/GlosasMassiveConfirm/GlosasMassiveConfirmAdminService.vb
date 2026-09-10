'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Carlos Ernesto Cordoba
' Created          : 30-03-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class GlosasMassiveConfirmAdminService
    Implements IGlosasMassiveConfirmAdminService

    Private _radicateInvoiceCRepository As IRadicateInvoiceCRepository
    Private _radicateInvoiceAdminService As IRadicateInvoiceAdminService
    Private _transferJuridicalDebtCRepository As ITransferJuridicalDebtCRepository
    Private _transferJuridicalDebtCAdminService As ITransferJuridicalDebtCAdminService

    Public Sub New(radicateInvoiceCRepository As IRadicateInvoiceCRepository, radicateInvoiceAdminService As IRadicateInvoiceAdminService, transferJuridicalDebtCAdminService As ITransferJuridicalDebtCAdminService,
                   transferJuridicalDebtCRepository As ITransferJuridicalDebtCRepository)
        _radicateInvoiceCRepository = radicateInvoiceCRepository
        _radicateInvoiceAdminService = radicateInvoiceAdminService
        _transferJuridicalDebtCAdminService = transferJuridicalDebtCAdminService
        _transferJuridicalDebtCRepository = transferJuridicalDebtCRepository
    End Sub

#Region "Methods"

    Public Function ConfirmGlosaDocument(processId As Integer, code As String, audit As AuditMessage, indigoSessionValues As SessionValues, operativeUnitId As Integer) As ActionResult(Of Tuple(Of String, Integer)) Implements IGlosasMassiveConfirmAdminService.ConfirmGlosaDocument
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing

        Select Case processId
            Case 508 'recepcion de objeciones                
                objectEmbbeded = New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2)
                Return New ActionResult(Of Tuple(Of String, Integer)) With {.ObjectEmbbeded = objectEmbbeded}
            Case 509 'Radicacion de cuentas
                Return ConfirmInvoiceRadicate(code, indigoSessionValues, operativeUnitId)
            Case 522 'conciliaciones
                objectEmbbeded = New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2)
                Return New ActionResult(Of Tuple(Of String, Integer)) With {.ObjectEmbbeded = objectEmbbeded}
            Case 582 'traslado a cobro juridico
                Return ConfirmTransferJuridicalDebtC(code, indigoSessionValues, operativeUnitId)
            Case Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2)
                Return New ActionResult(Of Tuple(Of String, Integer)) With {.ObjectEmbbeded = objectEmbbeded}
        End Select
    End Function

    Public Function ConfirmGlosasDocuments(processId As Integer, listDocuments As List(Of String), audit As AuditMessage, IndigoSessionValues As SessionValues, operativeUnitId As Integer) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IGlosasMassiveConfirmAdminService.ConfirmGlosasDocuments
        Select Case processId
            Case 509 'Radicacion de cuentas
                Return ConfirmInvoiceRadicate(listDocuments, IndigoSessionValues, operativeUnitId)
            Case 508 'recepcion de objeciones
                Dim listResult As New List(Of Tuple(Of String, Integer))
                listResult.Add(New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2))
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult}
            Case 522 'conciliaciones
                Dim listResult As New List(Of Tuple(Of String, Integer))
                listResult.Add(New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2))
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult}
            Case 582 'traslado a cobro juridico
                Return ConfirmTransferJuridicalDebtC(listDocuments, IndigoSessionValues, operativeUnitId)
            Case Else
                Dim listResult As New List(Of Tuple(Of String, Integer))
                listResult.Add(New Tuple(Of String, Integer)("No se encontraron documentos para procesar ", 2))
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult}
        End Select
    End Function

    Private Function ConfirmInvoiceRadicate(code As String, IndigoSessionValues As SessionValues, operativeUnitId As Integer) As ActionResult(Of Tuple(Of String, Integer))
        Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _radicateInvoiceCRepository.ListRadicateInvoiceMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            document.State = "2"
            document.OperatingUnitId = operativeUnitId
            document.MarkAsModified()
            Dim result = _radicateInvoiceAdminService.ConfirmInvoiceRadicateC(document, 0, IndigoSessionValues)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente la radicación de cuentas " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se confirmo la radicación de cuentas {0} por: {1}", code, result.Message), 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmInvoiceRadicate(listDocuments As List(Of String), IndigoSessionValues As SessionValues, operativeUnitId As Integer) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listResult As New List(Of Tuple(Of String, Integer))
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmInvoiceRadicate(code, IndigoSessionValues, operativeUnitId)

                                  listResult.Add(result.ObjectEmbbeded)
                                  If result.StateResult Then
                                      listDocumentsConfirmReturn.Add(code)
                                  End If
                              End Sub)

        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = listResult, .MessageResult = listDocumentsConfirmReturn}
    End Function

    Private Function ConfirmTransferJuridicalDebtC(code As String, IndigoSessionValues As SessionValues, operativeUnitId As Integer) As ActionResult(Of Tuple(Of String, Integer))
         Dim stateResult As Boolean = False
        Dim objectEmbbeded As Tuple(Of String, Integer) = Nothing
        Dim documents = _transferJuridicalDebtCRepository.ListTransferJuridicalDebtCollectionCMassiveConfirm(New List(Of String)(New String() {code}))

        If documents IsNot Nothing AndAlso documents.Count > 0 Then
            Dim document = documents.First()
            document.OperatingUnitId = operativeUnitId
            Dim result = _transferJuridicalDebtCAdminService.ConfirmTransferJuridicalDebt(document, IndigoSessionValues)
            If result.StateResult = True Then
                stateResult = True
                objectEmbbeded = New Tuple(Of String, Integer)("Se confirmo correctamente el traslado a cobro juridico " & code, 1)
            Else
                objectEmbbeded = New Tuple(Of String, Integer)("No se confirmo el traslado a cobro juridico " & code, 2)
            End If
        Else
            objectEmbbeded = New Tuple(Of String, Integer)(String.Format("No se encontro el documento con código '{0}'", code), 2)
        End If

        Return New ActionResult(Of Tuple(Of String, Integer)) With {.StateResult = stateResult, .ObjectEmbbeded = objectEmbbeded}
    End Function

    Private Function ConfirmTransferJuridicalDebtC(listDocuments As List(Of String), IndigoSessionValues As SessionValues, operativeUnitId As Integer) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim listResult As New List(Of Tuple(Of String, Integer))        
        Dim listDocumentsConfirmReturn As New List(Of String)

        listDocuments.ForEach(Sub(code)
                                  Dim result = Me.ConfirmTransferJuridicalDebtC(code, IndigoSessionValues, operativeUnitId)

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
                _transferJuridicalDebtCAdminService.Dispose()
            End If
            _radicateInvoiceCRepository = Nothing
            _radicateInvoiceAdminService = Nothing
            _transferJuridicalDebtCAdminService = Nothing
            _transferJuridicalDebtCRepository = Nothing
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