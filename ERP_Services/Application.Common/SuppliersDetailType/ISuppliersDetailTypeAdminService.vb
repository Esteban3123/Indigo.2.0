'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ISuppliersDetailTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene los tipoes de proveedores del proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuppliersDetailTypeByIdSupplier(id As Integer, ByVal audit As AuditMessage) As List(Of SupplierDetailType)

    ''' <summary>
    ''' Obtiene un tipo de proveedor que tiene el proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuppliersDetailTypeById(id As Integer, ByVal audit As AuditMessage) As SupplierDetailType

End Interface
