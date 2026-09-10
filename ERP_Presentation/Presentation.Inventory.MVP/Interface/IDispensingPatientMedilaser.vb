'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/09/2018
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IDispensingPatientMedilaser
    Inherits IcrudBase

    ''' <summary>
    ''' Codigo del centro de atencion
    ''' </summary>
    ''' <returns></returns>
    Property CareCenterCode As String

    ''' <summary>
    ''' Datasource centro de atencion
    ''' </summary>
    ''' <returns></returns>
    Property CareCenterXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WarehouseId As Integer

    ''' <summary>
    ''' Datasource almacenes
    ''' </summary>
    ''' <returns></returns>
    Property WarehouseXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' id del grupo de atención
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CareGroupId As Integer

    ''' <summary>
    ''' Datasource grupo de atención
    ''' </summary>
    ''' <returns></returns>
    Property CareGroupXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' id de la autorización
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillingAuthorizationId As Integer

    ''' <summary>
    ''' Datasource autorización
    ''' </summary>
    ''' <returns></returns>
    Property BillingAuthorizationXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Fecha
    ''' </summary>
    ''' <returns></returns>
    Property DateItem As DateTime?

    ''' <summary>
    ''' No. formula
    ''' </summary>
    ''' <returns></returns>
    Property Number As String

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Layout del form
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

End Interface