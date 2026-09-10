'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 23/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IAvailabilityExtensionAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene una prorroga de disponibilidad por id
    ''' </summary>
    '''<param name="Id">Id de la disponibilidad</param>
    ''' <returns></returns>
    Function GetAvailabilityExtensionById(Id As Integer) As AvailabilityExtension

    ''' <summary>
    ''' Guarda o Actualiza una prorroga de disponibilidad
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveAvailabilityExtension(ByVal availabilityExtension As AvailabilityExtension, ByVal audit As AuditMessage) As ActionResult(Of AvailabilityExtension)

#End Region

End Interface
