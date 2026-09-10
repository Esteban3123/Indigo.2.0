'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo
' Created          : 26-10-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance

Public Class CostCenterMaintenanceRepository
    Inherits GenericRepository(Of CostCenter)

    Implements ICostCenterMaintenanceRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IMaintenanceModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Mantenimiento
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IMaintenanceModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
 
    ''' <summary>
    ''' Obtiene un centro de costo especifico
    ''' </summary>
    ''' <param name="code">Codigo del centro de costo</param>
    ''' <returns>Centro de costo</returns>
    ''' <remarks></remarks>
    Public Function GetCostCenter(code As String, Optional desatach As Boolean = True) As CostCenter Implements ICostCenterMaintenanceRepository.GetCostCenter
        Dim costCenter = From e In _context.CostCenter
                        Where e.Code = code
                        Select e
        If costCenter.Count > 0 Then
            Dim objCostCenter = Nothing
            If desatach = False Then
                objCostCenter = (From e In _context.CostCenter.AsNoTracking
                                 Where e.Code = code
                                 Select e).SingleOrDefault
            Else
                objCostCenter = costCenter.SingleOrDefault()
            End If
            Return CType(objCostCenter, Entities.CostCenter)
        Else
            Return New CostCenter()
        End If
    End Function

    ''' <summary>
    ''' Centro de costo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterById(id As Integer, tracking As Boolean) As CostCenter Implements ICostCenterMaintenanceRepository.GetCostCenterById
        If tracking = True Then
            Dim busqueda = From e In _context.CostCenter
                       Where e.Id = id
                       Select e
            If busqueda.Count > 0 Then
                Return busqueda.FirstOrDefault()
            Else
                Return New CostCenter
            End If
        Else
            Dim busqueda = From e In _context.CostCenter.AsNoTracking
                       Where e.Id = id
                       Select e
            If busqueda.Count > 0 Then
                Return busqueda.FirstOrDefault()
            Else
                Return New CostCenter
            End If
        End If
    End Function

    ''' <summary>
    ''' Lista todos los centros de costos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllCostCenter() As List(Of CostCenter) Implements ICostCenterMaintenanceRepository.ListAllCostCenter
        Dim costCenter = From e In _context.CostCenter
                         Select e
        Return costCenter.ToList()
    End Function
End Class
