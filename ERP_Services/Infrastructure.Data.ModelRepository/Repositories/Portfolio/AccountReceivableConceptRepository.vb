'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepositiry
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class AccountReceivableConceptRepository
    Inherits GenericRepository(Of AccountReceivableConcept)
    Implements IAccountReceivableConceptRepository

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
    ''' metodo para obtener todos los conceptos de cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAccountReceivableConcept() As Object Implements IAccountReceivableConceptRepository.GetAllAccountReceivableConcept
        Return (From pc In _context.AccountReceivableConcept Select pc).ToList()
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de cuenta por pagar
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetAccountReceivableConceptByCode(code As String, Optional tracking As Boolean = True) As Object Implements IAccountReceivableConceptRepository.GetAccountReceivableConceptByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        If tracking = False Then
            Dim res = (From d As AccountReceivableConcept In _context.AccountReceivableConcept.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).ToList()
            If res IsNot Nothing Then
                res(0).OriginalValue = (From d As AccountReceivableConcept In _context.AccountReceivableConcept.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New AccountReceivableConcept()
            End If
        Else
            Dim res = (From d As AccountReceivableConcept In _context.AccountReceivableConcept Where d.Code.Equals(code.Trim()) Select d).ToList()
            If res IsNot Nothing Then
                res(0).OriginalValue = (From d As AccountReceivableConcept In _context.AccountReceivableConcept Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New AccountReceivableConcept()
            End If
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de cuenta x pagar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableConceptById(id As Integer) As AccountReceivableConcept Implements IAccountReceivableConceptRepository.GetAccountReceivableConceptById

        Dim res = (From d As AccountReceivableConcept In _context.AccountReceivableConcept.AsNoTracking Where d.Id = id Select d).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New AccountReceivableConcept()
        End If

    End Function

#End Region

End Class
