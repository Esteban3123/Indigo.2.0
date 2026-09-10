'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance
Imports Domain.Base.Entities
Imports Domain.Base

#End Region


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la entidad sucursal
''' </summary>
''' <remarks></remarks>

Public Class EquipmentTypeRepository
    Inherits GenericRepository(Of EquipmentType)
    Implements IEquipmentTypeRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IMaintenanceModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IMaintenanceModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub


    Public Function GetEquipmentType(codeEquipmentType As String) As EquipmentType Implements IEquipmentTypeRepository.GetEquipmentType
        Dim Busqueda = From e In _context.EquipmentType.Include("EquipmentTypeFeatureDetail").Include("EquipmentTypeMeasurementUnitDetail").Include("EquipmentTypeTechnicalLog").Include("EquipmentTypeTechnicalLog.TechnicalLog").Include("EquipmentTypePartsAccesoriesConsumibles").Include("EquipmentTypePartsAccesoriesConsumibles.PartsAccesoriesConsumables")
                       Where e.Code = codeEquipmentType
                       Select e

        If Busqueda.Count = 0 Then
            Return New EquipmentType
        Else
            Return Busqueda.Single
        End If

    End Function

    Public Function ListAllEquipmentType() As List(Of EquipmentType) Implements IEquipmentTypeRepository.ListAllEquipmentType
        Dim Busqueda = From e In _context.EquipmentType.Include("EquipmentTypeTechnicalLog")
                       Select e


        Return Busqueda.ToList
    End Function

    Public Function SaveEquipmentType(EquipmentType As EquipmentType) As Boolean Implements IEquipmentTypeRepository.SaveEquipmentType
        _context.EquipmentType.ApplyChanges(EquipmentType)
        Return True
    End Function


    Public Function ListEquipmentTypeInventoryType(IdInventoryType As Integer) As List(Of EquipmentType) Implements IEquipmentTypeRepository.ListEquipmentTypeInventoryType
        Dim Busqueda = From e In _context.EquipmentType
                       Where e.InventoryType = IdInventoryType
                       Select e

        Return Busqueda.ToList
    End Function

    Public Function GetEquipmentTypeById(IdEquipmentType As Integer) As EquipmentType Implements IEquipmentTypeRepository.GetEquipmentTypeById
        Dim Busqueda = From e In _context.EquipmentType.Include("EquipmentTypeFeatureDetail").Include("EquipmentTypeMeasurementUnitDetail").Include("EquipmentTypeTechnicalLog").Include("EquipmentTypeTechnicalLog.TechnicalLog").Include("EquipmentTypePartsAccesoriesConsumibles").Include("EquipmentTypePartsAccesoriesConsumibles.PartsAccesoriesConsumables")
                       Where e.Id = IdEquipmentType
                       Select e

        If Busqueda.Count = 0 Then
            Return New EquipmentType
        Else
            Return Busqueda.Single
        End If

    End Function

End Class
