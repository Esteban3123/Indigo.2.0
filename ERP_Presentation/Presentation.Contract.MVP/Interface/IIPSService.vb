'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports DevExpress.Xpo
Imports Presentation.Base
#End Region

Public Interface IIPSService
    Inherits ICrudBase
    ''' <summary>
    ''' Codigo del servicio IPS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String
    ''' <summary>
    ''' Nombre del servicio IPS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ServiceName As String
    ''' <summary>
    ''' Tipo de manual 1 - ISS 2001 , 2 - ISS 2004 , 3 - SOAT, 4 - Institucional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ServiceManual As Integer?
    ''' <summary>
    ''' Clase de servicio 1 - Ninguno , 2 - Cirujano , 3 - Anesteciologo , 4 - Ayudante , 5 - Derecho Sala , 6 - Materiales Sutura , 7 - Instrumentacion Quirurgica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ServiceClass As Integer?
    ''' <summary>
    ''' Tipo de servicio 1 - Ninguno , 2 - Diagnostico , 3  -Terapeutico , 4 - Proteccion Especifica , 5 - Deteccion temprana enfermedad general , 6 - Deteccion temprana enfermedad profesional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ServiceType As Integer?
    ''' <summary>
    ''' Presentacion del producto 1 - No quirurgico , 2 - Quirurgico , 3 - Paquete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PresentationProduct As Integer?
    ''' <summary>
    ''' Establece el nivel de autorizacion en rango es de uno a nueve
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AuthorizationLevel As Integer
    ''' <summary>
    ''' Semanas cotizadas para acceder al servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContributionsWeeks As Integer
    ''' <summary>
    ''' Procedimiento del servicio 1 - Diagnostico , 2 - Laboratorio , 3 - Odontologia , 4 - Consulta - Urgencias , 5 - Hospitalizacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Procedure As Integer?
    ''' <summary>
    ''' Codigo subatencion 1 - Ninguno,2 - Estancia Individual,3 - Habitacion compartida,4 - UCI Adultos,5 - UCI Neonatal,6 - UCI Cuidados Medianos,7 - Incubadora,8 - Consulta Medica General,9 - Consulta pecialista,10 - Interconsulta,11 - Visitas Hospitalarias,12 - Honorarios Cirujanos,13 - Honorarios Anestesia,14 - Honorarios Ayudantia,15 - Honorarios Instrumentacion,16 - Derechos Sala,17 - Derecho Anestesia,18 - Derecho Equipo,19 - Insumos Hospitalarios,20 - Material Quirurgico,21 - Medicamentos,22 - Oxigeno,23 - Laboratorio,24 - Radiologia,25 - Tomografias,26 - Medicina Nuclear,27 - Resonancia Magnetica,28 - Examenes Complementarios,29 - Examenes Vasculares,30 - Hemodinamia,31 - Banco Sangre,32 - Terapias,33 - Ambulancia,34 - Factura Integral
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SubattentionCode As Integer?
    ''' <summary>
    ''' Unidad de medida para la edad minima 1 - Años,2 - Meses,3 - Dias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MinimunAgeUnit As Integer?
    ''' <summary>
    ''' Edad minima para este servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MinimunAge As Integer
    ''' <summary>
    ''' Unidad de medida para la edad maxima 1 - Años,2 - Meses,3 - Dias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MaximumAgeUnit As Integer?
    ''' <summary>
    ''' Edad Maxima para acceder al servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MaximumAge As Integer
    ''' <summary>
    ''' campo para identificar si se aplica al sexo masculino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InMale As Boolean?
    ''' <summary>
    ''' campo para identificar si se aplica al sexo femenino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InFemale As Boolean?
    ''' <summary>
    ''' Especifica si el servicio corresponde a un parto o aun aborto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ChildbirthAbortion As Boolean?
    ''' <summary>
    ''' Especifica si el servicio petenece al POS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property POS As Boolean?
    ''' <summary>
    ''' Establece el nivel de complejidad 1 - Baja,2 - Media,3 - Alta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ComplexityLevel As Integer?
    ''' <summary>
    ''' Establece si es de promocion y prevención
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PromotionAndPrevention As Boolean?
    ''' <summary>
    ''' Actividades del servicio de promocion y prevencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PromotionAndPreventionActivities As String
    ''' <summary>
    ''' Establece que es una cirugia artroscopica, solo puede ser artroscopica si la presentacion es quirurgica o paquete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SurgeryArtroscopica As Boolean?
    ''' <summary>
    ''' establece si es un servicio de patologia, solo puese ser patologica si el procedimiento es Laboratorio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PathologyService As Boolean?
    ''' <summary>
    ''' estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Status As Boolean
    ''' <summary>
    ''' datasource de entidades CUPS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CupsEntityXPO As DevExpress.Xpo.XPInstantFeedbackSource
    ''' <summary>
    ''' datasource de servicios IPS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ServicesIPSXPO As DevExpress.Xpo.XPInstantFeedbackSource
    ''' <summary>
    ''' definicion del layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As Presentation.Controls.IndigoLayoutControl
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
    Property SurgicalGroupXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el valor uvr
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UVRNumber As Integer

    ''' <summary>
    ''' Obtiene o establece el puntaje
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Score As Integer

    ''' <summary>
    ''' Obtiene o establece la liquidacion de ingresos ambulatorios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OutPatientRecoveryFeeType As Integer?

    ''' <summary>
    ''' Obtiene o establece la liquidacion de ingresos hospitalarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InPatientRecoveryFeeType As Integer?

    ''' <summary>
    ''' Obtiene o establece si aplica cambio para puntaje > 450 UVR
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ApplyChangeScore As Boolean?

    ''' <summary>
    ''' Obtiene o establece el puntaje si es mayor a 450 UVR
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NewScore As Integer

    ''' <summary>
    ''' Obtiene o establece el id del material asociado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AssociatedMaterialIPSServiceId As Integer?

    ''' <summary>
    ''' Establece el datasource del material asociado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AssociatedMaterialIPSServiceIdXpo As XPInstantFeedbackSource

    Property ListGeneralLedgerIva As XPInstantFeedbackSource

End Interface
