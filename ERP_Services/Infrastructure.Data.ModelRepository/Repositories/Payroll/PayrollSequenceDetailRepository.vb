'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Juan Carlos Bermudez
' Created          : 16/07/2015
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

Public Class PayrollSequenceDetailRepository
    Inherits GenericRepository(Of PayrollSequenceDetail)
    Implements IPayrollSequenceDetailRepository

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

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Public Function GetSequenseDById(id As Integer) As PayrollSequenceDetail Implements IPayrollSequenceDetailRepository.GetSequenseDById
        Dim result = (From s As PayrollSequenceDetail In Me._context.PayrollSequenceDetail.Include("PayrollSequence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New PayrollSequenceDetail()
        End If
    End Function

    Public Function GetSequenseDetailUpdatedById(idSequence As Integer) As PayrollSequenceDetail Implements IPayrollSequenceDetailRepository.GetSequenseDetailUpdatedById
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        Dim queryString As String = " UPDATE Payroll.PayrollSequenceDetail SET Next = Next + 1 WHERE Id = " & idSequence & " "
        Using connection As New SqlConnection(connectionString)
            Dim Command As New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
        End Using

        Dim result = (From s As PayrollSequenceDetail In Me._context.PayrollSequenceDetail.AsNoTracking().Include("PayrollSequence").AsNoTracking().Include("Sequense").AsNoTracking() Where s.Id = idSequence).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            result(0).Next -= 1
            result(0).MarkAsUnchanged()
            Return result(0)
        Else
            Return New PayrollSequenceDetail()
        End If
    End Function

    ''' <summary>
    ''' Incrementa atomicamente el campo Next y retorna el valor PREVIO al incremento.
    ''' Disenado para uso en bucles masivos como confirmacion masiva
    ''' NO interactua con el ChangeTracker de EF, por lo que no genera conflictos
    ''' de tracking si la entidad ya esta cargada en el contexto.
    ''' Garantiza atomicidad mediante UPDATE a OUTPUT a nivel SQL.
    ''' </summary>
    Public Function IncrementSequenceAndGetNext(idSequence As Integer) As Long Implements IPayrollSequenceDetailRepository.IncrementSequenceAndGetNext
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)

        Dim newNextValue As Long
        Dim queryString As String = "UPDATE Payroll.PayrollSequenceDetail SET [Next] = [Next] + 1 OUTPUT INSERTED.[Next] WHERE Id = @id"

        Using connection As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand(queryString, connection)
            cmd.Parameters.AddWithValue("@id", idSequence)
            cmd.Connection.Open()
            newNextValue = CLng(cmd.ExecuteScalar())
        End Using

        Return newNextValue - 1
    End Function

#End Region

End Class
