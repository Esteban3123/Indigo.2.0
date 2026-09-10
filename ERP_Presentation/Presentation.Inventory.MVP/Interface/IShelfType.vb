'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Judy Andrea Díaz Reyes
' Created          : 24-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
#End Region

Public Interface IShelfType
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
	''' Obtiene o establece el largo del tipo de estante
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Property Large As Decimal

	''' <summary>
	''' Obtiene o establece el ancho del tipo de estante
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Property Wide As Decimal

	''' <summary>
	''' Obtiene o establece la profundidad del tipo de estante
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Property Deep As Decimal

	''' <summary>
	''' Obtiene o establece las divisiones por profundidad del tipo de estante
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Property PartitionXDeep As Decimal

	''' <summary>
	''' Obtiene o establece la cantidad de divisiones del tipo de estante
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Property Partitions As Decimal

	''' <summary>
	''' Obtiene o establece la cantidad de localizaciones por división del tipo de estante
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Property LocatioXPartition As Decimal

	''' <summary>
	''' Propiedad que contiene el estado del registro
	''' </summary>
	Property State As Boolean

	''' <summary>
	''' Propiedad para habilitar o desabilitar controles
	''' </summary>
	WriteOnly Property ActionsOnControls As Boolean

	''' <summary>
	''' Propiedad que contiene la configuración de secuencia asignada al formulario
	''' </summary>
	Property Sequence As InventorySequence

	''' <summary>
	''' Obtiene el tag del formulario
	''' </summary>
	''' <returns>Tag del formulario</returns>
	ReadOnly Property MyTag As Object

	ReadOnly Property MyLayoutControl As IndigoLayoutControl

#End Region
End Interface
