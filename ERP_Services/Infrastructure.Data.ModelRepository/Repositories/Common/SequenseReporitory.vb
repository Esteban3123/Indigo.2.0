'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
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
Public Class SequenseReporitory
    Inherits GenericRepository(Of Sequense)
    Implements ISequenseRepository

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

#Region "ISequenseRepository"

    Public Function GetPatternSequence(idSeq As Integer) As Sequense Implements ISequenseRepository.GetPatternSequence
        Dim result = (From s As Sequense In Me._context.Sequense Where s.Id = idSeq Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New Sequense()
        End If
    End Function

    Public Function ListSequences() As List(Of Sequense) Implements ISequenseRepository.ListSequences
        Dim result = (From s As Sequense In Me._context.Sequense Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result
        Else
            Return New List(Of Sequense)()
        End If
    End Function

    Public Function GetPatternSequenceByName(nameSeq As String) As Sequense Implements ISequenseRepository.GetPatternSequenceByName
        Dim result = (From s As Sequense In Me._context.Sequense Where s.Name = nameSeq Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New Sequense()
        End If
    End Function

#End Region

End Class
