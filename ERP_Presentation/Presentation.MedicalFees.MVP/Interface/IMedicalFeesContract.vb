'***********************************************************************
' Assembly         : Presentacion.MedicalFees.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
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

Public Interface IMedicalFeesContract
    Inherits IcrudBase

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Integer

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' Obtiene o establece el tipo de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractType As Integer?

    ''' <summary>
    ''' Obtiene o establece el nombre del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractName As String

    ''' <summary>
    ''' Obtiene o establece el numero del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractNumber As String

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
    ''' Obtiene o establece las observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observations As String

    ''' <summary>
    ''' Obtiene o establece el tipo de excepcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExceptionType As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la entidad cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CUPSEntityId As Integer

    ''' <summary>
    ''' Establece el datasource de la entidad cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CUPSEntityXpo As XPCollection

    ''' <summary>
    ''' Obtiene o establece el id del subgrupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CUPSSubgroupId As Integer

    ''' <summary>
    ''' Establece el datasource del subgrupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CUPSSubgroupXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CUPSGroupId As Integer

    ''' <summary>
    ''' Establece el datasource del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CUPSGroupXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del grupo de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CareGroupId As Integer

    ''' <summary>
    ''' Establece el datasource del grupo de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CareGroupXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractEntityId As Integer

    ''' <summary>
    ''' Establece el datasource de los contratos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractEntityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el tipo de manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualType As Integer

    ''' <summary>
    ''' Obtiene o establece el tipo de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateTypePopup As Integer

    ''' <summary>
    ''' Obtiene o establece el porcentaje
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PercentageRatePopup As Decimal

    ''' <summary>
    ''' Obtiene o establece el id del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualIdPopup As Integer

    ''' <summary>
    ''' Establece el datasource del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualXpoPopup As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece la variacion de la tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateVariationPopup As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor fijo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AmountPayablePopup As Integer

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.MedicalFeesSecuence

    ''' <summary>
    ''' Obtiene o establece la fecha de ultima liquidacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LastLiquidationDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece si especifica si se realiza descuento por aceptaciones de IPS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AutomaticDiscountForObjections As Boolean?

    ''' <summary>
    ''' Obtiene o establece el id de la linea de distribucion proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierDistributionLineId As Integer?

    ''' <summary>
    ''' Establece el datasource de la linea de distribucion proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierDistributionLineXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IPSServiceId As Integer

    ''' <summary>
    ''' Establece el datasource del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IPSServiceXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de la rejilla de tipos de liquidacion
    ''' dependiendo del tipo que escojan
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataSourceGridControls As XPCollection

    ''' <summary>
    ''' Establece el datasource de los médicos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HealthProfessionalXpo As XPInstantFeedbackSource

End Interface
