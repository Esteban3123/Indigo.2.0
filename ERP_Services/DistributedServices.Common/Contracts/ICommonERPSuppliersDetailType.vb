'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface ICommonERPSuppliersDetailType

    ''' <summary>
    ''' Obtiene los tipos de proveedor 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetSuppliersDetailTypeByIdSupplier(id As Integer, session As SessionValues) As List(Of SupplierDetailType)

    ''' <summary>
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetSuppliersDetailTypeById(id As Integer, session As SessionValues) As SupplierDetailType

    ''' <summary>
    ''' Lista de proveedores
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllSupplier(session As SessionValues) As List(Of Supplier)

End Interface
