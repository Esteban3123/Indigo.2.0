'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IEconomicIndicator
    Inherits IcrudBase
    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.PortfolioSequence
    ''' <summary>
    ''' Obtiene o establece el codigo del indicador economico
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Property Code As String
    ''' <summary>
    ''' obtiene o establece el año
    ''' </summary>
    ''' <value>
    ''' año
    ''' </value>
    Property Year As String

    ''' <summary>
    ''' obtine o establece el mes
    ''' </summary>
    ''' <value>
    ''' mes
    ''' </value>
    Property Month As String
    ''' <summary>
    ''' obtien o establece el interes financiero
    ''' </summary>
    ''' <value>
    ''' interes financiro
    ''' </value>
    Property FinancialInterests As Decimal

    ''' <summary>
    ''' obtiene o establece el interes de mora
    ''' </summary>
    ''' <value>
    ''' interes de mora
    ''' </value>
    Property LatePaymentInterest As Decimal

    ''' <summary>
    ''' obtiene o establece el estado del registro
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [status]; otherwise, <c>false</c>.
    ''' </value>
    Property Status As Boolean
    ''' <summary>
    ''' establece el estado de los controles
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean



End Interface
