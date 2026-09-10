'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 03-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports System.Globalization
Imports Application.Payroll
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.Data.PayrollRepository

Public Class IncreaseSalaryAdminService
    Implements IIncreaseSalaryAdminService

    ''' <summary>
    ''' 'Repositorio de Empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private _employeeRepository As IEmployeeRepository

    Private _increaseSalaryDomain As IIncreaseSalaryDomain

    ''' <summary>
    ''' Repositorio de Contratos, para el contrato Actual
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractRepositoryActual As IContractRepository

    ''' <summary>
    ''' Repositorio de Contratos, para el contrato nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractRepositoryNew As IContractRepository

    ''' <summary>
    ''' Repositorio de Grupos
    ''' </summary>
    ''' <remarks></remarks>
    Private _groupRepository As IGroupRepository

    ''' <summary>
    ''' Repositorio de Retroactividad
    ''' </summary>
    ''' <remarks></remarks>
    Private _RetroactiveCRepository As IRetroactiveCRepository

    ''' <summary>
    ''' Repositorio de Retroactividad D
    ''' </summary>
    ''' <remarks></remarks>
    Private _RetroactiveDRepository As IRetroactiveDRepository

    ''' <summary>
    ''' Repositorio de Aumento de Salarios
    ''' </summary>
    Private _increaseSalaryRepository As IIncreaseSalaryRepository

    Private ConfirmIncreaseSalary As SP_ConfirmIncreaseEmployeSalary_Result

    'Devuelve el contexto en este repositorio 
    Private _context As IPayrollUnitOfWork

    Public Sub New(employeeRepository As IEmployeeRepository, increaseSalaryDomain As IIncreaseSalaryDomain, contractRepositoryActual As IContractRepository, contractRepositoryNew As IContractRepository, groupRepository As IGroupRepository, RetroactiveCRepository As IRetroactiveCRepository, ByVal contex As IPayrollUnitOfWork, IncreaseSalaryRepository As IIncreaseSalaryRepository, RetroactiveDRepository As IRetroactiveDRepository)
        If employeeRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Empleados Vacío")
        End If

        If increaseSalaryDomain Is Nothing Then
            Throw New ArgumentNullException("Repositorio de increaseSalaryDomain Vacío")
        End If

        If contractRepositoryActual Is Nothing Then
            Throw New ArgumentNullException("Repositorio de contractRepositoryActual Vacío")
        End If

        _employeeRepository = employeeRepository
        _increaseSalaryDomain = increaseSalaryDomain
        _contractRepositoryActual = contractRepositoryActual
        _contractRepositoryNew = contractRepositoryNew
        _groupRepository = groupRepository
        _RetroactiveCRepository = RetroactiveCRepository
        _RetroactiveDRepository = RetroactiveDRepository
        _context = contex
        _increaseSalaryRepository = IncreaseSalaryRepository
    End Sub

    ''' <summary>
    ''' Función que ejecuta el Aumento de Sueldo
    ''' </summary>
    ''' <param name="IdGroup"></param>
    ''' <param name="IdFunctionalUnit"></param>
    ''' <param name="IdPosition"></param>
    ''' <param name="PercentageIncrease"></param>
    ''' <param name="Confirm"></param>
    ''' <param name="AproxValue"></param>
    ''' <param name="ModificationReasonId"></param>
    ''' <param name="InitialDateNewSalary"></param>
    ''' <param name="PayrollPaid"></param>
    ''' <param name="SessionValues"></param>
    ''' <param name="WithRetroactive"></param>
    ''' <param name="RetroactiveInitialDate"></param>
    ''' <param name="PayrollPaidRetroactive"></param>
    ''' <returns></returns>
    Public Function ExecuteIncreaseSalary(IdGroup As Integer, IdFunctionalUnit As Integer, IdPosition As Integer, PercentageIncrease As Decimal, Confirm As Boolean, AproxValue As Integer, ModificationReasonId As Integer, InitialDateNewSalary As Date, PayrollPaid As Byte, SessionValues As SessionValues, Optional WithRetroactive As Integer = 0, Optional RetroactiveInitialDate As String = Nothing, Optional PayrollPaidRetroactive As Integer = 0) As List(Of SP_IncreaseEmployeSalary_Result) Implements IIncreaseSalaryAdminService.ExecuteIncreaseSalary
        Try

            Dim ListIncreaseSalary = _increaseSalaryRepository.ExecuteIncreaseSalary(IdGroup, IdFunctionalUnit, IdPosition, PercentageIncrease, Confirm, AproxValue, ModificationReasonId, InitialDateNewSalary, PayrollPaid, SessionValues, WithRetroactive, RetroactiveInitialDate, PayrollPaidRetroactive)

            If ListIncreaseSalary IsNot Nothing AndAlso ListIncreaseSalary.Count > 0 Then
                Return ListIncreaseSalary
            Else
                Return Nothing
            End If


        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", SessionValues)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Función para confirmar el Aumento de Salario
    ''' </summary>
    ''' <param name="IncreaseSalaryData"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="PercentageIncrease"></param>
    ''' <param name="ModificationReasonId"></param>
    ''' <param name="InitialDateNewSalary"></param>
    ''' <returns></returns>
    Public Function ConfirmIncreaseEmployeSalary(IncreaseSalaryData As List(Of SP_IncreaseEmployeSalary_Result), UserCode As String, PercentageIncrease As Decimal, ModificationReasonId As Integer, InitialDateNewSalary As Date) As SP_ConfirmIncreaseEmployeSalary_Result Implements IIncreaseSalaryAdminService.ConfirmIncreaseEmployeSalary
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlIncrease(IncreaseSalaryData)

            Dim ConfirmIncreaseSalary = _increaseSalaryRepository.ConfirmIncreaseEmployeSalary(xmlObject, UserCode, PercentageIncrease, ModificationReasonId, InitialDateNewSalary)

            If ConfirmIncreaseSalary IsNot Nothing Then
                Return ConfirmIncreaseSalary
            Else
                Return Nothing
            End If

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Función para Validar datos importados desde el Excel
    ''' </summary>
    ''' <param name="dataimport"></param>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetIncreaseSalaryFromFile(dataimport As List(Of ImportFileRow), data As List(Of List(Of String))) As List(Of SP_SetIncreaseSalaryFromFile_Result) Implements IIncreaseSalaryAdminService.SetIncreaseSalaryFromFile
        Try

            'Objeto xml
            Dim xmlObject As String = String.Empty

            'Listado de registros con errores
            Dim listRecordsErrors As New List(Of String())

            'Listado de errores
            Dim listErrors As New List(Of String)

            xmlObject = ConvertToXml(dataimport)
            Dim resultStore = Me._increaseSalaryRepository.SetIncreaseSalaryFromFile(xmlObject)
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                Return resultStore
            Else
                Return Nothing
            End If

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Función auxiliar para convertir Si/No a 1/0
    ''' </summary>
    ''' <param name="value">Valor a convertir (Si, NO, Sí, si, no, etc.)</param>
    ''' <returns>1 si es "Si", 0 en cualquier otro caso</returns>
    Private Function ConvertSiNoToInt(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then
            Return "0"
        End If

        ' Convertir a mayúsculas y quitar espacios para comparar
        Dim normalizedValue = value.Trim().ToUpper()

        ' Verificar si es "SI" o "SÍ" (con o sin acento)
        If normalizedValue = "SI" OrElse normalizedValue = "SÍ" OrElse normalizedValue = "1" Then
            Return "1"
        Else
            Return "0"
        End If
    End Function

    ''' <summary>
    ''' Función que convierte el DataSource en XML
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Private Function ConvertToXml(data As List(Of ImportFileRow)) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Data>")

        For Each item In data

            Dim indexRow = item.IndexRow
            Dim Columns = item.Row.Count

            builder.Append("<Row>")

            builder.Append("<NitEmployee>" & If(Columns > 0, item.Row.Item(0), String.Empty) & "</NitEmployee>")
            builder.Append("<InitialDate>" & If(Columns > 1, item.Row.Item(1), String.Empty) & "</InitialDate>")
            builder.Append("<ModificationReasonCode>" & If(Columns > 2, item.Row.Item(2), String.Empty) & "</ModificationReasonCode>")
            builder.Append("<PercentageIncrease>" & If(Columns > 3, item.Row.Item(3)?.ToString.Replace(",", "."), String.Empty) & "</PercentageIncrease>")
            builder.Append("<AproximationValue>" & If(Columns > 4, item.Row.Item(4), String.Empty) & "</AproximationValue>")
            builder.Append("<BasicSalary>" & If(Columns > 5, item.Row.Item(5), String.Empty) & "</BasicSalary>")
            builder.Append("<ValueIncrease>" & If(Columns > 6, item.Row.Item(6)?.ToString.Replace(",", "."), String.Empty) & "</ValueIncrease>")
            Dim withRetroactiveValue = If(Columns > 7, ConvertSiNoToInt(item.Row.Item(7)), "0")
            builder.Append("<WithRetroactive>" & withRetroactiveValue & "</WithRetroactive>")
            builder.Append("<RetroactiveInitialDate>" & If(Columns > 8, item.Row.Item(8), String.Empty) & "</RetroactiveInitialDate>")
            Dim payrollPaidValue = If(Columns > 9, item.Row.Item(9), String.Empty)
            If Not String.IsNullOrWhiteSpace(payrollPaidValue) AndAlso Not IsNumeric(payrollPaidValue) Then
                payrollPaidValue = ConvertSiNoToInt(payrollPaidValue)
            End If
            builder.Append("<PayrollPaidRetroactive>" & payrollPaidValue & "</PayrollPaidRetroactive>")
            builder.Append("</Row>")

        Next
        builder.Append("</Data>")

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Función que convierte el DataSource en XML
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Private Function ConvertToXmlIncrease(data As List(Of SP_IncreaseEmployeSalary_Result)) As String
        Dim builder As StringBuilder = New StringBuilder()
        Dim culture As CultureInfo = CultureInfo.InvariantCulture

        For Each item In data
            builder.Append("<IncreaseEmployeeSalary>")
            builder.Append("<Id>" & item.Id & "</Id>")
            ' Formatear BasicSalary: quitar decimales innecesarios y usar punto decimal
            builder.Append("<BasicSalary>" & FormatDecimalValue(item.BasicSalary) & "</BasicSalary>")

            builder.Append("<ContractId>" & item.ContractId & "</ContractId>")
            builder.Append("<FunctionalUnidCode>" & item.FunctionalUnitCode & "</FunctionalUnidCode>")
            builder.Append("<InitialContractNumber>" & item.InitialContractNumber & "</InitialContractNumber>")

            ' Formatear fechas correctamente
            builder.Append("<InitialDate>" & item.InitialDate & "</InitialDate>")
            builder.Append("<ModificationReasonId>" & item.ModificationReasonId & "</ModificationReasonId>")
            builder.Append("<JobBondingDate>" & item.JobBondingDate & "</JobBondingDate>")

            ' Formatear NewSalary: quitar decimales innecesarios y usar punto decimal
            builder.Append("<NewSalary>" & FormatDecimalValue(item.NewSalary) & "</NewSalary>")

            builder.Append("<NitEmployee>" & item.NitEmployee & "</NitEmployee>")

            ' Formatear PercentageIncrease: usar punto decimal
            builder.Append("<PercentageIncrease>" & FormatDecimalValue(item.PercentageIncrease, 4) & "</PercentageIncrease>")

            builder.Append("<PositionCode>" & item.PositionCode & "</PositionCode>")

            ' Formatear ValueIncrease: quitar decimales innecesarios y usar punto decimal
            builder.Append("<ValueIncrease>" & FormatDecimalValue(item.ValueIncrease) & "</ValueIncrease>")

            builder.Append("</IncreaseEmployeeSalary>")
        Next

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Formatea un valor decimal eliminando decimales innecesarios y usando punto como separador
    ''' </summary>
    ''' <param name="value">Valor decimal a formatear</param>
    ''' <param name="maxDecimals">Número máximo de decimales (por defecto 2)</param>
    ''' <returns>String con el valor formateado</returns>
    Private Function FormatDecimalValue(value As Decimal, Optional maxDecimals As Integer = 2) As String
        Dim culture As CultureInfo = CultureInfo.InvariantCulture
        ' Redondear al número de decimales especificado
        Dim roundedValue As Decimal = Math.Round(value, maxDecimals)
        ' Usar formato "G29" que elimina ceros innecesarios
        Return roundedValue.ToString("G29", culture)
    End Function



    ''' <summary>
    ''' Función para Almacenar el Aumento de sueldo
    ''' </summary>
    ''' <param name="ObjListContract">Objeto de Lista de Contratos</param>
    ''' <param name="ContractInitialDate">Fecha de Inicio del Nuevo Contrato (OTRO SI)</param>
    ''' <param name="ContractModificationReasonId">I de la Razón de Modificación del Contrato</param>
    ''' <param name="Indigo">Variable de Sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveIncreaseSalary(ObjListContract As String, ContractInitialDate As Date, ContractModificationReasonId As Integer, Indigo As SessionValues, Optional ListRetroactiveC As List(Of RetroactiveC) = Nothing) As Boolean Implements IIncreaseSalaryAdminService.SaveIncreaseSalary

        Dim unitWorkContractActual As IUnitWork = _contractRepositoryActual.UnitWork
        Dim unitWorkContractNew As IUnitWork = _contractRepositoryNew.UnitWork
        Dim unitWorkRetroactive As IUnitWork = _RetroactiveCRepository.UnitWork
        Try

            Dim ObjContractList As Object = Utils.DeserializeJsonToObject(ObjListContract)

            If ObjContractList Is Nothing Then
                Return False
            End If

            'If ObjContractList IsNot Nothing Then
            For Each o As Object In ObjContractList.List
                Dim ContractId As Integer = o.ContractId
                Dim NewSalary As Double = o.NewSalary

                Dim ActualContract As Contract = _contractRepositoryActual.GetContractById(ContractId, True)

                Dim NewContract As Contract = Me.CloneContract(ActualContract, ContractInitialDate, NewSalary, Indigo)

                'Tomo el Contrato Actual y lo inactivo
                Dim ContractEndingNewDate As Date

                If ContractInitialDate > ActualContract.ContractInitialDate Then
                    ContractEndingNewDate = DateAdd(DateInterval.Day, -1, ContractInitialDate)
                Else
                    ContractEndingNewDate = ActualContract.ContractInitialDate
                End If

                ActualContract.ContractEndingDate = ContractEndingNewDate
                ActualContract.Valid = False
                ActualContract.Status = 4
                ActualContract.ContractModificationReasonId = ContractModificationReasonId
                ActualContract.ModificationDate = Date.Now()
                ActualContract.ModificationUserId = Indigo.UserIndigoId
                ActualContract.MarkAsModified()

                ''Creacion de Fondos
                NewContract = Me.CreateFundContract(ActualContract.FundContract.ToList(), NewContract, ContractInitialDate, Indigo)

                _contractRepositoryActual.SaveEntity(ActualContract)
                _contractRepositoryNew.SaveEntity(NewContract)
                unitWorkContractActual.Commit()
                unitWorkContractNew.Commit()

                Dim unitWorkEmployee As IUnitWork = _employeeRepository.UnitWork
                Dim employee As Employee = _employeeRepository.GetEmployeeById(ActualContract.EmployeeId, True)
                Dim activeContract = employee?.Contract?.FirstOrDefault(Function(c) c.PositionId = NewContract.PositionId AndAlso c.Valid)
                If activeContract?.Position?.ProfessionalRisk IsNot Nothing Then
                    employee.ProfessionalRiskPercentage = activeContract.Position.ProfessionalRisk.Percentage
                    employee.UserModified = Indigo.UserIndigoId
                    employee.DateModified = Date.Now
                    _employeeRepository.SaveEntity(employee)
                    unitWorkEmployee.Commit()
                End If

                Dim inta = NewContract.Id

            Next
            ' End If

            If ListRetroactiveC IsNot Nothing AndAlso ListRetroactiveC.Count > 0 Then

                For Each ObjRetroactiveC As RetroactiveC In ListRetroactiveC
                    ObjRetroactiveC.MarkAsModified()
                    ObjRetroactiveC.Status = 2
                    ObjRetroactiveC.ConfirmationUserId = Indigo.UserIndigoId
                    ObjRetroactiveC.ConfirmationDate = Date.Now

                    For Each ObjRetroactiveD As RetroactiveD In ObjRetroactiveC.RetroactiveD
                        ObjRetroactiveD.MarkAsModified()
                    Next

                    _RetroactiveCRepository.SaveEntity(ObjRetroactiveC)
                    unitWorkRetroactive.Commit()


                Next

            End If

            Return True

        Catch ex As Exception
            unitWorkContractActual.RollbackChanges()
            unitWorkContractNew.RollbackChanges()
            unitWorkRetroactive.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Indigo)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función para Crear el Clón de Contrato
    ''' </summary>
    ''' <param name="Contract">Contrato</param>
    ''' <param name="ContractInitialDate">Fecha Inicio del Contrato</param>
    ''' <param name="Indigo">Variable de Sesión</param>
    ''' <returns>Contract</returns>
    ''' <remarks></remarks>
    Function CloneContract(Contract As Contract, ContractInitialDate As Date, NewSalary As Double, Indigo As SessionValues) As Contract Implements IIncreaseSalaryAdminService.CloneContract
        Dim NewContract As New Contract()

        Dim ContractInitialNewDate As Date

        If ContractInitialDate > Contract.JobBondingDate Then
            ContractInitialNewDate = ContractInitialDate
        Else
            ContractInitialNewDate = Contract.ContractInitialDate
        End If

        NewContract.ResolutionNumber = Contract.ResolutionNumber
        NewContract.ResolutionDate = Contract.ResolutionDate
        NewContract.PosesionDate = Contract.PosesionDate
        NewContract.CertificateOfficeNumber = Contract.CertificateOfficeNumber

        NewContract.Autoliquidation = Contract.Autoliquidation
        NewContract.BankAccountNumber = Contract.BankAccountNumber
        NewContract.BankAccountType = Contract.BankAccountType
        NewContract.BankId = Contract.BankId
        NewContract.BaseIncome = Contract.BaseIncome
        NewContract.BasicSalary = NewSalary
        NewContract.CertificateOfficeNumber = Contract.CertificateOfficeNumber
        NewContract.ContractCreationDate = Date.Now()
        NewContract.ContractEndingDate = Contract.ContractEndingDate
        NewContract.ContractInitialDate = ContractInitialNewDate
        NewContract.ContractLiquidation = Contract.ContractLiquidation
        NewContract.ContractModificationReasonId = Nothing
        NewContract.ContractTypeId = Contract.ContractTypeId
        NewContract.CreationUserId = Indigo.UserIndigoId
        NewContract.EmployeeId = Contract.EmployeeId
        NewContract.FunctionalUnitId = Contract.FunctionalUnitId
        NewContract.GroupId = Contract.GroupId
        NewContract.HoursDaily = Contract.HoursDaily
        NewContract.IncomeDailyBase = NewSalary

        If Contract.InitialContractNumber > 0 Then
            NewContract.InitialContractNumber = Contract.InitialContractNumber
        Else
            NewContract.InitialContractNumber = Contract.Id
        End If

        NewContract.JobBondingDate = Contract.JobBondingDate
        NewContract.LiquidationPayroll = Contract.LiquidationPayroll
        NewContract.Notes = Contract.Notes
        NewContract.PaymentPeriod = Contract.PaymentPeriod
        NewContract.PaymentType = Contract.PaymentType
        NewContract.PosesionDate = Contract.PosesionDate
        NewContract.PositionId = Contract.PositionId
        NewContract.ResolutionDate = Contract.ResolutionDate
        NewContract.ResolutionNumber = Contract.ResolutionNumber
        NewContract.RowType = 2
        NewContract.Status = 1
        NewContract.TrialPeriod = Contract.TrialPeriod
        NewContract.TrialPeriodSalaryPercentage = Contract.TrialPeriodSalaryPercentage
        NewContract.TrialPeriodTime = Contract.TrialPeriodTime
        NewContract.TypeOfPensionContribution = Contract.TypeOfPensionContribution
        NewContract.Valid = True
        NewContract.ModificationUserId = Indigo.UserIndigoId
        NewContract.ModificationDate = Date.Now()

        Return NewContract
    End Function

    ''' <summary>
    ''' Función para Crear el Clón de Contrato
    ''' </summary>
    ''' <param name="ListFundContract">ListFundContract</param>
    ''' <param name="ContractInitialDate">Fecha Inicio del Contrato</param>
    ''' <param name="Indigo">Variable de Sesión</param>
    ''' <returns>Contract</returns>
    ''' <remarks></remarks>
    Function CreateFundContract(ListFundContract As List(Of FundContract), NewContract As Contract, ContractInitialDate As Date, Indigo As SessionValues) As Contract

        Dim ListNewFundContract As New List(Of FundContract)

        For Each ObjListFundContract As FundContract In ListFundContract

            Dim NewFundContract As New FundContract()

            NewFundContract.FundId = ObjListFundContract.FundId
            NewFundContract.ContractId = NewContract.Id
            NewFundContract.FundType = ObjListFundContract.FundType
            NewFundContract.InitialDate = ContractInitialDate
            NewFundContract.MembershipNumber = ObjListFundContract.MembershipNumber
            NewFundContract.VoluntaryContribution = ObjListFundContract.VoluntaryContribution
            NewFundContract.VoluntaryContributionValue = ObjListFundContract.VoluntaryContributionValue
            NewFundContract.State = 1
            NewFundContract.CreationDate = Date.Now()
            NewFundContract.CreationUser = Indigo.UserIndigo

            NewContract.FundContract.Add(NewFundContract)

        Next

        Return NewContract
    End Function

    Public Function ExecuteRetroactive(IdGroup As Integer, IncreasePercentage As Decimal, RetroactiveInitialDate As Date, PayrollPaid As Byte, Indigo As SessionValues, Optional FunctionalId As Integer = 0, Optional PositionId As Integer = 0, Optional SpecificEmployeeId As Integer = 0) As List(Of RetroactiveC) Implements IIncreaseSalaryAdminService.ExecuteRetroactive

        Dim unitWorkRetroactive As IUnitWork = _RetroactiveCRepository.UnitWork
        Dim unitWorkRetroactiveD As IUnitWork = _RetroactiveDRepository.UnitWork
        Try
            Dim ListEmployee = _employeeRepository.GetEmployeeByGroup(IdGroup, True, Nothing, Nothing, SpecificEmployeeId)
            Dim ContractList As New List(Of Contract)

            If FunctionalId > 0 Then
                ListEmployee = ListEmployee.Where(Function(x) x.Contract.Any(Function(c) c.FunctionalUnitId = FunctionalId AndAlso c.Status = 1)).ToList()
            End If

            If PositionId > 0 Then
                ListEmployee = ListEmployee.Where(Function(x) x.Contract.Any(Function(c) c.PositionId = PositionId)).ToList()
            End If

            For i As Integer = 0 To ListEmployee.Count() - 1
                Dim ContractToAdd = (ListEmployee.Item(i).Contract.Where(Function(y) y.Status = 1 And y.RetirementDate Is Nothing).FirstOrDefault())

                If ContractToAdd IsNot Nothing Then
                    ContractList.Add(ContractToAdd)
                End If
            Next

            Dim Group = _groupRepository.GetGroupById(IdGroup)

            Dim ObjExist = _RetroactiveCRepository.GetListRetroactive(RetroactiveInitialDate, IdGroup, SpecificEmployeeId)

            If ObjExist IsNot Nothing Then
                If Not ObjExist.Any(Function(x) x.Status = 2) Then
                    ' Solo eliminar si NO hay registros confirmados (Status = 2)
                    If SpecificEmployeeId > 0 Then
                        For Each retroactiveC In ObjExist
                            If retroactiveC.RetroactiveD IsNot Nothing AndAlso retroactiveC.RetroactiveD.Count > 0 Then
                                For Each detail In retroactiveC.RetroactiveD.ToList()
                                    _RetroactiveDRepository.DeleteEntity(detail)
                                Next
                            End If
                            _RetroactiveCRepository.DeleteEntity(retroactiveC)
                        Next
                    Else
                        Dim ExecuteDelete = _context.SP_DeleteRetroactiveNoConfirm(RetroactiveInitialDate, IdGroup)
                    End If
                End If
            End If


            ' Pasar el EmployeeId específico al dominio para optimizar las consultas
            ' Si viene SpecificEmployeeId > 0, se usa para filtrar las liquidaciones e incentivos
            Dim ListRetroactive = _increaseSalaryDomain.ExecuteRetroactive(ContractList, RetroactiveInitialDate, IncreasePercentage, Group, PayrollPaid, Indigo, SpecificEmployeeId)

            If ListRetroactive IsNot Nothing AndAlso ListRetroactive.Count() > 0 Then

                For Each ObjRetroactive As RetroactiveC In ListRetroactive
                    _RetroactiveCRepository.SaveEntity(ObjRetroactive)
                    unitWorkRetroactive.Commit()
                Next
                unitWorkRetroactiveD.Commit()
                Return ListRetroactive
            Else
                Return Nothing
            End If

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            unitWorkRetroactive.RollbackChanges()
            unitWorkRetroactiveD.RollbackChanges()
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _increaseSalaryDomain.Dispose()
            End If
            _employeeRepository = Nothing
            _increaseSalaryDomain = Nothing
            _contractRepositoryActual = Nothing
            _contractRepositoryNew = Nothing
            _groupRepository = Nothing
            _RetroactiveCRepository = Nothing
            _RetroactiveDRepository = Nothing
            _context = Nothing
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
