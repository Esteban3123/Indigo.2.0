'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepositiry
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region


Public Class ProvisionRangesRepository
    Inherits GenericRepository(Of ProvisionRanges)
    Implements IProvisionRangesRepository

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
    ''' Metodo para obtener rago de provision por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetProvisionRangesByCode(code As String, Optional tracking As Boolean = True) As Object Implements IProvisionRangesRepository.GetProvisionRangesByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        If tracking = False Then
            Dim res = (From d As ProvisionRanges In _context.ProvisionRanges.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).ToList()
            If res IsNot Nothing Then
                res(0).OriginalValue = (From d As ProvisionRanges In _context.ProvisionRanges.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New ProvisionRanges()
            End If
        Else
            Dim res = (From d As ProvisionRanges In _context.ProvisionRanges Where d.Code.Equals(code.Trim()) Select d).ToList()
            If res IsNot Nothing Then
                res(0).OriginalValue = (From d As ProvisionRanges In _context.ProvisionRanges Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New ProvisionRanges()
            End If
        End If
    End Function
#End Region
    
End Class
