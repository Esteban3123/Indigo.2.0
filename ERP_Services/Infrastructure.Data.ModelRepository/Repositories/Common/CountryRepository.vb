Imports Infrastructure.Data.Base
Imports Domain.Entities


Public Class CountryRepository
    Inherits GenericRepository(Of Country)
    Implements ICountryRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function GetCountry(code As String) As Country Implements ICountryRepository.GetCountry
        Dim _country = From e In _context.Country
            Where e.Code = code
        Select e
        If (_country.Count > 0) Then
            _country.Single().OriginalValue = (From e In _context.Country.AsNoTracking()
                                               Where e.Code = code
                                               Select e).FirstOrDefault()
            Return _country.Single()
        Else
            Return New Country()
        End If
    End Function

    ''' <summary>
    ''' Devuelve un país por ID
    ''' </summary>
    ''' <param name="idCountry">Id del pais</param>
    ''' <returns>El pais</returns>
    ''' <remarks></remarks>
    Public Function GetCountryById(ByVal idCountry As Integer, Optional tracking As Boolean = True) As Country Implements ICountryRepository.GetCountryById
        If tracking Then
            Dim _country = From e In _context.Country
                       Where e.Id = idCountry
            Select e

            If _country.Count > 0 Then
                Return _country.Single
            Else
                Return New Country
            End If
        Else
            Dim _country = From e In _context.Country.AsNoTracking
                           Where e.Id = idCountry
                           Select e

            If _country.Count > 0 Then
                Return _country.Single
            Else
                Return New Country
            End If
        End If

    End Function

    Public Function ListAllCountry() As List(Of Country) Implements ICountryRepository.ListAllCountry
        Dim Busqueda = From e In _context.Country
                       Select e
        Return Busqueda.ToList()
    End Function
End Class
