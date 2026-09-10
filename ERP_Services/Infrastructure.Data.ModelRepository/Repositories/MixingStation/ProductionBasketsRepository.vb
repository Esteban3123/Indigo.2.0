'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class ProductionBasketsRepository
    Inherits GenericRepository(Of ProductionBaskets)
    Implements IProductionBasketsRepository, Inject

    ''' <summary>
    ''' Contexto de Package
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Package
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetProductionBaskets(code As String, Optional tracking As Boolean = True) As ProductionBaskets Implements IProductionBasketsRepository.GetProductionBaskets
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim productionBaskets As ProductionBaskets = Nothing

        If tracking Then
            productionBaskets = (From e In _context.ProductionBaskets.Include("ProductionBasketsDetail")
                                 Where e.Code = code
                                 Select e).FirstOrDefault()
        Else
            productionBaskets = (From e In _context.ProductionBaskets.AsNoTracking().Include("ProductionBasketsDetail").AsNoTracking()
                                 Where e.Code = code
                                 Select e).FirstOrDefault()
        End If

        If productionBaskets IsNot Nothing Then
            If productionBaskets.ProductionBasketsDetail IsNot Nothing AndAlso productionBaskets.ProductionBasketsDetail.Count > 0 Then
                For Each itemDetail In productionBaskets.ProductionBasketsDetail
                    Select Case itemDetail.ComponentType
                        Case 1
                            itemDetail.ComponentTypeName = "Medicamento"
                        Case 2
                            itemDetail.ComponentTypeName = "Insumo"
                        Case 3
                            itemDetail.ComponentTypeName = "Producto"
                    End Select

                    If itemDetail.AtcId IsNot Nothing Then
                        itemDetail.SourceCodeName = (From x In _context.ATC.AsNoTracking Where x.Id = itemDetail.AtcId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
                    ElseIf itemDetail.ProductId IsNot Nothing Then
                        itemDetail.SourceCodeName = (From x In _context.InventoryProduct.AsNoTracking Where x.Id = itemDetail.ProductId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
                    Else
                        itemDetail.SourceCodeName = (From x In _context.InventorySupplie.AsNoTracking Where x.Id = itemDetail.SupplieId Select String.Concat(x.Code, " - ", x.SupplieName)).FirstOrDefault()
                    End If

                    itemDetail.MeasureUnitDescription = (From x In _context.InventoryMeasurementUnit.AsNoTracking Where x.Id = itemDetail.MeasurementUnitId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
                Next
            End If

            Return productionBaskets
        Else
            Return New ProductionBaskets()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetProductionBasketsById(id As Integer?) As ProductionBaskets Implements IProductionBasketsRepository.GetProductionBasketsById
        Return (From bg In _context.ProductionBaskets.Include("ProductionBasketsDetail").AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()

    End Function

End Class