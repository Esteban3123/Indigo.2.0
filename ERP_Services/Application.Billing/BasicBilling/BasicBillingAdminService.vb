#Region "Imports"

Imports System.Configuration
Imports System.Data.Entity.Core
Imports System.Text
Imports System.Threading.Tasks
Imports System.Transactions
Imports Application.EventHandlers
Imports Application.EventHandlers.Enums.Enums
Imports Application.EventHandlers.Model
Imports Application.EventHandlers.Security
Imports Application.EventHandlers.Security.Entities
Imports Application.Inventory.InventoryAdjustment
Imports Application.Inventory.Sequense
Imports Application.Portfolio
Imports Application.Treasury
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class BasicBillingAdminService
    Implements IBasicBillingAdminService

#Region "Properties"

    'Repositorios
    Private _basicBillingRepository As IBasicBillingRepository
    Private _billingAuthorizationRepository As IBillingAuthorizationRepository
    Private _billingNoteRepository As IBillingNoteRepository
    Private _billingReversalReasonRepository As IBillingReversalReasonRepository
    Private _budgetRepository As IBudgetRepository
    Private _budgetSequenceRepository As IBudgetSequenceRepository
    Private _cashReceiptConceptRepository As ICashReceiptConceptRepository
    Private _categoryRepository As IBudgetItemRepository
    Private _electronicDocumentRepository As IElectronicDocumentRepository
    Private _invoiceRepository As IInvoiceRepository
    Private _operatingUnitRepository As IOperatingUnitRepository
    Private _recognitionRepository As IRecognitionRepository
    Private _sequenseRepository As IBillingSequenceRepository
    Private _sequenseTreasuryRepository As ISequenseTreasuryCRepository
    Private _sequensePortfolioRepository As ISequensePortfolioCRepository
    Private _settingsAccountRepository As ISettingsAccountRepository
    Private _thirdpartyRepository As IThirdPartyRepository
    Private _CurrencyRepository As ICurrencyRepository
    Private _settingsBillingRepository As ISettingsBillingRepository
    Private _accountReceivableRepository As IAccountReceivableRepository
    Private _functionalUnitRepository As IFunctionalUnitRepository
    Private _costCenterRepository As ICostCenterRepository
    Private _portfolioNoteConceptRepository As IPortfolioNoteConceptRepository
    Private _customerRepository As ICustomerRepository
    Private _billingConceptRepository As IBillingConceptRepository
    Private _mainAccountsRepository As ICostDistributionsRepository
    Private _inventoryProductRepository As IInventoryProductRepository
    Private _warehouseRepository As IWarehouseRepository
    Private _supplierRepository As ISupplierRepository
    Private _physicalInventoryRepository As IPhysicalInventoryRepository
    Private _fixedAssetEntryRepository As IFixedAssetEntryRepository
    Private _generalLedgerIVARepository As IGeneralLedgerIVARepository
    Private _careGroupRepository As ICareGroupRepository
    Private _invoiceCopayRepository As IInvoiceCopayRepository
    Private _portfolioTransferRepository As IPortfolioTransferRepository

    'Events
    Private ReadOnly _eventProxy As IEventProxy

    'AdminServices
    Private _cashReceiptsAdminService As ICashReceiptsAdminService
    Private _treasuryNoteAdminService As ITreasuryNoteAdminService
    Private _portfolioNoteAdminService As IPortfolioNoteAdminService
    Private _inventorySequenceAdminService As IInventorySequenceAdminService
    Private _inventoryAdjustmentAdminService As IInventoryAdjustmentAdminService

    Private Num_Rounding As Integer

    Private Num_Decimal As Integer
#End Region

#Region "Builder"

    Public Sub New(basicBillingRepository As IBasicBillingRepository, billingAuthorizationRepository As IBillingAuthorizationRepository, billingNoteRepository As IBillingNoteRepository, billingReversalReasonRepository As IBillingReversalReasonRepository,
                   budgetRepository As IBudgetRepository, budgetSequenceRepository As IBudgetSequenceRepository, cashReceiptConceptRepository As ICashReceiptConceptRepository, categoryRepository As IBudgetItemRepository,
                   electronicDocumentRepository As IElectronicDocumentRepository, invoiceRepository As IInvoiceRepository, operatingUnitRepository As IOperatingUnitRepository, recognitionRepository As IRecognitionRepository, sequenseRepository As IBillingSequenceRepository,
                   sequenseTreasuryRepository As ISequenseTreasuryCRepository, sequensePortfolioRepository As ISequensePortfolioCRepository, settingsAccountRepository As ISettingsAccountRepository, thirdpartyRepository As IThirdPartyRepository, cashReceiptsAdminService As ICashReceiptsAdminService,
                   portfolioNoteAdminService As IPortfolioNoteAdminService, CurrencyRepository As ICurrencyRepository, settingsBillingRepository As ISettingsBillingRepository,
                   accountReceivableRepository As IAccountReceivableRepository, functionalUnitRepository As IFunctionalUnitRepository, costCenterRepository As ICostCenterRepository, portfolioNoteConceptRepository As IPortfolioNoteConceptRepository,
                   customerRepository As ICustomerRepository, billingConceptRepository As IBillingConceptRepository, mainAccountsRepository As ICostDistributionsRepository, treasuryNoteAdminService As ITreasuryNoteAdminService, inventoryProductRepository As IInventoryProductRepository,
                   warehouseRepository As IWarehouseRepository, supplierRepository As ISupplierRepository, inventorySequenceAdminService As IInventorySequenceAdminService, physicalInventoryRepository As IPhysicalInventoryRepository, inventoryAdjustmentAdminService As IInventoryAdjustmentAdminService,
                   fixedAssetEntryRepository As IFixedAssetEntryRepository, eventProxy As IEventProxy, generalLedgerIVARepository As IGeneralLedgerIVARepository, careGroupRepository As ICareGroupRepository, invoiceCopayRepository As IInvoiceCopayRepository,
                   portfolioTransferRepository As IPortfolioTransferRepository)

        If basicBillingRepository Is Nothing Then
            Throw New ArgumentNullException("basicBillingRepository")
        End If

        _careGroupRepository = careGroupRepository
        _invoiceCopayRepository = invoiceCopayRepository
        Me._basicBillingRepository = basicBillingRepository
        Me._billingAuthorizationRepository = billingAuthorizationRepository
        Me._billingNoteRepository = billingNoteRepository
        Me._billingReversalReasonRepository = billingReversalReasonRepository
        Me._budgetRepository = budgetRepository
        Me._budgetSequenceRepository = budgetSequenceRepository
        Me._cashReceiptConceptRepository = cashReceiptConceptRepository
        Me._categoryRepository = categoryRepository
        Me._electronicDocumentRepository = electronicDocumentRepository
        Me._invoiceRepository = invoiceRepository
        Me._operatingUnitRepository = operatingUnitRepository
        Me._recognitionRepository = recognitionRepository
        Me._sequenseRepository = sequenseRepository
        Me._sequenseTreasuryRepository = sequenseTreasuryRepository
        Me._sequensePortfolioRepository = sequensePortfolioRepository
        Me._settingsAccountRepository = settingsAccountRepository
        Me._thirdpartyRepository = thirdpartyRepository
        Me._portfolioNoteAdminService = portfolioNoteAdminService
        Me._cashReceiptsAdminService = cashReceiptsAdminService
        Me._CurrencyRepository = CurrencyRepository
        Me._settingsBillingRepository = settingsBillingRepository
        Me._accountReceivableRepository = accountReceivableRepository
        Me._functionalUnitRepository = functionalUnitRepository
        Me._costCenterRepository = costCenterRepository
        Me._portfolioNoteConceptRepository = portfolioNoteConceptRepository
        Me._customerRepository = customerRepository
        Me._billingConceptRepository = billingConceptRepository
        Me._mainAccountsRepository = mainAccountsRepository
        Me._treasuryNoteAdminService = treasuryNoteAdminService
        Me._inventoryProductRepository = inventoryProductRepository
        Me._warehouseRepository = warehouseRepository
        Me._supplierRepository = supplierRepository
        Me._inventorySequenceAdminService = inventorySequenceAdminService
        Me._physicalInventoryRepository = physicalInventoryRepository
        Me._inventoryAdjustmentAdminService = inventoryAdjustmentAdminService
        Me._fixedAssetEntryRepository = fixedAssetEntryRepository
        Me._generalLedgerIVARepository = generalLedgerIVARepository
        _portfolioTransferRepository = portfolioTransferRepository
        _eventProxy = eventProxy
    End Sub

#End Region

