'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 25-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core
Imports System.Data.Entity.Validation
Imports System.Transactions
Imports Application.Accounting
Imports Application.Portfolio
Imports Application.Treasury
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.InterfaceERPPayroll
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.ModelRepository

Public Class CostDistributionAdminService
    Implements ICostDistributionAdminService

    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Repositorio de Distribución de Gastos
    ''' </summary>
    ''' <remarks></remarks>
    Private _CostDistributionRepository As ICostDistributionsRepository

    ''' <summary>
    ''' Servicios de Domnio de Distribucíón de Gastos
    ''' </summary>
    ''' <remarks></remarks>
    Private _CostDistributionDomain As ICostDistributionDomain

    ''' <summary>
    ''' Repositorio de Liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Repositorio de Schedule Detail
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleDetailRepository As IScheduleDetailRepository

    ''' <summary>
    ''' Servicios de Liquidación de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationDomain As Domain.Payroll.ILiquidationDomain

    ''' <summary>
    ''' Servicios de Interface ERP NEt para Nómina
    ''' </summary>
    ''' <remarks></remarks>
    Private _InterfaceERPPayrollNet As Domain.InterfaceERPPayroll.IInterfaceNET

    ''' <summary>
    ''' Repositorio de Empleados
    ''' </summary>
    ''' <remarks></remarks>
    Private _employeeRepository As IEmployeeRepository

    ''' <summary>
    ''' Aplicación de Documentos Contables
    ''' </summary>
    ''' <remarks></remarks>
    Private _AccountingDocumentAdmin As IAccountingDocumentAdminService

    Private _groupRepository As IGroupRepository

    ''' <summary>
    ''' Repositorio de secuencia numerica para pagos
    ''' </summary>
    ''' <remarks></remarks>
    Private _sequensePortfolioCRepository As SequensePortfolioCRepository

    ''' <summary>
    ''' Repositorio de Clase de Convenios
    ''' </summary>
    Private _kindsAgreementsRepository As IKindsAgreementsRepository

    ''' <summary>
    ''' Repositorio de Parámetros de Nómina
    ''' </summary>
    Private _payrollSettingsRepository As IPayrollSettingsRepository

    ''' <summary>
    ''' REpositorio de Secuencias de Tesoreria
    ''' </summary>
    Private _sequenseTreasuryRepository As SequenseTreasuryCRepository

    Private _voucherTransactionAdmin As IVoucherTransactionAdminService

    ''' <summary>
    ''' incia el repositorio de CostDistributionRepository
    ''' </summary>
    ''' <param name="CostDistributionRepository">Repositorio de CostDistributionRepository</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal CostDistributionRepository As ICostDistributionsRepository, CostDistributionDomain As ICostDistributionDomain, liquidationRepository As IPayrollLiquidationRepository, scheduleDetailRepository As IScheduleDetailRepository, liquidationDomain As Domain.Payroll.LiquidationDomain, InterfaceERPPayrollNet As Domain.InterfaceERPPayroll.IInterfaceNET, employeeRepository As IEmployeeRepository, AccountingDocumentAdmin As IAccountingDocumentAdminService, groupRepository As IGroupRepository,
                   sequensePortfolioCRepository As SequensePortfolioCRepository, kindsAgreementsRepository As IKindsAgreementsRepository, payrollSettingsRepository As IPayrollSettingsRepository, sequenseTreasuryRepository As SequenseTreasuryCRepository, voucherTransactionAdmin As IVoucherTransactionAdminService)
        If (CostDistributionRepository Is Nothing = True) Then
            Throw New ArgumentNullException("CostDistributionRepository Vacio")
        End If
        If (CostDistributionDomain Is Nothing = True) Then
            Throw New ArgumentNullException("CostDistributionDomain Vacio")
        End If
        If (liquidationRepository Is Nothing = True) Then
            Throw New ArgumentNullException("liquidationRepository Vacio")
        End If
        If (scheduleDetailRepository Is Nothing = True) Then
            Throw New ArgumentNullException("scheduleDetailRepository Vacio")
        End If
        _CostDistributionRepository = CostDistributionRepository
        _CostDistributionDomain = CostDistributionDomain
        _liquidationRepository = liquidationRepository
        _scheduleDetailRepository = scheduleDetailRepository
        _liquidationDomain = liquidationDomain
        _InterfaceERPPayrollNet = InterfaceERPPayrollNet
        _employeeRepository = employeeRepository
        _AccountingDocumentAdmin = AccountingDocumentAdmin
        _groupRepository = groupRepository
        _sequensePortfolioCRepository = sequensePortfolioCRepository
        _kindsAgreementsRepository = kindsAgreementsRepository
        _payrollSettingsRepository = payrollSettingsRepository
        _sequenseTreasuryRepository = sequenseTreasuryRepository
        _voucherTransactionAdmin = voucherTransactionAdmin
    End Sub

    ''' <summary>
    ''' Lista la Distribución de la Nómina por Fecha y Id del Contrato
    ''' </summary>
    ''' <param name="PayrollDate">Fecha de Liquidación de Nómina</param>
    ''' <param name="ContractId">ID del Contrato</param>
    ''' <returns>List(Of CostDistributions)</returns>
    ''' <remarks></remarks>
    Public Function ListCostDistributionsByPayrollDateContractId(PayrollDate As Date, ContractId As Integer) As List(Of CostDistributions) Implements ICostDistributionAdminService.ListCostDistributionsByPayrollDateContractId
        If String.IsNullOrEmpty(PayrollDate) Then
            Throw New ArgumentNullException("PayrollDate Vacio")
        End If
        If String.IsNullOrEmpty(ContractId) Then
            Throw New ArgumentNullException("ContractId Vacio")
        End If
        Try
            Return _CostDistributionRepository.ListCostDistributionsByPayrollDateContractId(PayrollDate, ContractId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of CostDistributions)
        End Try
    End Function

    ''' <summary>
    ''' Lista la Distribución de la Nómina por Fecha y Id del Grupo
    ''' </summary>
    ''' <param name="PayrollDate">Fecha de Liquidación de Nómina</param>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>List(Of CostDistributions)</returns>
    ''' <remarks></remarks>
    Public Function ListCostDistributionsByPayrollDateGroupId(PayrollDate As Date, GroupId As Integer) As List(Of CostDistributions) Implements ICostDistributionAdminService.ListCostDistributionsByPayrollDateGroupId
        If String.IsNullOrEmpty(PayrollDate) Then
            Throw New ArgumentNullException("PayrollDate Vacio")
        End If
        If String.IsNullOrEmpty(GroupId) Then
            Throw New ArgumentNullException("GroupId Vacio")
        End If
        Try
            Return _CostDistributionRepository.ListCostDistributionsByPayrollDateGroupId(PayrollDate, GroupId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of CostDistributions)
        End Try
    End Function

    ''' <summary>
    ''' Almaceno la Distribución del Gasto
    ''' </summary>
    ''' <param name="ListCostDistributions">ListCostDistributions</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveCostDistribution(ListCostDistributions As List(Of CostDistributions), audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements ICostDistributionAdminService.SaveCostDistribution
        If ListCostDistributions Is Nothing Then
            Throw New ArgumentNullException("ListCostDistributions Vacio")
        End If
        Dim unitWork As IUnitWork = _CostDistributionRepository.UnitWork
        Try

            For Each costDistribution As CostDistributions In ListCostDistributions
                _CostDistributionRepository.SaveEntity(costDistribution)
            Next

            'Confirmo
            unitWork.Commit()

            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Elimino la Distribución del Gasto
    ''' </summary>
    ''' <param name="ListCostDistributions">ListCostDistributions</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteCostDistribution(ListCostDistributions As List(Of CostDistributions), audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements ICostDistributionAdminService.DeleteCostDistribution
        If ListCostDistributions Is Nothing Then
            Throw New ArgumentNullException("ListCostDistributions Vacio")
        End If
        Dim unitWork As IUnitWork = _CostDistributionRepository.UnitWork
        Try

            For Each costDistribution As CostDistributions In ListCostDistributions
                costDistribution.MarkAsDeleted()
                _CostDistributionRepository.SaveEntity(costDistribution)
            Next

            'Confirmo
            unitWork.Commit()

            Return True
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función que permite generar la Distribución de Gasto
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <param name="PayrollDate">Fecha Liquidación</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateCostDistribution(GroupId As Integer, PayrollDate As Date, Indigo As SessionValues) As ActionResult(Of List(Of String)) Implements ICostDistributionAdminService.GenerateCostDistribution
        Dim UnitOfWork As IUnitWork = _CostDistributionRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim ListMessage As New List(Of String)
                Dim resultStore = Me._CostDistributionRepository.SP_GenerateJournalVouchers(GroupId, PayrollDate, Indigo.IndigoPayrollIntegration, Indigo.AuditMessageWcf.CodeUser)

                If resultStore.Count = 0 Or resultStore.Any(Function(r) r.CodeMessage <> 0) Then
                    UnitOfWork.RollbackChanges()
                    transaction.Dispose()
                    ListMessage = resultStore.Where(Function(r) r.CodeMessage <> 0).Select(Function(r) r.Message).ToList()
                    Return New ActionResult(Of List(Of String)) With {.StateResult = False, .MessageResult = ListMessage}
                End If

                transaction.Complete()
                ListMessage = resultStore.Select(Function(r) r.Message).ToList()
                Return New ActionResult(Of List(Of String)) With {.StateResult = True, .MessageResult = ListMessage}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Indigo)
                Return New ActionResult(Of List(Of String)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _CostDistributionDomain.Dispose()
                _liquidationDomain.Dispose()
                _AccountingDocumentAdmin.Dispose()
            End If
            _CostDistributionRepository = Nothing
            _CostDistributionDomain = Nothing
            _liquidationRepository = Nothing
            _scheduleDetailRepository = Nothing
            _liquidationDomain = Nothing
            _InterfaceERPPayrollNet = Nothing
            _employeeRepository = Nothing
            _AccountingDocumentAdmin = Nothing
            _groupRepository = Nothing
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