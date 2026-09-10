'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 25-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IEquipmentTypeTechnicalLogAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="ListEquipmentTypeTechnicalLog">Objeto ListEquipmentTypeTechnicalLog</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveEquipmentTypeTechnicalLog(ListEquipmentTypeTechnicalLog As List(Of EquipmentTypeTechnicalLog), audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina una Lista de Equipos x Registro Técnico
    ''' </summary>
    ''' <param name="ListEquipmentTypeTechnicalLog"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteEquipmentTypeTechnicalLog(ListEquipmentTypeTechnicalLog As List(Of Domain.Maintenance.Entities.EquipmentTypeTechnicalLog), audit As AuditMessage) As Boolean

End Interface
