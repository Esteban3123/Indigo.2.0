'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 25-09-2014
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
Imports System.Data.Entity.Validation
Imports Application.Payments
Imports Application.Portfolio
Imports Application.Accounting
Imports Application.Common

#End Region

Public Class CrossingAccountAdminService
    Implements ICrossingAccountAdminService

#Region "Fields"

    Private Const MODULE_NAME As String = "Treasury"
    ''' <summary>
    ''' Repositorio de tarjetas
    ''' </summary>
    Private _crossingAccountRepository As ICrossingAccountRepository
    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseTreasuryDRepository
    ''' <summary>
    ''' Repositorio de cuentas contables
    ''' </summary>
    Private _pucRepository As IPUCRepository
    ''' <summary>
    ''' Repositorio de meses cerrados
    ''' </summary>
    Private _closeMonthRepository As ICloseMonthRepository
    ''' <summary>
    ''' repositorios de cruce de cuentas CxP
    ''' </summary>
    Private _crossingAccountDetailCxP As ICrossingAccountDetailCxPRepository
    ''' <summary>
    ''' repositorios de cruce de cuentas CxC
    ''' </summary>
    Private _crossingAccountDetailCxC As ICrossingAccountDetailCxCRepository
    ''' <summary>
    ''' Variable tipo repositorio para parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingsTreasuryRepository As ISettingsTreasuryRepository
    ''' <summary>
    ''' repositorio de cuentas por pagar
    ''' </summary>
    Private _accountPayableRepository As IAccountPayableRepository
    ''' <summary>
    ''' repositorio de recibos de caja
    ''' </summary>
    Private _accountReceivableRepository As IAccountReceivableRepository
    ''' <summary>
    ''' servicios de aplicacion de cuentas por pagar
    ''' </summary>
    Private _accountPayableAdminService As IAccountPayableAdminService
    ''' <summary>
    ''' servicios de aplicacion de recibos de caja
    ''' </summary>
    Private _accountReceivableAdminService As IAccountReceivableAdminService
    ''' <summary>
    ''' servicios de aplicacion de contabilidad
    ''' </summary>
    Private _accountingAdminService As IAccountingDocumentAdminService

    Private treasuryService As ITreasuryServices

    Private _treasuryControlAdminService As ITreasuryControlAdminService

    Private _currencyAdminService As ICurrencyAdminService

    Private _companySettingsRepository As ICompanySettingsRepository

#End Region

