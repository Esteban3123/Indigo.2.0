'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Judy Andrea Díaz Reyes
' Created          : 27/05/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ShelfTypeRepository
	Inherits GenericRepository(Of ShelfType)
	Implements IShelfTypeRepository

	''' <summary>
	''' Contexto de Tipo de Estante
	''' </summary>
	Private _context As IGlobalModelUnitOfWork

	''' <summary>
	''' Inicia el contexto de Tipo de Estante
	''' </summary>
	''' <param name="context">Contexto</param>
	''' <remarks></remarks>
	Public Sub New(ByVal context As IGlobalModelUnitOfWork)
		MyBase.New(context)
		_context = context
	End Sub

	''' <summary>
	''' Lista todos los tipos de estante
	''' </summary>
	''' <returns>Lista de tipos de estante</returns>
	''' <remarks></remarks>
	Public Function ListAllShelfType() As List(Of ShelfType) Implements IShelfTypeRepository.ListAllShelfType
		Dim shelfType = From e In _context.ShelfType
						Select e
		Return shelfType.ToList()
	End Function

	''' <summary>
	''' Obtiene un tipo de estante especifivo
	''' </summary>
	''' <param name="code">Codigo del tipo de estante</param>
	''' <returns>Tipo de Estante</returns>
	''' <remarks></remarks>
	Public Function GetShelfType(code As String, Optional tracking As Boolean = True) As ShelfType Implements IShelfTypeRepository.GetShelfType
		Dim shelfType = From e In _context.ShelfType
						Where e.Code = code
						Select e
		If shelfType.Count > 0 Then
			Dim ObjShelfType = Nothing
			If tracking = False Then
				ObjShelfType = (From e In _context.ShelfType.AsNoTracking
								Where e.Code = code
								Select e).SingleOrDefault
			Else
				ObjShelfType = shelfType.SingleOrDefault
			End If
			Return ObjShelfType
		Else
			Return New ShelfType()
		End If
	End Function

	''' <summary>
	''' Obtiene un tipo de estante por el identificador
	''' </summary>
	''' <param name="id">The identifier.</param>
	''' <param name="tracking"></param>
	''' <returns></returns>
	Public Function GetShelfTypeById(id As Integer, Optional tracking As Boolean = True) As ShelfType Implements IShelfTypeRepository.GetShelfTypeById
		Dim shelfType = From e In _context.ShelfType
						Where e.Id = id
						Select e
		If shelfType.Count > 0 Then
			Dim ObjShelfType = Nothing
			If tracking = False Then
				ObjShelfType = (From e In _context.ShelfType.AsNoTracking
								Where e.Id = id
								Select e).SingleOrDefault
			Else
				ObjShelfType = shelfType.SingleOrDefault
			End If
			Return ObjShelfType
		Else
			Return New ShelfType()
		End If
	End Function

	''' <summary>
	''' Función que se utiliza para Almacenar o Actualizar un Tipo de Estante
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
	''' <param name="CodeUser"></param>
	''' <returns></returns>
	Public Function SaveShelfType(Code As String, Description As String, Large As Decimal, Wide As Decimal, Deep As Decimal, PartitionXDeep As Decimal, Partitions As Decimal, LocationXPartition As Decimal, State As Boolean, CodeUser As String) As SP_SaveShelfType_Result Implements IShelfTypeRepository.SaveShelfType
		Return _context.SP_SaveShelfType(Code, Description, Large, Wide, Deep, PartitionXDeep, Partitions, LocationXPartition, State, CodeUser).FirstOrDefault()
	End Function

End Class