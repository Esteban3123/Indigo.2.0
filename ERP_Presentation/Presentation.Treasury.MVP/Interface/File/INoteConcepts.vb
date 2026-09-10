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
Imports Presentation.Controls

#End Region

Public Interface INoteConcepts
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
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
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value>
    ''' The code note concepts.
    ''' </value>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del concepto
    ''' </summary>
    ''' <value>
    ''' The description.
    ''' </value>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece si afecta presupuesto
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [affect budget]; otherwise, <c>false</c>.
    ''' </value>
    Property AffectBudget As Byte?

    ''' <summary>
    ''' Obtiene o establece si tiene recaudo automatico
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [automatic collection]; otherwise, <c>false</c>.
    ''' </value>
    Property AutomaticCollection As Byte?

    ''' <summary>
    ''' Obtiene o establece la naturaleza de la cuenta
    ''' </summary>
    ''' <value>
    ''' The character.
    ''' </value>
    Property Character As Integer

    ''' <summary>
    ''' Obtiene o establece la cuenta contable
    ''' </summary>
    ''' <value>
    ''' The account accounting.
    ''' </value>
    Property AccountAccounting As Integer

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state note concepts]; otherwise, <c>false</c>.
    ''' </value>
    Property StateNoteConcepts As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas contables
    ''' </summary>
    ''' <value>
    ''' The account accounting.
    ''' </value>
    Property AccountAccountingDatasource As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' obtiene o establece la afectacion del concepto de flujo de caja
    ''' </summary>
    ''' <returns></returns>
    Property AffectCashFlowConcept As Byte?

    ''' <summary>
    ''' obtiene o establece el id de concepto de flujo de caja
    ''' </summary>
    ''' <returns></returns>
    Property IdAffectCashFlowConcept As Integer?

    ''' <summary>
    ''' obtiene o establece si realiza reversion
    ''' </summary>
    ''' <returns></returns>
    Property Reversion As Byte?

    ''' <summary>
    ''' Establece/Obtiene el datasource de conceptos de flujo de efectivo
    ''' </summary>
    Property CashFlowConceptDataSource As DevExpress.Xpo.XPInstantFeedbackSource
#End Region

End Interface
