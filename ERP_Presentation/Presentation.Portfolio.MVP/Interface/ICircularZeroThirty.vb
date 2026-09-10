'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Faiber Julian Mora Dussan
' Created          : 26-09-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface ICircularZeroThirty
    Inherits IcrudBase

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
    ''' Obtiene o establece el trimestre que se reporta
    ''' </summary>
    ''' <value>Triemestre que se reporta</value>
    ''' <returns>El Trimestre que se reporta</returns>
    ReadOnly Property Trimester As Byte

    ''' <summary>
    ''' Obtiene o establece el año que se reporta
    ''' </summary>
    ''' <value>Año que se reporta</value>
    ''' <returns>El Año que se reporta</returns>
    ReadOnly Property Year As Integer

#End Region

#Region "Methods"

    Sub LoadTrimesters()

    Function LoadTrimestersAsync() As Task

#End Region

End Interface