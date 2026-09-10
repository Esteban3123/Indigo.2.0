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
Public Class InventorySequenceRepository
    Inherits GenericRepository(Of InventorySequence)
    Implements IInventorySequenceRepository

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

#Region "ISequensePaymentsRepository"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idForm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSequenseByIdForm(idForm As String) As InventorySequence Implements IInventorySequenceRepository.GetSequenseByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Dim result = (From s As InventorySequence In Me._context.InventorySequence.Include("Sequense").Include("InventorySequenceDetail.Sequense").Include("InventorySequenceDetail.OperatingUnit") Where s.IdForm.Equals(idForm.Trim()) Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            For Each d In result(0).InventorySequenceDetail
                d.OperatingUnitName = If(d.OperatingUnit IsNot Nothing, d.OperatingUnit.UnitName, String.Empty)
                d.PatternName = If(d.Sequense IsNot Nothing, d.Sequense.Pattern, String.Empty)
            Next
            Return result(0)
        Else
            Return New InventorySequence()
        End If
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idForm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSequenseByPrefix(prefix As String, idSequencce As Integer) As InventorySequenceDetail Implements IInventorySequenceRepository.GetSequenseByPrefix
        If prefix Is Nothing OrElse prefix.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("prefix")
        End If
        Dim result = (From s As InventorySequenceDetail In Me._context.InventorySequenceDetail Where s.Prefix.Equals(prefix.Trim()) And s.InventorySequenceId = idSequencce Select s)?.FirstOrDefault()
        If result IsNot Nothing Then
            Return result
        Else
            Return New InventorySequenceDetail()
        End If
    End Function

#End Region

End Class
