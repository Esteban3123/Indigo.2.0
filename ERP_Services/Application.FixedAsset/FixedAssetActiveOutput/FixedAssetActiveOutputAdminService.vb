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

Public Class FixedAssetActiveOutputAdminService
    Implements IFixedAssetActiveOutputAdminService

#Region "Variables"

    'Repositorio de la aseguradora
    Private _FixedAssetActiveOutputRepository As IFixedAssetActiveOutputRepository

    'Repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    'Repositorio de entrada de activos
    Private _FixedAssetEntryRepository As IFixedAssetEntryRepository

    'Repositorio para el comprobante
    Private _accountingRepository As IAccountingDocumentAdminService

    'Repositorio para la secuencia de la cuenta por cobrar
    Private _sequensePortfolio As ISequensePortfolioCRepository

    'Repositorio para la aplicación de cxc
    Private _accountReceivableAdminService As IAccountReceivableAdminService

    'Repositorio para el Physical
    Private _fixedAssetPhysicalAssetRepository As IFixedAssetPhysicalAssetRepository

    Private _legalBookRepository As IBookRepository

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
    Public Sub New(FixedAssetActiveOutputRepository As IFixedAssetActiveOutputRepository, sequenceRepository As IFixedAssetSequenceDetailRepository,
                   FixedAssetEntryRepository As IFixedAssetEntryRepository, accountingRepository As IAccountingDocumentAdminService, sequensePortfolio As ISequensePortfolioCRepository,
                   accountReceivableAdminService As IAccountReceivableAdminService, fixedAssetPhysicalAssetRepository As IFixedAssetPhysicalAssetRepository,
                   legalBookRepository As IBookRepository, pucAdminService As IPUCAdminService)
        If sequenceRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceRepository")
        End If
        If FixedAssetActiveOutputRepository Is Nothing Then
            Throw New ArgumentNullException("FixedAssetActiveOutputRepository")
        End If
        If FixedAssetEntryRepository Is Nothing Then
            Throw New ArgumentNullException("FixedAssetEntryRepository")
        End If
        If accountingRepository Is Nothing Then
            Throw New ArgumentNullException("accountingRepository")
        End If
        If sequensePortfolio Is Nothing Then
            Throw New ArgumentNullException("sequensePortfolio")
        End If
        If accountReceivableAdminService Is Nothing Then
            Throw New ArgumentNullException("accountReceivableAdminService")
        End If
        If fixedAssetPhysicalAssetRepository Is Nothing Then
            Throw New ArgumentNullException("fixedAssetPhysicalAssetRepository")
        End If
        If legalBookRepository Is Nothing Then
            Throw New ArgumentNullException("fixedAssetPhysicalAssetRepository")
        End If
        _sequenceRepository = sequenceRepository
        _FixedAssetActiveOutputRepository = FixedAssetActiveOutputRepository
        _FixedAssetEntryRepository = FixedAssetEntryRepository
        _accountingRepository = accountingRepository
        _sequensePortfolio = sequensePortfolio
        _accountReceivableAdminService = accountReceivableAdminService
        _fixedAssetPhysicalAssetRepository = fixedAssetPhysicalAssetRepository
        _legalBookRepository = legalBookRepository
        _pucAdminService = pucAdminService
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Confirma el registro
    ''' </summary>
    ''' <param name="FixedAssetActiveOutput"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmFixedAssetActiveOutput(FixedAssetActiveOutput As FixedAssetActiveOutput, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetActiveOutput) Implements IFixedAssetActiveOutputAdminService.ConfirmFixedAssetActiveOutput
        If FixedAssetActiveOutput Is Nothing Then
            Throw New ArgumentNullException("FixedAssetActiveOutput")
        End If

        'Se consulta si hay parámetros de activo fijo
        Dim settingFixedAsset As SettingFixedAsset = _FixedAssetEntryRepository.GetSettingFixedAssetByOperatingUnidId(FixedAssetActiveOutput.OperatingUnitId)
        If settingFixedAsset Is Nothing Then
            Return New ActionResult(Of FixedAssetActiveOutput) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe parámetros de activo fijo para la unidad operativa escogida."}
        End If
        'Se valida que la fecha de traslado este en el mismo mes que la fecha de parametros
        If FixedAssetActiveOutput.DocumentDate.Month <> settingFixedAsset.ProcessDate.Month Then
            Return New ActionResult(Of FixedAssetActiveOutput) With {.StatusCode = eStatusResult.WARNING, .Message = "El mes de la fecha de traslado(" + FixedAssetActiveOutput.DocumentDate.ToString("MMMM") + ") debe ser el mismo a la fecha de proceso de parámetros(" + settingFixedAsset.ProcessDate.ToString("MMMM") + ")"}
        End If

        'Se valida que los articulos que se van a confirmar no esten en una salida ya confirmada
        If FixedAssetActiveOutput.FixedAssetActiveOutputDetail IsNot Nothing AndAlso FixedAssetActiveOutput.FixedAssetActiveOutputDetail.Count > 0 Then
            Dim stringBuilder = _FixedAssetActiveOutputRepository.ValidateItemInOutput(FixedAssetActiveOutput.FixedAssetActiveOutputDetail.ToList())
            If stringBuilder.Length > 0 Then
                Return New ActionResult(Of FixedAssetActiveOutput) With {.StatusCode = eStatusResult.WARNING, .Message = stringBuilder}
            End If
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Se guarda o actualiza la salida de activos
                Dim resultSave As ActionResult(Of FixedAssetActiveOutput) = SaveFixedAssetActiveOutput(FixedAssetActiveOutput, audit, idSequense)
                If resultSave.StateResult = False Then
                    transaction.Dispose()
                    If resultSave.StatusCode = eStatusResult.EXCEPTION Then
                        Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = resultSave.Message}
                    ElseIf resultSave.StatusCode = eStatusResult.WARNING Then
                        Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultSave.Message}
                    End If
                End If

                'Se construye el mensaje que se devuelve al confirmar la salida de activos
                Dim message As New StringBuilder
                message.AppendLine("El registro se guardó con código: " + resultSave.ObjectEmbbeded.Code)

                'Se recalcula la clasificación real de cada activo desde su catálogo (fuente de verdad en BD),
                'ya que ClassificationFixedAsset puede llegar sin diligenciar desde el cliente cuando el objeto
                'no pasó antes por GetFixedAssetActiveOutput, lo que anulaba silenciosamente esta validación
                For Each item In FixedAssetActiveOutput.FixedAssetActiveOutputDetail
                    If item.PhysicalAssetId IsNot Nothing Then
                        Dim physicalForClassification = _FixedAssetActiveOutputRepository.GetFixedAssetPhysicalAssetById(item.PhysicalAssetId.Value)
                        item.ClassificationFixedAsset = physicalForClassification.FixedAssetItem.FixedAssetItemCatalog.Classification
                    End If
                Next

                Dim ThereUncontrollableAssets As Boolean = FixedAssetActiveOutput.FixedAssetActiveOutputDetail.Any(Function(item) item.ClassificationFixedAsset = 3)
                If ThereUncontrollableAssets Then
                    If Not FixedAssetActiveOutput.FixedAssetActiveOutputDetail.All(Function(item) item.ClassificationFixedAsset = FixedAssetActiveOutput.FixedAssetActiveOutputDetail.FirstOrDefault()?.ClassificationFixedAsset) Then
                        Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "No todos los activos/partes tienen la misma clasificación"}
                    End If
                End If
                'Se generan los comprobantes contables
                If Not ThereUncontrollableAssets Then

                    Dim resultJournalVouchers = CreateJournalVouchers(resultSave.ObjectEmbbeded, settingFixedAsset)
                    If resultJournalVouchers.StateResult = False Then
                        transaction.Dispose()
                        Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultJournalVouchers.Message, .MessageResult = resultJournalVouchers.MessageResult}
                    End If

                    Dim toDoJournalVoucher = False
                    Dim ListJournalVouchers = resultJournalVouchers.ObjectEmbbeded
                    If ListJournalVouchers IsNot Nothing AndAlso ListJournalVouchers.Count > 0 Then
                        'Si es un comodato tercerizado o un renting operativo no debe contabilizar
                        For Each activeOutputDetail In resultSave.ObjectEmbbeded.FixedAssetActiveOutputDetail.ToList
                            Dim physicalAsset = _FixedAssetActiveOutputRepository.GetFixedAssetPhysicalAssetById(activeOutputDetail.PhysicalAssetId)
                            If physicalAsset.AdquisitionType <> 8 AndAlso physicalAsset.AdquisitionType <> 10 Then
                                toDoJournalVoucher = True
                                Exit For
                            End If
                        Next
                    Else
                        'Verifico si existe algun activo que debía contabilizar
                        For Each activeOutputDetail In FixedAssetActiveOutput.FixedAssetActiveOutputDetail.ToList
                            'Valido si es un activo y no una parte
                            If activeOutputDetail.PhysicalAssetId IsNot Nothing Then
                                Dim physicalAsset = _FixedAssetActiveOutputRepository.GetFixedAssetPhysicalAssetById(activeOutputDetail.PhysicalAssetId)
                                If physicalAsset.AdquisitionType <> 8 AndAlso physicalAsset.AdquisitionType <> 10 Then
                                    transaction.Dispose()
                                    Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "No se generaron los comprobantes contables"}
                                    Exit For
                                End If
                            End If
                        Next
                    End If

                    If toDoJournalVoucher Then
                        Dim consecutivesJournalVoucher As String = String.Empty
                        Dim counter As Integer = 0
                        'Se recorren los comprobantes generados para guardarlos
                        For Each journalVoucher In ListJournalVouchers
                            'Se guarda el comprobante
                            Dim resultAccounting As ActionMessageResult(Of JournalVouchers) = _accountingRepository.SaveAccountingDocument(journalVoucher, audit)
                            If resultAccounting.StateResult = False Then
                                transaction.Dispose()
                                Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultAccounting.Message}
                            End If

                            If counter <> 0 Then
                                consecutivesJournalVoucher += ","
                            Else
                                counter = 1
                            End If
                            consecutivesJournalVoucher += resultAccounting.ObjectEmbbeded.Consecutive.ToString
                        Next

                        'Se consulta que tipo de comprobante fue que genero
                        Dim journalVoucherType As JournalVoucherTypes = _FixedAssetEntryRepository.GetJournalVoucherType(settingFixedAsset.IdOutputAccountingVoucher)
                        message.AppendLine("Se generaron comprobantes contables " + journalVoucherType.Name.ToString + ": " + consecutivesJournalVoucher)

                        'Se genera las cuentas por cobrar siempre y cuando los detalles sean de tipo venta o perdida reposición
                        Dim ListAccountReceivable = CreateAccountReceivable(resultSave.ObjectEmbbeded, settingFixedAsset)
                        If ListAccountReceivable IsNot Nothing AndAlso ListAccountReceivable.Count > 0 Then 'Si el listado trae items es porque se va a guardar las cxc
                            'Se consulta la secuencia numérica de cxc para poder guardar
                            Dim sequense = _sequensePortfolio.GetSequenseByIdForm("682")
                            If sequense.Id = 0 OrElse sequense.PortfolioSequenceDetail Is Nothing OrElse sequense.PortfolioSequenceDetail.Count = 0 Then
                                transaction.Dispose()
                                Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "No existe secuencia numérica para generar las cuentas por cobrar"}
                            End If

                            Dim codesAccountReceivable As String = String.Empty
                            counter = 0
                            'Se recorren las cxc generadas para guardarlas
                            For Each AccountReceivable In ListAccountReceivable
                                'Se guarda la cxc
                                Dim resultAccountReceivable = _accountReceivableAdminService.SaveAccountReceivable(AccountReceivable, audit, sequense.PortfolioSequenceDetail(0).Id, True)
                                If resultAccountReceivable.StateResult = False Then
                                    transaction.Dispose()
                                    Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultAccountReceivable.Message}
                                End If

                                If counter <> 0 Then
                                    codesAccountReceivable += ","
                                Else
                                    counter = 1
                                End If
                                codesAccountReceivable += resultAccountReceivable.ObjectEmbbeded.Code.ToString
                            Next
                            message.AppendLine("Se generaron las cuentas por cobrar: " + codesAccountReceivable)
                        End If
                    End If
                Else
                    'Se actualiza el estado del activo y la fecha de salida
                    'Entidad del activo
                    Dim physicalAsset As FixedAssetPhysicalAsset = Nothing
                    Dim UnitOfWorkPhysical As IUnitWork = _fixedAssetPhysicalAssetRepository.UnitWork
                    For Each activeOutputDetail In resultSave.ObjectEmbbeded.FixedAssetActiveOutputDetail.ToList
                        'Obtengo el physicalAsset
                        physicalAsset = _FixedAssetActiveOutputRepository.GetFixedAssetPhysicalAssetById(activeOutputDetail.PhysicalAssetId, True)
                        physicalAsset.Status = 0
                        physicalAsset.HasOutput = True
                        physicalAsset.OutputDate = FixedAssetActiveOutput.DocumentDate
                        physicalAsset.MarkAsModified()
                        _fixedAssetPhysicalAssetRepository.SaveEntity(physicalAsset)
                        UnitOfWorkPhysical.Commit()
                    Next

                    message.AppendLine("Se confirmó el registro con código: " + resultSave.ObjectEmbbeded.Code)
                End If
                transaction.Complete()
                Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = True, .ObjectEmbbeded = FixedAssetActiveOutput, .StatusCode = eStatusResult.SUCCESS, .Message = message.ToString}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .Message = "Error de concurrencia", .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Metodo que genera la cxc
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateAccountReceivable(FixedAssetActiveOutput As FixedAssetActiveOutput, SettingFixedAsset As SettingFixedAsset) As List(Of AccountReceivable)
        'Diccionario de cuentas por cobrar
        Dim DictionaryAccountReceivable As New Dictionary(Of Integer, AccountReceivable)()
        'Listado a devolver de CxC
        Dim ListAccountReceivable As New List(Of AccountReceivable)

        'Se recorre los detalles de la salida de activos para generar las respectivas CxC siempre y cuando el tipo de salida sea venta o sea de tipo reposición
        For Each activeOutputDetail In (From d In FixedAssetActiveOutput.FixedAssetActiveOutputDetail Where d.OutputType = 2 Or d.LowType = 3 Select d).ToList

            If activeOutputDetail.ActiveType = 1 Then 'Si el tipo de activo es activo

                'Se obtiene el activo que se va recorriendo
                Dim physicalAsset As FixedAssetPhysicalAsset = _FixedAssetActiveOutputRepository.GetFixedAssetPhysicalAssetById(activeOutputDetail.PhysicalAssetId)

                'Tercero
                Dim ThirdPartyId As Integer = 0
                If activeOutputDetail.ThirdPartyId IsNot Nothing Then 'Se obtiene el tercero de la entidad
                    ThirdPartyId = activeOutputDetail.ThirdPartyId
                Else 'Se obtiene el tercero del activo
                    ThirdPartyId = physicalAsset.FixedAssetResponsible.ThirdPartyId
                End If

                If Not DictionaryAccountReceivable.ContainsKey(ThirdPartyId) Then 'Si el tercero no está dentro del diccionario se crea la cabecera de la cxc
                    DictionaryAccountReceivable.Add(ThirdPartyId, GenerateAccountReceivable(FixedAssetActiveOutput, SettingFixedAsset, ThirdPartyId))
                End If

                'Obtengo la cxc del tercero para sumarle los valores
                Dim accountReceivable As AccountReceivable = DictionaryAccountReceivable(ThirdPartyId)
                'Se asignan los valores a accountReceivableAccounting
                accountReceivable.AccountReceivableAccounting(0).Value = accountReceivable.AccountReceivableAccounting(0).Value + activeOutputDetail.SalesValue
                accountReceivable.AccountReceivableAccounting(0).Balance = accountReceivable.AccountReceivableAccounting(0).Balance + activeOutputDetail.SalesValue
                'Se asignan los valores a accountReceivableShare
                accountReceivable.AccountReceivableShare(0).Value = accountReceivable.AccountReceivableShare(0).Value + activeOutputDetail.SalesValue
                accountReceivable.AccountReceivableShare(0).Balance = accountReceivable.AccountReceivableShare(0).Balance + activeOutputDetail.SalesValue
                'Se asignan los valores a accountReceivable
                accountReceivable.Value = accountReceivable.AccountReceivableAccounting.Sum(Function(item) item.Value)
                accountReceivable.Balance = accountReceivable.AccountReceivableAccounting.Sum(Function(item) item.Balance)
            End If
        Next

        'Se adiciona al listado que se retorna las cuentas por cobrar
        For Each item In DictionaryAccountReceivable
            ListAccountReceivable.Add(item.Value)
        Next

        'Retorno el listado generado
        Return ListAccountReceivable
    End Function

    ''' <summary>
    ''' Genera la entidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateAccountReceivable(FixedAssetActiveOutput As FixedAssetActiveOutput, SettingFixedAsset As SettingFixedAsset, ThirdPartyId As Integer) As AccountReceivable
        Dim accountReceivable As New AccountReceivable
        'Se genera la cabecera
        With accountReceivable
            .Code = String.Empty
            .OperatingUnitId = FixedAssetActiveOutput.OperatingUnitId
            .AccountReceivableType = 1
            .ThirdPartyId = ThirdPartyId
            .InvoiceNumber = String.Empty
            .AccountReceivableDate = FixedAssetActiveOutput.DocumentDate
            .Term = 30
            .ExpiredDate = DateAdd(DateInterval.Day, .Term, .AccountReceivableDate)
            .Observations = "Cuenta por cobrar generada desde salida de activos"
            .PortfolioStatus = 1
            .OpeningBalance = False
            .PaymentAgreement = False
            .RegistrationAdjusted = False
            .MainAccountWithoutFilingId = SettingFixedAsset.SalesMainAccountId
            .AccountWithoutRadicateId = SettingFixedAsset.SalesMainAccountId
            .NumberShares = 1
            .Value = 0
            .Balance = 0
            .Status = 2

            'Se genera el accountReceivableAccounting
            Dim accountReceivableAccounting As New AccountReceivableAccounting
            accountReceivableAccounting.MainAccountId = SettingFixedAsset.SalesMainAccountId
            accountReceivableAccounting.ThirdPartyId = ThirdPartyId
            accountReceivableAccounting.Value = 0
            accountReceivableAccounting.Balance = 0
            .AccountReceivableAccounting.Add(accountReceivableAccounting)

            'Se genera el accountReceivableShare
            Dim accountReceivableShare As New AccountReceivableShare
            accountReceivableShare.Number = 1
            accountReceivableShare.ExpiredDate = .ExpiredDate
            .Value = 0
            .Balance = 0
            .AccountReceivableShare.Add(accountReceivableShare)
        End With
        Return accountReceivable
    End Function

    ''' <summary>
    ''' Convierte la entidad a objeto xml
    ''' </summary>
    ''' <param name="FixedAssetActiveOutput"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXml(FixedAssetActiveOutput As FixedAssetActiveOutput) As String
        Dim builder As StringBuilder = New StringBuilder()

        'FixedAssetActiveOutput
        builder.Append("<FixedAssetActiveOutput>")

        'Se arma la cabacera

        builder.Append("<Id>" & FixedAssetActiveOutput.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & FixedAssetActiveOutput.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & FixedAssetActiveOutput.Code & "</Code>")
        builder.Append("<DocumentDate>" & FixedAssetActiveOutput.DocumentDate.ToString("dd/MM/yyyy") & "</DocumentDate>")
        builder.Append("<Observation>" & FixedAssetActiveOutput.Observation & "</Observation>")
        builder.Append("<Status>" & FixedAssetActiveOutput.Status & "</Status>")

        If FixedAssetActiveOutput.FixedAssetActiveOutputDetail IsNot Nothing AndAlso FixedAssetActiveOutput.FixedAssetActiveOutputDetail.Count > 0 Then
            For Each activeOutputDetail In FixedAssetActiveOutput.FixedAssetActiveOutputDetail
                If activeOutputDetail.ChangeTracker.State <> ObjectState.Deleted Then 'Sin detalles eliminados
                    'Se arma los detalles
                    builder.Append("<FixedAssetActiveOutputDetail>")

                    builder.Append("<Id>" & activeOutputDetail.Id & "</Id>")
                    builder.Append("<FixedAssetActiveOutputId>" & activeOutputDetail.FixedAssetActiveOutputId & "</FixedAssetActiveOutputId>")
                    builder.Append("<ActiveType>" & activeOutputDetail.ActiveType & "</ActiveType>")

                    If activeOutputDetail.PhysicalAssetId IsNot Nothing Then
                        builder.Append("<PhysicalAssetId>" & activeOutputDetail.PhysicalAssetId & "</PhysicalAssetId>")
                    Else
                        builder.Append("<PhysicalAssetId>" & 0 & "</PhysicalAssetId>")
                    End If

                    If activeOutputDetail.PhysicalAssetPartsId IsNot Nothing Then
                        builder.Append("<PhysicalAssetPartsId>" & activeOutputDetail.PhysicalAssetPartsId & "</PhysicalAssetPartsId>")
                    Else
                        builder.Append("<PhysicalAssetPartsId>" & 0 & "</PhysicalAssetPartsId>")
                    End If

                    builder.Append("<MainAccountId>" & activeOutputDetail.MainAccountId & "</MainAccountId>")
                    builder.Append("<OutputType>" & activeOutputDetail.OutputType & "</OutputType>")
                    builder.Append("<LowType>" & activeOutputDetail.LowType & "</LowType>")
                    builder.Append("<SalesValue>" & activeOutputDetail.SalesValue & "</SalesValue>")

                    If activeOutputDetail.ThirdPartyId IsNot Nothing Then
                        builder.Append("<ThirdPartyId>" & activeOutputDetail.ThirdPartyId & "</ThirdPartyId>")
                    Else
                        builder.Append("<ThirdPartyId>" & 0 & "</ThirdPartyId>")
                    End If

                    If activeOutputDetail.AccountReceivableId IsNot Nothing Then
                        builder.Append("<AccountReceivableId>" & activeOutputDetail.AccountReceivableId & "</AccountReceivableId>")
                    Else
                        builder.Append("<AccountReceivableId>" & 0 & "</AccountReceivableId>")
                    End If

                    builder.Append("</FixedAssetActiveOutputDetail>")
                End If
            Next
        End If

        builder.Append("</FixedAssetActiveOutput>")

        Return builder.ToString
    End Function

    ''' <summary>
    ''' Crean los comprobantes contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateJournalVouchers(FixedAssetActiveOutput As FixedAssetActiveOutput, settingsFixedAsset As SettingFixedAsset) As ActionResult(Of List(Of JournalVouchers))
        'Se valida que haya parámetros de contabilidad general con la unidad operativa escogida
        Dim SettingGeneralLedger = _FixedAssetActiveOutputRepository.GetSettingGeneralLedgerByOperatingUnitId(FixedAssetActiveOutput.OperatingUnitId)
        If SettingGeneralLedger Is Nothing Then
            Return New ActionResult(Of List(Of JournalVouchers)) With {.StateResult = False, .Message = "No existe parámetros de contabilidad general para la unidad operativa escogida"}
        End If

        'Listado de comprobantes a devolver
        Dim ListJournalVouchers As New List(Of JournalVouchers)
        'Entidad del activo
        Dim physicalAsset As FixedAssetPhysicalAsset = Nothing
        'Listado de errores
        Dim ListErrors As New StringBuilder
        'Diccionario de comprobantes contables
        Dim DictionaryJournalVoucher As New Dictionary(Of Integer, Tuple(Of Boolean, JournalVouchers))()
        'Se obtiene el listado de ids de legalBook para mirar si se realiza el comrpobante contable con ese libro
        Dim ListLegalBookId As List(Of Integer) = _FixedAssetActiveOutputRepository.GetListLegalBookIds()
        'Libro oficial
        Dim book As LegalBook = Nothing

        'Se valida que el libro que se va recorriendo este en el listado de permiso para realizar el comprobante
        If ListLegalBookId Is Nothing OrElse ListLegalBookId.Count = 0 Then
            Return New ActionResult(Of List(Of JournalVouchers)) With {.StateResult = False, .Message = "No existen libros parametrizados para la contabilizacion (VieBot)"}
        End If

        Dim balanceTracker As New Dictionary(Of String, BalanceAcc)(StringComparer.OrdinalIgnoreCase)
        Dim unbalanced As New List(Of String)()

        'Unidad de trabajo
        Dim UnitOfWorkPhysical As IUnitWork = _fixedAssetPhysicalAssetRepository.UnitWork
        'Se recorren los detalles para generar los comprobantes contables
        For Each activeOutputDetail In FixedAssetActiveOutput.FixedAssetActiveOutputDetail.ToList
            If activeOutputDetail.ActiveType = 1 Then 'Si el tipo de activo es Activo
                'Obtengo el physicalAsset
                physicalAsset = _FixedAssetActiveOutputRepository.GetFixedAssetPhysicalAssetById(activeOutputDetail.PhysicalAssetId, True)

                'Si es una salida de tipo venta
                If activeOutputDetail.OutputType = 2 Then
                    If physicalAsset.AdquisitionType = 3 Then
                        'El activo no puede ser un comodato
                        ListErrors.AppendLine("El activo con placa " + physicalAsset.Plate + " es un comodato y no puede tener la opción de venta")
                        Continue For
                    ElseIf physicalAsset.AdquisitionType = 8 Then
                        'El activo no puede ser un comodato tercerizado
                        ListErrors.AppendLine("El activo con placa " + physicalAsset.Plate + " es un comodato tercerizado y no puede tener la opción de venta")
                        Continue For
                    ElseIf physicalAsset.AdquisitionType = 10 Then
                        'El activo no puede ser un renting operativo
                        ListErrors.AppendLine("El activo con placa " + physicalAsset.Plate + " es un renting operativo y no puede tener la opción de venta")
                        Continue For
                    End If
                End If

                If physicalAsset.AdquisitionType <> 8 AndAlso physicalAsset.AdquisitionType <> 10 Then
                    If physicalAsset.Depreciate = True Then 'Si el Activo Deprecia y además el tipo de adquisición es diferente a comodato tercerizado y renting operativo
                        'Valido si el activo tiene detalles de libros contables
                        If physicalAsset.FixedAssetPhysicalAssetDetailBook Is Nothing OrElse physicalAsset.FixedAssetPhysicalAssetDetailBook.Count = 0 Then
                            ListErrors.AppendLine("El activo con placa " + physicalAsset.Plate + " deprecia pero no tiene registrado los libros contables")
                            Continue For
                        End If

                        'Se recorren los libros que tenga el activo
                        For Each physicalAssetDetailBook In physicalAsset.FixedAssetPhysicalAssetDetailBook
                            'Si el libro tiene todos los valores en cero (activo subido por saldos iniciales
                            'marcado como que deprecia pero sin valores de depreciación), se trata como activo
                            'que no deprecia para garantizar la correcta contrapartida contable
                            If physicalAssetDetailBook.HistoricalValue = 0 AndAlso
                               physicalAssetDetailBook.DepreciatedValue = 0 AndAlso
                               physicalAssetDetailBook.ResidualValue = 0 AndAlso
                               physicalAssetDetailBook.Devaluation = 0 Then
                                ProcessBookWithoutDepreciation(FixedAssetActiveOutput, physicalAsset, physicalAssetDetailBook.LegalBookId, SettingGeneralLedger, settingsFixedAsset, DictionaryJournalVoucher, balanceTracker, ListLegalBookId)
                            Else
                                'Usar la función auxiliar para procesar el libro con depreciación
                                ProcessBookWithDepreciation(FixedAssetActiveOutput, physicalAsset, physicalAssetDetailBook, activeOutputDetail, SettingGeneralLedger, settingsFixedAsset, DictionaryJournalVoucher, balanceTracker, ListLegalBookId)
                            End If
                        Next
                    ElseIf physicalAsset.FixedAssetPhysicalAssetDetailBook IsNot Nothing AndAlso physicalAsset.FixedAssetPhysicalAssetDetailBook.Count > 0 Then
                        'Si el parámetro Depreciate es False pero el activo tiene libros contables, evaluar libro por libro
                        'según si tienen valor depreciado o no
                        For Each physicalAssetDetailBook In physicalAsset.FixedAssetPhysicalAssetDetailBook

                            'Si el libro tiene valor depreciado, procesarlo como activo que deprecia
                            If physicalAssetDetailBook.DepreciatedValue > 0 Then
                                'Usar la función auxiliar para procesar libro con depreciación
                                ProcessBookWithDepreciation(FixedAssetActiveOutput, physicalAsset, physicalAssetDetailBook, activeOutputDetail, SettingGeneralLedger, settingsFixedAsset, DictionaryJournalVoucher, balanceTracker, ListLegalBookId)
                            Else
                                'Si el libro NO tiene valor depreciado, procesarlo como activo que no deprecia
                                'Usar la función auxiliar para procesar libro sin depreciación
                                ProcessBookWithoutDepreciation(FixedAssetActiveOutput, physicalAsset, physicalAssetDetailBook.LegalBookId, SettingGeneralLedger, settingsFixedAsset, DictionaryJournalVoucher, balanceTracker, ListLegalBookId)
                            End If
                        Next
                    Else 'Si el Activo no deprecia y no tiene libros de activo
                        'Si tiene detalles de libros contables y es diferente a comodato
                        If physicalAsset.FixedAssetItem.FixedAssetItemDetail.Count > 0 AndAlso physicalAsset.AdquisitionType <> 3 Then
                            For Each detail In physicalAsset.FixedAssetItem.FixedAssetItemDetail

                                'Se valida que el libro que se va recorriendo este en el listado de permiso para realizar el comprobante
                                If ListLegalBookId.Contains(detail.LegalBookId) = False Then
                                    Continue For
                                End If

                                If Not DictionaryJournalVoucher.ContainsKey(detail.LegalBookId) Then 'Si el libro no existe en el diccionario se crea la cabecera del comprobante para cierto libro
                                    book = _legalBookRepository.GetBookById(detail.LegalBookId)
                                    DictionaryJournalVoucher.Add(detail.LegalBookId, New Tuple(Of Boolean, JournalVouchers)(book.OfficialBook, GenerateHeader(FixedAssetActiveOutput, detail.LegalBookId, settingsFixedAsset)))
                                End If

                                Dim JournalVoucher As JournalVouchers = DictionaryJournalVoucher(detail.LegalBookId).Item2 'Obtengo la cabecera correspondiente al libro que voy recorriendo para agregarle los detalles
                                If JournalVoucher.JournalVoucherDetails Is Nothing Then
                                    JournalVoucher.JournalVoucherDetails = New Domain.Entities.TrackableCollection(Of JournalVoucherDetails)
                                End If

                                Dim det12 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(detail.LegalBookId).Item1, physicalAsset, 1)
                                AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det12)
                                AddAndTrack(det12, balanceTracker, physicalAsset.Plate, detail.LegalBookId)


                                Dim det13 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(detail.LegalBookId).Item1, physicalAsset, 8)
                                AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det13)
                                AddAndTrack(det13, balanceTracker, physicalAsset.Plate, detail.LegalBookId)
                            Next
                        Else 'Si es comodato o el activo no tiene libros contables parametrizados
                            For Each bookId In ListLegalBookId
                                If Not DictionaryJournalVoucher.ContainsKey(bookId) Then 'Si el libro no existe en el diccionario se crea la cabecera del comprobante para cierto libro
                                    book = _legalBookRepository.GetBookById(bookId)
                                    DictionaryJournalVoucher.Add(bookId, New Tuple(Of Boolean, JournalVouchers)(book.OfficialBook, GenerateHeader(FixedAssetActiveOutput, bookId, settingsFixedAsset)))
                                End If

                                Dim JournalVoucher As JournalVouchers = DictionaryJournalVoucher(bookId).Item2 'Obtengo la cabecera correspondiente al libro que voy recorriendo para agregarle los detalles
                                If JournalVoucher.JournalVoucherDetails Is Nothing Then
                                    JournalVoucher.JournalVoucherDetails = New Domain.Entities.TrackableCollection(Of JournalVoucherDetails)
                                End If

                                Dim det14 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(bookId).Item1, physicalAsset, 1)
                                AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det14)
                                AddAndTrack(det14, balanceTracker, physicalAsset.Plate, bookId)

                                Dim det15 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(bookId).Item1, physicalAsset, 8)
                                AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det15)
                                AddAndTrack(det15, balanceTracker, physicalAsset.Plate, bookId)
                            Next
                        End If
                    End If
                End If

                ' ===== Validar y mostrar el balance de ESTA placa =====
                FlushBalanceForPlate(balanceTracker,
                                     physicalAsset.Plate,
                                     unbalanced,
                                     decimales:=2,
                                     tolerancia:=0.01D)

                'Se actualiza los campos de HasOutput y OutputDate de la tabla FixedAssetPhysicalAsset
                'physicalAsset.StartTracking()
                physicalAsset.Status = 0
                physicalAsset.HasOutput = True
                physicalAsset.OutputDate = FixedAssetActiveOutput.DocumentDate
                physicalAsset.MarkAsModified()
                _fixedAssetPhysicalAssetRepository.SaveEntity(physicalAsset)
                UnitOfWorkPhysical.Commit()
            End If
        Next

        If ListErrors.Length > 0 Then 'Si hay errores retorno error
            Return New ActionResult(Of List(Of JournalVouchers)) With {.StateResult = False, .Message = ListErrors.ToString}
        End If

        If unbalanced?.Any() Then
            Return New ActionResult(Of List(Of JournalVouchers)) With {
                  .StateResult = False,
                  .ObjectEmbbeded = ListJournalVouchers,
                  .MessageResult = unbalanced
              }
        End If

        'Se adiciona al listado que se retorna los comprobantes generados
        For Each item In DictionaryJournalVoucher
            ListJournalVouchers.Add(item.Value.Item2)
        Next

        Return New ActionResult(Of List(Of JournalVouchers)) With {.StateResult = True, .ObjectEmbbeded = ListJournalVouchers}
    End Function

    ''' <summary>
    ''' Agrega un detalle al comprobante solo si tiene al menos un valor mayor a 0 en débito o crédito
    ''' </summary>
    ''' <param name="journalVoucherDetails">Colección de detalles del comprobante</param>
    ''' <param name="detail">Detalle a agregar</param>
    Private Sub AddDetailIfValid(journalVoucherDetails As Domain.Entities.TrackableCollection(Of JournalVoucherDetails), detail As JournalVoucherDetails)
        If detail.DebitValue > 0 OrElse detail.CreditValue > 0 Then
            journalVoucherDetails.Add(detail)
        End If
    End Sub

    ''' <summary>
    ''' Genera la cabacera del comprobante
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateHeader(fixedAssetActiveOutput As FixedAssetActiveOutput, LegalBookId As Integer, settingsFixedAsset As SettingFixedAsset) As JournalVouchers
        Dim JournalVoucher As New JournalVouchers
        'Se arma la cabecera
        With JournalVoucher
            .LegalBookId = LegalBookId
            .IdJournalVoucher = settingsFixedAsset.IdOutputAccountingVoucher
            .VoucherDate = fixedAssetActiveOutput.DocumentDate
            .Imported = False
            .Status = 2
            .Detail = "Comprobante contable generado desde salida de activos"
            .EntityCode = fixedAssetActiveOutput.Code
            .EntityId = fixedAssetActiveOutput.Id
            .EntityName = GetType(FixedAssetActiveOutput).Name
            .IsClosedYear = False
        End With
        Return JournalVoucher
    End Function

    ''' <summary>
    ''' Genera los detalles del comprobante
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDetail(thirdPartyId As Integer, isLegalBook As Boolean, physicalAsset As FixedAssetPhysicalAsset, typeDetail As Integer,
                                    Optional physicalAssetDetailBook As FixedAssetPhysicalAssetDetailBook = Nothing, Optional settingFixedAsset As SettingFixedAsset = Nothing,
                                    Optional activeOutputDetail As FixedAssetActiveOutputDetail = Nothing) As JournalVoucherDetails
        Dim JournalVoucherDetails As New JournalVoucherDetails
        Dim idCostCenter As Integer? = physicalAsset?.FixedAssetLocation?.FunctionalUnit?.CostCenterId
        'Se arma el detalle
        With JournalVoucherDetails
            Select Case typeDetail
                Case 1
                    'La cuenta depende del tipo de adquisicion, si es comodato, leasing financiero usar la cuenta del catalogo
                    If physicalAsset.AdquisitionType = 3 Then
                        .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.DebitLoanAccountId
                    ElseIf physicalAsset.AdquisitionType = 7 Then
                        .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.IncomeLeasingAccountId
                    Else
                        .IdMainAccount = physicalAsset.MainAccountId
                    End If
                    .DebitValue = 0
                    ' En cualquier escenario se debe contemplar el Descuento Financiero
                    If physicalAssetDetailBook Is Nothing Or (physicalAssetDetailBook IsNot Nothing AndAlso physicalAssetDetailBook.HistoricalValue = 0) Then
                        .CreditValue = physicalAsset.HistoricalValue - physicalAsset.FinancialDiscount
                    Else
                        .CreditValue = physicalAssetDetailBook.HistoricalValue - physicalAsset.FinancialDiscount ' Si existe valor Historico del libro, debe contabilizar ese valor
                    End If
                Case 2
                    'La cuenta depende del tipo de adquisicion, si es leasing financiero usar la cuenta del catalogo
                    If physicalAsset.AdquisitionType = 7 Then
                        .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.DepreciationLeasingAccountId
                    ElseIf physicalAsset.AdquisitionType = 3 Then 'Si el tipo de adquisición es comodato
                        .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.LoanLeasingAccountId
                    Else
                        .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.DepreciationAccountId
                    End If
                    .DebitValue = physicalAssetDetailBook.DepreciatedValue
                    .CreditValue = 0
                Case 3
                    .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.LossMainAccountId
                    If activeOutputDetail.SalesValue > 0 Then
                        .DebitValue = physicalAssetDetailBook.ResidualValue - activeOutputDetail.SalesValue
                    Else
                        .DebitValue = GetBookValueForRetirement(physicalAsset, physicalAssetDetailBook)
                    End If
                    .CreditValue = 0
                Case 4
                    .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.NetIncomeAccountId
                    .DebitValue = 0
                    .CreditValue = activeOutputDetail.SalesValue - physicalAssetDetailBook.ResidualValue
                Case 5
                    .IdMainAccount = settingFixedAsset.SalesMainAccountId
                    .DebitValue = activeOutputDetail.SalesValue
                    .CreditValue = 0
                Case 6
                    .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.ReplacementCreditMainAccountId
                    .DebitValue = 0
                    .CreditValue = activeOutputDetail.SalesValue
                Case 7
                    .IdMainAccount = settingFixedAsset.ReplacementMainAccountId
                    .DebitValue = activeOutputDetail.SalesValue
                    .CreditValue = 0
                Case 8 'Perdida de todo el valor cuando se da de baja un activo que no deprecia
                    If physicalAsset.AdquisitionType = 3 Then
                        .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.CreditLoanAccountId
                    Else
                        .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.LossMainAccountId
                    End If
                    ' Se contempla el descuento financiero en la contrapartida
                    .DebitValue = IIf(isLegalBook = True, physicalAsset.HistoricalValue - physicalAsset.FinancialDiscount, IIf(physicalAsset.FairValue = 0, physicalAsset.HistoricalValue - physicalAsset.FinancialDiscount, physicalAsset.FairValue - physicalAsset.FinancialDiscount))
                    .CreditValue = 0
                Case 9
                    Dim catalogDetail As FixedAssetItemCatalogDetail
                    Dim handlesDepreciationByDistribution = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.HandlesDepreciationbyDistribution
                    If handlesDepreciationByDistribution Then
                        catalogDetail = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.FixedAssetItemCatalogDetail.Where(Function(d) d.CostCenterId = physicalAsset.FixedAssetLocation.FunctionalUnit.CostCenterId).FirstOrDefault()
                    Else
                        catalogDetail = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.FixedAssetItemCatalogDetail.Where(Function(d) d.AccountingStructureId = physicalAsset.FixedAssetLocation.FunctionalUnit.AccountingStructureId).FirstOrDefault()
                    End If
                    'La cuenta depende del tipo de adquisicion, si es leasing financiero usar la cuenta del catalogo
                    If physicalAsset.AdquisitionType = 7 Then
                        .IdMainAccount = catalogDetail.LoanLeasingSpendAccountId
                    ElseIf physicalAsset.AdquisitionType = 3 Then 'Si el tipo de adquisición es comodato
                        .IdMainAccount = catalogDetail.ExpenseLoanAccountId
                    Else
                        .IdMainAccount = catalogDetail.LoanSpendAccountId
                    End If
                    .DebitValue = GetBookValueForRetirement(physicalAsset, physicalAssetDetailBook)
                    .CreditValue = 0
                Case 10 'caso para crear el detalle del comprobante contable de valorizacion
                    'La cuenta depende del tipo de adquisicion, si es leasing financiero usar la cuenta del catalogo
                    If physicalAsset.AdquisitionType = 7 Then
                        .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.IncomeLeasingAccountId
                    ElseIf physicalAsset.AdquisitionType = 3 Then 'Si el tipo de adquisición es comodato
                        .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.CreditLoanAccountId
                    Else
                        .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.CreditValorizationAccountId
                    End If
                    .DebitValue = 0
                    .CreditValue = physicalAssetDetailBook.Valorization
                Case 11 'Asiento por pérdida contable cuando el activo es dado de baja por devaluación (Devaluation > 0)
                    .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.LossMainAccountId
                    .DebitValue = physicalAssetDetailBook.Devaluation
                    .CreditValue = 0
                Case 12 'Asiento por pérdida contable credito
                    .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.LossMainAccountId
                    .CreditValue = physicalAssetDetailBook.Devaluation
                    .DebitValue = 0
                Case 13 'Activo sobre-depreciado (depreciacion acumulada mayor al valor historico).
                    .IdMainAccount = physicalAsset.FixedAssetItem.FixedAssetItemCatalog.NetIncomeAccountId
                    .DebitValue = 0
                    .CreditValue = Math.Abs(GetBookValueForRetirement(physicalAsset, physicalAssetDetailBook))
            End Select
            .IdThirdParty = If(_pucAdminService.MainAccountHandlesThirdParty(.IdMainAccount), thirdPartyId, Nothing)
            .IdCostCenter = If(_pucAdminService.MainAccountHandlesCostCenter(.IdMainAccount), idCostCenter, Nothing)
            .Detail = "Detalle generado desde salida de activos"
        End With
        Return JournalVoucherDetails
    End Function

    ''' <summary>
    ''' garantizar que el comprobante de salida cuadre. Se usa en lugar de ResidualValue porque el proceso de
    ''' depreciacion puede cerrar el activo con ResidualValue = 0 dejando la depreciacion incompleta.
    ''' </summary>
    Private Function GetBookValueForRetirement(physicalAsset As FixedAssetPhysicalAsset, physicalAssetDetailBook As FixedAssetPhysicalAssetDetailBook) As Decimal
        Dim historical As Decimal
        If physicalAssetDetailBook Is Nothing OrElse physicalAssetDetailBook.HistoricalValue = 0 Then
            historical = physicalAsset.HistoricalValue
        Else
            historical = physicalAssetDetailBook.HistoricalValue
        End If
        Dim depreciated As Decimal = If(physicalAssetDetailBook Is Nothing, 0D, physicalAssetDetailBook.DepreciatedValue)
        Return historical - physicalAsset.FinancialDiscount - depreciated
    End Function

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetActiveOutput(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetActiveOutput) Implements IFixedAssetActiveOutputAdminService.GetFixedAssetActiveOutput
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetActiveOutput As FixedAssetActiveOutput = Me._FixedAssetActiveOutputRepository.GetFixedAssetActiveOutput(code.Trim())
            If FixedAssetActiveOutput IsNot Nothing AndAlso FixedAssetActiveOutput.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetActiveOutput)(FixedAssetActiveOutput, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = True, .ObjectEmbbeded = FixedAssetActiveOutput}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetActiveOutputById(Id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetActiveOutput) Implements IFixedAssetActiveOutputAdminService.GetFixedAssetActiveOutputById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetActiveOutput As FixedAssetActiveOutput = Me._FixedAssetActiveOutputRepository.GetFixedAssetActiveOutputById(Id)
            If FixedAssetActiveOutput IsNot Nothing AndAlso FixedAssetActiveOutput.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetActiveOutput)(FixedAssetActiveOutput, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = True, .ObjectEmbbeded = FixedAssetActiveOutput}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="FixedAssetTransfer"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetActiveOutput(FixedAssetActiveOutput As FixedAssetActiveOutput, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetActiveOutput) Implements IFixedAssetActiveOutputAdminService.SaveFixedAssetActiveOutput
        If FixedAssetActiveOutput Is Nothing Then
            Throw New ArgumentNullException("FixedAssetActiveOutput")
        End If
        Dim unitOfWork As IUnitWork = Me._FixedAssetActiveOutputRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As FixedAssetSequenceDetail = Nothing
                If FixedAssetActiveOutput.Code Is Nothing OrElse FixedAssetActiveOutput.Code.Trim().Equals(String.Empty) Then
                    seq = Me._sequenceRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            FixedAssetActiveOutput.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            transaction.Dispose()
                            Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .Message = "El rango de la secuencia numérica ya se excedió", .StatusCode = eStatusResult.WARNING}
                        End If
                    Else
                        transaction.Dispose()
                        Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .Message = "El formulario no tiene parametrizada la secuencia numérica", .StatusCode = eStatusResult.WARNING}
                    End If
                End If

                Dim auxFixedAssetActiveOutput As FixedAssetActiveOutput = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetActiveOutput)
                Dim status As Integer

                If FixedAssetActiveOutput.ChangeTracker.State = ObjectState.Added Then
                    FixedAssetActiveOutput.CreationUser = audit.CodeUser
                    FixedAssetActiveOutput.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf FixedAssetActiveOutput.ChangeTracker.State = ObjectState.Modified Then
                    auxFixedAssetActiveOutput = FixedAssetActiveOutput.OriginalValue
                    If FixedAssetActiveOutput.Status = 1 Then
                        FixedAssetActiveOutput.ModificationUser = audit.CodeUser
                        FixedAssetActiveOutput.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    End If
                    If FixedAssetActiveOutput.Status = 2 Then
                        FixedAssetActiveOutput.ModificationUser = audit.CodeUser
                        FixedAssetActiveOutput.ModificationDate = DateTime.Now
                        FixedAssetActiveOutput.ConfirmationUser = audit.CodeUser
                        FixedAssetActiveOutput.ConfirmationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                    End If
                    If FixedAssetActiveOutput.Status = 3 Then
                        FixedAssetActiveOutput.ModificationUser = audit.CodeUser
                        FixedAssetActiveOutput.ModificationDate = DateTime.Now
                        FixedAssetActiveOutput.AnnulmentUser = audit.CodeUser
                        FixedAssetActiveOutput.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End If
                End If

                Me._FixedAssetActiveOutputRepository.SaveEntity(FixedAssetActiveOutput)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetActiveOutput)(FixedAssetActiveOutput, audit, status, auxFixedAssetActiveOutput)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                FixedAssetActiveOutput.MarkAsUnchanged()

                transaction.Complete()
                Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = True, .ObjectEmbbeded = FixedAssetActiveOutput, .StatusCode = eStatusResult.SUCCESS}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .Message = "Error de concurrencia", .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetActiveOutput) With {.StateResult = False, .Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
            End Try
        End Using
    End Function

#End Region


    ''' <summary>
    ''' Traker
    ''' </summary>
    ''' <param name="tracker"></param>
    ''' <param name="plate"></param>
    ''' <param name="bookId"></param>
    ''' <param name="det"></param>
    Private Sub TrackBalance(tracker As Dictionary(Of String, BalanceAcc),
                         plate As String,
                         bookId As Integer,
                         det As JournalVoucherDetails)
        Dim key = $"{plate}|{bookId}"
        Dim acc As BalanceAcc = Nothing
        If Not tracker.TryGetValue(key, acc) Then
            acc = New BalanceAcc()
            tracker(key) = acc
        End If
        acc.Debit += CDec(det.DebitValue)
        acc.Credit += CDec(det.CreditValue)
        acc.Lines.Add(det)
    End Sub


    ''' <summary>
    ''' Imprime y limpia los acumulados de UNA placa (todas sus llaves plate|libro)
    ''' </summary>
    ''' <param name="tracker"></param>
    ''' <param name="plate"></param>
    ''' <param name="mensajes"></param>
    ''' <param name="decimales"></param>
    ''' <param name="tolerancia"></param>
    Private Sub FlushBalanceForPlate(tracker As Dictionary(Of String, BalanceAcc),
                                 plate As String,
                                 mensajes As IList(Of String),
                                 Optional decimales As Integer = 2,
                                 Optional tolerancia As Decimal = 0.01D)
        Dim writeLine As Action(Of String) = Sub(s) Debug.WriteLine(s)

        Dim prefix = plate & "|"
        Dim keys = tracker.Keys.Where(Function(k) k.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)).ToList()
        Dim header As String

        For Each k In keys
            Dim acc = tracker(k)
            Dim dif = Math.Round(acc.Debit - acc.Credit, decimales)

            If Math.Abs(dif) > tolerancia Then
                Dim partes = k.Split("|"c)
                Dim libroTxt = If(partes.Length > 1, partes(1), "?")
                header = $"[DESBALANCE] Placa {plate}  Libro:{libroTxt}  Débito {acc.Debit:N2}  Crédito {acc.Credit:N2}  Dif {dif:N2}"
                mensajes.Add(header)
            End If
            tracker.Remove(k)
        Next
    End Sub

    ''' <summary>
    ''' Agrega el detalle  en el tracker
    ''' </summary>
    ''' <param name="det"></param>
    ''' <param name="tracker"></param>
    ''' <param name="plate"></param>
    ''' <param name="bookId"></param>
    Private Sub AddAndTrack(
                        det As JournalVoucherDetails,
                        tracker As Dictionary(Of String, BalanceAcc),
                        plate As String,
                        bookId As Integer)
        TrackBalance(tracker, plate, bookId, det)
    End Sub

    ''' <summary>
    ''' Procesa un libro contable con depreciación acumulada
    ''' </summary>
    Private Sub ProcessBookWithDepreciation(
                        FixedAssetActiveOutput As FixedAssetActiveOutput,
                        physicalAsset As FixedAssetPhysicalAsset,
                        physicalAssetDetailBook As FixedAssetPhysicalAssetDetailBook,
                        activeOutputDetail As FixedAssetActiveOutputDetail,
                        SettingGeneralLedger As GeneralLedgerSettings,
                        settingsFixedAsset As SettingFixedAsset,
                        DictionaryJournalVoucher As Dictionary(Of Integer, Tuple(Of Boolean, JournalVouchers)),
                        balanceTracker As Dictionary(Of String, BalanceAcc),
                        ListLegalBookId As List(Of Integer))

        'Se valida que el libro esté en el listado de permiso para realizar el comprobante
        If Not ListLegalBookId.Contains(physicalAssetDetailBook.LegalBookId) Then
            Return
        End If

        'Crear o recuperar el comprobante del diccionario
        If Not DictionaryJournalVoucher.ContainsKey(physicalAssetDetailBook.LegalBookId) Then
            Dim book = _legalBookRepository.GetBookById(physicalAssetDetailBook.LegalBookId)
            DictionaryJournalVoucher.Add(physicalAssetDetailBook.LegalBookId, New Tuple(Of Boolean, JournalVouchers)(book.OfficialBook, GenerateHeader(FixedAssetActiveOutput, physicalAssetDetailBook.LegalBookId, settingsFixedAsset)))
        End If

        Dim JournalVoucher As JournalVouchers = DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item2
        If JournalVoucher.JournalVoucherDetails Is Nothing Then
            JournalVoucher.JournalVoucherDetails = New Domain.Entities.TrackableCollection(Of JournalVoucherDetails)
        End If

        'Cuenta de ingreso al crédito
        Dim det1 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 1, physicalAssetDetailBook)
        AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det1)
        AddAndTrack(det1, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)

        'Depreciación al débito
        If (physicalAssetDetailBook.ResidualValue <= 0 OrElse physicalAssetDetailBook.ResidualValue > 0) AndAlso physicalAssetDetailBook.DepreciatedValue > 0 Then
            Dim det2 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 2, physicalAssetDetailBook)
            AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det2)
            AddAndTrack(det2, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)
        End If

        'Valorización
        If physicalAssetDetailBook.Valorization > 0 Then
            Dim det3 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 10, physicalAssetDetailBook)
            AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det3)
            AddAndTrack(det3, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)
        End If

        'Validaciones según tipo de salida
        If activeOutputDetail.OutputType = 2 Then
            'Venta
            If activeOutputDetail.SalesValue > physicalAssetDetailBook.ResidualValue Then
                Dim det4 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 4, physicalAssetDetailBook, Nothing, activeOutputDetail)
                AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det4)
                AddAndTrack(det4, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)
            ElseIf activeOutputDetail.SalesValue < physicalAssetDetailBook.ResidualValue Then
                Dim det5 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 3, physicalAssetDetailBook, Nothing, activeOutputDetail)
                AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det5)
                AddAndTrack(det5, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)
            End If
            Dim det6 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 5, Nothing, settingsFixedAsset, activeOutputDetail)
            AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det6)
            AddAndTrack(det6, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)
        Else
            'No es venta
            If GetBookValueForRetirement(physicalAsset, physicalAssetDetailBook) > 0 Then
                If activeOutputDetail.LowType = 5 Then
                    Dim det7 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 9, physicalAssetDetailBook, Nothing, activeOutputDetail)
                    AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det7)
                    AddAndTrack(det7, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)
                Else
                    Dim det8 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 3, physicalAssetDetailBook, Nothing, activeOutputDetail)
                    AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det8)
                    AddAndTrack(det8, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)
                End If
            ElseIf GetBookValueForRetirement(physicalAsset, physicalAssetDetailBook) < 0 AndAlso
                   If(physicalAsset.FixedAssetItem.FixedAssetItemCatalog.NetIncomeAccountId, 0) > 0 Then
                Dim det17 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 13, physicalAssetDetailBook, Nothing, activeOutputDetail)
                AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det17)
                AddAndTrack(det17, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)
            End If

            'Desvalorización
            If physicalAssetDetailBook.Devaluation > 0 AndAlso physicalAssetDetailBook.HistoricalValue = 0 Then
                Dim det9 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 11, physicalAssetDetailBook)
                AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det9)
                AddAndTrack(det9, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)

                If physicalAssetDetailBook.ResidualValue > 0 Then
                    Dim det16 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 12, physicalAssetDetailBook)
                    AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det16)
                    AddAndTrack(det16, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)
                End If
            End If
        End If

        'Reposición
        If activeOutputDetail.OutputType = 1 AndAlso activeOutputDetail.LowType = 3 Then
            Dim det10 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 6, Nothing, Nothing, activeOutputDetail)
            AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det10)
            AddAndTrack(det10, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)

            Dim det11 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(physicalAssetDetailBook.LegalBookId).Item1, physicalAsset, 7, Nothing, settingsFixedAsset, activeOutputDetail)
            AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det11)
            AddAndTrack(det11, balanceTracker, physicalAsset.Plate, physicalAssetDetailBook.LegalBookId)
        End If
    End Sub

    ''' <summary>
    ''' Procesa un libro contable sin depreciación acumulada
    ''' </summary>
    Private Sub ProcessBookWithoutDepreciation(
                        FixedAssetActiveOutput As FixedAssetActiveOutput,
                        physicalAsset As FixedAssetPhysicalAsset,
                        legalBookId As Integer,
                        SettingGeneralLedger As GeneralLedgerSettings,
                        settingsFixedAsset As SettingFixedAsset,
                        DictionaryJournalVoucher As Dictionary(Of Integer, Tuple(Of Boolean, JournalVouchers)),
                        balanceTracker As Dictionary(Of String, BalanceAcc),
                        ListLegalBookId As List(Of Integer))

        'Se valida que el libro esté en el listado de permiso
        If Not ListLegalBookId.Contains(legalBookId) Then
            Return
        End If

        'Crear o recuperar el comprobante del diccionario
        If Not DictionaryJournalVoucher.ContainsKey(legalBookId) Then
            Dim book = _legalBookRepository.GetBookById(legalBookId)
            DictionaryJournalVoucher.Add(legalBookId, New Tuple(Of Boolean, JournalVouchers)(book.OfficialBook, GenerateHeader(FixedAssetActiveOutput, legalBookId, settingsFixedAsset)))
        End If

        Dim JournalVoucher As JournalVouchers = DictionaryJournalVoucher(legalBookId).Item2
        If JournalVoucher.JournalVoucherDetails Is Nothing Then
            JournalVoucher.JournalVoucherDetails = New Domain.Entities.TrackableCollection(Of JournalVoucherDetails)
        End If

        'Cuenta de ingreso al crédito
        Dim det12 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(legalBookId).Item1, physicalAsset, 1)
        AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det12)
        AddAndTrack(det12, balanceTracker, physicalAsset.Plate, legalBookId)

        'Todo a pérdida al débito
        Dim det13 = GenerateDetail(SettingGeneralLedger.IdDian, DictionaryJournalVoucher(legalBookId).Item1, physicalAsset, 8)
        AddDetailIfValid(JournalVoucher.JournalVoucherDetails, det13)
        AddAndTrack(det13, balanceTracker, physicalAsset.Plate, legalBookId)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _accountReceivableAdminService.Dispose()
            End If
            _sequenceRepository = Nothing
            _FixedAssetActiveOutputRepository = Nothing
            _FixedAssetEntryRepository = Nothing
            _accountingRepository = Nothing
            _sequensePortfolio = Nothing
            _accountReceivableAdminService = Nothing
            _fixedAssetPhysicalAssetRepository = Nothing
            _legalBookRepository = Nothing
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


''' <summary>
''' Clase auxiliar para llevar registros del balance de la salida de activos
''' </summary>
Public Class BalanceAcc
    Public Debit As Decimal
    Public Credit As Decimal
    Public Lines As New List(Of JournalVoucherDetails)
End Class