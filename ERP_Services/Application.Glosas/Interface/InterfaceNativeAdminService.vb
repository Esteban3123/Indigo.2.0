'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 22-01-2015
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Base
Imports Domain.InterfaceERPGlosa
Imports Domain.Entities.Service
Imports Application.Accounting
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Resources

Public Class InterfaceNativeAdminService
    Implements IInterfaceNativeAdminService


#Region "Fields"

    ''' <summary>
    ''' repositorio de movimientos glosas
    ''' </summary>
    ''' <remarks></remarks>
    Private _MovementGlosaRepository As IMovementGlosaRepository
    ''' <summary>
    ''' Servicio de secuencias numericas
    ''' </summary>
    ''' <remarks></remarks>
    Private _sequensePortfolioCRepository As ISequensePortfolioCRepository
    ''' <summary>
    ''' Repositorio de secuencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _sequensePortfolioDRepository As ISequensePortfolioDRepository
    ''' <summary>
    ''' Repositorio de Cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountReceivableRepository As IAccountReceivableRepository
    ''' <summary>
    ''' repositorio Grupo de Atencion
    ''' </summary>
    ''' <remarks></remarks>
    Private _careGroupRepository As ICareGroupRepository
    ''' <summary>
    ''' Repositorio de Reclasificacion de cartera - glosas
    ''' </summary>
    ''' <remarks></remarks>
    Private _ReclassificationRepository As IReclassificationRepository
    ''' <summary>
    ''' Repositorio de Estructrura cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Private _AccountReceivableAccountingRepository As IAccountReceivableAccountingRepository
    ''' <summary>
    ''' Repositorio de documento contable
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountingRepository As IAccountingDocumentAdminService
    ''' <summary>
    ''' repositorio de cuotas de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private _AccountReceivableShareRepository As IAccountReceivableShareRepository
    ''' <summary>
    ''' Repositorio de concepto de notas 
    ''' </summary>
    ''' <remarks></remarks>
    Private _PortfolioNoteConceptRepository As IPortfolioNoteConceptRepository
    ''' <summary>
    ''' Servicio de cartera para la creacion de notas 
    ''' </summary>
    ''' <remarks></remarks>
    Private _PortfolioNoteAdminService As IPortfolioNoteAdminService
    ''' <summary>
    ''' repositorio de Honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Private _MedicalFeesCausationRepository As IMedicalFeesCausationRepository
    ''' <summary>
    ''' repositorio de conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Private _ConceptGlosasRepository As IConceptGlosasRepository
    ''' <summary>
    ''' PUC 
    ''' </summary>
    ''' <remarks></remarks>
    Private _PUCRepository As IPUCRepository
    ''' <summary>
    ''' Repositorio de Parametros de Glosas
    ''' </summary>
    ''' <remarks></remarks>
    Private _ITimeGlossParametersRepository As ITimeParametersRepository

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Glosas"
#End Region

#Region "Builder"
    ''' <summary>
    ''' Initializa una nueva instancia de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal MovementGlosaRepository As IMovementGlosaRepository, ByVal sequensePortfolioCRepository As ISequensePortfolioCRepository, ByVal sequenseRepository As ISequensePortfolioDRepository,
                   accountReceivableRepository As IAccountReceivableRepository, careGroupRepository As ICareGroupRepository, ByVal ReclassificationRepository As IReclassificationRepository,
                   AccountReceivableAccountingRepository As IAccountReceivableAccountingRepository, accountingRepository As IAccountingDocumentAdminService, accountReceivableShareRepository As IAccountReceivableShareRepository,
                    PortfolioNoteConceptRepository As IPortfolioNoteConceptRepository, PortfolioNoteAdminService As IPortfolioNoteAdminService, MedicalFeesCausationRepository As IMedicalFeesCausationRepository,
                    ConceptGlosasRepository As IConceptGlosasRepository, PUCRepository As IPUCRepository, ITimeGlossParametersRepository As ITimeParametersRepository)
        If MovementGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Moviminetos Glosa Vacío")
        End If
        If sequensePortfolioCRepository Is Nothing Then
            Throw New ArgumentNullException("sequensePortfolioCRepository", "Repositorio de Secuencias Vacio")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository", "Repositorio de Secuencias Vacio")
        End If
        If accountReceivableRepository Is Nothing Then
            Throw New ArgumentNullException("accountReceivableRepository vacio", "repositorio de cuentas por cobrar vacio")
        End If
        If careGroupRepository Is Nothing Then
            Throw New ArgumentNullException("careGroupRepository vacio", "repositorio de centro de atencion vacio")
        End If
        If ReclassificationRepository Is Nothing Then
            Throw New ArgumentNullException("ReclassificationRepository vacio", "Repositorio de reclasificacion vacio")
        End If
        If AccountReceivableAccountingRepository Is Nothing Then
            Throw New ArgumentNullException("AccountReceivableAccountingRepository vacio", "Repositorio de estructura de cuenta de cobro vacio")
        End If
        If accountingRepository Is Nothing Then
            Throw New ArgumentNullException("AccountingRepository vacio", "Repositorio de Contabilidad vacio")
        End If
        If accountReceivableShareRepository Is Nothing Then
            Throw New ArgumentNullException("accountReceivableShareRepository vacio", "repositorio de cuotas de cuentas por cobrar vacio")
        End If
        If PortfolioNoteConceptRepository Is Nothing Then
            Throw New ArgumentNullException("PortfolioNoteConceptRepository vacio", "Repositorio de conceptos de notas vacio")
        End If
        If PortfolioNoteAdminService Is Nothing Then
            Throw New ArgumentNullException("PortfolioNoteAdminService vacio", "Servicio de cartera vacio")
        End If
        If MedicalFeesCausationRepository Is Nothing Then
            Throw New ArgumentNullException("MedicalFeesCausationRepository vacio", "repositorio de honorarios medicos vacio")
        End If
        If ConceptGlosasRepository Is Nothing Then
            Throw New ArgumentNullException("ConceptGlosasRepository vacio", "repositorio de concepto de glosa vacio")
        End If
        If PUCRepository Is Nothing Then
            Throw New ArgumentNullException("PUCRepository")
        End If
        _MovementGlosaRepository = MovementGlosaRepository
        _sequensePortfolioCRepository = sequensePortfolioCRepository
        _sequensePortfolioDRepository = sequenseRepository
        _accountReceivableRepository = accountReceivableRepository
        _careGroupRepository = careGroupRepository
        _ReclassificationRepository = ReclassificationRepository
        _AccountReceivableAccountingRepository = AccountReceivableAccountingRepository
        _accountingRepository = accountingRepository
        _AccountReceivableShareRepository = accountReceivableShareRepository
        _PortfolioNoteConceptRepository = PortfolioNoteConceptRepository
        _PortfolioNoteAdminService = PortfolioNoteAdminService
        _MedicalFeesCausationRepository = MedicalFeesCausationRepository
        _ConceptGlosasRepository = ConceptGlosasRepository
        _PUCRepository = PUCRepository
        _ITimeGlossParametersRepository = ITimeGlossParametersRepository
    End Sub
