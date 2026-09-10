'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepositiry
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region


Public Class EconomicIndicatorRepository
    Inherits GenericRepository(Of EconomicIndicator)
    Implements IEconomicIndicatorRepository


    'contexto de cartera
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo para listar todos los
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllEconomicIndicator() As List(Of EconomicIndicator) Implements IEconomicIndicatorRepository.GetAllEconomicIndicator
        Return (From ei In _context.EconomicIndicator Select ei).ToList()
    End Function

  
    ''' <summary>
    ''' Metodo para obtener un indicador economico por año y mes
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetEconomicIndicator(year As String, month As String, Optional tracking As Boolean = True) As EconomicIndicator Implements IEconomicIndicatorRepository.GetEconomicIndicator
        Dim EconomicIndicator = From ei In _context.EconomicIndicator Where ei.Year = year And ei.Month = month Select ei
        If EconomicIndicator.Count() > 0 Then
            Dim objEconomicIndicator = Nothing
            If tracking = False Then
                objEconomicIndicator = (From ei In _context.EconomicIndicator.AsNoTracking() Where ei.Year = year And ei.Month = month Select ei).SingleOrDefault()
            Else
                objEconomicIndicator = EconomicIndicator.SingleOrDefault()
            End If
            Return objEconomicIndicator
        Else
            Return New EconomicIndicator()
        End If
    End Function

    ''' <summary>
    ''' Metodo para obtener un indicador economico por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetEconomicIndicatorByCode(code As String, Optional tracking As Boolean = True) As EconomicIndicator Implements IEconomicIndicatorRepository.GetEconomicIndicatorByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        If tracking = False Then
            Dim res = (From d As EconomicIndicator In _context.EconomicIndicator.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).ToList()
            If res IsNot Nothing Then
                res(0).OriginalValue = (From d As EconomicIndicator In _context.EconomicIndicator.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New EconomicIndicator()
            End If
        Else
            Dim res = (From d As EconomicIndicator In _context.EconomicIndicator Where d.Code.Equals(code.Trim()) Select d).ToList()
            If res IsNot Nothing Then
                res(0).OriginalValue = (From d As EconomicIndicator In _context.EconomicIndicator Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New EconomicIndicator()
            End If
        End If
    End Function
#End Region
    
End Class
