'***********************************************************************
' Assembly         : Presentacion.Corporation.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 21-08-2013
'
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 

Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Public Interface IInability
    Inherits ICrudBase

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Propiedad que establece el tipo de novedad
    ''' </summary>
    ''' <returns></returns>
    Property CalculationType As Byte?
    ''' <summary>
    ''' Listara los conceptos de las licencias 
    ''' </summary>
    ''' <returns></returns>
    Property ListLicensingConceptsNoveltyXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Propiedad para lamacenar el valor de la tupla de tipo de licencia
    ''' </summary>
    ''' <returns></returns>
    Property NoveltyLicensingType As Integer

End Interface
