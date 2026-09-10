'************************************************************
' Assembly         : Infraestructure.Data.CommonRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 03-07-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Base
Imports Infrastructure.Data.Base
Imports Domain.Entities
Public Class DisabilityRepository
    Inherits GenericRepository(Of Disability)
    Implements IDisabilityRepository

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
    '''  Obtiene una discapacidad especifica
    ''' </summary>
    ''' <param name="code">Codigo de la discapacidad</param>
    ''' <returns>Discapacidad</returns>
    Public Function GetDisability(code As String, Optional tracking As Boolean = True) As Disability Implements IDisabilityRepository.GetDisability
        If tracking Then
            Dim disability = From e In _context.Disability
                         Where e.Code = code
                         Select e
            If disability.Count > 0 Then
                Return disability.SingleOrDefault
            Else
                Return New Disability()
            End If
        Else
            Dim disability = From e In _context.Disability.AsNoTracking
                         Where e.Code = code
                         Select e
            If disability.Count > 0 Then
                Return disability.SingleOrDefault
            Else
                Return New Disability()
            End If
        End If
    End Function

    ''' <summary>
    ''' Retorna todas las discapacidades
    ''' </summary>
    ''' <returns>Lista de Discapacidades</returns>
    Public Function ListAllDisability() As List(Of Disability) Implements IDisabilityRepository.ListAllDisability
        Dim disability = From e In _context.Disability
                         Select e
        Return disability.ToList()
    End Function
End Class
