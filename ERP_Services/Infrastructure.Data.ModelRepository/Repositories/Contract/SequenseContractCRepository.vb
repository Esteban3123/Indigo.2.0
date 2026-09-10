'***********************************************************************
' Assembly         : Infrastructure.Data.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/09/2014
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
Public Class SequenseContractCRepository
    Inherits GenericRepository(Of ContractSequence)
    Implements ISequenseContractCRepository

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
    Public Function GetSequenseByIdForm(idForm As String) As ContractSequence Implements ISequenseContractCRepository.GetSequenseByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Dim result = (From s As ContractSequence In Me._context.ContractSequence.Include("ContractSequenceDetail.Sequense").Include("ContractSequenceDetail.OperatingUnit") Where s.IdForm.Equals(idForm.Trim()) Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            For Each d In result(0).ContractSequenceDetail
                d.OperatingUnitName = If(d.OperatingUnit IsNot Nothing, d.OperatingUnit.UnitName, String.Empty)
                d.PatternName = If(d.Sequense IsNot Nothing, d.Sequense.Pattern, String.Empty)
            Next
            Return result(0)
        Else
            Return New ContractSequence()
        End If
    End Function

#End Region
    
End Class
