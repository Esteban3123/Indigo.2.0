'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Faiber Julian Mora D.
' Created          : 28-09-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class InventoryRequestDevolutionRespository
    Inherits GenericRepository(Of InventoryRequestDevolution)
    Implements IInventoryRequestDevolutionRepository

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene la devolución de solicitud por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryRequestDevolutionByCode(code As String) As InventoryRequestDevolution Implements IInventoryRequestDevolutionRepository.GetInventoryRequestDevolutionByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentException("code")
        End If
        Dim res = (From ir In Me._context.InventoryRequestDevolution.Include("InventoryRequestDevolutionDetail") Where ir.Code.Equals(code.Trim()) Select ir).FirstOrDefault()
        If res IsNot Nothing Then
            'Agrega los campos personalizados en el detalle
            If res.InventoryRequestDevolutionDetail IsNot Nothing AndAlso res.InventoryRequestDevolutionDetail.Count > 0 Then
                For Each item As InventoryRequestDevolutionDetail In res.InventoryRequestDevolutionDetail
                    Dim requestDetail = (From rd In _context.InventoryRequestDetail.AsNoTracking.Include("InventoryRequest").AsNoTracking Where rd.Id = item.InventoryRequestDetailId Select rd).FirstOrDefault
                    item.RequestCode = requestDetail.InventoryRequest.Code

                    Dim product = (From p In _context.InventoryProduct.AsNoTracking Where p.Id = requestDetail.InventoryProductId Select p).FirstOrDefault
                    item.ProductoCodeName = product.Code + " - " + product.Name

                    item.RequestType = requestDetail.InventoryRequest.RequestType
                    item.OutstandingQuantity = requestDetail.OutstandingQuantity
                Next
            End If

            res.OriginalValue = (From ir In Me._context.InventoryRequestDevolution.AsNoTracking Where ir.Code.Equals(code.Trim()) Select ir).FirstOrDefault()

            Return res
        Else
            Return New InventoryRequestDevolution
        End If
    End Function

    ''' <summary>
    ''' Obtiene la devolución de solicitud por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryRequestDevolutionById(id As Integer) As InventoryRequestDevolution Implements IInventoryRequestDevolutionRepository.GetInventoryRequestDevolutionById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From ir In Me._context.InventoryRequestDevolution.Include("InventoryRequestDevolutionDetail") Where ir.Id = id
                   Select ir).FirstOrDefault

        If res IsNot Nothing Then
            res.OriginalValue = (From ir In Me._context.InventoryRequestDevolution.AsNoTracking Where ir.Id = id Select ir).FirstOrDefault
            Return res
        Else
            Return New InventoryRequestDevolution
        End If
    End Function

End Class
