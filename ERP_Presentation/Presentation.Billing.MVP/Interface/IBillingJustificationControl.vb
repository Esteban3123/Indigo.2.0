'***********************************************************************
' Assembly         : Presentacion.JustificationControl.MVP
' Author           : Cristian Camilo Bahamón Castaño
' Created          : 15-07-2022
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IBillingJustificationControl

    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ''' <summary>
    ''' Esta Propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna el tag del funcional
    ''' </summary>
    ''' <value>Tag del fucnional</value>
    ''' <returns></returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.BillingSequence

    ''' <summary>
    ''' Obtiene o asigna el codigo de la cuenta hospitalaria
    ''' </summary>
    ''' <value>Codigo de la cuenta hospitalaria</value>
    ''' <returns>El codigo de la cuenta hospitalaria</returns>
    Property Code As String

    ''' <summary>
    ''' Obtiene o asigna una descripcion de la cuenta hospitalaria
    ''' </summary>
    ''' <value>Descripcion de la cuenta hospitalaria</value>
    ''' <returns>La descripcion de la cuenta hospitalaria</returns>
    Property Description As String

    ''' <summary>
    ''' Obtiene o asigna una observacion de la cuenta hospitalaria
    ''' </summary>
    ''' <value>Observacion de la cuenta hospitalaria</value>
    ''' <returns>La observacion de la cuenta hospitalaria</returns>
    Property Observation As String

    ''' <summary>
    ''' Obtiene o asigna un valor que indique si se le omite la liquidacion o no de la cuenta hospitalaria
    ''' </summary>
    ''' <value>Omitir liquidacion</value>
    ''' <returns>Omitir Liquidacion</returns>
    Property SkipClearance As Boolean

    ''' <summary>
    ''' Obtiene o establece el id del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdUser As Integer?

    ''' <summary>
    ''' Establece el datasource de usuarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UserXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource



#End Region


End Interface
