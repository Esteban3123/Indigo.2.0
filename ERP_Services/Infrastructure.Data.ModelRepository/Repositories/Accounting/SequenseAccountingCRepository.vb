'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
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
Public Class SequenseAccountingCRepository
    Inherits GenericRepository(Of GeneralLedgerSequence)
    Implements ISequenseAccountingCRepository

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

#Region "ISequenseAccountingRepository"

    ''' <summary>
    ''' <see cref="ISequenseAccountingRepository.GetSequenseByIdForm" />
    ''' </summary>
    ''' <param name="idForm"><see cref="ISequenseAccountingRepository.GetSequenseByIdForm" /></param>
    ''' <returns><see cref="ISequenseAccountingRepository.GetSequenseByIdForm" /></returns>
    Public Function GetSequenseByIdForm(idForm As String) As GeneralLedgerSequence Implements ISequenseAccountingCRepository.GetSequenseByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Dim result = (From s As GeneralLedgerSequence In Me._context.GeneralLedgerSequence.Include("GeneralLedgerSequenceDetail.Sequense").Include("GeneralLedgerSequenceDetail.OperatingUnit") Where s.IdForm.Equals(idForm.Trim()) Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            For Each d In result(0).GeneralLedgerSequenceDetail
                d.OperatingUnitName = If(d.OperatingUnit IsNot Nothing, d.OperatingUnit.UnitName, String.Empty)
                d.PatternName = If(d.Sequense IsNot Nothing, d.Sequense.Pattern, String.Empty)
            Next
            Return result(0)
        Else
            Return New GeneralLedgerSequence()
        End If
    End Function

#End Region

End Class
