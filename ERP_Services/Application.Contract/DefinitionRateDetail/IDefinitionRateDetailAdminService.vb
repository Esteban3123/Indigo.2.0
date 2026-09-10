'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IDefinitionRateDetailAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveDefinitionRateDetail(ByVal DefinitionRateDetail As DefinitionRateDetail, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of DefinitionRateDetail)

    ''' <summary>
    ''' Elimina
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteDefinitionRateDetail(ByVal DefinitionRateDetail As DefinitionRateDetail, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un detalle de una definicion de tarifa por id
    ''' </summary>
    ''' <returns></returns>
    Function GetDefinitionRateDetailById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of DefinitionRateDetail)

    ''' <summary>
    ''' Obtiene el listado de condiciones del detalle de la definicion de tarifa
    ''' </summary>
    ''' <returns></returns>
    Function GetListDefinitionRateDetailConditionByDefinitionRateDetailId(definitionRateDetailId As Integer, ByVal audit As AuditMessage) As ActionResult(Of List(Of DefinitionRateDetailCondition))

    ''' <summary>
    ''' exporta la estructura detalle de la definicion de tarifa de forma Limpia (solo las columnas)
    ''' </summary>
    ''' <returns></returns>
    Function ExportCleanStructure() As ActionResult(Of Byte())

    ''' <summary>
    ''' importa datos de una estructura excel y retorna la lista de la entidad DefinitionRateDetail
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ImportDataToAdd(dataImportFile As List(Of ImportFileRow), audit As AuditMessage) As ActionResult(Of List(Of DefinitionRateDetail))

    ''' <summary>
    ''' funcion para exportar la estructura de DefinitionRateDetailCondition por codigo de la definicion de tarifas
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Function ExportConditionStructureByCode(code As String) As ActionResult(Of Byte())

    ''' <summary>
    ''' funcion para exportar la estructura de DefinitionRateDetailCondition por Id de la definicion de tarifas
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function ExportConditionStructureById(id As Integer) As ActionResult(Of Byte())

    ''' <summary>
    ''' funcion que importa las condiciones de las reglas de la definicion de tarifa
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ImportConditionDataToAdd(dataImportFile As List(Of ImportFileRow), audit As AuditMessage) As ActionResult(Of List(Of DefinitionRateDetailCondition))

End Interface
