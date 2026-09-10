'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CupsSubGroupRepository
    Inherits GenericRepository(Of CupsSubgroup)
    Implements ICupsSubGroupRepository

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
    ''' Obtiene un subgrupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsSubgroup(code As String) As CupsSubgroup Implements ICupsSubGroupRepository.GetCupsSubgroup
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As CupsSubgroup In Me._context.CupsSubgroup
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim cupsGroup = (From cg In _context.CupsGroup.AsNoTracking Where cg.Id = res.CupsGroupId Select cg).FirstOrDefault
            res.CupsGroupDescription = cupsGroup.Code + " - " + cupsGroup.Name

            res.OriginalValue = (From g In _context.CupsSubgroup.AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault

            Return res
        Else
            Return New CupsSubgroup()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un subgrupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsSubgroupById(id As Integer) As CupsSubgroup Implements ICupsSubGroupRepository.GetCupsSubgroupById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.CupsSubgroup Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As CupsSubgroup In Me._context.CupsSubgroup.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New CupsSubgroup()
        End If
    End Function
    
End Class
