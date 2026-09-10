'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ISupplierTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un tipo de proveedor
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveSupplierType(ByVal SupplierType As SupplierType, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of SupplierType)

    ''' <summary>
    ''' Elimina un tipo de proveedor
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteSupplierType(ByVal SupplierType As SupplierType, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetSupplierType(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of SupplierType)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of SupplierType)

    ''' <summary>
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplierTypeById(id As String, ByVal audit As AuditMessage) As ActionResult(Of SupplierType)

    ''' <summary>
    ''' Obtiene los tipos de proveedor a las cuales tiene tenga asociado el mismo,
    ''' tambien carga los tipos de proveedor padres 
    ''' </summary>
    ''' <param name="supplierId">Codigo del usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplierTypeBySupplierId(supplierId As Integer) As ActionResult(Of List(Of SupplierType))

End Interface
