'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Andres Alarcon
' Created          : 20-02-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo

#End Region

Public Interface ILineClearanceCriteria
    Inherits ICrudBase
#Region "Properties"

    ''' <summary>
    ''' Propiedad que contiene el código del registro
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Propiedad que contiene la descripción del registro
    ''' </summary>
    Property Name As String

    ''' <summary>
    ''' Propiedad que contiene el estado del registro
    ''' </summary>
    Property State As Boolean

    ''' <summary>
    ''' Propiedad que asigna si se valida criterio
    ''' </summary>
    Property ValidateCriteria As Boolean

    ''' <summary>
    ''' Propiedad que asigna si valida la opcion 
    ''' </summary>
    Property ValidateOption As Boolean?

    ''' <summary>
    ''' Propiedad que contiene si maneja quimico de produccion
    ''' </summary>
    Property ChemicalProduction As Boolean

    ''' <summary>
    ''' Propiedad que contiene si maneja auxiliar de produccion
    ''' </summary>
    Property AuxiliarProduction As Boolean

    ''' <summary>
    ''' Propiedad que contiene los tipos de dosis unitaria
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

#End Region
End Interface
