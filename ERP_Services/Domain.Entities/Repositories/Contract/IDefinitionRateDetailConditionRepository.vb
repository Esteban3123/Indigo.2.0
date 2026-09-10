'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 18/11/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IDefinitionRateDetailConditionRepository
    Inherits IRepository(Of DefinitionRateDetailCondition)
    ''' <summary>
    ''' obtiene un detalle de la definicion por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailConditionById(Id As Integer, Optional tracking As Boolean = True) As DefinitionRateDetailCondition
    ''' <summary>
    ''' obtiene una condicion por tiempo
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="time"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailConditionByTime(definitionRateDetailId As Integer, time As TimeSpan) As DefinitionRateDetailCondition
    ''' <summary>
    ''' obtiene una condicion por especialidad
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="specialty"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailConditionBySpecialty(definitionRateDetailId As Integer, specialty As String) As DefinitionRateDetailCondition
    ''' <summary>
    ''' obtiene una condicion por unidad funcional
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="functionalUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailConditionByFunctionalUnit(definitionRateDetailId As Integer, functionalUnitId As Integer) As DefinitionRateDetailCondition
    ''' <summary>
    ''' obtiene una condicion por tipo de unidad
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="unitTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailConditionByUnitType(definitionRateDetailId As Integer, unitTypeId As Integer) As DefinitionRateDetailCondition

    ''' <summary>
    ''' Metodo para buscar por dos condiciones las cuales son Horario y Especialidad
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailByDefinitionRateDetailId(definitionRateDetailId As Integer) As List(Of DefinitionRateDetailCondition)

    ''' <summary>
    ''' obtiene una condicion por rias
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="riasId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailConditionByRiasId(definitionRateDetailId As Integer, riasId As Integer) As DefinitionRateDetailCondition

    ''' <summary>
    ''' obtiene una condicion por descripcion
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="descriptionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailConditionByDescriptionId(definitionRateDetailId As Integer, descriptionId As Integer) As DefinitionRateDetailCondition

End Interface
