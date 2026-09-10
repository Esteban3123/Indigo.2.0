'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 26-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class ContractModificationReasonRepository
    Inherits GenericRepository(Of ContractModificationReason)
    Implements IContractModificationReasonRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una razon de otro si por codigo
    ''' </summary>
    ''' <returns>razones de otro si</returns>
    ''' <remarks></remarks>
    Public Function GetContractModificationReason(code As String, Optional tracking As Boolean = True) As ContractModificationReason Implements IContractModificationReasonRepository.GetContractModificationReason
        Dim contractModification = From e In _context.ContractModificationReason
                                   Where e.Code = code
                                   Select e
        If contractModification.Count > 0 Then
            Dim objcontractModification = Nothing
            If tracking = False Then
                objcontractModification = (From e In _context.ContractModificationReason.AsNoTracking
                                           Where e.Code = code
                                           Select e).SingleOrDefault
            Else
                objcontractModification = contractModification.SingleOrDefault()
            End If
            Return objcontractModification

        Else
            Return Nothing
        End If
    End Function
End Class
