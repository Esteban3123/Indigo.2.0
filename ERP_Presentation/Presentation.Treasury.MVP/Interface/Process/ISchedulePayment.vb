'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 21-08-2014
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
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Interface ISchedulePayment
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el parametro de de pagos
    ''' </summary>
    ''' <value>
    ''' The payment concept datasource.
    ''' </value>
    Property PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo

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
    ''' Obtiene o establece el codigo de la programacion de pagos
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la fecha de la programación de pagos
    ''' </summary>
    ''' <value>
    ''' The scheduled date.
    ''' </value>
    Property ScheduledDate As Date

    ''' <summary>
    ''' Obtiene o establece el estado de la programación de pagos
    ''' </summary>
    ''' <value>
    ''' The status.
    ''' </value>
    Property Status As String

    ''' <summary>
    ''' obtiene o establece el datasource de la programacion de pagos
    ''' </summary>
    ''' <value>
    ''' The schedule payment datasource.
    ''' </value>
    Property SchedulePaymentDatasource As List(Of SP_SchedulePayment_Result)

    ''' <summary>
    ''' Corresponde al datasource inicial (el de mas cantidad de items, este se puede modificar a medida que se modifican los items)
    ''' </summary>
    ''' <value>
    ''' The original datasource.
    ''' </value>
    Property OriginalDatasource As List(Of SP_SchedulePayment_Result)


    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
