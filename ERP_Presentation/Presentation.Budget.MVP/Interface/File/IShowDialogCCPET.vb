'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/07/2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Presentation.Controls

Public Interface IShowDialogCCPET
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Name As String

    ''' <summary>
    ''' Obtiene o establece el  Id Concepto CCPET padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CCPETOwnerId As Integer?

    '' <summary>
    '' Obtiene o establece OBTIENE Ó ESTABLECE EL TIPO DE RUBRO (INGRESO=1,GASTO = 2)
    '' </summary>
    '' <value></value>
    '' <returns></returns>
    '' <remarks></remarks>
    Property ItemType As Byte

    '' <summary>
    '' Obtiene o establece Tipo de Cuenta: Agregación. (A). Cuentas de Captura (C)  (Solo permite vincular a otras funcionalidades los marcados como tipo C.)
    '' </summary>
    '' <value></value>
    '' <returns></returns>
    '' <remarks></remarks>
    Property AccountType As Boolean

    ''' <summary>
    ''' Obtiene o establece Vincula Cuenta CPC: Si / No .Esta opción debe estar disponible si el campo Tipo de Cuenta es (C). dejar todas en No.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LinkAccount As Boolean

    ''' <summary>
    ''' Estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Status As Boolean

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Establece el datasource de los rubros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ParentXpo As DevExpress.Xpo.XPInstantFeedbackSource

#End Region

End Interface
