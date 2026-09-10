'***********************************************************************
' Assembly         : Infrastructure.Data.MisxingStation
' Author           : Andres Alarcon
' Created          : 26/08/2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class CategoryDefectsRepository
    Inherits GenericRepository(Of DefectClassificationGroup)
    Implements ICategoryDefectsRepository, Inject

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

    Public Function GetCategoryDefectsByCode(code As String) As DefectClassificationGroup Implements ICategoryDefectsRepository.GetCategoryDefectsByCode
        Dim res = (From bg In _context.DefectClassificationGroup Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.DefectClassificationGroup.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New DefectClassificationGroup
        End If
    End Function

    Public Function GetCategoryDefectsById(id As Integer) As DefectClassificationGroup Implements ICategoryDefectsRepository.GetCategoryDefectsById
        Dim res = (From bg In _context.DefectClassificationGroup Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New DefectClassificationGroup
        End If
    End Function
End Class
