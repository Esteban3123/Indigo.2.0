'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 13-05-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IPaymentConcept
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Obtiene o establece el codigo del concepto de pago
    ''' </summary>
    ''' <value>
    ''' The code payment concept.
    ''' </value>
    Property CodePaymentConcept As String

    ''' <summary>
    ''' Obtiene o establece el nombre del concepto de pago
    ''' </summary>
    ''' <value>
    ''' The name payment concept.
    ''' </value>
    Property NamePaymentConcept As String

    ''' <summary>
    ''' Obtiene o establece el estado del concepto de pago
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state payment concept]; otherwise, <c>false</c>.
    ''' </value>
    Property StatePaymentConcept As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

#End Region
End Interface
