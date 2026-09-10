'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 28-10-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.Xpo
Imports System.Dynamic

#End Region

Public Class MRemissionOutput
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

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "METHODS"
    ''' <summary>
    ''' lista los almacenes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListWarehouse() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, _indigoSessionValues.UserIndigo)
    End Function
    ''' <summary>
    ''' lista los clientes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCustomer() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CommonService.ListCustomerByStatus(True)
    End Function

    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetRemissionOutputByCode(code As String) As Task(Of RemissionOutput)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetRemissionOutputByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRemissionOutputById(id As Integer) As RemissionOutput
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetRemissionOutputById(id)
    End Function
    ''' <summary>
    ''' guardar una remision
    ''' </summary>
    ''' <param name="RemissionOutput"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveRemissionOutput(RemissionOutput As RemissionOutput, idSequense As Integer, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of RemissionOutput))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveRemissionOutputAsync(RemissionOutput, Me._indigoSessionValues.AuditMessageWcf, idSequense, sequenceC)
    End Function
    ''' <summary>
    ''' guardar y confirmar una remision
    ''' </summary>
    ''' <param name="RemissionOutput"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmRemissionOutput(RemissionOutput As RemissionOutput, idSequense As Integer, action As Integer, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of Domain.Entities.RemissionOutput))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmbRemissionOutputAsync(RemissionOutput, idSequense, Me._indigoSessionValues.AuditMessageWcf, sequenceC, action)
    End Function
    ''' <summary>
    ''' lista los detalles de la remision
    ''' </summary>
    ''' <param name="RemissionOutputId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRemissionOutputDetailByRemissionOutputId(RemissionOutputId As Integer) As List(Of RemissionOutputDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetRemissionOutputDetailByRemissionOutputId(RemissionOutputId)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
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

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
