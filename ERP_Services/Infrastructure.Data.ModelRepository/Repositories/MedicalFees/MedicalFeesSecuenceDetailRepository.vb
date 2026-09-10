'***********************************************************************
' Assembly         : Infrastructure.Data.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class MedicalFeesSecuenceDetailRepository
    Inherits GenericRepository(Of MedicalFeesSecuenceDetail)
    Implements IMedicalFeesSecuenceDetailRepository

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
    Public Function GetSequenseDById(id As Integer) As MedicalFeesSecuenceDetail Implements IMedicalFeesSecuenceDetailRepository.GetSequenseDById
        Dim result = (From s As MedicalFeesSecuenceDetail In Me._context.MedicalFeesSecuenceDetail.Include("MedicalFeesSecuence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New MedicalFeesSecuenceDetail()
        End If
    End Function

#End Region

End Class
