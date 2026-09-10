'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ProductHierarchyRepository
    Inherits GenericRepository(Of ProductHierarchy)
    Implements IProductHierarchyRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Lista las Jerarquías relacionadas al producto final
    ''' </summary>
    ''' <param name="HierarchyProductFinalId"></param>
    ''' <returns></returns>
    Public Function ListProductHierarchyByHierarchyProductFinalId(HierarchyProductFinalId As Integer) As List(Of ProductHierarchy) Implements IProductHierarchyRepository.ListProductHierarchyByHierarchyProductFinalId
        If HierarchyProductFinalId = 0 Then
            Throw New ArgumentNullException("HierarchyProductFinalId")
        End If
        Return (From h In _context.ProductHierarchy Where h.HierarchyProductFinalId = HierarchyProductFinalId Select h).ToList()
    End Function
#End Region

End Class