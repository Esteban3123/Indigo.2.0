'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 03-09-2013
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
Imports Domain.Maintenance.Entities

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IPart
    Inherits IcrudBase
#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo de la Parte
    ''' </summary>
    Property CodePart As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la Parte
    ''' </summary>
    Property NamePart As String
    ''' <summary>
    ''' Esta propiedad contiene el id del equipo seleccionado
    ''' </summary>
    Property IdEquipment As Integer
    ''' <summary>
    ''' Propiedad que carga todos los equipos
    ''' </summary>
    WriteOnly Property EquipmentDataSource As List(Of EquipmentType)
    ''' <summary>
    ''' Esta propiedad contiene el estado de la Parte
    ''' </summary>
    Property StatePart As Boolean
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
