'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Hector Rodriguez Rubiano
' Created          : 05/11/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class CashFlowConceptRepository
    Inherits GenericRepository(Of CashFlowConcept)
    Implements ICashFlowConceptRepository

    'contexto de cartera
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para obtener un concepto de flujo de efectivo por codigo
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Public Function GetCashFlowConceptByCode(code As String) As CashFlowConcept Implements ICashFlowConceptRepository.GetCashFlowConceptByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As CashFlowConcept In _context.CashFlowConcept Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From d As CashFlowConcept In _context.CashFlowConcept.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New CashFlowConcept()
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de cuenta x pagar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCashFlowConceptById(id As Integer) As CashFlowConcept Implements ICashFlowConceptRepository.GetCashFlowConceptById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d As CashFlowConcept In _context.CashFlowConcept Where d.Id = id Select d).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From d As CashFlowConcept In _context.CashFlowConcept.AsNoTracking Where d.Id = id Select d).FirstOrDefault()
            Return res
        Else
            Return New CashFlowConcept()
        End If
    End Function
#End Region

End Class
