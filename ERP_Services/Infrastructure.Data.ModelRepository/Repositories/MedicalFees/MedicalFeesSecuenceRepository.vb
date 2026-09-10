'***********************************************************************
' Assembly         : Infrastructure.Data.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class MedicalFeesSecuenceRepository
    Inherits GenericRepository(Of MedicalFeesSecuence)
    Implements IMedicalFeesSecuenceRepository

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
    ''' 
    ''' </summary>
    ''' <param name="idForm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSequenseByIdForm(idForm As String) As MedicalFeesSecuence Implements IMedicalFeesSecuenceRepository.GetSequenseByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Dim result = (From s As MedicalFeesSecuence In Me._context.MedicalFeesSecuence.Include("MedicalFeesSecuenceDetail.Sequense").Include("MedicalFeesSecuenceDetail.OperatingUnit") Where s.IdForm.Equals(idForm.Trim()) Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            For Each d In result(0).MedicalFeesSecuenceDetail
                d.OperatingUnitName = If(d.OperatingUnit IsNot Nothing, d.OperatingUnit.UnitName, String.Empty)
                d.PatternName = If(d.Sequense IsNot Nothing, d.Sequense.Pattern, String.Empty)
            Next
            Return result(0)
        Else
            Return New MedicalFeesSecuence()
        End If
    End Function

#End Region

End Class
