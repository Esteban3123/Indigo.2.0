'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Text

#End Region

Public Class ConfirmationDocumentsAdminService
    Implements IConfirmationDocumentsAdminService
    ''' <summary>
    ''' PAC Modificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _annualizedCashFlowModificationAdminService As IAnnualizedCashFlowModificationAdminService
    ''' <summary>
    ''' PAC Traslados
    ''' </summary>
    ''' <remarks></remarks>
    Private _annualizedCashFlowTransferAdminService As IAnnualizedCashFlowTransferAdminService
    ''' <summary>
    ''' Presupuesto Modificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetModificationAdminService As IBudgetModificationAdminService
    ''' <summary>
    ''' Presupuesto Traslados
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetTransferAdminService As IBudgetTransferAdminService
    ''' <summary>
    ''' Recaudos
    ''' </summary>
    ''' <remarks></remarks>
    Private _collectionAdminService As ICollectionAdminService
    ''' <summary>
    ''' Recaudos Modificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _collectionModificationAdminService As ICollectionModificationAdminService
    ''' <summary>
    ''' Reconocimientos Modificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _recognitionModificationAdminService As IRecognitionModificationAdminService
    ''' <summary>
    ''' Reconocimientos
    ''' </summary>
    ''' <remarks></remarks>
    Private _recognitionAdminService As IRecognitionAdminService
    ''' <summary>
    ''' disponibilidades
    ''' </summary>
    ''' <remarks></remarks>
    Private _availabilityAdminService As IAvailabilityAdminService
    ''' <summary>
    ''' modificación de disponibilidades
    ''' </summary>
    ''' <remarks></remarks>
    Private _availabilityModificationAdminService As IAvailabilityModificationAdminService
    ''' <summary>
    ''' Compromisos
    ''' </summary>
    ''' <remarks></remarks>
    Private _commitmentAdminService As ICommitmentAdminService
    ''' <summary>
    ''' Modificación de compromisos
    ''' </summary>
    ''' <remarks></remarks>
    Private _commitmentModificationAdminService As ICommitmentModificationAdminService
    ''' <summary>
    ''' Obligaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _obligationAdminService As IObligationAdminService
    ''' <summary>
    ''' Modificación de Obligaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _obligationModificationAdminService As IObligationModificationAdminService
    ''' <summary>
    ''' Ordenes de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentOrderAdminService As IPaymentOrderAdminService

    Public Sub New(annualizedCashFlowModificationAdminService As IAnnualizedCashFlowModificationAdminService, annualizedCashFlowTransferAdminService As IAnnualizedCashFlowTransferAdminService,
                   budgetModificationAdminService As IBudgetModificationAdminService, budgetTransferAdminService As IBudgetTransferAdminService, collectionAdminService As ICollectionAdminService,
                   collectionModificationAdminService As ICollectionModificationAdminService, recognitionModificationAdminService As IRecognitionModificationAdminService,
                   recognitionAdminService As IRecognitionAdminService, availabilityAdminService As IAvailabilityAdminService, availabilityModificationAdminService As IAvailabilityModificationAdminService,
                   commitmentAdminService As ICommitmentAdminService, commitmentModificationAdminService As ICommitmentModificationAdminService, obligationAdminService As IObligationAdminService,
                   obligationModificationAdminService As IObligationModificationAdminService, paymentOrderAdminService As IPaymentOrderAdminService)
        If annualizedCashFlowModificationAdminService Is Nothing Then
            Throw New ArgumentNullException("annualizedCashFlowModificationAdminService")
        End If
        If annualizedCashFlowTransferAdminService Is Nothing Then
            Throw New ArgumentNullException("annualizedCashFlowTransferAdminService")
        End If
        If budgetModificationAdminService Is Nothing Then
            Throw New ArgumentNullException("budgetModificationAdminService")
        End If
        If budgetTransferAdminService Is Nothing Then
            Throw New ArgumentNullException("budgetTransferAdminService")
        End If
        If collectionAdminService Is Nothing Then
            Throw New ArgumentNullException("collectionAdminService")
        End If
        If collectionModificationAdminService Is Nothing Then
            Throw New ArgumentNullException("collectionModificationAdminService")
        End If
        If recognitionModificationAdminService Is Nothing Then
            Throw New ArgumentNullException("recognitionModificationAdminService")
        End If
        If recognitionAdminService Is Nothing Then
            Throw New ArgumentNullException("recognitionAdminService")
        End If
        If availabilityAdminService Is Nothing Then
            Throw New ArgumentNullException("availabilityAdminService")
        End If
        If availabilityModificationAdminService Is Nothing Then
            Throw New ArgumentNullException("availabilityModificationAdminService")
        End If
        If commitmentAdminService Is Nothing Then
            Throw New ArgumentNullException("commitmentAdminService")
        End If
        If commitmentModificationAdminService Is Nothing Then
            Throw New ArgumentNullException("commitmentModificationAdminService")
        End If
        If obligationAdminService Is Nothing Then
            Throw New ArgumentNullException("obligationAdminService")
        End If
        If obligationModificationAdminService Is Nothing Then
            Throw New ArgumentNullException("obligationModificationAdminService")
        End If
        If paymentOrderAdminService Is Nothing Then
            Throw New ArgumentNullException("paymentOrderAdminService")
        End If
        _annualizedCashFlowModificationAdminService = annualizedCashFlowModificationAdminService
        _annualizedCashFlowTransferAdminService = annualizedCashFlowTransferAdminService
        _budgetModificationAdminService = budgetModificationAdminService
        _budgetTransferAdminService = budgetTransferAdminService
        _collectionAdminService = collectionAdminService
        _collectionModificationAdminService = collectionModificationAdminService
        _recognitionModificationAdminService = recognitionModificationAdminService
        _recognitionAdminService = recognitionAdminService
        _availabilityAdminService = availabilityAdminService
        _availabilityModificationAdminService = availabilityModificationAdminService
        _commitmentAdminService = commitmentAdminService
        _commitmentModificationAdminService = commitmentModificationAdminService
        _obligationAdminService = obligationAdminService
        _obligationModificationAdminService = obligationModificationAdminService
        _paymentOrderAdminService = paymentOrderAdminService
    End Sub

    ''' <summary>
    ''' metodo para confirmar documentos de presupuesto ingresos
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ConfirmDocuments(listDocuments As List(Of Tuple(Of Integer, Integer, EBudgetDocumentType)), audit As AuditMessage) As ActionResult Implements IConfirmationDocumentsAdminService.ConfirmDocuments
        Try
            Dim errors As New StringBuilder
            For Each item In listDocuments
                Select Case item.Item3
                    Case EBudgetDocumentType.BudgetModification
                        Dim budgetModification = _budgetModificationAdminService.GetBudgetModificationById(item.Item2)
                        budgetModification.Status = 2
                        budgetModification.MarkAsModified()
                        Dim result = _budgetModificationAdminService.SaveBudgetMofication(budgetModification, Nothing, audit)
                        If result.StateResult = False Then
                            errors.AppendLine("No se confirmo la modificación del presupuesto " + budgetModification.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentType.BudgetTransfer
                        Dim budgetTransfer = _budgetTransferAdminService.GetBudgetTransferById(item.Item2, audit).ObjectEmbbeded
                        budgetTransfer.Status = 2
                        budgetTransfer.MarkAsModified()
                        Dim result = _budgetTransferAdminService.SaveBudgetTransfer(budgetTransfer, audit)
                        If result.StateResult = False Then
                            errors.AppendLine("No se confirmo el traslado de presupuesto " + budgetTransfer.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentType.AnnualizedCashFlowModification
                        Dim AnnualizedCashFlowModification = _annualizedCashFlowModificationAdminService.GetAnnualizedCashFlowModificationById(item.Item2)
                        AnnualizedCashFlowModification.Status = 2
                        AnnualizedCashFlowModification.MarkAsModified()
                        Dim result = _annualizedCashFlowModificationAdminService.SaveAnnualizedCashFlowModification(AnnualizedCashFlowModification, audit)
                        If result.StateResult = False Then
                            errors.AppendLine("No se confirmo la modificacion del PAC " + AnnualizedCashFlowModification.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentType.AnnualizedCashFlowTransfer
                        Dim AnnualizedCashFlowTransfer = _annualizedCashFlowTransferAdminService.GetAnnualizedCashFlowTransferById(item.Item2)
                        AnnualizedCashFlowTransfer.Status = 2
                        AnnualizedCashFlowTransfer.MarkAsModified()
                        Dim result = _annualizedCashFlowTransferAdminService.SaveAnnualizedCashFlowTransfer(AnnualizedCashFlowTransfer, audit)
                        If result.StateResult = False Then
                            errors.AppendLine("No se confirmo el traslado del PAC " + AnnualizedCashFlowTransfer.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentType.Recognition
                        Dim Recognition = _recognitionAdminService.GetRecognitionById(item.Item2, audit).ObjectEmbbeded
                        Recognition.Status = 2
                        Recognition.MarkAsModified()
                        Dim result = _recognitionAdminService.SaveRecognition(Recognition, Nothing, audit)
                        If result.StateResult = False Then
                            errors.AppendLine("No se confirmo el reconocimiento " + Recognition.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentType.RecognitionModification
                        Dim RecognitionModification = _recognitionModificationAdminService.GetRecognitionModificationById(item.Item2)
                        RecognitionModification.Status = 2
                        RecognitionModification.MarkAsModified()
                        Dim result = _recognitionModificationAdminService.SaveRecognitionModification(RecognitionModification, Nothing, audit)
                        If result.StateResult = False Then
                            errors.AppendLine("No se confirmo la modificación del reconocimiento " + RecognitionModification.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentType.Collection
                        Dim Collection = _collectionAdminService.GetCollectionById(item.Item2)
                        Collection.Status = 2
                        Collection.MarkAsModified()
                        Dim result = _collectionAdminService.SaveCollection(Collection, Nothing, audit)
                        If result.StateResult = False Then
                            errors.AppendLine("No se confirmo el recaudo " + Collection.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentType.CollectionModification
                        Dim CollectionModification = _collectionModificationAdminService.GetCollectionModificationById(item.Item2)
                        CollectionModification.Status = 2
                        CollectionModification.MarkAsModified()
                        Dim result = _collectionModificationAdminService.SaveCollectionModification(CollectionModification, Nothing, audit)
                        If result.StateResult = False Then
                            errors.AppendLine("No se confirmo la modificación del recaudo " + CollectionModification.Code + " porque " + result.Message)
                        End If
                End Select
            Next
            Return New ActionResult With {.StateResult = True, .Message = errors.ToString()}
        Catch ex As OptimisticConcurrencyException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para confirmar documentos de presupuesto gastos
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ConfirmDocumentsExpense(listDocuments As List(Of Tuple(Of Integer, Integer, EBudgetDocumentTypeExpense)), audit As AuditMessage) As ActionResult Implements IConfirmationDocumentsAdminService.ConfirmDocumentsExpense
        Try
            Dim errors As New StringBuilder
            For Each item In listDocuments
                Select Case item.Item3
                    Case EBudgetDocumentTypeExpense.BudgetModification
                        Dim budgetModification = _budgetModificationAdminService.GetBudgetModificationById(item.Item2)
                        budgetModification.Status = 2
                        budgetModification.MarkAsModified()
                        Dim result = _budgetModificationAdminService.SaveBudgetMofication(budgetModification, Nothing, audit)
                        If result.StateResult = False Then
                            budgetModification.Status = 1
                            budgetModification.MarkAsUnchanged()
                            errors.AppendLine("No se confirmo la modificación del presupuesto " + budgetModification.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentTypeExpense.BudgetTransfer
                        Dim budgetTransfer = _budgetTransferAdminService.GetBudgetTransferById(item.Item2, audit).ObjectEmbbeded
                        budgetTransfer.Status = 2
                        budgetTransfer.MarkAsModified()
                        Dim result = _budgetTransferAdminService.SaveBudgetTransfer(budgetTransfer, audit)
                        If result.StateResult = False Then
                            budgetTransfer.Status = 1
                            budgetTransfer.MarkAsUnchanged()
                            errors.AppendLine("No se confirmo el traslado de presupuesto " + budgetTransfer.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentTypeExpense.AnnualizedCashFlowModification
                        Dim AnnualizedCashFlowModification = _annualizedCashFlowModificationAdminService.GetAnnualizedCashFlowModificationById(item.Item2)
                        AnnualizedCashFlowModification.Status = 2
                        AnnualizedCashFlowModification.MarkAsModified()
                        Dim result = _annualizedCashFlowModificationAdminService.SaveAnnualizedCashFlowModification(AnnualizedCashFlowModification, audit)
                        If result.StateResult = False Then
                            AnnualizedCashFlowModification.Status = 1
                            AnnualizedCashFlowModification.MarkAsUnchanged()
                            errors.AppendLine("No se confirmo la modificacion del PAC " + AnnualizedCashFlowModification.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentTypeExpense.AnnualizedCashFlowTransfer
                        Dim AnnualizedCashFlowTransfer = _annualizedCashFlowTransferAdminService.GetAnnualizedCashFlowTransferById(item.Item2)
                        AnnualizedCashFlowTransfer.Status = 2
                        AnnualizedCashFlowTransfer.MarkAsModified()
                        Dim result = _annualizedCashFlowTransferAdminService.SaveAnnualizedCashFlowTransfer(AnnualizedCashFlowTransfer, audit)
                        If result.StateResult = False Then
                            AnnualizedCashFlowTransfer.Status = 1
                            AnnualizedCashFlowTransfer.MarkAsUnchanged()
                            errors.AppendLine("No se confirmo el traslado del PAC " + AnnualizedCashFlowTransfer.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentTypeExpense.Availability
                        Dim availability = _availabilityAdminService.GetAvailabilityById(item.Item2)
                        availability.Status = 2
                        availability.MarkAsModified()
                        Dim result = _availabilityAdminService.SaveAvailability(availability, audit)
                        If result.StateResult = False Then
                            availability.Status = 1
                            availability.MarkAsUnchanged()
                            errors.AppendLine("No se confirmo la disponibilidad " + availability.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentTypeExpense.AvailabilityModification
                        Dim availabilityModification = _availabilityModificationAdminService.GetAvailabilityModificationById(item.Item2)
                        availabilityModification.Status = 2
                        availabilityModification.MarkAsModified()
                        Dim result = _availabilityModificationAdminService.SaveAvailabilityModification(availabilityModification, Nothing, audit)
                        If result.StateResult = False Then
                            availabilityModification.Status = 1
                            availabilityModification.MarkAsUnchanged()
                            errors.AppendLine("No se confirmo la modificación de disponibilidad " + availabilityModification.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentTypeExpense.Commitment
                        Dim commitment = _commitmentAdminService.GetCommitmentById(item.Item2)
                        commitment.Status = 2
                        commitment.MarkAsModified()
                        Dim result = _commitmentAdminService.SaveCommitment(commitment, Nothing, audit)
                        If result.StateResult = False Then
                            commitment.Status = 1
                            commitment.MarkAsUnchanged()
                            errors.AppendLine("No se confirmo el compromiso " + commitment.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentTypeExpense.CommitmentModification
                        Dim commitmentModification = _commitmentModificationAdminService.GetCommitmentModificationById(item.Item2)
                        commitmentModification.Status = 2
                        commitmentModification.MarkAsModified()
                        Dim result = _commitmentModificationAdminService.SaveCommitmentModification(commitmentModification, Nothing, audit)
                        If result.StateResult = False Then
                            commitmentModification.Status = 1
                            commitmentModification.MarkAsUnchanged()
                            errors.AppendLine("No se confirmo la modificación del compromiso " + commitmentModification.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentTypeExpense.Obligation
                        Dim obligation = _obligationAdminService.GetObligationById(item.Item2)
                        obligation.Status = 2
                        obligation.MarkAsModified()
                        Dim result = _obligationAdminService.SaveObligation(obligation, Nothing, audit)
                        If result.StateResult = False Then
                            obligation.Status = 1
                            obligation.MarkAsUnchanged()
                            errors.AppendLine("No se confirmo la obligación " + obligation.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentTypeExpense.ObligationModification
                        Dim obligationModification = _obligationModificationAdminService.GetObligationModificationById(item.Item2, audit).ObjectEmbbeded
                        obligationModification.Status = 2
                        obligationModification.MarkAsModified()
                        Dim result = _obligationModificationAdminService.SaveObligationModification(obligationModification, Nothing, audit)
                        If result.StateResult = False Then
                            obligationModification.Status = 1
                            obligationModification.MarkAsUnchanged()
                            errors.AppendLine("No se confirmo la modificación de obligación " + obligationModification.Code + " porque " + result.Message)
                        End If
                    Case EBudgetDocumentTypeExpense.PaymentOrder
                        Dim paymentOrder = _paymentOrderAdminService.GetPaymentOrderById(item.Item2)
                        paymentOrder.Status = 2
                        paymentOrder.MarkAsModified()
                        Dim result = _paymentOrderAdminService.SavePaymentOrder(paymentOrder, Nothing, audit)
                        If result.StateResult = False Then
                            paymentOrder.Status = 1
                            paymentOrder.MarkAsUnchanged()
                            errors.AppendLine("No se confirmo la orden de pago " + paymentOrder.Code + " porque " + result.Message)
                        End If
                End Select
            Next
            Return New ActionResult With {.StateResult = True, .Message = errors.ToString()}
        Catch ex As OptimisticConcurrencyException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _annualizedCashFlowModificationAdminService.Dispose()
                _annualizedCashFlowTransferAdminService.Dispose()
                _budgetModificationAdminService.Dispose()
                _budgetTransferAdminService.Dispose()
                _collectionAdminService.Dispose()
                _collectionModificationAdminService.Dispose()
                _recognitionModificationAdminService.Dispose()
                _recognitionAdminService.Dispose()
                _availabilityAdminService.Dispose()
                _availabilityModificationAdminService.Dispose()
                _commitmentAdminService.Dispose()
                _commitmentModificationAdminService.Dispose()
                _obligationAdminService.Dispose()
                _obligationModificationAdminService.Dispose()
                _paymentOrderAdminService.Dispose()
            End If
            _annualizedCashFlowModificationAdminService = Nothing
            _annualizedCashFlowTransferAdminService = Nothing
            _budgetModificationAdminService = Nothing
            _budgetTransferAdminService = Nothing
            _collectionAdminService = Nothing
            _collectionModificationAdminService = Nothing
            _recognitionModificationAdminService = Nothing
            _recognitionAdminService = Nothing
            _availabilityAdminService = Nothing
            _availabilityModificationAdminService = Nothing
            _commitmentAdminService = Nothing
            _commitmentModificationAdminService = Nothing
            _obligationAdminService = Nothing
            _obligationModificationAdminService = Nothing
            _paymentOrderAdminService = Nothing
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
