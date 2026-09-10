'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 08-07-2013
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

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IContractTemplate
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo del plantilla de contrato
    ''' </summary>
    Property CodeCT As String

    ''' <summary>
    ''' Esta propiedad contiene el nombre del plantilla de contrato
    ''' </summary>
    Property NameCT As String

    ''' <summary>
    ''' Esta propiedad contiene el estado del plantilla de contrato
    ''' </summary>
    Property StatusCT As Boolean

    ''' <summary>
    ''' Propiedad que contiene el id del tipo de contrato
    ''' </summary>
    Property ContractTypeId As Integer

    ''' <summary>
    ''' Propiedad que establece el datasource en el control de grupos de contratos
    ''' </summary>
    WriteOnly Property DataSourceContractType As List(Of ContractType)

    ''' <summary>
    ''' Propiedad que contiene toda la información del contrato
    ''' </summary>
    Property FullContract As String

    ''' <summary>
    ''' controla la accion de los controles del frontal
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
