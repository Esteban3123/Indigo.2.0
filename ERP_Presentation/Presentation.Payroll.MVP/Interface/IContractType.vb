'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 05-07-2013
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

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IContractType
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo del tipo de contrato
    ''' </summary>
    Property CodeCT As String

    ''' <summary>
    ''' Esta propiedad contiene el nombre del tipo de contrato
    ''' </summary>
    Property NameCT As String

    ''' <summary>
    ''' Esta propiedad contiene el estado del tipo de contrato
    ''' </summary>
    Property StatusCT As Boolean

    ''' <summary>
    ''' Propiedad que establece el datasource en el control de grupos de contratos
    ''' </summary>
    WriteOnly Property DataSourceJobBondingTypeXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' controla la accion de los controles del frontal
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
