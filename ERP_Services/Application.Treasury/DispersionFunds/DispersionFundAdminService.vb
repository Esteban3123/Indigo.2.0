'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
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
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities.Service
Imports System.Text
Imports Application.Payments
Imports Application.Common

#End Region

Public Class DispersionFundAdminService
    Implements IDispersionFundAdminService

#Region "Fields"

    ''' <summary>
    ''' The tagform
    ''' </summary>
    Private Const TAGFORM As String = "636"
    Private Const NAME_MODULE As String = "Treasury"
    ''' <summary>
    ''' Repositorio de comprobantes de egreso
    ''' </summary>
    Private _voucherTransactionRepository As IVoucherTransactionRepository
    ''' <summary>
    ''' servicios de aplicacion de comprobantes de egreso
    ''' </summary>
    Private _voucherTransactionAdminService As IVoucherTransactionAdminService
    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseTreasuryDRepository
    ''' <summary>
    ''' servicios de aplicacion de secuencias numericas
    ''' </summary>
    Private _sequenceAdminService As ITreasurySequenseAdminService
    ''' <summary>
    ''' repositorio de programacion de pagos
    ''' </summary>
    Private _schedulePaymentRepository As ISchedulePaymentRepository
    ''' <summary>
    ''' Acceso al repositorio a bancos desde dispersión
    ''' </summary>
    Private _schedulePaymentBankAccountRepository As ISchedulePaymentBankAccountRepository
    ''' <summary>
    ''' Servicios de aplicacion de programacion de pagos
    ''' </summary>
    Private _schedulePaymentAdminService As ISchedulePaymentAdminService
    ''' <summary>
    ''' Repositorio de cuentas contables
    ''' </summary>
    Private _entityBankAccountRepository As IEntityBankAccountRepository
    ''' <summary>
    ''' repositorio de proveedores
    ''' </summary>
    Private _supplierRepository As ISupplierRepository
    ''' <summary>
    ''' The _schedule payment detail repository
    ''' </summary>
    Private _schedulePaymentDetailRepository As ISchedulePaymentDetailRepository
    Private _expenseConceptoRepository As IExpenseConceptRepository
    Private _treasuryService As ITreasuryServices

    ''' <summary>
    ''' Repositorio de cuentas por pagar
    ''' </summary>
    Private _accountPayableRepository As IAccountPayableRepository

    ''' <summary>
    ''' repositorio de lineas de distribucion
    ''' </summary>
    Private _distributionLinesRepository As IDistributionLinesRepository

    ''' <summary>
    ''' repositorio parametros cuentas por pagar
    ''' </summary>
    Private _settingPaymentsRepository As ISettingPaymentsRepository

    ''' <summary>
    ''' adminservice de nota debito credito cuentas por pagar
    ''' </summary>
    Private _notesDebitCreditAdminService As INotesDebitCreditAdminService

    ''' <summary>
    ''' Repo de las secuencias numericas de CxP
    ''' </summary>
    Private _sequensePaymentsCRepository As ISequensePaymentsCRepository

    Private _companySettingsRepository As ICompanySettingsRepository

    Private _currencyAdminService As ICurrencyAdminService
#End Region

#Region "Methods"

    Public Sub New(ByVal voucherTransactionRepository As IVoucherTransactionRepository, ByVal secuenseDRepository As ISequenseTreasuryDRepository, ByVal schedulePaymentRepository As ISchedulePaymentRepository,
                   ByVal entityBankAccountRepository As IEntityBankAccountRepository, ByVal supplierRepository As ISupplierRepository, ByVal voucherTransactionAdminService As IVoucherTransactionAdminService,
                   ByVal sequenceAdminService As ITreasurySequenseAdminService, ByVal schedulePaymentAdminService As ISchedulePaymentAdminService, ByVal schedulePaymentDetailRepository As ISchedulePaymentDetailRepository,
                   treasuryService As ITreasuryServices, expenseConceptoRepository As IExpenseConceptRepository, AccountPayableRepository As IAccountPayableRepository, DistributionLinesRepository As IDistributionLinesRepository,
                   SettingPaymentsRepository As ISettingPaymentsRepository, NotesDebitCreditAdminService As INotesDebitCreditAdminService, SequensePaymentsCRepository As ISequensePaymentsCRepository,
                   companySettingsRepository As ICompanySettingsRepository, currencyAdminService As ICurrencyAdminService, schedulePaymentBankAccountRepository As ISchedulePaymentBankAccountRepository)
        If voucherTransactionRepository Is Nothing Then
            Throw New ArgumentNullException("voucherTransactionRepository")
        End If
        If schedulePaymentDetailRepository Is Nothing Then
            Throw New ArgumentNullException("schedulePaymentDetailRepository")
        End If
        If schedulePaymentAdminService Is Nothing Then
            Throw New ArgumentNullException("schedulePaymentAdminService")
        End If
        If sequenceAdminService Is Nothing Then
            Throw New ArgumentNullException("sequenceAdminService")
        End If
        If voucherTransactionAdminService Is Nothing Then
            Throw New ArgumentNullException("voucherTransactionAdminService")
        End If
        If supplierRepository Is Nothing Then
            Throw New ArgumentNullException("supplierRepository")
        End If
        If entityBankAccountRepository Is Nothing Then
            Throw New ArgumentNullException("entityBankAccountRepository")
        End If
        If schedulePaymentRepository Is Nothing Then
            Throw New ArgumentNullException("schedulePaymentRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        Me._supplierRepository = supplierRepository
        Me._entityBankAccountRepository = entityBankAccountRepository
        Me._schedulePaymentRepository = schedulePaymentRepository
        Me._voucherTransactionRepository = voucherTransactionRepository
        Me._secuenseDRepository = secuenseDRepository
        Me._voucherTransactionAdminService = voucherTransactionAdminService
        Me._sequenceAdminService = sequenceAdminService
        Me._schedulePaymentAdminService = schedulePaymentAdminService
        Me._schedulePaymentDetailRepository = schedulePaymentDetailRepository
        Me._treasuryService = treasuryService
        Me._accountPayableRepository = AccountPayableRepository
        Me._distributionLinesRepository = DistributionLinesRepository
        Me._settingPaymentsRepository = SettingPaymentsRepository
        Me._notesDebitCreditAdminService = NotesDebitCreditAdminService
        Me._sequensePaymentsCRepository = SequensePaymentsCRepository
        _expenseConceptoRepository = expenseConceptoRepository
        Me._companySettingsRepository = companySettingsRepository
        Me._currencyAdminService = currencyAdminService
        Me._schedulePaymentBankAccountRepository = _schedulePaymentBankAccountRepository
    End Sub

    ''' <summary>
    ''' Efectúa los pagos de la programación
    ''' </summary>
    Public Function MakeSchedulePayment(schedulePayment As SchedulePayment, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0, Optional ByVal sequenceC As TreasurySequence = Nothing) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IDispersionFundAdminService.MakeSchedulePayment
        If schedulePayment Is Nothing Then
            Throw New ArgumentNullException("schedulePayment")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Dim messageList As New List(Of Tuple(Of String, Integer))
        Dim ListIdsAccount = New List(Of Integer)
        Dim listIdsDistributionLines = New List(Of Integer)
        Dim listDistributionLines = New List(Of DistributionLines)
        Dim ListAccountPayables = New List(Of AccountPayable)
        Dim SettingCxP = _settingPaymentsRepository.GetSettingPaymentsByIdOperatingUnit(schedulePayment.OperativeUnitId, False)
        Dim officialCurrency = _companySettingsRepository.FirstOrDefault(Function(x) True, False, {"Currency"})?.Currency
        Dim listTRM As New List(Of TRM)
        'bandera para saber si alguna nota fue confirmada, y si no se llega a confirma el comprobante de egreso devuelve la transaccion
        Dim FlagToNoteConfirm As Boolean = False

        Try

            Dim resultSave As ActionResult(Of SchedulePayment) = _schedulePaymentAdminService.SaveSchedulePayment(schedulePayment, audit, False, idSequence)
            If resultSave Is Nothing OrElse Not resultSave.StateResult Then
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = resultSave.Message}
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                'validacion de que este confirmada la programacion de pagos
                If schedulePayment IsNot Nothing AndAlso schedulePayment.Id > 0 Then

                    Dim listSchedulePaymentDetail As List(Of SchedulePaymentDetail) = schedulePayment.SchedulePaymentDetail.ToList()
                    If listSchedulePaymentDetail?.Any() Then
                        Dim entityBankAccount As EntityBankAccounts = _entityBankAccountRepository.GetEntityBankAccountById(schedulePayment.EntityBankAccountId)

                        If entityBankAccount IsNot Nothing AndAlso entityBankAccount.Id > 0 Then

                            Dim currencyBank = entityBankAccount?.Currency
                            If currencyBank Is Nothing Then
                                currencyBank = officialCurrency
                            End If

                            'consulta masiva para traer las entidades necesarias para crear la Nota debito
                            ListIdsAccount = listSchedulePaymentDetail?.Select(Function(f) f.AccountPayableId).ToList()
                            ListAccountPayables = _accountPayableRepository.GetByFilter(Function(x) ListIdsAccount.Contains(x.Id), False, {"AccountPayableShares", "Currency", "AccountPayableDetailConcept", "AccountPayableDetailConcept.MainAccounts", "AccountPayableDetailConcept.MainAccounts.MainAccountClasses"})

                            If listSchedulePaymentDetail?.Any(Function(d) d.DiscountValue > 0) Then
                                listIdsDistributionLines = listSchedulePaymentDetail.Where(Function(s) s.DiscountValue > 0).Select(Function(g) g.DistributionLineId).ToList()
                                listDistributionLines = _distributionLinesRepository.GetByFilter(Function(u) listIdsDistributionLines.Contains(u.Id), False, {"AccountPayableConceptNotes", "SuppliersDistributionLines"})
                            End If

                            Dim dicVourcherTransactionSchedulePaymentDetal As New Dictionary(Of Int32, List(Of Int32))()
                            For Each supplierId As Integer In listSchedulePaymentDetail.Where(Function(y) y.VoucherTransactionId Is Nothing).Cast(Of SchedulePaymentDetail).ToList().Select(Function(x) x.SupplierId).Cast(Of Integer).ToList().Distinct().ToList()

                                Dim listSchedulePaymentDetailBySupplier As List(Of SchedulePaymentDetail) = listSchedulePaymentDetail.Where(Function(x) x.SupplierId = supplierId).Cast(Of SchedulePaymentDetail).ToList()
                                Dim voucherTransaction As New VoucherTransaction()
                                Dim third As ThirdParty = _supplierRepository.GetThirdPartyById(listSchedulePaymentDetailBySupplier.ElementAt(0).ThirdPartyId) ' Traer de schedulepaymentdetail
                                Dim dischargeBill As DischargeBill
                                Dim voucherTransactionDetail As VoucherTransactionDetails

                                Dim listDistributionLineIds As List(Of Integer) = listSchedulePaymentDetailBySupplier.Select(Function(x) x.DistributionLineId).Cast(Of Integer).ToList().Distinct().ToList()
                                Dim headerDetail As String = "Generado con Planilla de Dispersión No " & schedulePayment.Code & vbCrLf

                                'Obtengo la cuenta bancaria del proveedor
                                Dim supplierPaymentBankAccount As SchedulePaymentBankAccount = schedulePayment.SchedulePaymentBankAccount?. _
                                                                                                   Where(Function(w) w.SupplierId = supplierId).FirstOrDefault()
                                If supplierPaymentBankAccount?.Id = 0 Then
                                    supplierPaymentBankAccount = _schedulePaymentBankAccountRepository.Query(Function(q) q.SchedulePaymentId = schedulePayment.Id _
                                                                                                                     AndAlso q.SupplierId = supplierId, False)
                                End If

                                For Each distributionLineId As Integer In listDistributionLineIds
                                    voucherTransactionDetail = Nothing
                                    voucherTransactionDetail = New VoucherTransactionDetails()
                                    'detalle de egresos

                                    Dim description As String = "Comprobante de Egreso : {0}"
                                    Dim invoices As New List(Of String)()
                                    Dim dictionaryAccountPayableInovices As New Dictionary(Of String, String)()
                                    For Each scheduleDetail As SchedulePaymentDetail In listSchedulePaymentDetailBySupplier.Where(Function(x) x.DistributionLineId = distributionLineId).ToList()
                                        Dim accountPayableCurrency = ListAccountPayables.Find(Function(e) e.Id = scheduleDetail.AccountPayableId)?.Currency
                                        Dim tRMValue As Decimal? = 1
                                        ' si no viene moneda en la cxp se toma la oficial
                                        If accountPayableCurrency Is Nothing Then
                                            accountPayableCurrency = officialCurrency
                                        End If

                                        ''Logica para generar la nota debito por cxp siempre y cuando tenga aplicado descuento
                                        If scheduleDetail.DiscountValue > 0 Then
                                            Dim NoteDebitCxP = CreateNote(scheduleDetail, ListAccountPayables.Find(Function(e) e.Id = scheduleDetail.AccountPayableId), listDistributionLines.Where(Function(r) r.Id = scheduleDetail.DistributionLineId).FirstOrDefault,
                                                            schedulePayment.OperativeUnitId, audit, SettingCxP, schedulePayment.Id, schedulePayment.Code, headerDetail)
                                            If NoteDebitCxP Is Nothing OrElse Not NoteDebitCxP.StateResult Then
                                                messageList.Add(New Tuple(Of String, Integer)(NoteDebitCxP?.Message, eMessageType.Errors))
                                                Continue For
                                            Else
                                                messageList.Add(New Tuple(Of String, Integer)(NoteDebitCxP?.Message, eMessageType.Confirm))
                                                FlagToNoteConfirm = True
                                            End If
                                        End If

                                        tRMValue = listTRM?.Find(Function(x) x.CurrencyId = currencyBank?.Id AndAlso x.OfficialCurrencyId = accountPayableCurrency?.Id _
                                                            AndAlso x.MeasurementDate = Date.Now.Date)?.Value

                                        If tRMValue Is Nothing OrElse tRMValue = 0 Then
                                            Dim result = _currencyAdminService.GetTRMbyCurrencyId(currencyBank?.Id, accountPayableCurrency?.Id, New SessionValues With {.OfficialCurrencyId = officialCurrency?.Id, .CurrencyISO4217 = officialCurrency?.Abbreviation})

                                            If result Is Nothing OrElse Not result?.StateResult Then
                                                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = result?.Message}
                                            End If

                                            listTRM.Add(result.ObjectEmbbeded)
                                            tRMValue = listTRM?.Find(Function(x) x.CurrencyId = currencyBank?.Id _
                                                                         AndAlso x.OfficialCurrencyId = accountPayableCurrency?.Id _
                                                                         AndAlso x.MeasurementDate = Date.Now.Date)?.Value
                                        End If

                                        dischargeBill = New DischargeBill()
                                        With dischargeBill
                                            .IdAccountPayable = scheduleDetail.AccountPayableId
                                            .IdAccountPayableShare = scheduleDetail.AccountPayableShareId
                                            .AdvancedValue = scheduleDetail.AmountPaid
                                            .ValueInCurrencyHeader = scheduleDetail.AmountPaid / tRMValue
                                            .AdvancePercent = scheduleDetail.AmountPercent
                                            .IdPaymentConcept = scheduleDetail.PaymentConceptId
                                            .BaseValueDiscount = 0
                                            .DiscountPercent = 0
                                            .ValueDiscountInCurrencyHeader = 0

                                            If scheduleDetail.SchedulePaymentDetailBudget IsNot Nothing AndAlso scheduleDetail.SchedulePaymentDetailBudget.Any() Then
                                                For Each schedulePaymentDetailBudget In scheduleDetail.SchedulePaymentDetailBudget
                                                    dischargeBill.DischargeBillBudget.Add(New DischargeBillBudget With {
                                                        .ObligationDetailId = schedulePaymentDetailBudget.ObligationDetailId,
                                                        .Value = schedulePaymentDetailBudget.Value
                                                    })
                                                Next
                                            End If
                                        End With
                                        scheduleDetail.GeneratedVoucher = True
                                        voucherTransactionDetail.DischargeBill.Add(dischargeBill)
                                        voucherTransaction.SchedulePaymentDetail.Add(scheduleDetail.MarkAsModified())
                                        invoices.Add(scheduleDetail.Invoice)

                                        If dictionaryAccountPayableInovices.ContainsKey(scheduleDetail.AccountPayableCode) Then
                                            dictionaryAccountPayableInovices(scheduleDetail.AccountPayableCode) = dictionaryAccountPayableInovices(scheduleDetail.AccountPayableCode) & ", " & scheduleDetail.Invoice
                                        Else
                                            dictionaryAccountPayableInovices.Add(scheduleDetail.AccountPayableCode, scheduleDetail.Invoice)
                                        End If
                                        'headerDetail &= String.Format("CxP {0} Factura: {1}{2}", scheduleDetail.AccountPayableCode, scheduleDetail.Invoice, vbCrLf)
                                    Next

                                    headerDetail &= String.Join(vbCrLf, dictionaryAccountPayableInovices.Select(Function(o) String.Concat("CxP ", o.Key, " Facturas: ", o.Value)).ToArray)

                                    'Detalle del comprobante de egresos
                                    Dim schedulePaymentD As SchedulePaymentDetail = listSchedulePaymentDetailBySupplier.Where(Function(x) x.DistributionLineId = distributionLineId).Cast(Of SchedulePaymentDetail).FirstOrDefault()
                                    Dim valueIVA As Decimal = 0
                                    With voucherTransactionDetail
                                        .IdEntityBankAccount = Nothing 'schedulePayment.EntityBankAccountId
                                        .IdThirdParty = third.Id
                                        .SupplierBankAccountId = supplierPaymentBankAccount.SupplierBankAccountId
                                        .IdExpenseConcept = schedulePaymentD.ExpenseConceptId 'Sacar de SchedulePaymentDetail Agregar Linea y Concepto de Egreso y Cuenta Contable 
                                        .IdMainAccount = schedulePaymentD.MainAccountId 'Cuenta contable de la linea de distribucion que esta en SchedulePaymentDetail
                                        .Nature = schedulePaymentD.Nature 'Naturaleza debe estar persistida en SchedulePaymentDetail
                                        .IdCostCenter = schedulePayment.CostCenterId
                                        .Value = voucherTransactionDetail.DischargeBill.Sum(Function(x) x.ValueInCurrencyHeader)
                                        .ValueIVA = valueIVA
                                        .TotalConcept = voucherTransactionDetail.DischargeBill.Sum(Function(x) x.ValueInCurrencyHeader) + valueIVA
                                        .IdRetentionConcept = Nothing
                                        .PercentRetention = Nothing
                                    End With
                                    Dim concept As ExpenseConcepts = _expenseConceptoRepository.GetExpenseConceptById(schedulePaymentD.ExpenseConceptId, False)
                                    If concept IsNot Nothing AndAlso concept.Id > 0 Then
                                        description &= ", Concepto : " & concept.Code
                                    End If
                                    If invoices IsNot Nothing AndAlso invoices.Any() Then
                                        description &= ", Facturas : (" & String.Join("-", invoices) & ")"
                                    End If
                                    voucherTransactionDetail.Observation = description
                                    voucherTransaction.VoucherTransactionDetails.Add(voucherTransactionDetail)
                                Next

                                'cabecera del comprobante de egresos
                                With voucherTransaction
                                    .Code = String.Empty
                                    .IdThirdParty = third.Id
                                    .IdMainAccount = entityBankAccount.IdMainAccount
                                    .IdCostCenter = schedulePayment.CostCenterId
                                    .VoucherClass = 1 'Clase de comprobante (Pagos)
                                    .ExpenseType = 1 'Cuenta Bancaria
                                    .Detail = headerDetail
                                    .DocumentDate = Date.Now
                                    .IdEntityBankAccount = entityBankAccount.Id
                                    .SupplierBankAccountId = supplierPaymentBankAccount.SupplierBankAccountId
                                    .PaymentMethod = schedulePayment.PaymentMethod
                                    .NoteNumber = schedulePayment.NumberNote
                                    .IdChecks = schedulePayment.CheckId
                                    .IsDispersionFundGenerated = True
                                    If .IdChecks <> 0 Then
                                        '.CheckNumber = 0 'Se genera en VoucherTransaction
                                        .TransactionDate = Date.Now
                                    End If

                                    If voucherTransaction.CheckNumber Is Nothing Then
                                        .CheckNumber = 0
                                    End If
                                    .TaxByMil = schedulePayment.TaxByMil
                                    If .TaxByMil Then
                                        .TaxByMilValue = TreasuryStaticServices.CalculateRateByMilValue(entityBankAccount.Rate, voucherTransaction.VoucherTransactionDetails.Sum(Function(x) x.Value)).Item1
                                    Else
                                        .TaxByMilValue = 0
                                    End If
                                    .Value = voucherTransaction.VoucherTransactionDetails.Sum(Function(x) x.Value) - .TaxByMilValue 'Sacarlos de la suma de los detalles
                                    .CashRegisterExpense = False
                                    .RefundCashRegisterExpense = False
                                    .SchedulePaymentId = schedulePayment.Id
                                    .BeneficiaryIdentification = third.Nit
                                    .Beneficiary = third.Name
                                    .TransactionRelationship = False
                                    .CheckReconciled = False
                                    .IdPaymentOrder = Nothing
                                    .Printed = False
                                    .RTEValue = 0
                                    .IVAValue = 0
                                    .ICAValue = 0
                                    .OtherValue = 0
                                    .BankAccountNumber = entityBankAccount.Number
                                    .BankName = entityBankAccount.Bank.Name
                                    .DetailsInterfaceBudget = Nothing
                                    .IdUnitOperative = schedulePayment.OperativeUnitId
                                    .IdRefund = Nothing
                                    .Status = 1 'Estado Registrado
                                    .CreationDate = Date.Now
                                    .CreationUser = audit.CodeUser
                                    .CurrencyId = currencyBank?.Id
                                End With

                                Dim listAux As New List(Of Int32)(voucherTransaction.SchedulePaymentDetail.Select(Function(o) o.Id).ToArray())
                                'Guarda y confirma el comprobante de egresos generado
                                Dim resultVoucherTransaction As ActionResult(Of VoucherTransaction) = _voucherTransactionAdminService.SaveVoucherTransaction(voucherTransaction, audit, True, idSequence, sequenceC, True)
                                Dim messResult As String
                                If resultVoucherTransaction.StateResult Then
                                    dicVourcherTransactionSchedulePaymentDetal.Add(resultVoucherTransaction.ObjectEmbbeded.Id, New List(Of Int32)(listAux))
                                    If resultVoucherTransaction.ObjectEmbbeded.Status = 2 Then 'ResourceManager.GetString("VoucherTransactionListConfirm", NAME_MODULE)
                                        messResult = String.Format("Tercero ({0}) Comprobante de Egreso: {1}", String.Concat(third.Nit, " - ", third.Name), resultVoucherTransaction.Message)
                                        messageList.Add(New Tuple(Of String, Integer)(messResult, eMessageType.Confirm)) ' Se guarda y confirma el documento
                                    ElseIf resultVoucherTransaction.ObjectEmbbeded.Status <> 2 And Not FlagToNoteConfirm Then
                                        'ResourceManager.GetString("VoucherTransactionListSaveButNotConfirm", NAME_MODULE)
                                        messResult = String.Format("Tercero ({0}) Comprobante de Egreso: {1}", String.Concat(third.Nit, " - ", third.Name), resultVoucherTransaction.Message)
                                        messageList.Add(New Tuple(Of String, Integer)(messResult, eMessageType.Warning)) ' se genera el comprobante pero hubo problemas al confirmar
                                    Else
                                        messResult = String.Format(ResourceManager.GetString("VoucherTransactionGeneratedError", NAME_MODULE), String.Concat(third.Nit, " - ", third.Name), resultVoucherTransaction.Message)
                                        messageList.Add(New Tuple(Of String, Integer)(messResult, eMessageType.Errors)) ' ni se genera y confirma el comprobante de egreso
                                        _voucherTransactionRepository.UnitWork.RollbackChanges()
                                    End If
                                Else
                                    messResult = String.Format(ResourceManager.GetString("VoucherTransactionGeneratedError", NAME_MODULE), String.Concat(third.Nit, " - ", third.Name), resultVoucherTransaction.Message)
                                    messageList.Add(New Tuple(Of String, Integer)(messResult, eMessageType.Errors)) ' ni se genera y confirma el comprobante de egreso
                                    _voucherTransactionRepository.UnitWork.RollbackChanges()
                                End If
                            Next
                            Dim countConfirmList As Integer = messageList.Where(Function(x) x.Item2 = eMessageType.Confirm).Cast(Of Tuple(Of String, Integer)).ToList().Count
                            Dim countWarningList As Integer = messageList.Where(Function(x) x.Item2 = eMessageType.Warning).Cast(Of Tuple(Of String, Integer)).ToList().Count
                            Dim countErrorList As Integer = messageList.Where(Function(x) x.Item2 = eMessageType.Errors).Cast(Of Tuple(Of String, Integer)).ToList().Count

                            'Dado que se presentó un error, y lo mas posible es que se realice un rollback del voucher transaction, devolvemos el mensaje de error
                            If countErrorList > 0 Then
                                Dim listError As New StringBuilder()
                                For Each item In messageList
                                    If item.Item2 = eMessageType.Errors Then
                                        listError.AppendLine(item.Item1)
                                    End If
                                Next

                                scope.Dispose()
                                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = listError.ToString()}
                            ElseIf countErrorList = 0 AndAlso (countWarningList <> 0 OrElse countConfirmList <> 0) Then 'Generado total (cuando no hay mensajes de error)
                                schedulePayment.Status = 5
                                schedulePayment.PartialPaymentUser = audit.CodeUser
                                schedulePayment.PartialPaymentDate = Date.Now
                            ElseIf countErrorList <> 0 AndAlso (countConfirmList <> 0 OrElse countWarningList <> 0) Then
                                schedulePayment.Status = 4
                                schedulePayment.FullPaymentUser = audit.CodeUser
                                schedulePayment.FullPaymentDate = Date.Now
                            End If

                            For Each sc In schedulePayment.SchedulePaymentDetail
                                sc.VoucherTransactionId = dicVourcherTransactionSchedulePaymentDetal.Where(Function(kv) kv.Value.Any(Function(i) i = sc.Id)).FirstOrDefault().Key
                            Next

                            Dim resultSchedulePayment = _schedulePaymentAdminService.SaveSchedulePayment(schedulePayment, audit, False, idSequence)
                            If resultSchedulePayment.StateResult = False Then
                                scope.Dispose()
                                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SchedulePaymentUpdateStateError", NAME_MODULE)}
                            End If
                            scope.Complete()
                            Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = messageList, .Message = ""}
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("MainAccountNotFound", NAME_MODULE)}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("InvoiceNotFound", NAME_MODULE)}
                    End If
                Else
                    scope.Dispose()
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorUnknown")}
                End If
            End Using
            Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Nothing}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")

            Dim message As String = String.Empty

            Dim listError As New StringBuilder()
            For Each item In messageList
                If item.Item2 = eMessageType.Errors Then
                    listError.AppendLine(item.Item1)
                End If
            Next

            If String.IsNullOrEmpty(listError.ToString()) Then
                message = IndigoManagementExceptions.GetExceptionDetails(ex)
            Else
                message = listError.ToString()
            End If

            Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = message}
        End Try
    End Function

    ''' <summary>
    ''' funcion para crea la nota debito del descuento de la Cxp
    ''' </summary>
    ''' <param name="SchedulePaymentDetail"></param>
    ''' <param name="AccountPayable">Cuenta por pagar</param>
    ''' <param name="DistributionLine">Linea de distribucion</param>
    ''' <param name="OperativeUnitId">Unidad operativa</param>
    ''' <param name="audit"></param>
    ''' <param name="SettingCxP">parametros cxp</param>
    ''' <param name="Comment">Comentario</param>
    ''' <returns></returns>
    Private Function CreateNote(SchedulePaymentDetail As SchedulePaymentDetail, AccountPayable As AccountPayable, DistributionLine As DistributionLines, OperativeUnitId As Integer, audit As AuditMessage,
            SettingCxP As SettingPayments, SchedulePaymentId As Integer, SchedulePaymentCode As String, Optional Comment As String = "") As ActionResult
        Try
            Dim StringBuilder = New StringBuilder
            Dim IdSequense As Integer = 0
            Dim PaymentNote = New PaymentNotes
            Dim officialCurrency = _companySettingsRepository.FirstOrDefault(Function(x) True, False, {"Currency"})?.Currency

            If SchedulePaymentDetail Is Nothing OrElse SchedulePaymentDetail.Id = 0 Then
                Throw New Exception("El detalle de la programación de pago esta vacia")
            End If

            If SettingCxP Is Nothing OrElse SettingCxP.Id = 0 Then
                Throw New Exception("No se encontró parametros de cuentas por pagar")
            End If

            If AccountPayable Is Nothing OrElse AccountPayable.Id = 0 OrElse Not AccountPayable.AccountPayableShares.Any() Then
                Throw New Exception("No se encontró la cuenta por pagar")
            End If

            If DistributionLine Is Nothing OrElse DistributionLine.Id = 0 Then
                Throw New Exception("No se encontró la linea de distribución")
            End If

            Dim SequensePayment = Me._sequensePaymentsCRepository.GetSequenseByIdForm("731")
            If Not SequensePayment.IsManual Then
                Select Case SequensePayment.Scope
                    Case "O"
                        IdSequense = SequensePayment.PaymentsSecuenceDetail(0).Id
                    Case "OU"
                        IdSequense = SequensePayment.PaymentsSecuenceDetail.SingleOrDefault(Function(t) t.IdOperatingUnit = OperativeUnitId).Id
                End Select
            End If

            With AccountPayable
                .Adjustment = SchedulePaymentDetail.DiscountValue
                .Percentage = Math.Round(((100 * SchedulePaymentDetail.DiscountValue) / .Value), 2)
                .HandlesAddModifyDelete = 1
            End With

            'se realiza el descuento a las cuotas
            Dim adjustement As Decimal = AccountPayable.Adjustment
            If AccountPayable.Adjustment <= AccountPayable.Balance Then
                For Each i As AccountPayableShares In AccountPayable.AccountPayableShares.ToList()
                    If i.Balance <= adjustement Then
                        adjustement -= i.Balance
                        i.ValueNoteShare = i.Balance
                    Else
                        i.ValueNoteShare = adjustement
                        adjustement = 0
                    End If
                Next
            Else
                Throw New Exception($"El valor del ajuste no puede ser mayor al saldo de la CxP : {AccountPayable.BillNumber} ")
            End If

            With PaymentNote
                .Code = ""
                .NoteDate = DateTime.Now
                .IdSupplier = SchedulePaymentDetail.SupplierId
                .Comment = Comment
                .Nature = 1
                .Reinstatement = False
                .IdVoucherTransaction = Nothing
                .IdSupplierDistributionLines = DistributionLine.SuppliersDistributionLines.Where(Function(f) f.IdSupplier = SchedulePaymentDetail.SupplierId).FirstOrDefault.Id
                .IndicatesBillAdvance = 0
                .CancelCheck = Nothing
                .BudgetInterface = SettingCxP.BudgetInterface
                .IdOperatingUnit = OperativeUnitId
                .Status = 1
                .EntityName = "SchedulePayment"
                .EntityId = SchedulePaymentId
                .EntityCode = SchedulePaymentCode
                'Se guarda moneda oficial por el momento, pero se necesita agregar multimoneda al proceso'
                .CurrencyId = officialCurrency.Id


                'Si la cxp proviene desde inventario sus detalles se crean como estaba antiguamente
                If AccountPayable.EntityName = "EntranceVoucher" Then
                    Dim PaymentNoteDetails = New PaymentsNoteDetails
                    With PaymentNoteDetails
                        .IdAccountPayableConceptNotes = DistributionLine.AccountPayableConceptNotesId
                        .IdAccount = DistributionLine.MainAccountAccountPayableConceptNotesId
                        .IdThirdParty = SchedulePaymentDetail.ThirdPartyId
                        .IdCostCenter = AccountPayable.IdCostCenter
                        .Comments = "Descuento Pronto Pago"
                        .Value = SchedulePaymentDetail.DiscountValue
                        .BillingValue = 0
                        .Nature = CByte(2)
                        .BaseValue = SchedulePaymentDetail.DiscountValue
                        .HandlesMainAccountByConcept = 2
                        .TotalConceptValue = SchedulePaymentDetail.DiscountValue
                    End With
                    PaymentNote.PaymentsNoteDetails.Add(PaymentNoteDetails)
                Else

                    ''si proviene de dispersion de fondos, o cxp
                    ''se obtiene la suma total de la base de los conceptos que sean tipo resultado o de tipo balance (que cumplan con: naturaleza debito y tipo de rentencion ninguna.)
                    Dim totalConcepts = AccountPayable.AccountPayableDetailConcept.
                        Where(Function(x) x.MainAccounts.MainAccountClasses.Type = 2 OrElse (x.MainAccounts.MainAccountClasses.Type = 1 AndAlso x.MainAccounts.Nature = 1 AndAlso x.MainAccounts.RetencionType = 0)).ToList().
                        Sum(Function(x) x.BaseValue)

                    Dim sumaProrrateo As New Decimal
                    ''se recorre cada concepto de la cxp
                    For Each item In AccountPayable.AccountPayableDetailConcept

                        'se valida que sean tipo resultado o balance(Con ciertas caracteristicas que describo en la parte superior)
                        If item.MainAccounts.MainAccountClasses.Type = 2 OrElse (item.MainAccounts.MainAccountClasses.Type = 1 AndAlso item.MainAccounts.Nature = 1 AndAlso item.MainAccounts.RetencionType = 0) Then

                            'la linea de distribucion debe tener parametrizado un concepto general
                            If DistributionLine.AccountPayableConceptNotesId <= 0 Then
                                Return New ActionResult With {.StateResult = False, .Message = $"Error Creando la Nota de la CxP{AccountPayable.BillNumber}, la linea de distribucion no tiene asociado un concepto general"}
                            Else
                                Dim PaymentNoteDetails = New PaymentsNoteDetails
                                With PaymentNoteDetails

                                    ''se obtiene el % del prorrateo de cada concepto
                                    Dim prorrateo = item.BaseValue / totalConcepts
                                    ''basado en el % se obtiene el valor del prorrateo de cada concepto
                                    Dim valueProrrateo = Math.Round(SchedulePaymentDetail.DiscountValue * prorrateo, 2, MidpointRounding.AwayFromZero)
                                    sumaProrrateo += valueProrrateo
                                    .IdAccountPayableConceptNotes = DistributionLine.AccountPayableConceptNotesId
                                    .IdAccount = item.IdAccount
                                    .IdThirdParty = SchedulePaymentDetail.ThirdPartyId
                                    .IdCostCenter = item.IdCostCenter
                                    .Comments = "Descuento Pronto Pago"
                                    .Value = valueProrrateo
                                    .BillingValue = 0
                                    .Nature = CByte(2)
                                    .BaseValue = valueProrrateo
                                    .HandlesMainAccountByConcept = 2
                                    .TotalConceptValue = valueProrrateo
                                End With
                                PaymentNote.PaymentsNoteDetails.Add(PaymentNoteDetails)
                            End If
                        End If
                    Next

                    ''se mira si hay diferencia entre el valor real(SchedulePaymentDetail.DiscountValue) y el valor dado por el prorrateo(sumaProrrateo)
                    If sumaProrrateo <> SchedulePaymentDetail.DiscountValue Then

                        ''se obtiene la diferencia de estos
                        ''NOTA: la diferencia siempre se tiene que agregar o quitar de los detalles ya que se debe hacer la nota con el valor 
                        ''real del descuento que seria SchedulePaymentDetail.DiscountValue
                        Dim diff = SchedulePaymentDetail.DiscountValue - sumaProrrateo

                        ''se busca el registro que tenga el valor mas alto para asi poder restarle la diferencia y que la nota no tenga desbalance
                        With PaymentNote.PaymentsNoteDetails.Where(Function(x) x.Value > diff).FirstOrDefault()
                            .Value += diff
                            .BaseValue += diff
                            .TotalConceptValue += diff
                        End With

                    End If
                End If

            End With

            Dim Result = _notesDebitCreditAdminService.SavePaymentNotesComplete(PaymentNote, {AccountPayable}.ToList(), Nothing, True, audit, IdSequense, True)
            If Result Is Nothing OrElse Result.StateResult = False Then
                Return New ActionResult With {.StateResult = False, .Message = $"Error Creando la Nota de la CxP{AccountPayable.BillNumber},{Result?.Message}"}
            End If

            Return New ActionResult With {.StateResult = True, .Message = Result.Message}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

#End Region

    ''' <summary>
    ''' metodo para generar el archivo para pagos en bancos
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="bankId"></param>
    ''' <returns></returns>
    Public Function GenerateBankFile(SchedulePayment As SchedulePayment, bankId As Integer, companyNIT As String, companyName As String, Optional optionalParameters As List(Of String) = Nothing) As ActionResult(Of String) Implements IDispersionFundAdminService.GenerateBankFile
        Try
            If SchedulePayment.fromVoucherTransaction IsNot Nothing Then
                Dim res = Me.GenerateSchedulePaymentByVoucherTransaction(SchedulePayment.fromVoucherTransaction)
                If res.StateResult Then
                    SchedulePayment = res.ObjectEmbbeded
                Else
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = res.Message}
                End If
            End If
            Return _treasuryService.GenerateBankFile(SchedulePayment, bankId, companyNIT, companyName, optionalParameters)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Genera un objeto de tipo SchedulePayment basado en VoucherTransaction
    ''' </summary>
    ''' <param name="voucherTransaction"></param>
    ''' <returns></returns>
    Private Function GenerateSchedulePaymentByVoucherTransaction(voucherTransaction As VoucherTransaction) As ActionResult(Of SchedulePayment)
        If Not voucherTransaction.VoucherTransactionDetails.Any() Then
            Return New ActionResult(Of SchedulePayment) With {.StateResult = False, .Message = "El documento no tiene detalles"}
        End If
        Try
            Dim _schedulePayment As New SchedulePayment With {.Code = voucherTransaction.Code,
                                                          .ScheduledDate = voucherTransaction.DocumentDate,
                                                          .EntityBankAccountId = voucherTransaction.IdEntityBankAccount,
                                                          .CostCenterId = voucherTransaction.IdCostCenter,
                                                          .PaymentMethod = voucherTransaction.PaymentMethod,
                                                          .CheckId = voucherTransaction.IdChecks,
                                                          .NumberNote = voucherTransaction.NoteNumber,
                                                          .TaxByMil = voucherTransaction.TaxByMil,
                                                          .OperativeUnitId = voucherTransaction.IdUnitOperative,
                                                          .Status = 5,
                                                          .CreationUser = voucherTransaction.CreationUser,
                                                          .CreationDate = DateTime.Now,
                                                          .PaymentDate = voucherTransaction.DocumentDate}
            Dim supplier = _supplierRepository.GetSupplierByIdThirdParty(voucherTransaction.IdThirdParty)
            If supplier Is Nothing Then
                Return New ActionResult(Of SchedulePayment) With {.StateResult = False, .Message = ($"No se encontro el proveedor {voucherTransaction.FullNameThird}")}
            End If
            For Each details In voucherTransaction.VoucherTransactionDetails
                If details.SupplierBankAccountId IsNot Nothing Then
                    _schedulePayment.SchedulePaymentBankAccount.Add(New SchedulePaymentBankAccount With
                                                                {.SupplierId = supplier.Id,
                                                                .SupplierBankAccountId = details.SupplierBankAccountId
                                                                })
                End If
                _schedulePayment.SchedulePaymentDetail.Add(New SchedulePaymentDetail With
                                                           {
                                                            .Id = details.Id,
                                                            .SupplierId = supplier.Id,
                                                            .ThirdPartyId = details.IdThirdParty,
                                                            .DistributionLineId = 0, ' supplier.SuppliersDistributionLines(0)?.Id,
                                                            .ExpenseConceptId = details.IdExpenseConcept,
                                                            .MainAccountId = details.IdMainAccount,
                                                            .Nature = details.Nature,
                                                            .AccountPayableId = 0,
                                                            .AccountPayableShareId = 0,
                                                            .AmountPaid = details.Value,
                                                            .AmountPercent = 0,
                                                            .PaymentConceptId = 0,
                                                            .Paid = 0,
                                                            .Description = details.Detail,
                                                            .GeneratedVoucher = 0,
                                                            .DiscountValue = 0
                                                           })
            Next
            Return New ActionResult(Of SchedulePayment) With {.StateResult = True, .ObjectEmbbeded = _schedulePayment}
        Catch ex As Exception
            Return New ActionResult(Of SchedulePayment) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _voucherTransactionAdminService.Dispose()
                _sequenceAdminService.Dispose()
                _schedulePaymentAdminService.Dispose()
                _treasuryService.Dispose()
            End If
            _supplierRepository = Nothing
            _entityBankAccountRepository = Nothing
            _schedulePaymentRepository = Nothing
            _voucherTransactionRepository = Nothing
            _secuenseDRepository = Nothing
            _voucherTransactionAdminService = Nothing
            _sequenceAdminService = Nothing
            _schedulePaymentAdminService = Nothing
            _schedulePaymentDetailRepository = Nothing
            _treasuryService = Nothing
            _expenseConceptoRepository = Nothing
            Me._schedulePaymentBankAccountRepository = Nothing
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

Public Enum eMessageType
    Confirm = 1
    Errors = 2
    Warning = 3
End Enum