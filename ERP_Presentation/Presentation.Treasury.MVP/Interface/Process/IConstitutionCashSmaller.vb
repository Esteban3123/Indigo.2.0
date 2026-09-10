'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/01/2017
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.TreasuryRepository

#End Region

Public Interface IConstitutionCashSmaller
    Inherits ICrudBase

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
    ''' Obtiene o establece el codigo del comprobante de egreso
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    Property DocumentDate As Date?

    ''' <summary>
    ''' Tipo documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentType As Integer

    ''' <summary>
    ''' Id caja menor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CashRegisterSmallerId As Integer

    ''' <summary>
    ''' Datasource caja menor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CashRegisterSmallerXpo As XPInstantFeedbackSource

    Property SelectedCashRegisterSmaller As CashRegisterXpo

    ''' <summary>
    ''' Tipo fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SourceType As Integer

    ''' <summary>
    ''' Id caja mayor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CashRegisterId As Integer?

    ''' <summary>
    ''' Datasource caja mayor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CashRegisterXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id banco
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EntityBankAccountId As Integer?

    ''' <summary>
    ''' Datasource banco
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EntityBankAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el valor del comprobante
    ''' </summary>
    Property Value As Decimal

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface