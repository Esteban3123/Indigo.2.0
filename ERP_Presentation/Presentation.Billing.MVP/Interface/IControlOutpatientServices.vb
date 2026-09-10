'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 28-10-2014
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
Imports Presentation.Controls
#End Region

Public Interface IControlOutpatientServices
    Inherits ICrudBase

#Region "Form"
    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean
#End Region

#Region "DataSource"
    ''' <summary>
    '''  propiedad para establcer el datasource de centros de atencion de crystal
    ''' </summary>
    ''' <returns></returns>
    Property CareCenterXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property FunctionalUnitXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property HealtAdministratorXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property CareGroupXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property SMLVXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property HemocomponentXPO As XPInstantFeedbackSource
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ProfessionalXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de consultorios
    ''' </summary>
    ''' <returns></returns>
    Property ConsultingRoomXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de actividad de agendamientio
    ''' </summary>
    ''' <returns></returns>
    Property ScheduleActivityOtherXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de tipo de ingreso
    ''' </summary>
    ''' <returns></returns>
    Property AdmissionTypeDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de  Vías Ingreso Servicios de Salud
    ''' </summary>
    ''' <returns></returns>
    Property EntryRoutesHealthServicesDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de Finalidades tecnologías de la salud
    ''' </summary>
    ''' <returns></returns>
    Property HealthPurposesDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de Modalidades de Atención
    ''' </summary>
    ''' <returns></returns>
    Property AdmissionModalitiesDatasource As XPInstantFeedbackSource


#End Region

#Region "ViewFields"
    ''' <summary>
    ''' id del centro de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CareCenterId As String

    Property FunctionalUnitId As Integer?

    Property HealthAdministratorId As Integer?

    Property CareGroupId As Integer?

    Property Dispatched As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property RequestHemoReserve As Integer?

    ''' <summary>
    ''' tipo de actividad de la cita
    ''' </summary>
    ''' <returns></returns>
    Property ActivityType As Integer

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property HemocomponentId As Integer?
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ProfessionalId As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property HemoQuantity As Integer

    ''' <summary>
    ''' almacena los permisos del form
    ''' </summary>
    ''' <returns></returns>
    Property PermissionsForm As Dictionary(Of Integer, String)

    ''' <summary>
    ''' Bnadera que establece si el sistema es impuesto incluido o no
    ''' </summary>
    Property FlagTaxInclude As Boolean

    ''' <summary>
    ''' Codigo del consultorio (cuando la actividad es de tipo "Otro")
    ''' </summary>
    ''' <returns></returns>
    Property ConsultingRoom As String

    ''' <summary>
    ''' codigo de la actividad de agendamiento
    ''' </summary>
    ''' <returns></returns>
    Property ScheduleActivityCode(Optional NullText As String = Nothing) As String

    ''' <summary>
    ''' Tipo de ingreso
    ''' </summary>
    ''' <returns></returns>
    Property AdmissionType As Integer?

    ''' <summary>
    '''  Vías Ingreso Servicios Salud
    ''' </summary>
    ''' <returns></returns>
    Property EntryRoutesHealthServices As Integer?

    ''' <summary>
    '''  Finalidades tecnologías de la salud
    ''' </summary>
    ''' <returns></returns>
    Property HealthPurposes As Integer?

    ''' <summary>
    ''' Modalidades de Atención
    ''' </summary>
    ''' <returns></returns>
    Property AdmissionModalities As Integer?


#End Region

End Interface
