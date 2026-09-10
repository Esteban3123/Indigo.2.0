'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Public Class PaymentsService

    ''' <summary>
    ''' Elimina un tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSupplierType(SupplierType As Domain.Entities.SupplierType, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPaymentsSupplierType.DeleteSupplierType
        Using service As ISupplierTypeAdminService = Container.Current.Resolve(Of ISupplierTypeAdminService)()
            Return service.DeleteSupplierType(SupplierType, audit)
        End Using
        'Return Me._supplierTypeAdminService.DeleteSupplierType(SupplierType, audit)
    End Function

    ''' <summary>
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierType(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SupplierType) Implements IPaymentsSupplierType.GetSupplierType
        Using service As ISupplierTypeAdminService = Container.Current.Resolve(Of ISupplierTypeAdminService)()
            Return service.GetSupplierType(code, audit)
        End Using
        'Return Me._supplierTypeAdminService.GetSupplierType(code, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSupplierType(SupplierType As Domain.Entities.SupplierType, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SupplierType) Implements IPaymentsSupplierType.SaveSupplierType
        Using service As ISupplierTypeAdminService = Container.Current.Resolve(Of ISupplierTypeAdminService)()
            Return service.SaveSupplierType(SupplierType, audit, idSequense)
        End Using
        'Return Me._supplierTypeAdminService.SaveSupplierType(SupplierType, audit, idSequense)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateSupplierType(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SupplierType) Implements IPaymentsSupplierType.ChangeStateSupplierType
        Using service As ISupplierTypeAdminService = Container.Current.Resolve(Of ISupplierTypeAdminService)()
            Return service.ChangeState(code, state, audit)
        End Using
        'Return Me._supplierTypeAdminService.ChangeState(code, state, audit)
    End Function

    ''' <summary>
    ''' Consulta el concepto de nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierTypeById(id As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SupplierType) Implements IPaymentsSupplierType.GetSupplierTypeById
        Using service As ISupplierTypeAdminService = Container.Current.Resolve(Of ISupplierTypeAdminService)()
            Return service.GetSupplierTypeById(id, audit)
        End Using
        'Return Me._supplierTypeAdminService.GetSupplierTypeById(id, audit)
    End Function

    ''' <summary>
    ''' Obtiene los tipos de proveedor hijos que tiene asociado el proveedor,
    ''' y sus respectivos padres
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierTypeBySupplierId(supplierId As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.SupplierType)) Implements IPaymentsSupplierType.GetSupplierTypeBySupplierId
        Using service As ISupplierTypeAdminService = Container.Current.Resolve(Of ISupplierTypeAdminService)()
            Return service.GetSupplierTypeBySupplierId(supplierId)
        End Using
        'Return Me._supplierTypeAdminService.GetSupplierTypeBySupplierId(supplierId)
    End Function

End Class
