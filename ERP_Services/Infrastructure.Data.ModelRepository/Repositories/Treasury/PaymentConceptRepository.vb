'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PaymentConceptRepository
    Inherits GenericRepository(Of TreasuryPaymentConcepts)
    Implements IPaymentConceptRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetPaymentConcept(code As String) As TreasuryPaymentConcepts Implements IPaymentConceptRepository.GetPaymentConcept
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As TreasuryPaymentConcepts In Me._context.TreasuryPaymentConcepts Where d.Code.Equals(code.Trim()) Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As TreasuryPaymentConcepts In Me._context.TreasuryPaymentConcepts.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New TreasuryPaymentConcepts()
        End If
    End Function

End Class
