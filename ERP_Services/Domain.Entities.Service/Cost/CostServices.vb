Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports System.Data

Public Class CostServices
    Implements ICostServices
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Cost"

    Private _distributionManpowerRepository As ICostDistributionManpowerRepository
    Private _distributionFixedAssetRepository As ICostDistributionFixedAssetRepository
    Private _distributionIntermediateRepository As ICostDistributionIntermediateRepository
    Private _distributionSecondaryRepository As ICostDistributionSecondaryRepository
    Private _costActivityRepository As ICostActivityRepository

#Region "Builder"
    Public Sub New(distributionManpowerRepository As ICostDistributionManpowerRepository, distributionSecondaryRepository As ICostDistributionSecondaryRepository,
                   distributionIntermediateRepository As ICostDistributionIntermediateRepository, distributionFixedAssetRepository As ICostDistributionFixedAssetRepository,
                   costActivityRepository As ICostActivityRepository)
        If distributionManpowerRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Distribución de mano de obra")
        End If
        _distributionManpowerRepository = distributionManpowerRepository
        _distributionSecondaryRepository = distributionSecondaryRepository
        _distributionIntermediateRepository = distributionIntermediateRepository
        _distributionFixedAssetRepository = distributionFixedAssetRepository
        _costActivityRepository = costActivityRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Validates the distribution intermediate save.
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateDistributionIntermediateSave(ByVal distributionIntermediate As CostDistributionIntermediate) As ActionResult Implements ICostServices.ValidateDistributionIntermediateSave
        'Consultamos por empleado para revisar si ya day datos para este empleado
        Dim errorList As New StringBuilder()
        Dim productionCenterRepeat As Boolean = False
        Dim _distributionIntermediate As CostDistributionIntermediate = _distributionIntermediateRepository.GetDistributionIntermediateByProductionCenterIdAndYearMonth(distributionIntermediate.ProductionCenterId, distributionIntermediate.Year, distributionIntermediate.Month, False)
        If _distributionIntermediate IsNot Nothing AndAlso _distributionIntermediate.Id > 0 Then
            If distributionIntermediate.Id <> _distributionIntermediate.Id AndAlso distributionIntermediate.ProductionCenterId = _distributionIntermediate.ProductionCenterId Then
                productionCenterRepeat = True
                errorList.AppendLine("Ya existe un registro para el mismo periodo y Activo Fijo")
            End If
        End If
        If distributionIntermediate.CostDistributionIntermediateDetail.Any(Function(x) x.Proportion = 0) Then
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

