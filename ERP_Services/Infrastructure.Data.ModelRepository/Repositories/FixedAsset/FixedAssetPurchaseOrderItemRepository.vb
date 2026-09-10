'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Oscar stiven astudillo reyes
' Created          : 2024-11-26
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Import"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
#End Region

Public Class FixedAssetPurchaseOrderItemRepository
    Inherits GenericRepository(Of FixedAssetPurchaseOrderItem)
    Implements IFixedAssetPurchaseOrderItemRepository

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


#Region "Methods"
    ''' <summary>
    ''' Busca registro por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetFixedAssetPurchaseOrderItemById(id As Integer) As FixedAssetPurchaseOrderItem Implements IFixedAssetPurchaseOrderItemRepository.GetFixedAssetPurchaseOrderItemById
        If id.ToString.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Id")
        End If
        Return (From p In _context.FixedAssetPurchaseOrderItem Where p.Id = id Select p).FirstOrDefault
    End Function
#End Region


End Class
