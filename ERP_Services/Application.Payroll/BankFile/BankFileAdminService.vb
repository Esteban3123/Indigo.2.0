'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Base
Imports Application.Payroll
Imports Application.Treasury
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports System.Text
Imports System.Transactions

Public Class BankFileAdminService
    Implements IBankFileAdminService

#Region "Properties"

    Private _auditoryBankFileRepository As IAuditoryBankFileRepository
    Private _bank As IBankRepository
    Private _bankFileDomain As IBankFileDomain
    Private _bankFileRepository As IBankFileRepository
    Private _functionalUnitRepository As IFunctionalUnitRepository
    Private _groupRepository As IGroupRepository
    Private _liquidationRepository As IPayrollLiquidationRepository
    Private _positionRepository As IPositionRepository
    Private _sequenseTreasuryRepository As Domain.Entities.ISequenseTreasuryCRepository
    Private _voucherTransactionAdmin As IVoucherTransactionAdminService

#End Region

#Region "Builder"

    ''' <summary>
    ''' inicia el repositorio de Plano de Bancos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal auditoryBankFileRepository As IAuditoryBankFileRepository, ByVal bank As IBankRepository, ByVal bankFileDomain As IBankFileDomain, bankFileRepository As IBankFileRepository,
                   functionalUnitRepository As IFunctionalUnitRepository, groupRepository As IGroupRepository, ByVal liquidationRepository As IPayrollLiquidationRepository,
                   positionRepository As IPositionRepository, voucherTransactionAdmin As IVoucherTransactionAdminService, sequenseTreasuryRepository As Domain.Entities.ISequenseTreasuryCRepository)
        If (bankFileDomain Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Archivos Planos de Bancos vacio")
        End If

        Me._auditoryBankFileRepository = auditoryBankFileRepository
        Me._bank = bank
        Me._bankFileDomain = bankFileDomain
        Me._bankFileRepository = bankFileRepository
        Me._functionalUnitRepository = functionalUnitRepository
        Me._groupRepository = groupRepository
        Me._liquidationRepository = liquidationRepository
        Me._positionRepository = positionRepository
        Me._sequenseTreasuryRepository = sequenseTreasuryRepository
        Me._voucherTransactionAdmin = voucherTransactionAdmin
    End Sub

#End Region

#Region "Methods"

    Public Function GetDocumentInvoiceProductSalesById(id As Integer) As BankFile Implements IBankFileAdminService.GetBankFileById
        Try
            Return Me._bankFileRepository.GetBankFileById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New BankFile()
        End Try
    End Function

    Public Function GetBankFileByCode(code As String, audit As AuditMessage) As BankFile Implements IBankFileAdminService.GetBankFileByCode
        Try
            Dim BankFile As BankFile = Me._bankFileRepository.GetBankFileByCode(code)
            If BankFile IsNot Nothing AndAlso BankFile.Id > 0 Then
                Dim auditProcess As IndigoAuditSimpleEntity(Of BankFile) = New IndigoAuditSimpleEntity(Of BankFile)(BankFile, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return BankFile
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New BankFile()
        End Try
    End Function

    Public Function SaveBankFile(BankFile As BankFile, listBankFileDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of BankFile) Implements IBankFileAdminService.SaveBankFile
        If BankFile Is Nothing Then
            Throw New ArgumentNullException("BankFile")
        End If

        Dim unitOfWork As IUnitWork = Me._bankFileRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlBankFile(BankFile)
                Dim ListString As List(Of String) = ConvertToXmlListDelete(listBankFileDetailDelete)

                Dim resultStore = Me._bankFileRepository.SP_SaveBankFile(EntityXml, ListString, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of BankFile) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList, .Message = resultStore.Message}
                End If

                BankFile.Id = resultStore.Id
                BankFile.Code = resultStore.Code
                BankFile.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of BankFile) With {.StateResult = True, .ObjectEmbbeded = BankFile}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of BankFile) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of BankFile) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function GenerateBankFile(bankFileId As Integer, session As SessionValues) As ActionMessageResult(Of StringBuilder) Implements IBankFileAdminService.GenerateBankFile
        Try
            Dim bankFile As BankFile = Me._bankFileRepository.GetBankFileByIdForGenerateFile(bankFileId)
            Return Me._bankFileDomain.SelectBank(bankFile)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionMessageResult(Of StringBuilder)() With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function ConfirmBankFile(bankFile As BankFile, session As SessionValues) As ActionResult(Of BankFile) Implements IBankFileAdminService.ConfirmBankFile
        Dim unitOfWork As IUnitWork = Me._bankFileRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim resultStore = Me._bankFileRepository.SP_ConfirmBankFile(bankFile.Id, session.AuditMessageWcf.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of BankFile) With {.StateResult = False, .Message = resultStore.Message}
                End If

                Dim messageResult As New StringBuilder()
                messageResult.AppendLine($"Archivo Plano: {bankFile.Code}")

                '' SECUENCIAS NUMÉRICAS DE COMPROBANTE DE EGRESO
                Dim SequenseDetailTreasuryId As Integer = 0
                'Se consulta la secuencia de pagos por el id del form
                Dim treasurySequence As Domain.Entities.TreasurySequence = _sequenseTreasuryRepository.GetSequenseByIdForm("636")
                If treasurySequence.Id = 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of BankFile) With {.StateResult = False, .Message = "No existe secuencia numérica para el formulario de Comprobante de Egresos."}
                End If
                'Se valida que la secuencia numerica no sea manual
                If treasurySequence.IsManual Then
                    transaction.Dispose()
                    Return New ActionResult(Of BankFile) With {.StateResult = False, .Message = "La secuencia numerica de Comprobante de Entrada es manual."}
                End If
                'Se valida la secuencia numerica
                If treasurySequence.Scope = "O" Then 'Si la secuencia es por organización
                    SequenseDetailTreasuryId = (From x In treasurySequence.TreasurySequenceDetail Select x.Id).FirstOrDefault()
                ElseIf treasurySequence.Scope = "OU" Then 'Si la secuencia es por unidad operativa
                    'Se valida que la unidad operativa seleccionada este en la secuencia
                    If (From x In treasurySequence.TreasurySequenceDetail Where x.IdOperatingUnit = bankFile.OperatingUnitId Select x).Count = 0 Then
                        transaction.Dispose()
                        Return New ActionResult(Of BankFile) With {.StateResult = False, .Message = "No existe la unidad operativa seleccionada en la secuencia de Comprobantes de Egresos."}
                    End If
                    SequenseDetailTreasuryId = (From x In treasurySequence.TreasurySequenceDetail Where x.IdOperatingUnit = bankFile.OperatingUnitId Select x.Id).FirstOrDefault()
                ElseIf treasurySequence.Scope = "CC" Then 'Si la secuencia es por tipo de comprobante
                    'Se valida que exista una secuencia para el tipo de pago
                    If (From x In treasurySequence.TreasurySequenceDetail Where x.Type = 1 Select x).Count = 0 Then
                        transaction.Dispose()
                        Return New ActionResult(Of BankFile) With {.StateResult = False, .Message = "No existe la secuencia de Comprobantes de Egresos para el tipo Pago."}
                    End If
                    SequenseDetailTreasuryId = (From x In treasurySequence.TreasurySequenceDetail Where x.Type = 1 Select x.Id).FirstOrDefault()
                End If

                bankFile = Me._bankFileRepository.GetBankFileById(bankFile.Id, True)
                Dim objVoucherTransaction = Me._bankFileDomain.CreateVoucherTransaction(bankFile)
                If objVoucherTransaction.StateResult = True Then
                    Dim ObjResultVoucherTransaction = _voucherTransactionAdmin.SaveVoucherTransaction(objVoucherTransaction.ObjectEmbbeded, session.AuditMessageWcf, 0, SequenseDetailTreasuryId, treasurySequence, 0)
                    If ObjResultVoucherTransaction.StateResult = False Then
                        transaction.Dispose()
                        Return New ActionResult(Of BankFile) With {.StateResult = False, .Message = ObjResultVoucherTransaction.Message}
                    End If

                    bankFile.VoucherTransactionId = ObjResultVoucherTransaction.ObjectEmbbeded.Id
                    _bankFileRepository.SaveEntity(bankFile)
                    _bankFileRepository.UnitWork.Commit()

                    messageResult.AppendLine("Comprobante de Egreso: " + ObjResultVoucherTransaction.ObjectEmbbeded.Code)
                Else
                    transaction.Dispose()
                    Return New ActionResult(Of BankFile) With {.StateResult = False, .Message = objVoucherTransaction.Message}
                End If

                transaction.Complete()
                Return New ActionResult(Of BankFile) With {.StateResult = True, .ObjectEmbbeded = bankFile, .Message = messageResult.ToString()}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of BankFile) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return New ActionResult(Of BankFile) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function SaveAndConfirmBankFile(BankFile As BankFile, listBankFileDetailDelete As List(Of Integer), session As SessionValues) As ActionResult(Of BankFile) Implements IBankFileAdminService.SaveAndConfirmBankFile
        BankFile.Status = 1
        Dim result = Me.SaveBankFile(BankFile, listBankFileDetailDelete, session.AuditMessageWcf)
        If result.StateResult = True Then
            Dim resultConfirm = Me.ConfirmBankFile(result.ObjectEmbbeded, session)
            If resultConfirm.StateResult = True Then
                If BankFile.ChangeTracker.State = ObjectState.Added Then
                    Return New ActionResult(Of BankFile) With {.StateResult = True, .StateResultAux = True, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = "Se guardo y se confirmo correctamente" + Environment.NewLine + resultConfirm.Message}
                Else
                    Return New ActionResult(Of BankFile) With {.StateResult = True, .StateResultAux = True, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = "Se actualizo y se confirmo correctamente" + Environment.NewLine + resultConfirm.Message}
                End If
            Else
                If BankFile.ChangeTracker.State = ObjectState.Added Then
                    Return New ActionResult(Of BankFile) With {.StateResult = True, .StateResultAux = False, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = String.Format(ResourceManager.GetString("SavedNoConfirmed", "Billing"), result.ObjectEmbbeded.Code, Environment.NewLine + resultConfirm.Message)}
                Else
                    Return New ActionResult(Of BankFile) With {.StateResult = True, .StateResultAux = False, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = String.Format(ResourceManager.GetString("UpdatedNoConfirmed", "Billing"), result.ObjectEmbbeded.Code, Environment.NewLine + resultConfirm.Message)}
                End If
            End If
        Else
            Return New ActionResult(Of BankFile) With {.StateResult = False, .StateResultAux = False, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = String.Format(ResourceManager.GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message))}
        End If
    End Function

    Private Function ConvertToXmlBankFile(BankFile As BankFile) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<BankFile>")

        builder.Append("<Id>" & BankFile.Id & "</Id>")
        builder.Append("<Code>" & BankFile.Code & "</Code>")
        builder.Append("<OperatingUnitId>" & BankFile.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<CompanyId>" & BankFile.CompanyId & "</CompanyId>")
        builder.Append("<LiquidationDate>" & BankFile.LiquidationDate.ToString("dd/MM/yyyy") & "</LiquidationDate>")
        builder.Append("<EntityBankAccountId>" & BankFile.EntityBankAccountId & "</EntityBankAccountId>")
        builder.Append("<ExpenseConceptId>" & BankFile.ExpenseConceptId & "</ExpenseConceptId>")
        builder.Append("<Value>" & BankFile.Value.ToString().Replace(",", ".") & "</Value>")
        builder.Append("<Status>" & BankFile.Status & "</Status>")
        builder.Append("<Process>" & BankFile.Process & "</Process>")
        builder.Append("<YearIncentivePayment>" & BankFile.YearIncentivePayment & "</YearIncentivePayment>")
        builder.Append("<PeriodIncentivePayment>" & BankFile.PeriodIncentivePayment & "</PeriodIncentivePayment>")

        For Each detail In BankFile.BankFileDetail
            builder.Append("<BankFileDetail>")

            builder.Append("<Id>" & detail.Id & "</Id>")
            builder.Append("<BankFileId>" & detail.BankFileId & "</BankFileId>")
            builder.Append("<LiquidationId>" & detail.LiquidationId & "</LiquidationId>")
            builder.Append("<GroupId>" & detail.GroupId & "</GroupId>")
            builder.Append("<PositionId>" & detail.PositionId & "</PositionId>")
            builder.Append("<FunctionalUnitId>" & detail.FunctionalUnitId & "</FunctionalUnitId>")
            builder.Append("<ContractId>" & detail.ContractId & "</ContractId>")
            builder.Append("<EmployeeId>" & detail.EmployeeId & "</EmployeeId>")
            builder.Append("<EmployeeBankId>" & detail.EmployeeBankId & "</EmployeeBankId>")
            builder.Append("<EmployeeBankTypeAccount>" & detail.EmployeeBankTypeAccount & "</EmployeeBankTypeAccount>")
            builder.Append("<EmployeeBankAccountNumber>" & detail.EmployeeBankAccountNumber & "</EmployeeBankAccountNumber>")
            builder.Append("<BasicSalary>" & detail.BasicSalary.ToString().Replace(",", ".") & "</BasicSalary>")
            builder.Append("<TotalAccrued>" & detail.TotalAccrued.ToString().Replace(",", ".") & "</TotalAccrued>")
            builder.Append("<TotalDeducted>" & detail.TotalDeducted.ToString().Replace(",", ".") & "</TotalDeducted>")
            builder.Append("<TotalPaid>" & detail.TotalPaid.ToString().Replace(",", ".") & "</TotalPaid>")

            builder.Append("</BankFileDetail>")
        Next

        builder.Append("</BankFile>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListDelete(listBankFileDetailDelete As List(Of Integer)) As List(Of String)
        Dim builder As StringBuilder
        Dim listString As New List(Of String)

        builder = New StringBuilder()
        If listBankFileDetailDelete IsNot Nothing AndAlso listBankFileDetailDelete.Count > 0 Then
            For Each detailId In listBankFileDetailDelete
                builder.Append("<BankFileDetailDelete>")
                builder.Append("<Id>" & detailId & "</Id>")
                builder.Append("</BankFileDetailDelete>")
            Next
        End If
        listString.Add(builder.ToString)

        Return listString
    End Function
    ''' <summary>
    ''' Funcion que consume el sp BankFileIncentivePaymentWithoutConfirm
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="DateLiquidated"></param>
    ''' <returns></returns>
    Public Function ShowIncentivePayment(Period As Integer, DateLiquidated As Date) As List(Of SP_BankFileIncentivePaymentWithoutConfirm_Result) Implements IBankFileAdminService.ShowIncentivePayment
        Try
            'Se consume el procedimiento almacenado
            Dim resultStore = Me._bankFileRepository.ShowIncentivePayment(Period, DateLiquidated)

            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                Return resultStore.ToList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _bankFileDomain.Dispose()
            End If

            _auditoryBankFileRepository = Nothing
            _bank = Nothing
            _bankFileDomain = Nothing
            _liquidationRepository = Nothing
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
