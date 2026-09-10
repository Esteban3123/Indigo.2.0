'***********************************************************************
' Assembly         : Domain.Seedwork
' Author           : Juan F. Tamayo
' Created          : 2014-01-15
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IRetentionConceptAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene un concepto de retencion
    ''' </summary>
    ''' <param name="code">Código de la clase contable</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Clase contable</returns>
    Function GetRetentionConcept(ByVal code As String, ByVal audit As AuditMessage) As RetentionConcepts

    ''' <summary>
    ''' Guarda un concepto de retencion
    ''' </summary>
    ''' <param name="retentionConcept">The retention concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveRetentionConcept(ByVal retentionConcept As RetentionConcepts, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of RetentionConcepts)

    ''' <summary>
    ''' Elimina un concepto de retención
    ''' </summary>
    ''' <param name="retentionConcept">The retention concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteRetentionConcept(ByVal retentionConcept As RetentionConcepts, ByVal audit As AuditMessage) As ActionResult


    ''' <summary>
    ''' Gets the retention by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetRetentionById(ByVal id As Integer, ByVal audit As AuditMessage) As RetentionConcepts

    ''' <summary>
    ''' Gets the retention concept by city.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="addressId">The address identifier.</param>
    ''' <returns></returns>
    Function GetRetentionConceptByCity(id As Integer, addressId As Integer) As RetentionConceptByCity

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ChangeStateRetentionConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of RetentionConcepts)

    ''' <summary>
    ''' Obtiene un listado de rangos de retenciones para 383 y 384
    ''' </summary>
    ''' <param name="RetentionConceptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListRetentionRangeByRetentionConceptId(RetentionConceptId As Integer) As ActionResult(Of List(Of RetentionConceptRanges))

    ''' <summary>
    ''' Obtiene el concepto de retención ICA asociado a una determinada sucursal
    ''' </summary>
    ''' <param name="brachOfficeId">The brach office identifier.</param>
    ''' <returns></returns>
    Function GetRetentionConceptByIdBrachOfficeId(brachOfficeId As Integer) As RetentionConcepts
    Function GetIVARetentionConceptByThirdPartyId(thirdPartyId As Integer) As RetentionConcepts

#End Region

End Interface
