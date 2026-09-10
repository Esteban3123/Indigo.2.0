'************************************************************
' Assembly         : Infraestructure.Data.CommonRepository
' Author           : Rafael Eduardo Patiño
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"


Imports Infrastructure.Data.Base
Imports Domain.Entities


#End Region

''' <summary>
''' Repositorio Conceptos GLosas.
''' </summary>
''' 
Public Class ConceptGlosasRepositoryvb
    Inherits GenericRepository(Of ConceptGlosas)
    Implements IConceptGlosasRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Funcion para listar concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptGlosas() As List(Of ConceptGlosas) Implements IConceptGlosasRepository.ListConceptGlosas
        Dim result = From e In _context.ConceptGlosas
               Select e
        For Each item As ConceptGlosas In result
            item.ConceptCodeName = item.Code + " - " + item.NameSpecific
        Next
        Return result.ToList
    End Function

    ''' <summary>
    ''' Funcion para cargar todos los conceptos glosa según tipo
    ''' </summary>
    ''' <returns>Lista de todos los conceptos de glosa</returns>
    Public Function ListConceptGlosaByType(type As String) As List(Of ConceptGlosas) Implements IConceptGlosasRepository.ListConceptGlosaByType
        Dim result = From e In _context.ConceptGlosas
                     Where e.Type = type
                 Select e
        For Each item As ConceptGlosas In result
            item.ConceptCodeName = item.Code + " - " + item.NameSpecific
        Next
        Return result.ToList
    End Function


    ''' <summary>
    ''' Funcion para cargar todos los conceptos glosa según lista de tipos
    ''' </summary>
    ''' <returns>Lista de todos los conceptos de glosa</returns>
    Public Function ListConceptGlosaByListTypes(types As List(Of String)) As List(Of ConceptGlosas) Implements IConceptGlosasRepository.ListConceptGlosaByListTypes
        Dim result = From e In _context.ConceptGlosas
                     Where types.Contains(e.Type)
                 Select e
        For Each item As ConceptGlosas In result
            item.ConceptCodeName = item.Code + " - " + item.NameSpecific
        Next
        Return result.ToList
    End Function

    ''' <summary>
    ''' Funcion para retornar lista de conceptos que aplican para descuentos de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptGlosasWithMedicalFees() As List(Of Integer) Implements IConceptGlosasRepository.ListConceptGlosasWithMedicalFees
        Dim result = From e In _context.ConceptGlosas
                     Where e.DiscountedMedicalFees = True
                     Select e.Id
        Return result.ToList
    End Function

End Class
