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
Public Class SequensePaymentsCRepository
    Inherits GenericRepository(Of PaymentsSecuence)
    Implements ISequensePaymentsCRepository

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
    Public Function GetSequenseByIdForm(idForm As String) As PaymentsSecuence Implements ISequensePaymentsCRepository.GetSequenseByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Dim result = (From s As PaymentsSecuence In Me._context.PaymentsSecuence.Include("PaymentsSecuenceDetail").Include("PaymentsSecuenceDetail.Sequense").Include("PaymentsSecuenceDetail.OperatingUnit") Where s.IdForm.Equals(idForm.Trim()) Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            For Each d In result(0).PaymentsSecuenceDetail
                d.OperatingUnitName = If(d.OperatingUnit IsNot Nothing, d.OperatingUnit.UnitName, String.Empty)
                d.PatternName = If(d.Sequense IsNot Nothing, d.Sequense.Pattern, String.Empty)
            Next
            Return result(0)
        Else
            Return New PaymentsSecuence()
        End If
    End Function

#End Region

End Class
