'***********************************************************************
' Assembly         : Infrastructure.Data.CommonRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Maintenance
Imports Infrastructure.Data.Base

Public Class PersonMaintenanceRepository
    Inherits GenericRepository(Of Domain.Maintenance.Entities.Person)
    Implements IPersonMaintenanceRepository

    ' contexto del repositorio de ciudades
    Private _context As IMaintenanceModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IMaintenanceModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una persona atraves de una identificacion
    ''' </summary>
    ''' <param name="identificationNumber">Numero de Identificacion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPersonByIdentification(identificationNumber As String) As Domain.Maintenance.Entities.Person Implements IPersonMaintenanceRepository.GetPersonByIdentification
        Dim identification = From e In _context.Person
                             Where e.IdentificationNumber = identificationNumber
                             Select e
        If identification.Count > 0 Then
            Return identification.SingleOrDefault()
        Else
            Return New Domain.Maintenance.Entities.Person()
        End If
    End Function

    ''' <summary>
    ''' Lista todas las personas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPerson() As List(Of Domain.Maintenance.Entities.Person) Implements IPersonMaintenanceRepository.ListAllPerson
        Dim identification = From e In _context.Person
                             Select e
        Return identification.ToList()
    End Function
End Class
