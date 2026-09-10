#Region "Imports"

Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Domain.Entities

#End Region

Public Interface IElectronicDocumentTraceability

#Region "Fields"

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

#End Region

#Region "Properties"

    ''' <summary>
    ''' Unidad Operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property OperatingUnitId As Integer

    ''' <summary>
    ''' Estado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Status As String

#End Region

#Region "XPO"

    ''' <summary>
    ''' Datasource de las Unidades Operativas
    ''' </summary>
    ''' <returns></returns>
    Property OperatingUnitXpo As List(Of OperatingUnit)

    ''' <summary>
    ''' Datasource de las facturas
    ''' </summary>
    ''' <returns></returns>
    Property InvoiceXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de las Notas Debitos
    ''' </summary>
    ''' <returns></returns>
    Property DebitNoteXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de las Notas Creditos
    ''' </summary>
    ''' <returns></returns>
    Property CreditNoteXpo As XPInstantFeedbackSource

#End Region

End Interface
