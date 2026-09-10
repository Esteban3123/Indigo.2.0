'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : John Ortiz
' Created          : 30/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class InventoryRiskLevelRepository
    Inherits GenericRepository(Of InventoryRiskLevel)
    Implements IInventoryRiskLevelRepository

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
    ''' Metodo para obtener un nivel de riesgo por codigo
    ''' </summary>
    ''' <param name="code">Codigo del nivel de riesgo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRiskLevelByCode(code As String, Optional ByVal tracking As Boolean = True) As InventoryRiskLevel Implements IInventoryRiskLevelRepository.GetRiskLevelByCode
        Dim query As IQueryable(Of InventoryRiskLevel)
        If tracking = True Then
            query = From e In _context.InventoryRiskLevel
                    Where e.Code = code
                    Select e
        Else
            query = From e In _context.InventoryRiskLevel.AsNoTracking()
                    Where e.Code = code
                    Select e
        End If
        If query.Count > 0 Then
            Return query.SingleOrDefault()
        Else
            Return New InventoryRiskLevel()
        End If
    End Function

    ''' <summary>
    ''' Función que se utiliza para Almacenar o Actualizar una Unidad de Medida
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="Description"></param>
    ''' <param name="Abbreviation"></param>
    ''' <param name="UnitType"></param>
    ''' <param name="Status"></param>
    ''' <param name="AllowEditCostValue"></param>
    ''' <param name="CostValue"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SaveRiskLevel(Code As String, Description As String, Status As Boolean, CodeUser As String) As SP_SaveInventoryRiskLevel_Result Implements IInventoryRiskLevelRepository.SaveRiskLevel
        Return _context.SP_SaveInventoryRiskLevel(Code, Description, Status, CodeUser).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function SP_DeleteRiskLevels(Id As Integer) As SP_DeleteRiskLevels_Result Implements IInventoryRiskLevelRepository.SP_DeleteRiskLevels
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_DeleteRiskLevels(Id).SingleOrDefault
    End Function

End Class
