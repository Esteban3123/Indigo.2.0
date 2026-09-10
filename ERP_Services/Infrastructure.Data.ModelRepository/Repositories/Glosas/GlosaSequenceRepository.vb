#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class GlosaSequenceRepository
    Inherits GenericRepository(Of GlosaSequence)
    Implements IGlosaSequenceRepository

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

#Region "Methods"


    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>
    ''' Secuencia numerica asignada al frontal
    ''' </returns>
    ''' <exception cref="System.ArgumentNullException">idForm</exception>
    Public Function GetSequenseByIdForm(idForm As String) As GlosaSequence Implements IGlosaSequenceRepository.GetSequenseByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Dim result = (From s As GlosaSequence In Me._context.GlosaSequence.Include("GlosaSequenceDetail.Sequense").Include("GlosaSequenceDetail.OperatingUnit") Where s.IdForm.Equals(idForm.Trim()) Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            For Each d In result(0).GlosaSequenceDetail
                d.OperatingUnitName = If(d.OperatingUnit IsNot Nothing, d.OperatingUnit.UnitName, String.Empty)
                d.PatternName = If(d.Sequense IsNot Nothing, d.Sequense.Pattern, String.Empty)
            Next
            Return result(0)
        Else
            Return New GlosaSequence()
        End If
    End Function

#End Region

End Class
