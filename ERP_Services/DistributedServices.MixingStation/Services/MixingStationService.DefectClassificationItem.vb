'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 22-09-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
Imports DistributedServices.MixingStation
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceDefectClassificationItem

    ''' <summary>
    ''' Lista los defecto
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListAllDefectClassificationItem(audit As AuditMessage) As List(Of DefectClassificationItem) Implements IMixingStationServiceDefectClassificationItem.ListAllDefectClassificationItem
        Using service As IDefectClassificationItemLineService = Container.Current.Resolve(Of IDefectClassificationItemLineService)()
            Return service.ListAllDefectClassificationItem(audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un defecto
    ''' </summary>
    ''' <param name="Defects"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveDefectClassificationItem(defectClassificationItem As DefectClassificationItem, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of DefectClassificationItem) Implements IMixingStationServiceDefectClassificationItem.SaveDefectClassificationItem
        Using service As IDefectClassificationItemLineService = Container.Current.Resolve(Of IDefectClassificationItemLineService)()
            Return service.SaveDefectClassificationItem(defectClassificationItem, audit, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="productionLineUnitDoseType"></param>
    Public Function SaveDefectsUnitDoseType(defectsUnitDoseType As DefectsUnitDoseType) As ActionResult(Of DefectsUnitDoseType) Implements IMixingStationServiceDefectClassificationItem.SaveDefectsUnitDoseType
        Using service As IDefectClassificationItemLineService = Container.Current.Resolve(Of IDefectClassificationItemLineService)()
            Return service.SaveUnitDoseType(defectsUnitDoseType)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un defecto
    ''' </summary>
    ''' <param name="defectClassificationItem"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteDefectClassificationItem(defectClassificationItem As DefectClassificationItem, audit As AuditMessage) As ActionResult Implements IMixingStationServiceDefectClassificationItem.DeleteDefectClassificationItem
        Using service As IDefectClassificationItemLineService = Container.Current.Resolve(Of IDefectClassificationItemLineService)()
            Return service.DeleteDefectClassificationItem(defectClassificationItem, audit)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza un defecto
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function UpdateDefectClassificationItem(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DefectClassificationItem) Implements IMixingStationServiceDefectClassificationItem.UpdateDefectClassificationItem
        Using service As IDefectClassificationItemLineService = Container.Current.Resolve(Of IDefectClassificationItemLineService)()
            Return service.UpdateDefectClassificationItem(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un defecto por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetDefectClassificationItemByCode(code As String, audit As AuditMessage) As ActionResult(Of DefectClassificationItem) Implements IMixingStationServiceDefectClassificationItem.GetDefectClassificationItemByCode
        Using service As IDefectClassificationItemLineService = Container.Current.Resolve(Of IDefectClassificationItemLineService)()
            Return service.GetDefectClassificationItemByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un defecto por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetDefectClassificationItemById(id As String, audit As AuditMessage) As ActionResult(Of DefectClassificationItem) Implements IMixingStationServiceDefectClassificationItem.GetDefectClassificationItemById
        Using service As IDefectClassificationItemLineService = Container.Current.Resolve(Of IDefectClassificationItemLineService)()
            Return service.GetDefectClassificationItemById(id, audit)
        End Using
    End Function

    Public Function ListAllDefectsUnitDoseType(Id_DefectsClassificationItem As Integer, audit As AuditMessage) As List(Of Tuple(Of Integer, String)) Implements IMixingStationServiceDefectClassificationItem.ListAllDefectsUnitDoseType
        Using service As IDefectClassificationItemLineService = Container.Current.Resolve(Of IDefectClassificationItemLineService)()
            Return service.ListAllDefectsUnitDoseType(Id_DefectsClassificationItem, audit)
        End Using
    End Function

    Public Function ValidateDefectsUnitDoseType(defectClassificationItemId As Integer, unitDoseTypeId As Integer) As ActionResult Implements IMixingStationServiceDefectClassificationItem.ValidateDefectsUnitDoseType
        Using service As IDefectClassificationItemLineService = Container.Current.Resolve(Of IDefectClassificationItemLineService)()
            Return service.ValidateDefectsUnitDoseType(defectClassificationItemId, unitDoseTypeId)
        End Using
    End Function
End Class
