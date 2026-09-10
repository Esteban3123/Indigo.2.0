'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Daniel Eduardo Arévalo
' Created          : 28-10-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance
Imports Domain.Base.Entities
Imports Domain.Base
Imports Infrastructure.Data.ModelRepository

#End Region

Public Class SupplierMaintenanceRepository
    Inherits GenericRepository(Of SupplierMaintenance)
    Implements ISupplierMaintenanceRepository


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

    ''' <summary>
    ''' Obtiene un proveedor (Mantenimiento) por codigo
    ''' </summary>
    ''' <param name="Nit">Nit del Proveedor</param>
    ''' <returns>SupplierMaintenance</returns>
    ''' <remarks></remarks>
    Public Function GetSupplier(Nit As String, Optional tracking As Boolean = True) As SupplierMaintenance Implements ISupplierMaintenanceRepository.GetSupplier
        If Nit Is Nothing OrElse Nit.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Nit")
        End If
        If tracking = True Then
            Dim query = From e In _context.SupplierMaintenance.Include("AddressMaintenance").Include("PhoneMaintenance").Include("EmailMaintenance")
                    Where e.Nit = Nit
                    Select e
            If query.ToList().Count > 0 Then
                Return query.FirstOrDefault()
            Else
                Return New SupplierMaintenance()
            End If

           
        Else
            Dim query = From e In _context.SupplierMaintenance.AsNoTracking
                    Where e.Nit = Nit
                    Select e
            If query.Count > 0 Then
                Return query.FirstOrDefault()
            Else
                Return New SupplierMaintenance()
            End If
        End If
    End Function

    ''' <summary>
    ''' Obtiene un proveedor (Mantenimiento) por su id
    ''' </summary>
    ''' <param name="Id">Id del Proveedor</param>
    ''' <param name="tracking"></param>
    ''' <returns>SupplierMaintenance</returns>
    ''' <remarks></remarks>
    Public Function GetSupplierById(Id As Integer, Optional tracking As Boolean = True) As SupplierMaintenance Implements ISupplierMaintenanceRepository.GetSupplierById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.SupplierMaintenance.Include("AddressMaintenance").Include("PhoneMaintenance").Include("EmailMaintenance").AsNoTracking() Where d.Id = Id Select d)
        If res.Count > 0 Then
            Return res.SingleOrDefault
        Else
            Return New SupplierMaintenance()
        End If
    End Function

    ''' <summary>
    ''' Lsita todos los Proveedores de Mantenimiento
    ''' </summary>
    ''' <returns>List(Of SupplierMaintenance)</returns>
    ''' <remarks></remarks>
    Public Function ListAllSupplier() As List(Of SupplierMaintenance) Implements ISupplierMaintenanceRepository.ListAllSupplier
        Dim Busqueda = From e In _context.SupplierMaintenance.Include("AddressMaintenance").Include("PhoneMaintenance").Include("EmailMaintenance")
                     Select e


        Return Busqueda.ToList
    End Function

   
End Class
