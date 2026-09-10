#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region


Public Class MaintenanceContractRepository

    Inherits GenericRepository(Of MaintenanceContract)
    Implements IMaintenanceContractRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="context"></param>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Función que obtiene un Contrato por codigo
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetMaintenanceContractByCode(Code As String) As MaintenanceContract Implements IMaintenanceContractRepository.GetMaintenanceContractByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If

        Dim res = (From d As MaintenanceContract In _context.MaintenanceContract.Include("MaintenanceContractDetail") Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault

        If res IsNot Nothing Then
            Dim objContractType = (From a In _context.InventoryContractType.AsNoTracking Where a.Id = res.ContractTypeId Select a).FirstOrDefault()
            res.DescriptionContractType = objContractType.Code & " - " & objContractType.Name

            Dim objSupplier = (From a In _context.Supplier.AsNoTracking Where a.Id = res.SupplierId Select a).FirstOrDefault()
            res.DescriptionSupplier = objSupplier.Code & " - " & objSupplier.Name

            For Each detail In res.MaintenanceContractDetail
                Dim physicalAsset = (From a In _context.FixedAssetPhysicalAsset.AsNoTracking.Include("FixedAssetItem").AsNoTracking() Where a.Id = detail.PhysicalAssetId Select a).FirstOrDefault()

                detail.DescriptionPlateName = physicalAsset.Plate
                detail.DescriptionItem = physicalAsset.FixedAssetItem.Description
            Next

            res.OriginalValue = (From d As MaintenanceContract In Me._context.MaintenanceContract.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New MaintenanceContract()
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas los contratos de mantenimiento
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllMaintenanceContract() As List(Of MaintenanceContract) Implements IMaintenanceContractRepository.ListAllMaintenanceContract
        Dim ListMaintenanceContract = From e In _context.MaintenanceContract
                                      Select e

        If ListMaintenanceContract.Count() > 0 Then
            Return ListMaintenanceContract.ToList()
        Else
            Return New List(Of MaintenanceContract)
        End If
    End Function

    ''' <summary>
    ''' Obtiene un activo fijo por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetPhysicalAssetById(Id As Integer) As FixedAssetPhysicalAsset Implements IMaintenanceContractRepository.GetPhysicalAssetById
        Dim PhysicalAsset = From e In _context.FixedAssetPhysicalAsset
                            Where e.Id = Id
                            Select e

        If PhysicalAsset IsNot Nothing Then
            Return PhysicalAsset.FirstOrDefault()
        Else
            Return Nothing
        End If
    End Function

End Class
