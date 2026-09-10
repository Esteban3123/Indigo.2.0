'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 07-10-2014
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

Public Interface IConsignmentTransfer
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    Property Sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Consecutivo
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    Property DocumentDate As DateTime

    ''' <summary>
    ''' Obtiene o establece el id de cuentas bancarias
    ''' </summary>
    Property EntityBankAccountId As Integer

    ''' <summary>
    ''' Obtiene o establece el id de cuentas contables
    ''' </summary>
    Property MainAccountId As Integer

    ''' <summary>
    ''' Obtiene o establece el id de centros de costo
    ''' </summary>
    Property CostCenterId As Integer?

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el valor de la consignacion / traslado
    ''' </summary>
    Property Value As Decimal

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Property Status As String

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas bancarias
    ''' </summary>
    Property EntityBankAccountDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de los centros de costo
    ''' </summary>
    Property CostCenterDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource de las cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Property CashDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Sub CleanControls()

#End Region

End Interface