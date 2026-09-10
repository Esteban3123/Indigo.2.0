'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 04-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance
Imports Domain.Base.Entities
Imports Domain.Base

#End Region


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la entidad aseguradora
''' </summary>
''' <remarks></remarks>
Public Class InsuranceRepository
    Inherits GenericRepository(Of Insurance)
    Implements IInsuranceRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IMaintenanceModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IMaintenanceModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub


    ''' <summary>
    ''' funcion para consultar una aseguradora por codigo
    ''' </summary>
    ''' <param name="codeInsurance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInsurance(codeInsurance As String, Optional Tracking As Boolean = False) As Insurance Implements IInsuranceRepository.GetInsurance
        If Tracking = False Then
            Dim Busqueda = From e In _context.Insurance.Include("Person").Include("Person.Address").Include("Person.Phone").Include("Person.Email")
                       Where e.Nit = codeInsurance
                       Select e
            If Busqueda.Count = 0 Then
                Dim Persona = From i In _context.Person
                                       Where i.IdentificationNumber = codeInsurance
                                       Select i
                Dim Insurance As New Insurance

                If Persona.Count = 0 Then
                    Insurance.Person = New Person
                Else
                    Insurance.Person = CType(Persona.Single, Person)
                End If

                Return Insurance

            Else
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.Insurance.Include("Person").AsNoTracking
                       Where e.Nit = codeInsurance
                       Select e
            If Busqueda.Count = 0 Then
                Dim Persona = From i In _context.Person
                                       Where i.IdentificationNumber = codeInsurance
                                       Select i
                Dim Insurance As New Insurance

                If Persona.Count = 0 Then
                    Insurance.Person = New Person
                Else
                    Insurance.Person = CType(Persona.Single, Person)
                End If

                Return Insurance

            Else
                Return Busqueda.Single
            End If
        End If
        
    End Function

    ''' <summary>
    ''' funcion para listar todas las aseguradores activas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllInsurance() As List(Of Insurance) Implements IInsuranceRepository.ListAllInsurance


        Dim Busqueda = From e In _context.Insurance
                                      Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' funcion para almacenar la entidad aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveInsurance(Insurance As Insurance) As Boolean Implements IInsuranceRepository.SaveInsurance
        _context.Insurance.ApplyChanges(Insurance)
        Return True
    End Function
End Class