#End Region

#Region "MetHods"

    ''' <summary>
    ''' Funcion para la creacion del documento de reclasificacion de cartera
    ''' </summary>
    ''' <param name="_idSequence">Id secuencia a crear</param>
    ''' <param name="_AccountReceivable">Obj de la cartera</param>
    ''' <param name="_BalanceInvoice">valor del documento</param>
    ''' <param name="audit">Objeto info. de auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreatePortfolioReclassification(ByVal TypePortfolioReclassification As TypePortfolioReclassification, ByVal _idSequence As Integer,
                                                     ByVal _AccountReceivable As AccountReceivable, ByVal _BalanceInvoice As Decimal, ByVal _ThirdPartyId As Integer, ByVal GlossParameter As TimeParameters, ByVal audit As AuditMessage) As ActionResult Implements IInterfaceNativeAdminService.CreatePortfolioReclassification
        If TypePortfolioReclassification = 0 Then
            Throw New ArgumentNullException("TypePortfolioReclassification", "RadicateInvoiceAdminService.CreatePortfolioReclassification")
        End If
        If _idSequence = 0 Then
            Throw New ArgumentNullException("_idSequence", "RadicateInvoiceAdminService.CreatePortfolioReclassification")
        End If
        If _AccountReceivable Is Nothing Then
            Throw New ArgumentNullException("_AccountReceivable", "RadicateInvoiceAdminService.CreatePortfolioReclassification")
        End If
        If _BalanceInvoice = 0 Then
            Throw New ArgumentNullException("_BalanceInvoice", "RadicateInvoiceAdminService.CreatePortfolioReclassification")
        End If
        If _ThirdPartyId = 0 Then
            Throw New ArgumentNullException("_ThirdPartyId", "RadicateInvoiceAdminService.CreatePortfolioReclassification")
        End If
        Dim result As New ActionResult
        Try
            'consulta secuencia reclasificacion documentos de cartera
            Dim _sequense As Domain.Entities.PortfolioSequence = Me._sequensePortfolioCRepository.GetSequenseByIdForm("1529") 'tag del formulario de notaradicacion de cuentas para cargar secuencia de reclasificacion 
            If _sequense IsNot Nothing AndAlso _sequense.Id > 0 AndAlso _sequense.Sequential Then
                If _sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    _idSequence = _sequense.PortfolioSequenceDetail(0).Id
                ElseIf _sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    If _sequense.PortfolioSequenceDetail.Any() Then
                        _idSequence = _sequense.PortfolioSequenceDetail.FirstOrDefault().Id
                    Else
                        Return New ActionResult With {.StateResult = False, .MessageResult = {ResourceManager.GetString("OperatingUnitUnassigned")}.ToList()}
                    End If
                End If
            Else
                Return New ActionResult With {.StateResult = False, .MessageResult = {"No se encuentra parametrizada la secuencia numérica para Reclasificación de documentos de cartera"}.ToList()}
            End If

            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim sequenceUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
                Dim ReclassificationUnitOfWork As IUnitWork = Me._ReclassificationRepository.UnitWork
                Dim seq As PortfolioSequenceDetail = Nothing
                'secuencia
                Dim _PortfolioReclassification As New PortfolioReclassification
                seq = _sequensePortfolioDRepository.GetSequenseDById(_idSequence)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        _PortfolioReclassification.Code = res
                        seq.Next += 1
                        Me._sequensePortfolioDRepository.SaveEntity(seq)
                    Else
                        scope.Dispose()
                        Return New ActionResult With {.StateResult = False, .MessageResult = {"La secuencia numérica llegó a su máximo valor"}.ToList()}
                    End If
                Else
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .MessageResult = {"No se encuentra parametrizada la secuencia numérica para Radicación de Cuentas"}.ToList()}
                End If
                Dim tmpDate As DateTime = DateTime.Now
                Dim _SourceAccountId As Integer
                Dim _TargetAccountId As Integer
                Dim _CommentVouchers As String = String.Empty
                Dim _JournalVoucherTypeId As Integer
                If TypePortfolioReclassification = 1 Then
                    _JournalVoucherTypeId = GlossParameter.RadicationJournalVoucherTypeId
                    _SourceAccountId = _AccountReceivable.AccountWithoutRadicateId
                    _TargetAccountId = _AccountReceivable.AccountRadicateId
                    _CommentVouchers = "Comprobante de reclasificacion - Radicacion De cuentas"
                ElseIf TypePortfolioReclassification = 2 Then
                    _JournalVoucherTypeId = GlossParameter.DevolutionJournalVoucherTypeId
                    _SourceAccountId = _AccountReceivable.AccountRadicateId
                    _TargetAccountId = _AccountReceivable.AccountWithoutRadicateId
                    _CommentVouchers = "Comprobante de reclasificacion - Devolución De cuentas"
                ElseIf TypePortfolioReclassification = 3 Then
                    _JournalVoucherTypeId = GlossParameter.ReceptionObjectionJournalVoucherTypeId
                    _SourceAccountId = _AccountReceivable.AccountRadicateId
                    _TargetAccountId = _AccountReceivable.AccountObjectionRemediedId
                    _CommentVouchers = "Comprobante de reclasificacion - Recepcion de Objeciones Glosa Subsanable"
                ElseIf TypePortfolioReclassification = 4 Then
                    _JournalVoucherTypeId = GlossParameter.ConciliationJournalVoucherTypeId
                    _SourceAccountId = _AccountReceivable.AccountObjectionRemediedId
                    _TargetAccountId = _AccountReceivable.AccountConciliationId
                    _CommentVouchers = "Comprobante de reclasificacion - Conciliaciones"
                ElseIf TypePortfolioReclassification = 5 Then
                    _JournalVoucherTypeId = GlossParameter.TransferLegalJournalVoucherTypeId
                    _SourceAccountId = _AccountReceivable.AccountObjectionRemediedId
                    _TargetAccountId = _AccountReceivable.AccountLegalCollectionId
                    _CommentVouchers = "Comprobante de reclasificacion - Traslado Cobro Juridico"
                ElseIf TypePortfolioReclassification = 6 Then
                    _JournalVoucherTypeId = GlossParameter.ConciliationJournalVoucherTypeId
                    _SourceAccountId = _AccountReceivable.AccountObjectionRemediedId
                    _TargetAccountId = _AccountReceivable.AccountConciliationId
                    _CommentVouchers = "Comprobante de reclasificacion - Aceptacion EAPB por saldo restante en reiteraciones"
                End If
                'creamos documento de reclasificacion
                With _PortfolioReclassification
                    .DocumentType = TypePortfolioReclassification
                    .AccountReceivableId = _AccountReceivable.Id
                    .SourceAccountId = _SourceAccountId 'id cuenta origen
                    .TargetAccountId = _TargetAccountId 'Id Cuenta destino
                    .Value = _BalanceInvoice
                    .Status = 2 ' confirmado
                    .CreationUser = audit.CodeUser
                    .CreationDate = tmpDate
                    .ModificationUser = audit.CodeUser
                    .ModificationDate = tmpDate
                    .ConfirmationUser = audit.CodeUser
                    .ConfirmationDate = tmpDate
                End With
                _ReclassificationRepository.SaveEntity(_PortfolioReclassification)
                sequenceUnitOfWork.Commit()
                ReclassificationUnitOfWork.Commit()

                Dim messageReturn As String = "Doc. Reclasificación " & _PortfolioReclassification.Code

                'Si la cuenta radicada es diferente a la cuenta de glosa subsanable se realiza el comprobante contable,
                'esto se realiza porque en Medilaser manejan dos cuentas en glosas(Radicada y Sin Radicar)
                If _AccountReceivable.AccountRadicateId <> _AccountReceivable.AccountObjectionRemediedId Then
                    Dim PortfolioServices As New PortfolioServices(_PUCRepository)
                    'Creo el documento contable
                    Dim accounting As JournalVouchers = PortfolioServices.CreateAccountingAccount(_JournalVoucherTypeId,
                                                                                                _CommentVouchers,
                                                                                                _PortfolioReclassification.Code,
                                                                                                _PortfolioReclassification.Id,
                                                                                                GetType(PortfolioReclassification).Name,
                                                                                                _SourceAccountId,
                                                                                                _TargetAccountId,
                                                                                                _ThirdPartyId,
                                                                                                _CommentVouchers,
                                                                                                _BalanceInvoice,
                                                                                                _AccountReceivable.CostCenterId)
                    Dim resultAccounting As ActionMessageResult(Of JournalVouchers)
                    resultAccounting = _accountingRepository.SaveAccountingDocument(accounting, audit)
                    If resultAccounting.StateResult = False Then
                        scope.Dispose()
                        Return New ActionResult() With {.StateResult = False, .MessageResult = {resultAccounting.Message.ToString()}.ToList()}
                    End If

                    messageReturn = messageReturn + " " + " Comprobante Contable " & resultAccounting.ObjectEmbbeded.Consecutive.ToString()
                End If

                Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioReclassification)
                auditProcess = New IndigoAuditSimpleEntity(Of PortfolioReclassification)(_PortfolioReclassification, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditProcess.Execute()
                scope.Complete()
                Dim ListStrMessage As New List(Of String)
                ListStrMessage.Add(messageReturn)
                result.StateResult = True
                result.MessageResult = ListStrMessage
            End Using
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.MessageResult = New List(Of String)({ex.Message.ToString}), .StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para la creacion del documento de reclasificacion de cartera de una transferencia juridica
    ''' </summary>
    ''' <param name="TypePortfolioReclassification">tipo de reclassificacion</param>
    ''' <param name="_idSequence">Id secuencia a crear</param>
    ''' <param name="_AccountReceivable">Obj  de la cartera</param>
    ''' <param name="listAccount">lista de cuentas contables de la factura con saldo</param>
    ''' <param name="_ThirdPartyId">tercero</param>
    ''' <param name="GlossParameter">parametros de glosa</param>
    ''' <param name="audit">Objeto info. de auditoria</param>
    ''' <returns></returns>
    Private Function CreatePortfolioReclassificationLegalTransfer(ByVal TypePortfolioReclassification As TypePortfolioReclassification, ByVal _idSequence As Integer,
                                                     ByVal _AccountReceivable As AccountReceivable, ByVal listAccount As List(Of Tuple(Of Integer, Decimal)), ByVal _ThirdPartyId As Integer, ByVal GlossParameter As TimeParameters, ByVal audit As AuditMessage) As ActionResult Implements IInterfaceNativeAdminService.CreatePortfolioReclassificationLegalTransfer
        If TypePortfolioReclassification = 0 Then
            Throw New ArgumentNullException("TypePortfolioReclassification", "RadicateInvoiceAdminService.CreatePortfolioReclassification")
        End If
        If _idSequence = 0 Then
            Throw New ArgumentNullException("_idSequence", "RadicateInvoiceAdminService.CreatePortfolioReclassification")
        End If
        If _AccountReceivable Is Nothing Then
            Throw New ArgumentNullException("_AccountReceivable", "RadicateInvoiceAdminService.CreatePortfolioReclassification")
        End If
        If listAccount Is Nothing OrElse listAccount.Count = 0 Then
            Throw New ArgumentNullException("_BalanceInvoice", "RadicateInvoiceAdminService.CreatePortfolioReclassification")
        End If
        If _ThirdPartyId = 0 Then
            Throw New ArgumentNullException("_ThirdPartyId", "RadicateInvoiceAdminService.CreatePortfolioReclassification")
        End If
        Dim result As New ActionResult
        Dim _BalanceInvoice As Decimal
        _BalanceInvoice = listAccount(0).Item2
        If _BalanceInvoice = 0 Then
            Throw New ArgumentNullException("_BalanceInvoice", "RadicateInvoiceAdminService.CreatePortfolioReclassification")
        End If
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim sequenceUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
                Dim ReclassificationUnitOfWork As IUnitWork = Me._ReclassificationRepository.UnitWork
                Dim seq As PortfolioSequenceDetail = Nothing
                'secuencia
                Dim _PortfolioReclassification As New PortfolioReclassification
                Dim _PortfolioReclassificationAux As New PortfolioReclassification
                seq = _sequensePortfolioDRepository.GetSequenseDById(_idSequence)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        _PortfolioReclassification.Code = res
                        seq.Next += 1
                        Me._sequensePortfolioDRepository.SaveEntity(seq)
                    Else
                        scope.Dispose()
                        Return New ActionResult With {.StateResult = False, .MessageResult = {"La secuencia numérica llegó a su máximo valor"}.ToList()}
                    End If
                Else
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .MessageResult = {"No se encuentra parametrizada la secuencia numérica para Radicación de Cuentas"}.ToList()}
                End If
                Dim tmpDate As DateTime = DateTime.Now
                Dim _SourceAccountId As Integer
                Dim _TargetAccountId As Integer
                Dim _CommentVouchers As String = String.Empty
                Dim _JournalVoucherTypeId As Integer
                If TypePortfolioReclassification = 1 Then
                    _JournalVoucherTypeId = GlossParameter.RadicationJournalVoucherTypeId
                    _SourceAccountId = _AccountReceivable.AccountWithoutRadicateId
                    _TargetAccountId = _AccountReceivable.AccountRadicateId
                    _CommentVouchers = "Comprobante de reclasificacion - Radicacion De cuentas"
                ElseIf TypePortfolioReclassification = 2 Then
                    _JournalVoucherTypeId = GlossParameter.DevolutionJournalVoucherTypeId
                    _SourceAccountId = _AccountReceivable.AccountRadicateId
                    _TargetAccountId = _AccountReceivable.AccountWithoutRadicateId
                    _CommentVouchers = "Comprobante de reclasificacion - Devolución De cuentas"
                ElseIf TypePortfolioReclassification = 3 Then
                    _JournalVoucherTypeId = GlossParameter.ReceptionObjectionJournalVoucherTypeId
                    _SourceAccountId = _AccountReceivable.AccountRadicateId
                    _TargetAccountId = _AccountReceivable.AccountObjectionRemediedId
                    _CommentVouchers = "Comprobante de reclasificacion - Recepcion de Objeciones Glosa Subsanable"
                ElseIf TypePortfolioReclassification = 4 Then
                    _JournalVoucherTypeId = GlossParameter.ConciliationJournalVoucherTypeId
                    _SourceAccountId = _AccountReceivable.AccountObjectionRemediedId
                    _TargetAccountId = _AccountReceivable.AccountConciliationId
                    _CommentVouchers = "Comprobante de reclasificacion - Conciliaciones"
                ElseIf TypePortfolioReclassification = 5 Then
                    _JournalVoucherTypeId = GlossParameter.TransferLegalJournalVoucherTypeId
                    _SourceAccountId = listAccount(0).Item1
                    _TargetAccountId = _AccountReceivable.AccountLegalCollectionId
                    _CommentVouchers = "Comprobante de reclasificacion - Traslado Cobro Juridico"
                ElseIf TypePortfolioReclassification = 6 Then
                    _JournalVoucherTypeId = GlossParameter.ConciliationJournalVoucherTypeId
                    _SourceAccountId = _AccountReceivable.AccountObjectionRemediedId
                    _TargetAccountId = _AccountReceivable.AccountConciliationId
                    _CommentVouchers = "Comprobante de reclasificacion - Aceptacion EAPB por saldo restante en reiteraciones"
                End If
                'creamos documento de reclasificacion
                With _PortfolioReclassification
                    .DocumentType = TypePortfolioReclassification
                    .AccountReceivableId = _AccountReceivable.Id
                    .SourceAccountId = _SourceAccountId 'id cuenta origen
                    .TargetAccountId = _TargetAccountId 'Id Cuenta destino
                    .Value = _BalanceInvoice
                    .Status = 2 ' confirmado
                    .CreationUser = audit.CodeUser
                    .CreationDate = tmpDate
                    .ModificationUser = audit.CodeUser
                    .ModificationDate = tmpDate
                    .ConfirmationUser = audit.CodeUser
                    .ConfirmationDate = tmpDate
                End With

                'si no se logra crear el documento de reclasificacion retornamos error 
                If _PortfolioReclassification Is Nothing OrElse _PortfolioReclassification.AccountReceivableId = 0 Then
                    scope.Dispose()
                    Throw New Exception("No se logró realizar la reclasificación para la factura = " & _AccountReceivable.InvoiceNumber)
                End If

                _PortfolioReclassificationAux = _PortfolioReclassification.Clone
                _ReclassificationRepository.SaveEntity(_PortfolioReclassification)
                Dim contador = 0
                For Each a In listAccount
                    If contador <> 0 Then
                        _PortfolioReclassification = _PortfolioReclassificationAux.Clone
                        _PortfolioReclassification.SourceAccountId = a.Item1
                        _PortfolioReclassification.Value = a.Item2
                        _ReclassificationRepository.SaveEntity(_PortfolioReclassification)
                    End If
                    contador = contador + 1
                Next
                sequenceUnitOfWork.Commit()
                ReclassificationUnitOfWork.Commit()

                Dim messageReturn As String = "Doc. Reclasificación " & _PortfolioReclassification.Code

                'Si la cuenta radicada es diferente a la cuenta de glosa subsanable se realiza el comprobante contable,
                'esto se realiza porque en Medilaser manejan dos cuentas en glosas(Radicada y Sin Radicar)
                If _AccountReceivable.AccountRadicateId <> _AccountReceivable.AccountObjectionRemediedId Then
                    Dim PortfolioServices As New PortfolioServices(_PUCRepository)
                    'Creo el documento contable
                    Dim accounting As JournalVouchers = PortfolioServices.CreateAccountingAccount(_JournalVoucherTypeId,
                                                                                                _CommentVouchers,
                                                                                                _PortfolioReclassification.Code,
                                                                                                _PortfolioReclassification.Id,
                                                                                                GetType(PortfolioReclassification).Name,
                                                                                                _SourceAccountId,
                                                                                                _TargetAccountId,
                                                                                                _ThirdPartyId,
                                                                                                _CommentVouchers,
                                                                                                _BalanceInvoice,
                                                                                                _AccountReceivable.CostCenterId)

                    Dim _journalVoucherDetail As JournalVoucherDetails
                    contador = 0
                    For Each a In listAccount
                        If contador <> 0 Then
                            _journalVoucherDetail = accounting.JournalVoucherDetails.Where(Function(d) d.IdMainAccount = _SourceAccountId).FirstOrDefault.Clone
                            _journalVoucherDetail.IdMainAccount = a.Item1
                            _journalVoucherDetail.CreditValue = a.Item2
                            accounting.JournalVoucherDetails.Add(_journalVoucherDetail)
                            _journalVoucherDetail = accounting.JournalVoucherDetails.Where(Function(d) d.IdMainAccount = _TargetAccountId).FirstOrDefault
                            _journalVoucherDetail.DebitValue += a.Item2
                        End If
                        contador = contador + 1
                    Next


                    Dim resultAccounting As ActionMessageResult(Of JournalVouchers)
                    resultAccounting = _accountingRepository.SaveAccountingDocument(accounting, audit)

                    If resultAccounting.StateResult = False Then
                        scope.Dispose()
                        Return New ActionResult() With {.StateResult = False, .MessageResult = {resultAccounting.Message.ToString()}.ToList()}
                    End If

                    messageReturn = messageReturn + " " + " Comprobante Contable " & resultAccounting.ObjectEmbbeded.Consecutive.ToString()
                End If

                Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioReclassification)
                auditProcess = New IndigoAuditSimpleEntity(Of PortfolioReclassification)(_PortfolioReclassification, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditProcess.Execute()
                scope.Complete()
                Dim ListStrMessage As New List(Of String)
                ListStrMessage.Add(messageReturn)
                result.StateResult = True
                result.MessageResult = ListStrMessage
            End Using
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.MessageResult = New List(Of String)({ex.Message.ToString}), .StateResult = False}
        End Try
    End Function


    ''' <summary>
    ''' Crea comprobante contable movimiento cuentas de orden para empresas del sector publico
    ''' </summary>
    ''' <param name="_AccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function createJournalVouchersCompanyPublic(ByVal First As Integer, ByVal itemD As GlosaObjectionsReceptionD, _AccountReceivable As AccountReceivable, ByVal GlossParameter As TimeParameters, valueGlosa As Decimal, IndigoSessionValues As SessionValues) As ActionResult Implements IInterfaceNativeAdminService.createJournalVouchersCompanyPublic
        Dim ListStrMessage As New List(Of String)
        If _AccountReceivable.AccountCreditorOrder Is Nothing Or _AccountReceivable.AccountCreditorOrder = 0 Then
            ListStrMessage.Add("La cuenta de orden acreedoras de glosa no esta configurada para la factura: " & _AccountReceivable.InvoiceNumber)
            Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
        End If
        If _AccountReceivable.AccountDebtorOrder Is Nothing Or _AccountReceivable.AccountDebtorOrder = 0 Then
            ListStrMessage.Add("La cuenta de orden debito no esta configurada para la factura: " & _AccountReceivable.InvoiceNumber)
            Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
        End If
        If GlossParameter.ReceptionObjectionJournalVoucherTypeId = 0 Then
            ListStrMessage.Add("El tipo de documento contable para recepcion de objeciones no esta configurada para la factura: " & _AccountReceivable.InvoiceNumber)
            Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
        End If
        Dim _SourceAccountId As Integer
        Dim _TargetAccountId As Integer
        Dim _CommentVouchers As String = String.Empty
        If First = 1 Then
            _SourceAccountId = _AccountReceivable.AccountCreditorOrder
            _TargetAccountId = _AccountReceivable.AccountDebtorOrder
            _CommentVouchers = "Comprobante de Recepcion de Objeciones - Glosa Subsanable"
        Else
            _SourceAccountId = _AccountReceivable.AccountDebtorOrder
            _TargetAccountId = _AccountReceivable.AccountCreditorOrder
            _CommentVouchers = "Comprobante de tramite de Objeciones - Glosa Subsanable"
        End If
        Dim _JournalVoucherTypeId As Integer = GlossParameter.ReceptionObjectionJournalVoucherTypeId
        Dim PortfolioServices As New PortfolioServices(_PUCRepository)
        'Creo el documento contable
        Dim accounting As JournalVouchers = PortfolioServices.CreateAccountingAccount(_JournalVoucherTypeId,
                                                                                    _CommentVouchers,
                                                                                    itemD.InvoiceNumber,
                                                                                    If(itemD.ReiterateId Is Nothing, itemD.Id, itemD.ReiterateId),
                                                                                    GetType(GlosaObjectionsReceptionD).Name,
                                                                                    _SourceAccountId,
                                                                                    _TargetAccountId,
                                                                                    _AccountReceivable.ThirdPartyId,
                                                                                    _CommentVouchers,
                                                                                    valueGlosa,
                                                                                    _AccountReceivable.CostCenterId)
        Dim resultAccounting As ActionMessageResult(Of JournalVouchers)
        resultAccounting = _accountingRepository.SaveAccountingDocument(accounting, IndigoSessionValues.AuditMessageWcf)
        If resultAccounting.StateResult = False Then
            Return New ActionResult() With {.StateResult = False, .MessageResult = {resultAccounting.Message}.ToList()}
        End If
        ListStrMessage.Add("Se generarón los Sig. Documentos: Comprobante Contable " & resultAccounting.ObjectEmbbeded.Consecutive.ToString())
        Return New ActionResult() With {.StateResult = True, .MessageResult = ListStrMessage}
    End Function

    ''' <summary>
    ''' Funcion de creacion de Nota Creditos
    ''' </summary>
    Public Function createCreditNote(ByVal typeAcceptedIPS As ETypeAcceptedIPSModule, ByVal _GlosaPortfolioGlosada As GlosaPortfolioGlosada, ByVal _AccountReceivable As AccountReceivable, ByVal GlossParameter As TimeParameters, ByVal _idSequence As Integer, ByVal _idOperativeUnit As Integer, ByVal Session As SessionValues, Optional ConciliationCId As Integer? = Nothing) As ActionResult Implements IInterfaceNativeAdminService.createCreditNote
        Dim audit As AuditMessage = Session.AuditMessageWcf
        Dim tmpDate As DateTime = DateTime.Now
        Dim _PortfolioNoteConceptId As Integer
        Dim Anio As Integer = Year(_GlosaPortfolioGlosada.InvoiceDate)
        Dim _MainAccountId As Integer
        Dim ListStrMessage As New List(Of String)
        Dim _AffectedService As Boolean
        Dim _AccountReceivableShare As AccountReceivableShare = _AccountReceivableShareRepository.GetAccountReceivableSharebyAccountId(_AccountReceivable.Id)

        'el cargue de la estructura de cuentas por cobrar depende de la entidad si es PRIVADA o PUBLICA
        Dim AccountReceivableAccounting As AccountReceivableAccounting = Nothing
        If Session.IndigoCompanyType = eCompanyType.PrivateCompany Then
            If _AccountReceivable.AccountObjectionRemediedId Is Nothing Then
                ListStrMessage.Add("La cuenta contable de glosa subsanada esta vacia, para el fatura: " & _AccountReceivable.InvoiceNumber)
                Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
            End If
            AccountReceivableAccounting = _AccountReceivableAccountingRepository.GetAccountReceivableAccounting(_AccountReceivable.Id, _AccountReceivable.AccountObjectionRemediedId)
            If AccountReceivableAccounting.Id = 0 Then
                If _AccountReceivable.AccountRadicateId > 0 Then
                    AccountReceivableAccounting = _AccountReceivableAccountingRepository.GetAccountReceivableAccounting(_AccountReceivable.Id, _AccountReceivable.AccountRadicateId)
                End If
                If AccountReceivableAccounting.Id = 0 Then
                    ListStrMessage.Add("Estructura de cartera no existe para la cuenta por cobrar: " & _AccountReceivable.Code & " - cuenta contable de glosa subsanable ")
                    Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
                End If
            End If
        ElseIf Session.IndigoCompanyType = eCompanyType.PublicCompany Then
            'Se realiza esta validación por el bug 9326
            'Id de la cuenta contable para poder validar la conciliación dependiendo del estado
            Dim mainAccountId As Integer = Nothing

            If _AccountReceivable.PortfolioStatus = 15 OrElse (_AccountReceivable.PortfolioStatus = 16 AndAlso _GlosaPortfolioGlosada.TempState = 15) Then 'Si el estado de la cxc es de dificil recaudo se obtiene la cuenta AccountHardCollectionId
                mainAccountId = _AccountReceivable.AccountHardCollectionId
            Else 'Sino se obtiene la cuenta radicada a entidad AccountRadicateId
                mainAccountId = _AccountReceivable.AccountRadicateId
            End If

            AccountReceivableAccounting = _AccountReceivableAccountingRepository.GetAccountReceivableAccounting(_AccountReceivable.Id, mainAccountId)
            If AccountReceivableAccounting.Id = 0 Then
                ListStrMessage.Add("Estructura de cartera no existe para la cuenta por cobrar: " & _AccountReceivable.Code & " - cuenta contable de factura radicada ")
                Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
            End If
        End If

        If GlossParameter.AffectedService Is Nothing Then
            ListStrMessage.Add("El parámetro 'Afecta Servicios Aceptaciones Glosa' no está parametrizado. Es necesario configurarlo para habilitar las funcionalidades asociadas.")
            Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
        End If

        _AffectedService = GlossParameter.AffectedService

        If Anio = Year(tmpDate) AndAlso Not _AffectedService Then

            If GlossParameter.GeneralGlossConceptNoteId Is Nothing Then
                ListStrMessage.Add("El concepto de la nota de cartera de Glosas General para las aceptaciones IPS esta vacio, para la factura: " & _AccountReceivable.InvoiceNumber)
                Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
            End If

            _PortfolioNoteConceptId = GlossParameter.GeneralGlossConceptNoteId ' si no afecta servicio tomamos el concepto general y la cuenta general del concepto
            Dim _PortfolioNoteConcept As Domain.Entities.PortfolioNoteConcept = _PortfolioNoteConceptRepository.GetPortfolioNoteConceptById(_PortfolioNoteConceptId)

            If _PortfolioNoteConcept.MainAccounts Is Nothing Then
                ListStrMessage.Add("El concepto de la nota de cartera de Glosas General no tiene cuenta contable, Concepto de Nota: " & _PortfolioNoteConcept.Code & " - " & _PortfolioNoteConcept.Name)
                Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
            End If

            _MainAccountId = _PortfolioNoteConcept.MainAccounts.Id   'se toma la cuenta del concepto
        End If

        If Anio <> Year(tmpDate) Then
            _AffectedService = False
            _PortfolioNoteConceptId = GlossParameter.PreviousLifetimesConceptNoteId
            Dim _PortfolioNoteConcept As Domain.Entities.PortfolioNoteConcept = _PortfolioNoteConceptRepository.GetPortfolioNoteConceptById(_PortfolioNoteConceptId)
            _MainAccountId = _PortfolioNoteConcept.MainAccounts.Id ' se toma la cuneta del concepto
        End If

        Dim _ListObj As List(Of Object) = Nothing
        Dim _comments As String = String.Empty
        Dim _AdjusmentValue As Decimal
        Dim entityName As String = MODULE_NAME
        If typeAcceptedIPS = ETypeAcceptedIPSModule.AcceptanceGlossProcessed Then
            _ListObj = _MovementGlosaRepository.ListExpanDObj_AcceptedIPSFirtsInstance(_AccountReceivable.InvoiceNumber, _AffectedService)
            _comments = "Aceptacion IPS glosa Subsanable - Glosa"
            _AdjusmentValue = _GlosaPortfolioGlosada.ValueAcceptedFirstInstance
        ElseIf typeAcceptedIPS = ETypeAcceptedIPSModule.AcceptanceReiterationProcessed Then
            _ListObj = _MovementGlosaRepository.ListExpanDObj_AcceptedIPSSecondInstance(_AccountReceivable.InvoiceNumber, _AffectedService)
            _comments = "Aceptacion IPS glosa Subsanable - Reiteracion"
            _AdjusmentValue = _GlosaPortfolioGlosada.ValueAcceptedSecondInstance
        ElseIf typeAcceptedIPS = ETypeAcceptedIPSModule.AcceptanceConciliationProcessed Then
            _ListObj = _MovementGlosaRepository.ListExpanDObj_AcceptedIPSConciliation(_AccountReceivable.InvoiceNumber, ConciliationCId, _AffectedService)
            _comments = "Aceptacion IPS glosa conciliacion"
            _AdjusmentValue = _GlosaPortfolioGlosada.ValueAcceptedIPSconciliation
        End If

        Dim result = ValidateDataToCreatePortfolioNote(_AffectedService, _AdjusmentValue, _ListObj, _PortfolioNoteConceptId)

        If result Is Nothing OrElse Not result.StateResult Then
            ListStrMessage.Add(result?.Message)
            Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
        End If

        'Si es una factura electronica, la glosa al crear la nota se debe enviar a la dian con el concepto 1: Devolución o no aceptación de partes del servicio
        Dim conceptId As Integer? = Nothing
        Dim cufe As String = _accountReceivableRepository.GetCUFEByAccountReceivableId(_AccountReceivable.Id)
        If Not String.IsNullOrEmpty(cufe) Then
            conceptId = 1
        End If

        Dim PortfolioNote As New PortfolioNote
        With PortfolioNote
            .NoteDate = tmpDate
            .CustomerId = _AccountReceivable.CustomerId
            .Observations = _comments
            .Nature = 2 ' credito
            .NoteType = If(_AffectedService, EPortfolioNoteType.Detail, EPortfolioNoteType.TotalInvoice) 'si afecta servicios = nota tipo detalle sino factura total
            .OperatingUnitId = _idOperativeUnit
            .Status = 2 'confirmado
            .CreationUser = audit.CodeUser
            .CreationDate = tmpDate
            .ModificationUser = audit.CodeUser
            .ModificationDate = tmpDate
            .ConfirmationUser = audit.CodeUser
            .ConfirmationDate = tmpDate
            .EntityName = entityName
            .CurrencyId = _AccountReceivable?.CurrencyId

            Dim _PortfolioNoteAccountReceivableAdvance As New PortfolioNoteAccountReceivableAdvance
            With _PortfolioNoteAccountReceivableAdvance
                .AccountReceivableId = _AccountReceivable.Id
                .AccountReceivableShareId = _AccountReceivableShare.Id
                .MainAccountId = AccountReceivableAccounting.MainAccountId
                .AccountReceivableAccountingId = AccountReceivableAccounting.Id
                .PortfolioAdvanceId = Nothing
                .AdjusmentValue = _AdjusmentValue
                .PercentageValue = 0
                .ConceptId = conceptId
            End With

            If Not _AffectedService Then
                Dim _PortfolioNoteDetail As New PortfolioNoteDetail
                With _PortfolioNoteDetail
                    .PortfolioNoteConceptId = _PortfolioNoteConceptId
                    .MainAccountId = _MainAccountId  'si no afecta por servicio, tomamos la cuenta del concepto
                    .CostCenterId = GlossParameter.PreviousLifetimesCostCenterId
                    .Value = _AdjusmentValue
                    .ThirdPartyId = _AccountReceivable.ThirdPartyId
                    .Nature = 1 'Debito
                    .Observations = _comments
                End With
                .PortfolioNoteDetail.Add(_PortfolioNoteDetail)
            End If

            For Each item In _ListObj
                Dim portfolioNoteAccountReceivableDetail As New PortfolioNoteAccountReceivableDetail
                With portfolioNoteAccountReceivableDetail
                    .EntityName = item.EntityName
                    .EntityId = item.EntityId
                    If _AffectedService Then
                        .MainAccountId = item.MainAccountId ' tomamos cuenta del servicio con el que se facturo el ingreso
                        .CostCenterId = item.CostCenterId 'tomamos el centro de costo del servicio
                    Else
                        .MainAccountId = _MainAccountId
                        .CostCenterId = GlossParameter.PreviousLifetimesCostCenterId
                    End If
                    .TaxId = item.TaxId
                    .TaxPercentage = item.TaxPercent
                    Dim dictionaryResult = Utils.SetValueSalesPrice(True, item.Value, item.TaxPercent)
                    .BaseValue = dictionaryResult.Item(Utils.eTypeTaxControl.GrossValue)
                    .TaxValue = dictionaryResult.Item(Utils.eTypeTaxControl.TaxValue)
                    .Value = dictionaryResult.Item(Utils.eTypeTaxControl.SubtotalSalesPrice)
                End With
                _PortfolioNoteAccountReceivableAdvance.PortfolioNoteAccountReceivableDetail.Add(portfolioNoteAccountReceivableDetail)
            Next
            .PortfolioNoteAccountReceivableAdvance.Add(_PortfolioNoteAccountReceivableAdvance)
        End With

        Dim _result As New ActionResult
        Dim _resultSaveAndConfirm = _PortfolioNoteAdminService.SavePortfolioNote(PortfolioNote, audit, Session, _idSequence)
        If _resultSaveAndConfirm.StateResult = True Then
            _result.StateResult = True
            ListStrMessage.Add(_resultSaveAndConfirm.Message.ToString())
            _result.MessageResult = ListStrMessage
            Return _result
        Else
            _result.StateResult = False
            ListStrMessage.Add(_resultSaveAndConfirm.Message.ToString())
            _result.MessageResult = ListStrMessage
            Return _result
        End If
    End Function

    ''' <summary>
    ''' funcion para validar los parametro para crear la nota de cartera
    ''' </summary>
    ''' <param name="isDetail"></param>
    ''' <param name="adjusmentValue"></param>
    ''' <param name="listObj"></param>
    ''' <param name="portfolioNoteConceptId"></param>
    ''' <returns></returns>
    Private Function ValidateDataToCreatePortfolioNote(isDetail As Boolean,
                                                      adjusmentValue As Decimal,
                                                      listObj As List(Of Object),
                                                      Optional portfolioNoteConceptId As Integer? = Nothing) As ActionResult

        If (listObj Is Nothing OrElse Not listObj.Any()) Then
            Return New ActionResult With {.StateResult = False, .Message = "Lista de movimientos no puede estar vacia "}
        End If

        If adjusmentValue <= 0 Then
            Return New ActionResult With {.StateResult = False, .Message = "El valor del ajuste tiene que ser mayor a 0"}
        End If

        If Not listObj.Exists(Function(x) CType(x, IDictionary(Of String, Object)).ContainsKey("EntityName")) Then
            Return New ActionResult With {.StateResult = False, .Message = "Para crear la Nota de tipo detalle es necesario la propiedad ""EntityName"""}
        End If

        If Not listObj.Exists(Function(x) CType(x, IDictionary(Of String, Object)).ContainsKey("EntityId")) Then
            Return New ActionResult With {.StateResult = False, .Message = "Para crear la Nota de tipo detalle es necesario la propiedad ""EntityId"""}
        End If

        If Not listObj.Exists(Function(x) CType(x, IDictionary(Of String, Object)).ContainsKey("TaxPercent")) Then
            Return New ActionResult With {.StateResult = False, .Message = "Para crear la Nota de tipo detalle es necesario la propiedad ""TaxPercent"""}
        End If

        If Not listObj.Exists(Function(x) CType(x, IDictionary(Of String, Object)).ContainsKey("TaxId")) Then
            Return New ActionResult With {.StateResult = False, .Message = "Para crear la Nota de tipo detalle es necesario la propiedad ""TaxId"""}
        End If

        If Not isDetail AndAlso portfolioNoteConceptId Is Nothing Then
            Return New ActionResult With {.StateResult = False, .Message = "Para crear la Nota de tipo Factura Total es necesario tener el concepto al cual se va asociar"}
        End If

        Return New ActionResult With {.StateResult = True}
    End Function

    Public Function GeneratePortfolioReclasification(operatingUnitId As Integer, radicateInvoiceId As Integer, codeUser As String) As ActionResult Implements IInterfaceNativeAdminService.GeneratePortfolioReclasification
        Try
            Dim result = _ITimeGlossParametersRepository.GeneratePoortfolioReclasification(operatingUnitId, radicateInvoiceId, codeUser)
            If result.StateResult Then
                Return New ActionResult With {.StateResult = True, .Message = result.Message}
            Else
                Return New ActionResult With {.StateResult = False, .Message = result.Message}
            End If

        Catch ex As Exception
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _PortfolioNoteAdminService.Dispose()
            End If
            _MovementGlosaRepository = Nothing
            _sequensePortfolioCRepository = Nothing
            _sequensePortfolioDRepository = Nothing
            _accountReceivableRepository = Nothing
            _careGroupRepository = Nothing
            _ReclassificationRepository = Nothing
            _AccountReceivableAccountingRepository = Nothing
            _accountingRepository = Nothing
            _AccountReceivableShareRepository = Nothing
            _PortfolioNoteConceptRepository = Nothing
            _PortfolioNoteAdminService = Nothing
            _MedicalFeesCausationRepository = Nothing
            _ConceptGlosasRepository = Nothing
            _PUCRepository = Nothing
            _ITimeGlossParametersRepository = Nothing
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
