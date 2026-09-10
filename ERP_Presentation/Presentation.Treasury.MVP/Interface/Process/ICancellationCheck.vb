'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 23-05-2014
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

Public Interface ICancellationCheck
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
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el Id de la entidad bancaria que contiene el cheque a cancelar
    ''' </summary>
    ''' <value>
    ''' The identifier entity account.
    ''' </value>
    Property IdEntityAccount As Integer

    ''' <summary>
    ''' Obtiene o establece la fecha de la cancelacion
    ''' </summary>
    ''' <value>
    ''' The cancellation date.
    ''' </value>
    Property CancellationDate As DateTime

    ''' <summary>
    ''' Obtiene o establece el numero del cheque a cancelar
    ''' </summary>
    ''' <value>
    ''' The check number.
    ''' </value>
    Property CheckNumber As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cancelacion del cheque
    ''' </summary>
    ''' <value>
    ''' The description.
    ''' </value>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el datasource de entidades bancarias
    ''' </summary>
    ''' <value>
    ''' The entity bank account datasource.
    ''' </value>
    Property EntityBankAccountDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
