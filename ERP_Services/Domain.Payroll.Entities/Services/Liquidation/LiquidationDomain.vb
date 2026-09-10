Imports System.CodeDom.Compiler
Imports System.Reflection

Public Class LiquidationDomain
    Implements ILiquidationDomain

    ''' <summary>
    ''' Función para reemplazar Datos en la fórmula
    ''' </summary>
    ''' <param name="payrollParameter">Objeto PayrollParameter</param>
    ''' <param name="BasicSalary">Salario Básico Empleado</param>
    ''' <param name="DaysWorkedEmployee">Días Trabajados Empleado</param>
    ''' <param name="WorkHours">Horas Trabajadas</param>
    ''' <param name="PayrollDays">Días Nómina</param>
    ''' <param name="FormulaConcept">Fórmula del Concepto</param>
    ''' <returns>Valor del Concepto</returns>
    ''' <remarks></remarks>
    Public Function ReplaceData(payrollParameter As PayrollParameter, BasicSalary As String, DaysWorkedEmployee As String, WorkHours As String, PayrollDays As String, FormulaConcept As String, employeePayroll As Contract, BaseIBCPension As Double, BaseIBCHealth As Double, IBCHealth As Double, BaseIBCHealthEmployer As Double, _
                                BaseIBCPensionEmployer As Double, IBCSENA As Double, IBCICBF As Double, IBCPeriod As Double, IBCSeverance As Double, IBCCompensationFund As Double, IBCPension As Double, IBCARP As Double, ValueAmbulatoryInability As Double, ValueHospitalInability As Double, ValueMaternity As Double, _
                                ValueSanctions As Double, TotalPeriodDaysInabilities As Integer, HospitalInabilityDays As Integer, RemuneratedLicensesDays As Integer, UnpaidLicensesDays As Integer, ValueUnpaidLicenses As Double, ValueProfesionalInabilities As Double, HealthEmployee As Double, _
                                PensionEmployee As Double, BaseIBCArp As Double, BaseParafiscalCompensationFund As Double, BaseParafiscalICBF As Double, RetentionValue As Double, ProvisionDays As Integer, ProfessionalRiskPercentage As Double, VacationValue As Double, AdjustVacationValue As Double, _
                                incentiePaymentValue As Double, unemployedInteresValue As Double) As Dictionary(Of String, String) Implements ILiquidationDomain.ReplaceData

        Dim descriptions As Dictionary(Of String, String)

        FormulaConcept = Replace(FormulaConcept, "[Salario Mínimo]", Replace(payrollParameter.LegalSalaryMinimum.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Auxilio Transporte]", Replace(payrollParameter.TransportHelpValue.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Salud Empleado]", Replace(payrollParameter.EmployeeHealthContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Salud Patrono]", Replace(payrollParameter.EmployerHealthContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Pensión Empleado]", Replace(payrollParameter.EmployeePensionContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Pensión Patrono]", Replace(payrollParameter.EmployerPensionContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% SENA]", Replace(payrollParameter.SenaContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% ICBF]", Replace(payrollParameter.ICBFContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Caja]", Replace(payrollParameter.CompensationFundContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Sueldo Contrato]", Replace(BasicSalary.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Laboradas]", Replace(WorkHours.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Días Incapacidad]", Replace(TotalPeriodDaysInabilities.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Días Nómina]", Replace(PayrollDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Pensión]", Replace(BaseIBCPension.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Salud]", Replace(BaseIBCHealth.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Salud Patrono]", Replace(BaseIBCHealthEmployer.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Pensión Patrono]", Replace(BaseIBCPensionEmployer.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad Ambulatoria]", Replace(ValueAmbulatoryInability.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad Hospitalaria]", Replace(ValueHospitalInability.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Días Incapacidad Hospitalaria]", Replace(HospitalInabilityDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Maternidad]", Replace(ValueMaternity.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Días Licencia]", Replace(RemuneratedLicensesDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor licencia no Remunerada]", Replace(ValueUnpaidLicenses.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Días licencia no Remunerada]", Replace(UnpaidLicensesDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Sanciones]", Replace(ValueSanctions.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad Riesgos Pro]", Replace(ValueProfesionalInabilities.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias Trabajados]", Replace(DaysWorkedEmployee.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC SENA]", Replace(IBCSENA.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC ICBF]", Replace(IBCICBF.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Periodo]", Replace(IBCPeriod.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Cesantias]", Replace(IBCSeverance.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Caja]", Replace(IBCCompensationFund.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Pensión]", Replace(IBCPension.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC ARP]", Replace(IBCARP.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Salud]", Replace(IBCHealth.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Aporte a Salud Empleado]", Replace(HealthEmployee.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Aporte a Pensión Empleado]", Replace(PensionEmployee.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Riesgos Profesionales]", Replace(BaseIBCArp.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Aporte Parafiscal Caja de Compensación]", Replace(BaseParafiscalCompensationFund.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Aporte Parafiscal ICBF]", Replace(BaseParafiscalICBF.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Retención]", Replace(RetentionValue.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias Provisión]", Replace(ProvisionDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Tarifa ARP]", Replace(ProfessionalRiskPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Vacaciones]", Replace(VacationValue.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Ajuste Vacaciones]", Replace(AdjustVacationValue.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Ajuste Vacaciones]", Replace(AdjustVacationValue.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Interes de Cesantias]", Replace(unemployedInteresValue.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Primas]", Replace(incentiePaymentValue.ToString, ",", "."))


        'Días Provisión



        'Dim ConceptValue As Double = Me.EvalFunc(Of Double)(FormulaConcept)
        Dim ConceptValue As Double = Me.EvalTest(FormulaConcept)
        'Dim ConceptValue As Double = Me.Eval(FormulaConcept)



        'ConceptValue = Me.RoundedValues(ConceptValue)

        descriptions = New Dictionary(Of String, String)
        descriptions.Add("Formula", FormulaConcept)
        descriptions.Add("Valor", ConceptValue)

        Return descriptions

    End Function

    ''' <summary>
    ''' Obtiene la Fecha Final Nómina
    ''' </summary>
    ''' <param name="payrollLiquidation">Forma Liquidación Nómina</param>
    ''' <param name="payrollStarDate">Fecha Inicial Nómina</param>
    ''' <returns>Fecha Final nómina </returns>
    ''' <remarks></remarks>
    Public Function GetEndPayrollDate(payrollLiquidation As Integer, payrollStarDate As Date) As Date Implements ILiquidationDomain.GetEndPayrollDate

        Dim PayrollEndDate As Date

        If (payrollLiquidation = 1) Then '' Nómina Mensual 

            PayrollEndDate = DateAdd(DateInterval.Month, 1, payrollStarDate)
            PayrollEndDate = DateAdd(DateInterval.Day, -1, PayrollEndDate)

        ElseIf (payrollLiquidation = 2) Then '' Nómina Quincenal
            If Day(payrollStarDate) = 1 Then
                PayrollEndDate = "15/" + CStr(payrollStarDate.Month) + "/" + CStr(payrollStarDate.Year)
            ElseIf Day(payrollStarDate) = 16 Then
                PayrollEndDate = DateAdd(DateInterval.Month, 1, payrollStarDate)
                PayrollEndDate = DateAdd(DateInterval.Day, -1, payrollStarDate)
            End If
        End If

        Return PayrollEndDate

    End Function

    ''' <summary>
    ''' Obtiene los días trabajados del Empleado sin descuentos
    ''' </summary>
    ''' <param name="EmployeeInitialDate">Fecha Inicial Empleado</param>
    ''' <param name="employeEndDate">Fecha Fin Empleado</param>
    ''' <param name="payrollInitialDate">Fecha Inicial Nómina</param>
    ''' <param name="payrollEndDate">Fecha Final Nómina</param>
    ''' <param name="payrollMonth">Mes Nómina</param>
    ''' <returns>Días trabajados Empleado</returns>
    ''' <remarks></remarks>
    Public Function DaysWorkedEmployee(ByVal EmployeeInitialDate As Date, ByVal employeEndDate As Date, ByVal payrollInitialDate As Date, ByVal payrollEndDate As Date, payrollMonth As Char) As Integer Implements ILiquidationDomain.DaysWorkedEmployee

        Dim DaysWorkEmployee As Integer

        '' es un empleado antiguo
        If EmployeeInitialDate <= payrollInitialDate And employeEndDate >= payrollEndDate Then
            EmployeeInitialDate = payrollInitialDate
            employeEndDate = payrollEndDate
        End If

        '' es por que el empleado se le vence el contrato antes de la payrollEndDate y no le han renovado contrato
        If EmployeeInitialDate <= payrollInitialDate And employeEndDate < payrollEndDate Then
            employeEndDate = employeEndDate
        End If

        '' El empleado ingresó después de iniciado el periodo de nómina
        If EmployeeInitialDate > payrollInitialDate And employeEndDate >= payrollEndDate Then
            employeEndDate = payrollEndDate
        End If

        If payrollMonth = "1" Then
            DaysWorkEmployee = Days360(EmployeeInitialDate, employeEndDate)
        Else
            DaysWorkEmployee = DateDiff(DateInterval.Day, EmployeeInitialDate, employeEndDate) + 1
        End If

        Return DaysWorkEmployee

    End Function

    ''' <summary>
    ''' Evalúa y ejecuta una función escrita en código VB
    ''' </summary>
    ''' <typeparam name="T">Tipo de dato que retorna la función a evaluar</typeparam>
    ''' <param name="codeFunc">Código de la función a evaluar</param>
    ''' <returns>El resultado de tipo T</returns>
    Public Function EvalFunc(Of T)(ByVal codeFunc As String) As T Implements ILiquidationDomain.EvalFunc
        Try
            'Dim nameDll As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "EvalFunc")
            'If System.IO.File.Exists(nameDll & ".dll") Then
            '    System.IO.File.Delete(nameDll & ".dll")
            'End If
            'If System.IO.File.Exists(nameDll & ".pdb") Then
            '    System.IO.File.Delete(nameDll & ".pdb")
            'End If
            Dim provider As New VBCodeProvider()
            Dim params As New System.CodeDom.Compiler.CompilerParameters()
            Dim sb As New System.Text.StringBuilder
            Dim result As System.CodeDom.Compiler.CompilerResults
            With params
                .GenerateInMemory = True
                '.GenerateExecutable = False
                '.OutputAssembly = nameDll
                '.IncludeDebugInformation = True
            End With
            sb.AppendLine("Imports System")
            sb.AppendLine("Namespace TheNameSpace")
            sb.AppendLine("Public Class TheClass")
            sb.AppendLine("Public Function Fuck() As " & GetType(T).Name)
            sb.AppendLine("Return " & codeFunc)
            sb.AppendLine("End Function")
            sb.AppendLine("End Class")
            sb.AppendLine("End Namespace")
            result = provider.CompileAssemblyFromSource(params, sb.ToString())
            If result IsNot Nothing AndAlso result.Errors.Count <= 0 Then
                Dim args() As Object
                Dim obj = result.CompiledAssembly.CreateInstance("TheNameSpace.TheClass", False, Reflection.BindingFlags.CreateInstance, Nothing, args, Nothing, Nothing)
                Return CType(obj.Fuck(), T)
            Else
                Throw New Exception("La evaluación de la función ha retornado errores, porfavor corrijalos")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Function

    Private Function EvalTest(ByVal Expresion As String)

        Dim MyProvider As New VBCodeProvider 'Create a new VB Code Compiler
        Dim cp As New CompilerParameters     'Create a new Compiler parameter object.
        cp.GenerateExecutable = False        'Don't create an object on disk
        cp.GenerateInMemory = True           'But do create one in memory.
        'If cp.OutputAssembly is used with a VBCodeProvider, it seems to want to read before it is executed.  
        'See C# CodeBank example for explanation of why it was used.

        'the below is an empty VB.NET Project with a function that simply returns the value of our command parameter.
        Dim TempModuleSource As String = "Imports System" & Environment.NewLine & _
                                         "Namespace ns " & Environment.NewLine & _
                                         "Public Class class1" & Environment.NewLine & _
                                         "Public Shared Function Evaluate()" & Environment.NewLine & _
                                         "Return " & Expresion & Environment.NewLine & _
                                         "End Function" & Environment.NewLine & _
                                         "End Class" & Environment.NewLine & _
                                         "End Namespace"
        'Create a compiler output results object and compile the source code.
        Dim cr As CompilerResults = MyProvider.CompileAssemblyFromSource(cp, TempModuleSource)
        If cr.Errors.Count > 0 Then
            'If the expression passed is invalid or "", the compiler will generate errors.
            Throw New ArgumentOutOfRangeException("Invalid Expression - please use something VB could evaluate")
        Else
            'Find our Evaluate method.
            Dim methInfo As MethodInfo = cr.CompiledAssembly.GetType("ns.class1").GetMethod("Evaluate")
            'Invoke it on nothing, so that we can get the return value
            Return Convert.ToDouble(methInfo.Invoke(Nothing, Nothing))
        End If

    End Function
    Private CrLf As String = Environment.NewLine
    Public Function Eval(ByVal expresion As String) As Double

        ' Autor: Eduardo A. Morcillo

        Dim res As CompilerResults = Nothing

        Try
            ' Parámetros que utilizará el compilador
            Dim cpar As New CompilerParameters()
            'cpar.GenerateExecutable = False     ' Generar DLL 
            cpar.GenerateInMemory = True         ' Generar en memoria 
            'cpar.IncludeDebugInformation = True

            ' Añadir referencias 
            cpar.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll")

            ' Referenciamos el compilador de código de Visual Basic
            Dim vbcp As New VBCodeProvider()

            ' Escribimos el código fuente del ensamblado
            Dim source As String = _
                "Imports Microsoft.VisualBasic" & CrLf & _
                "Namespace MiNamespace" & CrLf & _
                "  Public Class MiClase" & CrLf & _
                "    Public Shared Function Eval() As Object " & CrLf & _
                "       Return " & expresion & CrLf & _
                "    End Function" & CrLf & _
                "  End Class" & CrLf & _
                "End Namespace"

            ' Compilamos el ensamblado
            res = vbcp.CompileAssemblyFromSource(cpar, source)
            res.TempFiles.KeepFiles = True

            ' Obtengo el Type de la clase recien compilada 
            Dim ty As System.Type = _
                res.CompiledAssembly.GetType("MiNamespace.MiClase")

            ' Obtengo el metodo Eval de la clase 
            Dim funceval As MethodInfo = ty.GetMethod("Eval")

            ' Ejecuto la funcion Eval recien creada 
            Return Convert.ToDouble(funceval.Invoke(Nothing, Nothing))

        Catch ex As System.IO.FileNotFoundException
            ' Si no se ha podido generar el ensamblado, el sistema
            ' será incapaz de encontrar el archivo de la biblioteca.
            '
            Throw New ArgumentException(res.Errors(0).ErrorText)

        Catch ex As Exception
            ' Devolvemos la excepción al procedimiento llamador
            Throw

        End Try

    End Function

    ''' <summary>
    ''' Función 360 días para calcular en dos rangos de Fechas siempre con meses de 30 días
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>Días Totales (Calculado en Meses de 30 días)</returns>
    ''' <remarks></remarks>
    Public Function Days360(ByVal initialDate As Date, ByVal endDate As Date) As Integer Implements ILiquidationDomain.Days360
        Dim TotalDays As Integer
        Dim AuxiliarDays As Integer

        If Day(endDate) > 30 Then
            AuxiliarDays = 0
        Else
            AuxiliarDays = 1
        End If

        If Math.Abs(Year(initialDate) - Year(endDate)) = 0 Then
            TotalDays = 30 - Day(initialDate) + (Math.Abs(Month(initialDate) - Month(endDate)) - 1) * 30 + Day(endDate) + AuxiliarDays
        Else
            TotalDays = (Math.Abs(Year(endDate) - Year(initialDate) - 1) * 360) + (360 - Month(initialDate) * 30) + ((Month(endDate) - 1) * 30 + Day(endDate)) + (30 - Day(initialDate)) + AuxiliarDays
        End If

        Return TotalDays

    End Function

    ''' <summary>
    ''' Función para Redondear Valores
    ''' </summary>
    ''' <param name="ValueIn">Valor a Redondear</param>
    ''' <returns>Valor Redondeado</returns>
    ''' <remarks></remarks>
    Public Function RoundedValues(ByVal ValueIn As Double) As Double Implements ILiquidationDomain.RoundedValues

        Dim ValueReturn As Double

        If ValueIn > 0 Then
            If Right(ValueIn, 2) = "00" Then
                ValueReturn = ValueIn
            Else
                ValueReturn = (Int(ValueIn / 100) + 1) * 100
            End If
        Else
            ValueReturn = 0
        End If

        Return ValueReturn

    End Function

    ''' <summary>
    ''' Obtiene el Número de Horas por Concepto
    ''' </summary>
    ''' <param name="handlesTurnsChart">Maneja Cuadro de Turno</param>
    ''' <param name="PayrollDays">Días de la Nómina</param>
    ''' <param name="AuthorizationConcept">Objeto Conceptos Autorizados</param>
    ''' <param name="HoursDaily">Horas Diarias</param>
    ''' <param name="ScheduleDetailEmployee">Objeto Detalle del Cuadro de Turno por Empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HourByConcept(ByVal handlesTurnsChart As Boolean, ByVal PayrollDays As Integer, AuthorizationConcept As AuthorizationConcept, HoursDaily As Integer, ByVal ScheduleDetailEmployee As List(Of ScheduleDetail)) As Integer Implements ILiquidationDomain.HourByConcept

        Dim VarHour As Integer = 0
        Dim Hour As Integer = 0
        Dim DistributeExpense As Boolean = False
        Dim CompareConceptType As Boolean

        If handlesTurnsChart = True Or AuthorizationConcept.Concept.ConceptClass <> "005" Then

            For i As Integer = 0 To (ScheduleDetailEmployee.Count() - 1)

                For j As Integer = 0 To (ScheduleDetailEmployee.Item(i).ScheduleDetailHour.Count() - 1)

                    For l As Integer = 0 To (ScheduleDetailEmployee.Item(i).ScheduleDetailHour.Item(j).ScheduleDetailConcept.Count() - 1)

                        Dim ConceptType = ScheduleDetailEmployee.Item(i).ScheduleDetailHour.Item(j).ScheduleDetailConcept.Item(l).ConceptType
                        Dim AppliedConcept = ScheduleDetailEmployee.Item(i).ScheduleDetailHour.Item(j).AppliedLiquidationConcept

                        If ConceptType = 1 Then
                            CompareConceptType = True
                        Else
                            CompareConceptType = False
                        End If

                        If ScheduleDetailEmployee.Item(i).ScheduleDetailHour.Item(j).ScheduleDetailConcept.Item(l).ConceptId = AuthorizationConcept.Concept.Id And AppliedConcept = CompareConceptType Then
                            VarHour = ScheduleDetailEmployee.Item(i).ScheduleDetailHour.Item(j).TotalNumberHours
                            'VarHour = ScheduleDetailEmployee.Item(i).TotalNumberHours
                            Hour = Hour + VarHour
                        Else
                            VarHour = 0
                        End If
                    Next
                Next
            Next
        Else
            If AuthorizationConcept.Concept.ConceptClass = "005" Then
                For i As Integer = 0 To (ScheduleDetailEmployee.Count() - 1)
                    For j As Integer = 0 To (ScheduleDetailEmployee.Item(i).ScheduleDetailHour.Count() - 1)
                        For l As Integer = 0 To (ScheduleDetailEmployee.Item(i).ScheduleDetailHour.Item(j).ScheduleDetailConcept.Count() - 1)

                            Dim ConceptType = ScheduleDetailEmployee.Item(i).ScheduleDetailHour.Item(j).ScheduleDetailConcept.Item(l).ConceptType
                            Dim AppliedConcept = ScheduleDetailEmployee.Item(i).ScheduleDetailHour.Item(j).AppliedLiquidationConcept

                            If ConceptType = 1 Then
                                CompareConceptType = True
                            Else
                                CompareConceptType = False
                            End If

                            If ScheduleDetailEmployee.Item(i).ScheduleDetailHour.Item(j).ScheduleDetailConcept.Item(l).ConceptId = AuthorizationConcept.Concept.Id And AppliedConcept = CompareConceptType Then
                                VarHour = ScheduleDetailEmployee.Item(i).ScheduleDetailHour.Item(j).TotalNumberHours
                                'VarHour = ScheduleDetailEmployee.Item(i).TotalNumberHours
                                Hour = Hour + VarHour
                            Else
                                VarHour = 0
                            End If
                        Next
                    Next
                Next
            ElseIf handlesTurnsChart = False Then
                Hour = PayrollDays * HoursDaily
            End If
        End If

        Return Hour

    End Function


    ''' <summary>
    ''' Calcula el valor de la Incapacidad
    ''' </summary>
    ''' <param name="InitialDateInability">Fecha Inicial Incapacidad</param>
    ''' <param name="EndDateInability">Fecha Fin Incapacidad</param>
    ''' <param name="PayrollInitialDate">Fecha Inicial Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Final Nómina</param>
    ''' <param name="TotalInabilityDays">Días Totales de Incapacidad</param>
    ''' <param name="ValueTotalInability">Valor Total Incapacidad</param>
    ''' <param name="PeriodDays">Días Periodo</param>
    ''' <returns>Valor de la Novedad</returns>
    ''' <remarks></remarks>
    Public Function CalculatingValueInability(ByVal InitialDateInability As Date, ByVal EndDateInability As Date, ByVal PayrollInitialDate As Date, ByVal PayrollEndDate As Date, ByVal TotalInabilityDays As Integer, ByVal ValueTotalInability As Double, ByVal PeriodDays As Integer) As Double Implements ILiquidationDomain.CalculatingValueInability

        Dim ValueDisability As Double

        If DateDiff(DateInterval.Day, InitialDateInability, PayrollEndDate) <= 0 Then
            ValueDisability = (PeriodDays * ValueTotalInability) / TotalInabilityDays
        ElseIf DateDiff(DateInterval.Day, InitialDateInability, PayrollInitialDate) <= 0 Then
            ValueDisability = ValueTotalInability
        Else
            ValueDisability = (PeriodDays * ValueTotalInability) / TotalInabilityDays
        End If

        Return CInt(ValueDisability)

    End Function

    ''' <summary>
    ''' Calcula los días de las Incapacidades
    ''' </summary>
    ''' <param name="InitialDateInability">Fecha Inicial Incapacidad</param>
    ''' <param name="EndDateInability">Fecha Final Incapacidad</param>
    ''' <param name="PayrollInitialDate">Fecha Inicial Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nómina</param>
    ''' <param name="TotalInabilityDays">Días Totales Incapacidad</param>
    ''' <returns>Días de Incapacidad</returns>
    ''' <remarks></remarks>
    Public Function CalculatinDaysInabilities(ByVal InitialDateInability As Date, ByVal EndDateInability As Date, ByVal PayrollInitialDate As Date, ByVal PayrollEndDate As Date, ByVal TotalInabilityDays As Integer) As Integer Implements ILiquidationDomain.CalculatinDaysInabilities

        Dim DaysDisability As Integer
        Dim MonthDays As Integer

        If DateDiff(DateInterval.Day, EndDateInability, PayrollEndDate) <= 0 Then
            If DateDiff(DateInterval.Day, InitialDateInability, PayrollInitialDate) > 0 Then
                DaysDisability = DateDiff(DateInterval.Day, PayrollInitialDate, PayrollEndDate)
            Else
                DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, PayrollEndDate)
            End If
        Else
            If DateDiff(DateInterval.Day, InitialDateInability, PayrollInitialDate) <= 0 Then
                DaysDisability = TotalInabilityDays
            Else
                DaysDisability = DateDiff(DateInterval.Day, PayrollInitialDate, PayrollEndDate)
            End If
        End If

        MonthDays = DateDiff(DateInterval.Day, DateSerial(Year(PayrollInitialDate), Month(PayrollInitialDate), 1), DateSerial(Year(PayrollInitialDate), Month(PayrollInitialDate) + 1, 1))

        If MonthDays = 31 And DateDiff(DateInterval.Day, EndDateInability, PayrollEndDate) <= 0 Then
            DaysDisability = DaysDisability - 1
        End If

        If MonthDays = 28 And DateDiff(DateInterval.Day, EndDateInability, PayrollEndDate) <= 0 Then
            DaysDisability = DaysDisability + 2
        End If

        If MonthDays = 29 And DateDiff(DateInterval.Day, EndDateInability, PayrollEndDate) <= 0 Then
            DaysDisability = DaysDisability + 1
        End If

        If DaysDisability > 30 Then DaysDisability = 30

        Return DaysDisability

    End Function

    ''' <summary>
    ''' Obtiene los Días de la nómina
    ''' </summary>
    ''' <param name="PayrollInitialDate">Fecha Inicial Nómina</param>
    ''' <param name="PayronEndDate">Fecha Final Nómina</param>
    ''' <param name="PayrollMonth">Nómina de 30 días / Días Calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PayrollDays(ByVal PayrollInitialDate As Date, ByVal PayronEndDate As Date, ByVal PayrollMonth As Char) As Integer Implements ILiquidationDomain.PayrollDays

        Dim PayrollDay As Integer

        If PayrollMonth = "1" Then ' Nómina de 30 días
            PayrollDay = Days360(PayrollInitialDate, PayronEndDate)
        Else ' Nómina Días Calendarios
            PayrollDay = DateDiff(DateInterval.Day, PayrollInitialDate, PayronEndDate)
        End If

        Return PayrollDay

    End Function

    ''' <summary>
    ''' Función para Averiguar los Fondos ACTIVOS de un Empleado
    ''' </summary>
    ''' <param name="EmployeeContract">Objeto Contrato Empleado</param>
    ''' <param name="FundType">Tipo de Fondo</param>
    ''' <param name="Voluntary">Voluntario SI - NO</param>
    ''' <returns>Id del Fondo</returns>
    ''' <remarks></remarks>
    Public Function GetFundEmployee(ByVal EmployeeContract As Contract, ByVal FundType As String, ByVal Voluntary As Boolean) As FundContract Implements ILiquidationDomain.GetFundEmployee

        Dim fundContractEmployee As FundContract

        For i As Integer = 0 To (EmployeeContract.FundContract().Count - 1)

            ' Fondo de Pensiones
            If FundType = "Pension" Then
                If EmployeeContract.FundContract.Item(i).Fund.Pension = True And EmployeeContract.FundContract.Item(i).State = True And EmployeeContract.FundContract.Item(i).VoluntaryContribution = Voluntary Then
                    fundContractEmployee = EmployeeContract.FundContract.Item(i)
                End If
            End If
            '' Fondo de Salud
            If FundType = "Health" Then
                If EmployeeContract.FundContract.Item(i).Fund.Health = True And EmployeeContract.FundContract.Item(i).State = True And EmployeeContract.FundContract.Item(i).VoluntaryContribution = Voluntary Then
                    fundContractEmployee = EmployeeContract.FundContract.Item(i)
                End If
            End If

            ' Fondo de Riesgos Profesionales
            If FundType = "Risk" Then
                If EmployeeContract.FundContract.Item(i).Fund.Risk = True And EmployeeContract.FundContract.Item(i).State = True And EmployeeContract.FundContract.Item(i).VoluntaryContribution = Voluntary Then
                    fundContractEmployee = EmployeeContract.FundContract.Item(i)
                End If
            End If

            ' Fondo de Cesantías
            If FundType = "Unemployment" Then
                If EmployeeContract.FundContract.Item(i).Fund.Unemployment = True And EmployeeContract.FundContract.Item(i).State = True And EmployeeContract.FundContract.Item(i).VoluntaryContribution = Voluntary Then
                    fundContractEmployee = EmployeeContract.FundContract.Item(i)
                End If
            End If

            ' Fondo de IIS
            'If FundType = "IIS" Then
            '    If EmployeeContract.FundContract.Item(i).Fund.IIS = True And EmployeeContract.FundContract.Item(i).State = True And EmployeeContract.FundContract.Item(i).VoluntaryContribution = Voluntary Then
            '        fundContractEmployee = EmployeeContract.FundContract.Item(i)
            '    End If
            'End If
        Next


        If fundContractEmployee IsNot Nothing Then
            Return fundContractEmployee
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Obtiene el valor Base de Cálculo de Pensión del Empleado
    ''' </summary>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="LegalSalaryMinimum">Salario Mínimo Legal Vigente</param>
    ''' <param name="VarIBCPension">Valor IBC de Pensión</param>
    ''' <param name="PayrollDays">Días de Nómina</param>
    ''' <param name="UnpaidLicensesDay">Días de Licencias No remuneradas</param>
    ''' <returns>Valor Base de Cálculo de Pénsión</returns>
    ''' <remarks></remarks>
    Public Function GetIBCPension(ByVal EmployeeSalary As Double, ByVal LegalSalaryMinimum As Double, ByVal VarIBCPension As Double, PayrollDays As Integer, ByVal UnpaidLicensesDay As Integer, ByVal AccruedValue As Double, ByVal EmployeePensionPercentage As Double) As Double Implements ILiquidationDomain.GetIBCPension

        Dim BaseIBCPension As Double

        If EmployeeSalary < LegalSalaryMinimum Then
            If (((EmployeeSalary / 30) * PayrollDays) + VarIBCPension) < LegalSalaryMinimum Then
                If AccruedValue - ((((LegalSalaryMinimum / 30) * PayrollDays) + VarIBCPension) * (EmployeePensionPercentage / 100)) <= 0 Then
                    BaseIBCPension = 0
                Else
                    BaseIBCPension = ((((LegalSalaryMinimum / 30) * PayrollDays)) * (EmployeePensionPercentage / 100))
                End If
            Else
                If AccruedValue - ((((LegalSalaryMinimum / 30) * PayrollDays) + VarIBCPension) * (EmployeePensionPercentage / 100)) <= 0 Then
                    BaseIBCPension = 0
                Else
                    BaseIBCPension = ((((LegalSalaryMinimum / 30) * PayrollDays) + VarIBCPension) * (EmployeePensionPercentage / 100))
                End If
            End If
        Else
            If AccruedValue - ((((LegalSalaryMinimum / 30) * PayrollDays) + VarIBCPension) * (EmployeePensionPercentage / 100)) <= 0 Then
                BaseIBCPension = 0
            Else
                BaseIBCPension = (((((EmployeeSalary / 30) * (PayrollDays - UnpaidLicensesDay)) + VarIBCPension) * (EmployeePensionPercentage / 100)) + (((LegalSalaryMinimum / 30) * UnpaidLicensesDay) * (EmployeePensionPercentage / 100)))

            End If
        End If

        Return BaseIBCPension

    End Function

    ''' <summary>
    ''' Obtiene el Valor Base de Cálculo de Salud del Empleado
    ''' </summary>
    ''' <param name="VarIBCHealth">Valor IBC Salud del Empleado</param>
    ''' <param name="LegalSalaryMinimum">Salario Mínimo Legal Vigente</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días de Vacaciones</param>
    ''' <param name="PayrollDays">Días de Nómina</param>
    ''' <param name="EmployeeDays">Días del Empleado</param>
    ''' <returns>Valor Base de Cálculo de Salud</returns>
    ''' <remarks></remarks>
    Public Function GetIBCHealth(ByVal VarIBCHealth As Double, ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal VacationDays As Integer, ByVal PayrollDays As Integer, ByVal EmployeeHealthPercentage As Double, ByVal AccruedValue As Double, ByVal UnpaidLicensesDay As Integer) As Double Implements ILiquidationDomain.GetIBCHealth

        Dim BaseIBCHealth As Double

        If VarIBCHealth < LegalSalaryMinimum Then
            If PayrollDays < 30 Then
                If EmployeeSalary < LegalSalaryMinimum Then
                    If AccruedValue - ((VarIBCHealth / 30) * PayrollDays) * EmployeeHealthPercentage <= 0 Then
                        BaseIBCHealth = 0
                    Else
                        BaseIBCHealth = (((LegalSalaryMinimum / 30) * PayrollDays) * (EmployeeHealthPercentage / 100))
                    End If
                Else
                    If AccruedValue - (VarIBCHealth * EmployeeHealthPercentage) <= 0 Then
                        BaseIBCHealth = 0
                    Else
                        BaseIBCHealth = (VarIBCHealth * (EmployeeHealthPercentage / 100))
                    End If

                End If
            Else
                If AccruedValue - (VarIBCHealth * EmployeeHealthPercentage) <= 0 Then
                    BaseIBCHealth = 0
                Else
                    BaseIBCHealth = (((LegalSalaryMinimum / 30) * PayrollDays) * (EmployeeHealthPercentage / 100))
                End If

            End If
        Else
            If AccruedValue - (VarIBCHealth * (EmployeeHealthPercentage / 100)) <= 0 Then
                BaseIBCHealth = 0
            Else
                BaseIBCHealth = ((VarIBCHealth * (EmployeeHealthPercentage / 100)) + ((LegalSalaryMinimum / 30) * UnpaidLicensesDay) * (EmployeeHealthPercentage / 100))
            End If
        End If

        Return BaseIBCHealth

    End Function

    ''' <summary>
    ''' Obtiene el Valor Base de Salud del Patrono
    ''' </summary>
    ''' <param name="VarIBCHealthEmployer">Valor IBC de Salud del Patrono</param>
    ''' <param name="LegalSalaryMinimum">Salario Mínimo Legal Vigente</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días de Vacaciones</param>
    ''' <param name="PayrollDays">Días de Nómina</param>
    ''' <param name="EmployeeDays">Días del Empleado</param>
    ''' <param name="VarIBCHealthEmployee">Valor IBC de la Salud del Empleado</param>
    ''' <returns>Valor Base de Cálculo de Salud del Patrono</returns>
    ''' <remarks></remarks>
    Public Function GetIBCHealthEmployer(ByVal VarIBCHealthEmployer As Double, ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal VacationDays As Integer, ByVal PayrollDays As Integer, ByVal EmployeeDays As Integer, ByVal VarIBCHealthEmployee As Double, ByVal IBCHealth As Double, EmployerHealthContributionPercentage As Double, TotalDaysLicensesUnpaid As Integer) As Double Implements ILiquidationDomain.GetIBCHealthEmployer

        Dim BaseIBCHealthEmployer As Double

        If VarIBCHealthEmployee < LegalSalaryMinimum Then
            If (VarIBCHealthEmployee / PayrollDays) > (LegalSalaryMinimum / 30) Then
                BaseIBCHealthEmployer = VarIBCHealthEmployee
            Else
                BaseIBCHealthEmployer = (((LegalSalaryMinimum / 30) * PayrollDays))
            End If
        Else
            BaseIBCHealthEmployer = ((VarIBCHealthEmployee + (((LegalSalaryMinimum / 30) * TotalDaysLicensesUnpaid))))
        End If

        If TotalDaysLicensesUnpaid = 30 Then BaseIBCHealthEmployer = 0

        Return BaseIBCHealthEmployer

    End Function

    ''' <summary>
    ''' Obtiene el Valor Base de Pensión del Patrono
    ''' </summary>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="LegalSalaryMinimum">Salario Legal Mínimo Vigente</param>
    ''' <param name="PayrollDays">Días de la Nómina</param>
    ''' <param name="UnpaidLicensesDay">Días de Licencias</param>
    ''' <param name="EmployeeDays">Días del Empleado</param>
    ''' <param name="VarIBCPensionEmployee">Valor IBC Pensión del Empleado</param>
    ''' <returns>Valor Base de Cálculo de Pensión del Patrono</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function GetIBCPensionEmployer(ByVal PensionIBC As Double, ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal UnpaidLicensesDay As Integer, PayrollDays As Integer, EmployerPensionContributionPercentage As Double) As Double Implements ILiquidationDomain.GetIBCPensionEmployer

        Dim BaseIBCPensionEmployer As Double

        If EmployeeSalary < LegalSalaryMinimum Then
            If (((EmployeeSalary / 30) * PayrollDays) + PensionIBC) < LegalSalaryMinimum Then
                BaseIBCPensionEmployer = ((((LegalSalaryMinimum / 30) * (PayrollDays - UnpaidLicensesDay))) * (EmployerPensionContributionPercentage / 100)) + (((LegalSalaryMinimum / 30) * UnpaidLicensesDay) * (EmployerPensionContributionPercentage / 100))
            Else
                BaseIBCPensionEmployer = (((((EmployeeSalary / 30) * (PayrollDays - UnpaidLicensesDay)) + PensionIBC) * (EmployerPensionContributionPercentage / 100)) + (((LegalSalaryMinimum / 30) * UnpaidLicensesDay) * (EmployerPensionContributionPercentage / 100)))
            End If
        Else
            BaseIBCPensionEmployer = (((((EmployeeSalary / 30) * (PayrollDays - UnpaidLicensesDay)) + PensionIBC) * (EmployerPensionContributionPercentage / 100)) + (((LegalSalaryMinimum / 30) * UnpaidLicensesDay) * (EmployerPensionContributionPercentage / 100)))
        End If

        Return BaseIBCPensionEmployer

    End Function

    ' Concepto 903
    Public Function GetBaseARP(ByVal EmployeeSalary As Double, ByVal LegalSalaryMinimun As Double, ByVal ARPDays As Integer, ByVal IBCArp As Double, ByVal ProfessionalRiskPercentage As Double) As Double Implements ILiquidationDomain.GetBaseARP

        Dim BaseIBCArp As Double

        If EmployeeSalary < LegalSalaryMinimun Then
            If (((EmployeeSalary / 30) * ARPDays) + IBCArp) < LegalSalaryMinimun Then
                BaseIBCArp = ((((LegalSalaryMinimun / 30) * ARPDays)) * (ProfessionalRiskPercentage / 100))
            Else
                BaseIBCArp = ((((EmployeeSalary / 30) * ARPDays) + IBCArp) * (ProfessionalRiskPercentage / 100))
            End If
        Else
            BaseIBCArp = ((((EmployeeSalary / 30) * ARPDays) + IBCArp) * (ProfessionalRiskPercentage / 100))
        End If

        Return BaseIBCArp

    End Function

    ' Concepto 904
    Public Function GetBaseParafiscalCompensationFund(ByVal IBCFundValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, ByVal CompensationFundContributionPercentage As Double) As Double Implements ILiquidationDomain.GetBaseParafiscalCompensationFund

        Dim BaseParafiscalCompensationFund As Double

        If (IBCFundValue / PayrollDays) >= (LegalSalaryMinimun / 30) Then
            BaseParafiscalCompensationFund = IBCFundValue * (CompensationFundContributionPercentage / 100)
        Else
            BaseParafiscalCompensationFund = (((LegalSalaryMinimun / 30) * PayrollDays) * (CompensationFundContributionPercentage / 100))
        End If

        ' Cuando existen novedades en días de Incapacidades o Licencias
        If PaidLicensesDays > 0 Or UnpaidLicensesDays > 0 Or HospitalInabilitiesDays > 0 Or InabilitiesPeriodDays > 0 Then
            If (EmployeeSalary < LegalSalaryMinimun) Then
                BaseParafiscalCompensationFund = (((LegalSalaryMinimun / 30) * (PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays)) * (CompensationFundContributionPercentage / 100))
            Else
                BaseParafiscalCompensationFund = IBCFundValue * (CompensationFundContributionPercentage / 100)
            End If
        End If

        If UnpaidLicensesDays = 30 Then
            BaseParafiscalCompensationFund = 0
        End If

        If VacationDays > 30 Then
            BaseParafiscalCompensationFund = (((EmployeeSalary) / 30 * ((PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays))) * (CompensationFundContributionPercentage / 100))
        End If

        Return BaseParafiscalCompensationFund

    End Function

    'Concepto 905
    Public Function GetBaseParafiscalICBF(ByVal IBCICBFValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, ICBFContributionPercentage As Double) As Double Implements ILiquidationDomain.GetBaseParafiscalICBF

        Dim BaseParafiscalICBF As Double

        If (IBCICBFValue / PayrollDays) >= (LegalSalaryMinimun / 30) Then
            BaseParafiscalICBF = IBCICBFValue * (ICBFContributionPercentage / 100)
        Else
            BaseParafiscalICBF = ((LegalSalaryMinimun / 30) * PayrollDays) * (ICBFContributionPercentage / 100)
        End If

        ' Cuando existen novedades en días de Incapacidades o Licencias
        If PaidLicensesDays > 0 Or UnpaidLicensesDays > 0 Or HospitalInabilitiesDays > 0 Or InabilitiesPeriodDays > 0 Then
            If (EmployeeSalary < LegalSalaryMinimun) Then
                BaseParafiscalICBF = ((LegalSalaryMinimun / 30) * (PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays)) * (ICBFContributionPercentage / 100)
            Else
                BaseParafiscalICBF = IBCICBFValue * (ICBFContributionPercentage / 100)
            End If
        End If

        If UnpaidLicensesDays = 30 Then
            BaseParafiscalICBF = 0
        End If

        If VacationDays > 30 Then
            BaseParafiscalICBF = ((EmployeeSalary) / 30 * ((PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays))) * (ICBFContributionPercentage / 100)
        End If

        Return BaseParafiscalICBF

    End Function


    Public Function EmployeeLiquitadion(payrollParameter As PayrollParameter, payrollEmployee As List(Of Contract), employeeAutorizationConcept As List(Of AuthorizationConcept), ScheduleEmployee As ILiquidationDomain.GetScheduleDetailByEmployeeFuctionalUnitBetweenDateWithoutNovelties, Novelties As ILiquidationDomain.GetNoveltyByEmployeeDateInitialEnd, PayrollConfirmada As String, payrollEndDate As Date, PayrollMonth As String, PayrollStarDate As Date, PayrollLiquidation As Byte, EmployeeLiquidation As ILiquidationDomain.EmployeePayrollCheckLiquidation, Retention As Retention, valida As ILiquidationDomain.GetPayrollValidation, VacationEmployee As ILiquidationDomain.GetEmployeeVacation, IncentivePayment As ILiquidationDomain.GetIncentivePaymentByContractIdPayrollNextDate, unemployedEmployee As ILiquidationDomain.GetUnemployedLiquidationByContractIdInterestPayDay, completePayroll As Boolean) As List(Of Liquidation) Implements ILiquidationDomain.EmployeeLiquitadion


        Dim ListLiquidation As New List(Of Liquidation)
        ' ScheduleEmployee As List(Of Schedule)
        Dim SenaContributionPercentage As Double ' Porcentaje de Aporte SENA
        Dim ICBFContributionPercentage As Double ' Porcentaje de Aporte ICBF
        Dim CompensationFundContributionPercentage As Double ' Porcentaje de Aporte de Caja de Compensación
        Dim legalMinimimunSalary As Double ' Salario Mínimo Legal Vigente
        Dim transportHelpValue As Double ' Auxilio de Transporte
        Dim EmployeePensionContributionPercentage As Double ' Porcentaje de Aporte Pensión Empleado
        Dim EmployeeHealthContributionPercentage As Double ' Porcentaje de Aporte Salud Empleado
        Dim EmployerPensionContributionPercentage As String ' Porcentaje de Aporte Pensión Empleador
        Dim EmployerHealthContributionPercentage As Double ' Porcentaje de aporte Salud Empleador

        Dim BasicSalary As Double ' Salario Básico Empleado
        Dim IBCHour As Double = 0
        Dim EmployeeId As Integer ' ID del Empleado
        Dim PayrollDays As Integer ' Días Nómina
        Dim DaysWorkedEmployee As Integer ' Días Trabajados Empleado
        Dim WorkHours As Integer ' Horas Trabajadas
        Dim EmployeeLiquidated As Liquidation
        Dim EmployeeLiquidatedDetail As LiquidationDetail
        Dim MessageLiquitadion As Message
        Dim ConceptoDevengadoSuma As Double = 0 ' Sumatoria de Devengado
        Dim ConceptoDeducidoSuma As Double = 0 ' Sumatoria de Deducido

        'Variables de Incapacidades

        Dim TotalPeriodDaysInabilities As Integer ' Dias Total de Incapacidades en el Periodo
        Dim TotalPeriodValuesInabilities As Integer ' Valor Total de Incapacidades en el Periodo
        Dim TotalDaysInabilities As Integer ' Días Total de Incapacidades
        Dim TotalValuesInabilities As Double ' Valor Total de Incapacidades
        Dim TotalValuesAmbulatoryInabilitiy As Double ' Valor Total de Incapacidades Ambulatorias
        Dim TotalDaysAmbulatoryInability As Integer ' Total Días de Incapacidad Ambulatoria
        Dim TotalValueMaternity As Double ' Valor Total de Maternidad
        Dim TotalDaysMaternity As Integer ' Total Días de Maternidad
        Dim TotalValueLicensesRemunerated As Double ' Valor Total De Licencias Remuneradas
        Dim TotalDaysLicensesRemunerated As Integer ' Total Días De Licencias Remuneradas
        Dim TotalValueLicensesUnpaid As Double ' Valor Total De Licencias No Remuneradas
        Dim TotalDaysLicensesUnpaid As Integer ' Total Días De Licencias No Remuneradas
        Dim TotalValueSanctions As Double ' Valor Total De Sanciones
        Dim TotalDaysSanctions As Integer ' Total Días De Sanciones
        Dim TotalValueInabilityARP As Double ' Valor Total De Incapacidades Riesgos Profesionales
        Dim TotalDaysInabilityARP As Integer ' Total Días De Incapacidades Riesgos Profesionales
        Dim TotalDaysPermit As Integer ' Total Días De Permiso
        Dim TotalValuePermit As Double ' Valor Total De Permisos
        Dim TotalValueHospitalInability As Double ' Valor Total De Incapacidades Hospitalarias
        Dim TotalDaysHospitalInability As Double ' Total Días De Incapacidades Hospitalarias

        Dim AmbulatoryInabilityDays As Integer ' Día Inicial Incapacidad Ambulatoria
        Dim ValueAmbulatoryInability As Double ' Valor Incapacidad Ambulatoria

        Dim HospitalInabilityDays As Integer
        Dim ValueHospitalInability As Double

        Dim MaternityDays As Integer
        Dim ValueMaternity As Double

        Dim SanctionsDays As Integer
        Dim ValueSanction As Double

        Dim UnpaidLicensesDays As Integer
        Dim ValueUnpaidLicenses As Double

        Dim ValueRemuneratedLicenses As Double
        Dim RemuneratedLicensesDays As Integer

        Dim ValueProfesionalInabilities As Double
        Dim ProfesionalInabilitiesDays As Integer

        Dim PensionDays As Integer
        Dim HealthDays As Integer
        Dim ARPDays As Integer
        Dim CompensationFundDays As Integer
        Dim WorkDays As Integer
        Dim ProvisionDays As Integer


        ' Variables Temporales de Cálculo
        Dim IBCSENA As Double = 0 'ibc sena
        Dim IBCRTF As Double = 0 'ibc rtf
        Dim IBCICBF As Double = 0 'dias icbf
        Dim IBCSeverance As Double = 0 'ibc cesantias
        Dim IBCCompensationFund As Double = 0 'ibc caja de Compensación
        Dim IBCHealth As Double = 0 'ibc salud
        Dim IBCPension As Double = 0 'ibc pension
        Dim IBCARP As Double = 0 'ibc arp
        Dim IBCPeriod As Double = 0 ' ibc Periodo
        Dim TotalAccrued As Double = 0 ' Total Devengado
        Dim TotalDeducted As Double = 0 ' Todal Deducido
        Dim HoursSchedule As Integer = 0 ' Horas de Cuadro de Turno 
        Dim IncentivePaymentServices As Double = 0 ' variable prima de servicios
        Dim IncetivePaymentServicesExtra As Double = 0 ' variable para otras nummaxpri 
        Dim BonusServices As Double = 0 ' bonificacion x servicios
        Dim Salary As Double = 0 ' Salario
        Dim AcumulatedHelpTransport As Double = 0 ' Acumulado Auxilio de Transporte
        Dim Compensation As Double = 0 ' Indemnizaciones
        Dim Unemployment As Double = 0 ' Cesantías
        Dim RiskContribution As Double = 0 ' Aporte Riesgos
        Dim OtherAccrued As Double = 0 ' Otros Devengados
        Dim OtherDeducted As Double = 0 ' Otros Deducidos
        Dim EveningOvertime As Double = 0 ' Horas Extras Nocturnas
        Dim SundayOvertime As Double = 0 ' Horas Extras Dominicales
        Dim PensionEmployee As Double = 0 ' Pensión Empleado
        Dim PensionEmployer As Double = 0 ' Pensión Patrono
        Dim VoluntaryPension As Double = 0 ' Pensión Voluntaria
        Dim HealthEmployee As Double = 0 ' Salud Empleado
        Dim HealthEmployer As Double = 0 ' Salud Patrono
        Dim VoluntaryHealth As Double = 0 ' Salud Voluntaria
        Dim Withholding As Double = 0 ' Retención en la fuente
        Dim AmbulatoryInability As Double = 0 ' Incapacidad Ambulatoria
        Dim HospitalInability As Double = 0 ' Incapacidad Hospitalaria
        Dim Maternity As Double = 0 ' Maternidad
        Dim Licenses As Double = 0 ' Licencias
        Dim UnpaidLicenses As Double = 0 ' Licencias No remuneradas
        Dim Sanctions As Double = 0 ' Sanciones
        Dim InabilityProfessionalRisk As Double = 0 ' Incapacidad Riesgos Profesionales
        Dim Permission As Double = 0 ' Permisos
        Dim Vacation As Double = 0 ' Vacaciones
        Dim ProvisionVacation As Double = 0 ' Provisión Vacaciones
        Dim MaxNumberIncentiveProvision As Double = 0 ' Provisión Número Máximo de Primas
        Dim UnemploymentInterestsProvision As Double = 0 ' Provisión Intereses de Cesantías
        Dim SenaProvision As Double = 0 ' Provisión Sena
        Dim CompensationFundProvision As Double = 0 ' Provisión Caja de Compensación
        Dim ICBFProvision As Double = 0 ' Provisión ICBF
        Dim ContributionPensionSolidarityFund As Double = 0 ' Aporte Fondo de Solidaridad Pensional
        Dim StudyDeducted As Double = 0 ' Deducciones por Estudio
        Dim HousingDeducted As Double = 0 ' Deducciones por vivienda
        Dim AccruedValueConcept As Double = 0 ' Valor de Concepto Devengado
        Dim DeductedValueConcept As Double = 0 ' Valor Concepto Deducido

        Dim MaximunDiscountPercentage As Double = 0
        Dim EducationStudyDiscountPercentage As Double = 0
        Dim HousingDeductionMaximumValue As Double = 0

        Dim CountWorkHours As Integer = 0


        Dim HourSchedule As Double = 0

        Dim RTFExcempt As Double

        Dim RetentionValue As Double 'valor retencion
        Dim PercentageRetention As Double ' Porcentaje de Retención
        Dim RetentionBase As Double


        Dim AmbulatoryInabilityInitialDate As Date
        Dim AmbulatoryInabilityEndDate As Date
        Dim AutorizationNumberAmbulatoryInability As String

        Dim HospitalInabilityInitialDate As Date
        Dim HospitalInabilityEndDate As Date
        Dim AutorizationNumberHospitalInability As String

        Dim MaternityInitialDate As Date
        Dim MaternityEndDate As Date
        Dim AutorizationNumberMaternity As String

        Dim UnpaidLicensesInitialDate As Date
        Dim UnpaidLicensesEndDate As Date
        Dim AutorizationNumberUnpaidLicenses As String

        Dim SanctionsInitialDate As Date
        Dim SanctionsEndDate As Date


        Dim InabilityARPInitialDate As Date
        Dim InabilityARPEndDate As Date
        Dim AutorizationNumberInabilityARP As String

        Dim PaidLicensesInitialDate As Date
        Dim PaidLicensesEndDate As Date

        Dim BaseIBCHealth As Double

        Dim IBCVacation As Double = 0 ' ibc Vacaciones
        Dim IBCIncentivePayment As Double = 0 ' ibc Primas

        'Cargo Datos de los Parámetros de Nómina
        legalMinimimunSalary = CDbl(payrollParameter.LegalSalaryMinimum)
        transportHelpValue = payrollParameter.TransportHelpValue
        EmployeePensionContributionPercentage = CDbl(payrollParameter.EmployeePensionContributionPercentage)
        EmployeeHealthContributionPercentage = CDbl(payrollParameter.EmployeeHealthContributionPercentage)
        EmployerPensionContributionPercentage = CDbl(payrollParameter.EmployerPensionContributionPercentage)
        'EmployerPensionContributionPercentage = "12"
        EmployerHealthContributionPercentage = CDbl(payrollParameter.EmployerHealthContributionPercentage)
        SenaContributionPercentage = CDbl(payrollParameter.SenaContributionPercentage)
        ICBFContributionPercentage = CDbl(payrollParameter.ICBFContributionPercentage)
        CompensationFundContributionPercentage = CDbl(payrollParameter.CompensationFundContributionPercentage)
        RTFExcempt = CDbl(payrollParameter.RTFExemptPercentage)
        MaximunDiscountPercentage = CDbl(payrollParameter.MaximunDiscountPercentage)
        EducationStudyDiscountPercentage = CDbl(payrollParameter.EducationHealthDiscountPercentage)
        HousingDeductionMaximumValue = CDbl(payrollParameter.HousingDeductionMaximumValue)

        Dim minHourAmount As Integer = 0
        Dim maxHourAmount As Integer = 0

        Dim Contador As Integer = 0
        Dim Aux As Integer

        Dim InitialDateEval As Date
        Dim EndingDateEval As Date

        Dim employeeNovelty As New List(Of Novelty)

        Dim ContractNumber As Integer
        Dim ContadorContratos As Integer

        Dim TrialPeriodTime As Integer

        Dim ListVacationEmployee As New List(Of Vacation)

        Dim VacationDays As Integer
        Dim VacationValue As Double
        Dim VacationValueCash As Double

        Dim VacationHealthEmployee As Double
        Dim VacationHealthEmployer As Double
        Dim VacationPensionEmployee As Double
        Dim VacationPensionEmployer As Double
        Dim TotalWorkDays As Integer

        Dim incentiePaymentValue As Double
        Dim unemployedInterestValue As Double

        Dim PayrollCheckLiquidation As New List(Of Liquidation)

        Dim IBCSeveranceNoSanctions As Double ' IBC Cesantias SIN sanciones

        Dim WorkCenterId As Integer

        ListVacationEmployee = VacationEmployee(PayrollStarDate, 1)

        For i As Integer = 0 To (payrollEmployee.Count() - 1)

            Dim AuxCount As Integer

            Dim EmployeeValid = valida(payrollEmployee.Item(i).EmployeeId, PayrollLiquidation, payrollEndDate)

            ContadorContratos = ListLiquidation.Where(Function(x) x.EmployeeId = payrollEmployee.Item(i).EmployeeId).Count()

            If ContadorContratos > 0 Then
                Continue For
            End If

            Dim ListPayrollAux As New List(Of Contract)
            Dim AuxContador As Integer = 0

            Dim ScheduleDetailEmployee As New List(Of ScheduleDetail)

            If EmployeeValid = True Then
                Contador = 0
                CountWorkHours = 0

                EmployeeLiquidated = New Liquidation()
                MessageLiquitadion = New Message()

                ' Días de Nómina
                PayrollDays = Me.PayrollDays(PayrollStarDate, payrollEndDate, PayrollMonth)

                ' Averiguo los Fondos de los Empleados y los días de Afiliación
                ' PENSIÓN
                Dim PensionFundContract As FundContract
                Dim PensionFundId As Integer = Nothing
                Dim AfiliationPensionDays As Integer = Nothing

                PensionFundContract = Me.GetFundEmployee(payrollEmployee.Item(i), "Pension", False)

                If PensionFundContract IsNot Nothing Then
                    PensionFundId = PensionFundContract.FundId
                    AfiliationPensionDays = DateDiff(DateInterval.Day, PensionFundContract.InitialDate, payrollEndDate) + 1
                End If

                ' PENSIÓN VOLUNTARIA
                Dim PensionVoluntaryFundContract As FundContract
                Dim PensionVoluntaryFundId As Integer = Nothing

                PensionVoluntaryFundContract = Me.GetFundEmployee(payrollEmployee.Item(i), "Pension", True)

                If PensionVoluntaryFundContract IsNot Nothing Then
                    PensionVoluntaryFundId = PensionVoluntaryFundContract.FundId
                End If

                ' SALUD
                Dim HealthFundContract As FundContract
                Dim HealthFundId As Integer = Nothing
                Dim AfiliationHealthDays As Integer = Nothing

                HealthFundContract = Me.GetFundEmployee(payrollEmployee.Item(i), "Health", False)

                If HealthFundContract IsNot Nothing Then
                    HealthFundId = HealthFundContract.FundId
                    AfiliationHealthDays = DateDiff(DateInterval.Day, HealthFundContract.InitialDate, payrollEndDate) + 1
                End If

                ' SALUD VOLUNTARIA
                Dim HealthVoluntaryFundContract As FundContract
                Dim HealthVoluntaryFundId As Integer = Nothing

                HealthVoluntaryFundContract = Me.GetFundEmployee(payrollEmployee.Item(i), "Health", True)

                If HealthVoluntaryFundContract IsNot Nothing Then
                    HealthVoluntaryFundId = HealthVoluntaryFundContract.FundId
                End If

                ' RIESGOS PROFESIONALES
                Dim RiskFundContract As FundContract
                Dim RiskFundId As Integer = Nothing

                RiskFundContract = Me.GetFundEmployee(payrollEmployee.Item(i), "Risk", False)

                If RiskFundContract IsNot Nothing Then
                    RiskFundId = RiskFundContract.FundId
                End If

                ' Reviso si el Empleado tiene Conceptos Autorizados
                If employeeAutorizationConcept.Count() <= 0 Then
                    MessageLiquitadion = CreateMessage("El empleado NO tiene Conceptos Autorizados", True, payrollEndDate)
                    EmployeeLiquidated.Message.Add(MessageLiquitadion)
                End If


                ' If AuxCount = 0 Then
                AuxCount = payrollEmployee.Where(Function(x) x.EmployeeId = payrollEmployee.Item(i).EmployeeId).Count()
                'End If


                If AuxCount > 1 Then
                    ListPayrollAux = payrollEmployee.Where(Function(x) x.EmployeeId = payrollEmployee.Item(i).EmployeeId).ToList()

                    For List As Integer = 0 To ListPayrollAux.Count() - 2
                        If (ListPayrollAux.Item(List).ContractInitialDate = ListPayrollAux.Item(List + 1).ContractInitialDate) And (ListPayrollAux.Item(List).ContractEndingDate = ListPayrollAux.Item(List + 1).ContractEndingDate) Then
                            ListPayrollAux = ListPayrollAux.Where(Function(x) x.Valid = True).ToList()
                            AuxContador = 0
                        End If
                    Next

                Else
                    ListPayrollAux.Add(payrollEmployee.Item(i))
                    AuxContador = 0
                End If


                For Aux = 0 To ListPayrollAux.Count() - 1

                    ' Significa que el Empleado tiene Más de un Contrato en el Periodo de la nómina
                    If AuxContador > 1 Then
                        AuxContador = AuxContador + 1
                    Else
                        AuxContador = 0
                        'ListPayrollAux.Add(payrollEmployee.Item(i))
                    End If

                    ContractNumber = ListPayrollAux.Item(Aux).Id

                    IBCHour = 8
                    BasicSalary = ListPayrollAux.Item(Aux).BasicSalary
                    EmployeeId = ListPayrollAux.Item(Aux).EmployeeId

                    HousingDeducted = ListPayrollAux.Item(Aux).Employee.HousingDeductionValue
                    StudyDeducted = ListPayrollAux.Item(Aux).Employee.EducationDeductionValue

                    'Cargo los Datos de los Cargos
                    minHourAmount = ListPayrollAux.Item(Aux).Position.MinHourAmount
                    maxHourAmount = ListPayrollAux.Item(Aux).Position.MaxHourAmount
                    Dim MinBasicSalary = ListPayrollAux.Item(Aux).Position.MinBasicSalary
                    Dim MaxBasicSalary = ListPayrollAux.Item(Aux).Position.MaxBasicSalary
                    Dim HandlesTurnChart = ListPayrollAux.Item(Aux).Position.HandlesTurnsChart
                    Dim ProfessionalRiskPercentage = ListPayrollAux.Item(Aux).Employee.ProfessionalRiskPercentage
                    Dim ContractInitialDate = ListPayrollAux.Item(Aux).ContractInitialDate
                    Dim ContractEndingDate = ListPayrollAux.Item(Aux).ContractEndingDate

                    ''Valido Auxilio de Transporte
                    If (BasicSalary > (legalMinimimunSalary * 2)) Or ((IBCHour <= 4) And (BasicSalary * 2) > (legalMinimimunSalary * 2)) Then
                        payrollParameter.TransportHelpValue = 0

                        MessageLiquitadion = CreateMessage("No se paga Auxilio de Transporte puesto que gana más de 2 salarios mínimos", False, payrollEndDate)
                        EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    Else
                        payrollParameter.TransportHelpValue = transportHelpValue
                    End If


                    ' Valido el Periodo de Prueba
                    If ListPayrollAux.Item(Aux).TrialPeriod = True Then
                        TrialPeriodTime = ListPayrollAux.Item(Aux).TrialPeriodTime
                        Dim AuxDate As Date
                        Dim DaysTrialPeriod As Integer
                        Dim BasicSalaryTrial As Double
                        Dim TrialPeriodSalaryPercentage As Double = ListPayrollAux.Item(Aux).TrialPeriodSalaryPercentage
                        Dim EndingDateTrialPeriod As Date = DateAdd(DateInterval.Day, TrialPeriodTime, ListPayrollAux.Item(Aux).ContractInitialDate)


                        If EndingDateTrialPeriod <= payrollEndDate Then
                            AuxDate = DateAdd(DateInterval.Day, TrialPeriodTime, ListPayrollAux.Item(Aux).ContractInitialDate)

                            If AuxDate >= PayrollStarDate Then

                                DaysTrialPeriod = DateDiff(DateInterval.Day, PayrollStarDate, AuxDate)
                                Dim Sueldo1 = BasicSalary - (BasicSalary * (TrialPeriodSalaryPercentage / 100))
                                Sueldo1 = (Sueldo1 * DaysTrialPeriod) / 30

                                Dim DiasSueldoNormal = PayrollDays - DaysTrialPeriod
                                'BasicSalary = (BasicSalary * DiasSueldoNormal) / 30

                                BasicSalary = BasicSalary - Sueldo1

                            End If
                        End If

                    End If

                    ' Verifico que el Empleado Maneje Cuadro de Turnos
                    If HandlesTurnChart = False Then
                        MessageLiquitadion = CreateMessage("El Cargo del Empleado (" & payrollEmployee.Item(i).Position.Name & ") NO maneja Cuadro de Turnos", False, payrollEndDate)
                        EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    End If


                    If AuxContador = 0 Then
                        If ContractInitialDate <= PayrollStarDate Then
                            InitialDateEval = PayrollStarDate
                        Else
                            InitialDateEval = ContractInitialDate
                        End If

                        If ContractEndingDate >= payrollEndDate Then
                            EndingDateEval = payrollEndDate
                        Else
                            EndingDateEval = ContractEndingDate
                        End If
                        'InitialDateEval = PayrollStarDate
                        'EndingDateEval = payrollEndDate
                    ElseIf AuxContador = 1 Then
                        If ContractInitialDate <= PayrollStarDate Then
                            InitialDateEval = PayrollStarDate
                        Else
                            InitialDateEval = ContractInitialDate
                        End If
                        'InitialDateEval = PayrollStarDate
                        If ListPayrollAux.Item(Aux).ContractType.Undefined = True Then ' Es porque es contrato a término Indefinido
                            EndingDateEval = ListPayrollAux.Item(Aux + 1).ContractInitialDate.AddDays(-1)
                        Else
                            EndingDateEval = ListPayrollAux.Item(Aux + 1).ContractInitialDate.AddDays(-1)
                        End If
                        'EndingDateEval = ListPayrollAux.Item(Aux).ContractEndingDate
                    ElseIf AuxContador = 2 Then
                        InitialDateEval = ListPayrollAux.Item(Aux).ContractInitialDate
                        EndingDateEval = payrollEndDate
                    End If


                    'Limpio las variables de las Incapacidades
                    Dim CountAmbulatoryInabilities As Integer

                    ' Busco si hay primas para este empleado y que se paguen en esta nómina
                    Dim IncentivePaymentContract = IncentivePayment(ListPayrollAux.Item(Aux).Id, PayrollStarDate)

                    If IncentivePaymentContract IsNot Nothing Then
                        incentiePaymentValue = IncentivePaymentContract.PaidValue
                    End If

                    'Busco si hay intereses de cesantías para pagar
                    Dim UnemployedInterestEmployed = unemployedEmployee(ListPayrollAux.Item(Aux).Id, PayrollStarDate)
                    If UnemployedInterestEmployed IsNot Nothing Then
                        unemployedInterestValue = UnemployedInterestEmployed.UnemployedInterestTotal
                    End If

                    'Cargo las novedades del Empleado del Periodo

                    employeeNovelty = Novelties(EmployeeId, InitialDateEval, EndingDateEval)

                    If employeeNovelty.Count() > 0 Then

                        'INCAPACIDAD AMBULATORIA
                        Dim EmployeeNoveltyAmbulatoryInability = employeeNovelty.Where(Function(x) x.TypeNovelty = 1 And x.InabilityClass = 1)
                        Parallel.For(0, EmployeeNoveltyAmbulatoryInability.Count() - 1, Sub(AmbInab)

                                                                                            TotalDaysAmbulatoryInability = TotalDaysAmbulatoryInability + EmployeeNoveltyAmbulatoryInability(AmbInab).Days

                                                                                            If EmployeeNoveltyAmbulatoryInability(AmbInab).Value IsNot Nothing Then
                                                                                                TotalValuesAmbulatoryInabilitiy = TotalValuesAmbulatoryInabilitiy + EmployeeNoveltyAmbulatoryInability(AmbInab).Value
                                                                                                CountAmbulatoryInabilities = CountAmbulatoryInabilities + 1
                                                                                            End If

                                                                                            AmbulatoryInabilityInitialDate = EmployeeNoveltyAmbulatoryInability(AmbInab).RealDate
                                                                                            AmbulatoryInabilityEndDate = EmployeeNoveltyAmbulatoryInability(AmbInab).EndDate
                                                                                            AutorizationNumberAmbulatoryInability = EmployeeNoveltyAmbulatoryInability(AmbInab).AutorizationNumber

                                                                                            AmbulatoryInabilityDays = AmbulatoryInabilityDays + Me.CalculatinDaysInabilities(EmployeeNoveltyAmbulatoryInability(AmbInab).RealDate, EmployeeNoveltyAmbulatoryInability(AmbInab).EndDate, PayrollStarDate, payrollEndDate, TotalDaysAmbulatoryInability)
                                                                                            ValueAmbulatoryInability = Me.CalculatingValueInability(EmployeeNoveltyAmbulatoryInability(AmbInab).RealDate, EmployeeNoveltyAmbulatoryInability(AmbInab).EndDate, PayrollStarDate, payrollEndDate, TotalDaysAmbulatoryInability, TotalValuesAmbulatoryInabilitiy, AmbulatoryInabilityDays)

                                                                                            MessageLiquitadion = CreateMessage("Existe registrada una Incapacidad Ambulatoria en el Periodo de la Nómina registrada entre el día " & AmbulatoryInabilityInitialDate & " y " & AmbulatoryInabilityEndDate, False, payrollEndDate)
                                                                                            EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                                                                        End Sub
                        )

                        ' INCAPACIDAD HOSPITALARIA
                        Dim EmployeeNoveltyHospitalInability = employeeNovelty.Where(Function(x) x.TypeNovelty = 1 And x.InabilityClass = 2)
                        Parallel.For(0, EmployeeNoveltyHospitalInability.Count() - 1, Sub(HospInab)

                                                                                          TotalDaysHospitalInability = TotalDaysHospitalInability + EmployeeNoveltyHospitalInability(HospInab).Days ' Total Días de Incapacidad Hospitalaria
                                                                                          If EmployeeNoveltyHospitalInability(HospInab).Value IsNot Nothing Then
                                                                                              TotalValueHospitalInability = TotalValueHospitalInability + EmployeeNoveltyHospitalInability(HospInab).Value
                                                                                          End If

                                                                                          HospitalInabilityInitialDate = EmployeeNoveltyHospitalInability(HospInab).RealDate
                                                                                          HospitalInabilityEndDate = EmployeeNoveltyHospitalInability(HospInab).EndDate
                                                                                          AutorizationNumberHospitalInability = EmployeeNoveltyHospitalInability(HospInab).AutorizationNumber

                                                                                          HospitalInabilityDays = HospitalInabilityDays + Me.CalculatinDaysInabilities(EmployeeNoveltyHospitalInability(HospInab).RealDate, EmployeeNoveltyHospitalInability(HospInab).EndDate, PayrollStarDate, payrollEndDate, TotalDaysHospitalInability)
                                                                                          ValueHospitalInability = Me.CalculatingValueInability(EmployeeNoveltyHospitalInability(HospInab).RealDate, EmployeeNoveltyHospitalInability(HospInab).EndDate, PayrollStarDate, payrollEndDate, TotalDaysHospitalInability, TotalValueHospitalInability, HospitalInabilityDays)

                                                                                          MessageLiquitadion = CreateMessage("Existe registrada una Incapacidad Hospitalaria en el Periodo de la Nómina registrada entre el día " & HospitalInabilityInitialDate & " y " & HospitalInabilityEndDate, False, payrollEndDate)
                                                                                          EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                                                                      End Sub
                        )

                        ' MATERNIDAD
                        Dim EmployeeNoveltyMaternity = employeeNovelty.Where(Function(x) x.TypeNovelty = 1 And x.InabilityClass = 3)
                        Parallel.For(0, EmployeeNoveltyMaternity.Count() - 1, Sub(Matern)

                                                                                  TotalDaysMaternity = TotalDaysMaternity + EmployeeNoveltyMaternity(Matern).Days ' Total Días Maternidad

                                                                                  MaternityInitialDate = EmployeeNoveltyMaternity(Matern).RealDate
                                                                                  MaternityEndDate = EmployeeNoveltyMaternity(Matern).EndDate
                                                                                  AutorizationNumberMaternity = EmployeeNoveltyMaternity(Matern).AutorizationNumber

                                                                                  MaternityDays = MaternityDays + Me.CalculatinDaysInabilities(EmployeeNoveltyMaternity(Matern).RealDate, EmployeeNoveltyMaternity(Matern).EndDate, PayrollStarDate, payrollEndDate, TotalDaysMaternity)
                                                                                  ValueMaternity = Me.CalculatingValueInability(EmployeeNoveltyMaternity(Matern).RealDate, EmployeeNoveltyMaternity(Matern).EndDate, PayrollStarDate, payrollEndDate, TotalDaysMaternity, 0, MaternityDays)

                                                                                  MessageLiquitadion = CreateMessage("Existe registrada una Licencia de Maternidad en el Periodo de la Nómina registrada entre el día " & MaternityInitialDate & " y " & MaternityEndDate, False, payrollEndDate)
                                                                                  EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                                                              End Sub
                       )

                        'ENFERMEDAD PROFESIONAL
                        Dim EmployeeNoveltyARL = employeeNovelty.Where(Function(x) x.TypeNovelty = 1 And x.InabilityClass = 4)
                        Parallel.For(0, EmployeeNoveltyARL.Count() - 1, Sub(Arl)

                                                                            TotalDaysInabilityARP = TotalDaysInabilityARP + EmployeeNoveltyARL(Arl).Days ' Total Días Enfermedad Profesional


                                                                            If EmployeeNoveltyARL(Arl).Value IsNot Nothing Then
                                                                                TotalValueInabilityARP = TotalValueInabilityARP + EmployeeNoveltyARL(Arl).Value
                                                                            End If


                                                                            InabilityARPInitialDate = EmployeeNoveltyARL(Arl).RealDate
                                                                            InabilityARPEndDate = EmployeeNoveltyARL(Arl).EndDate
                                                                            AutorizationNumberInabilityARP = EmployeeNoveltyARL(Arl).AutorizationNumber

                                                                            ProfesionalInabilitiesDays = ProfesionalInabilitiesDays + Me.CalculatinDaysInabilities(EmployeeNoveltyARL(Arl).RealDate, EmployeeNoveltyARL(Arl).EndDate, PayrollStarDate, payrollEndDate, TotalDaysHospitalInability)
                                                                            ValueProfesionalInabilities = Me.CalculatingValueInability(EmployeeNoveltyARL(Arl).RealDate, EmployeeNoveltyARL(Arl).EndDate, PayrollStarDate, payrollEndDate, TotalDaysHospitalInability, TotalValueHospitalInability, ProfesionalInabilitiesDays)

                                                                            MessageLiquitadion = CreateMessage("Existe registrada una Enfermedad Profesional en el Periodo de la Nómina registrada entre el día " & InabilityARPInitialDate & " y " & InabilityARPEndDate, False, payrollEndDate)
                                                                            EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                                                        End Sub
                        )

                        ' SANCIÓN
                        Dim EmployeeNoveltySanctions = employeeNovelty.Where(Function(x) x.TypeNovelty = 2)
                        Parallel.For(0, EmployeeNoveltySanctions.Count() - 1, Sub(Sanc)

                                                                                  TotalDaysSanctions = TotalDaysSanctions + EmployeeNoveltySanctions(Sanc).Days ' Total Días de Sanciones

                                                                                  SanctionsInitialDate = EmployeeNoveltySanctions(Sanc).RealDate
                                                                                  SanctionsEndDate = EmployeeNoveltySanctions(Sanc).EndDate

                                                                                  SanctionsDays = SanctionsDays + Me.CalculatinDaysInabilities(EmployeeNoveltySanctions(Sanc).RealDate, EmployeeNoveltySanctions(Sanc).EndDate, PayrollStarDate, payrollEndDate, TotalDaysSanctions)
                                                                                  ValueSanction = Me.CalculatingValueInability(EmployeeNoveltySanctions(Sanc).RealDate, EmployeeNoveltySanctions(Sanc).EndDate, PayrollStarDate, payrollEndDate, TotalDaysSanctions, 0, SanctionsDays)

                                                                                  MessageLiquitadion = CreateMessage("Existe registrada una Sanción en el Periodo de la Nómina registrada entre el día " & SanctionsInitialDate & " y " & SanctionsEndDate, False, payrollEndDate)
                                                                                  EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                                                              End Sub
                        )

                        ' LICENCIAS NO REMUNERADAS
                        Dim EmployeeNoveltyUnpaidLicenses = employeeNovelty.Where(Function(x) x.TypeNovelty = 3 And x.LicenseClass = 2)
                        Parallel.For(0, EmployeeNoveltyUnpaidLicenses.Count() - 1, Sub(UnpLic)

                                                                                       TotalDaysLicensesUnpaid = TotalDaysLicensesUnpaid + EmployeeNoveltyUnpaidLicenses(UnpLic).Days ' Total Días de Licencias NO Remuneradas


                                                                                       If EmployeeNoveltyUnpaidLicenses(UnpLic).Value IsNot Nothing Then
                                                                                           TotalValueLicensesUnpaid = TotalValueLicensesUnpaid + EmployeeNoveltyUnpaidLicenses(UnpLic).Value
                                                                                       End If



                                                                                       UnpaidLicensesInitialDate = EmployeeNoveltyUnpaidLicenses(UnpLic).RealDate
                                                                                       UnpaidLicensesEndDate = EmployeeNoveltyUnpaidLicenses(UnpLic).EndDate
                                                                                       AutorizationNumberUnpaidLicenses = EmployeeNoveltyUnpaidLicenses(UnpLic).AutorizationNumber

                                                                                       UnpaidLicensesDays = UnpaidLicensesDays + Me.CalculatinDaysInabilities(EmployeeNoveltyUnpaidLicenses(UnpLic).RealDate, EmployeeNoveltyUnpaidLicenses(UnpLic).EndDate, PayrollStarDate, payrollEndDate, TotalDaysLicensesUnpaid)
                                                                                       ValueUnpaidLicenses = Me.CalculatingValueInability(EmployeeNoveltyUnpaidLicenses(UnpLic).RealDate, EmployeeNoveltyUnpaidLicenses(UnpLic).EndDate, PayrollStarDate, payrollEndDate, TotalDaysLicensesUnpaid, 0, UnpaidLicensesDays)

                                                                                       MessageLiquitadion = CreateMessage("Existe registrada una Licencia NO Remunerada en el Periodo de la Nómina registrada entre el día " & UnpaidLicensesInitialDate & " y " & UnpaidLicensesEndDate, False, payrollEndDate)
                                                                                       EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                                                                   End Sub
                        )

                        ' LICENCIAS REMUNERADAS
                        Dim EmployeeNoveltyPaidLicenses = employeeNovelty.Where(Function(x) x.TypeNovelty = 3 And x.LicenseClass = 1)
                        Parallel.For(0, EmployeeNoveltyPaidLicenses.Count() - 1, Sub(PaidLic)

                                                                                     TotalDaysLicensesRemunerated = TotalDaysLicensesRemunerated + EmployeeNoveltyPaidLicenses(PaidLic).Days ' Total Días de Licencias Remuneradas


                                                                                     If EmployeeNoveltyPaidLicenses(PaidLic).Value IsNot Nothing Then
                                                                                         TotalValueLicensesRemunerated = TotalValueLicensesRemunerated + EmployeeNoveltyPaidLicenses(PaidLic).Value
                                                                                     End If


                                                                                     RemuneratedLicensesDays = RemuneratedLicensesDays + Me.CalculatinDaysInabilities(EmployeeNoveltyPaidLicenses(PaidLic).RealDate, EmployeeNoveltyPaidLicenses(PaidLic).EndDate, PayrollStarDate, payrollEndDate, TotalDaysLicensesRemunerated)
                                                                                     ValueRemuneratedLicenses = Me.CalculatingValueInability(EmployeeNoveltyPaidLicenses(PaidLic).RealDate, EmployeeNoveltyPaidLicenses(PaidLic).EndDate, PayrollStarDate, payrollEndDate, TotalDaysLicensesRemunerated, 0, RemuneratedLicensesDays)

                                                                                     PaidLicensesInitialDate = EmployeeNoveltyPaidLicenses(PaidLic).RealDate
                                                                                     PaidLicensesEndDate = EmployeeNoveltyPaidLicenses(PaidLic).EndDate

                                                                                     MessageLiquitadion = CreateMessage("Existe registrada una Licencia Remunerada en el Periodo de la Nómina registrada entre el día " & PaidLicensesInitialDate & " y " & PaidLicensesEndDate, False, payrollEndDate)
                                                                                     EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                                                                 End Sub
                        )

                    End If


                    'VACACIONES

                    Dim VacationDaysApart As Integer
                    Dim VacationValueNet As Double
                    Dim AdjustVacationValue As Double
                    Dim EnjoyVacationDay As Integer
                    Dim PensionAdjustValue As Double
                    Dim HealthAdjustValue As Double

                    'Averiguo si el empleado tiene Vacaciones
                    Dim EmployeeVacation = ListVacationEmployee.Where(Function(x) x.VacationPeriod.ContractId = ListPayrollAux.Item(Aux).Id And x.LiquidationDate = PayrollStarDate).ToList()

                    If EmployeeVacation.Count() > 0 Then
                        For EV As Integer = 0 To (EmployeeVacation.Count() - 1)
                            VacationDays = EmployeeVacation.Item(EV).EnjoyDays
                            VacationValue = EmployeeVacation.Item(EV).VacationValue

                            If EmployeeVacation.Item(EV).TypeVacation = 1 Then ' Disfruta en Dinero
                                VacationValueCash = EmployeeVacation.Item(EV).VacationValueNet
                            End If

                            VacationHealthEmployee = EmployeeVacation.Item(EV).VacationValueNet * (EmployeeHealthContributionPercentage / 100)
                            VacationHealthEmployer = EmployeeVacation.Item(EV).VacationValueNet * (EmployerHealthContributionPercentage / 100)
                            VacationPensionEmployee = EmployeeVacation.Item(EV).VacationValueNet * (EmployeePensionContributionPercentage / 100)
                            VacationPensionEmployer = EmployeeVacation.Item(EV).VacationValueNet * (EmployerPensionContributionPercentage / 100)

                            'Revisamos si hay vacaciones ajustadas
                            Dim AdjustedVacation = EmployeeVacation.Where(Function(x) x.State = 2).ToList()

                            If AdjustedVacation.Count() > 0 Then
                                For AV As Integer = 0 To (AdjustedVacation.Count() - 1)
                                    VacationDaysApart = DateDiff(DateInterval.Day, AdjustedVacation.Item(AV).IncorporationDate, AdjustedVacation.Item(AV).IncorporationDateReal)
                                    VacationValueNet = AdjustedVacation.Item(AV).VacationValueNet
                                    EnjoyVacationDay = AdjustedVacation.Item(AV).TakenDays
                                    AdjustVacationValue = (VacationValueNet * VacationDaysApart) / EnjoyVacationDay

                                    PensionAdjustValue = (AdjustedVacation.Item(AV).PensionContribution * VacationDaysApart) / EnjoyVacationDay
                                    PensionAdjustValue = PensionAdjustValue - AdjustedVacation.Item(AV).PensionContribution
                                    HealthAdjustValue = (AdjustedVacation.Item(AV).HealthContribution * VacationDaysApart) / EnjoyVacationDay
                                    HealthAdjustValue = HealthAdjustValue - AdjustedVacation.Item(AV).HealthContribution

                                    AdjustVacationValue = AdjustVacationValue - PensionAdjustValue - HealthAdjustValue

                                Next
                            End If

                        Next

                    End If


                    ' Totalizo las Incapacidades
                    TotalDaysInabilities = 0
                    TotalValuesInabilities = 0
                    TotalDaysInabilities = TotalDaysInabilityARP + TotalDaysMaternity + TotalDaysHospitalInability + TotalDaysAmbulatoryInability
                    TotalValuesInabilities = TotalValueInabilityARP + TotalValueHospitalInability + TotalValueHospitalInability + TotalValuesAmbulatoryInabilitiy

                    ' Totalizo las Incapacidades del Periodo
                    TotalPeriodDaysInabilities = 0
                    TotalPeriodValuesInabilities = 0
                    TotalPeriodDaysInabilities = ProfesionalInabilitiesDays + MaternityDays + HospitalInabilityDays + AmbulatoryInabilityDays
                    TotalPeriodValuesInabilities = ValueProfesionalInabilities + ValueMaternity + ValueHospitalInability + ValueAmbulatoryInability

                    'Días Trabajados del Empleado
                    DaysWorkedEmployee = 0
                    DaysWorkedEmployee = Me.DaysWorkedEmployee(InitialDateEval, EndingDateEval, PayrollStarDate, payrollEndDate, PayrollMonth)

                    ' Calculo Días Claves en el Periodo
                    PensionDays = DaysWorkedEmployee - SanctionsDays
                    HealthDays = DaysWorkedEmployee
                    ARPDays = DaysWorkedEmployee - TotalPeriodDaysInabilities - SanctionsDays - UnpaidLicensesDays - RemuneratedLicensesDays - VacationDays ' Falta Permisos
                    CompensationFundDays = DaysWorkedEmployee - ValueUnpaidLicenses - MaternityDays - TotalDaysInabilityARP - TotalDaysAmbulatoryInability - TotalDaysHospitalInability
                    WorkDays = DaysWorkedEmployee - TotalPeriodDaysInabilities - UnpaidLicensesDays - SanctionsDays - VacationDays
                    ProvisionDays = DaysWorkedEmployee - SanctionsDays - UnpaidLicensesDays
                    TotalWorkDays = TotalWorkDays + WorkDays

                    ''Averiguo el Cuadro de Turnos del Empleado
                    'ScheduleDetailEmployee = ScheduleEmployee(EmployeeId, payrollEmployee.Item(i).FunctionalUnitId, InitialDateEval, EndingDateEval)
                    ScheduleDetailEmployee = ScheduleEmployee(EmployeeId, ListPayrollAux.Item(Aux).FunctionalUnitId, InitialDateEval, EndingDateEval)


                    'Recorro los Conceptos Autorizados para el Empleado

                    Dim employeeAutorizationConceptAccrued = employeeAutorizationConcept.Where(Function(x) x.Concept.ConceptType = "1")
                    'Dim Acr As Integer = 0
                    Parallel.For(0, employeeAutorizationConceptAccrued.Count() - 1, Sub(Acr)
                                                                                        WorkHours = 0

                                                                                        If ScheduleDetailEmployee IsNot Nothing Then
                                                                                            WorkHours = Me.HourByConcept(HandlesTurnChart, PayrollDays, employeeAutorizationConceptAccrued(Acr), IBCHour, ScheduleDetailEmployee)
                                                                                            CountWorkHours = CountWorkHours + 1
                                                                                        Else
                                                                                            WorkHours = 0
                                                                                        End If

                                                                                        'Cargo la Base de Liquidación de Pensión - Empleado
                                                                                        Dim BaseIBCPension = Me.GetIBCPension(BasicSalary, payrollParameter.LegalSalaryMinimum, IBCPension, PayrollDays, TotalDaysLicensesUnpaid, ConceptoDevengadoSuma, EmployeePensionContributionPercentage)

                                                                                        ' Cargo la Base de Liquidación de Salud - Empleado

                                                                                        BaseIBCHealth = Me.GetIBCHealth(IBCHealth, payrollParameter.LegalSalaryMinimum, BasicSalary, 0, PayrollDays, EmployeeHealthContributionPercentage, ConceptoDevengadoSuma, TotalDaysLicensesUnpaid)

                                                                                        ' Cargo la Base de Liquidación de Salud - Patrono
                                                                                        Dim BaseIBCHealthEmployer = Me.GetIBCHealthEmployer(IBCHealth, payrollParameter.LegalSalaryMinimum, BasicSalary, 0, PayrollDays, WorkDays, BaseIBCHealth, IBCHealth, EmployerHealthContributionPercentage, TotalDaysLicensesUnpaid)

                                                                                        ' Cargo la Base de Liquidación de Pensión - Patrono
                                                                                        Dim BaseIBCPensionEmployer = Me.GetIBCPensionEmployer(IBCPension, payrollParameter.LegalSalaryMinimum, BasicSalary, TotalDaysLicensesUnpaid, PayrollDays, EmployeePensionContributionPercentage)

                                                                                        ' Cargo la Base de ARP
                                                                                        Dim BaseIBCArp = Me.GetBaseARP(BasicSalary, payrollParameter.LegalSalaryMinimum, ARPDays, IBCARP, ProfessionalRiskPercentage)

                                                                                        ' Cargo la Base Parafiscal de la Caja de Compensación
                                                                                        Dim BaseParafiscalCompensationFund = Me.GetBaseParafiscalCompensationFund(IBCCompensationFund, PayrollDays, payrollParameter.LegalSalaryMinimum, RemuneratedLicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, CompensationFundContributionPercentage)

                                                                                        Dim BaseParafiscalICBF = Me.GetBaseParafiscalICBF(IBCICBF, PayrollDays, payrollParameter.LegalSalaryMinimum, RemuneratedLicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, ICBFContributionPercentage)

                                                                                        Dim FormulaConcept = employeeAutorizationConceptAccrued(Acr).Concept.Formulates

                                                                                        Dim ConceptValue As Double
                                                                                        Dim ReplaceFormula As String


                                                                                        Dim ConceptData = Me.ReplaceData(payrollParameter, BasicSalary, WorkDays, WorkHours, PayrollDays, FormulaConcept, payrollEmployee.Item(i), BaseIBCPension, BaseIBCHealth, IBCHealth, BaseIBCHealthEmployer, BaseIBCPensionEmployer, IBCSENA, IBCICBF, _
                                                                                                                        IBCPeriod, IBCSeverance, IBCCompensationFund, IBCPension, IBCARP, ValueAmbulatoryInability, ValueHospitalInability, ValueMaternity, ValueSanction, TotalPeriodDaysInabilities, HospitalInabilityDays, _
                                                                                                                        RemuneratedLicensesDays, UnpaidLicensesDays, ValueUnpaidLicenses, ValueProfesionalInabilities, HealthEmployee, PensionEmployee, BaseIBCArp, BaseParafiscalCompensationFund, BaseParafiscalICBF, RetentionValue, ProvisionDays, ProfessionalRiskPercentage, VacationValue, AdjustVacationValue, incentiePaymentValue, unemployedInterestValue)

                                                                                        ReplaceFormula = ConceptData("Formula")
                                                                                        ConceptValue = CDbl(ConceptData("Valor"))


                                                                                        If ConceptValue > 0 Then

                                                                                            'Horas Extras
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "001" Then HourSchedule = HourSchedule + ConceptValue

                                                                                            'Prima de Servicios
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "002" Then IncentivePaymentServices = IncentivePaymentServices + ConceptValue

                                                                                            'otras nummaxpri
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "003" Then IncetivePaymentServicesExtra = IncetivePaymentServicesExtra + ConceptValue

                                                                                            ' Bonificación por Servicios
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "004" Then BonusServices = BonusServices + ConceptValue

                                                                                            ' Sueldo
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "005" Then Salary = Salary + ConceptValue

                                                                                            ' Acumulado Auxilio de Transporte
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "006" Then AcumulatedHelpTransport = AcumulatedHelpTransport + ConceptValue

                                                                                            'Indemnizaciones()
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "007" Then Compensation = Compensation + ConceptValue

                                                                                            'Cesantias()
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "008" Then Unemployment = Unemployment + ConceptValue

                                                                                            'Aporte Riesgos
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "009" Then RiskContribution = RiskContribution + ConceptValue

                                                                                            'Otros Devengados
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "010" Then OtherAccrued = OtherAccrued + ConceptValue

                                                                                            'Otros Deducidos
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "011" Then OtherDeducted = OtherDeducted + ConceptValue

                                                                                            'Horas Extras Nocturnas
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "012" Then EveningOvertime = EveningOvertime + ConceptValue

                                                                                            'Horas Extras Dominicales
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "013" Then SundayOvertime = SundayOvertime + ConceptValue

                                                                                            'Pensión Empleado
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "014" Then PensionEmployee = PensionEmployee + ConceptValue

                                                                                            'Pensión Patrono
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "015" Then PensionEmployer = PensionEmployer + ConceptValue

                                                                                            'Pensión Voluntaria
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "016" Then VoluntaryPension = VoluntaryPension + ConceptValue

                                                                                            'Salud Empleado
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "017" Then HealthEmployee = HealthEmployee + ConceptValue

                                                                                            'Salud Patrono
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "018" Then HealthEmployer = HealthEmployer + ConceptValue

                                                                                            'Salud Voluntaria
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "019" Then VoluntaryHealth = VoluntaryHealth + ConceptValue

                                                                                            'Retención en la fuente
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "020" Then Withholding = Withholding + ConceptValue

                                                                                            'Incapacidad Ambulatoria
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "021" Then AmbulatoryInability = AmbulatoryInability + ConceptValue

                                                                                            'Incapacidad Hospitalaria
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "022" Then HospitalInability = HospitalInability + ConceptValue

                                                                                            'Maternidad
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "023" Then Maternity = Maternity + ConceptValue

                                                                                            'Licencias
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "024" Then Licenses = Licenses + ConceptValue

                                                                                            'Licencias No Remuneradas
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "025" Then UnpaidLicenses = UnpaidLicenses + ConceptValue

                                                                                            'Sanciones
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "026" Then Sanctions = Sanctions + ConceptValue

                                                                                            'Incapacidad Riesgos Profesionales
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "027" Then InabilityProfessionalRisk = InabilityProfessionalRisk + ConceptValue

                                                                                            'Permisos
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "028" Then Permission = Permission + ConceptValue

                                                                                            'Vacaciones
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "030" Then Vacation = Vacation + ConceptValue

                                                                                            'Provisión Vacaciones
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "031" Then ProvisionVacation = ProvisionVacation + ConceptValue

                                                                                            'Provisión Número Máximo de Primas
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "033" Then MaxNumberIncentiveProvision = MaxNumberIncentiveProvision + ConceptValue

                                                                                            'Provisión Intereses de Cesantías
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "034" Then UnemploymentInterestsProvision = UnemploymentInterestsProvision + ConceptValue

                                                                                            'Provisión SENA
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "035" Then SenaProvision = SenaProvision + ConceptValue

                                                                                            'Provisión Caja de Compensación
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "036" Then CompensationFundProvision = CompensationFundProvision + ConceptValue

                                                                                            'Provisión ICBF
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "037" Then ICBFProvision = ICBFProvision + ConceptValue

                                                                                            'Aporte Fondo de Solidaridad Pensional
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "038" Then ContributionPensionSolidarityFund = ContributionPensionSolidarityFund + ConceptValue

                                                                                            'Deducción por Estudio
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "039" Then StudyDeducted = StudyDeducted + ConceptValue

                                                                                            'Deducción por Vivienda
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.ConceptClass = "040" Then HousingDeducted = HousingDeducted + ConceptValue

                                                                                            For l As Integer = 0 To (employeeAutorizationConceptAccrued(Acr).Concept.ConceptGroup.Count() - 1)
                                                                                                If employeeAutorizationConceptAccrued(Acr).Concept.ConceptGroup.Item(l).MaximunDiscount = True Then

                                                                                                    'Porcentaje de Descuento Máximo
                                                                                                    If (ConceptoDevengadoSuma * MaximunDiscountPercentage / 100) < ConceptoDeducidoSuma + ConceptValue Then
                                                                                                        MessageLiquitadion = CreateMessage("El Concepto " & employeeAutorizationConceptAccrued(Acr).Concept.Name & " sobrepasa el % de Descuento Máximo", True, payrollEndDate)
                                                                                                        EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                                                                                    End If

                                                                                                    'Porcentaje Descuento Estudio y Salud
                                                                                                    If (employeeAutorizationConceptAccrued(Acr).Concept.Code = "017" Or employeeAutorizationConceptAccrued(Acr).Concept.Code = "039") Then
                                                                                                        If (ConceptoDevengadoSuma * EducationStudyDiscountPercentage / 100) < ConceptoDeducidoSuma + ConceptValue Then
                                                                                                            MessageLiquitadion = CreateMessage("El Concepto " & employeeAutorizationConceptAccrued(Acr).Concept.Name & " sobrepasa el % de Deduccion de Salud", True, payrollEndDate)
                                                                                                            EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                                                                                        Else
                                                                                                            MessageLiquitadion = CreateMessage("El Concepto " & employeeAutorizationConceptAccrued(Acr).Concept.Name & " sobrepasa el % de Deduccion de Estudio", True, payrollEndDate)
                                                                                                            EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                                                                                        End If
                                                                                                    End If

                                                                                                    'Deducción máxima por vivienda
                                                                                                    If employeeAutorizationConceptAccrued(Acr).Concept.Code = "040" Then
                                                                                                        If HousingDeductionMaximumValue < ConceptValue Then
                                                                                                            MessageLiquitadion = CreateMessage("El Concepto " & employeeAutorizationConceptAccrued(Acr).Concept.Name & " sobrepasa el valor de Deducción Máximo por Vivienda", True, payrollEndDate)
                                                                                                            EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                                                                                        End If
                                                                                                    End If

                                                                                                    'SMLV maximos para liquidar salud y pension
                                                                                                    If (employeeAutorizationConceptAccrued(Acr).Concept.Code = "014" Or employeeAutorizationConceptAccrued(Acr).Concept.Code = "017") Then
                                                                                                        If MaximunDiscountPercentage < (ConceptoDevengadoSuma / legalMinimimunSalary) Then
                                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.Code = "014" Then
                                                                                                                MessageLiquitadion = CreateMessage("El Concepto " & employeeAutorizationConceptAccrued(Acr).Concept.Name & " sobrepasa el numero de SMLV maximos para liquidar pensión", True, payrollEndDate)
                                                                                                                EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                                                                                            Else
                                                                                                                MessageLiquitadion = CreateMessage("El Concepto " & employeeAutorizationConceptAccrued(Acr).Concept.Name & " sobrepasa el numero de SMLV maximos para liquidar salud", True, payrollEndDate)
                                                                                                                EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                                                                                            End If
                                                                                                        End If
                                                                                                    End If
                                                                                                End If
                                                                                            Next

                                                                                            EmployeeLiquidatedDetail = New LiquidationDetail()

                                                                                            EmployeeLiquidatedDetail.RegisterStatus = 1
                                                                                            EmployeeLiquidatedDetail.PayrollDate = payrollEndDate
                                                                                            EmployeeLiquidatedDetail.LiquidationPeriod = "1"
                                                                                            EmployeeLiquidatedDetail.ConceptClass = employeeAutorizationConceptAccrued(Acr).Concept.ConceptType.ToString()
                                                                                            EmployeeLiquidatedDetail.ConceptId = employeeAutorizationConceptAccrued(Acr).Concept.Id
                                                                                            EmployeeLiquidatedDetail.ConceptCode = employeeAutorizationConceptAccrued(Acr).Concept.Code
                                                                                            EmployeeLiquidatedDetail.ConceptDetail = employeeAutorizationConceptAccrued(Acr).Concept.Name & " Contrato No. " & ContractNumber.ToString()
                                                                                            EmployeeLiquidatedDetail.ConceptTotalValue = ConceptValue


                                                                                            ConceptoDevengadoSuma = ConceptoDevengadoSuma + ConceptValue
                                                                                            AccruedValueConcept = ConceptValue

                                                                                            ' Cargo los IBC's
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.AffectIBC = True Then IBCPeriod = IBCPeriod + ConceptValue
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.AffectIBCRTF = True Then IBCRTF = IBCRTF + ConceptValue
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.AffectIBCSENA = True Then IBCSENA = IBCSENA + ConceptValue
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.AffectIBCICBF = True Then IBCICBF = IBCICBF + ConceptValue
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.AffectIBCCompensationFund = True Then IBCCompensationFund = IBCCompensationFund + ConceptValue
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.AffectIBCSeverance = True Then IBCSeverance = IBCSeverance + ConceptValue
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.AffectIBCHealth = True Then IBCHealth = IBCHealth + ConceptValue
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.AffectIBCPension = True Then IBCPension = IBCPension + ConceptValue
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.AffectIBCARP = True Then IBCARP = IBCARP + ConceptValue
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.AffectIBCVacation = True Then IBCVacation = IBCVacation + ConceptValue
                                                                                            If employeeAutorizationConceptAccrued(Acr).Concept.AffectIBCIncentivePayment = True Then IBCIncentivePayment = IBCIncentivePayment + ConceptValue


                                                                                            EmployeeLiquidatedDetail.AccruedValue = AccruedValueConcept
                                                                                            EmployeeLiquidatedDetail.DeductedValue = DeductedValueConcept
                                                                                            EmployeeLiquidatedDetail.RetentionBase = 0
                                                                                            EmployeeLiquidatedDetail.RetentionPercentage = 0
                                                                                            EmployeeLiquidatedDetail.InitialBalance = 0
                                                                                            EmployeeLiquidatedDetail.ConceptType = employeeAutorizationConceptAccrued(Acr).Concept.ConceptType
                                                                                            EmployeeLiquidatedDetail.ConceptFormulate = FormulaConcept
                                                                                            EmployeeLiquidatedDetail.ReplaceConceptFormulate = ReplaceFormula
                                                                                            EmployeeLiquidated.LiquidationDetail.Add(EmployeeLiquidatedDetail)

                                                                                            AccruedValueConcept = 0
                                                                                            DeductedValueConcept = 0
                                                                                        End If


                                                                                    End Sub
                    )

                    Dim employeeAutorizationConceptDeducted = employeeAutorizationConcept.Where(Function(x) x.Concept.ConceptType = "2")
                    'Dim Acr As Integer = 0
                    Parallel.For(0, employeeAutorizationConceptDeducted.Count() - 1, Sub(Ded)
                                                                                         WorkHours = 0

                                                                                         If ScheduleDetailEmployee IsNot Nothing Then
                                                                                             WorkHours = Me.HourByConcept(HandlesTurnChart, PayrollDays, employeeAutorizationConceptDeducted(Ded), IBCHour, ScheduleDetailEmployee)
                                                                                             CountWorkHours = CountWorkHours + 1
                                                                                         Else
                                                                                             WorkHours = 0
                                                                                         End If

                                                                                         'Cargo la Base de Liquidación de Pensión - Empleado
                                                                                         Dim BaseIBCPension = Me.GetIBCPension(BasicSalary, payrollParameter.LegalSalaryMinimum, IBCPension, PayrollDays, TotalDaysLicensesUnpaid, ConceptoDevengadoSuma, EmployeePensionContributionPercentage)

                                                                                         ' Cargo la Base de Liquidación de Salud - Empleado

                                                                                         BaseIBCHealth = Me.GetIBCHealth(IBCHealth, payrollParameter.LegalSalaryMinimum, BasicSalary, 0, PayrollDays, EmployeeHealthContributionPercentage, ConceptoDevengadoSuma, TotalDaysLicensesUnpaid)

                                                                                         ' Cargo la Base de Liquidación de Salud - Patrono
                                                                                         Dim BaseIBCHealthEmployer = Me.GetIBCHealthEmployer(IBCHealth, payrollParameter.LegalSalaryMinimum, BasicSalary, 0, PayrollDays, WorkDays, BaseIBCHealth, IBCHealth, EmployerHealthContributionPercentage, TotalDaysLicensesUnpaid)

                                                                                         ' Cargo la Base de Liquidación de Pensión - Patrono
                                                                                         Dim BaseIBCPensionEmployer = Me.GetIBCPensionEmployer(IBCPension, payrollParameter.LegalSalaryMinimum, BasicSalary, TotalDaysLicensesUnpaid, PayrollDays, EmployeePensionContributionPercentage)

                                                                                         ' Cargo la Base de ARP
                                                                                         Dim BaseIBCArp = Me.GetBaseARP(BasicSalary, payrollParameter.LegalSalaryMinimum, ARPDays, IBCARP, ProfessionalRiskPercentage)

                                                                                         ' Cargo la Base Parafiscal de la Caja de Compensación
                                                                                         Dim BaseParafiscalCompensationFund = Me.GetBaseParafiscalCompensationFund(IBCCompensationFund, PayrollDays, payrollParameter.LegalSalaryMinimum, RemuneratedLicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, CompensationFundContributionPercentage)

                                                                                         Dim BaseParafiscalICBF = Me.GetBaseParafiscalICBF(IBCICBF, PayrollDays, payrollParameter.LegalSalaryMinimum, RemuneratedLicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, ICBFContributionPercentage)

                                                                                         Dim FormulaConcept = employeeAutorizationConceptDeducted(Ded).Concept.Formulates

                                                                                         Dim ConceptValue As Double
                                                                                         Dim ReplaceFormula As String


                                                                                         Dim ConceptData = Me.ReplaceData(payrollParameter, BasicSalary, WorkDays, WorkHours, PayrollDays, FormulaConcept, payrollEmployee.Item(i), BaseIBCPension, BaseIBCHealth, IBCHealth, BaseIBCHealthEmployer, BaseIBCPensionEmployer, IBCSENA, IBCICBF, _
                                                                                                                         IBCPeriod, IBCSeverance, IBCCompensationFund, IBCPension, IBCARP, ValueAmbulatoryInability, ValueHospitalInability, ValueMaternity, ValueSanction, TotalPeriodDaysInabilities, HospitalInabilityDays, _
                                                                                                                         RemuneratedLicensesDays, UnpaidLicensesDays, ValueUnpaidLicenses, ValueProfesionalInabilities, HealthEmployee, PensionEmployee, BaseIBCArp, BaseParafiscalCompensationFund, BaseParafiscalICBF, RetentionValue, ProvisionDays, ProfessionalRiskPercentage, VacationValue, AdjustVacationValue, incentiePaymentValue, unemployedInterestValue)

                                                                                         ReplaceFormula = ConceptData("Formula")
                                                                                         ConceptValue = CDbl(ConceptData("Valor"))


                                                                                         If ConceptValue > 0 Then

                                                                                             'Horas Extras
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "001" Then HourSchedule = HourSchedule + ConceptValue

                                                                                             'Prima de Servicios
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "002" Then IncentivePaymentServices = IncentivePaymentServices + ConceptValue

                                                                                             'otras nummaxpri
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "003" Then IncetivePaymentServicesExtra = IncetivePaymentServicesExtra + ConceptValue

                                                                                             ' Bonificación por Servicios
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "004" Then BonusServices = BonusServices + ConceptValue

                                                                                             ' Sueldo
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "005" Then Salary = Salary + ConceptValue

                                                                                             ' Acumulado Auxilio de Transporte
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "006" Then AcumulatedHelpTransport = AcumulatedHelpTransport + ConceptValue

                                                                                             'Indemnizaciones()
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "007" Then Compensation = Compensation + ConceptValue

                                                                                             'Cesantias()
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "008" Then Unemployment = Unemployment + ConceptValue

                                                                                             'Aporte Riesgos
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "009" Then RiskContribution = RiskContribution + ConceptValue

                                                                                             'Otros Devengados
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "010" Then OtherAccrued = OtherAccrued + ConceptValue

                                                                                             'Otros Deducidos
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "011" Then OtherDeducted = OtherDeducted + ConceptValue

                                                                                             'Horas Extras Nocturnas
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "012" Then EveningOvertime = EveningOvertime + ConceptValue

                                                                                             'Horas Extras Dominicales
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "013" Then SundayOvertime = SundayOvertime + ConceptValue

                                                                                             'Pensión Empleado
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "014" Then PensionEmployee = PensionEmployee + ConceptValue

                                                                                             'Pensión Patrono
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "015" Then PensionEmployer = PensionEmployer + ConceptValue

                                                                                             'Pensión Voluntaria
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "016" Then VoluntaryPension = VoluntaryPension + ConceptValue

                                                                                             'Salud Empleado
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "017" Then HealthEmployee = HealthEmployee + ConceptValue

                                                                                             'Salud Patrono
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "018" Then HealthEmployer = HealthEmployer + ConceptValue

                                                                                             'Salud Voluntaria
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "019" Then VoluntaryHealth = VoluntaryHealth + ConceptValue

                                                                                             'Retención en la fuente
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "020" Then Withholding = Withholding + ConceptValue

                                                                                             'Incapacidad Ambulatoria
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "021" Then AmbulatoryInability = AmbulatoryInability + ConceptValue

                                                                                             'Incapacidad Hospitalaria
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "022" Then HospitalInability = HospitalInability + ConceptValue

                                                                                             'Maternidad
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "023" Then Maternity = Maternity + ConceptValue

                                                                                             'Licencias
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "024" Then Licenses = Licenses + ConceptValue

                                                                                             'Licencias No Remuneradas
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "025" Then UnpaidLicenses = UnpaidLicenses + ConceptValue

                                                                                             'Sanciones
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "026" Then Sanctions = Sanctions + ConceptValue

                                                                                             'Incapacidad Riesgos Profesionales
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "027" Then InabilityProfessionalRisk = InabilityProfessionalRisk + ConceptValue

                                                                                             'Permisos
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "028" Then Permission = Permission + ConceptValue

                                                                                             'Vacaciones
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "030" Then Vacation = Vacation + ConceptValue

                                                                                             'Provisión Vacaciones
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "031" Then ProvisionVacation = ProvisionVacation + ConceptValue

                                                                                             'Provisión Número Máximo de Primas
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "033" Then MaxNumberIncentiveProvision = MaxNumberIncentiveProvision + ConceptValue

                                                                                             'Provisión Intereses de Cesantías
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "034" Then UnemploymentInterestsProvision = UnemploymentInterestsProvision + ConceptValue

                                                                                             'Provisión SENA
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "035" Then SenaProvision = SenaProvision + ConceptValue

                                                                                             'Provisión Caja de Compensación
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "036" Then CompensationFundProvision = CompensationFundProvision + ConceptValue

                                                                                             'Provisión ICBF
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "037" Then ICBFProvision = ICBFProvision + ConceptValue

                                                                                             'Aporte Fondo de Solidaridad Pensional
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "038" Then ContributionPensionSolidarityFund = ContributionPensionSolidarityFund + ConceptValue

                                                                                             'Deducción por Estudio
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "039" Then StudyDeducted = StudyDeducted + ConceptValue

                                                                                             'Deducción por Vivienda
                                                                                             If employeeAutorizationConceptDeducted(Ded).Concept.ConceptClass = "040" Then HousingDeducted = HousingDeducted + ConceptValue

                                                                                             EmployeeLiquidatedDetail = New LiquidationDetail()

                                                                                             EmployeeLiquidatedDetail.RegisterStatus = 1
                                                                                             EmployeeLiquidatedDetail.PayrollDate = payrollEndDate
                                                                                             EmployeeLiquidatedDetail.LiquidationPeriod = "1"
                                                                                             EmployeeLiquidatedDetail.ConceptClass = employeeAutorizationConceptDeducted(Ded).Concept.ConceptType.ToString()
                                                                                             EmployeeLiquidatedDetail.ConceptId = employeeAutorizationConceptDeducted(Ded).Concept.Id
                                                                                             EmployeeLiquidatedDetail.ConceptCode = employeeAutorizationConceptDeducted(Ded).Concept.Code
                                                                                             EmployeeLiquidatedDetail.ConceptDetail = employeeAutorizationConceptDeducted(Ded).Concept.Name & " Contrato No. " & ContractNumber.ToString()
                                                                                             EmployeeLiquidatedDetail.ConceptTotalValue = ConceptValue

                                                                                             ConceptoDeducidoSuma = ConceptoDeducidoSuma + ConceptValue
                                                                                             DeductedValueConcept = ConceptValue

                                                                                             EmployeeLiquidatedDetail.AccruedValue = AccruedValueConcept
                                                                                             EmployeeLiquidatedDetail.DeductedValue = DeductedValueConcept
                                                                                             EmployeeLiquidatedDetail.RetentionBase = 0
                                                                                             EmployeeLiquidatedDetail.RetentionPercentage = 0
                                                                                             EmployeeLiquidatedDetail.InitialBalance = 0
                                                                                             EmployeeLiquidatedDetail.ConceptType = employeeAutorizationConceptDeducted(Ded).Concept.ConceptType
                                                                                             EmployeeLiquidatedDetail.ConceptFormulate = FormulaConcept
                                                                                             EmployeeLiquidatedDetail.ReplaceConceptFormulate = ReplaceFormula
                                                                                             EmployeeLiquidated.LiquidationDetail.Add(EmployeeLiquidatedDetail)

                                                                                             AccruedValueConcept = 0
                                                                                             DeductedValueConcept = 0
                                                                                         End If

                                                                                     End Sub
                    )


                    Dim employeeAutorizationConceptEmployer = employeeAutorizationConcept.Where(Function(x) x.Concept.ConceptType = "3")
                    'Dim Acr As Integer = 0
                    Parallel.For(0, employeeAutorizationConceptEmployer.Count() - 1, Sub(Emp)
                                                                                         WorkHours = 0

                                                                                         If ScheduleDetailEmployee IsNot Nothing Then
                                                                                             WorkHours = Me.HourByConcept(HandlesTurnChart, PayrollDays, employeeAutorizationConceptEmployer(Emp), IBCHour, ScheduleDetailEmployee)
                                                                                             CountWorkHours = CountWorkHours + 1
                                                                                         Else
                                                                                             WorkHours = 0
                                                                                         End If

                                                                                         ' Cargo la Base de Liquidación de Pensión - Empleado
                                                                                         Dim BaseIBCPension = Me.GetIBCPension(BasicSalary, payrollParameter.LegalSalaryMinimum, IBCPension, PayrollDays, TotalDaysLicensesUnpaid, ConceptoDevengadoSuma, EmployeePensionContributionPercentage)

                                                                                         ' Cargo la Base de Liquidación de Salud - Empleado

                                                                                         BaseIBCHealth = Me.GetIBCHealth(IBCHealth, payrollParameter.LegalSalaryMinimum, BasicSalary, 0, PayrollDays, EmployeeHealthContributionPercentage, ConceptoDevengadoSuma, TotalDaysLicensesUnpaid)

                                                                                         ' Cargo la Base de Liquidación de Salud - Patrono
                                                                                         Dim BaseIBCHealthEmployer = Me.GetIBCHealthEmployer(IBCHealth, payrollParameter.LegalSalaryMinimum, BasicSalary, 0, PayrollDays, WorkDays, BaseIBCHealth, IBCHealth, EmployerHealthContributionPercentage, TotalDaysLicensesUnpaid)

                                                                                         ' Cargo la Base de Liquidación de Pensión - Patrono
                                                                                         Dim BaseIBCPensionEmployer = Me.GetIBCPensionEmployer(IBCPension, payrollParameter.LegalSalaryMinimum, BasicSalary, TotalDaysLicensesUnpaid, PayrollDays, EmployeePensionContributionPercentage)

                                                                                         ' Cargo la Base de ARP
                                                                                         Dim BaseIBCArp = Me.GetBaseARP(BasicSalary, payrollParameter.LegalSalaryMinimum, ARPDays, IBCARP, ProfessionalRiskPercentage)

                                                                                         ' Cargo la Base Parafiscal de la Caja de Compensación
                                                                                         Dim BaseParafiscalCompensationFund = Me.GetBaseParafiscalCompensationFund(IBCCompensationFund, PayrollDays, payrollParameter.LegalSalaryMinimum, RemuneratedLicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, CompensationFundContributionPercentage)

                                                                                         Dim BaseParafiscalICBF = Me.GetBaseParafiscalICBF(IBCICBF, PayrollDays, payrollParameter.LegalSalaryMinimum, RemuneratedLicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, ICBFContributionPercentage)

                                                                                         ' RETEFUENTE
                                                                                         'If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "020" Then

                                                                                         '    'Dim RetentionDateValue = Me.Retention(IBCRTF, payrollEmployee.Employee.ThirdParty.RetentionType, "Valor", 0, PensionEmployee, VoluntaryPension, VoluntaryHealth, ContributionPensionSolidarityFund, RTFExcempt, HousingDeducted, StudyDeducted, payrollEndDate, EmployeeLiquidation, Retention)
                                                                                         '    'RetentionValue = RetentionDateValue("Retenciones")

                                                                                         '    PayrollCheckLiquidation = EmployeeLiquidation(ListPayrollAux.Item(Aux).EmployeeId)

                                                                                         '    RetentionValue = Me.Retention(IBCRTF, ListPayrollAux.Item(Aux).Employee.ThirdParty.RetentionType, "Valor", 0, PensionEmployee, VoluntaryPension, VoluntaryHealth, ContributionPensionSolidarityFund, RTFExcempt, HousingDeducted, StudyDeducted, payrollEndDate, PayrollCheckLiquidation, Retention)("Retenciones")
                                                                                         '    PercentageRetention = Me.Retention(IBCRTF, ListPayrollAux.Item(Aux).Employee.ThirdParty.RetentionType, "Porcentaje", 0, PensionEmployee, VoluntaryPension, VoluntaryHealth, ContributionPensionSolidarityFund, RTFExcempt, HousingDeducted, StudyDeducted, payrollEndDate, PayrollCheckLiquidation, Retention)("Retenciones")
                                                                                         '    RetentionBase = Me.Retention(IBCRTF, ListPayrollAux.Item(Aux).Employee.ThirdParty.RetentionType, "Porcentaje", 0, PensionEmployee, VoluntaryPension, VoluntaryHealth, ContributionPensionSolidarityFund, RTFExcempt, HousingDeducted, StudyDeducted, payrollEndDate, PayrollCheckLiquidation, Retention)("RetentionBase")

                                                                                         '    Dim MensajeRetencion = Me.Retention(IBCRTF, ListPayrollAux.Item(Aux).Employee.ThirdParty.RetentionType, "Valor", 0, PensionEmployee, VoluntaryPension, VoluntaryHealth, ContributionPensionSolidarityFund, RTFExcempt, HousingDeducted, StudyDeducted, payrollEndDate, PayrollCheckLiquidation, Retention)("Mensaje")

                                                                                         '    If MensajeRetencion = 1 Then
                                                                                         '        MessageLiquitadion = CreateMessage("Calcular la retenciones por el Procedimento 2, es imposible ya que no existen las suficientes nóminas liquidadas confirmadas", False, payrollEndDate)
                                                                                         '        EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                                                                         '    End If

                                                                                         'End If

                                                                                         '*******************************************************************

                                                                                         Dim FormulaConcept = employeeAutorizationConceptEmployer(Emp).Concept.Formulates

                                                                                         Dim ConceptValue As Double
                                                                                         Dim ReplaceFormula As String


                                                                                         Dim ConceptData = Me.ReplaceData(payrollParameter, BasicSalary, WorkDays, WorkHours, PayrollDays, FormulaConcept, payrollEmployee.Item(i), BaseIBCPension, BaseIBCHealth, IBCHealth, BaseIBCHealthEmployer, BaseIBCPensionEmployer, IBCSENA, IBCICBF, _
                                                                                                                         IBCPeriod, IBCSeverance, IBCCompensationFund, IBCPension, IBCARP, ValueAmbulatoryInability, ValueHospitalInability, ValueMaternity, ValueSanction, TotalPeriodDaysInabilities, HospitalInabilityDays, _
                                                                                                                         RemuneratedLicensesDays, UnpaidLicensesDays, ValueUnpaidLicenses, ValueProfesionalInabilities, HealthEmployee, PensionEmployee, BaseIBCArp, BaseParafiscalCompensationFund, BaseParafiscalICBF, RetentionValue, ProvisionDays, ProfessionalRiskPercentage, VacationValue, AdjustVacationValue, incentiePaymentValue, unemployedInterestValue)

                                                                                         ReplaceFormula = ConceptData("Formula")
                                                                                         ConceptValue = CDbl(ConceptData("Valor"))


                                                                                         If ConceptValue > 0 Then

                                                                                             'Horas Extras
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "001" Then HourSchedule = HourSchedule + ConceptValue

                                                                                             'Prima de Servicios
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "002" Then IncentivePaymentServices = IncentivePaymentServices + ConceptValue

                                                                                             'otras nummaxpri
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "003" Then IncetivePaymentServicesExtra = IncetivePaymentServicesExtra + ConceptValue

                                                                                             ' Bonificación por Servicios
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "004" Then BonusServices = BonusServices + ConceptValue

                                                                                             ' Sueldo
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "005" Then Salary = Salary + ConceptValue

                                                                                             ' Acumulado Auxilio de Transporte
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "006" Then AcumulatedHelpTransport = AcumulatedHelpTransport + ConceptValue

                                                                                             'Indemnizaciones()
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "007" Then Compensation = Compensation + ConceptValue

                                                                                             'Cesantias()
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "008" Then Unemployment = Unemployment + ConceptValue

                                                                                             'Aporte Riesgos
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "009" Then RiskContribution = RiskContribution + ConceptValue

                                                                                             'Otros Devengados
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "010" Then OtherAccrued = OtherAccrued + ConceptValue

                                                                                             'Otros Deducidos
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "011" Then OtherDeducted = OtherDeducted + ConceptValue

                                                                                             'Horas Extras Nocturnas
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "012" Then EveningOvertime = EveningOvertime + ConceptValue

                                                                                             'Horas Extras Dominicales
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "013" Then SundayOvertime = SundayOvertime + ConceptValue

                                                                                             'Pensión Empleado
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "014" Then PensionEmployee = PensionEmployee + ConceptValue

                                                                                             'Pensión Patrono
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "015" Then PensionEmployer = PensionEmployer + ConceptValue

                                                                                             'Pensión Voluntaria
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "016" Then VoluntaryPension = VoluntaryPension + ConceptValue

                                                                                             'Salud Empleado
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "017" Then HealthEmployee = HealthEmployee + ConceptValue

                                                                                             'Salud Patrono
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "018" Then HealthEmployer = HealthEmployer + ConceptValue

                                                                                             'Salud Voluntaria
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "019" Then VoluntaryHealth = VoluntaryHealth + ConceptValue

                                                                                             'Retención en la fuente
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "020" Then Withholding = Withholding + ConceptValue

                                                                                             'Incapacidad Ambulatoria
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "021" Then AmbulatoryInability = AmbulatoryInability + ConceptValue

                                                                                             'Incapacidad Hospitalaria
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "022" Then HospitalInability = HospitalInability + ConceptValue

                                                                                             'Maternidad
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "023" Then Maternity = Maternity + ConceptValue

                                                                                             'Licencias
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "024" Then Licenses = Licenses + ConceptValue

                                                                                             'Licencias No Remuneradas
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "025" Then UnpaidLicenses = UnpaidLicenses + ConceptValue

                                                                                             'Sanciones
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "026" Then Sanctions = Sanctions + ConceptValue

                                                                                             'Incapacidad Riesgos Profesionales
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "027" Then InabilityProfessionalRisk = InabilityProfessionalRisk + ConceptValue

                                                                                             'Permisos
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "028" Then Permission = Permission + ConceptValue

                                                                                             'Vacaciones
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "030" Then Vacation = Vacation + ConceptValue

                                                                                             'Provisión Vacaciones
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "031" Then ProvisionVacation = ProvisionVacation + ConceptValue

                                                                                             'Provisión Número Máximo de Primas
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "033" Then MaxNumberIncentiveProvision = MaxNumberIncentiveProvision + ConceptValue

                                                                                             'Provisión Intereses de Cesantías
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "034" Then UnemploymentInterestsProvision = UnemploymentInterestsProvision + ConceptValue

                                                                                             'Provisión SENA
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "035" Then SenaProvision = SenaProvision + ConceptValue

                                                                                             'Provisión Caja de Compensación
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "036" Then CompensationFundProvision = CompensationFundProvision + ConceptValue

                                                                                             'Provisión ICBF
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "037" Then ICBFProvision = ICBFProvision + ConceptValue

                                                                                             'Aporte Fondo de Solidaridad Pensional
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "038" Then ContributionPensionSolidarityFund = ContributionPensionSolidarityFund + ConceptValue

                                                                                             'Deducción por Estudio
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "039" Then StudyDeducted = StudyDeducted + ConceptValue

                                                                                             'Deducción por Vivienda
                                                                                             If employeeAutorizationConceptEmployer(Emp).Concept.ConceptClass = "040" Then HousingDeducted = HousingDeducted + ConceptValue

                                                                                             EmployeeLiquidatedDetail = New LiquidationDetail()

                                                                                             EmployeeLiquidatedDetail.RegisterStatus = 1
                                                                                             EmployeeLiquidatedDetail.PayrollDate = payrollEndDate
                                                                                             EmployeeLiquidatedDetail.LiquidationPeriod = "1"
                                                                                             EmployeeLiquidatedDetail.ConceptClass = employeeAutorizationConceptEmployer(Emp).Concept.ConceptType.ToString()
                                                                                             EmployeeLiquidatedDetail.ConceptId = employeeAutorizationConceptEmployer(Emp).Concept.Id
                                                                                             EmployeeLiquidatedDetail.ConceptCode = employeeAutorizationConceptEmployer(Emp).Concept.Code
                                                                                             EmployeeLiquidatedDetail.ConceptDetail = employeeAutorizationConceptEmployer(Emp).Concept.Name & " Contrato No. " & ContractNumber.ToString()
                                                                                             EmployeeLiquidatedDetail.ConceptTotalValue = ConceptValue

                                                                                             ' Patrono
                                                                                             AccruedValueConcept = ConceptValue

                                                                                             EmployeeLiquidatedDetail.AccruedValue = AccruedValueConcept
                                                                                             EmployeeLiquidatedDetail.DeductedValue = DeductedValueConcept
                                                                                             EmployeeLiquidatedDetail.RetentionBase = 0
                                                                                             EmployeeLiquidatedDetail.RetentionPercentage = 0
                                                                                             EmployeeLiquidatedDetail.InitialBalance = 0
                                                                                             EmployeeLiquidatedDetail.ConceptType = employeeAutorizationConceptEmployer(Emp).Concept.ConceptType
                                                                                             EmployeeLiquidatedDetail.ConceptFormulate = FormulaConcept
                                                                                             EmployeeLiquidatedDetail.ReplaceConceptFormulate = ReplaceFormula
                                                                                             EmployeeLiquidated.LiquidationDetail.Add(EmployeeLiquidatedDetail)

                                                                                             AccruedValueConcept = 0
                                                                                             DeductedValueConcept = 0

                                                                                         End If

                                                                                     End Sub
                    )

                    ''*******************************************************************
                    'For j As Integer = 0 To (employeeAutorizationConcept.Count() - 1)

                    '    WorkHours = 0

                    '    If ScheduleDetailEmployee IsNot Nothing Then
                    '        WorkHours = Me.HourByConcept(HandlesTurnChart, PayrollDays, employeeAutorizationConcept.Item(j), IBCHour, ScheduleDetailEmployee)
                    '        CountWorkHours = CountWorkHours + 1
                    '    Else
                    '        WorkHours = 0
                    '    End If

                    '    ' Cargo la Base de Liquidación de Pensión - Empleado
                    '    Dim BaseIBCPension = Me.GetIBCPension(BasicSalary, payrollParameter.LegalSalaryMinimum, IBCPension, PayrollDays, TotalDaysLicensesUnpaid, ConceptoDevengadoSuma, EmployeePensionContributionPercentage)

                    '    ' Cargo la Base de Liquidación de Salud - Empleado

                    '    BaseIBCHealth = Me.GetIBCHealth(IBCHealth, payrollParameter.LegalSalaryMinimum, BasicSalary, 0, PayrollDays, EmployeeHealthContributionPercentage, ConceptoDevengadoSuma, TotalDaysLicensesUnpaid)

                    '    ' Cargo la Base de Liquidación de Salud - Patrono
                    '    Dim BaseIBCHealthEmployer = Me.GetIBCHealthEmployer(IBCHealth, payrollParameter.LegalSalaryMinimum, BasicSalary, 0, PayrollDays, WorkDays, BaseIBCHealth, IBCHealth, EmployerHealthContributionPercentage, TotalDaysLicensesUnpaid)

                    '    ' Cargo la Base de Liquidación de Pensión - Patrono
                    '    Dim BaseIBCPensionEmployer = Me.GetIBCPensionEmployer(IBCPension, payrollParameter.LegalSalaryMinimum, BasicSalary, TotalDaysLicensesUnpaid, PayrollDays, EmployeePensionContributionPercentage)

                    '    ' Cargo la Base de ARP
                    '    Dim BaseIBCArp = Me.GetBaseARP(BasicSalary, payrollParameter.LegalSalaryMinimum, ARPDays, IBCARP, ProfessionalRiskPercentage)

                    '    ' Cargo la Base Parafiscal de la Caja de Compensación
                    '    Dim BaseParafiscalCompensationFund = Me.GetBaseParafiscalCompensationFund(IBCCompensationFund, PayrollDays, payrollParameter.LegalSalaryMinimum, RemuneratedLicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, CompensationFundContributionPercentage)

                    '    Dim BaseParafiscalICBF = Me.GetBaseParafiscalICBF(IBCICBF, PayrollDays, payrollParameter.LegalSalaryMinimum, RemuneratedLicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, ICBFContributionPercentage)

                    '    ' RETEFUENTE
                    '    'If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "020" Then

                    '    '    'Dim RetentionDateValue = Me.Retention(IBCRTF, payrollEmployee.Employee.ThirdParty.RetentionType, "Valor", 0, PensionEmployee, VoluntaryPension, VoluntaryHealth, ContributionPensionSolidarityFund, RTFExcempt, HousingDeducted, StudyDeducted, payrollEndDate, EmployeeLiquidation, Retention)
                    '    '    'RetentionValue = RetentionDateValue("Retenciones")

                    '    '    PayrollCheckLiquidation = EmployeeLiquidation(ListPayrollAux.Item(Aux).EmployeeId)

                    '    '    RetentionValue = Me.Retention(IBCRTF, ListPayrollAux.Item(Aux).Employee.ThirdParty.RetentionType, "Valor", 0, PensionEmployee, VoluntaryPension, VoluntaryHealth, ContributionPensionSolidarityFund, RTFExcempt, HousingDeducted, StudyDeducted, payrollEndDate, PayrollCheckLiquidation, Retention)("Retenciones")
                    '    '    PercentageRetention = Me.Retention(IBCRTF, ListPayrollAux.Item(Aux).Employee.ThirdParty.RetentionType, "Porcentaje", 0, PensionEmployee, VoluntaryPension, VoluntaryHealth, ContributionPensionSolidarityFund, RTFExcempt, HousingDeducted, StudyDeducted, payrollEndDate, PayrollCheckLiquidation, Retention)("Retenciones")
                    '    '    RetentionBase = Me.Retention(IBCRTF, ListPayrollAux.Item(Aux).Employee.ThirdParty.RetentionType, "Porcentaje", 0, PensionEmployee, VoluntaryPension, VoluntaryHealth, ContributionPensionSolidarityFund, RTFExcempt, HousingDeducted, StudyDeducted, payrollEndDate, PayrollCheckLiquidation, Retention)("RetentionBase")

                    '    '    Dim MensajeRetencion = Me.Retention(IBCRTF, ListPayrollAux.Item(Aux).Employee.ThirdParty.RetentionType, "Valor", 0, PensionEmployee, VoluntaryPension, VoluntaryHealth, ContributionPensionSolidarityFund, RTFExcempt, HousingDeducted, StudyDeducted, payrollEndDate, PayrollCheckLiquidation, Retention)("Mensaje")

                    '    '    If MensajeRetencion = 1 Then
                    '    '        MessageLiquitadion = CreateMessage("Calcular la retenciones por el Procedimento 2, es imposible ya que no existen las suficientes nóminas liquidadas confirmadas", False, payrollEndDate)
                    '    '        EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    '    '    End If

                    '    'End If

                    '    '*******************************************************************

                    '    Dim FormulaConcept = employeeAutorizationConcept.Item(j).Concept.Formulates

                    '    Dim ConceptValue As Double
                    '    Dim ReplaceFormula As String


                    '    Dim ConceptData = Me.ReplaceData(payrollParameter, BasicSalary, WorkDays, WorkHours, PayrollDays, FormulaConcept, payrollEmployee.Item(i), BaseIBCPension, BaseIBCHealth, IBCHealth, BaseIBCHealthEmployer, BaseIBCPensionEmployer, IBCSENA, IBCICBF, _
                    '                                    IBCPeriod, IBCSeverance, IBCCompensationFund, IBCPension, IBCARP, ValueAmbulatoryInability, ValueHospitalInability, ValueMaternity, ValueSanction, TotalPeriodDaysInabilities, HospitalInabilityDays, _
                    '                                    RemuneratedLicensesDays, UnpaidLicensesDays, ValueUnpaidLicenses, ValueProfesionalInabilities, HealthEmployee, PensionEmployee, BaseIBCArp, BaseParafiscalCompensationFund, BaseParafiscalICBF, RetentionValue, ProvisionDays, ProfessionalRiskPercentage, VacationValue, AdjustVacationValue)

                    '    ReplaceFormula = ConceptData("Formula")
                    '    ConceptValue = CDbl(ConceptData("Valor"))


                    '    If ConceptValue > 0 Then

                    '        'Horas Extras
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "001" Then HourSchedule = HourSchedule + ConceptValue

                    '        'Prima de Servicios
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "002" Then IncentivePaymentServices = IncentivePaymentServices + ConceptValue

                    '        'otras nummaxpri
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "003" Then IncetivePaymentServicesExtra = IncetivePaymentServicesExtra + ConceptValue

                    '        ' Bonificación por Servicios
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "004" Then BonusServices = BonusServices + ConceptValue

                    '        ' Sueldo
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "005" Then Salary = Salary + ConceptValue

                    '        ' Acumulado Auxilio de Transporte
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "006" Then AcumulatedHelpTransport = AcumulatedHelpTransport + ConceptValue

                    '        'Indemnizaciones()
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "007" Then Compensation = Compensation + ConceptValue

                    '        'Cesantias()
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "008" Then Unemployment = Unemployment + ConceptValue

                    '        'Aporte Riesgos
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "009" Then RiskContribution = RiskContribution + ConceptValue

                    '        'Otros Devengados
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "010" Then OtherAccrued = OtherAccrued + ConceptValue

                    '        'Otros Deducidos
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "011" Then OtherDeducted = OtherDeducted + ConceptValue

                    '        'Horas Extras Nocturnas
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "012" Then EveningOvertime = EveningOvertime + ConceptValue

                    '        'Horas Extras Dominicales
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "013" Then SundayOvertime = SundayOvertime + ConceptValue

                    '        'Pensión Empleado
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "014" Then PensionEmployee = PensionEmployee + ConceptValue

                    '        'Pensión Patrono
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "015" Then PensionEmployer = PensionEmployer + ConceptValue

                    '        'Pensión Voluntaria
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "016" Then VoluntaryPension = VoluntaryPension + ConceptValue

                    '        'Salud Empleado
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "017" Then HealthEmployee = HealthEmployee + ConceptValue

                    '        'Salud Patrono
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "018" Then HealthEmployer = HealthEmployer + ConceptValue

                    '        'Salud Voluntaria
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "019" Then VoluntaryHealth = VoluntaryHealth + ConceptValue

                    '        'Retención en la fuente
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "020" Then Withholding = Withholding + ConceptValue

                    '        'Incapacidad Ambulatoria
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "021" Then AmbulatoryInability = AmbulatoryInability + ConceptValue

                    '        'Incapacidad Hospitalaria
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "022" Then HospitalInability = HospitalInability + ConceptValue

                    '        'Maternidad
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "023" Then Maternity = Maternity + ConceptValue

                    '        'Licencias
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "024" Then Licenses = Licenses + ConceptValue

                    '        'Licencias No Remuneradas
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "025" Then UnpaidLicenses = UnpaidLicenses + ConceptValue

                    '        'Sanciones
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "026" Then Sanctions = Sanctions + ConceptValue

                    '        'Incapacidad Riesgos Profesionales
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "027" Then InabilityProfessionalRisk = InabilityProfessionalRisk + ConceptValue

                    '        'Permisos
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "028" Then Permission = Permission + ConceptValue

                    '        'Vacaciones
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "030" Then Vacation = Vacation + ConceptValue

                    '        'Provisión Vacaciones
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "031" Then ProvisionVacation = ProvisionVacation + ConceptValue

                    '        'Provisión Número Máximo de Primas
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "033" Then MaxNumberIncentiveProvision = MaxNumberIncentiveProvision + ConceptValue

                    '        'Provisión Intereses de Cesantías
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "034" Then UnemploymentInterestsProvision = UnemploymentInterestsProvision + ConceptValue

                    '        'Provisión SENA
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "035" Then SenaProvision = SenaProvision + ConceptValue

                    '        'Provisión Caja de Compensación
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "036" Then CompensationFundProvision = CompensationFundProvision + ConceptValue

                    '        'Provisión ICBF
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "037" Then ICBFProvision = ICBFProvision + ConceptValue

                    '        'Aporte Fondo de Solidaridad Pensional
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "038" Then ContributionPensionSolidarityFund = ContributionPensionSolidarityFund + ConceptValue

                    '        'Deducción por Estudio
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "039" Then StudyDeducted = StudyDeducted + ConceptValue

                    '        'Deducción por Vivienda
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptClass = "040" Then HousingDeducted = HousingDeducted + ConceptValue


                    '        'Antes de Ingresar se revisan los Descuentos
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptType = "1" Then

                    '            For l As Integer = 0 To (employeeAutorizationConcept.Item(j).Concept.ConceptGroup.Count() - 1)
                    '                If employeeAutorizationConcept.Item(j).Concept.ConceptGroup.Item(l).MaximunDiscount = True Then

                    '                    'Porcentaje de Descuento Máximo
                    '                    If (ConceptoDevengadoSuma * MaximunDiscountPercentage / 100) < ConceptoDeducidoSuma + ConceptValue Then
                    '                        MessageLiquitadion = CreateMessage("El Concepto " & employeeAutorizationConcept.Item(j).Concept.Name & " sobrepasa el % de Descuento Máximo", True, payrollEndDate)
                    '                        EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    '                    End If

                    '                    'Porcentaje Descuento Estudio y Salud
                    '                    If (employeeAutorizationConcept.Item(j).Concept.Code = "017" Or employeeAutorizationConcept.Item(j).Concept.Code = "039") Then
                    '                        If (ConceptoDevengadoSuma * EducationStudyDiscountPercentage / 100) < ConceptoDeducidoSuma + ConceptValue Then
                    '                            MessageLiquitadion = CreateMessage("El Concepto " & employeeAutorizationConcept.Item(j).Concept.Name & " sobrepasa el % de Deduccion de Salud", True, payrollEndDate)
                    '                            EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    '                        Else
                    '                            MessageLiquitadion = CreateMessage("El Concepto " & employeeAutorizationConcept.Item(j).Concept.Name & " sobrepasa el % de Deduccion de Estudio", True, payrollEndDate)
                    '                            EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    '                        End If
                    '                    End If

                    '                    'Deducción máxima por vivienda
                    '                    If employeeAutorizationConcept.Item(j).Concept.Code = "040" Then
                    '                        If HousingDeductionMaximumValue < ConceptValue Then
                    '                            MessageLiquitadion = CreateMessage("El Concepto " & employeeAutorizationConcept.Item(j).Concept.Name & " sobrepasa el valor de Deducción Máximo por Vivienda", True, payrollEndDate)
                    '                            EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    '                        End If
                    '                    End If

                    '                    'SMLV maximos para liquidar salud y pension
                    '                    If (employeeAutorizationConcept.Item(j).Concept.Code = "014" Or employeeAutorizationConcept.Item(j).Concept.Code = "017") Then
                    '                        If MaximunDiscountPercentage < (ConceptoDevengadoSuma / legalMinimimunSalary) Then
                    '                            If employeeAutorizationConcept.Item(j).Concept.Code = "014" Then
                    '                                MessageLiquitadion = CreateMessage("El Concepto " & employeeAutorizationConcept.Item(j).Concept.Name & " sobrepasa el numero de SMLV maximos para liquidar pensión", True, payrollEndDate)
                    '                                EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    '                            Else
                    '                                MessageLiquitadion = CreateMessage("El Concepto " & employeeAutorizationConcept.Item(j).Concept.Name & " sobrepasa el numero de SMLV maximos para liquidar salud", True, payrollEndDate)
                    '                                EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    '                            End If
                    '                        End If
                    '                    End If
                    '                End If
                    '            Next

                    '        End If


                    '        EmployeeLiquidatedDetail = New LiquidationDetail()
                    '        EmployeeLiquidatedDetail.RegisterStatus = 1
                    '        EmployeeLiquidatedDetail.PayrollDate = payrollEndDate
                    '        EmployeeLiquidatedDetail.LiquidationPeriod = "1"
                    '        EmployeeLiquidatedDetail.ConceptClass = employeeAutorizationConcept.Item(j).Concept.ConceptType.ToString()
                    '        EmployeeLiquidatedDetail.ConceptId = employeeAutorizationConcept.Item(j).Concept.Id
                    '        EmployeeLiquidatedDetail.ConceptDetail = employeeAutorizationConcept.Item(j).Concept.Name & " Contrato No. " & ContractNumber.ToString()
                    '        EmployeeLiquidatedDetail.ConceptTotalValue = ConceptValue

                    '        ' Valido si el concepto es Devengado o Deducido para Sumarlos 
                    '        If employeeAutorizationConcept.Item(j).Concept.ConceptType.ToString() = "1" Then
                    '            ' Devengando
                    '            ConceptoDevengadoSuma = ConceptoDevengadoSuma + ConceptValue
                    '            AccruedValueConcept = ConceptValue

                    '            ' Cargo los IBC's
                    '            If employeeAutorizationConcept.Item(j).Concept.AffectIBC = True Then IBCPeriod = IBCPeriod + ConceptValue
                    '            If employeeAutorizationConcept.Item(j).Concept.AffectIBCRTF = True Then IBCRTF = IBCRTF + ConceptValue
                    '            If employeeAutorizationConcept.Item(j).Concept.AffectIBCSENA = True Then IBCSENA = IBCSENA + ConceptValue
                    '            If employeeAutorizationConcept.Item(j).Concept.AffectIBCICBF = True Then IBCICBF = IBCICBF + ConceptValue
                    '            If employeeAutorizationConcept.Item(j).Concept.AffectIBCCompensationFund = True Then IBCCompensationFund = IBCCompensationFund + ConceptValue
                    '            If employeeAutorizationConcept.Item(j).Concept.AffectIBCSeverance = True Then IBCSeverance = IBCSeverance + ConceptValue
                    '            If employeeAutorizationConcept.Item(j).Concept.AffectIBCHealth = True Then IBCHealth = IBCHealth + ConceptValue
                    '            If employeeAutorizationConcept.Item(j).Concept.AffectIBCPension = True Then IBCPension = IBCPension + ConceptValue
                    '            If employeeAutorizationConcept.Item(j).Concept.AffectIBCARP = True Then IBCARP = IBCARP + ConceptValue
                    '            If employeeAutorizationConcept.Item(j).Concept.AffectIBCVacation = True Then IBCVacation = IBCVacation + ConceptValue
                    '            If employeeAutorizationConcept.Item(j).Concept.AffectIBCIncentivePayment = True Then IBCIncentivePayment = IBCIncentivePayment + ConceptValue


                    '        ElseIf employeeAutorizationConcept.Item(j).Concept.ConceptType.ToString() = "2" Then
                    '            ' Deducido
                    '            ConceptoDeducidoSuma = ConceptoDeducidoSuma + ConceptValue
                    '            DeductedValueConcept = ConceptValue

                    '        ElseIf employeeAutorizationConcept.Item(j).Concept.ConceptType.ToString() = "3" Then
                    '            ' Patrono
                    '            DeductedValueConcept = ConceptValue
                    '        End If


                    '        EmployeeLiquidatedDetail.AccruedValue = AccruedValueConcept
                    '        EmployeeLiquidatedDetail.DeductedValue = DeductedValueConcept
                    '        EmployeeLiquidatedDetail.RetentionBase = 0
                    '        EmployeeLiquidatedDetail.RetentionPercentage = 0
                    '        EmployeeLiquidatedDetail.InitialBalance = 0
                    '        EmployeeLiquidatedDetail.ConceptType = employeeAutorizationConcept.Item(j).Concept.ConceptType
                    '        EmployeeLiquidatedDetail.ConceptFormulate = FormulaConcept
                    '        EmployeeLiquidatedDetail.ReplaceConceptFormulate = ReplaceFormula
                    '        EmployeeLiquidated.LiquidationDetail.Add(EmployeeLiquidatedDetail)

                    '        AccruedValueConcept = 0
                    '        DeductedValueConcept = 0

                    '    End If



                    'Next

                    If ConceptoDevengadoSuma < ConceptoDeducidoSuma Then
                        MessageLiquitadion = CreateMessage("El Total Devengado es MENOS que el Total Deducido", True, payrollEndDate)
                        EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    End If

                    If CountWorkHours <= 0 Then
                        MessageLiquitadion = CreateMessage("El empleado NO tuvo registradas Horas en el Cuadro de Turno", False, payrollEndDate)
                        EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    End If

                    If WorkHours > maxHourAmount Then
                        MessageLiquitadion = CreateMessage("Las Horas en el Cuadro de Turno superan en " & (WorkHours - maxHourAmount) & " a las Horas Máximas del Cargo", False, payrollEndDate)
                        EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    End If

                    ' Calculo el IBC Cesantías SIN sanciones
                    If SanctionsDays > 0 Then
                        IBCSeveranceNoSanctions = (IBCSeverance * DaysWorkedEmployee) / WorkDays
                    Else
                        IBCSeveranceNoSanctions = IBCSeverance
                    End If

                    WorkCenterId = ListPayrollAux.Item(Aux).Employee.WorkCenterId

                    EmployeeLiquidated.Contract = ListPayrollAux.Item(Aux)


                    EmployeeLiquidated.WorkCenterId = WorkCenterId
                    EmployeeLiquidated.GroupId = ListPayrollAux.Item(Aux).GroupId  'payrollEmployee.Item(i).GroupId
                    EmployeeLiquidated.EmployeeId = ListPayrollAux.Item(Aux).EmployeeId ' payrollEmployee.Item(i).EmployeeId
                    EmployeeLiquidated.ContractId = ListPayrollAux.Item(Aux).Id ' payrollEmployee.Item(i).Id
                    EmployeeLiquidated.RegisterStatus = "C"
                    EmployeeLiquidated.PayrollDateLiquidated = payrollEndDate
                    EmployeeLiquidated.LiquidationPeriod = PayrollLiquidation.ToString()
                    EmployeeLiquidated.BasicSalary = Salary
                    EmployeeLiquidated.SalaryType = ListPayrollAux.Item(Aux).ContractType.SalaryType.ToString() 'payrollEmployee.Item(i).ContractType.SalaryType.ToString()
                    EmployeeLiquidated.PayrollDays = PayrollDays
                    EmployeeLiquidated.DaysWorked = TotalWorkDays
                    EmployeeLiquidated.ProvisionDays = ProvisionDays
                    EmployeeLiquidated.Overtime = HourSchedule
                    EmployeeLiquidated.EveningOvertime = EveningOvertime
                    EmployeeLiquidated.DiurnalOvertime = SundayOvertime
                    EmployeeLiquidated.HoursHolidays = 0 ' Revisar
                    EmployeeLiquidated.HolidaysEveningHours = 0 ' Revisar
                    EmployeeLiquidated.ValueTransportingRelief = AcumulatedHelpTransport
                    EmployeeLiquidated.VacationDays = VacationDays
                    EmployeeLiquidated.VacationValueEnjoy = VacationValue
                    EmployeeLiquidated.VacationValueLiquidated = 0 ' Revisar
                    EmployeeLiquidated.BonusValueServices = IncentivePaymentServices
                    EmployeeLiquidated.ValueOtherBonuses = IncetivePaymentServicesExtra
                    EmployeeLiquidated.PensionFundId = PensionFundId
                    EmployeeLiquidated.PensionContributionValue = PensionEmployee
                    EmployeeLiquidated.PensionContributionDays = PensionDays
                    EmployeeLiquidated.PensionEnrollmentDays = AfiliationPensionDays
                    EmployeeLiquidated.EmployerPensionContributionValue = PensionEmployer
                    EmployeeLiquidated.VoluntaryPensionFundId = PensionVoluntaryFundId
                    EmployeeLiquidated.VoluntaryContributionPensionValue = VoluntaryPension
                    EmployeeLiquidated.PensionSolidarityFundId = Nothing ' Revisar OJO
                    EmployeeLiquidated.PensionSolidarityFundValueContribution = 0 ' Revisar OJO
                    EmployeeLiquidated.PensionSolidarityFundContributionDays = 0 ' Revisar OJO
                    EmployeeLiquidated.PensionSolidarityFundEnrollmentDays = 0 ' Revisar OJO
                    EmployeeLiquidated.EmployeeHealthContributionValue = HealthEmployee
                    EmployeeLiquidated.HealthFundId = HealthFundId
                    EmployeeLiquidated.QuoteHealthDays = HealthDays
                    EmployeeLiquidated.AffiliateHealthDays = AfiliationHealthDays
                    EmployeeLiquidated.EmployerHealthContributionValue = HealthEmployer
                    EmployeeLiquidated.AdditionalPUTValue = 0 ' Revisar OJO
                    EmployeeLiquidated.HealthJCBLicenses = 0 ' Programa Antiguo así
                    EmployeeLiquidated.HealthContributionLicensesValue = 0 ' Programa Antiguo así
                    EmployeeLiquidated.HealthLicensesDays = 0 ' Programa Antiguo así
                    EmployeeLiquidated.VoluntaryHealthFundId = HealthVoluntaryFundId
                    EmployeeLiquidated.VoluntaryHealthContributionValue = VoluntaryHealth
                    EmployeeLiquidated.TotalBaseRetention = IBCRTF
                    EmployeeLiquidated.ExemptValueRetention = RetentionBase
                    EmployeeLiquidated.RealBaseRetention = IBCRTF - RetentionBase
                    EmployeeLiquidated.CalculatedWithholdingValue = RetentionValue
                    EmployeeLiquidated.RetentionPercentageApplied = PercentageRetention
                    EmployeeLiquidated.AccumulatedOtherAccrued = OtherAccrued
                    EmployeeLiquidated.AccumulatedOtherDeducted = OtherDeducted
                    EmployeeLiquidated.DeductingAccumulated = ConceptoDeducidoSuma
                    EmployeeLiquidated.PeriodJCB = IBCPeriod
                    EmployeeLiquidated.PensionJCB = IBCPension
                    EmployeeLiquidated.HealthJCB = IBCHealth
                    EmployeeLiquidated.AccumulatedBenefit = HealthEmployee + VoluntaryHealth + PensionEmployee + VoluntaryPension ' Acumulado de Prestaciones
                    EmployeeLiquidated.AccumulatedDisabilityValue = TotalPeriodValuesInabilities
                    EmployeeLiquidated.DisabilityDays = TotalPeriodDaysInabilities
                    EmployeeLiquidated.AmbulatoryDisabilityDays = AmbulatoryInabilityDays
                    EmployeeLiquidated.AmbulatoryDisabilityInitialDate = AmbulatoryInabilityInitialDate
                    EmployeeLiquidated.AmbulatoryDisabilityEndDate = AmbulatoryInabilityEndDate
                    EmployeeLiquidated.AmbulatoryDisabilityAuthorizationNumber = AutorizationNumberAmbulatoryInability
                    EmployeeLiquidated.AmbulatoryDisabilityValue = ValueAmbulatoryInability
                    EmployeeLiquidated.DisabilityHospitalInitialDate = HospitalInabilityInitialDate
                    EmployeeLiquidated.DisabilityHospitalEndDate = HospitalInabilityEndDate
                    EmployeeLiquidated.DisabilityHospitalReleasedNumber = AutorizationNumberHospitalInability
                    EmployeeLiquidated.DisabilityHospitalDays = HospitalInabilityDays
                    EmployeeLiquidated.DisabilityHospitalValue = ValueHospitalInability
                    EmployeeLiquidated.MaternityLeaveDays = MaternityDays
                    EmployeeLiquidated.MaternityLeaveInitialDate = MaternityInitialDate
                    EmployeeLiquidated.MaternityLeaveEndDate = MaternityEndDate
                    EmployeeLiquidated.MaternityLeaveAutorizationNumber = AutorizationNumberMaternity
                    'EmployeeLiquidated.VacationInitialDate =
                    'EmployeeLiquidated.VacationEndDate =
                    EmployeeLiquidated.LicenseDays = RemuneratedLicensesDays
                    EmployeeLiquidated.LicenseValue = ValueRemuneratedLicenses
                    EmployeeLiquidated.UnpaidLicenseValue = ValueUnpaidLicenses
                    EmployeeLiquidated.UnpaidLicenseAutorizarionNumber = AutorizationNumberUnpaidLicenses
                    EmployeeLiquidated.UnpaidLicenseDays = UnpaidLicensesDays
                    EmployeeLiquidated.UnpaidLicenseInitialDate = UnpaidLicensesInitialDate
                    EmployeeLiquidated.UnpaidLicenseEndDate = UnpaidLicensesEndDate
                    EmployeeLiquidated.SanctionInitialDate = SanctionsInitialDate
                    EmployeeLiquidated.SanctionEndDate = SanctionsEndDate
                    EmployeeLiquidated.SanctionValue = ValueSanction
                    EmployeeLiquidated.SanctionDays = SanctionsDays
                    EmployeeLiquidated.OccupationalRisksContributionValue = RiskContribution
                    EmployeeLiquidated.OccupationalRisksDisabilityValue = ValueProfesionalInabilities
                    EmployeeLiquidated.OccupationalRisksDays = ProfesionalInabilitiesDays
                    EmployeeLiquidated.OccupationalRisksDisabilityAutorizationNumber = AutorizationNumberInabilityARP
                    EmployeeLiquidated.OccupationalRisksDisabilityInitialDate = InabilityARPInitialDate
                    EmployeeLiquidated.OccupationalRisksDisabilityEndDate = InabilityARPEndDate
                    EmployeeLiquidated.OccupationalRisksFundId = RiskFundId
                    EmployeeLiquidated.PermissionsValue = 0 ' Revisar OJO
                    EmployeeLiquidated.UnemploymentAccumulated = Unemployment
                    EmployeeLiquidated.CompensationAccumulated = 0 ' Revisar OJO
                    EmployeeLiquidated.VacationHealthJCB = 0 ' Programa Antiguo así
                    EmployeeLiquidated.VacationValueEnjoy = 0
                    EmployeeLiquidated.VacationHealthContributionValueEmployee = VacationHealthEmployee
                    EmployeeLiquidated.VacationHealthContributionValueEmployer = VacationHealthEmployer
                    EmployeeLiquidated.VacationPensionJCB = 0 ' Programa Antiguo así
                    EmployeeLiquidated.VacationPensionContributionValueEmployee = VacationPensionEmployee
                    EmployeeLiquidated.VacationPensionContributionValueEmployer = VacationPensionEmployer
                    EmployeeLiquidated.AccountingVouchersNumber = 0 ' Programa Antiguo así
                    EmployeeLiquidated.TotalAccrued = ConceptoDevengadoSuma
                    EmployeeLiquidated.TotalDeducted = ConceptoDeducidoSuma
                    EmployeeLiquidated.TotalPaid = ConceptoDevengadoSuma - ConceptoDeducidoSuma
                    EmployeeLiquidated.PermissionDays = 0 ' Revisar OJO
                    EmployeeLiquidated.SenaContributionValue = SenaProvision
                    EmployeeLiquidated.FamilyCompensationFundContributionValue = CompensationFundProvision
                    EmployeeLiquidated.ICBFContributionValue = ICBFProvision
                    EmployeeLiquidated.ParafiscalContribution = SenaProvision + CompensationFundProvision + ICBFProvision
                    EmployeeLiquidated.ProvisionsValue = Unemployment + UnemploymentInterestsProvision + MaxNumberIncentiveProvision + ProvisionVacation
                    EmployeeLiquidated.VacationNumberBussinesDays = 0 ' Revisar OJO
                    EmployeeLiquidated.CompletePayroll = completePayroll
                    EmployeeLiquidated.PayrollProcessDate = Date.Today()
                    EmployeeLiquidated.PayrollConfirmationDate = Date.Today()
                    EmployeeLiquidated.PayrollProcessUser = 1
                    EmployeeLiquidated.PayrollConfirmationUser = 1

                    EmployeeLiquidated.IBCUnemployment = IBCSeverance
                    EmployeeLiquidated.IBCSENA = IBCSENA
                    EmployeeLiquidated.IBCCompensationFund = IBCCompensationFund
                    EmployeeLiquidated.IBCICBF = IBCICBF

                    EmployeeLiquidated.IBCUnemploymentNoSanctions = IBCSeveranceNoSanctions

                    EmployeeLiquidated.IBCVacation = IBCVacation
                    EmployeeLiquidated.IBCIncentivePayment = IBCIncentivePayment

                    EmployeeLiquidated.IBCOccupationalRisks = IBCARP
                    EmployeeLiquidated.QuotedOccupationalRisksDays = CompensationFundDays
                    EmployeeLiquidated.QuotedCompensationDays = CompensationFundDays

                    If PayrollConfirmada = "C" Then
                        EmployeeLiquidated.Contract = ListPayrollAux.Item(Aux)
                        EmployeeLiquidated.Contract.LastLiquidationDate = payrollEndDate
                    End If

                    ' Limpiamos las variables
                    ConceptoDeducidoSuma = 0
                    ConceptoDevengadoSuma = 0
                    IBCSENA = 0
                    IBCRTF = 0
                    IBCICBF = 0
                    IBCSeverance = 0
                    IBCCompensationFund = 0
                    IBCHealth = 0
                    IBCPension = 0
                    IBCARP = 0
                    IBCPeriod = 0
                    TotalAccrued = 0
                    TotalDeducted = 0
                    HoursSchedule = 0
                    IncentivePaymentServices = 0
                    IncetivePaymentServicesExtra = 0
                    BonusServices = 0
                    Salary = 0
                    AcumulatedHelpTransport = 0
                    Compensation = 0
                    Unemployment = 0
                    RiskContribution = 0
                    OtherAccrued = 0
                    OtherDeducted = 0
                    EveningOvertime = 0
                    SundayOvertime = 0
                    PensionEmployee = 0
                    PensionEmployer = 0
                    VoluntaryPension = 0
                    HealthEmployee = 0
                    HealthEmployer = 0
                    VoluntaryHealth = 0
                    Withholding = 0
                    AmbulatoryInability = 0
                    HospitalInability = 0
                    Maternity = 0
                    Licenses = 0
                    UnpaidLicenses = 0
                    Sanctions = 0
                    InabilityProfessionalRisk = 0
                    Permission = 0
                    Vacation = 0
                    ProvisionVacation = 0
                    MaxNumberIncentiveProvision = 0
                    UnemploymentInterestsProvision = 0
                    SenaProvision = 0
                    CompensationFundProvision = 0
                    ICBFProvision = 0
                    ContributionPensionSolidarityFund = 0
                    StudyDeducted = 0
                    HousingDeducted = 0
                    DeductedValueConcept = 0

                    TotalValuesAmbulatoryInabilitiy = 0
                    AmbulatoryInabilityDays = 0
                    ValueAmbulatoryInability = 0
                    TotalDaysHospitalInability = 0
                    HospitalInabilityDays = 0
                    ValueHospitalInability = 0
                    TotalValueHospitalInability = 0
                    TotalDaysMaternity = 0
                    MaternityDays = 0
                    ValueMaternity = 0
                    TotalDaysInabilityARP = 0
                    TotalValueInabilityARP = 0
                    ValueProfesionalInabilities = 0
                    ProfesionalInabilitiesDays = 0
                    SanctionsDays = 0
                    ValueSanction = 0
                    TotalValueSanctions = 0
                    TotalDaysLicensesUnpaid = 0
                    TotalValueLicensesUnpaid = 0
                    TotalDaysLicensesRemunerated = 0
                    TotalValueLicensesRemunerated = 0
                    WorkDays = 0
                    TotalWorkDays = 0
                    DaysWorkedEmployee = 0
                    TotalPeriodDaysInabilities = 0
                    UnpaidLicensesDays = 0
                    VacationDays = 0

                    IBCVacation = 0
                    IBCIncentivePayment = 0
                    IBCSeveranceNoSanctions = 0
                    'ContadorContratos = 0


                Next


            End If

            ListLiquidation.Add(EmployeeLiquidated)


        Next

        Return ListLiquidation


    End Function

    Public Function Retention(ByVal searchValue As Double, procedure As Integer, returnValue As String, balanceEmployee As Double, PensionEmployee As Double, VoluntaryPension As Double, VoluntaryHealth As Double, ContributionPensionSolidarityFund As Double, RTFExcempt As Double, HousingDeducted As Double, StudyDeducted As Double, payrollEndDate As Date, employeeLiquidation As List(Of Liquidation), varRetention As Retention, Optional ValorPro2 As Double = 0) As Dictionary(Of String, Double)

        Dim RetentionBase As Double
        Dim UVTValue As Double
        Dim Orden As Integer
        Dim DescuentoRA As Double
        Dim Base As Double
        Dim Tarifa As Double
        Dim RetentionValue As Double
        Dim Impuesto As Double
        Dim RetencionesPesos As Double
        Dim Retenciones As Double
        Dim Porcentaje As Double
        Dim Mensaje As Double = 0

        Dim descriptions As Dictionary(Of String, Double)

        If procedure = 1 Then
            searchValue = searchValue + PensionEmployee + VoluntaryPension + VoluntaryHealth + ContributionPensionSolidarityFund
            searchValue = searchValue * (100 - (RTFExcempt)) / 100
            searchValue = searchValue - HousingDeducted - StudyDeducted
        Else
            ' Promedio 12 Meses
            searchValue = 0

            Dim RangoI = DateAdd(DateInterval.Month, -12, CDate("01/" & Month(payrollEndDate) & "/" & Year(payrollEndDate)))
            Dim RangoF = CDate("01/" & Month(payrollEndDate) & "/" & Year(payrollEndDate))

            Dim varEmployeeLiquitadion = employeeLiquidation.FindAll(Function(x) x.PayrollDateLiquidated >= RangoI And x.PayrollDateLiquidated <= RangoF)

            If varEmployeeLiquitadion.Count() <= 0 Then
                If returnValue = "Valor" Then
                    Mensaje = 1
                    'CreateMessage("Calcular la retenciones por el Procedimento 2, es imposible ya que no existen las suficientes nominas liquidadas confirmadas", False, payrollEndDate)

                End If
            End If

            If varEmployeeLiquitadion.Count < 12 Then
                If returnValue = "Valor" Then
                    Mensaje = 1
                    'CreateMessage("Calcular la retenciones por el Procedimento 2, es imposible ya que no existen las suficientes nominas liquidadas confirmadas", False, payrollEndDate)
                End If
            Else
                For i As Integer = 0 To (varEmployeeLiquitadion.Count() - 1)
                    searchValue = searchValue + varEmployeeLiquitadion.Item(i).TotalBaseRetention
                Next
            End If

            ValorPro2 = ValorPro2 * (100 - (RTFExcempt))
        End If

        If returnValue <> "Porcentaje" Then
            RetentionBase = 0
            RetentionBase = searchValue
        End If

        If searchValue <= 0 Then searchValue = 1
        If ValorPro2 <= 0 Then ValorPro2 = 1

        searchValue = RoundedValues(searchValue) - balanceEmployee

        ValorPro2 = RoundedValues(ValorPro2)

        If procedure = 1 Then
            UVTValue = RoundedValues((searchValue / varRetention.UVTValue))

            If UVTValue > 9999 Then
                UVTValue = 9999
            End If

            If UVTValue < 1 Then UVTValue = 1

            If varRetention.UVTMin <= UVTValue And varRetention.UVTMax >= UVTValue Then
                Orden = varRetention.Consecutive

                If Orden > 1 Then
                    DescuentoRA = varRetention.UVTMax
                    Base = RoundedValues(UVTValue) - RoundedValues(DescuentoRA)
                    Tarifa = varRetention.Rate
                    RetentionValue = RoundedValues(((searchValue / varRetention.UVTValue) - DescuentoRA) * (Tarifa / 100))
                    Impuesto = varRetention.AdditionalRate
                    RetentionValue = RetentionValue + Impuesto
                    RetencionesPesos = (((searchValue / varRetention.UVTValue) - DescuentoRA) * (Tarifa / 100) + Impuesto) * varRetention.UVTValue
                    Retenciones = Math.Round((CDec(RetencionesPesos) / 1000), 0) * 1000
                    If returnValue = "Porcentaje" Then
                        Retenciones = Tarifa
                    End If
                Else
                    Retenciones = 0
                End If
            End If
        Else
            UVTValue = RoundedValues((searchValue / varRetention.UVTValue))

            If UVTValue > 9999 Then
                UVTValue = 9999
            End If

            If UVTValue = 0 Then UVTValue = 1

            If varRetention.UVTMin <= UVTValue And varRetention.UVTMax >= UVTValue Then
                Orden = varRetention.Consecutive

                If Orden <> 1 Then
                    DescuentoRA = varRetention.UVTMax
                    Base = RoundedValues(UVTValue) - RoundedValues(DescuentoRA)
                    Tarifa = varRetention.Rate
                    RetentionValue = RoundedValues(((searchValue / varRetention.UVTValue) - DescuentoRA) * (Tarifa / 100))
                    Impuesto = varRetention.AdditionalRate
                    RetentionValue = RetentionValue + Impuesto
                    Porcentaje = RoundedValues((((searchValue / varRetention.UVTValue) - DescuentoRA) * (Tarifa / 100) + Impuesto) / UVTValue)
                    RetencionesPesos = ((((searchValue / varRetention.UVTValue) - DescuentoRA) * (Tarifa / 100) + Impuesto) / varRetention.UVTValue) * ValorPro2
                    Retenciones = Math.Round((CDec(RetencionesPesos) / 1000), 0) * 1000
                    If returnValue = "Porcentaje" Then
                        Retenciones = Tarifa
                    End If
                Else
                    Retenciones = 0
                End If
            End If

        End If

        If varRetention Is Nothing Then
            Retenciones = 0
        End If

        descriptions = New Dictionary(Of String, Double)
        descriptions.Add("Retenciones", Retenciones)
        descriptions.Add("RetentionBase", RetentionBase)
        descriptions.Add("Mensaje", Mensaje)

        Return descriptions

    End Function

    ''' <summary>
    ''' Función para Crear Mensajes
    ''' </summary>
    ''' <param name="Description">Descripción del Mensaje</param>
    ''' <param name="ErrorType">Tipo de Error: True: Grave, False: Informativos</param>
    ''' <param name="payrollDate">Fecha de Liquidación</param>
    ''' <returns>Mensaje</returns>
    ''' <remarks></remarks>
    Public Function CreateMessage(ByVal Description As String, ByVal ErrorType As Boolean, payrollDate As Date) As Message Implements ILiquidationDomain.CreateMessage

        Dim MessageLiquidation As New Message

        MessageLiquidation.Description = Description
        MessageLiquidation.Error = ErrorType
        MessageLiquidation.PayrollDate = payrollDate

        Return MessageLiquidation

    End Function

End Class
