'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base

#End Region


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la entidad sucursal
''' </summary>
''' <remarks></remarks>

Public Class FixedAssetItemTypeRepository
    Inherits GenericRepository(Of FixedAssetItemType)
    Implements IFixedAssetItemTypeRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub


    Public Function GetEquipmentType(codeEquipmentType As String) As FixedAssetItemType Implements IFixedAssetItemTypeRepository.GetEquipmentType
        Dim Busqueda = (From e In _context.FixedAssetItemType.Include("FixedAssetItemTypePartsAccesories.FixedAssetPartsAccesoriesConsumables")
                        Where e.Code = codeEquipmentType
                        Select e).FirstOrDefault

        If Busqueda IsNot Nothing Then

            If Busqueda.FixedAssetItemTypePartsAccesories IsNot Nothing AndAlso Busqueda.FixedAssetItemTypePartsAccesories.Count > 0 Then
                For Each item In Busqueda.FixedAssetItemTypePartsAccesories
                    Dim AccesoriesConsumables = item.FixedAssetPartsAccesoriesConsumables
                    item.FixedAssetPartsAccesoriesConsumablesCode = AccesoriesConsumables.Code
                    item.FixedAssetPartsAccesoriesConsumablesName = AccesoriesConsumables.Name
                Next
            End If

            Return Busqueda
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Obtiene los tipos de equipo padre e hijos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllEquipmentType() As List(Of FixedAssetItemType) Implements IFixedAssetItemTypeRepository.ListAllEquipmentType
        Dim res = (From d As FixedAssetItemType In Me._context.FixedAssetItemType
                   Where d.Status = True
                   Select d).ToList()
        Return res


    End Function

    Public Function SaveEquipmentType(EquipmentType As FixedAssetItemType) As Boolean Implements IFixedAssetItemTypeRepository.SaveEquipmentType
        _context.FixedAssetItemType.ApplyChanges(EquipmentType)
        Return True
    End Function


    Public Function ListEquipmentTypeInventoryType(IdInventoryType As Integer) As List(Of FixedAssetItemType) Implements IFixedAssetItemTypeRepository.ListEquipmentTypeInventoryType
        Dim Busqueda = From e In _context.FixedAssetItemType
                       Where e.InventoryTypeId = IdInventoryType
                       Select e

        Return Busqueda.ToList
    End Function

    Public Function GetEquipmentTypeById(IdEquipmentType As Integer) As FixedAssetItemType Implements IFixedAssetItemTypeRepository.GetEquipmentTypeById
        Dim Busqueda = From e In _context.FixedAssetItemType
                       Where e.Id = IdEquipmentType
                       Select e

        If Busqueda.Count = 0 Then
            Return New FixedAssetItemType
        Else
            Return Busqueda.Single
        End If

    End Function

End Class