#Region "AverageStandarCost"
    ''' <summary>
    ''' Carga datos desde un excel
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <returns></returns>
    Public Function ImportOrCopyAndPasteStandarCostDetails(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of StandarCostDetails)) Implements ICostServices.ImportOrCopyAndPasteStandarCostDetails
        If dataImportFile IsNot Nothing Then
            Return ImportStandarCostDetails(dataImportFile)
        Else
            Return CopyAndPasteStandarCostDetails(dataCopyPaste)
        End If
    End Function

    ''' <summary>
    ''' Import
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <returns></returns>
    Private Function ImportStandarCostDetails(dataImportFile As List(Of ImportFileRow)) As ActionResult(Of List(Of StandarCostDetails))
        Dim listErrors As New List(Of String)
        Dim listResult As New List(Of StandarCostDetails)

        'Valido la estructura
        If dataImportFile.Any(Function(a) a.Row.Count <> 6) Then
            listErrors.Add("La estructura del archivo no cumple con la requerida")
            Return New ActionResult(Of List(Of StandarCostDetails)) With {.StatusCode = eStatusResult.SUCCESS, .MessageResult = listErrors}
        End If

        Dim CodeActivity = (From x In dataImportFile Where Not String.IsNullOrEmpty(x.Row.Item(0)) Select TryCast(x.Row.Item(0).ToString(), String)).ToList()

        If Not CodeActivity.Any() Then
            listErrors.Add("La columna de actividad esta vacía")
            Return New ActionResult(Of List(Of StandarCostDetails)) With {.StatusCode = eStatusResult.SUCCESS, .MessageResult = listErrors}
        End If
        Dim activityObj = _costActivityRepository.Query(Function(q) CodeActivity.Contains(q.Code), False).Select(Function(s) New With {s.Id, s.Code, s.Name}).ToList()
        If Not activityObj.Any() Then
            listErrors.Add("Ninguna de las actividades existe")
            Return New ActionResult(Of List(Of StandarCostDetails)) With {.StatusCode = eStatusResult.SUCCESS, .MessageResult = listErrors}
        End If
        Dim activity As New With {.Id = New Integer, .Code = String.Empty, .Name = String.Empty}
        For Each row In dataImportFile
            Dim indexRow = row.IndexRow
            If row.Row.All(Function(x) String.IsNullOrEmpty(x)) Then
                Continue For
            End If

            'Valido campos
            If row.Row.Item(0) Is Nothing OrElse row.Row.Item(0) Is String.Empty Then
                listErrors.Add(String.Format("Item {0}: El código de actividad está vacio", (indexRow).ToString()))
                Continue For
            End If

            If Not activityObj.Any(Function(a) a.Code = row.Row.Item(0)) Then
                listErrors.Add(String.Format("Item {0}: La actividad no existe", (indexRow).ToString()))
                Continue For
            Else
                activity = activityObj.Where(Function(w) w.Code = row.Row.Item(0)).FirstOrDefault()
            End If

            Dim fixedAssetValue As Decimal = 0
            Dim payrollValue As Decimal = 0
            Dim inventoryValue As Decimal = 0
            Dim additionalCost As Decimal = 0
            Dim averageCost As Decimal = 0

            If Not String.IsNullOrEmpty(row.Row.Item(1)) AndAlso Not IsNumeric(row.Row.Item(1)) Then
                listErrors.Add(String.Format("Item {0}: El valor del activo fijo no es valido", (indexRow).ToString()))
                Continue For
            Else
                fixedAssetValue = CDec(row.Row.Item(1))
            End If
            If Not String.IsNullOrEmpty(row.Row.Item(2)) AndAlso Not IsNumeric(row.Row.Item(2)) Then
                listErrors.Add(String.Format("Item {0}: El valor de nómina no es valido", (indexRow).ToString()))
                Continue For
            Else
                payrollValue = CDec(row.Row.Item(2))
            End If
            If Not String.IsNullOrEmpty(row.Row.Item(3)) AndAlso Not IsNumeric(row.Row.Item(3)) Then
                listErrors.Add(String.Format("Item {0}: El valor de inventario no es valido", (indexRow).ToString()))
                Continue For
            Else
                inventoryValue = CDec(row.Row.Item(3))
            End If
            If Not String.IsNullOrEmpty(row.Row.Item(4)) AndAlso Not IsNumeric(row.Row.Item(4)) Then
                listErrors.Add(String.Format("Item {0}: El valor del costo adicional no es valido", (indexRow).ToString()))
                Continue For
            Else
                additionalCost = CDec(row.Row.Item(4))
            End If
            averageCost = fixedAssetValue + payrollValue + inventoryValue + additionalCost
            If averageCost = 0 Then
                listErrors.Add(String.Format("Item {0}: No todos los valores pueden ir en 0", (indexRow).ToString()))
            End If

            listResult.Add(New StandarCostDetails With {
                       .CostActivityId = activity.Id,
                       .CodeNameActivity = $"{activity.Code} - {activity.Name}",
                       .FixedAssetValue = fixedAssetValue,
                       .PayrollValue = payrollValue,
                       .InventoryValue = inventoryValue,
                       .AdditionalCost = additionalCost,
                       .StandarCostValue = averageCost,
                       .Observation = row.Row.Item(5).ToString()
                       })
        Next
        Return New ActionResult(Of List(Of StandarCostDetails)) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
    End Function

    ''' <summary>
    ''' Copy&Paste
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <returns></returns>
    Private Function CopyAndPasteStandarCostDetails(dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of StandarCostDetails))
        Dim listErrors As New List(Of String)
        Dim listResult As New List(Of StandarCostDetails)

        Dim CodeActivity = (From x In dataCopyPaste Where Not String.IsNullOrEmpty(x.Item(0)) Select TryCast(x.Item(0).ToString(), String)).ToList()

        If Not CodeActivity.Any() Then
            listErrors.Add("La columna de servicio esta vacía")
            Return New ActionResult(Of List(Of StandarCostDetails)) With {.StatusCode = eStatusResult.SUCCESS, .MessageResult = listErrors}
        End If
        Dim activityObj = _costActivityRepository.Query(Function(q) CodeActivity.Contains(q.Code), False).Select(Function(s) New With {s.Id, s.Code, s.Name}).ToList()
        If Not activityObj.Any() Then
            listErrors.Add("Ninguno de las actividades existe")
            Return New ActionResult(Of List(Of StandarCostDetails)) With {.StatusCode = eStatusResult.SUCCESS, .MessageResult = listErrors}
        End If
        Dim activity As New With {.Id = New Integer, .Code = String.Empty, .Name = String.Empty}
        For i = 0 To dataCopyPaste.Count - 1 Step 1
            Dim indexRow = i + 1
            'valido la estructura
            If dataCopyPaste.Item(i).Count <> 6 Then
                listErrors.Add(String.Format("La estructura del item {0} es incorrecta", (i + 1).ToString()))
                Continue For
            End If

            'Valido campos
            If dataCopyPaste.Item(i).Item(0) Is Nothing OrElse dataCopyPaste.Item(i).Item(0) Is String.Empty Then
                listErrors.Add(String.Format("Item {0}: La actividad está vacia", (indexRow).ToString()))
                Continue For
            End If

            If Not activityObj.Any(Function(a) a.Code = dataCopyPaste.Item(i).Item(0)) Then
                listErrors.Add(String.Format("Item {0}: La actividad no existe", (indexRow).ToString()))
                Continue For
            Else
                activity = activityObj.Where(Function(w) w.Code = dataCopyPaste.Item(i).Item(0)).FirstOrDefault()
            End If

            Dim fixedAssetValue As Decimal = 0
            Dim payrollValue As Decimal = 0
            Dim inventoryValue As Decimal = 0
            Dim additionalCost As Decimal = 0
            Dim averageCost As Decimal = 0

            If Not String.IsNullOrEmpty(dataCopyPaste.Item(i).Item(1)) AndAlso Not IsNumeric(dataCopyPaste.Item(i).Item(1)) Then
                listErrors.Add(String.Format("Item {0}: El valor del activo fijo no es valido", (indexRow).ToString()))
                Continue For
            Else
                fixedAssetValue = CDec(dataCopyPaste.Item(i).Item(1))
            End If
            If Not String.IsNullOrEmpty(dataCopyPaste.Item(i).Item(2)) AndAlso Not IsNumeric(dataCopyPaste.Item(i).Item(2)) Then
                listErrors.Add(String.Format("Item {0}: El valor de nómina no es valido", (indexRow).ToString()))
                Continue For
            Else
                payrollValue = CDec(dataCopyPaste.Item(i).Item(2))
            End If
            If Not String.IsNullOrEmpty(dataCopyPaste.Item(i).Item(3)) AndAlso Not IsNumeric(dataCopyPaste.Item(i).Item(3)) Then
                listErrors.Add(String.Format("Item {0}: El valor de inventario no es valido", (indexRow).ToString()))
                Continue For
            Else
                inventoryValue = CDec(dataCopyPaste.Item(i).Item(3))
            End If
            If Not String.IsNullOrEmpty(dataCopyPaste.Item(i).Item(4)) AndAlso Not IsNumeric(dataCopyPaste.Item(i).Item(4)) Then
                listErrors.Add(String.Format("Item {0}: El valor del costo adicional no es valido", (indexRow).ToString()))
                Continue For
            Else
                additionalCost = CDec(dataCopyPaste.Item(i).Item(4))
            End If
            averageCost = fixedAssetValue + payrollValue + inventoryValue + additionalCost
            If averageCost = 0 Then
                listErrors.Add(String.Format("Item {0}: No todos los valores pueden ir en 0", (indexRow).ToString()))
            End If

            listResult.Add(New StandarCostDetails With {
                       .CostActivityId = activity.Id,
                       .CodeNameActivity = $"{activity.Code} - {activity.Name}",
                       .FixedAssetValue = fixedAssetValue,
                       .PayrollValue = payrollValue,
                       .InventoryValue = inventoryValue,
                       .AdditionalCost = additionalCost,
                       .StandarCostValue = averageCost,
                       .Observation = dataCopyPaste.Item(i).Item(5).ToString()
                       })
        Next
        Return New ActionResult(Of List(Of StandarCostDetails)) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
    End Function

#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _distributionManpowerRepository = Nothing
            _distributionSecondaryRepository = Nothing
            _distributionIntermediateRepository = Nothing
            _distributionFixedAssetRepository = Nothing
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
