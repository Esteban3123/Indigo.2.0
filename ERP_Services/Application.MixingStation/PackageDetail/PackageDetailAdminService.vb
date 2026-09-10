'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 12-06-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region
Public Class PackageDetailAdminService
    Implements IPackageDetailAdminService, Inject

#Region "Properties"
    Private Const FORM_NAME As String = "FrmPackageDetail"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _packageDetailRepository As IPackageDetailRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository

#End Region

    Public Sub New(packageDetailRepository As IPackageDetailRepository)
        _packageDetailRepository = packageDetailRepository
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene el paquete por codigo
    ''' </summary>
    ''' <param name="packageId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException"></exception>
    Public Function GetPackageDetailByPackageId(packageId As Integer, audit As AuditMessage) As ActionResult(Of PackageDetail) Implements IPackageDetailAdminService.GetPackageDetailBypackageId
        If String.IsNullOrEmpty(packageId) Then
            Throw New ArgumentNullException("packageId")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim packageDetail As PackageDetail = Me._packageDetailRepository.GetPackageDetailByPackageId(packageId)
            If packageDetail IsNot Nothing AndAlso packageDetail.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PackageDetail)(packageDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of PackageDetail) With {.StateResult = True, .ObjectEmbbeded = packageDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PackageDetail) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetProductRateDetailPackageByPackageId(packageId As String) As IEnumerable(Of ProductRateDetailPackage) Implements IPackageDetailAdminService.GetProductRateDetailPackageByPackageId
        Dim packageDetails = _packageDetailRepository.GetByFilter(Function(m) m.PackageId = packageId, tracking:=False, includes:={"InventoryProduct", "InventorySupplie", "ATC"})

        Return packageDetails?.Select(Function(m) New ProductRateDetailPackage With {
            .ItemType = IIf(m.AtcId.HasValue, 1, IIf(m.SupplieId.HasValue, 2, 3)),
            .ItemCodeName = GetItemName(m),
            .ProductRateDetailId = 0,
            .PackageDetailId = m.Id,
            .ProductId = 0,
            .Quantity = 0,
            .DoseNumber = 0,
            .ItemId = IIf(.ItemType = 1, m.AtcId, IIf(.ItemType = 2, m.SupplieId, m.ProductId)),
            .MainMedicine = m.MainMedicine,
            .Thinner = m.Thinner,
            .Vehicle = m.Vehicle
        })?.ToList()
    End Function

    Private Function GetItemName(item As PackageDetail) As String
        If item.ProductId.HasValue Then
            Return $"{item.InventoryProduct.Code} - {item.InventoryProduct.Name}"
        ElseIf item.SupplieId.HasValue Then
            Return $"{item.InventorySupplie.Code} - {item.InventorySupplie.SupplieName}"
        Else
            Return $"{item.ATC.Code} - {item.ATC.Name}"
        End If
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            _packageDetailRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#Region "Properties"

#End Region
End Class
