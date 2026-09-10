'***********************************************************************
' Assembly         : Application.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.Threading.Tasks
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Billing.POCO
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IInvoiceEntityCapitatedAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una factura por id
    ''' </summary>
    Function GetInvoiceEntityCapitatedById(ByVal Id As Integer) As InvoiceEntityCapitated

    ''' <summary>
    ''' Obtiene una factura de entidad capitada por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetInvoiceEntityCapitated(code As String, audit As AuditMessage) As ActionResult(Of InvoiceEntityCapitated)

    ''' <summary>
    ''' Guarda una factura a entidad capitada
    ''' </summary>
    Function SaveInvoiceEntityCapitatedAsync(ByVal invoice As InvoiceEntityCapitated, session As SessionValues) As Task(Of ActionResult(Of InvoiceEntityCapitated))

    ''' <summary>
    ''' Obtiene los valores totales por concepto de recaudo para un periodo de tiempo y grupo de atención establecido.
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial</param>
    ''' <param name="finalDate">Fecha final</param>
    ''' <param name="careGroupId">Grupo de atención</param>
    ''' <returns></returns>
    Function GetCollectionValuesAsync(initialDate As DateTime, finalDate As DateTime, careGroupId As Integer) As Task(Of CollectionValues)

End Interface
