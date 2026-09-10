'************************************************************
' Assembly         : Domain.Crystal.Service
' Author           : Juan F. Tamayo
' Created          : 2015-01-25
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports System.Text
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions


#End Region
''' <summary>
''' Expone servicios de lógica de dominio para el calculo de estancias
''' </summary>
Public Class StayService
    Implements IStayService

#Region "Consts"

    ''' <summary>
    ''' Nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME As String = "Crystal"

#End Region

#Region "Fields"

    ''' <summary>
    ''' Servicio de dominio de facturación
    ''' </summary>
    Private _billingDomainService As IBillingServices

    ''' <summary>
    ''' Repositorio de estancias
    ''' </summary>
    Private _stayRepository As IStayRepository

    ''' <summary>
    ''' Repositorio de grupo de atención
    ''' </summary>
    Private _caregroupRepository As ICareGroupRepository

    ''' <summary>
    ''' Repositorio de parametros
    ''' </summary>
    Private _parameterRepository As IParameterRepository

    ''' <summary>
    ''' Repositorio de CUPS
    ''' </summary>
    Private _cupsEntityRepository As ICupsEntityRepository

    Private _cupsEntityContractDescriptionsRepository As ICupsEntityContractDescriptionsRepository

    ''' <summary>
    ''' Repositorio de ingresos
    ''' </summary>
    Private _admissionRepository As IAdmissionRepository

    ''' <summary>
    ''' Repositorio de camas
    ''' </summary>
    Private _bedRateRepository As IBedRateRepository

    ''' <summary>
    ''' Repositorio estancias para control de cuentas
    ''' </summary>
    Private _accountControlStayRepository As IAccountControlStayRepository
    ''' <summary>
    ''' Repositorio estancias para control de justificaciones
    ''' </summary>
    Private _accountControlJustificationRepository As IAccountControlJustificationRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="stayRepository">Repositorio de estancias</param>
    ''' <param name="caregroupRepository">Repositorio de grupo de atención</param>
    ''' <param name="parameterRepository">Repositorio de parametros</param>
    Public Sub New(ByVal stayRepository As IStayRepository, ByVal caregroupRepository As ICareGroupRepository,
                   parameterRepository As IParameterRepository, ByVal billingService As IBillingServices,
                   cupsEntityRepository As ICupsEntityRepository,
                   admissionRepository As IAdmissionRepository,
                   bedRateRepository As IBedRateRepository,
                   accountControlStayRepository As IAccountControlStayRepository,
                   accountControlJustificationRepository As IAccountControlJustificationRepository,
                   cupsEntityContractDescriptionsRepository As ICupsEntityContractDescriptionsRepository)
        Me._stayRepository = stayRepository
        _bedRateRepository = bedRateRepository
        Me._billingDomainService = billingService
        _admissionRepository = admissionRepository
        Me._parameterRepository = parameterRepository
        Me._caregroupRepository = caregroupRepository
        Me._cupsEntityRepository = cupsEntityRepository
        _accountControlJustificationRepository = accountControlJustificationRepository
        _cupsEntityContractDescriptionsRepository = cupsEntityContractDescriptionsRepository
        _accountControlStayRepository = accountControlStayRepository
    End Sub

#End Region

