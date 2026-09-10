'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports System.Threading.Tasks

#End Region

''' <summary>
''' Contrato de repositorio para la entidad secuencia numerica cabecera y detalle
''' </summary>
Public Interface IInventorySequenceDetailRepository
    Inherits IRepository(Of InventorySequenceDetail)

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    <Obsolete>
    Function GetSequenseDById(ByVal id As Int32) As InventorySequenceDetail

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id de forma asincrona
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Function GetSequenseDByIdAsync(ByVal id As Int32) As Task(Of InventorySequenceDetail)

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por prefijo y id de la secuencia padre
    ''' </summary>
    ''' <param name="idSequence">Id de la secuencioa padre</param>
    Function GetSequenseDByIdSequenceAndPrefix(ByVal idSequence As Integer, inventorySecuenceID As Integer, prefix As String) As InventorySequenceDetail

	''' <summary>
	''' Obtiene la secuencia numerica primero haciendo el update para bloquear la tabla y no hayan problemas de concurrencia
	''' </summary>
	''' <param name="id">The identifier.</param>
	''' <returns></returns>
	Function GetSequenseDetailUpdatedById(ByVal id As Int32) As InventorySequenceDetail

	''' <summary>
	''' Lista todas las secuencia numericas de detalle por el id de la secuencia padre
	''' </summary>
	''' <param name="idSequence">Id de la secuencioa padre</param>
	Function ListSequenseDByIdSequence(ByVal idSequence As Integer, inventorySecuenceID As Integer) As List(Of InventorySequenceDetail)
    'Sub Actualizar(idSecuence As Long)

#End Region

End Interface