Imports System.Resources
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Interface IPayrollNoveltyDomain
    Function CalculateTwoFirstDays(employeeId As Integer, session As SessionValues) As ActionResult(Of Decimal)

    Function CalculateNoveltyConcept(employeeContract As Contract, noveltyDays As Integer, session As SessionValues) As ActionResult(Of Decimal)
End Interface

Public Class PayrollNoveltyDomain
    Implements IPayrollNoveltyDomain

    Private ReadOnly _conceptFormulatePrefix = "[CM_"
    Private ReadOnly _liquidationDomain As ILiquidationDomain
    Private ReadOnly _employeeRepository As IEmployeeRepository
    Private ReadOnly _manualConceptRepository As IManualConcepts
    Private ReadOnly _payrollSettingsRepository As IPayrollSettingsRepository
    Private ReadOnly _payrollConceptRepository As IConceptRepository
    Private Const _formName = "Novedades"
    Public Sub New(
            liquidationDomain As ILiquidationDomain,
            employeeRepository As IEmployeeRepository,
            manualConceptRepository As IManualConcepts,
            payrollSettingsRepository As IPayrollSettingsRepository,
            payrollConceptRepository As IConceptRepository)
        _liquidationDomain = liquidationDomain
        _employeeRepository = employeeRepository
        _manualConceptRepository = manualConceptRepository
        _payrollSettingsRepository = payrollSettingsRepository
        _payrollConceptRepository = payrollConceptRepository
    End Sub

    Public Function CalculateTwoFirstDays(employeeId As Integer, session As SessionValues) As ActionResult(Of Decimal) Implements IPayrollNoveltyDomain.CalculateTwoFirstDays
        Dim employee = _employeeRepository.GetEmployeeById(employeeId, False)
        If employee Is Nothing Then
            Throw New IndigoValidationException("empleado no encontrado")
        End If

        Dim settings = _payrollSettingsRepository.GetSettingPayroll()
        If settings Is Nothing Then
            Throw New IndigoValidationException("parámetros de nómina no encontrados")
        End If

        If settings.HandlesLiquidationFirstTwoDays Then
            Dim twoFirstDaysFormula = settings.FirstTwoDaysFormulate
            Dim activeContract = employee.Contract.Where(Function(m) m.Valid AndAlso m.Status = 1).FirstOrDefault()
            If activeContract Is Nothing Then
                Throw New IndigoValidationException("el empleado no tiene un contrato activo")
            End If

            Dim groupEmployee As Group = activeContract.Group
            Dim executionLiquidation = _liquidationDomain.NewExecuteLiquitadion({employee}.ToList(), groupEmployee, False, session)

            If Not executionLiquidation.StateResult Then
                Throw New IndigoValidationException(executionLiquidation.Message)
            End If

            Dim liquidation = executionLiquidation.ObjectEmbbeded.FirstOrDefault()
            Dim daysWorked = liquidation.DaysWorked
            Dim basicSalary = liquidation.BasicSalary


            If twoFirstDaysFormula.Contains(_conceptFormulatePrefix) Then
                twoFirstDaysFormula = ReplaceManualConceptAsVariable(employeeId, twoFirstDaysFormula)
            End If

            Dim resultFormula = ResolveFormula(twoFirstDaysFormula, New NoveltyFormulaData With {
                .Salary = basicSalary,
                .WorkedDays = daysWorked
            })

            Return New ActionResult(Of Decimal) With {.StateResult = True, .ObjectEmbbeded = resultFormula("Value").AsDecimal()}
        End If

        Return Nothing
    End Function

    Private Function ReplaceManualConceptAsVariable(employeeId As Integer, formula As String) As String
        Dim pattern As String = $"\{_conceptFormulatePrefix}(\d+)\]"
        Dim matches = Text.RegularExpressions.Regex.Matches(formula, pattern)

        For Each match As Text.RegularExpressions.Match In matches
            Dim conceptCode = match.Groups(1).Value
            Dim concept = _manualConceptRepository.FirstOrDefault(Function(m) m.EmployeeId = employeeId AndAlso m.Concept.Code = conceptCode AndAlso m.State = 1) ' Concepto activo

            If concept IsNot Nothing Then
                formula = formula.Replace($"{_conceptFormulatePrefix}{conceptCode}]", concept.QuoteValue.ToString().Replace(",", "."))
            End If
        Next

        Return formula
    End Function

    Public Function ResolveFormula(formula As String, data As NoveltyFormulaData) As Dictionary(Of String, String)
        Dim resolvedFormula = formula

        resolvedFormula = resolvedFormula.Replace("[Sueldo Contrato]", Replace(data.Salary, ",", "."))
        resolvedFormula = resolvedFormula.Replace("[Dias Trabajados Liq Contrato]", data.WorkedDays)

        Dim result = Utils.EvalExpression(resolvedFormula)
        Dim dic = New Dictionary(Of String, String) From {
            {"Formula", formula},
            {"Value", IIf(result.StateResult, result.ObjectEmbbeded, 0)}
        }

        Return dic
    End Function

    Public Function CalculateNoveltyConcept(employeeContract As Contract, noveltyDays As Integer, session As SessionValues) As ActionResult(Of Decimal) Implements IPayrollNoveltyDomain.CalculateNoveltyConcept
        Dim integralSalaryConceptNoveltyId = _payrollSettingsRepository.GetSettingPayroll().ConceptBenefitFactortId.AsDecimal
        'Dim integralSalaryConceptNoveltyId = settings.ConceptBenefitFactortId
        If integralSalaryConceptNoveltyId Is Nothing Then
            Return New ActionResult(Of Decimal) With {
                .StateResult = False,
                .ObjectEmbbeded = 0,
                .Message = "No se encuentra concepto asoaciado a los Párametros de Nómina"
            }
        End If
        Dim integralSalaryConceptNovelty = _payrollConceptRepository.GetConceptId(integralSalaryConceptNoveltyId)
        If integralSalaryConceptNovelty Is Nothing OrElse String.IsNullOrWhiteSpace(integralSalaryConceptNovelty.Formulates) Then
            Return New ActionResult(Of Decimal) With {
            .StateResult = False,
            .ObjectEmbbeded = 0,
            .Message = "El concepto con la fórmula no está definido correctamente."
        }
        End If
        Dim formulate As String = integralSalaryConceptNovelty.Formulates
        formulate = formulate.Replace("[Sueldo Contrato]", employeeContract.BasicSalary.ToString(System.Globalization.CultureInfo.InvariantCulture))
        formulate = formulate.Replace("[Días Incapacidad]", noveltyDays.ToString(System.Globalization.CultureInfo.InvariantCulture))

        Dim resultValue As Decimal
        Try
            Dim table As New DataTable()
            ' Creamos una columna "expression" cuyo Expression es la fórmula resultante
            table.Columns.Add("expression", GetType(Double), formulate)
            Dim row As DataRow = table.NewRow()
            table.Rows.Add(row)

            ' El valor calculado queda en row("expression")
            resultValue = Convert.ToDecimal(row("expression"))
        Catch ex As Exception
            Return New ActionResult(Of Decimal) With {
            .StateResult = False,
            .ObjectEmbbeded = 0,
            .Message = "Error al evaluar la fórmula: " & ex.Message
        }
        End Try

        Return New ActionResult(Of Decimal) With {
        .StateResult = True,
        .ObjectEmbbeded = resultValue
         }

    End Function
End Class
