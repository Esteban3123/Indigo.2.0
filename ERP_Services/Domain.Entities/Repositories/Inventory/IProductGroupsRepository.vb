'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IProductGroupsRepository
    Inherits IRepository(Of ProductGroup)

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProductGroup(code As String) As ProductGroup

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProductGroupById(id As Integer) As ProductGroup

    ''' <summary>
    ''' Gets the product group by product identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetProductGroupByProductId(id As Integer) As ProductGroup

    ''' <summary>
    ''' Obtiene un grupo con la informacion contable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProductGroupByIdForAccounting(id As Integer, Optional IsDeclarant As Boolean = True) As ProductGroup

End Interface
