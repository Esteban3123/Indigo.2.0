'************************************************************
' Assembly         : Domain.Entities
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-04-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Contrato de repositorio para la entidad concepto de retencion
''' </summary>
Public Interface IRetentionConceptRepository
    Inherits IRepository(Of RetentionConcepts)

    ''' <summary>
    ''' Obtiene un concepto de retención
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetRetentionConcept(ByVal code As String) As RetentionConcepts

    ''' <summary>
    ''' Gets the retention by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetRetentionById(ByVal id As Integer, Optional ByVal Tracking As Boolean = False) As RetentionConcepts

    ''' <summary>
    ''' Gets the retention concept by city.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="addressId">The address identifier.</param>
    ''' <returns></returns>
    Function GetRetentionConceptByCity(id As Integer, addressId As Integer) As RetentionConceptByCity

    ''' <summary>
    ''' Obtiene un listado de rangos de las retenciones 383 y 384
    ''' </summary>
    ''' <param name="RetentionConceptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListRetentionRangeByRetentionConceptId(ByVal RetentionConceptId As Integer) As List(Of RetentionConceptRanges)
    ''' <summary>
    ''' Obtiene el concepto de retención ICA asociado a una determinada sucursal
    ''' </summary>
    ''' <param name="brachOfficeId">The brach office identifier.</param>
    ''' <returns></returns>
    Function GetRetentionConceptByIdBrachOfficeId(brachOfficeId As Integer) As RetentionConcepts
    ''' <summary>
    ''' Gets the iva retention concept by third party identifier.
    ''' </summary>
    ''' <param name="thirdPartyId">The third party identifier.</param>
    ''' <returns></returns>
    Function GetIVARetentionConceptByThirdPartyId(thirdPartyId As Integer) As RetentionConcepts
End Interface