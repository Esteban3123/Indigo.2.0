Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class RevenueRecognitionDetailRepository
    Inherits GenericRepository(Of RevenueRecognitionDetail)
    Implements IRevenueRecognitionDetailRepository

    ' contexto del repositorio
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Constructor del repositorio el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' odtiene el listado de detalles de reconocimientos de ingresos asociados a un folio
    ''' </summary>
    ''' <param name="RevenueControlDetailId"></param>
    ''' <returns></returns>
    Public Function GetRevenueRecognitionDetailByRevenueControlDetailId(RevenueControlDetailId As Integer) As List(Of RevenueRecognitionDetail) Implements IRevenueRecognitionDetailRepository.GetRevenueRecognitionDetailByRevenueControlDetailId
        Return (From rrd In _context.RevenueRecognitionDetail.AsNoTracking().Include("RevenueRecognition").AsNoTracking() Where rrd.RevenueControlDetailId = RevenueControlDetailId Select rrd).ToList()
    End Function


    'Include("RevenueRecognitionDetail").AsNoTracking().Include("RevenueRecognitionDetail.RevenueRecognition").AsNoTracking()
End Class
