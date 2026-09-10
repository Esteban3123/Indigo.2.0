'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Felix Camilo Salazar Roldan
' Created          : 13-12-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Configuration
Imports System.Data.SqlClient
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Class AccountManagementSequenceDetailRepository
    Inherits GenericRepository(Of AccountManagementSequenceDetail)
    Implements IAccountManagementSequenceDetailRepository

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

#Region "ISequenseBudgetRepository"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Public Function GetSequenseDById(id As Integer) As AccountManagementSequenceDetail Implements IAccountManagementSequenceDetailRepository.GetSequenseDById
        Dim result = (From s As AccountManagementSequenceDetail In Me._context.AccountManagementSequenceDetail.Include("AccountManagementSequence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New AccountManagementSequenceDetail()
        End If
    End Function


    Public Function GetSequenseDetailUpdatedById(idSequence As Int32) As AccountManagementSequenceDetail Implements IAccountManagementSequenceDetailRepository.GetSequenseDetailUpdatedById
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        Dim queryString As String = " UPDATE AccountManagement.AccountManagementSequenceDetail SET Next = Next + 1 WHERE Id = " & idSequence & " "
        Using connection As New SqlConnection(connectionString)
            Dim Command As New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
        End Using

        Dim result = (From s As AccountManagementSequenceDetail In Me._context.AccountManagementSequenceDetail.AsNoTracking().Include("AccountManagementSequence").AsNoTracking().Include("Sequense").AsNoTracking() Where s.Id = idSequence).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            result(0).Next -= 1
            result(0).MarkAsUnchanged()
            Return result(0)
        Else
            Return New AccountManagementSequenceDetail()
        End If
    End Function
#End Region
End Class

