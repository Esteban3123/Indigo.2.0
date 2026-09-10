'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IDefinitionRateDetailRepository
    Inherits IRepository(Of DefinitionRateDetail)

    ''' <summary>
    ''' Obtiene un detalle de definicion de tarifa por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailById(id As Integer, Optional tracking As Boolean = True) As DefinitionRateDetail

    ''' <summary>
    ''' Obtiene el listado de condiciones de los detalles de la definicion de tarifa
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListDefinitionRateDetailConditionByDefinitionRateDetailId(definitionRateDetailId As Integer) As List(Of DefinitionRateDetailCondition)
    ''' <summary>
    ''' obtiene un detalle de la definicion de tarifa por el id del servicio ips
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <param name="ipsServiceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailByIPSServiceId(definitionRateId As Integer, ipsServiceId As Integer) As List(Of DefinitionRateDetail)
    ''' <summary>
    ''' obtiene un detalle de la definicion de tarifa por el id del cups
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <param name="cupsId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailByCUPSEntityId(definitionRateId As Integer, cupsId As Integer) As List(Of DefinitionRateDetail)
    ''' <summary>
    ''' obtiene un detalle de la definicion de tarifa por el id del subgrupo
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <param name="subgroupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailByCUPSSubGroupId(definitionRateId As Integer, subgroupId As Integer) As List(Of DefinitionRateDetail)
    ''' <summary>
    ''' obtiene un detalle de la definicion de tarifa por el id del grupo
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <param name="groupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailByCUPSGroupId(definitionRateId As Integer, groupId As Integer) As List(Of DefinitionRateDetail)
    ''' <summary>
    ''' obtiene un detalle de la definicion de tarifa por el tipo de regla
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <param name="groupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateDetailByRuleType(definitionRateId As Integer, ruleType As Integer) As List(Of DefinitionRateDetail)

    ''' <summary>
    ''' Obtiene las reglas de tipo servicio ips que estan en la definición de tarifas asociado al grupo de atención
    ''' </summary>
    ''' <param name="careGroupId"></param>
    ''' <param name="requestDate"></param>
    ''' <param name="cupsEntityId"></param>
    ''' <returns></returns>
    Function GetIPSRules(careGroupId As Integer, requestDate As Date, cupsEntityId As Integer) As List(Of InfoEntityTemp)

    ''' <summary>
    ''' Obtiene las reglas de tipo servicio ips que estan en la definición de tarifas asociado al contrato de centro de atencion externo en central de mezclas
    ''' </summary>
    ''' <param name="ContractExternalClientId"></param>
    ''' <param name="requestDate"></param>
    ''' <param name="cupsEntityId"></param>
    ''' <returns></returns>
    Function GetIPSRulesMs(ContractExternalClientId As Integer, requestDate As Date, cupsEntityId As Integer) As List(Of InfoEntityTemp)

    ''' <summary>
    ''' Obtiene la definción de tarifa segpun el tipo de manual tarifario.
    ''' </summary>
    ''' <param name="DefinitionRateId"></param>
    ''' <param name="cups"></param>
    ''' <param name="cupsHomologation"></param>
    ''' <returns></returns>
    Function GetRateManualDetailByCupsHomologation(DefinitionRateId As Integer, cups As CUPSEntity, cupsHomologation As CupsHomologation) As List(Of DefinitionRateDetailByManual)

    ''' <summary>
    ''' Consulta masiva de CUPS,IPS,Grupo CUPS,Sub Grupo CUPS,Manual,Vigencia Manual, para la importacion de datos
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Function GetQueryToImportData(obj As DefinitionDetailQuery) As DefinitionDetailQuery

    ''' <summary>
    ''' Consulta masiva de Especialidad,Unidad funcional, RIAS, Descripcion,Manual,Vigencia Manual, para la importacion de datos
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Function GetQueryToImportCoditionData(obj As DefinitionDetailQuery) As DefinitionDetailQuery
End Interface
