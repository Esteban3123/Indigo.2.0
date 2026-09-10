'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 17-03-2014
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

#End Region

Public Interface IThirdPartyAccount
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece la cabecera de la secuencia
    ''' </summary>
    ''' <value>
    ''' The sequence.
    ''' </value>
    Property Sequence As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Obtiene o establece el codigo de las cuentas corrientes
    ''' </summary>
    ''' <value>
    ''' The code third party account.
    ''' </value>
    Property CodeThirdPartyAccount As String

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value>
    ''' The description.
    ''' </value>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece
    ''' </summary>
    ''' <value>
    ''' The type.
    ''' </value>
    Property Type As String

    ''' <summary>
    ''' Obtiene o establece el numero
    ''' </summary>
    ''' <value>
    ''' The number.
    ''' </value>
    Property Number As String

    ''' <summary>
    ''' Obtiene o establece el tercero
    ''' </summary>
    ''' <value>
    ''' The third party.
    ''' </value>
    Property ThirdParty As Integer

    ''' <summary>
    ''' Obtiene o establece el banco
    ''' </summary>
    ''' <value>
    ''' The bank.
    ''' </value>
    Property Bank As Integer

    ''' <summary>
    ''' Obtiene o establece la ciudad de radicacion
    ''' </summary>
    ''' <value>
    ''' The city.
    ''' </value>
    Property City As Integer

    ''' <summary>
    ''' Obtiene o establece la ciudad bancaria
    ''' </summary>
    ''' <value>
    ''' The bank city.
    ''' </value>
    Property BankCity As Integer

    ''' <summary>
    ''' Gets or sets a value indicating whether [state].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state]; otherwise, <c>false</c>.
    ''' </value>
    Property State As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource de terceros
    ''' </summary>
    ''' <value>
    ''' The third party datasource.
    ''' </value>
    Property ThirdPartyDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o establece los datasources de los bancos
    ''' </summary>
    ''' <value>
    ''' The bank datasource.
    ''' </value>
    Property BankDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de ciudades
    ''' </summary>
    ''' <value>
    ''' The city datasource.
    ''' </value>
    Property CityDatasource As XPInstantFeedbackSource

    ''' <summary>
    '''  Obtiene o establece el datasource de ciudades bancarias
    ''' </summary>
    ''' <value>
    ''' The bank city datasource.
    ''' </value>
    Property BankCityDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
