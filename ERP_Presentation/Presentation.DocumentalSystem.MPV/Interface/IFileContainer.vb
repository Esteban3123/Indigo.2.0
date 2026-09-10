'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Juan Diego Diaz
' Created          : 07-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Domain.Common.Entities
#End Region

''' <summary>
''' Interfaz que maneja el frontal de contenedores de archivos
''' </summary>
Public Interface IFileContainer
    Inherits IcrudBase
    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' propiedad que contiene el estado del contenedor de archivos
    ''' </summary>
    Property StatusResponsible As Boolean
    ''' <summary>
    ''' Propiedad para asignar los detalles de formularios por contenedor
    ''' </summary>
    ''' <value>Objeto</value>
    Property DataSourceForm As List(Of Domain.DocumentalSystem.Entities.FileContainersForm)

    ''' <summary>
    ''' Propiedad para asignar los registros de metadata
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataSourceMetadata As List(Of Domain.DocumentalSystem.Entities.Metadata)



End Interface

