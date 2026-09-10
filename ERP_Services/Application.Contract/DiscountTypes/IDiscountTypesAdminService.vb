'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Angi Camila Duran Vargas
' Created          : 15/03/2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IDiscountTypesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un tipo de descuento
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveDiscountTypes(ByVal DiscountTypes As DiscountTypes, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of DiscountTypes)

    ''' <summary>
    ''' Elimina un tipo de descuento
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteDiscountTypes(ByVal DiscountTypes As DiscountTypes, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un tipo de descuento por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetDiscountTypes(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of DiscountTypes)

    ''' <summary>
    ''' Obtiene un tipo de descuento por id
    ''' </summary>
    ''' <returns></returns>
    Function GetDiscountTypesById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of DiscountTypes)

End Interface
