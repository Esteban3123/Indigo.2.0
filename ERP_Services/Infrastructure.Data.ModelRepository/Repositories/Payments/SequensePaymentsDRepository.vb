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

#End Region

''' <summary>
''' Repositorio de la entidad secuencia numerica
''' </summary>
Public Class SequensePaymentsDRepository
    Inherits GenericRepository(Of PaymentsSecuenceDetail)
    Implements ISequensePaymentsDRepository

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

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Public Function GetSequenseDById(id As Integer) As PaymentsSecuenceDetail Implements ISequensePaymentsDRepository.GetSequenseDById
        Dim result = (From s As PaymentsSecuenceDetail In Me._context.PaymentsSecuenceDetail.Include("PaymentsSecuence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New PaymentsSecuenceDetail()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id, validando el Id del encabezado
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Public Function GetSequenseDByIdSequencePaymentsC(id As Integer) As PaymentsSecuenceDetail Implements ISequensePaymentsDRepository.GetSequenseDByIdSequencePaymentsC
        Dim result = (From s As PaymentsSecuenceDetail In Me._context.PaymentsSecuenceDetail.Include("PaymentsSecuence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New PaymentsSecuenceDetail()
        End If
    End Function

    Public Function GetSequenseDetailUpdatedById(idSequence As Int32) As PaymentsSecuenceDetail Implements ISequensePaymentsDRepository.GetSequenseDetailUpdatedById
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        Dim queryString As String = " UPDATE Payments.PaymentsSecuenceDetail SET Next = Next + 1 WHERE Id = " & idSequence & " "
        Using connection As New SqlConnection(connectionString)
            Dim Command As New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
        End Using

        Dim result = (From s As PaymentsSecuenceDetail In Me._context.PaymentsSecuenceDetail.AsNoTracking().Include("PaymentsSecuence").AsNoTracking().Include("Sequense").AsNoTracking() Where s.Id = idSequence).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            result(0).Next -= 1
            result(0).MarkAsUnchanged()
            Return result(0)
        Else
            Return New PaymentsSecuenceDetail()
        End If
    End Function

#End Region

End Class
