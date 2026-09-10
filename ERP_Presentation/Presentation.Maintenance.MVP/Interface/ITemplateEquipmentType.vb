'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 05-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Base
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Entities

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface ITemplateEquipmentType
    Inherits IcrudBase

#Region "properties"
    Property CodeTemplate As String
    ''' <summary>
    ''' Esta propiedad contiene los terminos y condiciones
    ''' </summary>
    Property Terms As String
    ''' <summary>
    ''' Esta propiedad contiene los datos generales
    ''' </summary>
    Property GeneralData As String
    ''' <summary>
    ''' Esta propiedad contiene la descripcion de la operacion del equipo
    ''' </summary>
    Property OperationDescription As String
    ''' <summary>
    ''' Esta propiedad contiene  las precauciones 
    ''' </summary>
    Property HandlingPrecautions As String
    ''' <summary>
    ''' Esta propiedad la descripcion de la limpiza del equipo
    ''' </summary>
    Property Cleaning As String
    ''' <summary>
    ''' Esta propiedad contiene codigo
    ''' </summary>
    Property IdEquipmentType As Integer
    ''' <summary>
    ''' Esta propiedad contiene el estado del tipo de poliza
    ''' </summary>
    WriteOnly Property EquipmentTypeDatasource As List(Of Object)
    ''' <summary>
    ''' Esta propiedad contiene el estado del tipo de poliza
    ''' </summary>
    Property StateEquipmentType As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
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
    Property Sequense As Domain.Entities.MaintenanceSequence

#End Region
End Interface
