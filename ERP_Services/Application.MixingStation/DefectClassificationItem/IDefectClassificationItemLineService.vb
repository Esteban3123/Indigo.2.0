'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 21-09-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IDefectClassificationItemLineService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos defectos
    ''' </summary>
    ''' <returns>Lista de tipos de dosis unitaria</returns>
    Function ListAllDefectClassificationItem(ByVal audit As AuditMessage) As List(Of DefectClassificationItem)

    ''' <summary>
    ''' Guarda un Defecto
    ''' </summary>
    ''' <param name="DefectClassificationItem">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function SaveDefectClassificationItem(ByVal defectClassificationItem As DefectClassificationItem, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of DefectClassificationItem)

    ''' <summary>
    ''' Guarda un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="defectsUnitDoseType">The identifier.</param>
    Function SaveUnitDoseType(ByVal defectsUnitDoseType As DefectsUnitDoseType) As ActionResult(Of DefectsUnitDoseType)

    ''' <summary>
    ''' Elimina un defecto
    ''' </summary>
    ''' <param name="defects">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function DeleteDefectClassificationItem(ByVal defectClassificationItem As DefectClassificationItem, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Actualiza un defecto
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <param name="state">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function UpdateDefectClassificationItem(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of DefectClassificationItem)

    ''' <summary>
    ''' Obtiene un defecto por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    Function GetDefectClassificationItemByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of DefectClassificationItem)

    ''' <summary>
    ''' Obtiene un defecto por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function GetDefectClassificationItemById(id As String, ByVal audit As AuditMessage) As ActionResult(Of DefectClassificationItem)

    ''' <summary>
    ''' Obtiene los tipos de dosis unitarias
    ''' </summary>
    ''' <param name="Id_Defects">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function ListAllDefectsUnitDoseType(ByVal Id_DefectsClassificationItem As Integer, ByVal audit As AuditMessage) As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Validaciones para guardar un defecto
    ''' </summary>
    ''' <param name="productionLineId"></param>
    ''' <returns></returns>
    Function ValidateDefectsUnitDoseType(defectClassificationItemId As Integer, unitDoseTypeId As Integer) As ActionResult
End Interface
