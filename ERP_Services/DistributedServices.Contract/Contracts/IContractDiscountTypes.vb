'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Angi Camila Duran Vargas
' Created          : 15/03/2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IContractDiscountTypes

    ''' <summary>
    ''' Guarda o Actualiza un tipo de descuento
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveDiscountTypes(DiscountTypes As Domain.Entities.DiscountTypes, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DiscountTypes)

    ''' <summary>
    ''' Elimina un tipo de descuento
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteDiscountTypes(DiscountTypes As Domain.Entities.DiscountTypes, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un determinado  tipo de descuento
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDiscountTypes(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DiscountTypes)

    ''' <summary>
    ''' Obtiene  un tipo de descuento
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDiscountTypesById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DiscountTypes)

End Interface
