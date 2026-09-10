'***********************************************************************
' Assembly         : Infrastructure.Data.BillingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/02/2020
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

Public Class SequenceAuthorizationDRepository
    Inherits GenericRepository(Of AuthorizationSequenceDetail)
    Implements ISequenceAuthorizationDRepository

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
    Public Function GetSequenseDById(id As Integer) As AuthorizationSequenceDetail Implements ISequenceAuthorizationDRepository.GetSequenseDById
        Dim result = (From s As AuthorizationSequenceDetail In Me._context.AuthorizationSequenceDetail.Include("AuthorizationSequence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New AuthorizationSequenceDetail()
        End If
    End Function


    Public Function GetSequenseDetailUpdatedById(idSequence As Int32) As AuthorizationSequenceDetail Implements ISequenceAuthorizationDRepository.GetSequenseDetailUpdatedById
        Dim result = (From s As AuthorizationSequenceDetail In Me._context.AuthorizationSequenceDetail.AsNoTracking().Include("AuthorizationSequence").AsNoTracking().Include("Sequense").AsNoTracking() Where s.Id = idSequence).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            result(0).Next -= 1
            result(0).MarkAsUnchanged()
            Return result(0)
        Else
            Return New AuthorizationSequenceDetail()
        End If
    End Function
#End Region

End Class
