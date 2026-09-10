'***********************************************************************
' Assembly         : Domain.Entities.Inventory
' Author           : Judy Andrea Díaz Reyes
' Created          : 27-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IShelfTypeRepository
	Inherits IRepository(Of ShelfType)

	''' <summary>
	''' Obtiene todos los tipos de estante
	''' </summary>
	''' <returns>Lista de Tipos de Estante</returns>
	''' <remarks></remarks>
	Function ListAllShelfType() As List(Of ShelfType)

	''' <summary>
	''' Obtiene los tipos de estante por código
	''' </summary>
	''' <param name="code">The code.</param>
	''' <returns></returns>
	Function GetShelfType(code As String, Optional tracking As Boolean = True) As ShelfType

	''' <summary>
	''' Obtiene un tipo de estante por id
	''' </summary>
	''' <param name="id">The identifier.</param>
	''' <returns></returns>
	Function GetShelfTypeById(id As Integer, Optional tracking As Boolean = True) As ShelfType

	''' <summary>
	''' Función que se utiliza para almacenar el Tipo de Estante
	''' </summary>
	''' <param name="Code"></param>
	''' <param name="Description"></param>
	''' <param name="Large"></param>
	''' <param name="Wide"></param>
	''' <param name="Deep"></param>
	''' <param name="PartitionXDeep"></param>
	''' <param name="Partitions"></param>
	''' <param name="LocationXPartition"></param>
	''' <param name="State"></param>
	''' <param name="Codeuser"></param>
	''' <returns></returns>
	Function SaveShelfType(Code As String, Description As String, Large As Decimal, Wide As Decimal, Deep As Decimal, PartitionXDeep As Decimal, Partitions As Decimal, LocationXPartition As Decimal, State As Boolean, CodeUser As String) As SP_SaveShelfType_Result

End Interface
