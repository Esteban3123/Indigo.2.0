'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Miguel Angel Fonseca Castro
' Created          : 2019-03-13
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
#End Region

Public Class MInvoiceEntityCapitatedDistribution
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
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        _indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Function GetInvoiceEntityCapitatedDistributionById(Id As Integer) As Task(Of ActionResult(Of InvoiceEntityCapitatedDistribution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetInvoiceEntityCapitatedDistributionByIdAsync(Id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetInvoiceEntityCapitatedDistribution(code As String) As Task(Of ActionResult(Of InvoiceEntityCapitatedDistribution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetInvoiceEntityCapitatedDistributionByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetInvoiceEntityCapitatedDistributionDetailsByInvoiceEntityCapitatedId(ByVal invoiceEntityCapitatedId As Integer, ByVal invoiceEntityCapitatedDistributionId As Integer) As Task(Of ActionResult(Of List(Of InvoiceEntityCapitatedDistributionDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetInvoiceEntityCapitatedDistributionDetailsByInvoiceEntityCapitatedIdAsync(invoiceEntityCapitatedId, invoiceEntityCapitatedDistributionId)
    End Function

    Public Async Function SaveInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution, listInvoiceEntityCapitatedDistributionDetail As List(Of Integer), idSequense As Integer) As Task(Of ActionResult(Of InvoiceEntityCapitatedDistribution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveInvoiceEntityCapitatedDistributionAsync(invoiceEntityCapitatedDistribution, listInvoiceEntityCapitatedDistributionDetail, Me._indigoSessionValues, idSequense)
    End Function

    Public Async Function ReverseInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution) As Task(Of ActionResult(Of InvoiceEntityCapitatedDistribution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ReverseInvoiceEntityCapitatedDistributionAsync(invoiceEntityCapitatedDistribution, Me._indigoSessionValues)
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean

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

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
