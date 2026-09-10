'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Transactions

#End Region

Public Class AnnualizedCashFlowModificationDetailAdminService
    Implements IAnnualizedCashFlowModificationDetailAdminService

    Private _annualizedCashFlowModificationDetailRepository As IAnnualizedCashFlowModificationDetailRepository

    Public Sub New(annualizedCashFlowModificationDetailRepository As IAnnualizedCashFlowModificationDetailRepository)
        If annualizedCashFlowModificationDetailRepository Is Nothing Then
            Throw New ArgumentNullException("annualizedCashFlowModificationDetailRepository")
        End If
        _annualizedCashFlowModificationDetailRepository = annualizedCashFlowModificationDetailRepository
    End Sub

    ''' <summary>
    ''' obtiene le detalle de la modificacion por id de la cabecera
    ''' </summary>
    ''' <param name="pacModificationId"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowModificationDetailByPACModificationId(pacModificationId As Integer) As List(Of AnnualizedCashFlowModificationDetail) Implements IAnnualizedCashFlowModificationDetailAdminService.GetAnnualizedCashFlowModificationDetailByPACModificationId
        Try
            Return _annualizedCashFlowModificationDetailRepository.GetAnnualizedCashFlowModificationDetailByPACModificationId(pacModificationId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of AnnualizedCashFlowModificationDetail)
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _annualizedCashFlowModificationDetailRepository = Nothing
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
