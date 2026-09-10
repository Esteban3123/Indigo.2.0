'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class RateManualDetailRepository
    Inherits GenericRepository(Of RateManualDetail)
    Implements IRateManualDetailRepository


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
    ''' Obtiene un detalle de manual tarifario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRateManualDetailById(id As Integer) As RateManualDetail Implements IRateManualDetailRepository.GetRateManualDetailById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From rmd In Me._context.RateManualDetail.AsNoTracking().Include("RateManual").AsNoTracking() Where rmd.Id = id Select rmd).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From rmd As RateManualDetail In Me._context.RateManualDetail.AsNoTracking() Where rmd.Id = id Select rmd).FirstOrDefault
            Return res
        Else
            Return New RateManualDetail()
        End If
    End Function

    

    ''' <summary>
    ''' metodo para obtener un detalla del reta
    ''' </summary>
    ''' <param name="rateManualId"></param>
    ''' <param name="ipsServiceId"></param>
    ''' <returns></returns>
    Public Function GetRateManualDetailServiceOrder(rateManualId As Integer, ipsServiceId As Integer) As RateManualDetail Implements IRateManualDetailRepository.GetRateManualDetailServiceOrder
        Return (From rmd In _context.RateManualDetail.Include("RateManual").AsNoTracking() Where rmd.RateManualId = rateManualId And rmd.IPSServiceId = ipsServiceId Select rmd).FirstOrDefault()
    End Function
End Class
