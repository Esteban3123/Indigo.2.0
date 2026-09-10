'***********************************************************************
' Assembly         : Presentation.AccountManagement.MVP
' Author           : Felix Camilo Salazar Roldan
' Created          : 14-11-2024
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IManagementAreas
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta Propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna el tag del funcional
    ''' </summary>
    ''' <value>Tag del fucnional</value>
    ''' <returns></returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.AccountManagementSequence

    ''' <summary>
    ''' Obtiene o establece el consecutivo de la condicion de venta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la condicion de venta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Name As String

    ''' <summary>
    ''' Obtiene o estable el tiempo en desarrollar la actividad
    ''' </summary>
    ''' <returns></returns>
    Property LifeTime As Integer

    ''' <summary>
    ''' Obtiene o estable unidad 
    ''' </summary>
    ''' <returns></returns>
    Property LifeUnit As Byte

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Establece el datasource de usuarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UserXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el Id del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdUser As Integer?

#End Region

End Interface
