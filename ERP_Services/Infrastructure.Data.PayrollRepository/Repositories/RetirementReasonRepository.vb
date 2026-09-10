'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Infrastructure.Data.Base

Public Class RetirementReasonRepository

    Inherits GenericRepository(Of RetirementReason)
    Implements IRetirementReasonRepository

    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia la instancia del contexto
    ''' </summary>
    ''' <param name="context">Contexto de payroll</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una Razón de Retiro
    ''' </summary>
    ''' <param name="code">Código de Razón de Retiro</param>
    ''' <returns>Razón de Retiro</returns>
    Public Function GetRetirementReason(code As String, Optional tracking As Boolean = True) As RetirementReason Implements IRetirementReasonRepository.GetRetirementReason
        Dim RetirementReason = From e In _context.RetirementReason
                                Where e.Code = code
                                Select e

        If (RetirementReason.Count > 0) Then
            Dim objRetirementReason = Nothing
            If tracking = False Then
                objRetirementReason = (From e In _context.RetirementReason.AsNoTracking
                                       Where e.Code = code
                                       Select e).SingleOrDefault
            Else
                objRetirementReason = RetirementReason.SingleOrDefault()
            End If
            Return objRetirementReason
        Else
            Return New RetirementReason()
        End If

    End Function


    ''' <summary>
    ''' Obtiene una Razón de Retiro por id
    ''' </summary>
    ''' <param name="id">Código de Razón de Retiro</param>
    ''' <returns>Razón de Retiro</returns>
    Public Function GetRetirementReasonById(id As Integer) As RetirementReason Implements IRetirementReasonRepository.GetRetirementReasonById

        Dim RetirementReason = From e In _context.RetirementReason
                                Where e.Id = id
                                Select e

        If (RetirementReason.Count > 0) Then
            Return RetirementReason.SingleOrDefault()
        Else
            Return New RetirementReason()
        End If

    End Function

    ''' <summary>
    ''' Lista Todas las Razones de Retiro
    ''' </summary>
    ''' <returns>Lista de Razones de Retiro</returns>
    Public Function ListAllRetirementReason() As List(Of RetirementReason) Implements IRetirementReasonRepository.ListAllRetirementReason
        Dim RetirementReason = From e In _context.RetirementReason
                               Select e
        Return RetirementReason.ToList()
    End Function
End Class
