'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 05/11/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface ICashFlowConcept
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene o establece el codigo del concepto
    ''' </summary>
    ''' <value>
    ''' code.
    ''' </value>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre del concepto
    ''' </summary>
    ''' <value>
    ''' name.
    ''' </value>
    Property Name As String

    ''' <summary>
    ''' Tipo de concepto
    ''' </summary>
    ''' <returns></returns>
    Property Type As Byte

    ''' <summary>
    ''' Actividad de concepto
    ''' </summary>
    ''' <returns></returns>
    Property Activity As Byte

    ''' <summary>
    ''' obtiene o establece el estado del registro
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [status]; otherwise, <c>false</c>.
    ''' </value>
    Property Status As Boolean

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' establece el estado de los controles
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
