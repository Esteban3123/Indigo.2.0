'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ProcedureCupsRepository
    Inherits GenericRepository(Of ProcedureCups)
    Implements IProcedureCupsRepository


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
    ''' Obtiene un detalle plantilla de procedimiento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetProcedureCupsById(id As Integer) As ProcedureCups Implements IProcedureCupsRepository.GetProcedureCupsById
        Dim res = (From pt In _context.ProcedureCups Where pt.Id = id Select pt).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New ProcedureCups
    End Function

    

    
    ''' <summary>
    ''' lista los procedimientos cups por platilla de procedimiento y cupsId
    ''' </summary>
    ''' <param name="procedureTemplateId"></param>
    ''' <param name="CupsId"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetProcedureProcedureCupsByProcedureTemplateIdAndCUPSId(procedureTemplateId As Integer, CupsId As Integer, Optional tracking As Boolean = True) As List(Of ProcedureCups) Implements IProcedureCupsRepository.GetProcedureProcedureCupsByProcedureTemplateIdAndCUPSId
        If tracking Then
            Return (From pc In _context.ProcedureCups Where pc.ProceduresTemplateId = procedureTemplateId And pc.CupsId = CupsId Select pc).ToList()
        End If
        Return (From pc In _context.ProcedureCups.AsNoTracking() Where pc.ProceduresTemplateId = procedureTemplateId And pc.CupsId = CupsId Select pc).ToList()
    End Function

    Public Function GetCountProcedureCupsByProcedureTemplateIdAndCupsId(procedureTemplateId As Integer, CupsId As Integer, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As Integer Implements IProcedureCupsRepository.GetCountProcedureCupsByProcedureTemplateIdAndCupsId
        If (RiasId Is Nothing Or RiasId = 0) AndAlso ContractDescriptionId IsNot Nothing AndAlso ContractDescriptionId > 0 Then
            Return (From pc In _context.ProcedureCups.AsNoTracking() Where pc.ProceduresTemplateId = procedureTemplateId And pc.CupsId = CupsId And pc.ContractDescriptionId = ContractDescriptionId Select pc).Count()
        End If

        Return (From pc In _context.ProcedureCups.AsNoTracking() Where pc.ProceduresTemplateId = procedureTemplateId And pc.CupsId = CupsId Select pc).Count()
    End Function

End Class