#Region "Methods"
    Public Function ListOfStaysWithConfigurationByAdmissionToModel(
            admissionCode As String,
            stayOption As eLiquidateStayOption,
            medicalOrderDate As Date?,
            audit As AuditMessage,
            Optional endDate As Date? = Nothing) As List(Of StayInfoModel) Implements IStayService.ListOfStaysWithConfigurationByAdmissionToModel
        Dim admission = _admissionRepository.FirstOrDefault(Function(m) m.NUMINGRES = admissionCode)

        Dim stays As List(Of CHREGESTA) = ListOfStaysWithConfigurationByAdmission(admissionCode, stayOption, medicalOrderDate, endDate)

        Dim accountStays = _accountControlStayRepository.Query(Function(m) m.AdmissionCode = admissionCode).ToList()
        Dim startDate As Date

        Dim listIds = accountStays.Select(Function(w) w.Id).ToList()


        Dim listAccountControlJustification = _accountControlJustificationRepository.Query(Function(x) x.EntityName = NameOf(AccountControlStays) AndAlso
                                                                                               x.EntityTap = "INDLcgStays" AndAlso
                                                                                               listIds.Contains(x.EntityId),
                                                                                                False, {"BillingJustificationControl"}).ToList()

        If accountStays.Any() Then
            startDate = accountStays.Max(Function(m) m.StartDate).AddDays(1).Date
        ElseIf stays.Any() Then
            startDate = GetInitialDate(stays).Date
        End If

        If stays.Exists(Function(m) m.INPROFSAL Is Nothing) Then
            Throw New IndigoValidationException("Existe un registro con el código del profesional vacío.")
        End If

        Dim staysModel = MapStaysToModel(stays, startDate, audit.CodeUser, listAccountControlJustification)

        Dim lastStays = accountStays? _
            .Select(Function(m)
                        Dim regesta = _stayRepository.FirstOrDefault(Function(o) o.ID = m.StayId, False, {"CHTIPESTA", "CHCAMASHO.INUNIFUNC", "INPROFSAL"})
                        Dim cup = _cupsEntityRepository.FirstOrDefault(Function(o) o.Id = m.CupsId)
                        Dim cupsEntityContractDescriptions As CUPSEntityContractDescriptions = Nothing
                        If m.CUPSEntityContractDescriptionId.HasValue Then
                            cupsEntityContractDescriptions = _cupsEntityContractDescriptionsRepository.FirstOrDefault(Function(o) o.Id = m.CUPSEntityContractDescriptionId, False, {"ContractDescriptions"})
                        End If

                        Dim stayInfoModel As New StayInfoModel()
                        With stayInfoModel
                            .Selected = m.LiquidationDate IsNot Nothing
                            .AdmissionCode = m.AdmissionCode
                            .Bed = m.Bed
                            .InitialDate = m.StartDate
                            .EndDate = m.EndDate
                            .FunctionalUnitCode = m.FunctionalUnitCode
                            .FunctionalUnitName = regesta.CHCAMASHO.INUNIFUNC.UFUDESCRI
                            .LiquidationDate = m.LiquidationDate
                            .GENSERVICEORDER = m.ServiceOrderDetailId
                            .Quantity = 1
                            .StayInBed = (m.EndDate - m.StartDate).ToString()
                            .CUPSId = m.CupsId
                            .CUPsCodeName = $"{cup.Code} - {cup.Description}"
                            .CUPSEntityContractDescriptionId = m.CUPSEntityContractDescriptionId
                            .ContractDescriptionCodeName = $"{cupsEntityContractDescriptions?.ContractDescriptions?.Code} - {cupsEntityContractDescriptions?.ContractDescriptions?.Name}"
                            .ContractDescriptionId = cupsEntityContractDescriptions?.ContractDescriptionId
                            .StayTypeCode = regesta.CHTIPESTA.CODTIPEST
                            .StayTypeName = regesta.CHTIPESTA.DESTIPEST.Trim()
                            .Stay = regesta
                            .AccountControlStaysId = m.Id
                            Dim accountControlJustification = listAccountControlJustification?.Find(Function(x) x.EntityId = m.Id)
                            .ACJustificationId = accountControlJustification?.Id
                            .JustificationCodeName = $"{accountControlJustification?.BillingJustificationControl?.Code} - {accountControlJustification?.BillingJustificationControl?.Description}"
                            .SkipLiquidation = If(accountControlJustification Is Nothing, False, accountControlJustification?.BillingJustificationControl?.SkipClearance)
                        End With

                        Return stayInfoModel
                    End Function).ToList()

        Return staysModel.Union(lastStays).OrderByDescending(Function(m) m.InitialDate).ToList()
    End Function

    Private Function MapStaysToModel(stays As List(Of CHREGESTA),
                                     startDate As Date,
                                     userCode As String,
                                     Optional listAccountControlJustification As List(Of AccountControlJustification) = Nothing) As List(Of StayInfoModel)
        Dim _ini As Date?
        Dim model = stays?.OrderBy(Function(m) m.FECINIEST) _
            .SelectMany(Function(m)
                            Dim lst As New List(Of StayInfoModel)()

                            If Not _ini.HasValue Then
                                _ini = If(m.FECINIEST > startDate, m.FECINIEST, startDate)
                            End If

                            For i As Integer = 1 To m.TotalUnits
                                Dim _end As Date = If(
                                    m.FECFINEST.Year = 1900,
                                    _ini.Value.Date.AddDays(1).AddSeconds(-1),
                                    If(
                                        m.FECFINEST < _ini.Value.Date.AddDays(1).AddSeconds(-1),
                                        m.FECFINEST, _ini.Value.Date.AddDays(1).AddSeconds(-1)
                                    )
                                )

                                insertAccountControlStay(m.NUMINGRES.Trim(), m.ID,
                                    _ini, _end, m.CHCAMASHO.NUMCAMHOS.Trim(),
                                    m.CHCAMASHO.INUNIFUNC.UFUCODIGO,
                                    m.CupsId, m.CUPSEntityContractDescriptionId,
                                    m.INPROFSAL.CODPROSAL, m.CODESPECI, userCode
                                )

                                Dim cupsEntityContractDescriptions As CUPSEntityContractDescriptions = Nothing
                                If m.CUPSEntityContractDescriptionId.HasValue Then
                                    cupsEntityContractDescriptions = _cupsEntityContractDescriptionsRepository.FirstOrDefault(Function(o) o.Id = m.CUPSEntityContractDescriptionId, False, {"ContractDescriptions"})
                                End If

                                Dim time = _end - _ini
                                Dim stay As New StayInfoModel()
                                With stay
                                    .Selected = False
                                    .AdmissionCode = m.NUMINGRES.Trim()
                                    .Bed = m.CHCAMASHO.NUMCAMHOS.Trim()
                                    .InitialDate = _ini
                                    .EndDate = _end
                                    .FunctionalUnitCode = m.CHCAMASHO.INUNIFUNC.UFUCODIGO
                                    .FunctionalUnitName = m.CHCAMASHO.INUNIFUNC.UFUDESCRI
                                    .LiquidationDate = Nothing
                                    .Quantity = 1
                                    .StayInBed = time.ToString()
                                    .CUPSId = m.CupsId
                                    .CUPsCodeName = m.CUPsCodeName
                                    .CUPSEntityContractDescriptionId = m.CUPSEntityContractDescriptionId
                                    .ContractDescriptionCodeName = $"{cupsEntityContractDescriptions?.ContractDescriptions?.Code} - {cupsEntityContractDescriptions?.ContractDescriptions?.Name}"
                                    .ContractDescriptionId = cupsEntityContractDescriptions?.ContractDescriptionId
                                    .StayTypeCode = m.CHTIPESTA.CODTIPEST
                                    .StayTypeName = m.CHTIPESTA.DESTIPEST.Trim()
                                    .Stay = m
                                    Dim accountControlJustification = listAccountControlJustification?.Find(Function(x) x.Id = m.ID)
                                    .ACJustificationId = accountControlJustification?.Id
                                    .JustificationCodeName = $"{accountControlJustification?.BillingJustificationControl?.Code} - {accountControlJustification?.BillingJustificationControl?.Description}"
                                    .SkipLiquidation = If(accountControlJustification Is Nothing, False, accountControlJustification?.BillingJustificationControl?.SkipClearance)
                                End With

                                lst.Add(stay)
                                _ini = _ini.Value.Date.AddDays(1)
                            Next

                            Return lst
                        End Function)?.ToList()

        _accountControlStayRepository.UnitWork.Commit()

        Return model
    End Function

    Private Sub insertAccountControlStay(admissionCode As String,
            stayId As Integer,
            startDate As Date,
            endDate As Date,
            bed As String,
            functionalUnitCode As String,
            cupsId As Integer,
            CUPSEntityContractDescriptionId As Integer?,
            professionalCode As String,
            specialyCode As String,
            userCode As String)
        If Not _accountControlStayRepository.Any(Function(o) o.AdmissionCode = admissionCode _
                                                                         AndAlso o.StayId = stayId _
                                                                         AndAlso o.StartDate = startDate _
                                                                         AndAlso o.EndDate = endDate) Then

            Dim stayAccountControl As New AccountControlStays()

            With stayAccountControl
                .AdmissionCode = admissionCode
                .Bed = bed
                .StayId = stayId
                .FunctionalUnitCode = functionalUnitCode
                .CupsId = cupsId
                .CUPSEntityContractDescriptionId = CUPSEntityContractDescriptionId
                .ProfessionalCode = professionalCode
                .ProfessionalSpecialistCode = specialyCode
                .ServiceOrderDetailId = Nothing
                .StartDate = startDate
                .EndDate = endDate
                .LiquidationDate = Nothing
                .CreationUser = userCode
                .CreationDate = Date.Now
            End With

            _accountControlStayRepository.SaveEntity(stayAccountControl)
        End If
    End Sub

    Public Function ListOfStaysWithConfigurationByAdmission(
            admissionCode As String,
            stayOption As eLiquidateStayOption,
            medicalOrderDate As Date?,
            Optional endDate As Date? = Nothing) As List(Of CHREGESTA) Implements IStayService.ListOfStaysWithConfigurationByAdmission

        Dim admission = _admissionRepository.FirstOrDefault(Function(m) m.NUMINGRES = admissionCode)
        Dim careGroupId = admission.GENCAREGROUP

        Dim configurationStay = GetConfigurationStay(careGroupId)

        If stayOption <> eLiquidateStayOption.DefectoManualGrupoAtencion Then
            configurationStay.paymentType = stayOption
        End If

        Dim staysWithOutLiquidate As List(Of CHREGESTA) = GetUnliquidatedStays(admissionCode)

        If staysWithOutLiquidate Is Nothing Or staysWithOutLiquidate.Count() = 0 Then
            Return New List(Of CHREGESTA)()
        End If

        'se valida que No existan mas de una estancia sin egreso y se excepciona el proceso para que no genere ningun calculo
        Dim validateStays = staysWithOutLiquidate.OrderBy(Function(x) x.ID).ToList().FindAll(Function(e) e.FECFINEST < e.FECINIEST)
        If validateStays.Count > 1 Then
            Dim bedOrigin = validateStays.FirstOrDefault
            Dim bedTarget = validateStays.LastOrDefault
            Throw New IndigoValidationException($"El paciente con Ingreso {admissionCode} actualmente tiene 2 estancias activas en las camas (Código: {bedOrigin.CHCAMASHO.NUMCAMHOS.Trim()} - Unidad Funcional: {bedOrigin.CHCAMASHO.UFUCODIGO.Trim()}) y (Código: {bedTarget.CHCAMASHO.NUMCAMHOS.Trim()} - Unidad Funcional: {bedTarget.CHCAMASHO.UFUCODIGO.Trim()}).")
        End If

        Dim initDate As Date = GetInitialDate(staysWithOutLiquidate)

        Dim lastAccountStay = _accountControlStayRepository.Query(Function(m) m.AdmissionCode = admissionCode) _
            .OrderByDescending(Function(m) m.Id) _
            .FirstOrDefault()

        If lastAccountStay IsNot Nothing Then
            initDate = lastAccountStay.EndDate.AddDays(1).Date
        End If

        If endDate Is Nothing Then endDate = GetEndDate(staysWithOutLiquidate)

        ValidateRate(staysWithOutLiquidate)

        'Consultamos los parametros por el centro de atención
        Dim parameters = _parameterRepository.FirstOrDefault(Function(m) m.CODCENATE = admission.CODCENATE)

        If parameters Is Nothing Then Throw New IndigoValidationException($"No existen parámetros para el centro de atención ({admission.CODCENATE})")

        ' Solo tomamos las estancias que no han sido aplicadas
        Dim staysToCalculate = staysWithOutLiquidate.Where(Function(m) If(m.FECFINEST.Year = 1900, New Date(9999, 1, 1), m.FECFINEST) >= initDate).ToList()

        Dim listErrors As New List(Of String)()
        Dim diffBetweenDates As UnitStay = Utils.CalcUnitStay(initDate, endDate, parameters.GENHORACORTE)
        _dictionaryRateHigherValue.Clear()
        _dictionaryRateLastAdmission.Clear()

        If diffBetweenDates.Days = 0 AndAlso diffBetweenDates.Hours = 0 Then
            Return New List(Of CHREGESTA)()
        End If

        If diffBetweenDates.Days > 0 Then 'Si la diferencia es en días
            CalculateStayByDays(
                staysToCalculate,
                careGroupId:=careGroupId,
                initDate:=initDate,
                days:=diffBetweenDates.Days,
                medicalOrderDate:=medicalOrderDate,
                stayOption:=configurationStay.paymentType,
                endDate:=endDate,
                hoursOfRecoveryInluded:=configurationStay.minimumHours,
                listErrors:=listErrors,
                HoursObservation:=configurationStay.HoursObservation
            )
        End If

        Dim estanciaActiva = staysWithOutLiquidate.Any(Function(e) e.REGESTADO = 1)
        Dim dateToday As Date = GetDateToday()

        If ((configurationStay.liquidOutgoing OrElse (estanciaActiva AndAlso endDate < dateToday)) AndAlso diffBetweenDates.Hours > 0) _
           OrElse (lastAccountStay Is Nothing AndAlso diffBetweenDates.Days = 0 AndAlso diffBetweenDates.Hours > 0) Then
            CalculateStayByHours(
                stays:=staysToCalculate,
                careGroupId:=careGroupId,
                stayOption:=configurationStay.paymentType,
                hoursOfRecoveryInluded:=configurationStay.minimumHours,
                medicalOrderDate:=medicalOrderDate,
                endDate:=endDate,
                hours:=diffBetweenDates.Hours,
                listErrors:=listErrors,
                HoursObservation:=configurationStay.HoursObservation
            )
        End If

        'Si ocurrio algun error al calcular la estancia
        If listErrors.Any() Then
            Throw New IndigoValidationException(String.Join(vbCrLf, listErrors))
        End If

        Dim estancias = staysWithOutLiquidate.FindAll(Function(e) (e.FECINIEST <= endDate) AndAlso e.TotalUnits > 0)
        'Colocar las de cantidad cero tambien en liquidadas solo si nunca van a tener cantidad
        For Each est In estancias
            If If(est.FECFINEST < est.FECINIEST, Date.Now, est.FECFINEST) >= endDate Then
                If est.FECFINEST > est.FECINIEST Then 'Se liquida total
                    If est.FECFINEST >= endDate Then
                        Dim fechaValidar As Date = If(est.FECFINEST < est.FECINIEST, Date.Now, est.FECFINEST)
                        'verificamos si endDate mas un día se cobra en la estancia actual o en otra estancia
                        Dim estanciasQueCoincidenConFecha = staysWithOutLiquidate.FindAll(Function(m) m.FECINIEST <= fechaValidar AndAlso If(m.FECFINEST < m.FECINIEST, Date.Now, m.FECFINEST) >= fechaValidar)
                        Dim res = _billingDomainService.GetPriceToStaysList(careGroupId, estanciasQueCoincidenConFecha, stayOption)
                        If res.StateResult Then
                            Dim estanciaCobrada = res.ObjectEmbbeded.Where(Function(est1) est1.Value = res.ObjectEmbbeded.Max(Function(es) es.Value)).FirstOrDefault()

                            If estanciaCobrada.ID.Equals(est.ID) Then
                                If est.ID.Equals(staysWithOutLiquidate.LastOrDefault().ID) AndAlso est.FECFINEST > est.FECINIEST Then
                                    est.GENESTLIQ = 3
                                Else
                                    est.GENESTLIQ = 2
                                End If
                            Else
                                est.GENESTLIQ = 2
                            End If
                        Else
                            est.GENESTLIQ = 2 ' TODO:
                        End If
                    Else
                        est.GENESTLIQ = 3
                    End If
                Else 'Se liquida parcial
                    est.GENESTLIQ = 2
                End If
            Else
                est.GENESTLIQ = 3
            End If
            est.MarkAsModified()
        Next

        Return estancias
    End Function

    Private Sub CalculateStayByHours(
            ByRef stays As List(Of CHREGESTA),
            careGroupId As Integer,
            stayOption As eLiquidateStayOption,
            hoursOfRecoveryInluded As Integer,
            medicalOrderDate As Date?,
            endDate As Date?,
            hours As Integer,
            ByRef listErrors As List(Of String),
            HoursObservation As Integer
        )

        Dim cupsId As Integer = 0
        Dim cUPSEntityContractDescriptionId As Integer?
        Dim stay As CHREGESTA = Nothing
        Dim staysList = stays _
                .FindAll(Function(o) o.FECINIEST.Date <= endDate.Value.Date _
                    AndAlso New Date(
                        IIf(o.FECFINEST.Year = 1900, 9999, o.FECFINEST.Year), o.FECFINEST.Month, o.FECFINEST.Day, 23, 59, 59
                    ) >= endDate.Value.Date
                )

        ' validamos que se cumplan las horas minimas de estancia
        If staysList.Any() Then
            Dim min = GetMinValueDate(staysList, endDate.Value)
            Dim max = GetMaxValueDate(staysList, endDate.Value)
            Dim medicalOrderHCHISPACA As Date?

            If max.Subtract(min).TotalHours >= hoursOfRecoveryInluded Then
                If medicalOrderDate Is Nothing Then 'Si no tiene orden medica se cobra observación
                    If stayOption = eLiquidateStayOption.MayorValor AndAlso staysList.TrueForAll(Function(e) e.CHCAMASHO.CODCLACAM = 1) Then

                        'si existe orden medica y la diferencia entre la orden y la fecha hasta las 23.59 es mayor a las horas de observacion, busco Hospitalizacion sino Observacion
                        'ejemplo : diff(2023-10-25 5 am vs 2023-10-25 23.59) > 2h entonces es HOSPI SINO OBSERVACION
                        If medicalOrderHCHISPACA Is Nothing Then
                            medicalOrderHCHISPACA = _stayRepository.GetFirstHCHISPACAByAdmissionNumber(stays?.FirstOrDefault?.NUMINGRES)?.FECHISPAC
                        End If

                        Dim collectStay = If(medicalOrderHCHISPACA Is Nothing, False, DateDiff(DateInterval.Hour, medicalOrderHCHISPACA.Value, New Date(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59)) > HoursObservation)

                        Dim res = getStayHigherValue(staysList, careGroupId, stayOption, collectStay)

                        If res.errors.Any() Then
                            listErrors.AddRange(res.errors)
                        End If

                        cupsId = res.cupsId
                        cUPSEntityContractDescriptionId = res.cUPSEntityContractDescriptionId
                        stay = res.stay
                    Else 'Si tiene orden medica se cobra estancia
                        'Obtenemos los precios para cada uno de los servicios en las estancias
                        Dim res = getStayLastAdmissionBed(staysList, careGroupId, stayOption, True, IIf(stayOption = eLiquidateStayOption.CamaUltimoIngreso, Nothing, endDate))

                        If res.errors.Any() Then
                            listErrors.AddRange(res.errors)
                        End If

                        cupsId = res.cupsId
                        cUPSEntityContractDescriptionId = res.cUPSEntityContractDescriptionId
                        stay = res.stay
                    End If
                Else
                    'Obtenemos los precios para cada uno de los servicios en las estancias
                    Dim res = getStayLastAdmissionBed(staysList, careGroupId, stayOption, True, endDate)

                    If res.errors.Any() Then
                        listErrors.AddRange(res.errors)
                    End If

                    cupsId = res.cupsId
                    cUPSEntityContractDescriptionId = res.cUPSEntityContractDescriptionId
                    stay = res.stay
                End If

                If cupsId = 0 Then
                    Dim msgerror = "No se encontró un cups asociado a la estancia"

                    If Not listErrors.Contains(msgerror) Then
                        listErrors.Add(msgerror)
                    End If
                Else
                    stay.CupsId = cupsId
                    stay.CUPSEntityContractDescriptionId = cUPSEntityContractDescriptionId
                    stay.TotalUnits += 1
                End If
            End If
        End If
    End Sub

    Private Function GetMaxValueDate(staysList As List(Of CHREGESTA), dateValue As Date) As Date
        Return staysList.Max(Function(m)
                                 If m.FECFINEST.Date > dateValue.Date OrElse m.FECFINEST.Year = 1900 Then
                                     Return dateValue.Date.AddDays(1)
                                 End If

                                 Return m.FECFINEST
                             End Function)
    End Function

    Private Function GetMinValueDate(stays As List(Of CHREGESTA), dateValue As Date) As Date
        Dim min = stays.Min(Function(m) m.FECINIEST)

        If dateValue.Date > min.Date Then
            min = dateValue.Date
        End If

        Return min
    End Function

    Private Sub CalculateStayByDays(
            ByRef stayList As List(Of CHREGESTA),
            careGroupId As Integer,
            initDate As Date,
            days As Integer,
            medicalOrderDate As Date?,
            stayOption As eLiquidateStayOption,
            endDate As Date?,
            hoursOfRecoveryInluded As Integer,
            ByRef listErrors As List(Of String),
            HoursObservation As Integer)

        Dim cupsId As Integer = 0
        Dim cUPSEntityContractDescriptionId As Integer?
        Dim stay As CHREGESTA = Nothing
        Dim nextDate As Date = initDate.Date

        For day As Integer = 1 To days
            Dim medicalOrderHCHISPACA As Date?
            ' Tomar las estancias para el día que cumpla con las horas minimas de estancia
            Dim stays = stayList _
                .FindAll(Function(o) o.FECINIEST.Date <= nextDate _
                    AndAlso New Date(
                        IIf(o.FECFINEST.Year = 1900, 9999, o.FECFINEST.Year), o.FECFINEST.Month, o.FECFINEST.Day, 23, 59, 59
                    ) >= nextDate
                )

            If stays.Any() Then
                Dim min = GetMinValueDate(stays, nextDate)
                Dim max = GetMaxValueDate(stays, nextDate)

                If max.Subtract(min).TotalHours >= hoursOfRecoveryInluded Then
                    ' validamos que se cumplan las horas minimas de estancia

                    If medicalOrderDate Is Nothing Then 'Si no tiene orden medica
                        If stayOption = eLiquidateStayOption.MayorValor AndAlso stays.TrueForAll(Function(e) e.CHCAMASHO.CODCLACAM = 1) Then
                            'Si todas son de observación o urgencias
                            'si existe orden medica y la diferencia entre la orden y la fecha hasta las 23.59 es mayor a las horas de observacion, busco Hospitalizacion sino Observacion
                            'ejemplo : diff(2023-10-25 5 am vs 2023-10-25 23.59) > 2h entonces es HOSPI SINO OBSERVACION
                            If medicalOrderHCHISPACA Is Nothing Then
                                medicalOrderHCHISPACA = _stayRepository.GetFirstHCHISPACAByAdmissionNumber(stays?.FirstOrDefault?.NUMINGRES)?.FECHISPAC
                            End If

                            Dim collectStay = If(medicalOrderHCHISPACA Is Nothing, False, DateDiff(DateInterval.Hour, medicalOrderHCHISPACA.Value, New Date(nextDate.Year, nextDate.Month, nextDate.Day, 23, 59, 59)) > HoursObservation)

                            Dim res = getStayHigherValue(stays, careGroupId, stayOption, collectStay)

                            If res.errors.Any() Then
                                listErrors.AddRange(res.errors)
                            End If

                            cupsId = res.cupsId
                            cUPSEntityContractDescriptionId = res.cUPSEntityContractDescriptionId
                            stay = res.stay
                        Else
                            Dim res = getStayLastAdmissionBed(stays, careGroupId, stayOption, True,
                            IIf(stayOption = eLiquidateStayOption.CamaUltimoIngreso, Nothing, endDate)
                        )

                            If res.errors.Any() Then
                                listErrors.AddRange(res.errors)
                            End If

                            cupsId = res.cupsId
                            cUPSEntityContractDescriptionId = res.cUPSEntityContractDescriptionId
                            stay = res.stay
                        End If
                    Else 'Si tiene orden medica
                        Dim collectStay = medicalOrderDate.Value > nextDate 'Aqui cobro una unidad de observación
                        Dim res = getStayLastAdmissionBed(stays, careGroupId, stayOption, Not collectStay, Nothing)

                        If res.errors.Any() Then
                            listErrors.AddRange(res.errors)
                        End If

                        cupsId = res.cupsId
                        cUPSEntityContractDescriptionId = res.cUPSEntityContractDescriptionId
                        stay = res.stay
                    End If

                    If cupsId = 0 Then
                        Dim msgerror = "No se encontró un cups asociado a la estancia"

                        If Not listErrors.Contains(msgerror) Then
                            listErrors.Add(msgerror)
                        End If
                    Else
                        stay.CupsId = cupsId
                        stay.CUPSEntityContractDescriptionId = cUPSEntityContractDescriptionId
                        stay.TotalUnits += 1
                    End If

                End If
            End If

            nextDate = nextDate.AddDays(1)
        Next
    End Sub

    Private _dictionaryRateLastAdmission As New Dictionary(Of String, (cupsId As Integer, cUPSEntityContractDescriptionId As Integer?, stay As CHREGESTA, errors As List(Of String)))

    Private Function getStayLastAdmissionBed(
                stays As List(Of CHREGESTA),
                careGroupId As Integer,
                stayOption As eLiquidateStayOption,
                collectStay As Boolean,
                endDate As Date?
            ) As (cupsId As Integer, cUPSEntityContractDescriptionId As Integer?, stay As CHREGESTA, errors As List(Of String))
        Dim key = $"{careGroupId}_{String.Join("-", stays.Select(Function(m) m.ID).ToArray())}_{stayOption}_{collectStay}_{If(endDate Is Nothing, "", endDate.Value.ToString("yyyy_MM_dd_HH_mm_ss"))}"

        If _dictionaryRateLastAdmission.ContainsKey(key) Then Return _dictionaryRateLastAdmission(key)

        Dim cupsId As Integer = 0
        Dim cUPSEntityContractDescriptionId As Integer?
        Dim stay As CHREGESTA = Nothing
        Dim listErrors As New List(Of String)()

        Dim res = Me._billingDomainService.GetPriceToStaysList(careGroupId, stays, stayOption, collectStay, endDate)
        If res.StateResult Then
            stay = res.ObjectEmbbeded.FirstOrDefault(Function(o) o.Value = res.ObjectEmbbeded.Max(Function(m) m.Value))

            If stay IsNot Nothing Then
                cupsId = stay.CupsId
                cUPSEntityContractDescriptionId = stay.CUPSEntityContractDescriptionId

                If cupsId > 0 Then
                    Dim cups = _cupsEntityRepository.FirstOrDefault(Function(m) m.Id = cupsId)
                    stay.CUPsCodeName = $"{cups.Code} - {cups.Description}"
                End If
            End If
        Else 'Aqui hubo errores
            If Not String.IsNullOrEmpty(res.Message) AndAlso Not listErrors.Contains(res.Message) Then
                listErrors.Add(res.Message)
            End If

            If res.MessageResult IsNot Nothing Then
                listErrors.AddRange(res.MessageResult.FindAll(Function(m) Not listErrors.Contains(m)))
            End If
        End If

        Dim result = (cupsId, cUPSEntityContractDescriptionId, stay, listErrors)
        _dictionaryRateLastAdmission.Add(key, result)

        Return result
    End Function

    Private _dictionaryRateHigherValue As New Dictionary(Of String, (cupsId As Integer, cUPSEntityContractDescriptionId As Integer?, stay As CHREGESTA, errors As List(Of String)))

    Private Function getStayHigherValue(stays As List(Of CHREGESTA),
                                        careGroupId As Integer,
                                        stayOption As eLiquidateStayOption, Optional collectStay As Boolean = False) As (cupsId As Integer, cUPSEntityContractDescriptionId As Integer?, stay As CHREGESTA, errors As List(Of String))
        Dim key = $"{careGroupId}_{String.Join("-", stays.Select(Function(m) m.ID).ToArray())}_{stayOption}"

        If _dictionaryRateHigherValue.ContainsKey(key) Then Return _dictionaryRateHigherValue(key)

        Dim cupsId As Integer = 0
        Dim cUPSEntityContractDescriptionId As Integer?
        Dim stay As CHREGESTA = Nothing
        Dim listErrors As New List(Of String)()

        Dim tariCurrentEst = stays(0).CHCAMASHO.CHGENTARI _
                            .FirstOrDefault(Function(tar) tar.CHTIPESTA.CODTIPEST.Equals(stays(0).CHTIPESTA.CODTIPEST) _
                                    AndAlso tar.TIPLIQEST = stays(0).CHCAMASHO.CODCLACAM)
        Dim res = _billingDomainService.GetPriceToStaysList(careGroupId, stays, stayOption, False)

        If res.StateResult Then
            cupsId = If(collectStay, tariCurrentEst.GENCUPS2, tariCurrentEst.GENCUPS.Value)
            stay = stays.FirstOrDefault()

            If cupsId > 0 Then
                Dim cups = _cupsEntityRepository.FirstOrDefault(Function(m) m.Id = cupsId)
                stay.CUPsCodeName = $"{cups.Code} - {cups.Description}"
            End If

            Dim estnr = res.ObjectEmbbeded.FirstOrDefault(Function(est) est.Value = res.ObjectEmbbeded.Max(Function(es) es.Value))

            If estnr IsNot Nothing AndAlso estnr.CupsId = cupsId Then
                cUPSEntityContractDescriptionId = estnr.CUPSEntityContractDescriptionId
            Else
                cUPSEntityContractDescriptionId = If(collectStay, tariCurrentEst?.IDDESCRIPCIONRELACIONADA_CUPS2, tariCurrentEst?.IDDESCRIPCIONRELACIONADA_CUPS)
            End If

        Else 'Aqui hubo errores
            If Not String.IsNullOrEmpty(res.Message) AndAlso Not listErrors.Contains(res.Message) Then
                listErrors.Add(res.Message)
            End If

            If res.MessageResult IsNot Nothing Then
                listErrors.AddRange(res.MessageResult.FindAll(Function(m) Not listErrors.Contains(m)))
            End If
        End If

        Dim result = (cupsId, cUPSEntityContractDescriptionId, stay, listErrors)
        _dictionaryRateHigherValue.Add(key, result)

        Return result
    End Function

    Private Sub ValidateCurrentDateStay(stays As List(Of CHREGESTA), currentDate As Date)
        Dim existValidStays = stays.Any(Function(o) o.FECINIEST.Date <= currentDate _
            And New DateTime(
                IIf(o.FECFINEST.Year = 1900, 9999, o.FECFINEST.Year),
                o.FECFINEST.Month,
                o.FECFINEST.Day, 23, 59, 59
            ) >= currentDate)

        'Validamos que las estancias no tengan intervalos vacios entre ellas
        If Not existValidStays Then
            Throw New IndigoValidationException("Existe un intervalo de tiempo irregular entre las estancias.")
        End If
    End Sub

    Private Sub ValidateRate(stays As List(Of CHREGESTA))
        Dim errors As New StringBuilder()

        For Each est In stays
            'If Not _bedRateRepository.Any(Function(m) m.CODICAMAS = est.CODICAMAS AndAlso m.CHTIPESTA.CODTIPEST = est.CHTIPESTA.CODTIPEST) Then
            If Not est.CHCAMASHO.CHGENTARI.Any(Function(tar) tar.CHTIPESTA.CODTIPEST.Equals(est.CHTIPESTA.CODTIPEST)) Then
                errors.AppendLine($"Falta configurar la tarifa de la cama ({est.CHCAMASHO.NUMCAMHOS.Trim()}) para el tipo de estancia ({est.CHTIPESTA.CODTIPEST} - {est.CHTIPESTA.DESTIPEST})")
            End If
        Next

        If errors.Length > 0 Then Throw New IndigoValidationException(errors.ToString()) With {.Source = "999"}
    End Sub

    Private Function GetEndDate(stays As List(Of CHREGESTA)) As Date
        Dim endDate As Date
        'se valida si exiten un registro donde la fecha final sea menor a la inicial 
        ' esto sucede porque por defecto el EHR guarda 01-01-1900 cuando no existe fecha fin
        If stays?.Any(Function(x) x.FECFINEST < x.FECINIEST) Then
            'Si esta abierto, se toma la fecha actual
            Dim hoy = Date.Now.AddDays(-1)
            endDate = New Date(hoy.Year, hoy.Month, hoy.Day, 23, 59, 59)
        Else
            endDate = stays.Max(Function(o) o.FECFINEST)
        End If

        Return endDate
    End Function

    Private Function GetInitialDate(stays As List(Of CHREGESTA)) As Date
        Dim initDate = stays.Min(Function(o) o.FECINIEST)
        Return initDate
    End Function

    Private Function GetDateToday() As Date
        Dim dateToday = Date.Now
        dateToday = New Date(dateToday.Year, dateToday.Month, dateToday.Day, 23, 59, 59)
        Return dateToday
    End Function

    Private Function GetUnliquidatedStays(admissionCode As String) As List(Of CHREGESTA)
        Dim stays = _stayRepository.Query(Function(m) m.NUMINGRES = admissionCode _
            AndAlso (m.GENESTLIQ = 1 OrElse m.GENESTLIQ = 2 OrElse m.GENESTLIQ Is Nothing) _
            , tracking:=True _
            , includes:={"CHREGESTADET", "CHCAMASHO.CHGENTARI.CHTIPESTA", "CHCAMASHO.CHGENTARI.ADCENATEN", "CHCAMASHO.INUNIFUNC", "CHTIPESTA", "INPROFSAL"}
        ) _
        .OrderBy(Function(m) m.FECINIEST) _
        .ToList()

        Return stays
    End Function

    Private Function GetConfigurationStay(careGroupId As Integer) As (paymentType As eLiquidateStayOption, minimumHours As Integer, liquidOutgoing As Boolean, HoursObservation As Integer)
        Dim caregroup = _caregroupRepository.FirstOrDefault(Function(m) m.Id = careGroupId, tracking:=False)

        If caregroup Is Nothing Then Throw New IndigoValidationException("No se encontró el grupo de atención seleccionado")

        Dim liquidationType As eLiquidateStayOption
        Dim hoursObservation As Integer = 0

        If caregroup.TypeLiquidationEmergencyStays = 1 Then
            liquidationType = eLiquidateStayOption.MayorValor
            hoursObservation = caregroup.MinimumObservationTime
        Else
            liquidationType = eLiquidateStayOption.CamaUltimoIngreso
            hoursObservation = caregroup.MaximumObservationTime
        End If

        Return (liquidationType, caregroup.HoursOfRecoveryIncluded, caregroup.LiquidateDayDischarge, hoursObservation)
    End Function

    ''' <summary>
    ''' Lista las estancias que se encuentran sin liquidar, con el calculo de las unidades
    ''' que le corresponde a cada estancias hasta la fecha dada
    ''' </summary>
    ''' <param name="admissionCode"></param>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="medicalOrderDate">Fecha de la orden medica</param>
    ''' <param name="endDate">Fecha limite a liquidar</param>
    ''' <param name="asNoTracking">Valor que indica si se consulta las entidades con seguimiento</param>
    ''' <returns>Lista de estancias sin liquidar, liquidadas hasta la fecha dada</returns>
    Public Function ListDontLiquidatedStays(admissionCode As String, caregroupId As Integer, stayOption As Domain.Entities.eLiquidateStayOption,
                                            medicalOrderDate As Date?, Optional endDate As Date? = Nothing,
                                            Optional asNoTracking As Boolean = True) As ActionResult(Of List(Of CHREGESTA)) Implements IStayService.ListDontLiquidatedStays
        Dim errorList As New StringBuilder()
        Dim listStays As New List(Of CHREGESTA)()
        Dim listStaysLiquidada As New List(Of CHREGESTA)()
        Dim result As New ActionResult(Of List(Of CHREGESTA))() With
        {
            .Message = String.Empty,
            .MessageResult = New List(Of String)(),
            .StateResult = False
        }

        Try
