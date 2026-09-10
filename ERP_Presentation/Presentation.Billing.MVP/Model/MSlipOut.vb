'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 05/01/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Presentation.Base

#End Region

Public Class MSlipOut
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
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
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtener un boletin de salida por codigo
    ''' </summary>
    ''' <param name="code">codigo de un boletin de salida</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSlipOutByCode(ByVal code As String) As Task(Of Domain.Entities.SlipOut)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetSlipOutByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' obterner un boletin de salida por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSlipOutById(ByVal id As Integer) As Task(Of Domain.Entities.SlipOut)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoBilling.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetSlipOutByIdAsync(id)
        End Using
    End Function

    ''' <summary>
    ''' guarda o actualiza una boleta de salida
    ''' </summary>
    ''' <param name="slipOut"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveSlipOut(slipOut As SlipOut, ByVal idSequense As Int64, ByVal sequenceC As Domain.Entities.BillingSequence) As Task(Of ActionResult(Of SlipOut))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveSlipOutAsync(slipOut, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' obtener un boletin de salida por numero de admisión
    ''' </summary>
    ''' <param name="admissionNumber">numero de admisión</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSlipOutByNumberAdmission(ByVal admissionNumber As String) As Task(Of Domain.Entities.SlipOut)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetSlipOutByNumberAdmissionAsync(admissionNumber, Me._indigoSessionValues.AuditMessageWcf)
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
