'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/09/2014
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

Public Interface IPopupCareGroup
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene o establece el tipo de liquidacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LiquidationType As Integer

    ''' <summary>
    ''' Obtiene o establece el tipo de unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UnitTypeId As Integer

    ''' <summary>
    ''' Obtiene o establece la fecha inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InitialDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece la fecha final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EndDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece el id del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualId As Integer

    ''' <summary>
    ''' establece el datasource del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el porcentaje de la variacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateVariation As Decimal

    ''' <summary>
    ''' Obtiene o establece el codigo(Id) de la especialidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyId As String

    ''' <summary>
    ''' Establece el datasource de especialidades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyXpo As XPCollection

    ''' <summary>
    ''' Obtiene o establece el id de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitId As Integer

    ''' <summary>
    ''' Establece el datasource de las unidades funcionales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitXpo As XPCollection

    ''' <summary>
    ''' Obtiene o establece el valor del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValue As Decimal

    ''' <summary>
    ''' Obtiene o establece el tipo de manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ManualType As Integer

    ''' <summary>
    ''' Obtiene o establece el valor del recargo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValueWithSurcharge As Decimal

    ''' <summary>
    ''' Obtiene o establece si permite cambiar valores cuando se esta facturando
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AllowValueChange As Boolean

End Interface
