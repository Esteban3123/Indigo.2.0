'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 27-10-2014
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
Imports Domain.Entities
Imports DevExpress.Data.Linq

#End Region

Public Interface ICashing
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
    ''' Obtiene o establece el id de comprobantes de egreso
    ''' </summary>
    Property VoucherTransactionId As Integer

    ''' <summary>
    ''' Obtiene o establece el codigo del comprobante de egreso
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la clase de comprobante
    ''' </summary>
    Property VoucherClass As Byte

    ''' <summary>
    ''' Obtiene o establece el tercero
    ''' </summary>
    Property IdThirdParty As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta bancaria
    ''' </summary>
    Property IdEntityBankAccount As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    Property MainAccountId As Integer

    ''' <summary>
    ''' Obtiene o establece el numero del cheque
    ''' </summary>
    Property CurrentCheck As Long

    ''' <summary>
    ''' Obtiene o establece el numero del cheque a reemplazar
    ''' </summary>
    Property NextCheck As Long

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    Property DocumentDate As Date

    ''' <summary>
    ''' Obtiene o establece el detalle
    ''' </summary>
    Property Detail As String

    ''' <summary>
    ''' Obtiene o establece el valor del comprobante
    ''' </summary>
    Property Value As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor de la tasa por mil
    ''' </summary>
    Property TaxByMil As Decimal?

    ''' <summary>
    ''' Obtiene o establece el estado del comprobante
    ''' </summary>
    Property Status As Byte

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource de comprobantes de egreso
    ''' </summary>
    ''' <value>
    ''' The voucher transaction datasource.
    ''' </value>
    Property VoucherTransactionDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Sub CleanControls()

#End Region

End Interface