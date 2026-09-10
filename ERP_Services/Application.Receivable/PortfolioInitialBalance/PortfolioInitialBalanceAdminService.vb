'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 17-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity.Core
Imports System.Data.Entity.Validation
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class PortfolioInitialBalanceAdminService
    Implements IPortfolioInitialBalanceAdminService


#Region "Field"
    Private _portfolioInitialBalanceRepository As IPortfolioInitialBalanceRepository
    Private _accountReceivableRepository As IAccountReceivableRepository
    Private _sequensePortfolioDRepository As ISequensePortfolioDRepository
    Private _thirdPartyRepository As IThirdPartyRepository
    Private _customerRepository As ICustomerRepository
    Private _accountingRepository As IPUCRepository
    Private _costCenterRepository As ICostCenterRepository
    Private _portfolioNoteConceptRepository As IPortfolioNoteConceptRepository
    Private _documentTypeRepository As IDocumentTypeRepository
    Private _billingInvoiceCategoriesRepository As IBillingInvoiceCategories
    Private _contractAccountingStructureRepository As IContractAccountingStructureRepository
#End Region

#Region "Builder"
    Public Sub New(portfolioInitialBalanceRepository As IPortfolioInitialBalanceRepository, accountReceivableRepository As IAccountReceivableRepository,
                   sequensePortfolioDRepository As ISequensePortfolioDRepository, thirdPartyRepository As IThirdPartyRepository,
                   customerRepository As ICustomerRepository, accountingRepository As IPUCRepository, costCenterRepository As ICostCenterRepository,
                   portfolioNoteConceptRepository As IPortfolioNoteConceptRepository, documentTypeRepository As IDocumentTypeRepository,
                   billingInvoiceCategoriesRepository As IBillingInvoiceCategories,
                   contractAccountingStructureRepository As IContractAccountingStructureRepository)
        If portfolioInitialBalanceRepository Is Nothing Then
            Throw New ArgumentNullException("portfolioInitialBalanceRepository")
        End If
        If accountReceivableRepository Is Nothing Then
            Throw New ArgumentNullException("accountReceivableRepository")
        End If
        If sequensePortfolioDRepository Is Nothing Then
            Throw New ArgumentNullException("sequensePortfolioDRepository")
        End If
        _portfolioInitialBalanceRepository = portfolioInitialBalanceRepository
        _accountReceivableRepository = accountReceivableRepository
        _sequensePortfolioDRepository = sequensePortfolioDRepository
        _thirdPartyRepository = thirdPartyRepository
        _customerRepository = customerRepository
        _accountingRepository = accountingRepository
        _costCenterRepository = costCenterRepository
        _portfolioNoteConceptRepository = portfolioNoteConceptRepository
        _documentTypeRepository = documentTypeRepository
        _billingInvoiceCategoriesRepository = billingInvoiceCategoriesRepository
        _contractAccountingStructureRepository = contractAccountingStructureRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' confirmar el saldo incial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ConfirmPortfolioInitialBalance(idPortfolioInitialBalance As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of String) Implements IPortfolioInitialBalanceAdminService.ConfirmPortfolioInitialBalance
        Try
            Dim summary = _portfolioInitialBalanceRepository.ConfirmPortfolioInitialBalanceSetBased(
                idPortfolioInitialBalance,
                audit.CodeUser,
                ServerSessionValues.Current.CurrentContainer)
            Dim confirmationParts As New List(Of String)

            If summary.Created > 0 Then
                confirmationParts.Add(String.Format("{0} factura(s) confirmada(s)", summary.Created))
            End If
            If summary.OmittedConflicts > 0 Then
                confirmationParts.Add(String.Format("{0} factura(s) en conflicto omitida(s)", summary.OmittedConflicts))
            End If

            Return New ActionResult(Of String) With {
                .StateResult = True,
                .Message = String.Join(" - ", confirmationParts),
                .ObjectEmbbeded = idPortfolioInitialBalance.ToString()
            }
        Catch ex As Exception
            Dim businessSqlMessage = GetBusinessSqlErrorMessage(ex)
            If Not String.IsNullOrEmpty(businessSqlMessage) Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = businessSqlMessage}
            End If

            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {
                .StateResult = False,
                .Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            }
        End Try
    End Function

    Private Shared Function GetBusinessSqlErrorMessage(exception As Exception) As String
        Dim currentException = exception
        For depth As Integer = 0 To 4
            If currentException Is Nothing Then Exit For

            Dim sqlException = TryCast(currentException, SqlException)
            If sqlException IsNot Nothing Then
                For Each sqlError As SqlError In sqlException.Errors
                    If sqlError.Number = 51000 Then Return sqlError.Message
                Next
                Return Nothing
            End If

            currentException = currentException.InnerException
        Next
        Return Nothing
    End Function

    Public Function GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of Domain.Entities.PortfolioInitialBalanceAccountReceivable) Implements IPortfolioInitialBalanceAdminService.GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance
        Try
            Return _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of PortfolioInitialBalanceAccountReceivable)
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener los anticipos del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of Domain.Entities.PortfolioInitialBalanceAdvance) Implements IPortfolioInitialBalanceAdminService.GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance
        Try
            Return _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of PortfolioInitialBalanceAdvance)
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener el saldo inicial por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code</exception>
    Public Function GetPortfolioInitialBalanceByCode(code As String, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.PortfolioInitialBalance Implements IPortfolioInitialBalanceAdminService.GetPortfolioInitialBalanceByCode
        If code Is String.Empty Then
            Throw New ArgumentNullException("Code")
        End If
        Try
            Dim portfolioInitialBalance = _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceByCode(code)
            If portfolioInitialBalance IsNot Nothing AndAlso portfolioInitialBalance.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioInitialBalance)(portfolioInitialBalance, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return portfolioInitialBalance
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PortfolioInitialBalance()
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener el saldo inicial por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceById(id As Integer) As Domain.Entities.PortfolioInitialBalance Implements IPortfolioInitialBalanceAdminService.GetPortfolioInitialBalanceById
        Try
            Return _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PortfolioInitialBalance()
        End Try
    End Function

    ''' <summary>
    ''' metodo para guardar y confirmar el saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Function SaveAndConfirmPortfolioInitialBalance(PortfolioInitialBalance As Domain.Entities.PortfolioInitialBalance, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Integer = 0) As Domain.Base.Entities.ActionResult(Of String) Implements IPortfolioInitialBalanceAdminService.SaveAndConfirmPortfolioInitialBalance
        Dim message As String = String.Empty
        ' Save path discriminado por Import flag (staging vs entity directa). Confirm path siempre se invoca tras
        ' Save OK, independiente del origen del registro (Import Excel o entrada manual). Frontend realiza pre-check
        ' Cosmos via RCM antes de invocar este método para garantizar que toda factura con CUV tenga JSON subido.
        Dim resultSave As ActionResult(Of PortfolioInitialBalance)
        If PortfolioInitialBalance.Import Then
            resultSave = SavePortfolioInitialBalanceImportFile(PortfolioInitialBalance, audit, idSequence)
        Else
            resultSave = SavePortfolioInitialBalance(PortfolioInitialBalance, audit, idSequence)
        End If

        If Not resultSave.StateResult Then
            message = String.Format(ResourceManager.GetString("NotSave", "Portfolio"), resultSave.Message)
            Return New ActionResult(Of String) With {.StateResult = False, .StateResultAux = False, .Message = message}
        End If

        Dim resultConfirm = ConfirmPortfolioInitialBalance(resultSave.ObjectEmbbeded.Id, audit)
        If resultConfirm.StateResult = True Then
            message = String.Format(ResourceManager.GetString("SaveAndConfirmDocument", "Portfolio"), resultSave.ObjectEmbbeded.Code, resultConfirm.Message)
            Return New ActionResult(Of String) With {.StateResult = True, .StateResultAux = True, .Message = message}
        Else
            message = String.Format(ResourceManager.GetString("SaveButNotConfirm", "Portfolio"), resultSave.ObjectEmbbeded.Code, resultConfirm.Message)
            Return New ActionResult(Of String) With {.StateResult = True, .StateResultAux = False, .Message = message}
        End If
    End Function
    ''' <summary>
    ''' metodo para guardar el documento cuando esta importando desde excel
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SavePortfolioInitialBalanceImportFile(PortfolioInitialBalance As Domain.Entities.PortfolioInitialBalance, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Integer = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioInitialBalance)

        Try
            Dim seq As PortfolioSequenceDetail = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioInitialBalance)
            Dim status As Integer
            Dim auxPortfolioInitialBalance As PortfolioInitialBalance = Nothing
            Dim _portfolioInitialBalanceUnitOfWork As IUnitWork = _portfolioInitialBalanceRepository.UnitWork
            Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
            If PortfolioInitialBalance.Code Is Nothing OrElse PortfolioInitialBalance.Code.Trim().Equals(String.Empty) Then
                seq = _sequensePortfolioDRepository.GetSequenseDById(idSequence)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        PortfolioInitialBalance.Code = res
                        seq.Next += 1
                        Me._sequensePortfolioDRepository.SaveEntity(seq)
                    Else

                        Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else

                    Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Select Case PortfolioInitialBalance.Status
                Case 1
                    If PortfolioInitialBalance.ChangeTracker.State = ObjectState.Added Then
                        PortfolioInitialBalance.CreationUser = audit.CodeUser
                        PortfolioInitialBalance.CreationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert
                    Else
                        PortfolioInitialBalance.ModificationDate = DateTime.Now
                        PortfolioInitialBalance.ModificationUser = audit.CodeUser
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                        auxPortfolioInitialBalance = PortfolioInitialBalance.OriginalValue
                    End If
                Case 3
                    PortfolioInitialBalance.ModificationDate = DateTime.Now
                    PortfolioInitialBalance.ModificationUser = audit.CodeUser
                    PortfolioInitialBalance.AnnulmentDate = DateTime.Now
                    PortfolioInitialBalance.AnnulmentUser = audit.CodeUser
                    auxPortfolioInitialBalance = PortfolioInitialBalance.OriginalValue
            End Select
            Dim useBulkImportPersistence = PortfolioInitialBalance.Import AndAlso
                                           PortfolioInitialBalance.PortfolioInitialBalanceAccountReceivable.Count > 0 AndAlso
                                           PortfolioInitialBalance.PortfolioInitialBalanceAdvance.Count = 0
            If useBulkImportPersistence Then
                _portfolioInitialBalanceRepository.RegisterAccountReceivableImportBatch(PortfolioInitialBalance)
            Else
                _portfolioInitialBalanceRepository.SaveEntity(PortfolioInitialBalance)
            End If
            If useBulkImportPersistence Then
                _portfolioInitialBalanceRepository.CommitAccountReceivableImportBatch()
            Else
                _portfolioInitialBalanceUnitOfWork.Commit()
            End If
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of PortfolioInitialBalance)(PortfolioInitialBalance, audit, status, auxPortfolioInitialBalance)
            auditProcess.Execute()

            Return New ActionResult(Of PortfolioInitialBalance) With {.ObjectEmbbeded = PortfolioInitialBalance, .StateResult = True}

        Catch ex As OptimisticConcurrencyException

            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As DbEntityValidationException

            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception

            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try


    End Function

    ''' <summary>
    ''' guardar saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Function SavePortfolioInitialBalance(PortfolioInitialBalance As Domain.Entities.PortfolioInitialBalance, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Integer = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioInitialBalance) Implements IPortfolioInitialBalanceAdminService.SavePortfolioInitialBalance
        If PortfolioInitialBalance Is Nothing Then
            Throw New ArgumentNullException("PortfolioInitialBalance")
        End If
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TimeSpan.FromMinutes(30) 'TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As PortfolioSequenceDetail = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioInitialBalance)
                Dim status As Integer
                Dim auxPortfolioInitialBalance As PortfolioInitialBalance = Nothing
                Dim _portfolioInitialBalanceUnitOfWork As IUnitWork = _portfolioInitialBalanceRepository.UnitWork
                Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
                If PortfolioInitialBalance.Code Is Nothing OrElse PortfolioInitialBalance.Code.Trim().Equals(String.Empty) Then
                    seq = _sequensePortfolioDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            PortfolioInitialBalance.Code = res
                            seq.Next += 1
                            Me._sequensePortfolioDRepository.SaveEntity(seq)
                        Else
                            transaction.Dispose()
                            Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        transaction.Dispose()
                        Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                Select Case PortfolioInitialBalance.Status
                    Case 1
                        If PortfolioInitialBalance.ChangeTracker.State = ObjectState.Added Then
                            PortfolioInitialBalance.CreationUser = audit.CodeUser
                            PortfolioInitialBalance.CreationDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Insert
                        Else
                            PortfolioInitialBalance.ModificationDate = DateTime.Now
                            PortfolioInitialBalance.ModificationUser = audit.CodeUser
                            status = Infrastructure.CrossCutting.Audit.Actions.Update
                            auxPortfolioInitialBalance = PortfolioInitialBalance.OriginalValue
                        End If
                    Case 3
                        PortfolioInitialBalance.ModificationDate = DateTime.Now
                        PortfolioInitialBalance.ModificationUser = audit.CodeUser
                        PortfolioInitialBalance.AnnulmentDate = DateTime.Now
                        PortfolioInitialBalance.AnnulmentUser = audit.CodeUser
                        auxPortfolioInitialBalance = PortfolioInitialBalance.OriginalValue
                End Select
                Dim importPersistenceTimer As Stopwatch = Nothing
                Dim importedRowsCount As Integer = 0
                Dim useBulkImportPersistence = PortfolioInitialBalance.Import AndAlso
                                               PortfolioInitialBalance.PortfolioInitialBalanceAccountReceivable.Count > 0 AndAlso
                                               PortfolioInitialBalance.PortfolioInitialBalanceAdvance.Count = 0
                If useBulkImportPersistence Then
                    importedRowsCount = PortfolioInitialBalance.PortfolioInitialBalanceAccountReceivable.Count
                    importPersistenceTimer = Stopwatch.StartNew()
                    _portfolioInitialBalanceRepository.RegisterAccountReceivableImportBatch(PortfolioInitialBalance)
                    Debug.WriteLine(String.Format(
                        "Portfolio import: registro EF {0:N2} s; filas {1}.",
                        importPersistenceTimer.Elapsed.TotalSeconds,
                        importedRowsCount))
                    importPersistenceTimer.Restart()
                Else
                    _portfolioInitialBalanceRepository.SaveEntity(PortfolioInitialBalance)
                End If
                If useBulkImportPersistence Then
                    _portfolioInitialBalanceRepository.CommitAccountReceivableImportBatch()
                Else
                    _portfolioInitialBalanceUnitOfWork.Commit()
                End If
                If importPersistenceTimer IsNot Nothing Then
                    Debug.WriteLine(String.Format(
                        "Portfolio import: BulkSaveChanges {0:N2} s.",
                        importPersistenceTimer.Elapsed.TotalSeconds))
                End If
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of PortfolioInitialBalance)(PortfolioInitialBalance, audit, status, auxPortfolioInitialBalance)
                auditProcess.Execute()
                transaction.Complete()
                If PortfolioInitialBalance.Import Then
                    ' El import solo consume Id/Code. Evitar devolver y serializar nuevamente
                    ' el grafo completo del lote (facturas, accounting y shares).
                    Dim importSummary As New PortfolioInitialBalance With {
                        .Id = PortfolioInitialBalance.Id,
                        .Code = PortfolioInitialBalance.Code,
                        .Status = PortfolioInitialBalance.Status,
                        .Import = True
                    }
                    Return New ActionResult(Of PortfolioInitialBalance) With {.ObjectEmbbeded = importSummary, .StateResult = True}
                End If
                Return New ActionResult(Of PortfolioInitialBalance) With {.ObjectEmbbeded = PortfolioInitialBalance, .StateResult = True}

            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    Public Function SetAdvanceCopyPaste(data As List(Of List(Of String))) As ActionResult(Of List(Of PortfolioInitialBalanceAdvance)) Implements IPortfolioInitialBalanceAdminService.SetAdvanceCopyPaste
        Dim portfolioService As New PortfolioServices(_accountingRepository, _thirdPartyRepository, _customerRepository, _costCenterRepository)
        Return portfolioService.SetAdvancesCopyPaste(data)
    End Function

    Public Function SetBillsCopyPaste(data As List(Of List(Of String)), companyType As Integer) As ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable)) Implements IPortfolioInitialBalanceAdminService.SetBillsCopyPaste
        Dim portfolioService As New PortfolioServices(_accountReceivableRepository, _accountingRepository, _thirdPartyRepository, _customerRepository, _costCenterRepository, _portfolioNoteConceptRepository, _documentTypeRepository, _billingInvoiceCategoriesRepository)
        Return portfolioService.SetBillsCopyPaste(data, companyType)
    End Function
#End Region

    ''' <summary>
    ''' metodo para validar la carga del archivo para los saldo iniciales
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function ValidateFileBillsInitialBalance(data As List(Of ImportFileRow), companyType As Integer) As ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable)) Implements IPortfolioInitialBalanceAdminService.ValidateFileBillsInitialBalance
        Dim portfolioService As New PortfolioServices(_accountReceivableRepository, _accountingRepository, _thirdPartyRepository, _customerRepository, _costCenterRepository, _portfolioNoteConceptRepository, _documentTypeRepository, _billingInvoiceCategoriesRepository, _contractAccountingStructureRepository)
        Return portfolioService.SetBillsImportFile(data, companyType)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _portfolioInitialBalanceRepository = Nothing
            _accountReceivableRepository = Nothing
            _sequensePortfolioDRepository = Nothing
            _thirdPartyRepository = Nothing
            _customerRepository = Nothing
            _accountingRepository = Nothing
            _costCenterRepository = Nothing
            _portfolioNoteConceptRepository = Nothing
            _documentTypeRepository = Nothing
            _billingInvoiceCategoriesRepository = Nothing
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