#Region "Initial Data"
            'Consultamos el grupo de atención
            Dim objCaregroup As CareGroup = Me._caregroupRepository.FirstOrDefault(Function(m) m.Id = caregroupId)
            If objCaregroup Is Nothing OrElse objCaregroup.Id = 0 Then
                result.Message = "No se encontró el grupo de atención seleccionado"
                Return result
            End If

            If stayOption = eLiquidateStayOption.DefectoManualGrupoAtencion Then
                If objCaregroup.TypeLiquidationEmergencyStays = 1 Then
                    'IIS
                    stayOption = eLiquidateStayOption.MayorValor
                ElseIf objCaregroup.TypeLiquidationEmergencyStays = 2 Then
                    'SOAT
                    stayOption = eLiquidateStayOption.CamaUltimoIngreso
                End If
            End If

            'Consulto las estancias sin liquidar, donde su fecha final sea menor o igual a la fecha final especificada
            listStays = Me._stayRepository.ListDontLiquidatedStaysByAdmissionCode(admissionCode, asNoTracking)
            'Se consultan las estancias liquidadas
            listStaysLiquidada = Me._stayRepository.ListLiquidatedStaysByAdmissionCode(admissionCode, asNoTracking)
            For Each est In listStaysLiquidada
                'Se eliminan las estancias que correspondan a la fecha de la estancia liquidada.
                listStays.RemoveAll(Function(o) o.FECFINEST.Date >= est.FECFINEST.ToString("yyyy-MM-dd") And o.FECFINEST.Date <= est.FECFINEST.ToString("yyyy-MM-dd"))
            Next

            If listStays.Count = 0 Then 'No ha estancias por liquidar
                'result.Message = ResourceManager.GetString("StaysDontExists", MODULE_NAME)
                Return result
            End If

            'Se debe retornar las estancias consultadas aun así ocurra un error validable
            result.ObjectEmbbeded = listStays

            'Validamos tarifas
            For Each est In listStays
                If Not est.CHCAMASHO.CHGENTARI.Any(Function(tar) tar.CHTIPESTA.CODTIPEST.Equals(est.CHTIPESTA.CODTIPEST)) Then
                    errorList.AppendLine(String.Format("Falta configurar la tarifa de la cama ({0}) para el tipo de estancia ({1} - {2})", est.CHCAMASHO.NUMCAMHOS.Trim(), est.CHTIPESTA.CODTIPEST, est.CHTIPESTA.DESTIPEST))
                End If
            Next
            If errorList.Length > 0 Then
                result.Message = errorList.ToString() 'Faltan tarifas por configurar en algunas camas
                Return result
            End If

            'Consultamos los parametros por el centro de atención
            Dim parameters As CHPARAMET = Me._parameterRepository.GetParameterByAttentionCenter(listStays(0).ADINGRESO.CODCENATE)
            If parameters.CODCENATE Is Nothing OrElse parameters.CODCENATE.Trim().Equals(String.Empty) Then
                result.Message = String.Format("No existen parámetros para el centro de atención ({0})", listStays(0).ADINGRESO.CODCENATE)
                Return result
            End If

            'Aqui inicia el calculo de estancias

            'Fecha inicial de liquidación
            Dim LastLiquidateStay = Me._stayRepository.GetByFilter(Function(x) x.NUMINGRES = admissionCode And x.GENESTLIQ = 3, False).OrderByDescending(Function(d) d.FECFINEST).FirstOrDefault?.FECFINEST
            ' Dim initDate As Date = If(listStays(0).ADINGRESO.GENULTLIQUI Is Nothing, listStays.Min(Function(o) o.FECINIEST), listStays(0).ADINGRESO.GENULTLIQUI.Value )
            Dim initDate As Date = New Date
            If listStays(0).ADINGRESO.GENULTLIQUI Is Nothing Then
                initDate = listStays.Min(Function(o) o.FECINIEST)
            ElseIf LastLiquidateStay IsNot Nothing AndAlso listStays(0).ADINGRESO.GENULTLIQUI.Value < LastLiquidateStay Then
                initDate = LastLiquidateStay.Value
            Else
                initDate = listStays(0).ADINGRESO.GENULTLIQUI.Value
            End If

            If listStays(0).ADINGRESO.GENULTLIQUI IsNot Nothing Then
                initDate = New Date(initDate.Year, initDate.Month, initDate.Day, 23, 59, 59)
            End If

            'Fecha final de liquidación: Si es nula, se mira la ultimo estancia y si esta cerrada, se toma su fecha final, sino se toma la fecha actual
            If endDate Is Nothing Then
                If listStays.LastOrDefault().FECFINEST < listStays.LastOrDefault().FECINIEST Then 'Si esta abierto, se toma la fecha actual
                    Dim hoy = DateTime.Now.AddDays(-1)
                    endDate = New Date(hoy.Year, hoy.Month, hoy.Day, 23, 59, 59)
                Else
                    endDate = listStays.LastOrDefault().FECFINEST
                End If
            Else
                endDate = New Date(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59)
            End If
