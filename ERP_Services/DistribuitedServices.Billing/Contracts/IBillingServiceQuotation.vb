'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/01/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IBillingServiceQuotation

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveQuotation(Quotation As Quotation, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Quotation)

    ''' <summary>
    ''' Obtiene por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetQuotation(code As String) As ActionResult(Of Quotation)

    ''' <summary>
    ''' Obtiene por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetQuotationById(id As Integer) As ActionResult(Of Quotation)

End Interface
