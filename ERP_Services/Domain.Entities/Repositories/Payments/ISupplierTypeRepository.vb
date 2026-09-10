'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface ISupplierTypeRepository
    Inherits IRepository(Of SupplierType)

    ''' <summary>
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplierType(code As String, Optional tracking As Boolean = True) As SupplierType

    ''' <summary>
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplierTypeById(id As String, Optional tracking As Boolean = True) As SupplierType

    ''' <summary>
    ''' Obtiene los tipos de proveedor a las cuales tiene tenga asociado el mismo,
    ''' tambien carga los tipos de proveedor padres 
    ''' </summary>
    ''' <param name="supplierId">Codigo del usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplierTypeBySupplierId(supplierId As Integer) As List(Of SupplierType)

    ''' <summary>
    ''' Obtiene todos los hijos de un tipo de proveedor
    ''' </summary>
    ''' <param name="supplierTypeId">The supplier type identifier.</param>
    ''' <returns></returns>
    Function GetAllSupplierTypeIdListByParentId(supplierTypeId As Integer) As List(Of Integer)

End Interface
