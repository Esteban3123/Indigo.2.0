'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 18-07-2014
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

#End Region

Public Interface IRefund
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
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <value>
    ''' The date server.
    ''' </value>
    Property DateServer As DateTime

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Obtiene o establece el codigo del reembolso
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la caja del reembolso
    ''' </summary>
    ''' <value>
    ''' The identifier cash register.
    ''' </value>
    Property IdCashRegister As Integer

    ''' <summary>
    ''' Obtiene o establece la fecha inicial del reembolso
    ''' </summary>
    ''' <value>
    ''' The initial date.
    ''' </value>
    Property InitialDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece la fecha final del reembolso
    ''' </summary>
    ''' <value>
    ''' The final date.
    ''' </value>
    Property FinalDate As DateTime

    ''' <summary>
    ''' Obtiene o establece el valor del reembolso
    ''' </summary>
    ''' <value>
    ''' The value.
    ''' </value>
    Property Value As Decimal

    ''' <summary>
    ''' Obtiene o establece el detalle del reembolso
    ''' </summary>
    ''' <value>
    ''' The detail.
    ''' </value>
    Property Detail As String

    ''' <summary>
    ''' Obtiene o establece el estado del documento
    ''' </summary>
    ''' <value>
    ''' The status.
    ''' </value>
    Property Status As Byte

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Property CashDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
