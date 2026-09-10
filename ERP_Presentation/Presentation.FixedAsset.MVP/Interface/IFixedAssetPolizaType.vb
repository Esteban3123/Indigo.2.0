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
Imports Presentation.Controls

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IFixedAssetPolizaType
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del tipo de poliza
    ''' </summary>
    Property CodePolizaType As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del tipo de poliza
    ''' </summary>
    Property NamePolizaType As String
    ''' <summary>
    ''' Esta propiedad contiene el estado del tipo de poliza
    ''' </summary>
    Property StatePolizaType As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.FixedAssetSequence

    ReadOnly Property MyLayoutControl As IndigoLayoutControl
#End Region

End Interface
