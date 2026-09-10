'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/11/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo
#End Region

Public Interface IPatientExternalCareCenter
    'Inherits ICrudBase

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Layout del formulario
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
    ''' Obtiene o establece el número de identificación del paciente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdentificationNumber As String

    ''' <summary>
    ''' Obtiene o establece el tipo de identificación del paciente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdentificationType As Integer

    ''' <summary>
    ''' Obtiene o establece el nombre del paciente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Name As String

    ''' <summary>
    ''' Obtiene o establece el apellido del paciente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LastName As String

    ''' <summary>
    ''' Obtiene o establece el género del paciente
    ''' </summary>
    ''' <returns></returns>
    Property Gender As Integer

    ''' <summary>
    ''' Obtiene o establece el número de celular del apciente
    ''' </summary>
    ''' <returns></returns>
    Property PatientMobileNumber As String

    ''' <summary>
    ''' Obtiene o establece el Email del paciente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PatientEmail As String

    ''' <summary>
    ''' Obtiene o establece la unidad funcional del paciente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnit As String

    ''' <summary>
    ''' Obtiene o establece la cama del paciente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Bed As String

    ''' <summary>
    ''' Datasource de los tipo de identificación
    ''' </summary>
    ''' <returns></returns>
    Property IdentificationTypeDatasource As XPInstantFeedbackSource


    ''' <summary>
    ''' Datasource de los géneros
    ''' </summary>
    ''' <returns></returns>
    Property ListGenderDatasource As XPInstantFeedbackSource

End Interface
