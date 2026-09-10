'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries"
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

Public Class TaxesPropertyAdminService
    Implements ITaxesPropertyAdminService

    Private _taxesPropertyRepository As ITaxesPropertyRepository

    Public Sub New(taxesPropertyRepository As ITaxesPropertyRepository)
        _taxesPropertyRepository = taxesPropertyRepository
    End Sub

    Private Function GenerateXml(data As List(Of String)) As String
        Dim result As New StringBuilder()
        result.Append("<Data>")
        For Each item In data
            result.Append("<Record>" & item.CleanSpecialChars() & "</Record>")
        Next
        result.Append("</Data>")
        Return result.ToString()
    End Function

    Private Function GenerateXmlItem(item As String) As String
        Dim result As New StringBuilder()
        result.Append("<Record>" & item & "</Record>")
        Return result.ToString()
    End Function

    Public Function ValidateLoadPlaneCollection(data As List(Of String)) As ActionResult(Of List(Of Tuple(Of Integer, String, String, String))) Implements ITaxesPropertyAdminService.ValidateLoadPlaneCollection
        Try
            Dim listReturn As New List(Of Tuple(Of Integer, String, String, String))
            Dim items = GenerateXml(data)
            'My.Computer.FileSystem.WriteAllText("C:\xml.txt", items, True)
            CType(_taxesPropertyRepository.UnitWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
            Dim result = _taxesPropertyRepository.ValidateLoadPlaneCollection(items).ToList()
            If result.Count = 1 AndAlso result(0).State = 0 Then
                Return New ActionResult(Of List(Of Tuple(Of Integer, String, String, String))) With {.StatusCode = eStatusResult.WARNING, .Message = result(0).Message}
            End If
            For Each item In result
                listReturn.Add(New Tuple(Of Integer, String, String, String)(item.State, item.Message, item.Record, GenerateXmlItem(item.Record)))
            Next

            Return New ActionResult(Of List(Of Tuple(Of Integer, String, String, String))) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listReturn}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Tuple(Of Integer, String, String, String))) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function SaveLoadPlaneCollection(data As List(Of String), audit As AuditMessage) As ActionResult Implements ITaxesPropertyAdminService.SaveLoadPlaneCollection
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim XmlData As New StringBuilder()
                XmlData.Append("<Data>")
                For Each item In data
                    XmlData.Append(item)
                Next
                XmlData.Append("</Data>")
                Dim items = XmlData.ToString
                'My.Computer.FileSystem.WriteAllText("C:\xml.txt", items, True)
                CType(_taxesPropertyRepository.UnitWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                Dim result = _taxesPropertyRepository.SaveLoadPlaneCollection(items, audit.CodeUser).FirstOrDefault()
                If result.Status = 1 Then
                    transaction.Complete()
                    Return New ActionResult With {.Message = result.Message, .StatusCode = eStatusResult.SUCCESS}
                Else
                    Return New ActionResult With {.Message = result.Message, .StatusCode = eStatusResult.WARNING}
                End If

            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    Public Function GetAllTaxedProperties() As List(Of TaxesProperty) Implements ITaxesPropertyAdminService.GetAllTaxedProperties
        Try
            Dim result = _taxesPropertyRepository.GetAllTaxedProperties()
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of TaxesProperty)()
        End Try
    End Function

    ''' <summary>
    ''' Busca una TaxesProperty atraves de su code
    ''' </summary>
    ''' <param name="code">Code del TaxesProperty</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTaxesPropertyByCode(code As String) As Domain.Entities.TaxesProperty Implements ITaxesPropertyAdminService.GetTaxesPropertyByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("cedula catastral vacia")
        End If
        Try
            Return _taxesPropertyRepository.GetTaxesPropertyByCode(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New TaxesProperty()
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _taxesPropertyRepository = Nothing
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
