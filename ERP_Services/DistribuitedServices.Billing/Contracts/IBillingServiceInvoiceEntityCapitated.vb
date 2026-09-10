'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-19
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Billing.POCO
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base


#End Region

<ServiceContract()>
Public Interface IBillingServiceInvoiceEntityCapitated

    ''' <summary>
    ''' Obtiene una factura por id
    ''' </summary>
    <OperationContract()>
    Function GetInvoiceEntityCapitatedById(ByVal Id As Integer) As InvoiceEntityCapitated

    ''' <summary>
    ''' Obtiene una factura de entidad capitada por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetInvoiceEntityCapitated(code As String, audit As AuditMessage) As ActionResult(Of InvoiceEntityCapitated)

    ''' <summary>
    ''' Guarda una factura a entidad capitada
    ''' </summary>
    <OperationContract()>
    Function SaveInvoiceEntityCapitated(invoice As InvoiceEntityCapitated, session As SessionValues) As Task(Of ActionResult(Of InvoiceEntityCapitated))

    ''' <summary>
    ''' Obtiene los valores totales por concepto de recaudo para un periodo de tiempo y grupo de atención establecido.
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial</param>
    ''' <param name="finalDate">Fecha final</param>
    ''' <param name="careGroupId">Grupo de atención</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCollectionValuesAsync(initialDate As DateTime, finalDate As DateTime, careGroupId As Integer, invoiceCategoryId As Integer) As Task(Of CollectionValues)

End Interface
