Imports Domain.Base.Entities

Public Interface IContractServices
    Inherits IDisposable

    Function GetHomologationCupsByListCUPS(ParamArray parameters As Object()) As ActionResult(Of List(Of CupsHomologation))

    Function GetHomologationCups(CareGroupId As Integer, CupsId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, IPSServiceId As Integer, Optional ManualType As Integer = 0, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of List(Of CupsHomologation))

    Function GetHomologationErrorMessage(CareGroupId As Integer, CupsId As Integer)

    Function GetRateValue(IPSServiceId As Integer, CupsEntityId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, initialService As ERateOptions, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition)

    Function GetRateValueConditionSurgicalProcedures(DefinitionRateDetailId As Integer, CupsEntityId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, initialService As ERateOptions, RiasId As Integer?, ContractDescriptionId As Integer?) As ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition)

    Function GetDefinitionRateDetailList(definitionRateId As Integer, CupsEntityId As Integer, eRateOptions As ERateOptions, IPSServiceId As Integer) As List(Of DefinitionRateDetail)
    Function GetDefinitionRateDetailByDefinitionRateDetailId(definitionRateDetailId As Integer) As List(Of DefinitionRateDetailCondition)
    Function getConditionByTimeSpecialty(time As TimeSpan, specialtyId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByTimeFunctionalUnit(time As TimeSpan, functionalUnitId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByTimeUnitType(time As TimeSpan, unitType As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByTimeRias(time As TimeSpan, riasId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByTimeDescription(time As TimeSpan, descriptionId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionBySpecialtyTime(specialtyId As String, time As TimeSpan, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionBySpecialtyFunctionalUnit(specialtyId As String, functionalUnitId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionBySpecialtyUnitType(specialtyId As String, unitType As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionBySpecialtyRias(specialtyId As String, riasId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionBySpecialtyDescription(specialtyId As String, descriptionId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByFunctionalUnitTime(functionalUnitId As Integer, time As TimeSpan, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByFunctionalUnitSpecialty(functionalUnitId As Integer, specialtyId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByFunctionalUnitUnitType(functionalUnitId As Integer, unitType As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByFunctionalUnitRias(functionalUnitId As Integer, riasId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByFunctionalUnitDescription(functionalUnitId As Integer, descriptionId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByUnitTypeTime(unitTypeId As Integer, time As TimeSpan, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByUnitTypeSpecialty(unitTypeId As Integer, specialtyId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByUnitTypeFunctionalUnit(unitTypeId As Integer, functionalUnitId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByUnitTypeRias(unitTypeId As Integer, riasId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByUnitTypeDescription(unitTypeId As Integer, descriptionId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByRiasTime(riasId As Integer, time As TimeSpan, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByRiasSpecialty(riasId As Integer, specialtyId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByRiasFunctionalUnit(riasId As Integer, functionalUnitId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByRiasUnitType(riasId As String, unitType As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByRiasDescription(riasId As Integer, descriptionId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByDescriptionTime(descriptionId As Integer, time As TimeSpan, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByDescriptionSpecialty(descriptionId As Integer, specialtyId As String, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByDescriptionFunctionalUnit(descriptionId As Integer, functionalUnitId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByDescriptionUnitType(descriptionId As String, unitType As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function getConditionByDescriptionRias(descriptionId As Integer, riasId As Integer, detailConditionList As List(Of DefinitionRateDetailCondition)) As DefinitionRateDetailCondition
    Function GetServiceValueBySurgicalProcedureService(serviceOrderDetail As ServiceOrderDetail, listSurgicalProcedureServiceDefault As List(Of SurgicalProcedureService)) As ActionResult(Of ServiceOrderDetail)
End Interface
