'***********************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
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
Public Class FixedAssetSequenceRepository
    Inherits GenericRepository(Of FixedAssetSequence)
    Implements IFixedAssetSequenceRepository

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

#Region "ISequenseFixedAssetRepository"

    ''' <summary>
    ''' <see cref="ISequenseFixedAssetRepository.GetSequenseByIdForm" />
    ''' </summary>
    ''' <param name="idForm"><see cref="ISequenseFixedAssetRepository.GetSequenseByIdForm" /></param>
    ''' <returns><see cref="ISequenseFixedAssetRepository.GetSequenseByIdForm" /></returns>
    Public Function GetSequenseByIdForm(idForm As String) As FixedAssetSequence Implements IFixedAssetSequenceRepository.GetSequenseByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Dim result = (From s As FixedAssetSequence In Me._context.FixedAssetSequence.Include("FixedAssetSequenceDetail.Sequense").Include("FixedAssetSequenceDetail.OperatingUnit") Where s.IdForm.Equals(idForm.Trim()) Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            For Each d In result(0).FixedAssetSequenceDetail
                d.OperatingUnitName = If(d.OperatingUnit IsNot Nothing, d.OperatingUnit.UnitName, String.Empty)
                d.PatternName = If(d.Sequense IsNot Nothing, d.Sequense.Pattern, String.Empty)
            Next
            Return result(0)
        Else
            Return New FixedAssetSequence()
        End If
    End Function

#End Region

End Class
