'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 15-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities

#End Region

Public Class EquipmentTypeTechnicalLogAdminService

    Implements IEquipmentTypeTechnicalLogAdminService

    'Repositorio de tipo de ubicacion
    Private _EquipmentTypeTechnicalLogRespository As IEquipmentTypeTechnicalLogRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="EquipmentTypeTechnicalLogRespository">Repositorio de Marcas</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal EquipmentTypeTechnicalLogRespository As IEquipmentTypeTechnicalLogRepository)
        If (EquipmentTypeTechnicalLogRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de EquipmentTypeTechnicalLogRespository vacío")
        End If
        _EquipmentTypeTechnicalLogRespository = EquipmentTypeTechnicalLogRespository
    End Sub

    ''' <summary>
    ''' Almaceno el listado de Registros Técnicos x Tipos de Equipos
    ''' </summary>
    ''' <param name="ListEquipmentTypeTechnicalLog"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveEquipmentTypeTechnicalLog(ListEquipmentTypeTechnicalLog As List(Of Domain.Maintenance.Entities.EquipmentTypeTechnicalLog), audit As AuditMessage) As Boolean Implements IEquipmentTypeTechnicalLogAdminService.SaveEquipmentTypeTechnicalLog

        Dim unitWork As IUnitWork = _EquipmentTypeTechnicalLogRespository.UnitWork
        Try
            For Each ObjEquipmentTypeTechnicalLog As Domain.Maintenance.Entities.EquipmentTypeTechnicalLog In ListEquipmentTypeTechnicalLog
                _EquipmentTypeTechnicalLogRespository.SaveEntity(ObjEquipmentTypeTechnicalLog)
            Next
            unitWork.Commit()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return False
        End Try

    End Function

    Public Function DeleteEquipmentTypeTechnicalLog(ListEquipmentTypeTechnicalLog As List(Of Domain.Maintenance.Entities.EquipmentTypeTechnicalLog), audit As AuditMessage) As Boolean Implements IEquipmentTypeTechnicalLogAdminService.DeleteEquipmentTypeTechnicalLog
        If ListEquipmentTypeTechnicalLog Is Nothing Then
            Throw New ArgumentNullException("ListEquipmentTypeTechnicalLog vacio")
        End If
        Dim unitWork As IUnitWork = _EquipmentTypeTechnicalLogRespository.UnitWork
        Try

            For Each ObjEquipmentTypeTechnicalLog As Domain.Maintenance.Entities.EquipmentTypeTechnicalLog In ListEquipmentTypeTechnicalLog
                ObjEquipmentTypeTechnicalLog.MarkAsDeleted()
                _EquipmentTypeTechnicalLogRespository.DeleteEntity(ObjEquipmentTypeTechnicalLog)
            Next
            unitWork.Commit()
            'IndigoAuditSimpleEntity(Of Trademark).Execute(Trademark, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, audit.Company)
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return False
        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _EquipmentTypeTechnicalLogRespository = Nothing
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
