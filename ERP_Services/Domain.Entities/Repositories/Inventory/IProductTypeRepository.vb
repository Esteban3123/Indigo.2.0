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


Public Interface IProductTypeRepository
    Inherits IRepository(Of ProductType)

    ''' <summary>
    ''' Obtiene un tipo de producto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProductType(code As String) As ProductType

    ''' <summary>
    ''' Obtiene un tipo de producto por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProductTypeById(id As Integer) As ProductType

End Interface
