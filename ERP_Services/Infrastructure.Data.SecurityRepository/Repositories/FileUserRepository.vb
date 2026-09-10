#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Security.Entities
Imports Domain.Security
Imports System.Globalization
#End Region
Public Class FileUserRepository
    Inherits GenericRepository(Of FilePerson)
    Implements IFileUserRepository

    ''' <summary>
    ''' Esta variable  contiene el contexto de nuestro modelo.
    ''' </summary>
    Private _context As ISeguridadUnitOfWork
    ''' <summary>
    ''' Inicializa una nueva instancia <see cref="UserRepository" /> class.	
    ''' </summary>
    ''' <param name="contex">nThe contex.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Funcion  Para cargar Archivo Personales y validar 
    ''' </summary>
    ''' <param name="CodePerson">codigo de la persona</param>
    ''' <returns></returns>
    Public Function GetFileUser(CodePerson As String) As FilePerson Implements IFileUserRepository.getFileUser
        Dim Busqueda = (From e In _context.FilePerson
                       Where e.IdPerson = CodePerson
                       Select e).ToList()
        If Busqueda IsNot Nothing AndAlso Busqueda.Count > 0 Then
            Return Busqueda(0)
        Else
            Return New FilePerson()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el archivo personal del usuario
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Archivo personal</returns>
    Public Function GetFileUserByUserCode(userCode As String) As FilePerson Implements IFileUserRepository.GetFileUserByUserCode
        Dim res = (From u In Me._context.User.Include("Person") Where u.UserCode = userCode Select u).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Dim id = res.Item(0).IdPerson
            Dim result = (From f In Me._context.FilePerson.Include("Person.User") Where f.Person.Id = id Select f).ToList()
            If result IsNot Nothing AndAlso result.Count > 0 Then
                Return result(0)
            Else
                Return New FilePerson()
            End If
        Else
            Return New FilePerson()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el archivo personal del usuario
    ''' </summary>
    ''' <param name="userId">Codigo del usuario</param>
    ''' <returns>Archivo personal</returns>
    Public Function GetFileUserByUserId(userId As Integer) As FilePerson Implements IFileUserRepository.GetFileUserByUserId
        'Dim res = (From u In Me._context.User.Include("Person") Where u.Id = userId Select u).ToList()
        'If res IsNot Nothing AndAlso res.Count > 0 Then
        '    Dim id = res.Item(0).Id

        'Else
        '    Return New FilePerson()
        'End If
        Dim result = (From f In Me._context.FilePerson Where f.IdPerson = userId Select f).FirstOrDefault()
        If result IsNot Nothing Then
            Return result
        Else
            Return New FilePerson()
        End If
    End Function
End Class
