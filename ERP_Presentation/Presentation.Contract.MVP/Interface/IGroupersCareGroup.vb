'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
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

Public Interface IGroupersCareGroup

    Inherits IcrudBase


    ''' <summary>
    ''' Obtiene o establece el codigo de rips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property GroupersId As Integer

    ''' <summary>
    ''' Obtiene o establece la descripcion de rips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MinimunRange As Integer

    ''' <summary>
    ''' Obtiene o establece el id del subgrupo cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MaximunRange As Integer

    ''' <summary>
    ''' Obtiene o establece el id del subgrupo cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MeasurementUnit As Integer

    Property WarningFor As Integer

    Property WarningMessage As String

    Property MaximunRangeRestrict As Boolean

    Property RestrictMessage As String

    ''' <summary>
    ''' Establece el datasource de contratos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CUPSXpo As XPInstantFeedbackSource

    Property GroupersXpo As XPInstantFeedbackSource

    Property ActivitiesXpo As XPInstantFeedbackSource

End Interface
