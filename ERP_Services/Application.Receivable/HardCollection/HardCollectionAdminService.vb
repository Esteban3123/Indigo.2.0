'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Validation
Imports Domain.Entities.Service
Imports System.Text

#End Region

Public Class HardCollectionAdminService
    Implements IHardCollectionAdminService



    Private _hardCollectionRepository As IHardCollectionRepository
    Private _portfolioService As IPortfolioService

    Public Sub New(hardCollectionRepository As IHardCollectionRepository, portfolioService As IPortfolioService)
        _hardCollectionRepository = hardCollectionRepository
        _portfolioService = portfolioService
    End Sub

    Public Function GetHardCollectionByCode(code As String, audit As AuditMessage) As HardCollection Implements IHardCollectionAdminService.GetHardCollectionByCode
        Try
            Dim hardCollection = _hardCollectionRepository.GetHardCollectionByCode(code)
            If hardCollection IsNot Nothing AndAlso hardCollection.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of HardCollection)(hardCollection, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return hardCollection
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New HardCollection
        End Try
    End Function

    Public Function SaveHardCollection(HardCollection As HardCollection, audit As AuditMessage) As ActionResult(Of HardCollection) Implements IHardCollectionAdminService.SaveHardCollection
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = HardCollection.ToXML()

                Dim result = _hardCollectionRepository.GenerateHardCollectionSP(xml, audit.CodeUser).ToList().ElementAt(0)
                If result.CodeMessage = "999" Then
                    Return New ActionResult(Of HardCollection) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = result.Message}
                Else
                    Dim hardCollectionReturn = _hardCollectionRepository.GetHardCollectionById(result.HardCollectionId)
                    Transaction.Complete()
                    If result.Message.Length = 0 Then
                        Return New ActionResult(Of HardCollection) With {.StateResult = True, .ObjectEmbbeded = hardCollectionReturn}
                    Else
                        Return New ActionResult(Of HardCollection) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = hardCollectionReturn}
                    End If

                End If

                Return New ActionResult(Of HardCollection) With {.StatusCode = eStatusResult.SUCCESS, .Message = "Proceso Finalizado Correctamente"}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult(Of HardCollection) With {.StatusCode = eStatusResult.EXCEPTION, .StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            Catch ex As DbEntityValidationException
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of HardCollection) With {.StatusCode = eStatusResult.EXCEPTION, .StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of HardCollection) With {.StatusCode = eStatusResult.EXCEPTION, .StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    Public Function GetHardCollectionDetailByHardCollectionId(hardCollectionId As Integer) As List(Of HardCollectionDetail) Implements IHardCollectionAdminService.GetHardCollectionDetailByHardCollectionId
        Try
            Return _hardCollectionRepository.GetHardCollectionDetailByHardCollectionId(hardCollectionId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of HardCollectionDetail)
        End Try
    End Function

    Public Function SetCopyPasteOrImportFileHardCollection(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of HardCollectionDetail)) Implements IHardCollectionAdminService.SetCopyPasteOrImportFileHardCollection
        If dataImportFile IsNot Nothing Then
            Return SetImportFileHardCollection(dataImportFile)
        Else
            Return SetCopyPasteHardCollection(dataCopyPaste)
        End If
    End Function

    Private Function SetCopyPasteHardCollection(dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of HardCollectionDetail))
        Try
            Dim invoiceXml As New StringBuilder
            invoiceXml.Append("<HardCollectionInvoice>")
            For i As Integer = 0 To dataCopyPaste.Count - 1 Step 1
                invoiceXml.Append("<InvoiceNumber>")
                invoiceXml.Append(dataCopyPaste.Item(i).Item(0))
                invoiceXml.Append("</InvoiceNumber>")
            Next
            invoiceXml.Append("</HardCollectionInvoice>")
            Dim result = _hardCollectionRepository.SetInvoicesHardCollection(invoiceXml.ToString()).ToList()
            Dim listDetailsResult As New List(Of HardCollectionDetail)
            Dim listErros As New List(Of String)

            For Each item In result.FindAll(Function(x) x.Status = 1)
                Dim hardCollectionDetail = New HardCollectionDetail
                With hardCollectionDetail
                    .AccountReceivableId = item.AccountReceivableId
                    .Balance = item.Balance
                    .InvoiceNumber = item.InvoiceNumber
                End With
                listDetailsResult.Add(hardCollectionDetail)
            Next
            listErros.AddRange(From e In result Where e.Status = 2 Select e.Message)

            Return New ActionResult(Of List(Of HardCollectionDetail)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listDetailsResult, .MessageResult = listErros}
        Catch ex As Exception
            Return New ActionResult(Of List(Of HardCollectionDetail)) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Private Function SetImportFileHardCollection(data As List(Of ImportFileRow)) As ActionResult(Of List(Of HardCollectionDetail))
        Try
            Dim invoiceXml As New StringBuilder
            invoiceXml.Append("<HardCollectionInvoice>")
            For Each row In data
                invoiceXml.Append("<InvoiceNumber>")
                invoiceXml.Append(row.Row.Item(0))
                invoiceXml.Append("</InvoiceNumber>")
            Next
            invoiceXml.Append("</HardCollectionInvoice>")
            Dim result = _hardCollectionRepository.SetInvoicesHardCollection(invoiceXml.ToString()).ToList()
            Dim listDetailsResult As New List(Of HardCollectionDetail)
            Dim listErros As New List(Of String)

            For Each item In result.FindAll(Function(x) x.Status = 1)
                Dim hardCollectionDetail = New HardCollectionDetail
                With hardCollectionDetail
                    .AccountReceivableId = item.AccountReceivableId
                    .Balance = item.Balance
                    .InvoiceNumber = item.InvoiceNumber
                End With
                listDetailsResult.Add(hardCollectionDetail)
            Next
            listErros.AddRange(From e In result Where e.Status = 2 Select e.Message)
            Return New ActionResult(Of List(Of HardCollectionDetail)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listDetailsResult, .MessageResult = listErros}
        Catch ex As Exception
            Return New ActionResult(Of List(Of HardCollectionDetail)) With {.StatusCode = eStatusResult.EXCEPTION}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _portfolioService.Dispose()
            End If
            _hardCollectionRepository = Nothing
            _portfolioService = Nothing
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
