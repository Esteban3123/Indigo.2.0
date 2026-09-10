'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CupsGroupRepository
    Inherits GenericRepository(Of CupsGroup)
    Implements ICupsGroupRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsGroup(code As String) As CupsGroup Implements ICupsGroupRepository.GetCupsGroup
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As CupsGroup In Me._context.CupsGroup
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From g In _context.CupsGroup.AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault

            Return res
        Else
            Return New CupsGroup()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsGroupById(id As Integer) As CupsGroup Implements ICupsGroupRepository.GetCupsGroupById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.CupsGroup Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As CupsGroup In Me._context.CupsGroup.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New CupsGroup()
        End If
    End Function
    
End Class
