'***********************************************************************
' Assembly         : Application.Taxes
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/09/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Text
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FiscalizationAdminService
    Implements IFiscalizationAdminService

    'Repositorio para la liquidación de impuestos
    Private _fiscalizationRepository As IFiscalizationRepository

    Public Sub New(fiscalizationRepository As IFiscalizationRepository)
        _fiscalizationRepository = fiscalizationRepository
    End Sub

    ''' <summary>
    ''' Valida el archivo
    ''' </summary>
    ''' <param name="ListData"></param>
    ''' <param name="Year"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ValidateTaxBase(ListData As List(Of String), Year As Integer) As ActionResult(Of List(Of SP_ValidateTaxBase_Result)) Implements IFiscalizationAdminService.SP_ValidateTaxBase
        Try
            Dim Xml = GenerateXml(ListData)
            Dim result = _fiscalizationRepository.SP_ValidateTaxBase(Xml, Year).ToList()
            If result.Count = 1 AndAlso result(0).Status = 0 Then
                Return New ActionResult(Of List(Of SP_ValidateTaxBase_Result)) With {.StatusCode = eStatusResult.WARNING, .Message = result(0).Message}
            End If
            
            Return New ActionResult(Of List(Of SP_ValidateTaxBase_Result)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = result}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_ValidateTaxBase_Result)) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Genera el xml
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateXml(data As List(Of String)) As String
        Dim result As New StringBuilder()
        result.Append("<Data>")
        For Each item In data
            'result.Append("<Record>" & item.CleanSpecialChars() & "</Record>")
            result.Append("<Record>" & item.ToString & "</Record>")
        Next
        result.Append("</Data>")
        Return result.ToString()
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _fiscalizationRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
