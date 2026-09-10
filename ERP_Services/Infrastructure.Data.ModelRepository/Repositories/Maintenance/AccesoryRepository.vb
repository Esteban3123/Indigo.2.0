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

Public Class AccesoryRepository
    Inherits GenericRepository(Of Accessory)
    Implements IAccesoryRepository

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


    Public Function GetAccessory(codeAccessory As String, Optional Tracking As Boolean = False) As Accessory Implements IAccesoryRepository.GetAccessory
        Dim Busqueda As Accessory = Nothing
        If Tracking = False Then
            Busqueda = (From e In _context.Accessory.Include("AccesoryDetail.FixedAssetItemType")
                        Where e.Code = codeAccessory
                        Select e).FirstOrDefault()
        Else
            Busqueda = (From e In _context.Accessory.Include("AccesoryDetail.FixedAssetItemType").AsNoTracking()
                        Where e.Code = codeAccessory
                        Select e).FirstOrDefault()
        End If
        If Busqueda Is Nothing Then
            Return New Accessory()
        Else
            Busqueda.StartTracking()
            If Busqueda.AccesoryDetail.Any() Then
                For Each i In Busqueda.AccesoryDetail
                    Dim equipmentType As FixedAssetItemType = (From e In _context.FixedAssetItemType.AsNoTracking() Where e.Id = i.IdEquipmentType Select e).FirstOrDefault()
                    i.EquipmentTypeCode = equipmentType.Code
                    i.EquipmentTypeName = equipmentType.Name
                Next
            End If
            Return Busqueda
        End If
    End Function

    Public Function ListAllAccessory() As List(Of Accessory) Implements IAccesoryRepository.ListAllAccessory
        Dim Busqueda = From e In _context.Accessory
                       Select e
        Return Busqueda.ToList
    End Function

    Public Function SaveAccessory(Accessory As Accessory) As Boolean Implements IAccesoryRepository.SaveAccessory
        _context.Accessory.ApplyChanges(Accessory)
        Return True
    End Function

    Public Function ListEquipmentType() As List(Of FixedAssetEquipmentType) Implements IAccesoryRepository.ListEquipmentType
        Dim Busqueda = From e In _context.FixedAssetEquipmentType
                       Where e.Status = True
                       Select e
        Return Busqueda.ToList
    End Function
End Class
