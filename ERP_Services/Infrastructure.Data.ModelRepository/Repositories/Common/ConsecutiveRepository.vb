'************************************************************
' Assembly         : Infraestructure.Data.CommonRepository
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Infrastructure.Data.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio Consecutivos.
''' </summary>
Public Class ConsecutiveRepository
    Inherits GenericRepository(Of Consecutive)
    Implements IConsecutiveRepository

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
    ''' Función para obtener un consecutivo segun el codigo.
    ''' </summary>
    ''' <param name="code">Código Consecutivo</param>
    ''' <returns>Objeto Consecutivo</returns>
    Public Function GetConsecutiveByCode(code As String) As Consecutive Implements IConsecutiveRepository.GetConsecutiveByCode

        Dim Consecutive = (From e In _context.Consecutive
     Where e.Code = code
     Select e)
        If Consecutive.Count > 0 Then
            Return Consecutive.Single
        Else
            Dim objeto As New Consecutive
            Return objeto
        End If
    End Function

    ''' <summary>
    ''' Función para obtener todas los consecutivos.
    ''' </summary>
    ''' <returns>Lista de Consecutivos</returns>
    Public Function ListAllConsecutives() As List(Of Consecutive) Implements IConsecutiveRepository.ListAllConsecutives
        Dim Busqueda = From e In _context.Consecutive
                  Select e

        Return Busqueda.ToList
    End Function

End Class
