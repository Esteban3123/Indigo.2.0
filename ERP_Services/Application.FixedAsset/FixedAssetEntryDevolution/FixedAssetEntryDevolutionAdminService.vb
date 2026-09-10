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
Imports Application.Accounting
Imports Application.Portfolio

#End Region

Public Class FixedAssetEntryDevolutionAdminService
    Implements IFixedAssetEntryDevolutionAdminService

#Region "Variables"

    'Repositorio de la aseguradora
    Private _FixedAssetEntryDevolutionRepository As IFixedAssetEntryDevolutionRepository

    'Repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    'Repositorio de secuencia numerica para pagos
    Private _sequensePaymentsCRepository As ISequensePaymentsCRepository

    'Repositorio de la aseguradora
    Private _FixedAssetEntryRepository As IFixedAssetEntryRepository

    'Repositorio de proveedor
    Private _supplierRepository As ISupplierRepository

    'Repositorio para concepto de pago
    Private _paymentsConceptRepository As IPaymentsConceptRepository

    'Repositorio para el comprobante
    Private _accountingRepository As IAccountingDocumentAdminService

    'Servicio de aplicación para notas debito/credito
    Private _notesDebitCreditAdminService As INotesDebitCreditAdminService

    'Servicio de aplicación para datos de empresa
    Private _companySettingsRepository As ICompanySettingsRepository
    ''' <summary>
    ''' Repositorio de viebot
    ''' </summary>
    ''' <remarks></remarks>
    Private _vieBotRepository As IVieBotRepository

    ''' <summary>
    ''' Repositorio para la tarifa de IVA
    ''' </summary>
    ''' <remarks></remarks>
    Private _generalLedgerIVARepository As IGeneralLedgerIVARepository

    ''' <summary>
    ''' Repositorio del detalle de orden de compra
    ''' </summary>
    Private _fixedAssetPurchaseOrderItem As IFixedAssetPurchaseOrderItemRepository

    ''' <summary>
    ''' Repositorio de las cuentas
    ''' </summary>
    Private _pucAdminService As IPUCAdminService
#End Region

