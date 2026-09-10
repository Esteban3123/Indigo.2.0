Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class RevenueRecognitionRepository
    Inherits GenericRepository(Of RevenueControl)
    Implements IRevenueRecognitionRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function SP_GenerateRecognition(careGroupId As Integer, careGroupTotal As Decimal, operatingUnitId As Integer, recognitionDate As Date, userCode As String) As SP_GenerateRecognition_Result Implements IRevenueRecognitionRepository.SP_GenerateRecognition
        DirectCast(_context, System.Data.Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateRecognition(careGroupId, careGroupTotal, operatingUnitId, recognitionDate, userCode).FirstOrDefault()
    End Function

    Public Function SP_ReverseRecognition(revenueRecognitionId As Integer, userCode As String) As SP_ReverseRecognition_Result Implements IRevenueRecognitionRepository.SP_ReverseRecognition
        Return _context.SP_ReverseRecognition(revenueRecognitionId, userCode).FirstOrDefault()
    End Function

End Class
