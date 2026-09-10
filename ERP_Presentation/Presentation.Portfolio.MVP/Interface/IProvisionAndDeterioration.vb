'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/10/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IProvisionAndDeterioration
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <returns>My layout control.</returns>
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
    Property Sequense As Domain.Entities.PortfolioSequence

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
    ''' Fecha de corte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CourtDate As DateTime?

    ''' <summary>
    ''' Proceso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Process As Integer?

    ''' <summary>
    ''' Aplica el deterioro a
    ''' </summary>
    ''' <returns></returns>
    Property ApplyDeterioration As Byte?

    ''' <summary>
    ''' Descripción
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Id de la edad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AgesId As Integer?

    ''' <summary>
    ''' Código factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceCode As String

#End Region

#Region "XPO"

    ''' <summary>
    ''' Datasource edades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListAgesPorfolio As List(Of Domain.Entities.AgesPortfolio)

    ''' <summary>
    ''' Datasource facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceNumberXpo As XPInstantFeedbackSource

#End Region

End Interface
