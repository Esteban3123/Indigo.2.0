'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 01-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IAvailabilityModificationRepository
    Inherits IRepository(Of AvailabilityModification)

    ''' <summary>
    ''' obtiene una modificacion de disponibilidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAvailabilityModificationByCode(code As String, BudgetaryValidityId As Integer) As AvailabilityModification

    ''' <summary>
    ''' obtiene una modificacion de disponibilidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAvailabilityModificationById(id As Integer) As AvailabilityModification

    ''' <summary>
    ''' Guarda la modificacion
    ''' </summary>
    ''' <param name="AvailabilityModificationXml"></param>
    ''' <param name="AvailabilityModificationDetailForDeleteXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveAvailabilityModification(AvailabilityModificationXml As String, AvailabilityModificationDetailForDeleteXml As String, CodeUser As String) As SP_SaveAvailabilityModification_Result

End Interface
