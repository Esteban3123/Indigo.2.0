'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Henry Alejandro Vargas Polania
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentSupplier

    ''' <summary>
    ''' Obtiene el porcentaje de retencion de iva que maneja el proveedor
    ''' </summary>
    ''' <param name="SupplierId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetIVARetentionPercentageBySupplierId(ByVal SupplierId As Integer) As Decimal

    ''' <summary>
    ''' Obtiene el porcentaje de retencion de iva que maneja el proveedor
    ''' </summary>
    ''' <param name="IdDistributionLines"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetSupplierIdByIdDistributionLines(ByVal IdDistributionLines As Integer) As Integer

End Interface
