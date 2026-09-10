#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region

Public Class MaintenanceManufacturersRepository

    Inherits GenericRepository(Of MaintenanceManufacturers)
    Implements IMaintenanceManufacturersRepository

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
    Public Function GetMaintenanceManufacturersByCode(Code As String, Optional tracking As Boolean = True) As MaintenanceManufacturers Implements IMaintenanceManufacturersRepository.GetMaintenanceManufacturersByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From t In _context.MaintenanceManufacturers Where t.Code = Code Select t).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From t In _context.MaintenanceManufacturers.AsNoTracking Where t.Code = Code Select t).FirstOrDefault
            Return res
        Else
            Return New MaintenanceManufacturers
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas los fabricantes
    ''' </summary>
    ''' <returns>Lista de fabricantes</returns>
    ''' <remarks></remarks>
    Public Function ListAllManufacturers() As List(Of MaintenanceManufacturers) Implements IMaintenanceManufacturersRepository.ListAllMaintenanceManufacturers
        Dim ListManufacturers = From e In _context.MaintenanceManufacturers
                                Select e

        If ListManufacturers.Count() > 0 Then
            Return ListManufacturers.ToList()
        Else
            Return New List(Of MaintenanceManufacturers)
        End If


    End Function
End Class
