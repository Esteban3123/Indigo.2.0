'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Configuration
Imports Infrastructure.CrossCutting.Base
Imports System.Data.SqlClient
Imports System.Data.Entity

Public Class ProductTemplateRepository
    Inherits GenericRepository(Of ProductRate)
    Implements IProductTemplateRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ProductTemplateRepository"/> class.
    ''' </summary>
    ''' <param name="context">The context.</param>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Obtiene un cubrimiento por codigo
    ''' </summary>
    Public Async Function GetProductTemplate(code As String) As Task(Of ProductRate) Implements IProductTemplateRepository.GetProductTemplate
        If String.IsNullOrEmpty(code) Then Throw New ArgumentNullException(NameOf(code))

        Dim query = Await (From p In _context.ProductRate Where p.Code.Equals(code) Select p).FirstOrDefaultAsync()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = Await (From p In _context.ProductRate.AsNoTracking() Where p.Code.Equals(code) Select p).FirstOrDefaultAsync()
            Return query
        Else
            Return New ProductRate()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un cubrimiento por id
    ''' </summary>
    Public Function GetProductTemplateById(id As Integer) As ProductRate Implements IProductTemplateRepository.GetProductTemplateById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From p In _context.ProductRate Where p.Id = id Select p).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From p In _context.ProductRate.AsNoTracking() Where p.Id = id Select p).FirstOrDefault()
            Return query
        Else
            Return New ProductRate()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un cubrimiento por id
    ''' </summary>
    Public Function GetProductRateGeneralConditionByTemplateById(id As Integer) As List(Of ProductRateGeneral) Implements IProductTemplateRepository.GetProductRateGeneralConditionByTemplateById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From p In _context.ProductRateGeneral.Include("ProductRateGeneralCondition") Where p.ProductRateId = id Select p).ToList()
        If query IsNot Nothing AndAlso query.Count > 0 Then
            Return query
        Else
            Return New List(Of ProductRateGeneral)
        End If
    End Function

    ''' <summary>
    ''' Obtiene un cubrimiento por id
    ''' </summary>
    Public Function GetProductTemplateAll() As List(Of ProductRate) Implements IProductTemplateRepository.GetProductTemplateAll
        Dim query = (From p In _context.ProductRate.Include("ProductRateDetail"))
        If query IsNot Nothing AndAlso query.Count > 0 Then
            Return query.ToList()
        Else
            Return New List(Of ProductRate)
        End If
    End Function


    ''' <summary>
    ''' Borrar detalles
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Sub DeleteDetailUpdatedById(id As Integer) Implements IProductTemplateRepository.DeleteDetailUpdatedById
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        Dim queryString As String = " Delete From Inventory.ProductRateDetail WHERE ProductRateId = " & id & " "
        Using connection As New SqlConnection(connectionString)
            Dim Command As New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
        End Using
    End Sub

    ''' <summary>
    ''' Borrar detalles
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Sub DeleteConditionsAllById(id As Integer) Implements IProductTemplateRepository.DeleteConditionsAllById
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        ''Se eliminan primero las condiciones
        Dim queryString As String = " Delete prdc  From Inventory.ProductRateGeneralCondition prdc
                                      join Inventory.ProductRateGeneral prg on prg.Id = prdc.ProductRateGeneralId
                                      WHERE  prg.ProductRateId = " & id & " "
        Using connection As New SqlConnection(connectionString)
            Dim Command As New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
        End Using
        ''se elimina la tarifa
        Dim queryString2 As String = " Delete From Inventory.ProductRateGeneral WHERE ProductRateId = " & id & " "
        Using connection As New SqlConnection(connectionString)
            Dim Command As New SqlCommand(queryString2, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
        End Using
    End Sub

    ''' <summary>
    ''' Borrar detalles
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Sub DeleteConditionsById(id As Integer) Implements IProductTemplateRepository.DeleteConditionsById
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        ''Se eliminan primero las condiciones
        Dim queryString As String = " Delete From Inventory.ProductRateGeneralCondition WHERE ProductRateGeneralId = " & id & " "
        Using connection As New SqlConnection(connectionString)
            Dim Command As New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
        End Using
        ''se elimina la tarifa
        Dim queryString2 As String = " Delete From Inventory.ProductRateGeneral WHERE Id = " & id & " "
        Using connection As New SqlConnection(connectionString)
            Dim Command As New SqlCommand(queryString2, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
        End Using
    End Sub
#End Region

End Class