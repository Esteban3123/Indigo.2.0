'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 22/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IIPSServiceGroupAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Guarda o Actualiza una IPSServiceGroup
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveIPSServiceGroup(ByVal IPSServiceGroup As BillingConcept, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of BillingConcept)

    ''' <summary>
    ''' Elimina una IPSServiceGroup
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteIPSServiceGroup(ByVal IPSServiceGroup As BillingConcept, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una IPSServiceGroup por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetIPSServiceGroup(ByVal code As String, ByVal audit As AuditMessage) As BillingConcept

    ''' <summary>
    ''' Obtiene una IPSServiceGroup por id
    ''' </summary>
    ''' <returns></returns>
    Function GetIPSServiceGroupById(ByVal id As Integer) As BillingConcept
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateIPSServiceGroup(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of BillingConcept)
    ''' <summary>
    ''' Copia y pega los centros de costo por sucursal y unidad funcional
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Function CopyAndPasteBillingConceptCostCenter(data As List(Of List(Of String))) As ActionResult(Of List(Of BillingConceptCostCenter))
End Interface
