'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Oscar stiven Astudillo reyes
' Created          : 2023-02-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.Data.SecurityRepository

Public Class ConsignmentCostListRepository
    Inherits GenericRepository(Of ConsignmentCostList)
    Implements IConsignmentCostListRepository

    Private _context As IGlobalModelUnitOfWork
    Private _contextSecurity As ISeguridadUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork, ByVal ContextSecurity As ISeguridadUnitOfWork)
        MyBase.New(Context)
        _context = Context
        _contextSecurity = ContextSecurity
    End Sub
    ''' <summary>
    ''' Obtiene una registro por proveedor incluyendo la unidad operativa
    ''' </summary>
    Public Function GetConsignmentCostListBySupplierId(SupplierId As Integer, OperatingUnitId As Integer) As ConsignmentCostList Implements IConsignmentCostListRepository.GetConsignmentCostListBySupplierId
        If SupplierId = 0 Or OperatingUnitId = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From p In _context.ConsignmentCostList.Include("ConsignmentCostListDetail.InventoryProduct.ProductType").Include("ConsignmentCostListDetail.ConsignmentCostListDetailRecord")
                     Where p.SupplierId.Equals(SupplierId) AndAlso p.OperatingUnit.Id.Equals(OperatingUnitId)
                     Select p).FirstOrDefault()
        If query IsNot Nothing Then
            For Each detail In query.ConsignmentCostListDetail
                If detail.InventoryProduct IsNot Nothing Then
                    detail.ProductCodeName = detail.InventoryProduct.Code & " - " & detail.InventoryProduct.Name
                    detail.ProductType = IIf(detail.InventoryProduct.ProductType.Class = 2, "Producto", "Insumo")
                End If
                If detail.ConsignmentCostListDetailRecord IsNot Nothing Then
                    detail.ConsignmentCostListDetailRecord.ToList.ForEach(Sub(x)
                                                                              x.CodeNameUser = (From u In _contextSecurity.User.Include("Person")
                                                                                                Where u.UserCode = x.CreationUser
                                                                                                Select String.Concat(u.UserCode, " - ", u.Person.Fullname)).FirstOrDefault()
                                                                          End Sub)
                End If
            Next
            Return query
        Else
            Return Nothing
        End If
    End Function

    Public Function GetConsignmentCostListById(id As Integer) As ConsignmentCostList Implements IConsignmentCostListRepository.GetConsignmentCostListById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = _context.ConsignmentCostList.AsNoTracking().
                 FirstOrDefault(Function(d) d.Id = id)

        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return Nothing
        End If
    End Function


    ''' <summary>
    ''' Obtiene una detalle por Id 
    ''' </summary>
    Public Function GetDetailById(id As Integer) As ConsignmentCostListDetail Implements IConsignmentCostListRepository.GetDetailById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = _context.ConsignmentCostListDetail.AsNoTracking().
                 FirstOrDefault(Function(d) d.Id = id)

        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    '''  Guarda una tarifa de productos y servicios
    ''' </summary>
    ''' <param name="EntityXml"></param>
    ''' <returns></returns>
    Function SetConsignmentCostListDetailFromFile(xmlObject As String) As List(Of SP_CopyAndPasteConsignmentCostListDetail_Result) Implements IConsignmentCostListRepository.SetConsignmentCostListDetailFromFile
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteConsignmentCostListDetail(xmlObject).ToList()
    End Function

    Public Function DeleteDetail(detail As ConsignmentCostListDetail) As ConsignmentCostList Implements IConsignmentCostListRepository.DeleteDetail
        _context.ConsignmentCostListDetail.Remove(detail)
    End Function


    ''' <summary>
    ''' Obtiene el resultado del  comprobante contable 
    ''' </summary>
    ''' <param name="XMLConsignmentCostList"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SaveJournalVoucherConsignmentCostList(XMLConsignmentCostList As String, CodeUser As String) As SP_GenerateJournalVoucherByConsignmentCostListDetail_Result Implements IConsignmentCostListRepository.SaveJournalVoucherConsignmentCostList
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateJournalVoucherByConsignmentCostListDetail(XMLConsignmentCostList, CodeUser).FirstOrDefault()
    End Function


End Class
