'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Common
Imports Microsoft.Practices.Unity

Partial Public Class PaymentsService

    ''' <summary>
    ''' Obtiene el valor del porcentaje de retencion de iva que maneja el proveedor
    ''' </summary>
    ''' <param name="SupplierId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIVARetentionPercentageBySupplierId(SupplierId As Integer) As Decimal Implements IPaymentSupplier.GetIVARetentionPercentageBySupplierId
        Using service As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Return service.GetIVARetentionPercentageBySupplierId(SupplierId)
        End Using
        'Return Me._supplierAdminService.GetIVARetentionPercentageBySupplierId(SupplierId)
    End Function

    ''' <summary>
    ''' Obtiene el valor del porcentaje de retencion de iva que maneja el proveedor
    ''' </summary>
    ''' <param name="IdDistributionLines"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierIdByIdDistributionLines(IdDistributionLines As Integer) As Integer Implements IPaymentSupplier.GetSupplierIdByIdDistributionLines
        Using service As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Return service.GetSupplierIdByIdDistributionLines(IdDistributionLines)
        End Using
        'Return Me._supplierAdminService.GetSupplierIdByIdDistributionLines(IdDistributionLines)
    End Function

End Class