#End Region

            'Obtenemos la diferencia entre las fechas
            Dim diffBetweenDates As UnitStay = Utils.CalcUnitStay(initDate, endDate, parameters.GENHORACORTE)

            'variables para el calculo de las estancia
            Dim cupsId As Integer = 0
            Dim CUPSEntityContractDescriptionId As Integer?
            Dim estn As CHREGESTA = Nothing
            Dim listErrors As New List(Of String)()
            Dim listEstanciaCobradaPorDia As New Dictionary(Of DateTime, String)()

            If diffBetweenDates.Days > 0 Then 'Si la diferencia es en días
                Dim dateTmp As Date = initDate.Date
                If listStays(0).ADINGRESO.GENULTLIQUI IsNot Nothing Then
                    dateTmp = dateTmp.AddDays(1)
                End If

                For i As Integer = 1 To diffBetweenDates.Days
                    If i >= 30 Then Exit For

                    Dim staysList = listStays.FindAll(Function(o) o.FECINIEST.Date <= dateTmp _
                                AndAlso New Date(
                                    IIf(o.FECFINEST.Year = 1900, 9999, o.FECFINEST.Year), o.FECFINEST.Month, o.FECFINEST.Day, 23, 59, 59) >= dateTmp
                                )

                    'Validamos que las estancias no tengan intervalos vacios entre ellas
                    If Not staysList.Any() Then
                        result.Message = "Existe un intervalo de tiempo irregular entre las estancias."
                        Return result
                    End If

                    If medicalOrderDate Is Nothing Then 'Si no tiene orden medica

                        If objCaregroup.TypeLiquidationEmergencyStays = 1 Then 'ISS

                            If staysList.TrueForAll(Function(e) e.CHCAMASHO.CODCLACAM = 1) Then
                                'Si todas son de observación o urgencias

                                Dim tariCurrentEst = staysList(0).CHCAMASHO.CHGENTARI _
                                    .FirstOrDefault(Function(tar) tar.CHTIPESTA.CODTIPEST.Equals(staysList(0).CHTIPESTA.CODTIPEST) _
                                            AndAlso tar.TIPLIQEST = staysList(0).CHCAMASHO.CODCLACAM)

                                Dim res = Me._billingDomainService.GetPriceToStaysList(caregroupId, staysList, False)
                                If res.StateResult Then
                                    cupsId = tariCurrentEst.GENCUPS.Value
                                    estn = staysList(0)

                                    Dim estnr = res.ObjectEmbbeded.Where(Function(est) est.Value = res.ObjectEmbbeded.Max(Function(es) es.Value)).FirstOrDefault()
                                    If estnr IsNot Nothing Then
                                        CUPSEntityContractDescriptionId = estnr.CUPSEntityContractDescriptionId
                                    End If
                                Else 'Aqui hubo errores
                                    If Not String.IsNullOrEmpty(res.Message) Then
                                        If Not listErrors.Contains(res.Message) Then
                                            listErrors.Add(res.Message)
                                            errorList.AppendLine(res.Message)
                                        End If
                                    End If

                                    If res.MessageResult IsNot Nothing Then
                                        For Each message In res.MessageResult
                                            If Not listErrors.Contains(message) Then
                                                listErrors.Add(message)
                                                errorList.AppendLine(message)
                                            End If
                                        Next
                                    End If
                                End If
                            Else 'Si no, es igual que SOAT
                                Dim res = Me._billingDomainService.GetPriceToStaysList(caregroupId, staysList, stayOption, True, endDate)
                                If res.StateResult Then
                                    estn = res.ObjectEmbbeded.Where(Function(est) est.Value = res.ObjectEmbbeded.Max(Function(es) es.Value)).FirstOrDefault()
                                    If estn IsNot Nothing Then
                                        cupsId = estn.CupsId
                                        CUPSEntityContractDescriptionId = estn.CUPSEntityContractDescriptionId
                                    Else
                                        Continue For
                                    End If
                                Else 'Aqui hubo errores
                                    If Not String.IsNullOrEmpty(res.Message) Then
                                        If Not listErrors.Contains(res.Message) Then
                                            listErrors.Add(res.Message)
                                            errorList.AppendLine(res.Message)
                                        End If
                                    End If

                                    If res.MessageResult IsNot Nothing Then
                                        For Each message In res.MessageResult
                                            If Not listErrors.Contains(message) Then
                                                listErrors.Add(message)
                                                errorList.AppendLine(message)
                                            End If
                                        Next
                                    End If
                                End If
                            End If
                        Else 'SOAT
                            'Obtenemos los precios para cada uno de los servicios en las estancias
                            Dim res = Me._billingDomainService.GetPriceToStaysList(caregroupId, staysList, stayOption)
                            If res.StateResult Then
                                If res.ObjectEmbbeded IsNot Nothing AndAlso res.ObjectEmbbeded.Count > 0 Then
                                    estn = res.ObjectEmbbeded.Where(Function(est) est.Value = res.ObjectEmbbeded.Max(Function(es) es.Value)).FirstOrDefault()
                                    If estn IsNot Nothing Then
                                        cupsId = estn.CupsId
                                        CUPSEntityContractDescriptionId = estn.CUPSEntityContractDescriptionId
                                    End If
                                End If
                            Else 'Aqui hubo errores
                                If Not String.IsNullOrEmpty(res.Message) Then
                                    If Not listErrors.Contains(res.Message) Then
                                        listErrors.Add(res.Message)
                                        errorList.AppendLine(res.Message)
                                    End If
                                End If

                                If res.MessageResult IsNot Nothing Then
                                    For Each message In res.MessageResult
                                        If Not listErrors.Contains(message) Then
                                            listErrors.Add(message)
                                            errorList.AppendLine(message)
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    Else 'Si tiene orden medica
                        If (medicalOrderDate.Value > dateTmp) Then 'Aqui cobro una unidad de observación
                            'Obtenemos la tarifa de la estancia actual
                            Dim res = Me._billingDomainService.GetPriceToStaysList(caregroupId, staysList, stayOption, False)

                            If res.StateResult Then
                                estn = res.ObjectEmbbeded.Where(Function(est) est.Value = res.ObjectEmbbeded.Max(Function(es) es.Value)).FirstOrDefault()
                                If estn IsNot Nothing Then
                                    cupsId = estn.CupsId
                                    CUPSEntityContractDescriptionId = estn.CUPSEntityContractDescriptionId
                                End If
                            Else 'Aqui hubo errores
                                If Not String.IsNullOrEmpty(res.Message) Then
                                    If Not listErrors.Contains(res.Message) Then
                                        listErrors.Add(res.Message)
                                        errorList.AppendLine(res.Message)
                                    End If
                                End If

                                If res.MessageResult IsNot Nothing Then
                                    For Each message In res.MessageResult
                                        If Not listErrors.Contains(message) Then
                                            listErrors.Add(message)
                                            errorList.AppendLine(message)
                                        End If

                                    Next
                                End If
                            End If
                        Else 'Si es hospitalización
                            'Obtenemos los precios para cada uno de los servicios en las estancias
                            Dim res = Me._billingDomainService.GetPriceToStaysList(caregroupId, staysList, stayOption)
                            If res.StateResult Then
                                estn = res.ObjectEmbbeded.Where(Function(est) est.Value = res.ObjectEmbbeded.Max(Function(es) es.Value)).FirstOrDefault()
                                If estn IsNot Nothing Then
                                    cupsId = estn.CupsId
                                    CUPSEntityContractDescriptionId = estn.CUPSEntityContractDescriptionId
                                End If
                            Else 'Aqui hubo errores
                                If Not String.IsNullOrEmpty(res.Message) Then
                                    If Not listErrors.Contains(res.Message) Then
                                        listErrors.Add(res.Message)
                                        errorList.AppendLine(res.Message)
                                    End If
                                End If

                                If res.MessageResult IsNot Nothing Then
                                    For Each message In res.MessageResult
                                        If Not listErrors.Contains(message) Then
                                            listErrors.Add(message)
                                            errorList.AppendLine(message)
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If

                    If cupsId = 0 Then
                        Dim message = "No se encontró un cups asociado a la estancia"
                        If Not listErrors.Contains(message) Then
                            listErrors.Add(message)
                            errorList.AppendLine(message)
                        End If
                    Else 'Consultamos el CUPS para obtener el nombre
                        Dim resCups As CUPSEntity = Me._cupsEntityRepository.GetCupsEntityById(cupsId)
                        estn.CupsId = cupsId
                        'Se asigna valores
                        If estn.CHREGESTADET.Any(Function(det) det.GENCUPSLIQ = cupsId AndAlso det.ChangeTracker.State = ObjectState.Added) Then
                            estn.CHREGESTADET.Where(Function(det) det.GENCUPSLIQ = cupsId AndAlso det.ChangeTracker.State = ObjectState.Added).First().CANTIDADLIQ += 1
                        Else
                            Dim estnDet As New CHREGESTADET()
                            estnDet.GENLIQUIDA = endDate
                            estnDet.GENULTFEC = estn.ADINGRESO.GENULTLIQUI
                            estnDet.GENCUPSLIQ = cupsId
                            estnDet.CodeNameCups = (resCups.Code & "-" & resCups.Description)
                            estnDet.CANTIDADLIQ = 1
                            estnDet.Liquidated = False
                            estn.CHREGESTADET.Add(estnDet)
                        End If
                        estn.TotalUnits += 1
                        For Each det In estn.CHREGESTADET.Where(Function(d) d.CodeNameCups Is Nothing OrElse d.CodeNameCups.Equals(String.Empty)).ToList()
                            resCups = Me._cupsEntityRepository.GetCupsEntityById(det.GENCUPSLIQ)
                            det.CodeNameCups = (resCups.Code & "-" & resCups.Description)
                            det.Liquidated = If(det.ID > 0, True, False)
                        Next
                    End If

                    'Aumentamos un dia en la fecha temporal
                    dateTmp = dateTmp.AddDays(1)
                Next
            ElseIf diffBetweenDates.Hours > 0 Then 'Si la diferencia es en horas
                If objCaregroup.TypeLiquidationEmergencyStays = 1 Then 'ISS
                    If medicalOrderDate Is Nothing Then 'Si no tiene orden medica se cobra observación
                        Dim stayList = listStays.FindAll(Function(o) endDate >= o.FECINIEST AndAlso endDate <= o.FECFINEST)
                        If stayList.Any() Then
                            Dim res = Me._billingDomainService.GetPriceToStaysList(caregroupId, New List(Of CHREGESTA)({stayList(stayList.Count - 1)}), stayOption, True, endDate)
                            If res.StateResult Then
                                estn = res.ObjectEmbbeded.Where(Function(est) est.Value = res.ObjectEmbbeded.Max(Function(es) es.Value)).FirstOrDefault()
                                If estn IsNot Nothing Then
                                    cupsId = estn.CupsId
                                    CUPSEntityContractDescriptionId = estn.CUPSEntityContractDescriptionId
                                End If
                            Else 'Aqui hubo errores
                                If Not String.IsNullOrEmpty(res.Message) Then
                                    If Not listErrors.Contains(res.Message) Then
                                        listErrors.Add(res.Message)
                                        errorList.AppendLine(res.Message)
                                    End If
                                End If

                                If res.MessageResult IsNot Nothing Then
                                    For Each message In res.MessageResult
                                        If Not listErrors.Contains(message) Then
                                            listErrors.Add(message)
                                            errorList.AppendLine(message)
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    Else 'Si tiene orcen medica se cobra estancia
                        'Obtenemos los precios para cada uno de los servicios en las estancias
                        Dim res = Me._billingDomainService.GetPriceToStaysList(caregroupId, listStays, stayOption)
                        If res.StateResult Then
                            estn = res.ObjectEmbbeded.Where(Function(est) est.Value = res.ObjectEmbbeded.Max(Function(es) es.Value)).FirstOrDefault()
                            If estn IsNot Nothing Then
                                cupsId = estn.CupsId
                            End If
                        Else 'Aqui hubo errores
                            If Not String.IsNullOrEmpty(res.Message) Then
                                If Not listErrors.Contains(res.Message) Then
                                    listErrors.Add(res.Message)
                                    errorList.AppendLine(res.Message)
                                End If
                            End If

                            If res.MessageResult IsNot Nothing Then
                                For Each message In res.MessageResult
                                    If Not listErrors.Contains(message) Then
                                        listErrors.Add(message)
                                        errorList.AppendLine(message)
                                    End If
                                Next
                            End If
                        End If
                    End If
                Else 'SOAT
                    If diffBetweenDates.Hours >= objCaregroup.MaximumObservationTime Then 'Se cobra hospitalización
                        'Obtenemos los precios para cada uno de los servicios en las estancias
                        Dim res = Me._billingDomainService.GetPriceToStaysList(caregroupId, listStays, stayOption, True, endDate)
                        If res.StateResult Then
                            estn = res.ObjectEmbbeded.Where(Function(est) est.Value = res.ObjectEmbbeded.Max(Function(es) es.Value)).FirstOrDefault()
                            If estn IsNot Nothing Then
                                cupsId = estn.CupsId
                            End If
                        Else 'Aqui hubo errores
                            If Not String.IsNullOrEmpty(res.Message) Then
                                If Not listErrors.Contains(res.Message) Then
                                    listErrors.Add(res.Message)
                                    errorList.AppendLine(res.Message)
                                End If
                            End If

                            If res.MessageResult IsNot Nothing Then
                                For Each message In res.MessageResult
                                    If Not listErrors.Contains(message) Then
                                        listErrors.Add(message)
                                        errorList.AppendLine(message)
                                    End If
                                Next
                            End If
                        End If
                    Else 'Se cobra observación
                        Dim res = Me._billingDomainService.GetPriceToStaysList(caregroupId, listStays, stayOption)
                        If res.StateResult Then
                            estn = res.ObjectEmbbeded.Where(Function(est) est.Value = res.ObjectEmbbeded.Max(Function(es) es.Value)).FirstOrDefault()
                            If estn IsNot Nothing Then
                                cupsId = estn.CupsId
                            End If
                        Else 'Aqui hubo errores
                            If Not String.IsNullOrEmpty(res.Message) Then
                                If Not listErrors.Contains(res.Message) Then
                                    listErrors.Add(res.Message)
                                    errorList.AppendLine(res.Message)
                                End If
                            End If

                            If res.MessageResult IsNot Nothing Then
                                For Each message In res.MessageResult
                                    If Not listErrors.Contains(message) Then
                                        listErrors.Add(message)
                                        errorList.AppendLine(message)
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If

                If cupsId = 0 Then
                    Dim message = "No se encontró un cups asociado a la estancia"
                    If Not listErrors.Contains(message) Then
                        listErrors.Add(message)
                        errorList.AppendLine(message)
                    End If
                Else 'Consultamos el CUPS para obtener el nombre
                    Dim resCups = Me._cupsEntityRepository.GetCupsEntityById(cupsId)
                    estn.CupsId = cupsId
                    'Se asigna valores
                    If estn.CHREGESTADET.Any(Function(det) det.GENCUPSLIQ = cupsId AndAlso det.ChangeTracker.State = ObjectState.Added) Then
                        estn.CHREGESTADET.Where(Function(det) det.GENCUPSLIQ = cupsId AndAlso det.ChangeTracker.State = ObjectState.Added).First().CANTIDADLIQ += 1
                    Else
                        Dim estnDet As New CHREGESTADET()
                        estnDet.GENLIQUIDA = endDate
                        estnDet.GENULTFEC = estn.ADINGRESO.GENULTLIQUI
                        estnDet.GENCUPSLIQ = cupsId
                        estnDet.CUPSEntityContractDescriptionId = CUPSEntityContractDescriptionId
                        estnDet.CodeNameCups = (resCups.Code & "-" & resCups.Description)
                        estnDet.CANTIDADLIQ = 1
                        estnDet.Liquidated = False
                        estn.CHREGESTADET.Add(estnDet)
                    End If
                    estn.TotalUnits += 1
                    For Each det In estn.CHREGESTADET.Where(Function(d) d.CodeNameCups Is Nothing OrElse d.CodeNameCups.Equals(String.Empty)).ToList()
                        resCups = Me._cupsEntityRepository.GetCupsEntityById(det.GENCUPSLIQ)
                        det.CodeNameCups = (resCups.Code & "-" & resCups.Description)
                        det.Liquidated = If(det.ID > 0, True, False)
                    Next
                End If
            Else
                result.Message = "Existen estancias sin unidades de tiempo por liquidar"
                Return result
            End If

            'Si ocurrio algun error al calcular la estancia
            If errorList.Length > 0 Then
                result.Message = errorList.ToString()
                Return result
            End If

            Dim estancias = listStays.FindAll(Function(e) (e.FECINIEST <= endDate) _
                                                  AndAlso e.CHREGESTADET.Any(Function(m) m.CANTIDADLIQ > 0))
            'Colocar las de cantidad cero tambien en liquidadas solo si nunca van a tener cantidad
            For Each est In listStays.FindAll(Function(e) (e.FECINIEST <= endDate) _
                                                  AndAlso e.CHREGESTADET.Any(Function(m) m.CANTIDADLIQ > 0))
                If If(est.FECFINEST < est.FECINIEST, Date.Now >= endDate, est.FECFINEST >= endDate) Then
                    If est.FECFINEST > est.FECINIEST Then 'Se liquida total
                        If est.FECFINEST >= endDate Then
                            Dim fechaValidar As Date = If(est.FECFINEST < est.FECINIEST, Date.Now, est.FECFINEST)
                            'verificamos si endDate mas un día se cobra en la estancia actual o en otra estancia
                            Dim estanciasQueCoincidenConFecha = listStays.FindAll(Function(m) m.FECINIEST <= fechaValidar AndAlso If(m.FECFINEST < m.FECINIEST, Date.Now, m.FECFINEST) >= fechaValidar)
                            Dim res = Me._billingDomainService.GetPriceToStaysList(caregroupId, estanciasQueCoincidenConFecha, stayOption)
                            If res.StateResult Then
                                Dim estanciaCobrada = res.ObjectEmbbeded.Where(Function(est1) est1.Value = res.ObjectEmbbeded.Max(Function(es) es.Value)).FirstOrDefault()
                                'If estanciaCobrada.CODICAMAS.Equals(est.CODICAMAS) Then
                                If estanciaCobrada.ID.Equals(est.ID) Then
                                    If est.ID.Equals(listStays.LastOrDefault().ID) AndAlso est.FECFINEST > est.FECINIEST Then
                                        est.GENESTLIQ = 3
                                    Else
                                        est.GENESTLIQ = 2
                                    End If
                                Else
                                    est.GENESTLIQ = 2
                                End If
                                'est.GENESTLIQ = If(listStays.Where(Function(e) (e.FECINIEST <= endDate) And (e.FECFINEST <= endDate)).ToList().Count = listStays.Count, 3, 2)
                            Else
                                est.GENESTLIQ = 2 ' TODO:
                            End If
                        Else
                            est.GENESTLIQ = 3
                        End If
                    Else 'Se liquida parcial
                        est.GENESTLIQ = 2
                    End If
                Else
                    est.GENESTLIQ = 3
                End If
                est.MarkAsModified()
            Next
        Catch ex As Exception
            result.ObjectEmbbeded = Nothing
            errorList.AppendLine(Utils.GetInnerExceptionMessageToString(ex))
            errorList.AppendLine(ex.StackTrace)
            result.Message = errorList.ToString()
            Return result
        End Try

        result.StateResult = True
        result.Message = errorList.ToString()
        Return result
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _billingDomainService.Dispose()
            End If
            Me._stayRepository = Nothing
            Me._caregroupRepository = Nothing
            Me._parameterRepository = Nothing
            Me._billingDomainService = Nothing
            Me._cupsEntityRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
