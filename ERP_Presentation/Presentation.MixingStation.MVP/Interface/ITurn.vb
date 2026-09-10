'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Yoe Andres Cardenas
' Created          : 17-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
#End Region

Public Interface ITurn
    Inherits IcrudBase
#Region "Properties"
    ''' <summary>
    ''' Propiedad que contiene el código del registro
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del registro
    ''' </summary>
    Property Name As String

    ''' <summary>
    ''' Propiedad que contiene el estado del registro
    ''' </summary>
    Property State As Boolean

    ''' <summary>
    ''' Propiedad que contiene la fracción de la semana del registro
    ''' 1 = De lunes a viernes
    ''' 2 = Sabados y domingos
    ''' </summary>
    Property WeekFraction As Integer

    ''' <summary>
    ''' Propiedad que contiene la descripción del registro
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Propiedad que contiene la hora inicial de trabajo del registro
    ''' </summary>
    Property InitialTimeWork As DateTime?

    ''' <summary>
    ''' Propiedad que contiene la hora final de trabajo del registro
    ''' </summary>
    Property EndingTimeWork As DateTime?

    ''' <summary>
    ''' Propiedad que contiene la hora inicial de entrega del registro
    ''' </summary>
    Property InitialTimeDelivery As DateTime?

    ''' <summary>
    ''' Propiedad que contiene la hora final de entrega del registro
    ''' </summary>
    Property EndingTimeDelivery As DateTime?

    ''' <summary>
    ''' Propiedad para habilitar o desabilitar controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Propiedad que contiene la configuración de secuencia asignada al formulario
    ''' </summary>
    Property Sequence As MixingStationSequence

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ReadOnly Property MyLayoutControl As IndigoLayoutControl

#End Region
End Interface
