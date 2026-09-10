'***********************************************************************
' Assembly         : Infrastructure.Data.CostRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
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
Public Class CostSequenceRepository
    Inherits GenericRepository(Of CostSecuence)
    Implements ICostSequenceRepository

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

#Region "ISequenseInteropCostRepository"
    Public Function GetSequenseByIdForm1(idForm As String) As CostSecuence Implements ICostSequenceRepository.GetSequenseByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Dim result = (From s As CostSecuence In Me._context.CostSecuence.Include("CostSecuenceDetail.Sequense").Include("CostSecuenceDetail.OperatingUnit") Where s.IdForm.Equals(idForm.Trim()) Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            For Each d In result(0).CostSecuenceDetail
                d.OperatingUnitName = If(d.OperatingUnit IsNot Nothing, d.OperatingUnit.UnitName, String.Empty)
                d.PatternName = If(d.Sequense IsNot Nothing, d.Sequense.Pattern, String.Empty)
            Next
            Return result(0)
        Else
            Return New CostSecuence()
        End If
    End Function
#End Region

End Class