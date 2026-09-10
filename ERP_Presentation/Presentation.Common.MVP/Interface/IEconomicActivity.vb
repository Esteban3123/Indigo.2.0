'***********************************************************************
' Assembly         : Presentation.Common.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports Presentation.Controls

Public Interface IEconomicActivity
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el codigo de la actividad economica
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Nombre de la actividad economica
    ''' </summary>
    Property NameEA As String

    ''' <summary>
    ''' Estado de la actividad economica
    ''' </summary>
    Property Status As Boolean


    ''' <summary>
    ''' Valida si la actividad es generadora de ingreso
    ''' </summary>
    ''' <returns></returns>
    Property IsIncomeGenerating As Boolean



    ''' <summary>
    ''' Activa o desactiva los controles del formulario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

End Interface
