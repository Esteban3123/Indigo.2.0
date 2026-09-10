'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania 
' Created          : 26/03/2015
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
#End Region

Public Interface IInventoryAdjustments
    Inherits IcrudBase

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
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.InventorySequence

    Property ListWareHouse As XPInstantFeedbackSource

    Property ListConcepts As XPInstantFeedbackSource

    Property ListThirdParty As XPInstantFeedbackSource

    Property ListInventoryControl As XPInstantFeedbackSource

    Property Code As String

    Property DocumentDate As DateTime?

    Property AdjustmentType As Integer?

    Property Detail As String

    Property AdjustmentConceptId As Integer?

    Property WareHouseId As Integer?

    Property ThirdPartyId As Integer?

    Property ListProducts As List(Of Object)

    Property Status As Byte

    Property InventoryControlId As Integer?

    Property AdmissionNumberDatasource As XPInstantFeedbackSource

    Property AdmissionNumber As String

    'Property RevenueControlId As Integer?

 
End Interface




