'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepository
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio de la entidad secuencia numerica
''' </summary>
Public Class SequensePortfolioCRepository
    Inherits GenericRepository(Of PortfolioSequence)
    Implements ISequensePortfolioCRepository

#Region "Fields"

    ''' <summary>
    ''' Contexto de contabilidad
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="acountingContext">Contexto de cintabilidad</param>
    Public Sub New(ByVal acountingContext As IGlobalModelUnitOfWork)
        MyBase.New(acountingContext)
        Me._context = acountingContext
    End Sub

#End Region

#Region "ISequensePortfolioRepository"

    ''' <summary>
    ''' <see cref="ISequensePortfolioRepository.GetSequenseByIdForm" />
    ''' </summary>
    ''' <param name="idForm"><see cref="ISequensePortfolioRepository.GetSequenseByIdForm" /></param>
    ''' <returns><see cref="ISequensePortfolioRepository.GetSequenseByIdForm" /></returns>
    Public Function GetSequenseByIdForm(idForm As String) As PortfolioSequence Implements ISequensePortfolioCRepository.GetSequenseByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Dim result = (From s In Me._context.PortfolioSequence.Include("PortfolioSequenceDetail.Sequense").Include("PortfolioSequenceDetail.OperatingUnit") Where s.IdForm.Equals(idForm.Trim()) Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            For Each d In result(0).PortfolioSequenceDetail
                d.OperatingUnitName = If(d.OperatingUnit IsNot Nothing, d.OperatingUnit.UnitName, String.Empty)
                d.PatternName = If(d.Sequense IsNot Nothing, d.Sequense.Pattern, String.Empty)
            Next
            Return result(0)
        Else
            Return New PortfolioSequence()
        End If
    End Function

#End Region

End Class
