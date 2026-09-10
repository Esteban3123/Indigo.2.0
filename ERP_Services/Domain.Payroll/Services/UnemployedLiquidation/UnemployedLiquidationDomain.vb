'***********************************************************************
' Assembly         : Domain.Payroll.Entities
' Author           : Kevin Garay Rodriguez
' Created          : 11-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Common.Entities
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base.Utils
Imports Infrastructure.CrossCutting.Base

#End Region
''' <summary>
''' Clase que contiene la lógica aplicada en el frontal de la liquidación de cesantías
''' </summary>
''' <remarks></remarks>
Public Class UnemployedLiquidationDomain
    Implements IUnemployedLiquidationDomain


#Region "Repositories"

    ''' <summary>
    ''' Repositorio de liquidacion de cesantías
    ''' </summary>
    ''' <remarks></remarks>
    Private _unemployedLiquidationRepository As IUnemployedLiquidationRepository

    ''' <summary>
    ''' Repositorio de liquidacion de cesantías para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private _unemployedLiquidationRepositoryToSave As IUnemployedLiquidationRepository

    ''' <summary>
    ''' Repositorio del detalle de liquidacion de cesantias 
    ''' </summary>
    ''' <remarks></remarks>
    Private _unemployedLiquidationDetailRepository As IUnemployedLiquidationDetailRepository

    ''' <summary>
    ''' Repositorio de liquidacion de nomina
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Dominio de la liquidacion de nomina
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationDomain As ILiquidationDomain

    ''' <summary>
    ''' Repositorio de empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private _employeeRepository As IEmployeeRepository

    ''' <summary>
    ''' Repositorio de Conceptos Manuales
    ''' </summary>
    ''' <remarks></remarks>
    Private _ManualConceptsRepository As IManualConcepts

    ''' <summary>
    ''' Repositorio de Novedades
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyRepository As INoveltyRepository

    ''' <summary>
    ''' Repositorio de Parámetros de Nómina
    ''' </summary>
    Private _payrollSettings As IPayrollSettingsRepository

    ''' <summary>
    ''' Repositorio de Conceptos
    ''' </summary>
    Private _conceptRepository As IConceptRepository


#End Region

