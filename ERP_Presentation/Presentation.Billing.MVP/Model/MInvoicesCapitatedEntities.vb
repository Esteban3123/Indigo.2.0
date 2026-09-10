'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 20-12-2014
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
Imports Domain.Billing.POCO

#End Region

Public Class MInvoicesCapitatedEntities
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
    ''' lista los grupos de atencion por tipo de liquidacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCareGroup() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListCareGroupByStatusAndLiquidationType(True, "2, 3, 4, 5")
    End Function

    ''' <summary>
    ''' Obtiene una factura por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Async Function GetInvoiceEntityCapitatedById(Id As Integer) As Task(Of InvoiceEntityCapitated)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetInvoiceEntityCapitatedByIdAsync(Id)
    End Function

    ''' <summary>
    ''' Obtiene una factura de entidad capitada por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetInvoiceEntityCapitated(code As String) As Task(Of ActionResult(Of InvoiceEntityCapitated))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetInvoiceEntityCapitatedAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda una factura a entidad capitada
    ''' </summary>
    ''' <param name="invoice"></param>
    ''' <returns></returns>
    Public Async Function SaveInvoiceEntityCapitated(invoice As InvoiceEntityCapitated) As Task(Of ActionResult(Of InvoiceEntityCapitated))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveInvoiceEntityCapitatedAsync(invoice, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene el documento electrónico de la factura previa
    ''' </summary>
    ''' <param name="InvoiceId"></param>
    ''' <returns></returns>
    Public Async Function GetElectronicDocumentByInvoiceId(ByVal InvoiceId As Integer) As Task(Of ElectronicDocumentXpo)
        Dim Filter As String = "EntityName = 'Invoice' And EntityId =" & InvoiceId
        Return Await Task.Run(Function() XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.GetXPOObject(Of ElectronicDocumentXpo)(Filter))
    End Function

    ''' <summary>
    ''' Obtiene los valores totales por concepto de recaudo para facturas capitas/PGP
    ''' </summary>
    ''' <param name="initialDate"></param>
    ''' <param name="endDate"></param>
    ''' <param name="careGroupId"></param>
    ''' <returns></returns>
    Public Async Function GetCollectionValues(initialDate As DateTime, endDate As DateTime, careGroupId As Integer) As Task(Of CollectionValues)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetCollectionValuesAsync(initialDate, endDate, careGroupId)
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
