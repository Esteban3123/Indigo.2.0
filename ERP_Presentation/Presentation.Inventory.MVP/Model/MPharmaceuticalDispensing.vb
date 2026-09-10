'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-01-2015
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
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

Public Class MPharmaceuticalDispensing
    Implements IDisposable

#Region "Fields"

    '' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una dispensación farmacéutica
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetPharmaceuticalDispensing(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of PharmaceuticalDispensing))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPharmaceuticalDispensingAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una dispensación farmacéutica por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetPharmaceuticalDispensingById(ByVal id As Integer) As Task(Of PharmaceuticalDispensing)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPharmaceuticalDispensingByIdAsync(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una dispensación farmacéutica
    ''' </summary>
    ''' <param name="pharmaceuticalDispensing">The pharmaceutical dispensing.</param>
    ''' <param name="idSequense">The identifier sequense.</param>
    ''' <returns></returns>
    Public Async Function SavePharmaceuticalDispensing(ByVal pharmaceuticalDispensing As PharmaceuticalDispensing, ByVal idSequense As Int64, ByVal confirm As Boolean, Optional affectInventory As Boolean = True) As Task(Of ActionResult(Of PharmaceuticalDispensing))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SavePharmaceuticalDispensingAsync(pharmaceuticalDispensing, confirm, idSequense, Me.Indigo.AuditMessageWcf, affectInventory)
    End Function

    ''' <summary>
    ''' Elimina una dispensación farmacéutica
    ''' </summary>
    ''' <param name="pharmaceuticalDispensing">The pharmaceutical dispensing.</param>
    ''' <returns></returns>
    Public Async Function DeletePharmaceuticalDispensing(ByVal pharmaceuticalDispensing As PharmaceuticalDispensing) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeletePharmaceuticalDispensingAsync(pharmaceuticalDispensing, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista todos los BatchSerial de la dispensación
    ''' </summary>
    ''' <param name="id">Id de la dispensación</param>
    ''' <returns>Lista de BatchSerial de la dispensación</returns>
    Public Async Function ListPharmaceuticalDispensingDetailBatchSerialByDispensingId(ByVal id As Int32) As Task(Of List(Of PharmaceuticalDispensingDetailBatchSerial))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPharmaceuticalDispensingDetailBatchSerialByPharmaceuticalDispensingIdAsync(id)
        End Using
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