'***********************************************************************
' Assembly         : Domain.Common
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Public Interface ISuppliersDetailTypeRepository
    Inherits IRepository(Of SupplierDetailType)

    ''' <summary>
    ''' Obtiene los tipos de proveedores del proveedor
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuppliersDetailTypeByIdSupplier(id As Integer, Optional tracking As Boolean = True) As List(Of SupplierDetailType)

    ''' <summary>
    ''' Obtiene un tipo de proveedor del proveedor
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuppliersDetailTypeById(id As Integer, Optional tracking As Boolean = True) As SupplierDetailType

End Interface
