'***********************************************************************
' Assembly         : Infrastructure.Data.Common
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class EconomicActivityRepository
    Inherits GenericRepository(Of EconomicActivity)
    Implements IEconomicActivityRepository

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
    ''' Obtiene una actividad economica por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEconomicActivity(code As String, Optional tracking As Boolean = True) As EconomicActivity Implements IEconomicActivityRepository.GetEconomicActivity
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As EconomicActivity In Me._context.EconomicActivity Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From e In _context.EconomicActivity.AsNoTracking Where e.Id = res.Id Select e).FirstOrDefault

            Return res
        Else
            Return New EconomicActivity()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una actividad economica por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEconomicActivityById(id As Integer, Optional tracking As Boolean = True) As EconomicActivity Implements IEconomicActivityRepository.GetEconomicActivityById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.EconomicActivity Where d.Id = id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d In Me._context.EconomicActivity.AsNoTracking Where d.Id = id Select d).FirstOrDefault()
            Return res
        Else
            Return New EconomicActivity()
        End If
    End Function
End Class
