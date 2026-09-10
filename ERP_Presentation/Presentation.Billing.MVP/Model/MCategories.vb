'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-10-2015
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
Imports Infrastructure.Data.Xpo.BillingRepository
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Dynamic
#End Region

Public Class MCategories
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

#Region "Methods"

    ''' <summary>
    ''' Copiar y pegar para la rejilla de plantilla de procedimientos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function CopyAndPasteCategories(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of InvoiceCategoriesUser), List(Of Tuple(Of String, Integer))))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.CopyAndPasteCategoriesAsync(data)
    End Function

    Public Async Function DeleteInvoiceCategoryAsync(invoiceCategory As Domain.Entities.InvoiceCategories) As Task(Of Domain.Base.Entities.ActionResult)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.DeleteInvoiceCategoryAsync(invoiceCategory, _indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetInvoiceCategoryAsync(code As String) As Task(Of Domain.Entities.InvoiceCategories)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetInvoiceCategoryAsync(code, _indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetInvoiceCategoryByIdAsync(id As Integer) As Task(Of Domain.Entities.InvoiceCategories)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetInvoiceCategoryByIdAsync(id)
    End Function

    Public Async Function SaveInvoiceCategoryAsync(invoiceCategory As Domain.Entities.InvoiceCategories, Optional idSequence As Long = 0) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.InvoiceCategories))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveInvoiceCategoryAsync(invoiceCategory, _indigoSessionValues.AuditMessageWcf, idSequence)
    End Function

    Public Async Function UpdateStateInvoiceCategoryAsync(code As String, state As Boolean) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.InvoiceCategories))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.UpdateStateInvoiceCategoryAsync(code, state, _indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function ListUsers() As LinqInstantFeedbackSource
        'Return XpoServiceEx.Instance(_indigoSessionValues.SecurityContainer).SecurityService.GetAllUser()
        Return XpoServiceEx.Instance(_indigoSessionValues.SecurityContainer).SecurityService.ListUserByContainer(_indigoSessionValues.IndigoContainerId)
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
