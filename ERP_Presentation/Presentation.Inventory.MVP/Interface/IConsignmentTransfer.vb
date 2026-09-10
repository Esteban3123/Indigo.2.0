
'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego A. Roldán Lozano
' Created          : 2023-04-04
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IConsignmentTransfer
    Inherits ICrudBase

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Byte
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o establece el consecutivo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String
    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    ''' <returns></returns>
    Property DocumentDate As Date
    ''' <summary>
    ''' Descripción
    ''' </summary>
    ''' <returns></returns>
    Property Description As String
    ''' <summary>
    ''' Almacén
    ''' </summary>
    ''' <returns></returns>
    Property WareHouseId As Integer

    ''' <summary>
    ''' Obtiene o establece el Id de la moneda del documento
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId(Optional CurrencyAbbreviation As String = Nothing) As Integer

    ''' <summary>
    ''' Obtiene o establece el datasource del combo de moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequence As Domain.Entities.InventorySequence

End Interface
