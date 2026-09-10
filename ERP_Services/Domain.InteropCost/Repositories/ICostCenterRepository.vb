'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 15-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.InteropCost.Entities

Public Interface ICostCenterRepository
    Inherits IRepository(Of CTNCENCOS)

    ''' <summary>
    ''' Obtiene un centro de costo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCostCenter(ByVal code As String) As CTNCENCOS

    ''' <summary>
    ''' Obtiene un centro de costo por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetCostCenterById(id As Integer) As CTNCENCOS

End Interface