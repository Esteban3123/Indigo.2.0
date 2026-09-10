#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class GlosaSequenceDetailRepository
    Inherits GenericRepository(Of GlosaSequenceDetail)
    Implements IGlosaSequenceDetailRepository

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
    Public Function GetSequenseDById(id As Integer) As GlosaSequenceDetail Implements IGlosaSequenceDetailRepository.GetSequenseDById
        Dim result = (From s As GlosaSequenceDetail In Me._context.GlosaSequenceDetail.Include("GlosaSequence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New GlosaSequenceDetail()
        End If
    End Function


    Public Function GetSequenseDetailUpdatedById(idSequence As Int32) As GlosaSequenceDetail Implements IGlosaSequenceDetailRepository.GetSequenseDetailUpdatedById
        Dim result = (From s As GlosaSequenceDetail In Me._context.GlosaSequenceDetail.AsNoTracking().Include("GlosaSequence").AsNoTracking().Include("Sequense").AsNoTracking() Where s.Id = idSequence).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            result(0).Next -= 1
            result(0).MarkAsUnchanged()
            Return result(0)
        Else
            Return New GlosaSequenceDetail()
        End If
    End Function
#End Region

End Class
