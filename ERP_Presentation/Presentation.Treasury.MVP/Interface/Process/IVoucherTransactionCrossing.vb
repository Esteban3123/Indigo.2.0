'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 23-09-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports DevExpress.Data.Linq
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region

Public Interface IVoucherTransactionCrossing
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
    Property Sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Consecutivo
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value>
    ''' The third party identifier.
    ''' </value>
    Property ThirdPartyId As Integer

    ''' <summary>
    ''' Tipo de cruce
    ''' </summary>
    ''' <value>
    ''' The type of the crossing.
    ''' </value>
    Property CrossingType As Byte

    ''' <summary>
    ''' Obtiene o establece el detalle del cruce
    ''' </summary>
    ''' <value>
    ''' The detail.
    ''' </value>
    Property Detail As String

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value>
    ''' The status.
    ''' </value>
    Property Status As String

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value>
    ''' The document date.
    ''' </value>
    Property DocumentDate As DateTime

    ''' <summary>
    ''' Obtiene o establece el datasource de terceros
    ''' </summary>
    ''' <value>
    ''' The third party datasource.
    ''' </value>
    Property ThirdPartyDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas por pagar
    ''' </summary>
    ''' <value>
    ''' The account payable datasource.
    ''' </value>
    Property AccountPayableDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas por cobrar
    ''' </summary>
    ''' <value>
    ''' The account payable datasource.
    ''' </value>
    Property AccountRecivableDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property IdCashFlowConceptCxC As Integer?
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property IdCashFlowConceptCxP As Integer?
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property CashFlowConceptCodeNameOther As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property CashFlowConceptIdOther As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la moneda
    ''' </summary>
    Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer?

    ''' <summary>
    ''' Codigo standart de la moneda ISO4217
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property CurrencyAbbreviation As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property CashFlowConceptCxCDataSource As DevExpress.Xpo.XPInstantFeedbackSource
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property CashFlowConceptCxPDataSource As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Sub CleanControls()


#End Region

End Interface
