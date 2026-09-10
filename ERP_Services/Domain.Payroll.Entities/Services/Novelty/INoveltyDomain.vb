'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 12-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


Public Interface INoveltyDomain
    Inherits IDisposable

    ''' <summary>
    ''' Aumenta el consecutivo en 1 y si la novedad es nueva le asigna el consecutivo a la novedad
    ''' </summary>
    ''' <param name="consecutive">Consecutivo</param>
    ''' <param name="novelty">Novedad</param>
    ''' <remarks></remarks>
    Sub LoadConsecutive(ByRef consecutive As Domain.Entities.Consecutive, ByRef novelty As Novelty)

    ''' <summary>
    ''' Funcion que retorna la letra de la novedad que haya elegido el usuario
    ''' </summary>
    ''' <param name="letterId">Id de la letra</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ReturnLetter(letterId As Integer, licenseClass As Nullable(Of Byte)) As String

    ''' <summary>
    ''' Se recorre la lista de detalle para saber cuales son menores a la fecha de liquidacion e insertar en una lista y los mayores 
    ''' a la fecha de liquidacion se modifican y agregan a otra lista
    ''' </summary>
    ''' <param name="listDetailSource">Lista de detalles que se va a recorrer</param>
    ''' <param name="novelty">Novedad del empleado</param>
    ''' <param name="listDiscount">Lista de descuentos</param>
    ''' <param name="listSchedule">Lista de detalles</param>
    ''' <remarks></remarks>
    Sub LoadListSchedule(ByVal listDetailSource As List(Of ScheduleDetail), ByVal novelty As Novelty, employee As Employee, ByRef listDiscount As List(Of NoveltyScheduleDetail), ByRef listSchedule As List(Of ScheduleDetail), ByRef timeDiscountPeriod As Dictionary(Of KeyValuePair(Of String, Integer), Integer), ByRef errorMessage As String)

    ''' <summary>
    ''' carga un detalle al calendario y lo retorna
    ''' </summary>
    ''' <param name="schedule">Calendario</param>
    ''' <param name="scheduleDetail">Detalle que se va asignar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadDetailToDay(ByVal schedule As Schedule, ByVal scheduleDetail As ScheduleDetail) As Schedule

    ''' <summary>
    ''' carga un diccionario que tiene como llave el periodo
    ''' </summary>
    ''' <param name="listScheduleDetail">Lista de detalles que se va agrupar en el diccionario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadDictionarySchedule(ByVal listScheduleDetail As List(Of ScheduleDetail)) As Dictionary(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail))

    ''' <summary>
    ''' Elimina lis detalle de un schedule
    ''' </summary>
    ''' <param name="schedule">Calendario</param>
    ''' <param name="listScheduleDetail">Lista de detalles</param>
    ''' <returns>Calendario</returns>
    ''' <remarks></remarks>
    Function DeleteDetailToSchedule(schedule As Schedule, listScheduleDetail As List(Of ScheduleDetail)) As Schedule

    ''' <summary>
    ''' Clona una novedad creando una nueva con los mismos valores
    ''' </summary>
    ''' <param name="novelty">Novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CloneNovelty(novelty As Novelty) As Novelty

    ''' <summary>
    ''' Calcula el promedio las liquidaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CalculateAverageIBCLiquidationLastMonth(listLiquidation As List(Of Liquidation), numberLastMonth As Integer, Optional SalaryType As Byte = 1) As Decimal

End Interface
