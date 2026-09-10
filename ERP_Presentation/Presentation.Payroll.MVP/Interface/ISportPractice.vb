'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Juan Diego Díaz
' Created          : 30-08-2018
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
Public Interface ISportPractice
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo del deporte practicado
    ''' </summary>
    Property CodeSP As String

    ''' <summary>
    ''' Esta propiedad contiene el nombre del deporte practicado
    ''' </summary>
    Property NameSP As String

    ''' <summary>
    ''' Esta propiedad contiene el estado del deporte practicado
    ''' </summary>
    Property StatusSP As Boolean

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
