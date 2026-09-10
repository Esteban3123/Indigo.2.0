'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IAttributeProductTypeRepository
    Inherits IRepository(Of AttributeProductType)

    ''' <summary>
    ''' Obtiene un atributo para tipo de producto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAttributeProductType(code As String) As AttributeProductType

    ''' <summary>
    ''' Obtiene un atributo para tipo de producto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAttributeProductTypeById(id As Integer) As AttributeProductType

End Interface
