'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports Application.Payments
Imports Infrastructure.Data.ModelRepository
Imports Infrastructure.Data.Base
Imports System.Text
Imports Application.Cost
Imports Application.Accounting

Public Class CostDistributionDirectCostAdminService
    Implements ICostDistributionDirectCostAdminService, Inject

#Region "Fields"

    ''' <summary>
    ''' Repositorio de gastos generales
    ''' </summary>
    Private _distributionDirectCostRepository As ICostDistributionDirectCostRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ICostSequenceDetailRepository

    ''' <summary>
    ''' repositorio de parametros de costos
    ''' </summary>
    Private _settingCostRepository As ICostSettingRepository

    ''' <summary>
    ''' repositorio de secuencias numericas de cuentas por pagar
    ''' </summary>
    Private _sequensePaymentsCRepository As ISequensePaymentsCRepository

    ''' <summary>
    ''' Aplicacion de cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableAdminService As IAccountPayableAdminService

    ''' <summary>
    ''' Variable tipo repositorio para una cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableRepository As IAccountPayableRepository

    ''' <summary>
    ''' Variable tipo repositorio para el detalle de la cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableDetailConceptRepository As IAccountPayableDetailConceptRepository

    Private _generalLedgerIVARepository As IGeneralLedgerIVARepository

    ''' <summary>
    ''' Repositorio de gastos generales
    ''' </summary>
    Private _generalExpenseRepository As ICostGeneralExpensesRepository

    ''' <summary>
    ''' Repositorio de los Ivas para el detalle de la distribución de los elementos del costo
    ''' </summary>
    Private _costDistributionDirectCostDetailIvaRepository As ICostDistributionDirectCostDetailIvaRepository

    Private _companySettingsRepository As ICompanySettingsRepository

    ''' <summary>
    ''' Aplicación de Documentos Contables
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountingAdminService As IAccountingDocumentAdminService

    ''' <summary>
    ''' Repositorio de Retenciones
    ''' </summary>
    Private _retentionRepository As IRetentionConceptRepository

#End Region

