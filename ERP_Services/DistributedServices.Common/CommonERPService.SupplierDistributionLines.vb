'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 09-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Common
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

Partial Class CommonERPService
    Implements ICommonERPSuppliersDistributionLines


    ''' <summary>
    ''' Elimina las lineas de distribucion del proveedor
    ''' </summary>
    ''' <param name="supplierDistributionLine"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSuppliersDistributionLines(supplierDistributionLine As Domain.Entities.SuppliersDistributionLines, session As SessionValues) As ActionResult Implements ICommonERPSuppliersDistributionLines.DeleteSuppliersDistributionLines
        Using suppliersDistributionLinesAdminService As ISuppliersDistributionLinesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISuppliersDistributionLinesAdminService)()
            Return suppliersDistributionLinesAdminService.DeleteSuppliersDistributionLines(supplierDistributionLine, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el listado de lineas de distribucion dle proveedor
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuppliersDistributionLinesByIdSupplier(id As Integer, session As SessionValues) As List(Of Domain.Entities.SuppliersDistributionLines) Implements ICommonERPSuppliersDistributionLines.GetSuppliersDistributionLinesByIdSupplier
        Using suppliersDistributionLinesAdminService As ISuppliersDistributionLinesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISuppliersDistributionLinesAdminService)()
            Return suppliersDistributionLinesAdminService.GetSuppliersDistributionLinesByIdSupplier(id, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza las lineas de distribucion del proveedor
    ''' </summary>
    ''' <param name="supplierDistributionLine"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSuppliersDistributionLines(supplierDistributionLine As Domain.Entities.SuppliersDistributionLines, session As SessionValues) As ActionResult(Of Domain.Entities.SuppliersDistributionLines) Implements ICommonERPSuppliersDistributionLines.SaveSuppliersDistributionLines
        Using suppliersDistributionLinesAdminService As ISuppliersDistributionLinesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISuppliersDistributionLinesAdminService)()
            Return suppliersDistributionLinesAdminService.SaveSuppliersDistributionLines(supplierDistributionLine, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuppliersDistributionLinesById(id As Integer, session As SessionValues) As Domain.Entities.SuppliersDistributionLines Implements ICommonERPSuppliersDistributionLines.GetSuppliersDistributionLinesById
        Using suppliersDistributionLinesAdminService As ISuppliersDistributionLinesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISuppliersDistributionLinesAdminService)()
            Return suppliersDistributionLinesAdminService.GetSuppliersDistributionLinesById(id, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el porcentaje del ICA que maneja la linea de distribucion
    ''' </summary>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetICARetentionConceptBySupplierDistributionLine(idSupplierDistributionLine As Integer, OperatingUnitId As Integer, session As SessionValues) As Domain.Entities.RetentionConcepts Implements ICommonERPSuppliersDistributionLines.GetICARetentionConceptBySupplierDistributionLine
        Using suppliersDistributionLinesAdminService As ISuppliersDistributionLinesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISuppliersDistributionLinesAdminService)()
            Return suppliersDistributionLinesAdminService.GetICARetentionConceptBySupplierDistributionLine(idSupplierDistributionLine, OperatingUnitId)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene de un proveedor, la lista de lineas de distribución por Concepto Acreencia
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="accusationConcept"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetDistributionLinesByIdSupplierAndAccusationConcept(idSupplier As Integer, accusationConcept As Integer, session As SessionValues) As List(Of Domain.Entities.SuppliersDistributionLines) Implements ICommonERPSuppliersDistributionLines.GetDistributionLinesByIdSupplierAndAccusationConcept
        Using suppliersDistributionLinesAdminService As ISuppliersDistributionLinesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISuppliersDistributionLinesAdminService)()
            Return suppliersDistributionLinesAdminService.GetDistributionLinesByIdSupplierAndAccusationConcept(idSupplier, accusationConcept, session.AuditMessageWcf)
        End Using
    End Function
End Class
