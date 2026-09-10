'************************************************************
' Assembly         : Infrastructure.Data.MaintenanceRepository
' Author           : Oscar Ivan Sierra
' Created          : 02-04-2014
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

Public Class BrandRepository
    Inherits GenericRepository(Of Brand)
    Implements IBrandRepository



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




  

    Public Function GetBrand(codeBrand As String) As Brand Implements IBrandRepository.GetBrand
        Dim Busqueda = From e In _context.Brand
                   Where e.Code = codeBrand
                   Select e
        If Busqueda.Count = 0 Then
            Return New Brand
        Else
            Busqueda.Single.StartTracking()
            Return Busqueda.Single
        End If
    End Function

    Public Function ListAllBrand() As List(Of Brand) Implements IBrandRepository.ListAllBrand
        Dim Busqueda = From e In _context.Brand
              Select e

        Return Busqueda.ToList
    End Function

    Public Function SaveBrand(Brand As Brand) As Boolean Implements IBrandRepository.SaveBrand
        _context.Brand.ApplyChanges(Brand)
        Return True
    End Function
End Class