#Region "Builder"

    Public Sub New(ByVal distributionDirectCostRepository As ICostDistributionDirectCostRepository, ByVal sequenceDRepository As ICostSequenceDetailRepository, ByVal settingCostRepository As ICostSettingRepository,
                   sequensePaymentsCRepository As ISequensePaymentsCRepository, accountPayableAdminService As IAccountPayableAdminService,
                   ByVal accountPayableRepository As IAccountPayableRepository, accountPayableDetailConceptRepository As IAccountPayableDetailConceptRepository,
                   ByVal context As IGlobalModelUnitOfWork, generalLedgerIVARepository As IGeneralLedgerIVARepository, ByVal generalExpenseRepository As ICostGeneralExpensesRepository,
                   costDistributionDirectCostDetailIvaRepository As ICostDistributionDirectCostDetailIvaRepository, ByVal companySettingsRepository As ICompanySettingsRepository,
                   ByVal accountingAdminService As IAccountingDocumentAdminService, ByVal retentionRepository As IRetentionConceptRepository)
        If distributionDirectCostRepository Is Nothing Then
            Throw New ArgumentNullException("distributionDirectCostRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        If settingCostRepository Is Nothing Then
            Throw New ArgumentNullException("settingCostRepository")
        End If
        If sequensePaymentsCRepository Is Nothing Then
            Throw New ArgumentNullException("sequensePaymentsCRepository")
        End If
        If accountPayableAdminService Is Nothing Then
            Throw New ArgumentNullException("accountPayableAdminService")
        End If
        If accountPayableRepository Is Nothing Then
            Throw New ArgumentNullException("accountPayableRepository Vacio")
        End If
        If accountPayableDetailConceptRepository Is Nothing Then
            Throw New ArgumentNullException("accountPayableDetailConceptRepository Vacio")
        End If
        If companySettingsRepository Is Nothing Then
            Throw New ArgumentNullException("companySettingsRepository Vacio")
        End If
        If accountingAdminService Is Nothing Then
            Throw New ArgumentNullException("accountingAdminService Vacio")
        End If
        If retentionRepository Is Nothing Then
            Throw New ArgumentNullException("retentionRepository")
        End If

        _distributionDirectCostRepository = distributionDirectCostRepository
        _sequenceDRepository = sequenceDRepository
        _settingCostRepository = settingCostRepository
        _sequensePaymentsCRepository = sequensePaymentsCRepository
        _accountPayableAdminService = accountPayableAdminService
        _accountPayableRepository = accountPayableRepository
        _accountPayableDetailConceptRepository = accountPayableDetailConceptRepository
        _context = context
        _generalLedgerIVARepository = generalLedgerIVARepository
        _generalExpenseRepository = generalExpenseRepository
        _costDistributionDirectCostDetailIvaRepository = costDistributionDirectCostDetailIvaRepository
        _companySettingsRepository = companySettingsRepository
        _accountingAdminService = accountingAdminService
        _retentionRepository = retentionRepository

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina un gasto directo
    ''' </summary>
    Public Function DeleteDistributionDirectCost(distributionDirectCost As CostDistributionDirectCost, audit As AuditMessage) As ActionResult Implements ICostDistributionDirectCostAdminService.DeleteDistributionDirectCost
        If distributionDirectCost Is Nothing Then
            Throw New ArgumentNullException("distributionDirectCost")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionDirectCostRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While distributionDirectCost.CostDistributionDirectCostDetail.Count > 0
                    distributionDirectCost.CostDistributionDirectCostDetail.Item(distributionDirectCost.CostDistributionDirectCostDetail.Count() - 1).MarkAsDeleted()
                End While
                While distributionDirectCost.CostDistributionDirectCostValues.Count > 0
                    distributionDirectCost.CostDistributionDirectCostValues.Item(distributionDirectCost.CostDistributionDirectCostValues.Count() - 1).MarkAsDeleted()
                End While
                distributionDirectCost.MarkAsDeleted()
                distributionDirectCost.ModificationUser = audit.CodeUser
                distributionDirectCost.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionDirectCost)(distributionDirectCost, audit, status)
                Me._distributionDirectCostRepository.SaveEntity(distributionDirectCost)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    Public Function GetDistributionDirectCost(code As String, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionDirectCost) Implements ICostDistributionDirectCostAdminService.GetDistributionDirectCost
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim distributionDirectCost As CostDistributionDirectCost = Me._distributionDirectCostRepository.GetDistributionDirectCost(code.Trim())
            If distributionDirectCost IsNot Nothing AndAlso distributionDirectCost.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionDirectCost)(distributionDirectCost, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionDirectCost}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un gasto directo por id
    ''' </summary>
    Public Function GetDistributionDirectCostById(id As Integer) As CostDistributionDirectCost Implements ICostDistributionDirectCostAdminService.GetDistributionDirectCostById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._distributionDirectCostRepository.GetDistributionDirectCostById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un gasto directo
    ''' </summary>
    Public Function SaveDistributionDirectCost(distributionDirectCost As CostDistributionDirectCost, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of CostDistributionDirectCost) Implements ICostDistributionDirectCostAdminService.SaveDistributionDirectCost
        If distributionDirectCost Is Nothing Then
            Throw New ArgumentNullException("distributionDirectCost")
        End If

        Dim unitOfWork As IUnitWork = Me._distributionDirectCostRepository.UnitWork
        Dim accountPayableUnitOfWork As IUnitWork = Me._accountPayableRepository.UnitWork

        Try
            If distributionDirectCost.DeductibleIva Is Nothing Or distributionDirectCost.DeductibleIva = False Then
                If distributionDirectCost.Value <> Math.Round((From x In distributionDirectCost.CostDistributionDirectCostDetail Where x.ChangeTracker.State <> ObjectState.Deleted Select x.Value).Sum(), 2, MidpointRounding.AwayFromZero) And distributionDirectCost.Status = 2 Then
                    Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "El valor a distribuir es distinto al valor total"}
                End If
            End If
            If distributionDirectCost.CostDistributionDirectCostDetail Is Nothing OrElse Not distributionDirectCost.CostDistributionDirectCostDetail.Any() Then
                Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "Debe agregar al menos un detalle"}
            End If
            If distributionDirectCost.CostDistributionDirectCostDetail.Any(Function(x) x.Value <= 0) Then
                Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "No debe haber detalles con valor cero o negativos"}
            End If

            Dim costSetting As CostSetting = _settingCostRepository.GetCostSetting()
            If costSetting Is Nothing OrElse costSetting.Id = 0 Then
                Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "No existen parámetros de costo"}
            End If

            If distributionDirectCost.CostGeneralExpense Is Nothing Then
                Dim costGeneralExpense As CostGeneralExpense = _generalExpenseRepository.GetGeneralExpenseById(distributionDirectCost.GeneralExpenseId)
                If costGeneralExpense Is Nothing Then
                    Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "No se puede encontrar el Elemento de Costo"}
                Else
                    distributionDirectCost.CostGeneralExpense = costGeneralExpense
                End If
            End If

            Dim ap As AccountPayable = Nothing
            Dim AccountPayableSequenseDetailId As Integer = 0
            If distributionDirectCost.AccountPayableId Is Nothing Then
                If costSetting.AccountingCosts Then
                    If distributionDirectCost.Status = 2 Then
                        Dim sequensePayments As PaymentsSecuence = _sequensePaymentsCRepository.GetSequenseByIdForm("730")
                        If sequensePayments.Id = 0 Then
                            Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe secuencia numérica para el formulario de cuentas por pagar."}
                        ElseIf sequensePayments.IsManual Then
                            Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "La secuencia numerica de CxP es manual."}
                        End If

                        If sequensePayments.Scope = "O" Then 'Si la secuencia es por organización
                            AccountPayableSequenseDetailId = (From x In sequensePayments.PaymentsSecuenceDetail Select x.Id).FirstOrDefault()
                        Else 'Si la secuencia es por unidad operativa
                            If (From x In sequensePayments.PaymentsSecuenceDetail Where x.IdOperatingUnit = distributionDirectCost.OperatingUnitId Select x).Count = 0 Then
                                Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe la unidad operativa seleccionada en la secuencia de CxP."}
                            End If
                            AccountPayableSequenseDetailId = (From x In sequensePayments.PaymentsSecuenceDetail Where x.IdOperatingUnit = distributionDirectCost.OperatingUnitId Select x.Id).FirstOrDefault()
                        End If
                    End If

                    If Not distributionDirectCost.ProvisionDocument Then
                        If distributionDirectCost.CostGeneralExpense.GenerateAccountPayable.HasValue AndAlso Not distributionDirectCost.CostGeneralExpense.GenerateAccountPayable Then
                            distributionDirectCost.Year = distributionDirectCost.DocumentDate.Value.Year
                            distributionDirectCost.Month = distributionDirectCost.DocumentDate.Value.Month
                        Else
                            distributionDirectCost.Year = distributionDirectCost.ServicePeriodDate.Value.Year
                            distributionDirectCost.Month = distributionDirectCost.ServicePeriodDate.Value.Month
                        End If
                    End If
                Else
                    Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "Debe seleccionar una factura"}
                End If
            Else

                ap = _accountPayableRepository.FirstOrDefault(Function(x) x.Id = distributionDirectCost.AccountPayableId, True, {"AccountPayableDetailConcept", "AccountPayableShares"})
                ''se actualiza el campo de iva descontable en caso de que haya sido modificado
                ap.DeductibleIva = distributionDirectCost.DeductibleIva
                distributionDirectCost.Year = ap.DocumentDate.Year
                distributionDirectCost.Month = ap.DocumentDate.Month
                If distributionDirectCost.Status <> 3 Then
                    If ap Is Nothing OrElse ap.Status <> 1 Then
                        Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe, o no se encuentra sin confirmar la cuenta por pagar asociada."}
                    End If

                    If ap.CostDistributionDirectCostId IsNot Nothing OrElse (ap.AccountPayableDetailConcept IsNot Nothing AndAlso ap.AccountPayableDetailConcept.Any(Function(d) d.IsDirectCost)) Then
                        Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = String.Format("La cuenta por pagar asociada ({0}) ya se encuentra distribuida", ap.Code)}
                    End If
                End If
            End If

            If distributionDirectCost.Status <> 3 Then
                If distributionDirectCost.Year < costSetting.Year OrElse (distributionDirectCost.Year = costSetting.Year AndAlso distributionDirectCost.Month < costSetting.Month) Then
                    Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "La factura corresponde a una fecha anterior al periodo actual del módulo de costos"}
                End If
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As New Text.StringBuilder
                If String.IsNullOrEmpty(distributionDirectCost.Code) Then
                    Dim seq As CostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.CostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            distributionDirectCost.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxExpenseConcept As CostDistributionDirectCost = Nothing
                Dim status As Integer
                If distributionDirectCost.ChangeTracker.State = ObjectState.Added Then
                    MessageResult.AppendLine(String.Format(ResourceManager.GetString("SavedWithCode"), distributionDirectCost.Code))
                    distributionDirectCost.CreationDate = Date.Now
                    distributionDirectCost.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult.AppendLine(String.Format(ResourceManager.GetString("UpdatedWithCode"), distributionDirectCost.Code))
                    distributionDirectCost.ModificationDate = Date.Now
                    distributionDirectCost.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxExpenseConcept = distributionDirectCost.OriginalValue
                End If

                If distributionDirectCost.Status = 2 Then
                    distributionDirectCost.ConfirmDate = Date.Now
                    distributionDirectCost.ConfirmUser = audit.CodeUser

                    CalculateIvaByDetail(distributionDirectCost)

                ElseIf distributionDirectCost.Status = 3 Then
                    distributionDirectCost.AnnulmentDate = Date.Now
                    distributionDirectCost.AnnulmentUser = audit.CodeUser
                ElseIf distributionDirectCost.Status = 4 Then
                    Dim resultJournalVouchers = GenerateJournalVoucher(distributionDirectCost, audit)

                    If resultJournalVouchers Is Nothing OrElse resultJournalVouchers.StateResult = False Then
                        scope.Dispose()
                        Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "No se pudo generar el comprobante contable de la Distribución de Elementos del Costo"}
                    End If

                    'Guardo el comprobante contable
                    Dim resultSaveAccounting As ActionMessageResult(Of JournalVouchers)
                    resultSaveAccounting = _accountingAdminService.SaveAccountingDocument(resultJournalVouchers.ObjectEmbbeded, audit)
                    If resultSaveAccounting.StateResult Then
                        MessageResult.AppendLine("Se generó comprobante contable de reversión " + resultSaveAccounting.ObjectEmbbeded.Consecutive.ToString())
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CostDistributionDirectCost) With {.StateResult = eStatusResult.WARNING, .Message = resultSaveAccounting.Message}
                    End If
                End If

                For Each detail In distributionDirectCost.CostDistributionDirectCostDetail
                    Dim idTmp = detail.MeasurementUnitId
                    detail.InventoryMeasurementUnit = Nothing
                    detail.MeasurementUnitId = idTmp
                Next

                Me._distributionDirectCostRepository.SaveEntity(distributionDirectCost)
                unitOfWork.Commit()

                If distributionDirectCost.Status = 2 Then
                    If Not distributionDirectCost.ProvisionDocument Then
                        ''Validacion para que en caso de que tenga dato de iva descontable valide las cuentas parametrizadas y asi permitir o no realizar la cuenta x pagar
                        If Not IsDBNull(distributionDirectCost.DeductibleIva) Then
                            For Each item In distributionDirectCost.CostDistributionDirectCostIva
                                Dim generalIvaDeductible As GeneralLedgerIVA = (From i In _context.GeneralLedgerIVA.AsNoTracking Where i.Id = item.GeneralLedgerIvaId Select i).FirstOrDefault
                                If distributionDirectCost.DeductibleIva = False AndAlso (generalIvaDeductible.IdAccountDebitControlFiscal Is Nothing Or generalIvaDeductible.IdAccountCreditControlFiscal Is Nothing) Then
                                    MessageResult.Clear()
                                    MessageResult.AppendLine(String.Format("El registro {0} se guardó pero no se confirmó, debe de parametrizar las cuentas de control fiscal para la tarifa IVA {1}", distributionDirectCost.Code, generalIvaDeductible.Name))
                                    Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = MessageResult.ToString()}
                                End If
                                If distributionDirectCost.DeductibleIva = True AndAlso generalIvaDeductible.IdAccountPurchaseService Is Nothing Then
                                    MessageResult.Clear()
                                    MessageResult.AppendLine(String.Format("El registro {0} se guardó pero no se confirmó, debe de parametrizar la cuenta IVA Compra/Servicio para la tarifa IVA {1}", distributionDirectCost.Code, generalIvaDeductible.Name))
                                    Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = MessageResult.ToString()}
                                End If
                            Next
                        End If

                        MessageResult.Clear()
                        MessageResult.AppendLine(String.Format("El registro {0} se confirmó correctamente", distributionDirectCost.Code))
                        If costSetting.AccountingCosts Then
                            If ap Is Nothing Then
                                ' Se omite el proceso de generar CxP si así lo establece el parámetro
                                If Not distributionDirectCost.CostGeneralExpense.GenerateAccountPayable.HasValue OrElse distributionDirectCost.CostGeneralExpense.GenerateAccountPayable Then
                                    ap = Me.GenerateAccountPayable(distributionDirectCost, costSetting)
                                    Dim ListAccountPayable As New List(Of AccountPayable) From {ap}
                                    Dim resultCxp As ActionResult(Of List(Of AccountPayable)) = _accountPayableAdminService.SaveListAccountPayableWithReturn(ListAccountPayable, Nothing, False, audit, AccountPayableSequenseDetailId)
                                    If Not resultCxp.StateResult Then
                                        scope.Dispose()
                                        Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = resultCxp.MessageResult(0).ToString}
                                    End If

                                    ap = resultCxp.ObjectEmbbeded(0)
                                    distributionDirectCost.AccountPayableId = ap.Id
                                    Me._distributionDirectCostRepository.SaveEntity(distributionDirectCost)
                                    MessageResult.AppendLine(String.Format("Se generó la Cuenta por Pagar {0}", ap.Code))
                                    MessageResult.AppendLine(String.Format("No. Radicado: {0}", ap.NumberFiling))
                                Else
                                    Dim resultJournalVouchers = GenerateJournalVoucher(distributionDirectCost, audit)

                                    If resultJournalVouchers Is Nothing OrElse resultJournalVouchers.StateResult = False Then
                                        scope.Dispose()
                                        Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "No se pudo generar el comprobante contable de la Distribución de Elementos del Costo"}
                                    End If

                                    'Guardo el comprobante contable
                                    Dim resultSaveAccounting As ActionMessageResult(Of JournalVouchers)
                                    resultSaveAccounting = _accountingAdminService.SaveAccountingDocument(resultJournalVouchers.ObjectEmbbeded, audit)
                                    If resultSaveAccounting.StateResult Then
                                        MessageResult.AppendLine("Se generó comprobante contable " + resultSaveAccounting.ObjectEmbbeded.Consecutive.ToString())
                                    Else
                                        scope.Dispose()
                                        Return New ActionResult(Of CostDistributionDirectCost) With {.StateResult = eStatusResult.WARNING, .Message = resultSaveAccounting.Message}
                                    End If
                                End If
                            Else
                                ap.InvoiceValue = distributionDirectCost.Value
                                ap.Coments = distributionDirectCost.Observation
                                ap.CostDistributionDirectCostId = distributionDirectCost.Id

                                Dim listAccountPayableDetailConcept = Me.GenerateAccountPayableDetailConcept(distributionDirectCost, costSetting)
                                For Each item In listAccountPayableDetailConcept
                                    ap.Value = ap.Value + item.Value
                                    ap.Balance = ap.Balance + item.Value
                                    ap.AccountPayableDetailConcept.Add(item)
                                Next

                                For Each accountPayableShares In ap.AccountPayableShares
                                    If accountPayableShares.Share = 1 Then
                                        accountPayableShares.InitialValue = Utils.RoundValue(ap.Value, Utils.RoundLevel.Unit)
                                        accountPayableShares.Balance = Utils.RoundValue(ap.Balance, Utils.RoundLevel.Unit)
                                    Else
                                        accountPayableShares.InitialValue = 0
                                        accountPayableShares.Balance = 0
                                    End If
                                Next

                                Me._accountPayableRepository.SaveEntity(ap)
                                accountPayableUnitOfWork.Commit()
                                MessageResult.AppendLine(String.Format("Se actualizó la Cuenta por Pagar {0}", ap.Code))
                            End If

                        End If
                    Else

                        MessageResult.Clear()
                        Dim resultCostDistributionDirectCost As ActionResult(Of CostDistributionDirectCost) = SaveDistributionDirectCostProvisionDocument(distributionDirectCost.Id, audit)
                        If Not resultCostDistributionDirectCost.StateResult Then
                            scope.Dispose()
                            Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = resultCostDistributionDirectCost.MessageResult(0)}
                        End If

                        For Each item In resultCostDistributionDirectCost.MessageResult
                            MessageResult.AppendLine(item)
                        Next

                    End If
                End If

                unitOfWork.CommitAndRefreshChanges()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionDirectCost)(distributionDirectCost, audit, status, auxExpenseConcept)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionDirectCost, .Message = MessageResult.ToString()}
            End Using
        Catch ex As OptimisticConcurrencyException
            accountPayableUnitOfWork.RollbackChangesUnitOfWork()
            unitOfWork.RollbackChangesUnitOfWork()
            Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            accountPayableUnitOfWork.RollbackChangesUnitOfWork()
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Calcula el Iva para cada detalle de la distribución de los elementos del costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CalculateIvaByDetail(distributionDirectCost As CostDistributionDirectCost)

        For Each iva In distributionDirectCost.CostDistributionDirectCostIva
            For Each item In distributionDirectCost.CostDistributionDirectCostDetail

                'Se calcula el procentaje por detalle
                Dim percentageForDistributionIVA = item.Value / distributionDirectCost.Value

                'Se calcula el Iva por detalle
                Dim IvaValue = Math.Round(iva.IvaValue * percentageForDistributionIVA, 2, MidpointRounding.AwayFromZero)
                Dim baseIva = Math.Round(IvaValue * 100 / iva.Percentage, 2, MidpointRounding.AwayFromZero)

                Dim detailIva = New CostDistributionDirectCostDetailIva With {
                                                             .GeneralLedgerIvaId = iva.GeneralLedgerIvaId,
                                                             .BaseIva = baseIva,
                                                             .IvaValue = IvaValue
                                                             }
                item.CostDistributionDirectCostDetailIva.Add(detailIva)
            Next
        Next
    End Sub

    ''' <summary>
    ''' Reversa un documento de provisión
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ReverseProvisionDocument(IdProvisionDocument As Integer, audit As AuditMessage) As ActionResult(Of CostDistributionDirectCost) Implements ICostDistributionDirectCostAdminService.ReverseProvisionDocument
        If IdProvisionDocument = Nothing Then
            Throw New ArgumentNullException("IdProvisionDocument")
        End If

        Dim UnitOfWork As IUnitWork = _distributionDirectCostRepository.UnitWork
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                Dim resultStore = _distributionDirectCostRepository.SP_ReverseProvisionDocumentDistributionDirectCost(IdProvisionDocument, audit.CodeUser, 2).FirstOrDefault()
                If resultStore.CodeMessage = 999 Then
                    transaction.Dispose()
                    Return New ActionResult(Of CostDistributionDirectCost) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList}
                End If

                transaction.Complete()
                Return New ActionResult(Of CostDistributionDirectCost) With {.StateResult = True, .MessageResult = {resultStore.Message}.ToList}

            Catch ex As Exception
                UnitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CostDistributionDirectCost) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Confirma las distribuciones de elementos del costo cuando son documentos tipo provisión
    ''' </summary>
    ''' <remarks></remarks>
    Public Function SaveDistributionDirectCostProvisionDocument(IdProvisionDocument As Integer, audit As AuditMessage) As ActionResult(Of CostDistributionDirectCost) Implements ICostDistributionDirectCostAdminService.SaveDistributionDirectCostProvisionDocument
        If IdProvisionDocument = Nothing Then
            Throw New ArgumentNullException("IdProvisionDocument")
        End If

        Dim UnitOfWork As IUnitWork = _distributionDirectCostRepository.UnitWork
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                Dim resultStore = _distributionDirectCostRepository.SP_ConfirmProvisionDocumentDistributionDirectCost(IdProvisionDocument, audit.CodeUser).ToList()
                If resultStore.Item(0).CodeMessage = 999 Then
                    transaction.Dispose()
                    Return New ActionResult(Of CostDistributionDirectCost) With {.StateResult = False, .MessageResult = {resultStore.Item(0).Message}.ToList}
                End If

                transaction.Complete()
                Return New ActionResult(Of CostDistributionDirectCost) With {.StateResult = True, .MessageResult = resultStore.Select(Function(x) x.Message).ToList()}

            Catch ex As Exception
                UnitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CostDistributionDirectCost) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Updates the state distribution direct cost.
    ''' </summary>
    Public Function UpdateStateDistributionDirectCost(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostDistributionDirectCost) Implements ICostDistributionDirectCostAdminService.UpdateStateDistributionDirectCost
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim distributionDirectCost As CostDistributionDirectCost = Me._distributionDirectCostRepository.GetDistributionDirectCost(code.Trim())
            If distributionDirectCost IsNot Nothing AndAlso distributionDirectCost.Id > 0 Then
                distributionDirectCost.Status = state
            End If
            Return Me.SaveDistributionDirectCost(distributionDirectCost, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Gets the main account value by container number account year and motn.
    ''' </summary>
    Public Function GetMainAccountValueByContainerNumberAccountYearAndMotn(mainAccountId As Integer, year As String, month As Integer) As Decimal Implements ICostDistributionDirectCostAdminService.GetMainAccountValueByNumberAccountYearAndMotn

        If String.IsNullOrEmpty(year) Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("Month")
        End If

        Try
            Dim mainAccountValue As Decimal = Me._distributionDirectCostRepository.GetMainAccountValueByNumberAccountYearAndMotn(mainAccountId, year, month)

            Return mainAccountValue
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return 0D
        End Try
    End Function

    ''' <summary>
    ''' Lista los gastos directos por año y mes
    ''' </summary>
    Public Function ListDistributionDirectCostByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionDirectCost) Implements ICostDistributionDirectCostAdminService.ListDistributionDirectCostByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Return Me._distributionDirectCostRepository.ListDistributionDirectCostByYearMonth(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Calcula el valor de distribucion en los diferentes centros de producción
    ''' </summary>
    ''' <param name="CostGeneralExpenseId"></param>
    ''' <param name="value"></param>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <param name="containerName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateCostDistribution(CostGeneralExpenseId As Integer, value As Decimal, year As Integer, month As Integer, containerName As String) As ActionResult(Of List(Of CostDistributionDirectCostDetail)) Implements ICostDistributionDirectCostAdminService.CalculateCostDistribution
        Try
            Dim listCostDistributionDirectCostDetail As New List(Of CostDistributionDirectCostDetail)()
            Dim listPCenter = Me._distributionDirectCostRepository.CalculateCostDistribution(CostGeneralExpenseId, year, month, value)

            If listPCenter IsNot Nothing AndAlso listPCenter.Count > 0 Then
                For Each pCenter In listPCenter
                    Dim costDistributionDirectCostDetail As New CostDistributionDirectCostDetail()
                    costDistributionDirectCostDetail.ProductionCenterId = pCenter.ProductionCenterId
                    costDistributionDirectCostDetail.ProductionCenterCodeName = pCenter.ProductionCenterCodeName
                    costDistributionDirectCostDetail.MainAccountId = pCenter.MainAccountId
                    costDistributionDirectCostDetail.MainAccountCodeName = pCenter.MainAccountNumberName
                    costDistributionDirectCostDetail.CostCenterId = pCenter.CostCenterId
                    costDistributionDirectCostDetail.CostCenterCodeName = pCenter.CostCenterCodeName
                    costDistributionDirectCostDetail.Percentage = pCenter.Percentage
                    costDistributionDirectCostDetail.Value = pCenter.Value
                    listCostDistributionDirectCostDetail.Add(costDistributionDirectCostDetail)
                Next
            End If

            Return New ActionResult(Of List(Of CostDistributionDirectCostDetail)) With {.StateResult = True, .ObjectEmbbeded = listCostDistributionDirectCostDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CostDistributionDirectCostDetail)) With {.StateResult = False, .Message = ex.Message & vbCrLf & ex.StackTrace}
        End Try
    End Function

    ''' <summary>
    ''' Desconfirma la distribución del elemento del costo
    ''' </summary>
    Public Function DisconfirmDistributionDirectCost(distributionDirectCost As CostDistributionDirectCost, audit As AuditMessage) As ActionResult(Of CostDistributionDirectCost) Implements ICostDistributionDirectCostAdminService.DisconfirmDistributionDirectCost
        If distributionDirectCost Is Nothing Then
            Throw New ArgumentNullException("distributionDirectCost")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionDirectCostRepository.UnitWork
        Dim accountPayableUnitOfWork As IUnitWork = Me._accountPayableRepository.UnitWork
        Dim accountPayableDetailConceptUnitOfWork As IUnitWork = Me._accountPayableDetailConceptRepository.UnitWork
        Dim costDistributionDirectCostDetailIvaUnitOfWork As IUnitWork = Me._costDistributionDirectCostDetailIvaRepository.UnitWork

        Try
            Dim costSetting As CostSetting = _settingCostRepository.GetCostSetting()
            If costSetting Is Nothing OrElse costSetting.Id = 0 Then
                Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "No existen parámetros de costo"}
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                distributionDirectCost = _distributionDirectCostRepository.GetDistributionDirectCostById(distributionDirectCost.Id)

                'Valido que la factura este en estado registrada
                Dim ap As AccountPayable = _accountPayableRepository.GetAccountPayableById(distributionDirectCost.AccountPayableId, False)
                If ap IsNot Nothing Then
                    If ap.Status <> 1 Then
                        Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "La factura asociada debe estar en estado registrada."}
                    End If

                    If ap.CostDistributionDirectCostId <> distributionDirectCost.Id Then
                        Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "La factura se encuentra asociada a una distribución diferente."}
                    End If

                    If costSetting.AccountingCosts Then
                        For Each item In ap.AccountPayableDetailConcept.Where(Function(d) d.IsDirectCost).ToList()
                            ap.Value = ap.Value - item.Value
                            ap.Balance = ap.Balance - item.Value
                            _accountPayableDetailConceptRepository.DeleteEntity(item)
                            accountPayableDetailConceptUnitOfWork.Commit()
                        Next
                        ap.CostDistributionDirectCostId = Nothing
                        Me._accountPayableRepository.SaveEntity(ap)
                        accountPayableUnitOfWork.Commit()
                    End If
                End If

                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Update
                Dim auxExpenseConcept As CostDistributionDirectCost = Nothing
                distributionDirectCost.Status = 1
                distributionDirectCost.ModificationDate = Date.Now
                distributionDirectCost.ModificationUser = audit.CodeUser
                auxExpenseConcept = distributionDirectCost.OriginalValue

                For Each item In distributionDirectCost.CostDistributionDirectCostDetail
                    For Each itemDetail In item.CostDistributionDirectCostDetailIva
                        _costDistributionDirectCostDetailIvaRepository.DeleteEntity(itemDetail)
                        costDistributionDirectCostDetailIvaUnitOfWork.Commit()
                    Next
                Next

                Me._distributionDirectCostRepository.SaveEntity(distributionDirectCost)
                unitOfWork.CommitAndRefreshChanges()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionDirectCost)(distributionDirectCost, audit, status, auxExpenseConcept)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionDirectCost, .Message = "El documento se desconfirmó con éxito"}
            End Using
        Catch ex As OptimisticConcurrencyException
            accountPayableDetailConceptUnitOfWork.RollbackChangesUnitOfWork()
            accountPayableUnitOfWork.RollbackChangesUnitOfWork()
            unitOfWork.RollbackChangesUnitOfWork()
            Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            accountPayableDetailConceptUnitOfWork.RollbackChangesUnitOfWork()
            accountPayableUnitOfWork.RollbackChangesUnitOfWork()
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionDirectCost) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Copy And Paste Detalles de la distribucion de elementos del costo
    ''' </summary>
    ''' <param name="GeneralExpenseId"></param>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function CopyAndPasteCostDistributionDirectCostDetail(GeneralExpenseId As Integer, data As List(Of List(Of String))) As ActionResult(Of List(Of CostDistributionDirectCostDetail)) Implements ICostDistributionDirectCostAdminService.CopyAndPasteCostDistributionDirectCostDetail
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListCostDistributionDirectCostDetail As New List(Of CostDistributionDirectCostDetail)
        'Listado de errores
        Dim listErrors As New List(Of String)
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlCopyAndPasteCostDistributionDirectCostDetail(GeneralExpenseId, data)
            'Se consume el procedimiento almacenado
            Dim resultStore = Me._distributionDirectCostRepository.SP_CopyAndPasteCostDistributionDirectCostDetail(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then
                        'Se crea el nuevo objeto para agregarlo al listado
                        ListCostDistributionDirectCostDetail.Add(New CostDistributionDirectCostDetail With
                        {
                            .ProductionCenterId = itemXml.ProductionCenterId,
                            .ProductionCenterCodeName = itemXml.ProductionCenterCodeName,
                            .MainAccountId = itemXml.MainAccountId,
                            .MainAccountCodeName = itemXml.MainAccountNumberName,
                            .CostCenterId = itemXml.CostCenterId,
                            .CostCenterCodeName = itemXml.CostCenterCodeName,
                            .MeasurementUnitId = itemXml.MeasurementUnitId,
                            .MeasurementUnitCodeName = itemXml.MeasurementUnitCodeName,
                            .ThirdPartyId = itemXml.ThirdPartyId,
                            .ThirdPartyNitName = itemXml.ThirdPartyNitName,
                            .EmployeeId = itemXml.EmployeeId,
                            .HandlesThirdParty = itemXml.HandlesThirdParty,
                            .JobTitle = itemXml.JobTitle,
                            .PositionId = itemXml.PositionId,
                            .PositionCodeName = itemXml.PositionCodeName,
                            .HoursManpower = itemXml.HoursManpower,
                            .ManpowerHoursContracted = itemXml.ManpowerHoursContracted,
                            .ProductId = itemXml.ProductId,
                            .ProductCodeName = itemXml.ProductCodeName,
                            .Percentage = itemXml.Percentage,
                            .Count = itemXml.Count,
                            .CostValue = itemXml.CostValue,
                            .BaseValue = itemXml.BaseValue,
                            .IvaValue = itemXml.IvaValue,
                            .Value = itemXml.Value,
                            .RetentionId = itemXml.RetentionId,
                            .BaseRetention = itemXml.BaseRetention,
                            .Nature = itemXml.Nature,
                            .NatureText = itemXml.NatureText,
                            .InvoicedValue = itemXml.InvoicedValue,
                            .Observations = itemXml.Observation,
                            .PayrollConcept = itemXml.PayrollConcept,
                            .ProcessDate = itemXml.ProcessDate
                        })
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of CostDistributionDirectCostDetail)) With {.StateResult = True, .ObjectEmbbeded = ListCostDistributionDirectCostDetail, .MessageResult = listErrors}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of CostDistributionDirectCostDetail)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of CostDistributionDirectCostDetail)) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of CostDistributionDirectCostDetail)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