#Region "Builder"

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(FixedAssetEntryDevolutionRepository As IFixedAssetEntryDevolutionRepository, sequenceRepository As IFixedAssetSequenceDetailRepository,
                   sequensePaymentsCRepository As ISequensePaymentsCRepository, FixedAssetEntryRepository As IFixedAssetEntryRepository, supplierRepository As ISupplierRepository,
                   paymentsConceptRepository As IPaymentsConceptRepository, notesDebitCreditAdminService As INotesDebitCreditAdminService, accountingRepository As IAccountingDocumentAdminService,
                   vieBotRepository As IVieBotRepository, companySettingsRepository As ICompanySettingsRepository, GeneralLedgerIVARepository As IGeneralLedgerIVARepository,
                   fixedAssetPurchaseOrderItemRepository As IFixedAssetPurchaseOrderItemRepository, pucAdminService As IPUCAdminService)
        If sequenceRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceRepository")
        End If
        If FixedAssetEntryDevolutionRepository Is Nothing Then
            Throw New ArgumentNullException("FixedAssetEntryDevolutionRepository")
        End If
        If sequensePaymentsCRepository Is Nothing Then
            Throw New ArgumentNullException("sequensePaymentsCRepository")
        End If
        If FixedAssetEntryRepository Is Nothing Then
            Throw New ArgumentNullException("FixedAssetEntryRepository")
        End If
        If supplierRepository Is Nothing Then
            Throw New ArgumentNullException("supplierRepository")
        End If
        If paymentsConceptRepository Is Nothing Then
            Throw New ArgumentNullException("paymentsConceptRepository")
        End If
        If notesDebitCreditAdminService Is Nothing Then
            Throw New ArgumentNullException("notesDebitCreditAdminService")
        End If
        If vieBotRepository Is Nothing Then
            Throw New ArgumentNullException("vieBotRepository")
        End If
        If fixedAssetPurchaseOrderItemRepository Is Nothing Then
            Throw New ArgumentNullException("FixedAssetPurchaseOrderItemRepository")
        End If
        _sequenceRepository = sequenceRepository
        _FixedAssetEntryDevolutionRepository = FixedAssetEntryDevolutionRepository
        _sequensePaymentsCRepository = sequensePaymentsCRepository
        _FixedAssetEntryRepository = FixedAssetEntryRepository
        _supplierRepository = supplierRepository
        _paymentsConceptRepository = paymentsConceptRepository
        _accountingRepository = accountingRepository
        _notesDebitCreditAdminService = notesDebitCreditAdminService
        _vieBotRepository = vieBotRepository
        _companySettingsRepository = companySettingsRepository
        _generalLedgerIVARepository = GeneralLedgerIVARepository
        _fixedAssetPurchaseOrderItem = fixedAssetPurchaseOrderItemRepository
        _pucAdminService = pucAdminService
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetEntryDevolution(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetEntryDevolution) Implements IFixedAssetEntryDevolutionAdminService.GetFixedAssetEntryDevolution
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetEntryDevolution As FixedAssetEntryDevolution = Me._FixedAssetEntryDevolutionRepository.GetFixedAssetEntryDevolution(code.Trim())
            If FixedAssetEntryDevolution IsNot Nothing AndAlso FixedAssetEntryDevolution.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetEntryDevolution)(FixedAssetEntryDevolution, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = True, .ObjectEmbbeded = FixedAssetEntryDevolution}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetEntryDevolutionById(Id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetEntryDevolution) Implements IFixedAssetEntryDevolutionAdminService.GetFixedAssetEntryDevolutionById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetEntryDevolution As FixedAssetEntryDevolution = Me._FixedAssetEntryDevolutionRepository.GetFixedAssetEntryDevolutionById(Id)
            If FixedAssetEntryDevolution IsNot Nothing AndAlso FixedAssetEntryDevolution.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetEntryDevolution)(FixedAssetEntryDevolution, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = True, .ObjectEmbbeded = FixedAssetEntryDevolution}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="FixedAssetEntryDevolution"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetEntryDevolution(FixedAssetEntryDevolution As FixedAssetEntryDevolution, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetEntryDevolution) Implements IFixedAssetEntryDevolutionAdminService.SaveFixedAssetEntryDevolution
        If FixedAssetEntryDevolution Is Nothing Then
            Throw New ArgumentNullException("FixedAssetEntryDevolution")
        End If
        Dim unitOfWork As IUnitWork = Me._FixedAssetEntryDevolutionRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXml(FixedAssetEntryDevolution)
                Dim resultStore = _FixedAssetEntryDevolutionRepository.SP_SaveFixedAssetEntryDevolution(EntityXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .Message = resultStore.Message, .StatusCode = eStatusResult.WARNING}
                End If

                FixedAssetEntryDevolution.Id = resultStore.Id
                FixedAssetEntryDevolution.Code = resultStore.CodeResult
                FixedAssetEntryDevolution.MarkAsUnchanged()
                unitOfWork.Commit()
                transaction.Complete()
                Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = True, .ObjectEmbbeded = FixedAssetEntryDevolution, .StatusCode = eStatusResult.SUCCESS}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .Message = "Error de concurrencia", .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Convierte la entidad a objeto xml
    ''' </summary>
    ''' <param name="FixedAssetActiveOutput"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXml(FixedAssetEntryDevolution As FixedAssetEntryDevolution) As String
        Dim builder As StringBuilder = New StringBuilder()

        'FixedAssetDevolution
        builder.Append("<FixedAssetDevolution>")

        'Se arma la cabacera
        builder.Append("<Id>" & FixedAssetEntryDevolution.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & FixedAssetEntryDevolution.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & FixedAssetEntryDevolution.Code & "</Code>")
        builder.Append("<DocumentDate>" & FixedAssetEntryDevolution.DocumentDate.ToString("dd/MM/yyyy") & "</DocumentDate>")
        builder.Append("<FixedAssetEntryId>" & FixedAssetEntryDevolution.FixedAssetEntryId & "</FixedAssetEntryId>")
        builder.Append("<Description>" & FixedAssetEntryDevolution.Description & "</Description>")
        builder.Append("<FreightValue>" & FixedAssetEntryDevolution.FreightValue.ToString("0").Replace(",", ".") & "</FreightValue>")
        builder.Append("<FreightIVAPercentage>" & FixedAssetEntryDevolution.FreightIVAPercentage.ToString().Replace(",", ".") & "</FreightIVAPercentage>")
        builder.Append("<FreightIVAValue>" & FixedAssetEntryDevolution.FreightIVAValue.ToString("0").Replace(",", ".") & "</FreightIVAValue>")
        builder.Append("<Value>" & FixedAssetEntryDevolution.Value.ToString("0").Replace(",", ".") & "</Value>")
        builder.Append("<ValueDiscount>" & FixedAssetEntryDevolution.ValueDiscount.ToString("0").Replace(",", ".") & "</ValueDiscount>")
        builder.Append("<ValueTax>" & FixedAssetEntryDevolution.ValueTax.ToString("0").Replace(",", ".") & "</ValueTax>")
        builder.Append("<WithholdingTax>" & FixedAssetEntryDevolution.WithholdingTax.ToString("0").Replace(",", ".") & "</WithholdingTax>")
        builder.Append("<WithholdingICA>" & FixedAssetEntryDevolution.WithholdingICA.ToString("0").Replace(",", ".") & "</WithholdingICA>")
        builder.Append("<RetentionSource>" & FixedAssetEntryDevolution.RetentionSource.ToString("0").Replace(",", ".") & "</RetentionSource>")
        builder.Append("<RetentionOther>" & FixedAssetEntryDevolution.RetentionOther.ToString("0").Replace(",", ".") & "</RetentionOther>")
        builder.Append("<DeductionOther>" & FixedAssetEntryDevolution.DeductionOther.ToString("0").Replace(",", ".") & "</DeductionOther>")
        builder.Append("<TotalValue>" & FixedAssetEntryDevolution.TotalValue.ToString("0").Replace(",", ".") & "</TotalValue>")
        builder.Append("<Status>" & FixedAssetEntryDevolution.Status & "</Status>")

        If FixedAssetEntryDevolution.FixedAssetEntryDevolutionDetail IsNot Nothing AndAlso FixedAssetEntryDevolution.FixedAssetEntryDevolutionDetail.Count > 0 Then
            For Each itemDetail In FixedAssetEntryDevolution.FixedAssetEntryDevolutionDetail
                'Se arma los detalles
                builder.Append("<FixedAssetDevolutionDetail>")

                builder.Append("<Id>" & itemDetail.Id & "</Id>")
                builder.Append("<FixedAssetEntryDevolutionId>" & itemDetail.FixedAssetEntryDevolutionId & "</FixedAssetEntryDevolutionId>")
                builder.Append("<FixedAssetEntryItemId>" & itemDetail.FixedAssetEntryItemId & "</FixedAssetEntryItemId>")
                builder.Append("<FixedAssetEntryItemDetailId>" & itemDetail.FixedAssetEntryItemDetailId & "</FixedAssetEntryItemDetailId>")
                builder.Append("<UnitValue>" & itemDetail.UnitValue.ToString("n4").Replace(".", "").Replace(",", ".") & "</UnitValue>")
                builder.Append("<SubTotalValue>" & itemDetail.SubTotalValue.ToString("n4").Replace(".", "").Replace(",", ".") & "</SubTotalValue>")
                builder.Append("<IvaPercentage>" & itemDetail.IvaPercentage.ToString().Replace(",", ".") & "</IvaPercentage>")
                builder.Append("<IvaValue>" & itemDetail.IvaValue.ToString("n4").Replace(".", "").Replace(",", ".") & "</IvaValue>")
                builder.Append("<DiscountPercentage>" & itemDetail.DiscountPercentage.ToString().Replace(",", ".") & "</DiscountPercentage>")
                builder.Append("<DiscountValue>" & itemDetail.DiscountValue.ToString("n4").Replace(".", "").Replace(",", ".") & "</DiscountValue>")
                builder.Append("<TotalValue>" & itemDetail.TotalValue.ToString("n4").Replace(".", "").Replace(",", ".") & "</TotalValue>")
                builder.Append("<RTFPercentage>" & itemDetail.RTFPercentage.ToString().Replace(",", ".") & "</RTFPercentage>")
                builder.Append("<RTFValue>" & itemDetail.RTFValue.ToString("n4").Replace(".", "").Replace(",", ".") & "</RTFValue>")
                builder.Append("<CheckOption>" & itemDetail.CheckOption & "</CheckOption>")

                builder.Append("</FixedAssetDevolutionDetail>")
            Next
        End If

        If FixedAssetEntryDevolution.FixedAssetEntryDevolutionObligationBudget IsNot Nothing AndAlso FixedAssetEntryDevolution.FixedAssetEntryDevolutionObligationBudget.Count > 0 Then
            For Each item In FixedAssetEntryDevolution.FixedAssetEntryDevolutionObligationBudget
                builder.Append("<FixedAssetEntryDevolutionObligationBudget>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("<FixedAssetEntryDevolutionId>" & item.FixedAssetEntryDevolutionId & "</FixedAssetEntryDevolutionId>")
                builder.Append("<ObligationDetailId>" & item.ObligationDetailId & "</ObligationDetailId>")
                builder.Append("<Value>" & item.Value & "</Value>")
                If item.ChangeTracker.State <> ObjectState.Deleted Then
                    builder.Append("<IsDelete>" & 0 & "</IsDelete>")
                Else
                    builder.Append("<IsDelete>" & 1 & "</IsDelete>")
                End If
                builder.Append("</FixedAssetEntryDevolutionObligationBudget>")
            Next
        End If

        builder.Append("</FixedAssetDevolution>")

        Return builder.ToString
    End Function

    ''' <summary>
    ''' Confirma un registro
    ''' </summary>
    ''' <param name="FixedAssetEntryDevolution"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmFixedAssetEntryDevolution(FixedAssetEntryDevolution As FixedAssetEntryDevolution, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetEntryDevolution) Implements IFixedAssetEntryDevolutionAdminService.ConfirmFixedAssetEntryDevolution
        If FixedAssetEntryDevolution Is Nothing Then
            Throw New ArgumentNullException("FixedAssetEntryDevolution")
        End If

        Try
            'Se consulta la secuencia de notas debito/credito por el id del form
            Dim sequensePayments As PaymentsSecuence = _sequensePaymentsCRepository.GetSequenseByIdForm("731")
            If sequensePayments.Id = 0 Then
                Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "No existe secuencia numérica para el formulario de notas débito/crédito."}
            End If

            'Valida la secuencia para la unidad operativa actual.
            Dim sequensePaymentsResult As PaymentsSecuenceDetail
            If (sequensePayments.Scope = "O") Then
                sequensePaymentsResult = sequensePayments.PaymentsSecuenceDetail.FirstOrDefault()
            Else
                sequensePaymentsResult = sequensePayments.PaymentsSecuenceDetail.Where(Function(x) x.IdOperatingUnit = FixedAssetEntryDevolution.OperatingUnitId).FirstOrDefault()
            End If

            If (sequensePaymentsResult.Id = 0 OrElse sequensePaymentsResult Is Nothing) Then
                Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "No se puede generar la Nota Debito porque no existe secuencia numérica para la unidad operativa."}
            End If

            'Se consulta si hay parámetros de activo fijo
            Dim settingFixedAsset As SettingFixedAsset = _FixedAssetEntryRepository.GetSettingFixedAssetByOperatingUnidId(FixedAssetEntryDevolution.OperatingUnitId)
            If settingFixedAsset Is Nothing Then
                Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "No existe parámetros de activo fijo para la unidad operativa escogida."}
            End If

            If settingFixedAsset.FreightIVAPercentage Is Nothing Then
                Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "No se ha parametrizado un concepto de retención de IVA del Flete."}
            End If

            'Se valida que existan parámetros definidos para el libro oficial en la unidad operativa actual
            If settingFixedAsset.SettingFixedAssetByLegalBook.Where(Function(d) d.LegalBook.OfficialBook = True AndAlso d.LegalBook.Status = True).Count() = 0 Then
                Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "No existe parámetros de activo fijo en unidad operativa escogida para el libro oficial."}
            End If

            'Para la contabilización, se verifica que todos los libros válidos de viebot para el proceso actual esten parametrizados
            Dim errors As New StringBuilder
            Dim ListVieBot As List(Of VieBot) = Me._vieBotRepository.GetVieBotByForm(FixedAssetEntryDevolution.GetType().Name, False)
            If ListVieBot Is Nothing Then
                errors.AppendLine("No hay libros parametrizados en viebot para la devolución de ingresos de activos")
            Else
                For Each itemVieBot In ListVieBot
                    If settingFixedAsset.SettingFixedAssetByLegalBook.Where(Function(d) d.LegalBookId = itemVieBot.LegalBookId).Count() = 0 Then
                        errors.AppendLine("El libro " + itemVieBot.CodeNameLegalBook + " no esta parametrizado para la unidad operativa escogida")
                    ElseIf itemVieBot.Allow = True AndAlso itemVieBot.HandlesHomologation = True Then
                        errors.AppendLine("El libro " + itemVieBot.CodeNameLegalBook + " no debe ser homologable")
                    End If
                Next
            End If

            If errors.Length > 0 Then
                Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = errors.ToString()}
            End If

            'Se valida que la fecha de ingreso este en el mismo mes que la fecha de parametros
            If FixedAssetEntryDevolution.DocumentDate.Month <> settingFixedAsset.ProcessDate.Month Then
                Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "El mes de la fecha del documento(" + FixedAssetEntryDevolution.DocumentDate.ToString("MMMM") + ") debe ser el mismo a la fecha de proceso de parámetros(" + settingFixedAsset.ProcessDate.ToString("MMMM") + ")"}
            End If

            'Se valida que los parámetros tengan asociada el concepto de notas debito/credito
            If settingFixedAsset.RefundAccountPayableConceptNoteId Is Nothing Then
                Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "Los parámetros de activos fijos no tiene parametrizado el concepto de notas para la devolución de ingreso de activos."}
            End If

            'Se consulta el ingreso seleccionado en el formulario
            Dim FixedAssetEntry = _FixedAssetEntryDevolutionRepository.GetFixedAssetEntryById(FixedAssetEntryDevolution.FixedAssetEntryId)

            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Try
                    'Se guarda la devolución de ingreso de activos
                    Dim resultEntryDevolution As ActionResult(Of FixedAssetEntryDevolution) = SaveFixedAssetEntryDevolution(FixedAssetEntryDevolution, audit, idSequense)
                    If Not resultEntryDevolution.StateResult Then
                        Transaction.Dispose()
                        Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = resultEntryDevolution.StatusCode, .Message = resultEntryDevolution.Message}
                    End If

                    'Se vuelve a consultar la devolución de ingreso de activos
                    Dim FixedAssetEntryDevolutionNew As FixedAssetEntryDevolution = _FixedAssetEntryDevolutionRepository.GetFixedAssetEntryDevolutionById(resultEntryDevolution.ObjectEmbbeded.Id)

                    FixedAssetEntryDevolution.Id = FixedAssetEntryDevolutionNew.Id
                    FixedAssetEntryDevolution.Code = FixedAssetEntryDevolutionNew.Code

                    'Mensaje que se devuelve para informarle al usuario de lo que se generó
                    Dim message As New StringBuilder
                    message.AppendLine(String.Format("El documento {0} se guardó y confirmó.", FixedAssetEntryDevolutionNew.Code))

                    If {1, 7}.Contains(FixedAssetEntry.AdquisitionType) Then
                        'Se crea nota para los tipos que únicamente crearon una Cuenta por Pagar, estos son Compra Directa o leasing financiero
                        'por tanto se valida que el ingreso seleccionado tenga asociada una cxp para poder realizar la nota
                        If FixedAssetEntry.AccountPayableId Is Nothing Then
                            Transaction.Dispose()
                            Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "El ingreso seleccionado no tiene asociada una cuenta por pagar."}
                        End If

                        'Se consulta la cuenta por pagar asociada al ingreso de activos
                        Dim ListAccountPayable = Nothing
                        ListAccountPayable = _FixedAssetEntryDevolutionRepository.GetAccountPayableById(FixedAssetEntry.AccountPayableId)
                        ListAccountPayable(0).Adjustment = FixedAssetEntryDevolution.Value + FixedAssetEntryDevolution.ValueTax + FixedAssetEntryDevolution.FreightValue + FixedAssetEntryDevolution.FreightIVAValue - FixedAssetEntryDevolution.ValueDiscount - Utils.RoundValue(FixedAssetEntryDevolution.WithholdingTax, Utils.RoundLevel.TwoDecimal) - Utils.RoundValue(FixedAssetEntryDevolution.WithholdingICA, Utils.RoundLevel.TwoDecimal) - Utils.RoundValue(FixedAssetEntryDevolution.RetentionSource, Utils.RoundLevel.TwoDecimal)
                        ListAccountPayable(0).Adjustment = Utils.RoundValue(ListAccountPayable(0).Adjustment, Utils.RoundLevel.TwoDecimal)
                        ListAccountPayable(0).HandlesAddModifyDelete = 1
                        ListAccountPayable(0).AccountPayableShares(0).ValueNoteShare = ListAccountPayable(0).Adjustment

                        If FixedAssetEntryDevolution.FixedAssetEntryDevolutionObligationBudget IsNot Nothing AndAlso FixedAssetEntryDevolution.FixedAssetEntryDevolutionObligationBudget.Count > 0 Then
                            For Each item In FixedAssetEntryDevolution.FixedAssetEntryDevolutionObligationBudget
                                Dim accountPayableCommitments As New AccountPayableCommitments
                                accountPayableCommitments.Id = item.ObligationDetailId
                                accountPayableCommitments.AccountPayableId = ListAccountPayable(0).Id
                                accountPayableCommitments.CommitmentDetailId = item.CommitmentDetailId
                                accountPayableCommitments.Value = item.Value
                                ListAccountPayable(0).AccountPayableCommitments.Add(accountPayableCommitments)
                            Next
                        End If

                        'Se crea nota para los tipos que únicamente crearon una Cuenta por Pagar, estos son Compra Directa o leasing financiero
                        Dim resultGeneratePaymentNote = GeneratePaymentNote(FixedAssetEntryDevolution, FixedAssetEntry, settingFixedAsset, ListAccountPayable(0), ListVieBot)
                        If Not resultGeneratePaymentNote.StateResult Then
                            Transaction.Dispose()
                            Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultGeneratePaymentNote.Message}
                        End If

                        'Se guarda y confirma la nota debito credito
                        Dim resultPaymentNote = _notesDebitCreditAdminService.SavePaymentNotesComplete(resultGeneratePaymentNote.ObjectEmbbeded, ListAccountPayable, Nothing, True, audit, sequensePaymentsResult.Id)
                        If Not resultPaymentNote.StateResult Then
                            Transaction.Dispose()

                            Dim messageResult As String = resultPaymentNote.MessageResult(0).ToString
                            If Not String.IsNullOrEmpty(resultPaymentNote.Message) Then
                                messageResult = resultPaymentNote.Message
                            End If

                            Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = messageResult}
                        End If

                        message.AppendLine(resultPaymentNote.MessageResult(0))
                    ElseIf Not (FixedAssetEntry.AdquisitionType = 8 OrElse FixedAssetEntry.AdquisitionType = 10) Then
                        'Si NO es Compra directa o leasing financiero pues contabilizan por medio de una nota
                        'Tampoco Comodato Tercerizado ni Renting Operativo puesto que estos Tipo de Adquisición no Contabilizan
                        Dim resultJournalVouchers = CreateJournalVouchers(FixedAssetEntryDevolution, FixedAssetEntry, settingFixedAsset, audit)
                        If resultJournalVouchers Is Nothing OrElse resultJournalVouchers.StateResult = False Then
                            Transaction.Dispose()
                            Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultJournalVouchers.Message}
                        End If

                        Dim JournalVoucher = resultJournalVouchers.ObjectEmbbeded
                        'Contabilizamos en cada uno de los libros habilitados en viebot para Ingreso de Activos Fijos
                        For Each itemVieBot In ListVieBot
                            If itemVieBot.Allow Then
                                JournalVoucher.LegalBookId = itemVieBot.LegalBookId
                                Dim ObjActionResultJournal = _accountingRepository.SaveAccountingDocument(JournalVoucher, audit)
                                If ObjActionResultJournal.StateResult Then
                                    Dim journalVoucherType As JournalVoucherTypes = _FixedAssetEntryRepository.GetJournalVoucherType(settingFixedAsset.DevolutionJournalVoucherId)
                                    message.AppendLine("Se generó comprobante contable " + journalVoucherType.Name + ": " + ObjActionResultJournal.ObjectEmbbeded.Consecutive.ToString() + " en el libro contable: " + itemVieBot.CodeNameLegalBook)
                                Else
                                    Transaction.Dispose()
                                    Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ObjActionResultJournal.Message.ToString()}
                                End If
                            End If
                        Next
                    End If
                    'Se obtiene los items del de ingreso
                    Dim fixedAssetEntryItem = _FixedAssetEntryDevolutionRepository.GetFixedAssetEntryItemsForDevolution(FixedAssetEntryDevolution.Id)
                    If fixedAssetEntryItem.Any(Function(i) i.RemissionSource = 3) Then
                        'Se liberan los items cruzados de la orden de compra 
                        For Each item As FixedAssetEntryItem In fixedAssetEntryItem
                            'Obtengo la cantidad a devolver
                            Dim quantityToAdjust = FixedAssetEntryDevolution.FixedAssetEntryDevolutionDetail.
                                   Where(Function(t) t.FixedAssetEntryItemId = item.Id).
                                   ToList().Count
                            Dim fixedAssetPurchaseOrderItem = _fixedAssetPurchaseOrderItem.GetFixedAssetPurchaseOrderItemById(item.PurchaseOrderItemId)
                            If fixedAssetPurchaseOrderItem IsNot Nothing Then
                                ' Ajusta las cantidades del item según la cantidad de devoluciones por item
                                fixedAssetPurchaseOrderItem.OutstandingQuantity += quantityToAdjust
                                fixedAssetPurchaseOrderItem.CancelledQuantity -= quantityToAdjust
                                fixedAssetPurchaseOrderItem.MarkAsModified()
                                _fixedAssetPurchaseOrderItem.SaveEntity(fixedAssetPurchaseOrderItem)
                                _fixedAssetPurchaseOrderItem.UnitWork.Commit()
                            End If
                        Next
                    End If

                    Transaction.Complete()
                    Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = FixedAssetEntryDevolutionNew, .Message = message.ToString}
                Catch ex As Exception
                    Transaction.Dispose()
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                    Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = Utils.GetInnerExceptionMessageToString(ex)}
                End Try
            End Using
        Catch ex As Exception
            Return New ActionResult(Of FixedAssetEntryDevolution) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Crean los comprobantes contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateJournalVouchers(FixedAssetEntryDevolution As FixedAssetEntryDevolution, FixedAssetEntry As FixedAssetEntry, SettingFixedAsset As SettingFixedAsset, audit As AuditMessage) As ActionResult(Of JournalVouchers)
        Dim JournalVouchers As New JournalVouchers
        Dim CostCenterId As Integer?

        'Se genera la cabecera del comprobante contable
        With JournalVouchers
            .IdJournalVoucher = SettingFixedAsset.DevolutionJournalVoucherId
            .VoucherDate = FixedAssetEntryDevolution.DocumentDate
            .Imported = False
            .Status = 2
            .Detail = "Comprobante contable generado por Devolución de Ingreso de Activos: " + FixedAssetEntryDevolution.Code
            .EntityId = FixedAssetEntryDevolution.Id
            .EntityCode = FixedAssetEntryDevolution.Code
            .EntityName = GetType(FixedAssetEntryDevolution).Name
            .IsClosedYear = False
            .CreationDate = Date.Now
            .CreationUser = audit.IdUser

            If FixedAssetEntry.LocationId IsNot Nothing AndAlso FixedAssetEntry.LocationId > 0 Then
                'La Localización es General
                Dim Location = _FixedAssetEntryRepository.GetFixedAssetLocationById(FixedAssetEntry.LocationId)
                CostCenterId = Location.FunctionalUnit.CostCenterId
            End If

            'Diccionario de catalogos de equipos
            Dim dictionaryItemCatalog As New Dictionary(Of Integer, FixedAssetItemCatalog)()
            'Catalogo de articulos
            Dim FixedAssetItemCatalog As FixedAssetItemCatalog = Nothing

            'Libro del catalogo que es utilizado para sacar las cuentas cuando el tipo de adquisición sea renting financiero
            Dim FixedAssetItemCatalogAdquisitionType As FixedAssetItemCatalogAdquisitionType = Nothing

            For Each entryDevolutionDetail As FixedAssetEntryDevolutionDetail In FixedAssetEntryDevolution.FixedAssetEntryDevolutionDetail
                'Obtengo el FixedAssetEntryItem
                Dim FixedAssetEntryItemDetail = _FixedAssetEntryDevolutionRepository.GetFixedAssetEntryItemDetailById(entryDevolutionDetail.FixedAssetEntryItemDetailId)
                'Obtengo el FixedAssetItem
                Dim FixedAssetItem = FixedAssetEntryItemDetail.FixedAssetEntryItem.FixedAssetItem

                'Si el catalogo no existe en el diccionario los consulto y lo agrego
                If Not dictionaryItemCatalog.ContainsKey(FixedAssetItem.ItemCatalogId) Then
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
                        Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "El catálogo " + FixedAssetItemCatalog.Code + " - " + FixedAssetItemCatalog.Description + " no tiene parametrizado el libro oficial"}
                    End If
                End If

                Dim IdCreditAccount As Integer
                Dim IdDebitAccount As Integer

                If FixedAssetEntry.AdquisitionType = 3 Then
                    'Comodato
                    IdDebitAccount = FixedAssetItemCatalog.CreditLoanAccountId
                    IdCreditAccount = FixedAssetItemCatalog.DebitLoanAccountId
                ElseIf FixedAssetEntry.AdquisitionType = 4 Then
                    'Donacion
                    IdDebitAccount = SettingFixedAsset.DonationMainAccountId
                    IdCreditAccount = FixedAssetItemCatalog.IncomeAccountId
                ElseIf FixedAssetEntry.AdquisitionType = 5 Then
                    'Traspaso de Bienes
                    IdDebitAccount = SettingFixedAsset.TransferPropertyMainAccountId
                    IdCreditAccount = FixedAssetItemCatalog.IncomeAccountId
                ElseIf FixedAssetEntry.AdquisitionType = 6 Then
                    'Otros Conceptos
                    IdDebitAccount = SettingFixedAsset.OtherConceptsMainAccountId
                    IdCreditAccount = FixedAssetItemCatalog.IncomeAccountId
                ElseIf FixedAssetEntry.AdquisitionType = 9 Then
                    'Renting Financiero
                    'Para la devolución, se invierte la contabilización del ingreso
                    'Crédito: La cuenta del libro oficial que se usó en el ingreso (activo)
                    IdCreditAccount = FixedAssetItemCatalogAdquisitionType.MainAccountId
                    'Débito: La cuenta de renting financiero del catálogo (pasivo)
                    If Not FixedAssetItemCatalog.FinancialRentingAccountId.HasValue Then
                        Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "El catálogo " + FixedAssetItemCatalog.Code + " - " + FixedAssetItemCatalog.Description + " no tiene parametrizada la cuenta de Renting Financiero"}
                    End If
                    IdDebitAccount = FixedAssetItemCatalog.FinancialRentingAccountId.Value
                End If

                For i As Integer = 0 To 1
                    Dim JournalVouchersDetail As New JournalVoucherDetails
                    With JournalVouchersDetail
                        If i = 0 Then
                            .IdMainAccount = IdDebitAccount
                            .DebitValue = entryDevolutionDetail.TotalValue
                        Else
                            .IdMainAccount = IdCreditAccount
                            .CreditValue = entryDevolutionDetail.TotalValue
                        End If
                        .Detail = "Detalle del Comprobante Contable generado por la Devolución de Ingreso de Activos: " + FixedAssetEntryDevolution.Code
                        .IdThirdParty = _supplierRepository.GetSupplierById(FixedAssetEntry.SupplierId, False).IdThirdParty
                        .IdCostCenter = CostCenterId
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
    ''' Se genera la cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GeneratePaymentNote(FixedAssetEntryDevolution As FixedAssetEntryDevolution, FixedAssetEntry As FixedAssetEntry, SettingFixedAsset As SettingFixedAsset, ByRef AccountPayable As AccountPayable, ListVieBot As List(Of VieBot)) As ActionResult(Of PaymentNotes)
        Dim PaymentNotes As New PaymentNotes
        Dim idCostCenter As Integer?
        Dim FixedAssetEntryControl = _FixedAssetEntryRepository.GetFixedAssetEntrySimpleById(FixedAssetEntryDevolution.FixedAssetEntryId)

        'La Nota al igual que la cuenta por pagar se realiza con la configuración del libro oficial
        Dim SettingFixedAssetLegalBookOfficial = SettingFixedAsset.SettingFixedAssetByLegalBook.Where(Function(d) d.LegalBook.OfficialBook = True AndAlso d.LegalBook.Status = True).FirstOrDefault()
        Dim officialCurrency = _companySettingsRepository.FirstOrDefault(Function(x) True, False, {"Currency"})?.Currency

        'Se genera la cabecera de la nota de pagos
        With PaymentNotes
            .NoteDate = FixedAssetEntryDevolution.DocumentDate
            .Status = 1
            .IdSupplier = FixedAssetEntry.SupplierId
            .IdSupplierDistributionLines = FixedAssetEntry.SupplierDistributionLineId
            .IdCostCenter = Nothing 'Preguntar de donde se saca
            .Comment = "Nota Débito generada por Devolución de Ingreso de Activos: " + FixedAssetEntryDevolution.Code
            .Nature = 1 'Debito
            .Reinstatement = False
            .IdVoucherTransaction = Nothing
            .IndicatesBillAdvance = 0
            .CancelCheck = Nothing
            .BudgetInterface = False
            .IdOperatingUnit = FixedAssetEntryDevolution.OperatingUnitId
            .JournalVoucherId = SettingFixedAsset.DevolutionJournalVoucherId
            .EntityId = FixedAssetEntryDevolution.Id
            .EntityCode = FixedAssetEntryDevolution.Code
            .EntityName = GetType(FixedAssetEntryDevolution).Name
            .CurrencyId = officialCurrency?.Id
            'Se crean los detalles de la nota con los articulos de cada detalle de la rejilla

            'Tercero
            Dim ThirdPartyId = AccountPayable.IdThirdParty

            'Proveedor
            Dim supplier As Supplier = _supplierRepository.GetSupplierById(FixedAssetEntry.SupplierId)

            'Catalogo de articulos
            Dim FixedAssetItemCatalog As FixedAssetItemCatalog = Nothing

            'Diccionario de catalogos de equipos
            Dim dictionaryItemCatalog As New Dictionary(Of Integer, FixedAssetItemCatalog)()

            'Concepto de cxp
            Dim AccountPayableConcepts As AccountPayableConcepts

            'Detalle de la nota
            Dim PaymentsNoteDetails As PaymentsNoteDetails = Nothing

            'PaymentsNoteDetails
            Dim ListPaymentsNoteDetails As New List(Of PaymentsNoteDetails)

            'IVA
            Dim GeneralLedgerIVA As GeneralLedgerIVA = Nothing

            'Lista que guarda los conceptos generados para contabilizar el IVA en los libros 
            Dim ListItemsIVA As New List(Of PaymentsNoteDetails)

            'Diccionario que guarda las tarifas de IVA
            Dim DictionaryIVA As New Dictionary(Of Integer, GeneralLedgerIVA)()

            'Diccionario que guarda los conceptos dependiendo de la clase de localizacion y si lleva o no el iva al costo
            Dim DictionaryLocation As New Dictionary(Of Integer, PaymentsNoteDetails)()

            'Valor usado para ajustar el valor de los detalles de manera que coincidan con el subtotal y el iva de la cabecera
            Dim DictionarySumValues As New Dictionary(Of Integer, ControlDetails)()

            FixedAssetEntryDevolution.FixedAssetEntryDevolutionDetail.ToList.ForEach(Sub(entryDevolutionDetail)

                                                                                         'Obtengo el FixedAssetEntryItem
                                                                                         Dim FixedAssetEntryItemDetail = _FixedAssetEntryDevolutionRepository.GetFixedAssetEntryItemDetailById(entryDevolutionDetail.FixedAssetEntryItemDetailId)
                                                                                         Dim FixedAssetEntryItem = FixedAssetEntryItemDetail.FixedAssetEntryItem

                                                                                         'Obtengo el FixedAssetItem
                                                                                         Dim FixedAssetItem = FixedAssetEntryItemDetail.FixedAssetEntryItem.FixedAssetItem
                                                                                         If Not dictionaryItemCatalog.ContainsKey(FixedAssetItem.ItemCatalogId) Then 'Si el catalogo no existe en el diccionario los consulto y lo agrego
                                                                                             FixedAssetItemCatalog = _FixedAssetEntryRepository.GetFixedAssetItemCatalogById(FixedAssetItem.ItemCatalogId)
                                                                                             dictionaryItemCatalog.Add(FixedAssetItem.ItemCatalogId, FixedAssetItemCatalog)
                                                                                         Else 'Si el catalogo ya existe en el diccionario lo obtengo del diccionario
                                                                                             FixedAssetItemCatalog = dictionaryItemCatalog(FixedAssetItem.ItemCatalogId)
                                                                                         End If

                                                                                         If FixedAssetEntryItem.SubTotalValue > 0 Then 'Si el valor del subtotal del articulo es mayor a cero
                                                                                             'Se saca los valores para asignarselos a cada item generado dependiendo si lleva o no el iva al costo
                                                                                             Dim BaseValueWithoutIva As Decimal = entryDevolutionDetail.SubTotalValue - entryDevolutionDetail.DiscountValue

                                                                                             'Se consulta la localizacion
                                                                                             Dim Location = _FixedAssetEntryRepository.GetFixedAssetLocationById(FixedAssetEntryItemDetail.LocationId)
                                                                                             idCostCenter = Location?.FunctionalUnit?.CostCenterId
                                                                                             'De acuerdo al tipo de adquisición y la localización se asigna la cuenta contable
                                                                                             Dim IdAccount As Integer
                                                                                             If FixedAssetEntry.AdquisitionType = 7 Then 'Si el tipo de adquisicion es de leasing financiero
                                                                                                 IdAccount = FixedAssetItemCatalog.IncomeLeasingAccountId
                                                                                             Else 'Cuando es diferente a leasing financiero
                                                                                                 Select Case Location.Class
                                                                                                     Case 1, 2 'Administrativo, Operativo
                                                                                                         IdAccount = FixedAssetItemCatalog.IncomeAccountId
                                                                                                     Case 3 'Mantenimiento
                                                                                                         IdAccount = FixedAssetItemCatalog.MaintenanceAssetsMainAccountId
                                                                                                     Case 4 'Almacén y Bodega
                                                                                                         IdAccount = FixedAssetItemCatalog.WarehouseAssetsMainAccountId
                                                                                                 End Select
                                                                                             End If

                                                                                             If Not DictionaryLocation.ContainsKey(IdAccount) Then 'Si no esta en el diccionario se crea uno nuevo
                                                                                                 PaymentsNoteDetails = New PaymentsNoteDetails
                                                                                                 PaymentsNoteDetails.IdAccountPayableConceptNotes = SettingFixedAsset.RefundAccountPayableConceptNoteId
                                                                                                 PaymentsNoteDetails.IdAccount = IdAccount
                                                                                                 PaymentsNoteDetails.IdThirdParty = ThirdPartyId
                                                                                                 PaymentsNoteDetails.Nature = 2 'Crédito
                                                                                                 PaymentsNoteDetails.BaseValue = BaseValueWithoutIva
                                                                                                 PaymentsNoteDetails.Value = PaymentsNoteDetails.BaseValue
                                                                                                 PaymentsNoteDetails.Comments = "Detalle de notas débito/crédito generada por Devolución de Ingreso de Activos 'Valor Bruto Articulo'"
                                                                                                 PaymentsNoteDetails.TaxRegistration = FixedAssetEntry.TaxRegistration
                                                                                                 PaymentsNoteDetails.IdCostCenter = If(_pucAdminService.MainAccountHandlesCostCenter(PaymentsNoteDetails.IdAccount), idCostCenter, Nothing)

                                                                                                 Select Case FixedAssetEntry.TaxRegistration
                                                                                                     Case 1, 4
                                                                                                         PaymentsNoteDetails.DiscountableIVA = 0
                                                                                                     Case 2
                                                                                                         PaymentsNoteDetails.DiscountableIVA = 1
                                                                                                 End Select

                                                                                                 If entryDevolutionDetail.IvaValue > 0 Then
                                                                                                     PaymentsNoteDetails.IdGeneralLedgerIVA = FixedAssetEntryItem.IVAId
                                                                                                     PaymentsNoteDetails.IVAValue = entryDevolutionDetail.IvaValue
                                                                                                 End If

                                                                                                 PaymentsNoteDetails.TotalConceptValue = PaymentsNoteDetails.BaseValue + If(PaymentsNoteDetails.IVAValue, 0)
                                                                                                 DictionaryLocation.Add(IdAccount, PaymentsNoteDetails)

                                                                                             Else 'Si esta en el diccionario se actualizan los campos
                                                                                                 PaymentsNoteDetails = DictionaryLocation(IdAccount)
                                                                                                 PaymentsNoteDetails.BaseValue += BaseValueWithoutIva
                                                                                                 PaymentsNoteDetails.Value = PaymentsNoteDetails.BaseValue
                                                                                                 PaymentsNoteDetails.IVAValue += entryDevolutionDetail.IvaValue
                                                                                                 PaymentsNoteDetails.TotalConceptValue = PaymentsNoteDetails.BaseValue + If(PaymentsNoteDetails.IVAValue, 0)
                                                                                             End If
                                                                                         End If

                                                                                         If entryDevolutionDetail.IvaValue > 0 Then

                                                                                             If Not DictionaryIVA.ContainsKey(FixedAssetItem.IVAId) Then
                                                                                                 GeneralLedgerIVA = _generalLedgerIVARepository.GetGeneralLedgerIVAById(FixedAssetItem.IVAId)

                                                                                                 If Not GeneralLedgerIVA?.IdAccountPurchaseService.HasValue OrElse
                                                                                                    Not GeneralLedgerIVA?.IdAccountDebitControlFiscal.HasValue OrElse
                                                                                                    Not GeneralLedgerIVA?.IdAccountCreditControlFiscal.HasValue Then
                                                                                                     Throw New IndigoValidationException($"La tarifa de IVA ({GeneralLedgerIVA.Code} - {GeneralLedgerIVA.Name}) no tiene una o más cuentas parametrizadas necesarias")
                                                                                                 End If

                                                                                                 DictionaryIVA.Add(FixedAssetItem.IVAId, GeneralLedgerIVA)
                                                                                             Else
                                                                                                 GeneralLedgerIVA = DictionaryIVA(FixedAssetItem.IVAId)
                                                                                             End If

                                                                                             Dim oldIdAccount = PaymentsNoteDetails.IdAccount
                                                                                             PaymentsNoteDetails = New PaymentsNoteDetails
                                                                                             If FixedAssetEntry.TaxRegistration = 1 Then
                                                                                                 PaymentsNoteDetails.IdAccount = GeneralLedgerIVA.IdAccountDebitControlFiscal
                                                                                             ElseIf FixedAssetEntry.TaxRegistration = 2 Then
                                                                                                 PaymentsNoteDetails.IdAccount = GeneralLedgerIVA.IdAccountPurchaseService
                                                                                             ElseIf FixedAssetEntry.TaxRegistration = 4 Then
                                                                                                 PaymentsNoteDetails.IdAccount = oldIdAccount
                                                                                             End If

                                                                                             PaymentsNoteDetails.IdThirdParty = ThirdPartyId
                                                                                             PaymentsNoteDetails.Nature = 2 'Credito
                                                                                             PaymentsNoteDetails.BaseValue = entryDevolutionDetail.IvaValue
                                                                                             PaymentsNoteDetails.BillingValue = PaymentsNoteDetails.BaseValue
                                                                                             PaymentsNoteDetails.Value = PaymentsNoteDetails.BaseValue
                                                                                             PaymentsNoteDetails.IdCostCenter = If(_pucAdminService.MainAccountHandlesCostCenter(PaymentsNoteDetails.IdAccount), idCostCenter, Nothing)
                                                                                             PaymentsNoteDetails.Comments = "Detalle de notas débito/crédito generada por Devolución de Ingreso de Activos 'IVA Articulo'"

                                                                                             ListItemsIVA.Add(PaymentsNoteDetails)

                                                                                             If FixedAssetEntry.TaxRegistration = 1 Then
                                                                                                 PaymentsNoteDetails = New PaymentsNoteDetails
                                                                                                 PaymentsNoteDetails.IdAccount = GeneralLedgerIVA.IdAccountCreditControlFiscal
                                                                                                 PaymentsNoteDetails.IdThirdParty = ThirdPartyId
                                                                                                 PaymentsNoteDetails.Nature = 1 'Debito
                                                                                                 PaymentsNoteDetails.BaseValue = entryDevolutionDetail.IvaValue
                                                                                                 PaymentsNoteDetails.BillingValue = PaymentsNoteDetails.BaseValue
                                                                                                 PaymentsNoteDetails.Value = PaymentsNoteDetails.BaseValue
                                                                                                 PaymentsNoteDetails.IdCostCenter = If(_pucAdminService.MainAccountHandlesCostCenter(PaymentsNoteDetails.IdAccount), idCostCenter, Nothing)
                                                                                                 PaymentsNoteDetails.Comments = "Detalle de cuenta por pagar generada por Ingreso de Activos 'IVA Articulo'"

                                                                                                 ListItemsIVA.Add(PaymentsNoteDetails)
                                                                                             End If
                                                                                         End If

                                                                                         '-----------------------------------------Separador-----------------------------------------------------

                                                                                         Dim RTFValue = entryDevolutionDetail.RTFValue
                                                                                         If RTFValue > 0 Then 'Si el articulo tiene retefuente
                                                                                             'Se obtiene el concepto de retencion si esta en el listado y se suma
                                                                                             Dim AccountPayableConceptId As Integer
                                                                                             If supplier.Declarant Then 'Si el proveedor es declarante
                                                                                                 AccountPayableConceptId = FixedAssetItemCatalog.DeclarantRetentionAccountPayableConceptId
                                                                                             Else 'Si no es declarante
                                                                                                 AccountPayableConceptId = FixedAssetItemCatalog.NotDeclarantRetentionAccountPayableConceptId
                                                                                             End If
                                                                                             AccountPayableConcepts = _paymentsConceptRepository.GetPaymentConceptByIdSimple(AccountPayableConceptId)

                                                                                             PaymentsNoteDetails = .PaymentsNoteDetails.Where(Function(item) item.IdRetentionConcept IsNot Nothing AndAlso item.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId).FirstOrDefault
                                                                                             If PaymentsNoteDetails IsNot Nothing Then
                                                                                                 'Modificacion del detalle, este cambio afecta el detalle de la lista con y sin iva al costo
                                                                                                 PaymentsNoteDetails.BaseValue += FixedAssetEntryItem.SubTotalValue
                                                                                                 PaymentsNoteDetails.BillingValue += FixedAssetEntryItem.SubTotalValue
                                                                                                 PaymentsNoteDetails.Value += RTFValue
                                                                                                 PaymentsNoteDetails.TotalConceptValue += RTFValue
                                                                                             Else
                                                                                                 PaymentsNoteDetails = New PaymentsNoteDetails
                                                                                                 PaymentsNoteDetails.IdAccountPayableConceptNotes = SettingFixedAsset.RefundAccountPayableConceptNoteId
                                                                                                 PaymentsNoteDetails.IdAccount = AccountPayableConcepts.IdAccount
                                                                                                 PaymentsNoteDetails.IdThirdParty = ThirdPartyId
                                                                                                 PaymentsNoteDetails.Nature = 1 'Debito
                                                                                                 PaymentsNoteDetails.BaseValue = FixedAssetEntryItem.SubTotalValue
                                                                                                 PaymentsNoteDetails.BillingValue = FixedAssetEntryItem.SubTotalValue
                                                                                                 PaymentsNoteDetails.Value = RTFValue
                                                                                                 PaymentsNoteDetails.TotalConceptValue = RTFValue
                                                                                                 PaymentsNoteDetails.IdCostCenter = If(_pucAdminService.MainAccountHandlesCostCenter(PaymentsNoteDetails.IdAccount), idCostCenter, Nothing)
                                                                                                 PaymentsNoteDetails.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                                                                                                 PaymentsNoteDetails.Percentage = FixedAssetEntryItem.RTFPercentage
                                                                                                 PaymentsNoteDetails.Comments = "Detalle de notas débito/crédito generada por Devolución de Ingreso de Activos 'RTF Articulo'"
                                                                                                 .PaymentsNoteDetails.Add(PaymentsNoteDetails)

                                                                                                 ListPaymentsNoteDetails.Add(PaymentsNoteDetails)
                                                                                             End If
                                                                                         End If
                                                                                     End Sub)

            'Se agrega lo que tenga el diccionario a la entidad de nota
            For Each item In DictionaryLocation
                ListPaymentsNoteDetails.Add(item.Value)
                .PaymentsNoteDetails.Add(item.Value)
            Next

            'Se redondean los valores, Actualmente el campo Value de la tabla no maneja decimales, por tal razon se redondean todos
            .PaymentsNoteDetails.ToList.ForEach(Sub(item) item.Value = Utils.RoundValue(item.Value, Utils.RoundLevel.TwoDecimal))
            ListPaymentsNoteDetails.ToList.ForEach(Sub(item) item.Value = Utils.RoundValue(item.Value, Utils.RoundLevel.TwoDecimal))

            If FixedAssetEntryDevolution.WithholdingTax > 0 Then 'Si tiene retencion del iva
                PaymentsNoteDetails = New PaymentsNoteDetails
                If SettingFixedAsset.IVARetention = 1 Then 'Se saca la retencion del iva del tercero
                    AccountPayableConcepts = _FixedAssetEntryRepository.GetAccountPayableConceptBySupplierDistributionLineId(FixedAssetEntry.SupplierDistributionLineId)
                    If AccountPayableConcepts Is Nothing Then
                        Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .ObjectEmbbeded = Nothing, .Message = "El ingreso aplica retención de IVA, pero no se encuentra parametrizado correctamente en relación con el proveedor y la línea de distribución"}
                    End If
                    PaymentsNoteDetails.IdAccountPayableConceptNotes = SettingFixedAsset.RefundAccountPayableConceptNoteId
                    PaymentsNoteDetails.IdAccount = AccountPayableConcepts.IdAccount
                    PaymentsNoteDetails.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                    PaymentsNoteDetails.Percentage = AccountPayableConcepts.RetentionConcepts.Rate
                Else 'Se saca la retencion del iva del concepto
                    AccountPayableConcepts = _paymentsConceptRepository.GetPaymentConceptByIdSimple(SettingFixedAsset.IVARetentionAccountPayableConceptId)
                    If AccountPayableConcepts Is Nothing Then
                        Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .ObjectEmbbeded = Nothing, .Message = "El ingreso aplica retención de IVA, pero no se encuentra parametrizado correctamente con el concepto"}
                    End If
                    PaymentsNoteDetails.IdAccountPayableConceptNotes = SettingFixedAsset.RefundAccountPayableConceptNoteId
                    PaymentsNoteDetails.IdAccount = AccountPayableConcepts.IdAccount
                    PaymentsNoteDetails.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                    PaymentsNoteDetails.Percentage = AccountPayableConcepts.RetentionConcepts.Rate
                End If

                PaymentsNoteDetails.IdThirdParty = ThirdPartyId
                PaymentsNoteDetails.TaxRegistration = FixedAssetEntry.TaxRegistration
                PaymentsNoteDetails.Nature = 1 'Debito
                PaymentsNoteDetails.BaseValue = FixedAssetEntryDevolution.ValueTax
                PaymentsNoteDetails.BillingValue = FixedAssetEntryDevolution.Value
                PaymentsNoteDetails.Value = Utils.RoundValue(FixedAssetEntryDevolution.WithholdingTax, Utils.RoundLevel.TwoDecimal)
                PaymentsNoteDetails.TotalConceptValue = Utils.RoundValue(FixedAssetEntryDevolution.WithholdingTax, Utils.RoundLevel.TwoDecimal)
                PaymentsNoteDetails.IdCostCenter = If(_pucAdminService.MainAccountHandlesCostCenter(PaymentsNoteDetails.IdAccount), idCostCenter, Nothing)
                PaymentsNoteDetails.Comments = "Detalle de notas débito/crédito generada por Devolución de Ingreso de Activos 'Retención IVA'"
                .PaymentsNoteDetails.Add(PaymentsNoteDetails)

                ListPaymentsNoteDetails.Add(PaymentsNoteDetails)
            End If

            If FixedAssetEntryDevolution.WithholdingICA > 0 Then 'Si tiene retencion ica
                PaymentsNoteDetails = New PaymentsNoteDetails
                AccountPayableConcepts = _paymentsConceptRepository.GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitIdPay(FixedAssetEntry.SupplierDistributionLineId, FixedAssetEntry.OperatingUnitId)
                If AccountPayableConcepts Is Nothing Then
                    Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .ObjectEmbbeded = Nothing, .Message = "El ingreso aplica retención de ICA, pero no se encuentra parametrizado correctamente en relación con el proveedor y la línea de distribución"}
                End If
                PaymentsNoteDetails.IdAccountPayableConceptNotes = SettingFixedAsset.RefundAccountPayableConceptNoteId
                PaymentsNoteDetails.IdAccount = AccountPayableConcepts.IdAccount
                PaymentsNoteDetails.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                PaymentsNoteDetails.IdThirdParty = ThirdPartyId
                PaymentsNoteDetails.TaxRegistration = FixedAssetEntry.TaxRegistration
                PaymentsNoteDetails.Nature = 1 'Debito
                PaymentsNoteDetails.BaseValue = Utils.RoundValue(FixedAssetEntryDevolution.Value, Utils.RoundLevel.TwoDecimal)
                PaymentsNoteDetails.BillingValue = Utils.RoundValue(FixedAssetEntryDevolution.Value, Utils.RoundLevel.TwoDecimal)
                PaymentsNoteDetails.Value = Utils.RoundValue(FixedAssetEntryDevolution.WithholdingICA, Utils.RoundLevel.TwoDecimal)
                PaymentsNoteDetails.TotalConceptValue = Utils.RoundValue(FixedAssetEntryDevolution.WithholdingICA, Utils.RoundLevel.TwoDecimal)
                PaymentsNoteDetails.IdCostCenter = If(_pucAdminService.MainAccountHandlesCostCenter(PaymentsNoteDetails.IdAccount), idCostCenter, Nothing)
                PaymentsNoteDetails.Percentage = AccountPayableConcepts.RetentionConcepts.Rate
                PaymentsNoteDetails.Comments = "Detalle de notas débito/crédito generada por Devolución  de Ingreso de Activos 'Retención ICA'"
                .PaymentsNoteDetails.Add(PaymentsNoteDetails)

                ListPaymentsNoteDetails.Add(PaymentsNoteDetails)
            End If

            If FixedAssetEntryDevolution.FreightValue > 0 Then 'Si tiene flete
                PaymentsNoteDetails = New PaymentsNoteDetails
                AccountPayableConcepts = _paymentsConceptRepository.GetPaymentConceptByIdSimple(SettingFixedAsset.FreightAccountPayableConceptId)
                If AccountPayableConcepts Is Nothing Then
                    Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .ObjectEmbbeded = Nothing, .Message = "El ingreso aplica flete, pero no se encuentra parametrizado con el concepto correspondiente"}
                End If
                PaymentsNoteDetails.IdAccountPayableConceptNotes = SettingFixedAsset.RefundAccountPayableConceptNoteId
                PaymentsNoteDetails.IdAccount = AccountPayableConcepts.IdAccount
                PaymentsNoteDetails.IdRetentionConcept = Nothing
                PaymentsNoteDetails.IdThirdParty = ThirdPartyId
                PaymentsNoteDetails.TaxRegistration = FixedAssetEntry.TaxRegistration
                PaymentsNoteDetails.Nature = 2 'Credito
                PaymentsNoteDetails.BaseValue = FixedAssetEntryDevolution.FreightValue
                PaymentsNoteDetails.BillingValue = FixedAssetEntryDevolution.FreightValue
                PaymentsNoteDetails.Value = Utils.RoundValue(FixedAssetEntryDevolution.FreightValue, Utils.RoundLevel.TwoDecimal)
                PaymentsNoteDetails.IdCostCenter = If(_pucAdminService.MainAccountHandlesCostCenter(PaymentsNoteDetails.IdAccount), idCostCenter, Nothing)
                PaymentsNoteDetails.Comments = "Detalle de notas débito/crédito generada por Devolución de Ingreso de Activos 'Flete'"
                .PaymentsNoteDetails.Add(PaymentsNoteDetails)

                ListPaymentsNoteDetails.Add(PaymentsNoteDetails)

                If FixedAssetEntryDevolution.FreightIVAValue > 0 Then 'Si el flete tiene iva
                    PaymentsNoteDetails = New PaymentsNoteDetails
                    AccountPayableConcepts = _paymentsConceptRepository.GetPaymentConceptByIdSimple(SettingFixedAsset.IVAFreightAccountPayableConceptId)
                    If AccountPayableConcepts Is Nothing Then
                        Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .ObjectEmbbeded = Nothing, .Message = "El ingreso aplica flete y genera IVA, pero no se encuentra correctamente parametrizado con el concepto correspondiente"}
                    End If
                    PaymentsNoteDetails.IdAccountPayableConceptNotes = SettingFixedAsset.RefundAccountPayableConceptNoteId
                    PaymentsNoteDetails.IdAccount = AccountPayableConcepts.IdAccount
                    PaymentsNoteDetails.IdRetentionConcept = AccountPayableConcepts.RetentionConceptId
                    PaymentsNoteDetails.IdThirdParty = ThirdPartyId
                    PaymentsNoteDetails.TaxRegistration = FixedAssetEntry.TaxRegistration
                    PaymentsNoteDetails.Nature = 2 'Credito
                    PaymentsNoteDetails.BaseValue = FixedAssetEntryDevolution.FreightIVAValue
                    PaymentsNoteDetails.BillingValue = FixedAssetEntryDevolution.FreightIVAValue
                    PaymentsNoteDetails.Value = Utils.RoundValue(FixedAssetEntryDevolution.FreightIVAValue, Utils.RoundLevel.TwoDecimal)
                    PaymentsNoteDetails.IdCostCenter = If(_pucAdminService.MainAccountHandlesCostCenter(PaymentsNoteDetails.IdAccount), idCostCenter, Nothing)
                    PaymentsNoteDetails.Percentage = SettingFixedAsset.FreightIVAPercentage
                    PaymentsNoteDetails.Comments = "Detalle de notas débito/crédito generada por Devolución de Ingreso de Activos 'IVA Flete'"
                    .PaymentsNoteDetails.Add(PaymentsNoteDetails)

                    ListPaymentsNoteDetails.Add(PaymentsNoteDetails)
                End If
            End If

            For Each itemVieBot In ListVieBot
                If itemVieBot.LegalBookId <> SettingFixedAssetLegalBookOfficial.LegalBookId Then
                    If itemVieBot.Allow Then
                        If Not .PaymentsNoteDetailOthersNotHomologatedBooks.ContainsKey(itemVieBot.LegalBookId) Then
                            .PaymentsNoteDetailOthersNotHomologatedBooks.Add(itemVieBot.LegalBookId, New List(Of PaymentsNoteDetails))
                            .PaymentsNoteDetailOthersNotHomologatedBooks(itemVieBot.LegalBookId).Add(New PaymentsNoteDetails With {
                                                                                                        .IdAccount = AccountPayable.IdAccount,
                                                                                                        .IdThirdParty = AccountPayable.IdThirdParty,
                                                                                                        .IdCostCenter = AccountPayable.IdCostCenter,
                                                                                                        .Comments = AccountPayable.Coments,
                                                                                                        .Nature = 1,
                                                                                                        .Value = AccountPayable.Adjustment
                                                                                                     })
                        End If

                        Dim PaymentsNoteDetailOthersNotHomologatedBooks = .PaymentsNoteDetailOthersNotHomologatedBooks(itemVieBot.LegalBookId)
                        For Each item In ListPaymentsNoteDetails

                            If item.Nature = 2 Then
                                Select Case FixedAssetEntry.TaxRegistration
                                    Case 1
                                        item.Value = item.BaseValue + If(item.IVAValue, 0)
                                    Case 2, 4
                                        item.Value = item.TotalConceptValue - If(item.IVAValue, 0)
                                End Select
                            End If

                            PaymentsNoteDetailOthersNotHomologatedBooks.Add(item)
                        Next

                        Select Case FixedAssetEntry.TaxRegistration
                            Case 1
                                PaymentsNoteDetailOthersNotHomologatedBooks.AddRange(
                                ListItemsIVA.Where(Function(itemIVA) itemIVA.Nature = 1 Or itemIVA.Nature = 2)
                            )
                            Case 2, 4
                                'Se agregan solamente los de Naturaleza Crédito porque los Débitos están contemplados en el valor del ajuste de la CxP
                                PaymentsNoteDetailOthersNotHomologatedBooks.AddRange(
                                ListItemsIVA.Where(Function(itemIVA) itemIVA.Nature = 2)
                            )
                        End Select
                    End If
                End If
            Next
        End With

        Return New ActionResult(Of PaymentNotes) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = PaymentNotes}
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _notesDebitCreditAdminService.Dispose()
            End If
            _sequenceRepository = Nothing
            _FixedAssetEntryDevolutionRepository = Nothing
            _sequensePaymentsCRepository = Nothing
            _FixedAssetEntryRepository = Nothing
            _supplierRepository = Nothing
            _paymentsConceptRepository = Nothing
            _notesDebitCreditAdminService = Nothing
            _FixedAssetPurchaseOrderItem = Nothing
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

Public Class ControlDetails
    Public Property Ids() As New List(Of Integer)
    Public Property Value() As Decimal
End Class