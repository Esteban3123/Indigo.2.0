'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Juan F. Tamay Puertas
' Created          : 2016-10-31
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
Imports DevExpress.Xpo
Imports Domain.Entities

#End Region

Public Interface IGeneralExpenseCategory
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el código de la estructura organizacional
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la estructura organizacional
    ''' </summary>
    Property NameCategory As String

    ''' <summary>
    ''' Obtiene o establece el id del registro padre
    ''' </summary>
    Property StructId As Integer?

    ''' <summary>
    ''' datasource padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StructXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Descripcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

#End Region

End Interface