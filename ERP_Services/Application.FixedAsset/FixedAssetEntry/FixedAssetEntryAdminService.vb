#Region "Imports"

Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Text
Imports Domain.Entities.Service
Imports Application.Payments
Imports System.Data.SqlClient
Imports Application.Accounting
Imports Application.FixedAsset

#End Region

Public Class FixedAssetEntryAdminService
    Implements IFixedAssetEntryAdminService

#Region "Variables"

    'Repositorio de la aseguradora
    Private _FixedAssetEntryRepository As IFixedAssetEntryRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    ''' <summary>
    ''' Dominio
    ''' </summary>
    ''' <remarks></remarks>
    Private _fixedAssetServices As IFixedAssetServices

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
    ''' Repositorio para la tarifa de IVA
    ''' </summary>
    ''' <remarks></remarks>
    Private _generalLedgerIVARepository As IGeneralLedgerIVARepository

    ''' <summary>
    ''' Aplicacion de cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableAdminService As IAccountPayableAdminService

    ''' <summary>
    ''' Repositorio de Catálogo de Artículos
    ''' </summary>
    Private _equipmentCatalogRepository As IFixedAssetItemCatalogRepository

    ''' <summary>
    ''' Aplicación de Documentos Contables
    ''' </summary>
    ''' <remarks></remarks>
    Private _AccountingDocumentAdmin As IAccountingDocumentAdminService

    ''' <summary>
    ''' Repositorio de viebot
    ''' </summary>
    ''' <remarks></remarks>
    Private _vieBotRepository As IVieBotRepository

    ''' <summary>
    ''' Aplicacion de cuentas
    ''' </summary>
    Private _pucAdminService As IPUCAdminService
#End Region

#Region "Builder"

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal FixedAssetEntryRepository As IFixedAssetEntryRepository, sequenceRepository As IFixedAssetSequenceDetailRepository, fixedAssetServices As IFixedAssetServices,
                   sequensePaymentsCRepository As ISequensePaymentsCRepository, supplierRepository As ISupplierRepository, supplierDistributionLineRepository As ISuppliersDistributionLinesRepository,
                   paymentsConceptRepository As IPaymentsConceptRepository, accountPayableAdminService As IAccountPayableAdminService, equipmentCatalogRepository As IFixedAssetItemCatalogRepository,
                   AccountingDocumentAdmin As IAccountingDocumentAdminService, vieBotRepository As IVieBotRepository,
                   GeneralLedgerIVARepository As IGeneralLedgerIVARepository, pucAdminService As IPUCAdminService)
        If FixedAssetEntryRepository Is Nothing Then
            Throw New ArgumentNullException("FixedAssetEntryRepository")
        End If
        If sequenceRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceRepository")
        End If
        If fixedAssetServices Is Nothing Then
            Throw New ArgumentNullException("fixedAssetServices")
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
        If equipmentCatalogRepository Is Nothing Then
            Throw New ArgumentNullException("equipmentCatalogRepository")
        End If
        If vieBotRepository Is Nothing Then
            Throw New ArgumentNullException("vieBotRepository")
        End If
        If GeneralLedgerIVARepository Is Nothing Then
            Throw New ArgumentNullException("GeneralLedgerIVA")
        End If

        _sequenceRepository = sequenceRepository
        _FixedAssetEntryRepository = FixedAssetEntryRepository
        _fixedAssetServices = fixedAssetServices
        _sequensePaymentsCRepository = sequensePaymentsCRepository
        _supplierRepository = supplierRepository
        _supplierDistributionLineRepository = supplierDistributionLineRepository
        _paymentsConceptRepository = paymentsConceptRepository
        _accountPayableAdminService = accountPayableAdminService
        _equipmentCatalogRepository = equipmentCatalogRepository
        _AccountingDocumentAdmin = AccountingDocumentAdmin
        _vieBotRepository = vieBotRepository
        _generalLedgerIVARepository = GeneralLedgerIVARepository
        _pucAdminService = pucAdminService
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Confirma el ingreso
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmFixedAssetEntry(FixedAssetEntry As FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook As List(Of FixedAssetEntryItemDetailPartBook), ListDeleteFixedAssetEntryItemDetailPart As List(Of FixedAssetEntryItemDetailPart), ListDeleteFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook), ListDeleteFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail), ListDeleteFixedAssetEntryItem As List(Of FixedAssetEntryItem), audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetEntry) Implements IFixedAssetEntryAdminService.ConfirmFixedAssetEntry
        If FixedAssetEntry Is Nothing Then
            Throw New ArgumentNullException("FixedAssetEntry")
        End If

        'Unidad de trabajo
        Dim unitOfWork As IUnitWork = Me._FixedAssetEntryRepository.UnitWork

        'Variable de secuencia numerica
        Dim SequenseDetailId As Integer = 0
        'Se consulta la secuencia de pagos por el id del form
        Dim sequensePayments As PaymentsSecuence = _sequensePaymentsCRepository.GetSequenseByIdForm("730")
        If sequensePayments.Id = 0 Then
            Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe secuencia numérica para el formulario de cuentas por pagar."}
        End If
        'Se valida que la secuencia numerica no sea manual
        If sequensePayments.IsManual Then
            Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = "La secuencia numerica de CxP es manual."}
        End If
        'Se valida la secuencia numerica
        If sequensePayments.Scope = "O" Then 'Si la secuencia es por organización
            SequenseDetailId = (From x In sequensePayments.PaymentsSecuenceDetail Select x.Id).FirstOrDefault()
        Else 'Si la secuencia es por unidad operativa
            'Se valida que la unidad operativa seleccionada este en la secuencia
            If (From x In sequensePayments.PaymentsSecuenceDetail Where x.IdOperatingUnit = FixedAssetEntry.OperatingUnitId Select x).Count = 0 Then
                Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe la unidad operativa seleccionada en la secuencia de CxP."}
            End If
            SequenseDetailId = (From x In sequensePayments.PaymentsSecuenceDetail Where x.IdOperatingUnit = FixedAssetEntry.OperatingUnitId Select x.Id).FirstOrDefault()
        End If

        'Se consulta si hay parámetros de activo fijo
        Dim settingFixedAsset As SettingFixedAsset = _FixedAssetEntryRepository.GetSettingFixedAssetByOperatingUnidId(FixedAssetEntry.OperatingUnitId)
        If settingFixedAsset Is Nothing Then
            Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe parámetros de activo fijo para la unidad operativa escogida."}
        End If
        If settingFixedAsset.FreightIVAPercentage Is Nothing Then
            Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = "No se ha parametrizado un concepto de pago IVA Flete que maneje Retención."}
        End If
        'Se valida que existan parámetros definidos para el libro oficial en la unidad operativa actual
        If settingFixedAsset.SettingFixedAssetByLegalBook.Where(Function(d) d.LegalBook.OfficialBook = True AndAlso d.LegalBook.Status = True).Count() = 0 Then
            Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe parámetros de activo fijo en unidad operativa escogida para el libro oficial."}
        End If
        'Para la contabilización, se verifica que todos los libros válidos de viebot para el proceso actual esten parametrizados
        Dim errors As New StringBuilder
        Dim ListVieBot As List(Of VieBot) = Me._vieBotRepository.GetVieBotByForm(FixedAssetEntry.GetType().Name, False)
        For Each itemVieBot In ListVieBot
            If settingFixedAsset.SettingFixedAssetByLegalBook.Where(Function(d) d.LegalBookId = itemVieBot.LegalBookId).Count() = 0 Then
                errors.AppendLine("El libro " + itemVieBot.CodeNameLegalBook + " no esta parametrizado para la unidad operativa escogida")
            ElseIf itemVieBot.Allow = True AndAlso itemVieBot.HandlesHomologation = True Then
                errors.AppendLine("El libro " + itemVieBot.CodeNameLegalBook + " no debe ser homologable")
            End If
        Next
        If errors.Length > 0 Then
            Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = errors.ToString()}
        End If

        'Se valida que la fecha de ingreso este en el mismo mes que la fecha de parametros
        If FixedAssetEntry.EntryDate.Month <> settingFixedAsset.ProcessDate.Month Then
            Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = "El mes de la fecha de ingreso(" + FixedAssetEntry.EntryDate.ToString("MMMM") + ") debe ser el mismo a la fecha de proceso de parámetros(" + settingFixedAsset.ProcessDate.ToString("MMMM") + ")"}
        End If

        If Not (FixedAssetEntry.AdquisitionType = 1 OrElse FixedAssetEntry.AdquisitionType = 7 OrElse FixedAssetEntry.AdquisitionType = 8 OrElse FixedAssetEntry.AdquisitionType = 9 OrElse FixedAssetEntry.AdquisitionType = 10) Then
            FixedAssetEntry.ValueTax = 0
            FixedAssetEntry.WithholdingTax = 0
            FixedAssetEntry.WithholdingICA = 0
            FixedAssetEntry.RetentionSource = 0
            FixedAssetEntry.RetentionOther = 0
            FixedAssetEntry.DeductionOther = 0
            FixedAssetEntry.IcaPercentage = 0
            FixedAssetEntry.FreightValue = 0
            FixedAssetEntry.FreightIVAPercentage = 0
            FixedAssetEntry.FreightIVAValue = 0

            If FixedAssetEntry.FixedAssetEntryItem IsNot Nothing And FixedAssetEntry.FixedAssetEntryItem.Count > 0 Then
                For Each ObjFixedAssetEntryItem As FixedAssetEntryItem In FixedAssetEntry.FixedAssetEntryItem
                    ObjFixedAssetEntryItem.IvaPercentage = 0
                    ObjFixedAssetEntryItem.IvaValue = 0
                    ObjFixedAssetEntryItem.RTFPercentage = 0
                    ObjFixedAssetEntryItem.RTFValue = 0
                Next
            End If
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                If settingFixedAsset.SettingFixedAssetByLegalBook.Where(Function(d) d.LegalBook.OfficialBook AndAlso d.LegalBook.Status).FirstOrDefault().IvaCost And FixedAssetEntry.TaxRegistration = 2 Then
                    FixedAssetEntry.TaxRegistration = 1 'Iva al costo(Control fiscal)
                End If

                'Se guarda o actualiza el ingreso de activo
                Dim resultEntry As ActionResult(Of FixedAssetEntry) = SaveFixedAssetEntry(FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook, ListDeleteFixedAssetEntryItemDetailPart, ListDeleteFixedAssetEntryItemDetailBook, ListDeleteFixedAssetEntryItemDetail, ListDeleteFixedAssetEntryItem, audit, idSequense)
                If resultEntry.StateResult = False Then
                    Transaction.Dispose()
                    Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = resultEntry.MessageResult(0).ToString}
                End If

                'Se consulta la entidad guardada
                FixedAssetEntry = _FixedAssetEntryRepository.GetFixedAssetEntrySimpleById(resultEntry.ObjectEmbbeded.Id)

                Dim message As New StringBuilder
                message.AppendLine(String.Format("Se confirmó correctamente el Ingreso de Activo {0}", resultEntry.ObjectEmbbeded.Code))

                'Se crea Cuenta por Pagar si el tipo de Adquisición es Compra Directa, leasing financiero o renting financiero
                If FixedAssetEntry.AdquisitionType = 1 OrElse FixedAssetEntry.AdquisitionType = 7 OrElse FixedAssetEntry.AdquisitionType = 9 Then

                    'Se valida que si hay compromisos agregados la sumatoria del valor de los productos que en el catalogo tenga asociado un presupuesto sea igual a la sumatoria de los compromisos
                    If FixedAssetEntry.FixedAssetEntryCommitment IsNot Nothing AndAlso FixedAssetEntry.FixedAssetEntryCommitment.Count > 0 Then
                        If _FixedAssetEntryRepository.ValidateItemsAndCommitments(FixedAssetEntry.FixedAssetEntryItem.ToList(), FixedAssetEntry.FixedAssetEntryCommitment.ToList()) = False Then
                            Transaction.Dispose()
                            Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = "La sumatoria del valor de los productos que en su catálogo tiene asociado un presupuesto no es igual a la sumatoria del valor de los compromisos"}
                        End If
                    End If

                    Dim resultGenerateCxP As ActionResult(Of AccountPayable) = GenerateAccountPayable(FixedAssetEntry, settingFixedAsset, ListVieBot)
                    If resultGenerateCxP.StatusCode = eStatusResult.EXCEPTION OrElse resultGenerateCxP.StatusCode = eStatusResult.WARNING Then
                        Transaction.Dispose()
                        Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = resultGenerateCxP.StatusCode, .Message = resultGenerateCxP.Message}
                    End If

                    'Se asignan los respectivos campos para poder saber desde donde se genero la cxp
                    Dim ListAccountPayable As New List(Of AccountPayable)
                    ListAccountPayable.Add(resultGenerateCxP.ObjectEmbbeded)
                    ListAccountPayable(0).AuxEntityId = FixedAssetEntry.Id
                    ListAccountPayable(0).AuxEntityCode = FixedAssetEntry.Code
                    ListAccountPayable(0).Coments = "CxP generada desde Ingreso de Activos con código " + FixedAssetEntry.Code + " y No. de factura " + ListAccountPayable(0).BillNumber

                    'Se guarda y confirma la cxp
                    Dim resultCxp As ActionResult(Of List(Of String)) = _accountPayableAdminService.SaveListAccountPayable(ListAccountPayable, Nothing, True, audit, SequenseDetailId)
                    If resultCxp.StateResult = False Then
                        Transaction.Dispose()
                        Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = resultCxp.MessageResult(0).ToString}
                    End If

                    'Llenar los datos obligatorios
                    FixedAssetEntry.AccountPayableId = ListAccountPayable(0).Id
                    'Se marca como modificada la entidad
                    FixedAssetEntry.MarkAsModified()
                    'Se actualiza el ingreso con el id de la cxp generada
                    _FixedAssetEntryRepository.SaveEntity(FixedAssetEntry)

                    unitOfWork.Commit()

                    'Agregamos los datos relacionados con la cuenta por pagar
                    message.AppendLine(resultCxp.MessageResult(0).ToString)
                ElseIf Not (FixedAssetEntry.AdquisitionType = 8 OrElse FixedAssetEntry.AdquisitionType = 10) Then
                    'Si NO es Compra directa o leasing financiero pues ya contabilizaron por medio de una cuenta por pagar
                    'Tampoco Comodato Tercerizado ni Renting Operativo puesto que estos Tipo de Adquisición no Contabilizan
                    Dim resultJournalVouchers = CreateJournalVoucher(FixedAssetEntry, settingFixedAsset, audit)
                    If resultJournalVouchers Is Nothing OrElse resultJournalVouchers.StateResult = False Then
                        Transaction.Dispose()
                        Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = "No se pudo generar el comprobante contable del ingreso del activo"}
                    End If

                    Dim JournalVoucher = resultJournalVouchers.ObjectEmbbeded
                    'Contabilizamos en cada uno de los libros habilitados en viebot para Ingreso de Activos Fijos
                    For Each itemVieBot In ListVieBot
                        If itemVieBot.Allow = True Then
                            JournalVoucher.LegalBookId = itemVieBot.LegalBookId
                            Dim ObjActionResultJournal = _AccountingDocumentAdmin.SaveAccountingDocument(JournalVoucher, audit, True)
                            If ObjActionResultJournal.StateResult = True Then
                                Dim journalVoucherType As JournalVoucherTypes = _FixedAssetEntryRepository.GetJournalVoucherType(settingFixedAsset.IdIngressAccountingVoucher)
                                message.AppendLine("Se generó comprobante contable " + journalVoucherType.Name + ": " + ObjActionResultJournal.ObjectEmbbeded.Consecutive.ToString() + " en el libro contable: " + itemVieBot.CodeNameLegalBook)
                            Else
                                Transaction.Dispose()
                                Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.WARNING, .Message = ObjActionResultJournal.Message.ToString()}
                            End If
                        End If
                    Next
                End If

                Transaction.Complete()
                Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = FixedAssetEntry, .Message = message.ToString}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")

                Dim message = ex.Message
                If ex.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.Message) Then
                    message = ex.InnerException.Message
                    If ex.InnerException.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.InnerException.Message) Then
                        message = ex.InnerException.InnerException.Message
                    End If
                End If

                Return New ActionResult(Of FixedAssetEntry) With {.StatusCode = eStatusResult.EXCEPTION, .Message = message}
            End Try
        End Using
    End Function

    Private Function ValidateConfigurationBooksCatalogs(ListFixedAssetEntryItem As List(Of FixedAssetEntryItem)) As String
        'String a devolver
        Dim errors As New StringBuilder

        'Se obtiene los id de los articulos agregados a la rejilla
        Dim ListItemId = From x In ListFixedAssetEntryItem Select x.ItemId Distinct.ToList()

        'Se obtienen los articulos con sus catalogos y los libros ordenando el resultado de forma descendiente
        Dim ListFixedAssetItems = _FixedAssetEntryRepository.GetItemByItemIds(ListItemId)

        'Se obtiene el primer catalogo ya que anteriormente se habia ordenado y el primero contiene el catalogo con mayor numero de libros para poder comparar
        Dim MaximumFixedAssetItemCatalog = (From y In ListFixedAssetItems Select y.FixedAssetItemCatalog).FirstOrDefault()

        If MaximumFixedAssetItemCatalog.FixedAssetItemCatalogAdquisitionType IsNot Nothing AndAlso MaximumFixedAssetItemCatalog.FixedAssetItemCatalogAdquisitionType.Count > 0 Then
            'Se recorren los catalogos para saber si estan de igual manera parametrizados en sus libros
            For Each item In (From x In ListFixedAssetItems Where x.FixedAssetItemCatalog.Id <> MaximumFixedAssetItemCatalog.Id Select x.FixedAssetItemCatalog).ToList
                If item.FixedAssetItemCatalogAdquisitionType Is Nothing OrElse item.FixedAssetItemCatalogAdquisitionType.Count = 0 OrElse
                    item.FixedAssetItemCatalogAdquisitionType.Count <> MaximumFixedAssetItemCatalog.FixedAssetItemCatalogAdquisitionType.Count Then
                    errors.AppendLine("La parametrización de los libros del catálogo " + item.Code + " - " + item.Description + " es diferente al del catálogo " + MaximumFixedAssetItemCatalog.Code + " - " + MaximumFixedAssetItemCatalog.Description)
                End If
            Next
        End If

        'Se retorna el string
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Se genera la cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateAccountPayable(FixedAssetEntry As FixedAssetEntry, SettingFixedAsset As SettingFixedAsset, ListVieBot As List(Of VieBot)) As ActionResult(Of AccountPayable)
        Try
            Dim AccountPayable As New AccountPayable

            'La cuenta por pagar se realiza con la configuración del libro oficial
            Dim SettingFixedAssetLegalBookOfficial = SettingFixedAsset.SettingFixedAssetByLegalBook.Where(Function(d) d.LegalBook.OfficialBook = True AndAlso d.LegalBook.Status = True).FirstOrDefault()

            'Se genera la cabecera de la cxp
            With AccountPayable
                .EntityId = FixedAssetEntry.Id
                .EntityCode = FixedAssetEntry.Code
                .EntityName = FixedAssetEntry.GetType().Name
                .IdSupplier = FixedAssetEntry.SupplierId
                .IdThirdParty = _supplierRepository.GetSupplierById(FixedAssetEntry.SupplierId, False).IdThirdParty
                .IdAccount = _supplierDistributionLineRepository.GetSuppliersDistributionLinesById(FixedAssetEntry.SupplierDistributionLineId).DistributionLines.MainAccounts.Id
                If FixedAssetEntry.CostCenterId IsNot Nothing Then
                    .IdCostCenter = FixedAssetEntry.CostCenterId
                Else
                    .IdCostCenter = Nothing
                End If
                .BillNumber = FixedAssetEntry.InvoiceNumber
                .BillDate = FixedAssetEntry.InvoiceDate
                .DocumentDate = FixedAssetEntry.EntryDate
                .ServicePeriodDate = FixedAssetEntry.EntryDate
                .FilingUnitId = SettingFixedAsset.FilingUnitId
                .Term = FixedAssetEntry.DayPeriod
                .ExpirationDate = PaymentServices.AddDaysDate(FixedAssetEntry.DayPeriod, FixedAssetEntry.InvoiceDate)
                .Coments = "Ingreso de Activos Factura: " + FixedAssetEntry.InvoiceNumber + ", " + FixedAssetEntry.Description
                .Status = 1
                .InitialBalance = False
                .PreviousBudget = False
                .Shares = 1
                .InvoiceValue = FixedAssetEntry.Value - FixedAssetEntry.ValueDiscount + FixedAssetEntry.ValueTax
                .Value = FixedAssetEntry.Value + FixedAssetEntry.ValueTax + FixedAssetEntry.FreightValue + FixedAssetEntry.FreightIVAValue - FixedAssetEntry.ValueDiscount - Utils.RoundValue(FixedAssetEntry.WithholdingTax.Value, Utils.RoundLevel.TwoDecimal) - Utils.RoundValue(FixedAssetEntry.WithholdingICA.Value, Utils.RoundLevel.TwoDecimal) - Utils.RoundValue(FixedAssetEntry.RetentionSource.Value, Utils.RoundLevel.TwoDecimal)
                .Value = Utils.RoundValue(.Value, Utils.RoundLevel.TwoDecimal)
                .Balance = .Value
                .IdOperatingUnit = FixedAssetEntry.OperatingUnitId
                .IdSuppliersDistributionLines = FixedAssetEntry.SupplierDistributionLineId
                .SupplierTypeId = FixedAssetEntry.SupplierTypeId
                .JournalVoucherId = SettingFixedAsset.IdIngressAccountingVoucher
                .CommitmentDetailId = FixedAssetEntry.CommitmentDetailId
                .HandlesDocumentSupport = If(FixedAssetEntry.DocumentSupportId Is Nothing, False, True)
                .DocumentSupportId = FixedAssetEntry.DocumentSupportId
                .CurrencyId = FixedAssetEntry.CurrencyId
                .ChangeProperties = True
                .AuxEntityId = FixedAssetEntry.Id
                .AuxEntityCode = FixedAssetEntry.Code
                .AuxEntityName = FixedAssetEntry.GetType().Name
                .TaxRegistration = FixedAssetEntry.TaxRegistration
                .IdEconomicActivity = FixedAssetEntry.EconomicActivityId

                'Se crean los detalles de la cxp con los articulos del ingreso de activos

                'Proveedor
                Dim supplier As Supplier = _supplierRepository.GetSupplierById(FixedAssetEntry.SupplierId)

                'Catalogo de articulos
                Dim FixedAssetItemCatalog As FixedAssetItemCatalog = Nothing

                'Libro del catalogo que es utilizado para sacar las cuentas cuando el tipo de adquisición sea renting financiero
                Dim FixedAssetItemCatalogAdquisitionType As FixedAssetItemCatalogAdquisitionType = Nothing

                'Diccionario de catalogos de equipos
                Dim dictionaryItemCatalog As New Dictionary(Of Integer, FixedAssetItemCatalog)()

                'Concepto de cxp
                Dim AccountPayableConcepts As AccountPayableConcepts

                'tarifa de IVA
                Dim GeneralLedgerIVA As GeneralLedgerIVA

                'Lista que guarda los conceptos generados para contabilizar el IVA en los libros 
                Dim ListItemsIVA As New List(Of AccountPayableDetailConcept)

                'Diccionario que guarda las tarifas de IVA
                Dim DictionaryIVA As New Dictionary(Of Integer, GeneralLedgerIVA)()

                'Detalle de la cxp
                Dim AccountPayableDetailConcept As AccountPayableDetailConcept = Nothing

                'AccountPayableDetailConcept
                Dim ListAccountPayableDetailConcept As New List(Of AccountPayableDetailConcept)

                'Errores para la validación de renting financiero
                Dim errorsFinancialRenting As New StringBuilder

                'Si es renting financiero se valida que los catalogos de los articulos agregados tengan la misma configuración de libros
                If FixedAssetEntry.AdquisitionType = 9 Then
                    Dim errorsValidateBooks = ValidateConfigurationBooksCatalogs(FixedAssetEntry.FixedAssetEntryItem.ToList.ToList())
                    If errorsValidateBooks.Length > 0 Then
                        Return New ActionResult(Of AccountPayable) With {.StatusCode = eStatusResult.WARNING, .ObjectEmbbeded = Nothing, .Message = errorsValidateBooks}
                    End If
                End If

                For Each entryItem In FixedAssetEntry.FixedAssetEntryItem.ToList

                    'Consulto el articulo por id
                    Dim FixedAssetItem As FixedAssetItem = _FixedAssetEntryRepository.GetFixedAssetItemById(entryItem.ItemId)
                    If Not dictionaryItemCatalog.ContainsKey(FixedAssetItem.ItemCatalogId) Then 'Si el catalogo no existe en el diccionario los consulto y lo agrego
                        FixedAssetItemCatalog = _FixedAssetEntryRepository.GetFixedAssetItemCatalogById(FixedAssetItem.ItemCatalogId)
                        dictionaryItemCatalog.Add(FixedAssetItem.ItemCatalogId, FixedAssetItemCatalog)
                    Else 'Si el catalogo ya existe en el diccionario lo obtengo del diccionario
                        FixedAssetItemCatalog = dictionaryItemCatalog(FixedAssetItem.ItemCatalogId)
                    End If

                    'Se valida si el ingreso es de tipo renting financiero
                    If FixedAssetEntry.AdquisitionType = 9 Then

                        'Se valida que el catalogo tenga parametrizado el libro oficial
                        FixedAssetItemCatalogAdquisitionType = _FixedAssetEntryRepository.GetOfficialBookInCatalogBooks(FixedAssetItemCatalog.Id)
                        If FixedAssetItemCatalogAdquisitionType Is Nothing Then
                            errorsFinancialRenting.AppendLine("El catálogo " + FixedAssetItemCatalog.Code + " - " + FixedAssetItemCatalog.Description + " no tiene parametrizado el libro oficial")
                            Continue For
                        End If

                    End If

                    'Control para el desbalanceo
                    Dim countItems = entryItem.Quantity

                    If entryItem.SubTotalValue > 0 Then 'Si el valor del subtotal del articulo es mayor a cero

                        'Valor usado para ajustar el valor de los detalles de manera que coincidan con el subtotal y el iva de la cabecera
                        Dim SubtotalWithoutIva As Decimal = entryItem.SubTotalValue - entryItem.DiscountValue
                        Dim SubtotalIva As Decimal = entryItem.IvaValue

                        'Se saca los valores para asignarselos a cada item generado dependiendo si lleva o no el iva al costo
                        Dim BaseValueWithoutIva As Decimal = Utils.RoundValue((SubtotalWithoutIva / countItems), Utils.RoundLevel.TwoDecimal)
                        Dim BaseIvaValue As Decimal = entryItem.IvaValue / countItems

                        'Diccionario que guarda los conceptos dependiendo de la clase de localizacion
                        Dim dictionaryLocation As New Dictionary(Of Integer, AccountPayableDetailConcept)()

                        'Se recorre los detalles del item
                        Dim cont = 0
                        For Each entryItemDetail In entryItem.FixedAssetEntryItemDetail
                            cont = cont + 1
                            'Se consulta la localizacion
                            Dim Location = _FixedAssetEntryRepository.GetFixedAssetLocationById(entryItemDetail.LocationId)
                            Dim idCostCenter As Integer? = Location?.FunctionalUnit?.CostCenterId

                            If Not dictionaryLocation.ContainsKey(Location.Class) Then 'Si no esta en el diccionario se crea uno nuevo

                                AccountPayableDetailConcept = New AccountPayableDetailConcept
                                AccountPayableDetailConcept.IdConceptAccountPayable = FixedAssetItemCatalog.IncomeAccountPayableConceptId

                                If FixedAssetEntry.AdquisitionType = 7 Then 'Si el tipo de adquisicion es de leasing financiero
                                    AccountPayableDetailConcept.IdAccount = FixedAssetItemCatalog.IncomeLeasingAccountId
                                ElseIf FixedAssetEntry.AdquisitionType = 9 Then 'Si el tipo de adquisición es de renting financiero
                                    AccountPayableDetailConcept.IdAccount = FixedAssetItemCatalogAdquisitionType.MainAccountId
                                Else 'Cuando es diferente a leasing financiero
                                    Select Case Location.Class
                                        Case 1, 2 'Administrativo, Operativo
                                            AccountPayableDetailConcept.IdAccount = FixedAssetItemCatalog.IncomeAccountId
                                        Case 3 'Mantenimiento
                                            AccountPayableDetailConcept.IdAccount = FixedAssetItemCatalog.MaintenanceAssetsMainAccountId
                                        Case 4 'Almacén y Bodega
                                            AccountPayableDetailConcept.IdAccount = FixedAssetItemCatalog.WarehouseAssetsMainAccountId
                                    End Select
                                End If

                                AccountPayableDetailConcept.IdCostCenter = If(_pucAdminService.MainAccountHandlesCostCenter(AccountPayableDetailConcept.IdAccount), idCostCenter, Nothing)
                                AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                                AccountPayableDetailConcept.Nature = 1 'Debito
                                AccountPayableDetailConcept.BaseValue = BaseValueWithoutIva
                                AccountPayableDetailConcept.BillingValue = AccountPayableDetailConcept.BaseValue
                                AccountPayableDetailConcept.Value = AccountPayableDetailConcept.BaseValue
                                AccountPayableDetailConcept.DeferredCausation = False
                                AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Ingreso de Activos 'Valor Bruto Articulo'"
                                AccountPayableDetailConcept.TotalConcept = BaseValueWithoutIva + BaseIvaValue

                                If BaseIvaValue > 0 Then
                                    If Not DictionaryIVA.ContainsKey(entryItem.IVAId) Then
                                        GeneralLedgerIVA = _generalLedgerIVARepository.GetGeneralLedgerIVAById(entryItem.IVAId)

                                        If Not GeneralLedgerIVA?.IdAccountPurchaseService.HasValue OrElse
                                           Not GeneralLedgerIVA?.IdAccountDebitControlFiscal.HasValue OrElse
                                           Not GeneralLedgerIVA?.IdAccountCreditControlFiscal.HasValue Then
                                            Throw New IndigoValidationException($"La tarifa de IVA ({GeneralLedgerIVA.Code} - {GeneralLedgerIVA.Name}) no tiene una o más cuentas parametrizadas necesarias")
                                        End If

                                        DictionaryIVA.Add(entryItem.IVAId, GeneralLedgerIVA)

                                    Else
                                        GeneralLedgerIVA = DictionaryIVA(entryItem.IVAId)
                                    End If

                                    AccountPayableDetailConcept.RateIva = GeneralLedgerIVA?.Id
                                    AccountPayableDetailConcept.IvaValue = BaseIvaValue
                                End If

                                dictionaryLocation.Add(Location.Class, AccountPayableDetailConcept)

                                'Si el tipo de adquisicion es renting financiero se agrega la configuracion de los libros del catalogo al diccionario
                                If FixedAssetEntry.AdquisitionType = 9 AndAlso FixedAssetItemCatalog.FixedAssetItemCatalogAdquisitionType IsNot Nothing AndAlso FixedAssetItemCatalog.FixedAssetItemCatalogAdquisitionType.Count > 0 Then
                                    For Each item In FixedAssetItemCatalog.FixedAssetItemCatalogAdquisitionType.ToList
                                        If item.LegalBook.OfficialBook = False Then
                                            AccountPayableDetailConcept.DictionaryConfigurationBooks.Add(item.LegalBookId, item.MainAccountId)
                                        End If
                                    Next
                                End If

                            Else 'Si esta en el diccionario se actualizan los campos
                                AccountPayableDetailConcept = dictionaryLocation(Location.Class)
                                AccountPayableDetailConcept.BaseValue += BaseValueWithoutIva
                                AccountPayableDetailConcept.BillingValue = AccountPayableDetailConcept.BaseValue
                                AccountPayableDetailConcept.Value = AccountPayableDetailConcept.BaseValue
                                AccountPayableDetailConcept.IvaValue += BaseIvaValue
                            End If

                            If SubtotalIva > 0 AndAlso (cont = countItems) Then

                                Dim oldIdAccount = AccountPayableDetailConcept.IdAccount
                                AccountPayableDetailConcept = New AccountPayableDetailConcept
                                If .TaxRegistration = 1 Then
                                    AccountPayableDetailConcept.IdAccount = GeneralLedgerIVA.IdAccountDebitControlFiscal
                                ElseIf .TaxRegistration = 2 Then
                                    AccountPayableDetailConcept.IdAccount = GeneralLedgerIVA.IdAccountPurchaseService
                                ElseIf .TaxRegistration = 4 Then
                                    AccountPayableDetailConcept.IdAccount = oldIdAccount
                                End If

                                AccountPayableDetailConcept.IdCostCenter = If(_pucAdminService.MainAccountHandlesCostCenter(AccountPayableDetailConcept.IdAccount), idCostCenter, Nothing)
                                AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                                AccountPayableDetailConcept.Nature = 1
                                AccountPayableDetailConcept.BaseValue = SubtotalIva
                                AccountPayableDetailConcept.BillingValue = AccountPayableDetailConcept.BaseValue
                                AccountPayableDetailConcept.Value = AccountPayableDetailConcept.BaseValue
                                AccountPayableDetailConcept.DeferredCausation = False
                                AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Ingreso de Activos 'IVA Articulo'"

                                ListItemsIVA.Add(AccountPayableDetailConcept)

                                If .TaxRegistration = 1 Then
                                    AccountPayableDetailConcept = New AccountPayableDetailConcept
                                    AccountPayableDetailConcept.IdAccount = GeneralLedgerIVA.IdAccountCreditControlFiscal

                                    AccountPayableDetailConcept.IdCostCenter = If(_pucAdminService.MainAccountHandlesCostCenter(AccountPayableDetailConcept.IdAccount), idCostCenter, Nothing)
                                    AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                                    AccountPayableDetailConcept.Nature = 2
                                    AccountPayableDetailConcept.BaseValue = SubtotalIva
                                    AccountPayableDetailConcept.BillingValue = AccountPayableDetailConcept.BaseValue
                                    AccountPayableDetailConcept.Value = AccountPayableDetailConcept.BaseValue
                                    AccountPayableDetailConcept.DeferredCausation = False
                                    AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Ingreso de Activos 'IVA Articulo'"

                                    ListItemsIVA.Add(AccountPayableDetailConcept)
                                End If
                            End If
                        Next

                        For Each item In dictionaryLocation
                            item.Value.BaseValue = Utils.RoundValue(item.Value.BaseValue, Utils.RoundLevel.TwoDecimal)
                            item.Value.BillingValue = Utils.RoundValue(CDec(item.Value.BillingValue), Utils.RoundLevel.TwoDecimal)
                            item.Value.Value = Utils.RoundValue(item.Value.Value, Utils.RoundLevel.TwoDecimal)

                            item.Value.TotalConcept = item.Value.Value + item.Value.IvaValue

                            ListAccountPayableDetailConcept.Add(item.Value)
                            .AccountPayableDetailConcept.Add(item.Value)
                        Next
                    End If

                    '-----------------------------------------Separador-----------------------------------------------------

                    If Utils.RoundValue(entryItem.RTFValue, Utils.RoundLevel.Unit) > 0 Then 'Si el articulo tiene retefuente
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
                            AccountPayableDetailConcept.BaseValue += entryItem.SubTotalValue
                            AccountPayableDetailConcept.BillingValue += entryItem.SubTotalValue
                            AccountPayableDetailConcept.Value += entryItem.RTFValue
                        Else
                            AccountPayableDetailConcept = New AccountPayableDetailConcept
                            AccountPayableDetailConcept.IdConceptAccountPayable = AccountPayableConcepts.Id
                            AccountPayableDetailConcept.IdAccount = AccountPayableConcepts.IdAccount
                            AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                            AccountPayableDetailConcept.Nature = 2 'Credito
                            AccountPayableDetailConcept.BaseValue = entryItem.SubTotalValue
                            AccountPayableDetailConcept.BillingValue = entryItem.SubTotalValue
                            AccountPayableDetailConcept.Value = entryItem.RTFValue
                            AccountPayableDetailConcept.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                            AccountPayableDetailConcept.Percentage = entryItem.RTFPercentage
                            AccountPayableDetailConcept.DeferredCausation = False
                            AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Ingreso de Activos 'RTF Articulo'"
                            .AccountPayableDetailConcept.Add(AccountPayableDetailConcept)

                            ListAccountPayableDetailConcept.Add(AccountPayableDetailConcept)
                        End If
                    End If
                Next

                'Si hay errores en renting financiero
                If errorsFinancialRenting.Length > 0 Then
                    Return New ActionResult(Of AccountPayable) With {.StatusCode = eStatusResult.WARNING, .ObjectEmbbeded = Nothing, .Message = errorsFinancialRenting.ToString()}
                End If

                'Se redondean los valores, a dos decimales
                .AccountPayableDetailConcept.ToList.ForEach(Sub(item) item.Value = Utils.RoundValue(item.Value, Utils.RoundLevel.TwoDecimal))
                ListAccountPayableDetailConcept.ToList.ForEach(Sub(item) item.Value = Utils.RoundValue(item.Value, Utils.RoundLevel.TwoDecimal))

                If FixedAssetEntry.WithholdingTax > 0 Then 'Si tiene retencion del iva
                    AccountPayableDetailConcept = New AccountPayableDetailConcept
                    If SettingFixedAsset.IVARetention = 1 Then 'Se saca la retencion del iva del tercero
                        AccountPayableConcepts = _FixedAssetEntryRepository.GetAccountPayableConceptBySupplierDistributionLineId(FixedAssetEntry.SupplierDistributionLineId)
                        AccountPayableDetailConcept.IdConceptAccountPayable = AccountPayableConcepts.Id
                        AccountPayableDetailConcept.IdAccount = AccountPayableConcepts.IdAccount
                        AccountPayableDetailConcept.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                        AccountPayableDetailConcept.Percentage = AccountPayableConcepts.RetentionConcepts.Rate
                    Else 'Se saca la retencion del iva del concepto
                        AccountPayableConcepts = _paymentsConceptRepository.GetPaymentConceptByIdSimple(SettingFixedAsset.IVARetentionAccountPayableConceptId)
                        AccountPayableDetailConcept.IdConceptAccountPayable = SettingFixedAsset.IVARetentionAccountPayableConceptId
                        AccountPayableDetailConcept.IdAccount = AccountPayableConcepts.IdAccount
                        AccountPayableDetailConcept.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                        AccountPayableDetailConcept.Percentage = AccountPayableConcepts.RetentionConcepts.Rate
                    End If
                    AccountPayableDetailConcept.IvaValue = 0
                    AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                    AccountPayableDetailConcept.Nature = 2 'Credito
                    AccountPayableDetailConcept.BaseValue = FixedAssetEntry.ValueTax
                    AccountPayableDetailConcept.BillingValue = FixedAssetEntry.Value
                    AccountPayableDetailConcept.Value = Math.Round(FixedAssetEntry.WithholdingTax.Value, 0, MidpointRounding.AwayFromZero)
                    AccountPayableDetailConcept.DeferredCausation = False
                    AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Ingreso de Activos 'Retención IVA'"
                    .AccountPayableDetailConcept.Add(AccountPayableDetailConcept)

                    ListAccountPayableDetailConcept.Add(AccountPayableDetailConcept)
                End If

                If FixedAssetEntry.WithholdingICA > 0 Then 'Si tiene retencion ica
                    AccountPayableDetailConcept = New AccountPayableDetailConcept
                    AccountPayableConcepts = _paymentsConceptRepository.GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitIdPay(FixedAssetEntry.SupplierDistributionLineId, FixedAssetEntry.OperatingUnitId)
                    AccountPayableDetailConcept.IdConceptAccountPayable = AccountPayableConcepts.Id
                    AccountPayableDetailConcept.IdAccount = AccountPayableConcepts.IdAccount
                    AccountPayableDetailConcept.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                    AccountPayableDetailConcept.IvaValue = 0
                    AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                    AccountPayableDetailConcept.Nature = 2 'Credito
                    AccountPayableDetailConcept.BaseValue = Utils.RoundValue(FixedAssetEntry.Value.Value, Utils.RoundLevel.TwoDecimal)
                    AccountPayableDetailConcept.BillingValue = Utils.RoundValue(FixedAssetEntry.Value.Value, Utils.RoundLevel.TwoDecimal)
                    AccountPayableDetailConcept.Value = Utils.RoundValue(FixedAssetEntry.WithholdingICA.Value, Utils.RoundLevel.TwoDecimal)
                    AccountPayableDetailConcept.Percentage = AccountPayableConcepts.RetentionConcepts.Rate
                    AccountPayableDetailConcept.DeferredCausation = False
                    AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Ingreso de Activos 'Retención ICA'"
                    .AccountPayableDetailConcept.Add(AccountPayableDetailConcept)

                    ListAccountPayableDetailConcept.Add(AccountPayableDetailConcept)
                End If

                If FixedAssetEntry.FreightValue > 0 Then 'Si tiene flete
                    AccountPayableDetailConcept = New AccountPayableDetailConcept
                    AccountPayableConcepts = _paymentsConceptRepository.GetPaymentConceptByIdSimple(SettingFixedAsset.FreightAccountPayableConceptId)
                    AccountPayableDetailConcept.IdConceptAccountPayable = SettingFixedAsset.FreightAccountPayableConceptId
                    AccountPayableDetailConcept.IdAccount = AccountPayableConcepts.IdAccount
                    AccountPayableDetailConcept.IdRetentionConcept = Nothing
                    AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                    AccountPayableDetailConcept.Nature = 1 'Debito
                    AccountPayableDetailConcept.BaseValue = FixedAssetEntry.FreightValue
                    AccountPayableDetailConcept.BillingValue = FixedAssetEntry.FreightValue
                    AccountPayableDetailConcept.Value = Math.Round(FixedAssetEntry.FreightValue, 2, MidpointRounding.AwayFromZero)
                    AccountPayableDetailConcept.Percentage = Nothing
                    AccountPayableDetailConcept.DeferredCausation = False
                    AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Ingreso de Activos 'Flete'"
                    .AccountPayableDetailConcept.Add(AccountPayableDetailConcept)

                    ListAccountPayableDetailConcept.Add(AccountPayableDetailConcept)

                    If FixedAssetEntry.FreightIVAValue > 0 Then 'Si el flete tiene iva
                        AccountPayableDetailConcept = New AccountPayableDetailConcept
                        AccountPayableConcepts = _paymentsConceptRepository.GetPaymentConceptByIdSimple(SettingFixedAsset.IVAFreightAccountPayableConceptId)
                        AccountPayableDetailConcept.IdConceptAccountPayable = SettingFixedAsset.IVAFreightAccountPayableConceptId
                        AccountPayableDetailConcept.IdAccount = AccountPayableConcepts.IdAccount
                        AccountPayableDetailConcept.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                        AccountPayableDetailConcept.IdThirdParty = .IdThirdParty
                        AccountPayableDetailConcept.Nature = 1 'Debito
                        AccountPayableDetailConcept.BaseValue = FixedAssetEntry.FreightIVAValue
                        AccountPayableDetailConcept.BillingValue = FixedAssetEntry.FreightIVAValue
                        AccountPayableDetailConcept.Value = Math.Round(FixedAssetEntry.FreightIVAValue.Value, 2, MidpointRounding.AwayFromZero)
                        AccountPayableDetailConcept.Percentage = FixedAssetEntry.FreightIVAPercentage
                        AccountPayableDetailConcept.DeferredCausation = False
                        AccountPayableDetailConcept.Detail = "Detalle de cuenta por pagar generada por Ingreso de Activos 'IVA Flete'"
                        .AccountPayableDetailConcept.Add(AccountPayableDetailConcept)

                        ListAccountPayableDetailConcept.Add(AccountPayableDetailConcept)
                    End If
                End If

                For Each itemVieBot In ListVieBot
                    If itemVieBot.LegalBookId <> SettingFixedAssetLegalBookOfficial.LegalBookId Then
                        If itemVieBot.Allow Then
                            'Variable para saber si contabilizo
                            Dim contabilization As Boolean = True

                            'Si el tipo de adquisición es renting financiero valido si esta parametrizado el libro que voy recorriendo en los detalles de los conceptos
                            If FixedAssetEntry.AdquisitionType = 9 Then
                                contabilization = False
                                If ListAccountPayableDetailConcept.Exists(Function(d) d.DictionaryConfigurationBooks IsNot Nothing AndAlso d.DictionaryConfigurationBooks.Any(Function(c) c.Key = itemVieBot.LegalBookId)) Then
                                    contabilization = True
                                End If
                            End If

                            Dim AccountPayableDetailConceptOthersNotHomologatedBooks As List(Of AccountPayableDetailConcept) = Nothing

                            If contabilization Then
                                If Not .AccountPayableDetailConceptOthersNotHomologatedBooks.ContainsKey(itemVieBot.LegalBookId) Then
                                    .AccountPayableDetailConceptOthersNotHomologatedBooks.Add(itemVieBot.LegalBookId, New List(Of AccountPayableDetailConcept))
                                End If
                                AccountPayableDetailConceptOthersNotHomologatedBooks = .AccountPayableDetailConceptOthersNotHomologatedBooks(itemVieBot.LegalBookId)
                            End If

                            'AccountPayableDetailConceptHomologation
                            Dim ListHomologationAccountPayableDetailConcept As List(Of AccountPayableDetailConcept) = ListAccountPayableDetailConcept.Select(Function(item) item.Clone()).ToList()
                            If contabilization Then
                                For Each item In ListHomologationAccountPayableDetailConcept

                                    'Si el detalle tiene agregado el diccionario de configuración de libros
                                    If FixedAssetEntry.AdquisitionType = 9 AndAlso item.DictionaryConfigurationBooks IsNot Nothing AndAlso item.DictionaryConfigurationBooks.Count > 0 Then
                                        'Si el libro que se esta recorriendo esta en el diccionario
                                        If Not item.DictionaryConfigurationBooks.ContainsKey(itemVieBot.LegalBookId) Then
                                            Continue For
                                        End If
                                        'Se obtiene la cuenta contable que esta en el diccionario por libro
                                        Dim MainAccountId As Integer = item.DictionaryConfigurationBooks(itemVieBot.LegalBookId)
                                        'Se cambia la configuración del detalle de la cxp
                                        item.IdAccount = MainAccountId
                                    End If

                                    If .TaxRegistration = 1 Then
                                        item.Value = item.Value + If(item.IvaValue, 0)
                                    End If

                                    AccountPayableDetailConceptOthersNotHomologatedBooks.Add(item)
                                Next

                                For Each itemIVA In ListItemsIVA
                                    AccountPayableDetailConceptOthersNotHomologatedBooks.Add(itemIVA)
                                Next

                            End If
                        End If
                    End If
                Next

                'Si hay detalles de compromisos los genero en la cxp
                If FixedAssetEntry.FixedAssetEntryCommitment IsNot Nothing AndAlso FixedAssetEntry.FixedAssetEntryCommitment.Count > 0 Then
                    For Each item In FixedAssetEntry.FixedAssetEntryCommitment
                        Dim apc As New AccountPayableCommitments()
                        apc.CommitmentDetailId = item.CommitmentDetailId
                        apc.Value = item.Value
                        .AccountPayableCommitments.Add(apc)
                    Next
                End If

                'Se crea la cuota de a cxp
                Dim AccountPayableShares As New AccountPayableShares
                AccountPayableShares.Share = 1
                AccountPayableShares.DateExpires = .ExpirationDate
                AccountPayableShares.InitialValue = Utils.RoundValue(.Value, Utils.RoundLevel.TwoDecimal)
                AccountPayableShares.Balance = Utils.RoundValue(.Value, Utils.RoundLevel.TwoDecimal)
                .AccountPayableShares.Add(AccountPayableShares)
            End With

            Return New ActionResult(Of AccountPayable) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = AccountPayable}
        Catch ex As IndigoValidationException
            Return New ActionResult(Of AccountPayable) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un ingreso por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetEntry(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetEntry) Implements IFixedAssetEntryAdminService.GetFixedAssetEntry
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetEntry As FixedAssetEntry = Me._FixedAssetEntryRepository.GetFixedAssetEntry(code.Trim())
            If FixedAssetEntry IsNot Nothing AndAlso FixedAssetEntry.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetEntry)(FixedAssetEntry, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetEntry) With {.StateResult = True, .ObjectEmbbeded = FixedAssetEntry}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetEntry) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un ingreso por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetEntryById(Id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetEntry) Implements IFixedAssetEntryAdminService.GetFixedAssetEntryById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetEntry As FixedAssetEntry = Me._FixedAssetEntryRepository.GetFixedAssetEntryById(Id)
            If FixedAssetEntry IsNot Nothing AndAlso FixedAssetEntry.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetEntry)(FixedAssetEntry, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetEntry) With {.StateResult = True, .ObjectEmbbeded = FixedAssetEntry}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetEntry) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un ingreso
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetEntry(FixedAssetEntry As FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook As List(Of FixedAssetEntryItemDetailPartBook), ListDeleteFixedAssetEntryItemDetailPart As List(Of FixedAssetEntryItemDetailPart), ListDeleteFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook), ListDeleteFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail), ListDeleteFixedAssetEntryItem As List(Of FixedAssetEntryItem), audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetEntry) Implements IFixedAssetEntryAdminService.SaveFixedAssetEntry
        If FixedAssetEntry Is Nothing Then
            Throw New ArgumentNullException("FixedAssetEntry")
        End If
        Dim unitOfWork As IUnitWork = Me._FixedAssetEntryRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                Dim ListString As List(Of String) = ConvertToXmlListDeletes(ListDeleteFixedAssetEntryItemDetailPartBook, ListDeleteFixedAssetEntryItemDetailPart, ListDeleteFixedAssetEntryItemDetailBook,
                                                                            ListDeleteFixedAssetEntryItemDetail, ListDeleteFixedAssetEntryItem, FixedAssetEntry.Status)
                Dim EntityXml As String = ConvertToXmlFixedAssetEntry(FixedAssetEntry)
                Dim resultStore = _FixedAssetEntryRepository.SP_SaveFixedAssetEntry(EntityXml, ListString, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of FixedAssetEntry) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList}
                End If

                FixedAssetEntry.Id = resultStore.Id
                FixedAssetEntry.Code = resultStore.Code
                FixedAssetEntry.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of FixedAssetEntry) With {.StateResult = True, .ObjectEmbbeded = FixedAssetEntry}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of FixedAssetEntry) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetEntry) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Convierte la entidad principal en xml
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXmlFixedAssetEntry(FixedAssetEntry As FixedAssetEntry) As String
        Dim builder As StringBuilder = New StringBuilder()

        Dim contTempId As Integer = 1
        Dim contTempId2 As Integer = 1
        Dim contTempId3 As Integer = 1

        'FixedAssetInitialBalance
        builder.Append("<FixedAssetEntry>")


        builder.Append("<Id>" & FixedAssetEntry.Id & "</Id>")
        builder.Append("<Code>" & FixedAssetEntry.Code & "</Code>")
        builder.Append("<EntryDate>" & FixedAssetEntry.EntryDate.ToString("dd/MM/yyyy HH:mm") & "</EntryDate>")
        builder.Append("<EntryNumber>" & FixedAssetEntry.EntryNumber & "</EntryNumber>")
        builder.Append("<AdquisitionType>" & FixedAssetEntry.AdquisitionType & "</AdquisitionType>")
        builder.Append("<TaxRegistration>" & FixedAssetEntry.TaxRegistration & "</TaxRegistration>")
        If FixedAssetEntry.NumberContractLeasing IsNot Nothing Then
            builder.Append("<NumberContractLeasing>" & FixedAssetEntry.NumberContractLeasing & "</NumberContractLeasing>")
        Else
            builder.Append("<NumberContractLeasing>" & "---" & "</NumberContractLeasing>")
        End If
        builder.Append("<InitialDateLeasing>" & FixedAssetEntry.InitialDateLeasing & "</InitialDateLeasing>")
        builder.Append("<EndDateLeasing>" & FixedAssetEntry.EndDateLeasing & "</EndDateLeasing>")
        builder.Append("<SupplierId>" & FixedAssetEntry.SupplierId & "</SupplierId>")
        builder.Append("<SupplierDistributionLineId>" & FixedAssetEntry.SupplierDistributionLineId & "</SupplierDistributionLineId>")
        builder.Append("<SupplierTypeId>" & FixedAssetEntry.SupplierTypeId & "</SupplierTypeId>")
		builder.Append("<Description>" & System.Security.SecurityElement.Escape(FixedAssetEntry.Description) & "</Description>")
		builder.Append("<GetLocationResponsible>" & FixedAssetEntry.GetLocationResponsible & "</GetLocationResponsible>")

        If FixedAssetEntry.LocationId IsNot Nothing Then
            builder.Append("<LocationId>" & FixedAssetEntry.LocationId & "</LocationId>")
            builder.Append("<ResponsibleId>" & FixedAssetEntry.ResponsibleId & "</ResponsibleId>")
        Else
            builder.Append("<LocationId>" & 0 & "</LocationId>")
            builder.Append("<ResponsibleId>" & 0 & "</ResponsibleId>")
        End If


        builder.Append("<RoundService>" & FixedAssetEntry.RoundService & "</RoundService>")
        builder.Append("<InvoiceNumber>" & FixedAssetEntry.InvoiceNumber & "</InvoiceNumber>")
        builder.Append("<InvoiceDate>" & FixedAssetEntry.InvoiceDate.ToString("dd/MM/yyyy") & "</InvoiceDate>")
        builder.Append("<DayPeriod>" & FixedAssetEntry.DayPeriod & "</DayPeriod>")
        builder.Append("<IcaPercentage>" & FixedAssetEntry.IcaPercentage.ToString().Replace(",", ".") & "</IcaPercentage>")
        builder.Append("<FreightValue>" & FixedAssetEntry.FreightValue.ToString().Replace(",", ".") & "</FreightValue>")
        builder.Append("<FreightIVAPercentage>" & FixedAssetEntry.FreightIVAPercentage.ToString().Replace(",", ".") & "</FreightIVAPercentage>")
        builder.Append("<FreightIVAValue>" & FixedAssetEntry.FreightIVAValue.ToString().Replace(",", ".") & "</FreightIVAValue>")
        builder.Append("<Value>" & FixedAssetEntry.Value.ToString().Replace(",", ".") & "</Value>")
        builder.Append("<ValueDiscount>" & FixedAssetEntry.ValueDiscount.ToString().Replace(",", ".") & "</ValueDiscount>")
        builder.Append("<ValueTax>" & FixedAssetEntry.ValueTax.ToString().Replace(",", ".") & "</ValueTax>")
        builder.Append("<WithholdingTax>" & FixedAssetEntry.WithholdingTax.ToString().Replace(",", ".") & "</WithholdingTax>")
        builder.Append("<WithholdingICA>" & FixedAssetEntry.WithholdingICA.ToString().Replace(",", ".") & "</WithholdingICA>")
        builder.Append("<RetentionSource>" & FixedAssetEntry.RetentionSource.ToString().Replace(",", ".") & "</RetentionSource>")
        builder.Append("<RetentionOther>" & FixedAssetEntry.RetentionOther.ToString().Replace(",", ".") & "</RetentionOther>")
        builder.Append("<DeductionOther>" & FixedAssetEntry.DeductionOther.ToString().Replace(",", ".") & "</DeductionOther>")
        builder.Append("<TotalValue>" & FixedAssetEntry.TotalValue.ToString().Replace(",", ".") & "</TotalValue>")
        builder.Append("<Status>" & FixedAssetEntry.Status & "</Status>")
        builder.Append("<EconomicActivityId>" & FixedAssetEntry.EconomicActivityId & "</EconomicActivityId>")

        If FixedAssetEntry.CommitmentDetailId IsNot Nothing Then
            builder.Append("<CommitmentDetailId>" & FixedAssetEntry.CommitmentDetailId & "</CommitmentDetailId>")
        End If
        If FixedAssetEntry.DocumentSupportId IsNot Nothing Then
            builder.Append("<DocumentSupportId>" & FixedAssetEntry.DocumentSupportId & "</DocumentSupportId>")
        End If
        If FixedAssetEntry.AccountPayableId IsNot Nothing Then
            builder.Append("<AccountPayableId>" & FixedAssetEntry.AccountPayableId & "</AccountPayableId>")
        End If
        If FixedAssetEntry.CostCenterId IsNot Nothing Then
            builder.Append("<CostCenterId>" & FixedAssetEntry.CostCenterId & "</CostCenterId>")
        End If
        builder.Append("<OperatingUnitId>" & FixedAssetEntry.OperatingUnitId & "</OperatingUnitId>")

        builder.Append("<CurrencyId>" & FixedAssetEntry.CurrencyId & "</CurrencyId>")

        If FixedAssetEntry.FixedAssetEntryCommitment IsNot Nothing AndAlso FixedAssetEntry.FixedAssetEntryCommitment.Count > 0 Then
            For Each entryCommitment In FixedAssetEntry.FixedAssetEntryCommitment
                builder.Append("<FixedAssetEntryCommitment>")
                builder.Append("<Id>" & entryCommitment.Id & "</Id>")
                builder.Append("<FixedAssetEntryId>" & entryCommitment.FixedAssetEntryId & "</FixedAssetEntryId>")
                builder.Append("<CommitmentDetailId>" & entryCommitment.CommitmentDetailId & "</CommitmentDetailId>")
                builder.Append("<Value>" & entryCommitment.Value & "</Value>")
                If entryCommitment.ChangeTracker.State <> ObjectState.Deleted Then
                    builder.Append("<IsDelete>" & 0 & "</IsDelete>")
                Else
                    builder.Append("<IsDelete>" & 1 & "</IsDelete>")
                End If
                builder.Append("</FixedAssetEntryCommitment>")
            Next
        End If

        'FixedAssetEntryItem
        For Each entryItem In FixedAssetEntry.FixedAssetEntryItem

            builder.Append("<FixedAssetEntryItem>")

            builder.Append("<Id>" & entryItem.Id & "</Id>")
            builder.Append("<FixedAssetEntryId>" & entryItem.FixedAssetEntryId & "</FixedAssetEntryId>")
            builder.Append("<RemissionSource>" & entryItem.RemissionSource & "</RemissionSource>")

            If entryItem.SourceCode IsNot Nothing Then
                builder.Append("<SourceCode>" & entryItem.SourceCode & "</SourceCode>")
            Else
                builder.Append("<SourceCode>" & 0 & "</SourceCode>")
            End If

            If entryItem.PurchaseOrderItemId IsNot Nothing Then
                builder.Append("<PurchaseOrderItemId>" & entryItem.PurchaseOrderItemId & "</PurchaseOrderItemId>")
            Else
                builder.Append("<PurchaseOrderItemId>" & 0 & "</PurchaseOrderItemId>")
            End If

            If entryItem.RemissionEntranceItemId IsNot Nothing Then
                builder.Append("<RemissionEntranceItemId>" & entryItem.RemissionEntranceItemId & "</RemissionEntranceItemId>")
            Else
                builder.Append("<RemissionEntranceItemId>" & 0 & "</RemissionEntranceItemId>")
            End If

            builder.Append("<ItemId>" & entryItem.ItemId & "</ItemId>")
            If entryItem.IVAId IsNot Nothing Then
                builder.Append("<IVAId>" & entryItem.IVAId & "</IVAId>")
            Else
                builder.Append("<IVAId>" & 0 & "</IVAId>")
            End If
            builder.Append("<TrademarkId>" & entryItem.TrademarkId & "</TrademarkId>")
            builder.Append("<Model>" & entryItem.Model & "</Model>")
            builder.Append("<PolicyId>" & entryItem.PolicyId & "</PolicyId>")
            builder.Append("<Quantity>" & entryItem.Quantity & "</Quantity>")
            builder.Append("<OutstandingQuantity>" & entryItem.OutstandingQuantity & "</OutstandingQuantity>")
            builder.Append("<UnitValue>" & entryItem.UnitValue.ToString().Replace(",", ".") & "</UnitValue>")
            builder.Append("<SubTotalValue>" & entryItem.SubTotalValue.ToString().Replace(",", ".") & "</SubTotalValue>")
            builder.Append("<IvaPercentage>" & entryItem.IvaPercentage.ToString().Replace(",", ".") & "</IvaPercentage>")
            builder.Append("<IvaValue>" & entryItem.IvaValue.ToString().Replace(",", ".") & "</IvaValue>")
            builder.Append("<DiscountPercentage>" & entryItem.DiscountPercentage.ToString().Replace(",", ".") & "</DiscountPercentage>")
            builder.Append("<DiscountValue>" & entryItem.DiscountValue.ToString().Replace(",", ".") & "</DiscountValue>")
            builder.Append("<TotalValue>" & entryItem.TotalValue.ToString().Replace(",", ".") & "</TotalValue>")
            builder.Append("<RTFPercentage>" & entryItem.RTFPercentage.ToString().Replace(",", ".") & "</RTFPercentage>")
            builder.Append("<RTFValue>" & entryItem.RTFValue.ToString().Replace(",", ".") & "</RTFValue>")
            builder.Append("<Observation>" & entryItem.Observation & "</Observation>")
            builder.Append("<TempId>" & contTempId & "</TempId>")

            'FixedAssetEntryItemDetail
            If entryItem.FixedAssetEntryItemDetail IsNot Nothing AndAlso entryItem.FixedAssetEntryItemDetail.Count > 0 Then
                For Each entryItemDetail In entryItem.FixedAssetEntryItemDetail

                    builder.Append("<FixedAssetEntryItemDetail>")

                    builder.Append("<Id>" & entryItemDetail.Id & "</Id>")
                    builder.Append("<FixedAssetEntryItemId>" & entryItemDetail.FixedAssetEntryItemId & "</FixedAssetEntryItemId>")
                    'builder.Append("<ItemId>" & entryItemDetail.ItemId & "</ItemId>")
                    builder.Append("<Plate>" & entryItemDetail.Plate & "</Plate>")
                    builder.Append("<Serie>" & entryItemDetail.Serie & "</Serie>")
                    builder.Append("<ReponsibleId>" & entryItemDetail.ReponsibleId & "</ReponsibleId>")
                    builder.Append("<LocationId>" & entryItemDetail.LocationId & "</LocationId>")
                    builder.Append("<AdquisitionDate>" & entryItemDetail.AdquisitionDate.ToString("dd/MM/yyyy") & "</AdquisitionDate>")
                    builder.Append("<Depreciate>" & entryItemDetail.Depreciate & "</Depreciate>")
                    builder.Append("<Amortize>" & entryItemDetail.Amortize & "</Amortize>")
                    builder.Append("<HandlesWarranty>" & entryItemDetail.HandlesWarranty & "</HandlesWarranty>")
                    builder.Append("<ValidSmallerAmount>" & entryItemDetail.ValidSmallerAmount & "</ValidSmallerAmount>")

                    If entryItemDetail.WarrantyExpirationDate IsNot Nothing Then
                        builder.Append("<WarrantyExpirationDate>" & entryItemDetail.WarrantyExpirationDate.Value.ToString("dd/MM/yyyy") & "</WarrantyExpirationDate>")
                    Else
                        builder.Append("<WarrantyExpirationDate>" & 0 & "</WarrantyExpirationDate>")
                    End If

                    builder.Append("<StatusAssetId>" & entryItemDetail.StatusAssetId & "</StatusAssetId>")
                    builder.Append("<ParentId>" & contTempId & "</ParentId>")
                    builder.Append("<TempId>" & contTempId2 & "</TempId>")

                    'FixedAssetEntryItemDetailBook
                    If entryItemDetail.FixedAssetEntryItemDetailBook IsNot Nothing AndAlso entryItemDetail.FixedAssetEntryItemDetailBook.Count > 0 Then
                        For Each entryItemDetailBook In entryItemDetail.FixedAssetEntryItemDetailBook

                            builder.Append("<FixedAssetEntryItemDetailBook>")

                            builder.Append("<Id>" & entryItemDetailBook.Id & "</Id>")
                            builder.Append("<FixedAssetEntryItemDetailId>" & entryItemDetailBook.FixedAssetEntryItemDetailId & "</FixedAssetEntryItemDetailId>")
                            builder.Append("<LegalBookId>" & entryItemDetailBook.LegalBookId & "</LegalBookId>")
                            builder.Append("<LifeTime>" & entryItemDetailBook.LifeTime & "</LifeTime>")
                            builder.Append("<UnitLifeTime>" & entryItemDetailBook.UnitLifeTime & "</UnitLifeTime>")
                            builder.Append("<DepreciationType>" & entryItemDetailBook.DepreciationType & "</DepreciationType>")
                            builder.Append("<TotalProductionUnit>" & entryItemDetailBook.TotalProductionUnit & "</TotalProductionUnit>")
                            builder.Append("<PercentageRescue>" & entryItemDetailBook.PercentageRescue.ToString().Replace(",", ".") & "</PercentageRescue>")
                            builder.Append("<ParentId>" & contTempId2 & "</ParentId>")

                            builder.Append("</FixedAssetEntryItemDetailBook>")

                        Next
                    End If

                    'FixedAssetEntryItemDetailPart
                    If entryItemDetail.FixedAssetEntryItemDetailPart IsNot Nothing AndAlso entryItemDetail.FixedAssetEntryItemDetailPart.Count > 0 Then
                        For Each entryItemDetailPart In entryItemDetail.FixedAssetEntryItemDetailPart

                            builder.Append("<FixedAssetEntryItemDetailPart>")

                            builder.Append("<Id>" & entryItemDetailPart.Id & "</Id>")
                            builder.Append("<FixedAssetEntryItemDetailId>" & entryItemDetailPart.FixedAssetEntryItemDetailId & "</FixedAssetEntryItemDetailId>")
                            builder.Append("<PartAccesoriesConsumiblesId>" & entryItemDetailPart.PartAccesoriesConsumiblesId & "</PartAccesoriesConsumiblesId>")
                            builder.Append("<DepreciatePart>" & entryItemDetailPart.DepreciatePart & "</DepreciatePart>")
                            builder.Append("<Value>" & entryItemDetailPart.Value.ToString().Replace(",", ".") & "</Value>")
                            builder.Append("<ParentId>" & contTempId2 & "</ParentId>")
                            builder.Append("<TempId>" & contTempId3 & "</TempId>")

                            'FixedAssetEntryItemDetailPartBook
                            If entryItemDetailPart.FixedAssetEntryItemDetailPartBook IsNot Nothing AndAlso entryItemDetailPart.FixedAssetEntryItemDetailPartBook.Count > 0 Then
                                For Each entryItemDetailPartBook In entryItemDetailPart.FixedAssetEntryItemDetailPartBook

                                    builder.Append("<FixedAssetEntryItemDetailPartBook>")

                                    builder.Append("<Id>" & entryItemDetailPartBook.Id & "</Id>")
                                    builder.Append("<FixedAssetEntryItemDetailPartId>" & entryItemDetailPartBook.FixedAssetEntryItemDetailPartId & "</FixedAssetEntryItemDetailPartId>")
                                    builder.Append("<LegalBookId>" & entryItemDetailPartBook.LegalBookId & "</LegalBookId>")
                                    builder.Append("<LifeTime>" & entryItemDetailPartBook.LifeTime & "</LifeTime>")
                                    builder.Append("<UnitLifeTime>" & entryItemDetailPartBook.UnitLifeTime & "</UnitLifeTime>")
                                    builder.Append("<DepreciationType>" & entryItemDetailPartBook.DepreciationType & "</DepreciationType>")
                                    builder.Append("<TotalProductionUnit>" & entryItemDetailPartBook.TotalProductionUnit & "</TotalProductionUnit>")
                                    builder.Append("<PercentageRescue>" & entryItemDetailPartBook.PercentageRescue.ToString().Replace(",", ".") & "</PercentageRescue>")
                                    builder.Append("<ParentId>" & contTempId3 & "</ParentId>")

                                    builder.Append("</FixedAssetEntryItemDetailPartBook>")

                                Next
                            End If

                            builder.Append("</FixedAssetEntryItemDetailPart>")

                            contTempId3 += 1

                        Next
                    End If

                    builder.Append("</FixedAssetEntryItemDetail>")

                    contTempId2 += 1

                Next
            End If

            builder.Append("</FixedAssetEntryItem>")

            contTempId += 1

        Next


        builder.Append("</FixedAssetEntry>")

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Convierte a objeto xml a todos los listados de eliminados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXmlListDeletes(ListDeleteFixedAssetEntryItemDetailPartBook As List(Of FixedAssetEntryItemDetailPartBook), ListDeleteFixedAssetEntryItemDetailPart As List(Of FixedAssetEntryItemDetailPart), ListDeleteFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook), ListDeleteFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail), ListDeleteFixedAssetEntryItem As List(Of FixedAssetEntryItem), Status As Integer) As List(Of String)
        Dim builder As StringBuilder
        Dim ListString As New List(Of String)

        builder = New StringBuilder()
        If ListDeleteFixedAssetEntryItemDetailPartBook IsNot Nothing AndAlso ListDeleteFixedAssetEntryItemDetailPartBook.Count > 0 AndAlso Status <> 3 Then
            For Each item In ListDeleteFixedAssetEntryItemDetailPartBook
                builder.Append("<ListDeleteFixedAssetEntryItemDetailPartBook>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("</ListDeleteFixedAssetEntryItemDetailPartBook>")
            Next
        Else
            builder.Append("<ListDeleteFixedAssetEntryItemDetailPartBook>")
            builder.Append("<Id>" & 0 & "</Id>")
            builder.Append("</ListDeleteFixedAssetEntryItemDetailPartBook>")
        End If
        ListString.Add(builder.ToString)

        builder = New StringBuilder()
        If ListDeleteFixedAssetEntryItemDetailPart IsNot Nothing AndAlso ListDeleteFixedAssetEntryItemDetailPart.Count > 0 AndAlso Status <> 3 Then
            For Each item In ListDeleteFixedAssetEntryItemDetailPart
                builder.Append("<ListDeleteFixedAssetEntryItemDetailPart>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("</ListDeleteFixedAssetEntryItemDetailPart>")
            Next
        Else
            builder.Append("<ListDeleteFixedAssetEntryItemDetailPart>")
            builder.Append("<Id>" & 0 & "</Id>")
            builder.Append("</ListDeleteFixedAssetEntryItemDetailPart>")
        End If
        ListString.Add(builder.ToString)

        builder = New StringBuilder()
        If ListDeleteFixedAssetEntryItemDetailBook IsNot Nothing AndAlso ListDeleteFixedAssetEntryItemDetailBook.Count > 0 AndAlso Status <> 3 Then
            For Each item In ListDeleteFixedAssetEntryItemDetailBook
                builder.Append("<ListDeleteFixedAssetEntryItemDetailBook>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("</ListDeleteFixedAssetEntryItemDetailBook>")
            Next
        Else
            builder.Append("<ListDeleteFixedAssetEntryItemDetailBook>")
            builder.Append("<Id>" & 0 & "</Id>")
            builder.Append("</ListDeleteFixedAssetEntryItemDetailBook>")
        End If
        ListString.Add(builder.ToString)

        builder = New StringBuilder()
        If ListDeleteFixedAssetEntryItemDetail IsNot Nothing AndAlso ListDeleteFixedAssetEntryItemDetail.Count > 0 AndAlso Status <> 3 Then
            For Each item In ListDeleteFixedAssetEntryItemDetail
                builder.Append("<ListDeleteFixedAssetEntryItemDetail>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("</ListDeleteFixedAssetEntryItemDetail>")
            Next
        Else
            builder.Append("<ListDeleteFixedAssetEntryItemDetail>")
            builder.Append("<Id>" & 0 & "</Id>")
            builder.Append("</ListDeleteFixedAssetEntryItemDetail>")
        End If
        ListString.Add(builder.ToString)

        builder = New StringBuilder()
        If ListDeleteFixedAssetEntryItem IsNot Nothing AndAlso ListDeleteFixedAssetEntryItem.Count > 0 AndAlso Status <> 3 Then
            For Each item In ListDeleteFixedAssetEntryItem
                builder.Append("<ListDeleteFixedAssetEntryItem>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("</ListDeleteFixedAssetEntryItem>")
            Next
        Else
            builder.Append("<ListDeleteFixedAssetEntryItem>")
            builder.Append("<Id>" & 0 & "</Id>")
            builder.Append("</ListDeleteFixedAssetEntryItem>")
        End If
        ListString.Add(builder.ToString)

        '(0): ListDeleteFixedAssetEntryItemDetailPartBook
        '(1): ListDeleteFixedAssetEntryItemDetailPart
        '(2): ListDeleteFixedAssetEntryItemDetailBook
        '(3): ListDeleteFixedAssetEntryItemDetail
        '(4): ListDeleteFixedAssetEntryItem
        Return ListString
    End Function

    ''' <summary>
    ''' Obtiene el catalogo por id del articulo
    ''' </summary>
    ''' <param name="ItemId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetItemCatalogByItemId(ItemId As Integer) As FixedAssetItemCatalog Implements IFixedAssetEntryAdminService.GetFixedAssetItemCatalogByItemId
        If ItemId = 0 Then
            Throw New ArgumentNullException("ItemId")
        End If
        Try
            Return Me._FixedAssetEntryRepository.GetFixedAssetItemCatalogByItemId(ItemId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


    Public Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection(connectionString)
            Try

                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable

            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()

            End Try
        End Using
    End Function
    ''' <summary>
    ''' Obtiene la información necesaria para el  informe de Hoja de Vida del Activo
    ''' </summary>
    ''' <param name="AdquisitionDateStart">The adquisition date start.</param>
    ''' <param name="AdquisitionDateEnd">The adquisition date end.</param>
    ''' <param name="PlateStart">The plate start.</param>
    ''' <param name="PlateEnd">The plate end.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' AdquisitionDateEnd
    ''' or
    ''' AdquisitionDateStart
    ''' </exception>
    Function GetListFixedAssetPhysicalAsset(AdquisitionDateStart As Integer, AdquisitionDateEnd As Integer, PlateStart As Integer, PlateEnd As Integer, ItemStart As Integer, ItemEnd As Integer, ItemTypeStart As Integer, ItemTypeEnd As Integer, LocationStart As Integer, LocationEnd As Integer, Responsible As String, ItemCatalog As String, StatusAsset As String, session As SessionValues) As DataSet Implements IFixedAssetEntryAdminService.GetListFixedAssetPhysicalAsset
        If AdquisitionDateEnd = 0 Then
            Throw New ArgumentNullException("AdquisitionDateEnd")
        End If
        If AdquisitionDateStart = 0 Then
            Throw New ArgumentNullException("AdquisitionDateStart")
        End If

        If Responsible Is Nothing Then
            Responsible = "null"
        End If

        If ItemCatalog Is Nothing Then
            ItemCatalog = "null"
        End If

        If StatusAsset Is Nothing Then
            StatusAsset = "null"
        End If


        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty

            query1 = "exec FixedAsset.SP_FixedAssetPhysicalAsset " & AdquisitionDateStart & "," & AdquisitionDateEnd & "," & PlateStart & "," & PlateEnd & "," & ItemStart & "," & ItemEnd & "," & ItemTypeStart & "," & ItemTypeEnd & "," & LocationStart & "," & LocationEnd & "," & Responsible & "," & ItemCatalog & "," & StatusAsset & ""

            Dim dt1 = Me.GetDatatable(query1, session, "FixedAssetPhysicalAsset")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function



    Public Function CreateJournalVoucher(fixedAssetEntry As FixedAssetEntry, FixedAssetSettings As SettingFixedAsset, audit As AuditMessage) As ActionResult(Of JournalVouchers)
        Dim JournalVouchers As New JournalVouchers
        Dim CostCenterId As Integer?
        Dim CostCenterIdDetail As Integer?
        Dim Location As FixedAssetLocation
        Dim supplier = _supplierRepository.GetSupplierById(fixedAssetEntry.SupplierId, False)

        With JournalVouchers

            .IdJournalVoucher = FixedAssetSettings.IdIngressAccountingVoucher
            .VoucherDate = fixedAssetEntry.EntryDate
            .Imported = False
            .Status = 2
            .Detail = String.Format("Ingreso de Activos No. {0} - Factura No. {1} - Proveedor: ({2} - {3})", fixedAssetEntry.Code, fixedAssetEntry.InvoiceNumber, supplier.Code, supplier.Name)
            .EntityId = fixedAssetEntry.Id
            .EntityCode = fixedAssetEntry.Code
            .EntityName = fixedAssetEntry.GetType().Name
            .IsClosedYear = False
            .CreationDate = Date.Now
            .CreationUser = audit.IdUser

            If fixedAssetEntry.LocationId IsNot Nothing AndAlso fixedAssetEntry.LocationId > 0 Then
                'La Localización es General
                Location = _FixedAssetEntryRepository.GetFixedAssetLocationById(fixedAssetEntry.LocationId)
                CostCenterId = Location.FunctionalUnit.CostCenterId
            End If


            For Each ObjFixedAssetEntryItem As FixedAssetEntryItem In fixedAssetEntry.FixedAssetEntryItem

                Dim IdCreditAccount As Integer
                Dim IdDebitAccount As Integer

                Dim EquipmentCatalog = _equipmentCatalogRepository.GetFixedAssetItemCatalogByItemId(ObjFixedAssetEntryItem.ItemId)

                If fixedAssetEntry.AdquisitionType = 3 Then
                    'Comodato
                    IdDebitAccount = EquipmentCatalog.DebitLoanAccountId
                    IdCreditAccount = EquipmentCatalog.CreditLoanAccountId
                ElseIf fixedAssetEntry.AdquisitionType = 4 Then
                    'Donacion
                    IdDebitAccount = EquipmentCatalog.IncomeAccountId
                    IdCreditAccount = FixedAssetSettings.DonationMainAccountId
                ElseIf fixedAssetEntry.AdquisitionType = 5 Then
                    'Traspaso de Bienes
                    IdDebitAccount = EquipmentCatalog.IncomeAccountId
                    IdCreditAccount = FixedAssetSettings.TransferPropertyMainAccountId
                ElseIf fixedAssetEntry.AdquisitionType = 6 Then
                    'Otros Conceptos
                    IdDebitAccount = EquipmentCatalog.IncomeAccountId
                    IdCreditAccount = FixedAssetSettings.OtherConceptsMainAccountId
                End If

                If CostCenterId Is Nothing Then
                    CostCenterIdDetail = Nothing
                    If ObjFixedAssetEntryItem.FixedAssetEntryItemDetail IsNot Nothing AndAlso ObjFixedAssetEntryItem.FixedAssetEntryItemDetail.Any(Function(d) d.LocationId > 0) Then
                        For Each ObjFixedAssetEntryItemDetail As FixedAssetEntryItemDetail In ObjFixedAssetEntryItem.FixedAssetEntryItemDetail.Where(Function(d) d.LocationId > 0)
                            If CostCenterIdDetail Is Nothing Then
                                Location = _FixedAssetEntryRepository.GetFixedAssetLocationById(ObjFixedAssetEntryItemDetail.LocationId)
                                CostCenterIdDetail = Location.FunctionalUnit.CostCenterId
                            End If
                        Next

                    End If
                End If

                For i As Integer = 0 To 1
                    Dim JournalVouchersDetail As New JournalVoucherDetails

                    With JournalVouchersDetail

                        If i = 0 Then
                            .IdMainAccount = IdDebitAccount
                            .DebitValue = ObjFixedAssetEntryItem.TotalValue
                        Else
                            .IdMainAccount = IdCreditAccount
                            .CreditValue = ObjFixedAssetEntryItem.TotalValue
                        End If
                        .Detail = "Detalle del Comprobante Contable generado por el Ingreso de Activos"
                        .IdThirdParty = _supplierRepository.GetSupplierById(fixedAssetEntry.SupplierId, False).IdThirdParty
                        .IdCostCenter = If(CostCenterId, CostCenterIdDetail)
                        .IdRetention = Nothing
                        .RetentionRate = 0
                    End With
                    .JournalVoucherDetails.Add(JournalVouchersDetail)
                Next
            Next

        End With

        Return New ActionResult(Of JournalVouchers) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = JournalVouchers}
    End Function

    ''' <summary>
    ''' Función para importar detalles de catálogo de artículos desde archivo
    ''' </summary>
    ''' <param name="dataimport"></param>
    ''' <param name="datapaste"></param>
    ''' <returns></returns>
    Public Function SetFixedAssetItemCatalogDetailFromFile(dataimport As List(Of ImportFileRow), datapaste As List(Of List(Of String))) As List(Of SP_SetFixedAssetItemCatalogDetailFromFile_Result) Implements IFixedAssetEntryAdminService.SetFixedAssetItemCatalogDetailFromFile
        Try

            'Objeto xml
            Dim xmlObject As String = String.Empty

            'Listado de registros con errores
            Dim listRecordsErrors As New List(Of String())

            'Listado de errores
            Dim listErrors As New List(Of String)

            If dataimport IsNot Nothing Then
                xmlObject = ConvertImportToXml(dataimport)
            Else
                xmlObject = ConvertPasteToXml(datapaste)
            End If


            'Se consume el procedimiento almacenado
            Dim resultStore = _equipmentCatalogRepository.SetFixedAssetItemCatalogDetailFromFile(xmlObject)

            Return resultStore

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función que convierte el DataSource en XML
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Private Function ConvertImportToXml(data As List(Of ImportFileRow)) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Data>")

        For Each item In data

            Dim indexRow = item.IndexRow
            Dim Columns = item.Row.Count

            builder.Append("<Row>")

            builder.Append("<CostCenter>" & If(Columns > 0, item.Row.Item(0), String.Empty) & "</CostCenter>")
            builder.Append("<DistributionPercentage>" & If(Columns > 1, item.Row.Item(1)?.ToString.Replace(",", "."), String.Empty) & "</DistributionPercentage>")
            builder.Append("<LoanSpendAccount>" & If(Columns > 2, item.Row.Item(2), String.Empty) & "</LoanSpendAccount>")
            builder.Append("<LoanLeasingSpendAccount>" & If(Columns > 3, item.Row.Item(3), String.Empty) & "</LoanLeasingSpendAccount>")
            builder.Append("<ExpenseLoanAccount>" & If(Columns > 4, item.Row.Item(4), String.Empty) & "</ExpenseLoanAccount>")
            builder.Append("<LoanFinancialRentingAccount>" & If(Columns > 5, item.Row.Item(5), String.Empty) & "</LoanFinancialRentingAccount>")

            builder.Append("</Row>")

        Next
        builder.Append("</Data>")

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Función que convierte el DataSource en XML
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Private Function ConvertPasteToXml(data As List(Of List(Of String))) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Data>")

        Dim indexRow As Integer = 0
        For Each item In data

            Dim Columns = item.Count

            builder.Append("<Row>")

            builder.Append("<CostCenter>" & If(Columns > 0, item(0), String.Empty) & "</CostCenter>")
            builder.Append("<DistributionPercentage>" & If(Columns > 1, item(1), String.Empty) & "</DistributionPercentage>")
            builder.Append("<LoanSpendAccount>" & If(Columns > 2, item(2), String.Empty) & "</LoanSpendAccount>")
            builder.Append("<LoanLeasingSpendAccount>" & If(Columns > 3, item(3), String.Empty) & "</LoanLeasingSpendAccount>")
            builder.Append("<ExpenseLoanAccount>" & If(Columns > 4, item(4).ToString.Replace(",", "."), String.Empty) & "</ExpenseLoanAccount>")
            builder.Append("<LoanFinancialRentingAccount>" & If(Columns > 5, item(5).ToString.Replace(",", "."), String.Empty) & "</LoanFinancialRentingAccount>")

            builder.Append("</Row>")

        Next
        builder.Append("</Data>")

        Return builder.ToString()
    End Function


#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _accountPayableAdminService.Dispose()
                _AccountingDocumentAdmin.Dispose()
            End If
            _sequenceRepository = Nothing
            _FixedAssetEntryRepository = Nothing
            _fixedAssetServices = Nothing
            _sequensePaymentsCRepository = Nothing
            _supplierRepository = Nothing
            _supplierDistributionLineRepository = Nothing
            _paymentsConceptRepository = Nothing
            _accountPayableAdminService = Nothing
            _equipmentCatalogRepository = Nothing
            _AccountingDocumentAdmin = Nothing
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
