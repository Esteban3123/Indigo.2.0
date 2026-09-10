'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Jose Luis Rojas
' Created          : 16-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports System.Transactions
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Base
Imports Application.Treasury
Imports Infrastructure.Data.ModelRepository
Imports Application.Accounting

Public Class ContractLiquidationAdminService
    Implements IContractLiquidationAdminService

#Region "Repositories"

    ''' <summary>
    ''' Contiene el repositorio de liquidacion de contrato
    ''' </summary>
    Private _contractLiquidationRepository As IContractLiquidationRepository

    ''' <summary>
    ''' Repositorio de empleado
    ''' </summary>
    Private _employeeRepository As IEmployeeRepository

    ''' <summary>
    ''' Repositorio de empleado
    ''' </summary>
    Private _employeeRepositoryCommit As IEmployeeRepository

    ''' <summary>
    ''' Repositorio de razones de retiro
    ''' </summary>
    Private _retirementReasonRepository As IRetirementReasonRepository

    ''' <summary>
    ''' Dominio de liquidacion de contrato
    ''' </summary>
    Private _contractLiquidationDomain As IContractLiquidationDomain

    ''' <summary>
    ''' Repositorio de Contratos
    ''' </summary>
    Private _contractRepository As IContractRepository

    Private _voucherTransactionAdmin As IVoucherTransactionAdminService

    ''' <summary>
    ''' REpositorio de Secuencias de Tesoreria
    ''' </summary>
    Private _sequenseTreasuryRepository As SequenseTreasuryCRepository

    ''' <summary>
    ''' Repositorio de Grupos
    ''' </summary>
    Private _groupRepository As IGroupRepository

    ''' <summary>
    ''' Aplicación de Documentos Contables
    ''' </summary>
    ''' <remarks></remarks>
    Private _AccountingDocumentAdmin As IAccountingDocumentAdminService

    ''' <summary>
    ''' Repositorio de Parámetros de Nómina
    ''' </summary>
    Private _payrollSettingsRepository As IPayrollSettingsRepository

    ''' <summary>
    ''' Repositorio de Conceptos Manuales
    ''' </summary>
    Private _manualConceptRepository As IManualConcepts

    ''' <summary>
    ''' Repositorio de Convenios
    ''' </summary>
    Private _agreementsCRepository As IAgreementsCRepository

    ''' <summary>
    ''' Repositorio de cabecera secuencias numericas
    ''' </summary>
    Private _secuenseCRepository As Domain.Entities.IPayrollSequenceRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository

    ''' <summary>
    ''' repositorio de los parametros de contabilidad
    ''' </summary>
    Private _settingsAccountRepository As Domain.Entities.ISettingsAccountRepository

    ''' <summary>
    ''' repositorio de los soportes de pago de nomina electronica
    ''' </summary>
    Private _electronicPayrollPaymentSupportRepository As IElectronicPayrollPaymentSupportRepository

    ''' <summary>
    ''' repositorio de nomina electronica
    ''' </summary>
    Private _electronicPayrollRepository As IElectronicPayrollRepository

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Constructor de la clase de aplicacion 
    ''' </summary>
    ''' <param name="contractLiquidationRepository">Repositorio de liquidacion de contrato inyectado por el contenedor de dependencias</param>
    Public Sub New(contractLiquidationRepository As IContractLiquidationRepository, contractLiquidationDomain As IContractLiquidationDomain, employeeRepository As IEmployeeRepository, retirementReasonRepository As IRetirementReasonRepository, employeeRepositoryCommit As IEmployeeRepository, contractRepository As IContractRepository,
                   voucherTransactionAdmin As IVoucherTransactionAdminService, sequenseTreasuryRepository As SequenseTreasuryCRepository, groupRepository As IGroupRepository, accountingDocumentAdmin As IAccountingDocumentAdminService, payrollSettingsRepository As IPayrollSettingsRepository, manualConceptRepository As IManualConcepts,
                   agreementsRepository As IAgreementsCRepository, secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository, secuenseCRepository As Domain.Entities.IPayrollSequenceRepository,
                   settingsAccountRepository As Domain.Entities.ISettingsAccountRepository, electronicPayrollPaymentSupportRepository As IElectronicPayrollPaymentSupportRepository, electronicPayrollRepository As IElectronicPayrollRepository)

        If contractLiquidationRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Liquidacion de contrato vacio")
        End If

        If employeeRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Empleado vacio")
        End If

        If retirementReasonRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Razones de retiro vacio")
        End If

        If contractLiquidationDomain Is Nothing Then
            Throw New ArgumentNullException("Dominio de liquidacion de contrato vacio")
        End If

        _contractLiquidationRepository = contractLiquidationRepository
        _contractLiquidationDomain = contractLiquidationDomain
        _employeeRepository = employeeRepository
        _retirementReasonRepository = retirementReasonRepository
        _employeeRepositoryCommit = employeeRepositoryCommit
        _contractRepository = contractRepository
        _voucherTransactionAdmin = voucherTransactionAdmin
        _sequenseTreasuryRepository = sequenseTreasuryRepository
        _AccountingDocumentAdmin = accountingDocumentAdmin
        _groupRepository = groupRepository
        _payrollSettingsRepository = payrollSettingsRepository
        _manualConceptRepository = manualConceptRepository
        _agreementsCRepository = agreementsRepository
        _secuenseCRepository = secuenseCRepository
        _secuenseDRepository = secuenseDRepository
        _settingsAccountRepository = settingsAccountRepository
        _electronicPayrollPaymentSupportRepository = electronicPayrollPaymentSupportRepository
        _electronicPayrollRepository = electronicPayrollRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que busca las nominas que se encuentran liquidadas por contrato y retorna un listado de nominas
    ''' </summary>
    ''' <param name="contractId">Id del contrato</param>
    ''' <returns>Listado de nominas</returns>
    Public Function GetPaymentsByContractId(contractId As Integer) As List(Of Liquidation) Implements IContractLiquidationAdminService.GetPaymentsByContractId
        Try
            Return _contractLiquidationRepository.GetPaymentsByContractId(contractId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Liquidation)
        End Try
    End Function


    ''' <summary>
    ''' Metodo que busca las nominas que se encuentran liquidadas por contrato base y retorna un listado de nominas
    ''' </summary>
    ''' <param name="baseContractId ">Id del contrato base</param>
    ''' <returns>Listado de nominas</returns>
    Public Function GetLiquidationsPaidByBaseContractId(baseContractId As Integer) As List(Of Liquidation) Implements IContractLiquidationAdminService.GetLiquidationsPaidByBaseContractId
        Try
            Return _contractLiquidationRepository.GetLiquidationsPaidByBaseContractId(baseContractId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Liquidation)
        End Try

    End Function

    ''' <summary>
    ''' Metodo que liquida los empleados recibidos en el parametro
    ''' </summary>
    ''' <param name="employeesToLiquidate">Diccionario que contiene el id del empleado como llave del diccionario, 
    ''' y una tupla con los valores de si confirma, la fecha de retiro, y el id del motivo de retiro</param>
    ''' <returns>El listado de mensaje de resultados asociados con la operacion</returns>
    Public Function LiquidateContracts(employeesToLiquidate As Dictionary(Of Integer, Tuple(Of Date, Integer)), ByVal session As SessionValues) As List(Of ActionMessageResult(Of ContractLiquidation)) Implements IContractLiquidationAdminService.LiquidateContracts

        'Primas, cesantias, intereses de cesantias, vacaciones, sueldo, dotacion

        Dim listLiquidationContract As New List(Of ActionMessageResult(Of ContractLiquidation))

        Try
            For Each item In employeesToLiquidate
                'id del empleado
                Dim employeeId As Integer = CInt(item.Key)
                'Confirma liquidacion
                'Fecha de Retiro
                Dim retirementDate As Date = item.Value.Item1
                'Id de la razon de retiro
                Dim retirementReasonId As Integer = item.Value.Item2
                'Empleado a liquidar
                Dim employee As Employee = _employeeRepository.GetEmployeeByIdContractLiquidation(employeeId)
                'indeminazacion
                Dim compensation As Boolean = HasCompensation(retirementReasonId)
                'Liquidacion de contrato

                Dim ObjPayrollSettings = _payrollSettingsRepository.GetSettingPayroll()

                If ObjPayrollSettings Is Nothing Then
                    listLiquidationContract.FirstOrDefault.StateResult = False
                    listLiquidationContract.FirstOrDefault.Message = "No se encontraron Parámetros de Nómina definidos"
                    Return listLiquidationContract
                End If

                If ObjPayrollSettings.LiquidateContractByFormulate Then
                    listLiquidationContract.Add(_contractLiquidationDomain.LiquidateContractByFormulate(employee, retirementDate, retirementReasonId, session))
                Else
                    listLiquidationContract.Add(_contractLiquidationDomain.LiquidateContract(employee, retirementDate, retirementReasonId, session))
                End If

            Next
            Return listLiquidationContract
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return listLiquidationContract
        End Try

    End Function


    ''' <summary>
    ''' Obtiene si la razon de retiro genera o no indemnizacion
    ''' </summary>
    ''' <param name="retirementReasonId">Id de la razon de retiro</param>
    ''' <returns>Verdadero si genera indemnizacion, Falso en caso contrario</returns>
    Private Function HasCompensation(retirementReasonId As Integer) As Boolean
        Try
            Dim result As Boolean = False
            'Motivo de retiro
            Dim retirementReason As RetirementReason = _retirementReasonRepository.GetRetirementReasonById(retirementReasonId)

            If retirementReason IsNot Nothing Then
                result = retirementReason.Compensation
            Else
                Throw New ObjectNotFoundException("No se encontró la razon de retiro")
            End If

            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Metodo para confirmar una liquidacion de contrato
    ''' </summary>
    ''' <param name="contractLiquidation">liquidacion contrato</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmLiquidationContract(contractLiquidation As ContractLiquidation, ByVal session As SessionValues) As ActionMessageResult Implements IContractLiquidationAdminService.ConfirmLiquidationContract
        If contractLiquidation Is Nothing Then
            Throw New ArgumentNullException("Liquidacion de contrato vacia")
        End If
        Dim audit = session.AuditMessageWcf
        Dim result As New ActionMessageResult()
        result.StateResult = True
        Dim unitWorkContractLiquidation As IUnitWork = _contractLiquidationRepository.UnitWork
        Dim unitWorkContract As IUnitWork = _contractRepository.UnitWork
        Dim unitWorkManualConcept As IUnitWork = _manualConceptRepository.UnitWork
        Dim unitWorkAgreements As IUnitWork = _agreementsCRepository.UnitWork
        Try
            Dim PayrollSequenseDetailId As Integer = 0
            'Se usa GetSettingAccountSimple: aquí solo se necesitan Id, IdDian y
            'HandlesElectronicPayroll (escalares). GetSettingAccount agrega includes y ~11 consultas
            'para armar descripciones, y desreferencia esos resultados sin validar nulos, por lo que
            'lanza NullReferenceException si la unidad operativa tiene una cuenta sin parametrizar.
            Dim accountingSettings = _settingsAccountRepository.GetSettingAccountSimple(session.IndigoOperatingUnitId)
            'GetSettingAccountSimple puede devolver Nothing y GetSettingAccount una entidad vacía,
            'por lo que se valida la referencia y el Id.
            If accountingSettings Is Nothing OrElse accountingSettings.Id = 0 Then
                Return New ActionMessageResult() With {.StateResult = False, .Message = "No existen parámetros de contabilidad para la unidad operativa de la sesión."}
            End If

            'Misma validación que en PayrollLiquidationAdminService: si el empleador maneja nómina
            'electrónica pero la unidad operativa de la sesión no la tiene habilitada, se aborta en
            'lugar de omitir el soporte en silencio.
            If Not accountingSettings.HandlesElectronicPayroll _
               AndAlso _settingsAccountRepository.EmployerHandlesElectronicPayroll(accountingSettings.IdDian) Then
                Return New ActionMessageResult() With {.StateResult = False,
                    .Message = "La unidad operativa de la sesión no tiene habilitada la nómina electrónica, " &
                               "pero el empleador sí la maneja. Inicie sesión con una unidad operativa " &
                               "habilitada, o solicite la parametrización, antes de liquidar el contrato."}
            End If

            If accountingSettings.HandlesElectronicPayroll Then
                Dim payrollSequense = _secuenseCRepository.GetSequenseByIdForm("2635")
                If payrollSequense.Id = 0 Then
                    Return New ActionMessageResult() With {.StateResult = False, .Message = "No existe secuencia numérica para los soportes de pago de nómina electrónica."}
                ElseIf payrollSequense.IsManual Then
                    Return New ActionMessageResult() With {.StateResult = False, .Message = "La secuencia numerica de los soportes de pago de nómina electrónica no puede ser manual."}
                End If

                If payrollSequense.Scope = "O" Then 'Si la secuencia es por organización
                    PayrollSequenseDetailId = (From x In payrollSequense.PayrollSequenceDetail Select x.Id).FirstOrDefault()
                Else 'Si la secuencia es por unidad operativa
                    If (From x In payrollSequense.PayrollSequenceDetail Where x.IdOperatingUnit = session.IndigoOperatingUnitId Select x).Count = 0 Then
                        Return New ActionMessageResult() With {.StateResult = False, .Message = "No existe la unidad operativa seleccionada en la secuencia de soportes de pago de nómina electrónica."}
                    End If
                    PayrollSequenseDetailId = (From x In payrollSequense.PayrollSequenceDetail Where x.IdOperatingUnit = session.IndigoOperatingUnitId Select x.Id).FirstOrDefault()
                End If
            End If

            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted ' Permite leer pero no modificar
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim contract = contractLiquidation.Contract
                Dim EmployeeId = contract.EmployeeId

                Dim ListcontractSaveTmp = _contractRepository.GetContractByInitialNumber(contract.InitialContractNumber, contract.EmployeeId)
                If ListcontractSaveTmp IsNot Nothing And ListcontractSaveTmp.Count > 0 Then
                    For Each contractSaveTmp As Contract In ListcontractSaveTmp
                        contractSaveTmp.RetirementReasonId = contract.RetirementReasonId
                        contractSaveTmp.RetirementDate = contractLiquidation.RetirementDate
                        contractSaveTmp.Status = 2
                        contractSaveTmp.Valid = False
                        contractSaveTmp.LastModificationDate = Date.Now
                        contractSaveTmp.ModificationUserId = audit.CodeUser
                        _contractRepository.SaveEntity(contractSaveTmp)

                    Next
                    'Consulto si ya tiene cesantías para eliminarlas de la lista
                    If contractLiquidation.Employee.UnemployedLiquidation IsNot Nothing OrElse contractLiquidation.Employee.UnemployedLiquidation.Count > 0 Then
                        While contractLiquidation.Employee.UnemployedLiquidation.Count > 0
                            Dim objectUnemployedLiquidation = contractLiquidation.Employee.UnemployedLiquidation.LastOrDefault
                            'Detalle
                            While objectUnemployedLiquidation.UnemployedLiquidationDetail.Count > 0
                                Dim ObjecRemove = objectUnemployedLiquidation.UnemployedLiquidationDetail.LastOrDefault
                                ObjecRemove.MarkAsDeleted
                            End While
                            'Concepto
                            While objectUnemployedLiquidation.UnemployedConcept.Count > 0
                                Dim ObjecRemove = objectUnemployedLiquidation.UnemployedConcept.LastOrDefault
                                ObjecRemove.MarkAsDeleted
                            End While
                            objectUnemployedLiquidation.MarkAsDeleted
                        End While
                        'Limpio el objeto de los detalles eliminados
                        contractLiquidation.Employee.ChangeTracker.ObjectsRemovedFromCollectionProperties.Clear()
                    End If
                    _contractLiquidationRepository.SaveEntity(contractLiquidation)
                End If

                'Si la Nómina está integreda, construimos el Comprobante de Egreso
                If session.IndigoPayrollIntegration = 1 Then
                    Dim ObjPayrollSettings = _payrollSettingsRepository.GetSettingPayroll()

                    If ObjPayrollSettings Is Nothing Then
                        scope.Dispose()
                        Return New ActionMessageResult() With {.StateResult = False, .Message = "No se han parametrizado los Parámetros de Nómina"}
                    End If

                    If ObjPayrollSettings.GenerateVoucherTransactionContractLiquidation = True Then

                        '' SECUENCIAS NUMÉRICAS DE COMPROBANTE DE EGRESO
                        Dim SequenseDetailTreasuryId As Integer = 0
                        'Se consulta la secuencia de pagos por el id del form
                        Dim treasurySequence As Domain.Entities.TreasurySequence = _sequenseTreasuryRepository.GetSequenseByIdForm("636")
                        If treasurySequence.Id = 0 Then
                            scope.Dispose()
                            Return New ActionMessageResult() With {.StateResult = False, .Message = "No existe secuencia numérica para el formulario de Comprobante de Egresos."}
                        End If
                        'Se valida que la secuencia numerica no sea manual
                        If treasurySequence.IsManual Then
                            scope.Dispose()
                            Return New ActionMessageResult() With {.StateResult = False, .Message = "La secuencia numerica de Comprobante de Entrada es manual."}
                        End If
                        'Se valida la secuencia numerica
                        If treasurySequence.Scope = "O" Then 'Si la secuencia es por organización
                            SequenseDetailTreasuryId = (From x In treasurySequence.TreasurySequenceDetail Select x.Id).FirstOrDefault()
                        ElseIf treasurySequence.Scope = "OU" Then 'Si la secuencia es por unidad operativa
                            'Se valida que la unidad operativa seleccionada este en la secuencia
                            If (From x In treasurySequence.TreasurySequenceDetail Where x.IdOperatingUnit = session.IndigoOperatingUnitId Select x).Count = 0 Then
                                scope.Dispose()
                                Return New ActionMessageResult() With {.StateResult = False, .Message = "No existe la unidad operativa seleccionada en la secuencia de Comprobantes de Egresos."}
                            End If
                            SequenseDetailTreasuryId = (From x In treasurySequence.TreasurySequenceDetail Where x.IdOperatingUnit = session.IndigoOperatingUnitId Select x.Id).FirstOrDefault()
                        ElseIf treasurySequence.Scope = "CC" Then 'Si la secuencia es por tipo de comprobante
                            'Se valida que exista una secuencia para el tipo de pago
                            If (From x In treasurySequence.TreasurySequenceDetail Where x.Type = 1 Select x).Count = 0 Then
                                scope.Dispose()
                                Return New ActionMessageResult() With {.StateResult = False, .Message = "No existe la secuencia de Comprobantes de Egresos para el tipo Pago."}
                            End If
                            SequenseDetailTreasuryId = (From x In treasurySequence.TreasurySequenceDetail Where x.Type = 1 Select x.Id).FirstOrDefault()
                        End If

                        Dim ObjVoucherTransaction = _contractLiquidationDomain.CreateVoucherTransaction(contractLiquidation, session)

                        If ObjVoucherTransaction.StateResult = True Then
                            Dim ObjResultVoucherTransaction = _voucherTransactionAdmin.SaveVoucherTransaction(ObjVoucherTransaction.ObjectEmbbeded, session.AuditMessageWcf, 0, SequenseDetailTreasuryId, treasurySequence, 0)
                            If ObjResultVoucherTransaction.StateResult = False Then
                                scope.Dispose()
                                Return New ActionMessageResult() With {.StateResult = False, .Message = ObjResultVoucherTransaction.Message}
                            End If
                            Dim Message As String = "Se ha generado el Comprobante de Egreso # " + ObjResultVoucherTransaction.ObjectEmbbeded.Code
                            result.Message = Message
                        Else
                            Return New ActionMessageResult() With {.StateResult = False, .Message = ObjVoucherTransaction.Message}
                        End If
                    End If


                    Dim Employee = contractLiquidation.Employee
                    Dim tmpObjGroup = _groupRepository.GetGroupById(contract.GroupId)

                    Dim ObjJournalVoucherContractLiquidation = _contractLiquidationDomain.CreateJournalVoucher(contractLiquidation.ContractLiquidationDetail.ToList, Employee, contract, tmpObjGroup, session.AuditMessageWcf)

                    If ObjJournalVoucherContractLiquidation.StateResult = True Then

                        For Each objJournalVoucher As Domain.Entities.JournalVouchers In ObjJournalVoucherContractLiquidation.ObjectEmbbeded
                            Dim ObjResultJournalVoucher = _AccountingDocumentAdmin.SaveAccountingDocument(objJournalVoucher, session.AuditMessageWcf, True)

                            If ObjResultJournalVoucher.StateResult = False Then
                                scope.Dispose()
                                Return New ActionMessageResult() With {.StateResult = False, .Message = ObjResultJournalVoucher.Message}
                            End If
                            If ObjResultJournalVoucher.ObjectEmbbeded.Consecutive = 0 Then
                                result.Message = ObjResultJournalVoucher.Message
                            Else
                                result.Message = result.Message + " Se ha generado el Comprobante Contable # " + ObjResultJournalVoucher.ObjectEmbbeded.Consecutive.ToString() + " - " + ObjResultJournalVoucher.ObjectEmbbeded.Detail.ToString()
                            End If
                        Next
                    Else
                        scope.Dispose()
                        Return New ActionMessageResult() With {.StateResult = False, .Message = ObjJournalVoucherContractLiquidation.Message}
                    End If
                End If

                '' Inactivo Conceptos Manuales, Convenios y Embargos
                Dim listManualConcepts = _manualConceptRepository.GetManualConceptsByLiquidationContract(EmployeeId, 1)

                If listManualConcepts IsNot Nothing AndAlso listManualConcepts.Count > 0 Then
                    For Each objManualConcepts As ManualConcepts In listManualConcepts
                        objManualConcepts.State = 2
                        objManualConcepts.MarkAsModified()

                        Dim InitialDate As New Date(contractLiquidation.RetirementDate.Year)

                        If objManualConcepts.ManualConceptsDetail IsNot Nothing AndAlso objManualConcepts.ManualConceptsDetail.Count > 0 Then
                            If objManualConcepts.ManualConceptsDetail.Any(Function(x) x.PayrollDateLiquidated.Month = contractLiquidation.RetirementDate.Month And x.PayrollDateLiquidated.Year = contractLiquidation.RetirementDate.Year) Then
                                For Each objManualConceptDetail As ManualConceptsDetail In objManualConcepts.ManualConceptsDetail.Where(Function(x) x.PayrollDateLiquidated.Month = contractLiquidation.RetirementDate.Month And x.PayrollDateLiquidated.Year = contractLiquidation.RetirementDate.Year).ToList()
                                    objManualConceptDetail.State = 2
                                    objManualConceptDetail.PayrollDateLiquidated = contract.RetirementDate
                                    objManualConceptDetail.MarkAsModified()
                                Next

                            Else
                                'Insertamos los Detalles de Conceptos Manuales
                                Dim objManualConceptDetail As New ManualConceptsDetail
                                objManualConceptDetail.PayrollDateLiquidated = contractLiquidation.RetirementDate
                                objManualConceptDetail.Value = objManualConcepts.QuoteValue
                                objManualConceptDetail.State = 2

                                objManualConcepts.ManualConceptsDetail.Add(objManualConceptDetail)
                            End If
                        End If

                        _manualConceptRepository.SaveEntity(objManualConcepts)
                    Next

                End If

                Dim listAgreements = _agreementsCRepository.GetAgreementsByEmployeeLiquidationContract(EmployeeId, 2)

                If listAgreements IsNot Nothing AndAlso listAgreements.Count > 0 Then
                    For Each objAgreements As AgreementsC In listAgreements
                        Dim ValueAgreements As Decimal = 0

                        If objAgreements.NumberShares = 0 Then
                            ValueAgreements = objAgreements.AgreementValue
                        Else
                            ValueAgreements = objAgreements.AgreementValue / objAgreements.NumberShares
                        End If

                        objAgreements.State = "4"
                        objAgreements.MarkAsModified()

                        'Insertamos los Detalles de Conceptos Manuales
                        Dim objAgreementsD As New AgreementsD
                        objAgreementsD.ShareValuePaid = ValueAgreements
                        objAgreementsD.DatePayment = contractLiquidation.RetirementDate
                        objAgreementsD.TypePayment = 5
                        objAgreementsD.StateShare = "Pago Registrado por Liquidación de Contrato"

                        objAgreements.AgreementsD.Add(objAgreementsD)

                        _agreementsCRepository.SaveEntity(objAgreements)
                    Next
                End If

                unitWorkAgreements.CommitAndRefreshChanges()
                unitWorkManualConcept.CommitAndRefreshChanges()
                'unitWorkContract commitea de último: el grafo de ContractLiquidation alcanza Payroll.Contract
                'por .Contract y por .Employee.Contract, y su commit sobrescribe Status y Valid.
                unitWorkContractLiquidation.CommitAndRefreshChanges()
                unitWorkContract.CommitAndRefreshChanges()

                If accountingSettings.HandlesElectronicPayroll Then
                    Dim support = _electronicPayrollPaymentSupportRepository.GetElectronicPayrollPaymentSupportByThirdPartyAndPeriod(contractLiquidation.Employee.ThirdPartyId, contractLiquidation.RetirementDate.Year, contractLiquidation.RetirementDate.Month)
                    If support Is Nothing Then
                        support = New ElectronicPayrollPaymentSupport With {
                                .EmployeePartyId = contractLiquidation.Employee.ThirdPartyId,
                                .Year = contractLiquidation.RetirementDate.Year,
                                .Month = contractLiquidation.RetirementDate.Month
                            }

                        Dim seq = Me._secuenseDRepository.GetSequenseDById(PayrollSequenseDetailId)
                        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                            support.Prefix = seq.Sequense.Pattern.Replace("#", "")
                            support.Consecutive = seq.Next

                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                            Me._secuenseDRepository.UnitWork.Commit()
                        Else
                            Return New ActionMessageResult() With {.StateResult = False, .Message = "_Seq01_"}
                        End If

                        _electronicPayrollPaymentSupportRepository.SaveEntity(support)
                        _electronicPayrollPaymentSupportRepository.UnitWork.Commit()

                        Dim electronicPayroll = New ElectronicPayroll With {
                                .DocumentType = 1,
                                .Year = support.Year,
                                .Month = support.Month,
                                .EmployeePartyId = support.EmployeePartyId,
                                .EntityName = support.GetType().Name,
                                .EntityId = support.Id,
                                .Prefix = support.Prefix,
                                .DocumentNumber = support.Consecutive,
                                .CUNE = support.CUNE,
                                .Status = 1,
                                .CreationDate = DateTime.Now
                            }

                        electronicPayroll.FilePath = System.IO.Path.Combine(
                                Utils.GetPathElectronicDocuments(),
                                session.TransactionalContainer,
                                electronicPayroll.Year,
                                electronicPayroll.Month,
                                electronicPayroll.getDocumentTypeName(),
                                electronicPayroll.Prefix,
                                electronicPayroll.DocumentNumber
                            )

                        _electronicPayrollRepository.SaveEntity(electronicPayroll)
                        _electronicPayrollRepository.UnitWork.Commit()
                    End If

                    support.ElectronicPayrollPaymentSupportDetail.Add(New ElectronicPayrollPaymentSupportDetail With {
                            .EntityId = contractLiquidation.Id,
                            .EntityName = contractLiquidation.GetType().Name
                        })

                    _electronicPayrollPaymentSupportRepository.SaveEntity(support)
                    _electronicPayrollPaymentSupportRepository.UnitWork.Commit()
                End If

                scope.Complete()
            End Using
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(contractLiquidation.GetType.Name, audit.Functional, contractLiquidation.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of ContractLiquidation)(contractLiquidation, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
            auditObject.Execute()
            Return result
        Catch ex As Exception
            unitWorkContract.RollbackChanges()
            unitWorkContractLiquidation.RollbackChanges()
            unitWorkManualConcept.RollbackChanges()
            unitWorkAgreements.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            If ex.InnerException Is Nothing Then
                result.Message = "Ha ocurrido un Error y no se ha podido confirmar " + ex.InnerException?.InnerException?.Message?.ToString
            Else
                result.Message = "Ha ocurrido un Error y no se ha podido confirmar " + ex.InnerException.ToString
            End If
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Metodo para confirmar una lista de liquidacion de contratos
    ''' </summary>
    ''' <param name="contractLiquidation">Lista de liquidacion contratos</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmListLiquidationContract(ListContractLiquidation As List(Of ContractLiquidation), audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult Implements IContractLiquidationAdminService.ConfirmListLiquidationContract
        If ListContractLiquidation Is Nothing Then
            Throw New ArgumentNullException("Lista de Liquidacion de contrato vacia")
        End If
        Dim result As New ActionMessageResult()
        result.StateResult = True
        Dim unitWorkContractLiquidation As IUnitWork = _contractLiquidationRepository.UnitWork
        Dim unitWorkContract As IUnitWork = _contractRepository.UnitWork
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted ' Permite leer pero no modificar
            'Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
            For Each itemContractLiquidation In ListContractLiquidation
                Dim contract = itemContractLiquidation.Contract
                itemContractLiquidation.Employee = Nothing
                itemContractLiquidation.Contract = Nothing
                itemContractLiquidation.RetirementReason = Nothing
                Dim contractSaveTmp = _contractRepository.GetContractById(contract.Id)
                contractSaveTmp.RetirementReasonId = contract.RetirementReasonId
                contractSaveTmp.RetirementDate = contract.RetirementDate
                contractSaveTmp.Status = 2
                contractSaveTmp.Valid = False
                contractSaveTmp.LastModificationDate = Date.Now
                contractSaveTmp.ModificationUserId = audit.CodeUser
                _contractRepository.SaveEntity(contractSaveTmp)
                _contractLiquidationRepository.SaveEntity(itemContractLiquidation)
                unitWorkContract.CommitAndRefreshChanges()
                unitWorkContractLiquidation.CommitAndRefreshChanges()
            Next
            unitWorkContract.CommitAndRefreshChanges()
            unitWorkContractLiquidation.CommitAndRefreshChanges()
            'scope.Complete()
            'End Using
            For Each itemContractLiquidation In ListContractLiquidation
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(itemContractLiquidation.GetType.Name, audit.Functional, itemContractLiquidation.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of ContractLiquidation)(itemContractLiquidation, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            Next
            Return result
        Catch ex As Exception
            unitWorkContract.RollbackChanges()
            unitWorkContractLiquidation.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Función que obtiene una Lista de Liquidaciones por Mes y Año
    ''' </summary>
    ''' <param name="Month">Mes</param>
    ''' <param name="Year">Año</param>
    ''' <returns>Lista de Liquidaciones de Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractLiquidationByMonthAndYear(Month As Integer, Year As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As List(Of ContractLiquidation) Implements IContractLiquidationAdminService.GetContractLiquidationByMonthAndYear
        Try
            Return _contractLiquidationRepository.GetContractLiquidationByMonthAndYear(Month, Year)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ContractLiquidation)
        End Try

    End Function

    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación de contratos con conceptos dinámicos
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="session">Valores de sesión</param>
    ''' <returns>DataTable con el reporte de liquidación de contratos detallado</returns>
    Public Function GetContractLiquidationDetailReport(initialDate As Date, endDate As Date, Optional employeeId As Integer? = Nothing, Optional session As SessionValues = Nothing) As System.Data.DataTable Implements IContractLiquidationAdminService.GetContractLiquidationDetailReport
        Try
            Return _contractLiquidationRepository.GetContractLiquidationDetailReport(initialDate, endDate, employeeId, session)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New System.Data.DataTable()
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _contractLiquidationDomain.Dispose()
            End If
            _contractLiquidationRepository = Nothing
            _contractLiquidationDomain = Nothing
            _employeeRepository = Nothing
            _retirementReasonRepository = Nothing
            _employeeRepositoryCommit = Nothing
            _contractRepository = Nothing
            _secuenseCRepository = Nothing
            _secuenseDRepository = Nothing
            _settingsAccountRepository = Nothing
            _electronicPayrollPaymentSupportRepository = Nothing
            _electronicPayrollRepository = Nothing
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
