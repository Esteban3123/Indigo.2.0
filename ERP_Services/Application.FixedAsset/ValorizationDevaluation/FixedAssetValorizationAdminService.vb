'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/04/2016
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
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Text
Imports Domain.Entities.Service
Imports System.Transactions
Imports Application.Payments
Imports Application.Accounting
Imports Application.Common
Imports System.Net


#End Region

Public Class FixedAssetValorizationAdminService
    Implements IFixedAssetValorizationAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _valorizationDevaluationRepository As IFixedAssetValorizationRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IFixedAssetSequenceDetailRepository

    ''' <summary>
    ''' Repositorio para la tarifa de IVA
    ''' </summary>
    ''' <remarks></remarks>
    Private _generalLedgerIVARepository As IGeneralLedgerIVARepository
    ''' <summary>
    ''' Dominio
    ''' </summary>
    ''' <remarks></remarks>
    Private _fixedAssetServices As IFixedAssetServices

    Private _FixedAssetEntryRepository As IFixedAssetEntryRepository

    ''' <summary>
    ''' Repositorio de secuencia numerica para pagos
    ''' </summary>
    ''' <remarks></remarks>
    Private _sequensePaymentsCRepository As ISequensePaymentsCRepository

    ''' <summary>
    ''' Repositorio de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierRepository As ISupplierRepository

    ''' <summary>
    ''' Linea de distribucion y proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierDistributionLineRepository As ISuppliersDistributionLinesRepository

    ''' <summary>
    ''' Repositorio para concepto de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentsConceptRepository As IPaymentsConceptRepository

    ''' <summary>
    ''' Aplicacion de cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableAdminService As IAccountPayableAdminService

    'Repositorio para el comprobante
    Private _accountingRepository As IAccountingDocumentAdminService

    ''' <summary>
    ''' Repositorio de viebot
    ''' </summary>
    ''' <remarks></remarks>
    Private _vieBotRepository As IVieBotRepository

    Private _currencyAdminService As ICurrencyAdminService

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(valorizationDevaluationRepository As IFixedAssetValorizationRepository, secuenseDRepository As IFixedAssetSequenceDetailRepository, fixedAssetServices As FixedAssetServices, FixedAssetEntryRepository As IFixedAssetEntryRepository,
                   sequensePaymentsCRepository As ISequensePaymentsCRepository, supplierRepository As ISupplierRepository, supplierDistributionLineRepository As ISuppliersDistributionLinesRepository,
                   paymentsConceptRepository As IPaymentsConceptRepository, accountPayableAdminService As IAccountPayableAdminService, accountingRepository As IAccountingDocumentAdminService,
                   vieBotRepository As IVieBotRepository, CurrencyAdminService As ICurrencyAdminService, GeneralLedgerIVARepository As IGeneralLedgerIVARepository)
        If valorizationDevaluationRepository Is Nothing Then
            Throw New ArgumentNullException("valorizationDevaluationRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If sequensePaymentsCRepository Is Nothing Then
            Throw New ArgumentNullException("sequensePaymentsCRepository")
        End If
        If supplierRepository Is Nothing Then
            Throw New ArgumentNullException("supplierRepository")
        End If
        If supplierDistributionLineRepository Is Nothing Then
            Throw New ArgumentNullException("supplierDistributionLineRepository")
        End If
        If paymentsConceptRepository Is Nothing Then
            Throw New ArgumentNullException("paymentsConceptRepository")
        End If
        If accountPayableAdminService Is Nothing Then
            Throw New ArgumentNullException("accountPayableAdminService")
        End If
        If accountingRepository Is Nothing Then
            Throw New ArgumentNullException("accountingRepository")
        End If
        If vieBotRepository Is Nothing Then
            Throw New ArgumentNullException("vieBotRepository")
        End If
        _valorizationDevaluationRepository = valorizationDevaluationRepository
        _secuenseDRepository = secuenseDRepository
        _fixedAssetServices = fixedAssetServices
        _FixedAssetEntryRepository = FixedAssetEntryRepository
        _sequensePaymentsCRepository = sequensePaymentsCRepository
        _supplierRepository = supplierRepository
        _supplierDistributionLineRepository = supplierDistributionLineRepository
        _paymentsConceptRepository = paymentsConceptRepository
        _accountPayableAdminService = accountPayableAdminService
        _accountingRepository = accountingRepository
        _vieBotRepository = vieBotRepository
        _currencyAdminService = CurrencyAdminService
        _generalLedgerIVARepository = GeneralLedgerIVARepository
    End Sub

    ''' <summary>
    ''' Anula
    ''' </summary>
    ''' <param name="ValorizationDevaluation"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AnnularValorizationDevaluation(ValorizationDevaluation As ValorizationDevaluation, audit As AuditMessage) As ActionResult(Of ValorizationDevaluation) Implements IFixedAssetValorizationAdminService.AnnularValorizationDevaluation

    End Function

    ''' <summary>
    ''' Obtiene por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValorizationDevaluationByCode(Code As String, audit As AuditMessage) As ActionResult(Of FixedAssetTransaction) Implements IFixedAssetValorizationAdminService.GetValorizationDevaluationByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetTransaction As FixedAssetTransaction = Me._valorizationDevaluationRepository.GetTransactionByCode(Code.Trim())
            If FixedAssetTransaction IsNot Nothing AndAlso FixedAssetTransaction.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetTransaction)(FixedAssetTransaction, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetTransaction) With {.StateResult = True, .ObjectEmbbeded = FixedAssetTransaction}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetTransaction) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValorizationDevaluationById(Id As Integer, audit As AuditMessage) As ActionResult(Of ValorizationDevaluation) Implements IFixedAssetValorizationAdminService.GetValorizationDevaluationById
        If Id = 0 Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ValorizationDevaluation As ValorizationDevaluation = Me._valorizationDevaluationRepository.GetValorizationDevaluationById(Id)
            If ValorizationDevaluation IsNot Nothing AndAlso ValorizationDevaluation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ValorizationDevaluation)(ValorizationDevaluation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ValorizationDevaluation) With {.StateResult = True, .ObjectEmbbeded = ValorizationDevaluation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ValorizationDevaluation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza
    ''' </summary>
    ''' <param name="FixedAssetTransaction"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveValorizationDevaluation(FixedAssetTransaction As FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook As List(Of FixedAssetTransactionDetailBook), ListDeleteFixedAssetTransactionDetail As List(Of FixedAssetTransactionDetail), audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetTransaction) Implements IFixedAssetValorizationAdminService.SaveValorizationDevaluation
        If FixedAssetTransaction Is Nothing Then
            Throw New ArgumentNullException("FixedAssetEntry")
        End If

        'Validaciones
        If FixedAssetTransaction.GenerateAccountPayable = True Then
            Dim ListErrors As New StringBuilder()
            If FixedAssetTransaction.SupplierId Is Nothing OrElse FixedAssetTransaction.SupplierDistributionLineId Is Nothing Then
                ListErrors.AppendLine("No se ha definido el proveedor con el cual generar la cuenta por pagar")
            End If
            If FixedAssetTransaction.SupplierTypeId Is Nothing Then
                ListErrors.AppendLine("No se ha definido el tipo de proveedor")
            End If
            If FixedAssetTransaction.InvoiceNumber Is Nothing Then
                ListErrors.AppendLine("No se ha definido el número de la factura con la cual realizar la cuenta por pagar")
            End If
            If FixedAssetTransaction.InvoiceDate Is Nothing Then
                ListErrors.AppendLine("No se ha definido la fecha de la factura con la cual realizar la cuenta por pagar")
            End If
            If ListErrors.Length > 0 Then
                Return New ActionResult(Of FixedAssetTransaction) With {.StateResult = False, .MessageResult = {ListErrors.ToString}.ToList}
            End If
        End If

        Dim unitOfWork As IUnitWork = Me._valorizationDevaluationRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                Dim ListString As List(Of String) = ConvertToXmlListDeletes(ListDeleteFixedAssetTransactionDetailBook, ListDeleteFixedAssetTransactionDetail, FixedAssetTransaction.Status)
                Dim EntityXml As String = ConvertXmlTransaction(FixedAssetTransaction)
                Dim resultStore = _valorizationDevaluationRepository.SP_SaveValorizationDevaluation(EntityXml, ListString, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of FixedAssetTransaction) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList}
                End If

                FixedAssetTransaction.Id = resultStore.Id
                FixedAssetTransaction.Code = resultStore.Code
                FixedAssetTransaction.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of FixedAssetTransaction) With {.StateResult = True, .ObjectEmbbeded = FixedAssetTransaction}
            Catch ex As DbUpdateException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of FixedAssetTransaction) With {.StateResult = False, .MessageResult = {DirectCast(ex.InnerException, System.Data.Entity.Core.UpdateException).InnerException.Message}.ToList()}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of FixedAssetTransaction) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")

                Dim message As String = ex.Message
                If ex.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.Message) Then
                    message = ex.InnerException.Message
                End If

                Return New ActionResult(Of FixedAssetTransaction) With {.StateResult = False, .MessageResult = {message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Genera el comprobante contable siempre y cuando la transacción no realice cxp
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateJournalVoucher(FixedAssetTransaction As FixedAssetTransaction, SettingFixedAsset As SettingFixedAsset, LegalBook As LegalBook) As ActionResult(Of JournalVouchers)
        'Cabecera del comprobante
        Dim journalVoucher As New JournalVouchers
        'Detalle del comprobante
        Dim journalVoucherDetail As New JournalVoucherDetails

        'Se genera la cabecera del comprobante
        With journalVoucher
            .LegalBookId = LegalBook.Id
            .IdJournalVoucher = SettingFixedAsset.IdValorizationDevaluationAccountingVoucher
            .VoucherDate = FixedAssetTransaction.DocumentDate
            .Imported = False
            .Status = 2
            .Detail = "Comprobante contable generado desde Transacciones"
            .EntityCode = FixedAssetTransaction.Code
            .EntityId = FixedAssetTransaction.Id
            .EntityName = GetType(FixedAssetTransaction).Name
            .IsClosedYear = False

            'Se generan los detalles del comprobante recorriendo los detalles de la transacción
            For Each itemTransactionDetail As FixedAssetTransactionDetail In FixedAssetTransaction.FixedAssetTransactionDetail
                If itemTransactionDetail.TransactionClass = 1 Then 'Si es activo

                    'Se obtiene el activo
                    Dim physicalAsset As FixedAssetPhysicalAsset = _valorizationDevaluationRepository.GetPhysicalAssetWithFixedAssetCatalog(itemTransactionDetail.PhysicalAssetId)

                    If itemTransactionDetail.TransactionType = 1 Then 'Si el tipo de transacción es Valorización
                        'Siempre se debita la cuenta 'Cuenta de propiedad, planta y equipo' del catalogo de equipos y con el valor que digiten
                        journalVoucherDetail = New JournalVoucherDetails
                        journalVoucherDetail.IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.IncomeAccountId
                        journalVoucherDetail.IdThirdParty = FixedAssetTransaction.ThirdPartyId
                        journalVoucherDetail.DebitValue = itemTransactionDetail.Value
                        journalVoucherDetail.CreditValue = 0
                        journalVoucherDetail.IdCostCenter = FixedAssetTransaction.CostCenterId
                        journalVoucherDetail.Detail = "Detalle generado desde Transacciones"
                        'Se agrega el detalle al objeto principal
                        .JournalVoucherDetails.Add(journalVoucherDetail)

                        If itemTransactionDetail.AffectDepreciation = False Then 'Si no afecta depreciación: Al crédito la cuenta 'Crédito valorización' del catálogo y el valor el que digiten
                            journalVoucherDetail = New JournalVoucherDetails
                            journalVoucherDetail.IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.CreditValorizationAccountId
                            journalVoucherDetail.IdThirdParty = FixedAssetTransaction.ThirdPartyId
                            journalVoucherDetail.DebitValue = 0
                            journalVoucherDetail.CreditValue = itemTransactionDetail.Value
                            journalVoucherDetail.IdCostCenter = FixedAssetTransaction.CostCenterId
                            journalVoucherDetail.Detail = "Detalle generado desde Transacciones"
                        Else 'Si afecta depreciación: Al crédito la cuenta 'Cuenta contable de servicios' de parámetros
                            journalVoucherDetail = New JournalVoucherDetails
                            journalVoucherDetail.IdMainAccount = SettingFixedAsset.ServiceMainAccountId
                            journalVoucherDetail.IdThirdParty = FixedAssetTransaction.ThirdPartyId
                            journalVoucherDetail.DebitValue = 0
                            journalVoucherDetail.CreditValue = itemTransactionDetail.Value
                            journalVoucherDetail.IdCostCenter = FixedAssetTransaction.CostCenterId
                            journalVoucherDetail.Detail = "Detalle generado desde Transacciones"
                        End If

                        'Se agrega el detalle al objeto principal
                        .JournalVoucherDetails.Add(journalVoucherDetail)
                    Else 'Si el tipo de transacción es Desvalorización
                        'Al crédito la cuenta propiedad, plantas y equipos con el valor que digitan
                        journalVoucherDetail = New JournalVoucherDetails
                        journalVoucherDetail.IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.IncomeAccountId
                        journalVoucherDetail.IdThirdParty = FixedAssetTransaction.ThirdPartyId
                        journalVoucherDetail.DebitValue = 0
                        journalVoucherDetail.CreditValue = itemTransactionDetail.Value
                        journalVoucherDetail.IdCostCenter = FixedAssetTransaction.CostCenterId
                        journalVoucherDetail.Detail = "Detalle generado desde Transacciones"
                        'Se agrega el detalle al objeto principal
                        .JournalVoucherDetails.Add(journalVoucherDetail)

                        'Al débito la cuenta debito desvalorización con el valor que digitan
                        journalVoucherDetail = New JournalVoucherDetails
                        journalVoucherDetail.IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.DebitDevaluationAccountId
                        journalVoucherDetail.IdThirdParty = FixedAssetTransaction.ThirdPartyId
                        journalVoucherDetail.DebitValue = itemTransactionDetail.Value
                        journalVoucherDetail.CreditValue = 0
                        journalVoucherDetail.IdCostCenter = FixedAssetTransaction.CostCenterId
                        journalVoucherDetail.Detail = "Detalle generado desde Transacciones"
                        'Se agrega el detalle al objeto principal
                        .JournalVoucherDetails.Add(journalVoucherDetail)
                    End If

                End If
            Next
        End With
        If journalVoucher.JournalVoucherDetails Is Nothing OrElse journalVoucher.JournalVoucherDetails.Count = 0 Then
            Return New ActionResult(Of JournalVouchers) With {.StatusCode = eStatusResult.WARNING, .Message = "El comprobante contable no tiene detalles debido a que los detalles de la transacción son de tipo partes."}
        End If
        Return New ActionResult(Of JournalVouchers) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = journalVoucher}
    End Function

    ''' <summary>
    ''' Confirma
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmValorizationDevaluation(FixedAssetTransaction As FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook As List(Of FixedAssetTransactionDetailBook), ListDeleteFixedAssetTransactionDetail As List(Of FixedAssetTransactionDetail), audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetTransaction) Implements IFixedAssetValorizationAdminService.ConfirmValorizationDevaluation
        If FixedAssetTransaction Is Nothing Then
            Throw New ArgumentNullException("FixedAssetTransaction")
        End If

        'Se consulta la secuencia de pagos por el id del form
        Dim sequensePayments As PaymentsSecuence = _sequensePaymentsCRepository.GetSequenseByIdForm("730")
        Dim AccountPayableSequenseDetailId As Integer = 0
        If sequensePayments.Id = 0 Then
            Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe secuencia numérica para el formulario de cuentas por pagar."}
        ElseIf sequensePayments.IsManual Then
            Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = "La secuencia numerica de CxP es manual."}
        End If

        If sequensePayments.Scope = "O" Then 'Si la secuencia es por organización
            AccountPayableSequenseDetailId = (From x In sequensePayments.PaymentsSecuenceDetail Select x.Id).FirstOrDefault()
        Else 'Si la secuencia es por unidad operativa
            If (From x In sequensePayments.PaymentsSecuenceDetail Where x.IdOperatingUnit = FixedAssetTransaction.OperatingUnitId Select x).Count = 0 Then
                Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe la unidad operativa seleccionada en la secuencia de CxP."}
            End If
            AccountPayableSequenseDetailId = (From x In sequensePayments.PaymentsSecuenceDetail Where x.IdOperatingUnit = FixedAssetTransaction.OperatingUnitId Select x.Id).FirstOrDefault()
        End If

        'Se consulta si hay parámetros de activo fijo
        Dim settingFixedAsset As SettingFixedAsset = _FixedAssetEntryRepository.GetSettingFixedAssetByOperatingUnidId(FixedAssetTransaction.OperatingUnitId)
        If settingFixedAsset Is Nothing Then
            Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe parámetros de activo fijo para la unidad operativa escogida."}
        End If
        'Se valida que existan parámetros definidos para el libro oficial en la unidad operativa actual
        If settingFixedAsset.SettingFixedAssetByLegalBook.Where(Function(d) d.LegalBook.OfficialBook = True AndAlso d.LegalBook.Status = True).Count() = 0 Then
            Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe parámetros de activo fijo en unidad operativa escogida para el libro oficial."}
        End If
        'Para la contabilización, se verifica que todos los libros válidos de viebot para el proceso actual esten parametrizados
        Dim errors As New StringBuilder
        Dim ListVieBot As List(Of VieBot) = Me._vieBotRepository.GetVieBotByForm(FixedAssetTransaction.GetType().Name, False)
        For Each itemVieBot In ListVieBot
            If settingFixedAsset.SettingFixedAssetByLegalBook.Where(Function(d) d.LegalBookId = itemVieBot.LegalBookId).Count() = 0 Then
                errors.AppendLine("El libro " + itemVieBot.CodeNameLegalBook + " no esta parametrizado para la unidad operativa escogida")
            ElseIf itemVieBot.Allow = True AndAlso itemVieBot.HandlesHomologation = True Then
                errors.AppendLine("El libro " + itemVieBot.CodeNameLegalBook + " no debe ser homologable")
            End If
        Next
        If errors.Length > 0 Then
            Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = errors.ToString()}
        End If

        'Se valida que la fecha de ingreso este en el mismo mes que la fecha de parametros
        If FixedAssetTransaction.DocumentDate.Month <> settingFixedAsset.ProcessDate.Month Then
            Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = "El mes de la fecha de ingreso(" + FixedAssetTransaction.DocumentDate.ToString("MMMM") + ") debe ser el mismo a la fecha de proceso de parámetros(" + settingFixedAsset.ProcessDate.ToString("MMMM") + ")"}
        End If
        'Se consulta el libro oficial de contabilidad
        Dim LegalBook As LegalBook = _valorizationDevaluationRepository.GetLegalBook()
        If LegalBook Is Nothing Then
            Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe libro oficial en contabilidad."}
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                ' Validación de parámetros del tenant y libros de activos fijos
                If FixedAssetTransaction.TaxRegistration > 0 Then
                    Dim SettingFixedAssetLegalBookOfficial As SettingFixedAssetByLegalBook =
                        settingFixedAsset.SettingFixedAssetByLegalBook.
                        Where(Function(d) d.LegalBook.OfficialBook = True AndAlso d.LegalBook.Status = True).
                        FirstOrDefault()
                    If SettingFixedAssetLegalBookOfficial IsNot Nothing Then
                        ' Si el parámetro IVA al costo esta activo y TaxRegistration = 2 (IVA descontable), se homologa al IVA al costo control fiscal
                        If SettingFixedAssetLegalBookOfficial.IvaCost AndAlso FixedAssetTransaction.TaxRegistration = 2 Then
                            FixedAssetTransaction.TaxRegistration = 1
                        End If
                    End If
                End If

                'Se guarda o actualiza el ingreso de activo
                Dim resultEntry As ActionResult(Of FixedAssetTransaction) = SaveValorizationDevaluation(FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook, ListDeleteFixedAssetTransactionDetail, audit, idSequense)
                If resultEntry.StateResult = False Then
                    Transaction.Dispose()
                    Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = resultEntry.MessageResult(0).ToString}
                End If

                Dim message As New StringBuilder
                message.AppendLine("El registro se guardó con código: " + resultEntry.ObjectEmbbeded.Code)

                If FixedAssetTransaction.GenerateAccountPayable = True Then 'Si se genera cxp no se realiza comprobante contable porque los métodos de cxp lo realiza

                    Dim resultGenerateCxP As ActionResult(Of AccountPayable) = GenerateAccountPayable(FixedAssetTransaction, settingFixedAsset, ListVieBot)
                    If resultGenerateCxP.StatusCode = eStatusResult.EXCEPTION OrElse resultGenerateCxP.StatusCode = eStatusResult.WARNING Then
                        Transaction.Dispose()
                        Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = resultGenerateCxP.StatusCode, .Message = resultGenerateCxP.Message}
                    End If

                    'Se agrega el objeto al listado porque el metodo construido lo recibe
                    Dim ListAccountPayable As New List(Of AccountPayable)
                    ListAccountPayable.Add(resultGenerateCxP.ObjectEmbbeded)

                    'Se guarda y confirma la cxp
                    Dim resultCxp As ActionResult(Of List(Of String)) = _accountPayableAdminService.SaveListAccountPayable(ListAccountPayable, Nothing, True, audit, AccountPayableSequenseDetailId)
                    If resultCxp.StateResult = False Then
                        Transaction.Dispose()
                        Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = resultCxp.MessageResult(0).ToString}
                    End If

                    message.AppendLine("Se generó CxP: " + resultCxp.ObjectEmbbeded(0))
                    message.AppendLine("Se generó No. Radicado: " + resultCxp.Message)
                    'Se consulta que tipo de comprobante fue que genero
                    Dim journalVoucherType As JournalVoucherTypes = _FixedAssetEntryRepository.GetJournalVoucherType(settingFixedAsset.IdValorizationDevaluationAccountingVoucher)
                    message.AppendLine("Se generó comprobante contable " + journalVoucherType.Name + ": " + resultCxp.ObjectEmbbeded(1))
                Else 'Si no genera cxp se realiza el comprobante contable de forma manual
                    If FixedAssetTransaction.FixedAssetTransactionDetail Is Nothing OrElse FixedAssetTransaction.FixedAssetTransactionDetail.Count = 0 Then 'Si no tiene detalles la transaccion
                        Transaction.Dispose()
                        Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = "La transacción no tiene detalles para poder realizar el comprobante contable"}
                    End If

                    'Se genera el comprobante contable
                    Dim resultJournalVoucher = GenerateJournalVoucher(resultEntry.ObjectEmbbeded, settingFixedAsset, LegalBook)
                    If resultJournalVoucher.StatusCode = eStatusResult.EXCEPTION OrElse resultJournalVoucher.StatusCode = eStatusResult.WARNING Then
                        Transaction.Dispose()
                        Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = resultJournalVoucher.StatusCode, .Message = resultJournalVoucher.Message}
                    End If

                    'Se obtiene el comprobante generado
                    Dim JournalVoucher = resultJournalVoucher.ObjectEmbbeded
                    'Contabilizamos en cada uno de los libros habilitados en viebot para Ingreso de Activos Fijos
                    For Each itemVieBot In ListVieBot
                        If itemVieBot.Allow Then
                            JournalVoucher.LegalBookId = itemVieBot.LegalBookId

                            'Realizamos la conversion dependiendo de la moneda parametrizada para el libro
                            If settingFixedAsset.CurrencyId <> itemVieBot.OfficialCurrencyId Then

                                Dim indigo = SessionValues.Instance
                                indigo.OfficialCurrencyId = itemVieBot.OfficialCurrencyId
                                Dim result = _currencyAdminService.GetTRMbyCurrencyId(itemVieBot.OfficialCurrencyId, settingFixedAsset.CurrencyId, indigo, resultEntry.ObjectEmbbeded.DocumentDate)

                                If result Is Nothing OrElse Not result.StateResult Then
                                    Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = $"No se logró hacer la conversión porque no se encontró TRM para la fecha {resultEntry.ObjectEmbbeded.DocumentDate}"}
                                End If

                                Dim tRMValue = result.ObjectEmbbeded.Value
                                Dim JournalVoucherTmp = JournalVoucher.Clone()
                                Parallel.ForEach(JournalVoucherTmp.JournalVoucherDetails, Sub(x)
                                                                                              x.DebitValue = Math.Round(x.DebitValue / tRMValue, 2, MidpointRounding.AwayFromZero)
                                                                                              x.CreditValue = Math.Round(x.CreditValue / tRMValue, 2, MidpointRounding.AwayFromZero)
                                                                                          End Sub)

                                Dim ObjActionResultJournal = _accountingRepository.SaveAccountingDocument(JournalVoucherTmp, audit, True)
                                If ObjActionResultJournal.StateResult Then
                                    Dim journalVoucherType As JournalVoucherTypes = _FixedAssetEntryRepository.GetJournalVoucherType(settingFixedAsset.IdValorizationDevaluationAccountingVoucher)
                                    message.AppendLine("Se generó comprobante contable " + journalVoucherType.Name + ": " + ObjActionResultJournal.ObjectEmbbeded.Consecutive.ToString() + " en el libro contable: " + itemVieBot.CodeNameLegalBook)
                                Else
                                    Transaction.Dispose()
                                    Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = ObjActionResultJournal.Message.ToString()}
                                End If

                            Else

                                Dim ObjActionResultJournal = _accountingRepository.SaveAccountingDocument(JournalVoucher, audit, True)
                                If ObjActionResultJournal.StateResult Then
                                    Dim journalVoucherType As JournalVoucherTypes = _FixedAssetEntryRepository.GetJournalVoucherType(settingFixedAsset.IdValorizationDevaluationAccountingVoucher)
                                    message.AppendLine("Se generó comprobante contable " + journalVoucherType.Name + ": " + ObjActionResultJournal.ObjectEmbbeded.Consecutive.ToString() + " en el libro contable: " + itemVieBot.CodeNameLegalBook)
                                Else
                                    Transaction.Dispose()
                                    Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.WARNING, .Message = ObjActionResultJournal.Message.ToString()}
                                End If
                            End If
                        End If
                    Next
                End If

                Transaction.Complete()
                Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = FixedAssetTransaction, .Message = message.ToString}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetTransaction) With {.StatusCode = eStatusResult.EXCEPTION, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Private Function ConvertToXmlListDeletes(ListDeleteFixedAssetTransactionDetailBook As List(Of FixedAssetTransactionDetailBook), ListDeleteFixedAssetTransactionDetail As List(Of FixedAssetTransactionDetail), Status As Integer) As List(Of String)
        Dim builder As StringBuilder
        Dim ListString As New List(Of String)

        builder = New StringBuilder()
        If ListDeleteFixedAssetTransactionDetailBook IsNot Nothing AndAlso ListDeleteFixedAssetTransactionDetailBook.Count > 0 AndAlso Status <> 3 Then
            For Each item In ListDeleteFixedAssetTransactionDetailBook
                builder.Append("<ListDeleteFixedAssetTransactionDetailBook>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("</ListDeleteFixedAssetTransactionDetailBook>")
            Next
        Else
            builder.Append("<ListDeleteFixedAssetTransactionDetailBook>")
            builder.Append("<Id>" & 0 & "</Id>")
            builder.Append("</ListDeleteFixedAssetTransactionDetailBook>")
        End If
        ListString.Add(builder.ToString)

        builder = New StringBuilder()
        If ListDeleteFixedAssetTransactionDetail IsNot Nothing AndAlso ListDeleteFixedAssetTransactionDetail.Count > 0 AndAlso Status <> 3 Then
            For Each item In ListDeleteFixedAssetTransactionDetail
                builder.Append("<ListDeleteFixedAssetTransactionDetail>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("</ListDeleteFixedAssetTransactionDetail>")
            Next
        Else
            builder.Append("<ListDeleteFixedAssetTransactionDetail>")
            builder.Append("<Id>" & 0 & "</Id>")
            builder.Append("</ListDeleteFixedAssetTransactionDetail>")
        End If
        ListString.Add(builder.ToString)


        '(0): ListDeleteFixedAssetTransactionDetailBook
        '(1): ListDeleteFixedAssetTransactionDetail

        Return ListString
    End Function

    Public Function ConvertXmlTransaction(Transaction As FixedAssetTransaction) As String

        Dim builder As StringBuilder = New StringBuilder()

        Dim contTempId As Integer = 1
        Dim contTempId2 As Integer = 1
        Dim formatXml As String = "<{0}>{1}</{0}>"

        builder.Append("<FixedAssetTransaction>")

        'Se arma la cabecera
        builder.Append("<Id>" & Transaction.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & Transaction.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & Transaction.Code & "</Code>")
        builder.Append("<DocumentDate>" & Transaction.DocumentDate.ToString("dd/MM/yyyy HH:mm") & "</DocumentDate>")
        builder.Append("<GenerateAccountPayable>" & Transaction.GenerateAccountPayable & "</GenerateAccountPayable>")
        builder.Append("<ThirdPartyId>" & Transaction.ThirdPartyId & "</ThirdPartyId>")

        If Transaction.SupplierId IsNot Nothing Then
            builder.Append("<SupplierId>" & Transaction.SupplierId & "</SupplierId>")
        Else
            builder.Append("<SupplierId>" & 0 & "</SupplierId>")
        End If

        If Transaction.SupplierDistributionLineId IsNot Nothing Then
            builder.Append("<SupplierDistributionLineId>" & Transaction.SupplierDistributionLineId & "</SupplierDistributionLineId>")
        Else
            builder.Append("<SupplierDistributionLineId>" & 0 & "</SupplierDistributionLineId>")
        End If

        If Transaction.SupplierTypeId IsNot Nothing Then
            builder.Append("<SupplierTypeId>" & Transaction.SupplierTypeId & "</SupplierTypeId>")
        Else
            builder.Append("<SupplierTypeId>" & 0 & "</SupplierTypeId>")
        End If

        builder.Append("<DayPeriod>" & Transaction.DayPeriod & "</DayPeriod>")
        builder.Append("<CreditMainAccountId>" & Transaction.CreditMainAccountId & "</CreditMainAccountId>")
        builder.Append("<CostCenterId>" & Transaction.CostCenterId & "</CostCenterId>")

        If Transaction.InvoiceNumber IsNot Nothing Then
            builder.Append("<InvoiceNumber>" & Transaction.InvoiceNumber & "</InvoiceNumber>")
        Else
            builder.Append("<InvoiceNumber>" & 0 & "</InvoiceNumber>")
        End If

        If Transaction.InvoiceDate IsNot Nothing Then
            Dim varInvoiceDate As New Date(Year(Transaction.InvoiceDate), Month(Transaction.InvoiceDate), Day(Transaction.InvoiceDate))
            builder.Append("<InvoiceDate>" & varInvoiceDate.ToString("dd/MM/yyyy HH:mm") & "</InvoiceDate>")
        Else
            Dim NewDate As New Date(9999, 12, 31)
            builder.Append("<InvoiceDate>" & NewDate.ToString("dd/MM/yyyy HH:mm") & "</InvoiceDate>")
        End If

        builder.Append(String.Format(formatXml, "Observation", CleanFields(Transaction.Observation)))
        builder.Append("<Value>" & Transaction.Value.ToString().Replace(",", ".") & "</Value>")
        builder.Append("<ValueDiscount>" & Transaction.ValueDiscount.ToString().Replace(",", ".") & "</ValueDiscount>")
        builder.Append("<ValueTax>" & Transaction.ValueTax.ToString().Replace(",", ".") & "</ValueTax>")
        builder.Append("<WithholdingTax>" & Transaction.WithholdingTax.ToString().Replace(",", ".") & "</WithholdingTax>")
        builder.Append("<WithholdingICA>" & Transaction.WithholdingICA.ToString().Replace(",", ".") & "</WithholdingICA>")
        builder.Append("<RetentionSource>" & Transaction.RetentionSource.ToString().Replace(",", ".") & "</RetentionSource>")
        builder.Append("<RetentionOther>" & Transaction.RetentionOther.ToString().Replace(",", ".") & "</RetentionOther>")
        builder.Append("<DeductionOther>" & Transaction.DeductionOther.ToString().Replace(",", ".") & "</DeductionOther>")
        builder.Append("<TotalValue>" & Transaction.TotalValue.ToString().Replace(",", ".") & "</TotalValue>")
        builder.Append("<Status>" & Transaction.Status & "</Status>")
        builder.Append("<CurrencyId>" & Transaction.CurrencyId & "</CurrencyId>")
        builder.Append("<TaxRegistration>" & Transaction.TaxRegistration & "</TaxRegistration>")
        'se arma el 1er detalle
        If Transaction.FixedAssetTransactionDetail IsNot Nothing AndAlso Transaction.FixedAssetTransactionDetail.Count > 0 Then
            For Each detail As FixedAssetTransactionDetail In Transaction.FixedAssetTransactionDetail
                builder.Append("<FixedAssetTransactionDetail>")

                builder.Append("<Id>" & detail.Id & "</Id>")
                builder.Append("<FixedAssetTransactionId>" & detail.FixedAssetTransactionId & "</FixedAssetTransactionId>")
                builder.Append("<TransactionClass>" & detail.TransactionClass & "</TransactionClass>")
                builder.Append("<PhysicalAssetId>" & detail.PhysicalAssetId & "</PhysicalAssetId>")
                builder.Append("<PhysicalAssetPartsId>" & detail.PhysicalAssetPartsId & "</PhysicalAssetPartsId>")
                builder.Append("<TransactionType>" & detail.TransactionType & "</TransactionType>")
                builder.Append("<ValorizationType>" & detail.ValorizationType & "</ValorizationType>")
                builder.Append("<AffectDepreciation>" & detail.AffectDepreciation & "</AffectDepreciation>")
                builder.Append("<Value>" & detail.Value.ToString().Replace(",", ".") & "</Value>")
                builder.Append("<LifeTime>" & detail.LifeTime & "</LifeTime>")
                builder.Append("<UnitLifeTime>" & detail.UnitLifeTime & "</UnitLifeTime>")
                builder.Append("<IVAId>" & detail.IVAId & "</IVAId>")
                builder.Append("<IvaPercentage>" & detail.IvaPercentage.ToString().Replace(",", ".") & "</IvaPercentage>")
                builder.Append("<IvaValue>" & detail.IvaValue.ToString().Replace(",", ".") & "</IvaValue>")
                builder.Append("<DiscountPercentage>" & detail.DiscountPercentage.ToString().Replace(",", ".") & "</DiscountPercentage>")
                builder.Append("<DiscountValue>" & detail.DiscountValue.ToString().Replace(",", ".") & "</DiscountValue>")
                builder.Append("<TotalValue>" & detail.TotalValue.ToString().Replace(",", ".") & "</TotalValue>")
                builder.Append("<RTFPercentage>" & detail.RTFPercentage.ToString().Replace(",", ".") & "</RTFPercentage>")
                builder.Append("<RTFValue>" & detail.RTFValue.ToString().Replace(",", ".") & "</RTFValue>")
                builder.Append("<AssetMainAccountId>" & detail.AssetMainAccountId & "</AssetMainAccountId>")
                builder.Append(String.Format(formatXml, "Detail", CleanFields(detail.Detail)))
                builder.Append("<TempId>" & contTempId & "</TempId>")

                If detail.FixedAssetTransactionDetailBook IsNot Nothing AndAlso detail.FixedAssetTransactionDetailBook.Count > 0 Then
                    For Each detailBook As FixedAssetTransactionDetailBook In detail.FixedAssetTransactionDetailBook
                        builder.Append("<FixedAssetTransactionDetailBook>")

                        builder.Append("<Id>" & detailBook.Id & "</Id>")
                        builder.Append("<FixedAssetTransactionDetailId>" & detailBook.FixedAssetTransactionDetailId & "</FixedAssetTransactionDetailId>")
                        builder.Append("<LegalBookId>" & detailBook.LegalBookId & "</LegalBookId>")
                        builder.Append("<Value>" & detailBook.Value.ToString().Replace(",", ".") & "</Value>")
                        builder.Append("<LifeTime>" & detailBook.LifeTime & "</LifeTime>")
                        builder.Append("<UnitLifeTime>" & detailBook.UnitLifeTime & "</UnitLifeTime>")
                        builder.Append("<ParentId>" & contTempId & "</ParentId>")
                        builder.Append("<TempId>" & contTempId2 & "</TempId>")

                        builder.Append("</FixedAssetTransactionDetailBook>")

                        contTempId2 += 1

                    Next

                End If

                builder.Append("</FixedAssetTransactionDetail>")

                contTempId += 1

            Next
        End If

        builder.Append("</FixedAssetTransaction>")

        Return builder.ToString()

    End Function

    ''' <summary>
    ''' Se genera la cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateAccountPayable(FixedAssetTransaction As FixedAssetTransaction, SettingFixedAsset As SettingFixedAsset, ListVieBot As List(Of VieBot)) As ActionResult(Of AccountPayable)
        Dim AccountPayable As New AccountPayable

        'La cuenta por pagar se realiza con la configuración del libro oficial
        Dim SettingFixedAssetLegalBookOfficial = SettingFixedAsset.SettingFixedAssetByLegalBook.Where(Function(d) d.LegalBook.OfficialBook = True AndAlso d.LegalBook.Status = True).FirstOrDefault()

        'Se genera la cabecera de la cxp
        With AccountPayable
            .EntityId = FixedAssetTransaction.Id
            .EntityCode = FixedAssetTransaction.Code
            .EntityName = FixedAssetTransaction.GetType().Name
            .IdSupplier = FixedAssetTransaction.SupplierId
            .IdThirdParty = _supplierRepository.GetSupplierById(FixedAssetTransaction.SupplierId, False).IdThirdParty
            .IdAccount = _supplierDistributionLineRepository.GetSuppliersDistributionLinesById(FixedAssetTransaction.SupplierDistributionLineId).DistributionLines.MainAccounts.Id
            If FixedAssetTransaction.CostCenterId > 0 Then
                .IdCostCenter = FixedAssetTransaction.CostCenterId
            Else
                .IdCostCenter = Nothing
            End If
            .BillNumber = FixedAssetTransaction.InvoiceNumber
            .BillDate = FixedAssetTransaction.InvoiceDate
            .DocumentDate = FixedAssetTransaction.DocumentDate
            .ServicePeriodDate = FixedAssetTransaction.InvoiceDate
            .FilingUnitId = SettingFixedAsset.FilingUnitId
            .Term = FixedAssetTransaction.DayPeriod
            .ExpirationDate = PaymentServices.AddDaysDate(FixedAssetTransaction.DayPeriod, FixedAssetTransaction.InvoiceDate)
            .Coments = "Cuenta por pagar generada desde Transacciones"
            .Status = 1
            .InitialBalance = False
            .PreviousBudget = False
            .Shares = 1
            .InvoiceValue = FixedAssetTransaction.Value - FixedAssetTransaction.ValueDiscount + FixedAssetTransaction.ValueTax
            .Value = FixedAssetTransaction.Value + FixedAssetTransaction.ValueTax - FixedAssetTransaction.ValueDiscount - FixedAssetTransaction.WithholdingTax - FixedAssetTransaction.WithholdingICA - FixedAssetTransaction.RetentionSource
            .Balance = .Value
            .IdOperatingUnit = FixedAssetTransaction.OperatingUnitId
            .IdSuppliersDistributionLines = FixedAssetTransaction.SupplierDistributionLineId
            .SupplierTypeId = FixedAssetTransaction.SupplierTypeId
            .JournalVoucherId = SettingFixedAsset.IdValorizationDevaluationAccountingVoucher
            .CurrencyId = FixedAssetTransaction.CurrencyId
            .TaxRegistration = FixedAssetTransaction.TaxRegistration
            'Se crean los detalles de la cxp con los articulos del ingreso de activos

            'Proveedor
            Dim supplier As Supplier = _supplierRepository.GetSupplierById(FixedAssetTransaction.SupplierId)
            'Catalogo de articulos
            Dim FixedAssetItemCatalog As FixedAssetItemCatalog = Nothing
            'Diccionario de catalogos de equipos
            Dim dictionaryItemCatalog As New Dictionary(Of Integer, FixedAssetItemCatalog)()
            'Concepto de cxp
            Dim AccountPayableConcepts As AccountPayableConcepts

            'Detalle de la cxp
            Dim AccountPayableDetailConcept As AccountPayableDetailConcept = Nothing

            Dim ListAccountPayableDetailConcept As New List(Of AccountPayableDetailConcept)

            'tarifa de IVA
            Dim GeneralLedgerIVA As GeneralLedgerIVA

            'Diccionario que guarda las tarifas de IVA
            Dim DictionaryIVA As New Dictionary(Of Integer, GeneralLedgerIVA)()

            'Lista que guarda los conceptos generados para contabilizar el IVA en los libros 
            Dim ListItemsIVA As New List(Of AccountPayableDetailConcept)

            'Diccionario que guarda los conceptos dependiendo del IVA y el Catálogo de artículos
            Dim dictionaryIVACatalog As New Dictionary(Of Tuple(Of Integer, Integer), AccountPayableDetailConcept)()

            'Se recorre los detalles del item
            FixedAssetTransaction.FixedAssetTransactionDetail.ToList.ForEach(Sub(entryItem)
                                                                                 Dim FixedAssetItem As FixedAssetItem
                                                                                 If entryItem.TransactionClass = 1 Then
                                                                                     'Activo
                                                                                     Dim ObjFixedAssetPhysicalAsset = _valorizationDevaluationRepository.GetFixedAssetPhysicalAssetById(entryItem.PhysicalAssetId)
                                                                                     FixedAssetItem = _FixedAssetEntryRepository.GetFixedAssetItemById(ObjFixedAssetPhysicalAsset.ItemId)
                                                                                 Else
                                                                                     'Parte
                                                                                     Dim ObjPartsFixedAssetPhysicalAsset = _valorizationDevaluationRepository.GetFixedAssetPhysicalAssetPartstById(entryItem.PhysicalAssetPartsId)
                                                                                     FixedAssetItem = _FixedAssetEntryRepository.GetFixedAssetItemById(ObjPartsFixedAssetPhysicalAsset.FixedAssetPhysicalAsset.ItemId)
                                                                                 End If

                                                                                 If Not dictionaryItemCatalog.ContainsKey(FixedAssetItem.ItemCatalogId) Then 'Si el catalogo no existe en el diccionario los consulto y lo agrego
                                                                                     FixedAssetItemCatalog = _FixedAssetEntryRepository.GetFixedAssetItemCatalogById(FixedAssetItem.ItemCatalogId)
                                                                                     dictionaryItemCatalog.Add(FixedAssetItem.ItemCatalogId, FixedAssetItemCatalog)
                                                                                 Else 'Si el catalogo ya existe en el diccionario lo obtengo del diccionario
                                                                                     FixedAssetItemCatalog = dictionaryItemCatalog(FixedAssetItem.ItemCatalogId)
                                                                                 End If

                                                                                 'Si el articulo no esta en el diccionario se crea un concepto nuevo
                                                                                 If entryItem.Value > 0 Then 'Si el valor del subtotal del articulo es mayor a cero

                                                                                     'Se saca los valores para asignarselos a cada item generado dependiendo si lleva o no el iva al costo
                                                                                     Dim BaseValueWithoutIva As Decimal = entryItem.Value - entryItem.DiscountValue
                                                                                     Dim BaseIvaValue As Decimal = entryItem.IvaValue

                                                                                     If Not dictionaryIVACatalog.ContainsKey(Tuple.Create(FixedAssetItem.IVAId, FixedAssetItemCatalog.Id)) Then

                                                                                         AccountPayableDetailConcept = New AccountPayableDetailConcept
                                                                                         AccountPayableDetailConcept.IdConceptAccountPayable = FixedAssetItemCatalog.IncomeAccountPayableConceptId
                                                                                         AccountPayableDetailConcept.IdAccount = FixedAssetItemCatalog.IncomeAccountId
                                                                                         AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                                                                                         AccountPayableDetailConcept.Nature = 1 'Debito
                                                                                         AccountPayableDetailConcept.BaseValue = BaseValueWithoutIva
                                                                                         AccountPayableDetailConcept.BillingValue = AccountPayableDetailConcept.BaseValue
                                                                                         AccountPayableDetailConcept.Value = AccountPayableDetailConcept.BaseValue
                                                                                         AccountPayableDetailConcept.DeferredCausation = False
                                                                                         AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Transacciones 'Valor Bruto Articulo'"
                                                                                         AccountPayableDetailConcept.TotalConcept = BaseValueWithoutIva + BaseIvaValue

                                                                                         'Se construyen líneas según TaxRegistration, validar parametrización y agrupar
                                                                                         If BaseIvaValue > 0 Then
                                                                                             If Not DictionaryIVA.ContainsKey(entryItem.IVAId) Then
                                                                                                 GeneralLedgerIVA = _generalLedgerIVARepository.GetGeneralLedgerIVAById(entryItem.IVAId)
                                                                                                 If .TaxRegistration <> 4 Then 'Se omiten validaciones de Cuentas Contables para el IVA al Costo ya que no se requiere cuentas especiales
                                                                                                     ValidateIvaAccountsForTaxReg(.TaxRegistration, GeneralLedgerIVA)
                                                                                                 End If
                                                                                                 DictionaryIVA.Add(entryItem.IVAId, GeneralLedgerIVA)
                                                                                             Else
                                                                                                 GeneralLedgerIVA = DictionaryIVA(entryItem.IVAId)
                                                                                             End If

                                                                                             AccountPayableDetailConcept.RateIva = GeneralLedgerIVA?.Id
                                                                                             AccountPayableDetailConcept.IvaValue = BaseIvaValue
                                                                                         End If

                                                                                         .AccountPayableDetailConcept.Add(AccountPayableDetailConcept)
                                                                                         ListAccountPayableDetailConcept.Add(AccountPayableDetailConcept)
                                                                                         dictionaryIVACatalog.Add(Tuple.Create(FixedAssetItem.IVAId, FixedAssetItemCatalog.Id), AccountPayableDetailConcept)

                                                                                     Else 'Si esta en el diccionario se actualizan los campos
                                                                                         AccountPayableDetailConcept = dictionaryIVACatalog(Tuple.Create(FixedAssetItem.IVAId, FixedAssetItemCatalog.Id))
                                                                                         AccountPayableDetailConcept.BaseValue += BaseValueWithoutIva
                                                                                         AccountPayableDetailConcept.BillingValue = AccountPayableDetailConcept.BaseValue
                                                                                         AccountPayableDetailConcept.Value = AccountPayableDetailConcept.BaseValue
                                                                                         AccountPayableDetailConcept.IvaValue += BaseIvaValue
                                                                                     End If

                                                                                     'Contabilizacion excepto la opción Bienes controlables del catalogo de articulos
                                                                                     If FixedAssetItemCatalog.Classification <> 3 AndAlso BaseIvaValue > 0 Then
                                                                                         'Asegura que exista la tarifa IVA cuando el registro la requiere
                                                                                         If .TaxRegistration = 1 OrElse .TaxRegistration = 2 Then
                                                                                             If Not DictionaryIVA.ContainsKey(entryItem.IVAId) Then
                                                                                                 GeneralLedgerIVA = _generalLedgerIVARepository.GetGeneralLedgerIVAById(entryItem.IVAId)
                                                                                                 ValidateIvaAccountsForTaxReg(.TaxRegistration, GeneralLedgerIVA)
                                                                                                 DictionaryIVA.Add(entryItem.IVAId, GeneralLedgerIVA)
                                                                                             Else
                                                                                                 GeneralLedgerIVA = DictionaryIVA(entryItem.IVAId)
                                                                                             End If
                                                                                         End If

                                                                                         Dim oldIdAccount = AccountPayableDetailConcept.IdAccount
                                                                                         AccountPayableDetailConcept = New AccountPayableDetailConcept

                                                                                         If .TaxRegistration = 1 Then ''IVA al costo control fiscal
                                                                                             AccountPayableDetailConcept.IdAccount = GeneralLedgerIVA.IdAccountDebitControlFiscal
                                                                                         ElseIf .TaxRegistration = 2 Then ''IVA Descontable
                                                                                             AccountPayableDetailConcept.IdAccount = GeneralLedgerIVA.IdAccountPurchaseService
                                                                                         ElseIf .TaxRegistration = 4 Then ''IVA al costo
                                                                                             AccountPayableDetailConcept.IdAccount = oldIdAccount
                                                                                         End If

                                                                                         AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                                                                                         AccountPayableDetailConcept.Nature = 1
                                                                                         AccountPayableDetailConcept.BaseValue = BaseIvaValue
                                                                                         AccountPayableDetailConcept.TotalConcept = BaseIvaValue
                                                                                         AccountPayableDetailConcept.BillingValue = AccountPayableDetailConcept.BaseValue
                                                                                         AccountPayableDetailConcept.Value = AccountPayableDetailConcept.BaseValue
                                                                                         AccountPayableDetailConcept.DeferredCausation = False
                                                                                         AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Transacciones 'IVA Articulo'"
                                                                                         ListItemsIVA.Add(AccountPayableDetailConcept)

                                                                                         If .TaxRegistration = 1 Then
                                                                                             AccountPayableDetailConcept = New AccountPayableDetailConcept
                                                                                             AccountPayableDetailConcept.IdAccount = GeneralLedgerIVA.IdAccountCreditControlFiscal
                                                                                             AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                                                                                             AccountPayableDetailConcept.Nature = 2
                                                                                             AccountPayableDetailConcept.BaseValue = BaseIvaValue
                                                                                             AccountPayableDetailConcept.TotalConcept = BaseIvaValue
                                                                                             AccountPayableDetailConcept.BillingValue = AccountPayableDetailConcept.BaseValue
                                                                                             AccountPayableDetailConcept.Value = AccountPayableDetailConcept.BaseValue
                                                                                             AccountPayableDetailConcept.DeferredCausation = False
                                                                                             AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Transacciones 'IVA Articulo'"
                                                                                             ListItemsIVA.Add(AccountPayableDetailConcept)
                                                                                         End If
                                                                                     End If
                                                                                 End If

                                                                                 '-----------------------------------------Separador-----------------------------------------------------

                                                                                 If entryItem.RTFValue > 0 Then 'Si el articulo tiene retefuente
                                                                                     'Se obtiene el concepto de retencion si esta en el listado y se suma
                                                                                     Dim AccountPayableConceptId As Integer
                                                                                     If supplier.Declarant Then 'Si el proveedor es declarante
                                                                                         AccountPayableConceptId = FixedAssetItemCatalog.DeclarantRetentionAccountPayableConceptId
                                                                                     Else 'Si no es declarante
                                                                                         AccountPayableConceptId = FixedAssetItemCatalog.NotDeclarantRetentionAccountPayableConceptId
                                                                                     End If
                                                                                     AccountPayableConcepts = _paymentsConceptRepository.GetPaymentConceptByIdSimple(AccountPayableConceptId)

                                                                                     AccountPayableDetailConcept = .AccountPayableDetailConcept.Where(Function(item) item.IdRetentionConcept IsNot Nothing AndAlso item.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId).FirstOrDefault

                                                                                     If AccountPayableDetailConcept IsNot Nothing Then
                                                                                         'Modificacion del detalle, este cambio afecta el detalle de la lista con y sin iva al costo
                                                                                         AccountPayableDetailConcept.BaseValue += entryItem.Value
                                                                                         AccountPayableDetailConcept.BillingValue += entryItem.Value
                                                                                         AccountPayableDetailConcept.Value += entryItem.RTFValue
                                                                                     Else
                                                                                         AccountPayableDetailConcept = New AccountPayableDetailConcept
                                                                                         AccountPayableDetailConcept.IdConceptAccountPayable = AccountPayableConcepts.Id
                                                                                         AccountPayableDetailConcept.IdAccount = AccountPayableConcepts.IdAccount
                                                                                         AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                                                                                         If AccountPayableConcepts.MainAccounts.HandlesCostCenter Then 'Si la cuenta del concepto maneja centro costo
                                                                                             AccountPayableDetailConcept.IdCostCenter = IIf(FixedAssetTransaction.CostCenterId > 0, FixedAssetTransaction.CostCenterId, Nothing)
                                                                                         End If
                                                                                         AccountPayableDetailConcept.Nature = 2 'Credito
                                                                                         AccountPayableDetailConcept.BaseValue = entryItem.Value
                                                                                         AccountPayableDetailConcept.BillingValue = entryItem.Value
                                                                                         AccountPayableDetailConcept.Value = entryItem.RTFValue
                                                                                         AccountPayableDetailConcept.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                                                                                         AccountPayableDetailConcept.Percentage = entryItem.RTFPercentage
                                                                                         AccountPayableDetailConcept.DeferredCausation = False
                                                                                         AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Transacciones 'RTF Articulo'"
                                                                                         .AccountPayableDetailConcept.Add(AccountPayableDetailConcept)

                                                                                         ListAccountPayableDetailConcept.Add(AccountPayableDetailConcept)
                                                                                     End If
                                                                                 End If
                                                                             End Sub)
            'Se redondean los valores, a dos decimales
            .AccountPayableDetailConcept.ToList.ForEach(Sub(item) item.Value = Utils.RoundValue(item.Value, Utils.RoundLevel.TwoDecimal))
            ListAccountPayableDetailConcept.ToList.ForEach(Sub(item) item.Value = Utils.RoundValue(item.Value, Utils.RoundLevel.TwoDecimal))

            If FixedAssetTransaction.WithholdingTax > 0 Then 'Si tiene retencion del iva
                AccountPayableDetailConcept = New AccountPayableDetailConcept
                If SettingFixedAsset.IVARetention = 1 Then 'Se saca la retencion del iva del tercero
                    AccountPayableConcepts = _FixedAssetEntryRepository.GetAccountPayableConceptBySupplierDistributionLineId(FixedAssetTransaction.SupplierDistributionLineId)
                    AccountPayableDetailConcept.IdConceptAccountPayable = AccountPayableConcepts.Id
                    AccountPayableDetailConcept.IdAccount = AccountPayableConcepts.IdAccount
                    AccountPayableDetailConcept.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                    AccountPayableDetailConcept.Percentage = AccountPayableConcepts.RetentionConcepts.Rate
                    If AccountPayableConcepts.MainAccounts.HandlesCostCenter Then 'Si la cuenta del concepto maneja centro costo
                        AccountPayableDetailConcept.IdCostCenter = IIf(FixedAssetTransaction.CostCenterId > 0, FixedAssetTransaction.CostCenterId, Nothing)
                    End If
                Else 'Se saca la retencion del iva del concepto
                    AccountPayableConcepts = _paymentsConceptRepository.GetPaymentConceptByIdSimple(SettingFixedAsset.IVARetentionAccountPayableConceptId)
                    AccountPayableDetailConcept.IdConceptAccountPayable = SettingFixedAsset.IVARetentionAccountPayableConceptId
                    AccountPayableDetailConcept.IdAccount = AccountPayableConcepts.IdAccount
                    AccountPayableDetailConcept.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                    AccountPayableDetailConcept.Percentage = AccountPayableConcepts.RetentionConcepts.Rate
                    If AccountPayableConcepts.MainAccounts.HandlesCostCenter Then 'Si la cuenta del concepto maneja centro costo
                        AccountPayableDetailConcept.IdCostCenter = IIf(FixedAssetTransaction.CostCenterId > 0, FixedAssetTransaction.CostCenterId, Nothing)
                    End If
                End If
                AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                AccountPayableDetailConcept.Nature = 2 'Credito
                AccountPayableDetailConcept.BaseValue = FixedAssetTransaction.ValueTax
                AccountPayableDetailConcept.BillingValue = FixedAssetTransaction.Value
                AccountPayableDetailConcept.Value = FixedAssetTransaction.WithholdingTax
                AccountPayableDetailConcept.DeferredCausation = False
                AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Transacciones 'Retención IVA'"
                .AccountPayableDetailConcept.Add(AccountPayableDetailConcept)

                ListAccountPayableDetailConcept.Add(AccountPayableDetailConcept)
            End If

            If FixedAssetTransaction.WithholdingICA > 0 Then 'Si tiene retencion del ica
                AccountPayableDetailConcept = New AccountPayableDetailConcept
                AccountPayableConcepts = _paymentsConceptRepository.GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitIdPay(FixedAssetTransaction.SupplierDistributionLineId, FixedAssetTransaction.OperatingUnitId)
                AccountPayableDetailConcept.IdConceptAccountPayable = AccountPayableConcepts.Id
                AccountPayableDetailConcept.IdAccount = AccountPayableConcepts.IdAccount
                AccountPayableDetailConcept.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                If AccountPayableConcepts.MainAccounts.HandlesCostCenter Then 'Si la cuenta del concepto maneja centro costo
                    AccountPayableDetailConcept.IdCostCenter = IIf(FixedAssetTransaction.CostCenterId > 0, FixedAssetTransaction.CostCenterId, Nothing)
                End If
                AccountPayableDetailConcept.Nature = 2 'Credito
                AccountPayableDetailConcept.BaseValue = FixedAssetTransaction.Value
                AccountPayableDetailConcept.BillingValue = FixedAssetTransaction.Value
                AccountPayableDetailConcept.Value = FixedAssetTransaction.WithholdingICA
                AccountPayableDetailConcept.Percentage = AccountPayableConcepts.RetentionConcepts.Rate
                AccountPayableDetailConcept.DeferredCausation = False
                AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Transacciones 'Retención ICA'"
                .AccountPayableDetailConcept.Add(AccountPayableDetailConcept)

                ListAccountPayableDetailConcept.Add(AccountPayableDetailConcept)
            End If

            For Each itemVieBot In ListVieBot
                If itemVieBot.LegalBookId <> SettingFixedAssetLegalBookOfficial.LegalBookId Then
                    If itemVieBot.Allow = True Then

                        If Not .AccountPayableDetailConceptOthersNotHomologatedBooks.ContainsKey(itemVieBot.LegalBookId) Then
                            .AccountPayableDetailConceptOthersNotHomologatedBooks.Add(itemVieBot.LegalBookId, New List(Of AccountPayableDetailConcept))
                        End If

                        Dim AccountPayableDetailConceptOthersNotHomologatedBooks = .AccountPayableDetailConceptOthersNotHomologatedBooks(itemVieBot.LegalBookId)
                        Dim SettingFixedAssetByLegalBook = SettingFixedAsset.SettingFixedAssetByLegalBook.Where(Function(d) d.LegalBookId = itemVieBot.LegalBookId).FirstOrDefault()

                        Dim ListHomologationAccountPayableDetailConcept As List(Of AccountPayableDetailConcept) = ListAccountPayableDetailConcept.Select(Function(item) item.Clone()).ToList()
                        For Each item In ListHomologationAccountPayableDetailConcept
                            If item.DictionaryConfigurationBooks IsNot Nothing AndAlso item.DictionaryConfigurationBooks.Count > 0 Then
                                If Not item.DictionaryConfigurationBooks.ContainsKey(itemVieBot.LegalBookId) Then
                                    Continue For
                                End If
                                Dim MainAccountId As Integer = item.DictionaryConfigurationBooks(itemVieBot.LegalBookId)
                                item.IdAccount = MainAccountId
                            End If
                            If .TaxRegistration = 1 Then ''IVA al costo control fiscal
                                item.Value = item.Value + If(item.IvaValue, 0)
                            End If
                            AccountPayableDetailConceptOthersNotHomologatedBooks.Add(item)
                        Next

                        For Each itemIVA In ListItemsIVA
                            AccountPayableDetailConceptOthersNotHomologatedBooks.Add(itemIVA)
                        Next

                    End If
                End If
            Next

            'Se crea la cuota de a cxp
            Dim AccountPayableShares As New AccountPayableShares
            AccountPayableShares.Share = 1
            AccountPayableShares.DateExpires = .ExpirationDate
            AccountPayableShares.InitialValue = .Value
            AccountPayableShares.Balance = .Value
            .AccountPayableShares.Add(AccountPayableShares)
        End With

        Return New ActionResult(Of AccountPayable) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = AccountPayable}
    End Function

    Private Function CleanFields(field As Object) As String
        If IsNumeric(field) Then
            Return field.ToString().Replace(",", ".")
        End If
        If IsDate(field) Then
            Return CDate(field).ToString("dd/MM/yyyy hh:mm:ss")
        End If
        Return field.ToString().CleanSpecialChars("(),.-%/º")
    End Function

#Region "METHODS"

    ' Validación de parámetros según el TaxRegistration
    Private Sub ValidateIvaAccountsForTaxReg(taxRegistration As Integer, glIva As GeneralLedgerIVA)
        Select Case taxRegistration
            Case 1 ' Control fiscal
                If glIva Is Nothing OrElse Not glIva.IdAccountDebitControlFiscal.HasValue OrElse Not glIva.IdAccountCreditControlFiscal.HasValue Then
                    Throw New IndigoValidationException("La tarifa de IVA no tiene parametrizadas las cuentas de control fiscal (débito/crédito).")
                End If
            Case 2 ' Descontable
                If glIva Is Nothing OrElse Not glIva.IdAccountPurchaseService.HasValue Then
                    Throw New IndigoValidationException("La tarifa de IVA no tiene parametrizada la cuenta de IVA compras/servicios.")
                End If
            Case Else
                Throw New IndigoValidationException($"Registro IVA ({taxRegistration}) no soportado.")
        End Select
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _fixedAssetServices.Dispose()
                _accountPayableAdminService.Dispose()
            End If
            _valorizationDevaluationRepository = Nothing
            _secuenseDRepository = Nothing
            _fixedAssetServices = Nothing
            _FixedAssetEntryRepository = Nothing
            _sequensePaymentsCRepository = Nothing
            _supplierRepository = Nothing
            _supplierDistributionLineRepository = Nothing
            _paymentsConceptRepository = Nothing
            _accountPayableAdminService = Nothing
            _accountingRepository = Nothing
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
