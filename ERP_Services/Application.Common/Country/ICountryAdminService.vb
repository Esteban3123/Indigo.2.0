Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Public Interface ICountryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los paises.
    ''' </summary>
    ''' <returns></returns>
    Function ListAllCountry() As List(Of Country)

    ''' <summary>
    ''' Elimina un pais
    ''' </summary>
    ''' <param name="country">el pais</param>
    ''' <returns></returns>
    Function DeleteCountry(ByVal country As Country, ByVal audit As AuditMessage) As ActionMessageResult(Of Country)
    ''' <summary>
    ''' graba un Pais
    ''' </summary>
    ''' <param name="country">el Pais</param>
    ''' <returns></returns>
    Function SaveCountry(ByVal country As Country, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of Country)
    ''' <summary>
    ''' consulta un Pais
    ''' </summary>
    ''' <param name="code">el codigo del pais</param>
    ''' <returns></returns>
    Function GetCountry(ByVal code As String) As Country

    ''' <summary>
    ''' Devuelve un país por ID
    ''' </summary>
    ''' <param name="idCountry">Id del pais</param>
    ''' <returns>El pais</returns>
    ''' <remarks></remarks>
    Function GetCountryById(ByVal idCountry As Integer, ByVal audit As AuditMessage) As Country

End Interface
