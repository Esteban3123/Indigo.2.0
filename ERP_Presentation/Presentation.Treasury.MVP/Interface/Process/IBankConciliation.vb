#Region "Imports"

Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IBankConciliation
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequence As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Obtiene o establece el codigo del comprobante de egreso
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Id de la cuenta bancaria
    ''' </summary>
    ''' <returns></returns>
    Property EntityBankAccountId As Integer?

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    ''' <returns></returns>
    Property DocumentDate As Date?

    ''' <summary>
    ''' Saldo final libros
    ''' </summary>
    ''' <returns></returns>
    Property EntityBankAccountValue As Decimal

    ''' <summary>
    ''' Saldo final extracto
    ''' </summary>
    ''' <returns></returns>
    Property ExtractValue As Decimal

    ''' <summary>
    ''' Diferencia a conciliar
    ''' </summary>
    ''' <returns></returns>
    Property DifferenceReconcile As Decimal

#Region "Extract Details"

    ''' <summary>
    ''' Tipo de Documento del detalle
    ''' </summary>
    ''' <returns></returns>
    Property ExtractDocumentType As Byte?

    ''' <summary>
    ''' Fecha del detalle
    ''' </summary>
    ''' <returns></returns>
    Property ExtractDocumentDate As Date?

    ''' <summary>
    ''' Detalle
    ''' </summary>
    ''' <returns></returns>
    Property ExtractDescription As String

    ''' <summary>
    ''' Detalle
    ''' </summary>
    ''' <returns></returns>
    Property ExtractDocumentNumber As String

    ''' <summary>
    ''' Naturaleza del detalle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExtractNature As Byte?

    ''' <summary>
    ''' Valor del detalle
    ''' </summary>
    ''' <returns></returns>
    Property ExtractDocumentValue As Decimal

#End Region

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Datasource de la cuenta bancaria
    ''' </summary>
    ''' <returns></returns>
    Property EntityBankAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de los tipos de documentos
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property FillingDocumentType As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Datasource de las naturalezas
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property FillingNature As List(Of Tuple(Of Byte, String))

#End Region

#Region "Methods"

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface