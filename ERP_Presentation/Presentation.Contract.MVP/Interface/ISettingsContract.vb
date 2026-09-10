'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/02/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface ISettingsContract
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Esta propiedad establece el valor MyLayoutControl
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
    ''' Permite saber si maneja descripciones relacionadas
    ''' </summary>
    ''' <returns></returns>
    Property CUPSWithRelatedDescription As Boolean

    ''' <summary>
    ''' Permite identifica si se Solicitan Cotización Servicios Ambulatorios de manera restrictiva
    ''' </summary>
    ''' <returns></returns>
    Property RequestQuoteOutpatientServices As Boolean

    ''' <summary>
    ''' Permite identifica si se Solicitan Cotización Servicios Intrahospitalarios de manera restrictiva
    ''' </summary>
    ''' <returns></returns>
    Property RequestQuoteIntrahospitalServices As Boolean

    ''' <summary>
    ''' Permite identifica si se Solicitan Cotización Productos Ambulatorios de manera restrictiva
    ''' </summary>
    ''' <returns></returns>
    Property RequestQuoteOutpatientProducts As Boolean

    ''' <summary>
    ''' Permite identifica si se Solicitan Cotización Productos Intrahospitalarios de manera restrictiva
    ''' </summary>
    ''' <returns></returns>
    Property RequestQuoteIntrahospitalProducts As Boolean

#End Region

End Interface
