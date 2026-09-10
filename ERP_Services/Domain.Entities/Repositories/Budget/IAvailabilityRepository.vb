'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25/082015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IAvailabilityRepository
    Inherits IRepository(Of Availability)

    ''' <summary>
    ''' Obtiene una disponibilidad por codigo
    ''' </summary>
    '''<param name="Code">Código del reconocimiento</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Function GetAvailability(Code As String, ItemType As Byte, BudgetaryValidityId As Integer, Optional FlagTracking As Boolean = False) As Availability

    ''' <summary>
    ''' Obtiene un disponibilidad por id
    ''' </summary>
    '''<param name="Id">Id del reconocimiento</param>
    ''' <returns></returns>
    Function GetAvailabilityById(Id As Integer, Optional FlagTracking As Boolean = False) As Availability

End Interface
