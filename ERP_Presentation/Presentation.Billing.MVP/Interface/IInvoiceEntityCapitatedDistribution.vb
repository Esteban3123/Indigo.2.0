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

Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Infrastructure.Data.Xpo.PortfolioRepository

#End Region

Public Interface IInvoiceEntityCapitatedDistribution
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequense As Domain.Entities.BillingSequence

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Fecha documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As DateTime?

    ''' <summary>
    ''' Fectura Monto Fijo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceEntityCapitatedId As Integer

    ''' <summary>
    ''' Grupo de Atención
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CareGroupId As Integer

    ''' <summary>
    ''' Fecha Inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InitialDate As DateTime?

    ''' <summary>
    ''' Fecha Final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EndDate As DateTime?

    ''' <summary>
    ''' Observación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observation As String

    ''' <summary>
    ''' Valor Facturado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceValue As Decimal

    ''' <summary>
    ''' Valor Total de los Controles
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property TotalControlValue As Decimal

#End Region

#Region "XPO"

    ''' <summary>
    ''' Datasource de las Facturas de Monto Fijo
    ''' </summary>
    ''' <returns></returns>
    Property InvoiceEntityCapitatedXpo As XPInstantFeedbackSource

#End Region

End Interface
