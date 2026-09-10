'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Diego A. Roldán L.
' Created          : 2023-03-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class RequestParamRepository
    Inherits GenericRepository(Of RequestParam)
    Implements IRequestParamRepository, Inject

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
    ''' Get by code
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetRequestParamByCode(code As String) As RequestParam Implements IRequestParamRepository.GetRequestParamByCode
        Dim query = _context.RequestParam _
            .Include(Function(m) m.RequestParamProduct) _
            .Include(Function(m) m.RequestParamFuncionalUnit) _
            .Include(Function(m) m.RequestParamDetailPeriodicity) _
            .Where(Function(m) m.Code = code) _
            .FirstOrDefault()

        If query IsNot Nothing Then
            If query.RequestParamProduct.Any() Then

                For Each item In query.RequestParamProduct
                    If item.Type = 1 Then
                        Dim supplie = _context.InventorySupplie.AsNoTracking().FirstOrDefault(Function(m) m.Id = item.SupplieId.Value)
                        If supplie IsNot Nothing Then
                            item.ProductCodeName = $"{supplie.Code} - {supplie.SupplieName}"
                        End If
                    ElseIf item.Type = 2 Then
                        Dim product = _context.InventoryProduct.AsNoTracking().FirstOrDefault(Function(m) m.Id = item.ProductId.Value)
                        If product IsNot Nothing Then
                            item.ProductCodeName = $"{product.Code} - {product.Name}"
                        End If
                    End If
                Next
            End If
        End If

        Return query
    End Function

End Class
