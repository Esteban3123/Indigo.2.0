'***********************************************************************
' Assembly         : Infrastructure.Data.MisxingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/11/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class CauseReprocessingRejectionRepository
    Inherits GenericRepository(Of CauseReprocessingRejection)
    Implements ICauseReprocessingRejectionRepository, Inject

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

    Public Function GetCauseReprocessingRejectionByCode(code As String) As CauseReprocessingRejection Implements ICauseReprocessingRejectionRepository.GetCauseReprocessingRejectionByCode
        Dim res = (From bg In _context.CauseReprocessingRejection Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.CauseReprocessingRejection.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New CauseReprocessingRejection
        End If
    End Function

    Public Function GetCauseReprocessingRejectionById(id As Integer) As CauseReprocessingRejection Implements ICauseReprocessingRejectionRepository.GetCauseReprocessingRejectionById
        Dim res = (From bg In _context.CauseReprocessingRejection Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New CauseReprocessingRejection
        End If
    End Function
End Class
