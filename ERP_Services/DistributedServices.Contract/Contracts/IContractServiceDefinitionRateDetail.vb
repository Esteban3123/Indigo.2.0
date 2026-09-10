'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface IContractServiceDefinitionRateDetail

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveDefinitionRateDetail(DefinitionRateDetail As Domain.Entities.DefinitionRateDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DefinitionRateDetail)

    ''' <summary>
    ''' Elimina
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteDefinitionRateDetail(DefinitionRateDetail As Domain.Entities.DefinitionRateDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un detalle de una definicion de tarifa por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDefinitionRateDetailById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DefinitionRateDetail)

    ''' <summary>
    ''' Obtiene el listado de condiciones del detalle de la definicion de tarifa
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListDefinitionRateDetailConditionByDefinitionRateDetailId(definitionRateDetailId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.DefinitionRateDetailCondition))


    ''' <summary>
    ''' exporta la estructura detalle de la definicion de tarifa de forma Limpia (solo las columnas)
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ExportCleanStructure() As ActionResult(Of Byte())

    ''' <summary>
    ''' importa datos de una estructura excel y retorna la lista de la entidad DefinitionRateDetail
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportDataToAdd(dataImportFile As List(Of ImportFileRow), audit As AuditMessage) As ActionResult(Of List(Of DefinitionRateDetail))

    ''' <summary>
    ''' funcion para exportar la estructura de DefinitionRateDetailCondition por codigo de la definicion de tarifas
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ExportConditionStructureByCode(code As String) As ActionResult(Of Byte())

    ''' <summary>
    ''' funcion para exportar la estructura de DefinitionRateDetailCondition por Id de la definicion de tarifas
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ExportConditionStructureById(id As Integer) As ActionResult(Of Byte())

    ''' <summary>
    ''' Servicio que importa las condiciones de las reglas de definicion de tarifas
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportConditionDataToAdd(dataImportFile As List(Of ImportFileRow), audit As AuditMessage) As ActionResult(Of List(Of DefinitionRateDetailCondition))
End Interface
