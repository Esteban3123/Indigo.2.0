'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class MarketingUnitRepository
    Inherits GenericRepository(Of MarketingUnit)
    Implements IMarketingUnitRepository

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
    ''' Obtiene una unidad de mercadeo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMarketingUnit(code As String) As MarketingUnit Implements IMarketingUnitRepository.GetMarketingUnit
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As MarketingUnit In Me._context.MarketingUnit.Include("MarketingUnitCups")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            For Each itemDetail As MarketingUnitCups In res.MarketingUnitCups
                Dim cupsEntity = (From ce In _context.CupsEntity.AsNoTracking Where ce.Id = itemDetail.CupsEntityId Select ce).FirstOrDefault
                itemDetail.CupsEntityCode = cupsEntity.Code
                itemDetail.CupsEntityDescription = cupsEntity.Description
            Next

            res.OriginalValue = (From g In _context.MarketingUnit.AsNoTracking.Include("MarketingUnitCups").AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault

            Return res
        Else
            Return New MarketingUnit()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una unidad de mercadeo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMarketingUnitById(id As Integer) As MarketingUnit Implements IMarketingUnitRepository.GetMarketingUnitById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.MarketingUnit.Include("MarketingUnitCups") Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As MarketingUnit In Me._context.MarketingUnit.AsNoTracking().Include("MarketingUnitCups").AsNoTracking Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New MarketingUnit()
        End If
    End Function
    
End Class
