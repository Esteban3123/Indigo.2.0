'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 01-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IAvailabilityModificationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene una modificacion de disponibilidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAvailabilityModificationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As AvailabilityModification
    ''' <summary>
    ''' obtiene una modificacion de disponibilidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAvailabilityModificationById(id As Integer) As AvailabilityModification
    ''' <summary>
    ''' Guarda una modificacion de disponibilidad
    ''' </summary>
    ''' <param name="availabilityModification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAvailabilityModification(AvailabilityModification As AvailabilityModification, listAvailabilityModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of AvailabilityModification)

End Interface
