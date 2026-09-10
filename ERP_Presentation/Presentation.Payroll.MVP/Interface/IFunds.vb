'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 17-04-2013
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
Imports Domain.Entities

#End Region
''' <summary>
''' Interfaz que va a implementar nuestra vista, y controlar el presentador
''' </summary>
''' <remarks></remarks>
Public Interface IFunds
    Inherits IcrudBase
#Region "Properties"
    ''' <summary>
    ''' Propiedad que contiene el codigo del fondo
    ''' </summary>
    Property CodeFund As String
    ''' <summary>
    ''' Propiedad que contiene el nit o identificacion del tercero
    ''' </summary>
    Property ThirdPartyId As String
    ''' <summary>
    ''' Propiedad que contiene el nombre del tercero
    ''' </summary>
    Property NameCustomer As String
    ''' <summary>
    ''' Propiedad que contiene el tipo del fondo, donde 0 es publico y 1 privado
    ''' </summary>
    Property TypeFund As String
    ''' <summary>
    ''' Propiedad que contiene el check cesantias
    ''' </summary>
    Property Unemployment As Boolean
    ''' <summary>
    ''' Propiedad que contiene el check salud
    ''' </summary>
    Property Health As Boolean
    ''' <summary>
    ''' Propiedad que contiene el check pension
    ''' </summary>
    Property Pension As Boolean
    ''' <summary>
    ''' Propiedad que contiene el check Riesgo
    ''' </summary>
    Property Risk As Boolean
    ''' <summary>
    ''' Propiedad que contiene el check IIS
    ''' </summary>
    Property IIS As Boolean
    ''' <summary>
    ''' Propiedad ACCAI para  Ley 2381 de 2024
    ''' </summary>
    ''' <returns></returns>
    Property ACCAI As Boolean
    ''' <summary>
    ''' Propiedad que contiene el codigo MiniSalud
    ''' </summary>
    Property CodeHealth As String
    ''' <summary>
    ''' Propiedad que contiene el estado de el fondo
    ''' </summary>
    Property StateFund As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Esta propiedad contiene el datasource del control grid look up terceros
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property DataSourceThirdParty As List(Of Domain.Entities.ThirdParty)

    Property Sequence As Domain.Entities.PayrollSequence

    ReadOnly Property MyLayoutControl As IndigoLayoutControl


    ReadOnly Property MyTag As Object
#End Region
End Interface
