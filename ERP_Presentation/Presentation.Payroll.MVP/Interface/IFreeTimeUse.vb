'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Juan Diego Díaz
' Created          : 06-09-2018
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
Imports DevExpress.Xpo

#End Region

' <summary>
' Esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presentador
' </summary>
Public Interface IFreeTimeUse
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo de la actividad de tiempo libre
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Esta propiedad contiene el nombre de la actividad de tiempo libre
    ''' </summary>
    Property Name As String

    ''' <summary>
    ''' Esta propiedad contiene el estado de la actividad de tiempo libre
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' controla la accion de los controles del frontal
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.PayrollSequence

#End Region

End Interface