#End Region

#Region "Internal Methods"

    Private Function GenerateAccountPayable(distributionDirectCost As CostDistributionDirectCost, costSetting As CostSetting) As AccountPayable
        Dim AccountPayable As New AccountPayable

        'Se genera la cabecera de la cxp
        With AccountPayable
            .EntityId = distributionDirectCost.Id
            .EntityCode = distributionDirectCost.Code
            .EntityName = distributionDirectCost.GetType().Name
            .IdOperatingUnit = distributionDirectCost.OperatingUnitId
            .DocumentDate = distributionDirectCost.ServicePeriodDate
            .IdSuppliersDistributionLines = distributionDirectCost.SuppliersDistributionLinesId
            .IdAccount = distributionDirectCost.MainAccountId
            .IdCostCenter = distributionDirectCost.CostCenterId
            .IdSupplier = distributionDirectCost.SupplierId
            .PositionId = distributionDirectCost.PositionId
            .SupplierTypeId = distributionDirectCost.SupplierTypeId
            .IdThirdParty = distributionDirectCost.ThirdPartyId
            .ServicePeriodDate = distributionDirectCost.ServicePeriodDate
            .FilingUnitId = distributionDirectCost.FilingUnitId
            .Status = 1
            .InitialBalance = False
            .PreviousBudget = False
            .DeductibleIva = distributionDirectCost.DeductibleIva
            .CurrencyId = distributionDirectCost.CurrencyId
            .TaxRegistration = distributionDirectCost.TaxRegistration

            'Detalle de la factura
            .BillNumber = distributionDirectCost.BillNumber
            .BillDate = distributionDirectCost.BillDate
            .Term = distributionDirectCost.Term
            .ExpirationDate = DateAdd(DateInterval.Day, .Term, .BillDate)
            .InvoiceValue = distributionDirectCost.Value
            .Value = distributionDirectCost.Value
            .Balance = distributionDirectCost.Value
            .Coments = distributionDirectCost.Observation
            .Shares = 1
            If .PositionId IsNot Nothing Then
                .Hours = distributionDirectCost.Hours
            End If
            .CostDistributionDirectCostId = distributionDirectCost.Id

            'Se agregan los detalles de la distribución
            Dim listAccountPayableDetailConcept = Me.GenerateAccountPayableDetailConcept(distributionDirectCost, costSetting)
            For Each item In listAccountPayableDetailConcept
                .AccountPayableDetailConcept.Add(item)
            Next


            'Se crea la cuota de a cxp
            Dim AccountPayableShares As New AccountPayableShares
            AccountPayableShares.Share = 1
            AccountPayableShares.DateExpires = .ExpirationDate
            AccountPayableShares.InitialValue = .Value
            AccountPayableShares.Balance = .Balance
            .AccountPayableShares.Add(AccountPayableShares)
        End With

        Return AccountPayable
    End Function

    Private Function GenerateAccountPayableDetailConcept(distributionDirectCost As CostDistributionDirectCost, costSetting As CostSetting) As List(Of AccountPayableDetailConcept)
        Dim listAccountPayableDetailConcept As New List(Of AccountPayableDetailConcept)

        Dim costGeneralExpense = Me._generalExpenseRepository.GetGeneralExpenseById(distributionDirectCost.GeneralExpenseId)
        Dim companySetting = _companySettingsRepository.GetCompanySettings()
		Dim AccountPayableSameSupplier As Boolean = distributionDirectCost.AccountPayableSameSupplier
		Dim thirdPartyId = distributionDirectCost.ThirdPartyId
		For Each item In distributionDirectCost.CostDistributionDirectCostDetail
			If item.ThirdPartyId IsNot Nothing And Not AccountPayableSameSupplier AndAlso (costGeneralExpense.DistributionType = 2 OrElse costGeneralExpense.DistributionType = 3) Then ' Distribución de mano de obra / Distribución de gastos generales
				thirdPartyId = item.ThirdPartyId
			End If

			Dim RateIva As Integer? = Nothing
			Dim BaseValue As Decimal = item.Value
			Dim IvaValue As Decimal? = Nothing
			Dim TotalConcept As Decimal? = item.Value

			'validamos si esta parametrizado como iva costo fiscal a cada concepto no le agrege su iva ya que queda de forma general
			If distributionDirectCost.TaxRegistration <> 1 Then
				If Not distributionDirectCost.DeductibleIva.Value AndAlso distributionDirectCost.CostDistributionDirectCostIva.Any Then
					RateIva = distributionDirectCost.CostDistributionDirectCostIva.First().GeneralLedgerIvaId
					BaseValue = item.BaseValue
					IvaValue = item.IvaValue
					TotalConcept = item.Value
				End If
			End If

			Dim data = AddAccountPayableDetailConcept(costSetting.AccountPayableConceptsId, item.MainAccountId, thirdPartyId, item.Value, item.CostCenterId, 1, RateIva, BaseValue, IvaValue, TotalConcept)
			listAccountPayableDetailConcept.Add(data)
		Next


		If distributionDirectCost.DeductibleIva = 0 Then
            '' validamos si es iva control fiscal para agregar el iva en dos lineas distintas
            If distributionDirectCost.TaxRegistration = 1 Then
                ''Dim sumValue = distributionDirectCost.CostDistributionDirectCostIva.Sum(Function(x) x.IvaValue)
                Dim debitList = New List(Of AccountPayableDetailConcept)
                Dim creditList = New List(Of AccountPayableDetailConcept)
                For Each item In distributionDirectCost.CostDistributionDirectCostIva
                    Dim itemIva As GeneralLedgerIVA = (From i In _context.GeneralLedgerIVA.AsNoTracking Where i.Id = item.GeneralLedgerIvaId Select i).FirstOrDefault

                    ''validacion para el caso de que las cuentas debito se repitan
                    Dim validationDebit = debitList.Where(Function(x) x.IdAccount = itemIva.IdAccountDebitControlFiscal).FirstOrDefault
                    If validationDebit IsNot Nothing Then
                        debitList.Where(Function(x) x.IdAccount = itemIva.IdAccountDebitControlFiscal).FirstOrDefault.Value += item.IvaValue
                    Else
                        Dim addDebit = AddAccountPayableDetailConcept(costSetting.AccountPayableConceptsId, itemIva.IdAccountDebitControlFiscal, distributionDirectCost.ThirdPartyId, item.IvaValue, Nothing, 1)
                        debitList.Add(addDebit)
                    End If

                    ''validacion en caso de que las cuentas credito se repitan
                    Dim validationCredit = creditList.Where(Function(x) x.IdAccount = itemIva.IdAccountCreditControlFiscal).FirstOrDefault
                    If validationCredit IsNot Nothing Then
                        creditList.Where(Function(x) x.IdAccount = itemIva.IdAccountCreditControlFiscal).FirstOrDefault.Value += item.IvaValue
                    Else
                        Dim addCredit = AddAccountPayableDetailConcept(costSetting.AccountPayableConceptsId, itemIva.IdAccountCreditControlFiscal, distributionDirectCost.ThirdPartyId, item.IvaValue, Nothing, 2)
                        creditList.Add(addCredit)
                    End If
                Next

                For Each item In debitList
                    listAccountPayableDetailConcept.Add(item)
                Next
                For Each item In creditList
                    listAccountPayableDetailConcept.Add(item)
                Next
            End If
        Else
            ''Se agregan las cuentas de compras parametrizadas
            Dim account = New List(Of AccountPayableDetailConcept)
            For Each item In distributionDirectCost.CostDistributionDirectCostIva
                Dim generalLedgerIva As GeneralLedgerIVA = _generalLedgerIVARepository.GetGeneralLedgerIVAById(item.GeneralLedgerIvaId)
                Dim validation = account.Where(Function(x) x.IdAccount = generalLedgerIva.IdAccountPurchaseService).FirstOrDefault
                If validation IsNot Nothing Then
                    account.Where(Function(x) x.IdAccount = generalLedgerIva.IdAccountPurchaseService).FirstOrDefault.Value += item.IvaValue
                Else
                    Dim dataDebit = AddAccountPayableDetailConcept(costSetting.AccountPayableConceptsId, generalLedgerIva.IdAccountPurchaseService, distributionDirectCost.ThirdPartyId, item.IvaValue, Nothing, 1,)
                    account.Add(dataDebit)
                End If
            Next
            For Each item In account
                listAccountPayableDetailConcept.Add(item)
            Next
        End If
        Return listAccountPayableDetailConcept
    End Function


    Private Function AddAccountPayableDetailConcept(AccountPayableConceptsId As Integer, IdAccount As Integer, ThirdPartyId As Integer, value As Decimal, IdCostCenter As Integer?, Nature As Integer,
                                                    Optional RateIva As Integer? = Nothing, Optional BaseValue As Decimal = 0, Optional IvaValue As Decimal? = Nothing,
                                                    Optional TotalConcept As Decimal? = Nothing) As AccountPayableDetailConcept
        Dim AccountPayableDetailConcept As New AccountPayableDetailConcept With
                {
                 .IdConceptAccountPayable = AccountPayableConceptsId,
                .IdAccount = IdAccount,
                .IdThirdParty = ThirdPartyId,
                .DeferredCausation = False,
                .HandlesDeferredCausation = False,
                .IdCostCenter = IdCostCenter,
                .Detail = "Detalle generado por distribución de elementos del costo",
                .IdRetentionConcept = Nothing,
                .Value = value,
                .BillingValue = 0,
                .Nature = Nature,
                .RateIva = RateIva,
                .BaseValue = BaseValue,
                .IvaValue = IvaValue,
                .TotalConcept = TotalConcept,
                .IsDirectCost = True
                }
        Return AccountPayableDetailConcept
    End Function

    Private Function ConvertToXmlCopyAndPasteCostDistributionDirectCostDetail(GeneralExpenseId As Integer, data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Data>")

        builder.Append("<GeneralExpenseId>" & GeneralExpenseId & "</GeneralExpenseId>")

        For Each itemRow In data
            builder.Append("<Row>")

            Dim index = 0
            For Each itemColumn In itemRow
                builder.Append(String.Format("<Item{0}>{1}</Item{0}>", index, itemColumn))
                index = index + 1
            Next

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")

        Return builder.ToString
    End Function

    ''' <summary>
    ''' Genera internamente el Comprobante Contable cuando No se genera CxP
    ''' </summary>
    ''' <param name="distributionDirectCost"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function GenerateJournalVoucher(distributionDirectCost As CostDistributionDirectCost, audit As AuditMessage) As ActionResult(Of JournalVouchers)
        Dim JournalVouchers As New JournalVouchers

        Try
            With JournalVouchers
                If distributionDirectCost.Status = 2 Then
                    .IdJournalVoucher = distributionDirectCost.CostGeneralExpense.JournalVoucherTypesId
                    .Detail = String.Format("Distribución de Elementos de Costo No. {0}", distributionDirectCost.Code)
                ElseIf distributionDirectCost.Status = 4 Then
                    .IdJournalVoucher = distributionDirectCost.CostGeneralExpense.ReversalJournalVoucherTypesId
                    .Detail = String.Format("Reversión del documento No. {0}", distributionDirectCost.Code)
                End If
                .VoucherDate = distributionDirectCost.DocumentDate
                .Imported = False
                .Status = 2
                .EntityId = distributionDirectCost.Id
                .EntityCode = distributionDirectCost.Code
                .EntityName = distributionDirectCost.GetType().Name
                .IsClosedYear = False
                .CreationDate = Date.Now
                .CreationUser = audit.IdUser

                For Each detail As CostDistributionDirectCostDetail In distributionDirectCost.CostDistributionDirectCostDetail
                    Dim JournalVouchersDetail As New JournalVoucherDetails

                    With JournalVouchersDetail
                        .IdMainAccount = detail.MainAccountId
                        If distributionDirectCost.Status = 2 Then
                            If detail.Nature = 1 Then
                                .DebitValue = detail.Value
                            Else
                                .CreditValue = detail.Value
                            End If
                            .Detail = "Detalle del Comprobante Contable generado por la Distribución de Elementos del Costo"
                        ElseIf distributionDirectCost.Status = 4 Then
                            If detail.Nature = 1 Then
                                .CreditValue = detail.Value
                            Else
                                .DebitValue = detail.Value
                            End If
                            .Detail = "Detalle del Comprobante Contable generado por la Reversión de Distribución de Elementos del Costo"
                        End If
                        .IdThirdParty = detail.ThirdPartyId
                        .IdCostCenter = detail.CostCenterId
                        If detail.RetentionId IsNot Nothing Then
                            Dim res = _retentionRepository.GetRetentionById(detail.RetentionId)
                            If res IsNot Nothing Then
                                .IdRetention = detail.RetentionId
                                .RetentionRate = res.Rate
                                .BaseValue = detail.BaseRetention
                            End If
                        Else
                            .IdRetention = Nothing
                            .RetentionRate = 0
                        End If
                    End With
                    .JournalVoucherDetails.Add(JournalVouchersDetail)
                Next
            End With
            'Valido que los valores credito y debito sean iguales y no este desbalanceado el documento
            Dim ban As Boolean = ValidateCreditDebit(JournalVouchers)
            If ban = False Then
                Dim DebitValue = JournalVouchers.JournalVoucherDetails.Sum(Function(d) d.DebitValue)
                Dim CreditValue = JournalVouchers.JournalVoucherDetails.Sum(Function(d) d.CreditValue)
                Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = String.Format("El Documento contable de Distribución de Elementos del Costo '{0}' esta desbalanceado (Debito: {1} - Credito: {2}).", distributionDirectCost.Code, DebitValue, CreditValue)}
            End If

            Return New ActionResult(Of JournalVouchers) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = JournalVouchers}
        Catch ex As Exception
            Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Valido que los valores credito y debito sean iguales y no este desbalanceado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ValidateCreditDebit(ByVal accounting As JournalVouchers) As Boolean
        Dim valCredit As Decimal = 0
        Dim valDebit As Decimal = 0
        For Each itemDetail As JournalVoucherDetails In accounting.JournalVoucherDetails
            If itemDetail.CreditValue > 0 Then
                valCredit = valCredit + itemDetail.CreditValue
            ElseIf itemDetail.DebitValue > 0 Then
                valDebit = valDebit + itemDetail.DebitValue
            End If
        Next
        If valCredit <> valDebit Then
            Return False
        End If
        Return True
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _accountPayableAdminService.Dispose()
            End If
            _distributionDirectCostRepository = Nothing
            _sequenceDRepository = Nothing
            _settingCostRepository = Nothing
            _sequensePaymentsCRepository = Nothing
            _accountPayableRepository = Nothing
            _accountPayableDetailConceptRepository = Nothing
            _generalExpenseRepository = Nothing
            _costDistributionDirectCostDetailIvaRepository = Nothing

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