#Region "Methods"

    Public Function GetDocumentInvoiceProductSalesById(id As Integer) As BasicBilling Implements IBasicBillingAdminService.GetBasicBillingById
        Try
            Return Me._basicBillingRepository.GetBasicBillingById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New BasicBilling()
        End Try
    End Function

    Public Function GetBasicBillingByCode(code As String, audit As AuditMessage) As BasicBilling Implements IBasicBillingAdminService.GetBasicBillingByCode
        Try
            Dim basicBilling = Me._basicBillingRepository.GetBasicBillingByCode(code)
            Dim currency As Domain.Entities.Currency = basicBilling?.Currency

            If basicBilling IsNot Nothing AndAlso basicBilling.Id > 0 AndAlso currency IsNot Nothing Then
                Select Case currency.RoundingType
                    Case 1
                        Num_Decimal = 2
                    Case 2
                        Num_Decimal = 1
                    Case 3
                        Num_Decimal = 0
                    Case 4
                        Num_Decimal = -1
                    Case 5
                        Num_Decimal = -2
                    Case 6
                        Num_Decimal = -3
                End Select
                Me.Num_Rounding = 10 ^ Num_Decimal
                basicBilling.TotalValue = Fix(basicBilling.TotalValue * Num_Rounding + 0.5) / Num_Rounding
                basicBilling.Value = Fix(basicBilling.Value * Num_Rounding + 0.5) / Num_Rounding
                basicBilling.ValueIVA = Fix(basicBilling.ValueIVA * Num_Rounding + 0.5) / Num_Rounding
                basicBilling.WithholdingTax = Fix(basicBilling.WithholdingTax * Num_Rounding + 0.5) / Num_Rounding
                basicBilling.WithholdingICA = Fix(basicBilling.WithholdingICA * Num_Rounding + 0.5) / Num_Rounding

                For Each biling In basicBilling.BasicBillingDetail
                    biling.Price = Fix(biling.Price * Num_Rounding + 0.5) / Num_Rounding
                    biling.Value = Fix(biling.Value * Num_Rounding + 0.5) / Num_Rounding
                    biling.WithholdingICA = Fix(biling.WithholdingICA * Num_Rounding + 0.5) / Num_Rounding
                    biling.WithholdingTax = Fix(biling.WithholdingTax * Num_Rounding + 0.5) / Num_Rounding
                Next
            End If

            Return basicBilling
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New BasicBilling()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los recibos de caja asociados a un Id de factura y los retorna en una Lista
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCashReceiptsByBasicBillingInvoice(id As Integer) As List(Of CashReceipts) Implements IBasicBillingAdminService.GetCashReceiptsByBasicBillingInvoice
        Try
            Dim CashReceipts As List(Of CashReceipts) = Me._basicBillingRepository.GetCashReceiptsByBasicBillingInvoice(id)
            Return CashReceipts
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of CashReceipts)
        End Try
    End Function

    Public Function SetBasicBillingDetailFromFile(addressId As Integer, wareHouseId As Integer, data As List(Of List(Of String))) As ActionResult(Of List(Of BasicBillingDetail), List(Of Tuple(Of String, Integer))) Implements IBasicBillingAdminService.SetBasicBillingDetailFromFile
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If

        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListBasicBillingDetail As New List(Of BasicBillingDetail)
        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))

        Try
            'Objeto xml
            Dim xmlObject = ConvertToXml(data)
            'Se consume el procedimiento almacenado
            Dim resultStore = Me._basicBillingRepository.SetBasicBillingDetailFromFile(addressId, wareHouseId, xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                'Se recorre el listado 
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then
                        'Se crea la entidad para agregar al listado
                        Dim basicBillingDetail = New BasicBillingDetail

                        'Si es un producto que maneje lote y ya existe un registro ya creado
                        If itemXml.DetailType = 1 AndAlso itemXml.PhysicalInventoryId > 0 AndAlso ListBasicBillingDetail.Any(Function(d) d.DetailType = itemXml.DetailType AndAlso d.ProductId = itemXml.ItemId) Then
                            basicBillingDetail = ListBasicBillingDetail.Where(Function(d) d.DetailType = 1 AndAlso d.ProductId = itemXml.ItemId).FirstOrDefault()
                            basicBillingDetail.Quantity = basicBillingDetail.Quantity + itemXml.Quantity
                        Else
                            With basicBillingDetail
                                .DetailType = itemXml.DetailType
                                If .DetailType = 1 Then
                                    .ProductId = itemXml.ItemId
                                ElseIf .DetailType = 2 Then
                                    .BillingConceptId = itemXml.ItemId
                                ElseIf .DetailType = 3 Then
                                    .PhysicalAssetId = itemXml.ItemId
                                ElseIf .DetailType = 4 Then
                                    .PhysicalAssetPartId = itemXml.ItemId
                                End If
                                .CodeName = itemXml.ItemDescription
                                .Quantity = itemXml.Quantity
                                .Price = itemXml.Price
                            End With

                            'Agregamos el detalle
                            ListBasicBillingDetail.Add(basicBillingDetail)
                        End If

                        With basicBillingDetail
                            .Value = .Quantity * .Price
                            .PercentageDiscount = itemXml.PercentageDiscount
                            .PercentageIVA = itemXml.PercentageIVA
                            .RetentionIdTax = itemXml.RetentionIdTax
                            .RetentionPercentageTax = itemXml.RetentionPercentageTax
                            .RetentionBaseTax = itemXml.RetentionBaseTax
                            .RetentionIdICA = itemXml.RetentionIdICA
                            .RetentionPercentageICA = itemXml.RetentionPercentageICA
                            .RetentionBaseICA = itemXml.RetentionBaseICA
                        End With

                        If itemXml.DetailType = 1 AndAlso itemXml.PhysicalInventoryId > 0 Then
                            basicBillingDetail.BasicBillingDetailItem.Add(New BasicBillingDetailItem With
                                {
                                    .PhysicalInventoryId = itemXml.PhysicalInventoryId,
                                    .Quantity = itemXml.Quantity,
                                    .BatchCode = itemXml.ItemBatchSerial
                                }
                            )
                        End If
                    Else
                        listErrors.Add(New Tuple(Of String, Integer)(itemXml.MessageField, 2))
                    End If
                Next
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of BasicBillingDetail), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListBasicBillingDetail, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of BasicBillingDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of BasicBillingDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of BasicBillingDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Private Function ConvertToXml(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        'Cantidad de item para validar 
        Dim count As Integer = 0

        For Each item In data
            'Se asigna la cantidad de items que contiene la fila
            count = item.Count

            builder.Append("<Row>")

            'Tipo de Detalle
            If count > 0 Then
                builder.Append("<DetailType>" & item(0) & "</DetailType>")
                count -= 1
            Else
                builder.Append("<DetailType></DetailType>")
            End If

            'Codigo
            builder.Append("<ItemId>" & 0 & "</ItemId>")
            If count > 0 Then
                builder.Append("<ItemCode>" & item(1) & "</ItemCode>")
                count -= 1
            Else
                builder.Append("<ItemCode></ItemCode>")
            End If
            builder.Append("<ItemDescription>" & "---" & "</ItemDescription>")

            'Lote
            builder.Append("<PhysicalInventoryId></PhysicalInventoryId>")
            If count > 0 Then
                builder.Append("<ItemBatchSerial>" & item(2) & "</ItemBatchSerial>")
                count -= 1
            Else
                builder.Append("<ItemBatchSerial></ItemBatchSerial>")
            End If

            'Cantidad
            If count > 0 Then
                builder.Append("<Quantity>" & item(3) & "</Quantity>")
                count -= 1
            Else
                builder.Append("<Quantity></Quantity>")
            End If

            'Precio Unitario
            If count > 0 Then
                builder.Append("<Price>" & item(4) & "</Price>")
                count -= 1
            Else
                builder.Append("<Price></Price>")
            End If

            '% de Descuento
            If count > 0 Then
                builder.Append("<PercentageDiscount>" & item(5) & "</PercentageDiscount>")
                count -= 1
            Else
                builder.Append("<PercentageDiscount></PercentageDiscount>")
            End If

            builder.Append("<PercentageIVA>0</PercentageIVA>")
            builder.Append("<RetentionIdTax>0</RetentionIdTax>")
            builder.Append("<RetentionPercentageTax>0</RetentionPercentageTax>")
            builder.Append("<RetentionBaseTax>0</RetentionBaseTax>")
            builder.Append("<RetentionIdICA>0</RetentionIdICA>")
            builder.Append("<RetentionPercentageICA>0</RetentionPercentageICA>")
            builder.Append("<RetentionBaseICA>0</RetentionBaseICA>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "" & "</MessageField>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Public Async Function SaveBasicBillingAsync(basicBilling As BasicBilling, audit As AuditMessage) As Task(Of ActionResult(Of BasicBilling)) Implements IBasicBillingAdminService.SaveBasicBillingAsync
        If basicBilling Is Nothing Then
            Throw New ArgumentNullException("basicBilling")
        End If

        Dim unitOfWork As IUnitWork = Me._basicBillingRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled)
            Try
                If basicBilling.Status <> 3 Then
                    basicBilling.DocumentDate = DateTime.Now
                End If

                Dim EntityXml As String = ConvertToXmlBasicBilling(basicBilling)

                Dim resultStore = Await Me._basicBillingRepository.SP_SaveBasicBilling(EntityXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of BasicBilling) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList, .Message = resultStore.Message}
                End If

                basicBilling.Id = resultStore.Id
                basicBilling.Code = resultStore.Code
                basicBilling.MarkAsUnchanged()
                transaction.Complete()

                Return New ActionResult(Of BasicBilling) With {.StateResult = True, .ObjectEmbbeded = basicBilling}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of BasicBilling) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of BasicBilling) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' funcion que obtiene los datos de la factura basica
    ''' </summary>
    ''' <param name="basicBillinId"></param>
    ''' <returns></returns>
    Private Function ToInvoiceEvent(basicBillinId As Integer) As ActionResult(Of List(Of InvoiceEvent))
        Try
            Dim data = _invoiceRepository.ExecuteQueryDR(Of VReportBasicBilling)($"SELECT * FROM Billing.VReportBasicBilling (NOLOCK) WHERE BasicBillingId In ({String.Join(",", basicBillinId)})", {})

            Dim result = data.Select(Function(m) New InvoiceEvent With {
                .InvoiceNumber = m.InvoiceNumber,
                .InvoiceDate = m.InvoiceDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                .Observation = m.Observation,
                .DocumentType = m.DocumentType,
                .ThirdPartyDiscountValue = m.ThirdPartyDiscountValue,
                .ThirdPartySalesValue = m.ThirdPartySalesValue,
                .Status = m.Status,
                .DocumentTypename = "Factura Basica",
                .User = New UserEvent With {.Code = m.UserCode, .Name = m.FullNameUser},
                .Client = If(String.IsNullOrEmpty(m.Identification), Nothing, New ClientEvent With {
                    .IdentificationType = 1,
                    .Identification = m.Identification,
                    .DigitVerification = m.DigitVerification,
                    .FullName = m.ThirdPartyFullName,
                    .Address = m.ThirdPartyAddress,
                    .Phone = m.ThirdPartyPhone
                }),
                .ElectronicDocument = New ElectronicDocumentEvent With {
                    .PaymentMethod = m.PaymentMethod,
                    .CUFE = m.CUFE,
                    .QR = m.QR,
                    .ValidationDate = m.ValidationDate?.ToString("yyyy-MM-ddTHH:mm:ss")
                }
            }).ToList()

            Return New ActionResult(Of List(Of InvoiceEvent)) With {.StateResult = True, .ObjectEmbbeded = result, .Message = "Creado Correctamente"}

        Catch ex As Exception
            Return New ActionResult(Of List(Of InvoiceEvent)) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Async Function ConfirmBasicBillingAsync(basicBilling As BasicBilling, cashReceipts As CashReceipts, session As SessionValues) As Threading.Tasks.Task(Of ActionResult(Of BasicBilling)) Implements IBasicBillingAdminService.ConfirmBasicBillingAsync
        Dim unitOfWork As IUnitWork = Me._basicBillingRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled)
            Try
                'Consulto los parametros de Contabilidad definidos para la unidad operativa
                Dim settingsAccount = Me._settingsAccountRepository.GetSettingAccountSimple(basicBilling.OperatingUnitId)
                If settingsAccount Is Nothing OrElse settingsAccount.Id = 0 Then
                    Return New ActionResult(Of BasicBilling)(False, Nothing, "No se encontro parametros de contabilidad para la unidad operativa seleccionada", Nothing)
                End If

                'Valido que, si la facturacion electronica se encuentra habilitada, y la autorizacion es de tipo electronica, esta tenga asignado un codigo
                Dim billingAuthorization = _billingAuthorizationRepository.GetBillingAuthorizationById(basicBilling.BillingAuthorizationId)
                If (billingAuthorization Is Nothing OrElse billingAuthorization.Id = 0) OrElse (settingsAccount.HandlesElectronicBilling = True AndAlso billingAuthorization.InvoiceType = 3 AndAlso String.IsNullOrEmpty(billingAuthorization.TechnicalKey)) Then
                    Dim billingAuthorizationName = String.Empty
                    If billingAuthorization IsNot Nothing Then
                        billingAuthorizationName = billingAuthorization.Name
                    End If
                    Return New ActionResult(Of BasicBilling)(False, Nothing, "No se ha parametrizado la clave tecnica en la autorización de facturación " + billingAuthorizationName, Nothing)
                End If

                Dim cashReceiptsXml As String = Nothing
                If cashReceipts IsNot Nothing Then
                    cashReceiptsXml = cashReceipts.ToXML()
                End If

                Dim resultStore = Await Me._basicBillingRepository.SP_ConfirmBasicBilling(basicBilling.Id, session.AuditMessageWcf.CodeUser, cashReceiptsXml, session.AuditMessageWcf.CompanyType)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = resultStore.Message}
                End If

                'Se genera el cruce de anticpos vs cxc, si se hace desde el frontal de cruces desde basica
                If basicBilling?.PortfolioAdvanceInvoicePayment?.ListPortfolioAdvance?.Any() Then
                    Dim listPortfolioAdvanceCrossingXml = basicBilling.PortfolioAdvanceInvoicePayment.ToXML()

                    Dim res = _billingAuthorizationRepository.ExecuteStoredProcedure(Of GeneratePortfolioTransferResponse)("[Portfolio].[SP_GeneratePortfolioTransferAsList]", {
                        ("@ListPortfolioAdvanceCrossingXml", listPortfolioAdvanceCrossingXml),
                        ("@OperativeUnitId", basicBilling.OperatingUnitId),
                        ("@UserCode", session.AuditMessageWcf.CodeUser),
                        ("@AccountReceivableId", resultStore.AccountReceivableId),
                        ("@CompanyType", session.AuditMessageWcf.CompanyType)
                        })

                    If res Is Nothing Then
                        Throw New IndigoValidationException("No se llevó a cabo el cruce de anticipo")
                    End If

                    If res(0).Code <> "0" Then
                        unitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = res(0).Message}
                    End If
                    resultStore.Message &= vbCrLf & res(0).Message
                End If

                If basicBilling.BasicBillingGifts.Any Then
                    basicBilling.InvoiceId = resultStore.InvoiceId
                    Dim resultAdjustment = Await Me.GenerateInventoryAdjusments(basicBilling, session.AuditMessageWcf)
                    If resultAdjustment.StateResult = False Then
                        unitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = resultAdjustment.Message}
                    End If

                    Dim sb As New StringBuilder()
                    sb.AppendLine(resultStore.Message)
                    sb.AppendLine(resultAdjustment.Message)
                    resultStore.Message = sb.ToString
                End If

                Dim resultElectronicDocument = Me.SaveElectronicDocument(settingsAccount, billingAuthorization, resultStore.InvoiceId, session)
                If resultElectronicDocument.StateResult = False Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = resultElectronicDocument.Message}
                End If

                'ejecucion de la facturacion electronica para Costa rica
                Dim data As Object
                Dim source As String

                Dim jsonSend = ToInvoiceEvent(basicBilling.Id)

                'si la creacion del jsom sale bien hace el proceso de publicacion normal
                If jsonSend.StateResult Then
                    data = jsonSend.ObjectEmbbeded
                    source = EventType.Invoice.ToString()
                Else
                    'si se se genera alguna Exception dentro de la creacion del jsom y enviamos un jsom de error para que quede registrado en la DeadLetterMessageAsync
                    'enviar json de error 
                    Dim errorHandler As New ErrorHandler()

                    errorHandler.InvoiceNumber = resultStore.InvoiceNumber
                    errorHandler.DateError = DateTime.Now()
                    errorHandler.Error = jsonSend.Message
                    errorHandler.CodeError = "99"

                    data = errorHandler
                    source = EventType.ErrorHandler.ToString()
                End If

                _eventProxy.Publish(New EventData(data, source, EventAction.added.ToString(), session.AuditMessageWcf.Company, session.AuditMessageWcf.CodeUser, DateTime.Now().GetTimestamp))

                transaction.Complete()
                Return New ActionResult(Of BasicBilling) With {.StateResult = True, .ObjectEmbbeded = basicBilling, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of BasicBilling) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Async Function SaveAndConfirmBasicBillingAsync(basicBilling As BasicBilling, cashReceipts As CashReceipts, session As SessionValues) As Threading.Tasks.Task(Of ActionResult(Of BasicBilling)) Implements IBasicBillingAdminService.SaveAndConfirmBasicBillingAsync
        basicBilling.Status = 1
        Dim result = Await Me.SaveBasicBillingAsync(basicBilling, session.AuditMessageWcf)
        If result.StateResult = True Then
            Dim resultConfirm = Await Me.ConfirmBasicBillingAsync(result.ObjectEmbbeded, cashReceipts, session)
            If resultConfirm.StateResult = True Then
                If basicBilling.ChangeTracker.State = ObjectState.Added Then
                    Return New ActionResult(Of BasicBilling) With {.StateResult = True, .StateResultAux = True, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = "Se guardo y se confirmo correctamente" + Environment.NewLine + resultConfirm.Message}
                Else
                    Return New ActionResult(Of BasicBilling) With {.StateResult = True, .StateResultAux = True, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = "Se actualizo y se confirmo correctamente" + Environment.NewLine + resultConfirm.Message}
                End If
            Else
                If basicBilling.ChangeTracker.State = ObjectState.Added Then
                    Return New ActionResult(Of BasicBilling) With {.StateResult = True, .StateResultAux = False, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = String.Format(ResourceManager.GetString("SavedNoConfirmed", "Billing"), result.ObjectEmbbeded.Code, Environment.NewLine + resultConfirm.Message)}
                Else
                    Return New ActionResult(Of BasicBilling) With {.StateResult = True, .StateResultAux = False, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = String.Format(ResourceManager.GetString("UpdatedNoConfirmed", "Billing"), result.ObjectEmbbeded.Code, Environment.NewLine + resultConfirm.Message)}
                End If
            End If
        Else
            Return New ActionResult(Of BasicBilling) With {.StateResult = False, .StateResultAux = False, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = String.Format(ResourceManager.GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message))}
        End If
    End Function

    ''' <summary>
    ''' Funcion que reversa la factura basica. Agrega motivo de reversión.
    ''' </summary>
    ''' <param name="basicBilling"></param>
    ''' <param name="reversalReasonId"></param>
    ''' <param name="reversalReasonDescription"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ReverseBasicBilling(basicBilling As BasicBilling, reversalReasonId As Integer, reversalReasonDescription As String, session As SessionValues) As ActionResult(Of BasicBilling) Implements IBasicBillingAdminService.ReverseBasicBilling

        ' Valida facturacion basica existente
        If basicBilling Is Nothing Then
            Throw New ArgumentNullException("basicBilling")
        End If

        ' Lista de mensaje emergente
        Dim listStrMessage As New List(Of String)
        ' Obtener el detalle de la facturacion basica
        Dim basicBillingDetail As List(Of BasicBillingDetail) = basicBilling.BasicBillingDetail.ToList
        Dim unitOfWork As IUnitWork = Me._basicBillingRepository.UnitWork
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted

        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim invoice = _invoiceRepository.GetInvoiceById(basicBilling.InvoiceId)

                If invoice Is Nothing OrElse invoice?.Id = 0 Then
                    Throw New ArgumentNullException("invoice")
                End If

                'Valida y trae recibos de caja asociados para que sean anulados
                Dim resultGetCashReceipt As List(Of CashReceipts) = Me._basicBillingRepository _
                                                                    .GetCashReceiptsByBasicBillingInvoice(basicBilling.InvoiceId) _
                                                                    .Where(Function(receipt) receipt.Status = 2) _
                                                                    .ToList()

                'conuslto si tiene asociado cruces de anticipos vs Cxc
                Dim listPortfolioTransfer = Me._portfolioTransferRepository?.GetByFilter(Function(x) x.PortfolioTransferDetail.Any(Function(s) s.AccountReceivable.InvoiceId = invoice.Id) And x.Status = 2, False, {"PortfolioAdvance"})?.ToList()

                'realiza la Nota siempre y cuando No exista asociado un cruce, esto para las facturas basicas que se generaron sin cruce por antigua funcionalidad
                If (resultGetCashReceipt?.Any()) AndAlso (listPortfolioTransfer Is Nothing OrElse Not listPortfolioTransfer.Any()) Then
                    For Each treasuryNoteForCashReceipt In resultGetCashReceipt
                        'Generar una nota de tesoreria para los recibos de caja
                        Dim generateTreasuryNote = Me.GenerateTreasuryNoteByReverseCashReceipt(basicBilling, basicBillingDetail, True, reversalReasonDescription, treasuryNoteForCashReceipt.Id, session)
                        If generateTreasuryNote.StateResult = False Then
                            unitOfWork.RollbackChanges()
                            transaction.Dispose()
                            Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = generateTreasuryNote.Message}
                        End If

                        'Obtener secuencia para nota de tesoreria
                        Dim sequenseTreasuryNote = GetSequenseByIdFormForTreasury("637", basicBilling.OperatingUnitId)
                        If sequenseTreasuryNote?.StateResult = False Then
                            unitOfWork.RollbackChanges()
                            transaction.Dispose()
                            Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = sequenseTreasuryNote.Message}
                        End If

                        'Guardar la nota de tesoreria
                        Dim resultTreasuryNote = _treasuryNoteAdminService.SaveTreasuryNote(generateTreasuryNote.ObjectEmbbeded, session.AuditMessageWcf, True, sequenseTreasuryNote.ObjectEmbbeded)
                        If resultTreasuryNote?.StateResultAux Then
                            'resultTreasuryNote.Message = String.Format(ResourceManager.GetString("SavedWithCode"), resultTreasuryNote.ObjectEmbbeded.Code)
                            resultTreasuryNote.Message = $"Se generó la nota de tesorería con código {resultTreasuryNote.ObjectEmbbeded.Code} para el recibo de caja {treasuryNoteForCashReceipt.Code}"
                            listStrMessage.Add(resultTreasuryNote.Message.ToString())
                        Else
                            unitOfWork.RollbackChanges()
                            transaction.Dispose()
                            Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = resultTreasuryNote.Message}
                        End If
                    Next
                End If

                'Obtener secuencia para nota debito/credito
                Dim sequensePortfolioNote = GetSequenseByIdFormForPortfolio("686", basicBilling.OperatingUnitId)
                If sequensePortfolioNote?.StateResult = False Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = sequensePortfolioNote.Message}
                End If

                'Si existe un cruce en estado confirmado realizamos La Nota de reversion del cruce vs cxc 
                'NOTA > (Cuando viene de una anulacion de fact salud el cruce viene en estado anulado) pero adicionalmente se valida que la fact basica no sea copago ya que la reversion del cruce en fact copago se hace desde otro punto
                If listPortfolioTransfer?.Any() AndAlso basicBilling.ThirdPartyEntityCopayId Is Nothing Then

                    For Each item In listPortfolioTransfer
                        Dim noteObjectResult = Me.CreateReverseTransferNoteObject(item, basicBilling.OperatingUnitId, reversalReasonDescription)

                        If noteObjectResult Is Nothing OrElse Not noteObjectResult?.StateResult Then
                            unitOfWork.RollbackChanges()
                            transaction.Dispose()
                            Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = If(noteObjectResult?.Message, "Error generando la nota de reversión del cruce del anticipo")}
                        End If

                        'Guardar la nota debito/credito
                        Dim resultReverseTransferNote = _portfolioNoteAdminService.SavePortfolioNote(noteObjectResult.ObjectEmbbeded, session.AuditMessageWcf, session, sequensePortfolioNote.ObjectEmbbeded)

                        If resultReverseTransferNote?.StateResult Then
                            listStrMessage.Add(resultReverseTransferNote.Message.ToString())
                        Else
                            unitOfWork.RollbackChanges()
                            transaction.Dispose()
                            Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = resultReverseTransferNote?.Message}
                        End If
                    Next
                End If

                'Generar una nota debito/credito
                Dim generatePortfolioNote = Me.GenerateCreditNoteByReverse(basicBilling, basicBillingDetail, reversalReasonDescription, session)
                If generatePortfolioNote.StateResult = False Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = generatePortfolioNote.Message}
                End If

                'Guardar la nota debito/credito
                Dim resultNote = _portfolioNoteAdminService.SavePortfolioNote(generatePortfolioNote.ObjectEmbbeded, session.AuditMessageWcf, session, sequensePortfolioNote.ObjectEmbbeded)
                If resultNote?.StateResult Then
                    listStrMessage.Add(resultNote.Message.ToString())
                Else
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = resultNote.Message}
                End If

                'Reversar la factura con motivo de anulacion
                'Alli dentro del sp generamos el movimiento en el kardex y del inventario
                Dim resultReverse = Me._basicBillingRepository.SP_ReverseBasicBilling(basicBilling.Id, session.AuditMessageWcf.CodeUser, reversalReasonId, reversalReasonDescription, session.AuditMessageWcf.CompanyType)
                If resultReverse.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = resultReverse.Message}
                Else
                    listStrMessage.Add(resultReverse.Message.ToString())
                End If

                Dim PortfolioNote = _portfolioNoteAdminService.GetPortfolioNoteById(resultNote.ObjectEmbbeded.Id)
                'Se valida que la factura tenga un CUFE para generar la nota electrónica por anulación
                If Not String.IsNullOrEmpty(invoice.CUFE) Then
                    Dim res = GenerateElectronicNoteByReverseInvoice(basicBilling, PortfolioNote, session)
                    If Not res.StateResult Then
                        Return New ActionResult(Of BasicBilling) With {.StateResult = False, .Message = res.Message}
                    End If
                End If

                unitOfWork.Commit()

                Dim eventValidate = ValidateExistEvent(EventType.Invoice.ToString(), session.TransactionalContainer)

                ''validacion si se debe publicar un evento o no
                If eventValidate Then
                    _eventProxy.Publish(New EventData(ToInvoiceEvent(basicBilling?.Invoice?.Id), NameOf(EventType.Invoice), NameOf(EventAction.annulate), session.TransactionalContainer, session.UserRol, DateTime.Now().GetTimestamp))
                End If

                transaction.Complete()
                Return New ActionResult(Of BasicBilling) With {.StateResult = True, .ObjectEmbbeded = basicBilling, .Message = String.Join(vbCrLf, listStrMessage)}

            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of BasicBilling) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return New ActionResult(Of BasicBilling) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' se valida si el cliente tiene un evento para ser publicado dependiendo del tipo de evento que se envie, en este caso mayormente facturacion que solo mente esta funcional para costa rica
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="company"></param>
    ''' <returns></returns>
    Public Function ValidateExistEvent(type As String, company As String)
        Dim securityContainer = ConfigurationManager.AppSettings.Get("containerSecurity")
        Dim validateEventConfiguration As EventConfiguration = Nothing

        ''se realiza la consulta a la tabla eventconfiguration
        Using context As New SecurityContext(
                Utils.GetEntityConnectionString(ConfigurationFile.CONX_GENESIS, String.Empty, securityContainer, True)
            )
            validateEventConfiguration = (From ec In context.EventsConfiguration.AsNoTracking()
                                          Where ec.Container.Code = company And ec.Code = type).SingleOrDefault()
        End Using

        ''si existe un evento a publicar se retorna true
        If validateEventConfiguration IsNot Nothing Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Genera el paquete de nota de tesoreria para los recibos de caja
    ''' </summary>
    ''' <param name="basicBilling"></param>
    ''' <param name="basicBillingDetail"></param>
    ''' <param name="withConfirm"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Private Function GenerateTreasuryNoteByReverseCashReceipt(basicBilling As BasicBilling, basicBillingDetail As List(Of BasicBillingDetail), withConfirm As Boolean, reversalReasonDescription As String, cashReceiptId As Integer, session As SessionValues) As ActionResult(Of TreasuryNote)
        'Valida si hay facturacion basica
        If basicBilling Is Nothing Then
            Return New ActionResult(Of TreasuryNote) With {.StateResult = False, .Message = "No existe facturacion basica"}
        End If

        'Armar una nota de tesoreria para reversar el recibo de caja
        Dim cashReceiptNote As New TreasuryNote
        With cashReceiptNote
            .Code = ""
            .NoteDate = DateTime.Now
            .NoteType = 4
            .OperatingUnitId = basicBilling.OperatingUnitId
            .CashRegisterId = Nothing
            .EntityBankAccountId = Nothing
            .VoucherTransactionId = Nothing
            .CashReceiptId = cashReceiptId
            .ConsignmentId = Nothing
            .MainAccountId = Nothing
            .CostCenterId = Nothing
            .CrossingAccountId = Nothing
            .Description = reversalReasonDescription
            .Nature = Nothing
            .Value = Nothing
            .CurrencyId = basicBilling?.CurrencyId
            .Status = If(withConfirm, 2, 1)
        End With

        Return New ActionResult(Of TreasuryNote) With {.StateResult = True, .Message = "OK", .ObjectEmbbeded = cashReceiptNote}

    End Function

    ''' <summary>
    ''' Funcion para generar una nota debito/credito
    ''' </summary>
    ''' <param name="basicBilling">Factura basica</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Private Function GenerateCreditNoteByReverse(basicBilling As BasicBilling, basicBillingDetail As List(Of BasicBillingDetail), reversalReasonDescription As String, session As SessionValues) As ActionResult(Of PortfolioNote)
        If basicBilling Is Nothing Then
            Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .Message = "No existe facturacion basica"}
        End If

        Dim settingsBilling = Me._settingsBillingRepository.GetSettingsBillingByIdUnitOperative(basicBilling.OperatingUnitId, False)
        'Dim settingsBilling = Me._settingsBillingRepository.Query(Function(x) x.IdOperatingUnit = basicBilling.OperatingUnitId, False).FirstOrDefault()

        If settingsBilling?.CapitationLossMainAccountId Is Nothing Then
            Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .Message = "No se encontro un concepto de ajuste en parametros de inventario"}
        End If

        If basicBilling.InvoiceId Is Nothing Then
            Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .Message = "No se encontro una factura"}
        End If

        Dim accountReceivable = Me._accountReceivableRepository.GetAccountReceivableByInvoiceId(basicBilling.InvoiceId).FirstOrDefault()
        If accountReceivable Is Nothing Then
            Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .Message = "No se encontro una factura en cartera"}
        End If

        Dim customer = Me._customerRepository.GetCustomerById(basicBilling.CustomerId)
        If customer Is Nothing Then
            Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .Message = "No se encontro un cliente asociado con factura y tercero"}
        End If

        Dim accountAccounting = accountReceivable.AccountReceivableAccounting.Where(Function(f) f.Balance > 0).FirstOrDefault()
        If accountAccounting Is Nothing Then
            Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .Message = "No se encontro una contabilidad de cuentas con balance mayor a cero"}
        End If

        Dim accountShares = accountReceivable.AccountReceivableShare.FirstOrDefault()
        Dim observations = String.Format("Nota crédito generada por reversión de factura básica {0} - {1}: ", basicBilling.Code, reversalReasonDescription)

        'Obtener el concepto de nota general que no tenga un idAccount
        Dim portfolioNoteConcept = Me._portfolioNoteConceptRepository.GetPortfolioNoteConceptByFilter(noteType:=1, idAccout:=Nothing, status:=1)
        If Not portfolioNoteConcept Is Nothing AndAlso Not portfolioNoteConcept.Id > 0 Then
            Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .Message = "No se encontro un concepto general activo sin cuenta contable relacionada"}
        End If

        'Armar una nota credito
        Try
            Dim portfoliTransferId As Integer? = Nothing
            If basicBilling.InvoiceId.HasValue Then
                Dim invoiceId = basicBilling.InvoiceId.Value
                Dim accountReceivableId = _accountReceivableRepository.Query(Function(m) m.InvoiceId = invoiceId).Select(Function(m) m.Id).FirstOrDefault()
                portfoliTransferId = _portfolioTransferRepository _
                    .Query(Function(m) m.PortfolioTransferDetail.Any(Function(o) o.AccountReceivableId = accountReceivableId)) _
                    .Select(Function(m) m.Id).FirstOrDefault()
            End If

            Dim portfolioNote As New PortfolioNote
            With portfolioNote
                .Code = ""
                .NoteDate = DateTime.Now
                .CustomerId = basicBilling.CustomerId
                .Observations = observations
                .CurrencyId = basicBilling?.CurrencyId  'Moneda
                .EntityName = "ReverseBasicBilling"
                .EntityId = basicBilling.Id
                .EntityCode = basicBilling.Code
                .Nature = 2 ' credito
                .NoteType = 1 ' factura total
                .OperatingUnitId = basicBilling.OperatingUnitId
                .Status = 2 'confirmado
                .CreationUser = session.AuditMessageWcf.CodeUser
                .CreationDate = DateTime.Now
                .ModificationUser = session.AuditMessageWcf.CodeUser
                .ModificationDate = DateTime.Now
                .ConfirmationUser = session.AuditMessageWcf.CodeUser
                .ConfirmationDate = DateTime.Now
                .AnnulmentUser = Nothing
                .AnnulmentDate = Nothing
                .PortfolioAdvanceId = Nothing
                .PortfolioTransferId = portfoliTransferId

                .PortfolioNoteDistribution.Clear()
                .PortfolioNoteAccountReceivableAdvance.Clear()
                .PortfolioNoteDetail.Clear()

                'Concepto de la nota de acuerdo al tipo ' tipo producto = 1 | tipo servicio = 2
                For Each detail In basicBillingDetail
                    detail.RoundLevel = basicBilling.RoundLevel

                    'Id cuenta contable ReteICA y ReteFuente -- se establece dependiendo del tipo de detalle
                    Dim withholdingICAAccountId As Integer? = Nothing
                    Dim withholdingTaxAccountId As Integer? = Nothing

                    If (detail.DetailType = 1 AndAlso detail.ProductId IsNot Nothing) Then 'Producto
                        Dim productWithGroup = Me._inventoryProductRepository.GetInventoryProductByIdWithProducGroup(detail.ProductId)
                        Dim accountPayableConcept = Me._fixedAssetEntryRepository.GetAccountPayableConceptById(productWithGroup.ProductGroup.InventoryAccountPayableConceptId)
                        If detail.CostCenterId = 0 Then
                            detail.CostCenterId = Nothing
                        End If

                        withholdingICAAccountId = productWithGroup.ProductGroup.WithholdingICAAccountId
                        withholdingTaxAccountId = productWithGroup.ProductGroup.WithholdingTaxAccountId

                        'Cuenta Contable Inventario
                        .PortfolioNoteDetail.Add(New PortfolioNoteDetail With {
                                                 .PortfolioNoteConceptId = portfolioNoteConcept.Id, 'Concepto de nota
                                                 .MainAccountId = accountPayableConcept.IdAccount,    'Cuenta contable
                                                 .CostCenterId = detail.CostCenterId, 'Centro de costo 
                                                 .Value = productWithGroup.ProductCost,
                                                 .ThirdPartyId = customer.ThirdPartyId,
                                                 .Nature = 1, 'Debito
                                                 .Observations = reversalReasonDescription
                                                     })

                        'Cuenta contable ingresos
                        .PortfolioNoteDetail.Add(New PortfolioNoteDetail With {
                                                 .PortfolioNoteConceptId = portfolioNoteConcept.Id, 'Concepto de nota
                                                 .MainAccountId = productWithGroup.ProductGroup.IncomeAccountId,    'Cuenta contable
                                                 .CostCenterId = detail.CostCenterId, 'Centro de costo
                                                 .Value = IIf(detail.ValueDiscount > 0 And IIf(basicBilling.CommercialDiscountAccounting Is Nothing, settingsBilling.AccountsConditionalCommercialDiscount, basicBilling.CommercialDiscountAccounting) _
                                                 , detail.Value, detail.Value - detail.ValueDiscount),
                                                 .ThirdPartyId = customer.ThirdPartyId,
                                                 .Nature = 1, 'Debito
                                                 .Observations = reversalReasonDescription})

                        If detail.ValueDiscount > 0 And IIf(basicBilling.CommercialDiscountAccounting Is Nothing, settingsBilling.AccountsConditionalCommercialDiscount, basicBilling.CommercialDiscountAccounting) Then
                            .PortfolioNoteDetail.Add(New PortfolioNoteDetail With {
                                                     .PortfolioNoteConceptId = portfolioNoteConcept.Id, 'Concepto de nota
                                                     .MainAccountId = productWithGroup.ProductGroup.ProductGroupFunctionalUnit.Where(Function(x) x.FunctionalUnitId = detail.FunctionalUnitId).FirstOrDefault().DiscountAccountId,    'Cuenta contable de Cuenta IVA por pagar
                                                     .CostCenterId = detail.CostCenterId, 'Centro de costo
                                                     .Value = detail.ValueDiscount,
                                                     .ThirdPartyId = customer.ThirdPartyId,
                                                     .Nature = 2, 'Credito
                                                     .Observations = reversalReasonDescription})
                        End If

                        If (detail.PercentageIVA > 0) Then
                            'Cuenta IVA por Pagar - al DEBITO
                            .PortfolioNoteDetail.Add(New PortfolioNoteDetail With {
                                                     .PortfolioNoteConceptId = portfolioNoteConcept.Id, 'Concepto de nota
                                                     .MainAccountId = productWithGroup.ProductGroup.IVAAccountId,    'Cuenta contable de Cuenta IVA por pagar
                                                     .CostCenterId = detail.CostCenterId, 'Centro de costo
                                                     .Value = detail.CalculateValueIVA,
                                                     .ThirdPartyId = customer.ThirdPartyId,
                                                     .Nature = 1, 'Debito
                                                     .Observations = reversalReasonDescription})
                        End If

                        'Cuenta Contable Costo Inventario
                        .PortfolioNoteDetail.Add(New PortfolioNoteDetail With {
                                                 .PortfolioNoteConceptId = portfolioNoteConcept.Id, 'Concepto de nota
                                                 .MainAccountId = productWithGroup.ProductGroup.InventoryCostMainAccountId,    'Cuenta contable de Cuenta contable costo inventario
                                                 .CostCenterId = detail.CostCenterId, 'Centro de costo
                                                 .Value = productWithGroup.ProductCost,
                                                 .ThirdPartyId = customer.ThirdPartyId,
                                                 .Nature = 2, 'Credito
                                                 .Observations = reversalReasonDescription})

                    ElseIf (detail.DetailType = 2 AndAlso detail.BillingConceptId IsNot Nothing) Then 'Servicio
                        Dim billingConcept = Me._billingConceptRepository.GetBillingConceptById(detail.BillingConceptId)

                        Dim mainAccountId As Integer = 0
                        If billingConcept.EntityIncomeAccountId.HasValue Then
                            mainAccountId = billingConcept.EntityIncomeAccountId
                        ElseIf basicBilling.InvoiceId.HasValue Then

                            Dim caregroupLiquidationType As Byte = _invoiceCopayRepository.Query(Function(m) m.BasicBillingId = basicBilling.Id).Select(Function(m) m.Invoice.CareGroup.LiquidationType).FirstOrDefault()

                            If caregroupLiquidationType = 1 Then
                                mainAccountId = billingConcept.CopayMainAccountId
                            Else
                                mainAccountId = billingConcept.RecoveryFixedAmountMainAccountId
                            End If
                        End If

                        withholdingICAAccountId = billingConcept?.WithholdingICAAccountId
                        withholdingTaxAccountId = billingConcept?.WithholdingTaxAccountId

                        .PortfolioNoteDetail.Add(New PortfolioNoteDetail With {
                                                 .PortfolioNoteConceptId = portfolioNoteConcept.Id, 'Concepto de nota
                                                 .MainAccountId = mainAccountId, 'Cuenta contable
                                                 .CostCenterId = detail.CostCenterId, 'Centro de Costo
                                                 .Value = IIf(detail.ValueDiscount > 0 And IIf(basicBilling.CommercialDiscountAccounting Is Nothing, settingsBilling.AccountsConditionalCommercialDiscount, basicBilling.CommercialDiscountAccounting) _
                                                 , detail.Value, detail.Value - detail.ValueDiscount),
                                                 .ThirdPartyId = accountReceivable.ThirdPartyId,
                                                 .Nature = 1, 'Debito
                                                 .Observations = reversalReasonDescription})

                        If (detail.PercentageIVA > 0) Then

                            Dim generalLedgerIVAResult = _generalLedgerIVARepository.GetGeneralLedgerIVAById(billingConcept.IVAId)

                            'Cuenta IVA por Pagar - al DEBITO
                            .PortfolioNoteDetail.Add(New PortfolioNoteDetail With {
                                                     .PortfolioNoteConceptId = portfolioNoteConcept.Id, 'Concepto de nota
                                                     .MainAccountId = generalLedgerIVAResult.IdAccountSale,    'Cuenta contable de Cuenta IVA por pagar
                                                     .CostCenterId = detail.CostCenterId, 'Centro de costo
                                                     .Value = detail.CalculateValueIVAWithOutRound,
                                                     .ThirdPartyId = customer.ThirdPartyId,
                                                     .Nature = 1, 'Debito
                                                     .Observations = reversalReasonDescription})
                        End If

                        If detail.ValueDiscount > 0 And IIf(basicBilling.CommercialDiscountAccounting Is Nothing, settingsBilling.AccountsConditionalCommercialDiscount, basicBilling.CommercialDiscountAccounting) Then
                            .PortfolioNoteDetail.Add(New PortfolioNoteDetail With {
                                                     .PortfolioNoteConceptId = portfolioNoteConcept.Id, 'Concepto de nota
                                                     .MainAccountId = billingConcept.DiscountAccountId,    'Cuenta contable de Cuenta IVA por pagar
                                                     .CostCenterId = detail.CostCenterId, 'Centro de costo
                                                     .Value = detail.ValueDiscount,
                                                     .ThirdPartyId = customer.ThirdPartyId,
                                                     .Nature = 2, 'Credito
                                                     .Observations = reversalReasonDescription})
                        End If


                    ElseIf (detail.DetailType = 3 AndAlso detail.PhysicalAssetId IsNot Nothing) Then 'Activo Fijo
                        Throw New Exception("No se tiene contemplado cuando existen Activos Fijos")
                    End If

                    '----------------------------------------------------------------------------------------------
                    'RETENCIONES
                    If (detail.WithholdingICA > 0) Then

                        If withholdingICAAccountId Is Nothing Then
                            Throw New Exception("No se encontró la cuenta de retención de ICA")
                        End If

                        .PortfolioNoteDetail.Add(New PortfolioNoteDetail With {
                                                     .PortfolioNoteConceptId = portfolioNoteConcept.Id, 'Concepto de nota
                                                     .MainAccountId = withholdingICAAccountId,
                                                     .CostCenterId = detail.CostCenterId, 'Centro de costo
                                                     .Value = detail.WithholdingICA,
                                                     .ThirdPartyId = customer.ThirdPartyId,
                                                     .Nature = 2, 'Credito
                                                     .RetentionConceptId = detail.RetentionIdICA,
                                                     .Percentage = detail.RetentionPercentageICA,
                                                     .BaseValue = detail.Value,
                                                     .Observations = reversalReasonDescription})
                    End If

                    If (detail.WithholdingTax > 0) Then

                        If withholdingTaxAccountId Is Nothing Then
                            Throw New Exception("No se encontró la cuenta de retefuente")
                        End If

                        .PortfolioNoteDetail.Add(New PortfolioNoteDetail With {
                                                     .PortfolioNoteConceptId = portfolioNoteConcept.Id, 'Concepto de nota
                                                     .MainAccountId = withholdingTaxAccountId,
                                                     .CostCenterId = detail.CostCenterId, 'Centro de costo
                                                     .Value = detail.WithholdingTax,
                                                     .ThirdPartyId = customer.ThirdPartyId,
                                                     .Nature = 2, 'Credito
                                                     .RetentionConceptId = detail.RetentionIdTax,
                                                     .Percentage = detail.RetentionPercentageTax,
                                                     .BaseValue = detail.Value,
                                                     .Observations = reversalReasonDescription})
                    End If

                    If (basicBilling.RetentionPercentageIVA > 0) Then
                        .PortfolioNoteDetail.Add(New PortfolioNoteDetail With {
                                                 .PortfolioNoteConceptId = portfolioNoteConcept.Id, 'Concepto de nota
                                                 .MainAccountId = settingsBilling.ReteIVAMainAccountId,
                                                 .CostCenterId = detail.CostCenterId, 'Centro de costo
                                                 .Value = Math.Round((detail.CalculateValueIVA * basicBilling.RetentionPercentageIVA / 100), 2, MidpointRounding.AwayFromZero),
                                                 .ThirdPartyId = customer.ThirdPartyId,
                                                 .Nature = 2, 'Credito
                                                 .RetentionConceptId = basicBilling.RetentionIdIVA,
                                                 .Percentage = basicBilling.RetentionPercentageIVA,
                                                 .BaseValue = detail.CalculateValueIVA,
                                                 .Observations = reversalReasonDescription})
                    End If
                Next

                ' AdjusmentValue debe ser accountAccounting.Balance para que el SP pueda validar
                ' que no supera el saldo ni el valor original de la factura.
                ' Si la suma de los detalles difiere de ese saldo por redondeo acumulado de la
                ' facturacion original, se absorbe la diferencia en el detalle de mayor valor de
                ' naturaleza debito, dejando todos los valores exactos.
                Dim totalDebitDetails As Decimal = .PortfolioNoteDetail.Where(Function(d) d.Nature = 1).Sum(Function(d) d.Value)
                Dim totalCreditDetails As Decimal = .PortfolioNoteDetail.Where(Function(d) d.Nature = 2).Sum(Function(d) d.Value)
                Dim roundingDiff As Decimal = accountAccounting.Balance - (totalDebitDetails - totalCreditDetails)
                If roundingDiff <> 0D Then
                    Dim largestDebitDetail = .PortfolioNoteDetail.Where(Function(d) d.Nature = 1).OrderByDescending(Function(d) d.Value).FirstOrDefault()
                    If largestDebitDetail IsNot Nothing Then
                        largestDebitDetail.Value += roundingDiff
                    End If
                End If

                .PortfolioNoteAccountReceivableAdvance.Add(New PortfolioNoteAccountReceivableAdvance With {
                                                                .AccountReceivableId = accountReceivable.Id,
                                                                .AccountReceivableShareId = accountShares.Id,
                                                                .MainAccountId = accountAccounting.MainAccountId,
                                                                .AccountReceivableAccountingId = accountAccounting.Id,
                                                                .PortfolioAdvanceId = Nothing,
                                                                .AdjusmentValue = accountAccounting.Balance,
                                                                .ConceptId = Nothing,
                                                                .PercentageValue = 0
                                                           })
            End With
            Return New ActionResult(Of PortfolioNote) With {.StateResult = True, .Message = "OK", .ObjectEmbbeded = portfolioNote}
        Catch ex As Exception
            Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .Message = ex.Message, .ObjectEmbbeded = Nothing}
        End Try
    End Function

    ''' <summary>
    ''' funcion que se encarga de crear el objeto de Notas de cartera de tipo reversion de cruce de anticipo vs cxc
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateReverseTransferNoteObject(portfolioTransfer As PortfolioTransfer, operativeUnitId As Integer?, Optional reversalReasonDescription As String = Nothing) As ActionResult(Of PortfolioNote)

        If portfolioTransfer Is Nothing OrElse portfolioTransfer.Id = 0 Then
            Throw New ArgumentNullException("portfolioTransferId")
        End If

        If operativeUnitId Is Nothing OrElse operativeUnitId = 0 Then
            Throw New ArgumentNullException("operativeUnitId")
        End If

        Dim currencyId As Integer

        If portfolioTransfer?.PortfolioAdvance?.CurrencyId IsNot Nothing Then
            currencyId = portfolioTransfer?.PortfolioAdvance?.CurrencyId
        ElseIf portfolioTransfer.CurrencyId IsNot Nothing Then
            currencyId = portfolioTransfer.CurrencyId
        Else
            Throw New IndigoValidationException("No se logró obtener la moneda del cruce")
        End If

        Try
            Dim portfolioNote As PortfolioNote = New PortfolioNote
            With portfolioNote
                .NoteDate = DateTime.Now
                .Observations = $"Reversión del cruce de Anticipo vs CxC: {portfolioTransfer.Code}  por: {reversalReasonDescription}"
                .Nature = 1
                .NoteType = 5
                .OperatingUnitId = operativeUnitId
                .CreationDate = DateTime.Now
                .CurrencyId = currencyId
                .Status = 2
                .PortfolioTransferId = portfolioTransfer.Id
            End With

            Return New ActionResult(Of PortfolioNote) With {.StateResult = True, .ObjectEmbbeded = portfolioNote}
        Catch e As IndigoValidationException
            Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(e)}
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Private Properties"

    Private Function SaveElectronicDocument(settingsAccount As GeneralLedgerSettings, billingAuthorization As BillingAuthorization, invoiceId As Integer, session As SessionValues) As ActionResult(Of ElectronicDocument)
        Dim electronicDocument As New ElectronicDocument
        'Consultamos las facturas y, si estas se encuentran habilitadas para la facturación electronica realizamos la actualización de los datos correspondientes
        'Si se maneja facturación electrónica y la autorización de facturación es de tipo electrónica, se agregan los datos que debe incluir la factura
        If settingsAccount.HandlesElectronicBilling = True AndAlso billingAuthorization.InvoiceType = 3 Then
            Dim invoice = _invoiceRepository.GetInvoiceById(invoiceId)
            'Verificamos que se haya asignado un numero de autorización, puesto que este solo se asigna si es pago por servicios (Liquidacion Tipo = 1)
            If invoice.BillingAuthorizationId IsNot Nothing Then
                Dim supplierThirdParty = _thirdpartyRepository.GetThirdPartyById(settingsAccount.IdDian, False)
                Dim customerThirdParty = _thirdpartyRepository.GetThirdPartyById(invoice.ThirdPartyId, False)

                invoice.DianVersion = settingsAccount.DianVersion
                invoice.NitFE = supplierThirdParty.Person.IdentificationNumber
                invoice.TipAdq = customerThirdParty.Person.getAcquirerType()
                invoice.NumAdq = customerThirdParty.Person.IdentificationNumber
                invoice.ClTec = billingAuthorization.TechnicalKey
                invoice.SoftwarePin = settingsAccount.SoftwarePin
                invoice.Environment = settingsAccount.Environment

                invoice.CUFE = invoice.getCUFE()
                invoice.QR = invoice.GetQRCode()

                Dim unitOfWorkInvoice As IUnitWork = _invoiceRepository.UnitWork
                _invoiceRepository.SaveEntity(invoice)
                unitOfWorkInvoice.Commit()

                Dim documentNumber = invoice.InvoiceNumber
                If Not String.IsNullOrEmpty(billingAuthorization.InvoicePrefix) Then
                    documentNumber = invoice.InvoiceNumber.Replace(billingAuthorization.InvoicePrefix, "")
                End If

                With electronicDocument
                    .DianVersion = settingsAccount.DianVersion
                    .OperatingUnitId = invoice.OperatingUnitId
                    .CustomerPartyId = customerThirdParty.Id
                    .EntityId = invoice.Id
                    .EntityName = invoice.GetType().Name
                    .DocumentDate = invoice.InvoiceDate
                    .DocumentType = invoice.DocumentType
                    .Status = 1
                    .CreationDate = DateTime.Now
                    .Container = session.TransactionalContainer
                    .Prefix = billingAuthorization.InvoicePrefix
                    .DocumentNumber = documentNumber
                    .CUFE = invoice.CUFE
                    .Year = DateTime.Now.Year
                End With

                Dim operatingUnit = _operatingUnitRepository.GetOperatingUnitById(invoice.OperatingUnitId)
                electronicDocument.FilePath = System.IO.Path.Combine(
                    Utils.GetPathElectronicDocuments(),
                    electronicDocument.Container,
                    operatingUnit.UnitCode,
                    electronicDocument.DocumentDate.Year.ToString(),
                    electronicDocument.DocumentDate.Month.ToString(),
                    electronicDocument.getDocumentTypeName(),
                    invoice.InvoiceNumber
                )

                Dim unitOfWorkElectronicDocument As IUnitWork = Me._electronicDocumentRepository.UnitWork
                _electronicDocumentRepository.SaveEntity(electronicDocument)
                unitOfWorkElectronicDocument.Commit()
            End If
        End If

        Return New ActionResult(Of ElectronicDocument) With {.StateResult = True, .ObjectEmbbeded = electronicDocument}
    End Function

    Private Function ConvertToXmlBasicBilling(basicBilling As BasicBilling) As String
        Dim builder As StringBuilder = New StringBuilder()

        Dim rowBasicBillingDetail = 1
        Dim rowBasicBillingGifts = 1

        builder.Append("<BasicBilling>")

        builder.Append("<Id>" & basicBilling.Id & "</Id>")
        builder.Append("<Code>" & basicBilling.Code & "</Code>")
        builder.Append("<DocumentDate>" & basicBilling.DocumentDate.ToString("dd/MM/yyyy HH:mm") & "</DocumentDate>")
        builder.Append("<Description>" & basicBilling.Description & "</Description>")
        builder.Append("<SaleModality>" & basicBilling.SaleModality & "</SaleModality>")
        builder.Append("<Status>" & basicBilling.Status & "</Status>")
        builder.Append("<OperatingUnitId>" & basicBilling.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<BillingAuthorizationId>" & basicBilling.BillingAuthorizationId & "</BillingAuthorizationId>")
        builder.Append("<CustomerId>" & basicBilling.CustomerId & "</CustomerId>")
        builder.Append($"<CurrencyId>{basicBilling.CurrencyId}</CurrencyId>")
        If Not String.IsNullOrEmpty(basicBilling.ThirdPartyCustomerId) Then
            builder.Append($"<ThirdPartyId>{basicBilling.ThirdPartyCustomerId.Split("-")(0).Trim()}</ThirdPartyId>")
        End If
        builder.Append("<AddressId>" & basicBilling.AddressId & "</AddressId>")
        builder.Append("<Value>" & basicBilling.Value.ToString().Replace(",", ".") & "</Value>")

        If basicBilling.ThirdPartyEntityCopayId.HasValue Then
            builder.Append($"<ThirdPartyEntityCopayId>{basicBilling.ThirdPartyEntityCopayId}</ThirdPartyEntityCopayId>")

            If basicBilling.InvoiceId.HasValue Then
                builder.Append("<InvoiceId>" & basicBilling.InvoiceId & "</InvoiceId>")
            End If
        End If

        If basicBilling.ConditionSalesId.HasValue Then
            builder.Append($"<ConditionSalesId>{basicBilling.ConditionSalesId}</ConditionSalesId>")
        End If

        If basicBilling.EconomicActivityId.HasValue Then
            builder.Append($"<EconomicActivityId>{basicBilling.EconomicActivityId}</EconomicActivityId>")
        End If

        builder.Append("<ValueDiscount>" & basicBilling.ValueDiscount.ToString().Replace(",", ".") & "</ValueDiscount>")
        builder.Append("<ValueIVA>" & basicBilling.ValueIVA.ToString().Replace(",", ".") & "</ValueIVA>")
        builder.Append("<WithholdingTax>" & basicBilling.WithholdingTax.ToString().Replace(",", ".") & "</WithholdingTax>")
        builder.Append("<RetentionIdIVA>" & IIf(basicBilling.RetentionIdIVA = 0, "", basicBilling.RetentionIdIVA) & "</RetentionIdIVA>")
        builder.Append("<RetentionPercentageIVA>" & basicBilling.RetentionPercentageIVA.ToString().Replace(",", ".") & "</RetentionPercentageIVA>")
        builder.Append("<WithholdingIVA>" & basicBilling.WithholdingIVA.ToString().Replace(",", ".") & "</WithholdingIVA>")
        builder.Append("<WithholdingICA>" & basicBilling.WithholdingICA.ToString().Replace(",", ".") & "</WithholdingICA>")
        builder.Append("<TotalValue>" & basicBilling.TotalValue.ToString().Replace(",", ".") & "</TotalValue>")
        If basicBilling.BudgetId IsNot Nothing Then
            builder.Append("<BudgetId>" & basicBilling.BudgetId & "</BudgetId>")
        End If

        builder.Append("<RoundLevel>" & basicBilling.RoundLevel & "</RoundLevel>")
        builder.Append("<IsImported>" & basicBilling.IsImported & "</IsImported>")

        For Each detail In basicBilling.BasicBillingDetail
            builder.Append("<BasicBillingDetail>")

            builder.Append("<Id>" & detail.Id & "</Id>")
            builder.Append("<TempId>" & rowBasicBillingDetail & "</TempId>")
            builder.Append("<BasicBillingId>" & detail.BasicBillingId & "</BasicBillingId>")
            builder.Append("<DetailType>" & detail.DetailType & "</DetailType>")
            builder.Append("<ProductId>" & detail.ProductId & "</ProductId>")
            builder.Append("<BillingConceptId>" & detail.BillingConceptId & "</BillingConceptId>")
            builder.Append("<PhysicalAssetId>" & detail.PhysicalAssetId & "</PhysicalAssetId>")
            builder.Append("<PhysicalAssetPartId>" & detail.PhysicalAssetPartId & "</PhysicalAssetPartId>")
            builder.Append("<Quantity>" & detail.Quantity & "</Quantity>")
            builder.Append("<Price>" & detail.Price.ToString().Replace(",", ".") & "</Price>")
            builder.Append("<Value>" & detail.Value.ToString().Replace(",", ".") & "</Value>")
            builder.Append("<PercentageDiscount>" & detail.PercentageDiscount.ToString().Replace(",", ".") & "</PercentageDiscount>")
            builder.Append("<ValueDiscount>" & detail.ValueDiscount.ToString().Replace(",", ".") & "</ValueDiscount>")
            builder.Append("<PercentageIVA>" & detail.PercentageIVA.ToString().Replace(",", ".") & "</PercentageIVA>")
            builder.Append("<RetentionIdTax>" & IIf(detail.RetentionIdTax Is Nothing OrElse detail.RetentionIdTax = 0, "", detail.RetentionIdTax) & "</RetentionIdTax>")
            builder.Append("<RetentionPercentageTax>" & detail.RetentionPercentageTax.ToString().Replace(",", ".") & "</RetentionPercentageTax>")
            builder.Append("<WithholdingTax>" & detail.WithholdingTax.ToString().Replace(",", ".") & "</WithholdingTax>")

            If detail.RetentionIdICA IsNot Nothing Then
                builder.Append("<RetentionIdICA>" & IIf(detail.RetentionIdICA = 0, "", detail.RetentionIdICA) & "</RetentionIdICA>")
            End If

            builder.Append("<RetentionPercentageICA>" & detail.RetentionPercentageICA.ToString().Replace(",", ".") & "</RetentionPercentageICA>")
            builder.Append("<WithholdingICA>" & detail.WithholdingICA.ToString().Replace(",", ".") & "</WithholdingICA>")
            builder.Append("<ServicesProvided>" & detail.ServicesProvidedId.ToString().Replace(",", ".") & "</ServicesProvided>")
            builder.Append("<Supplier>" & detail.SupplierId.ToString().Replace(",", ".") & "</Supplier>")
            builder.Append("<SalesExecutive>" & detail.SalesExecutiveId.ToString().Replace(",", ".") & "</SalesExecutive>")

            If detail.WarehouseId IsNot Nothing AndAlso detail.ProductId > 0 Then
                builder.Append("<WarehouseId>" & detail.WarehouseId & "</WarehouseId>")
            End If

            builder.Append("<FeeId>" & detail.FeeId & "</FeeId>")
            builder.Append("<FunctionalUnitId>" & detail.FunctionalUnitId & "</FunctionalUnitId>")
            builder.Append("<EconomicActivityId>" & detail.EconomicActivityId & "</EconomicActivityId>")
            builder.Append("<IsDelete>" & If(detail.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

            If detail.BasicBillingDetailItem IsNot Nothing AndAlso detail.BasicBillingDetailItem.Count > 0 Then
                For Each item In detail.BasicBillingDetailItem
                    builder.Append("<BasicBillingDetailItem>")

                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<ParentId>" & rowBasicBillingDetail & "</ParentId>")
                    builder.Append("<BasicBillingDetailId>" & item.BasicBillingDetailId & "</BasicBillingDetailId>")
                    builder.Append("<PhysicalInventoryId>" & item.PhysicalInventoryId & "</PhysicalInventoryId>")
                    builder.Append("<Quantity>" & item.Quantity & "</Quantity>")

                    builder.Append("</BasicBillingDetailItem>")
                Next
            End If

            builder.Append("</BasicBillingDetail>")
            rowBasicBillingDetail += 1
        Next

        For Each detail In basicBilling.BasicBillingGifts
            builder.Append("<BasicBillingGifts>")

            builder.Append("<Id>" & detail.Id & "</Id>")
            builder.Append("<TempId>" & rowBasicBillingGifts & "</TempId>")
            builder.Append("<BasicBillingId>" & detail.BasicBillingId & "</BasicBillingId>")
            builder.Append("<ProductId>" & detail.ProductId & "</ProductId>")
            builder.Append("<Quantity>" & detail.Quantity & "</Quantity>")
            builder.Append("<WarehouseId>" & detail.WarehouseId & "</WarehouseId>")
            builder.Append("<IsDelete>" & If(detail.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

            If detail.BasicBillingGiftsItem IsNot Nothing AndAlso detail.BasicBillingGiftsItem.Count > 0 Then
                For Each item In detail.BasicBillingGiftsItem
                    builder.Append("<BasicBillingGiftsItem>")

                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<ParentId>" & rowBasicBillingGifts & "</ParentId>")
                    builder.Append("<BasicBillingGiftsId>" & item.BasicBillingGiftsId & "</BasicBillingGiftsId>")
                    builder.Append("<PhysicalInventoryId>" & item.PhysicalInventoryId & "</PhysicalInventoryId>")
                    builder.Append("<Quantity>" & item.Quantity & "</Quantity>")

                    builder.Append("</BasicBillingGiftsItem>")
                Next
            End If

            builder.Append("</BasicBillingGifts>")
            rowBasicBillingGifts += 1
        Next

        builder.Append("</BasicBilling>")

        Return builder.ToString()
    End Function

    Private Async Function GenerateInventoryAdjusments(basicBilling As BasicBilling, audit As AuditMessage) As Threading.Tasks.Task(Of ActionResult)
        Dim settingsBilling = _settingsBillingRepository.GetSettingsBillingByIdUnitOperative(basicBilling.OperatingUnitId, False)
        If settingsBilling Is Nothing Then
            Throw New IndigoValidationException("No se encontraron parámetros de facturación")
        End If
        If settingsBilling.GiftProductOutletConcept Is Nothing Then
            Throw New IndigoValidationException("No se encontró parámetro de Concepto salida producto obsequio")
        End If

        Dim sequenceId = _inventorySequenceAdminService.GetCurrentSequenceByIdForm("318", basicBilling.OperatingUnitId)
        Dim invoice = _invoiceRepository.GetInvoiceById(basicBilling.InvoiceId)

        Dim sb As New StringBuilder()
        For Each warehouseId In basicBilling.BasicBillingGifts.GroupBy(Function(gift) gift.WarehouseId).Select(Function(g) g.Key)
            Dim warehouse = Await _warehouseRepository.GetWarehouseByIdAsync(warehouseId.Value)
            Dim supplier = _supplierRepository.GetSupplierById(warehouse.SupplierId)
            Dim basicBillingGifts = basicBilling.BasicBillingGifts.Where(Function(gift) gift.WarehouseId = warehouseId).ToList()

            Dim resultAdjustment = Await Me.ConfirmInventoryAdjustmentOutput(warehouseId,
                                                                       settingsBilling.GiftProductOutletConcept,
                                                                       supplier.IdThirdParty,
                                                                       invoice.InvoiceDate,
                                                                       $"Salida de producto Obsequio relacionada a la factura de venta No {invoice.InvoiceNumber}",
                                                                       basicBilling.OperatingUnitId,
                                                                       basicBillingGifts,
                                                                       sequenceId,
                                                                       audit)
            sb.AppendLine(resultAdjustment.Message)
        Next

        Return New ActionResult With {.StateResult = True, .Message = sb.ToString()}
    End Function

    Private Async Function ConfirmInventoryAdjustmentOutput(wareHouseId As Integer,
        inventoryAdjustmentConceptOutputId As Integer,
        thirdPartyId As Integer,
        documentDate As Date,
        description As String,
        operativeUnitId As Integer,
        basicBillingGifts As List(Of BasicBillingGifts),
        sequenceId As Integer,
        audit As AuditMessage) As Threading.Tasks.Task(Of ActionResult(Of InventoryAdjustment))

        Dim adjustmentControl As New InventoryAdjustment With {
            .DocumentDate = documentDate,
            .AdjustmentType = 2, ' Tipo de ajuste de Inventario Salida
            .ThirdPartyId = thirdPartyId,
            .Description = description,
            .AdjustmentConceptId = inventoryAdjustmentConceptOutputId,
            .WarehouseId = wareHouseId,
            .OperatingUnitId = operativeUnitId,
            .Status = 2
        }

        For Each gift In basicBillingGifts
            Dim adjustmentDetail As New InventoryAdjustmentDetail With {
                .Id = 0,
                .ProductId = gift.ProductId,
                .Quantity = gift.Quantity,
                .UnitValue = 0
            }

            For Each item In gift.BasicBillingGiftsItem
                Dim physicalInventory = _physicalInventoryRepository.GetPhysicalInventoryById(item.PhysicalInventoryId)

                Dim adjustmentDetailBatchSerial As New InventoryAdjustmentDetailBatchSerial With {
                    .Id = 0,
                    .Quantity = item.Quantity,
                    .BatchSerialId = physicalInventory.BatchSerialId
                }
                adjustmentDetail.InventoryAdjustmentDetailBatchSerial.Add(adjustmentDetailBatchSerial)
            Next

            adjustmentControl.InventoryAdjustmentDetail.Add(adjustmentDetail)
        Next

        If Not adjustmentControl.InventoryAdjustmentDetail.Any() Then
            Throw New IndigoValidationException("No se encontraron detalles con cantidades a ajustar")
        End If

        Dim result = Await _inventoryAdjustmentAdminService.SaveInventoryAdjustment(adjustmentControl, audit, sequenceId, Nothing)
        If Not result.StateResult Then
            Throw New IndigoValidationException(result.Message)
        End If

        Dim resultConfirm = Await _inventoryAdjustmentAdminService.ConfirmInventoryAdjustment(result.ObjectEmbbeded, audit, operativeUnitId)
        If Not resultConfirm.StateResult Then
            Throw New IndigoValidationException(resultConfirm.Message)
        End If

        If resultConfirm.ObjectEmbbeded IsNot Nothing Then
            resultConfirm.Message = $"Se generó el ajuste de inventario {resultConfirm.ObjectEmbbeded.Code}"
        End If

        Return resultConfirm
    End Function

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm"></param>
    ''' <param name="_idOperativeUnit"></param>
    ''' <returns></returns>
    Public Function GetSequenseByIdFormForPortfolio(idForm As String, Optional _idOperativeUnit As Integer? = Nothing) As ActionResult(Of Integer?)

        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Try
            Dim _idCurrentSequence As Integer
            Dim obj = Me._sequensePortfolioRepository.GetSequenseByIdForm(idForm.Trim())

            If obj.Scope.Equals("O") Then 'El ambito es a nivel de organización
                _idCurrentSequence = obj.PortfolioSequenceDetail(0).Id
            ElseIf obj.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If obj.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = _idOperativeUnit) Then
                    _idCurrentSequence = obj.PortfolioSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = _idOperativeUnit).Id
                Else
                    Throw New Exception("No se encontro secuencia numerica para la unidad operativa")
                End If
            Else
                Throw New Exception("No se encontro secuencia numerica ")
            End If

            Return New ActionResult(Of Integer?) With {.StateResult = True, .ObjectEmbbeded = _idCurrentSequence}
        Catch ex As Exception
            Return New ActionResult(Of Integer?) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenseByIdFormForTreasury(idForm As String, Optional _idOperativeUnit As Integer? = Nothing) As ActionResult(Of Integer?)

        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Try
            Dim _idCurrentSequence As Integer
            Dim obj = Me._sequenseTreasuryRepository.GetSequenseByIdForm(idForm.Trim())

            If obj.Scope.Equals("O") Then 'El ambito es a nivel de organización
                _idCurrentSequence = obj.TreasurySequenceDetail(0).Id
            ElseIf obj.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If obj.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = _idOperativeUnit) Then
                    _idCurrentSequence = obj.TreasurySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = _idOperativeUnit).Id
                Else
                    Throw New Exception("No se encontro secuencia numerica para la unidad operativa")
                End If
            Else
                Throw New Exception("No se encontro secuencia numerica ")
            End If

            Return New ActionResult(Of Integer?) With {.StateResult = True, .ObjectEmbbeded = _idCurrentSequence}
        Catch ex As Exception
            Return New ActionResult(Of Integer?) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Genera la nota que se envia a la DIAN por anulación de factura
    ''' </summary>
    ''' <param name="basicBilling"></param>
    ''' <param name="portFolioNote"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Private Function GenerateElectronicNoteByReverseInvoice(basicBilling As BasicBilling, portFolioNote As PortfolioNote, session As SessionValues) As ActionResult
        Dim codeNote As String = String.Empty
        Dim invoice = _invoiceRepository.GetInvoiceById(basicBilling.InvoiceId)
        If String.IsNullOrEmpty(invoice.CUFE) Then
            Return New ActionResult With {.StateResult = False, .Message = "La factura no corresponde a una factura electrónica."}
        End If
        Dim reservation = Me._sequenseRepository.ReserveNextFormattedCodeByFormId("2037")
        If Not reservation.Success Then
            Return New ActionResult With {.StateResult = False, .Message = reservation.Message}
        End If
        codeNote = reservation.Code
        Dim BillingNote As New BillingNote With
        {
            .Code = codeNote,
            .NoteDate = DateTime.Now,
            .CustomerPartyId = invoice.ThirdPartyId,
            .Observations = portFolioNote.Observations,
            .Nature = 2,
            .OperatingUnitId = basicBilling.OperatingUnitId,
            .EntityId = portFolioNote.Id,
            .EntityName = portFolioNote.GetType().Name
        }
        Dim billingNoteDetail As New BillingNoteDetail With
        {
            .InvoiceId = invoice.Id,
            .InvoiceNumber = invoice.InvoiceNumber,
            .CUFE = invoice.CUFE,
            .DocumentDate = invoice.InvoiceDate,
            .AdjusmentValue = (invoice.InvoiceValue + invoice.ValueTax),
            .BillingValue = invoice.InvoiceValue,
            .DiscountValue = invoice.ThirdPartyDiscountValue,
            .ConceptId = 2
        }
        ' Un solo recorrido: acumula gravados (pct > 0) y exentos (pct = 0, tipo 3) en paralelo
        Dim gravadoBase As New Dictionary(Of Decimal, Decimal)
        Dim gravadoTax As New Dictionary(Of Decimal, Decimal)
        Dim gravadoIvaId As New Dictionary(Of Decimal, Integer?)
        Dim exentBase As New Dictionary(Of Integer, Decimal)
        Dim ivaTypeCache As New Dictionary(Of Integer, Byte)

        For Each detail In basicBilling.BasicBillingDetail
            Dim detailIvaId As Integer? = detail.InventoryProduct?.IVAId
            If detailIvaId Is Nothing Then
                detailIvaId = If(detail.FixedAssetPhysicalAsset?.FixedAssetItem IsNot Nothing,
                                 CType(detail.FixedAssetPhysicalAsset.FixedAssetItem.IVAId, Integer?), Nothing)
            End If
            If detailIvaId Is Nothing AndAlso detail.BillingConceptId IsNot Nothing Then
                detailIvaId = _billingConceptRepository.GetBillingConceptById(detail.BillingConceptId.Value, tracking:=False)?.IVAId
            End If

            If detail.PercentageIVA > 0 Then
                Dim pct = detail.PercentageIVA
                Dim lineBase = detail.Value - detail.ValueDiscount
                If gravadoBase.ContainsKey(pct) Then
                    gravadoBase(pct) += lineBase
                    gravadoTax(pct) += detail.CalculateValueIVA()
                    If Not gravadoIvaId(pct).HasValue Then gravadoIvaId(pct) = detailIvaId
                Else
                    gravadoBase(pct) = lineBase
                    gravadoTax(pct) = detail.CalculateValueIVA()
                    gravadoIvaId(pct) = detailIvaId
                End If
            ElseIf detailIvaId.HasValue Then
                ' pct = 0: verificar si es exento (tipo 3) o excluido (tipo 2)
                Dim taxClassification As Byte
                If Not ivaTypeCache.TryGetValue(detailIvaId.Value, taxClassification) Then
                    Dim iva = _generalLedgerIVARepository.GetGeneralLedgerIVAById(detailIvaId.Value)
                    taxClassification = If(iva IsNot Nothing, iva.TaxClassificationType, CByte(0))
                    ivaTypeCache(detailIvaId.Value) = taxClassification
                End If
                If taxClassification = 3 Then
                    Dim lineBase = detail.Value - detail.ValueDiscount
                    If exentBase.ContainsKey(detailIvaId.Value) Then
                        exentBase(detailIvaId.Value) += lineBase
                    Else
                        exentBase(detailIvaId.Value) = lineBase
                    End If
                End If
            End If
        Next

        For Each pct In gravadoBase.Keys
            billingNoteDetail.BillingNoteDetailTax.Add(
                New BillingNoteDetailTax With
                {
                .TaxPercentage = pct,
                .TaxValue = gravadoTax(pct),
                .BaseValue = gravadoBase(pct),
                .IVAId = gravadoIvaId(pct)
                })
        Next
        For Each kvp In exentBase
            billingNoteDetail.BillingNoteDetailTax.Add(
                New BillingNoteDetailTax With
                {
                .TaxPercentage = 0,
                .TaxValue = 0,
                .BaseValue = kvp.Value,
                .IVAId = kvp.Key
                })
        Next
        BillingNote.BillingNoteDetail.Add(billingNoteDetail)

        Dim unitWorkBillingNote = _billingNoteRepository.UnitWork
        BillingNote.CUDE = BillingNote.getCUDE()
        _billingNoteRepository.SaveEntity(BillingNote)
        unitWorkBillingNote.Commit()

        Dim operatingUnit = _operatingUnitRepository.GetOperatingUnitById(basicBilling.OperatingUnitId)
        Dim accountingSettings = _settingsAccountRepository.GetSettingAccountSimple(basicBilling.OperatingUnitId)

        ' Preparamos dos StringBuilder con capacidad estimada
        Dim prefixSb As New Text.StringBuilder(codeNote.Length)
        Dim numberSb As New Text.StringBuilder(codeNote.Length)

        For Each c As Char In codeNote
            If Char.IsDigit(c) Then
                numberSb.Append(c)
            Else
                prefixSb.Append(c)
            End If
        Next

        Dim electronicDocument As New ElectronicDocument With
        {
            .DianVersion = accountingSettings.DianVersion,
            .OperatingUnitId = basicBilling.OperatingUnitId,
            .CustomerPartyId = invoice.ThirdPartyId,
            .EntityId = BillingNote.Id,
            .EntityName = BillingNote.GetType().Name,
            .DocumentDate = BillingNote.NoteDate,
            .DocumentType = BillingNote.GetDocumentType(),
            .Status = 1,
            .CreationDate = DateTime.Now,
            .Container = session.TransactionalContainer,
            .Prefix = prefixSb.ToString(),
            .DocumentNumber = numberSb.ToString(),
            .CUFE = BillingNote.CUDE,
            .Year = DateTime.Now.Year
        }
        electronicDocument.FilePath = System.IO.Path.Combine(
                Utils.GetPathElectronicDocuments(),
                electronicDocument.Container,
                operatingUnit.UnitCode,
                electronicDocument.DocumentDate.Year.ToString(),
                electronicDocument.DocumentDate.Month.ToString(),
                electronicDocument.getDocumentTypeName(),
                String.Concat(electronicDocument.Prefix, electronicDocument.DocumentNumber)
                )

        Dim unitOfWorkElectronicDocument = _electronicDocumentRepository.UnitWork
        _electronicDocumentRepository.SaveEntity(electronicDocument)
        unitOfWorkElectronicDocument.Commit()
        Return New ActionResult With {.StateResult = True, .Message = "OK"}
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                Me._inventorySequenceAdminService.Dispose()
                Me._inventoryAdjustmentAdminService.Dispose()
            End If

            'Limpiar
            Me._basicBillingRepository = Nothing
            Me._settingsBillingRepository = Nothing
            Me._accountReceivableRepository = Nothing
            Me._functionalUnitRepository = Nothing
            Me._costCenterRepository = Nothing
            Me._portfolioNoteConceptRepository = Nothing
            Me._customerRepository = Nothing
            Me._billingConceptRepository = Nothing
            Me._mainAccountsRepository = Nothing
            Me._sequensePortfolioRepository = Nothing
            Me._sequenseTreasuryRepository = Nothing
            Me._inventoryProductRepository = Nothing
            Me._warehouseRepository = Nothing
            Me._supplierRepository = Nothing
            Me._inventorySequenceAdminService = Nothing
            Me._physicalInventoryRepository = Nothing
            Me._inventoryAdjustmentAdminService = Nothing
            Me._fixedAssetEntryRepository = Nothing

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
