'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 05/05/2015
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


Public Class MUpdateExpirationDate
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _indigoSessionValues As SessionValues
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
        _indigoSessionValues = SessionValues.Instance
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <returns>La factura de cartera</returns>
    Public Async Function GetServerDate() As Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync()
    End Function


    ''' <summary>
    ''' obtiene los lotes
    ''' </summary>
    ''' <param name="IdProduct">Id de Producto</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBatchSerialByProductIdIncludeExpirationDate(IdProduct As Integer) As List(Of BatchSerial)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListBatchSerialByProductIdIncludeExpirationDate(IdProduct)
        End Using
    End Function

    ''' <summary>
    ''' Actualizar fecha de vencimiento
    ''' </summary>
    ''' <param name="ListBatchSerial">lista de lotes as actualizar fecha de vencimiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveBatchSerial(ListBatchSerial As List(Of BatchSerial)) As Task(Of ActionResult(Of BatchSerial))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveListBatchSerialAsync(ListBatchSerial, Me._indigoSessionValues.AuditMessageWcf)
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
