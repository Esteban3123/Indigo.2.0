'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class SuppliersDetailTypeRepository
    Inherits GenericRepository(Of SupplierDetailType)
    Implements ISuppliersDetailTypeRepository

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
    ''' Obtiene los tipo de proveedor del proveedor
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuppliersDetailTypeById(id As Integer, Optional tracking As Boolean = True) As SupplierDetailType Implements ISuppliersDetailTypeRepository.GetSuppliersDetailTypeById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.SupplierDetailType Where d.Id = id Select d)
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d In Me._context.SupplierDetailType Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New SupplierDetailType()
        End If
    End Function


    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuppliersDetailTypeByIdSupplier(id As Integer, Optional tracking As Boolean = True) As List(Of SupplierDetailType) Implements ISuppliersDetailTypeRepository.GetSuppliersDetailTypeByIdSupplier
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim list = (From dl In Me._context.SupplierDetailType Where dl.SupplierId = id Select dl).ToList

        If list IsNot Nothing AndAlso list.Count > 0 Then
            For Each item As SupplierDetailType In list
                Dim supplierType = (From st In _context.SupplierType.AsNoTracking Where st.Id = item.SupplierTypeId Select st).FirstOrDefault
                item.SupplierTypeDescription = supplierType.Code + " - " + supplierType.Name
            Next
        End If

        Return list
    End Function

End Class
