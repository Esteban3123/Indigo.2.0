'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Judy Andrea Díaz Reyes
' Created          : 30-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo

Public Interface ICMConfig
    Inherits IcrudBase
#Region "Properties"

    ''' <summary>
    ''' Propiedad que contiene el código del registro
    ''' </summary>
    Property Code As String


    ''' <summary>
    ''' Obtiene o establece el valor del tipo de central de mezclas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property msType As Byte?


    ''' <summary>
    ''' Obtiene o establece el nombre de la central de mezclas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property cmName As String

    '''' <summary>
    '''' Propiedad que contiene el estado del registro
    '''' </summary>
    Property State As Boolean

    ''' <summary>
    ''' Propiedad para habilitar o desabilitar controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Propiedad que contiene la configuración de secuencia asignada al formulario
    ''' </summary>
    Property Sequence As MixingStationSequence

    ''' <summary>
    ''' Id de línea de producción
    ''' </summary>
    ''' <returns></returns>
    Property IdProductionLine As Integer?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ProductionLineDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    '''  Obtiene o establece el prefijo de la central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Property cmPrefix As String

    ''' <summary>
    ''' Id del director Principal
    ''' </summary>
    ''' <returns></returns>
    Property IdDirector As Integer

    ''' <summary>
    ''' Id del director Suplente
    ''' </summary>
    ''' <returns></returns>
    Property IdDirectorSp As Integer?

#End Region

#Region "Metodos"

#End Region

End Interface
