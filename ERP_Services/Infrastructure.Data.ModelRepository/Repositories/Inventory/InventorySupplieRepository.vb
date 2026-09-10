'***********************************************************************
' Assembly         : Infrastructura.Data.ModelRepository.Inventory
' Author           : Hector Rodriguez Rubiano
' Created          : 08-01-2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class InventorySupplieRepository
    Inherits GenericRepository(Of InventorySupplie)
    Implements IInventorySupplieRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="context">el contexto.</param>
    ''' 
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Function GetInventorySupplieByCode(Code As String) As InventorySupplie Implements IInventorySupplieRepository.GetInventorySupplieByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From d As InventorySupplie In _context.InventorySupplie Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From isp As InventorySupplie In Me._context.InventorySupplie.AsNoTracking() Where isp.Code.Equals(Code.Trim()) Select isp).FirstOrDefault()
            res.NullTextLevelRisk = (From irl In Me._context.InventoryRiskLevel.AsNoTracking Where irl.Id = res.RiskLevelId Select String.Concat(irl.Code, " - ", irl.Name)).FirstOrDefault
            Return res
        Else
            Return New InventorySupplie()
        End If
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllInventorySupplie() As List(Of InventorySupplie) Implements IInventorySupplieRepository.ListAllInventorySupplie
        Dim ListInventorySupplie = From e In _context.InventorySupplie
                                   Select e

        If ListInventorySupplie.Count() > 0 Then
            Return ListInventorySupplie.ToList()
        Else
            Return New List(Of InventorySupplie)
        End If
    End Function

    ''' <summary>
    ''' Obtiene un insumo de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventorySupplieById(id As Integer) As InventorySupplie Implements IInventorySupplieRepository.GetInventorySupplieById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From ir In Me._context.InventorySupplie Where ir.Id = id
                   Select ir).FirstOrDefault

        If res IsNot Nothing Then
            res.OriginalValue = (From isp In Me._context.InventorySupplie.AsNoTracking Where isp.Id = id Select isp).FirstOrDefault
            res.NullTextLevelRisk = (From irl In Me._context.InventoryRiskLevel.AsNoTracking Where irl.Id = res.RiskLevelId Select String.Concat(irl.Code, " - ", irl.Name)).FirstOrDefault
            Return res
        Else
            Return New InventorySupplie
        End If

    End Function

    ''' <summary>
    ''' Guarda el insumo
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveSupplie(Xml As String, UserCode As String) As SP_SaveSupplie_Result Implements IInventorySupplieRepository.SP_SaveSupplie
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveSupplie(Xml, UserCode).SingleOrDefault
    End Function

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function SP_DeleteSupplie(Id As Integer) As SP_DeleteSupplie_Result Implements IInventorySupplieRepository.SP_DeleteSupplie
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_DeleteSupplie(Id).SingleOrDefault
    End Function
End Class
