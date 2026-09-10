Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class DepartmentRepository
    Inherits GenericRepository(Of Department)
    Implements IDepartmentRepository



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

    Public Function GetDepartment(code As String, IdCountry As String) As Department Implements IDepartmentRepository.GetDepartment
        Dim _department = From e In _context.Department
           Where e.Code = code And e.CountryId = IdCountry
       Select e
        If (_department.Count > 0) Then
            Return _department.Single()
        Else
            Return New Department()
        End If
    End Function

    Public Function GetDepartmentById(ByVal idDepartment As Integer, Optional traking As Boolean = True) As Department Implements IDepartmentRepository.GetDepartmentById
        If traking Then
            Dim _department = From e In _context.Department
                              Where e.Id = idDepartment
                              Select e
            If (_department.Count > 0) Then
                Return _department.Single()
            Else
                Return New Department()
            End If
        Else
            Dim _department = From e In _context.Department.AsNoTracking
                              Where e.Id = idDepartment
                              Select e
            If (_department.Count > 0) Then
                Return _department.Single
            Else
                Return New Department()
            End If
        End If

    End Function

    Public Function ListAllDepartment() As List(Of Department) Implements IDepartmentRepository.ListAllDepartment
        Dim Busqueda = From e In _context.Department
                       Select e
        Return Busqueda.ToList()
    End Function

    Public Function GetDepartments(IdCountry As String) As List(Of Department) Implements IDepartmentRepository.GetDepartments
        Dim _department = From e In _context.Department
           Where e.CountryId = IdCountry
       Select e
        If (_department.Count > 0) Then
            Return _department.ToList
        Else
            Return New List(Of Department)
        End If
    End Function
End Class
