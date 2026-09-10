'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class ProcedureTemplateRepository
    Inherits GenericRepository(Of ProcedureTemplate)
    Implements IProcedureTemplateRepository

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
    ''' Obtiene una plantilla de procedimiento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetProcedureTemplate(code As String) As ProcedureTemplate Implements IProcedureTemplateRepository.GetProcedureTemplate
        Dim res = (From pt In _context.ProcedureTemplate Where pt.Code = code Select pt).FirstOrDefault()
        If res IsNot Nothing Then
            'For Each item In res.ProcedureCups
            '    Dim cups = (From c In _context.CupsEntity.AsNoTracking() Where item.CupsId = c.Id Select c).FirstOrDefault()
            '    item.CodeNameCUPS = cups.Code + " - " + cups.Description
            'Next
            res.OriginalValue = (From pt In _context.ProcedureTemplate.AsNoTracking Where pt.Code = code Select pt).FirstOrDefault()
            Return res
        End If
        Return New ProcedureTemplate
    End Function

    ''' <summary>
    ''' Obtiene una plantilla de procedimiento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetProcedureTemplateById(id As Integer) As ProcedureTemplate Implements IProcedureTemplateRepository.GetProcedureTemplateById
        Dim res = (From pt In _context.ProcedureTemplate Where pt.Id = id Select pt).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From pt In _context.ProcedureTemplate.AsNoTracking Where pt.Id = id Select pt).FirstOrDefault()
            Return res
        End If
        Return New ProcedureTemplate
    End Function

    ''' <summary>
    ''' Guarda el listado en la BD
    ''' </summary>
    ''' <param name="ListProcedureCups"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveList(ListProcedureCups As List(Of ProcedureCups)) As List(Of ProcedureCups) Implements IProcedureTemplateRepository.SaveList
        Return Me._context.ProcedureCups.AddRange(ListProcedureCups).ToList()
    End Function

    ''' <summary>
    ''' Opción de copiar y pegar del form de plantilla de procedimientos
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_CopyAndPasteProcedureTemplate(xmlObject As String) As List(Of SP_CopyAndPasteProcedureTemplate_Result) Implements IProcedureTemplateRepository.SP_CopyAndPasteProcedureTemplate
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteProcedureTemplate(xmlObject).ToList
    End Function

End Class
