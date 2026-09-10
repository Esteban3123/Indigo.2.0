'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 19-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
#End Region

Public Interface IEquipmentEntry

    Inherits IcrudBase

    ''' <summary>
    ''' codigo de la remision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String
    ''' <summary>
    ''' fecha de la remision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RemissionDate As Date?
    ''' <summary>
    ''' id de la linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierDistributionLineId As Integer?
    ''' <summary>
    ''' id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdquisitionTypeId As Integer?
    ''' <summary>
    ''' numero de la remision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RemissionNumber As String
    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequense As Domain.Entities.FixedAssetSequence

    Property LocationId As Integer?

    Property ResponsibleId As Integer?

    Property GetLocationResponsible As Byte

End Interface