#Region "Methods"
    Public Sub New(ByVal crossingAccountRepository As ICrossingAccountRepository, ByVal secuenseDRepository As ISequenseTreasuryDRepository, ByVal pucRepository As IPUCRepository,
                   ByVal closeMonthRepository As ICloseMonthRepository, ByVal crossingAccountDetailCxP As ICrossingAccountDetailCxPRepository,
                   ByVal crossingAccountDetailCxC As ICrossingAccountDetailCxCRepository, ByVal settingsTreasuryRepository As ISettingsTreasuryRepository,
                   ByVal accountPayableRepository As IAccountPayableRepository, ByVal accountReceivableRepository As IAccountReceivableRepository,
                   ByVal accountPayableAdminService As IAccountPayableAdminService, ByVal accountReceivableAdminService As IAccountReceivableAdminService,
                   ByVal accountingAdminService As IAccountingDocumentAdminService, _treasuryService As ITreasuryServices,
                   treasuryControlAdminService As ITreasuryControlAdminService, companySettingsRepository As ICompanySettingsRepository, currencyAdminService As ICurrencyAdminService)
        If crossingAccountRepository Is Nothing Then
            Throw New ArgumentNullException("crossingAccountRepository")
        End If
        If accountingAdminService Is Nothing Then
            Throw New ArgumentNullException("accountingAdminService")
        End If
        If accountPayableAdminService Is Nothing Then
            Throw New ArgumentNullException("accountPayableAdminService")
        End If
        If accountReceivableAdminService Is Nothing Then
            Throw New ArgumentNullException("accountReceivableAdminService")
        End If
        If accountReceivableRepository Is Nothing Then
            Throw New ArgumentNullException("accountReceivableRepository")
        End If
        If accountPayableRepository Is Nothing Then
            Throw New ArgumentNullException("accountPayableRepository")
        End If
        If settingsTreasuryRepository Is Nothing Then
            Throw New ArgumentNullException("settingsTreasuryRepository")
        End If
        If crossingAccountDetailCxC Is Nothing Then
            Throw New ArgumentNullException("crossingAccountDetailCxC")
        End If
        If crossingAccountDetailCxP Is Nothing Then
            Throw New ArgumentNullException("crossingAccountDetailCxP")
        End If
        If closeMonthRepository Is Nothing Then
            Throw New ArgumentNullException("closeMonthRepository")
        End If
        If pucRepository Is Nothing Then
            Throw New ArgumentNullException("pucRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _crossingAccountRepository = crossingAccountRepository
        _secuenseDRepository = secuenseDRepository
        _pucRepository = pucRepository
        _closeMonthRepository = closeMonthRepository
        _crossingAccountDetailCxP = crossingAccountDetailCxP
        _crossingAccountDetailCxC = crossingAccountDetailCxC
        _settingsTreasuryRepository = settingsTreasuryRepository
        _accountPayableRepository = accountPayableRepository
        _accountReceivableRepository = accountReceivableRepository
        _accountPayableAdminService = accountPayableAdminService
        _accountReceivableAdminService = accountReceivableAdminService
        _accountingAdminService = accountingAdminService
        treasuryService = _treasuryService
        _treasuryControlAdminService = treasuryControlAdminService
        Me._companySettingsRepository = companySettingsRepository
        Me._currencyAdminService = currencyAdminService
    End Sub
#End Region

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por codigo
    ''' </summary>
    Public Function GetCrossingAccount(code As String, audit As AuditMessage, Optional tracking As Boolean = False) As ActionResult(Of CrossingAccount) Implements ICrossingAccountAdminService.GetCrossingAccount
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim crossingAccount As CrossingAccount = Me._crossingAccountRepository.GetCrossingAccount(code.Trim(), tracking)
            If crossingAccount IsNot Nothing AndAlso crossingAccount.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CrossingAccount)(crossingAccount, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CrossingAccount) With {.StateResult = True, .ObjectEmbbeded = crossingAccount}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CrossingAccount) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por ir
    ''' </summary>
    Public Function GetCrossingAccountById(Id As Integer, Optional tracking As Boolean = False) As CrossingAccount Implements ICrossingAccountAdminService.GetCrossingAccountById
        Try
            Return _crossingAccountRepository.GetCrossingAccountById(Id, tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina un cruce de cuentas
    ''' </summary>
    Public Function DeleteCrossingAccount(crossingAccount As CrossingAccount, audit As AuditMessage) As ActionResult Implements ICrossingAccountAdminService.DeleteCrossingAccount
        If crossingAccount Is Nothing Then
            Throw New ArgumentNullException("crossingAccount")
        End If
        Dim unitOfWork As IUnitWork = Me._crossingAccountRepository.UnitWork
        Try
            If crossingAccount.ChangeTracker.State = ObjectState.Deleted Then
                crossingAccount.ModificationUser = audit.CodeUser
                crossingAccount.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CrossingAccount)(crossingAccount, audit, status)
                Me._crossingAccountRepository.DeleteEntity(crossingAccount)
                unitOfWork.Commit()
                auditProcess.Execute()
                Return New ActionResult With {.StateResult = True}
            Else
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorUnknown")}
            End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Guardar Cruce de Cuentas
    ''' </summary>
    Public Function SaveCrossingAccount(crossingAccount As CrossingAccount, audit As AuditMessage, withConfirm As Boolean, Optional idSequence As Long = 0) As ActionResult(Of CrossingAccount) Implements ICrossingAccountAdminService.SaveCrossingAccount
        If crossingAccount Is Nothing Then
            Throw New ArgumentNullException("crossingAccount")
        End If
        Dim unitOfWork As IUnitWork = Me._crossingAccountRepository.UnitWork
        Dim unitWorkSequence As IUnitWork = Me._secuenseDRepository.UnitWork
        Dim listTRM As New List(Of TRM)
        Dim officialCurrency = _companySettingsRepository.FirstOrDefault(Function(x) True, False, {"Currency"})?.Currency
        Dim resultValidateVoucher As ActionResult(Of String) = treasuryService.ValidateCrossingAccountSave(crossingAccount)
        If resultValidateVoucher.StateResult = False Then
            Return New ActionResult(Of CrossingAccount) With {.StateResult = False, .Message = resultValidateVoucher.Message}
        End If

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                If String.IsNullOrEmpty(crossingAccount.Code) Then
                    Dim seq As TreasurySequenceDetail = _secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            crossingAccount.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Throw New Exception(ResourceManager.GetString("SequenceNotFound"))
                        End If
                    Else
                        Throw New Exception(ResourceManager.GetString("SequenceNotFound"))
                    End If
                End If

                Dim auxCrossingAccount As CrossingAccount = Nothing
                Dim state As Integer
                If crossingAccount.ChangeTracker.State = ObjectState.Added Then
                    crossingAccount.CreationDate = Date.Now
                    crossingAccount.CreationUser = audit.CodeUser
                    state = Infrastructure.CrossCutting.Audit.Actions.Insert

                    Dim treasuryControl As New TreasuryControl() With {.DocumentNumber = crossingAccount.Code, .DocumentType = 6, .DocumentUser = audit.CodeUser, .DocumentDate = crossingAccount.DocumentDate}
                    Dim resultSaveControl = _treasuryControlAdminService.SaveTreasuryControl(treasuryControl, audit)
                    If resultSaveControl.StateResult = False Then
                        Throw New Exception(ResourceManager.GetString("SaveTreasuryControlError", "Treasury"))
                    End If

                ElseIf crossingAccount.ChangeTracker.State = ObjectState.Modified Then
                    crossingAccount.ModificationDate = Date.Now
                    crossingAccount.ModificationUser = audit.CodeUser
                    state = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxCrossingAccount = crossingAccount.OriginalValue
                    If crossingAccount.Status = 3 Then
                        state = Infrastructure.CrossCutting.Audit.Actions.Annular
                        crossingAccount.AnnulmentDate = Date.Now
                        crossingAccount.AnnulmentUser = audit.CodeUser

                        Dim treasuryControl As TreasuryControl = _treasuryControlAdminService.GetTreasuryControlByDocumentNumber(crossingAccount.Code, 6)
                        If treasuryControl IsNot Nothing AndAlso treasuryControl.Id > 0 Then
                            treasuryControl.MarkAsDeleted()
                            Dim resultSaveControl = _treasuryControlAdminService.DeleteTreasuryControl(treasuryControl, audit)
                            If resultSaveControl.StateResult = False Then
                                Throw New Exception(ResourceManager.GetString("DeleteTreasuryControlError", "Treasury"))
                            End If
                        End If

                    End If
                End If


                If crossingAccount.CrossingAccountDetailCxC IsNot Nothing AndAlso crossingAccount.CrossingAccountDetailCxC.Any() Then
                    For Each c In crossingAccount.CrossingAccountDetailCxC
                        c.Detail = String.Format(c.Detail, crossingAccount.Code)
                        Dim tRMValue As Decimal = getTrmValue(listTRM, c.InvoiceCurrencyId, crossingAccount?.CurrencyId, officialCurrency)
                        If Not tRMValue > 0 Then
                            Return New ActionResult(Of CrossingAccount) With {.StateResult = False, .Message = "No se encontró tasa de TRM para la conversión"}
                        End If
                        c.ValueInCurrencyInvoice = Math.Round((CDec(c.CrossingValue / tRMValue)), 2)
                    Next
                End If
                If crossingAccount.CrossingAccountDetailCxP IsNot Nothing AndAlso crossingAccount.CrossingAccountDetailCxP.Any() Then
                    For Each c In crossingAccount.CrossingAccountDetailCxP
                        c.Detail = String.Format(c.Detail, crossingAccount.Code)
                        Dim tRMValue As Decimal = getTrmValue(listTRM, c.InvoiceCurrencyId, crossingAccount?.CurrencyId, officialCurrency)
                        If Not tRMValue > 0 Then
                            Return New ActionResult(Of CrossingAccount) With {.StateResult = False, .Message = "No se encontró tasa de TRM para la conversión"}
                        End If
                        c.ValueInCurrencyInvoice = Math.Round((CDec(c.CrossingValue / tRMValue)), 2)
                    Next
                End If
                If crossingAccount.CrossingAccountDetailOtherConcept IsNot Nothing AndAlso crossingAccount.CrossingAccountDetailOtherConcept.Any() Then
                    For Each c In crossingAccount.CrossingAccountDetailOtherConcept
                        c.Detail = String.Format(c.Detail, crossingAccount.Code)
                    Next
                End If

                Me._crossingAccountRepository.SaveEntity(crossingAccount)
                unitOfWork.Commit()
                unitWorkSequence.Commit()
                scope.Complete()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CrossingAccount)(crossingAccount, audit, state, auxCrossingAccount)
                auditProcess.Execute()
            End Using


            If withConfirm Then
                Dim resultConfirm = ConfirmCrossingAccount(crossingAccount.Id, audit, crossingAccount)
                If resultConfirm.StateResult = False Then
                    unitOfWork.RollbackChangesUnitOfWork()
                    Return New ActionResult(Of CrossingAccount) With {.StateResult = True, .StateResultAux = False, .ObjectEmbbeded = crossingAccount, .MessageResult = resultConfirm.MessageResult, .Message = resultConfirm.Message}
                End If
                Return New ActionResult(Of CrossingAccount) With {.StateResult = True, .StateResultAux = True, .ObjectEmbbeded = crossingAccount, .MessageResult = {resultConfirm.ObjectEmbbeded, resultConfirm.Message}.ToList()}
            Else
                Return New ActionResult(Of CrossingAccount) With {.StateResult = True, .ObjectEmbbeded = crossingAccount}
            End If

            'Return New ActionResult(Of CrossingAccount) With {.StateResult = True, .ObjectEmbbeded = crossingAccount}
        Catch ex As ArgumentException
            unitOfWork.RollbackChangesUnitOfWork()
            Return New ActionResult(Of CrossingAccount) With {.StateResult = False, .Message = ex.Message}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChangesUnitOfWork()
            Return New ActionResult(Of CrossingAccount) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CrossingAccount) With {.StateResult = False}
        End Try

    End Function

    Public Function getTrmValue(listTRM As List(Of TRM), FromCurrency As Integer, ToCurrency As Integer, officialCurrency As Currency) As Decimal
        Dim tRMValue As Decimal? = 1
        tRMValue = listTRM?.Find(Function(x) x.CurrencyId = FromCurrency AndAlso x.OfficialCurrencyId = ToCurrency _
                                                            AndAlso x.MeasurementDate = Date.Now.Date)?.Value

        If tRMValue Is Nothing OrElse tRMValue = 0 Then
            Dim result = _currencyAdminService.GetTRMbyCurrencyId(FromCurrency, ToCurrency, New SessionValues With {.OfficialCurrencyId = officialCurrency?.Id, .CurrencyISO4217 = officialCurrency?.Abbreviation})

            If result Is Nothing OrElse Not result?.StateResult Then
                Return -1
            End If

            listTRM.Add(result.ObjectEmbbeded)
            tRMValue = listTRM?.Find(Function(x) x.CurrencyId = FromCurrency _
                                         AndAlso x.OfficialCurrencyId = ToCurrency _
                                         AndAlso x.MeasurementDate = Date.Now.Date)?.Value

        End If
        Return tRMValue
    End Function

    Private Function ConvertToXml(crossingAccount As CrossingAccount) As String
        Dim builder As StringBuilder = New StringBuilder()
        For Each item In crossingAccount.CrossingAccountDetailCxC
            builder.Append("<Data>")
            builder.Append("<AccountReceivableId>" & item.AccountReceivableId & "</AccountReceivableId>")
            builder.Append("<CrossingValue>" & item.CrossingValue.ToString.Replace(",", ".") & "</CrossingValue>")
            builder.Append("<MainAccountId>" & item.MainAccountId & "</MainAccountId>")
            builder.Append("<Detail>" & item.Detail & "</Detail>")
            builder.Append("<AccountReceivableAccountingId>" & item.AccountReceivableAccountingId & "</AccountReceivableAccountingId>")
            builder.Append("<ValueInCurrencyInvoice>" & item.ValueInCurrencyInvoice.ToString.Replace(",", ".") & "</ValueInCurrencyInvoice>")
            builder.Append("</Data>")
        Next
        Return builder.ToString
    End Function

    ''' <summary>
    ''' Confirma el cruce de cuentas CxP y CxC
    ''' </summary>
    Public Function ConfirmCrossingAccount(crossingAccountId As Integer, audit As AuditMessage, Optional crossingAccount As CrossingAccount = Nothing) As ActionResult(Of String) Implements ICrossingAccountAdminService.ConfirmCrossingAccount
        If crossingAccountId = 0 Then
            Throw New ArgumentNullException("crossingAccountId")
        End If
        Dim unitOfWork As IUnitWork = Me._crossingAccountRepository.UnitWork
        Dim unitOfWorkAccountReceivable As IUnitWork = _accountReceivableRepository.UnitWork
        CType(unitOfWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 7200
        CType(unitOfWorkAccountReceivable, IObjectContextAdapter).ObjectContext.CommandTimeout = 7200
        Try
            Dim errorList As New StringBuilder()
            If crossingAccount Is Nothing Then
                crossingAccount = Me.GetCrossingAccountById(crossingAccountId, True)
            End If

            Dim resultValidateObject = treasuryService.ValidateCrossingAccountConfirm(crossingAccount)
            If Not resultValidateObject.StateResult Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = resultValidateObject.Message}
            End If
            crossingAccount = resultValidateObject.ObjectEmbbeded 'Actualiza el objeto con los agregados
            crossingAccount.Status = 2
            Dim treasuryControl As TreasuryControl = _treasuryControlAdminService.GetTreasuryControlByDocumentNumber(crossingAccount.Code, 6)
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = IsolationLevel.ReadCommitted})
                Dim consecutive As String = String.Empty

                If treasuryControl IsNot Nothing AndAlso treasuryControl.Id > 0 Then
                    treasuryControl.MarkAsDeleted()
                    Dim resultSaveControl = _treasuryControlAdminService.DeleteTreasuryControl(treasuryControl, audit, False)
                    If resultSaveControl.StateResult = False Then
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("DeleteTreasuryControlError", "Treasury")}
                    End If
                End If

                'Lista de CxP para  ajuste diferencial
                Dim ListCxPDifferencialAdjustment As List(Of CxPDifferentialAdjustment) = New List(Of CxPDifferentialAdjustment)

                'Disminuir los saldos / Afectar tambien las cuotas
                For Each crossingCxP As CrossingAccountDetailCxP In crossingAccount.CrossingAccountDetailCxP
                    Dim accountPayable As AccountPayable = _accountPayableRepository.FirstOrDefault(Function(c) c.Id = crossingCxP.AccountPayableId, True, {"AccountPayableDetailConcept", "AccountPayableShares", "Currency"}) ' _accountPayableRepository.GetAccountPayableById(crossingCxP.AccountPayableId, False)
                    Dim _crossingValue As Decimal = crossingCxP.ValueInCurrencyInvoice ''Ahora se toma el ValueInCurrencyInvoice ya que es el valor en la moneda de la factura
                    For Each accountPayableShare As AccountPayableShares In accountPayable.AccountPayableShares
                        If _crossingValue >= accountPayableShare.Balance Then
                            _crossingValue -= accountPayableShare.Balance
                            accountPayableShare.CrossingValue += accountPayableShare.Balance
                            accountPayableShare.Balance = 0
                        Else
                            accountPayableShare.Balance -= _crossingValue
                            accountPayableShare.CrossingValue += _crossingValue
                            _crossingValue = 0
                            Exit For
                        End If
                    Next
                    accountPayable.Balance -= crossingCxP.ValueInCurrencyInvoice ''Se cambia al valor en la moneda de la factura
                    Dim resultAccountPayable As ActionResult(Of AccountPayable) = _accountPayableAdminService.SaveAccountPayable(accountPayable, audit)

                    If resultAccountPayable.StateResult = False Then
                        Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"CE0006"}.ToList(), .Message = String.Format(ResourceManager.GetString("BalanceUpdateErrorInvoiceNumber", MODULE_NAME), accountPayable.BillNumber)}
                    End If

                    Dim cxPDifferentialAdjustment = New CxPDifferentialAdjustment With {.Id = accountPayable.Id, .ValuePaid = crossingCxP.ValueInCurrencyInvoice, .EntityId = crossingAccountId, .EntityName = NameOf(Domain.Entities.CrossingAccount)}
                    ListCxPDifferencialAdjustment.Add(cxPDifferentialAdjustment)
                Next

                'Valida que hayan facturas agregadas tanto en cxp y en cxc 
                If crossingAccount.CrossingAccountDetailCxC.Count = 0 OrElse crossingAccount.CrossingAccountDetailCxP.Count = 0 Then
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = "No se puede confirmar la transacción. Es necesario que exista tanto una Cuenta por Pagar (CXP) como una Cuenta por Cobrar (CXC) para proceder con el cruce. Verifique que ambos documentos estén correctamente ingresados antes de confirmar"}
                End If

                'Afectas AccountReceivable, AccountReceivableShares y AccountReceivableAccounting
                Dim XmlObjectSave = ConvertToXml(crossingAccount)
                Dim resultStore = _crossingAccountRepository.SP_SaveMasiveCxCCrossingAccount(XmlObjectSave, crossingAccount.CrossingType, crossingAccount.ThirdPartyId)
                If resultStore Is Nothing OrElse resultStore.Count = 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"CE0006"}.ToList(), .Message = "No se generaron detalles contables para el comprobante"}
                End If
                If (From x In resultStore Where x.Status = 0 OrElse x.Status = 2 Select x).Count > 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"CE0006"}.ToList(), .Message = resultStore(0).Message}
                End If

                '********************************************* AJUSTE DIFERENCIAL CXP y CxC  ******************************************
                Dim resultMessage = New StringBuilder
                If ListCxPDifferencialAdjustment.Any() Then
                    Dim xMLCxP = New StringBuilder
                    For Each item In ListCxPDifferencialAdjustment
                        xMLCxP.AppendLine(Utils.SerializeToXmlString(item).Replace("<?xml version=""1.0"" encoding=""utf-16""?>", ""))
                    Next
                    Dim listResultAdjustCxP = _crossingAccountRepository.ExecuteStoredProcedure(Of SPResultModelDiffAdjustment)("[Payments].[SP_AccountPayableRevaluation_WithOut_Output]", {("@ListAccountPayableXml", xMLCxP.ToString()),
                                                                                                                                                                            ("@UserCode", audit.CodeUser)}).ToList()

                    If listResultAdjustCxP Is Nothing OrElse listResultAdjustCxP.Exists(Function(x) x.Code = "999") Then
                        scope.Dispose()
                        Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"CE0006"}.ToList(), .Message = $"error al ejecutar el ajuste diferencial CxP: {listResultAdjustCxP?.Find(Function(x) x.Code = "999")?.MessageResult}"}
                    End If

                    If listResultAdjustCxP.Any() Then
                        Dim joinList = String.Join(", ", listResultAdjustCxP.Select(Function(x) x.MessageResult))
                        resultMessage.AppendLine($"Ajuste Diferencial CxP: {joinList}")
                    End If

                End If

                Dim listResultAdjustCxC As List(Of SPResultModel) = New List(Of SPResultModel)
                For Each item In crossingAccount.CrossingAccountDetailCxC

                    Dim resultAdjustCxC = _crossingAccountRepository _
                                            .ExecuteStoredProcedure(Of SPResultModel)("[Portfolio].[SP_AccountReceivableRevaluation]",
                                                                    {("@AccountReceivableId", item.AccountReceivableId),
                                                                        ("@ValuePaid", item.ValueInCurrencyInvoice),
                                                                        ("@EntityCode", crossingAccount.Code),
                                                                        ("@EntityId", crossingAccount.Id),
                                                                        ("@EntityName", NameOf(Domain.Entities.CrossingAccount)),
                                                                        ("@UserCode", audit.CodeUser),
                                                                        ("@DocumentDate", crossingAccount.DocumentDate)}).FirstOrDefault

                    If resultAdjustCxC?.CodeMessage = "999" Then
                        scope.Dispose()
                        Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"CE0006"}.ToList(), .Message = $"Error al ejecutar el ajuste diferencial CxC: {resultAdjustCxC?.Message}"}
                    End If

                    If resultAdjustCxC IsNot Nothing Then
                        listResultAdjustCxC.Add(resultAdjustCxC)
                    End If
                Next

                If listResultAdjustCxC.Any() Then
                    Dim joinList = String.Join(", ", listResultAdjustCxC.Select(Function(x) x.Message))
                    resultMessage.AppendLine($"Ajuste Diferencial CxC: {joinList}")
                End If

                '*********************************************************************************************************************
                'Comprobante contable
                Dim settingTreasury As SettingsTreasury = _settingsTreasuryRepository.GetSettingsTreasuryByIdUnitOperative(crossingAccount.OperatingUnitId)
                If settingTreasury.Id > 0 Then
                    Dim accounting As New Domain.Entities.JournalVouchers()
                    With accounting
                        .IdJournalVoucher = settingTreasury.JournalVoucherTypeVoucherTransactionCrossing
                        .VoucherDate = crossingAccount.DocumentDate
                        .Status = crossingAccount.Status
                        .Detail = crossingAccount.Description
                        .EntityCode = crossingAccount.Code
                        .EntityId = crossingAccount.Id
                        .EntityName = GetType(CrossingAccount).Name
                        .IsClosedYear = False
                        .BookCurrencyId = crossingAccount.CurrencyId

                        For Each detailCxP As CrossingAccountDetailCxP In crossingAccount.CrossingAccountDetailCxP
                            Dim accountPayable As AccountPayable = _accountPayableRepository.GetAccountPayableById(detailCxP.AccountPayableId, False)
                            Dim accountDetail As New JournalVoucherDetails
                            With accountDetail
                                .IdMainAccount = detailCxP.MainAccountId
                                If crossingAccount.CrossingType = 1 Then
                                    'si el tipo es de cruce es del mismo tercero
                                    .IdThirdParty = crossingAccount.ThirdPartyId
                                Else
                                    'si el tipo es de cruce es de diferencte tercero
                                    .IdThirdParty = accountPayable.IdThirdParty
                                End If

                                .IdCostCenter = accountPayable.IdCostCenter
                                .DebitValue = detailCxP.CrossingValue
                                .Detail = detailCxP.Detail
                                .IdRetention = Nothing
                                .RetentionRate = Nothing
                            End With
                            accounting.JournalVoucherDetails.Add(accountDetail)
                        Next

                        For Each detailConcept As CrossingAccountDetailOtherConcept In crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 1).ToList()
                            Dim accountDetail As New JournalVoucherDetails
                            With accountDetail
                                .IdMainAccount = detailConcept.MainAccountId
                                If crossingAccount.CrossingType = 1 Then
                                    'si el tipo es de cruce es del mismo tercero
                                    .IdThirdParty = crossingAccount.ThirdPartyId
                                Else
                                    'si el tipo es de cruce es de diferencte tercero
                                    .IdThirdParty = detailConcept.ThirdPartyId
                                End If
                                .IdCostCenter = detailConcept.CostCenterId
                                .DebitValue = detailConcept.Value
                                .Detail = detailConcept.Detail
                                .IdRetention = Nothing
                                .RetentionRate = Nothing
                            End With
                            accounting.JournalVoucherDetails.Add(accountDetail)
                        Next

                        'Se comenta el código para darle una solución momentanea para poder confirmar un cruce con bastantes registros
                        For Each detailCxC In resultStore.ToList
                            Dim accountDetail As New JournalVoucherDetails
                            With accountDetail
                                .IdMainAccount = detailCxC.IdMainAccount
                                .IdThirdParty = detailCxC.IdThirdParty
                                .IdCostCenter = detailCxC.IdCostCenter
                                .DebitValue = detailCxC.DebitValue
                                .CreditValue = detailCxC.CreditValue
                                .Detail = detailCxC.Detail
                                .IdRetention = Nothing
                                .RetentionRate = Nothing
                            End With
                            accounting.JournalVoucherDetails.Add(accountDetail)
                        Next

                        For Each detailConcept As CrossingAccountDetailOtherConcept In crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 2).ToList()
                            Dim accountDetail As New JournalVoucherDetails
                            With accountDetail
                                .IdMainAccount = detailConcept.MainAccountId
                                If crossingAccount.CrossingType = 1 Then
                                    'si el tipo es de cruce es del mismo tercero
                                    .IdThirdParty = crossingAccount.ThirdPartyId
                                Else
                                    'si el tipo es de cruce es de diferencte tercero
                                    .IdThirdParty = detailConcept.ThirdPartyId
                                End If
                                .IdCostCenter = detailConcept.CostCenterId
                                .CreditValue = detailConcept.Value
                                .Detail = detailConcept.Detail
                                .IdRetention = Nothing
                                .RetentionRate = Nothing
                            End With
                            accounting.JournalVoucherDetails.Add(accountDetail)
                        Next

                        Dim resultAccounting As ActionMessageResult(Of Domain.Entities.JournalVouchers)
                        resultAccounting = _accountingAdminService.SaveAccountingDocument(accounting, audit)
                        If resultAccounting.StateResult = False Then
                            scope.Dispose()
                            'capturar el mensaje de error de contabilidad
                            Return New ActionResult(Of String) With {.StateResult = False, .Message = resultAccounting.Message}
                        Else
                            consecutive = resultAccounting.ObjectEmbbeded.Consecutive
                        End If
                    End With
                Else
                    scope.Dispose()
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"CE0006"}.ToList(), .Message = ResourceManager.GetString("SettingTreasuryNotExist", MODULE_NAME)}
                End If

                crossingAccount.ModificationDate = Date.Now
                crossingAccount.ModificationUser = audit.CodeUser
                crossingAccount.ConfirmationDate = Date.Now
                crossingAccount.ConfirmationUser = audit.CodeUser
                Me._crossingAccountRepository.SaveEntity(crossingAccount)
                unitOfWork.Commit()

                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Confirm
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CrossingAccount)(crossingAccount, audit, status, crossingAccount.OriginalValue)
                auditProcess.Execute()
                crossingAccount.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = consecutive, .Message = resultMessage.ToString()}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWorkAccountReceivable.RollbackChangesUnitOfWork()
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As DbEntityValidationException
            unitOfWorkAccountReceivable.RollbackChangesUnitOfWork()
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception
            unitOfWorkAccountReceivable.RollbackChangesUnitOfWork()
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try

    End Function

    ''' <summary>
    ''' establece las cuentas por cobrar del copiar y pegar
    ''' </summary>
    ''' <param name="data">Listado que se va a procesar</param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="crossingType">1-Mismo Tercero, 2-Diferente Tercero</param>
    ''' <param name="processType">1 - CxP, 2 - CxC</param>
    ''' <returns></returns>
    Public Function SetDocumentsCrossingCopyPaste(data As List(Of List(Of String)), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) Implements ICrossingAccountAdminService.SetDocumentsCrossingCopyPaste
        Try
            Return treasuryService.SetDocumentsCrossingCopyPaste(data, idThirdPaty, crossingType, processType)
        Catch ex As Exception
            Return New ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) With {.Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
        End Try
    End Function

    Public Function SetDocumentsCrossingImportFile(data As List(Of ImportFileRow), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) Implements ICrossingAccountAdminService.SetDocumentsCrossingImportFile
        Try
            Return treasuryService.SetDocumentsCrossingImportFile(data, idThirdPaty, crossingType, processType)
        Catch ex As Exception
            Return New ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) With {.Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
        End Try
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="treasuryNote"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function ReverseCrossingAccount(treasuryNote As TreasuryNote, audit As AuditMessage) As ActionResult(Of String) Implements ICrossingAccountAdminService.ReverseCrossingAccount
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                CType(_crossingAccountRepository.UnitWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                Dim result = _crossingAccountRepository.SP_ReverseCrossingAccount(treasuryNote.Id, audit.CodeUser).ToList().ElementAt(0)
                If result.CodeMessage = 999 Then
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = result.Message}
                Else
                    transaction.Complete()
                    Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = result.IdJournalVoucher, .Message = result.Message}
                End If
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As DbEntityValidationException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _accountPayableAdminService.Dispose()
                _accountReceivableAdminService.Dispose()
                _accountingAdminService.Dispose()
                treasuryService.Dispose()
                _treasuryControlAdminService.Dispose()
            End If
            _crossingAccountRepository = Nothing
            _secuenseDRepository = Nothing
            _pucRepository = Nothing
            _closeMonthRepository = Nothing
            _crossingAccountDetailCxP = Nothing
            _crossingAccountDetailCxC = Nothing
            _settingsTreasuryRepository = Nothing
            _accountPayableRepository = Nothing
            _accountReceivableRepository = Nothing
            _accountPayableAdminService = Nothing
            _accountReceivableAdminService = Nothing
            _accountingAdminService = Nothing
            treasuryService = Nothing
            _treasuryControlAdminService = Nothing
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
