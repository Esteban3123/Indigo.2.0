'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 23-04-2015
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

Public Interface IBillingAuthorization
    Inherits IcrudBase

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
    ''' Obtiene o establece el consecutivo de la autorizacion de facturación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la autorización de facturación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillingAuthorizationName As String

    ''' <summary>
    ''' Obtiene o establece el Numero de la resolución
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResolutionNumber As String

    ''' <summary>
    ''' Obtiene o establece la Fecha de la resolución
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResolutionDate As Date

    ''' <summary>
    ''' Obtiene o establece el prefijo de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoicePrefix As String

    ''' <summary>
    ''' Obtiene o establece la Factura Inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InitialInvoice As Long

    ''' <summary>
    ''' Obtiene o establece la factura final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FinalInvoice As Long

    ''' <summary>
    ''' Obtiene o establece la fecha de vigencia Inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InitialDate As Date?

    ''' <summary>
    ''' Obtiene o establece la fecha de vigencia final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FinalDate As Date?

    ''' <summary>
    ''' Obtiene o establece el tipo de factura
    ''' </summary>
    ''' <value>1 - Digitalizada 2 - Manual</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceType As Integer?

    ''' <summary>
    ''' Obtiene o establece el consecutivo actual de la autorizacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Consecutive As Long

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Establece el datasource de usuarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UserXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdUser As Integer?

    ''' <summary>
    ''' Obtiene o establece la clave tecnica usada para la facturación electrónica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TechnicalKey As String

#End Region


End Interface
