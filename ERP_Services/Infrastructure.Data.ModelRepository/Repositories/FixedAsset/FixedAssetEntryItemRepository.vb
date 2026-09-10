Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class FixedAssetEntryItemRepository
    Inherits GenericRepository(Of FixedAssetEntryItem)
    Implements IFixedAssetEntryItemRepository

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtiene un ingreso de artículo por su ID
    ''' </summary>
    ''' <param name="itemId">ID del artículo</param>
    ''' <returns>Lista de ingresos de artículo</returns>
    Public Function GetFixedAssetEntryItemByItemId(itemId As Integer) As List(Of FixedAssetEntryItem) Implements IFixedAssetEntryItemRepository.GetFixedAssetEntryItemByItemId
        Dim Res = (From fe As FixedAssetEntryItem In Me._context.FixedAssetEntryItem.Include("FixedAssetEntry") Where fe.ItemId.Equals(itemId) Select fe
                           ).ToList()
        If Res IsNot Nothing Then
            Return Res
        Else
            Return New List(Of FixedAssetEntryItem)
        End If
    End Function
End Class
