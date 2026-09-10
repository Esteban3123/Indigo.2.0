#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

#End Region

''' <summary>
''' clase para hacer todas las operaciones de persistencia 
''' </summary>
''' <remarks></remarks>
Public Class MaintenanceFailureRequestRepository
    Inherits GenericRepository(Of MaintenanceFailureRequest)
    Implements IMaintenanceFailureRequestRepository

#Region "Builder"

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la nueva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Función que obtiene una Falla por Id
    ''' </summary>
    ''' <param name="Id">Id de la Falla</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetMaintenanceFailureRequestById(Id As Integer) As MaintenanceFailureRequest Implements IMaintenanceFailureRequestRepository.GetMaintenanceFailureRequestById
        Dim maintenanceFailureRequest = (From d As MaintenanceFailureRequest In Me._context.MaintenanceFailureRequest.
                    Include("MaintenanceFailureRequestDetail").
                    Include("MaintenanceFailureRequestDetail.MaintenanceFailureRequestDetailNotification")
                                         Where d.Id = Id
                                         Select d).FirstOrDefault

        maintenanceFailureRequest.Company = (From gls In Me._context.GeneralLedgerSettings.AsNoTracking().Include("ThirdParty").AsNoTracking() Select gls.ThirdParty.Name).FirstOrDefault()
        If maintenanceFailureRequest.BranchOfficeId IsNot Nothing Then
            maintenanceFailureRequest.BranchOfficeCodeName = (From bo In Me._context.BranchOffice.AsNoTracking() Where bo.Id = maintenanceFailureRequest.BranchOfficeId Select bo.Name).FirstOrDefault()
        End If

        For Each detail In maintenanceFailureRequest.MaintenanceFailureRequestDetail
            Dim physicalAsset = (From fapa In Me._context.FixedAssetPhysicalAsset.AsNoTracking() Where fapa.Id = detail.PhysicalAssetId).FirstOrDefault()
            Dim responsible = (From far In Me._context.FixedAssetResponsible.AsNoTracking().Include("ThirdParty").AsNoTracking() Where far.Id = physicalAsset.ResponsibleId).FirstOrDefault()
            Dim phone = (From p In Me._context.Phone.AsNoTracking() Where p.IdPerson = responsible.ThirdParty.PersonId).FirstOrDefault()

            detail.NameArticle = (From fai In Me._context.FixedAssetItem.AsNoTracking() Where fai.Id = physicalAsset.ItemId Select fai.Description).FirstOrDefault()
            detail.Plate = physicalAsset.Plate
            detail.Model = physicalAsset.Model
            detail.Serie = physicalAsset.Serie
            detail.Location = (From fal In Me._context.FixedAssetLocation.AsNoTracking() Where fal.Id = physicalAsset.LocationId Select fal.Name).FirstOrDefault()
            detail.Responsible = responsible.ThirdParty.Name
            detail.ResponsiblePhone = If(phone IsNot Nothing, phone.Phone1, "-")
        Next

        Return maintenanceFailureRequest
    End Function

    ''' <summary>
    ''' Obtiene un activo a reportar
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMaintenanceFailureRequestByCode(Code As String) As MaintenanceFailureRequest Implements IMaintenanceFailureRequestRepository.GetMaintenanceFailureRequestByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d As MaintenanceFailureRequest In Me._context.MaintenanceFailureRequest.Include("MaintenanceFailureRequestDetail") Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            If res.BranchOfficeId IsNot Nothing Then
                Dim branchOffice = (From bo As BranchOffice In Me._context.BranchOffice.AsNoTracking() Where bo.Id = res.BranchOfficeId).FirstOrDefault
                res.BranchOfficeCodeName = String.Format("{0} - {1}", branchOffice.Code, branchOffice.Name)
            End If

            If res.MaintenanceFailureRequestDetail IsNot Nothing AndAlso res.MaintenanceFailureRequestDetail.Count > 0 Then
                For Each ObjMaintenanceFailureRequestDetail As MaintenanceFailureRequestDetail In res.MaintenanceFailureRequestDetail
                    Dim PhysicalAssetId = ObjMaintenanceFailureRequestDetail.PhysicalAssetId
                    Dim PhysicalAssetPartsId = ObjMaintenanceFailureRequestDetail.PhysicalAssetPartsId

                    Dim ObjPhysicalAsset = (From a In _context.FixedAssetPhysicalAsset.AsNoTracking.Include("FixedAssetItem").AsNoTracking Where a.Id = PhysicalAssetId Select a).FirstOrDefault()
                    ObjMaintenanceFailureRequestDetail.Plate = ObjPhysicalAsset.Plate
                    ObjMaintenanceFailureRequestDetail.NameArticle = ObjPhysicalAsset.FixedAssetItem.Description

                    If PhysicalAssetPartsId IsNot Nothing Then
                        Dim ObjPhysicalAssetParts = (From a In _context.FixedAssetPhysicalAssetParts.AsNoTracking.Include("FixedAssetPartsAccesoriesConsumables").AsNoTracking Where a.Id = PhysicalAssetPartsId Select a).FirstOrDefault()
                        ObjMaintenanceFailureRequestDetail.PartsName = ObjPhysicalAssetParts.FixedAssetPartsAccesoriesConsumables.Name
                    End If

                    If ObjMaintenanceFailureRequestDetail.TransactionClass.ToString IsNot Nothing Then
                        Select Case ObjMaintenanceFailureRequestDetail.TransactionClass
                            Case 1
                                ObjMaintenanceFailureRequestDetail.NameClassPart = "Activo"
                            Case 2
                                ObjMaintenanceFailureRequestDetail.NameClassPart = "Parte de Activo"
                        End Select
                    End If

                    If res.TypeRequest.ToString IsNot Nothing Then
                        Dim TypeRol = (From p In _context.MaintenanceFailureRequest.AsNoTracking Where p.TypeRequest = res.TypeRequest Select p).FirstOrDefault
                        Select Case res.TypeRequest
                            Case 1
                                TypeRol.NameRequest = "Falla General"
                            Case 2
                                TypeRol.NameRequest = "Revisión"
                            Case 3
                                TypeRol.NameRequest = "Otro"
                        End Select
                    End If

                    If res.TypeRequest.ToString IsNot Nothing Then
                        Dim Report = (From p In _context.MaintenanceFailureRequest.AsNoTracking Where p.Report = res.Report Select p).FirstOrDefault
                        Select Case res.TypeRequest
                            Case 1
                                Report.NameReport = "Usuario del Sistema"
                            Case 2
                                Report.NameReport = "Otro"
                        End Select
                    End If
                Next
            End If

            res.OriginalValue = (From d As MaintenanceFailureRequest In Me._context.MaintenanceFailureRequest.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New MaintenanceFailureRequest()
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas las tipos de fallas
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllMaintenanceFailureRequest() As List(Of MaintenanceFailureRequest) Implements IMaintenanceFailureRequestRepository.ListAllMaintenanceFailureRequest
        Dim ListMaintenanceFailureRequest = From e In _context.MaintenanceFailureRequest
                                            Select e

        If ListMaintenanceFailureRequest.Count() > 0 Then
            Return ListMaintenanceFailureRequest.ToList()
        Else
            Return New List(Of MaintenanceFailureRequest)
        End If
    End Function

#End Region

End Class
