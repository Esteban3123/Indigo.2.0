'***********************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz Mosquera
' Created          : 12-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

''' <summary>
''' 
''' </summary>
Public Class ResponsibleRepository

    Inherits GenericRepository(Of Responsible)
    Implements IResponsibleRepository

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

    ''' <summary>
    ''' Función para obtener un responsable
    ''' </summary>
    ''' <param name="codeResponsible">Código Responsable</param>
    ''' <returns></returns>
    Public Function GetResponsible(codeResponsible As String) As Responsible Implements IResponsibleRepository.GetResponsible
        Dim Responsible = From e In _context.Responsible
             Where e.Code = codeResponsible
             Select e
        If Responsible.Count > 0 Then
            Dim ResponsibleData = Responsible.SingleOrDefault
            ResponsibleData.OriginalValue = (From e In _context.Responsible.AsNoTracking
         Where e.Code = codeResponsible
         Select e).SingleOrDefault
            Return ResponsibleData
        End If
        Return New Responsible
    End Function

    ''' <summary>
    ''' Función para obtener un responsable por código ERP.
    ''' </summary>
    ''' <param name="codeERPResponsible">Código ERP</param>
    ''' <returns></returns>
    Public Function GetResponsibleByCodeERP(codeERPResponsible As String) As Responsible Implements IResponsibleRepository.GetResponsibleByCodeERP
        Dim Responsible = From e In _context.Responsible
         Where e.CodeUser = codeERPResponsible And e.State = True
         Select e
        If Responsible.Count > 0 Then
            Return Responsible.Single
        Else
            Return New Responsible
        End If
    End Function

    ''' <summary>
    ''' Función para listar todos los responsables.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListResponsibleAll() As List(Of ResponsibleAll) Implements IResponsibleRepository.ListResponsibleAll
        Dim Busqueda = From e In _context.Responsible
                       Where e.State = True
                            Select New ResponsibleAll With {.Id = e.Id, .responsibleCode = e.Code, .responsibleName = e.Name, .responsibleCodeName = e.Code + " - " + e.Name}
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Consulta el ID del responsable
    ''' </summary>
    ''' <param name="codeResponsible"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIdResponsible(codeResponsible As String) As Integer Implements IResponsibleRepository.GetIdResponsible
        Dim Responsible = From e In _context.Responsible
       Where e.Code = codeResponsible And e.State = True
       Select e
        If Responsible.Count > 0 Then
            Return Responsible.Single.Id
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Consulta el ID del concepto
    ''' </summary>
    ''' <param name="codeConcept"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIdConcepto(codeConcept As String) As Integer Implements IResponsibleRepository.GetIdConcepto
        Dim Concept = From e In _context.ConceptGlosas
       Where e.Code = codeConcept And e.State = True
       Select e
        If Concept.Count > 0 Then
            Return Concept.Single.Id
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' funcion para validar que no se repita codigo de usuarios
    ''' </summary>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListValidateResponsibleByCodeUSer(codeUser As String) As List(Of Responsible) Implements IResponsibleRepository.GetListValidateResponsibleByCodeUSer
        Dim Responsible = (From e In _context.Responsible
       Where e.CodeUser = codeUser
       Select e)
        Return Responsible.ToList()
    End Function

End Class
