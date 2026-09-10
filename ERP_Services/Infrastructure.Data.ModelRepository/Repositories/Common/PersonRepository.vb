'***********************************************************************
' Assembly         : Infrastructure.Data.CommonRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.CrossCutting.Base

Public Class PersonRepository
    Inherits GenericRepository(Of Person)
    Implements IPersonRepository

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
    ''' Obtiene una persona atraves de una identificacion
    ''' </summary>
    ''' <param name="identificationNumber">Numero de Identificacion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPersonByIdentification(identificationNumber As String, Optional tracking As Boolean = True) As Person Implements IPersonRepository.GetPersonByIdentification
        If tracking = True Then

            Dim identification = From e In _context.Person.Include("Email").Include("Address").Include("Phone")
                                 Where e.IdentificationNumber = identificationNumber
                                 Select e
            If identification.Count > 0 Then
                Dim person = identification.SingleOrDefault()

                If person.Address IsNot Nothing AndAlso person.Address.Count > 0 Then
                    For Each address In person.Address
                        If address.DepartmentId IsNot Nothing Then
                            address.DepartmentName = (From d In _context.Department Where d.Id = address.DepartmentId Select d.Name).FirstOrDefault()
                            If address.CityId IsNot Nothing Then
                                address.CityName = (From d In _context.City Where d.Id = address.CityId Select d.Name).FirstOrDefault()
                            End If
                        End If
                    Next
                End If

                If person?.IdentificationTypeId IsNot Nothing Then
                    Dim ADTIPOIDENTIFICA = (From x In _context.ADTIPOIDENTIFICA.AsNoTracking() Where x.ID = person.IdentificationTypeId Select x).FirstOrDefault
                    person.IdentificationType = Utils.IdentificationTypeObsolete(ADTIPOIDENTIFICA?.SIGLA).Item1
                    person.IdentificationTypeName = $"{ADTIPOIDENTIFICA.CODIGO} - {ADTIPOIDENTIFICA.NOMBRE}"
                End If

                Return person
            Else
                Return New Person() With {.State = True}
            End If
        Else

            Dim person = From e In _context.Person.AsNoTracking
                         Where e.IdentificationNumber = identificationNumber
                         Select e
            If person.Count > 0 Then
                Return person.FirstOrDefault()
            Else
                Return New Person() With {.State = True}
            End If
        End If
    End Function

    ''' <summary>
    ''' Lista todas las personas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPerson() As List(Of Person) Implements IPersonRepository.ListAllPerson
        Dim identification = From e In _context.Person
                             Select e
        Return identification.ToList()
    End Function

    ''' <summary>
    ''' Eliminar el Tercero,  direcciones, telefonos, Correos y persona
    ''' </summary>
    ''' <param name="NitThirdParty">Nit del tercero</param>
    ''' <returns></returns>
    Public Function SP_DeleteThirdParty(NitThirdParty As String) As SP_DeleteThirdParty_Result Implements IPersonRepository.SP_DeleteThirdParty
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_DeleteThirdParty(NitThirdParty).SingleOrDefault
    End Function
End Class
