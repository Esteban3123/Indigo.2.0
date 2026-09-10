'***********************************************************************
' Assembly         : Infrastructure.Data.BillingRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 06-11-2014
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

Public Class BillingSequenceDetailRepository
    Inherits GenericRepository(Of BillingSequenceDetail)
    Implements IBillingSequenceDetailRepository

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
    Public Function GetSequenseDById(id As Integer) As BillingSequenceDetail Implements IBillingSequenceDetailRepository.GetSequenseDById
        Dim result = (From s As BillingSequenceDetail In Me._context.BillingSequenceDetail.Include("BillingSequence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New BillingSequenceDetail()
        End If
    End Function


    Public Function GetSequenseDetailUpdatedById(idSequence As Int32) As BillingSequenceDetail Implements IBillingSequenceDetailRepository.GetSequenseDetailUpdatedById
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        Dim queryString As String = " UPDATE Billing.BillingSequenceDetail SET Next = Next + 1 WHERE Id = " & idSequence & " "
        Using connection As New SqlConnection(connectionString)
            Dim Command As New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
        End Using

        Dim result = (From s As BillingSequenceDetail In Me._context.BillingSequenceDetail.AsNoTracking().Include("BillingSequence").AsNoTracking().Include("Sequense").AsNoTracking() Where s.Id = idSequence).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            result(0).Next -= 1
            result(0).MarkAsUnchanged()
            Return result(0)
        Else
            Return New BillingSequenceDetail()
        End If
    End Function
#End Region
End Class
