'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 24-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"


Imports Domain.Payroll.Entities
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Presentation.Base


#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IFunctionalUnit
    Inherits IcrudBase

#Region "Properties"
    ''' <summary>
    ''' Propiedad que contiene el codigo de la unidad funcional
    ''' </summary>
    Property Code As String
    ''' <summary>
    ''' Propiedad que contiene el nombre de la unidad de negocio
    ''' </summary>
    Property NameFuncUnit As String
    ''' <summary>
    ''' Propiedad que contiene el id de la sucursal
    ''' </summary>
    Property BranchOfficeId As Integer
    ''' <summary>
    ''' Propiedad que contiene el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterId As Integer
    ''' <summary>
    ''' propiedad que asigna el listado de las sucursales al control grid look up edit
    ''' </summary>
    WriteOnly Property DatasourceBranchOffice As List(Of Domain.Payroll.Entities.BranchOffice)
    ''' <summary>
    ''' Propiedad que Contiene el estado de la unidad de negocio
    ''' </summary>
    Property StateFuncUnit As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Propiedad que contiene el id de la Estructura Contable de Nómina
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountingStructureId As Integer?
    ''' <summary>
    ''' Obtiene o establece el tipo de unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UnitType As Integer

    '''' <summary>
    '''' Propiedad que contiene los turnos
    '''' </summary>
    'Property TurnId As Integer

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property DeliveryTimePharmaService As DateTime?

    ReadOnly Property MyTag As String
    Property Sequence As PayrollSequence

    'Property TurnDatasource As XPInstantFeedbackSource

#End Region

End Interface
