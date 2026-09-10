'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 29-01-2017
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports System.ServiceModel

#End Region

Public Class MPrivateBudget

    Inherits ModelBaseBudget
    Implements IDisposable

    Public Shared TAG As String = "205"

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        MyBase.New(TAG)
        _indigoSessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el registro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetBudgetPrivate() As Task(Of ActionResult(Of List(Of SP_ListPrivateBudget_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetPrivateBudgetAsync(Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPrivateBudgetItemsStructureById(id As String) As Task(Of ActionResult(Of PrivateBudgetItemsStructure))
        'Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoBudget.InnerChannel)
        '    _indigoSessionValues.AuditMessageWcf.Functional = TAG
        '    Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
        '    Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
        '    OperationContext.Current.OutgoingMessageHeaders.Add(header)
        '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetPrivateBudgetItemsStructureByIdAsync(id)
        'End Using
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Public Async Function DeletePrivateBudgetItemsStructure(PrivateBudgetItemsStructure As PrivateBudgetItemsStructure) As Task(Of Domain.Base.Entities.ActionResult)
        'Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoBudget.InnerChannel)
        '    _indigoSessionValues.AuditMessageWcf.Functional = TAG
        '    Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
        '    Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
        '    OperationContext.Current.OutgoingMessageHeaders.Add(header)
        '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeletePrivateBudgetItemsStructureAsync(PrivateBudgetItemsStructure)
        'End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Async Function SavePrivateBudget(PrivateBudget As List(Of SP_ListPrivateBudget_Result), ByVal idSequense As Int64) As Task(Of ActionResult(Of List(Of SP_ListPrivateBudget_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SavePrivateBudgetAsync(PrivateBudget, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
