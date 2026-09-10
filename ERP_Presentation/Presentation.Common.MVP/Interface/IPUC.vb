'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 19-03-2013
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
Public Interface IPUC
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' contiene el estado de la cuenta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property State As Boolean

    ''' <summary>
    ''' contiene el codigo de la clase de cuenta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CodeAccountingClass As String

    ''' <summary>
    ''' Gets or sets the level xpo.
    ''' </summary>
    ''' <value>
    ''' The level xpo.
    ''' </value>
    Property LevelXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece la clases de cuentas contables
    ''' </summary>
    ''' <value>
    ''' The class acounting xpo.
    ''' </value>
    Property ClassAcountingXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Maneja base
    ''' </summary>
    ''' <returns></returns>
    Property HandleBase As Boolean

    ''' <summary>
    ''' Maneja restriccion de centro de costo
    ''' </summary>
    ''' <returns></returns>
    Property HandlesCostCenterRestriction As Boolean

    ''' <summary>
    ''' maneja restriccion de tercero
    ''' </summary>
    ''' <returns></returns>
    Property HandlesThirdPartyRestriction As Boolean

#End Region

End Interface
