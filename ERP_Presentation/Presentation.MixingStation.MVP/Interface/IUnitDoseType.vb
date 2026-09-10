'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Judy Andrea Díaz Reyes
' Created          : 20-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
#End Region

Public Interface IUnitDoseType
	Inherits IcrudBase
#Region "Properties"
	''' <summary>
	''' Propiedad que contiene el código del registro
	''' </summary>
	Property Code As String

	''' <summary>
	''' Propiedad que contiene la descripción del registro
	''' </summary>
	Property Description As String


	''' <summary>
	''' Propiedad que contiene el estado del registro
	''' </summary>
	Property State As Boolean

    ''' <summary>
    ''' Obtiene o establece el valor del tipo de clase a la que pertenece el tipo de dosis unitaria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property msClass As Integer

    ''' <summary>
    ''' Grupo de inventarios
    ''' </summary>
    ''' <returns></returns>
    Property InventoryGroupId As Integer?

    ''' <summary>
    ''' Subgrupo de inventarios
    ''' </summary>
    ''' <returns></returns>
    Property InventorySubGroupId As Integer?

    ''' <summary>
    ''' Código iva
    ''' </summary>
    ''' <returns></returns>
    Property IVACodeId As Integer?

    ''' <summary>
    ''' grupo de facturacion
    ''' </summary>
    ''' <returns></returns>
    Property BillingGroupId As Integer?

    ''' <summary>
    ''' Propiedad para habilitar o desabilitar controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

	''' <summary>
	''' Propiedad que contiene la configuración de secuencia asignada al formulario
	''' </summary>
	Property Sequence As MixingStationSequence

	''' <summary>
	''' Obtiene el tag del formulario
	''' </summary>
	''' <returns>Tag del formulario</returns>
	ReadOnly Property MyTag As Object

	ReadOnly Property MyLayoutControl As IndigoLayoutControl

#End Region
End Interface
