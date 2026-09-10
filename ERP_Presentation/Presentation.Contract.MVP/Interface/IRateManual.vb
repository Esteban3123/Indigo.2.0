'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/10/2014
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

Public Interface IRateManual
    Inherits IcrudBase

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ReadOnly Property MyLayoutControl As IndigoLayoutControl

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
    ''' Obtiene o establece el consecutivo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NameRM As String

    ''' <summary>
    ''' Obtiene o establece el tipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Type As Integer?

    ''' <summary>
    ''' Obtiene o establece el metodo de redondeo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RoundService As Integer?

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.ContractSequence

    ''' <summary>
    ''' Obtiene o establece el id del salario minimo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractMinimumWageId As Integer?

    ''' <summary>
    ''' Establece el datasource del salario minimo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractMinimumWageXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de servicios ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IPSServiceXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establec el id del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IPSServiceId As Integer

    ''' <summary>
    ''' Obtiene o establece el valor del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValue As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor con recargo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValueWithSurcharge As Decimal
    ''' <summary>
    ''' Establece el datasource de servicios ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IPSServiceXpoSurgical As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establec el id del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IPSServiceIdSurgical As Integer

    ''' <summary>
    ''' Obtiene o establece el valor del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValueSurgical As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor con recargo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValueWithSurchargeSurgical As Decimal

    ''' <summary>
    ''' Obtiene o establece el id del grupo quirurgico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SurgicalGroupId As Integer?

    ''' <summary>
    ''' Establece el datasource del grupo quirurgico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SurgicalGroupXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del rango uvr
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UVRRangeId As Integer?

    ''' <summary>
    ''' Establece el datasource de rangos uvr
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UVRRangeXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el porcentaje de la sala que se va a cobrar cuando sea no cruento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PercentageNoBloodyRoom As Decimal?

    ''' <summary>
    ''' Obtiene o establece el id del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MaterialNoBloodyIPSServiceId As Integer?

    ''' <summary>
    ''' Establece el datasource del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MaterialNoBloodyIPSServiceIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece la propiedad   Liquidar todas las cirugías MIVIE. para manuales IIS
    ''' </summary>
    ''' <returns></returns>
    Property LiquidateAllMIVIE As Boolean?

End Interface
