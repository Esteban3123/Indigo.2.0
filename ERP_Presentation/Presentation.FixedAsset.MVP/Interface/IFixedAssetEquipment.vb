'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 03-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Base
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IFixedAssetEquipment
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo del equipo
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Esta propiedad contiene el nombre del equipo
    ''' </summary>
    Property NameEquipment As String

    ''' <summary>
    ''' Esta propiedad contiene id del tipo de equipo
    ''' </summary>
    Property IdEquipmentType As Integer?

    ''' <summary>
    ''' Esta propiedad contiene los comentarios
    ''' </summary>
    Property Comments As String

    ''' <summary>
    ''' Esta propiedad contiene el estado de la torre
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Permite saber si deprecia por tiempo de uso de la ubicación
    ''' </summary>
    ''' <returns></returns>
    Property DepreciateByTimeUse As Boolean

    ''' <summary>
    ''' Propiedad que carga todas  tipos de equipo
    ''' </summary>
    Property EquipmentTypeXpo As XPCollection

    ''' <summary>
    ''' Propiedad que carga todas  tipos de equipo
    ''' </summary>
    Property CatalogOfPropertyandServicesXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece el id del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LegalBookId As Integer?

    ''' <summary>
    ''' Establece el datasource del libro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LegalBookXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Establece el datasource del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVAXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource del catalogo de equipos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EquipmentCatalogXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del catalogo de equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EquipmentCatalogId As Integer?

    ''' <summary>
    ''' Obtiene o establece el Id del catalogo de bienes y servicios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CatalogOfPropertyandServicesId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVAId As Integer?

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el ultimo costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LastCost As Decimal

    ''' <summary>
    ''' Vida util
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LifeUtil As Integer

    ''' <summary>
    ''' Unidad de la vida util
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UnitLifeUtilId As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de depreciación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DepreciationTypeId As Integer?

    ''' <summary>
    ''' Obtiene o establece el total de unidades de produccion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TotalProductionUnit As Int64

    ''' <summary>
    ''' Obtiene o establece el porcentaje de salvamento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PercentageRescue As Decimal

    ''' <summary>
    ''' Permite saber si se deprecia o no el articulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AllowDepreciate As Boolean?

    ''' <summary>
    ''' Valor razonable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FairValue As Decimal

    ''' <summary>
    ''' Permite saber  catalogo de equipos amortiza o no 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Amortizes As Boolean?

#End Region
End Interface
