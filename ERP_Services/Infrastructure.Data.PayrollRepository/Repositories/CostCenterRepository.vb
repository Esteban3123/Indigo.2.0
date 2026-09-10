'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 20-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base

Public Class CostCenterRepository
    Inherits GenericRepository(Of CostCenter)
    Implements ICostCenterRepository, Inject

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un centro de costo especifico
    ''' </summary>
    ''' <param name="code">Codigo del centro de costo</param>
    ''' <returns>Centro de costo</returns>
    ''' <remarks></remarks>
    Public Function GetCostCenter(code As String, Optional tracking As Boolean = True) As CostCenter Implements ICostCenterRepository.GetCostCenter
        Dim objCostCenter As CostCenter
        If tracking = False Then
            objCostCenter = (From e In _context.CostCenter.AsNoTracking()
                             Where e.Code = code
                             Select e).FirstOrDefault()
        Else
            objCostCenter = (From e In _context.CostCenter
                             Where e.Code = code
                             Select e).FirstOrDefault()
        End If

        If objCostCenter Is Nothing Then
            Return New CostCenter
        End If

        If objCostCenter.BranchOfficeId IsNot Nothing Then
            Dim branchOffice = (From d In _context.BranchOffice.AsNoTracking Where d.Id = objCostCenter.BranchOfficeId).FirstOrDefault()
            objCostCenter.BranchOfficeCodeName = String.Format("{0} - {1}", branchOffice.Code, branchOffice.Name)
        End If

        Return objCostCenter
    End Function

    Function GetListCostCenterByCodePOCO(listCode As List(Of String)) As List(Of CostCenter) Implements ICostCenterRepository.GetListCostCenterByCodePOCO
        If listCode Is Nothing OrElse listCode.Count = 0 Then
            Return New List(Of CostCenter)
        End If
        Return (From e In _context.CostCenter.AsNoTracking() Where listCode.Contains(e.Code) Select e).ToList()
    End Function

    Public Function GetCostCenterSimple(code As String) As CostCenter Implements ICostCenterRepository.GetCostCenterSimple
        Dim costCenter As CostCenter = (From e In _context.CostCenter
                                        Where e.Code = code
                                        Select e).FirstOrDefault()

        If costCenter Is Nothing Then
            Return New CostCenter
        End If

        Return costCenter
    End Function

    ''' <summary>
    ''' Lista todos los centros de costos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllCostCenter() As List(Of CostCenter) Implements ICostCenterRepository.ListAllCostCenter
        Dim costCenter = From e In _context.CostCenter
                         Select e
        Return costCenter.ToList()
    End Function

    ''' <summary>
    ''' Centro de costo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterById(id As Integer, tracking As Boolean) As CostCenter Implements ICostCenterRepository.GetCostCenterById
        If tracking = True Then
            Dim busqueda = (From e In _context.CostCenter
                            Where e.Id = id
                            Select e).FirstOrDefault()
            If busqueda IsNot Nothing AndAlso busqueda.Id > 0 Then
                Return busqueda
            Else
                Return New CostCenter()
            End If
        Else
            Dim busqueda = (From e In _context.CostCenter.AsNoTracking()
                            Where e.Id = id
                            Select e).FirstOrDefault()
            If busqueda IsNot Nothing AndAlso busqueda.Id > 0 Then
                Return busqueda
            Else
                Return New CostCenter
            End If
        End If
    End Function
End Class
