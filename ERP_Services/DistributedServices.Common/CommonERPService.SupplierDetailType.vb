'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 09-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Common
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

Partial Class CommonERPService
    Implements ICommonERPSuppliersDetailType

    Public Function GetSuppliersDetailTypeById(id As Integer, session As SessionValues) As Domain.Entities.SupplierDetailType Implements ICommonERPSuppliersDetailType.GetSuppliersDetailTypeById
        Using suppliersDetailTypeAdminService As ISuppliersDetailTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISuppliersDetailTypeAdminService)()
            Return suppliersDetailTypeAdminService.GetSuppliersDetailTypeById(id, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetSuppliersDetailTypeByIdSupplier(id As Integer, session As SessionValues) As List(Of Domain.Entities.SupplierDetailType) Implements ICommonERPSuppliersDetailType.GetSuppliersDetailTypeByIdSupplier
        Using suppliersDetailTypeAdminService As ISuppliersDetailTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISuppliersDetailTypeAdminService)()
            Return suppliersDetailTypeAdminService.GetSuppliersDetailTypeByIdSupplier(id, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Lista de proveedores
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllSupplier(session As SessionValues) As List(Of Domain.Entities.Supplier) Implements ICommonERPSuppliersDetailType.ListAllSupplier
        Using CommonAdminService As ISupplierAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISupplierAdminService)()
            Return CommonAdminService.ListAllSupplier()
        End Using
    End Function
End Class
