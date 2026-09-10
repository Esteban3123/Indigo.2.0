'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 05-08-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base

#End Region

''' <summary>
''' Model de conexion con los servicios distribuidos de ciudades
''' </summary>
Public Class MDistributionLines
    Inherits ModelBase
    Implements IDisposable

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

    Public Shared TAG As String = "584"

#Region "Methods"

    ''' <summary>
    ''' Obtiene una linea de distribucion
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Function GetDistributionLinesById(ByVal id As Integer) As ActionResult(Of Domain.Entities.DistributionLines)
        Me.Indigo.AuditMessageWcf.Functional = TAG
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetDistributionLinesById(id, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene una linea de distribucion
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Async Function GetDistributionLines(ByVal code As String) As Task(Of ActionResult(Of Domain.Entities.DistributionLines))
        Me.Indigo.AuditMessageWcf.Functional = TAG
        Dim OriginalTransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        Dim TransactionalContainer As String = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        If Not String.IsNullOrEmpty(Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer) Then
            If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf IsNot Nothing Then
                If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional IsNot Nothing Then
                    If Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission IsNot Nothing Then
                        Dim vieForm = Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission.FirstOrDefault(Function(f) f.Id = Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional)
                        If vieForm IsNot Nothing AndAlso vieForm.IsFoundational Then
                            TransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer
                        End If
                    End If
                    Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional = Nothing
                End If
            End If
        End If
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = TransactionalContainer
        Dim result = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetDistributionLinesAsync(code, Indigo)
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = OriginalTransactionalContainer
        Return result
    End Function

    ''' <summary>
    ''' Guarda o actualiza la linea de distribucion
    ''' </summary>
    ''' <param name="distributionLines"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveDistributionLines(ByVal distributionLines As Domain.Entities.DistributionLines, Optional ByVal idSequense As Int64 = 0) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.DistributionLines))
        Me.Indigo.AuditMessageWcf.Functional = TAG
        Dim OriginalTransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        Dim TransactionalContainer As String = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        If Not String.IsNullOrEmpty(Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer) Then
            If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf IsNot Nothing Then
                If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional IsNot Nothing Then
                    If Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission IsNot Nothing Then
                        Dim vieForm = Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission.FirstOrDefault(Function(f) f.Id = Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional)
                        If vieForm IsNot Nothing AndAlso vieForm.IsFoundational Then
                            TransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer
                        End If
                    End If
                    Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional = Nothing
                End If
            End If
        End If
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = TransactionalContainer
        Dim result = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveDistributionLinesAsync(distributionLines, Indigo, idSequense)
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = OriginalTransactionalContainer
        Return result
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveDistributionLinesAsync(distributionLines, Indigo, idSequense)
    End Function

    ''' <summary>
    ''' Elimina una linea de distribucion
    ''' </summary>
    ''' <param name="distributionLines"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteDistributionLines(ByVal distributionLines As Domain.Entities.DistributionLines) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = TAG
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteDistributionLinesAsync(distributionLines, Indigo)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Domain.Entities.DistributionLines))
        Me.Indigo.AuditMessageWcf.Functional = TAG
        Dim OriginalTransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        Dim TransactionalContainer As String = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        If Not String.IsNullOrEmpty(Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer) Then
            If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf IsNot Nothing Then
                If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional IsNot Nothing Then
                    If Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission IsNot Nothing Then
                        Dim vieForm = Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission.FirstOrDefault(Function(f) f.Id = Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional)
                        If vieForm IsNot Nothing AndAlso vieForm.IsFoundational Then
                            TransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer
                        End If
                    End If
                    Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional = Nothing
                End If
            End If
        End If
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = TransactionalContainer
        Dim result = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ChangeStateDistributionLinesAsync(code, state, Indigo)
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = OriginalTransactionalContainer
        Return result
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ChangeStateDistributionLinesAsync(code, state, Indigo)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
