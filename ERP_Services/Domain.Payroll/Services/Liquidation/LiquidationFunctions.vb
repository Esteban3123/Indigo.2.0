
Imports System.CodeDom.Compiler
Imports System.Reflection
Imports Domain.Payroll.Entities
Imports System.Threading
Imports Domain.Payroll
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Common
Imports Domain.Entities
Imports DevExpress.Data.Filtering

Public Class LiquidationFunctions

    Implements ILiquidationFunctions

    ''' <summary>
    ''' Enumerador de la clase de licencia en novedades
    ''' </summary>
    Private Enum eNoveltyLicenseClass As Byte
        Remunerada = 1
        NoRemunerada = 2
        Permiso = 3
        ConCargoVacaciones = 4
        CalamidadDomestica = 5
        LicenciaLuto = 6
        DiaFamilia = 7
    End Enum

    '''<summary>
    '''String que representa el lenguaje utilizado en la cultura de Costa Rica
    '''</summary>
    Private Const _cultureCR As String = "es-CR"

    Public Function AdjustDominicalValue(ByVal ScheduleDetailEmployee As List(Of ScheduleDetail), ListHolidays As List(Of Holiday), ByVal group As Group, Optional LastMonthInitialDate As Date = Nothing, Optional LastMonthEvent As Boolean = False) As List(Of Tuple(Of String, Decimal)) Implements ILiquidationFunctions.AdjustDominicalValue

        Dim ResultTuple As New List(Of Tuple(Of String, Decimal))

        Dim VarHour As Decimal = 0
        Dim LegalDailyHours As Integer = 8
        Dim DominicalFestivo As Decimal = 0
        Dim RecargoNocturnoFestivo As Decimal = 0
        Dim HoraExtraFestivaDiurna As Decimal = 0
        Dim HoraExtraFestivaNocturna As Decimal = 0

        Dim TotalHours As Decimal = 0

        For Each ObjScheduleDetail As ScheduleDetail In ScheduleDetailEmployee.OrderBy(Function(x) x.DateDetail)
            Dim DailyHours As Decimal = 0
            Dim EveningHours As Decimal = 0

            Dim ObjScheduleEvent As New List(Of ScheduleDetailHour)
            Dim TmpListEventSunday As New List(Of ScheduleDetailHour)

            Dim TotalLegaliceDay As Integer = 8

            'Armo el objeto que debo recorrer
            Dim TmpListEvent As New List(Of ScheduleDetailHour)

            ObjScheduleEvent = ObjScheduleDetail.ScheduleDetailHour.Where(Function(x) x.Event = True And x.Approved = True).ToList()

            TmpListEventSunday = ObjScheduleEvent.Where(Function(x) x.DateTimeInitial.Date.DayOfWeek = DayOfWeek.Sunday And x.NextDay = False).ToList()

            If TmpListEventSunday IsNot Nothing AndAlso TmpListEventSunday.Count > 0 Then
                TmpListEvent.AddRange(TmpListEventSunday)
            End If

            If TmpListEventSunday.Count = 0 Then
                TmpListEventSunday = ObjScheduleEvent.Where(Function(x) x.NextDay = True And x.DateTimeEnding.Date.AddDays(1).DayOfWeek = DayOfWeek.Sunday).ToList()
                If TmpListEventSunday IsNot Nothing AndAlso TmpListEventSunday.Count > 0 Then
                    TmpListEvent.AddRange(TmpListEventSunday)
                End If
            End If

            For Each ObjTmpScheduleEvent As ScheduleDetailHour In ObjScheduleEvent
                If ListHolidays.FindAll(Function(x) x.Holiday1 = ObjTmpScheduleEvent.DateTimeInitial.Date And ObjTmpScheduleEvent.NextDay = False).Count > 0 Then
                    If TmpListEvent.FindAll(Function(x) x.Id = ObjTmpScheduleEvent.Id).Count > 0 Then
                        Continue For
                    End If
                    TmpListEvent.Add(ObjTmpScheduleEvent)
                End If

                If ListHolidays.FindAll(Function(x) x.Holiday1 = ObjTmpScheduleEvent.DateTimeInitial.Date.AddDays(1) And ObjTmpScheduleEvent.NextDay = True).Count > 0 Then
                    If TmpListEvent IsNot Nothing AndAlso TmpListEvent.Count > 0 Then
                        If TmpListEvent.FindAll(Function(x) x.Id = ObjTmpScheduleEvent.Id).Count > 0 Then
                            Continue For
                        End If
                    End If
                    TmpListEvent.Add(ObjTmpScheduleEvent)
                End If

            Next

            If TmpListEvent IsNot Nothing AndAlso TmpListEvent.Count > 0 Then

                Dim TotalHoursNormal = ObjScheduleDetail.ScheduleDetailHour.Where(Function(x) x.Event = False And x.NextDay = False).Sum(Function(x) x.TotalNumberHours)

                For Each AnEvent As ScheduleDetailHour In TmpListEvent

                    Dim TotalHoursDailyEvent = 0

                    If AnEvent.DateTimeInitial.TimeOfDay >= group.PayrollParameter.InitialTimeOrdinaryDay Then
                        If AnEvent.DateTimeEnding.TimeOfDay <> New TimeSpan(0, 0, 0) Then
                            TotalHoursDailyEvent = TotalHoursDailyEvent + AnEvent.TotalNumberHours
                        End If
                    End If

                    Dim DateTimeEnding As TimeSpan = New TimeSpan

                    If AnEvent.DateTimeEnding.TimeOfDay = New TimeSpan(0, 0, 0) Then
                        DateTimeEnding = New TimeSpan(23, 59, 59)
                    Else
                        DateTimeEnding = AnEvent.DateTimeEnding.TimeOfDay
                    End If


                    If AnEvent.DateTimeInitial.TimeOfDay >= group.PayrollParameter.InitialTimeOrdinaryDay And DateTimeEnding <= group.PayrollParameter.EndTimeOrdinaryDay And AnEvent.NextDay = False Then

                        Dim TmpDominicalFestivo = 0
                        'Eventos de 6 AM a 9 PM
                        Dim TmpTotalHoursEvent = AnEvent.TotalNumberHours

                        'Consultamos si viene de turno
                        Dim HoursInLastDay As Integer = 0

                        Dim HoursInLastTurn As Integer = 0

                        'Busco si viene de turnos ese día (Es para saber si trabajó la noche anterior)
                        For Each tmpScheduleDetail As ScheduleDetail In ScheduleDetailEmployee.Where(Function(x) x.DateDetail.Date = DateAdd(DateInterval.Day, -1, ObjScheduleDetail.DateDetail.Date) And x.Id <> ObjScheduleDetail.Id).ToList()
                            HoursInLastDay = tmpScheduleDetail.ScheduleDetailHour.Where(Function(x) x.NextDay = True And x.DateTimeEnding.Date = DateAdd(DateInterval.Day, -1, ObjScheduleDetail.DateDetail.Date)).Sum(Function(y) y.TotalNumberHours)
                        Next

                        'Busco si es un evento partido, y si es asi, si tuvo unas horas de eventos antes de este turno
                        For Each tmpScheduleDetail As ScheduleDetail In ScheduleDetailEmployee.Where(Function(x) x.DateDetail.Date = ObjScheduleDetail.DateDetail.Date).ToList()
                            Dim ObjtmpSchedule = tmpScheduleDetail.ScheduleDetailHour.Where(Function(x) x.NextDay = False And x.DateTimeEnding.Date = ObjScheduleDetail.DateDetail.Date And x.Id <> AnEvent.Id And x.DateTimeEnding.Hour <= AnEvent.DateTimeInitial.Hour And x.Event = True And x.DateTimeEnding.Date = AnEvent.DateTimeInitial.Date).FirstOrDefault()
                            If ObjtmpSchedule IsNot Nothing Then

                                If ObjtmpSchedule.DateTimeEnding.Hour <> 0 Then
                                    HoursInLastTurn = tmpScheduleDetail.ScheduleDetailHour.Where(Function(x) x.NextDay = False And x.DateTimeEnding.Date = ObjScheduleDetail.DateDetail.Date And x.Id <> AnEvent.Id And x.DateTimeEnding.Hour <= AnEvent.DateTimeInitial.Hour And x.DateTimeEnding.Hour <> 0 And x.Event = True And x.DateTimeEnding.Date = AnEvent.DateTimeInitial.Date).Sum(Function(y) y.TotalNumberHours)
                                End If
                            End If

                            Dim ObjtmpScheduleNormalTurn = tmpScheduleDetail.ScheduleDetailHour.Where(Function(x) x.NextDay = False And x.DateTimeEnding.Date = ObjScheduleDetail.DateDetail.Date And x.Id <> AnEvent.Id And x.DateTimeEnding.Hour <= AnEvent.DateTimeInitial.Hour And x.Event = False And x.DateTimeEnding.Date = AnEvent.DateTimeInitial.Date).FirstOrDefault()

                            If ObjtmpScheduleNormalTurn IsNot Nothing Then

                                If ObjScheduleDetail.ScheduleDetailHour.Any(Function(x) x.Id = ObjtmpScheduleNormalTurn.Id) Then
                                    TotalHoursNormal = TotalHoursNormal - ObjtmpScheduleNormalTurn.TotalNumberHours
                                End If

                                HoursInLastTurn = HoursInLastTurn + ObjtmpScheduleNormalTurn.TotalNumberHours
                            End If

                        Next

                        If HoursInLastDay > 0 Then
                            'Si tuvo turnos el día anterior
                            If HoursInLastDay + HoursInLastTurn < LegalDailyHours Then
                                'Debo crear Valor Dominical para completar
                                TmpDominicalFestivo = LegalDailyHours - (HoursInLastDay + HoursInLastTurn)
                            Else
                                TmpDominicalFestivo = 0
                            End If

                        Else
                            'Si no tuvo turnos el día anterior
                            If TmpTotalHoursEvent + TotalHoursNormal + HoursInLastTurn > LegalDailyHours Then
                                If TotalHoursNormal + HoursInLastTurn < LegalDailyHours Then
                                    'Debo repartir Valor Dominical
                                    TmpDominicalFestivo = LegalDailyHours - TotalHoursNormal - HoursInLastTurn
                                Else
                                    'Ya está ocupado 
                                    TmpDominicalFestivo = 0
                                End If
                            Else
                                TmpDominicalFestivo = TmpTotalHoursEvent
                            End If


                        End If

                        DominicalFestivo = DominicalFestivo + TmpDominicalFestivo

                        If TmpTotalHoursEvent - TmpDominicalFestivo > 0 Then
                            'Queda evento por repartir, debo pagarlo como Hora Extra Diurna Festiva
                            Dim TmpHoraExtraFestivaDiurna = TmpTotalHoursEvent - TmpDominicalFestivo
                            HoraExtraFestivaDiurna = HoraExtraFestivaDiurna + TmpHoraExtraFestivaDiurna
                        End If

                        TotalLegaliceDay = TotalLegaliceDay - DominicalFestivo - HoraExtraFestivaDiurna - HoursInLastDay

                    ElseIf AnEvent.NextDay = True AndAlso AnEvent.DateTimeInitial.TimeOfDay >= group.PayrollParameter.InitialTimeOrdinaryDay Then

                        'Eventos de 6 AM a 7 AM del Día siguiente
                        Dim TmpTotalHoursEvent = AnEvent.TotalNumberHours
                        Dim HoursInThisDay As Integer = 0

                        'Busco si tiene turnos ese día
                        For Each tmpScheduleDetail As ScheduleDetail In ScheduleDetailEmployee.Where(Function(x) x.DateDetail = DateAdd(DateInterval.Day, 1, ObjScheduleDetail.DateDetail) And x.Id <> ObjScheduleDetail.Id).ToList()
                            HoursInThisDay = tmpScheduleDetail.ScheduleDetailHour.Where(Function(x) x.NextDay = False And x.DateTimeEnding.Date = DateAdd(DateInterval.Day, 1, ObjScheduleDetail.DateDetail)).Sum(Function(y) y.TotalNumberHours)
                        Next

                        ''Es la hora de la mañana
                        Dim TmpHoraExtraFestivaDiurna = 0
                        Dim TmpHorasDominicales = 0

                        If ListHolidays.FindAll(Function(x) x.Holiday1 = AnEvent.DateTimeEnding.Date.AddDays(1)).Count > 0 Or AnEvent.DateTimeEnding.AddDays(1).DayOfWeek = DayOfWeek.Sunday Then
                            If HoursInThisDay > 0 Then
                                TmpHoraExtraFestivaDiurna = AnEvent.TotalNumberHours
                                HoraExtraFestivaDiurna = HoraExtraFestivaDiurna + TmpHoraExtraFestivaDiurna
                            Else
                                'Se paga como valor dominical
                                TmpHorasDominicales = AnEvent.TotalNumberHours
                                DominicalFestivo = DominicalFestivo + TmpHorasDominicales

                            End If
                        End If

                    Else
                        'Turno entre las 9 PM y las 6 AM

                        Dim TmpHoraExtraFestivaDiurna = 0
                        Dim TmpHoraExtraNocturna = 0
                        Dim TmpRecargoNocturnoFestivo = 0

                        If AnEvent.NextDay = False Then
                            'Turnos de 9 pm a 12 AM

                            'Consultamos si viene de turno
                            Dim HoursInLastDay As Integer = 0

                            'Busco si viene de turnos ese día (Es para saber si trabajó la noche anterior)
                            For Each tmpScheduleDetail As ScheduleDetail In ScheduleDetailEmployee.Where(Function(x) x.DateDetail.Date = DateAdd(DateInterval.Day, -1, ObjScheduleDetail.DateDetail.Date) And x.Id <> ObjScheduleDetail.Id).ToList()
                                HoursInLastDay = tmpScheduleDetail.ScheduleDetailHour.Where(Function(x) x.NextDay = True And x.DateTimeEnding.Date = DateAdd(DateInterval.Day, -1, ObjScheduleDetail.DateDetail.Date)).Sum(Function(y) y.TotalNumberHours)
                            Next

                            'Busco las horas de este día del turno
                            Dim HoursThisTurnDay As Integer = 0

                            For Each tmpScheduleDetail As ScheduleDetail In ScheduleDetailEmployee.Where(Function(x) x.DateDetail.Date = ObjScheduleDetail.DateDetail.Date).ToList()
                                HoursThisTurnDay = tmpScheduleDetail.ScheduleDetailHour.Where(Function(x) x.NextDay = False And x.DateTimeEnding.Date = ObjScheduleDetail.DateDetail.Date And x.Id <> AnEvent.Id).Sum(Function(y) y.TotalNumberHours)
                            Next

                            If HoursInLastDay > 0 Then
                                'Tuvo turno la noche anterior
                                If TotalLegaliceDay > 0 Then
                                    'Aun queda por repartir horas legales
                                    TmpRecargoNocturnoFestivo = TotalLegaliceDay - HoursInLastDay

                                    If AnEvent.TotalNumberHours - TmpRecargoNocturnoFestivo > 0 Then
                                        TmpHoraExtraNocturna = AnEvent.TotalNumberHours - TmpRecargoNocturnoFestivo
                                    End If
                                Else
                                    TmpHoraExtraNocturna = AnEvent.TotalNumberHours
                                End If

                            Else
                                If HoursThisTurnDay < LegalDailyHours Then

                                    If HoursThisTurnDay + AnEvent.TotalNumberHours > LegalDailyHours Then
                                        'Significa que aún necesito horas para el Recargo Nocturno Festivo, pero no todas las del turno
                                        TmpRecargoNocturnoFestivo = LegalDailyHours - HoursThisTurnDay
                                        TmpHoraExtraNocturna = AnEvent.TotalNumberHours - TmpRecargoNocturnoFestivo
                                    Else
                                        TmpRecargoNocturnoFestivo = AnEvent.TotalNumberHours
                                    End If
                                Else
                                    TmpHoraExtraNocturna = AnEvent.TotalNumberHours
                                End If
                            End If

                            RecargoNocturnoFestivo = RecargoNocturnoFestivo + TmpRecargoNocturnoFestivo
                            HoraExtraFestivaNocturna = HoraExtraFestivaNocturna + TmpHoraExtraNocturna
                        Else
                            'Turno de 12 AM  a 6 AM
                            Dim TotalHoursNextTurn As Integer = 0
                            For Each tmpScheduleDetail As ScheduleDetail In ScheduleDetailEmployee.Where(Function(x) x.DateDetail = DateAdd(DateInterval.Day, 1, ObjScheduleDetail.DateDetail) And x.Id <> ObjScheduleDetail.Id).ToList()
                                TotalHoursNextTurn = tmpScheduleDetail.ScheduleDetailHour.Where(Function(x) x.NextDay = False And x.DateTimeEnding.Date = DateAdd(DateInterval.Day, 1, ObjScheduleDetail.DateDetail) And x.Event = False).Sum(Function(y) y.TotalNumberHours)
                            Next

                            Dim TotalHoursThisEvent = AnEvent.TotalNumberHours

                            If TotalHoursNextTurn > 0 Then
                                'Si tiene turnos el siguiente día
                                If TotalHoursNextTurn + TotalHoursThisEvent > LegalDailyHours Then
                                    TmpRecargoNocturnoFestivo = LegalDailyHours - TotalHoursNextTurn
                                    RecargoNocturnoFestivo = RecargoNocturnoFestivo + TmpRecargoNocturnoFestivo
                                    If TotalHoursThisEvent - TmpRecargoNocturnoFestivo > 0 Then
                                        'Faltan Horas por repartir, van como hora extra festiva nocturna
                                        TmpHoraExtraNocturna = TotalHoursThisEvent - TmpRecargoNocturnoFestivo
                                        HoraExtraFestivaNocturna = HoraExtraFestivaNocturna + TmpHoraExtraNocturna
                                    End If
                                Else
                                    TmpRecargoNocturnoFestivo = TotalHoursThisEvent
                                    RecargoNocturnoFestivo = RecargoNocturnoFestivo + TmpRecargoNocturnoFestivo
                                End If
                            Else
                                'Si no hay turnos el siguiente día, va como Recargo Nocturno Festivo
                                If ListHolidays.FindAll(Function(x) x.Holiday1 = AnEvent.DateTimeEnding.AddDays(1).Date).Count > 0 Or AnEvent.DateTimeEnding.AddDays(1).DayOfWeek = DayOfWeek.Sunday Then
                                    TmpRecargoNocturnoFestivo = TotalHoursThisEvent
                                    RecargoNocturnoFestivo = RecargoNocturnoFestivo + TmpRecargoNocturnoFestivo
                                End If
                            End If

                        End If

                    End If

                Next
            End If

        Next


        ResultTuple.Add(New Tuple(Of String, Decimal)("DominicalFestivo", DominicalFestivo))
        ResultTuple.Add(New Tuple(Of String, Decimal)("RecargoNocturnoFestivo", RecargoNocturnoFestivo))
        ResultTuple.Add(New Tuple(Of String, Decimal)("HoraExtraFestivaDiurna", HoraExtraFestivaDiurna))
        ResultTuple.Add(New Tuple(Of String, Decimal)("HoraExtraFestivaNocturna", HoraExtraFestivaNocturna))

        Return ResultTuple

    End Function


    Public Function AdjustFestiveHours(ByVal ScheduleDetailEmployee As List(Of ScheduleDetail), ListHolidays As List(Of Holiday), TmpScheduleDetailEmployeeLastMonth As List(Of ScheduleDetail), ByVal PayrollStarDate As Date) As Decimal Implements ILiquidationFunctions.AdjustFestiveHours

        Dim VarHour As Decimal = 0

        If TmpScheduleDetailEmployeeLastMonth IsNot Nothing Then
            If TmpScheduleDetailEmployeeLastMonth.Any(Function(x) x.ScheduleDetailHour.Any(Function(y) y.NextDay = True And y.DateTimeEnding.Date.AddDays(1).Month = PayrollStarDate.Month)) Then
                ScheduleDetailEmployee.AddRange(TmpScheduleDetailEmployeeLastMonth)
            End If
        End If

        For Each ObjScheduleDetail As ScheduleDetail In ScheduleDetailEmployee
            For Each ObjScheduleDetailHour As ScheduleDetailHour In ObjScheduleDetail.ScheduleDetailHour

                If ObjScheduleDetailHour.Event = False Then
                    'Analizo los eventos que iniciaron el festivo y terminaron el festivo
                    If ListHolidays.FindAll(Function(x) x.Holiday1.Date = ObjScheduleDetailHour.DateTimeEnding.Date And ObjScheduleDetailHour.NextDay = False).Count > 0 Then
                        VarHour = VarHour + ObjScheduleDetailHour.TotalNumberHours
                    End If

                    'Analizo los eventos que vienen del día anterior y que tienen horas en este
                    If ListHolidays.FindAll(Function(x) x.Holiday1.Date = ObjScheduleDetailHour.DateTimeInitial.Date.AddDays(1)).Count > 0 Then
                        If ObjScheduleDetailHour.NextDay = True AndAlso ListHolidays.FindAll(Function(x) x.Holiday1.Date = ObjScheduleDetailHour.DateTimeEnding.Date.AddDays(1)).Count > 0 Then
                            VarHour = VarHour + ObjScheduleDetailHour.TotalNumberHours
                        End If
                    End If
                End If

            Next

        Next

        Return VarHour

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
    Public Function HourByConcept(ByVal handlesTurnsChart As Boolean, ByVal PayrollDays As Integer, AuthorizationConcept As AuthorizationConcept, HoursDaily As Integer, ByVal ScheduleDetailEmployee As List(Of ScheduleDetail)) As Decimal Implements ILiquidationFunctions.HourByConcept

        Dim VarHour As Decimal = 0
        Dim Hour As Decimal = 0

        Dim CompareConceptType As Boolean

        If handlesTurnsChart = True Or AuthorizationConcept.Concept.ConceptClass <> "005" Then

            For i As Integer = 0 To (ScheduleDetailEmployee.Count() - 1)
                Dim listHour = (From e In ScheduleDetailEmployee.Item(i).ScheduleDetailHour Where e.Event = False Or (e.Event = True And e.Approved = True) Select e).ToList
                For j As Integer = 0 To (listHour.Count() - 1)

                    For l As Integer = 0 To (listHour.Item(j).ScheduleDetailConcept.Count() - 1)

                        Dim ConceptType = listHour.Item(j).ScheduleDetailConcept.Item(l).ConceptType
                        Dim AppliedConcept = listHour.Item(j).AppliedLiquidationConcept

                        If ConceptType = 1 Then
                            CompareConceptType = True
                        Else
                            CompareConceptType = False
                        End If

                        If listHour.Item(j).ScheduleDetailConcept.Item(l).ConceptId = AuthorizationConcept.Concept.Id And AppliedConcept = CompareConceptType Then
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
                    Dim listHour = (From e In ScheduleDetailEmployee.Item(i).ScheduleDetailHour Where e.Event = False Or (e.Event = True And e.Approved = True) Select e).ToList
                    For j As Integer = 0 To (listHour.Count() - 1)
                        For l As Integer = 0 To (listHour.Item(j).ScheduleDetailConcept.Count() - 1)

                            Dim ConceptType = listHour.Item(j).ScheduleDetailConcept.Item(l).ConceptType
                            Dim AppliedConcept = listHour.Item(j).AppliedLiquidationConcept

                            If ConceptType = 1 Then
                                CompareConceptType = True
                            Else
                                CompareConceptType = False
                            End If

                            If listHour.Item(j).ScheduleDetailConcept.Item(l).ConceptId = AuthorizationConcept.Concept.Id And AppliedConcept = CompareConceptType Then
                                VarHour = listHour.Item(j).TotalNumberHours
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

        If Hour > 0 Then
            Return Hour
        Else
            Return 0
        End If

    End Function

    ''' <summary>
    ''' Función para Calcular las Novedades Automáticas de Recargos generados por los ajustes de incapacidades
    ''' </summary>
    ''' <param name="handlesTurnsChart">Maneja Cuadro de Turnos</param>
    ''' <param name="PayrollDays">Días de Nómina</param>
    ''' <param name="AuthorizationConcept">AuthorizationConcept</param>
    ''' <param name="HoursDaily">Horas Diarias</param>
    ''' <param name="ScheduleDetailEmployee">ScheduleDetailEmployee</param>
    ''' <returns>Decimal</returns>
    Public Function NoveltyHourByConcept(ByVal handlesTurnsChart As Boolean, ByVal PayrollDays As Integer, AuthorizationConcept As AuthorizationConcept, HoursDaily As Integer, ByVal ScheduleDetailEmployee As List(Of NoveltyScheduleDetail)) As Decimal Implements ILiquidationFunctions.NoveltyHourByConcept

        Dim VarHour As Decimal = 0
        Dim Hour As Decimal = 0

        If handlesTurnsChart = True AndAlso AuthorizationConcept.Concept.ConceptClass <> "005" Then

            For i As Integer = 0 To (ScheduleDetailEmployee.Count() - 1)
                Dim listHour = (From e In ScheduleDetailEmployee.Item(i).NoveltyScheduleDetailHour).ToList
                For j As Integer = 0 To (listHour.Count() - 1)

                    For l As Integer = 0 To (listHour.Item(j).NoveltyScheduleDetailConcept.Count() - 1)

                        If listHour.Item(j).NoveltyScheduleDetailConcept.Item(l).Concept.IdAdjustmentConcept = AuthorizationConcept.Concept.Id Then
                            VarHour = ScheduleDetailEmployee.Item(i).NoveltyScheduleDetailHour.Item(j).TotalNumberHours
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

        If Hour > 0 Then
            Return Hour
        Else
            Return 0
        End If

    End Function

    ''' <summary>
    ''' Función que valida si Existen Novedades de Cuadros de Turnos
    ''' </summary>
    ''' <param name="ScheduleDetailEmployee">ScheduleDetailEmployee</param>
    ''' <returns>ActionMessageResult(Of List(Of Liquidation))</returns>
    Public Function ValidatedConceptNoveltySchedule(ScheduleDetailEmployee As List(Of NoveltyScheduleDetail), PayrollSettings As PayrollSettings) As ActionMessageResult(Of List(Of Liquidation)) Implements ILiquidationFunctions.ValidatedConceptNoveltySchedule

        Dim ActionMessageResult As New ActionMessageResult(Of List(Of Liquidation))
        ActionMessageResult.StateResult = True

        For i As Integer = 0 To (ScheduleDetailEmployee.Count() - 1)
            Dim listHour = (From e In ScheduleDetailEmployee.Item(i).NoveltyScheduleDetailHour).ToList
            For j As Integer = 0 To (listHour.Count() - 1)

                For l As Integer = 0 To (listHour.Item(j).NoveltyScheduleDetailConcept.Count() - 1)

                    If PayrollSettings.ConceptNoveltyAdjust.Any(Function(x) x.IdConcept = listHour.Item(j).NoveltyScheduleDetailConcept.Item(l).ConceptId) = False And listHour.Item(j).NoveltyScheduleDetailConcept.Item(l).Concept.ConceptClass <> "005" Then
                        Dim ObjConcept = listHour.Item(j).NoveltyScheduleDetailConcept.Item(l).Concept
                        ActionMessageResult.MessageResult.Add(New MessageResult("-008: Novedades Cuadros de Turno", "Se han agregado Novedades de ajustes de cuadro de turno, pero el Concepto " & ObjConcept.Code & " - " & ObjConcept.Name & " no tiene parametrizado el Concepto de Ajuste"))
                        ActionMessageResult.StateResult = False

                        Return ActionMessageResult
                    End If

                Next
            Next
        Next
        Return ActionMessageResult

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
    ''' Concepto 502
    Public Function GetIBCPension(ByVal EmployeeSalary As Double, ByVal LegalSalaryMinimum As Double, ByVal VarIBCPension As Double, PayrollDays As Integer, ByVal UnpaidLicensesDay As Integer, ByVal AccruedValue As Double, ByVal EmployeePensionPercentage As Double, VacationDays As Integer, RetirementFlag As Boolean, IBCHealth As Double, NumberOfContract As Integer, DaysWorkedEmployee As Integer, InabilitiesDays As Integer, WorkedDays As Integer, FlagIBCPension As Boolean) As Double Implements ILiquidationFunctions.GetIBCPension

        Dim BaseIBCPension As Double

        If FlagIBCPension = False Then

            If NumberOfContract = 1 Then
                If EmployeeSalary < LegalSalaryMinimum Then
                    If WorkedDays < 30 Then
                        If EmployeeSalary < LegalSalaryMinimum Then
                            If AccruedValue - (VarIBCPension * (EmployeePensionPercentage / 100)) <= 0 Then
                                BaseIBCPension = 0
                            Else
                                BaseIBCPension = ((LegalSalaryMinimum / 30) * PayrollDays)
                            End If
                        Else
                            If AccruedValue - (VarIBCPension * (EmployeePensionPercentage / 100)) <= 0 Then
                                BaseIBCPension = 0
                            Else
                                BaseIBCPension = ((EmployeeSalary / 30) * DaysWorkedEmployee) + VarIBCPension
                            End If
                        End If
                    Else
                        If AccruedValue - (VarIBCPension * (EmployeePensionPercentage / 100)) <= 0 Then
                            BaseIBCPension = 0
                        Else
                            BaseIBCPension = (LegalSalaryMinimum / 30) * PayrollDays
                        End If
                    End If
                Else
                    If AccruedValue - (VarIBCPension * (EmployeePensionPercentage / 100)) <= 0 Then
                        BaseIBCPension = 0
                    Else
                        If WorkedDays < 30 Then
                            BaseIBCPension = ((EmployeeSalary / 30) * (DaysWorkedEmployee - InabilitiesDays)) + VarIBCPension
                        Else
                            BaseIBCPension = ((EmployeeSalary / 30) * (PayrollDays - InabilitiesDays)) + VarIBCPension
                        End If

                    End If
                End If

                If VacationDays > 0 Then BaseIBCPension = (((EmployeeSalary / 30) * (DaysWorkedEmployee - VacationDays - InabilitiesDays)) + VarIBCPension)
                'If VacationDays > 0 Then BaseIBCPension = VarIBCPension
            Else
                ' En caso de que haya un otro si en el periodo
                BaseIBCPension = IBCHealth

                If VacationDays > 0 Then BaseIBCPension = (((EmployeeSalary / 30) * (DaysWorkedEmployee - VacationDays)) + VarIBCPension)
                'If VacationDays > 0 Then BaseIBCPension = VarIBCPension

            End If

            If RetirementFlag = True Then
                BaseIBCPension = IBCHealth
            End If
        Else
            BaseIBCPension = ((EmployeeSalary / 30) * (DaysWorkedEmployee - InabilitiesDays)) + VarIBCPension
            If VacationDays > 0 Then BaseIBCPension = (((EmployeeSalary / 30) * (DaysWorkedEmployee - VacationDays - InabilitiesDays)) + VarIBCPension)
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
    ''' Concepto 501
    Public Function GetIBCHealth(ByVal VarIBCHealth As Double, ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal VacationDays As Integer, ByVal PayrollDays As Integer, ByVal EmployeeHealthPercentage As Double, ByVal AccruedValue As Double, ByVal UnpaidLicensesDay As Integer, IBCPension As Double, NumberContract As Integer, PeriodDays As Integer, RetirementFlag As Boolean, FlagIBCHealth As Boolean) As Double Implements ILiquidationFunctions.GetIBCHealth

        Dim BaseIBCHealth As Double

        If FlagIBCHealth = False Then

            If NumberContract = 1 Then
                If VarIBCHealth < LegalSalaryMinimum Then
                    If PeriodDays < 30 Then
                        If EmployeeSalary < LegalSalaryMinimum Then
                            If AccruedValue - (VarIBCHealth * (EmployeeHealthPercentage / 100)) <= 0 Then
                                BaseIBCHealth = 0
                            Else
                                BaseIBCHealth = ((LegalSalaryMinimum / 30) * PayrollDays)
                            End If
                        Else
                            If AccruedValue - (VarIBCHealth * (EmployeeHealthPercentage / 100)) <= 0 Then
                                BaseIBCHealth = 0
                            Else
                                BaseIBCHealth = VarIBCHealth
                            End If
                        End If
                    Else
                        If AccruedValue - (VarIBCHealth * (EmployeeHealthPercentage / 100)) <= 0 Then
                            BaseIBCHealth = 0
                        Else
                            BaseIBCHealth = VarIBCHealth
                        End If
                    End If
                Else
                    If AccruedValue - (VarIBCHealth * (EmployeeHealthPercentage / 100)) <= 0 Then
                        BaseIBCHealth = 0
                    Else
                        BaseIBCHealth = VarIBCHealth
                    End If
                End If

                If VacationDays > 0 Then BaseIBCHealth = (((EmployeeSalary / 30) * PayrollDays) + IBCPension)
                'If VacationDays > 0 Then BaseIBCHealth = VarIBCHealth
            Else
                ' En caso de que haya un otro si en el periodo
                BaseIBCHealth = VarIBCHealth

                'If VacationDays > 0 Then BaseIBCHealth = (((EmployeeSalary / 30) * PeriodDays) + IBCPension)
                If VacationDays > 0 Then BaseIBCHealth = (((EmployeeSalary / 30) * PeriodDays) + IBCPension)

            End If

        Else
            BaseIBCHealth = VarIBCHealth
        End If

        If RetirementFlag = True Then
            BaseIBCHealth = VarIBCHealth
        End If

        Return BaseIBCHealth
    End Function

    ''' <summary>
    ''' Obtiene la base del Fondo de la Caja de Compensación
    ''' </summary>
    ''' <param name="IBCFundValue">Valor IBC del Fondo</param>
    ''' <param name="PayrollDays">Días de la Nómina</param>
    ''' <param name="LegalSalaryMinimun">Salario Legal Mínimo</param>
    ''' <param name="PaidLicensesDays">Licencias Remuneradas</param>
    ''' <param name="UnpaidLicensesDays">Licencias NO Remuneradas</param>
    ''' <param name="HospitalInabilitiesDays">Días Incapacidades Hospitalarias</param>
    ''' <param name="InabilitiesPeriodDays">Días de Incapacidades</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días de Vacaciones</param>
    ''' <param name="CompensationFundContributionPercentage">Porcentaje de la Caja de Compensación</param>
    ''' <returns></returns>
    ''' <remarks>Concepto 904</remarks>

    Public Function GetBaseParafiscalCompensationFund(ByVal IBCFundValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, ByVal CompensationFundContributionPercentage As Double, NumberOfContract As Integer, PeriodDays As Integer, IBCPension As Double) As Double Implements ILiquidationFunctions.GetBaseParafiscalCompensationFund

        Dim BaseParafiscalCompensationFund As Double

        If NumberOfContract = 1 Then

            If (IBCFundValue / PayrollDays) >= (LegalSalaryMinimun / 30) Then
                BaseParafiscalCompensationFund = IBCFundValue
            Else
                BaseParafiscalCompensationFund = ((LegalSalaryMinimun / 30) * PayrollDays)
            End If

            ' Cuando existen novedades en días de Incapacidades o Licencias
            If PaidLicensesDays > 0 Or UnpaidLicensesDays > 0 Or HospitalInabilitiesDays > 0 Or InabilitiesPeriodDays > 0 Then
                If (EmployeeSalary < LegalSalaryMinimun) Then
                    BaseParafiscalCompensationFund = (((LegalSalaryMinimun / 30) * (PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays)))
                Else
                    BaseParafiscalCompensationFund = IBCFundValue
                End If
            End If

            If UnpaidLicensesDays = 30 Then
                BaseParafiscalCompensationFund = 0
            End If

            If VacationDays > 0 Then
                BaseParafiscalCompensationFund = (((EmployeeSalary) / 30 * ((PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays)))) + IBCPension
            End If
        Else
            BaseParafiscalCompensationFund = IBCFundValue

            If VacationDays > 0 Then BaseParafiscalCompensationFund = ((EmployeeSalary) / 30) * (PeriodDays) + IBCPension
        End If

        Return BaseParafiscalCompensationFund

    End Function

    ''' <summary>
    ''' Obtiene la base del Fondo de la Caja de Compensación
    ''' </summary>
    ''' <param name="IBCFundValue">Valor IBC del Fondo</param>
    ''' <param name="PayrollDays">Días de la Nómina</param>
    ''' <param name="LegalSalaryMinimun">Salario Legal Mínimo</param>
    ''' <param name="PaidLicensesDays">Licencias Remuneradas</param>
    ''' <param name="UnpaidLicensesDays">Licencias NO Remuneradas</param>
    ''' <param name="HospitalInabilitiesDays">Días Incapacidades Hospitalarias</param>
    ''' <param name="InabilitiesPeriodDays">Días de Incapacidades</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días de Vacaciones</param>
    ''' <param name="CompensationFundContributionPercentage">Porcentaje de la Caja de Compensación</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBaseParafiscalCompensationFundIntegral(ByVal IBCFundValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, ByVal CompensationFundContributionPercentage As Double, NumberOfContract As Integer, PeriodDays As Integer) As Double Implements ILiquidationFunctions.GetBaseParafiscalCompensationFundIntegral

        Dim BaseParafiscalCompensationFundIntegral As Double

        If NumberOfContract = 1 Then
            If (IBCFundValue / PayrollDays) >= (LegalSalaryMinimun / 30) Then
                BaseParafiscalCompensationFundIntegral = (EmployeeSalary * 0.7)
            Else
                BaseParafiscalCompensationFundIntegral = (((LegalSalaryMinimun / 30) * PayrollDays))
            End If

            ' Cuando existen novedades en días de Incapacidades o Licencias
            If PaidLicensesDays > 0 Or UnpaidLicensesDays > 0 Or HospitalInabilitiesDays > 0 Or InabilitiesPeriodDays > 0 Then
                If (EmployeeSalary < LegalSalaryMinimun) Then
                    BaseParafiscalCompensationFundIntegral = (((LegalSalaryMinimun / 30) * (PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays)))
                Else
                    BaseParafiscalCompensationFundIntegral = (EmployeeSalary * 0.7)
                End If
            End If

            If UnpaidLicensesDays = 30 Then BaseParafiscalCompensationFundIntegral = 0

            If VacationDays > 0 Then
                BaseParafiscalCompensationFundIntegral = (((EmployeeSalary * 0.7) / 30 * ((PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays))))
            End If
        Else
            BaseParafiscalCompensationFundIntegral = ((EmployeeSalary * 0.7) / 30) * PeriodDays

        End If

        If (PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays) > 0 Then
            BaseParafiscalCompensationFundIntegral = ((EmployeeSalary * 0.7) / 30) * PeriodDays
        End If

        Return BaseParafiscalCompensationFundIntegral

    End Function

    ''' <summary>
    ''' Obtiene la base del ICBF
    ''' </summary>
    ''' <param name="IBCICBFValue">Valor IBC ICBF</param>
    ''' <param name="PayrollDays">Días Nómina</param>
    ''' <param name="LegalSalaryMinimun">Salario Mínimo Legal Vigente</param>
    ''' <param name="PaidLicensesDays">Días de Licencias Remuneradas</param>
    ''' <param name="UnpaidLicensesDays">Días de Licencias NO Remuneradas</param>
    ''' <param name="HospitalInabilitiesDays">Días Incapacidades Hospitalarias</param>
    ''' <param name="InabilitiesPeriodDays">Días de Incapacidades en el Periodo</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días Vacaciones</param>
    ''' <param name="ICBFContributionPercentage">Porcentaje del ICBF</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBaseParafiscalICBF(ByVal IBCICBFValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, ICBFContributionPercentage As Double, SalaryMonth As Double, NumberOfContract As Integer, PeriodDays As Integer) As Double Implements ILiquidationFunctions.GetBaseParafiscalICBF

        Dim BaseParafiscalICBF As Double

        If NumberOfContract = 1 Then

            If (IBCICBFValue / PayrollDays) >= (LegalSalaryMinimun / 30) Then
                BaseParafiscalICBF = IBCICBFValue
            Else
                BaseParafiscalICBF = ((LegalSalaryMinimun / 30) * PayrollDays)
            End If

            ' Cuando existen novedades en días de Incapacidades o Licencias
            If PaidLicensesDays > 0 Or UnpaidLicensesDays > 0 Or HospitalInabilitiesDays > 0 Or InabilitiesPeriodDays > 0 Then
                If (EmployeeSalary < LegalSalaryMinimun) Then
                    BaseParafiscalICBF = ((LegalSalaryMinimun / 30) * (PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays))
                Else
                    BaseParafiscalICBF = IBCICBFValue
                End If
            End If

            If UnpaidLicensesDays = 30 Then
                BaseParafiscalICBF = 0
            End If

            If VacationDays > 0 Then
                BaseParafiscalICBF = ((EmployeeSalary) / 30) * (PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays)
            End If

        Else
            ' Para el contrato OTRO SI
            BaseParafiscalICBF = IBCICBFValue
            If VacationDays > 0 Then BaseParafiscalICBF = ((EmployeeSalary) / 30) * PeriodDays

        End If

        Return BaseParafiscalICBF

    End Function

    ''' <summary>
    ''' Obtiene la base del ICBF Integral
    ''' </summary>
    ''' <param name="IBCICBFValue">Valor IBC ICBF</param>
    ''' <param name="PayrollDays">Días Nómina</param>
    ''' <param name="LegalSalaryMinimun">Salario Mínimo Legal Vigente</param>
    ''' <param name="PaidLicensesDays">Días de Licencias Remuneradas</param>
    ''' <param name="UnpaidLicensesDays">Días de Licencias NO Remuneradas</param>
    ''' <param name="HospitalInabilitiesDays">Días Incapacidades Hospitalarias</param>
    ''' <param name="InabilitiesPeriodDays">Días de Incapacidades en el Periodo</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días Vacaciones</param>
    ''' <param name="ICBFContributionPercentage">Porcentaje del ICBF</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBaseParafiscalICBFIntegral(ByVal IBCICBFValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, ICBFContributionPercentage As Double, NumberOfContract As Integer, PeriodDays As Integer) As Double Implements ILiquidationFunctions.GetBaseParafiscalICBFIntegral

        Dim BaseParafiscalICBFIntegral As Double

        If NumberOfContract = 1 Then
            If (IBCICBFValue / PayrollDays) >= (LegalSalaryMinimun / 30) Then
                BaseParafiscalICBFIntegral = (EmployeeSalary * 0.7)
            Else
                BaseParafiscalICBFIntegral = (((EmployeeSalary * 0.7) / 30) * PayrollDays)
            End If

            ' Cuando existen novedades en días de Incapacidades o Licencias
            If PaidLicensesDays > 0 Or UnpaidLicensesDays > 0 Or HospitalInabilitiesDays > 0 Or InabilitiesPeriodDays > 0 Then
                If (EmployeeSalary < LegalSalaryMinimun) Then
                    BaseParafiscalICBFIntegral = ((LegalSalaryMinimun / 30) * (PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays))
                Else
                    BaseParafiscalICBFIntegral = (EmployeeSalary * 0.7)
                End If
            End If

            If UnpaidLicensesDays = 30 Then
                BaseParafiscalICBFIntegral = 0
            End If

            If VacationDays > 30 Then
                BaseParafiscalICBFIntegral = ((EmployeeSalary * 0.7) / 30) * ((PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays))
            End If
        Else
            BaseParafiscalICBFIntegral = ((EmployeeSalary * 0.7) / 30) * PeriodDays
        End If

        If PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays > 0 Then
            BaseParafiscalICBFIntegral = ((EmployeeSalary * 0.7) / 30) * PeriodDays
        End If

        Return BaseParafiscalICBFIntegral

    End Function


    ''' <summary>
    ''' Obtiene el Valor Base de Salud del Patrono
    ''' </summary>
    ''' <param name="LegalSalaryMinimum">Salario Mínimo Legal Vigente</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días de Vacaciones</param>
    ''' <param name="PayrollDays">Días de Nómina</param>
    ''' <param name="EmployeeDays">Días del Empleado</param>
    ''' <param name="VarIBCHealthEmployee">Valor IBC de la Salud del Empleado</param>
    ''' <returns>Valor Base de Cálculo de Salud del Patrono</returns>
    ''' <remarks></remarks>
    ''' 901 Salud Empleado
    Public Function GetIBCHealthEmployer(ByVal LegalSalaryMinimum As Double, ByVal VacationDays As Integer, ByVal PayrollDays As Integer, ByVal IBCHealth As Double, EmployerHealthContributionPercentage As Double, TotalDaysLicensesUnpaid As Integer, IBCPension As Double, EmployeeSalary As Double, EmployeeHealthContributionPercentage As Double, NumberOfContract As Integer, PeriodDays As Integer, TotalPeriodDaysInabilities As Integer) As Double Implements ILiquidationFunctions.GetIBCHealthEmployer

        Dim BaseIBCHealthEmployer As Double

        If NumberOfContract = 1 Then
            If IBCHealth < LegalSalaryMinimum Then
                If (IBCHealth / PayrollDays) > (LegalSalaryMinimum / 30) Then
                    BaseIBCHealthEmployer = IBCHealth
                Else
                    BaseIBCHealthEmployer = ((LegalSalaryMinimum / 30) * PayrollDays)
                End If
            Else
                BaseIBCHealthEmployer = IBCHealth
            End If

            'If TotalDaysLicensesUnpaid = 30 Then BaseIBCHealthEmployer = 0

            If VacationDays > 0 Then
                BaseIBCHealthEmployer = IBCHealth '((EmployeeSalary / 30) * (PayrollDays - TotalDaysLicensesUnpaid - TotalPeriodDaysInabilities)) + IBCPension
            End If

            If PeriodDays < 30 AndAlso TotalDaysLicensesUnpaid > 0 Then
                BaseIBCHealthEmployer = EmployeeSalary
            End If

        Else
            ' En caso de que haya un otro si en el periodo
            BaseIBCHealthEmployer = IBCHealth

            If VacationDays > 0 Then
                BaseIBCHealthEmployer = ((EmployeeSalary / 30) * (PeriodDays)) + IBCPension
            End If
        End If

        Return BaseIBCHealthEmployer

    End Function


    ''' <summary>
    ''' Obtiene el Valor Base de Salud del Patrono para Salarios Integrales
    ''' </summary>
    ''' <param name="LegalSalaryMinimum">Salario Mínimo Legal Vigente</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="PayrollDays">Días de Nómina</param>
    ''' <returns>Valor Base de Cálculo de Salud del Patrono</returns>
    ''' <remarks></remarks>
    ''' Concepto 913

    Public Function GetIBCHealthEmployerIntegral(ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal PayrollDays As Integer, ByVal IBCHealth As Double, EmployerHealthContributionPercentage As Double, TotalDaysLicensesUnpaid As Integer, NumberOfContract As Integer, PeriodDays As Integer) As Double Implements ILiquidationFunctions.GetIBCHealthEmployerIntegral

        Dim BaseIBCHealthEmployerIntegral As Double

        If NumberOfContract = 1 Then
            If IBCHealth < LegalSalaryMinimum Then
                If ((EmployeeSalary * 0.7) / PayrollDays) > (LegalSalaryMinimum / 30) Then
                    BaseIBCHealthEmployerIntegral = (EmployeeSalary * 0.7)
                Else
                    BaseIBCHealthEmployerIntegral = (((LegalSalaryMinimum / 30) * PayrollDays))
                End If
            Else
                BaseIBCHealthEmployerIntegral = (((EmployeeSalary * 0.7) + (((LegalSalaryMinimum / 30) * TotalDaysLicensesUnpaid))))
            End If

            If TotalDaysLicensesUnpaid = 30 Then BaseIBCHealthEmployerIntegral = 0
        Else
            BaseIBCHealthEmployerIntegral = ((EmployeeSalary * 0.7) / 30) * PeriodDays
        End If

        Return BaseIBCHealthEmployerIntegral

    End Function

    ''' <summary>
    ''' Obtiene la base del Sena Integral
    ''' </summary>
    ''' <param name="IBCICBFValue">Valor IBC ICBF</param>
    ''' <param name="PayrollDays">Días Nómina</param>
    ''' <param name="LegalSalaryMinimun">Salario Mínimo Legal Vigente</param>
    ''' <param name="PaidLicensesDays">Días de Licencias Remuneradas</param>
    ''' <param name="UnpaidLicensesDays">Días de Licencias NO Remuneradas</param>
    ''' <param name="HospitalInabilitiesDays">Días Incapacidades Hospitalarias</param>
    ''' <param name="InabilitiesPeriodDays">Días de Incapacidades en el Periodo</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días Vacaciones</param>
    ''' <param name="SenaContributionPercentage">Porcentaje del SENA</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBaseSenaIntegral(ByVal IBCSENAValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, SenaContributionPercentage As Double, NumberOfContract As Integer, PeriodDays As Integer) As Double Implements ILiquidationFunctions.GetBaseSenaIntegral

        Dim BaseSenaIntegral As Double

        If NumberOfContract = 1 Then
            If (IBCSENAValue / PayrollDays) >= (LegalSalaryMinimun / 30) Then
                BaseSenaIntegral = (EmployeeSalary * 0.7)
            Else
                BaseSenaIntegral = ((LegalSalaryMinimun / 30) * PayrollDays)
            End If

            ' Cuando existen novedades en días de Incapacidades o Licencias
            If PaidLicensesDays > 0 Or UnpaidLicensesDays > 0 Or HospitalInabilitiesDays > 0 Or InabilitiesPeriodDays > 0 Then
                If (EmployeeSalary < LegalSalaryMinimun) Then
                    BaseSenaIntegral = ((LegalSalaryMinimun / 30) * (PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays))
                Else
                    BaseSenaIntegral = (EmployeeSalary * 0.7)
                End If
            End If

            If UnpaidLicensesDays = 30 Then
                BaseSenaIntegral = 0
            End If

            If VacationDays > 0 Then
                BaseSenaIntegral = ((EmployeeSalary * 0.7) / 30) * ((PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays))
            End If
        Else
            BaseSenaIntegral = ((EmployeeSalary * 0.7) / 30) * PeriodDays
        End If

        If PaidLicensesDays + UnpaidLicensesDays + HospitalInabilitiesDays + InabilitiesPeriodDays Then
            BaseSenaIntegral = ((EmployeeSalary * 0.7) / 30) * PeriodDays
        End If

        Return BaseSenaIntegral

    End Function

    ''' <summary>
    ''' Obtiene la base del Sena
    ''' </summary>
    ''' <param name="IBCICBFValue">Valor IBC ICBF</param>
    ''' <param name="PayrollDays">Días Nómina</param>
    ''' <param name="LegalSalaryMinimun">Salario Mínimo Legal Vigente</param>
    ''' <param name="PaidLicensesDays">Días de Licencias Remuneradas</param>
    ''' <param name="UnpaidLicensesDays">Días de Licencias NO Remuneradas</param>
    ''' <param name="HospitalInabilitiesDays">Días Incapacidades Hospitalarias</param>
    ''' <param name="InabilitiesPeriodDays">Días de Incapacidades en el Periodo</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días Vacaciones</param>
    ''' <param name="SenaContributionPercentage">Porcentaje del SENA</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBaseSena(ByVal IBCSENAValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, SenaContributionPercentage As Double, NumberOfContract As Integer, PeriodDays As Integer) As Double Implements ILiquidationFunctions.GetBaseSena

        Dim BaseSena As Double

        If NumberOfContract = 1 Then

            If (IBCSENAValue / PayrollDays) >= (LegalSalaryMinimun / 30) Then
                BaseSena = IBCSENAValue
            Else
                BaseSena = ((LegalSalaryMinimun / 30) * PayrollDays)
            End If

            ' Cuando existen novedades en días de Incapacidades o Licencias
            If PaidLicensesDays > 0 Or UnpaidLicensesDays > 0 Or HospitalInabilitiesDays > 0 Or InabilitiesPeriodDays > 0 Then
                If (EmployeeSalary < LegalSalaryMinimun) Then
                    BaseSena = ((LegalSalaryMinimun / 30) * (PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays))
                Else
                    BaseSena = IBCSENAValue
                End If
            End If

            If UnpaidLicensesDays = 30 Then BaseSena = 0

            If (HospitalInabilitiesDays + InabilitiesPeriodDays) = 30 Then BaseSena = 0

            If VacationDays > 0 Then
                BaseSena = ((EmployeeSalary) / 30 * ((PayrollDays - PaidLicensesDays - UnpaidLicensesDays - HospitalInabilitiesDays - InabilitiesPeriodDays)))
            End If
        Else
            BaseSena = IBCSENAValue
            If VacationDays > 0 Then BaseSena = ((EmployeeSalary) / 30) * PeriodDays

        End If

        Return BaseSena

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
    ''' Concepto 902
    ''' 
    Public Function GetIBCPensionEmployer(ByVal PensionIBC As Double, ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal UnpaidLicensesDay As Integer, PayrollDays As Integer, NumberOfContract As Integer, PeriodDays As Integer, TotalInabilitiesDays As Integer, IBCHealth As Double, WorkedDays As Integer, VacationDays As Integer) As Double Implements ILiquidationFunctions.GetIBCPensionEmployer

        Dim BaseIBCPensionEmployer As Double

        If NumberOfContract = 1 Then

            If EmployeeSalary < LegalSalaryMinimum Then
                If (((EmployeeSalary / 30) * PayrollDays) + PensionIBC) < LegalSalaryMinimum Then
                    BaseIBCPensionEmployer = ((((LegalSalaryMinimum / 30) * (PayrollDays - UnpaidLicensesDay)))) + (((LegalSalaryMinimum / 30) * UnpaidLicensesDay))
                Else
                    BaseIBCPensionEmployer = PensionIBC
                End If
            Else
                If WorkedDays < 30 Then
                    BaseIBCPensionEmployer = IBCHealth
                Else
                    BaseIBCPensionEmployer = (((EmployeeSalary / 30) * (PeriodDays - UnpaidLicensesDay - TotalInabilitiesDays)) + PensionIBC) + ((LegalSalaryMinimum / 30) * UnpaidLicensesDay)
                End If

                If VacationDays > 0 Then
                    BaseIBCPensionEmployer = PensionIBC '(((EmployeeSalary / 30) * PayrollDays) + PensionIBC)
                End If

                If WorkedDays < 30 AndAlso UnpaidLicensesDay > 0 Then
                    BaseIBCPensionEmployer = EmployeeSalary
                End If

            End If
        Else
            ' En caso de que haya un otro si en el periodo
            'BaseIBCPensionEmployer = (((EmployeeSalary / 30) * (PeriodDays)) + PensionIBC) + ((LegalSalaryMinimum / 30) * UnpaidLicensesDay)
            BaseIBCPensionEmployer = IBCHealth

            If VacationDays > 0 Then BaseIBCPensionEmployer = (((EmployeeSalary / 30) * PeriodDays) + PensionIBC)

        End If

        Return BaseIBCPensionEmployer

    End Function

    ''' <summary>
    ''' Obtiene el Valor Base de Pensión del Patrono Integral
    ''' </summary>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="LegalSalaryMinimum">Salario Legal Mínimo Vigente</param>
    ''' <param name="PayrollDays">Días de la Nómina</param>
    ''' <param name="UnpaidLicensesDay">Días de Licencias</param>
    ''' <param name="EmployeeDays">Días del Empleado</param>
    ''' <param name="VarIBCPensionEmployee">Valor IBC Pensión del Empleado</param>
    ''' <returns>Valor Base de Cálculo de Pensión del Patrono</returns>
    ''' <remarks></remarks>
    ''' Concepto 914
    Public Function GetIBCPensionEmployerIntegral(ByVal PensionIBC As Double, ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal UnpaidLicensesDay As Integer, PayrollDays As Integer, NumberOfContract As Integer, PeriodDays As Integer) As Double Implements ILiquidationFunctions.GetIBCPensionEmployerIntegral

        Dim BaseIBCPensionEmployerIntegral As Double

        If NumberOfContract = 1 Then
            If EmployeeSalary < LegalSalaryMinimum Then
                If ((((EmployeeSalary * 0.7) / 30) * PayrollDays) + PensionIBC) < LegalSalaryMinimum Then
                    BaseIBCPensionEmployerIntegral = ((((LegalSalaryMinimum / 30) * (PayrollDays - UnpaidLicensesDay)))) + (((LegalSalaryMinimum / 30) * UnpaidLicensesDay))
                Else
                    BaseIBCPensionEmployerIntegral = ((((((EmployeeSalary * 0.7) / 30) * (PayrollDays - UnpaidLicensesDay)) + PensionIBC) + (((LegalSalaryMinimum / 30) * UnpaidLicensesDay))))
                End If
            Else
                BaseIBCPensionEmployerIntegral = ((((((EmployeeSalary * 0.7) / 30) * (PayrollDays - UnpaidLicensesDay))) + PensionIBC) + (((LegalSalaryMinimum / 30) * UnpaidLicensesDay)))
            End If
        Else
            BaseIBCPensionEmployerIntegral = ((EmployeeSalary * 0.7) / 30) * (PeriodDays) + PensionIBC
        End If

        Return BaseIBCPensionEmployerIntegral

    End Function

    ''' <summary>
    ''' Obtiene el valor Base del ARL
    ''' </summary>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="LegalSalaryMinimun">Salario Mínimo Legal</param>
    ''' <param name="ARPDays">Días ARL</param>
    ''' <param name="IBCArp">IBC ARL</param>
    ''' <param name="ProfessionalRiskPercentage">Porcentaje Riesgo Profesional</param>
    ''' <returns>Valor Base de Cálculo de ARP</returns>
    ''' <remarks></remarks>
    Public Function GetBaseARP(ByVal EmployeeSalary As Double, ByVal LegalSalaryMinimun As Double, ByVal ARPDays As Integer, ByVal IBCArp As Double, ByVal ProfessionalRiskPercentage As Double) As Double Implements ILiquidationFunctions.GetBaseARP

        Dim BaseIBCArp As Double

        If EmployeeSalary < LegalSalaryMinimun Then
            If (((EmployeeSalary / 30) * ARPDays) + IBCArp) < LegalSalaryMinimun Then
                BaseIBCArp = ((LegalSalaryMinimun / 30) * ARPDays)
            Else
                BaseIBCArp = ((EmployeeSalary / 30) * ARPDays) + IBCArp
            End If
        Else
            BaseIBCArp = ((EmployeeSalary / 30) * ARPDays) + IBCArp
        End If

        Return BaseIBCArp

    End Function

    ''' <summary>
    ''' Obtiene el valor Base del ARL Integral
    ''' </summary>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="LegalSalaryMinimun">Salario Mínimo Legal</param>
    ''' <param name="ARPDays">Días ARL</param>
    ''' <param name="IBCArp">IBC ARL</param>
    ''' <param name="ProfessionalRiskPercentage">Porcentaje Riesgo Profesional</param>
    ''' <returns>Valor Base de Cálculo de ARP</returns>
    ''' <remarks></remarks>
    Public Function GetBaseARPIntegral(ByVal EmployeeSalary As Double, ByVal LegalSalaryMinimun As Double, ByVal ARPDays As Integer, ByVal IBCArp As Double, ByVal ProfessionalRiskPercentage As Double) As Double Implements ILiquidationFunctions.GetBaseARPIntegral

        Dim BaseIBCArpIntegral As Double

        If EmployeeSalary < LegalSalaryMinimun Then
            If ((((EmployeeSalary * 0.7) / 30) * ARPDays) + IBCArp) < LegalSalaryMinimun Then
                BaseIBCArpIntegral = ((((LegalSalaryMinimun / 30) * ARPDays)))
            Else
                BaseIBCArpIntegral = (((EmployeeSalary * 0.7) / 30) * ARPDays)
            End If
        Else
            BaseIBCArpIntegral = (((EmployeeSalary * 0.7) / 30) * ARPDays)
        End If

        Return BaseIBCArpIntegral
    End Function


    ' Concepto 532
    ''' <summary>
    ''' Obtiene la Base de Cálculo para el Concepto Fondo de Seguridad Pensional Integral 
    ''' </summary>
    ''' <param name="HealthIBC">IBC Salud</param>
    ''' <param name="LegalSalaryMinium">Salario Mínimo Legal Vigente</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="PayrollDays">Días de Nómina</param>
    ''' <param name="PensionIBC">IBC Pensión</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBaseSecurityPensionalIntegralFound(HealthIBC As Double, LegalSalaryMinium As Double, EmployeeSalary As Double, PayrollDays As Integer, PensionIBC As Double) Implements ILiquidationFunctions.GetBaseSecurityPensionalIntegralFound

        Dim BaseSecurityPensionalIntegral As Double

        If ((HealthIBC * 0.7) > (LegalSalaryMinium * 4)) And ((HealthIBC * 0.7) < (LegalSalaryMinium * 16)) Then
            BaseSecurityPensionalIntegral = ((PensionIBC * 0.7) * 0.01)
        Else
            If ((HealthIBC * 0.7) >= (LegalSalaryMinium * 16)) And ((HealthIBC * 0.7) < (LegalSalaryMinium * 17)) Then
                BaseSecurityPensionalIntegral = (PensionIBC * 0.7) * (1.2 / 100)
            Else
                If ((HealthIBC * 0.7) >= (LegalSalaryMinium * 17)) And ((HealthIBC * 0.7) < (LegalSalaryMinium * 18)) Then
                    BaseSecurityPensionalIntegral = (PensionIBC * 0.7) * (1.4 / 100)
                Else
                    If ((HealthIBC * 0.7) >= (LegalSalaryMinium * 18)) And ((HealthIBC * 0.7) < (LegalSalaryMinium * 19)) Then
                        BaseSecurityPensionalIntegral = (PensionIBC * 0.7) * (1.6 / 100)
                    Else
                        If ((HealthIBC * 0.7) >= (LegalSalaryMinium * 19)) And ((HealthIBC * 0.7) < (LegalSalaryMinium * 20)) Then
                            BaseSecurityPensionalIntegral = (PensionIBC * 0.7) * (1.8 / 100)
                        Else
                            If ((HealthIBC * 0.7) >= (LegalSalaryMinium * 20)) Then
                                BaseSecurityPensionalIntegral = (PensionIBC * 0.7) * (2 / 100)
                            Else
                                BaseSecurityPensionalIntegral = 0
                            End If
                        End If
                    End If
                End If
            End If
        End If

        Return BaseSecurityPensionalIntegral


    End Function

    Public Function EmployeeFundContract(ListFundContract As List(Of FundContract)) As List(Of Tuple(Of String, Integer, Integer, String, Decimal)) Implements ILiquidationFunctions.EmployeeFundContract
        Dim descriptions As New List(Of Tuple(Of String, Integer, Integer, String, Decimal))

        For Each objFundContract As FundContract In ListFundContract
            If objFundContract.FundType = 1 And objFundContract.VoluntaryContribution = False Then
                'SALUD
                descriptions.Add(New Tuple(Of String, Integer, Integer, String, Decimal)("Salud", objFundContract.FundId, objFundContract.Fund.ThirdPartyId, objFundContract.Fund.Name, 0))
            End If

            If objFundContract.FundType = 1 And objFundContract.VoluntaryContribution = True Then
                'SALUD VOLUNTARIA
                descriptions.Add(New Tuple(Of String, Integer, Integer, String, Decimal)("SaludVoluntaria", objFundContract.FundId, objFundContract.Fund.ThirdPartyId, objFundContract.Fund.Name, objFundContract.VoluntaryContributionValue))
            End If

            If objFundContract.FundType = 2 And objFundContract.VoluntaryContribution = False Then
                'PENSIÓN
                descriptions.Add(New Tuple(Of String, Integer, Integer, String, Decimal)("Pension", objFundContract.FundId, objFundContract.Fund.ThirdPartyId, objFundContract.Fund.Name, 0))
            End If

            If objFundContract.FundType = 2 And objFundContract.VoluntaryContribution = True Then
                'PENSIÓN VOLUNTARIA
                descriptions.Add(New Tuple(Of String, Integer, Integer, String, Decimal)("PensionVoluntaria", objFundContract.FundId, objFundContract.Fund.ThirdPartyId, objFundContract.Fund.Name, objFundContract.VoluntaryContributionValue))
            End If

            If objFundContract.FundType = 3 And objFundContract.VoluntaryContribution = False Then
                'CESANTIAS
                descriptions.Add(New Tuple(Of String, Integer, Integer, String, Decimal)("Cesantias", objFundContract.FundId, objFundContract.Fund.ThirdPartyId, objFundContract.Fund.Name, 0))
            End If

            If objFundContract.FundType = 4 And objFundContract.VoluntaryContribution = False Then
                'ARL
                descriptions.Add(New Tuple(Of String, Integer, Integer, String, Decimal)("ARL", objFundContract.FundId, objFundContract.Fund.ThirdPartyId, objFundContract.Fund.Name, 0))
            End If

            If objFundContract.FundType = 5 And objFundContract.VoluntaryContribution = False Then
                'CAJA
                descriptions.Add(New Tuple(Of String, Integer, Integer, String, Decimal)("CajaCompensacion", objFundContract.FundId, objFundContract.Fund.ThirdPartyId, objFundContract.Fund.Name, 0))
            End If

        Next

        Return descriptions
    End Function

    ''' <summary>
    ''' Función para el cálculo de los días de las Vacaciones
    ''' </summary>
    ''' <param name="PayrollStarDate">Fecha Inicio Nómina</param>
    ''' <param name="PayrollEndingDate">Fecha Fin Nómina</param>
    ''' <param name="ObjVacation">Objeto Vacaciones</param>
    ''' <returns></returns>
    Public Function AnalisisVacation(liquidationPayrollDomain As ILiquidationDomain, PayrollStarDate As Date, PayrollEndingDate As Date, ListVacation As List(Of Vacation), ContractEmployee As Entities.Contract, ByRef PaidVacation As Byte, ByRef PaidCredit As Byte, ByRef VacationInitialDate As Date, ByRef VacationEndDate As Date, ByRef VacationPaidType As Integer, ByRef EnjoyDays As Integer, ByRef VacationType As Byte) As List(Of Tuple(Of String, Decimal)) Implements ILiquidationFunctions.AnalisisVacation
        Dim descriptions As New List(Of Tuple(Of String, Decimal))

        Dim VacationDays As Integer = 0
        Dim VacationDaysInCash As Decimal = 0
        Dim VacationValue As Decimal = 0
        Dim VacationValueNet As Decimal = 0
        Dim HealthVacationValue As Decimal = 0
        Dim PensionVacationValue As Decimal = 0
        Dim SolidarityFundVacationValue As Decimal = 0
        Dim VacationBonificationValue As Decimal = 0
        Dim IncentivePaymentVacationValue As Decimal = 0
        Dim VacationalIncreaseValue As Decimal = 0
        Dim CompensationValueVacation As Decimal = 0

        Dim VacationValueRTF As Decimal = 0
        Dim VacationHealthRTF As Decimal = 0
        Dim VacationPensionRTF As Decimal = 0

        Dim InitialVacationDate As Date? = Nothing

        If ListVacation.Any(Function(x) x.TypeVacation <> 3) Then

            Dim DaysEnjoys = ListVacation.Sum(Function(x) x.EnjoyDays)

            For Each ObjVacation As Vacation In ListVacation

                Dim ObjVacationDays As Integer = 0
                VacationInitialDate = ObjVacation.VacationStartDate

                If ContractEmployee.ContractEndingDate < VacationInitialDate Then
                    VacationDays = 0
                    Return descriptions
                End If

                If ObjVacation.IncorporationDateReal <> New Date(1, 1, 1) Then
                    VacationEndDate = DateAdd(DateInterval.Day, -1, ObjVacation.IncorporationDateReal)
                Else
                    VacationEndDate = ObjVacation.VacationEndDate
                End If
                EnjoyDays = ObjVacation.EnjoyDays

                VacationType = ObjVacation.TypeVacation

                If InitialVacationDate Is Nothing Then
                    InitialVacationDate = ObjVacation.VacationStartDate.Date
                Else
                    If ObjVacation.VacationStartDate.Date = InitialVacationDate Then
                        'Es porque son las mismas vacaciones en periodos diferentes
                        Continue For
                    Else
                        InitialVacationDate = ObjVacation.VacationStartDate
                    End If
                End If

                Dim EndVacationDate As Date

                If ObjVacation.TypeVacation = 2 Then

                    If ObjVacation.State = 3 Or ObjVacation.State = 4 Then
                        'Aplazadas
                        EndVacationDate = DateAdd(DateInterval.Day, -1, IIf(ObjVacation.IncorporationDate <> New Date(1, 1, 1), ObjVacation.IncorporationDate, ObjVacation.VacationEndDate))
                    Else
                        EndVacationDate = DateAdd(DateInterval.Day, -1, IIf(ObjVacation.IncorporationDateReal <> New Date(1, 1, 1), ObjVacation.IncorporationDateReal, ObjVacation.VacationEndDate))

                        If ObjVacation.TypePayment = 2 AndAlso ObjVacation.LiquidationDate IsNot Nothing Then
                            If ObjVacation.LiquidationDate < PayrollStarDate And ObjVacation.ForceEntryResolutionDate IsNot Nothing Then
                                EndVacationDate = DateAdd(DateInterval.Day, -1, ObjVacation.IncorporationDate)
                            End If
                        End If

                    End If

                    'Valido los días correspondientes a las vacaciones para saber el disfrute
                    'Si las vacaciones inician y finalizan en el mes
                    If PayrollStarDate <= InitialVacationDate And PayrollEndingDate >= EndVacationDate Then
                        VacationDays = VacationDays + Math.Abs(liquidationPayrollDomain.Days360(InitialVacationDate, EndVacationDate))

                        ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
                        If EndVacationDate.Day = 31 And EndVacationDate <= PayrollEndingDate Then
                            VacationDays = VacationDays + 1
                        End If
                    End If

                    'Si las vacaciones iniciaron un mes antes y terminan este mes
                    If PayrollStarDate > InitialVacationDate And PayrollEndingDate >= EndVacationDate And EndVacationDate >= PayrollStarDate Then
                        VacationDays = VacationDays + liquidationPayrollDomain.Days360(PayrollStarDate, EndVacationDate)

                        ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
                        If EndVacationDate.Day = 31 And EndVacationDate <= PayrollEndingDate Then
                            VacationDays = VacationDays + 1
                        End If
                    End If

                    'Si las vacaciones incian este mes y terminan después
                    If PayrollStarDate <= InitialVacationDate And PayrollEndingDate < EndVacationDate Then
                        VacationDays = VacationDays + liquidationPayrollDomain.Days360(InitialVacationDate, PayrollEndingDate)

                        ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
                        If PayrollEndingDate.Day = 31 Then
                            VacationDays = VacationDays + 1
                        End If
                    End If

                    'Si las vacaciones iniciaron un mes antes y finalizan uno despúes
                    If PayrollStarDate > InitialVacationDate And PayrollEndingDate < EndVacationDate Then
                        VacationDays = VacationDays + liquidationPayrollDomain.Days360(PayrollStarDate, PayrollEndingDate)

                        ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
                        If PayrollEndingDate.Day = 31 Then
                            VacationDays = VacationDays + 1
                        End If
                    End If

                    If ObjVacation.TypeVacation = 1 Or ObjVacation.TypeVacation = 4 Then
                        'Vacaciones Compensadas, se pagan los conceptos pero no calcula días de vacaciones
                        VacationDays = 0
                    End If
                End If

                'Para años bisiestos
                If PayrollEndingDate.Month = 2 Then
                    If EndVacationDate >= PayrollEndingDate Then
                        If PayrollEndingDate.Day = 29 Then
                            VacationDays = VacationDays - 1
                        ElseIf PayrollEndingDate.Day = 28 Then
                            VacationDays = VacationDays - 2
                        End If
                    End If
                End If

                If PayrollEndingDate < InitialVacationDate Then
                    VacationDays = 0
                End If

                VacationValueRTF = ObjVacation.VacationValue
                VacationHealthRTF = ObjVacation.HealthContribution
                VacationPensionRTF = ObjVacation.PensionContribution
                VacationPaidType = ObjVacation.TypePayment



                'Ahora extraigo los valores en caso de que las vacaciones se paguen por nómina y sea esta:
                If ObjVacation.TypePayment = 2 AndAlso ObjVacation.LiquidationDate IsNot Nothing AndAlso ObjVacation.LiquidationDate = PayrollStarDate Then
                    PaidVacation = 1

                    If ObjVacation.TypeVacation = 1 Then
                        CompensationValueVacation = CompensationValueVacation + ObjVacation.VacationValue
                        VacationDaysInCash = VacationDaysInCash + ObjVacation.EnjoyDays
                    Else
                        VacationValue = VacationValue + ObjVacation.VacationValue
                    End If


                    HealthVacationValue = ObjVacation.HealthContribution
                    PensionVacationValue = ObjVacation.PensionContribution
                    SolidarityFundVacationValue = ObjVacation.SolidarityFundValue
                    VacationBonificationValue = ObjVacation.VacationBonificationValue
                    IncentivePaymentVacationValue = ObjVacation.IncentivePaymentVacationValue
                    VacationalIncreaseValue = ObjVacation.VacationalIncreaseValue
                    VacationValueNet = VacationValueNet + ObjVacation.VacationValueNet

                End If

                If ObjVacation.TypeVacation = 2 Then
                    If ObjVacation.LiquidationDate Is Nothing Then
                        PaidCredit = 0
                    Else
                        If ObjVacation.LiquidationDate = PayrollStarDate Then
                            PaidCredit = 1
                        ElseIf DateAdd(DateInterval.Month, 1, ObjVacation.LiquidationDate.Value) = PayrollStarDate Then
                            PaidCredit = 2
                        ElseIf DateAdd(DateInterval.Month, 2, ObjVacation.LiquidationDate.Value) = PayrollStarDate Then
                            PaidCredit = 3
                        Else
                            PaidCredit = 0
                        End If
                    End If
                Else
                    PaidCredit = 3
                End If

                If DaysEnjoys = VacationDays Then
                    Exit For
                End If
            Next

            descriptions.Add(New Tuple(Of String, Decimal)("DiasVacaciones", VacationDays))
            descriptions.Add(New Tuple(Of String, Decimal)("ValorVacaciones", VacationValue))
            descriptions.Add(New Tuple(Of String, Decimal)("ValorPagarVacaciones", VacationValueNet))
            descriptions.Add(New Tuple(Of String, Decimal)("PensionVacaciones", PensionVacationValue))
            descriptions.Add(New Tuple(Of String, Decimal)("SaludVacaciones", HealthVacationValue))
            descriptions.Add(New Tuple(Of String, Decimal)("FSPVacaciones", SolidarityFundVacationValue))
            descriptions.Add(New Tuple(Of String, Decimal)("BonificacionVacaciones", VacationBonificationValue))
            descriptions.Add(New Tuple(Of String, Decimal)("PrimaVacaciones", IncentivePaymentVacationValue))
            descriptions.Add(New Tuple(Of String, Decimal)("IncrementoVacaciones", VacationalIncreaseValue))
            descriptions.Add(New Tuple(Of String, Decimal)("ValorVacacionesRTF", VacationValueRTF))
            descriptions.Add(New Tuple(Of String, Decimal)("AporteSaludVacaciones", VacationHealthRTF))
            descriptions.Add(New Tuple(Of String, Decimal)("AportePensionVacaciones", VacationPensionRTF))
            descriptions.Add(New Tuple(Of String, Decimal)("ValorVacacionesCompensadas", CompensationValueVacation))
            descriptions.Add(New Tuple(Of String, Decimal)("DiasVacacionesCompensadas", VacationDaysInCash))

        End If

        Return descriptions

    End Function

    Public Function AgreementsPaid(PayrollStarDate As Date, PayrollEndingDate As Date, ListAgreements As List(Of AgreementsC), ByRef PaidVacation As Byte, ByRef VacationDays As Integer, ByRef NumAgreement As Integer, ByRef AgreementsDId As Integer, ByRef ThirdPartyAgreementsName As String, ByRef ThirdPartyAgreementsId As Integer, ByRef AgreementsId As Integer) As Decimal Implements ILiquidationFunctions.AgreementsPaid

        Dim ConceptValue As Decimal

        For Each objAgreements As AgreementsC In ListAgreements
            NumAgreement = objAgreements.Consecutive
            AgreementsId = objAgreements.Id
            ThirdPartyAgreementsId = objAgreements.Company.ThirdPartyId
            ThirdPartyAgreementsName = objAgreements.Company.ThirdParty.Name
            ConceptValue = 0

            'If objAgreements.StartingDate >= PayrollStarDate And PaidVacation > 0 Then
            '    PaidVacation = 1
            '    VacationDays = 0
            'End If

            If objAgreements.LiquidationType = 1 And objAgreements.TermType = 1 Then ' Fijo - Fijo
                If objAgreements.CurrentBalance > 0 Then
                    ConceptValue = objAgreements.AgreementValue / objAgreements.NumberShares
                    If objAgreements.CurrentBalance < ConceptValue Then
                        ConceptValue = objAgreements.CurrentBalance
                    End If
                End If
            ElseIf objAgreements.LiquidationType = 1 And objAgreements.TermType = 2 Then ' Fijo - Variable
                ConceptValue = objAgreements.AgreementValue
            ElseIf objAgreements.LiquidationType = 2 And objAgreements.TermType = 1 Then ' Variable - Fijo
                If objAgreements.CurrentBalance > 0 Then
                    ConceptValue = objAgreements.AgreementValue / objAgreements.NumberShares
                    If objAgreements.CurrentBalance < ConceptValue Then
                        ConceptValue = objAgreements.CurrentBalance
                    End If
                End If
            ElseIf objAgreements.LiquidationType = 2 And objAgreements.TermType = 2 Then ' Variable - Variable
                ConceptValue = objAgreements.AgreementValue
            End If

            If objAgreements.AgreementsD.Any(Function(x) x.DatePayment >= PayrollStarDate And x.DatePayment <= PayrollEndingDate And x.TypePayment = 4) Then
                ConceptValue = 0
                AgreementsDId = objAgreements.AgreementsD.Where(Function(x) x.DatePayment >= PayrollStarDate And x.DatePayment <= PayrollEndingDate And x.TypePayment = 4).FirstOrDefault().Id
            End If

            If objAgreements.AgreementsD.Any(Function(x) x.DatePayment >= PayrollStarDate And x.DatePayment <= PayrollEndingDate And x.TypePayment = 3) Then
                ConceptValue = objAgreements.AgreementsD.Where(Function(x) x.DatePayment >= PayrollStarDate And x.DatePayment <= PayrollEndingDate And x.TypePayment = 3).FirstOrDefault().ShareValuePaid
                AgreementsDId = objAgreements.AgreementsD.Where(Function(x) x.DatePayment >= PayrollStarDate And x.DatePayment <= PayrollEndingDate And x.TypePayment = 3).FirstOrDefault().Id
            End If

            If objAgreements.PaidVacation IsNot Nothing AndAlso objAgreements.PaidVacation = True Then
                ConceptValue = 0
            End If
        Next

        Return ConceptValue

    End Function

    Public Function HourManualConcept(PayrollStarDate As Date, PayrollEndingDate As Date, ListManualConcept As List(Of ManualConcepts), ByRef ManualConceptPaidHourFlag As Boolean) As Decimal Implements ILiquidationFunctions.HourManualConcept

        Dim WorksHours As Decimal = 0

        For Each ManualConceptHour As ManualConcepts In ListManualConcept
            ManualConceptPaidHourFlag = True

            Dim ManualConceptListDetail = ManualConceptHour.ManualConceptsDetail
            Dim DetailConcept = ManualConceptListDetail.Where(Function(x) x.State = 1 And x.PayrollDateLiquidated = PayrollStarDate).Count()

            WorksHours = WorksHours + ManualConceptHour.QuoteValue

        Next

        Return WorksHours

    End Function

    Public Function ValueManualConcept(PayrollStarDate As Date, PayrollEndingDate As Date, ObjManualConcept As ManualConcepts, ByRef ManualConceptPaidFlag As Boolean, QuarterFlag As Byte) As Decimal Implements ILiquidationFunctions.ValueManualConcept

        Dim ManualConceptValue As Decimal = 0

        If ObjManualConcept.PaidEndContract = True Then
            ManualConceptPaidFlag = True
        Else

            Dim ManualConceptListDetail = ObjManualConcept.ManualConceptsDetail
            Dim DetailConcept = ManualConceptListDetail.Where(Function(x) x.State = 1 And x.PayrollDateLiquidated = PayrollStarDate).Count()

            If DetailConcept > 0 Then
                ManualConceptPaidFlag = True
            Else
                ManualConceptPaidFlag = False
            End If

        End If

        If ObjManualConcept.PaidFormat <> QuarterFlag Then
            ManualConceptPaidFlag = False
        End If

        If ObjManualConcept.PaidFormat = 3 Then
            ManualConceptPaidFlag = True
        End If

        If ManualConceptPaidFlag = True Then
            ManualConceptValue = ObjManualConcept.QuoteValue
        Else
            ManualConceptValue = 0
        End If

        Return ManualConceptValue
    End Function

    Public Function ValueAFCManualConcept(PayrollStarDate As Date, PayrollEndingDate As Date, ObjManualConcept As ManualConcepts, ByRef ManualConceptPaidFlag As Boolean) As Decimal Implements ILiquidationFunctions.ValueAFCManualConcept

        Dim ManualConceptValue As Decimal = 0

        If ObjManualConcept.PaidEndContract = True Then
            ManualConceptPaidFlag = True
        Else

            Dim ManualConceptListDetail = ObjManualConcept.ManualConceptsDetail
            Dim DetailConcept = ManualConceptListDetail.Where(Function(x) {1, 2}.Contains(x.State) And x.PayrollDateLiquidated = PayrollStarDate).Count()

            If DetailConcept > 0 Then
                ManualConceptPaidFlag = True
            Else
                ManualConceptPaidFlag = False
            End If

        End If

        If ManualConceptPaidFlag = True Then
            ManualConceptValue = ObjManualConcept.QuoteValue
        Else
            ManualConceptValue = 0
        End If

        Return ManualConceptValue
    End Function

    Public Function ValueForeclousure(PayrollStarDate As Date, PayrollEndingDate As Date, ListForeclousure As List(Of Foreclousure), ByRef PercentageForeclousure As Decimal) As Decimal Implements ILiquidationFunctions.ValueForeclousure

        Dim ForeclousureValue As Decimal = 0

        For Each ObjForeclousure As Foreclousure In ListForeclousure
            If ObjForeclousure.DiscountClass = 1 Or ObjForeclousure.DiscountClass = 4 Then
                PercentageForeclousure += ObjForeclousure.Percentage
            Else
                ForeclousureValue += ObjForeclousure.QuoteValue
            End If

            If ObjForeclousure.DiscountClass = 3 Then
                If ObjForeclousure.CurrentBalance < ForeclousureValue Then
                    ForeclousureValue = ObjForeclousure.CurrentBalance
                End If
            End If

            If ObjForeclousure.QuoteNumber > 0 And (ObjForeclousure.QuoteNumber <= ObjForeclousure.ForeclousureDetail.Count()) Then
                ForeclousureValue = 0
            End If
        Next

        Return ForeclousureValue

    End Function

    Public Function AnalisisNovelty(liquidationPayrollDomain As ILiquidationDomain, ContractEmployee As Entities.Contract, PayrollStarDate As Date, PayrollEndingDate As Date, ListNovelties As List(Of Novelty), PayrollDays As Integer, noveltyRepository As INoveltyRepository, ByRef VacationInitialModifiedDate As Date?, ByRef VacationEndModifiedDate As Date?, ByVal SessionValues As SessionValues, ByRef shouldExcludeAccruedDeducted As Boolean, Optional Day31 As Boolean = False) As List(Of Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))) Implements ILiquidationFunctions.AnalisisNovelty

        Dim descriptions As New List(Of Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)))

        'Ambulatoria
        Dim AmbulatoryInabilityDays As Integer = 0
        Dim AmbulatoryInabilityEmployer As Decimal = 0
        Dim AmbulatoryInabilityEPS As Decimal = 0
        Dim AmbulatoryInabilityAutorizationNumber As String
        Dim AmbulatoryInabilityEmployeerDays = 0
        Dim AmbulatoryInabilityERPDays = 0
        Dim ValueAmbulatoryEmployeerInability As Decimal = 0
        Dim ValueAmbulatoryERPInability As Decimal = 0

        'Hospitalaria
        Dim HospitalaryInabilityDays As Integer = 0
        Dim HospitalaryInabilityEmployer As Decimal = 0
        Dim HospitalaryInabilityEPS As Decimal = 0
        Dim HospitalaryInabilityAutorizationNumber As String
        Dim HospitalInabilityEmployeerDays = 0
        Dim HospitalInabilityERPDays = 0
        Dim ValueHospitalEmployeerInability As Decimal = 0
        Dim ValueHospitalERPInability As Decimal = 0

        'Maternidad
        Dim MaternityInabilityDays As Integer = 0
        Dim MaternityInabilityEmployer As Decimal = 0
        Dim MaternityInabilityEPS As Decimal = 0
        Dim MaternityInabilityAutorizationNumber As String

        'Paternidad
        Dim PaternityInabilityDays As Integer = 0
        Dim PaternityInabilityEmployer As Decimal = 0
        Dim PaternityInabilityEPS As Decimal = 0
        Dim PaternityInabilityAutorizationNumber As String

        'Sanciones
        Dim SanctionDays As Integer = 0
        Dim SanctionEPSDays As Integer = 0
        Dim SanctionEmployerDays As Integer = 0

        'Licencias No Remuneradas
        Dim LicencesesNotRemuneratedDays As Integer = 0
        Dim LicensesNotRemuneratedEPSDays As Integer = 0
        Dim LicensesNotRemuneratedEmployerDays As Integer = 0
        Dim LicensesNotRemuneratedAutorizationNumber As String

        'Licencias Remuneradas
        Dim LicencesesRemuneratedDays As Integer = 0
        Dim LicencesesRemuneratedEmployer As Decimal = 0
        Dim LicencesesRemuneratedEPS As Decimal = 0
        Dim LicencesesRemuneratedEPSDays As Integer = 0
        Dim LicencesesRemuneratedEmployerDays As Integer = 0

        'Licencias Remuneradas
        Dim PermissionDays As Integer = 0
        Dim PermissionEmployer As Decimal = 0
        Dim PermissionEPS As Decimal = 0
        Dim PermissionEPSDays As Integer = 0
        Dim PermissionEmployerDays As Integer = 0
        Dim FamilyDay As Short = 0

        'Calamidad Doméstica
        Dim CalamidadDomesticaDays As Integer = 0
        Dim CalamidadDomesticaEPSDays As Integer = 0
        Dim CalamidadDomesticaEmployerDays As Integer = 0

        'Luto
        Dim LutoDays As Integer = 0
        Dim LutoEmployer As Decimal = 0
        Dim LutoEPS As Decimal = 0
        Dim LutoEPSDays As Integer = 0
        Dim LutoEmployerDays As Integer = 0

        'ARP
        Dim ARLInabilityDays As Integer = 0
        Dim ARLInabilityEmployer As Decimal = 0
        Dim ARLInabilityEPS As Decimal = 0
        Dim ARLInabilityAutorizationNumber As String

        Dim EmployerProfessionalDisabilityDays As Integer = 0    ''Dias patrono incapacidad riesgo profesional patrono
        Dim EmployerProfessionalDisabilityAmount As Decimal = 0   ''Valor incapacidad riesgo profesional patrono
        Dim ERPProfessionalDisabilityDays As Integer = 0          ''Dias  incapacidad riesgo profesional erp
        Dim ERPProfessionalDisabilityAmount As Decimal = 0     ''Valor patrono riesgo profesional erp


        Dim TotalInabilties As Integer = 0

        Dim ListNoveltyPast As New List(Of Novelty)

        Dim previousNovelty As Novelty = Nothing

        Dim PermanentInabity As Byte

        Dim extension As Byte = 0

        Dim totalDaysExtension As Integer = 0

        For Each ObjNovelty As Novelty In ListNovelties

            Dim TotalInabilitiesDays As Integer = 0
            extension = ObjNovelty.Extension

            If (ContractEmployee.ContractEndingDate < ObjNovelty.RealDate) OrElse (ObjNovelty.EndDate < ContractEmployee.ContractInitialDate AndAlso ContractEmployee.GroupId <> ObjNovelty.GroupId) OrElse (ObjNovelty.EndDate > ContractEmployee.ContractEndingDate AndAlso ContractEmployee.GroupId = ObjNovelty.GroupId AndAlso ContractEmployee.Valid = 1) Then
                TotalInabilitiesDays = 0
                Return descriptions
            End If

            VacationEndModifiedDate = ObjNovelty.VacationEndDateNovelty
            VacationInitialModifiedDate = ObjNovelty.VacationInitialDateNovelty

            If ObjNovelty.Extension = 1 Then
                ListNoveltyPast = noveltyRepository.GetListNoveltyByConsecutive(ObjNovelty.Consecutive)

                If ListNoveltyPast IsNot Nothing AndAlso ListNoveltyPast.Count > 0 Then
                    If ObjNovelty.Status = 2 Then
                        TotalInabilitiesDays = ListNoveltyPast.Sum(Function(x) x.Days And x.Status = 1)
                    Else
                        TotalInabilitiesDays = ListNoveltyPast.Where(Function(x) x.Id <> ObjNovelty.Id And x.Status = 1).Sum(Function(x) x.Days)

                        If TotalInabilitiesDays < 180 And ObjNovelty.RealDate < PayrollStarDate Then
                            TotalInabilitiesDays = TotalInabilitiesDays + (DateDiff(DateInterval.Day, ObjNovelty.RealDate, DateAdd(DateInterval.Day, -1, PayrollStarDate)) + 1)
                        End If
                    End If
                    totalDaysExtension = ListNoveltyPast.Sum(Function(x) x.Days)
                End If

            End If


            If ObjNovelty.TypeNovelty = 1 And (ObjNovelty.InabilityClass = 1 Or ObjNovelty.InabilityClass = 6) Then
                'Ambulatoria
                Dim AmbulatoryInabilityEPSDays As Integer = 0
                Dim AmbulatoryInabilityEmployerDays As Integer = 0
                Dim PaidEmployerValueAmbu As Decimal = 0

                GetSpendingNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, AmbulatoryInabilityEmployer, AmbulatoryInabilityEmployerDays, TotalInabilties, PayrollDays, SessionValues, Day31)
                GetInabilityCollectNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, AmbulatoryInabilityEPS, AmbulatoryInabilityEPSDays, TotalInabilties + AmbulatoryInabilityEmployerDays, PayrollDays, TotalInabilitiesDays, PermanentInabity, SessionValues, Day31)

                AmbulatoryInabilityDays = AmbulatoryInabilityEPSDays + AmbulatoryInabilityEmployerDays
                AmbulatoryInabilityEmployeerDays = AmbulatoryInabilityEmployerDays
                AmbulatoryInabilityERPDays = AmbulatoryInabilityEPSDays
                AmbulatoryInabilityAutorizationNumber = ObjNovelty.AutorizationNumber

                If AmbulatoryInabilityEmployerDays > 0 AndAlso ObjNovelty.PaidEmployerValue > 0 AndAlso ObjNovelty.EmployerDays > 0 Then
                    Dim valueDay As Decimal = ObjNovelty.PaidEmployerValue / ObjNovelty.EmployerDays
                    PaidEmployerValueAmbu = valueDay * AmbulatoryInabilityEmployerDays
                ElseIf ObjNovelty.PaidEmployerValue IsNot Nothing AndAlso ObjNovelty.IsCycleDate = False Then
                    PaidEmployerValueAmbu = ObjNovelty.PaidEmployerValue
                End If

                If previousNovelty IsNot Nothing Then
                    ' Si hay una novedad previa parcialmente pagada, no duplicar el valor
                    If previousNovelty.Status = 2 AndAlso AmbulatoryInabilityEmployerDays = 0 Then
                        PaidEmployerValueAmbu = 0
                    End If
                    If extension = 1 AndAlso previousNovelty.Consecutive = ObjNovelty.Consecutive Then
                        Dim idx = descriptions.FindIndex(Function(x) x.Rest.Item2 <> 0 AndAlso x.Rest.Item6 = 2)
                        If idx >= 0 Then
                            Dim old = descriptions(idx)
                            Dim updated As New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String,
                              Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))(
                                old.Item1,
                                old.Item2,
                                old.Item3,
                                old.Item4,
                                old.Item5,
                                old.Item6,
                                old.Item7,
                                New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(old.Rest.Item1, 0, old.Rest.Item3, old.Rest.Item4, old.Rest.Item5, old.Rest.Item6)
                            )
                            descriptions(idx) = updated
                        End If
                    End If
                End If
                Dim EPSRecognizeValueAmbu = AmbulatoryInabilityEPS

                TotalInabilties = TotalInabilties + AmbulatoryInabilityDays

                'Calcular el 50% del salario base del empleado cuando el total de días supera 90
                If totalDaysExtension > 90 Then
                    Dim baseSalary As Decimal = ObjNovelty.EmployeeBaseSalary
                    Dim fiftyPercentSalary As Decimal = baseSalary * 0.5

                    ' Calcular el valor proporcional basado solo en los días de extensión del periodo actual
                    Dim noveltiesInCurrentPeriod = ListNovelties.Where(Function(x) x.Extension = 1 And x.Consecutive = ObjNovelty.Consecutive)
                    Dim totalDaysInExtensionCurrentPeriod As Integer = noveltiesInCurrentPeriod.Sum(Function(x) x.Days)

                    If totalDaysInExtensionCurrentPeriod > 0 Then
                        AmbulatoryInabilityEPS = Math.Round(fiftyPercentSalary * ObjNovelty.Days / totalDaysInExtensionCurrentPeriod, 3)
                    Else
                        AmbulatoryInabilityEPS = fiftyPercentSalary
                    End If
                End If

                ' Verificar si se debe excluir el registro 
                ' Calcular PayrollEndDate basado en el período
                Dim PayrollEndDate As Date
                If PayrollStarDate.Day = 1 Then
                    ' Primera quincena - termina el día 15
                    PayrollEndDate = New Date(PayrollStarDate.Year, PayrollStarDate.Month, 15)
                Else
                    ' Segunda quincena - termina el último día del mes  
                    PayrollEndDate = New Date(PayrollStarDate.Year, PayrollStarDate.Month, DateTime.DaysInMonth(PayrollStarDate.Year, PayrollStarDate.Month))
                End If

                ' Validar impacto de incapacidad usando función helper
                Dim validationResult As IncapacityValidationResult = ValidateIncapacityImpactCR(
                    ObjNovelty,
                    PayrollStarDate,
                    PayrollEndDate,
                    _cultureCR,
                    SessionValues.LanguageCulture
                )

                ' Aplicar resultados de la validación
                Dim shouldExcludeDescription As Boolean = validationResult.ShouldExcludeDescription
                shouldExcludeAccruedDeducted = validationResult.ShouldExcludeAccruedDeducted
                Dim proportionalPayment As Double = validationResult.ProportionalPayment

                If AmbulatoryInabilityDays > 0 AndAlso Not shouldExcludeDescription Then
                    descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))("Ambulatoria", AmbulatoryInabilityDays, AmbulatoryInabilityEmployer, AmbulatoryInabilityEPS, ObjNovelty.RealDate, ObjNovelty.EndDate, AmbulatoryInabilityAutorizationNumber, New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(PermanentInabity, PaidEmployerValueAmbu, EPSRecognizeValueAmbu, AmbulatoryInabilityEmployerDays, AmbulatoryInabilityERPDays, ObjNovelty.Status)))
                End If

            End If

            If ObjNovelty.TypeNovelty = 1 And ObjNovelty.InabilityClass = 2 Then
                'Hospitalaria

                Dim HospitalaryInabilityEPSDays As Integer = 0
                Dim HospitalaryInabilityEmployerDays As Integer = 0
                'Valor a pagar de la novedad en caso que no esté dentro del nuevo ciclo de 30 días
                Dim PaidEmployerValueHosp = CType(IIf(ObjNovelty.PaidEmployerValue IsNot Nothing AndAlso ObjNovelty.IsCycleDate = False,
                                                      ObjNovelty.PaidEmployerValue, 0), Decimal)
                Dim EPSRecognizeValueHosp = CType(IIf(ObjNovelty.EPSRecognizeValue IsNot Nothing, ObjNovelty.EPSRecognizeValue, 0), Decimal)

                GetInabilityCollectNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, HospitalaryInabilityEPS, HospitalaryInabilityEPSDays, TotalInabilties, PayrollDays, TotalInabilitiesDays, PermanentInabity, SessionValues, Day31)
                GetSpendingNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, HospitalaryInabilityEmployer, HospitalaryInabilityEmployerDays, TotalInabilties, PayrollDays, SessionValues, Day31)

                HospitalaryInabilityDays = HospitalaryInabilityEPSDays + HospitalaryInabilityEmployerDays
                HospitalaryInabilityEmployerDays = HospitalaryInabilityEmployerDays
                HospitalInabilityERPDays = HospitalaryInabilityEPSDays
                HospitalaryInabilityAutorizationNumber = ObjNovelty.AutorizationNumber

                TotalInabilties = TotalInabilties + HospitalaryInabilityDays

                If HospitalaryInabilityDays > 0 Then
                    descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))("Hospitalaria", HospitalaryInabilityDays, HospitalaryInabilityEmployer, HospitalaryInabilityEPS, ObjNovelty.RealDate, ObjNovelty.EndDate, HospitalaryInabilityAutorizationNumber, New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(PermanentInabity, PaidEmployerValueHosp, EPSRecognizeValueHosp, HospitalaryInabilityEmployerDays, HospitalInabilityERPDays, ObjNovelty.Status)))
                End If
            End If

            If ObjNovelty.TypeNovelty = 1 And ObjNovelty.InabilityClass = 3 Then
                'Maternidad

                Dim MaternityInabilityEPSDays As Integer = 0
                Dim MaternityInabilityEmployerDays As Integer = 0

                GetInabilityCollectNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, MaternityInabilityEPS, MaternityInabilityEPSDays, TotalInabilties, PayrollDays, TotalInabilitiesDays, PermanentInabity, SessionValues, Day31)
                GetSpendingNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, MaternityInabilityEmployer, MaternityInabilityEmployerDays, TotalInabilties, PayrollDays, SessionValues, Day31)

                MaternityInabilityDays = MaternityInabilityEPSDays + MaternityInabilityEmployerDays
                MaternityInabilityAutorizationNumber = ObjNovelty.AutorizationNumber

                TotalInabilties = TotalInabilties + MaternityInabilityDays

                If MaternityInabilityDays > 0 Then
                    descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))("Maternidad", MaternityInabilityDays, MaternityInabilityEmployer, MaternityInabilityEPS, ObjNovelty.RealDate, ObjNovelty.EndDate, MaternityInabilityAutorizationNumber, New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(0, 0, 0, 0, 0, ObjNovelty.Status)))
                End If
            End If

            If ObjNovelty.TypeNovelty = 1 And ObjNovelty.InabilityClass = 5 Then
                'Paternidad

                Dim PaternityInabilityEPSDays As Integer = 0
                Dim PaternityInabilityEmployerDays As Integer = 0

                GetInabilityCollectNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, PaternityInabilityEPS, PaternityInabilityEPSDays, TotalInabilties, PayrollDays, TotalInabilitiesDays, PermanentInabity, SessionValues, Day31)
                GetSpendingNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, PaternityInabilityEmployer, PaternityInabilityEmployerDays, TotalInabilties, PayrollDays, SessionValues, Day31)

                PaternityInabilityDays = PaternityInabilityEPSDays + PaternityInabilityEmployerDays
                PaternityInabilityAutorizationNumber = ObjNovelty.AutorizationNumber

                TotalInabilties = TotalInabilties + PaternityInabilityDays

                If PaternityInabilityDays > 0 Then
                    descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))("Paternidad", PaternityInabilityDays, PaternityInabilityEmployer, PaternityInabilityEPS, ObjNovelty.RealDate, ObjNovelty.EndDate, PaternityInabilityAutorizationNumber, New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(0, 0, 0, 0, 0, ObjNovelty.Status)))
                End If
            End If

            If ObjNovelty.TypeNovelty = 2 Then
                'Sanción

                GetInabilityCollectNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, 0, SanctionEPSDays, TotalInabilties, PayrollDays, TotalInabilitiesDays, PermanentInabity, SessionValues, Day31)
                GetSpendingNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, 0, SanctionEmployerDays, TotalInabilties, PayrollDays, SessionValues, Day31)

                SanctionDays = SanctionEPSDays + SanctionEmployerDays

                TotalInabilties = TotalInabilties + SanctionDays

                If SanctionDays > 0 Then
                    descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))("Sancion", SanctionDays, 0, 0, ObjNovelty.RealDate, ObjNovelty.EndDate, String.Empty, New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(0, 0, 0, 0, 0, ObjNovelty.Status)))
                End If
            End If

            If ObjNovelty.TypeNovelty = 3 And ObjNovelty.LicenseClass = eNoveltyLicenseClass.NoRemunerada Then
                'Licencias No Remuneradas

                GetInabilityCollectNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, 0, LicensesNotRemuneratedEPSDays, TotalInabilties, PayrollDays, TotalInabilitiesDays, PermanentInabity, SessionValues, Day31)
                GetSpendingNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, 0, LicensesNotRemuneratedEmployerDays, TotalInabilties, PayrollDays, SessionValues, Day31)

                LicencesesNotRemuneratedDays = LicensesNotRemuneratedEPSDays + LicensesNotRemuneratedEmployerDays

                TotalInabilties = TotalInabilties + LicencesesNotRemuneratedDays

                If LicencesesNotRemuneratedDays > 0 Then
                    descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))("LicenciasNoRemuneradas", LicencesesNotRemuneratedDays, 0, 0, ObjNovelty.RealDate, ObjNovelty.EndDate, LicensesNotRemuneratedAutorizationNumber, New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(0, 0, 0, 0, 0, ObjNovelty.Status)))
                End If
            End If

            If ObjNovelty.TypeNovelty = 3 And ObjNovelty.LicenseClass = eNoveltyLicenseClass.Remunerada Then
                'Licencias Remuneradas

                GetInabilityCollectNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, LicencesesRemuneratedEPS, LicencesesRemuneratedEPSDays, TotalInabilties, PayrollDays, TotalInabilitiesDays, PermanentInabity, SessionValues, Day31)
                GetSpendingNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, LicencesesRemuneratedEmployer, LicencesesRemuneratedEmployerDays, TotalInabilties, PayrollDays, SessionValues, Day31)

                LicencesesRemuneratedDays = LicencesesRemuneratedEPSDays + LicencesesRemuneratedEmployerDays

                TotalInabilties = TotalInabilties + LicencesesRemuneratedDays

                If LicencesesRemuneratedDays > 0 Then
                    descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))("LicenciasRemuneradas", LicencesesRemuneratedDays, LicencesesRemuneratedEmployer, LicencesesRemuneratedEPS, ObjNovelty.RealDate, ObjNovelty.EndDate, String.Empty, New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(0, 0, 0, 0, 0, ObjNovelty.Status)))
                End If
            End If

            If ObjNovelty.TypeNovelty = 3 And ObjNovelty.LicenseClass = eNoveltyLicenseClass.DiaFamilia Then
                'Dia de la familia
                FamilyDay = 1

                TotalInabilties = TotalInabilties + FamilyDay

                If FamilyDay > 0 Then
                    descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))("DiaFamilia", FamilyDay, LicencesesRemuneratedEmployer, LicencesesRemuneratedEPS, ObjNovelty.RealDate, ObjNovelty.EndDate, String.Empty, New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(0, 0, 0, 0, 0, ObjNovelty.Status)))
                End If
            End If

            If ObjNovelty.TypeNovelty = 3 And ObjNovelty.LicenseClass = eNoveltyLicenseClass.Permiso Then
                'Permisos

                GetInabilityCollectNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, PermissionEPS, PermissionEPSDays, TotalInabilties, PayrollDays, TotalInabilitiesDays, PermanentInabity, SessionValues, Day31)
                GetSpendingNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, PermissionEmployer, PermissionEmployerDays, TotalInabilties, PayrollDays, SessionValues, Day31)

                PermissionDays = PermissionEPSDays + PermissionEmployerDays + ObjNovelty.Days.AsInt

                TotalInabilties = TotalInabilties + PermissionDays

                If PermissionDays > 0 Then
                    descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))("Permisos", PermissionDays, PermissionEmployer, PermissionEPS, ObjNovelty.RealDate, ObjNovelty.EndDate, String.Empty, New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(0, 0, 0, 0, 0, ObjNovelty.Status)))
                End If
            End If

            If ObjNovelty.TypeNovelty = 3 And ObjNovelty.LicenseClass = eNoveltyLicenseClass.LicenciaLuto Then
                'Luto

                GetInabilityCollectNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, LutoEPS, LutoEPSDays, TotalInabilties, PayrollDays, TotalInabilitiesDays, PermanentInabity, SessionValues, Day31)
                GetSpendingNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, LutoEmployer, LutoEmployerDays, TotalInabilties, PayrollDays, SessionValues, Day31)

                LutoDays = LutoEPSDays + LutoEmployerDays

                If LutoDays > 0 Then
                    descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))("Luto", LutoDays, LutoEmployer, LutoEPS, ObjNovelty.RealDate, ObjNovelty.EndDate, String.Empty, New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(0, 0, 0, 0, 0, ObjNovelty.Status)))
                End If
            End If

            If ObjNovelty.TypeNovelty = 1 And ObjNovelty.InabilityClass = 4 Then
                'ENFERMEDAD PROFESIONAL

                Dim ARLInabilityEPSDays As Integer = 0
                Dim ARLInabilityEmployerDays As Integer = 0

                GetInabilityCollectNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, ARLInabilityEPS, ARLInabilityEPSDays, TotalInabilties, PayrollDays, 0, PermanentInabity, SessionValues, Day31)
                GetSpendingNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, ARLInabilityEmployer, ARLInabilityEmployerDays, TotalInabilties, PayrollDays, SessionValues, Day31)

                ARLInabilityDays = ARLInabilityEPSDays + ARLInabilityEmployerDays

                ''patrono - eps
                EmployerProfessionalDisabilityDays = ARLInabilityEmployerDays
                ERPProfessionalDisabilityDays = ARLInabilityEPSDays
                EmployerProfessionalDisabilityAmount = ARLInabilityEmployer ''CType(IIf(ObjNovelty.PaidEmployerValue IsNot Nothing AndAlso ObjNovelty.IsCycleDate = False, ObjNovelty.PaidEmployerValue, 0), Decimal)
                ERPProfessionalDisabilityAmount = ARLInabilityEPS

                ARLInabilityAutorizationNumber = ObjNovelty.AutorizationNumber

                TotalInabilties = TotalInabilties + ARLInabilityDays

                If ARLInabilityDays > 0 Then
                    descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))("Profesional", ARLInabilityDays, ARLInabilityEmployer, ARLInabilityEPS, ObjNovelty.RealDate, ObjNovelty.EndDate, ARLInabilityAutorizationNumber, New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(PermanentInabity, EmployerProfessionalDisabilityAmount, ERPProfessionalDisabilityAmount, EmployerProfessionalDisabilityDays, ERPProfessionalDisabilityDays, ObjNovelty.Status)))
                End If
            End If

            If ObjNovelty.TypeNovelty = 3 And ObjNovelty.LicenseClass = eNoveltyLicenseClass.CalamidadDomestica Then
                'CALAMIDAD DOMÉSTICA

                GetInabilityCollectNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, PermissionEPS, CalamidadDomesticaEPSDays, TotalInabilties, PayrollDays, TotalInabilitiesDays, PermanentInabity, SessionValues, Day31)
                GetSpendingNovelty(liquidationPayrollDomain, ObjNovelty, PayrollEndingDate, PayrollStarDate, PermissionEmployer, CalamidadDomesticaEmployerDays, TotalInabilties, PayrollDays, SessionValues, Day31)

                CalamidadDomesticaDays = ObjNovelty.Days

                TotalInabilties = TotalInabilties + CalamidadDomesticaDays

                If CalamidadDomesticaDays > 0 Then
                    descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))("Calamidad", CalamidadDomesticaDays, PermissionEmployer, PermissionEPS, ObjNovelty.RealDate, ObjNovelty.EndDate, String.Empty, New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(0, 0, 0, 0, 0, ObjNovelty.Status)))
                End If
            End If
            previousNovelty = ObjNovelty
        Next

        ''Logica calculo salario proporcional
        If SessionValues.LanguageCulture = _cultureCR AndAlso Not shouldExcludeAccruedDeducted Then
            ' Calcular días del período total
            Dim totalPayrollDays As Integer = (PayrollEndingDate - PayrollStarDate).Days + 1
            If PayrollEndingDate.Day = 31 AndAlso PayrollStarDate.Day > 0 Then
                totalPayrollDays = totalPayrollDays - 1
            End If
            
            ' Calcular días de incapacidades que intersectan con el período de liquidación
            Dim totalIncapacityDaysInPeriod As Integer = 0
            For Each novelty As Novelty In ListNovelties
                If novelty.CalculationType = 8 Then ' Solo incapacidades generales
                    ' Calcular intersección entre incapacidad y período de liquidación
                    Dim intersectionDays As Integer = CalculateIntersectionDays(novelty.RealDate, novelty.EndDate, PayrollStarDate, PayrollEndingDate, Day31)
                    totalIncapacityDaysInPeriod += intersectionDays
                End If
            Next
            
            ' Calcular días trabajados (período total menos días de incapacidad que intersectan)
            Dim workedDays As Integer = totalPayrollDays - totalIncapacityDaysInPeriod
            ' Si hay días trabajados, agregar salario proporcional
            If workedDays > 0 Then
                ' Obtener salario base del empleado
                Dim baseSalary As Decimal = ContractEmployee.BasicSalary
                Dim dailySalary As Decimal = baseSalary / 30
                Dim proportionalSalary As Decimal = dailySalary * workedDays
                ' Agregar concepto de salario proporcional a la liquidación
                descriptions.Add(New Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte))(
                    "SalarioProporcional",
                    workedDays,
                    proportionalSalary,
                    0,
                    PayrollStarDate,
                    PayrollEndingDate,
                    "Días trabajados en período con incapacidad",
                    New Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)(0, proportionalSalary, 0, workedDays, 0, 0)
                ))

            End If
        End If
        Return descriptions

    End Function


    ''' <summary>
    ''' Valida el impacto de una incapacidad en la liquidación para Costa Rica
    ''' Maneja liquidaciones proporcionales considerando fechas de inicio y fin
    ''' </summary>
    ''' <param name="novelty">Objeto de la novedad (incapacidad)</param>
    ''' <param name="payrollStartDate">Fecha inicio del período de nómina</param>
    ''' <param name="payrollEndDate">Fecha fin del período de nómina</param>
    ''' <param name="cultureCR">Cultura de Costa Rica</param>
    ''' <param name="currentCulture">Cultura actual del sistema</param>
    ''' <returns>Resultado de la validación con flags y factor proporcional</returns>
    Private Function ValidateIncapacityImpactCR(novelty As Novelty,
                                               payrollStartDate As Date,
                                               payrollEndDate As Date,
                                               cultureCR As String,
                                               currentCulture As String) As IncapacityValidationResult

        Dim result As New IncapacityValidationResult()

        ' Valores por defecto (liquidar normal)
        result.ShouldExcludeAccruedDeducted = False
        result.ShouldExcludeDescription = False

        ' Obtener fechas de la incapacidad
        Dim incapacityStartDate As Date = novelty.RealDate
        Dim incapacityEndDate As Date = novelty.EndDate

        ' Validar que es Costa Rica y que es Incapacidad General
        If currentCulture <> cultureCR OrElse novelty.CalculationType <> 8 Then
            Return result ' Liquidar normal
        End If

        ' Validar estado de liquidación de la novedad
        If novelty.Status = 1 Then
            result.ShouldExcludeAccruedDeducted = True
            result.ShouldExcludeDescription = True
            Return result ' Bloquea Status = 1 
        Else
            If novelty.Status = 2 And incapacityEndDate > payrollEndDate Then
                result.ShouldExcludeAccruedDeducted = True
                result.ShouldExcludeDescription = True
                Return result ' Bloquea Status = 2
            End If
        End If
        ' Estado 0 (Normal) y Estado 2 (Parcialmente liquidada) - Continuar evaluación



        ' Verificar superposición entre período de nómina e incapacidad
        Dim hasOverlap As Boolean = Not (payrollEndDate < incapacityStartDate OrElse
                                        payrollStartDate > incapacityEndDate)
        If Not hasOverlap Then
            Return result ' Sin superposición - liquidar normal
        End If
        'determinar si liquidar o no (sin calcular proporciones)
        ' CASO 1: Superposición parcial al inicio - hay días trabajados antes de incapacidad
        If payrollStartDate < incapacityStartDate AndAlso payrollEndDate >= incapacityStartDate Then
            Dim daysBeforeIncapacity As Integer = (incapacityStartDate - payrollStartDate).Days
            If payrollEndDate.Day = 31 AndAlso daysBeforeIncapacity > 0 Then
                daysBeforeIncapacity = daysBeforeIncapacity - 1
            End If
            If daysBeforeIncapacity > 0 Then
                ' Hay días trabajados antes de la incapacidad - SÍ LIQUIDAR
                result.ShouldExcludeAccruedDeducted = False
                ' Solo mostrar concepto si es primera vez procesando (Status=0)
                If novelty.Status = 0 Then
                    result.ShouldExcludeDescription = False  ' SÍ mostrar concepto "Ambulatoria" 
                Else
                    result.ShouldExcludeDescription = True   ' NO mostrar concepto (ya procesada)
                End If
            Else
                ' No hay días trabajados - PERMITIR liquidación (subsidio de incapacidad)
                result.ShouldExcludeAccruedDeducted = False

                ' Solo mostrar concepto si es primera vez (Status=0) Y incapacidad inició antes del período  
                If novelty.Status = 0 AndAlso incapacityStartDate < payrollStartDate Then
                    result.ShouldExcludeDescription = False  ' SÍ mostrar concepto "Ambulatoria"
                Else
                    result.ShouldExcludeDescription = True   ' NO mostrar concepto
                End If
            End If
            ' CASO 2: Superposición parcial al final - hay días trabajados después de incapacidad
        ElseIf payrollStartDate <= incapacityEndDate AndAlso payrollEndDate > incapacityEndDate Then
            Dim daysAfterIncapacity As Integer = (payrollEndDate - incapacityEndDate).Days
            If payrollEndDate.Day = 31 AndAlso daysAfterIncapacity > 0 Then
                daysAfterIncapacity = daysAfterIncapacity - 1
            End If
            If daysAfterIncapacity > 0 Then
                ' Hay días trabajados después de la incapacidad - SÍ LIQUIDAR
                result.ShouldExcludeAccruedDeducted = False
                ' Solo mostrar concepto si es primera vez (Status=0) Y incapacidad inició antes del período
                If novelty.Status = 0 AndAlso incapacityStartDate < payrollStartDate Then
                    result.ShouldExcludeDescription = False  ' SÍ mostrar concepto "Ambulatoria"
                Else
                    result.ShouldExcludeDescription = True   ' NO mostrar concepto
                End If
            Else
                ' No hay días trabajados - PERMITIR liquidación (subsidio de incapacidad)
                result.ShouldExcludeAccruedDeducted = False
                ' Solo mostrar concepto si es primera vez (Status=0) Y incapacidad inició antes del período  
                If novelty.Status = 0 AndAlso incapacityStartDate < payrollStartDate Then
                    result.ShouldExcludeDescription = False  ' SÍ mostrar concepto "Ambulatoria"
                Else
                    result.ShouldExcludeDescription = True   ' NO mostrar concepto
                End If
            End If

        Else
            ' CASO 3: Superposición total - todo el período está en incapacidad  
            result.ShouldExcludeAccruedDeducted = False  ' PERMITIR liquidación (subsidio de incapacidad)
            ' Solo mostrar concepto si es primera vez procesando (Status=0) 
            If novelty.Status = 0 Then
                result.ShouldExcludeDescription = False  ' SÍ mostrar concepto "Ambulatoria"
            Else
                result.ShouldExcludeDescription = True   ' NO mostrar concepto (ya procesada)
            End If
        End If
        Return result
    End Function

    Public Sub GetInabilityCollectNovelty(LiquidationDomain As LiquidationDomain, ObjInability As Novelty, PayrollEndDate As Date, PayrollStarDate As Date, ByRef InabilityCollect As Decimal, ByRef DaysCalculation As Integer, ByVal DaysInabilitiesTotal As Integer, ByVal PayrollDays As Integer, TotalInabilitiesDays As Integer, ByRef PermanentInability As Byte, SessionValues As SessionValues, Optional Day31 As Boolean = False)

        Dim EmployerDays As Integer = IIf(ObjInability.EmployerDays IsNot Nothing, ObjInability.EmployerDays, 0)
        Dim EPSDays As Integer = IIf(ObjInability.EPSDays IsNot Nothing, ObjInability.EPSDays, 0)
        Dim EndDatePaidEmployer As Date = DateAdd(DateInterval.Day, EmployerDays - 1, ObjInability.RealDate)
        Dim InitialDateEPSPaid As Date = DateAdd(DateInterval.Day, 1, EndDatePaidEmployer)

        'Cargamos el lenguaje de la cultura que se está utilizando
        Dim actualCulture = SessionValues.LanguageCulture

        If EPSDays > 0 Then

            ''Incapacidades inician en un mes anterior y finalizan en el periodo
            If ObjInability.RealDate < PayrollStarDate And ObjInability.EndDate <= PayrollEndDate And ObjInability.Status = 2 Then
                If PayrollStarDate > InitialDateEPSPaid Then
                    If PayrollEndDate.Month = 2 Then
                        DaysCalculation = DateDiff(DateInterval.Day, PayrollStarDate, ObjInability.EndDate) + 1
                    Else
                        ' Usar Day31 para determinar si incluir día 31 en cálculos
                        If Day31 Then
                            DaysCalculation = DateDiff(DateInterval.Day, PayrollStarDate, ObjInability.EndDate) + 1
                        Else
                            DaysCalculation = LiquidationDomain.Days360(PayrollStarDate, ObjInability.EndDate)
                        End If
                    End If

                    If ObjInability.EndDate.Day = 31 Then
                        DaysCalculation = DateDiff(DateInterval.Day, PayrollStarDate, ObjInability.EndDate) + 1
                    End If

                Else
                    If PayrollEndDate.Month = 2 Then
                        DaysCalculation = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                    Else
                        ' Usar Day31 para determinar si incluir día 31 en cálculos
                        If Day31 Then
                            DaysCalculation = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                        Else
                            DaysCalculation = LiquidationDomain.Days360(InitialDateEPSPaid, ObjInability.EndDate)
                        End If
                    End If

                    If ObjInability.EndDate.Day = 31 Then
                        DaysCalculation = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                    End If

                End If
            End If

            '' Incapacidades inician este mes y finalizan en este
            If ObjInability.RealDate >= PayrollStarDate And ObjInability.EndDate <= PayrollEndDate Then
                If PayrollEndDate.Month = 2 Then
                    DaysCalculation = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                Else
                    ' Usar Day31 para determinar si incluir día 31 en cálculos
                    If Day31 Then
                        DaysCalculation = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                    Else
                        DaysCalculation = LiquidationDomain.Days360(InitialDateEPSPaid, ObjInability.EndDate)
                    End If
                End If

                If ObjInability.EndDate.Day = 31 Then
                    DaysCalculation = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                End If

            End If

            '' Incapacidades que inicia este mes y finaliza el siguiente
            If ObjInability.RealDate >= PayrollStarDate And ObjInability.EndDate > PayrollEndDate Then
                If PayrollEndDate > EndDatePaidEmployer Then
                    ' Usar Day31 para determinar si incluir día 31 en cálculos
                    If Day31 Then
                        DaysCalculation = DateDiff(DateInterval.Day, InitialDateEPSPaid, PayrollEndDate) + 1
                    Else
                        DaysCalculation = LiquidationDomain.Days360(InitialDateEPSPaid, PayrollEndDate)
                    End If
                Else
                    DaysCalculation = 0
                End If
            End If

            '' Incapacidades que Iniciaron un Periodo Antes, y finalizan un periodo después
            If ObjInability.RealDate < PayrollStarDate And ObjInability.EndDate > PayrollEndDate Then
                ' Usar Day31 para determinar si incluir día 31 en cálculos
                If Day31 Then
                    DaysCalculation = DateDiff(DateInterval.Day, PayrollStarDate, PayrollEndDate) + 1
                Else
                    DaysCalculation = LiquidationDomain.Days360(PayrollStarDate, PayrollEndDate)
                End If
            End If

            '' Incapacidades a Futuro
            If PayrollEndDate < ObjInability.RealDate Then
                DaysCalculation = 0
            End If

            'Analizo las incapacidades de meses pasados
            If ObjInability.RealDate < PayrollStarDate And ObjInability.Status = 0 Then
                Dim diferenciaDias As Integer = DateDiff(DateInterval.Day, ObjInability.RealDate, PayrollStarDate)
                If diferenciaDias > 15 Then
                    ' Usar Day31 para determinar si incluir día 31 en cálculos
                    If Day31 Then
                        DaysCalculation = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                    Else
                        DaysCalculation = LiquidationDomain.Days360(InitialDateEPSPaid, ObjInability.EndDate)
                    End If
                End If

                If diferenciaDias <= 15 Then
                    DaysCalculation = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                End If

                If DaysCalculation > 30 Then
                    DaysCalculation = 30
                End If

                If DaysCalculation < 0 Then
                    DaysCalculation = 0
                End If

            End If
        End If


        If DaysInabilitiesTotal > 0 Then
            If DaysCalculation + DaysInabilitiesTotal > PayrollDays Then
                DaysCalculation = PayrollDays - DaysInabilitiesTotal
            End If
        End If

        If DaysInabilitiesTotal > 0 Then
            If DaysCalculation + DaysInabilitiesTotal > PayrollDays Then
                If Day31 Then
                    DaysCalculation = PayrollDays - DaysInabilitiesTotal
                Else
                    DaysCalculation = PayrollDays - DaysInabilitiesTotal - EmployerDays
                End If
            End If
        End If

        PermanentInability = 0

        If TotalInabilitiesDays > 0 Then
            'Es porque esta incapacidad es una extensión. Toca validar si ya está en los 180 días
            If TotalInabilitiesDays + DaysCalculation >= 180 Then
                PermanentInability = 1

                If 180 > TotalInabilitiesDays Then
                    DaysCalculation = 180 - TotalInabilitiesDays
                End If
            End If
        End If

        'Si las incapacidades que ya están (del mismo tipo) suman más de 180 días, no se paga ya la incapacidad
        If TotalInabilitiesDays >= 180 Then
            ObjInability.EPSRecognizeValue = 0
            PermanentInability = 2
        End If

        If DaysCalculation > 0 Then
            InabilityCollect = Math.Round(CDbl(ObjInability.EPSRecognizeValue * DaysCalculation / EPSDays), 3)
        Else
            DaysCalculation = 0
            InabilityCollect = 0
        End If

        'Si la fecha está dentro del nuevo ciclo de 30 días y se pagaron los 3 días del 50% en la nómina anterior, no se paga la incapacidad
        If actualCulture = _cultureCR AndAlso ObjInability.IsCycleDate = True AndAlso ObjInability.RealDate < PayrollStarDate Then
            InabilityCollect = 0
        End If

    End Sub

    ''' <summary>
    ''' Función Para Calcular el Valor a Pagar por Contabilidad de la Incapacidad del Patrono
    ''' </summary>
    ''' <param name="ObjInability">Obj Incapacidad</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nomina</param>
    ''' <param name="PayrollStarDate">Fecha Inicio Nomina</param>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Sub GetSpendingNovelty(LiquidationDomain As LiquidationDomain, ObjInability As Novelty, PayrollEndDate As Date, PayrollInitialDate As Date, ByRef SpendingInabilityAmbulatory As Decimal, ByRef CalculationDays As Integer, ByVal DaysInabilitiesTotal As Integer, ByVal PayrollDays As Integer, SessionValues As SessionValues, Optional Day31 As Boolean = False)

        'Cargamos el lenguaje de la cultura que se está utilizando
        Dim actualCulture = SessionValues.LanguageCulture

        If ObjInability.EmployerDays IsNot Nothing Then

            Dim EmployerDays As Integer = IIf(ObjInability.EmployerDays IsNot Nothing, ObjInability.EmployerDays, 0)
            Dim InitialDateInability As Date = ObjInability.RealDate

            If ObjInability.TypeNovelty = 2 Or (ObjInability.TypeNovelty = 3 And (ObjInability.LicenseClass = eNoveltyLicenseClass.Remunerada Or ObjInability.LicenseClass = eNoveltyLicenseClass.NoRemunerada Or ObjInability.LicenseClass = eNoveltyLicenseClass.DiaFamilia)) Then
                EmployerDays = ObjInability.Days
                ObjInability.PaidEmployerValue = 0
            End If

            Dim EndDatePaidEmployer As Date = DateAdd(DateInterval.Day, EmployerDays - 1, ObjInability.RealDate)
            Dim InitialDatePatronoPaid As Date = DateAdd(DateInterval.Day, 1, EndDatePaidEmployer)

            If EndDatePaidEmployer < PayrollInitialDate And ObjInability.Status = 2 Then
                EmployerDays = 0
            End If

            If EmployerDays > 0 Then

                ''Incapacidades inician en un mes anterior y finalizan en el periodo
                If InitialDateInability < PayrollInitialDate And ObjInability.EndDate <= PayrollEndDate And ObjInability.Status = 2 Then

                    If PayrollEndDate.Month = 2 Then
                        CalculationDays = DateDiff(DateInterval.Day, PayrollInitialDate, EndDatePaidEmployer) + 1
                    Else
                        ' Usar Day31 para determinar si incluir día 31 en cálculos
                        If Day31 Then
                            CalculationDays = DateDiff(DateInterval.Day, PayrollInitialDate, EndDatePaidEmployer) + 1
                        Else
                            CalculationDays = LiquidationDomain.Days360(PayrollInitialDate, EndDatePaidEmployer)
                        End If
                    End If

                End If

                ''Incapacidades inician en un mes anterior y finalizan en el periodo y es nueva
                If InitialDateInability < PayrollInitialDate And ObjInability.EndDate < PayrollEndDate And ObjInability.Status = 0 Then
                    If ObjInability.RealDate.Month = 2 Then
                        CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, EndDatePaidEmployer) + 1
                    Else
                        'Analizo las incapacidades de meses pasados
                        If ObjInability.RealDate < PayrollInitialDate And ObjInability.Status = 0 Then
                            Dim diferenciaDias As Integer = DateDiff(DateInterval.Day, ObjInability.RealDate, PayrollInitialDate)
                            ' Usar Day31 para determinar si incluir día 31 en cálculos
                            If Day31 Then
                                CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, EndDatePaidEmployer) + 1
                            Else
                                CalculationDays = LiquidationDomain.Days360(InitialDateInability, EndDatePaidEmployer)
                            End If
                        End If
                    End If
                End If

                ''Incapacidades inician este mes y finalizan en este
                If InitialDateInability >= PayrollInitialDate And ObjInability.EndDate <= PayrollEndDate Then

                    If PayrollEndDate.Month = 2 Then
                        CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, EndDatePaidEmployer) + 1
                    Else
                        ' Usar Day31 para determinar si incluir día 31 en cálculos
                        If Day31 Then
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, EndDatePaidEmployer) + 1
                        Else
                            CalculationDays = LiquidationDomain.Days360(InitialDateInability, EndDatePaidEmployer)
                        End If
                    End If

                End If

                ''Incapacidades que inicia este mes y finaliza el siguiente
                If InitialDateInability >= PayrollInitialDate And ObjInability.EndDate > PayrollEndDate Then

                    ' Usar Day31 para determinar si incluir día 31 en cálculos
                    If Day31 Then
                        CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, PayrollEndDate) + 1
                    Else
                        CalculationDays = LiquidationDomain.Days360(InitialDateInability, PayrollEndDate)
                    End If

                    If CalculationDays > EmployerDays Then
                        CalculationDays = EmployerDays
                    End If

                End If

                ''Incapacidades que Iniciaron un Periodo Antes, y finalizan un periodo después
                If InitialDateInability < PayrollInitialDate And ObjInability.EndDate > PayrollEndDate Then
                    If EndDatePaidEmployer >= PayrollInitialDate Then
                        If actualCulture = _cultureCR AndAlso ObjInability.IsCycleDate = True AndAlso ObjInability.PaidEmployerValue IsNot Nothing Then
                            ' Usar Day31 para determinar si incluir día 31 en cálculos
                            If Day31 Then
                                CalculationDays = DateDiff(DateInterval.Day, PayrollInitialDate, PayrollEndDate) + 1
                            Else
                                CalculationDays = LiquidationDomain.Days360(PayrollInitialDate, PayrollEndDate)
                            End If
                            EmployerDays = CalculationDays
                        Else
                            ' Usar Day31 para determinar si incluir día 31 en cálculos
                            If Day31 Then
                                CalculationDays = DateDiff(DateInterval.Day, PayrollInitialDate, EndDatePaidEmployer) + 1
                            Else
                                CalculationDays = LiquidationDomain.Days360(PayrollInitialDate, EndDatePaidEmployer)
                            End If
                        End If
                    End If
                End If

                'Incapacidades Pasadas
                If InitialDateInability < PayrollInitialDate And ObjInability.EndDate < PayrollInitialDate And ObjInability.Status = 0 Then
                    If EndDatePaidEmployer >= InitialDateInability Then
                        CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, EndDatePaidEmployer) + 1
                    End If
                End If


                If DaysInabilitiesTotal > 0 Then
                    If CalculationDays + DaysInabilitiesTotal > PayrollDays Then
                        CalculationDays = PayrollDays - DaysInabilitiesTotal
                    End If
                End If

                ''Incapacidades a Futuro

                If PayrollEndDate < InitialDateInability Then
                    CalculationDays = 0
                End If

            End If

            If CalculationDays > 0 Then
                SpendingInabilityAmbulatory = Utils.RoundValue((IIf(ObjInability.PaidEmployerValue IsNot Nothing, ObjInability.PaidEmployerValue, 0) * CalculationDays) / EmployerDays)
            Else
                CalculationDays = 0
                SpendingInabilityAmbulatory = 0
            End If

            'Se valida que el lenguaje de la cultura sea COSTA RICA y esté la bandera del ciclo de los 30 días, para así postular el valor a pagar de la novedad
            If actualCulture = _cultureCR AndAlso ObjInability.IsCycleDate = True AndAlso ObjInability.PaidEmployerValue IsNot Nothing Then
                'Se postula el valor correspondiente según cuando se inicia la incapacidad y el estado de la misma
                If InitialDateInability >= PayrollInitialDate And ObjInability.EndDate > PayrollEndDate Then
                    SpendingInabilityAmbulatory = ObjInability.PaidEmployerValue
                ElseIf InitialDateInability < PayrollInitialDate And ObjInability.EndDate <= PayrollEndDate And ObjInability.Status = 2 Then
                    SpendingInabilityAmbulatory = 0
                End If
            End If
        Else
            CalculationDays = 0
            SpendingInabilityAmbulatory = 0
        End If
    End Sub


    Private Function VacationAdjustNovelty(LiquidationDomain As LiquidationDomain, enjoyDays As Integer, VacationInitialDate As Date, PayrollStarDate As Date, PayrollEndDate As Date, VacationEndModifiedDate As Date?, InitialDateInability As Date?, VacationInitialModifiedDate As Date) As Integer Implements ILiquidationFunctions.VacationAdjustNovelty

        Dim ReturnDays As Integer = 0

        If VacationEndModifiedDate <= PayrollEndDate Then

            Dim VacationEndDate As Date

            VacationEndDate = VacationInitialDate.AddDays(enjoyDays - 1)

            'Para Vacaciones que vienen del mes anterior y finalizan este mes
            If PayrollStarDate > VacationInitialDate And VacationEndDate <= PayrollEndDate Then
                ReturnDays = LiquidationDomain.Days360(PayrollStarDate, VacationEndDate)

                ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
                If VacationEndDate.Day = 31 And VacationEndDate <= PayrollEndDate Then
                    ReturnDays = ReturnDays + 1
                End If
            End If

            If VacationInitialModifiedDate >= PayrollStarDate And VacationEndModifiedDate <= VacationEndModifiedDate Then
                'analizo que las vacaciones modificadas estén en el rango de este mes
                ReturnDays = LiquidationDomain.Days360(VacationInitialModifiedDate, VacationEndModifiedDate)

                ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
                If VacationEndModifiedDate.Value.Day = 31 And VacationEndModifiedDate <= PayrollEndDate Then
                    ReturnDays = ReturnDays + 1
                End If
            End If

            'Para Vacaciones que iniciaron este mes y finalizan el siguiente
            If PayrollStarDate <= VacationInitialDate And VacationEndDate > PayrollEndDate Then
                ReturnDays = LiquidationDomain.Days360(VacationInitialDate, PayrollEndDate)

                ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
                If PayrollEndDate.Day = 31 Then
                    ReturnDays = ReturnDays + 1
                End If
            End If

            'Para Vacaciones que iniciaron el mes anterior y finalizan el proximo mes
            If PayrollStarDate > VacationInitialDate And VacationEndDate > PayrollEndDate Then
                ReturnDays = LiquidationDomain.Days360(PayrollStarDate, PayrollEndDate)

                ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
                If PayrollEndDate.Day = 31 Then
                    ReturnDays = ReturnDays + 1
                End If
            End If

        End If

        If InitialDateInability IsNot Nothing And PayrollStarDate <= VacationInitialDate And VacationEndModifiedDate > PayrollEndDate And InitialDateInability >= PayrollStarDate And InitialDateInability < PayrollEndDate Then
            'Para vacaciones que iniciaban este mes, pero por la incapacidad se corrió para el siguiente mes
            If InitialDateInability > VacationInitialDate Then
                ReturnDays = LiquidationDomain.Days360(VacationInitialDate, DateAdd(DateInterval.Day, -1, InitialDateInability.Value))
            End If
        End If

        Return ReturnDays

    End Function

    Private Function PermanentInability(Employee As Domain.Payroll.Entities.Employee, PayrollStarDate As Date, PayrollEndDate As Date, LiquidationDomain As LiquidationDomain) As Integer Implements ILiquidationFunctions.PermanentInability

        Dim ReturnDays As Integer = 0
        Dim EndDatePermanent As Date

        If Employee.EndDatePermanentInability Is Nothing Then
            EndDatePermanent = PayrollEndDate
        Else
            EndDatePermanent = Employee.EndDatePermanentInability
        End If

        'Si inició antes de que iniciara la Nómina y Finaliza Después que finalice esta Nómina
        If Employee.InitialDatePermanentInability < PayrollStarDate And PayrollEndDate < EndDatePermanent Then
            ReturnDays = LiquidationDomain.Days360(PayrollStarDate, PayrollEndDate)

            ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
            If PayrollEndDate.Day = 31 Then
                ReturnDays = ReturnDays + 1
            End If
        End If

        'Si inicia en la nómina, y finaliza después de la nómina
        If PayrollStarDate <= Employee.InitialDatePermanentInability And PayrollEndDate < EndDatePermanent Then
            ReturnDays = LiquidationDomain.Days360(Employee.InitialDatePermanentInability, PayrollEndDate)

            ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
            If PayrollEndDate.Day = 31 Then
                ReturnDays = ReturnDays + 1
            End If
        End If

        'Si inicia antes de la nómina, y finaliza en la nómina
        If Employee.InitialDatePermanentInability < PayrollStarDate And EndDatePermanent <= PayrollEndDate Then
            ReturnDays = LiquidationDomain.Days360(PayrollStarDate, EndDatePermanent)

            ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
            If EndDatePermanent.Day = 31 And EndDatePermanent <= PayrollEndDate Then
                ReturnDays = ReturnDays + 1
            End If
        End If

        'Si inicia y finaliza dentro de la Nómina
        If Employee.InitialDatePermanentInability >= PayrollStarDate And EndDatePermanent <= PayrollEndDate Then
            ReturnDays = LiquidationDomain.Days360(Employee.InitialDatePermanentInability, EndDatePermanent)

            ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
            If EndDatePermanent.Day = 31 And EndDatePermanent <= PayrollEndDate Then
                ReturnDays = ReturnDays + 1
            End If
        End If

        If Employee.InitialDatePermanentInability > PayrollEndDate Then
            ReturnDays = 0
        End If

        Return ReturnDays

    End Function

    Private Function GetDaysInabilityVacation(PayrollEndDate As Date, PayrollStarDate As Date, InabilityEndDate As Date?, InabilityStartDate As Date?, VacationEndDate As Date, VacationStartDate As Date, LiquidationDomain As LiquidationDomain) As Integer Implements ILiquidationFunctions.GetDaysInabilityVacation

        If InabilityEndDate Is Nothing Or InabilityStartDate Is Nothing Then
            Return 0
        End If

        'Se agrega validación para excluir incapacidades que están fuera del rango de las vacaciones
        If Not ((InabilityStartDate >= VacationStartDate AndAlso InabilityStartDate <= VacationEndDate) OrElse (InabilityEndDate >= VacationStartDate AndAlso InabilityEndDate <= VacationEndDate) OrElse (InabilityStartDate < VacationStartDate) AndAlso (InabilityEndDate > VacationEndDate)) Then
            Return 0
        End If

        Dim DaysInability As Integer = 0

        'Si la incapacidad se encuentra dentro de las vacaciones
        If PayrollStarDate <= VacationStartDate And VacationStartDate < InabilityStartDate And VacationEndDate > InabilityEndDate And InabilityEndDate <= PayrollEndDate Then
            DaysInability = LiquidationDomain.Days360(InabilityStartDate, InabilityEndDate)

            ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
            If InabilityEndDate.Value.Day = 31 And InabilityEndDate <= PayrollEndDate Then
                DaysInability = DaysInability + 1
            End If
        End If

        'Si se encuentra en vacaciones y tiene incapacidad, se acaban las vacaciones y sigue con la incapacidad
        If PayrollStarDate <= VacationStartDate And VacationStartDate <= InabilityStartDate And InabilityStartDate <= VacationEndDate And InabilityEndDate <= PayrollEndDate Then
            DaysInability = LiquidationDomain.Days360(VacationEndDate, InabilityEndDate)

            ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
            If InabilityEndDate.Value.Day = 31 And InabilityEndDate <= PayrollEndDate Then
                DaysInability = DaysInability + 1
            End If

        End If

        'Si esta incapacidad, sale a vacaciones regresa y continua la incapacidad
        If PayrollStarDate <= VacationStartDate And VacationStartDate > InabilityStartDate And VacationEndDate < InabilityEndDate And InabilityEndDate <= PayrollEndDate Then
            DaysInability = LiquidationDomain.Days360(VacationStartDate, VacationEndDate)

            ' Ajuste para día 31: Days360 no cuenta correctamente el día 31
            If VacationEndDate.Day = 31 And VacationEndDate <= PayrollEndDate Then
                DaysInability = DaysInability + 1
            End If
        End If

        Return DaysInability

    End Function

    ''' <summary>
    ''' Calcula la intersección en días entre dos rangos de fechas
    ''' </summary>
    ''' <param name="startDate1">Fecha inicio del primer rango (incapacidad)</param>
    ''' <param name="endDate1">Fecha fin del primer rango (incapacidad)</param>
    ''' <param name="startDate2">Fecha inicio del segundo rango (período de liquidación)</param>
    ''' <param name="endDate2">Fecha fin del segundo rango (período de liquidación)</param>
    ''' <param name="Day31">Si se debe incluir el día 31 en los cálculos</param>
    ''' <returns>Número de días que intersectan entre los dos rangos</returns>
    Private Function CalculateIntersectionDays(startDate1 As Date, endDate1 As Date, startDate2 As Date, endDate2 As Date, Day31 As Boolean) As Integer
        ' Calcular fechas de intersección
        Dim intersectionStart As Date = If(startDate1 > startDate2, startDate1, startDate2)
        Dim intersectionEnd As Date = If(endDate1 < endDate2, endDate1, endDate2)
        ' Si no hay intersección, retornar 0
        If intersectionStart > intersectionEnd Then
            Return 0
        End If
        ' Calcular días de intersección según configuración Day31
        Dim days As Integer
        days = DateDiff(DateInterval.Day, intersectionStart, intersectionEnd) + 1
        Return Math.Max(0, days)
    End Function

    ''' <summary>
    ''' Clase para el resultado de la validación de incapacidades
    ''' </summary>
    Private Class IncapacityValidationResult
        Public Property ShouldExcludeAccruedDeducted As Boolean
        Public Property ShouldExcludeDescription As Boolean
        Public Property ProportionalPayment As Double
    End Class

End Class
