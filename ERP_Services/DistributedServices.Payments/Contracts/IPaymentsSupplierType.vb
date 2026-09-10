'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsSupplierType

    ''' <summary>
    ''' Guarda o Actualiza un tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveSupplierType(SupplierType As Domain.Entities.SupplierType, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SupplierType)

    ''' <summary>
    ''' Elimina un tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteSupplierType(SupplierType As Domain.Entities.SupplierType, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSupplierType(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SupplierType)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateSupplierType(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SupplierType)

    ''' <summary>
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetSupplierTypeById(id As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SupplierType)

    ''' <summary>
    ''' Obtiene los tipos de proveedor a las cuales tiene tenga asociado el mismo,
    ''' tambien carga los tipos de proveedor padres 
    ''' </summary>
    ''' <param name="supplierId">Codigo del usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetSupplierTypeBySupplierId(supplierId As Integer) As ActionResult(Of List(Of SupplierType))

End Interface
