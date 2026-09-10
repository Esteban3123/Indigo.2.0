'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepositiry
' Author           : Oscar Astudillo Reyes
' Created          : 2024-11-10
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class PortfolioDeteriorationClassificationRepository
    Inherits GenericRepository(Of PortfolioDeteriorationClassification)
    Implements IPortfolioDeteriorationClassificationRepository

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
    ''' Busca un registro de clasificación de deterioro de cartera por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetPortfolioDeteriorationClassificationByCode(code As String) As PortfolioDeteriorationClassification Implements IPortfolioDeteriorationClassificationRepository.GetPortfolioDeteriorationClassificationByCode
        If String.IsNullOrWhiteSpace(code) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As PortfolioDeteriorationClassification In _context.PortfolioDeteriorationClassification.Include("PortfolioDeteriorationClassificationDetails")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault()
        Return res
    End Function


#End Region

End Class
