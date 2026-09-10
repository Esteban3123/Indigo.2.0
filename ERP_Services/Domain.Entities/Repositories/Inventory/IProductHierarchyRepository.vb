'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IProductHierarchyRepository
    Inherits IRepository(Of ProductHierarchy)

    ''' <summary>
    ''' Lista las Jerarquías relacionadas al producto final
    ''' </summary>
    Function ListProductHierarchyByHierarchyProductFinalId(ByVal HierarchyProductFinalId As Integer) As List(Of ProductHierarchy)

End Interface