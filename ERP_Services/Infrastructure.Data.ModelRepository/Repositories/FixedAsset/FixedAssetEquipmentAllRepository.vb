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
Public Class FixedAssetItemAllRepository
    Inherits GenericRepository(Of FixedAssetItem)
    Implements IFixedAssetItemAllRepository

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



    Public Function GetEquipment(codeEquipment As String, Optional Tracking As Boolean = False) As FixedAssetItem Implements IFixedAssetItemAllRepository.GetFixedAssetItem
        If codeEquipment Is Nothing OrElse codeEquipment.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As FixedAssetItem In Me._context.FixedAssetItem.Include("FixedAssetItemDetail").Include("FixedAssetItemAccesory") _
                       .Include("FixedAssetItemConsumible").Include("FixedAssetItemPart").Include("FixedAssetItemProtocol").Include("FixedAssetItemTechnicalLog")
                   Where d.Code.Equals(codeEquipment.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim EquipmentCatalog = (From e In _context.FixedAssetItemCatalog.AsNoTracking Where e.Id = res.ItemCatalogId Select e).FirstOrDefault
            res.CodeNameEquipmentCatalog = EquipmentCatalog.Code + " - " + EquipmentCatalog.Description

            If res.CatalogOfPropertyandServicesId IsNot Nothing Then
                res.CodeNameCatalogOfPropertyandServices = (From c In _context.FixedAssetCatalogOfPropertyandServices Where c.Id = res.CatalogOfPropertyandServicesId
                                                            Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()
            End If

            Dim IVA = (From i In _context.GeneralLedgerIVA.AsNoTracking Where i.Id = res.IVAId Select i).FirstOrDefault
            res.CodeNameIVA = IVA.Code + " - " + IVA.Name

            If res.FixedAssetItemDetail IsNot Nothing AndAlso res.FixedAssetItemDetail.Count > 0 Then
                For Each item In res.FixedAssetItemDetail
                    Dim LegalBook = (From l In _context.LegalBook.AsNoTracking Where l.Id = item.LegalBookId Select l).FirstOrDefault
                    item.CodeNameLegalBook = LegalBook.Code + " - " + LegalBook.Name
                Next
            End If

            If res.FixedAssetItemAccesory.Any() Then
                For Each item In res.FixedAssetItemAccesory
                    Dim accesory = (From a In _context.Accessory.AsNoTracking() Where a.Id = item.AccesoryId Select a).FirstOrDefault()
                    item.AccesoryCode = accesory.Code
                    item.AccesoryName = accesory.Name
                Next
            End If
            If res.FixedAssetItemConsumible.Any() Then
                For Each item In res.FixedAssetItemConsumible
                    Dim consumible = (From a In _context.Consumable.AsNoTracking() Where a.Id = item.ConsumibleId Select a).FirstOrDefault()
                    item.ConsumibleCode = consumible.Code
                    item.ConsumibleName = consumible.Name
                Next
            End If
            If res.FixedAssetItemPart.Any() Then
                For Each item In res.FixedAssetItemPart
                    Dim part = (From a In _context.FixedAssetPartsAccesoriesConsumables.AsNoTracking() Where a.Id = item.PartId Select a).FirstOrDefault()
                    item.PartCode = part.Code
                    item.PartName = part.Name
                Next
            End If
            If res.FixedAssetItemProtocol.Any() Then
                For Each item In res.FixedAssetItemProtocol
                    Dim protocol = (From a In _context.MaintenanceProtocol.AsNoTracking() Where a.Id = item.MaintenanceProtocolId Select a).FirstOrDefault()
                    item.ProtocolCode = protocol.Code
                    item.ProtocolName = protocol.Name
                Next
            End If
            If res.FixedAssetItemTechnicalLog.Any() Then
                For Each item In res.FixedAssetItemTechnicalLog
                    Dim technical = (From a In _context.TechnicalLog.AsNoTracking() Where a.Id = item.TechnicalLogId Select a).FirstOrDefault()
                    item.TechnicalLogCode = technical.Code
                    item.TechnicalLogName = technical.Name
                Next
            End If
            res.OriginalValue = (From d As FixedAssetItem In Me._context.FixedAssetItem.AsNoTracking() Where d.Code.Equals(codeEquipment.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetItem()
        End If
    End Function

    Public Function ListAllEquipment() As List(Of FixedAssetItem) Implements IFixedAssetItemAllRepository.ListAllFixedAssetItem
        Dim Busqueda = From e In _context.FixedAssetItem
               Select e

        Return Busqueda.ToList
    End Function

    Public Function SaveEquipment(Equipment As FixedAssetItem) As Boolean Implements IFixedAssetItemAllRepository.SaveFixedAssetItem
        _context.FixedAssetItem.ApplyChanges(Equipment)
        Return True
    End Function

    ''' <summary>
    ''' Metodo que consulta cuantos libros oficiales hay
    ''' para poder validar con la cantidad de libros que
    ''' se agregan en el form de articulos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CountQuantityLegalBook() As Integer Implements IFixedAssetItemAllRepository.CountQuantityLegalBook
        Return (From l In _context.LegalBook.AsNoTracking Where l.Status = True AndAlso l.TypeBook <> 3 Select l).Count
    End Function

End Class
