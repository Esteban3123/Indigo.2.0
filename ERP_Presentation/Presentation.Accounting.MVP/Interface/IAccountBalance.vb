'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 16-05-2014
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
#End Region

Public Interface IAccountBalance
    Inherits IcrudBase

#Region "Fields"

    ''' <summary>
    ''' Gets or sets the consecutive.
    ''' </summary>
    ''' <value>
    ''' The consecutive.
    ''' </value>
    Property Consecutive As String

    ''' <summary>
    ''' Gets or sets the i documente.
    ''' </summary>
    ''' <value>
    ''' The i documente.
    ''' </value>
    Property IDocumente As Integer
    ''' <summary>
    ''' Gets or sets the date document.
    ''' </summary>
    ''' <value>
    ''' The date document.
    ''' </value>
    Property DateDocument As DateTime
    ''' <summary>
    ''' Gets or sets the state.
    ''' </summary>
    ''' <value>
    ''' The state.
    ''' </value>
    Property State As Byte
    ''' <summary>
    ''' Gets or sets the observation.
    ''' </summary>
    ''' <value>
    ''' The observation.
    ''' </value>
    Property Observation As String
    ''' <summary>
    ''' Gets or sets the number document.
    ''' </summary>
    ''' <value>
    ''' The number document.
    ''' </value>
    Property NumberDocument As Integer

    ''' <summary>
    ''' Gets or sets the document type xpo.
    ''' </summary>
    ''' <value>
    ''' The document type xpo.
    ''' </value>
    Property DocumentTypeXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Codigo de 3 caracteres ISO 4217
    ''' </summary>
    ''' <returns></returns>
    Property CodeCurrency As String

    ''' <summary>
    ''' Id de la moenda del libro
    ''' </summary>
    ''' <returns></returns>
    Property BookCurrencyId As Integer?
#End Region

End Interface