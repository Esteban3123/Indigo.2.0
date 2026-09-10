'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/02/2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region
Public Interface IUPRUnitsRepository
    Inherits IRepository(Of UPRUnits)
    ''' <summary>
    ''' Obtiene la Unidad UPR por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Function getUPRUnits(code As String, Optional tracking As Boolean = True) As UPRUnits
    ''' <summary>
    ''' Obtiene la Unidad UPR por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function getUPRUnitsById(id As Integer) As UPRUnits

End Interface
