'***********************************************************************
' Assembly         : Infrastructure.Data.SecurityRepository
' Author           : OscarSierra
' Created          : 03-08-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-28
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Security.Entities
Imports Domain.Security
Imports System.Globalization
Imports System.Data.Entity

#End Region

''' <summary>
''' Esta clase Repositorio Empresas hereda del repositorio generico para poder obtener los metodos
''' comunes en todos los repositorios de manera que esta clase solo contiene los metodos y funciones no Comunes.
''' </summary>
Public Class LoginRepository
    Inherits GenericRepository(Of Roll)
    Implements ILoginRepository

    ''' <summary>
    ''' Esta variable  contiene el contexto de nuestro modelo.
    ''' </summary>
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia <see cref="LoginRepository" /> class.	
    ''' </summary>
    ''' <param name="contex">nThe contex.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' metodo que develve los valores originales de la entidad {T}
    ''' </summary>
    ''' <typeparam name="TEntidad">el tipo de la entidad.</typeparam>
    ''' <param name="entity">el objeto de la entidad.</param>
    ''' <returns></returns>
    Public Function GetSourceValues(Of TEntidad)(ByVal entity As TEntidad) As TEntidad
        Return IndigoContext.GetSourceValues(entity, CType(_context, DbContext))
    End Function

    ''' <summary>
    ''' Gets the finger print.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFingerPrint() As List(Of Domain.Security.Entities.Person) Implements ILoginRepository.GetFingerPrint
        Dim result = From e In _context.Person.Include("User")
                                     Select e
        Return result.ToList
    End Function

    ''' <summary>
    ''' Gets the rol user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRolUser(codeUser As String) As String Implements ILoginRepository.GetRolUser
        Dim roleUser = From e In _context.User
                      Where e.UserCode = codeUser.Trim And e.State = False
                      Select e.UserType

        If roleUser.Count > 0 Then
            Return roleUser.ToString
        Else
            Return String.Empty
        End If
    End Function

End Class
