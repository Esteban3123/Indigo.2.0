Imports Domain.Base
Imports Domain.Common.Entities

Public Interface ICountryRepository
    Inherits IRepository(Of Country)

    ''' <summary>
    ''' funcion la cual retorna todos los paises
    ''' </summary>
    ''' <returns>Lista de paises</returns>
    ''' <remarks></remarks>
    Function ListAllCountry() As List(Of Country)

    ''' <summary>
    '''  funcion el cual trae un pais en especifico
    ''' </summary>
    ''' <param name="code">_Codigo del pais</param>
    ''' <returns>Pais</returns>
    ''' <remarks></remarks>
    Function GetCountry(ByVal code As String) As Country

    ''' <summary>
    ''' Devuelve un país por ID
    ''' </summary>
    ''' <param name="idCountry">Id del pais</param>
    ''' <returns>El pais</returns>
    ''' <remarks></remarks>
    Function GetCountryById(ByVal idCountry As Integer, Optional tracking As Boolean = True) As Country

End Interface
