Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo

'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 09-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Public Class PContractLiquidation

    Public Enum retirementDates
        minimumRetirementDate
        maximumRetiremenDate
    End Enum


    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IContractLiquidation

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IContractLiquidation)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Async Sub Initializes()

        Dim modelo As New MBusqueda
        View.HumanTalentList = modelo.ConsultarEntidades(eDataSource.AllEmployeesWithContractToLiquidate)


        Using m As New MRetirementReason("")
            View.RetirementReasonList = Await m.ListAllRetirementReasonAsync()
        End Using



    End Sub

    ''' <summary>
    ''' Obtiene los datos del empleado seleccionado en el buscador de empleados
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    Public Async Function GetSelectedEmployeeById(id As Integer) As Task(Of Employee)

        Dim e As Employee

        Using model As New MEmployee(MEmployee.TAG)
            e = Await model.GetEmployeeByIdForContractLiquidation(id)
        End Using

        Return e
    End Function

    ''' <summary>
    ''' Obtiene las nominas liquidadas para el contrato enviado
    ''' </summary>
    ''' <param name="contractId">id del contrato</param>
    ''' <returns>Listado de nominas pagadas</returns>
    Public Function GetPaymentsByContractId(contractId As Integer) As IList(Of Liquidation)

        Dim l As List(Of Liquidation)

        Using model As New MContractLiquidation(MContractLiquidation.TAG)
            l = model.GetPaymentsByContractId(contractId)
        End Using

        Return l

    End Function

    ''' <summary>
    ''' Obtiene las nominas liquidadas para el contrato enviado
    ''' </summary>
    ''' <param name="baseContractId">id del contrato</param>
    ''' <returns>Listado de nominas pagadas</returns>
    Public Async Function GetLiquidationsByBaseContractId(baseContractId As Integer) As Task(Of IList(Of Liquidation))

        Dim l As List(Of Liquidation)

        Using model As New MContractLiquidation(MContractLiquidation.TAG)
            l = Await model.GetLiquidationsPaidByBaseContractId(baseContractId)
        End Using

        Return l

    End Function


    ''' <summary>
    ''' Funcion que calcula las fechas maximas y minimas permitidas para el retiro 
    ''' </summary>
    ''' <param name="employee">empleado</param>
    ''' <returns>Diccionario que contiene la fecha minima y maxima de retiro</returns>
    Public Async Function GetMaximunMinimumRetirementDates(employee As Employee) As Task(Of Dictionary(Of retirementDates, Date))

        Dim result As New Dictionary(Of retirementDates, Date)
        Dim validContract = employee.Contract.Where(Function(i) i.Valid = True).FirstOrDefault

        Dim tmpMax, tmpMin As Date

        If validContract IsNot Nothing Then

            Dim liquidationsPaid As List(Of Liquidation)

            If validContract.RowType = 1 AndAlso validContract.InitialContractNumber = 0 Then
                liquidationsPaid = GetPaymentsByContractId(validContract.Id)
            Else
                liquidationsPaid = Await GetLiquidationsByBaseContractId(validContract.InitialContractNumber)
            End If

            If liquidationsPaid.Count > 0 Then

                Dim liquidation = liquidationsPaid.OrderByDescending(Function(i) i.PayrollDateLiquidated).FirstOrDefault

                If liquidation.PayrollDateLiquidated = Date.MinValue Then
                    tmpMin = validContract.JobBondingDate
                ElseIf validContract.Group.Liquidation = 1 Then
                    '' Mensual

                    tmpMin = liquidation.PayrollDateLiquidated
                    tmpMin = New Date(tmpMin.Year, tmpMin.Month, 1)
                    If tmpMin.Month = 12 Then
                        tmpMax = New Date(tmpMin.Year + 1, 1, Date.DaysInMonth(tmpMin.Year + 1, 1))
                    Else
                        tmpMax = New Date(tmpMin.Year, tmpMin.Month + 1, Date.DaysInMonth(tmpMin.Year, tmpMin.Month + 1))
                    End If


                Else
                    '' Quincenal
                    If Day(liquidation.PayrollDateLiquidated) = 15 Then
                        tmpMin = New Date(liquidation.PayrollDateLiquidated.Year, liquidation.PayrollDateLiquidated.Month, 1)
                        tmpMax = New Date(tmpMin.Year, tmpMin.Month, Date.DaysInMonth(tmpMin.Year, tmpMin.Month))
                    ElseIf Day(liquidation.PayrollDateLiquidated) = 28 Or Day(liquidation.PayrollDateLiquidated) = 29 Or Day(liquidation.PayrollDateLiquidated) = 30 Or Day(liquidation.PayrollDateLiquidated) = 31 Then
                        If Month(liquidation.PayrollDateLiquidated) < 12 Then
                            tmpMin = New Date(liquidation.PayrollDateLiquidated.Year, liquidation.PayrollDateLiquidated.Month, 16)
                            tmpMax = New Date(tmpMin.Year, tmpMin.Month + 1, 15)
                        Else
                            tmpMin = New Date(liquidation.PayrollDateLiquidated.Year, 12, 16)
                            tmpMax = New Date(tmpMin.Year + 1, 1, 15)
                        End If
                    End If

                End If


                ' tmpMin = liquidation.PayrollDateLiquidated
                'tmpMin = New Date(tmpMin.Year, tmpMin.Month, 1)
                'tmpMax = validContract.ContractEndingDate

            Else
                tmpMin = validContract.JobBondingDate
                tmpMax = validContract.ContractEndingDate
            End If



        End If

        If tmpMax > validContract.ContractEndingDate Then
            tmpMax = validContract.ContractEndingDate
        End If

        'Providencial

        'tmpMin = validContract.JobBondingDate
        'tmpMax = validContract.ContractEndingDate


        'Fecha minima de retiro
        result.Add(retirementDates.minimumRetirementDate, tmpMin)
        'Fecha maxima de retiro
        result.Add(retirementDates.maximumRetiremenDate, tmpMax)

        Return result
    End Function

    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function


End Class
