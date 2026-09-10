'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Diego Andrés Roldán
' Created          : 10-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
#End Region

Public Interface IPatrimonialPart
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
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequense As Domain.Entities.GeneralLedgerSequence

    ''' <summary>
    ''' Obtiene o establece el codigo de participacion patrimonial
    ''' </summary>
    ''' <value>
    ''' The code cards.
    ''' </value>
    Property CodePatrimonialPart As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la participacion patrimonial
    ''' </summary>
    ''' <value>
    ''' The name cards.
    ''' </value>
    Property NamePatrimonialPart As String

    ''' <summary>
    ''' Obtiene o establece la participacion
    ''' </summary>
    ''' <value>
    ''' The part.
    ''' </value>
    Property Part As Decimal

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [status]; otherwise, <c>false</c>.
    ''' </value>
    Property Status As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

#End Region
End Interface
