'***********************************************************************
' Assembly         : Presentacion.Payrol.MVP
' Author           : Rafael Eduardo Patiño
' Created          : 07-01-2014
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
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Domain.Payroll.Entities
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base

#End Region
''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class MKindsAgreements
    Inherits ModelBase
    Implements IDisposable



#Region "Builders"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Obtiene una clase de convenio por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetKindsAgreements(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetKindsAgreementsAsync(code, Indigo)
    End Function
    ''' <summary>
    ''' Guarda una clase de convenio
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveKindsAgreements(ByVal Record As KindsAgreements) As Task(Of ActionResult(Of KindsAgreements))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveKindsAgreementsAsync(Record, Indigo)
    End Function
    ''' <summary>
    ''' Elimina una clase de convenio
    ''' </summary>
    ''' <param name="KindsAgreements"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteKindsAgreements(ByVal KindsAgreements As KindsAgreements) As Task(Of ActionMessageResult(Of KindsAgreements))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteKindsAgreementsAsync(KindsAgreements, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult(Of BlockRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me.Indigo)
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
    ''' Listar los campos nulls de la base de datos y porder customizar 
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetFieldsNULL() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("KindsAgreements", Me.Indigo)
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
