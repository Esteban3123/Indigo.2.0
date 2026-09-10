'***********************************************************************
' Assembly         : Infrastructure.Data.SecurityRepository
' Author           : Juan Diego Diaz
' Created          : 2013-08-20
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Security.Entities
Imports Domain.Security
Imports System.Globalization
#End Region

''' <summary>
''' Esta clase Repositorio Empresas hereda del repositorio generico para poder obtener los metodos
''' comunes en todos los repositorios de manera que esta clase solo contiene los metodos y funciones no Comunes.
''' </summary>


Public Class PersonRepository
    Inherits GenericRepository(Of Person)
    Implements IPersonRepository

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


End Class
