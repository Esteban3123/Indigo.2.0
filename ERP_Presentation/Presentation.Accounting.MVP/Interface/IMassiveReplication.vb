'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/06/2017
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

#End Region

''' <summary>
''' Define las propiedades y métodos de la vista
''' </summary>
Public Interface IMassiveReplication
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' fecha inicial
    ''' </summary>
    ''' <returns></returns>
    Property InitialDate As Date?

    ''' <summary>
    ''' Fecha final
    ''' </summary>
    ''' <returns></returns>
    Property EndDate As Date?

    ''' <summary>
    ''' Libro origen
    ''' </summary>
    ''' <returns></returns>
    Property BookOriginId As Integer?

    ''' <summary>
    ''' Datasource libro origen
    ''' </summary>
    ''' <returns></returns>
    Property BookOriginXpo As XPCollection

    ''' <summary>
    ''' Libro destino
    ''' </summary>
    ''' <returns></returns>
    Property BookDestinationId As Integer?

    ''' <summary>
    ''' Datasource libro destino
    ''' </summary>
    ''' <returns></returns>
    Property BookDestinationXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Tipo comprobante incial
    ''' </summary>
    ''' <returns></returns>
    Property CodeInitialJournalVoucherType As String

    ''' <summary>
    ''' Datasource tipo comprobante inicial
    ''' </summary>
    ''' <returns></returns>
    Property JournalVoucherTypeInitialXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Tipo comprobante final
    ''' </summary>
    ''' <returns></returns>
    Property CodeEndJournalVoucherType As String

    ''' <summary>
    ''' Datasource tipo comprobante final
    ''' </summary>
    ''' <returns></returns>
    Property JournalVoucherTypeEndXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

#End Region

End Interface
