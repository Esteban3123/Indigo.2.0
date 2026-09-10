'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 08-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo.PayrollRepository

#End Region
''' <summary>
''' Modelo de conexion con los servicios distribuidos de la corporacion
''' </summary>
Public Class MBankPayroll
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Funcion para obtener la corporacion Asincrono
    ''' </summary>
    ''' <param name="code">Codigo de la corporacion</param>
    ''' <returns></returns>
    Public Async Function GetBankAsync(ByVal code As String) As Task(Of Domain.Payroll.Entities.Bank)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
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
        Dim result = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetBankAsync(code, Indigo)
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = OriginalTransactionalContainer
        Return result
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetBankAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener la corporacion
    ''' </summary>
    ''' <param name="code">Codigo de la corporacion</param>
    ''' <returns></returns>
    Public Function GetBank(ByVal code As String) As Domain.Payroll.Entities.Bank
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetBank(code, Indigo)
    End Function

    Public Function GetBankById(ByVal Id As Integer) As Domain.Payroll.Entities.Bank
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetBankById(Id, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para guardar la corporacion
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    Public Function SaveBank(ByVal Record As Domain.Payroll.Entities.Bank, idSequence As Long) As ActionResult(Of Domain.Payroll.Entities.Bank)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveBank(Record, Indigo, idSequence)
    End Function

    ''' <summary>
    ''' Funcion para guardar la corporacion Asincrono
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    Public Async Function SaveBankAsync(ByVal Record As Domain.Payroll.Entities.Bank, idSequence As Long) As Task(Of ActionResult(Of Domain.Payroll.Entities.Bank))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
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
        Dim result = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveBankAsync(Record, Indigo, idSequence)
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = OriginalTransactionalContainer
        Return result
    End Function

    ''' <summary>
    ''' Funcion para borrar la corporacion
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    Public Function DeleteBank(ByVal Record As Domain.Payroll.Entities.Bank) As ActionMessageResult(Of Domain.Payroll.Entities.Bank)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteBank(Record, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para borrar la corporacion asincrono
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    Public Async Function DeleteBankAsync(ByVal Record As Domain.Payroll.Entities.Bank) As Task(Of ActionMessageResult(Of Domain.Payroll.Entities.Bank))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteBankAsync(Record, Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As Object
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Bank", Indigo)
    End Function

    Public Function GetBankConciliationConceptById(id As Integer) As BankConciliationConceptXpo
        Return Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetBankConciliationConceptById(id)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of BlockRecord)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult(Of BlockRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me.Indigo)
    End Function

    Public Async Function UpdateStateBankAsync(code As String, state As Boolean) As Task(Of ActionResult(Of Domain.Payroll.Entities.Bank))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
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
        Dim result = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.UpdateStateBankAsync(code, state, Indigo)
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = OriginalTransactionalContainer
        Return result
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me.Indigo)
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
