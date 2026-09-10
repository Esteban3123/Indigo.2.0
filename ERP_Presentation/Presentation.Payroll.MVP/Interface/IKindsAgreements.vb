'***********************************************************************
' Assembly         : Presentacion.Payrol.MVP
' Author           : Rafael Patiño
' Created          : 07-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Presentation.Base
#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IKindsAgreements
    Inherits IcrudBase

#Region "Propiedades"

    ''' <summary>
    ''' Esta propiedad contiene el codigo de la clase convenio
    ''' </summary>
    Property CodeKindsAgreements As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la clase de convenio
    ''' </summary>
    Property NameKindsAgreements As String
    ''' <summary>
    ''' propiedad que contiene el estado de la clase de convenio
    ''' </summary>
    Property StatusKindsAgreements As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Property ExpenseConceptDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' propiedad que contiene el afecta cuenta por cobrar
    ''' </summary>
    Property AffectsAccountsReceivable As Boolean

    ''' <summary>
    ''' propiedad que contiene el reclasifica cuenta por cobrar
    ''' </summary>
    Property ReclassifyAccountsReceivable As Boolean

    ''' <summary>
    ''' Obtiene o establece Concepto de cuenta por cobrar
    ''' </summary>
    Property AccountReceivableConceptDatasource As XPInstantFeedbackSource


#End Region
End Interface
