'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class SupplierTypeRepository
    Inherits GenericRepository(Of SupplierType)
    Implements ISupplierTypeRepository

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
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierType(code As String, Optional tracking As Boolean = True) As SupplierType Implements ISupplierTypeRepository.GetSupplierType
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As SupplierType In Me._context.SupplierType Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.ParentId IsNot Nothing Then
                Dim supplierType = (From st In _context.SupplierType.AsNoTracking Where st.Id = res.ParentId Select st).FirstOrDefault
                res.SupplierTypeDescription = supplierType.Code + " - " + supplierType.Name
            End If

            res.OriginalValue = (From d As SupplierType In Me._context.SupplierType.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New SupplierType()
        End If
    End Function

    ''' <summary>
    ''' Consulta un tipo de proveedor por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierTypeById(id As String, Optional tracking As Boolean = True) As SupplierType Implements ISupplierTypeRepository.GetSupplierTypeById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.SupplierType Where d.Id = id Select d)
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As SupplierType In Me._context.SupplierType.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New SupplierType()
        End If
    End Function

    ''' <summary>
    ''' Obtiene los tipos de proveedor hijos que tiene asociado el mismo,
    ''' y carga sus respectivos padres
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierTypeBySupplierId(supplierId As Integer) As List(Of SupplierType) Implements ISupplierTypeRepository.GetSupplierTypeBySupplierId
        Dim query = From e In _context.SupplierDetailType.AsNoTracking().Include("SupplierType").AsNoTracking() Where e.SupplierId = supplierId Select e.SupplierType
        Dim listSupplierType As List(Of SupplierType) = query.ToList()
        listSupplierType.ForEach(Sub(item)
                                     item.CodeName = item.Code & " - " & item.Name
                                     item.IsSon = True
                                 End Sub)
        For Each parentId In (From p In listSupplierType Select New With {.ParentId = p.ParentId}).ToList()
            RecursiveSupplierType(parentId.ParentId, listSupplierType)
        Next
        Return listSupplierType
    End Function

    ''' <summary>
    ''' Funcion para traer una lista de tipos de proveedor padres
    ''' </summary>
    ''' <param name="supplierTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecursiveSupplierType(supplierTypeId As Integer, listSupplierType As List(Of SupplierType)) As List(Of SupplierType)
        If Not listSupplierType.Exists(Function(x)
                                           If (x.Id = supplierTypeId) Then
                                               Return True
                                           Else
                                               Return False
                                           End If
                                       End Function) Then
            Dim query = From e In _context.SupplierType.AsNoTracking() Where e.Id = supplierTypeId Select e
            Dim supplierType As SupplierType = query.SingleOrDefault()
            supplierType.CodeName = supplierType.Code & " - " & supplierType.Name
            listSupplierType.Add(supplierType)
            If supplierType.ParentId IsNot Nothing Then
                RecursiveSupplierType(supplierType.ParentId, listSupplierType)
            Else
                Return listSupplierType
            End If
        Else
            Return listSupplierType
        End If
        Return listSupplierType
    End Function

    ''' <summary>
    ''' Obtiene todos los hijos de un tipo de proveedor
    ''' </summary>
    Public Function GetAllSupplierTypeIdListByParentId(supplierTypeId As Integer) As List(Of Integer) Implements ISupplierTypeRepository.GetAllSupplierTypeIdListByParentId
        Return (From st In _context.SupplierType.AsNoTracking() Where st.ParentId = supplierTypeId Select st.Id).ToList()
    End Function

End Class
