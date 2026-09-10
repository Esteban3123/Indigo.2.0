'***********************************************************************
' Assembly         : Infrastructure.Data.CostRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio de la entidad secuencia numerica
''' </summary>
Public Class CostSequenceDetailRepository
    Inherits GenericRepository(Of CostSecuenceDetail)
    Implements ICostSequenceDetailRepository

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

#Region "ISequenseInteropCostRepository"

    ''' <summary>
    ''' Gets the sequense d by id1.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetSequenseDById(id As Integer) As CostSecuenceDetail Implements ICostSequenceDetailRepository.GetSequenseDById
        Dim result = (From s As CostSecuenceDetail In Me._context.CostSecuenceDetail.Include("CostSecuence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New CostSecuenceDetail()
        End If
    End Function
#End Region

End Class