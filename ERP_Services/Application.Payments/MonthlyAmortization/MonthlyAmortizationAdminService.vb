'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

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
Imports Domain.Entities.Service
Imports Application.Accounting
Imports System.Text

Public Class MonthlyAmortizationAdminService
    Implements IMonthlyAmortizationAdminService

    ''' <summary>
    ''' Repositorio de documento contable
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountingRepository As IAccountingDocumentAdminService
    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _monthlyAmortizationRepository As IMonthlyAmortizationRepository
    ''' <summary>
    ''' Variable tipo repositorio para parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingsPaymentsRepository As ISettingPaymentsRepository
    Private _repositoryCloseMont As ICloseMonthRepository
    Private _repositoryMainAccounts As IPUCRepository
    Private _deferredCausationShare As IDeferredCausationShareRepository
    Private _journalVoucherRepository As IDocumentTypeRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal monthlyAmortizationRepository As IMonthlyAmortizationRepository, ByVal settingsPaymentsRepository As ISettingPaymentsRepository,
                   ByVal repositoryCloseMont As ICloseMonthRepository, ByVal repositoryMainAccounts As IPUCRepository, ByVal accountingRepository As IAccountingDocumentAdminService,
                   ByVal deferredCausationShare As IDeferredCausationShareRepository, ByVal journalVoucherRepository As IDocumentTypeRepository)
        If monthlyAmortizationRepository Is Nothing Then
            Throw New ArgumentNullException("monthlyAmortizationRepository Vacio")
        End If
        _monthlyAmortizationRepository = monthlyAmortizationRepository
        _settingsPaymentsRepository = settingsPaymentsRepository
        _repositoryCloseMont = repositoryCloseMont
        _repositoryMainAccounts = repositoryMainAccounts
        _accountingRepository = accountingRepository
        _deferredCausationShare = deferredCausationShare
        _journalVoucherRepository = journalVoucherRepository
    End Sub

    ''' <summary>
    ''' Confirma la amortizacion mensual
    ''' </summary>
    ''' <param name="listDeferredCausationShare"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmMonthlyAmortization(listDeferredCausationShare As List(Of DeferredCausationShare), audit As AuditMessage, idOperatingUnit As Integer) As ActionResult(Of List(Of DeferredCausationShare)) Implements IMonthlyAmortizationAdminService.ConfirmMonthlyAmortization
        If listDeferredCausationShare Is Nothing Then
            Throw New ArgumentNullException("listDeferredCausation")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Dim UnitOfWork As IUnitWork = _deferredCausationShare.UnitWork
        Dim listConsecutiveAccounting As New StringBuilder

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Pregunto si el periodo esta abierto
                Dim accountingServices As New AccountingServices(_repositoryMainAccounts, _repositoryCloseMont)
                Dim validatePeriod = accountingServices.ValidatePeriodAndReturnOpendMonths(listDeferredCausationShare.Item(0).DatePeriod)
                If validatePeriod.StateResult = False Then
                    transaction.Dispose()
                    Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False, .Message = validatePeriod.Message}
                End If

                'Consulto si hay parametros de pago con la unidad operativa
                Dim settingPayment As SettingPayments = _settingsPaymentsRepository.GetSettingPaymentsByIdOperatingUnit(idOperatingUnit)
                If settingPayment.Id = 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False, .Message = ResourceManager.GetString("FrmParameters_DontExists", "Payments")}
                End If
                'Consulto el tipo de comprobante que se genera y lo inserto en el stringBuilder
                Dim jv As JournalVoucherTypes = _journalVoucherRepository.GetJournalVoucherById(settingPayment.IdJournalVocuherAmortization)

                'Se valida que el item de cuota no se haya cambiado el valor desde el form de Modificación de Amortizaciones
                Dim listErrors As New StringBuilder
                For Each itemDeferredCausationShare As DeferredCausationShare In listDeferredCausationShare.FindAll(Function(x) x.Amortized)
                    If itemDeferredCausationShare Is Nothing Then
                        Continue For
                    End If
                    Dim _deferredCausationShareCompareValidation As DeferredCausationShare = _deferredCausationShare.GetDeferredCausationShareById(itemDeferredCausationShare.Id)
                    If _deferredCausationShareCompareValidation.Id > 0 Then
                        If itemDeferredCausationShare.Value <> _deferredCausationShareCompareValidation.Value Then
                            listErrors.AppendLine("La cuota de causación de la factura " & itemDeferredCausationShare.BillNumber & " con tercero " & itemDeferredCausationShare.ThirdPartyDescription & " ha cambiado el Valor. Vuelva a consultar.")
                        End If
                    End If
                Next
                If listErrors.Length > 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False, .Message = listErrors.ToString}
                End If

                'Entidad que guardar el listado de diferidos a amortizar para su ajuste diferencial
                Dim data = New DataRevaluation() With {.ListDeferredCausationRevaluation = New List(Of DeferredCausationRevaluation)}

                For Each itemDeferredCausationShare As DeferredCausationShare In listDeferredCausationShare.FindAll(Function(x) x.Amortized)
                    If itemDeferredCausationShare Is Nothing Then
                        Continue For
                    End If

                    Dim deferredCausation = _monthlyAmortizationRepository.GetDeferredCausationById(itemDeferredCausationShare.DeferredCausationId)
                    If deferredCausation Is Nothing OrElse deferredCausation.Id = 0 Then
                        Continue For
                    End If

                    'Creo el comprobante contable
                    Dim resultAccounting As ActionResult(Of JournalVouchers)
                    resultAccounting = PaymentServices.CreateAccountingByMonthlyAmortization(deferredCausation, itemDeferredCausationShare, settingPayment)
                    If resultAccounting.StateResult = False Then
                        transaction.Dispose()
                        Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False, .Message = resultAccounting.Message}
                    End If

                    'Entidad para enviar al ajuste diferencial
                    Dim deferredCausationRevaluation = New DeferredCausationRevaluation
                    With deferredCausationRevaluation
                        .Id = deferredCausation.Id
                        .EntityId = 0
                        .EntityName = NameOf(Domain.Entities.DeferredCausation)
                        .DocumentDate = resultAccounting.ObjectEmbbeded.VoucherDate.Date
                        .ValueAdjustment = itemDeferredCausationShare.Value
                    End With

                    'Se agrega a la lista para enviar al ajuste diferencial
                    data.ListDeferredCausationRevaluation.Add(deferredCausationRevaluation)

                    'Guardo el comprobante contable
                    Dim resultSaveAccounting As ActionMessageResult(Of JournalVouchers)
                    resultSaveAccounting = _accountingRepository.SaveAccountingDocument(resultAccounting.ObjectEmbbeded, audit)
                    If resultSaveAccounting Is Nothing OrElse Not resultSaveAccounting.StateResult Then
                        transaction.Dispose()
                        Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False, .Message = resultSaveAccounting.Message.ToString}
                    End If

                    'Modifico el campo de amortizar en la tabla DeferredCausationShare
                    itemDeferredCausationShare.Amortized = True
                    itemDeferredCausationShare.MarkAsModified()
                    _deferredCausationShare.SaveEntity(itemDeferredCausationShare)
                    UnitOfWork.Commit()

                    If listConsecutiveAccounting.Length = 0 Then
                        listConsecutiveAccounting.Append(resultSaveAccounting.ObjectEmbbeded.Consecutive.ToString)
                    Else
                        listConsecutiveAccounting.Append(", " + resultSaveAccounting.ObjectEmbbeded.Consecutive.ToString)
                    End If
                Next

                'Genero el ajuste diferencial de los diferidos a amortizar
                Dim generateDiff = _monthlyAmortizationRepository.GenerateDifferentialAdjustment(data, audit.CodeUser)

                If generateDiff Is Nothing OrElse generateDiff.Exists(Function(x) x.Code = "999") Then
                    transaction.Dispose()
                    Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False, .Message = $"Error al ejecutar el ajuste diferencial amortización de diferidos : {generateDiff?.Find(Function(x) x.Code = "999")?.MessageResult}"}
                End If

                Dim messageDiff As String

                If generateDiff.Any() Then
                    messageDiff = $"Ajuste diferencial diferidos : {String.Join(", ", generateDiff.Select(Function(x) x.MessageResult))}"
                End If

                transaction.Complete()
                Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = True, .ObjectEmbbeded = listDeferredCausationShare, .Message = jv.Name, .MessageResult = {listConsecutiveAccounting.ToString, messageDiff}.ToList}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el listado de causaciones diferidas por la fecha
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationByDate(year As Integer, month As Integer, audit As AuditMessage) As ActionResult(Of List(Of DeferredCausationShare)) Implements IMonthlyAmortizationAdminService.GetDeferredCausationByDate
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Dim listDeferredCausation As List(Of DeferredCausationShare) = _monthlyAmortizationRepository.GetDeferredCausationByDate(year, month)
            If listDeferredCausation Is Nothing Then
                Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False}
            End If
            Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = True, .ObjectEmbbeded = listDeferredCausation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una causacion diferida por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationById(id As String, audit As AuditMessage) As ActionResult(Of DeferredCausation) Implements IMonthlyAmortizationAdminService.GetDeferredCausationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim deferredCausation As DeferredCausation = _monthlyAmortizationRepository.GetDeferredCausationById(id)
            Return New ActionResult(Of DeferredCausation) With {.StateResult = True, .ObjectEmbbeded = deferredCausation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DeferredCausation) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _monthlyAmortizationRepository = Nothing
            _settingsPaymentsRepository = Nothing
            _repositoryCloseMont = Nothing
            _repositoryMainAccounts = Nothing
            _accountingRepository = Nothing
            _deferredCausationShare = Nothing
            _journalVoucherRepository = Nothing
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
