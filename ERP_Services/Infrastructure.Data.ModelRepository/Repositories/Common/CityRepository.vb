'***********************************************************************
' Assembly         : Infrastructure.Data.CommonRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 11-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities


Public Class CityRepository
    Inherits GenericRepository(Of City)
    Implements ICityRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    '''  Obtiene una ciudad en especifico
    ''' </summary>
    ''' <param name="code">Codigo ciudad</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    Public Function GetCity(code As String) As City Implements ICityRepository.GetCity
        Dim city = (From e In _context.City
                    Where e.Code = code
                    Select e).FirstOrDefault()
        If city IsNot Nothing Then

            If city.ICARetentionConceptId IsNot Nothing Then
                city.ICARetentionDescription = (From x In _context.RetentionConcepts Where x.Id = city.ICARetentionConceptId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
            End If

            Return city
        Else
            Return New City()
        End If

    End Function

    ''' <summary>
    ''' Retorna todas las ciudades
    ''' </summary>
    ''' <returns>Lista de ciudades</returns>
    ''' <remarks></remarks>
    Public Function ListAllCity() As List(Of City) Implements ICityRepository.ListAllCity
        Dim city = From e In _context.City
                   Select e
        Return city.ToList
    End Function

    ''' <summary>
    ''' Retorna una ciudad especifica
    ''' </summary>
    ''' <param name="code">Codigo Ciudad</param>
    ''' <param name="idDepartamento">IdDeop</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCity(code As String, idDepartamento As Integer) As City Implements ICityRepository.GetCity
        Dim city = (From e In _context.City
                    Where e.Code = code And e.DepartamentId = idDepartamento
                    Select e).FirstOrDefault()
        If city IsNot Nothing Then

            If city.ICARetentionConceptId IsNot Nothing Then
                city.ICARetentionDescription = (From x In _context.RetentionConcepts Where x.Id = city.ICARetentionConceptId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
            End If

            Return city
        Else
            Return New City()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una ciudad especifica
    ''' </summary>
    ''' <param name="idCity">Id de la ciudad</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    Public Function GetCityById(idCity As Integer, Optional tracking As Boolean = True) As City Implements ICityRepository.GetCityById
        If tracking Then
            Dim city = From e In _context.City
                  Where e.Id = idCity
                  Select e
            If (city.Count > 0) Then
                Return city.Single
            Else
                Return New City()
            End If
        Else
            Dim city = (From e In _context.City.AsNoTracking
                  Where e.Id = idCity
                  Select e).SingleOrDefault
            If city IsNot Nothing Then
                Return city
            Else
                Return New City()
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo que lista todas las ciudades dependiendo del departanmento
    ''' </summary>
    ''' <param name="IdDepartment">Id del departamento</param>
    ''' <returns>Lista de ciudades</returns>
    ''' <remarks></remarks>
    Public Function ListAllCitiesByIdDepartment(ByVal idDepartment As Integer) As List(Of City) Implements ICityRepository.ListAllCitiesByIdDepartment
        Dim city = From e In _context.City
                   Where e.DepartamentId = idDepartment
                   Select e
        Return city.ToList()
    End Function
End Class
