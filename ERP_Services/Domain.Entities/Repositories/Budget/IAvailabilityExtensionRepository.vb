'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Juan Carlos Bermudez
' Created          : 23/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IAvailabilityExtensionRepository
    Inherits IRepository(Of AvailabilityExtension)

    ''' <summary>
    ''' Obtiene una prorroga de disponibilidad por id
    ''' </summary>
    '''<param name="Id"></param>
    ''' <returns></returns>
    Function GetAvailabilityExtensionById(Id As Integer) As AvailabilityExtension

End Interface
