'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance
Imports Domain.Base.Entities
Imports Domain.Base

#End Region


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la entidad sucursal
''' </summary>
''' <remarks></remarks>

Public Class BranchRepository
    Inherits GenericRepository(Of Branch)
    Implements IBranchRepository






    'Devuelve el contexto en este repositorio 
    Private _context As IMaintenanceModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IMaintenanceModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub



    Public Function GetBranch(codeBranch As String, Optional Tracking As Boolean = False) As Branch Implements IBranchRepository.GetBranch
        If Tracking = False Then
            Dim Busqueda = From e In _context.Branch
                      Where e.Code = codeBranch
                      Select e

            If Busqueda.Count = 0 Then
                Return New Branch
            Else
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.Branch.AsNoTracking
                      Where e.Code = codeBranch
                      Select e

            If Busqueda.Count = 0 Then
                Return New Branch
            Else
                Return Busqueda.Single
            End If
        End If
        

    End Function

    Public Function ListAllBranch() As List(Of Branch) Implements IBranchRepository.ListAllBranch


        Dim Busqueda = From e In _context.Branch
                      Select e

        Return Busqueda.ToList
    End Function

    Public Function SaveBranch(Branch As Branch) As Boolean Implements IBranchRepository.SaveBranch
        _context.Branch.ApplyChanges(Branch)
        Return True
    End Function

    Public Function ListAllCostCenter() As List(Of CostCenter) Implements IBranchRepository.ListAllCostCenter
        Dim Busqueda = From e In _context.CostCenter
                       Where e.State = True
                      Select e

        Return Busqueda.ToList
    End Function
End Class
