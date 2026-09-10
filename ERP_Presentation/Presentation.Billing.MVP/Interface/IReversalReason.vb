'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 17-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Domain.Entities

#End Region

Public Interface IReversalReason
    Inherits IcrudBase

#Region "Properties"
    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Layout principal para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Código de la razón
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Nombre de la razón
    ''' </summary>
    Property Name As String

    ''' <summary>
    ''' Descripción de la razón
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Estado de la razón
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Secuencia de facturación
    ''' </summary>
    Property Sequense As BillingSequence

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Function LoadControls() As Task

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Sub CleanControls()

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Sub AssigningValues()

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
#End Region

End Interface
