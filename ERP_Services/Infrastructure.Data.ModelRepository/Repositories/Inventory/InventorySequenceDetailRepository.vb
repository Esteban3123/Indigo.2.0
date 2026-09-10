'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Configuration
Imports Infrastructure.CrossCutting.Base
Imports System.Data.SqlClient
Imports System.Data.Entity

#End Region

''' <summary>
''' Repositorio de la entidad secuencia numerica
''' </summary>
Public Class InventorySequenceDetailRepository
    Inherits GenericRepository(Of InventorySequenceDetail)
    Implements IInventorySequenceDetailRepository

#Region "Fields"

    ''' <summary>
    ''' Contexto de contabilidad
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="acountingContext">Contexto de cintabilidad</param>
    Public Sub New(ByVal acountingContext As IGlobalModelUnitOfWork)
        MyBase.New(acountingContext)
        Me._context = acountingContext
    End Sub

#End Region

#Region "ISequensePaymentsRepository"
    'Public Sub ActualizarSecuencia(idSecuence As Long) Implements IInventorySequenceDetailRepository.ActualizarSecuencia
    '    _context.InventorySequenceDetail.Add(_context.InventorySequenceDetail.Find(idSecuence))
    'End Sub

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Public Function GetSequenseDById(id As Integer) As InventorySequenceDetail Implements IInventorySequenceDetailRepository.GetSequenseDById
        Dim result = (From s As InventorySequenceDetail In Me._context.InventorySequenceDetail.Include("InventorySequence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New InventorySequenceDetail()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Public Async Function GetSequenseDByIdAsync(id As Integer) As Task(Of InventorySequenceDetail) Implements IInventorySequenceDetailRepository.GetSequenseDByIdAsync
        Dim result = Await (From s As InventorySequenceDetail In Me._context.InventorySequenceDetail.Include("InventorySequence").Include("Sequense")
                            Where s.Id = id).ToListAsync()

        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New InventorySequenceDetail()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="idSequence">Id de la secuencioa padre</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Public Function GetSequenseDByIdSequenceAndPrefix(ByVal idSequence As Integer, inventorySecuenceID As Integer, prefix As String) As InventorySequenceDetail Implements IInventorySequenceDetailRepository.GetSequenseDByIdSequenceAndPrefix
        Dim result = (From s As InventorySequenceDetail In Me._context.InventorySequenceDetail.Include("InventorySequence").Include("Sequense") Where s.IdSequense = idSequence And s.InventorySequenceId = inventorySecuenceID And s.Prefix = prefix).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New InventorySequenceDetail()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la secuencia numerica primero haciendo el update para bloquear la tabla y no hayan problemas de concurrencia
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetSequenseDetailUpdatedById(id As Integer) As InventorySequenceDetail Implements IInventorySequenceDetailRepository.GetSequenseDetailUpdatedById
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        Dim queryString As String = " UPDATE Inventory.InventorySequenceDetail SET Next = Next + 1 WHERE Id = " & id & " "
        Using connection As New SqlConnection(connectionString)
            Dim Command As New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
        End Using

        Dim result = (From s As InventorySequenceDetail In Me._context.InventorySequenceDetail.AsNoTracking().Include("InventorySequence").AsNoTracking().Include("Sequense").AsNoTracking() Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            result(0).Next -= 1
            result(0).MarkAsUnchanged()
            Return result(0)
        Else
            Return New InventorySequenceDetail()
        End If
    End Function

    ''' <summary>
    ''' Lista todas las secuencia numericas de detalle por el id de la secuencia padre
    ''' </summary>
    ''' <param name="idSequence">Id de la secuencioa padre</param>
    Public Function ListSequenseDByIdSequence(idSequence As Integer, inventorySecuenceID As Integer) As List(Of InventorySequenceDetail) Implements IInventorySequenceDetailRepository.ListSequenseDByIdSequence
        Dim result = (From s As InventorySequenceDetail In Me._context.InventorySequenceDetail.Include("InventorySequence").Include("Sequense") Where s.IdSequense = idSequence And s.InventorySequenceId = inventorySecuenceID).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result
        Else
            Return New List(Of InventorySequenceDetail)()
        End If
    End Function

#End Region

End Class
