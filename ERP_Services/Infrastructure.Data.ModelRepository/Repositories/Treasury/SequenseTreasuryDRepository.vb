'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
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
Public Class SequenseTreasuryDRepository
    Inherits GenericRepository(Of TreasurySequenceDetail)
    Implements ISequenseTreasuryDRepository

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

#Region "ISequenseTreasuryRepository"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Public Function GetSequenseDById(id As Integer) As TreasurySequenceDetail Implements ISequenseTreasuryDRepository.GetSequenseDById
        Dim result = (From s As TreasurySequenceDetail In Me._context.TreasurySequenceDetail.Include("TreasurySequence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New TreasurySequenceDetail()
        End If
    End Function



#End Region

End Class
