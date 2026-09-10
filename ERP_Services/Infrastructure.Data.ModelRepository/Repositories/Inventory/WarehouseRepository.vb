'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Configuration
Imports Infrastructure.CrossCutting.Base
Imports System.Data.SqlClient
Imports System.Data.Entity

Public Class WarehouseRepository
    Inherits GenericRepository(Of Warehouse)
    Implements IWarehouseRepository

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
    ''' Obtiene un almacen por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWarehouse(code As String, Optional tracking As Boolean = True) As Warehouse Implements IWarehouseRepository.GetWarehouse
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res As Warehouse
        If tracking = True Then
            res = (From d As Warehouse In Me._context.Warehouse.Include("WarehouseUser").Include("WarehouseUserRequest")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        Else
            res = (From d As Warehouse In Me._context.Warehouse.AsNoTracking().Include("WarehouseUser").Include("WarehouseUserRequest").AsNoTracking()
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        End If
        If res IsNot Nothing Then

            Dim accountDebit = (From ad In _context.MainAccounts.AsNoTracking Where ad.Id = res.LoanThirdPartyDebitAccountId Select ad).FirstOrDefault
            res.MainAccountThirdPartyDebitDescription = accountDebit.Number + " - " + accountDebit.Name

            Dim accountCredit = (From ac In _context.MainAccounts.AsNoTracking Where ac.Id = res.LoanThirdPartyCreditAccountId Select ac).FirstOrDefault
            res.MainAccountThirdPartyCreditDescription = accountCredit.Number + " - " + accountCredit.Name

            Dim costCenter = (From cc In _context.CostCenter.AsNoTracking Where cc.Id = res.CostCenterId Select cc).FirstOrDefault
            res.CostCenterDescription = costCenter.Code + " - " + costCenter.Name

            Dim supplier = (From s In _context.Supplier.AsNoTracking Where s.Id = res.SupplierId Select s).FirstOrDefault
            res.CodeNameSupplier = supplier.Code + " - " + supplier.Name

            'se realiza if para interfazar con nuevo campo unico para el tipo de almacen
            Select Case True
                Case res.VirtualStore
                    res.WareHouseType = 1
                Case res.WarehouseConsignment
                    res.WareHouseType = 2
                Case res.CustodyStore
                    res.WareHouseType = 3
                Case res.TransitStore
                    res.WareHouseType = 4
                Case res.ControlStore
                    res.WareHouseType = 5
            End Select

            res.OriginalValue = (From g In _context.Warehouse.AsNoTracking.Include("WarehouseUser").Include("WarehouseUserRequest").AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New Warehouse()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un almacen por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete>
    Public Function GetWarehouseById(id As Integer) As Warehouse Implements IWarehouseRepository.GetWarehouseById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.Warehouse.Include("WarehouseUser") Where d.Id = id Select d).FirstOrDefault()
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From d As Warehouse In Me._context.Warehouse.AsNoTracking.Include("WarehouseUser").AsNoTracking Where d.Id = id Select d).SingleOrDefault()
            Return res
        Else
            Return New Warehouse()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un almacen por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetWarehouseByIdAsync(id As Integer) As Task(Of Warehouse) Implements IWarehouseRepository.GetWarehouseByIdAsync
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = Await (From d In Me._context.Warehouse.Include("WarehouseUser")
                         Where d.Id = id
                         Select d).FirstOrDefaultAsync()
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From d As Warehouse In Me._context.Warehouse.AsNoTracking.Include("WarehouseUser").AsNoTracking Where d.Id = id Select d).SingleOrDefault()
            Return res
        Else
            Return New Warehouse()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un almacen por id
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteConditionsByWarehouseId(warehouseId As Integer) Implements IWarehouseRepository.DeleteConditionsByWarehouseId
        If warehouseId > 0 Then
            Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
            ''Se eliminan primero las condiciones
            Dim queryString As String = " Delete From Inventory.WarehouseRestrictedConditions WHERE WarehouseId = " & warehouseId & " "
            Using connection As New SqlConnection(connectionString)
                Dim Command As New SqlCommand(queryString, connection)
                Command.Connection.Open()
                Command.ExecuteNonQuery()
            End Using
        End If
    End Function

    Public Function ListPrefixs() As List(Of String) Implements IWarehouseRepository.ListPrefixs
        Return (From b As Warehouse In Me._context.Warehouse Select b.Prefix).Distinct().ToList()
    End Function

    ''' <summary>
    ''' Obtiene la bodega de un proveedor por tipo
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Function GetWarehouseSupplierByType(supplierId As Integer, type As Integer, Optional costCenterId As Integer? = Nothing) As Warehouse Implements IWarehouseRepository.GetWarehouseSupplierByType
        Dim res As Warehouse = Nothing

        If supplierId = 0 Then
            Throw New ArgumentNullException("supplierId")
        End If

        If costCenterId IsNot Nothing Then
            res = (From d In Me._context.Warehouse.AsNoTracking.Include("WarehouseUser").AsNoTracking Where d.CostCenterId = costCenterId AndAlso d.SupplierId = supplierId AndAlso d.WareHouseType = type AndAlso d.Status = True Select d).FirstOrDefault()
        Else
            res = (From d In Me._context.Warehouse.AsNoTracking.Include("WarehouseUser").AsNoTracking Where d.SupplierId = supplierId AndAlso d.WareHouseType = type AndAlso d.Status = True Select d).FirstOrDefault()
        End If

        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return New Warehouse()
        End If
    End Function
End Class
