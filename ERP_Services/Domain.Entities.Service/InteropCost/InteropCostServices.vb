Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Base

Public Class InteropCostServices
    Implements IInteropCostServices

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "InteropCost"

    Private _distributionManpowerRepository As IDistributionManpowerRepository
    Private _distributionFixedAssetRepository As IDistributionFixedAssetRepository
    Private _distributionIntermediateRepository As IDistributionIntermediateRepository
    Private _distributionSecondaryRepository As IDistributionSecondaryRepository
    Private _productionCenterRepository As IProductionCenterRepository

#Region "Builder"
    Public Sub New(distributionManpowerRepository As IDistributionManpowerRepository, distributionFixedAssetRepository As IDistributionFixedAssetRepository,
                   distributionIntermediateRepository As IDistributionIntermediateRepository, distributionSecondaryRepository As IDistributionSecondaryRepository,
                   productionCenterRepository As IProductionCenterRepository)
        If distributionManpowerRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Distribución de mano de obra")
        End If
        _distributionManpowerRepository = distributionManpowerRepository
        _distributionFixedAssetRepository = distributionFixedAssetRepository
        _distributionIntermediateRepository = distributionIntermediateRepository
        _distributionSecondaryRepository = distributionSecondaryRepository
        _productionCenterRepository = productionCenterRepository
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Validates the distribution manpower save.
    ''' </summary>
    ''' <param name="distributionManpower">The distribution manpower.</param>
    ''' <returns></returns>
    Public Function ValidateDistributionManpowerSave(ByVal distributionManpower As DistributionManpower) As ActionResult Implements IInteropCostServices.ValidateDistributionManpowerSave
        'Consultamos por empleado para revisar si ya day datos para este empleado
        Dim errorList As New StringBuilder()
        Dim employeeRepeat As Boolean = False
        Dim _distributionManpower As DistributionManpower = _distributionManpowerRepository.GetDistributionManpowerByEmployeeIdAndYearMonth(distributionManpower.EmployeeId, distributionManpower.Year, distributionManpower.Month, False)
        If _distributionManpower IsNot Nothing AndAlso _distributionManpower.Id > 0 Then
            If distributionManpower.Id <> _distributionManpower.Id AndAlso distributionManpower.EmployeeId = _distributionManpower.EmployeeId Then
                employeeRepeat = True
                errorList.AppendLine("Ya existe un registro para el mismo periodo y empleado")
            End If
        End If
        If distributionManpower.HoursWorked <> distributionManpower.DistributionManpowerDetail.Sum(Function(x) x.HoursQuantity) Then
            errorList.AppendLine("El total de horas no se ha distribuido completamente")
        End If
        If distributionManpower.DistributionManpowerDetail.Any(Function(x) x.HoursQuantity = 0) Then
            errorList.AppendLine("Hay centros de producción con cantidad de horas 0")
        End If
        If errorList.Length > 0 Then
            Dim mr As String = ""
            If employeeRepeat Then
                mr = "-001"
            End If
            Return New ActionResult With {.StateResult = False, .Message = errorList.ToString(), .MessageResult = {mr}.ToList()}
        Else
            Return New ActionResult With {.StateResult = True}
        End If
    End Function

    ''' <summary>
    ''' Validates the distribution manpower save.
    ''' </summary>
    ''' <param name="distributionFixedAsset">The distribution manpower.</param>
    ''' <returns></returns>
    Public Function ValidateDistributionFixedAssetSave(ByVal distributionFixedAsset As DistributionFixedAsset) As ActionResult Implements IInteropCostServices.ValidateDistributionFixedAssetSave
        'Consultamos por empleado para revisar si ya day datos para este empleado
        Dim errorList As New StringBuilder()
        Dim employeeRepeat As Boolean = False
        Dim _distributionFixedAsset As DistributionFixedAsset = _distributionFixedAssetRepository.GetDistributionFixedAssetByFixedAssetIdAndYearMonth(distributionFixedAsset.FixedAssetId, distributionFixedAsset.Year, distributionFixedAsset.Month, False)
        If _distributionFixedAsset IsNot Nothing AndAlso _distributionFixedAsset.Id > 0 Then
            If distributionFixedAsset.Id <> _distributionFixedAsset.Id AndAlso distributionFixedAsset.FixedAssetId = _distributionFixedAsset.FixedAssetId Then
                employeeRepeat = True
                errorList.AppendLine("Ya existe un registro para el mismo periodo y Activo Fijo")
            End If
        End If
        If distributionFixedAsset.DistributionFixedAssetDetail.Sum(Function(x) x.Proportion) <> 100 Then
            errorList.AppendLine("No se ha distribuido al 100% el valor de depreciación")
        End If
        If distributionFixedAsset.DistributionFixedAssetDetail.Any(Function(x) x.DepreciationValue = 0) Then
            errorList.AppendLine("Hay detalles con valor cero")
        End If
        If distributionFixedAsset.DistributionFixedAssetDetail.Any(Function(x) x.Proportion = 0) Then
            errorList.AppendLine("Hay centros de producción con proporción 0")
        End If
        If errorList.Length > 0 Then
            Dim mr As String = ""
            If employeeRepeat Then
                mr = "-001"
            End If
            Return New ActionResult With {.StateResult = False, .Message = errorList.ToString(), .MessageResult = {mr}.ToList()}
        Else
            Return New ActionResult With {.StateResult = True}
        End If
    End Function

    ''' <summary>
    ''' Validates the distribution intermediate save.
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateDistributionIntermediateSave(ByVal distributionIntermediate As DistributionIntermediate) As ActionResult Implements IInteropCostServices.ValidateDistributionIntermediateSave
        'Consultamos por empleado para revisar si ya day datos para este empleado
        Dim errorList As New StringBuilder()
        Dim productionCenterRepeat As Boolean = False
        Dim _distributionIntermediate As DistributionIntermediate = _distributionIntermediateRepository.GetDistributionIntermediateByProductionCenterIdAndYearMonth(distributionIntermediate.ProductionCenterId, distributionIntermediate.Year, distributionIntermediate.Month, False)
        If _distributionIntermediate IsNot Nothing AndAlso _distributionIntermediate.Id > 0 Then
            If distributionIntermediate.Id <> _distributionIntermediate.Id AndAlso distributionIntermediate.ProductionCenterId = _distributionIntermediate.ProductionCenterId Then
                productionCenterRepeat = True
                errorList.AppendLine("Ya existe un registro para el mismo periodo y Activo Fijo")
            End If
        End If
        If distributionIntermediate.DistributionIntermediateDetail.Any(Function(x) x.Proportion = 0) Then
            errorList.AppendLine("Hay centros de producción con proporción 0")
        End If
        If errorList.Length > 0 Then
            Dim mr As String = ""
            If productionCenterRepeat Then
                mr = "-001"
            End If
            Return New ActionResult With {.StateResult = False, .Message = errorList.ToString(), .MessageResult = {mr}.ToList()}
        Else
            Return New ActionResult With {.StateResult = True}
        End If
    End Function

    ''' <summary>
    ''' Validates the distribution intermediate save.
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateDistributionSecondarySave(ByVal distributionIntermediate As DistributionSecondary) As ActionResult Implements IInteropCostServices.ValidateDistributionSecondarySave
        'Consultamos por empleado para revisar si ya day datos para este empleado
        Dim errorList As New StringBuilder()
        Dim productionCenterRepeat As Boolean = False
        'Dim _distributionIntermediate As DistributionSecondary = _distributionSecondaryRepository.GetDistributionSecondaryByProductionCenterIdAndYearMonth(distributionIntermediate.ProductionCenterId, distributionIntermediate.Year, distributionIntermediate.Month, False)
        'If _distributionIntermediate IsNot Nothing AndAlso _distributionIntermediate.Id > 0 Then
        '    If distributionIntermediate.Id <> _distributionIntermediate.Id AndAlso distributionIntermediate.ProductionCenterId = _distributionIntermediate.ProductionCenterId Then
        '        productionCenterRepeat = True
        '        errorList.AppendLine("Ya existe un registro para el mismo periodo y Activo Fijo")
        '    End If
        'End If
        'If errorList.Length > 0 Then
        '    Dim mr As String = ""
        '    If productionCenterRepeat Then
        '        mr = "-001"
        '    End If
        '    Return New ActionResult With {.StateResult = False, .Message = errorList.ToString(), .MessageResult = {mr}.ToList()}
        'Else
        '    Return New ActionResult With {.StateResult = True}
        'End If
        Return Nothing
    End Function

    Public Function ValidateProductionCenterSave(productionCenter As ProductionCenter) As ActionResult Implements IInteropCostServices.ValidateProductionCenterSave
        Try

            'Dim r = _productionCenterRepository.GetProductionCenterByCostCenterOidList(productionCenter.ProductionCenterCostCenter.Select(Function(x) x.CostCenterId).ToList())
            'If r IsNot Nothing AndAlso r.Count > 1 Then
            '    Return New ActionResult With {.StateResult = False, .Message = "El Hay centros de costo que ya estan relacionados a centros de producción"}
            'ElseIf r IsNot Nothing OrElse (r.Count = 1 AndAlso r(0).Id > 0 AndAlso r(0).Id = productionCenter.Id) Then
            '    Return New ActionResult With {.StateResult = True}
            'Else
            '    Return New ActionResult With {.StateResult = True}
            'End If
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = ""}
        End Try
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _distributionManpowerRepository = Nothing
            _distributionFixedAssetRepository = Nothing
            _distributionIntermediateRepository = Nothing
            _distributionSecondaryRepository = Nothing
            _productionCenterRepository = Nothing
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

End Class
