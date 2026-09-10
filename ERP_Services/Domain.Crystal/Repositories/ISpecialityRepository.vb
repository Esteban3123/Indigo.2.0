'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Cristhian Mauricio Salazar
' Created          : 2014-11-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities
Public Interface ISpecialityRepository
    Inherits IRepository(Of INESPECIA)

    ''' <summary>
    ''' Obtiene una especialidad por codigo
    ''' </summary>
    ''' <returns>Entidad plana serializada</returns>
    Function GetSpecialityByCode(ByVal code As String) As INESPECIA

    ''' <summary>
    ''' Obtiene una RIAS por id
    ''' </summary>
    Function GetRIASById(ByVal id As Integer) As RIAS

End Interface
