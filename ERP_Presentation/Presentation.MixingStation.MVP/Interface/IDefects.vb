'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 30-08-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo
#End Region
Public Interface IDefects
    Inherits ICrudBase

    ''' <summary>
    ''' Propiedad que contiene el código del registro
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Propiedad que contiene la descripción del registro
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Propiedad que contiene el orden de como se listan los registro
    ''' </summary>
    Property Weight As Integer

    ''' <summary>
    ''' Propiedad si el registro contiene el tipo menor
    ''' </summary>
    Property Type As Byte

    ''' <summary>
    ''' Propiedad contiene el estado del registro
    ''' </summary>
    Property State As Boolean

    ''' <summary>
    ''' Propiedad que contiene las categorias de defectos
    ''' </summary>
    Property DefectClassificationGroupId As Integer

    ''' <summary>
    ''' Propiedad que contiene las unidades unitarios
    ''' </summary>
    Property UnitDoseTypeId As Integer

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

    ''' <summary>
    ''' Obtiene o establece el datasource de tipo de dosis unitaria
    ''' </summary>
    Property UnitDoseTypeDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de las categorias de defectos
    ''' </summary>
    Property DefectClassificationGroup As XPInstantFeedbackSource
End Interface
