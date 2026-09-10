'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Carlos Ernesto Cordoba
' Created          : 24-11-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting
Imports Domain.Base
Imports Domain.Base.Entities
Imports System.Globalization
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports System.Text
Imports Domain.Payroll
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class ContractServices
    Implements IContractServices


#Region "Fields"
    Private _rateManualValidityRepository As IRateManualValidityRepository
    Private ReadOnly _rateManualValidityDetailRepository As IRateManualValidityDetailRepository
    Private _rateManualRepository As IRateManualRepository
    Private _surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository
    Private ReadOnly _rateManualDetailRepository As IRateManualDetailRepository
    Private ReadOnly _cupsEntityContractDescriptionRepository As ICupsEntityContractDescriptionsRepository
    Private _cupsHomologationRepository As ICupsHomologationRepository
    Private _careGroupRepository As ICareGroupRepository
    Private _cupsEntityRepository As ICupsEntityRepository
    Private _ipsServiceRepository As IIPSServicesRepository
    Private _careGroupDefinitionRateRepository As ICareGroupDefinitionRateRepository
    Private _definitionRateDetailConditionRepository As IDefinitionRateDetailConditionRepository
    Private _definitionRateDetailRepository As IDefinitionRateDetailRepository
    Private _functionalUnitRepository As IFunctionalUnitRepository
    Private _procedureCupsRepository As IProcedureCupsRepository
    Private _rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository
    Private Const UVRNUMBER As Integer = 450
    Private Const MODULE_NAME = "Billing"

    Private _dictionaryCups As Dictionary(Of Integer, CUPSEntity)
    Private _dictionaryIPSService As Dictionary(Of Integer, IPSService)
    Private _dictionaryDefinitionRateDetailCondition As Dictionary(Of Integer, List(Of DefinitionRateDetailCondition))
    Private _dictionaryCareGroup As Dictionary(Of Integer, CareGroup)
    Private _dictionaryDefinitionRate As Dictionary(Of Tuple(Of Integer, Date), CareGroupDefinitionRate)
    Private _dictionaryDefinitionRateDetail As Dictionary(Of Tuple(Of Integer, Integer, ERateOptions, Integer), List(Of DefinitionRateDetail))
#End Region