#Region "Constructor"
    Public Sub New(unemployedLiquidationRepository As IUnemployedLiquidationRepository, unemployedLiquidationRepositoryToSave As IUnemployedLiquidationRepository, unemployedLiquidationDetailRepository As IUnemployedLiquidationDetailRepository, liquidationRepository As IPayrollLiquidationRepository,
                   employeeRepository As IEmployeeRepository, liquidationDomain As ILiquidationDomain, ManualConceptsRepository As IManualConcepts,
                   noveltyRepository As INoveltyRepository, PayrollSettings As IPayrollSettingsRepository, conceptRepository As IConceptRepository)

        If unemployedLiquidationRepository Is Nothing Then
            Throw New ArgumentNullException("unemployedLiquidationRepository Vacío")
        End If

        If unemployedLiquidationRepositoryToSave Is Nothing Then
            Throw New ArgumentNullException("unemployedLiquidationRepositoryDelete Vacío")
        End If

        If liquidationRepository Is Nothing Then
            Throw New ArgumentNullException("liquidationRepository Vacío")
        End If

        If unemployedLiquidationDetailRepository Is Nothing Then
            Throw New ArgumentNullException("unemployedLiquidationDetailRepository Vacío")
        End If

        If employeeRepository Is Nothing Then
            Throw New ArgumentNullException("employeeRepository Vacío")
        End If


        _ManualConceptsRepository = ManualConceptsRepository
        _unemployedLiquidationRepository = unemployedLiquidationRepository
        _unemployedLiquidationRepositoryToSave = unemployedLiquidationRepositoryToSave
        _unemployedLiquidationDetailRepository = unemployedLiquidationDetailRepository
        _liquidationRepository = liquidationRepository
        _employeeRepository = employeeRepository
        _liquidationDomain = liquidationDomain
        _noveltyRepository = noveltyRepository
        _payrollSettings = PayrollSettings
        _conceptRepository = conceptRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que contiene toda la lógica de la liquidación de cesantías anuales
    ''' </summary>
    ''' <param name="listEmployees">listado de empleados a liquidar</param>
    ''' <param name="InitialDate">Fecha inicio de cesantías</param>
    ''' <param name="EndingDate">Fecha Fin de cesantías</param>
    ''' <param name="confirm">si va confirmada o no la liquidación</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function YearlyLiquidation(listEmployees As List(Of Employee), InitialDate As Date, EndingDate As Date, confirm As Boolean, LiquidationType As Boolean, Sanction As Boolean, Optional AuthorizationDate As Date = Nothing, Optional UnemployedRetirementReason As String = "", Optional ResolutionNumber As String = "", Optional XtraLiquidation As Liquidation = Nothing, Optional liquidationContract As Boolean = False, Optional ByRef ReplaceFormulate As String = "", Optional ByRef ConceptFormulate As String = "", Optional ByVal BasePrimasCesantias As Decimal = 0, Optional ByVal UnemployedInterestPaidWithPayroll As Boolean = 0) As ActionMessageResult(Of List(Of UnemployedLiquidation)) Implements IUnemployedLiquidationDomain.YearlyLiquidation
        ' Dim ListUnemployedLiquidationResult As List(Of ActionMessageResult(Of UnemployedLiquidation)) = New List(Of ActionMessageResult(Of UnemployedLiquidation))
        Dim ActionMessageResult As New ActionMessageResult(Of List(Of UnemployedLiquidation))
        ActionMessageResult.StateResult = True

        If liquidationContract = False Then
            If Month(Date.Now) = 1 Or Month(Date.Now) = 2 Then
                ActionMessageResult.MessageResult.Add(New MessageResult("004: Mes", "Se utilizarán los datos de Auxilio de Transporte y Salario Mínimo del Año anterior que se parametrizan en Parámetros de Nómina"))
            End If
        End If


        Try
            Dim ListUnemployement As New List(Of UnemployedLiquidation)
            InitialDate = New Date(InitialDate.Year, InitialDate.Month, InitialDate.Day)
            EndingDate = New Date(EndingDate.Year, EndingDate.Month, EndingDate.Day)

            If EndingDate > Date.Now AndAlso liquidationContract = False Then
                EndingDate = Date.Now
            End If


            Dim ListUnemployedLiquidationToDelete As List(Of UnemployedLiquidation) = New List(Of UnemployedLiquidation)

            Dim ObjPayrollSettings = _payrollSettings.GetSettingPayroll()

            If ObjPayrollSettings Is Nothing Then
                ActionMessageResult.MessageResult.Add(New MessageResult("999: Parámetros de Nómina", "No se encontraron Parámetros de Nómina Definidos"))
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            End If

            If ObjPayrollSettings.UnemploymentTypeCalculated Is Nothing Then
                ActionMessageResult.MessageResult.Add(New MessageResult("999: Parámetros de Nómina", "No ha parametrizado el tipo de Cálculo de Cesantias"))
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            End If

            If ObjPayrollSettings.UnemploymentTypeCalculated = 2 Then
                ActionMessageResult.MessageResult.Add(New MessageResult("005: Tipo Liquidación", "El valor de las Cesantía será calculado por Suma de Provisiones"))
            Else
                ActionMessageResult.MessageResult.Add(New MessageResult("005: Tipo Liquidación", "El valor de las Cesantias será calculado por Fórmula Interna"))
            End If

            If ObjPayrollSettings.MinimunLegalSalaryLastYear Is Nothing Or ObjPayrollSettings.MinimunLegalSalaryLastYear = 0 Then
                ActionMessageResult.MessageResult.Add(New MessageResult("999: Parámetros de Nómina", "Debe parametrizar el Valor del Salario Mínimo del Año Anterior"))
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            End If

            If ObjPayrollSettings.TransportHealthValueLastYear Is Nothing Or ObjPayrollSettings.TransportHealthValueLastYear = 0 Then
                ActionMessageResult.MessageResult.Add(New MessageResult("999: Parámetros de Nómina", "Debe parametrizar el Valor del Auxilio de Transporte del Año Anterior"))
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            End If



            If ObjPayrollSettings.IdConceptInterestUnemployment Is Nothing Then
                ActionMessageResult.MessageResult.Add(New MessageResult("999: Parámetros de Nómina", "No ha parametrizado el Concepto de Intereses de Cesantias"))
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            Else
                Dim concept = _conceptRepository.GetConceptId(ObjPayrollSettings.IdConceptInterestUnemployment)
                If concept.ConceptType = 3 Then
                    ActionMessageResult.MessageResult.Add(New MessageResult("999: Parámetros de Nómina", String.Format("El tipo del concepto {0}  - {1}, no debe ser patronal", concept.Code, concept.Name)))
                    ActionMessageResult.StateResult = False
                    Return ActionMessageResult
                End If
            End If

            If ObjPayrollSettings.IdConceptUnemployment Is Nothing Then
                ActionMessageResult.MessageResult.Add(New MessageResult("999: Parámetros de Nómina", "No ha parametrizado el Concepto de Cesantias"))
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            Else
                Dim concept = _conceptRepository.GetConceptId(ObjPayrollSettings.IdConceptUnemployment)
                If concept.ConceptType = 3 Then
                    ActionMessageResult.MessageResult.Add(New MessageResult("999: Parámetros de Nómina", String.Format("El tipo del concepto {0}  - {1}, no debe ser patronal", concept.Code, concept.Name)))
                    ActionMessageResult.StateResult = False
                    Return ActionMessageResult
                End If
            End If
            For Each _employee As Employee In listEmployees.ToList() ' Se recorre lista de empleados a los que se les va a liquidar las cesantías

                Dim _contract As Contract = Nothing

                Dim EmployeeListContract As List(Of Contract)

                If liquidationContract = True Then
                    _contract = _employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault()
                Else
                    EmployeeListContract = _employee.Contract.Where(Function(x) x.ContractEndingDate >= InitialDate And x.ContractInitialDate <= EndingDate And x.Status <> 2).ToList()
                    _contract = EmployeeListContract.OrderByDescending(Function(x) x.ContractEndingDate).FirstOrDefault()
                End If


                Dim _fundContract As FundContract = Nothing

                If _contract Is Nothing Then ' si no tiene contrato
                    ActionMessageResult.MessageResult.Add(New MessageResult("001: Empleado", "El empleado " + _employee.ThirdParty.Name + " No tiene Contrato vigente"))
                    ActionMessageResult.StateResult = False
                    Continue For
                End If

                _fundContract = _contract.FundContract.Where(Function(x) x.FundType = 3).FirstOrDefault()
                If _fundContract Is Nothing And liquidationContract = True Then
                    _fundContract = New FundContract()
                End If
                ''''''''''''''''''''Valido que tenga contrato y que sea de tipo laboral, ademas que tenga fondo de cesantías
                If _fundContract Is Nothing Then ' si no tiene fondo de cesantías
                    ActionMessageResult.MessageResult.Add(New MessageResult("001: Empleado", "El empleado " + _employee.ThirdParty.Name + " No tiene Fondo de Cesantias"))
                    ActionMessageResult.StateResult = False
                ElseIf Not {2, 3, 4, 5, 6}.Contains(_contract.ContractType.ContractClass) Then
                    ActionMessageResult.MessageResult.Add(New MessageResult("001: Empleado", "El empleado " + _employee.ThirdParty.Name + " No tiene Contrato de Tipo Laboral"))
                Else
                    Dim EndingDateForExisting As Date = New Date(InitialDate.Year, 12, 31) 'Variable para almacenar la fecha fin para buscar cesantia existentes
                    If EndingDate > EndingDateForExisting Then 'si la fecha fin que llego al metodo, es mayor a el 31 de diciembr del año a liquidar, se toma la fecha que llego al metodo
                        EndingDateForExisting = EndingDate
                    End If
                    Dim UnemployedExisting = GetUnemployedLiquidationEmployee(_employee.Id, InitialDate, EndingDateForExisting) 'obtengo las liquidaciones de cesatias existentes en este periodo


                    Dim UnemployedLiquidatedValue = 0
                    If UnemployedExisting.Count > 0 Then
                        Dim UnemployedExistingL = UnemployedExisting.Where(Function(x) x.ConfirmLiquidated = True AndAlso x.UnemployedLiquidationType = True).ToList()
                        If UnemployedExistingL IsNot Nothing Then
                            UnemployedLiquidatedValue = UnemployedExistingL.Sum(Function(x) x.TotalUnemployed)
                        End If
                    End If


                    Dim TemporalDateInitial As New Date(InitialDate.Year, InitialDate.Month, InitialDate.Day)
                    If UnemployedExisting.Count > 0 Then 'si ya existe liquidacion de cesantías para ese día
                        If UnemployedExisting.Count = 1 Then
                            If UnemployedExisting.Item(0).Status = True OrElse UnemployedExisting.Item(0).ConfirmLiquidated = True Then
                                Dim _dateinitial As Date = UnemployedExisting.Item(0).UnemployedEndingDate
                                If UnemployedExisting.Item(0).UnemployedLiquidationType = False Then
                                    ActionMessageResult.MessageResult.Add(New MessageResult("002: Cesantias", "El empleado " + _employee.ThirdParty.Name + " Ya tiene Liquidaciones de Cesantias Anuales liquidadas")) 'ya tiene liquidacipon anual en este periodo
                                Else
                                    ActionMessageResult.MessageResult.Add(New MessageResult("002: Cesantias", "El empleado " + _employee.ThirdParty.Name + " Ya tiene Liquidaciones de Cesantias Parciales liquidadas, de " + UnemployedExisting.Item(0).UnemployedInitialDate + " a " + UnemployedExisting.Item(0).UnemployedEndingDate)) 'tiene liquidado desde "fecha" hasta "fecha"
                                End If
                                TemporalDateInitial = _dateinitial.AddDays(1)
                            Else
                                ListUnemployedLiquidationToDelete.Add(UnemployedExisting.Item(0))
                            End If
                        Else
                            For Each item In UnemployedExisting 'por cada item que encuentre de liquidacion anterior
                                Dim _dateinitial As Date = item.UnemployedEndingDate
                                If item.Status = True OrElse item.ConfirmLiquidated = True Then ' si esta confirmada, envie mensaje
                                    ActionMessageResult.MessageResult.Add(New MessageResult("002: Cesantias", "El empleado " + _employee.ThirdParty.Name + " Ya tiene Liquidaciones de Cesantias Anuales liquidadas")) 'ya tiene liquidacipon anual en este periodo
                                    TemporalDateInitial = _dateinitial.AddDays(1)
                                Else 'si no estaba liquidada, eliminarla y volver a registrarla
                                    ListUnemployedLiquidationToDelete.Add(item)
                                End If
                            Next
                        End If
                    End If
                    If EndingDate > TemporalDateInitial Or XtraLiquidation IsNot Nothing Then 'AndAlso TemporalDateInitial.Month <> EndingDate.Month
                        Dim newUneLiq As UnemployedLiquidation = New UnemployedLiquidation
                        'de las fechas que se mando a liquidar no queda nada por procesar
                        Dim InitialContractNumber As Integer = 0

                        If liquidationContract = False Then
                            InitialContractNumber = _contract.InitialContractNumber
                        End If

                        Dim UnemployedLiquidationDetail = CalculateMonthsWorked(_employee.Id, TemporalDateInitial, EndingDate, InitialContractNumber) 'calculo los detalles de liquidacion de cesantias por mes

                        If UnemployedLiquidationDetail.Count <= 0 AndAlso XtraLiquidation Is Nothing Then ' si no tiene nominas liquidadas
                            ActionMessageResult.MessageResult.Add(New MessageResult("003: Nomina", "El empleado " + _employee.ThirdParty.Name + " No tiene Nóminias Liquidadas"))
                        Else 'se puede liquidar cesantía
                            With newUneLiq ' crear registro de cesantía
                                If UnemployedLiquidationDetail.Count > 0 Then
                                    For Each itemDetail In UnemployedLiquidationDetail
                                        .UnemployedLiquidationDetail.Add(itemDetail)
                                    Next
                                End If
                                If XtraLiquidation IsNot Nothing Then
                                    If liquidationContract = True Then
                                        If UnemployedLiquidationDetail.Any(Function(x) x.Month = XtraLiquidation.PayrollDateLiquidated.Month AndAlso x.Year = XtraLiquidation.PayrollDateLiquidated.Year) = False Then
                                            If EndingDate <= XtraLiquidation.PayrollDateLiquidated Then
                                                .UnemployedLiquidationDetail.Add(ConstructEntityUnemployed(XtraLiquidation))
                                            ElseIf EndingDate > XtraLiquidation.PayrollDateLiquidated Then
                                                .UnemployedLiquidationDetail.Add(ConstructEntityUnemployed(XtraLiquidation))
                                            End If
                                        End If
                                    Else
                                        .UnemployedLiquidationDetail.Add(ConstructEntityUnemployed(XtraLiquidation))
                                    End If

                                End If
                                XtraLiquidation = Nothing
                                .ContractId = _contract.Id
                                .EmployeeId = _employee.Id
                                .Employee = _employee
                                .FundContractId = _fundContract.Id

                                .ConfirmLiquidated = confirm
                                .Status = True
                                .UnemployedLiquidationType = LiquidationType

                                If LiquidationType = True Then ' si es liquidacion parcial
                                    .UnemployedRetirementReason = UnemployedRetirementReason
                                    .AuthorizationDate = AuthorizationDate
                                    .ResolutionNumber = ResolutionNumber
                                End If
                                If .ConfirmLiquidated = True Then 'Fecha de confirmacion
                                    .ConfirmationDate = DateTime.Now
                                    .UnemployedInterestPayDate = _contract.Group.NextDateLiquidation
                                End If

                                '''''''''fechas del tiempo que se liquida
                                '''
                                Dim TmpInitialDate As Date
                                If TemporalDateInitial > _contract.JobBondingDate Then
                                    TmpInitialDate = TemporalDateInitial
                                Else
                                    TmpInitialDate = _contract.JobBondingDate
                                End If

                                .UnemployedInitialDate = TmpInitialDate

                                .UnemployedEndingDate = EndingDate


                                If _contract.JobBondingDate > InitialDate Then
                                    .UnemployedInitialDate = _contract.JobBondingDate
                                ElseIf .UnemployedLiquidationType = True Then
                                    .UnemployedInitialDate = New Date(.UnemployedLiquidationDetail.Item(0).Year, .UnemployedLiquidationDetail.Item(0).Month, 1)
                                End If
                                If _contract.ContractEndingDate < EndingDate Then
                                    .UnemployedEndingDate = _contract.ContractEndingDate
                                ElseIf .UnemployedLiquidationType = True Then
                                    'agarrar la fecha del ultimo mes liquidado, en caso de que sea liquidacion parcial
                                    .UnemployedEndingDate = New Date(.UnemployedLiquidationDetail.Item(.UnemployedLiquidationDetail.Count - 1).Year, .UnemployedLiquidationDetail.Item(.UnemployedLiquidationDetail.Count - 1).Month, DateTime.DaysInMonth(.UnemployedEndingDate.Value.Year, .UnemployedLiquidationDetail.Item(.UnemployedLiquidationDetail.Count - 1).Month))
                                End If
                                .UnemployedInterestPaidWithPayroll = UnemployedInterestPaidWithPayroll

                                .UnemployedLiquidationDate = DateTime.Now

                                '*****************calculos**************'
                                Dim _ibcaverage As Double = 0
                                Dim _ibcaveragenotsanction As Double = 0
                                Dim _workedTotalDays As Integer = 0
                                Dim _unemployedAverage As Decimal = 0
                                Dim _unemployedInterestAverage As Decimal = 0
                                Dim _sanctionTotalDays As Integer = 0
                                Dim TotalDeductionsDays As Integer = 0
                                Dim _febAddDays As Integer = 0 'variable que se utiliza para almacenar dias que faltan para que febrero tenga 30 dias, y poder realizar el calculo

                                Dim CountLiquidation As Integer = 0
                                For Each itemMonth As UnemployedLiquidationDetail In .UnemployedLiquidationDetail

                                    _ibcaverage = _ibcaverage + itemMonth.ValIBC
                                    _ibcaveragenotsanction = _ibcaveragenotsanction + itemMonth.ValIBCNotSanction
                                    _workedTotalDays = _workedTotalDays + itemMonth.WorkedDays
                                    _unemployedAverage = _unemployedAverage + itemMonth.UnemployedAverage
                                    _unemployedInterestAverage = _unemployedInterestAverage + itemMonth.UnemployedInterestAverage

                                    CountLiquidation = CountLiquidation + 1
                                Next


                                'Averiguo las Licencias No Remuneradas 
                                Dim UnpaidLicensesNovelty = _noveltyRepository.GetNoveltyUnpaidLicenses(_employee.Id)
                                Dim VarUnpaidLicensesDays As Integer = 0
                                If UnpaidLicensesNovelty IsNot Nothing And UnpaidLicensesNovelty.Any(Function(n) n.GroupId = _contract.Group.Id) Then
                                    If UnpaidLicensesNovelty.Any(Function(x) x.EndDate <= EndingDate And x.RealDate >= InitialDate) Then
                                        VarUnpaidLicensesDays = UnpaidLicensesNovelty.Where(Function(n) n.GroupId = _contract.Group.Id And n.EndDate <= EndingDate And n.RealDate >= InitialDate).Sum(Function(x) x.Days)
                                    End If
                                End If
                                'Se averigua los dias de sanción que tenga el empleado
                                Dim objSanctionNovelty = _noveltyRepository.GetNoveltySanctions(_employee.Id)
                                If objSanctionNovelty IsNot Nothing And objSanctionNovelty.Any(Function(n) n.GroupId = _contract.Group.Id) Then
                                    If objSanctionNovelty.Any(Function(x) x.EndDate <= EndingDate And x.RealDate >= InitialDate) Then
                                        _sanctionTotalDays = objSanctionNovelty.Where(Function(n) n.GroupId = _contract.Group.Id And n.EndDate <= EndingDate And n.RealDate >= InitialDate).Sum(Function(x) x.Days)
                                    End If
                                End If

                                TotalDeductionsDays = _sanctionTotalDays + VarUnpaidLicensesDays

                                If TotalDeductionsDays > 360 Then
                                    TotalDeductionsDays = 360
                                End If

                                Dim IBCAverage As Double = 0 ' promedio trabajado con cesantias con descuentos de sanciones
                                Dim IBCAverageNotSanctions As Double = _ibcaveragenotsanction / .UnemployedLiquidationDetail.Count ' promedio trabajado con cesantias sin descuentos de sanciones

                                Dim WorkDays As Integer = 0

                                Dim EvaluationInitialDate As Date
                                If InitialDate > _contract.JobBondingDate Then
                                    EvaluationInitialDate = InitialDate
                                Else
                                    EvaluationInitialDate = _contract.JobBondingDate
                                End If

                                WorkDays = _liquidationDomain.Days360(EvaluationInitialDate, EndingDate)

                                If EndingDate.Month = 2 AndAlso _contract.Group.Month = 2 Then
                                    If EndingDate.Day = 28 Then
                                        WorkDays = WorkDays - 2
                                    End If

                                    If EndingDate.Day = 29 Then
                                        WorkDays = WorkDays - 1
                                    End If
                                End If


                                Dim ValueAuxTransporte As Double

                                If _contract.BasicSalary <= 2 * _contract.Group.PayrollParameter.LegalSalaryMinimum Then
                                    ValueAuxTransporte = _contract.Group.PayrollParameter.TransportHelpValue
                                Else
                                    ValueAuxTransporte = 0
                                End If

                                'Auxilio de Transporte del Año Anterior
                                If Year(Date.Now) > Year(InitialDate) Then
                                    If _contract.BasicSalary <= 2 * ObjPayrollSettings.MinimunLegalSalaryLastYear Then
                                        ValueAuxTransporte = ObjPayrollSettings.TransportHealthValueLastYear
                                    Else
                                        ValueAuxTransporte = 0
                                    End If
                                End If

                                _ibcaverage = _ibcaverage + BasePrimasCesantias

                                If WorkDays > 30 Then
                                    _unemployedAverage = (_unemployedAverage / (WorkDays)) * 30
                                    IBCAverage = (_ibcaverage / WorkDays) * 30
                                Else
                                    IBCAverage = _ibcaverage
                                End If

                                Dim SalaryAverage As Decimal = 0
                                Dim AverageTransportHealthVAlue As Decimal = 0
                                If liquidationContract = False Then

                                    If EmployeeListContract IsNot Nothing Then
                                        Dim NewEmployeeListContract = EmployeeListContract.Where(Function(x) {2, 3, 4, 5, 6}.Contains(x.ContractType.ContractClass))
                                        Dim BasicSalary As Decimal = 0
                                        Dim VariacionSalario As Boolean = False

                                        For Each ObjContractTmp As Contract In NewEmployeeListContract

                                            If BasicSalary = 0 And ObjContractTmp.ContractInitialDate >= New Date(EndingDate.Year, EndingDate.Month - 2, 1) Then
                                                BasicSalary = ObjContractTmp.BasicSalary
                                            ElseIf BasicSalary <> ObjContractTmp.BasicSalary And ObjContractTmp.ContractInitialDate >= New Date(EndingDate.Year, EndingDate.Month - 2, 1) Then
                                                VariacionSalario = True
                                            Else
                                                VariacionSalario = False
                                                BasicSalary = ObjContractTmp.BasicSalary
                                            End If

                                        Next


                                        If VariacionSalario = True Then

                                            If NewEmployeeListContract.Count > 1 Then
                                                Dim DaysPromedy As Integer = 0
                                                For Each objContract As Domain.Payroll.Entities.Contract In NewEmployeeListContract

                                                    'Para contratos antiguos con variación en el Periodo de Primas

                                                    If objContract.ContractInitialDate < InitialDate And objContract.ContractEndingDate >= EndingDate Then
                                                        'Si no hay variaciones en todo el contrato
                                                        DaysPromedy = WorkDays
                                                    ElseIf objContract.ContractInitialDate > InitialDate And objContract.ContractEndingDate >= EndingDate Then
                                                        'Si el Contrato inició después de la Fecha Corte de Primas y finaliza después de la FEcha Corte de PRimas
                                                        DaysPromedy = _liquidationDomain.Days360(objContract.ContractInitialDate, EndingDate)
                                                    ElseIf objContract.ContractInitialDate > InitialDate And objContract.ContractEndingDate < EndingDate Then
                                                        'Si el contrato inició después de la Fecha de Corte de Primas y finaliza antes de la FEcha Corte de Primas
                                                        DaysPromedy = _liquidationDomain.Days360(objContract.ContractInitialDate, objContract.ContractEndingDate)
                                                    ElseIf objContract.ContractInitialDate <= InitialDate And objContract.ContractEndingDate < EndingDate Then
                                                        'Si el contrato inició antes o en la Fecha de Corte de Primas y finalizó ANTES de la FEcha de corte de Primas
                                                        DaysPromedy = _liquidationDomain.Days360(InitialDate, objContract.ContractEndingDate)
                                                    End If

                                                    SalaryAverage = SalaryAverage + ((objContract.BasicSalary * DaysPromedy) / WorkDays)

                                                    If objContract.BasicSalary <= 2 * _contract.Group.PayrollParameter.LegalSalaryMinimum Then
                                                        AverageTransportHealthVAlue = AverageTransportHealthVAlue + ((ValueAuxTransporte * DaysPromedy) / WorkDays)
                                                    End If

                                                Next

                                            Else
                                                SalaryAverage = _contract.BasicSalary

                                                If _contract.BasicSalary <= 2 * _contract.Group.PayrollParameter.LegalSalaryMinimum Then
                                                    AverageTransportHealthVAlue = ValueAuxTransporte
                                                End If

                                                'SalaryAverage = ActualContract.BasicSalary
                                            End If
                                        Else
                                            SalaryAverage = _contract.BasicSalary

                                            If _contract.BasicSalary <= 2 * _contract.Group.PayrollParameter.LegalSalaryMinimum Then
                                                AverageTransportHealthVAlue = ValueAuxTransporte
                                            End If
                                        End If

                                    End If
                                End If

                                Dim TotalUnemployed As Decimal = 0

                                If liquidationContract = False Then
                                    TotalUnemployed = (((SalaryAverage + AverageTransportHealthVAlue + IBCAverage) * (WorkDays - (TotalDeductionsDays))) / 360) - UnemployedLiquidatedValue
                                    ConceptFormulate = "(([Promedio Salario] + [Aux. Transporte] + [Salario Variable Cesantias]) * ([Dias Trabajados] - [Dias Sanción])) / 360"
                                    ReplaceFormulate = "((" + SalaryAverage.ToString() + " + " + AverageTransportHealthVAlue.ToString() + " + " + IBCAverage.ToString() + ") * (" + WorkDays.ToString() + " - " + (TotalDeductionsDays).ToString() + ")) / 360"
                                Else
                                    TotalUnemployed = (((_contract.BasicSalary + ValueAuxTransporte + IBCAverage) * (WorkDays - (TotalDeductionsDays))) / 360) - UnemployedLiquidatedValue
                                    ConceptFormulate = "(([Salario Básico] + [Aux. Transporte] + [Salario Variable Cesantias]) * ([Dias Trabajados] - ([Dias Sanción] + [Dias Lic. No Remuneradas])) / 360"
                                    ReplaceFormulate = "((" + _contract.BasicSalary.ToString() + " + " + ValueAuxTransporte.ToString() + " + " + IBCAverage.ToString() + ") * (" + WorkDays.ToString() + " - " + (TotalDeductionsDays).ToString() + ")) / 360"
                                End If

                                'Dim TotalUnemployed = ((_contract.BasicSalary + ValueAuxTransporte + _unemployedAverage) * (WorkDays - _sanctionTotalDays)) / 360
                                _unemployedInterestAverage = ((TotalUnemployed * 0.12) * (WorkDays - (TotalDeductionsDays))) / 360

                                Dim ConceptInterestFormulate = "(([Total Valor Cesantias] * 0.12) * ([Dias Trabajados] - ([Dias Sanción] + [Dias Lic. No Remuneradas])) / 360"
                                Dim ReplaceInterestFormulate = "((" + TotalUnemployed.ToString() + " * " + "0.12) * (" + WorkDays.ToString() + " - " + (TotalDeductionsDays).ToString() + ")) / 360"

                                If ObjPayrollSettings.UnemploymentTypeCalculated = 2 Then
                                    Dim ListLiquidation = _liquidationRepository.LiquidationEmployeeByDate(_employee.Id, TemporalDateInitial, EndingDate)
                                    Dim ConceptData = CalculatedProvisions(ListLiquidation)

                                    For Each ObjTuple As Tuple(Of String, Double) In ConceptData
                                        If ObjTuple.Item1 = "Cesantias" Then
                                            TotalUnemployed = Math.Round(ObjTuple.Item2, 0)
                                        End If
                                        If ObjTuple.Item1 = "Intereses" Then
                                            _unemployedInterestAverage = ObjTuple.Item2
                                        End If
                                    Next

                                    ConceptFormulate = "Sumatoria (Valor Provisiones Cesantias)"
                                    ReplaceFormulate = "Sumatoria ( " + TotalUnemployed.ToString() + " )"
                                    ConceptInterestFormulate = "Sumatoria (Valor Provisiones Intereses)"
                                    ReplaceInterestFormulate = "Sumatoria ( " + _unemployedInterestAverage.ToString() + " )"

                                End If

                                Dim TmpListDetail As New List(Of Tuple(Of Integer, Integer, Double, String, String))
                                'Cargo el Valor de Cesantias
                                TmpListDetail.Add(New Tuple(Of Integer, Integer, Double, String, String)(ObjPayrollSettings.IdConceptUnemployment, 1, TotalUnemployed, ConceptFormulate, ReplaceFormulate))
                                'Cargo el Valor de los Intereses de Cesantías
                                TmpListDetail.Add(New Tuple(Of Integer, Integer, Double, String, String)(ObjPayrollSettings.IdConceptInterestUnemployment, 1, _unemployedInterestAverage, ConceptInterestFormulate, ReplaceInterestFormulate))


                                For Each ObjDetailTuple As Tuple(Of Integer, Integer, Double, String, String) In TmpListDetail
                                    Dim UnemploymentConceptDetail = New UnemployedConcept()
                                    Dim AccruedValue As Double = 0
                                    Dim DeductedValue As Double = 0

                                    Dim ObjConcept = _conceptRepository.GetConceptId(ObjDetailTuple.Item1)

                                    UnemploymentConceptDetail.IdConcept = ObjDetailTuple.Item1

                                    If ObjDetailTuple.Item2 = 1 Then
                                        UnemploymentConceptDetail.AccruedValue = ObjDetailTuple.Item3
                                        UnemploymentConceptDetail.DeductedValue = 0
                                    Else
                                        UnemploymentConceptDetail.DeductedValue = ObjDetailTuple.Item3
                                        UnemploymentConceptDetail.AccruedValue = 0
                                    End If

                                    UnemploymentConceptDetail.ConceptName = ObjConcept.Name
                                    UnemploymentConceptDetail.ConceptType = ObjConcept.ConceptType

                                    UnemploymentConceptDetail.ConceptFormulate = ObjDetailTuple.Item4
                                    UnemploymentConceptDetail.ReplaceConceptFormulate = ObjDetailTuple.Item5

                                    .UnemployedConcept.Add(UnemploymentConceptDetail)
                                Next


                                Dim RangeAproximation = _employee.Contract.Item(0).Group.PayrollParameter.AproximationValue
                                If RangeAproximation Is Nothing Then
                                    RangeAproximation = 0
                                End If

                                '.TotalUnemployed = RoundedValuesByRate(_unemployedAverage, RangeAproximation)
                                .TotalUnemployed = RoundedValuesByRate(TotalUnemployed, RangeAproximation)
                                .WorkedTotalDays = WorkDays
                                .IBCAverage = IBCAverage
                                .IBCAverageNotSanction = IBCAverageNotSanctions

                                .DeductSanctionsDays = Sanction
                                .SanctionTotalDays = TotalDeductionsDays
                                .JobBondingDate = _contract.JobBondingDate
                                .Year = InitialDate.Year
                                .UnemployedInterestPercentage = 12 'cesantías procentaje
                                .UnemployedInterestTotal = RoundedValuesByRate(_unemployedInterestAverage, RangeAproximation)
                                .AverageMothNumber = .UnemployedLiquidationDetail.Count 'numero de meses que promedió
                                .BasicSalary = _contract.BasicSalary
                                .GroupId = _contract.GroupId
                                .CalculationType = ObjPayrollSettings.UnemploymentTypeCalculated
                                .ContractNumber = _contract.InitialContractNumber
                                .GroupName = _contract.Group.Code + " - " + _contract.Group.Name
                                If liquidationContract = False Then
                                    .FundName = _fundContract.Fund.Name
                                End If
                                .FullNameEmployee = _employee.ThirdParty.Nit + " - " + _employee.ThirdParty.Name
                                .NitEmployee = _employee.ThirdParty.Nit
                                .PositionName = _contract.Position.Name
                            End With
                            ListUnemployement.Add(newUneLiq)

                            'ProcessItemToAdd(ListUnemployedLiquidationResult, Nothing, _employee, newUneLiq)
                        End If
                    End If
                End If
            Next

            If ListUnemployedLiquidationToDelete.Count > 0 Then
                If DeleteListUnemployedLiquidationsWithoutConfirm(ListUnemployedLiquidationToDelete) = False Then
                    Return Nothing
                End If
            End If
            ActionMessageResult.ObjectEmbbeded = ListUnemployement
            Return ActionMessageResult
        Catch ex As Exception
            ActionMessageResult.StateResult = False
            ActionMessageResult.MessageResult.Add(New MessageResult("-999: Error", ex.Message))
            Return ActionMessageResult
        End Try
    End Function

    ''' <summary>
    ''' Metodo para Añadir registero de liquidacion en el listado final a retornar
    ''' </summary>
    ''' <param name="listUnemployedLiquidationResult">listado final a retornar</param>
    ''' <param name="messageResult">mensaje si lo hay</param>
    ''' <param name="_employee">entidad empleado</param>
    ''' <param name="newUneLiqTrue">entidad de registro de cesantía nuevo si lo hay</param>
    ''' <remarks></remarks>
    Sub ProcessItemToAdd(ByRef listUnemployedLiquidationResult As List(Of ActionMessageResult(Of UnemployedLiquidation)), messageResult As MessageResult, _employee As Employee, Optional newUneLiqTrue As UnemployedLiquidation = Nothing) Implements IUnemployedLiquidationDomain.ProcessItemToAdd
        Dim newActMesResult = New ActionMessageResult(Of UnemployedLiquidation)
        If messageResult IsNot Nothing Then
            If listUnemployedLiquidationResult.Count > 0 AndAlso listUnemployedLiquidationResult.Find(Function(x) x.ObjectEmbbeded.EmployeeId = _employee.Id) IsNot Nothing Then
                Dim _UneLiqExist As ActionMessageResult(Of UnemployedLiquidation) = listUnemployedLiquidationResult.Find(Function(x) x.ObjectEmbbeded.EmployeeId = _employee.Id)
                _UneLiqExist.MessageResult.Add(messageResult)
            Else
                Dim newUneLiq As UnemployedLiquidation = New UnemployedLiquidation
                newUneLiq.Employee = _employee
                newActMesResult.StateResult = False
                newActMesResult.MessageResult.Add(messageResult)
                newActMesResult.ObjectEmbbeded = newUneLiq
                listUnemployedLiquidationResult.Add(newActMesResult)
            End If
        Else
            If listUnemployedLiquidationResult.Count > 0 AndAlso listUnemployedLiquidationResult.Find(Function(x) x.ObjectEmbbeded.EmployeeId = _employee.Id) IsNot Nothing Then
                Dim _UneLiqExist As ActionMessageResult(Of UnemployedLiquidation) = listUnemployedLiquidationResult.Find(Function(x) x.ObjectEmbbeded.EmployeeId = _employee.Id)
                _UneLiqExist.ObjectEmbbeded = newUneLiqTrue
                _UneLiqExist.StateResult = True
            Else
                newActMesResult.StateResult = True
                newActMesResult.ObjectEmbbeded = newUneLiqTrue
                listUnemployedLiquidationResult.Add(newActMesResult)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Funcion para calcular la liquidacion entre un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>detallados mes a mes de liquidación</returns>
    ''' <remarks></remarks>
    Private Function CalculateMonthsWorked(employeeId As Integer, initialDate As Date, endDate As Date, Optional InitialContractNumber As Integer = 0) As TrackableCollection(Of UnemployedLiquidationDetail) Implements IUnemployedLiquidationDomain.CalculateMonthsWorked
        Dim ListUneLiqDetail As TrackableCollection(Of UnemployedLiquidationDetail) = New TrackableCollection(Of UnemployedLiquidationDetail)
        Dim listLiquidation As List(Of Liquidation) = _liquidationRepository.LiquidationEmployeeByDate(employeeId, initialDate, endDate)
        If listLiquidation.Count > 0 Then
            'se construyen los UnemployedLiquidationDetail
            For Each item In listLiquidation
                If InitialContractNumber > 0 Then
                    If item.InitialContractNumber = InitialContractNumber Then
                        ListUneLiqDetail.Add(ConstructEntityUnemployed(item))
                    End If
                Else
                    ListUneLiqDetail.Add(ConstructEntityUnemployed(item))
                End If

            Next
        End If
        Return ListUneLiqDetail
    End Function

    ''' <summary>
    ''' Obtiene las liquidaciones de cesantías de un empleado en determinado lapso de tiempo
    ''' </summary>
    ''' <param name="employeeId">empleado id</param>
    ''' <param name="initialDate">fecha inicio</param>
    ''' <param name="endDate">fecha fin</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUnemployedLiquidationEmployee(employeeId As Integer, initialDate As Date, endDate As Date) As List(Of UnemployedLiquidation) Implements IUnemployedLiquidationDomain.GetUnemployedLiquidationEmployee
        Dim ListUneLiqDetail As TrackableCollection(Of UnemployedLiquidationDetail) = New TrackableCollection(Of UnemployedLiquidationDetail)
        Dim listLiquidation = _unemployedLiquidationRepository.GetUnemployedLiquidationEmployeeByDate(employeeId, initialDate, endDate)
        Return listLiquidation
    End Function

    ''' <summary>
    ''' Metodo para eliminar las liquidaciones sin confirmar cuando se liquidan de nuevo
    ''' </summary>
    ''' <param name="listUnemployedLiquidation">listado de liquidacion de cesantías a eliminar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteListUnemployedLiquidationsWithoutConfirm(listUnemployedLiquidation As List(Of UnemployedLiquidation)) As Boolean Implements IUnemployedLiquidationDomain.DeleteListUnemployedLiquidationsWithoutConfirm
        Try
            For Each itemUL In listUnemployedLiquidation
                'itemUL.Employee = Nothing
                'Dim indexULD As Integer = 0
                'While itemUL.UnemployedLiquidationDetail.Count > 0

                '    indexULD = itemUL.UnemployedLiquidationDetail.Count - 1
                '    itemUL.UnemployedLiquidationDetail.Item(indexULD).MarkAsDeleted()
                '    _unemployedLiquidationDetailRepository.DeleteEntity(itemUL.UnemployedLiquidationDetail.Item(indexULD))

                'End While

                Dim StringDeleteDetail As String = "DELETE FROM Payroll.UnemployedLiquidationDetail where UnemployedLiquidationId = " + itemUL.Id.ToString()
                _unemployedLiquidationDetailRepository.UnitWork.ExecuteNonQuery(StringDeleteDetail)
                _unemployedLiquidationDetailRepository.UnitWork.Commit()

                Dim StringDeleteConcept As String = "DELETE FROM Payroll.UnemployedConcept WHERE IdUnemployementLiquidation = " + itemUL.Id.ToString()
                _unemployedLiquidationDetailRepository.UnitWork.ExecuteNonQuery(StringDeleteConcept)
                _unemployedLiquidationDetailRepository.UnitWork.Commit()

                'itemUL.MarkAsDeleted()
                _unemployedLiquidationRepositoryToSave.DeleteEntity(itemUL)
            Next

            '_unemployedLiquidationDetailRepository.UnitWork.Commit()
            _unemployedLiquidationRepositoryToSave.UnitWork.Commit()

            Return True
        Catch ex As Exception
            _unemployedLiquidationDetailRepository.UnitWork.RollbackAllChanges()
            _unemployedLiquidationRepositoryToSave.UnitWork.RollbackAllChanges()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Funcion que construye los unemployedliquidationdetail, dependiendo de los registros de liquidacion
    ''' </summary>
    ''' <param name="liq"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConstructEntityUnemployed(liq As Liquidation) As UnemployedLiquidationDetail

        Dim AccumulatedUnemployed As Double = 0
        Dim ProvisionInterest As Double = 0
        Dim IBCUnemployement As Double = 0

        ConstructEntityUnemployed = New UnemployedLiquidationDetail
        With ConstructEntityUnemployed
            .Month = liq.PayrollDateLiquidated.Month
            .WorkedDays = liq.DaysWorked
            .SanctionsDays = IIf(liq.SanctionDays Is Nothing, 0, liq.SanctionDays)
            If .Month = 2 AndAlso (.WorkedDays = 28 Or .WorkedDays = 29) Then
                .WorkedDays = 30
            End If

            If .WorkedDays > 30 Then
                .WorkedDays = 30
            End If
            .Year = liq.PayrollDateLiquidated.Year
            .Salary = liq.BasicSalary

            Dim LiquidationDetail As New List(Of LiquidationDetail)

            If liq.LiquidationDetail IsNot Nothing Then
                LiquidationDetail = liq.LiquidationDetail.Where(Function(x) x.ConceptClass <> "005" AndAlso x.ConceptClass <> "006").ToList()
                For i As Integer = 0 To LiquidationDetail.Count() - 1

                    'If liq.LiquidationDetail.Item(i).Concept.AffectIBCSeverance = True Then
                    '.ValIBC = liq.BasicSalary + liq.LiquidationDetail.Item(i).ConceptTotalValue

                    If LiquidationDetail.Item(i).ConceptClass = "008" Then
                        AccumulatedUnemployed = AccumulatedUnemployed + LiquidationDetail.Item(i).ConceptTotalValue
                    End If

                    If LiquidationDetail.Item(i).ConceptClass = "034" Then
                        ProvisionInterest = ProvisionInterest + LiquidationDetail.Item(i).ConceptTotalValue
                    End If

                    If LiquidationDetail.Item(i).Concept.AffectIBCSeverance = True Then
                        If LiquidationDetail.Item(i).ConceptType = 1 Then
                            IBCUnemployement = IBCUnemployement + LiquidationDetail.Item(i).ConceptTotalValue
                        End If

                        If LiquidationDetail.Item(i).ConceptType = 2 Then
                            IBCUnemployement = IBCUnemployement - LiquidationDetail.Item(i).ConceptTotalValue
                        End If
                    End If


                    ' End If
                Next
            End If

            .ValIBC = IBCUnemployement
            .ValIBCNotSanction = liq.BasicSalary + IIf(liq.IBCUnemploymentNoSanctions Is Nothing, 0, liq.IBCUnemploymentNoSanctions)
            .UnemployedAverage = AccumulatedUnemployed
            .UnemployedInterestAverage = ProvisionInterest
            'If .Month = 2 Then
            '    If (.WorkedDays + .SanctionsDays) <= 28 Then
            '        .WorkedDays = 30
            '    End If
            'End If
        End With
    End Function

    Public Function CalculatedProvisions(ListOfLiquidation As List(Of Liquidation)) As List(Of Tuple(Of String, Double))

        Dim ObjReturn As New List(Of Tuple(Of String, Double))
        Dim ProvisionUnemployment As Double = 0
        Dim ProvisionInterestUnemployment As Double = 0

        If ListOfLiquidation IsNot Nothing AndAlso ListOfLiquidation.Count > 0 Then
            For Each ObjLiquidation As Liquidation In ListOfLiquidation
                If ObjLiquidation.LiquidationDetail.Count > 0 Then
                    ProvisionUnemployment = ProvisionUnemployment + ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptClass = "008").FirstOrDefault().ConceptTotalValue
                    ProvisionInterestUnemployment = ProvisionInterestUnemployment + ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptClass = "034").FirstOrDefault().ConceptTotalValue
                End If
            Next
        End If

        ObjReturn.Add(New Tuple(Of String, Double)("Cesantias", ProvisionUnemployment))
        ObjReturn.Add(New Tuple(Of String, Double)("Intereses", ProvisionInterestUnemployment))

        Return ObjReturn

    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _liquidationDomain.Dispose()
            End If
            _ManualConceptsRepository = Nothing
            _unemployedLiquidationRepository = Nothing
            _unemployedLiquidationRepositoryToSave = Nothing
            _unemployedLiquidationDetailRepository = Nothing
            _liquidationRepository = Nothing
            _employeeRepository = Nothing
            _liquidationDomain = Nothing
            _noveltyRepository = Nothing
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
