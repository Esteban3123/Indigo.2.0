'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 03-04-2020
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
#End Region

Public Interface IBacterialResistanceMedication
    Inherits ICrudBase

#Region "properties"
    ''' <summary>
    ''' Establece el datasource de ATC
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InventoryATC As XPCollection(Of Infrastructure.Data.Xpo.InventoryRepository.ATCXpo)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ATCId As Integer?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property StartDate As DateTime?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property EndDate As DateTime?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property Observation As String

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

#End Region

End Interface

