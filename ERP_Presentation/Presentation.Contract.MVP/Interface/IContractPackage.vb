'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Giovanny Plazas Lozano
' Created          : 24/08/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Infrastructure.Data.Xpo.ContractRepository

#End Region

Public Interface IContractPackage
    Inherits ICrudBase

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el codigo del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Establece el Nombre del Paquete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Name As String

    ''' <summary>
    ''' Obtiene o establece el Id del Cups
    ''' </summary>
    ''' <returns></returns>
    Property CUPSEntityId As Integer?

    ''' <summary>
    ''' Obtiene o establece el Id de la descripcion
    ''' </summary>
    ''' <returns></returns>
    Property ContractDescriptionId As Integer?

    ''' <summary>
    ''' Obtiene o establece el Id del servicio
    ''' </summary>
    ''' <returns></returns>
    Property IPSServiceId As Integer?

    ''' <summary>
    ''' Obtiene o establece la descripcion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.ContractSequence
#Region "XPOProperties"
    ''' <summary>
    ''' DataSource de Cups Cabecera
    ''' </summary>
    ''' <returns></returns>
    Property XpoCupsEntity As XPInstantFeedbackSource
    ''' <summary>
    ''' DataSource de Cups detalle
    ''' </summary>
    ''' <returns></returns>
    Property XpoCupsEntityService As XPInstantFeedbackSource
    ''' <summary>
    ''' DataSource descripción relacionada Cabecera
    ''' </summary>
    ''' <returns></returns>
    Property XpoContractDescription As List(Of CUPSEntityContractDescriptionsXpo)
    ''' <summary>
    ''' DataSource descripción relacionada  Servicio
    ''' </summary>
    ''' <returns></returns>
    Property XpoContractDescriptionService As List(Of CUPSEntityContractDescriptionsXpo)
    ''' <summary>
    ''' DataSource Servicios IPS
    ''' </summary>
    ''' <returns></returns>
    Property XpoIPSServiceByPackage As XPInstantFeedbackSource
    ''' <summary>
    ''' Datasource producto por estado
    ''' </summary>
    ''' <returns></returns>
    Property XpoProductByStatus As XPInstantFeedbackSource

#End Region
End Interface
