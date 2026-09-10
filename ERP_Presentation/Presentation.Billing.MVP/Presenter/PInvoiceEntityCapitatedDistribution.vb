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
Imports Presentation.Base
Imports Infrastructure.Data.Xpo

#End Region

Public Class PInvoiceEntityCapitatedDistribution

#Region "Fields"

    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim View As IInvoiceEntityCapitatedDistribution

    ''' <summary>
    ''' Referencia a los valores de sesión
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista del frontal</param>
    Public Sub New(ByRef view As IInvoiceEntityCapitatedDistribution)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.Indigo = SessionValues.Instance
            Me.View = view
        End If
    End Sub


#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Function

    ''' <summary>
    ''' Lista las facturas de monto fijo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeInvoiceEntityCapitatedXPO()
        View.InvoiceEntityCapitatedXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListInvoiceEntityCapitated()
    End Sub

    Public Function GetinvoiceEntityCapitatedById(id As String) As BillingRepository.InvoiceEntityCapitatedXpo
        Dim filtroConsulta As String = "Id = " & id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of BillingRepository.InvoiceEntityCapitatedXpo)(Nothing, filtroConsulta)(0)
    End Function

#End Region

End Class
