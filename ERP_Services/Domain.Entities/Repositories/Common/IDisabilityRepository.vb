'************************************************************
' Assembly         : Domain.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 03-07-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Common.Entities
Imports Domain.Base
Public Interface IDisabilityRepository
    Inherits IRepository(Of Disability)

    ''' <summary>
    ''' Retorna todas las discapacidades
    ''' </summary>
    ''' <returns>Lista de Discapacidades</returns>
    Function ListAllDisability() As List(Of Disability)

    ''' <summary>
    '''  Obtiene una discapacidad especifica
    ''' </summary>
    ''' <param name="code">Codigo de la discapacidad</param>
    ''' <returns>Discapacidad</returns>
    Function GetDisability(ByVal code As String, Optional tracking As Boolean = True) As Disability

End Interface
