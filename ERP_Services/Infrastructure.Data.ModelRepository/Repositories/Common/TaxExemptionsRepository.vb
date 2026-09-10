'***********************************************************************
' Assembly         : Infrastructure.Data.Common
' Author           : Andres Alarcon
' Created          : 26/08/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class TaxExemptionsRepository
    Inherits GenericRepository(Of TaxExemptions)
    Implements ITaxExemptionsRepository

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una exoneracion tributaria por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTaxExemptions(code As String, Optional tracking As Boolean = True) As TaxExemptions Implements ITaxExemptionsRepository.GetTaxExemptions
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d As TaxExemptions In Me._context.TaxExemptions Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New TaxExemptions()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una exoneracion tributaria por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTaxExemptionsById(id As Integer, Optional tracking As Boolean = True) As TaxExemptions Implements ITaxExemptionsRepository.GetTaxExemptionsById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.TaxExemptions Where d.Id = id Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New TaxExemptions()
        End If
    End Function
End Class
