'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 25-04-2013
'
' Last Modified By :Kevin Garay Rodriguez
' Last Modified On : 05-06-2013
' Description      : Refactory
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports CommonEntities = Domain.Entities
Imports DevExpress.Xpo
#End Region
''' <summary>
''' Interfaz que va a implementar nuestra vista, y controlar el presentador
''' </summary>
''' <remarks></remarks>
Public Interface ICompany
    Inherits IcrudBase
#Region "properties"
    ''' <summary>
    ''' Propiedad que contiene el codigo de la empresa
    ''' </summary>
    Property CompanyNit As String
    ''' <summary>
    ''' Propiedad que contiene el tercero
    ''' </summary>
    Property CompanyName As String 
    ''' <summary>
    ''' Propiedad que contiene el representante legal de la empresa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LegalRepresentative As String
    ''' <summary>
    ''' Propiedad que contiene el departamento perteneciente de esta empresa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Department As Integer
    ''' <summary>
    ''' Propiedad que contiene la ciudad perteneciente de esta empresa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property City As Integer
    ''' <summary>
    ''' Propiedad que contiene el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CompanyState As Boolean

    ''' <summary>
    ''' Propiedad que contiene el ID de la ARL de la compañía
    ''' </summary>
    ''' <returns></returns>
    Property ARLFundId As Integer?
    ''' <summary>
    ''' Propiedad que carga los terceros
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property DepartmentDataSource As List(Of Domain.Entities.Department)
    ''' <summary>
    ''' Propiedad que carga las ciudades
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property CityDataSource As List(Of Domain.Entities.City)

    ''' <summary>
    ''' Propiedad que carga los Fondos
    ''' </summary>
    WriteOnly Property FundDataSource As List(Of Domain.Payroll.Entities.Fund)
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Lista de Terceros
    ''' </summary>
    ''' <returns></returns>
    Property ListThirdParty As XPInstantFeedbackSource
#End Region
End Interface
