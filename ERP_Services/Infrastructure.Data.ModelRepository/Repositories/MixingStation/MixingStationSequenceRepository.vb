'***********************************************************************
' Assembly         : Infrastructure.Data.MixingStationRepository
' Author           : Yoe Andres Cardenas
' Created          : 24-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Repositorio de la entidad secuencia numerica
''' </summary>
Public Class MixingStationSequenceRepository
    Inherits GenericRepository(Of MixingStationSequence)
    Implements IMixingStationSequenceRepository, Inject

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

#Region "IMixingStationSequenceRepository"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idForm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSequenceByIdForm(idForm As String) As MixingStationSequence Implements IMixingStationSequenceRepository.GetSequenceByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Dim result = (From s As MixingStationSequence In Me._context.MixingStationSequence.Include("MixingStationSequenceDetail.Sequense").Include("MixingStationSequenceDetail.OperatingUnit") Where s.IdForm.Equals(idForm.Trim()) Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            For Each d In result(0).MixingStationSequenceDetail
                d.OperatingUnitName = If(d.OperatingUnit IsNot Nothing, d.OperatingUnit.UnitName, String.Empty)
                d.PatternName = If(d.Sequense IsNot Nothing, d.Sequense.Pattern, String.Empty)
            Next
            Return result(0)
        Else
            Return New MixingStationSequence()
        End If
    End Function

#End Region

End Class
