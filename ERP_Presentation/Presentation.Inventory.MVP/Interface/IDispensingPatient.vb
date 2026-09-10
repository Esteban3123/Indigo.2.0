'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2018
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

Public Interface IDispensingPatient
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
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Layout del form
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Tipo de despacho
    ''' </summary>
    ''' <returns></returns>
    Property OfficeType As Integer

    ''' <summary>
    ''' Operador logistico
    ''' </summary>
    ''' <returns></returns>
    Property LogisticOperator As Integer

    ''' <summary>
    ''' Formula medica
    ''' </summary>
    ''' <returns></returns>
    Property PatientIdentification As String

    ''' <summary>
    ''' Tipo de identificación
    ''' </summary>
    ''' <returns></returns>
    Property TypeIdentification As Integer

    ''' <summary>
    ''' id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WarehouseCode As String

    ''' <summary>
    ''' Datasource almacenes
    ''' </summary>
    ''' <returns></returns>
    Property WarehouseXpo As XPInstantFeedbackSource

End Interface