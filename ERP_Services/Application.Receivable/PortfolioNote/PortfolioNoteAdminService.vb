'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
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
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Application.Accounting
Imports System.Data.Entity.Validation
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Resources
Imports Application.EventHandlers
Imports Application.EventHandlers.Model
Imports Application.EventHandlers.Enums.Enums
Imports System.Text
#End Region

Public Class PortfolioNoteAdminService
    Implements IPortfolioNoteAdminService

#Region "Fields"

    Private ReadOnly _eventProxy As IEventProxy
    Private _portfolioNoteRepository As IPortfolioNoteRepository
    Private _accountReceivableRepository As IAccountReceivableRepository
    Private _portfolioAdvanceRepository As IPortfolioAdvanceRepository

    Private _billingSequenseRepository As IBillingSequenceRepository
    Private _billingNoteRepository As IBillingNoteRepository
    Private _electronicDocumentRepository As IElectronicDocumentRepository
    Private _operatingUnitRepository As IOperatingUnitRepository
    Private _settingsAccountRepository As ISettingsAccountRepository
    Private _customerRepository As ICustomerRepository
    Private _companySettingsRepository As ICompanySettingsRepository
    Private Const GLOSAS_MODULE = "Glosas"

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(portfolioNoteRepository As IPortfolioNoteRepository, accountReceivableRepository As IAccountReceivableRepository, portfolioAdvanceRepository As IPortfolioAdvanceRepository,
                   billingSequenseRepository As IBillingSequenceRepository, billingNoteRepository As IBillingNoteRepository, electronicDocumentRepository As IElectronicDocumentRepository,
                   operatingUnitRepository As IOperatingUnitRepository, settingsAccountRepository As ISettingsAccountRepository, eventProxy As IEventProxy, customerRepository As ICustomerRepository, companySettingsRepository As ICompanySettingsRepository)
        If (portfolioNoteRepository Is Nothing) Then
            Throw New ArgumentNullException("portfolioNoteRepository Vacio")
        End If
        If accountReceivableRepository Is Nothing Then
            Throw New ArgumentNullException("accountReceivableRepository")
        End If
        If portfolioAdvanceRepository Is Nothing Then
            Throw New ArgumentNullException("portfolioAdvanceRepository")
        End If
        If billingSequenseRepository Is Nothing Then
            Throw New ArgumentNullException("billingSequenseRepository")
        End If
        If billingNoteRepository Is Nothing Then
            Throw New ArgumentNullException("billingNoteRepository")
        End If
        If electronicDocumentRepository Is Nothing Then
            Throw New ArgumentNullException("electronicDocumentRepository")
        End If
        If operatingUnitRepository Is Nothing Then
            Throw New ArgumentNullException("operatingUnitRepository")
        End If

        _portfolioNoteRepository = portfolioNoteRepository
        _accountReceivableRepository = accountReceivableRepository
        _portfolioAdvanceRepository = portfolioAdvanceRepository

        _billingSequenseRepository = billingSequenseRepository
        _billingNoteRepository = billingNoteRepository
        _electronicDocumentRepository = electronicDocumentRepository
        _operatingUnitRepository = operatingUnitRepository
        _settingsAccountRepository = settingsAccountRepository
        _eventProxy = eventProxy
        _customerRepository = customerRepository
        _companySettingsRepository = companySettingsRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para pegar en la rejilla
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <param name="CompanyType"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function SetCopyPasteOrImportFilePortfolioNote(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), CompanyType As Integer, OperatingUnit As Integer, ParamArray parameters() As Object) As ActionResult(Of List(Of PortfolioNoteAccountReceivableAdvance)) Implements IPortfolioNoteAdminService.SetCopyPasteOrImportFilePortfolioNote
        Dim _portfolioService = New PortfolioServices(_portfolioNoteRepository)
        Return _portfolioService.SetCopyPasteOrImportFilePortfolioNote(dataImportFile, dataCopyPaste, CompanyType, OperatingUnit, parameters)
    End Function

    ''' <summary>
    ''' obtener una nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteById(id As Integer) As PortfolioNote Implements IPortfolioNoteAdminService.GetPortfolioNoteById
        Try
            Return _portfolioNoteRepository.getPortfolioNoteById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PortfolioNote()
        End Try
    End Function

    ''' <summary>
    ''' obtener una nota por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code</exception>
    Public Function GetPortfolioNoteByCode(code As String, audit As AuditMessage) As PortfolioNote Implements IPortfolioNoteAdminService.GetPortfolioNoteByCode
        If code Is String.Empty Then
            Throw New ArgumentNullException("Code")
        End If
        Try
            Dim portfolioNote = _portfolioNoteRepository.GetPortfolioNoteByCode(code)
            If portfolioNote IsNot Nothing AndAlso portfolioNote.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioNote)(portfolioNote, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return portfolioNote
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PortfolioNote()
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener los detalles de la nota
    ''' </summary>
    ''' <param name="idPortfolioNote"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteDetailByIdPortfolioNote(idPortfolioNote As Integer) As List(Of PortfolioNoteDetail) Implements IPortfolioNoteAdminService.GetPortfolioNoteDetailByIdPortfolioNote
        Try
            Return _portfolioNoteRepository.GetPortfolioNoteDetailByIdPortfolioNote(idPortfolioNote)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of PortfolioNoteDetail)
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener las facturas o los anticipos asociados a esa nota
    ''' </summary>
    ''' <param name="idPortfolioNote"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote(idPortfolioNote As Integer) As List(Of PortfolioNoteAccountReceivableAdvance) Implements IPortfolioNoteAdminService.GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote
        Try
            Return _portfolioNoteRepository.GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote(idPortfolioNote)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of PortfolioNoteAccountReceivableAdvance)
        End Try
    End Function

    ''' <summary>
    ''' obtiene las distribuciones
    ''' </summary>
    ''' <param name="portfolioNoteId"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteDistributionByPortfolioNoteId(portfolioNoteId As Integer) As List(Of PortfolioNoteDistribution) Implements IPortfolioNoteAdminService.GetPortfolioNoteDistributionByPortfolioNoteId
        Try
            Return _portfolioNoteRepository.GetPortfolioNoteDistributionByPortfolioNoteId(portfolioNoteId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of PortfolioNoteDistribution)
        End Try
    End Function

    ''' <summary>
    ''' guardar una nota
    ''' </summary>
    ''' <param name="PortfolioNote"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">PortfolioNote Vacio</exception>
    Public Function SavePortfolioNote(PortfolioNote As PortfolioNote, audit As AuditMessage, session As SessionValues, Optional idSequence As Integer = 0) As ActionResult(Of PortfolioNote) Implements IPortfolioNoteAdminService.SavePortfolioNote
        If PortfolioNote Is Nothing Then
            Throw New ArgumentNullException("PortfolioNote")
        End If

        'UnitWork
        Dim unitOfWork As IUnitWork = _portfolioNoteRepository.UnitWork
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim codeNote As String = String.Empty
                Dim companyType As Byte = CByte(audit.CompanyType)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(PortfolioNote.Id, PortfolioNote.Status)

                Dim xml = PortfolioNote.ToXML()
                Dim resultStore = _portfolioNoteRepository.SP_SavePortfolioNote(xml, audit.CodeUser, companyType).ToList().ElementAt(0)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .Message = resultStore.MessageResult}
                End If

                PortfolioNote.Id = resultStore.Id
                PortfolioNote.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of PortfolioNote)(PortfolioNote, audit, auditStatus, PortfolioNote.OriginalValue)
                auditProcess.Execute()

                '===============================================================================================================================
                'If we're going to Confirm the document
                If PortfolioNote.Status = 2 Then

                    If PortfolioNote.ChangeTracker.State = ObjectState.Added Then
                        PortfolioNote = _portfolioNoteRepository.FirstOrDefault(Function(f) f.Id = resultStore.Id, False, {"PortfolioNoteDetail", "PortfolioNoteDetail.GeneralLedgerIVA", "Customer"})
                    End If

                    'we generate the Electronic Note Document
                    Dim portfolioNoteAccountReceivableAdvance = _portfolioNoteRepository.GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote(PortfolioNote.Id)

                    Dim resultElectronicNote = Me.ElectronicNoteDocumentByPortfolioNote(PortfolioNote, portfolioNoteAccountReceivableAdvance, session, audit)

                    If resultElectronicNote Is Nothing OrElse Not resultElectronicNote.StateResult Then
                        unitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .Message = resultElectronicNote?.Message}
                    End If

                    Dim stringBuilder = New StringBuilder
                    stringBuilder.AppendLine(resultStore.MessageResult)
                    stringBuilder.AppendLine(resultElectronicNote.Message)
                    resultStore.MessageResult = stringBuilder.ToString()
                End If
                '===============================================================================================================================

                PortfolioNote.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of PortfolioNote) With {.StateResult = True, .Message = resultStore.MessageResult, .ObjectEmbbeded = PortfolioNote}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return New ActionResult(Of PortfolioNote) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex), .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Function to create the electronic note document
    ''' </summary>
    ''' <param name="PortfolioNote"></param>
    ''' <param name="session"></param>
    ''' <param name="audit"></param>
    ''' <returns>Action Result, ObjectEmbbeded = BillingNoteId</returns>
    Private Function ElectronicNoteDocumentByPortfolioNote(PortfolioNote As PortfolioNote,
                                                           listPortfolioNoteAccountReceivableAdvance As List(Of PortfolioNoteAccountReceivableAdvance),
                                                           session As SessionValues, Optional audit As AuditMessage = Nothing) As ActionResult(Of Integer)

        'Parameters Validations
        If PortfolioNote Is Nothing OrElse PortfolioNote?.Id = 0 Then
            Throw New ArgumentNullException(NameOf(PortfolioNote), "Parámetro Obligatorio")
        End If

        If Not {1, 2, 6}.ToList().Contains(PortfolioNote.NoteType) Then
            Return New ActionResult(Of Integer) With {.StateResult = True, .Message = "No se crea el documento electronico porque no cumple con las condiciones"}
        End If

        If PortfolioNote.PortfolioNoteDetail Is Nothing Then
            Throw New ArgumentNullException(NameOf(PortfolioNote.PortfolioNoteDetail), "Parámetro Obligatorio")
        End If

        If Not listPortfolioNoteAccountReceivableAdvance?.Any() Then
            Throw New ArgumentNullException(NameOf(listPortfolioNoteAccountReceivableAdvance), "Parámetro Obligatorio")
        End If

        If PortfolioNote.CustomerId Is Nothing Then
            Throw New ArgumentNullException(NameOf(PortfolioNote.CustomerId), "Parámetro Obligatorio")
        End If

        If PortfolioNote.Status <> 2 OrElse Not listPortfolioNoteAccountReceivableAdvance?.Any(Function(invoice) invoice.ConceptId IsNot Nothing) Then
            Return New ActionResult(Of Integer) With {.StateResult = True, .Message = "No se crea el documento electronico porque no cumple con las condiciones"}
        End If

		Dim portfolioNoteAccountReceivableAdvance As List(Of PortfolioNoteAccountReceivableAdvance) = listPortfolioNoteAccountReceivableAdvance
		Dim settingsAccount = _settingsAccountRepository.GetSettingAccountSimple(PortfolioNote.OperatingUnitId)
		'si la URL no es null tiene integración con la DIAN, por ende solo puedo asociar 1 factura
		If settingsAccount.UrlInvoices IsNot Nothing Then
			If listPortfolioNoteAccountReceivableAdvance.Count > 1 Then
				Return New ActionResult(Of Integer) With {.StateResult = False, .Message = "No se puede generar la Nota electrónica con más de una factura asociada"}
			End If
		Else
			'At this moment, we can only create one electronic note per invoice or when the concept is only one
			If PortfolioNote.PortfolioNoteDetail.Count() > 1 Then
				Return New ActionResult(Of Integer) With {.StateResult = False, .Message = "No se puede generar la nota electrónica a multiples conceptos"}
			End If

			'No se permite generar la nota cuando venga mas de una factura y el concepto maneje iva
			If (PortfolioNote.PortfolioNoteDetail.Any(Function(x) x.IdGeneralLedgerIVA IsNot Nothing) AndAlso listPortfolioNoteAccountReceivableAdvance.Count > 1) Then
				Return New ActionResult(Of Integer) With {.StateResult = False, .Message = "No se puede generar la nota electrónica más de una factura con un concepto que maneje impuesto"}
			End If
		End If

		Dim sequenceResult = GetNextCodeElectronicNoteDocument()

		If sequenceResult Is Nothing OrElse Not sequenceResult.StateResult Then
            Return New ActionResult(Of Integer) With {.StateResult = False, .Message = sequenceResult?.Message}
        End If

        'Assign the next code to the electronic note document
        Dim codeNote As String = sequenceResult.ObjectEmbbeded
        Dim billingNoteId As Integer

        If PortfolioNote.Customer?.ThirdPartyId Is Nothing Then
            PortfolioNote.ThirdPartyId = _customerRepository.FirstOrDefault(Function(x) x.Id = PortfolioNote.CustomerId, False).ThirdPartyId
        Else
            PortfolioNote.ThirdPartyId = PortfolioNote.Customer.ThirdPartyId
        End If

        'create de BillingNote object 
        Dim billingNote As BillingNote = CreateBillingNote(PortfolioNote, codeNote)
        Dim listPortfolioNoteAccountReceivableAdvanceIds = portfolioNoteAccountReceivableAdvance.Select(Function(x) x.Id).ToList()
        Dim listInvoice = _portfolioNoteRepository.GetInvoiceByPortfolioNoteAccountReceivableAdvanceId(listPortfolioNoteAccountReceivableAdvanceIds)

        'Create Details Objects
        For Each pnara In portfolioNoteAccountReceivableAdvance
            Dim invoice = listInvoice.Find(Function(x) x.PortfolioNoteAccountReceivableAdvanceId IsNot Nothing AndAlso x.PortfolioNoteAccountReceivableAdvanceId = pnara.Id)
            If invoice IsNot Nothing AndAlso invoice.Id > 0 Then
                Dim baseValue As Decimal

                If PortfolioNote.NoteType = 6 OrElse PortfolioNote.EntityName = GLOSAS_MODULE Then
                    baseValue = pnara.PortfolioNoteAccountReceivableDetail.Sum(Function(s) s.BaseValue)
                Else
                    Dim sign As Integer = If(PortfolioNote.Nature = 1, -1, 1)
                    baseValue = PortfolioNote.PortfolioNoteDetail.Sum(Function(s) If(s.Nature = 1, 1, -1) * s.Value * sign)
                    If baseValue > pnara.AdjusmentValue Then
                        baseValue = pnara.AdjusmentValue
                    End If
                End If
                Dim billingNoteDetail As BillingNoteDetail = CreateBillingNoteDetail(pnara, invoice, baseValue)
                ' Agregar impuestos si aplica
                AddBillingNoteDetailTaxes(PortfolioNote, billingNoteDetail, pnara)
                billingNote.BillingNoteDetail.Add(billingNoteDetail)
            End If
        Next

        Dim unitOfWorkBillingNote As IUnitWork = _billingNoteRepository.UnitWork
        billingNote.CUDE = billingNote.getCUDE()
        _billingNoteRepository.SaveEntity(billingNote)
        unitOfWorkBillingNote.Commit()

        'obtenemos el id de billingNote
        billingNoteId = billingNote.Id

		Dim dianVersion As Decimal = settingsAccount.DianVersion
		Dim transactionalContainer As String = session.TransactionalContainer
        Dim operatingUnit = _operatingUnitRepository.GetOperatingUnitById(PortfolioNote.OperatingUnitId)
        Dim electronicDocument = CreateElectronicDocument(billingNote, dianVersion, transactionalContainer)
        electronicDocument.FilePath = System.IO.Path.Combine(
                    Utils.GetPathElectronicDocuments(),
                    electronicDocument.Container,
                    operatingUnit.UnitCode,
                    electronicDocument.DocumentDate.Year.ToString(),
                    electronicDocument.DocumentDate.Month.ToString(),
                    electronicDocument.getDocumentTypeName(),
                    String.Concat(electronicDocument.Prefix, electronicDocument.DocumentNumber)
                )

        'Obtain the Id of BillingNote to send the event
        Dim EventBillingNote = MapBillingNote(PortfolioNote.Id, billingNoteId, listInvoice)

        If EventBillingNote?.billingNoteDetailEvent?.Count > 0 Then
            _eventProxy.Publish(New EventData(EventBillingNote, NameOf(EventType.BillingNote), NameOf(EventAction.added), audit.Company, audit.CodeUser, DateTime.Now().GetTimestamp))
        End If

        Dim unitOfWorkElectronicDocument As IUnitWork = _electronicDocumentRepository.UnitWork
        _electronicDocumentRepository.SaveEntity(electronicDocument)
        unitOfWorkElectronicDocument.Commit()

        Return New ActionResult(Of Integer) With {.StateResult = True, .Message = $"Se creó la nota electrónica con código: {codeNote}", .ObjectEmbbeded = billingNoteId}
    End Function

    ''' <summary>
    ''' this function obtain the next code of the automatic sequence
    ''' </summary>
    ''' <returns>return action result .stateresult = false when the sequence is manual</returns>
    Private Function GetNextCodeElectronicNoteDocument() As ActionResult(Of String)
        'Secuencia de la Nota credito para la facturacion electronica
        Dim sequence As BillingSequence = _billingSequenseRepository.GetSequenseByIdForm("2037")
        Dim codeNote As String = String.Empty

        If (sequence IsNot Nothing AndAlso sequence.Id > 0 AndAlso sequence.Sequential AndAlso sequence?.BillingSequenceDetail?.Any()) Then
            codeNote = Infrastructure.CrossCutting.Base.Sequense.GetSequense(sequence.BillingSequenceDetail.First().Sequense.Pattern, sequence.BillingSequenceDetail.First().Next)

            If (String.IsNullOrEmpty(codeNote) OrElse codeNote.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)) Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = "La secuencia para cuentas por cobrar alcanzo su valor maximo."}
            End If

            Dim unitOfWorkSequence As IUnitWork = _billingSequenseRepository.UnitWork
            sequence.BillingSequenceDetail.First().Next += 1
            _billingSequenseRepository.SaveEntity(sequence)
            unitOfWorkSequence.Commit()
        Else
            Return New ActionResult(Of String) With {.StateResult = False, .Message = "La secuencia para las Notas Crédito de Facturacion Electronica no esta parametrizada o no es secuencial."}
        End If

        Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = codeNote}
    End Function

    ''' <summary>
    ''' Llena un objeto de la clase BillingNote
    ''' </summary>
    ''' <param name="PortfolioNote"></param>
    ''' <param name="codeNote"></param>
    ''' <returns></returns>
    Private Function CreateBillingNote(ByVal portfolioNote As PortfolioNote, ByVal codeNote As String) As BillingNote
        Return New BillingNote() With {
        .Code = codeNote,
        .NoteDate = DateTime.Now,
        .CustomerPartyId = portfolioNote.ThirdPartyId,
        .Observations = portfolioNote.Observations,
        .Nature = portfolioNote.Nature,
        .OperatingUnitId = portfolioNote.OperatingUnitId,
        .EntityId = portfolioNote.Id,
        .EntityName = portfolioNote.GetType().Name
    }
    End Function

    ''' <summary>
    ''' Llena un objeto de la clase BillingNoteDetail
    ''' </summary>
    ''' <param name="pnara"></param>
    ''' <param name="invoice"></param>
    ''' <param name="baseValue"></param>
    ''' <returns></returns>
    Private Function CreateBillingNoteDetail(ByVal pnara As PortfolioNoteAccountReceivableAdvance, ByVal invoice As Invoice, ByVal baseValue As Decimal) As BillingNoteDetail
        Return New BillingNoteDetail() With {
        .InvoiceId = invoice.Id,
        .InvoiceNumber = invoice.InvoiceNumber,
        .CUFE = If(pnara.ConceptId = 7 Or invoice.CUFE Is Nothing, String.Empty, invoice.CUFE),
        .DocumentDate = invoice.InvoiceDate,
        .AdjusmentValue = pnara.AdjusmentValue,
        .BillingValue = baseValue,
        .DiscountValue = 0,
        .ConceptId = If(pnara.ConceptId Is Nothing, 0, pnara.ConceptId)
    }
    End Function

    ''' <summary>
    ''' Agrega impuestos a BillingNoteDetail
    ''' </summary>
    ''' <param name="portfolioNote"></param>
    ''' <param name="billingNoteDetail"></param>
    Private Sub AddBillingNoteDetailTaxes(ByVal portfolioNote As PortfolioNote, ByRef billingNoteDetail As BillingNoteDetail, Optional ByVal pnara As PortfolioNoteAccountReceivableAdvance = Nothing)
        If billingNoteDetail?.BillingValue Is Nothing OrElse billingNoteDetail.BillingValue = 0 Then
            Exit Sub
        End If

        Dim groupTaxes As IEnumerable(Of Object) = Nothing

        If portfolioNote.NoteType = 6 OrElse portfolioNote.EntityName = GLOSAS_MODULE Then

            If Not HasValidAccountReceivableDetails(pnara?.PortfolioNoteAccountReceivableDetail?.ToList()) Then
                Exit Sub
            End If

            groupTaxes = Me.CalculateGroupTaxesFromAccountReceivable(pnara)

        Else
            If Not HasValidPortfolioNoteDetails(portfolioNote?.PortfolioNoteDetail?.ToList()) Then
                Exit Sub
            End If

            groupTaxes = Me.CalculateGroupTaxesFromPortfolioNote(portfolioNote)

            If groupTaxes.Any(Function(c) c.TaxValue < 0 OrElse c.BaseValue < 0) Then
                groupTaxes = groupTaxes.Where(Function(c) c.TaxValue > 0 AndAlso c.BaseValue > 0)
            End If

            If groupTaxes.Sum(Function(x) DirectCast(x.BaseValue, Decimal)) > billingNoteDetail.BillingValue Then
                Exit Sub
            End If
        End If


        If groupTaxes Is Nothing Then
            Exit Sub
        End If

        For Each item In groupTaxes

            billingNoteDetail.BillingNoteDetailTax.Add(
            New BillingNoteDetailTax With {
                .TaxPercentage = item.TaxPercentage,
                .TaxValue = item.TaxValue,
                .BaseValue = item.BaseValue
            })
        Next
    End Sub

    ''' <summary>
    ''' Funcion que valida  si los detalle de la nota son valido para agrupar impuestos
    ''' </summary>
    ''' <param name="portfolioNoteDetail"></param>
    ''' <returns></returns>
    Private Function HasValidPortfolioNoteDetails(portfolioNoteDetail As List(Of PortfolioNoteDetail)) As Boolean
        Return portfolioNoteDetail IsNot Nothing AndAlso
                portfolioNoteDetail.Any(Function(a) a.IvaRate IsNot Nothing AndAlso a.IvaRate > 0)
    End Function

    ''' <summary>
    ''' funcion que valida si los detalles de las facturas manejan iva
    ''' </summary>
    ''' <param name="portfolioNoteAccountReceivableDetail"></param>
    ''' <returns></returns>
    Private Function HasValidAccountReceivableDetails(portfolioNoteAccountReceivableDetail As List(Of PortfolioNoteAccountReceivableDetail)) As Boolean
        Return portfolioNoteAccountReceivableDetail IsNot Nothing AndAlso
                portfolioNoteAccountReceivableDetail.Any(Function(x) x.TaxPercentage IsNot Nothing AndAlso x.TaxPercentage > 0)

    End Function

    ''' <summary>
    ''' Obtiene los grupos de impuestos para la nota electronica
    ''' </summary>
    ''' <param name="portfolioNote"></param>
    ''' <returns></returns>
    Private Function CalculateGroupTaxesFromPortfolioNote(ByVal portfolioNote As PortfolioNote) As IEnumerable(Of Object)
        Dim sign As Integer = If(portfolioNote.Nature = 1, -1, 1)

        Return portfolioNote.PortfolioNoteDetail _
                            .Where(Function(w) w.IvaRate IsNot Nothing AndAlso w.IvaRate > 0) _
                            .GroupBy(Function(g) g.GeneralLedgerIVA?.Percentage) _
                            .Select(Function(f) New With {
                                Key .TaxPercentage = f.Key,
                                Key .TaxValue = f.Sum(Function(o) If(o.Nature = 1, 1, -1) * o.IvaRate * sign),
                                Key .BaseValue = f.Sum(Function(p) If(p.Nature = 1, 1, -1) * p.Value * sign)
                            })?.ToList()
    End Function

    ''' <summary>
    ''' funcion que obtiene los grupos de impuestos de la factura cuando la nota es de tipo detalle
    ''' </summary>
    ''' <param name="pnara"></param>
    ''' <returns></returns>
    Private Function CalculateGroupTaxesFromAccountReceivable(ByVal pnara As PortfolioNoteAccountReceivableAdvance) As IEnumerable(Of Object)
        Return pnara.PortfolioNoteAccountReceivableDetail _
                                    .Where(Function(x) x.TaxPercentage IsNot Nothing AndAlso x.TaxPercentage > 0) _
                                    .GroupBy(Function(d) d.TaxPercentage) _
                                    .Select(Function(f) New With {
                                        Key .TaxPercentage = f.Key,
                                        Key .TaxValue = f.Sum(Function(o) o.TaxValue),
                                        Key .BaseValue = f.Sum(Function(p) p.BaseValue)
                                    })?.ToList()
    End Function

    ''' <summary>
    ''' Llena un objeto de la clase ElectronicDocument
    ''' </summary>
    ''' <param name="billingNote"></param>
    ''' <param name="dianVersion"></param>
    ''' <param name="transactionalContainer"></param>
    ''' <returns></returns>
    Private Function CreateElectronicDocument(ByVal billingNote As BillingNote, ByVal dianVersion As Decimal, ByVal transactionalContainer As String) As ElectronicDocument
        ' Preparamos dos StringBuilder con capacidad estimada
        Dim prefixSb As New Text.StringBuilder(billingNote.Code.Length)
        Dim numberSb As New Text.StringBuilder(billingNote.Code.Length)

        For Each c As Char In billingNote.Code
            If Char.IsDigit(c) Then
                numberSb.Append(c)
            Else
                prefixSb.Append(c)
            End If
        Next

        Return New ElectronicDocument() With {
        .DianVersion = dianVersion,
        .OperatingUnitId = billingNote.OperatingUnitId,
        .CustomerPartyId = billingNote.CustomerPartyId,
        .EntityId = billingNote.Id,
        .EntityName = billingNote.GetType().Name,
        .DocumentDate = billingNote.NoteDate,
        .DocumentType = billingNote.GetDocumentType(),
        .Status = 1,
        .CreationDate = DateTime.Now,
        .Container = transactionalContainer,
        .Prefix = prefixSb.ToString(),
        .DocumentNumber = numberSb.ToString(),
        .CUFE = billingNote.CUDE,
        .Year = DateTime.Now.Year
    }
    End Function
    ''' <summary>
    ''' Llena un objeto a la clase
    ''' </summary>
    ''' <param name="portfolioNoteId"></param>
    ''' <returns></returns>
    Private Function MapBillingNote(portfolioNoteId As Integer, Optional billingNoteId As Integer = 0, Optional listBills As List(Of Invoice) = Nothing) As BillingNoteEvent
        Dim PortfolioNote = _portfolioNoteRepository.getPortfolioNoteById(portfolioNoteId)
        Dim portfolioNoteAccountReceivableAdvance = _portfolioNoteRepository.GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote(PortfolioNote.Id)
        Dim listInvoice As New List(Of BillingNoteDetailEvent)

        If portfolioNoteAccountReceivableAdvance?.Any() Then

            For Each pnara In portfolioNoteAccountReceivableAdvance

                Dim invoice = New Invoice

                If listBills?.Any(Function(x) x.PortfolioNoteAccountReceivableAdvanceId IsNot Nothing AndAlso x.PortfolioNoteAccountReceivableAdvanceId = pnara.Id) Then
                    invoice = listBills.Find(Function(x) x.PortfolioNoteAccountReceivableAdvanceId IsNot Nothing AndAlso x.PortfolioNoteAccountReceivableAdvanceId = pnara.Id)
                Else
                    invoice = _portfolioNoteRepository.GetInvoiceByPortfolioNoteAccountReceivableAdvanceId(pnara.Id)
                End If

                listInvoice.Add(New BillingNoteDetailEvent With
                {
                    .InvoiceId = invoice.Id,
                    .InvoiceNumber = invoice.InvoiceNumber,
                    .CUFE = invoice.CUFE,
                    .DocumentDate = invoice.InvoiceDate,
                    .AdjusmentValue = pnara.AdjusmentValue,
                    .BillingValue = pnara.AdjusmentValue,
                    .DiscountValue = 0
                })
            Next
        End If
        Return New BillingNoteEvent With
                {
                    .Id = billingNoteId,
                    .EntityId = PortfolioNote.Id,
                    .EntityName = PortfolioNote.GetType().Name,
                    .CodeNotePortfolio = PortfolioNote.Code,
                    .Nature = PortfolioNote.Nature,
                    .ThirdPartyId = PortfolioNote.ThirdPartyId,
                    .OperatingUnitId = PortfolioNote.OperatingUnitId,
                    .Observations = PortfolioNote.Observations,
                    .billingNoteDetailEvent = listInvoice
                }
    End Function

    ''' <summary>
    '''  funcion que se encarga de validar la cta del cxc seleccionada
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Function ValidateSelectedByAccountReceivableAccounting(obj As String) As ActionResult(Of Invoice) Implements IPortfolioNoteAdminService.ValidateSelectedByAccountReceivableAccounting
        Try
            Dim dictArgs = Me.DeserializeJsonToObject(obj)
            Dim key = "AccountReceivableAccountingId"

            If Not dictArgs.ContainsKey(key) OrElse dictArgs(key) Is Nothing Then
                Throw New ArgumentNullException(key, $"El argumento '{key}' no puede ser nulo o vacío.")
            End If

            Dim accountReceivableAccountingId As Integer = dictArgs(key)
            Dim accountReceivable = _accountReceivableRepository.GetAccountReceivableByAccountReceivableAccountingId(accountReceivableAccountingId)
            dictArgs.Add("Id", accountReceivable?.Id)

            Dim newObj = Utils.SerializeObjectToJson(dictArgs)
            Return Me.ValidateSelectedByAccountReceivable(newObj, accountReceivable)

        Catch ex As Exception
            Return New ActionResult(Of Invoice) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Funcion de validacion al momento de seleccionar un cxc
    ''' </summary>
    ''' <param name="obj">objeto  Json, que debe tener el Id de la cxc, tipo de Nota, moneda de la Nota, fecha de la Nota </param>
    ''' <returns>Retorna Objeto Invoice si No es saldo Inicial</returns>
    Private Function ValidateSelectedByAccountReceivable(obj As String, Optional accountReceivable As AccountReceivable = Nothing) As ActionResult(Of Invoice)
        Try

            Dim dictArgs = Me.DeserializeJsonToObject(obj)

            Me.ValidateDictionaryArgument(dictArgs, "Id")
            Me.ValidateDictionaryArgument(dictArgs, "NoteType")
            Me.ValidateDictionaryArgument(dictArgs, "CurrencyId")
            Me.ValidateDictionaryArgument(dictArgs, "NoteDate")

            Dim idAccountReceivable As Integer = dictArgs("Id")
            Dim currencyNoteId As Integer = dictArgs("CurrencyId")
            Dim noteType As Integer = dictArgs("NoteType")
            Dim noteDate As Date = dictArgs("NoteDate")

            ' Si no se proporciona accountReceivable, realizar la consulta
            If accountReceivable Is Nothing Then
                accountReceivable = _accountReceivableRepository.FirstOrDefault(Function(x) x.Id = idAccountReceivable, False, {"Invoice"}.ToList())
            End If

            ' Verificar si accountReceivable sigue siendo Nothing después de la consulta
            If accountReceivable Is Nothing Then
                Return New ActionResult(Of Invoice) With {.StateResult = False, .Message = "No se encontró la cxc"}
            End If

            If accountReceivable.CurrencyId Is Nothing Then
                Dim officialCurrencyId As Integer = _companySettingsRepository.GetCompanySettings().OfficialCurrencyId
                accountReceivable.CurrencyId = officialCurrencyId
            End If

            Dim stringBuilder As StringBuilder = New StringBuilder

            If accountReceivable.CurrencyId <> currencyNoteId Then
                stringBuilder.AppendLine(ResourceManager.GetString("CurrencyDocumentDifferent"))
            End If


            If stringBuilder.Length > 0 Then
                Return New ActionResult(Of Invoice) With {.StateResult = False, .Message = stringBuilder.ToString()}
            End If

            Return New ActionResult(Of Invoice) With {.StateResult = True, .ObjectEmbbeded = accountReceivable.Invoice}

        Catch ex As Exception
            Return New ActionResult(Of Invoice) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' valida los parametros del diccionario
    ''' </summary>
    ''' <param name="dict"></param>
    ''' <param name="key"></param>
    Private Sub ValidateDictionaryArgument(dict As IDictionary(Of String, Object), key As String)

        If Not dict.ContainsKey(key) OrElse dict(key) Is Nothing Then
            Throw New ArgumentNullException(key, $"El argumento '{key}' no puede ser nulo o vacío.")
        End If

        If key <> "NoteDate" AndAlso dict(key).ToString() = "0" Then
            Throw New ArgumentNullException(key, $"El argumento '{key}' no puede ser nulo o vacío.")
        End If
    End Sub

    ''' <summary>
    ''' Metodo para deserealiza el json y validar el objeto
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Private Function DeserializeJsonToObject(obj As String) As IDictionary(Of String, Object)
        If String.IsNullOrEmpty(obj) Then
            Throw New ArgumentNullException(NameOf(obj))
        End If

        Dim parameters As Object = Utils.DeserializeJsonToObject(obj)
        Dim dictArgs As IDictionary(Of String, Object) = TryCast(parameters, IDictionary(Of String, Object))

        If dictArgs Is Nothing Then
            Throw New ArgumentException("El objeto deserializado no es un diccionario válido.", NameOf(obj))
        End If

        Return dictArgs
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If

            _portfolioNoteRepository = Nothing
            _accountReceivableRepository = Nothing
            _portfolioAdvanceRepository = Nothing

            _billingSequenseRepository = Nothing
            _billingNoteRepository = Nothing
            _electronicDocumentRepository = Nothing
            _operatingUnitRepository = Nothing
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