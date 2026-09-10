'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/03/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IConceptsAccountsPayable
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad contiene el codigo de los bancos
    ''' </summary>
    Property CodeConceptsAccountsPayable As String

    ''' <summary>
    ''' Esta propiedad contiene el nombre de los bancos
    ''' </summary>
    Property NameConceptsAccountsPayable As String

    ''' <summary>
    ''' Esta propiedad contiene el concepto de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HandlesRetention As Boolean

    ''' <summary>
    ''' Esta propiedad contiene el concepto de maneja impuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HandleTaxes As Boolean

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RetentionConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RetentionConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Esta propiedad contiene si maneja causacion diferida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeferredCausation As Boolean

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

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
    Property Sequense As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property AccountsXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccount As Integer?

    ''' <summary>
    ''' Obtiene o establece si acumula para calculo presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccumulateBudgetCalculated As Boolean

    ''' <summary>
    ''' Obtiene o establece si libera recursos no ejecutados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FreeResourcesUnexecuted As Boolean

    ''' <summary>
    ''' Obtiene o establece el tipo de concepto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConceptType As Integer?

    ''' <summary>
    ''' Especifica si el concepto de retencion es de tipo empleado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EmployeeCategoryRetention As Boolean

    ''' <summary>
    ''' Concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThreeEightThreeRetentionConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThreeEightThreeRetentionConceptIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThreeEightFourRetentionConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThreeEightFourRetentionConceptIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccount383 As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccount384 As Integer?

    ''' <summary>
    ''' contiene listado de cuentas contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Account384Xpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' contiene listado de cuentas contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Account383Xpo As DevExpress.Xpo.XPInstantFeedbackSource

#End Region

End Interface
