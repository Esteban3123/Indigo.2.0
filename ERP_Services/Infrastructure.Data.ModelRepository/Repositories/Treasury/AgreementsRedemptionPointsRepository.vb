#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region

Public Class AgreementsRedemptionPointsRepository

    Inherits GenericRepository(Of AgreementsRedemptionPoints)
    Implements IAgreementsRedemptionPointsRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>        
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Función que obtiene un fabricantes por Código
    ''' </summary>
    ''' <param name="Code">Código del fabricante</param>
    ''' <returns>Manufacturers</returns>
    ''' <remarks></remarks>
    Public Function GetAgreementsRedemptionPointsByCode(Code As String, Optional tracking As Boolean = True) As AgreementsRedemptionPoints Implements IAgreementsRedemptionPointsRepository.GetAgreementsRedemptionPointsRepositoryByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From t In _context.AgreementsRedemptionPoints.Include("Currency").Include("Customer").Include("AgreementsRedemptionPointsDetail") Where t.Code = Code Select t).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From t In _context.AgreementsRedemptionPoints.AsNoTracking Where t.Code = Code Select t).FirstOrDefault
            Return res
        Else
            Return New AgreementsRedemptionPoints
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas los convenios de redención de puntos
    ''' </summary>
    ''' <returns>Lista de fabricantes</returns>
    ''' <remarks></remarks>
    Public Function ListAllAgreementsRedemptionPoints() As List(Of AgreementsRedemptionPoints) Implements IAgreementsRedemptionPointsRepository.ListAllAgreementsRedemptionPointsRepository
        Dim ListManufacturers = From e In _context.AgreementsRedemptionPoints.Include("Currency").Include("Customer")
                                Select e

        If ListManufacturers.Count() > 0 Then
            Return ListManufacturers.ToList()
        Else
            Return New List(Of AgreementsRedemptionPoints)
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas los convenios de redención de puntos
    ''' </summary>
    ''' <returns>Lista de fabricantes</returns>
    ''' <remarks></remarks>
    Public Function GetAgreementsRedemptionPointsDetails(Id As Integer) As List(Of AgreementsRedemptionPointsDetail) Implements IAgreementsRedemptionPointsRepository.GetAgreementsRedemptionPointsDetails
        Dim ListManufacturers = From e In _context.AgreementsRedemptionPointsDetail
                                Where e.AgreementsRedemptionPointsId = Id
                                Select e

        If ListManufacturers.Count() > 0 Then
            Return ListManufacturers.ToList()
        Else
            Return New List(Of AgreementsRedemptionPointsDetail)
        End If
    End Function
End Class
