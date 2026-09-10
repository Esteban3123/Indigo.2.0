'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Jeisson Herrera Peña
' Created          : 13-04-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
#End Region

''' <summary>
''' Define las propiedades y métodos de la vista
''' </summary>
Public Interface IGenerealLedgerIVA
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el Código del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Property Code As String

    ''' <summary>
    ''' Obtiene o asigna el Nombre del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Property GLedgerIVAName As String

    ''' <summary>
    ''' Obtiene o asigna el Porcentaje del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Property Percentage As Decimal

    ''' <summary>
    ''' Obtiene o asigna el Estado del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Property Status As Boolean

    ''' <summary>
    ''' Propiedad que contiene el listado de Cuentas IVA Compra/Servicio xpo
    ''' </summary>
    Property AccountsXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de Cuentas IVA Compra/Servicio xpo
    ''' </summary>
    Property AccountSaleXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de Cuentas control fiscal debito
    ''' </summary>
    Property AccountDebitControlFiscalXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de Cuentas control fiscal credito
    ''' </summary>
    Property AccountCreditControlFiscalXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccountPurchaseService As Integer?

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccountSale As Integer?

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccountDebitControlFiscal As Integer?

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccountCreditControlFiscal As Integer?


    ''' <summary>
    ''' Obtiene o asigna la configuración númerica asignada al Funcional
    ''' </summary>
    ''' <value>Configuración de Secuencia Numérica</value>
    ''' <returns>La configuración de secuencia numérica</returns>
    Property Sequence As GeneralLedgerSequence

    ''' <summary>
    ''' Obtiene o asigna el Tag del Funcional
    ''' </summary>
    ''' <value>Tag del Funcional</value>
    ''' <returns>El Tag del Funcional</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' propiedad que identifica si aplica o no devolucion del IVA
    ''' </summary>
    ''' <returns></returns>
    Property ApplyTaxDevolution As Boolean

    ''' <summary>
    ''' datasource que lista los metodos de pagos
    ''' </summary>
    ''' <returns></returns>
    Property DatasourcePaymentMethodTypes As List(Of Tuple(Of Byte, String))

#End Region

End Interface
