'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
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

Public Interface ICupsEntity
    Inherits ICrudBase

#Region "Properties"
    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

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
    ''' Obtiene o establece la descripcion del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el codigo de rips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RipsCode As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de rips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RipsDescription As String

    ''' <summary>
    ''' Obtiene o establece el id del subgrupo cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCupsSubGroup As Integer?

    ''' <summary>
    ''' Establece el datasource del subgrupo cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CupsSubGroupXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del grupo de servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IPSServiceGroupId As Integer?

    ''' <summary>
    ''' Establece el datasource del grupo de servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IPSServiceGroupXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del grupo facturacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillingGroupId As Integer?

    ''' <summary>
    ''' Establece el datasource de grupo de facturacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillingGroupXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el codigo del concepto rips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RIPSConcept As String

    ''' <summary>
    ''' Establece el datasource de los servicios RIPS.
    ''' </summary>
    ''' <returns></returns>
    Property RIPSServicesXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del servicio RIPS
    ''' </summary>
    ''' <returns></returns>
    Property RIPSServicesId As Integer?

    ''' <summary>
    ''' Obtiene o establece el servico de oxigeno
    ''' </summary>
    ''' <returns></returns>
    Property OxigenServices As Boolean

    ''' <summary>
    ''' Permite saber si aplica a RIAS
    ''' </summary>
    ''' <returns></returns>
    Property ApplyRIAS As Boolean

    ''' <summary>
    ''' Obtiene o establece el id del concepto facturacion si aplica RIAS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RIASBillingConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource del concepto de facturacion si aplica RIAS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RIASBillingConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del grupo de servicio ips si aplica RIAS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RIASBillingGroupId As Integer?

    ''' <summary>
    ''' Establece el datasource del grupo de servicio ips si aplica RIAS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RIASBillingGroupXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' indica si el Cups Es un panel
    ''' </summary>
    ''' <returns></returns>
    Property IsPanel As Byte?

    ''' <summary>
    ''' Establece si el CUP requiere lateralidad
    ''' </summary>
    ''' <returns></returns>
    Property RequiresLateraly As Boolean?

	Sub AsyncLoader(State As Boolean)

    ''' <summary>
    ''' Establece si el procedimiento quirúrgico requiere o no informe
    ''' </summary>
    ''' <returns></returns>
    Property SurgicalReport As Boolean

    ''' <summary>
    ''' Establece si el servicio se muestra o no en la sección de ordenes médicas
    ''' </summary>
    ''' <returns></returns>
    Property ShowServiceMedicalOrder As Integer?

    ''' <summary>
    ''' Establece si se maneja Servicio de apoyo imagenológico a procedimientos
    ''' </summary>
    ''' <returns></returns>
    Property ImageGuidanceProcedures As Boolean?

#End Region
#Region "Datasource"

    ''' <summary>
    ''' datasource de entidades CUPS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CupsEntityXPO As List(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo)
#End Region
End Interface