#Region "Builders"

    Public Sub New(rateManualValidityRepository As IRateManualValidityRepository,
                   rateManualRepository As IRateManualRepository,
                   cupsHomologationRepository As ICupsHomologationRepository,
                   careGroupRepository As ICareGroupRepository,
                   cupsEntityRepository As ICupsEntityRepository,
                   ipsServiceRepository As IIPSServicesRepository,
                   rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository,
                   careGroupDefinitionRateRepository As ICareGroupDefinitionRateRepository,
                   definitionRateDetailConditionRepository As IDefinitionRateDetailConditionRepository,
                   definitionRateDetailRepository As IDefinitionRateDetailRepository,
                   functionalUnitRepository As IFunctionalUnitRepository,
                   procedureCupsRepository As IProcedureCupsRepository,
                   rateManualDetailRepository As IRateManualDetailRepository,
                   cupsEntityContractDescriptionRepository As ICupsEntityContractDescriptionsRepository,
                   rateManualValidityDetailRepository As IRateManualValidityDetailRepository,
                   surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository)
        _rateManualValidityRepository = rateManualValidityRepository
        _rateManualRepository = rateManualRepository
        _cupsHomologationRepository = cupsHomologationRepository
        _surgicalProcedureServiceRepository = surgicalProcedureServiceRepository
        _careGroupRepository = careGroupRepository
        _cupsEntityRepository = cupsEntityRepository
        _rateManualDetailSurgicalRepository = rateManualDetailSurgicalRepository
        _ipsServiceRepository = ipsServiceRepository
        _careGroupDefinitionRateRepository = careGroupDefinitionRateRepository
        _definitionRateDetailConditionRepository = definitionRateDetailConditionRepository
        _definitionRateDetailRepository = definitionRateDetailRepository
        _functionalUnitRepository = functionalUnitRepository
        _procedureCupsRepository = procedureCupsRepository
        _rateManualDetailRepository = rateManualDetailRepository
        Me._cupsEntityContractDescriptionRepository = cupsEntityContractDescriptionRepository
        _rateManualValidityDetailRepository = rateManualValidityDetailRepository
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para obtener  el valor de la tarifa
    ''' </summary>
    ''' <param name="CupsEntityId"></param>
    ''' <param name="CareGroupId"></param>
    ''' <param name="FunctionalUnitId"></param>
    ''' <param name="Specialty"></param>
    ''' <param name="ServiceDate"></param>
    ''' <param name="initialService"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRateValue(IPSServiceId As Integer, CupsEntityId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, initialService As ERateOptions, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) Implements IContractServices.GetRateValue
        Dim careGroup As CareGroup = GetCareGroup(CareGroupId)
        Dim countCoverageCUPS = _procedureCupsRepository.GetCountProcedureCupsByProcedureTemplateIdAndCupsId(careGroup.ProcedureTemplateId, CupsEntityId, RiasId, ContractDescriptionId)
        If countCoverageCUPS > 0 Then 'Si esta cubierto
            Return GetRateValueCondition(IPSServiceId, CupsEntityId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, initialService, RiasId, ContractDescriptionId)
        Else
            Dim cups As CUPSEntity = GetCupsEntity(CupsEntityId, False)
            Return New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = False, .Message = "El CUPS " & cups.Code & " - " & cups.Description & " no esta cubierto en el grupo de atencion " & careGroup.Code & " - " & careGroup.Name}
        End If
    End Function

    ''' <summary>
    ''' Método que se encarga de calcular el valor de los qx
    ''' </summary>
    ''' <returns></returns>
    Private Function GetRateValueConditionSurgicalProcedures(DefinitionRateDetailId As Integer, CupsEntityId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, initialService As ERateOptions, RiasId As Integer?, ContractDescriptionId As Integer?) As ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) Implements IContractServices.GetRateValueConditionSurgicalProcedures
        'Variable que me retorna la definición de tarifa con la condición
        Dim result As New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition)

        'Se obtiene la definición de tarifa por el id
        Dim definitionRateDetail = _definitionRateDetailRepository.GetDefinitionRateDetailById(DefinitionRateDetailId, False)
        result.ObjectEmbbeded = definitionRateDetail

        If definitionRateDetail.LogicalOperator = 1 Then 'Si el operador logico es ninguna, quiere decir que solo maneja una condicion
            If definitionRateDetail.ConditionType <> EConditionType.Null Then 'Si es diferente a ninguna la liquidacion esta en la tabla de condiciones
                Dim rateCondition As DefinitionRateDetailCondition = Nothing
                Select Case definitionRateDetail.ConditionType
                    Case EConditionType.Hour
                        rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByTime(definitionRateDetail.Id, ServiceDate.TimeOfDay)
                    Case EConditionType.Specialty
                        rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionBySpecialty(definitionRateDetail.Id, Specialty)
                    Case EConditionType.FunctionalUnit
                        rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByFunctionalUnit(definitionRateDetail.Id, FunctionalUnitId)
                    Case EConditionType.UnitType
                        Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                        rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByUnitType(definitionRateDetail.Id, functionalUnit.UnitType)
                    Case EConditionType.Rias
                        If RiasId IsNot Nothing Then
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByRiasId(definitionRateDetail.Id, RiasId)
                        End If
                    Case EConditionType.Description
                        If ContractDescriptionId IsNot Nothing Then
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByDescriptionId(definitionRateDetail.Id, ContractDescriptionId)
                        End If
                End Select
                If rateCondition IsNot Nothing AndAlso rateCondition.Id > 0 Then
                    result.ObjectEmbbededAux = rateCondition
                End If
            End If
        Else 'Entonces maneja dos condiciones
            Dim rateCondition As DefinitionRateDetailCondition = Nothing
            Dim rateConditionList As List(Of DefinitionRateDetailCondition) = GetDefinitionRateDetailByDefinitionRateDetailId(definitionRateDetail.Id)
            Select Case definitionRateDetail.ConditionType
                Case 1 'Horario
                    Select Case definitionRateDetail.ConditionType2
                        Case 2 'Horario y Especialidad
                            rateCondition = getConditionByTimeSpecialty(ServiceDate.TimeOfDay, Specialty, rateConditionList)
                        Case 3 'Horario y Unidad Funcional
                            rateCondition = getConditionByTimeFunctionalUnit(ServiceDate.TimeOfDay, FunctionalUnitId, rateConditionList)
                        Case 4 'Horario y Tipo de unidad
                            Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                            rateCondition = getConditionByTimeUnitType(ServiceDate.TimeOfDay, functionalUnit.UnitType, rateConditionList)
                        Case 6 'Horario y Rias
                            If RiasId IsNot Nothing Then
                                rateCondition = getConditionByTimeRias(ServiceDate.TimeOfDay, RiasId, rateConditionList)
                            End If
                        Case 7 'Horario y Descripción
                            If ContractDescriptionId IsNot Nothing Then
                                rateCondition = getConditionByTimeDescription(ServiceDate.TimeOfDay, ContractDescriptionId, rateConditionList)
                            End If
                    End Select
                Case 2 'Especialidad
                    Select Case definitionRateDetail.ConditionType2
                        Case 1 'Especialidad y Horario
                            rateCondition = getConditionBySpecialtyTime(Specialty, ServiceDate.TimeOfDay, rateConditionList)
                        Case 3 'Especialidad y Unidad Funcional
                            rateCondition = getConditionBySpecialtyFunctionalUnit(Specialty, FunctionalUnitId, rateConditionList)
                        Case 4 'Especialidad y Tipo de unidad
                            Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                            rateCondition = getConditionBySpecialtyUnitType(Specialty, functionalUnit.UnitType, rateConditionList)
                        Case 6 'Especialidad y Rias
                            If RiasId IsNot Nothing Then
                                rateCondition = getConditionBySpecialtyRias(Specialty, RiasId, rateConditionList)
                            End If
                        Case 7 'Especialidad y Description
                            If ContractDescriptionId IsNot Nothing Then
                                rateCondition = getConditionBySpecialtyDescription(Specialty, ContractDescriptionId, rateConditionList)
                            End If
                    End Select
                Case 3 'Unidad Funcional
                    Select Case definitionRateDetail.ConditionType2
                        Case 1 'Unidad Funcional y Horario
                            rateCondition = getConditionByFunctionalUnitTime(FunctionalUnitId, ServiceDate.TimeOfDay, rateConditionList)
                        Case 2 'Unidad Funcional y Especialidad
                            rateCondition = getConditionByFunctionalUnitSpecialty(FunctionalUnitId, Specialty, rateConditionList)
                        Case 4 'Unidad Funcional y Tipo de unidad
                            Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                            rateCondition = getConditionByFunctionalUnitUnitType(FunctionalUnitId, functionalUnit.UnitType, rateConditionList)
                        Case 6 'Unidad Funcional y Rias
                            If RiasId IsNot Nothing Then
                                rateCondition = getConditionByFunctionalUnitRias(FunctionalUnitId, RiasId, rateConditionList)
                            End If
                        Case 7 'Unidad Funcional y Description
                            If ContractDescriptionId IsNot Nothing Then
                                rateCondition = getConditionByFunctionalUnitDescription(FunctionalUnitId, ContractDescriptionId, rateConditionList)
                            End If
                    End Select
                Case 4 'Tipo de Unidad
                    Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                    Select Case definitionRateDetail.ConditionType2
                        Case 1 'Tipo de Unidad y Horario
                            rateCondition = getConditionByUnitTypeTime(functionalUnit.UnitType, ServiceDate.TimeOfDay, rateConditionList)
                        Case 2 'Tipo de Unidad y Especialidad
                            rateCondition = getConditionByUnitTypeSpecialty(functionalUnit.UnitType, Specialty, rateConditionList)
                        Case 3 'Tipo de Unidad y Unidad Funcional
                            rateCondition = getConditionByUnitTypeFunctionalUnit(functionalUnit.UnitType, FunctionalUnitId, rateConditionList)
                        Case 6 'Tipo de Unidad y Rias
                            If RiasId IsNot Nothing Then
                                rateCondition = getConditionByUnitTypeRias(functionalUnit.UnitType, RiasId, rateConditionList)
                            End If
                        Case 7 'Tipo de Unidad y Description
                            If ContractDescriptionId IsNot Nothing Then
                                rateCondition = getConditionByUnitTypeDescription(functionalUnit.UnitType, ContractDescriptionId, rateConditionList)
                            End If
                    End Select
                Case 6 'Rias
                    Select Case definitionRateDetail.ConditionType2
                        Case 1 'Rias y horario
                            rateCondition = getConditionByRiasTime(RiasId, ServiceDate.TimeOfDay, rateConditionList)
                        Case 2 'Rias y Especialidad
                            rateCondition = getConditionByRiasSpecialty(RiasId, Specialty, rateConditionList)
                        Case 3 'Rias y Unidad Funcional
                            rateCondition = getConditionByRiasFunctionalUnit(RiasId, FunctionalUnitId, rateConditionList)
                        Case 4 'Rias y Tipo de Unidad
                            Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                            rateCondition = getConditionByRiasUnitType(RiasId, functionalUnit.UnitType, rateConditionList)
                        Case 7 'Rias y Descripcion
                            rateCondition = getConditionByRiasDescription(RiasId, ContractDescriptionId, rateConditionList)
                    End Select
                Case 7 'Descriptions
                    Select Case definitionRateDetail.ConditionType2
                        Case 1 'Description y horario
                            rateCondition = getConditionByDescriptionTime(ContractDescriptionId, ServiceDate.TimeOfDay, rateConditionList)
                        Case 2 'Description y Especialidad
                            rateCondition = getConditionByDescriptionSpecialty(ContractDescriptionId, Specialty, rateConditionList)
                        Case 3 'Description y Unidad Funcional
                            rateCondition = getConditionByDescriptionFunctionalUnit(ContractDescriptionId, FunctionalUnitId, rateConditionList)
                        Case 4 'Description y Tipo de Unidad
                            Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                            rateCondition = getConditionByDescriptionUnitType(ContractDescriptionId, functionalUnit.UnitType, rateConditionList)
                        Case 6 'Description y Rias
                            If RiasId IsNot Nothing Then
                                rateCondition = getConditionByDescriptionRias(ContractDescriptionId, RiasId, rateConditionList)
                            End If
                    End Select
            End Select
            If rateCondition IsNot Nothing AndAlso rateCondition.Id > 0 Then
                result.ObjectEmbbededAux = rateCondition
            End If
        End If

        'Se retorna el resultado
        Return result
    End Function

    Private Function GetRateValueCondition(IPSServiceId As Integer, CupsEntityId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, initialService As ERateOptions, RiasId As Integer?, ContractDescriptionId As Integer?) As ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition)
        If IPSServiceId > 0 Then
            Dim ips = GetIpsService(IPSServiceId)

            If ips Is Nothing Then
                Return New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = False, .Message = "Servicio IPS no encontrado "}
            End If

            If ips.ServiceClass <> 1 AndAlso initialService <> ERateOptions.IPSSERVICE Then
                Return New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = False, .Message = "No se puede consultar un detalle qx para tipo distinto a IPS"}
            End If
        End If

        Dim result As ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition)
        Dim rateDetails As List(Of DefinitionRateDetail) = New List(Of DefinitionRateDetail)()

        Dim definitionRate As CareGroupDefinitionRate = GetCareGroupDefinitionRate(CareGroupId, ServiceDate)
        If definitionRate.Id = 0 Then
            result = New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = False, .Message = "No se encontro una definicion de tarifas en la fecha " + ServiceDate.Date.ToString}
            Return result
        End If

        rateDetails = GetDefinitionRateDetailList(definitionRate.DefinitionRateId, CupsEntityId, initialService, IPSServiceId)

        'Recorro los detalle para empezar a verificar las condiciones
        For Each detail In rateDetails
            If detail.LogicalOperator = 1 Then 'Si el operador logico es ninguna, quiere decir que solo maneja una condicion
                If detail.ConditionType = EConditionType.Null Then 'Si es ninguna la liquidacion esta en la misma tabla de detalle
                    result = New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = True, .ObjectEmbbeded = detail}
                    Return result
                Else

                    Dim rateCondition As DefinitionRateDetailCondition = Nothing
                    Select Case detail.ConditionType
                        Case EConditionType.Hour
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByTime(detail.Id, ServiceDate.TimeOfDay)
                        Case EConditionType.Specialty
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionBySpecialty(detail.Id, Specialty)
                        Case EConditionType.FunctionalUnit
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByFunctionalUnit(detail.Id, FunctionalUnitId)
                        Case EConditionType.UnitType
                            Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByUnitType(detail.Id, functionalUnit.UnitType)
                        Case EConditionType.Rias
                            If RiasId Is Nothing Then
                                Continue For
                            End If
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByRiasId(detail.Id, RiasId)
                        Case EConditionType.Description
                            If ContractDescriptionId Is Nothing Then
                                Continue For
                            End If
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByDescriptionId(detail.Id, ContractDescriptionId)
                    End Select
                    If rateCondition IsNot Nothing AndAlso rateCondition.Id > 0 Then
                        result = New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = True, .ObjectEmbbeded = detail, .ObjectEmbbededAux = rateCondition}
                        Return result
                    End If
                End If
            Else 'Entonces maneja dos condiciones

                Dim rateCondition As DefinitionRateDetailCondition = Nothing
                Dim rateConditionList As List(Of DefinitionRateDetailCondition) = GetDefinitionRateDetailByDefinitionRateDetailId(detail.Id)
                Select Case detail.ConditionType
                    Case 1 'Horario
                        Select Case detail.ConditionType2
                            Case 2 'Horario y Especialidad
                                rateCondition = getConditionByTimeSpecialty(ServiceDate.TimeOfDay, Specialty, rateConditionList)
                            Case 3 'Horario y Unidad Funcional
                                rateCondition = getConditionByTimeFunctionalUnit(ServiceDate.TimeOfDay, FunctionalUnitId, rateConditionList)
                            Case 4 'Horario y Tipo de unidad
                                Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                                rateCondition = getConditionByTimeUnitType(ServiceDate.TimeOfDay, functionalUnit.UnitType, rateConditionList)
                            Case 6 'Horario y Rias
                                If RiasId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = getConditionByTimeRias(ServiceDate.TimeOfDay, RiasId, rateConditionList)
                            Case 7 'Horario y Descripción
                                If ContractDescriptionId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = getConditionByTimeDescription(ServiceDate.TimeOfDay, ContractDescriptionId, rateConditionList)
                        End Select
                    Case 2 'Especialidad
                        Select Case detail.ConditionType2
                            Case 1 'Especialidad y Horario
                                rateCondition = getConditionBySpecialtyTime(Specialty, ServiceDate.TimeOfDay, rateConditionList)
                            Case 3 'Especialidad y Unidad Funcional
                                rateCondition = getConditionBySpecialtyFunctionalUnit(Specialty, FunctionalUnitId, rateConditionList)
                            Case 4 'Especialidad y Tipo de unidad
                                Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                                rateCondition = getConditionBySpecialtyUnitType(Specialty, functionalUnit.UnitType, rateConditionList)
                            Case 6 'Especialidad y Rias
                                If RiasId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = getConditionBySpecialtyRias(Specialty, RiasId, rateConditionList)
                            Case 7 'Especialidad y Description
                                If ContractDescriptionId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = getConditionBySpecialtyDescription(Specialty, ContractDescriptionId, rateConditionList)
                        End Select
                    Case 3 'Unidad Funcional
                        Select Case detail.ConditionType2
                            Case 1 'Unidad Funcional y Horario
                                rateCondition = getConditionByFunctionalUnitTime(FunctionalUnitId, ServiceDate.TimeOfDay, rateConditionList)
                            Case 2 'Unidad Funcional y Especialidad
                                rateCondition = getConditionByFunctionalUnitSpecialty(FunctionalUnitId, Specialty, rateConditionList)
                            Case 4 'Unidad Funcional y Tipo de unidad
                                Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                                rateCondition = getConditionByFunctionalUnitUnitType(FunctionalUnitId, functionalUnit.UnitType, rateConditionList)
                            Case 6 'Unidad Funcional y Rias
                                If RiasId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = getConditionByFunctionalUnitRias(FunctionalUnitId, RiasId, rateConditionList)
                            Case 7 'Unidad Funcional y Description
                                If ContractDescriptionId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = getConditionByFunctionalUnitDescription(FunctionalUnitId, ContractDescriptionId, rateConditionList)
                        End Select
                    Case 4 'Tipo de Unidad
                        Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                        Select Case detail.ConditionType2
                            Case 1 'Tipo de Unidad y Horario
                                rateCondition = getConditionByUnitTypeTime(functionalUnit.UnitType, ServiceDate.TimeOfDay, rateConditionList)
                            Case 2 'Tipo de Unidad y Especialidad
                                rateCondition = getConditionByUnitTypeSpecialty(functionalUnit.UnitType, Specialty, rateConditionList)
                            Case 3 'Tipo de Unidad y Unidad Funcional
                                rateCondition = getConditionByUnitTypeFunctionalUnit(functionalUnit.UnitType, FunctionalUnitId, rateConditionList)
                            Case 6 'Tipo de Unidad y Rias
                                If RiasId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = getConditionByUnitTypeRias(functionalUnit.UnitType, RiasId, rateConditionList)
                            Case 7 'Tipo de Unidad y Description
                                If ContractDescriptionId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = getConditionByUnitTypeDescription(functionalUnit.UnitType, ContractDescriptionId, rateConditionList)
                        End Select
                    Case 6 'Rias
                        If RiasId Is Nothing Then
                            Continue For
                        End If
                        Select Case detail.ConditionType2
                            Case 1 'Rias y horario
                                rateCondition = getConditionByRiasTime(RiasId, ServiceDate.TimeOfDay, rateConditionList)
                            Case 2 'Rias y Especialidad
                                rateCondition = getConditionByRiasSpecialty(RiasId, Specialty, rateConditionList)
                            Case 3 'Rias y Unidad Funcional
                                rateCondition = getConditionByRiasFunctionalUnit(RiasId, FunctionalUnitId, rateConditionList)
                            Case 4 'Rias y Tipo de Unidad
                                Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                                rateCondition = getConditionByRiasUnitType(RiasId, functionalUnit.UnitType, rateConditionList)
                            Case 7 'Rias y Descripcion
                                rateCondition = getConditionByRiasDescription(RiasId, ContractDescriptionId, rateConditionList)
                        End Select
                    Case 7 'Descriptions
                        If ContractDescriptionId Is Nothing Then
                            Continue For
                        End If
                        Select Case detail.ConditionType2
                            Case 1 'Description y horario
                                rateCondition = getConditionByDescriptionTime(ContractDescriptionId, ServiceDate.TimeOfDay, rateConditionList)
                            Case 2 'Description y Especialidad
                                rateCondition = getConditionByDescriptionSpecialty(ContractDescriptionId, Specialty, rateConditionList)
                            Case 3 'Description y Unidad Funcional
                                rateCondition = getConditionByDescriptionFunctionalUnit(ContractDescriptionId, FunctionalUnitId, rateConditionList)
                            Case 4 'Description y Tipo de Unidad
                                Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                                rateCondition = getConditionByDescriptionUnitType(ContractDescriptionId, functionalUnit.UnitType, rateConditionList)
                            Case 6 'Description y Rias
                                If RiasId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = getConditionByDescriptionRias(ContractDescriptionId, RiasId, rateConditionList)
                        End Select
                End Select
                If rateCondition IsNot Nothing AndAlso rateCondition.Id > 0 Then
                    result = New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = True, .ObjectEmbbeded = detail, .ObjectEmbbededAux = rateCondition}
                    Return result
                End If
            End If
        Next
        If initialService = ERateOptions.GENERAL Then
            Dim cups = GetCupsEntity(CupsEntityId, False)
            Return New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = False, .Message = "No se encontro una tarifa para el CUPS " & cups.Code & " - " & cups.Description, .MessageResult = {"ERROR1"}.ToList}
        Else
            Return GetRateValueCondition(IPSServiceId, CupsEntityId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, initialService + 1, RiasId, ContractDescriptionId)
        End If
    End Function


    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Horario y Especialidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByTimeSpecialty(time As TimeSpan, specialtyId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByTimeSpecialty
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And time >= condition.StartTime And time <= condition.EndTime And condition.SpecialtyId2 = specialtyId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And time >= condition.StartTime And time <= condition.EndTime And condition.SpecialtyId2 <> specialtyId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And time < condition.StartTime And time > condition.EndTime And condition.SpecialtyId2 = specialtyId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And time < condition.StartTime And time > condition.EndTime And condition.SpecialtyId2 <> specialtyId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Horario y Rias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByTimeRias(time As TimeSpan, riasId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByTimeRias
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And time >= condition.StartTime And time <= condition.EndTime And condition.RIASId2 = riasId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And time >= condition.StartTime And time <= condition.EndTime And condition.RIASId2 <> riasId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And time < condition.StartTime And time > condition.EndTime And condition.RIASId2 = riasId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And time < condition.StartTime And time > condition.EndTime And condition.RIASId2 <> riasId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Horario y descripciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByTimeDescription(time As TimeSpan, descriptionId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByTimeDescription
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And time >= condition.StartTime And time <= condition.EndTime And condition.ContractDescriptionId2 = descriptionId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And time >= condition.StartTime And time <= condition.EndTime And condition.ContractDescriptionId2 <> descriptionId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And time < condition.StartTime And time > condition.EndTime And condition.ContractDescriptionId2 = descriptionId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And time < condition.StartTime And time > condition.EndTime And condition.ContractDescriptionId2 <> descriptionId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Horario y Unidad Funcional
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByTimeFunctionalUnit(time As TimeSpan, functionalUnitId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByTimeFunctionalUnit
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And time >= condition.StartTime And time <= condition.EndTime And condition.FunctionalUnitId2 = functionalUnitId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And time >= condition.StartTime And time <= condition.EndTime And condition.FunctionalUnitId2 <> functionalUnitId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And time < condition.StartTime And time > condition.EndTime And condition.FunctionalUnitId2 = functionalUnitId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And time < condition.StartTime And time > condition.EndTime And condition.FunctionalUnitId2 <> functionalUnitId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Horario y Tipo de Unidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByTimeUnitType(time As TimeSpan, unitType As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByTimeUnitType
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And time >= condition.StartTime And time <= condition.EndTime And condition.UnitTypeId2 = unitType Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And time >= condition.StartTime And time <= condition.EndTime And condition.UnitTypeId2 <> unitType Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And time < condition.StartTime And time > condition.EndTime And condition.UnitTypeId2 = unitType Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And time < condition.StartTime And time > condition.EndTime And condition.UnitTypeId2 <> unitType Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Especialidad y Horario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionBySpecialtyTime(specialtyId As String, time As TimeSpan, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionBySpecialtyTime
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.SpecialtyId = specialtyId And time >= condition.StartTime2 And time <= condition.EndTime2 Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.SpecialtyId = specialtyId And time < condition.StartTime2 And time > condition.EndTime2 Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.SpecialtyId <> specialtyId And time >= condition.StartTime2 And time <= condition.EndTime2 Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.SpecialtyId <> specialtyId And time < condition.StartTime2 And time > condition.EndTime2 Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Especialidad y Unidad Funcional
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionBySpecialtyFunctionalUnit(specialtyId As String, functionalUnitId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionBySpecialtyFunctionalUnit
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.SpecialtyId = specialtyId And condition.FunctionalUnitId2 = functionalUnitId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.SpecialtyId = specialtyId And condition.FunctionalUnitId2 <> functionalUnitId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.SpecialtyId <> specialtyId And condition.FunctionalUnitId2 = functionalUnitId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.SpecialtyId <> specialtyId And condition.FunctionalUnitId2 <> functionalUnitId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Especialidad y rias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionBySpecialtyRias(specialtyId As String, riasId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionBySpecialtyRias
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.SpecialtyId = specialtyId And condition.RIASId2 = riasId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.SpecialtyId = specialtyId And condition.RIASId2 <> riasId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.SpecialtyId <> specialtyId And condition.RIASId2 = riasId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.SpecialtyId <> specialtyId And condition.RIASId2 <> riasId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Especialidad y descripción
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionBySpecialtyDescription(specialtyId As String, descriptionId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionBySpecialtyDescription
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.SpecialtyId = specialtyId And condition.ContractDescriptionId2 = descriptionId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.SpecialtyId = specialtyId And condition.ContractDescriptionId2 <> descriptionId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.SpecialtyId <> specialtyId And condition.ContractDescriptionId2 = descriptionId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.SpecialtyId <> specialtyId And condition.ContractDescriptionId2 <> descriptionId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Especialidad y Tipo de Unidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionBySpecialtyUnitType(specialtyId As String, unitType As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionBySpecialtyUnitType
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.SpecialtyId = specialtyId And condition.UnitTypeId2 = unitType Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.SpecialtyId = specialtyId And condition.UnitTypeId2 <> unitType Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.SpecialtyId <> specialtyId And condition.UnitTypeId2 = unitType Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.SpecialtyId <> specialtyId And condition.UnitTypeId2 <> unitType Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Rias y Tipo de Unidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByRiasUnitType(riasId As String, unitType As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByRiasUnitType
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.RIASId = riasId And condition.UnitTypeId2 = unitType Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.RIASId = riasId And condition.UnitTypeId2 <> unitType Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.RIASId <> riasId And condition.UnitTypeId2 = unitType Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.RIASId <> riasId And condition.UnitTypeId2 <> unitType Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Description y Tipo de Unidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByDescriptionUnitType(descriptionId As String, unitType As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByDescriptionUnitType
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.ContractDescriptionId = descriptionId And condition.UnitTypeId2 = unitType Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.ContractDescriptionId = descriptionId And condition.UnitTypeId2 <> unitType Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.ContractDescriptionId <> descriptionId And condition.UnitTypeId2 = unitType Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.ContractDescriptionId <> descriptionId And condition.UnitTypeId2 <> unitType Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Unidad Funcional y Horario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByFunctionalUnitTime(functionalUnitId As Integer, time As TimeSpan, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByFunctionalUnitTime
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.FunctionalUnitId = functionalUnitId And time >= condition.StartTime2 And time <= condition.EndTime2 Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.FunctionalUnitId = functionalUnitId And time < condition.StartTime2 And time > condition.EndTime2 Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.FunctionalUnitId <> functionalUnitId And time >= condition.StartTime2 And time <= condition.EndTime2 Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.FunctionalUnitId <> functionalUnitId And time < condition.StartTime2 And time > condition.EndTime2 Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Rias y Horario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByRiasTime(riasId As Integer, time As TimeSpan, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByRiasTime
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.RIASId = riasId And time >= condition.StartTime2 And time <= condition.EndTime2 Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.RIASId = riasId And time < condition.StartTime2 And time > condition.EndTime2 Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.RIASId <> riasId And time >= condition.StartTime2 And time <= condition.EndTime2 Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.RIASId <> riasId And time < condition.StartTime2 And time > condition.EndTime2 Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Description y Horario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByDescriptionTime(descriptionId As Integer, time As TimeSpan, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByDescriptionTime
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.ContractDescriptionId = descriptionId And time >= condition.StartTime2 And time <= condition.EndTime2 Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.ContractDescriptionId = descriptionId And time < condition.StartTime2 And time > condition.EndTime2 Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.ContractDescriptionId <> descriptionId And time >= condition.StartTime2 And time <= condition.EndTime2 Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.ContractDescriptionId <> descriptionId And time < condition.StartTime2 And time > condition.EndTime2 Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Unidad Funcional y Especialidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByFunctionalUnitSpecialty(functionalUnitId As Integer, specialtyId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByFunctionalUnitSpecialty
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.FunctionalUnitId = functionalUnitId And condition.SpecialtyId2 = specialtyId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.FunctionalUnitId = functionalUnitId And condition.SpecialtyId2 <> specialtyId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.FunctionalUnitId <> functionalUnitId And condition.SpecialtyId2 = specialtyId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.FunctionalUnitId <> functionalUnitId And condition.SpecialtyId2 <> specialtyId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Unidad Funcional y Rias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByFunctionalUnitRias(functionalUnitId As Integer, riasId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByFunctionalUnitRias
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.FunctionalUnitId = functionalUnitId And condition.RIASId2 = riasId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.FunctionalUnitId = functionalUnitId And condition.RIASId2 <> riasId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.FunctionalUnitId <> functionalUnitId And condition.RIASId2 = riasId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.FunctionalUnitId <> functionalUnitId And condition.RIASId2 <> riasId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Unidad Funcional y descripción
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByFunctionalUnitDescription(functionalUnitId As Integer, descriptionId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByFunctionalUnitDescription
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.FunctionalUnitId = functionalUnitId And condition.ContractDescriptionId2 = descriptionId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.FunctionalUnitId = functionalUnitId And condition.ContractDescriptionId2 <> descriptionId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.FunctionalUnitId <> functionalUnitId And condition.ContractDescriptionId2 = descriptionId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.FunctionalUnitId <> functionalUnitId And condition.ContractDescriptionId2 <> descriptionId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Unidad Funcional y Tipo de unidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByFunctionalUnitUnitType(functionalUnitId As Integer, unitType As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByFunctionalUnitUnitType
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.FunctionalUnitId = functionalUnitId And condition.UnitTypeId2 = unitType Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.FunctionalUnitId = functionalUnitId And condition.UnitTypeId2 <> unitType Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.FunctionalUnitId <> functionalUnitId And condition.UnitTypeId2 = unitType Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.FunctionalUnitId <> functionalUnitId And condition.UnitTypeId2 <> unitType Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Tipo de Unidad y Horario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByUnitTypeTime(unitTypeId As Integer, time As TimeSpan, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByUnitTypeTime
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.UnitTypeId = unitTypeId And time >= condition.StartTime2 And time <= condition.EndTime2 Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.UnitTypeId = unitTypeId And time < condition.StartTime2 And time > condition.EndTime2 Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.UnitTypeId <> unitTypeId And time >= condition.StartTime2 And time <= condition.EndTime2 Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.UnitTypeId <> unitTypeId And time < condition.StartTime2 And time > condition.EndTime2 Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Tipo de Unidad y Especialidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getConditionByUnitTypeSpecialty(unitTypeId As Integer, specialtyId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByUnitTypeSpecialty
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.UnitTypeId = unitTypeId And condition.SpecialtyId2 = specialtyId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.UnitTypeId = unitTypeId And condition.SpecialtyId2 <> specialtyId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.UnitTypeId <> unitTypeId And condition.SpecialtyId2 = specialtyId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.UnitTypeId <> unitTypeId And condition.SpecialtyId2 <> specialtyId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Rias y Especialidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByRiasSpecialty(riasId As Integer, specialtyId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByRiasSpecialty
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.RIASId = riasId And condition.SpecialtyId2 = specialtyId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.RIASId = riasId And condition.SpecialtyId2 <> specialtyId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.RIASId <> riasId And condition.SpecialtyId2 = specialtyId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.RIASId <> riasId And condition.SpecialtyId2 <> specialtyId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Description y Especialidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByDescriptionSpecialty(descriptionId As Integer, specialtyId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByDescriptionSpecialty
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.ContractDescriptionId = descriptionId And condition.SpecialtyId2 = specialtyId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.ContractDescriptionId = descriptionId And condition.SpecialtyId2 <> specialtyId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.ContractDescriptionId <> descriptionId And condition.SpecialtyId2 = specialtyId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.ContractDescriptionId <> descriptionId And condition.SpecialtyId2 <> specialtyId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Tipo de Unidad y Rias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByUnitTypeRias(unitTypeId As Integer, riasId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByUnitTypeRias
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.UnitTypeId = unitTypeId And condition.RIASId2 = riasId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.UnitTypeId = unitTypeId And condition.RIASId2 <> riasId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.UnitTypeId <> unitTypeId And condition.RIASId2 = riasId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.UnitTypeId <> unitTypeId And condition.RIASId2 <> riasId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Tipo de Unidad y descripción
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByUnitTypeDescription(unitTypeId As Integer, descriptionId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByUnitTypeDescription
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.UnitTypeId = unitTypeId And condition.ContractDescriptionId2 = descriptionId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.UnitTypeId = unitTypeId And condition.ContractDescriptionId2 <> descriptionId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.UnitTypeId <> unitTypeId And condition.ContractDescriptionId2 = descriptionId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.UnitTypeId <> unitTypeId And condition.ContractDescriptionId2 <> descriptionId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Tipo de Unidad y Unidad Funcional
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getConditionByUnitTypeFunctionalUnit(unitTypeId As Integer, functionalUnitId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByUnitTypeFunctionalUnit
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.UnitTypeId = unitTypeId And condition.FunctionalUnitId2 = functionalUnitId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.UnitTypeId = unitTypeId And condition.FunctionalUnitId2 <> functionalUnitId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.UnitTypeId <> unitTypeId And condition.FunctionalUnitId2 = functionalUnitId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.UnitTypeId <> unitTypeId And condition.FunctionalUnitId2 <> functionalUnitId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Rias y Unidad Funcional
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByRiasFunctionalUnit(riasId As Integer, functionalUnitId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByRiasFunctionalUnit
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.RIASId = riasId And condition.FunctionalUnitId2 = functionalUnitId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.RIASId = riasId And condition.FunctionalUnitId2 <> functionalUnitId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.RIASId <> riasId And condition.FunctionalUnitId2 = functionalUnitId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.RIASId <> riasId And condition.FunctionalUnitId2 <> functionalUnitId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Rias y Description
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByRiasDescription(riasId As Integer, descriptionId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByRiasDescription
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.RIASId = riasId And condition.ContractDescriptionId2 = descriptionId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.RIASId = riasId And condition.ContractDescriptionId2 <> descriptionId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.RIASId <> riasId And condition.ContractDescriptionId2 = descriptionId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.RIASId <> riasId And condition.ContractDescriptionId2 <> descriptionId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Description y Unidad Funcional
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByDescriptionFunctionalUnit(descriptionId As Integer, functionalUnitId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByDescriptionFunctionalUnit
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.ContractDescriptionId = descriptionId And condition.FunctionalUnitId2 = functionalUnitId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.ContractDescriptionId = descriptionId And condition.FunctionalUnitId2 <> functionalUnitId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.ContractDescriptionId <> descriptionId And condition.FunctionalUnitId2 = functionalUnitId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.ContractDescriptionId <> descriptionId And condition.FunctionalUnitId2 <> functionalUnitId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la tarifa cuando las dos condiciones son Description y Rias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getConditionByDescriptionRias(descriptionId As Integer, riasId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition Implements IContractServices.getConditionByDescriptionRias
        For Each condition In detailConditionList
            If condition.Operator = 1 And condition.Operator2 = 1 And condition.ContractDescriptionId = descriptionId And condition.RIASId2 = riasId Then '= =
                Return condition
            ElseIf condition.Operator = 1 And condition.Operator2 = 2 And condition.ContractDescriptionId = descriptionId And condition.RIASId2 <> riasId Then '= <>
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 1 And condition.ContractDescriptionId <> descriptionId And condition.RIASId2 = riasId Then '<> =
                Return condition
            ElseIf condition.Operator = 2 And condition.Operator2 = 2 And condition.ContractDescriptionId <> descriptionId And condition.RIASId2 <> riasId Then  '<> <>
                Return condition
            End If
        Next
        Return Nothing
    End Function

    Public Function GetCareGroupDefinitionRate(careGroupId As Integer, serviceDate As Date) As CareGroupDefinitionRate
        If _dictionaryDefinitionRate Is Nothing Then
            _dictionaryDefinitionRate = New Dictionary(Of Tuple(Of Integer, Date), CareGroupDefinitionRate)()
        End If
        Dim drd As CareGroupDefinitionRate = Nothing
        If _dictionaryDefinitionRate.ContainsKey(New Tuple(Of Integer, Date)(careGroupId, serviceDate)) Then
            drd = _dictionaryDefinitionRate(New Tuple(Of Integer, Date)(careGroupId, serviceDate))
        Else
            drd = _careGroupDefinitionRateRepository.GetCareGroupDefinitionRateByCareGroupIdAndDate(careGroupId, serviceDate, False)
            If drd IsNot Nothing AndAlso drd.Id > 0 Then
                _dictionaryDefinitionRate.Add(New Tuple(Of Integer, Date)(careGroupId, serviceDate), drd)
            End If
        End If
        Return drd
    End Function

    Public Function GetDefinitionRateDetailByDefinitionRateDetailId(definitionRateDetailId As Integer) As List(Of DefinitionRateDetailCondition) Implements IContractServices.GetDefinitionRateDetailByDefinitionRateDetailId
        If _dictionaryDefinitionRateDetailCondition Is Nothing Then
            _dictionaryDefinitionRateDetailCondition = New Dictionary(Of Integer, List(Of DefinitionRateDetailCondition))()
        End If
        Dim drd As List(Of DefinitionRateDetailCondition) = Nothing
        If _dictionaryDefinitionRateDetailCondition.ContainsKey(definitionRateDetailId) Then
            drd = _dictionaryDefinitionRateDetailCondition(definitionRateDetailId)
        Else
            drd = _definitionRateDetailConditionRepository.GetDefinitionRateDetailByDefinitionRateDetailId(definitionRateDetailId)
            If drd IsNot Nothing AndAlso drd.Any() Then
                _dictionaryDefinitionRateDetailCondition.Add(definitionRateDetailId, drd)
            End If
        End If
        Return drd
    End Function

    Public Function GetDefinitionRateDetailList(definitionRateId As Integer, CupsEntityId As Integer, eRateOptions As ERateOptions, IPSServiceId As Integer) As List(Of DefinitionRateDetail) Implements IContractServices.GetDefinitionRateDetailList
        If _dictionaryDefinitionRateDetail Is Nothing Then
            _dictionaryDefinitionRateDetail = New Dictionary(Of Tuple(Of Integer, Integer, ERateOptions, Integer), List(Of DefinitionRateDetail))()
        End If
        Dim rateDetails As List(Of DefinitionRateDetail) = Nothing
        If _dictionaryDefinitionRateDetail.ContainsKey(New Tuple(Of Integer, Integer, ERateOptions, Integer)(definitionRateId, CupsEntityId, eRateOptions, IPSServiceId)) Then
            rateDetails = _dictionaryDefinitionRateDetail(New Tuple(Of Integer, Integer, ERateOptions, Integer)(definitionRateId, CupsEntityId, eRateOptions, IPSServiceId))
        Else
            Select Case eRateOptions
                Case Service.ERateOptions.IPSSERVICE
                    If IPSServiceId <> Nothing AndAlso IPSServiceId > 0 Then
                        'Se obtiene el servicio ips para saber si es Qx
                        Dim ipsService = GetIpsService(IPSServiceId)

                        'If ipsService IsNot Nothing AndAlso ipsService.Presentation <> 2 Then 'Si es No Qx o Paquete
                        rateDetails = _definitionRateDetailRepository.GetDefinitionRateDetailByIPSServiceId(definitionRateId, IPSServiceId)
                        'Else 'Si es Qx no se consulta ya que el valor del papa lo da la sumatoria de los items Qx
                        '    rateDetails = New List(Of DefinitionRateDetail)
                        'End If
                    Else
                        rateDetails = New List(Of DefinitionRateDetail)
                    End If
                Case Service.ERateOptions.CUPS
                    rateDetails = _definitionRateDetailRepository.GetDefinitionRateDetailByCUPSEntityId(definitionRateId, CupsEntityId)
                Case Service.ERateOptions.SUBGROUP
                    Dim cups = GetCupsEntity(CupsEntityId, False)
                    rateDetails = _definitionRateDetailRepository.GetDefinitionRateDetailByCUPSSubGroupId(definitionRateId, cups.CUPSSubGroupId)
                Case Service.ERateOptions.GROUP
                    Dim cups = GetCupsEntity(CupsEntityId, False)
                    rateDetails = _definitionRateDetailRepository.GetDefinitionRateDetailByCUPSGroupId(definitionRateId, cups.CupsSubgroup.CupsGroupId)
                Case Service.ERateOptions.GENERAL
                    rateDetails = _definitionRateDetailRepository.GetDefinitionRateDetailByRuleType(definitionRateId, 5)
            End Select
            If rateDetails IsNot Nothing AndAlso rateDetails.Any() Then
                _dictionaryDefinitionRateDetail.Add(New Tuple(Of Integer, Integer, ERateOptions, Integer)(definitionRateId, CupsEntityId, eRateOptions, IPSServiceId), rateDetails)
            End If
        End If
        If rateDetails IsNot Nothing AndAlso rateDetails.Count > 0 Then
            Return (From x In rateDetails Select x Order By x.Weight Descending).ToList()
        End If
        Return rateDetails
    End Function

    Public Function GetCupsEntity(id As Integer, tracking As Boolean) As CUPSEntity
        If _dictionaryCups Is Nothing Then
            _dictionaryCups = New Dictionary(Of Integer, CUPSEntity)()
        End If
        Dim cups As CUPSEntity = Nothing
        If _dictionaryCups.ContainsKey(id) Then
            cups = _dictionaryCups(id)
        Else
            cups = _cupsEntityRepository.GetCupsEntityById(id, tracking)
            If cups IsNot Nothing AndAlso cups.Id > 0 Then
                _dictionaryCups.Add(cups.Id, cups)
            End If
        End If
        Return cups
    End Function

    Public Function GetIpsService(id As Integer) As IPSService
        If _dictionaryIPSService Is Nothing Then
            _dictionaryIPSService = New Dictionary(Of Integer, IPSService)()
        End If
        Dim ipsService As IPSService = Nothing
        If _dictionaryIPSService.ContainsKey(id) Then
            ipsService = _dictionaryIPSService(id)
        Else
            ipsService = _ipsServiceRepository.GetIPSServiceById(id, False)
            If ipsService IsNot Nothing AndAlso ipsService.Id > 0 Then
                _dictionaryIPSService.Add(ipsService.Id, ipsService)
            End If
        End If
        Return ipsService
    End Function

    Public Function GetCareGroup(id As Integer) As CareGroup
        If _dictionaryCareGroup Is Nothing Then
            _dictionaryCareGroup = New Dictionary(Of Integer, CareGroup)()
        End If
        Dim caregroup As CareGroup = Nothing
        If _dictionaryCareGroup.ContainsKey(id) Then
            caregroup = _dictionaryCareGroup(id)
        Else
            caregroup = _careGroupRepository.GetCareGroupById(id)
            If caregroup IsNot Nothing AndAlso caregroup.Id > 0 Then
                _dictionaryCareGroup.Add(caregroup.Id, caregroup)
            End If
        End If
        Return caregroup
    End Function
    ''' <summary>
    ''' metodo para obtener las homolagaciones pero con un listado de CUPS
    ''' </summary>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHomologationCupsByListCUPS(ParamArray parameters As Object()) As ActionResult(Of List(Of CupsHomologation)) Implements IContractServices.GetHomologationCupsByListCUPS
        Dim errors As New StringBuilder
        Dim listHomologation = New List(Of CupsHomologation)

        For Each item In parameters(0)
            Dim result = GetHomologationCups(parameters(1), item, parameters(2), parameters(3), parameters(4), parameters(5), parameters(6))
            If result.StateResult = False Then
                errors.AppendLine(result.Message)
                Continue For
            End If
            listHomologation.AddRange(result.ObjectEmbbeded)
        Next
        If errors.Length > 0 AndAlso listHomologation.Count = 0 Then
            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = errors.ToString()}
        End If
        Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = True, .ObjectEmbbeded = listHomologation}
    End Function

    ''' <summary>
    ''' obtener el valor de los detalles del ips quirurgico cuando el usuario cambia los valores por defecto en el formulario
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <param name="listSurgicalProcedureServiceDefault"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServiceValueBySurgicalProcedureService(serviceOrderDetail As ServiceOrderDetail, listSurgicalProcedureServiceDefault As List(Of SurgicalProcedureService)) As ActionResult(Of ServiceOrderDetail) Implements IContractServices.GetServiceValueBySurgicalProcedureService
        Dim rateVariation As Decimal = 0
        Dim rateManual As RateManual = Nothing

        'Se consulta el id de la descripción relacionada
        Dim contractDescriptionId As Integer? = Nothing
        If serviceOrderDetail.CUPSEntityContractDescriptionId IsNot Nothing AndAlso serviceOrderDetail.CUPSEntityContractDescriptionId > 0 Then
            contractDescriptionId = _cupsEntityRepository.GetContractDescriptionIdByCupsEntityContractDescription(serviceOrderDetail.CUPSEntityContractDescriptionId)
        End If

        Dim result = GetRateValue(serviceOrderDetail.IPSServiceId, serviceOrderDetail.CUPSEntityId, serviceOrderDetail.CareGroupId, serviceOrderDetail.PerformsFunctionalUnitId, serviceOrderDetail.PerformsProfessionalSpecialty, serviceOrderDetail.ServiceDate, ERateOptions.IPSSERVICE, Nothing, contractDescriptionId)

        If result.ObjectEmbbeded Is Nothing AndAlso result.ObjectEmbbededAux Is Nothing AndAlso result.StateResult = False Then
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = result.Message}
        End If

        If result.ObjectEmbbededAux IsNot Nothing Then 'Fue por que la tarifa se saco de una condicion
            If result.ObjectEmbbededAux.LiquidationType = 1 AndAlso result.ObjectEmbbededAux?.RateManualId IsNot Nothing Then
                rateManual = _rateManualRepository.GetRateManualById(result.ObjectEmbbededAux.RateManualId)
            ElseIf result.ObjectEmbbededAux.LiquidationType = 2 Then 'Si es estandar
                rateVariation = result.ObjectEmbbededAux.RateVariation
                rateManual = _rateManualRepository.GetRateManualById(result.ObjectEmbbededAux.RateManualId)
            ElseIf result.ObjectEmbbededAux.LiquidationType = 3 Then 'Si es por vigencia
                rateVariation = result.ObjectEmbbededAux.RateVariation
                Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(result.ObjectEmbbededAux.RateManualValidityId)
                Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= serviceOrderDetail.ServiceDate AndAlso d.EndDate >= serviceOrderDetail.ServiceDate).FirstOrDefault
                If rateManualValidityDetail Is Nothing Then
                    Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = String.Format("El servicio IPS {0} no tiene parametrizado un manual de tarifas para la vigencia {1} - {2} en la fecha del servicio {3}", serviceOrderDetail.CodeNameIpsService, rateManualValidity.Code, rateManualValidity.Name, serviceOrderDetail.ServiceDate.ToString("yyyy-mm-dd"))}
                End If
                rateManual = _rateManualRepository.GetRateManualById(rateManualValidityDetail.RateManualId)
            End If
        Else 'Es por que la condicion fue ninguna y la tarifa esta en la cabecera
            If result.ObjectEmbbeded.LiquidationType = 1 AndAlso result?.ObjectEmbbeded?.RateManualId IsNot Nothing Then
                rateManual = _rateManualRepository.GetRateManualById(result.ObjectEmbbeded.RateManualId)
            ElseIf result.ObjectEmbbeded.LiquidationType = 2 Then 'Si es estandar
                rateVariation = result.ObjectEmbbeded.RateVariation
                rateManual = _rateManualRepository.GetRateManualById(result.ObjectEmbbeded.RateManualId)
            ElseIf result.ObjectEmbbeded.LiquidationType = 3 Then 'Si es por vigencia
                rateVariation = result.ObjectEmbbeded.RateVariation
                Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(result.ObjectEmbbeded.RateManualValidityId)
                Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= serviceOrderDetail.ServiceDate AndAlso d.EndDate >= serviceOrderDetail.ServiceDate).FirstOrDefault
                If rateManualValidityDetail Is Nothing Then
                    Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = String.Format("El servicio IPS {0} no tiene parametrizado un manual de tarifas para la vigencia {1} - {2} en la fecha del servicio {3}", serviceOrderDetail.CodeNameIpsService, rateManualValidity.Code, rateManualValidity.Name, serviceOrderDetail.ServiceDate.ToString("yyyy-mm-dd"))}
                End If
                rateManual = _rateManualRepository.GetRateManualById(rateManualValidityDetail.RateManualId)
            End If
        End If
        'consulto el ips y el cups
        Dim IPSService = _ipsServiceRepository.GetIPSServiceById(serviceOrderDetail.IPSServiceId)
        Dim cupsEntity = _cupsEntityRepository.GetCupsEntityById(serviceOrderDetail.CUPSEntityId)
        serviceOrderDetail.SubTotalSalesPrice = 0
        serviceOrderDetail.RateManualSalePrice = 0
        serviceOrderDetail.TotalSalesPrice = 0
        Dim listOriginalSurgicalProcedures = _surgicalProcedureServiceRepository.GetByFilter(Function(x) x.IPSServiceParentId = serviceOrderDetail.IPSServiceId, False).ToList()
        Dim errorsSurgical As New StringBuilder()
        Dim listValidateClassServiceISS = {EClassService.Surgeon, EClassService.Anesthesiologist, EClassService.Assistant}.ToList()
        Dim listISSManualType = {1, 2}.ToList()
        Dim resultItemSurgical = Me.GetListSurgicalProcedureServiceWithClassByte(listSurgicalProcedureServiceDefault)

        'diccionario que guardar el material de sutura asociado a una sala y su regla
        Dim dictionaryRuleRightRoom = New Dictionary(Of Integer, Integer?)

        For Each itemDictionary In resultItemSurgical.OrderBy(Function(item) item.Value)
            Dim itemSurgical = itemDictionary.Key.Item1
            'esta validacion la hago por si el servicio es no cruento no busque los materiales de sutura en las tablas
            If itemSurgical?.ClassService?.ToUpper() = ResourceManager.GetString("SutureMaterials", "Contract").ToUpper() AndAlso serviceOrderDetail.SurgicalInterventionType = 9 Then
                Continue For
            End If

            'consulto el manual de tarifas, se incializan variables 
            Dim rateManualDetailSurgical As RateManualDetailSurgical = Nothing
            Dim rateManualSurgical As RateManual = rateManual
            Dim currentRateVariationSurgical As Decimal = rateVariation
            Dim round As Integer = 1
            Dim costValueItem As Decimal = 0
            Dim totalSalesPriceItem As Decimal = 0
            Dim ipsTmp = itemDictionary.Key.Item2
            Dim liquidationTypeSurgical As Integer? = 0
            Dim salesValueSurgical As Decimal = 0
            Dim salesValueWithSurchargeSurgical As Decimal = 0
            Dim applyRateManual = True

            ' Consutlar si existe el servicio IPS
            Dim resultIPS = GetRateValue(itemSurgical.IPSServiceId,
                                         serviceOrderDetail.CUPSEntityId,
                                         serviceOrderDetail.CareGroupId,
                                         serviceOrderDetail.PerformsFunctionalUnitId,
                                         serviceOrderDetail.PerformsProfessionalSpecialty,
                                         serviceOrderDetail.ServiceDate,
                                         ERateOptions.IPSSERVICE,
                                         Nothing,
                                         contractDescriptionId)

            Dim definitionRateDetailId As Integer? = Nothing

            If resultIPS?.ObjectEmbbeded Is Nothing AndAlso resultIPS?.ObjectEmbbededAux Is Nothing AndAlso Not resultIPS?.StateResult Then
                'si no existe una regla por item qx ips, busco en la tabla de DefinitionRateDetailSurgicalProcedures
                definitionRateDetailId = _surgicalProcedureServiceRepository.ValidateIPSServiceInProcedures(result.ObjectEmbbeded.DefinitionRateId, IPSService.Id, itemSurgical.IPSServiceId, serviceOrderDetail.CUPSEntityId)

                'Si el material de sutura No esta parametrizado el solo (ya sea independiente como regla o asociado al DefinitionRateDetailSurgicalProcedures )
                'y esta asociado a una sala y adicional No esta parametrizado en los detalles qx del servicio IPS padres => obtengo la regla de la sala para que la adopte el material de sutura
                If (definitionRateDetailId Is Nothing OrElse definitionRateDetailId = 0) _
                    AndAlso (ipsTmp.ServiceClass = EClassService.SutureMaterials) _
                    AndAlso dictionaryRuleRightRoom.ContainsKey(ipsTmp.Id) _
                    AndAlso Not listOriginalSurgicalProcedures?.Exists(Function(x) x.IPSServiceId = ipsTmp.Id) Then
                    definitionRateDetailId = dictionaryRuleRightRoom(ipsTmp.Id)
                End If

                If (definitionRateDetailId Is Nothing OrElse definitionRateDetailId = 0) Then
                    'si no existe en la tabla surgical lo saco de la *regla del Papa*
                    'el cual parte de CUPS, grupo , sub grupo .. hasta general etc..
                    resultIPS = GetRateValue(serviceOrderDetail.IPSServiceId,
                                             serviceOrderDetail.CUPSEntityId,
                                             serviceOrderDetail.CareGroupId,
                                             serviceOrderDetail.PerformsFunctionalUnitId,
                                             serviceOrderDetail.PerformsProfessionalSpecialty,
                                             serviceOrderDetail.ServiceDate,
                                             ERateOptions.CUPS,
                                             Nothing,
                                             contractDescriptionId)
                End If
            End If

            If resultIPS.ObjectEmbbeded Is Nothing AndAlso resultIPS.ObjectEmbbededAux Is Nothing AndAlso Not resultIPS.StateResult Then
                'Se valida si el servicio ips qx que se recorre esta parametrizado dentro de la tabla DefinitionRateDetailSurgicalProcedures
                If definitionRateDetailId IsNot Nothing AndAlso definitionRateDetailId > 0 Then
                    applyRateManual = False
                    'Saco la tarifa correspondiente al servicio qx que se va recorriendo
                    Dim resultSurgicalProcedures = GetRateValueConditionSurgicalProcedures(definitionRateDetailId, serviceOrderDetail.CUPSEntityId, serviceOrderDetail.CareGroupId, serviceOrderDetail.PerformsFunctionalUnitId, serviceOrderDetail.PerformsProfessionalSpecialty, serviceOrderDetail.ServiceDate, ERateOptions.CUPS, Nothing, contractDescriptionId)
                    'Variables que se asignan dependiendo de la tarifa que se obtuvo
                    Dim rateVariationSurgical As Decimal = 0
                    Dim rateManualId As Integer

                    If resultSurgicalProcedures.ObjectEmbbededAux IsNot Nothing Then 'Fue por que la tarifa se saco de una condicion
                        liquidationTypeSurgical = resultSurgicalProcedures.ObjectEmbbededAux.LiquidationType
                        If resultSurgicalProcedures.ObjectEmbbededAux.LiquidationType = 1 Then 'Si es fija
                            salesValueSurgical = resultSurgicalProcedures.ObjectEmbbededAux.SalesValue
                            salesValueWithSurchargeSurgical = resultSurgicalProcedures.ObjectEmbbededAux.SalesValueWithSurcharge
                        ElseIf resultSurgicalProcedures.ObjectEmbbededAux.LiquidationType = 2 Then 'Si es estandar
                            rateManualId = resultSurgicalProcedures.ObjectEmbbededAux.RateManualId
                            rateVariationSurgical = resultSurgicalProcedures.ObjectEmbbededAux.RateVariation
                        ElseIf resultSurgicalProcedures.ObjectEmbbededAux.LiquidationType = 3 Then 'Si es por vigencia
                            Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(resultSurgicalProcedures.ObjectEmbbededAux.RateManualValidityId)
                            Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= serviceOrderDetail.ServiceDate AndAlso d.EndDate >= serviceOrderDetail.ServiceDate).FirstOrDefault
                            If rateManualValidityDetail Is Nothing Then
                                Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = String.Format("El servicio IPS {0} no tiene parametrizado un manual de tarifas para la vigencia {1} - {2} en la fecha del servicio {3}", serviceOrderDetail.CodeNameIpsService, rateManualValidity.Code, rateManualValidity.Name, serviceOrderDetail.ServiceDate.ToString("yyyy-mm-dd"))}
                            End If
                            rateManualId = rateManualValidityDetail.RateManualId
                            rateVariationSurgical = resultSurgicalProcedures.ObjectEmbbededAux.RateVariation
                        End If

                    Else 'Es por que la condicion fue ninguna y la tarifa esta en la cabecera
                        liquidationTypeSurgical = resultSurgicalProcedures.ObjectEmbbeded.LiquidationType
                        If resultSurgicalProcedures.ObjectEmbbeded.LiquidationType = 1 Then 'Si es Fija
                            salesValueSurgical = resultSurgicalProcedures.ObjectEmbbeded.SalesValue
                            salesValueWithSurchargeSurgical = resultSurgicalProcedures.ObjectEmbbeded.SalesValueWithSurcharge
                        ElseIf resultSurgicalProcedures.ObjectEmbbeded.LiquidationType = 2 Then 'Si es estandar
                            rateManualId = resultSurgicalProcedures.ObjectEmbbeded.RateManualId
                            rateVariationSurgical = resultSurgicalProcedures.ObjectEmbbeded.RateVariation
                        ElseIf resultSurgicalProcedures.ObjectEmbbeded.LiquidationType = 3 Then 'Si es por vigencia
                            Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(resultSurgicalProcedures.ObjectEmbbeded.RateManualValidityId)
                            Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= serviceOrderDetail.ServiceDate AndAlso d.EndDate >= serviceOrderDetail.ServiceDate).FirstOrDefault
                            If rateManualValidityDetail Is Nothing Then
                                Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = String.Format("El servicio IPS {0} no tiene parametrizado un manual de tarifas para la vigencia {1} - {2} en la fecha del servicio {3}", serviceOrderDetail.CodeNameIpsService, rateManualValidity.Code, rateManualValidity.Name, serviceOrderDetail.ServiceDate.ToString("yyyy-mm-dd"))}
                            End If

                            rateManualId = rateManualValidityDetail.RateManualId
                            rateVariationSurgical = resultSurgicalProcedures.ObjectEmbbeded.RateVariation
                        End If
                    End If

                    If liquidationTypeSurgical Is Nothing OrElse liquidationTypeSurgical = 0 Then
                        errorsSurgical.AppendLine(String.Format(ResourceManager.GetString("NullLiquidationType", MODULE_NAME), ipsTmp.Code + " - " + ipsTmp.Name))
                        Continue For
                    End If

                    If liquidationTypeSurgical = 1 Then ''Fija
                        totalSalesPriceItem = salesValueSurgical
                    Else 'Estandar o Por Vigencia
                        Dim salesValueItem As Decimal = 0

                        If listISSManualType.Contains(IPSService.ServiceManual) Then
                            Dim resultISS = Me.LogicUVRRateManualISS(New RateManual With {.Id = rateManualId, .Type = IPSService.ServiceManual},
                                                                     IPSService,
                                                                     ipsTmp,
                                                                     itemSurgical.ServiceAmount)
                            If resultISS Is Nothing OrElse Not resultISS?.StateResult Then
                                errorsSurgical.AppendLine($"Error: {resultISS.Message}")
                                Continue For
                            End If
                            salesValueItem = resultISS.ObjectEmbbeded
                        Else
                            rateManualDetailSurgical = _rateManualDetailSurgicalRepository.GetSurgicalDetailServiceOrder(rateManualId, itemSurgical.IPSServiceId, IPSService.SurgicalGroupId, IPSService.UVRNumber, IPSService.ServiceManual)

                            If rateManualDetailSurgical Is Nothing Then
                                'valido que el ips este parametrizado en el manual de tarifas
                                errorsSurgical.AppendLine(String.Format(ResourceManager.GetString("NotParameterizedIPSSurgical", MODULE_NAME), ipsTmp.Code + " - " + ipsTmp.Name))
                                Continue For
                            End If

                            salesValueItem = rateManualDetailSurgical.SalesValue
                        End If

                        totalSalesPriceItem = salesValueItem + (salesValueItem * (rateVariationSurgical / 100))
                        totalSalesPriceItem = Utils.RoundValue(totalSalesPriceItem, rateManualSurgical.RoundService)
                    End If
                End If
            Else
                If resultIPS.ObjectEmbbededAux IsNot Nothing Then 'Fue por que la tarifa se saco de una condicion
                    liquidationTypeSurgical = resultIPS.ObjectEmbbededAux.LiquidationType
                    If resultIPS.ObjectEmbbededAux.LiquidationType = 1 Then 'Si es Fija
                        salesValueSurgical = resultIPS.ObjectEmbbededAux.SalesValue
                        salesValueWithSurchargeSurgical = resultIPS.ObjectEmbbededAux.SalesValueWithSurcharge
                    ElseIf resultIPS.ObjectEmbbededAux.LiquidationType = 2 Then 'Si es estandar
                        currentRateVariationSurgical = resultIPS.ObjectEmbbededAux.RateVariation
                        rateManualSurgical = _rateManualRepository.GetRateManualById(resultIPS.ObjectEmbbededAux.RateManualId)
                    ElseIf resultIPS.ObjectEmbbededAux.LiquidationType = 3 Then 'Si es por vigencia
                        currentRateVariationSurgical = resultIPS.ObjectEmbbededAux.RateVariation
                        Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(resultIPS.ObjectEmbbededAux.RateManualValidityId)
                        Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= serviceOrderDetail.ServiceDate AndAlso d.EndDate >= serviceOrderDetail.ServiceDate).FirstOrDefault
                        If rateManualValidityDetail Is Nothing Then
                            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = String.Format("El servicio IPS {0} no tiene parametrizado un manual de tarifas para la vigencia {1} - {2} en la fecha del servicio {3}", serviceOrderDetail.CodeNameIpsService, rateManualValidity.Code, rateManualValidity.Name, serviceOrderDetail.ServiceDate.ToString("yyyy-mm-dd"))}
                        End If
                        rateManualSurgical = _rateManualRepository.GetRateManualById(rateManualValidityDetail.RateManualId)
                    End If
                Else 'Es por que la condicion fue ninguna y la tarifa esta en la cabecera
                    If (resultIPS.ObjectEmbbeded.RateManualId Is Nothing OrElse resultIPS.ObjectEmbbeded.RateManualId = 0) AndAlso listISSManualType.Contains(resultIPS.ObjectEmbbeded.LiquidationType) Then
                        Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = String.Format("El servicio IPS {0} no tiene parametrizado un manual de tarifas ", serviceOrderDetail.CodeNameIpsService)}
                    End If
                    liquidationTypeSurgical = resultIPS.ObjectEmbbeded.LiquidationType
                    If resultIPS.ObjectEmbbeded.LiquidationType = 1 Then 'Si es Fija
                        salesValueSurgical = resultIPS.ObjectEmbbeded.SalesValue
                        salesValueWithSurchargeSurgical = resultIPS.ObjectEmbbeded.SalesValueWithSurcharge
                        rateManualSurgical = _rateManualRepository.GetRateManualById(resultIPS.ObjectEmbbeded.RateManualId)
                    ElseIf resultIPS.ObjectEmbbeded.LiquidationType = 2 Then 'Si es estandar
                        currentRateVariationSurgical = resultIPS.ObjectEmbbeded.RateVariation
                        rateManualSurgical = _rateManualRepository.GetRateManualById(resultIPS.ObjectEmbbeded.RateManualId)
                    ElseIf resultIPS.ObjectEmbbeded.LiquidationType = 3 Then 'Si es por vigencia
                        currentRateVariationSurgical = resultIPS.ObjectEmbbeded.RateVariation
                        Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(resultIPS.ObjectEmbbeded.RateManualValidityId)
                        Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= serviceOrderDetail.ServiceDate AndAlso d.EndDate >= serviceOrderDetail.ServiceDate).FirstOrDefault
                        If rateManualValidityDetail Is Nothing Then
                            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = String.Format("El servicio IPS {0} no tiene parametrizado un manual de tarifas para la vigencia {1} - {2} en la fecha del servicio {3}", serviceOrderDetail.CodeNameIpsService, rateManualValidity.Code, rateManualValidity.Name, serviceOrderDetail.ServiceDate.ToString("yyyy-mm-dd"))}
                        End If
                        rateManualSurgical = _rateManualRepository.GetRateManualById(rateManualValidityDetail.RateManualId)
                    End If
                End If
            End If

            'si el derecho de sala tiene asociado un material adiciono al diccionario el Id del material y la regla de la sala
            If ipsTmp.ServiceClass = EClassService.RightRoom AndAlso ipsTmp.AssociatedMaterialIPSServiceId > 0 Then
                dictionaryRuleRightRoom.Add(ipsTmp.AssociatedMaterialIPSServiceId, If(definitionRateDetailId > 0, definitionRateDetailId, resultIPS?.ObjectEmbbeded?.Id))
            End If

            If applyRateManual Then
                If rateManualSurgical Is Nothing Then
                    errorsSurgical.AppendLine(String.Format("No se encotro tarifa para el detalle Qx {0} - {1}", ipsTmp.Code, ipsTmp.Name))
                    Continue For
                End If

                If liquidationTypeSurgical Is Nothing OrElse liquidationTypeSurgical = 0 Then
                    errorsSurgical.AppendLine(String.Format(ResourceManager.GetString("NullLiquidationType", MODULE_NAME), ipsTmp.Code + " - " + ipsTmp.Name))
                    Continue For
                End If

                If liquidationTypeSurgical = 1 Then ''Fija
                    totalSalesPriceItem = salesValueSurgical
                Else
                    'valido el tipo del manual de tarifas - Tipo del manual tarifario 1 - ISS 2001 , 2 - ISS 2004,  3 - SOAT
                    If rateManualSurgical.Type < 3 Then

                        Dim resultISS = Me.LogicUVRRateManualISS(New RateManual With {.Id = rateManualSurgical.Id, .Type = IPSService.ServiceManual},
                                                                     IPSService,
                                                                     ipsTmp,
                                                                     itemSurgical.ServiceAmount)

                        If resultISS Is Nothing OrElse Not resultISS?.StateResult Then
                            errorsSurgical.AppendLine($"Error: {resultISS.Message}")
                            Continue For
                        End If
                        costValueItem = resultISS.ObjectEmbbeded
                    Else
                        rateManualDetailSurgical = _rateManualDetailSurgicalRepository.GetSurgicalDetailServiceOrder(rateManualSurgical.Id, itemSurgical.IPSServiceId, IPSService.SurgicalGroupId, IPSService.UVRNumber, IPSService.ServiceManual)
                        If rateManualDetailSurgical Is Nothing Then
                            'valido que el ips este parametrizado en el manual de tarifas
                            errorsSurgical.AppendLine(String.Format(ResourceManager.GetString("NotParameterizedIPSSurgical", MODULE_NAME), ipsTmp.Code + " - " + ipsTmp.Name))
                            Continue For
                        End If
                        If serviceOrderDetail.SurchargeApply = True Then
                            costValueItem = rateManualDetailSurgical.SalesValueWithSurcharge * itemSurgical.ServiceAmount
                        Else
                            costValueItem = rateManualDetailSurgical.SalesValue * itemSurgical.ServiceAmount
                        End If
                    End If

                    totalSalesPriceItem = costValueItem + (costValueItem * (currentRateVariationSurgical / 100))
                    totalSalesPriceItem = Utils.RoundValue(totalSalesPriceItem, rateManualSurgical.RoundService)
                    round = rateManualSurgical.RoundService
                End If
            End If

            serviceOrderDetail.RateManualSalePrice += totalSalesPriceItem
            Dim serviceOrderDetailSurgical = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = itemSurgical.IPSServiceId).FirstOrDefault()
            'agrego un registro solo cuando no se encuentre en la entidad del detalle quirurgiico
            If serviceOrderDetailSurgical Is Nothing Then

                serviceOrderDetailSurgical = New ServiceOrderDetailSurgical
                With serviceOrderDetailSurgical
                    .CodeNameIpsService = ipsTmp.Code + " - " + ipsTmp.Name
                    .IPSServiceId = itemSurgical.IPSServiceId
                    .InvoicedQuantity = itemSurgical.ServiceAmount
                    .LiquidationPercentage = 0
                    .TotalSalesPrice = totalSalesPriceItem
                    .RateManualSalePrice = totalSalesPriceItem
                    .ClassServiceIps = itemSurgical.ClassService
                    If itemSurgical.ClassService?.ToUpper() = ResourceManager.GetString("Surgeon", "Contract")?.ToUpper Then
                        .PerformsHealthProfessionalCode = serviceOrderDetail.PerformsHealthProfessionalCode
                        .PerformsHealthProfessionalThirdPartyId = serviceOrderDetail.PerformsHealthProfessionalThirdPartyId
                    End If
                    .CostValue = serviceOrderDetail.CostValue
                    If ipsTmp.BillingConceptId Is Nothing Then
                        errorsSurgical.AppendLine("El servicio IPS " + ipsTmp.Code + " - " + ipsTmp.Name + " no tiene asignado concepto de facturación")
                        Continue For
                    End If
                    .BillingConceptId = ipsTmp.BillingConceptId
                    .CostCenterId = serviceOrderDetail.CostCenterId
                    If rateManualDetailSurgical IsNot Nothing Then
                        .RateManualDetailSurgicalId = rateManualDetailSurgical.Id
                    End If
                    .SurchargeApply = serviceOrderDetail.SurchargeApply
                    .RoundService = round
                End With
                serviceOrderDetail.ServiceOrderDetailSurgical.Add(serviceOrderDetailSurgical)
            End If
        Next
        serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
        serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
        serviceOrderDetail.GrossValue = serviceOrderDetail.SubTotalSalesPrice

        If errorsSurgical.Length > 0 Then
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = errorsSurgical.ToString()}
        End If
        serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
        serviceOrderDetail.RoundService = 1
        Return New ActionResult(Of ServiceOrderDetail) With {.ObjectEmbbeded = serviceOrderDetail, .StateResult = True}
    End Function

    ''' <summary>
    ''' funcion que lleva acabo la logica para obtener los valores de servicios qx ISS
    ''' </summary>
    ''' <returns></returns>
    Private Function LogicUVRRateManualISS(rateManualSurgical As RateManual,
                                           iPSServiceParent As IPSService,
                                           iPSServiceChild As IPSService,
                                           serviceAmount As Decimal,
                                           Optional surchargeApply As Boolean = False) As ActionResult(Of Decimal)

        Dim costValueItem As Decimal = 0
        Dim errorMessage As String = "El parámetro es obligatorio {0}"

        If rateManualSurgical Is Nothing Then
            Return New ActionResult(Of Decimal) With {.StateResult = False, .ObjectEmbbeded = 0, .Message = String.Format(errorMessage, NameOf(rateManualSurgical))}
        End If

        If iPSServiceParent Is Nothing Then
            Return New ActionResult(Of Decimal) With {.StateResult = False, .ObjectEmbbeded = 0, .Message = String.Format(errorMessage, NameOf(iPSServiceParent))}
        End If

        If iPSServiceChild Is Nothing Then
            Return New ActionResult(Of Decimal) With {.StateResult = False, .ObjectEmbbeded = 0, .Message = String.Format(errorMessage, NameOf(iPSServiceChild))}
        End If

        If rateManualSurgical.Type >= 3 Then
            Return New ActionResult(Of Decimal) With {.StateResult = False, .ObjectEmbbeded = 0, .Message = "El tipo de manual no es ISS"}
        End If

        If {EClassService.Surgeon, EClassService.Anesthesiologist, EClassService.Assistant}.ToList().Contains(iPSServiceChild.ServiceClass) Then ' 2 - Cirujano,  3 - Anestesiologo, 4 - Ayudante
            If iPSServiceParent.UVRNumber > UVRNUMBER AndAlso iPSServiceChild.ApplyChangeScore Then
                costValueItem = iPSServiceParent.UVRNumber * iPSServiceChild.NewScore * serviceAmount
            Else
                costValueItem = iPSServiceParent.UVRNumber * iPSServiceChild.Score * serviceAmount
            End If
        Else
            If iPSServiceChild.ServiceClass = EClassService.RightRoom AndAlso iPSServiceParent.UVRNumber > UVRNUMBER AndAlso iPSServiceChild.ApplyChangeScore Then '5 - Derecho Sala
                costValueItem = iPSServiceParent.UVRNumber * iPSServiceChild.NewScore * serviceAmount
            Else
                Dim rateManualDetailSurgical = _rateManualDetailSurgicalRepository.GetSurgicalDetailServiceOrder(rateManualSurgical.Id, iPSServiceChild.Id, Nothing, iPSServiceParent.UVRNumber, iPSServiceParent.ServiceManual)

                If rateManualDetailSurgical Is Nothing Then
                    'valido que el ips este parametrizado en el manual de tarifas
                    Return New ActionResult(Of Decimal) With {.StateResult = False, .ObjectEmbbeded = 0, .Message = String.Format(ResourceManager.GetString("NotParameterizedIPSSurgical", MODULE_NAME), iPSServiceChild.Code + " - " + iPSServiceChild.Name)}
                End If

                If surchargeApply Then
                    costValueItem = rateManualDetailSurgical.SalesValueWithSurcharge * serviceAmount
                Else
                    costValueItem = rateManualDetailSurgical.SalesValue * serviceAmount
                End If
            End If
        End If

        Return New ActionResult(Of Decimal) With {.StateResult = True, .ObjectEmbbeded = costValueItem, .Message = "Valor generado exitosamente"}

    End Function

    ''' <summary>
    ''' Funcion para obtener el diccionario con la consulta masiva de cada ips qx y su respectiva clase
    ''' </summary>
    ''' <param name="listSurgical"> lista de tipo SurgicalProcedureService</param>
    ''' <returns></returns>
    Private Function GetListSurgicalProcedureServiceWithClassByte(listSurgical As List(Of SurgicalProcedureService)) As Dictionary(Of Tuple(Of SurgicalProcedureService, IPSService), Byte)
        If listSurgical Is Nothing Then
            Return New Dictionary(Of Tuple(Of SurgicalProcedureService, IPSService), Byte)
        End If

        Dim IpsIds = listSurgical.GroupBy(Function(x) x.IPSServiceId).Select(Function(ips) ips.Key).ToList()
        Dim query = _ipsServiceRepository.GetByFilter(Function(item) IpsIds.Contains(item.Id), False).ToList()

        Dim dictionary = New Dictionary(Of Tuple(Of SurgicalProcedureService, IPSService), Byte)

        listSurgical.ForEach(Sub(item)
                                 Dim ipsTmp = query.Find(Function(f) f.Id = item.IPSServiceId)
                                 dictionary.Add(New Tuple(Of SurgicalProcedureService, IPSService)(item, ipsTmp), ipsTmp.ServiceClass)
                             End Sub)

        Return dictionary
    End Function

    ''' <summary>
    ''' metodo para obtener las homologaciones del cups
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    ''' <param name="CupsId"></param>
    ''' <param name="FunctionalUnitId"></param>
    ''' <param name="Specialty"></param>
    ''' <param name="ServiceDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHomologationCups(CareGroupId As Integer, CupsId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, IPSServiceId As Integer, Optional ManualType As Integer = 0, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of List(Of CupsHomologation)) Implements IContractServices.GetHomologationCups
        'Listado que se retorna
        Dim listHomologation As New List(Of CupsHomologation)
        Dim serviceType As Integer? = Nothing
        Dim cupsHomologations = _cupsHomologationRepository.ListCupsHomologationByCupsId(CupsId, 0)
        Dim cups = _cupsEntityRepository.FirstOrDefault(Function(m) m.Id = CupsId, False, {"CupsSubgroup"})
        Dim definitionRate = GetCareGroupDefinitionRate(CareGroupId, ServiceDate)
        Dim errors As New List(Of String)()


        For Each h In cupsHomologations
            ' se consulta las definiciones
            Dim listRateManualDetail = _definitionRateDetailRepository.GetRateManualDetailByCupsHomologation(definitionRate.DefinitionRateId, cups, h)
            ' se establece la bandera en false
            Dim FlagValidate As Boolean = False

            For Each rateManualDetail In listRateManualDetail

                'se valida si ya fue validado osea agregado en lista de homologos para saltarlo 
                'y no agregar posiblemente otro homologo del mismo servicio ips de forma inecesaria
                If FlagValidate Then
                    Continue For
                End If

                If rateManualDetail IsNot Nothing Then
                    h.RuleType = rateManualDetail?.RuleType
                    Dim rateManualId As Integer? = rateManualDetail?.RateManualId
                    Dim manualTtype As Byte? = rateManualDetail?.Type

                    If rateManualDetail.LiquidationType = 3 Then
                        ' Tipo de liquidación por vigencia: Busamos la vigencia y con este el manual de tarifas
                        Dim rateManualValidityDetail = _rateManualValidityDetailRepository _
                            .Query(Function(m) m.RateManualValidityId = rateManualDetail.RateManualValidityId AndAlso m.InitialDate <= ServiceDate AndAlso m.EndDate >= ServiceDate) _
                            .Select(Function(m) New With {Key m.RateManualId, Key m.RateManual.Type}) _
                            .FirstOrDefault()

                        If rateManualValidityDetail IsNot Nothing Then
                            rateManualId = rateManualValidityDetail.RateManualId
                            manualTtype = rateManualValidityDetail.Type
                        End If
                    End If

                    'si el tipo de condicion es por Descripcion y el tipo de liquidacion No esta definida en la cabecera
                    If {3, 7}.Contains(rateManualDetail.ConditionType) AndAlso rateManualDetail.LiquidationType Is Nothing Then

                        'valido que el tipo del servicio  sea igual (IIS Soat) a los parametrizados
                        Dim DefinitionRateDetailCondition As DefinitionRateDetailCondition
                        Select Case rateManualDetail.ConditionType
                            Case 7
                                'si la regal esta por descripcion Pero no viene una descripcion relacionada salta al siguiente homologo
                                If ContractDescriptionId Is Nothing Then
                                    Continue For
                                End If

                                DefinitionRateDetailCondition = _definitionRateDetailConditionRepository.FirstOrDefault(Function(x) x.DefinitionRateDetailId = rateManualDetail.DefinitionRateDetailId AndAlso x.ContractDescriptionId = ContractDescriptionId, False, {"RateManual"})
                            Case 3

                                If FunctionalUnitId = 0 Then
                                    Continue For
                                End If

                                DefinitionRateDetailCondition = _definitionRateDetailConditionRepository.FirstOrDefault(Function(x) x.DefinitionRateDetailId = rateManualDetail.DefinitionRateDetailId AndAlso x.FunctionalUnitId = FunctionalUnitId, False, {"RateManual"})
                            Case Else
                                DefinitionRateDetailCondition = Nothing
                        End Select

                        If DefinitionRateDetailCondition Is Nothing Then
                            Continue For
                        End If

                        'se verifica el tipo de liquidacion Fija, Manual, o por vigencia
                        Select Case DefinitionRateDetailCondition.LiquidationType
                            Case 1

                                If DefinitionRateDetailCondition?.ManualType = h?.IPSService?.ServiceManual Then
                                    FlagValidate = True
                                    listHomologation.Add(h)
                                    Continue For
                                Else
                                    Continue For
                                End If

                            Case 2
                                rateManualId = DefinitionRateDetailCondition?.RateManualId
                                manualTtype = DefinitionRateDetailCondition?.RateManual?.Type
                            Case 3
                                Dim Rate = _rateManualValidityDetailRepository.GetRateManualTypeAndRateManualId(DefinitionRateDetailCondition.RateManualValidityId, ServiceDate)

                                If Rate Is Nothing Then
                                    Continue For
                                Else
                                    rateManualId = Rate.RateManualId
                                    manualTtype = Rate.Type
                                End If

                        End Select
                        'si pasa por tipo manual o vigencia, actualizamos el tipo de liquidacion para reutilizar validacion de mas abajo
                        rateManualDetail.LiquidationType = DefinitionRateDetailCondition?.LiquidationType
                    End If

                    If h.IPSService.Presentation = 2 Then
                        If manualTtype = h.IPSService.ServiceManual Then
                            'Se valida si el servicio ips qx que se recorre esta parametrizado dentro de la tabla DefinitionRateDetailSurgicalProcedures
                            Dim procedures = _surgicalProcedureServiceRepository.Query(Function(m) m.IPSServiceParentId = h.IPSServiceId, includes:={"IPSService"}).ToList()
                            Dim cupsEntityContractDescriptionId As Integer? = Nothing
                            If ContractDescriptionId.HasValue Then
                                cupsEntityContractDescriptionId = _cupsEntityContractDescriptionRepository.Query(Function(m) m.CUPSEntityId = h.CupsEntityId AndAlso m.ContractDescriptionId = ContractDescriptionId) _
                            .Select(Function(m) m.Id).FirstOrDefault()
                            End If

                            Dim sod As New ServiceOrderDetail With {
                            .IPSServiceId = h.IPSServiceId,
                            .CUPSEntityId = CupsId,
                            .CareGroupId = CareGroupId,
                            .PerformsFunctionalUnitId = FunctionalUnitId,
                            .PerformsProfessionalSpecialty = Specialty,
                            .ServiceDate = ServiceDate,
                            .SurchargeApply = False,
                            .SurgicalInterventionType = 0,
                            .CUPSEntityContractDescriptionId = cupsEntityContractDescriptionId
                        }
                            'se mandan a validar solo los qx por defecto
                            Dim res = GetServiceValueBySurgicalProcedureService(sod, procedures.FindAll(Function(x) x.DefaultService))

                            If res.StateResult Then
                                FlagValidate = True
                                listHomologation.Add(h)
                            Else
                                errors.Add(res.Message)
                            End If
                        End If
                    Else
                        If rateManualId.HasValue Then
                            If rateManualDetail.LiquidationType.HasValue AndAlso {2, 3}.Contains(rateManualDetail.LiquidationType) Then
                                ' Tipo de liquidación Estandar: Buscamos el servicio en el manual de tarifas
                                Dim existsRateManual = _rateManualDetailRepository.Any(Function(m) m.RateManualId = rateManualId AndAlso m.IPSServiceId = h.IPSServiceId)

                                If existsRateManual Then
                                    FlagValidate = True
                                    listHomologation.Add(h)
                                End If
                            Else
                                FlagValidate = True
                                listHomologation.Add(h)
                            End If
                        ElseIf rateManualDetail.ConditionType <> 5 Then
                            FlagValidate = True
                            listHomologation.Add(h)
                        End If
                    End If
                End If

            Next
        Next

        Dim result As New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = True, .ObjectEmbbeded = listHomologation}
        If listHomologation Is Nothing OrElse listHomologation.Count = 0 Then
            If errors.Any() Then
                result.Message = String.Join(vbCrLf, errors.Distinct().ToArray())
                result.StateResult = False
                Return result
            End If

            Dim ipsServiceFullName As String = String.Empty
            If IPSServiceId <> 0 Then
                Dim ipsService As IPSService = GetIpsService(IPSServiceId)
                If ipsService IsNot Nothing AndAlso ipsService.Id > 0 Then
                    ipsServiceFullName = String.Concat(ipsService.Code, " - ", ipsService.Name)
                End If
            Else
                'Dim cups As CUPSEntity = GetCupsEntity(CupsId, False)
                If cups IsNot Nothing AndAlso cups.Id > 0 Then
                    ipsServiceFullName = String.Concat(cups.Code, " - ", cups.Description)
                End If
            End If

            Dim caregroup As CareGroup = GetCareGroup(CareGroupId)
            Dim careGroupFullName As String = String.Concat(caregroup.Code, " - ", caregroup.Name)
            result.Message = String.Format("No se Encontraron Homologos para el item {0} para el grupo de atención {1}", ipsServiceFullName, careGroupFullName)
            result.StateResult = False

            'se busca si solo existe un unico homologo con regla a servicio IPS dentro de la lista de homologos 
        ElseIf listHomologation.FindAll(Function(x) x.RuleType IsNot Nothing AndAlso x.RuleType = 1).Count = 1 Then
            'remuevo los otros homologos para dejar la regla a servicio IPS
            listHomologation.RemoveAll(Function(x) x.RuleType Is Nothing OrElse x.RuleType <> 1)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Obtiene el mensaje de error de homologación
    ''' </summary>
    ''' <param name="CareGroupId">The care group identifier.</param>
    ''' <param name="CupsId">The cups identifier.</param>
    ''' <returns></returns>
    Private Function GetHomologationErrorMessage(CareGroupId As Integer, CupsId As Integer) Implements IContractServices.GetHomologationErrorMessage
        Dim caregroup As CareGroup = _careGroupRepository.GetCareGroupById(CareGroupId)
        Dim cupsEntity As CUPSEntity = GetCupsEntity(CupsId, False)
        Return String.Format("No se encontro tarifa para el item {0} en el grupo de atención {1}", String.Concat(cupsEntity.Code, " - ", cupsEntity.Description), String.Concat(caregroup.Code, " - ", caregroup.Name))
    End Function


#End Region

#Region "Enums"
    Public Enum eLiquidationType
        UnitType = 1
        Speciality = 2
        FunctionalUnit = 3
        Fixed = 4
        Standard = 5
        Formula = 6
    End Enum
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _rateManualValidityRepository = Nothing
            _rateManualRepository = Nothing
            _cupsHomologationRepository = Nothing
            _careGroupRepository = Nothing
            _cupsEntityRepository = Nothing
            _ipsServiceRepository = Nothing
            _careGroupDefinitionRateRepository = Nothing
            _definitionRateDetailConditionRepository = Nothing
            _definitionRateDetailRepository = Nothing
            _functionalUnitRepository = Nothing
            _procedureCupsRepository = Nothing
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
