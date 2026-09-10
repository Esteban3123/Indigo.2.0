'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-10
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class RawMaterialDevolutionDetailRepository
    Inherits GenericRepository(Of RawMaterialDevolutionDetail)
    Implements IRawMaterialDevolutionDetailRepository, Inject

    ''' <summary>
    ''' Contexto de Tipo de dosis unitaria
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Tipo de Dosis Unitaria
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene los detalles de devolucion de materia prima
    ''' </summary>
    ''' <param name="rawMatertialDevolutionId"></param>
    ''' <returns></returns>
    Public Function GetRawMaterialDevolutionDetailByRawMaterialDevolutionId(rawMatertialDevolutionId As Integer, Optional tracking As Boolean = True) As List(Of RawMaterialDevolutionDetail) Implements IRawMaterialDevolutionDetailRepository.GetRawMaterialDevolutionDetailByRawMaterialDevolutionId
        Dim query = (From rmd In _context.RawMaterialDevolutionDetail _
                         .Include("CampaignDetailValidation.InventoryProduct") _
                         .Include("CampaignDetailValidation.BatchSerial") _
                         .Include("RawMaterialDevolution")
                     Where rmd.RawMaterialDevolutionId = rawMatertialDevolutionId Select rmd)

        If Not tracking Then query = query.AsNoTracking()

        Dim data = query.ToList()

        If data.Any() Then
            For Each item In data
                Dim validation = item.CampaignDetailValidation

                If validation IsNot Nothing Then
                    item.ProductId = validation.ProductId
                    item.ProductCodeName = $"{validation.InventoryProduct.Code} - {validation.InventoryProduct.Name}"
                    item.BatchSerialId = validation.BatchSerialId
                    item.BatchSerialCode = validation.BatchSerial.BatchCode
                    item.DeliveredQuantity = validation.DeliveredQuantity
                    item.DevolutionQuantity = validation.DevolutionQuantity
                End If
            Next
        End If

        Return data
    End Function
End Class
