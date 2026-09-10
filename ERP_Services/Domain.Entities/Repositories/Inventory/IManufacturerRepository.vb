'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IManufacturerRepository
    Inherits IRepository(Of Manufacturer)

    ''' <summary>
    ''' Obtiene un fabricante por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetManufacturer(code As String) As Manufacturer

    ''' <summary>
    ''' Obtiene un fabricante por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetManufacturerById(id As Integer) As Manufacturer

End Interface
