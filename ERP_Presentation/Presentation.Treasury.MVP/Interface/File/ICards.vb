'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 18-03-2014
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
Imports Infrastructure.Data.Xpo.TreasuryRepository

#End Region

Public Interface ICards
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
    ''' Gets my layout control.
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
    ''' Obtiene o establece el codigo de tarjetas
    ''' </summary>
    ''' <value>
    ''' The code cards.
    ''' </value>
    Property Code As String

    ''' <summary>
    ''' Gets or sets the name cards.
    ''' </summary>
    ''' <value>
    ''' The name cards.
    ''' </value>
    Property NameCard As String

    ''' <summary>
    ''' Gets or sets the third party cards.
    ''' </summary>
    ''' <value>
    ''' The third party cards.
    ''' </value>
    Property IdThirdParty As Integer

    ''' <summary>
    ''' Gets or sets the commission cards.
    ''' </summary>
    ''' <value>
    ''' The commission cards.
    ''' </value>
    Property IdCashReceiptConceptCommision As Integer


    ''' <summary>
    ''' Gets or sets the commission account.
    ''' </summary>
    ''' <value>
    ''' The commission account.
    ''' </value>
    Property IdRetentionConceptCommision As Integer

    ''' <summary>
    ''' Gets or sets the ica account.
    ''' </summary>
    ''' <value>
    ''' The ica account.
    ''' </value>
    Property IdCashReceiptConceptRTF As Integer

    ''' <summary>
    ''' Gets or sets the identifier retention concept RTF.
    ''' </summary>
    ''' <value>
    ''' The identifier retention concept RTF.
    ''' </value>
    Property IdRetentionConceptRTF As Integer

    ''' <summary>
    ''' Gets or sets the concept ret.
    ''' </summary>
    ''' <value>
    ''' The concept ret.
    ''' </value>
    Property IdCashReceiptConceptICA As Integer
    ''' <summary>
    ''' Gets or sets the identifier retention concept ica.
    ''' </summary>
    ''' <value>
    ''' The identifier retention concept ica.
    ''' </value>
    Property IdRetentionConceptICA As Integer
    ''' <summary>
    ''' Obtiene o establece el estado de los registros
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state cards]; otherwise, <c>false</c>.
    ''' </value>
    Property Status As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource de terceros
    ''' </summary>
    ''' <value>
    ''' The third party datasource.
    ''' </value>
    Property ThirdPartyXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para conceptos de retención
    ''' </summary>
    ''' <value>
    ''' The retention concept datasource.
    ''' </value>
    Property CashReceiptConceptIcaXPO As DevExpress.Data.Linq.LinqInstantFeedbackSource

    Property CashReceiptConceptRtfXPO As DevExpress.Data.Linq.LinqInstantFeedbackSource

    Property CashReceiptConceptCommisionXPO As XPCollection(Of CashReceiptConceptXpo)

    Property RetentionConceptCommisionXPO As DevExpress.Xpo.XPInstantFeedbackSource

    Property RetentionConceptRTFXPO As DevExpress.Xpo.XPInstantFeedbackSource

    Property RetentionConceptICAXPO As DevExpress.Xpo.XPInstantFeedbackSource
    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
