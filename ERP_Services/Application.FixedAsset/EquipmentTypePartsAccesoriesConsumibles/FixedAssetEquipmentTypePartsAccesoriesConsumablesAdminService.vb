'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 27-08-2015
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

Public Class FixedAssetItemTypePartsAccesoriesConsumablesAdminService

    Implements IFixedAssetItemTypePartsAccesoriesConsumablesAdminService

    'Repositorio de tipo de ubicacion
    Private _EquipmentTypePartsAccesoriesConsumablesRespository As IFixedAssetItemTypePartsAccesoriesConsumablesRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="EquipmentTypePartsAccesoriesConsumablesRespository">Repositorio de EquipmentTypePartsAccesoriesConsumablesRespository</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal EquipmentTypePartsAccesoriesConsumablesRespository As IFixedAssetItemTypePartsAccesoriesConsumablesRepository)
        If (EquipmentTypePartsAccesoriesConsumablesRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de EquipmentTypePartsAccesoriesConsumablesRespository vacío")
        End If
        _EquipmentTypePartsAccesoriesConsumablesRespository = EquipmentTypePartsAccesoriesConsumablesRespository
    End Sub

    Public Function DeleteEquipmentTypePartsAccesoriesConsumables(ListEquipmentTypePartsAccesoriesConsumables As List(Of FixedAssetItemTypePartsAccesories), audit As AuditMessage) As Boolean Implements IFixedAssetItemTypePartsAccesoriesConsumablesAdminService.DeleteEquipmentTypePartsAccesoriesConsumables
        If ListEquipmentTypePartsAccesoriesConsumables Is Nothing Then
            Throw New ArgumentNullException("ListEquipmentTypeTechnicalLog vacio")
        End If
        Dim unitWork As IUnitWork = _EquipmentTypePartsAccesoriesConsumablesRespository.UnitWork
        Try

            For Each ObjEquipmentTypePartsAccesoriesConsumables As Domain.Entities.FixedAssetItemTypePartsAccesories In ListEquipmentTypePartsAccesoriesConsumables
                ObjEquipmentTypePartsAccesoriesConsumables.MarkAsDeleted()
                _EquipmentTypePartsAccesoriesConsumablesRespository.DeleteEntity(ObjEquipmentTypePartsAccesoriesConsumables)
            Next
            unitWork.Commit()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return False
        End Try
    End Function

    Public Function SaveEquipmentTypePartsAccesoriesConsumables(ListEquipmentTypePartsAccesoriesConsumables As List(Of FixedAssetItemTypePartsAccesories), audit As AuditMessage) As Boolean Implements IFixedAssetItemTypePartsAccesoriesConsumablesAdminService.SaveEquipmentTypePartsAccesoriesConsumables
        Dim unitWork As IUnitWork = _EquipmentTypePartsAccesoriesConsumablesRespository.UnitWork
        Try
            For Each ObjEquipmentTypePartsAccesoriesConsumables As Domain.Entities.FixedAssetItemTypePartsAccesories In ListEquipmentTypePartsAccesoriesConsumables
                _EquipmentTypePartsAccesoriesConsumablesRespository.SaveEntity(ObjEquipmentTypePartsAccesoriesConsumables)
            Next
            unitWork.Commit()
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
            _EquipmentTypePartsAccesoriesConsumablesRespository = Nothing
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
