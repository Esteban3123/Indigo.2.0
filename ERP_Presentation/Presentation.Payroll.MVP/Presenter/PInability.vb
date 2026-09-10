'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 22-08-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo

Public Class PInability

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IInability

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IInability)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Async Function ValueIBCMonth(EmployeeId As Integer, year As Integer, month As Integer) As Task(Of Decimal)

        Dim returnValue As Decimal = 0
        Dim PartialBC As Decimal = 0

        Using model As New MNovelty(MConcept.TAG)
            Dim Liquidation = Await model.GetAverageIBCLiquidationRealDateMonth(EmployeeId, year, month)

            If Liquidation IsNot Nothing Then
                'Recorro los detalles para calcular
                For Each ObjDetail As LiquidationDetail In Liquidation.LiquidationDetail

                    If ObjDetail.Concept.AffectIBC = True Then
                        If ObjDetail.ConceptType = 1 Then
                            returnValue = returnValue + ObjDetail.ConceptTotalValue
                        End If

                        If ObjDetail.ConceptType = 2 Then
                            returnValue = returnValue - ObjDetail.ConceptTotalValue
                        End If

                    End If
                Next
            End If

            Dim InitialDate As New Date(year, month, 1)
            Dim EndDate As New Date(year, month, Date.DaysInMonth(year, month))
            Dim ObjVacation = Await model.GetVacactionInitialEndDate(EmployeeId, InitialDate, EndDate)


            If ObjVacation IsNot Nothing AndAlso ObjVacation.Count > 0 Then

                Dim tmpVacation = ObjVacation.FirstOrDefault()

                If tmpVacation.TypePayment = 1 Then

                    Dim enjoyDays As Integer = tmpVacation.EnjoyDays

                    If (New Date(tmpVacation.VacationStartDate.Year, tmpVacation.VacationStartDate.Month, Date.DaysInMonth(tmpVacation.VacationStartDate.Year, tmpVacation.VacationStartDate.Month))).Day = 31 Then
                        enjoyDays = enjoyDays - 1
                    End If

                    Dim ValueIBC = (tmpVacation.HealthContribution * 100) / 4


                    Dim DaysCalculation As Integer = 0

                    If tmpVacation.VacationStartDate < InitialDate Then
                        'Vacaciones Mes anterior
                        DaysCalculation = Await model.Days360(InitialDate, DateAdd(DateInterval.Day, -1, tmpVacation.IncorporationDateReal))
                    End If

                    If tmpVacation.VacationStartDate >= InitialDate And tmpVacation.VacationEndDate <= EndDate Then
                        'VAcaciones este mes
                        DaysCalculation = enjoyDays
                    End If

                    If tmpVacation.VacationStartDate >= InitialDate And tmpVacation.VacationEndDate > EndDate Then
                        'VAcaciones este mes
                        DaysCalculation = Await model.Days360(tmpVacation.VacationStartDate, EndDate)
                    End If

                    PartialBC = ValueIBC / enjoyDays * DaysCalculation

                End If

            End If

        End Using

        Return Math.Round(returnValue + PartialBC, 0)

    End Function

    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetXPOObject(Of PayrollSettingsXpo)()
    End Function

End Class
