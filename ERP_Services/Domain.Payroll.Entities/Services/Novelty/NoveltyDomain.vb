'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 12-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Class NoveltyDomain
    Implements INoveltyDomain

    ''' <summary>
    ''' Aumenta el consecutivo en 1 y si la novedad es nueva le asigna el consecutivo a la novedad
    ''' </summary>
    ''' <param name="consecutive">Consecutivo</param>
    ''' <param name="novelty">Novedad</param>
    ''' <remarks></remarks>
    Public Sub LoadConsecutive(ByRef consecutive As Domain.Entities.Consecutive, ByRef novelty As Novelty) Implements INoveltyDomain.LoadConsecutive
        'Valido si es nuevo para agregarle el consecutivo
        If novelty.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            consecutive.NumberConsecutive = consecutive.NumberConsecutive + 1
            novelty.Consecutive = consecutive.NumberConsecutive
        End If
    End Sub

    ''' <summary>
    ''' Funcion que retorna la letra de la novedad que haya elegido el usuario
    ''' </summary>
    ''' <param name="letterId">Id de la letra</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReturnLetter(letterId As Integer, licenseClass As Nullable(Of Byte)) As String Implements INoveltyDomain.ReturnLetter
        Dim letra As String = ""
        Select Case letterId
            Case 99 'Ficticia Vacaciones
                letra = "V"
            Case 1 'Incapacidad
                letra = "I"
            Case 2 ' Sancion
                letra = "S"
            Case 3 'Licencia
                Select Case licenseClass
                    Case 4
                        letra = "PV"
                    Case Else
                        letra = "L"
                End Select
        End Select
        Return letra
    End Function

    ''' <summary>
    ''' Se recorre la lista de detalle para saber cuales son menores a la fecha de liquidacion e insertar en una lista y los mayores 
    ''' a la fecha de liquidacion se modifican y agregan a otra lista
    ''' </summary>
    ''' <param name="listDetailSource">Lista de detalles que se va a recorrer</param>
    ''' <param name="novelty">Novedad del empleado</param>
    ''' <param name="listDiscount">Lista de descuentos</param>
    ''' <param name="listSchedule">Lista de detalles</param>
    ''' <remarks></remarks>
    Public Sub LoadListSchedule(listDetailSource As List(Of ScheduleDetail), novelty As Novelty, employee As Employee, ByRef listDiscount As List(Of NoveltyScheduleDetail), ByRef listSchedule As List(Of ScheduleDetail), ByRef timeDiscountPeriod As Dictionary(Of KeyValuePair(Of String, Integer), Integer), ByRef errorMessage As String) Implements INoveltyDomain.LoadListSchedule
        Dim contract As Contract = employee.Contract.Where(Function(x) x.Valid = True And x.Status = 1).FirstOrDefault() 'Obtengo el contrato del empleado
        Dim listDiscountTmp As List(Of NoveltyScheduleDetail) = New List(Of NoveltyScheduleDetail)
        Dim listScheduleTmp As List(Of ScheduleDetail) = New List(Of ScheduleDetail)
        Dim timeDiscountTmp As Dictionary(Of KeyValuePair(Of String, Integer), Integer) = New Dictionary(Of KeyValuePair(Of String, Integer), Integer)

        If contract Is Nothing Then
            errorMessage = "No se encontró un contrato válido para el empleado."
            Exit Sub
        End If

        For Each item As ScheduleDetail In listDetailSource

            'If contract.LastLiquidationDate IsNot Nothing AndAlso (item.DateDetail < contract.LastLiquidationDate) Then
            If item.DateDetail < contract.LastLiquidationDate Then
                Dim deduccionDetalle As NoveltyScheduleDetail = CType(item, NoveltyScheduleDetail)
                deduccionDetalle.PayrollDate = contract.Group.NextDateLiquidation
                listDiscountTmp.Add(deduccionDetalle)
            Else
                item.StartTracking()
                While item.ScheduleDetailHour.Count > 0
                    Dim cantidad = item.ScheduleDetailHour.Count - 1
                    While item.ScheduleDetailHour.Item(cantidad).ScheduleDetailConcept.Count > 0
                        Dim cantidadConcepto = item.ScheduleDetailHour.Item(cantidad).ScheduleDetailConcept.Count - 1
                        Dim concept = item.ScheduleDetailHour.Item(cantidad).ScheduleDetailConcept.Item(cantidadConcepto)
                        concept.StartTracking()
                        concept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    End While
                    Dim hours = item.ScheduleDetailHour.Item(cantidad)
                    hours.StartTracking()
                    hours.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                End While

                Dim period As String = item.DateDetail.ToString("MM/yyyy")
                Dim periodFunctionalUnit As New KeyValuePair(Of String, Integer)(period, item.ScheduleFunctionalUnitId)
                If timeDiscountTmp.ContainsKey(periodFunctionalUnit) Then
                    timeDiscountTmp(periodFunctionalUnit) += item.TotalNumberHours
                Else
                    timeDiscountTmp.Add(periodFunctionalUnit, item.TotalNumberHours)
                End If
                item.TotalNumberHours = 0
                item.ScheduleTemplate = Nothing
                item.ScheduleTemplateId = Nothing
                item.Letter = ReturnLetter(novelty.TypeNovelty, novelty.LicenseClass)
                item.MarkAsModified()
                listScheduleTmp.Add(item)
            End If
        Next
        Dim dateInitial As Date = novelty.RealDate
        While dateInitial <= novelty.EndDate
            If listDetailSource.Where(Function(x) x.DateDetail = dateInitial And x.ScheduleFunctionalUnitId = contract.FunctionalUnitId).Count = 0 Then
                Dim detailNovelty As New ScheduleDetail()
                detailNovelty.GroupId = contract.GroupId
                detailNovelty.EmployeeId = novelty.EmployeeId
                detailNovelty.ContractId = contract.Id
                detailNovelty.CompanyId = contract.FunctionalUnit.BranchOffice.CompanyId
                detailNovelty.BranchOfficeId = contract.FunctionalUnit.BranchOfficeId
                detailNovelty.FunctionalUnitId = contract.FunctionalUnitId
                detailNovelty.CenterCostId = employee.CostCenterId
                detailNovelty.ScheduleFunctionalUnitId = contract.FunctionalUnitId
                detailNovelty.Letter = ReturnLetter(novelty.TypeNovelty, novelty.LicenseClass)
                detailNovelty.DateDetail = dateInitial
                detailNovelty.TotalNumberHours = 0
                detailNovelty.ScheduleTemplateId = Nothing
                detailNovelty.Status = 0
                detailNovelty.State = True
                listScheduleTmp.Add(detailNovelty)
            End If
            dateInitial = dateInitial.AddDays(1)
        End While
        listDiscount = listDiscountTmp
        listSchedule = listScheduleTmp
        timeDiscountPeriod = timeDiscountTmp
    End Sub

    ''' <summary>
    ''' carga un detalle al calendario y lo retorna
    ''' </summary>
    ''' <param name="schedule">Calendario</param>
    ''' <param name="scheduleDetail">Detalle que se va asignar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadDetailToDay(schedule As Schedule, scheduleDetail As ScheduleDetail) As Schedule Implements INoveltyDomain.LoadDetailToDay
        Dim StrProperty As String = "ScheduleDetail"
        If scheduleDetail.DateDetail.Day > 1 Then
            StrProperty = "ScheduleDetail" & (scheduleDetail.DateDetail.Day - 1).ToString()
        End If
        Dim type As Type = schedule.GetType()
        Dim objProperty = type.GetProperty(StrProperty)
        objProperty.SetValue(schedule, scheduleDetail)
        Return schedule
    End Function

    ''' <summary>
    ''' carga un diccionario que tiene como llave el periodo
    ''' </summary>
    ''' <param name="listScheduleDetail">Lista de detalles que se va agrupar en el diccionario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadDictionarySchedule(listScheduleDetail As List(Of ScheduleDetail)) As Dictionary(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) Implements INoveltyDomain.LoadDictionarySchedule
        Dim scheduleDictionary As Dictionary(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) = New Dictionary(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail))
        For Each itemDetail As ScheduleDetail In listScheduleDetail
            Dim period As String = itemDetail.DateDetail.ToString("MM/yyyy")
            Dim periodoFunctionalUnir As New KeyValuePair(Of String, Integer)(period, itemDetail.ScheduleFunctionalUnitId)
            If scheduleDictionary.ContainsKey(periodoFunctionalUnir) Then
                scheduleDictionary(periodoFunctionalUnir).Add(itemDetail)
            Else
                Dim listTmp As List(Of ScheduleDetail) = New List(Of ScheduleDetail)()
                listTmp.Add(itemDetail)
                scheduleDictionary.Add(periodoFunctionalUnir, listTmp)
            End If
        Next
        Return scheduleDictionary
    End Function

    ''' <summary>
    ''' Elimina lis detalle de un schedule
    ''' </summary>
    ''' <param name="schedule">Calendario</param>
    ''' <param name="listScheduleDetail">Lista de detalles</param>
    ''' <returns>Calendario</returns>
    ''' <remarks></remarks>
    Public Function DeleteDetailToSchedule(schedule As Schedule, listScheduleDetail As List(Of ScheduleDetail)) As Schedule Implements INoveltyDomain.DeleteDetailToSchedule
        Dim type As Type = schedule.GetType()
        For Each item As ScheduleDetail In listScheduleDetail
            Dim strProperty As String = "ScheduleDetail"
            Dim strProperty2 As String = "D"
            If item.DateDetail.Day > 1 Then
                strProperty &= (item.DateDetail.Day - 1).ToString()
            End If
            If item.DateDetail.Day < 9 Then
                strProperty2 &= "0" & item.DateDetail.Day.ToString()
            Else
                strProperty2 &= item.DateDetail.Day.ToString()
            End If
            Dim ObjProperty = type.GetProperty(strProperty)
            'Dim scheduleDetail As ScheduleDetail = ObjProperty.GetValue(schedule)
            'scheduleDetail.StartTracking()
            'scheduleDetail.MarkAsDeleted()
            ObjProperty.SetValue(schedule, Nothing)
            'Dim ObjProperty2 = type.GetProperty(strProperty2)
            'ObjProperty2.SetValue(schedule, Nothing)
            'While scheduleDetail.ScheduleDetailHour.Count > 0
            '    Dim cantidad = scheduleDetail.ScheduleDetailHour.Count - 1
            '    While scheduleDetail.ScheduleDetailHour.Item(cantidad).ScheduleDetailConcept.Count > 0
            '        Dim cantidadConcepto = scheduleDetail.ScheduleDetailHour.Item(cantidad).ScheduleDetailConcept.Count - 1
            '        item.ScheduleDetailHour.Item(cantidad).ScheduleDetailConcept.Item(cantidadConcepto).MarkAsDeleted()
            '    End While
            '    scheduleDetail.ScheduleDetailHour.Item(cantidad).MarkAsDeleted()
            'End While
            'Dim ObjProperty2 = type.GetProperty(strProperty2)
            'ObjProperty2.SetValue(schedule, Nothing)
        Next
        Return schedule
    End Function

    ''' <summary>
    ''' Clona una novedad creando una nueva con los mismos valores
    ''' </summary>
    ''' <param name="novelty">Novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CloneNovelty(novelty As Novelty) As Novelty Implements INoveltyDomain.CloneNovelty
        Dim newNovelty As New Novelty()
        newNovelty.AutorizationNumber = novelty.AutorizationNumber
        newNovelty.CalculationType = novelty.CalculationType
        newNovelty.Consecutive = novelty.Consecutive
        newNovelty.Days = novelty.Days
        newNovelty.Employee = novelty.Employee
        newNovelty.EmployeeBaseSalary = novelty.EmployeeBaseSalary
        newNovelty.EmployeeId = novelty.EmployeeId
        newNovelty.EmployerDays = novelty.EmployerDays
        newNovelty.EndDate = novelty.EndDate
        newNovelty.EPSDays = novelty.EPSDays
        newNovelty.EPSRecognizeValue = novelty.EPSRecognizeValue
        newNovelty.Extension = novelty.Extension
        newNovelty.Group = novelty.Group
        newNovelty.GroupId = novelty.GroupId
        newNovelty.IBC = novelty.IBC
        newNovelty.InabilityClass = novelty.InabilityClass
        newNovelty.LicenseClass = novelty.LicenseClass
        newNovelty.LiquidatedIBCorSalary = novelty.LiquidatedIBCorSalary
        newNovelty.LiquidationBase = novelty.LiquidationBase
        newNovelty.NoveltyLiquidate = novelty.NoveltyLiquidate
        newNovelty.PaidEmployerValue = novelty.PaidEmployerValue
        newNovelty.PaidPayrollValue = novelty.PaidPayrollValue
        newNovelty.RealDate = novelty.RealDate
        newNovelty.Reason = novelty.Reason
        newNovelty.RiskType = novelty.RiskType
        newNovelty.Status = novelty.Status
        newNovelty.TypeNovelty = novelty.TypeNovelty
        newNovelty.Value = novelty.Value
        Return newNovelty
    End Function

    ''' <summary>
    ''' Calcula el promedio las liquidaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateAverageIBCLiquidationLastMonth(listLiquidation As List(Of Liquidation), numberLastMonth As Integer, Optional SalaryType As Byte = 1) As Decimal Implements INoveltyDomain.CalculateAverageIBCLiquidationLastMonth
        If listLiquidation.Count = 0 Then
            Return 0
        End If
        Dim averageTotalMonth As Decimal = 0

        If SalaryType = 1 Then

            For Each ObjLiquidation As Liquidation In listLiquidation

                Dim ObjTmpLiquidationDetail = ObjLiquidation.LiquidationDetail.Where(Function(x) x.Concept.AffectIBC = True).ToList()

                For Each ObjLiquidationDetail As LiquidationDetail In ObjTmpLiquidationDetail

                    If ObjLiquidationDetail.ConceptType = 1 Then
                        averageTotalMonth = averageTotalMonth + ObjLiquidationDetail.ConceptTotalValue
                    Else
                        averageTotalMonth = averageTotalMonth - ObjLiquidationDetail.ConceptTotalValue
                    End If
                Next
            Next
        Else
            For Each ObjLiquidation As Liquidation In listLiquidation

                Dim ObjTmpLiquidationDetail = ObjLiquidation.LiquidationDetail.Where(Function(x) x.Concept.AffectIBC = True).ToList()

                For Each ObjLiquidationDetail As LiquidationDetail In ObjTmpLiquidationDetail

                    If ObjLiquidationDetail.ConceptType = 1 Then
                        If ObjLiquidationDetail.ConceptClass = "005" Then
                            averageTotalMonth = averageTotalMonth + (ObjLiquidationDetail.ConceptTotalValue * 0.7)
                        Else
                            averageTotalMonth = averageTotalMonth + ObjLiquidationDetail.ConceptTotalValue
                        End If

                    Else
                        averageTotalMonth = averageTotalMonth - ObjLiquidationDetail.ConceptTotalValue
                    End If
                Next
            Next
        End If

        averageTotalMonth = averageTotalMonth / numberLastMonth
        Return averageTotalMonth
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
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
