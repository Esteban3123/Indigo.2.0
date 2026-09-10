'***********************************************************************
' Assembly         : Infrastructure.Data.BillingRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 06-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class BillingSequenceRepository
    Inherits GenericRepository(Of BillingSequence)
    Implements IBillingSequenceRepository

    Private Const SpReserveBillingSequenceDetailNext As String = "[Billing].[SP_ReserveBillingSequenceDetailNext]"

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
    Public Function GetSequenseByIdForm(idForm As String) As BillingSequence Implements IBillingSequenceRepository.GetSequenseByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Dim result = (From s As BillingSequence In Me._context.BillingSequence.Include("BillingSequenceDetail.Sequense").Include("BillingSequenceDetail.OperatingUnit") Where s.IdForm.Equals(idForm.Trim()) Select s).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            For Each d In result(0).BillingSequenceDetail
                d.OperatingUnitName = If(d.OperatingUnit IsNot Nothing, d.OperatingUnit.UnitName, String.Empty)
                d.PatternName = If(d.Sequense IsNot Nothing, d.Sequense.Pattern, String.Empty)
            Next
            Return result(0)
        Else
            Return New BillingSequence()
        End If
    End Function

    ''' <inheritdoc />
    Public Function ReserveNextFormattedCodeByFormId(idForm As String) As BillingSequenceCodeReservation Implements IBillingSequenceRepository.ReserveNextFormattedCodeByFormId
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException(NameOf(idForm))
        End If

        Dim rows = ExecuteStoredProcedure(Of SP_ReserveBillingSequenceDetailNext_Result)(SpReserveBillingSequenceDetailNext, {("@IdForm", idForm.Trim())})
        Dim raw = rows?.FirstOrDefault()
        If raw Is Nothing Then
            Return New BillingSequenceCodeReservation With {.Success = False, .Message = "No se obtuvo respuesta al reservar la secuencia numérica."}
        End If

        If raw.CodeResult <> 1 Then
            Return New BillingSequenceCodeReservation With {
                .Success = False,
                .Message = If(String.IsNullOrEmpty(raw.MessageResult), "La secuencia para las Notas Crédito de Facturacion Electronica no esta parametrizada o no es secuencial.", raw.MessageResult)
            }
        End If

        If Not raw.ReservedNext.HasValue OrElse String.IsNullOrEmpty(raw.Pattern) Then
            Return New BillingSequenceCodeReservation With {.Success = False, .Message = "La secuencia para las Notas Crédito de Facturacion Electronica no esta parametrizada o no es secuencial."}
        End If

        Dim codeNote = Infrastructure.CrossCutting.Base.Sequense.GetSequense(raw.Pattern, raw.ReservedNext.Value)
        If String.IsNullOrEmpty(codeNote) OrElse codeNote.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
            If raw.DetailId.HasValue AndAlso raw.DetailId.Value > 0 Then
                ExecuteNonQuery("UPDATE Billing.BillingSequenceDetail SET [Next] = [Next] - 1 WHERE Id = {0}", raw.DetailId.Value)
            End If
            Return New BillingSequenceCodeReservation With {.Success = False, .Message = "La secuencia para las Notas Crédito de Facturacion Electronica alcanzo su valor maximo."}
        End If

        Return New BillingSequenceCodeReservation With {.Success = True, .Code = codeNote}
    End Function

#End Region
End Class
