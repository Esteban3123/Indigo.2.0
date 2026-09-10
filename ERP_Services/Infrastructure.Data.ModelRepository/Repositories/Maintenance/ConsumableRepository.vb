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
''' clase para hacer todas las operaciones de persistencia para la entidad consumibles
''' </summary>
''' <remarks></remarks>

Public Class ConsumableRepository
    Inherits GenericRepository(Of Consumable)
    Implements IConsumableRepository

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

    Public Function GetConsumable(codeConsumable As String, Optional Tracking As Boolean = False) As Consumable Implements IConsumableRepository.GetConsumable
        Dim query As Consumable = Nothing
        If Tracking = False Then
            query = (From e In _context.Consumable.Include("ConsumableDetail.FixedAssetItemType")
                     Where e.Code = codeConsumable
                     Select e).FirstOrDefault()
        Else
            query = (From e In _context.Consumable.Include("ConsumableDetail.FixedAssetItemType").AsNoTracking
                     Where e.Code = codeConsumable
                     Select e).FirstOrDefault()
        End If
        If query Is Nothing Then
            Return New Consumable
        Else
            query.StartTracking()
            If query.ConsumableDetail.Any() Then
                For Each item In query.ConsumableDetail
                    Dim equipmentType As FixedAssetItemType = (From d In _context.FixedAssetItemType.AsNoTracking() Where d.Id = item.IdEquipmentType Select d).FirstOrDefault()
                    item.EquipmentTypeCode = equipmentType.Code
                    item.EquipmentTypeName = equipmentType.Name
                Next
            End If
            Return query
        End If
    End Function

    Public Function ListAllConsumable() As List(Of Consumable) Implements IConsumableRepository.ListAllConsumable

        Dim Busqueda = From e In _context.Consumable
                      Select e

        Return Busqueda.ToList
    End Function

    Public Function SaveConsumable(Consumable As Consumable) As Boolean Implements IConsumableRepository.SaveConsumable
        _context.Consumable.ApplyChanges(Consumable)
        Return True
    End Function
End Class
