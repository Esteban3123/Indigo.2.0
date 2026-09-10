'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 26-08-2015
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

Public Interface IFixedAssetPartsAccesoriesConsumables
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo de PartsAccesoriesConsumables
    ''' </summary>
    Property CodePartsAccesoriesConsumables As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre de PartsAccesoriesConsumables
    ''' </summary>
    Property NamePartsAccesoriesConsumables As String

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Obtiene o establece el tipo de depreciación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DepreciationTypeId As Integer?

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
    ''' Obtiene o establece el porcentaje de salvamento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PercentageRescue As Decimal

    ''' <summary>
    ''' Obtiene o establece el total de unidades de produccion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TotalProductionUnit As Int64

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
    ''' Permite saber si se deprecia o no el articulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AllowDepreciate As Boolean?
    Property Status As Boolean
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
#End Region

End Interface
