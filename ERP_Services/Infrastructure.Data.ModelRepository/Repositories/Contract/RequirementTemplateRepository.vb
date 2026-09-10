'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class RequirementTemplateRepository
    Inherits GenericRepository(Of RequirementTemplate)
    Implements IRequirementTemplateRepository


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
    ''' Obtiene una plantilla de requerimiento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetRequirementTemplate(code As String) As RequirementTemplate Implements IRequirementTemplateRepository.GetRequirementTemplate
        Dim res = (From pt In _context.RequirementTemplate.Include("Requirement") Where pt.Code = code Select pt).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From pt In _context.RequirementTemplate.AsNoTracking Where pt.Code = code Select pt).FirstOrDefault()
            Return res
        End If
        Return New RequirementTemplate
    End Function

    ''' <summary>
    ''' Obtiene una plantilla de requerimiento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetRequirementTemplateById(id As Integer) As RequirementTemplate Implements IRequirementTemplateRepository.GetRequirementTemplateById
        Dim res = (From pt In _context.RequirementTemplate.Include("Requirement") Where pt.Id = id Select pt).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From pt In _context.RequirementTemplate.AsNoTracking Where pt.Id = id Select pt).FirstOrDefault()
            Return res
        End If
        Return New RequirementTemplate
    End Function
End Class
