'***********************************************************************
' Assembly         : Domain.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 11-04-2011
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Common.Entities

Public Interface ICityRepository
    Inherits IRepository(Of City)

    ''' <summary>
    '''  metodo que retorna un ciudad especifica
    ''' </summary>
    ''' <param name="code">Codigo ciudad</param>
    ''' <returns>Ciudad</returns>
    Function GetCity(ByVal code As String) As City

    ''' <summary>
    ''' metodo que retorna todas las ciudades
    ''' </summary>
    ''' <returns>Lista de ciudades</returns>
    Function ListAllCity() As List(Of City)

    ''' <summary>
    ''' Metodo que retorna una ciudad especifica
    ''' </summary>
    ''' <param name="code">Codigo de la ciudad</param>
    ''' <param name="idDepartamento">IdDepartamento</param>
    ''' <returns>City</returns>
    ''' <remarks></remarks>
    Function GetCity(ByVal code As String, ByVal idDepartamento As Integer) As City

    ''' <summary>
    ''' Metodo que lista todas las ciudades dependiendo del departanmento
    ''' </summary>
    ''' <param name="IdDepartment">Id del departamento</param>
    ''' <returns>Lista de ciudades</returns>
    ''' <remarks></remarks>
    Function ListAllCitiesByIdDepartment(ByVal idDepartment As Integer) As List(Of City)

    ''' <summary>
    ''' Obtiene una ciudad especifica
    ''' </summary>
    ''' <param name="idCity">Id de la ciudad</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    Function GetCityById(ByVal idCity As Integer, Optional tracking As Boolean = True) As City

End Interface
