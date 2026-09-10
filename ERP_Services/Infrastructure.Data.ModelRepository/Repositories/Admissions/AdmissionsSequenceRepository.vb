'***********************************************************************
' Assembly         : Infrastructure.Data.BillingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/02/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class AdmissionsSequenceRepository
    Inherits GenericRepository(Of AdmissionsSequence)
    Implements IAdmissionsSequenceRepository

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
    ''' <param name="Context">Contexto de cintabilidad</param>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        Me._context = context
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
    Public Function GetSequenseByIdForm(idForm As String) As AdmissionsSequence Implements IAdmissionsSequenceRepository.GetSequenseByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Dim result = (From s As AdmissionsSequence In Me._context.AdmissionsSequence.Include("AdmissionsSequenceDetail.Sequense").Include("AdmissionsSequenceDetail.OperatingUnit") Where s.IdForm.Equals(idForm.Trim()) Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            For Each d In result(0).AdmissionsSequenceDetail
                d.OperatingUnitName = If(d.OperatingUnit IsNot Nothing, d.OperatingUnit.UnitName, String.Empty)
                d.PatternName = If(d.Sequense IsNot Nothing, d.Sequense.Pattern, String.Empty)
            Next
            Return result(0)
        Else
            Return New AdmissionsSequence()
        End If
    End Function

#End Region

End Class
