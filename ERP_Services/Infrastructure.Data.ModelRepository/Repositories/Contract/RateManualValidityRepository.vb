#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

#End Region

Public Class RateManualValidityRepository
    Inherits GenericRepository(Of RateManualValidity)
    Implements IRateManualValidityRepository

#Region "Builder"

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicializa el contexto
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetRateManualValidityById(id As Integer) As RateManualValidity Implements IRateManualValidityRepository.GetRateManualValidityById
        Dim rateManualValidity = (From rmv In Me._context.RateManualValidity Where rmv.Id = id Select rmv).FirstOrDefault()
        If rateManualValidity Is Nothing Then
            Return New RateManualValidity
        End If
        rateManualValidity.OriginalValue = (From rmv In Me._context.RateManualValidity.AsNoTracking() Where rmv.Id = id Select rmv).FirstOrDefault()
        Return rateManualValidity
    End Function

    Public Function GetRateManualValidityByIdWithAggregates(id As Integer) As RateManualValidity Implements IRateManualValidityRepository.GetRateManualValidityByIdWithAggregates
        Dim rateManualValidity = (From rmv In Me._context.RateManualValidity.AsNoTracking().Include("RateManualValidityDetail").AsNoTracking() Where rmv.Id = id Select rmv).FirstOrDefault()
        If rateManualValidity Is Nothing Then
            Return New RateManualValidity
        End If
        Return rateManualValidity
    End Function

    Public Function GetRateManualValidity(code As String) As RateManualValidity Implements IRateManualValidityRepository.GetRateManualValidity
        Dim rateManualValidity = (From rmv In Me._context.RateManualValidity.Include("RateManualValidityDetail") Where rmv.Code = code Select rmv).FirstOrDefault()
        If rateManualValidity Is Nothing Then
            Return New RateManualValidity
        End If

        For Each detail In rateManualValidity.RateManualValidityDetail
            detail.RateManualDescription = (From p In _context.RateManual.AsNoTracking Where detail.RateManualId = p.Id Select p.Code + " - " + p.Name).FirstOrDefault
        Next

        rateManualValidity.OriginalValue = (From rmv In Me._context.RateManualValidity.AsNoTracking() Where rmv.Id = rateManualValidity.Id Select rmv).FirstOrDefault()
        Return rateManualValidity
    End Function

#End Region

End Class
