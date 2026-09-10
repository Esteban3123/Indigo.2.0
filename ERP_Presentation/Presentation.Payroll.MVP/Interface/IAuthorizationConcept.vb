'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 09-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Payroll.Entities

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IAuthorizationConcept
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Propiedad que establece el datasource de los grupos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property Group_Datasource As List(Of Group)

    ''' <summary>
    ''' Propiedad que Contiene el Id del grupo
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Group_Id As Integer

    ''' <summary>
    ''' Propiedad que contiene el id del empleado
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Employee_Id As Integer

    ''' <summary>
    ''' Propiedad que contiene el datasource de los empleados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property Employee_Datasource As List(Of Employee)

    ''' <summary>
    ''' Propiedad que contiene el grid control de la rejilla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property AuthorizationCGridControl As DevExpress.XtraGrid.GridControl

    ''' <summary>
    ''' Propiedad que contiene el grid view de la rejilla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property AuthorizationCGridView As DevExpress.XtraGrid.Views.Grid.GridView

    ''' <summary>
    ''' controla la accion de los controles del frontal
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Propiedad que establece si es por grupo o por empleado
    ''' </summary>
    ReadOnly Property AuthorizationConceptBy As Integer

    ''' <summary>
    ''' Propiedad que contiene los indices de conceptos autorizados por grupos
    ''' </summary>
    Property List_AuthorizationConceptGroupIndex As List(Of AuthorizationConcept)

    ''' <summary>
    ''' Establece la accion en el control de seleccionar todos 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ShowCheckAllControl As Boolean

#End Region

End Interface
