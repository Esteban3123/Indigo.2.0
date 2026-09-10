'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/01/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmAssignPackage

    Public Sub New()
        InitializeComponent()
        addControlToBarButton()
    End Sub

    Private Sub addControlToBarButton()
        ctrTmp = New CtrDatosPacienteNPT()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        AdditionalControlPanel.Parent.MinimumSize = New System.Drawing.Size(450, AdditionalControlPanel.Height)
        AdditionalControlPanel.Parent.MaximumSize = New System.Drawing.Size(450, AdditionalControlPanel.Height)
        AdditionalControlPanel.MinimumSize = New System.Drawing.Size(450, AdditionalControlPanel.Height)
        AdditionalControlPanel.MaximumSize = New System.Drawing.Size(450, AdditionalControlPanel.Height)
    End Sub

#Region "Event"
    Private ctrTmp As CtrDatosPacienteNPT

    ''' <summary>
    ''' Evento para agregar un paquete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddPackageArgs(sender As Object, e As EventArgs)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del paquete seleccionado
    ''' </summary>
    Public Property PackageId As Integer?
        Get
            Return CType(INDslePackage.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDslePackage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el volumen total del preparado
    ''' </summary>
    Public Property VolumenTotal As Decimal
        Get
            Return Math.Round(CType(INDVolumenTotal.EditValue, Decimal), 2)
        End Get
        Set(value As Decimal)
            Try
                INDVolumenTotal.EditValue = value
            Catch ex As Exception
                INDVolumenTotal.EditValue = 0
            End Try
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la concentracion concatenada con las unidades de medida
    ''' </summary>
    Public Property ConcentrationWithMeasure As String
        Get
            Return INDtxtConcentration.EditValue
        End Get
        Set(value As String)
            INDtxtConcentration.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la concentracion
    ''' </summary>
    Public Property Concentration As Decimal

    ''' <summary>
    ''' Obtiene o establece si el tipo de preparacion es tipo estandar o perzonalizada
    ''' (Personalizada = True, Estandar = False)
    ''' </summary>
    Public Property PersonalizedMasterPreparation As Boolean
        Get
            Return INDslePersonalizedMasterPreparation.EditValue
        End Get
        Set(value As Boolean)
            INDslePersonalizedMasterPreparation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la linea de produccion seleccionada
    ''' </summary>
    Public Property ProductionLine As Integer?
        Get
            Return CType(INDsleProductionLine.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDsleProductionLine.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Entidad del diluyente
    ''' </summary>
    Private ReadOnly Property Thinner As PackageDetail
        Get
            Return packageToCreate?.PackageDetail.FirstOrDefault(Function(x) x.Thinner)
        End Get
    End Property

    ''' <summary>
    ''' Entidad del vehiculo
    ''' </summary>
    Private ReadOnly Property Vehicle As PackageDetail
        Get
            Return packageToCreate?.PackageDetail.FirstOrDefault(Function(x) x.Vehicle)
        End Get
    End Property

    ''' <summary>
    ''' Entidad del medicamento principal
    ''' </summary>
    Private ReadOnly Property MainMedicine As PackageDetail
        Get
            Return packageToCreate?.PackageDetail.FirstOrDefault(Function(x) x.MainMedicine)
        End Get
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa a la entidad de paquete
    ''' </summary>
    Private MixinStationPackageXpo As MixinStationPackageXpo

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PDashboardConfirmationUnitDose

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Representa al registro al cual se le va asignar el paquete
    ''' </summary>
    Public ViewListDashboardConfirmationUnitDoseXpo As ViewListDashboardConfirmationUnitDoseXpo

    ''' <summary>
    ''' Listado para asignar el paquete
    ''' </summary>
    Public ItemsToAssign As List(Of ViewListDashboardConfirmationUnitDoseXpo)

    Private confirmationUnitDoseList As New List(Of ConfirmationUnitDose)()

    ''' <summary>
    ''' Entidad que guarda el paquete
    ''' </summary>
    Private confirmationUnitDose As ConfirmationUnitDose

    ''' <summary>
    ''' Id del paquete personalizado asignado en preparación magistral personalizada
    ''' </summary>
    Private PersonalizedMasterPreparationPackageId As Integer

    ''' <summary>
    ''' Entidad del paquete personalizado que se va a crear 
    ''' </summary>
    Private packageToCreate As Package

#End Region

#Region "Methods"

    ''' <summary>
    ''' Abre el form de paquetes
    ''' </summary>
    Private Sub OpenFormPackage(isPersonalizedMasterPreparation As Boolean, Optional ListPackage As List(Of MixinStationPackageDetailXpo) = Nothing)
        If PackageId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un paquete base"
            Return
        End If

        Using formulario As New FrmPackage()
            AddHandler formulario.AddPackageArgs, AddressOf ReturnOpenFormPackageEventArgs
            formulario.MixinStationPackageXpo = packageToCreate
            formulario.IsDashboardConfirmationUnitDose = True
            formulario.IsPersonalizedMasterPreparation = isPersonalizedMasterPreparation
            formulario.ViewListDashboardConfirmationUnitDoseXpo = ViewListDashboardConfirmationUnitDoseXpo

            ' Inicializar UnitDoseType desde MixinStationPackageXpo para evitar consultas adicionales
            If MixinStationPackageXpo?.UnitDoseTypeId IsNot Nothing Then
                formulario.UnitDoseTypeFromDashboard = New UnitDoseType() With {
                    .Id = MixinStationPackageXpo.UnitDoseTypeId.Id,
                    .Code = MixinStationPackageXpo.UnitDoseTypeId.Code,
                    .Description = MixinStationPackageXpo.UnitDoseTypeId.Description,
                    .MSClass = MixinStationPackageXpo.UnitDoseTypeId.MSClass,
                    .State = MixinStationPackageXpo.UnitDoseTypeId.State
                }
            End If

            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.7
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.7
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.ListPackageDetailXpo = ListPackage
            formulario.ListPackageDetailUnitDoseConfirm = INDgcPersonalized.DataSource
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que se ejecuta al guardar un paquete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnOpenFormPackageEventArgs(sender As Object, e As SendPackageArgs)
        If e.IsPersonalizedMasterPreparation = False Then 'Si no es una preparación magistral, significa que se abrió el form de paquete desde el mas del control
            INDslePackage.Properties.DataSource = Nothing
            PackageId = Nothing
            INDslePackage.Properties.NullText = String.Empty

            PackageId = e.Package.Id
            INDslePackage.Properties.NullText = e.Package.Code + " - " + e.Package.Name
        Else 'Si es una preparación magistral se abrió el form desde el botón de la sección de preparación magistral
            INDtxtPersonalizedMasterPreparation.EditValue = e.Package.Code + " - " + e.Package.Name
            PersonalizedMasterPreparationPackageId = e.Package.Id
            packageToCreate = e.Package

            INDgcPersonalized.DataSource = e.Package.PackageDetail.
                                            OrderBy(Function(x) If(x.NPTItemOrder.HasValue, 0, 1)). ' Los NULL (Nothing) van al final
                                            ThenBy(Function(x) x.NPTItemOrder.GetValueOrDefault(255)). ' Orden ascendente por NPTItemOrder
                                            ToList()

            ' Asignar valores de concentración y volumen total del preparado desde el paquete personalizado
            If e.Package.VolumeTotalPrepared.HasValue Then
                VolumenTotal = e.Package.VolumeTotalPrepared.Value
            End If
            If Not String.IsNullOrEmpty(e.Package.ConcentrationAntibiotic) Then
                ConcentrationWithMeasure = e.Package.ConcentrationAntibiotic
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que guarda un paquete
    ''' </summary>
    Public Async Sub Guardar()
        If Not ValidateControls() Then
            Exit Sub
        End If

        If {EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Antibiotics}.Contains(MixinStationPackageXpo.UnitDoseTypeId?.MSClass) Then
            If VolumenTotal = Decimal.Zero Then
                Mensaje(EeventViewerImages.Advertencia) = "El volumen total no puede ser cero"
                Exit Sub

            ElseIf ConcentrationWithMeasure = String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = "La concentracion no puede estar vacia"
                Exit Sub
            End If

            If packageToCreate.PackageDetail.Any(Function(x) x.Quantity = 0) Then
                Mensaje(EeventViewerImages.Advertencia) = "Existen detalles con cantidad en cero"
                Exit Sub
            End If
        End If

        AssigningValues()
        Me.AsyncLoader(True)
        Try
            Using model As New MDashboardConfirmationUnitDose(Me.Tag.ToString())
                ' Solo regenerar la descripción si no existe (evita sobrescribir la que viene de FrmPackage con medicamento complementario)
                If packageToCreate.PackageDetail?.Any() AndAlso String.IsNullOrEmpty(packageToCreate.Description) Then
                    packageToCreate.Description = GetPackageDescription(packageToCreate.PackageDetail.ToList())
                End If

                ' Crear una copia limpia del paquete sin ChangeTracker activo
                ' El ChangeTracker con datos causa problemas de serialización con BinaryFormatter
                Dim cleanPackage = CreateCleanPackageForSave(packageToCreate)

                Dim result = Await model.SaveConfirmationUnitDoseAndPackageList(confirmationUnitDoseList, cleanPackage)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = "Paquete asignado correctamente"
                    Me.confirmationUnitDose = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.AsyncLoader(False)
                    RaiseEvent AddPackageArgs(Nothing, Nothing)
                    Me.Close()
                Else
                    Me.AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad principal
    ''' </summary>
    Private Sub AssigningValues()
        confirmationUnitDoseList.Clear()

        For Each item In ItemsToAssign
            confirmationUnitDose = New ConfirmationUnitDose()
            With confirmationUnitDose
                .ServiceCode = item.ServiceCode
                .ServiceName = item.ServiceName
                .CareCenterCode = item.CareCenterCode
                .Dosage = item.Dosage
                .Quantity = item.TotalQuantity
                .Source = item.SourceType
                .PersonalizedMasterPreparation = PersonalizedMasterPreparation
                .PackageId = PackageId
                .ProductionLineId = ProductionLine
                .Status = 1
                .CMConfigurationId = item.CMConfigurationId
                .KeyView = item.Id

                If item.SourceType = 1 Then
                    .GroupingCodeDose = New Guid(item.AGRUPAQUETE)
                End If

                .PersonalizedMasterPreparationPackageId = Nothing
            End With

            confirmationUnitDoseList.Add(confirmationUnitDose)
        Next
    End Sub

    ''' <summary>
    ''' carga los search quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listYesNot = New List(Of Tuple(Of Boolean, String))
        listYesNot.Add(New Tuple(Of Boolean, String)(True, "Personalizada"))
        listYesNot.Add(New Tuple(Of Boolean, String)(False, "Estándar"))
        INDslePersonalizedMasterPreparation.Properties.DataSource = listYesNot.ToList
    End Sub

    ''' <summary>
    ''' Funcion para realizar la creacion del paquete personalizado cuando viene de una orden medica
    ''' </summary>
    ''' <param name="ListPackageDetail"></param>
    Private Sub Magistralpackage(ListPackageDetail As List(Of MixinStationPackageDetailXpo))
        Dim resultValidationToAutomaticProcess = ValidationToAutomaticProcess(ListPackageDetail)

        If resultValidationToAutomaticProcess.StateResult Then
            Dim args As New SendPackageArgs
            args.Package = resultValidationToAutomaticProcess.ObjectEmbbeded
            args.IsPersonalizedMasterPreparation = True
            ReturnOpenFormPackageEventArgs(Nothing, args)
        ElseIf resultValidationToAutomaticProcess.StateResult = False And resultValidationToAutomaticProcess.StatusCode <> eStatusResult.EXCEPTION Then
            CustomDose(ListPackageDetail)
        Else
            Mensaje(EeventViewerImages.Advertencia) = resultValidationToAutomaticProcess.Message
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Funcion de validacion Para automatizar el proceso de guardado en la creacion de paquete personalizado
    ''' </summary>
    ''' <param name="listPackageDetail"></param>
    ''' <returns></returns>
    Private Function ValidationToAutomaticProcess(listPackageDetail As List(Of MixinStationPackageDetailXpo)) As ActionResult(Of Package)
        Try
            Dim packageEntity As New Package
            Dim newListPackageDetail As New List(Of PackageDetail)

            Dim newListPackageDetailNoParenteralNutrition = Me.GetListPackageDetailNoParenteralNutrition(listPackageDetail)
            If newListPackageDetailNoParenteralNutrition.Any Then
                newListPackageDetail.AddRange(newListPackageDetailNoParenteralNutrition)
            End If
            Dim ListPackageDetailParenteralNutritionParentalListPackageDetailParental = Me.GetListPackageDetailParenteralNutritionParental(listPackageDetail)
            If ListPackageDetailParenteralNutritionParentalListPackageDetailParental.Any Then
                newListPackageDetail.AddRange(ListPackageDetailParenteralNutritionParentalListPackageDetailParental)
            End If

            With packageEntity
                If {EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Antibiotics}.Contains(MixinStationPackageXpo.UnitDoseTypeId?.MSClass) Then
                    .VolumeTotalPrepared = CalculateVolumenTotalPrepared(listPackageDetail)
                    .MeasurementPreparedId = MixinStationPackageXpo.MeasurementPreparedId
                    VolumenTotal = .VolumeTotalPrepared
                    .PreparationType = MixinStationPackageXpo.PreparationType

                    If PersonalizedMasterPreparation Then
                        Dim MainMedicineTmp = newListPackageDetail.FirstOrDefault(Function(y) y.MainMedicine)
                        If MainMedicineTmp?.ATC?.FormulationType = 1 Then
                            Dim ThinnerTmp = newListPackageDetail.FirstOrDefault(Function(y) y.Thinner)
                            If ThinnerTmp IsNot Nothing Then
                                ThinnerTmp.Quantity = CalculateQuantityThinnerForPackagePerzonalized(ThinnerTmp, ViewListDashboardConfirmationUnitDoseXpo.Dosage, MainMedicineTmp)
                                ThinnerTmp.Dosis = $"{ThinnerTmp.Quantity} {ThinnerTmp.MeasureUnitDescription.Split("-")(1).Trim()}"
                                ThinnerTmp.QuantityMeasureunitname = $"{ThinnerTmp.Quantity} {ThinnerTmp.MeasurementUnitAbbreviation}"
                            End If
                        End If

                        Dim VehicleTmp = newListPackageDetail.FirstOrDefault(Function(y) y.Vehicle)
                        If VehicleTmp IsNot Nothing Then
                            VehicleTmp.Quantity = CalculateQuantityVehicleForPackagePerzonalized(VolumenTotal, newListPackageDetail)
                        End If
                    End If

                    ConcentrationWithMeasure = CalculateConcentration(VolumenTotal, newListPackageDetail)
                    .ConcentrationAntibiotic = ConcentrationWithMeasure

                ElseIf MixinStationPackageXpo.UnitDoseTypeId?.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
                    .VolumeTotalOrderPurga = newListPackageDetail.Where(Function(j) j.ComponentType = 1).Sum(Function(f) f.Quantity) + MixinStationPackageXpo.Purge
                    .WeightTotalSolution = newListPackageDetail.Where(Function(j) j.ComponentType = 1).Sum(Function(f) (f.Quantity + MixinStationPackageXpo.Purge) * f.Density)
                    .VolumeTotalOrder = ViewListDashboardConfirmationUnitDoseXpo.Dosage
                    .VolumeTotalOrderMeasurementUnitId = MixinStationPackageXpo.VolumeTotalOrderMeasurementUnitId?.Id
                Else
                    .VolumeTotalOrder = newListPackageDetail.Where(Function(j) (j.Thinner = True OrElse j.Vehicle = True) AndAlso j.ComponentType = 1).Sum(Function(x) x.Quantity)
                    .VolumeTotalOrderMeasurementUnitId = MixinStationPackageXpo.VolumeTotalOrderMeasurementUnitId?.Id
                    .VolumeTotalOrderMeasurementUnitCodeName = MixinStationPackageXpo.VolumeTotalOrderMeasurementUnitId?.CodeName

                    .VolumeTotalOrderPurga = 0
                    .WeightTotalSolution = 0

                    .Concentration = MixinStationPackageXpo.Concentration
                    .ConcentrationMeasurementUnitId = MixinStationPackageXpo.ConcentrationMeasurementUnitId?.Id
                    .ConcentrationMeasurementUnitCodeName = MixinStationPackageXpo.ConcentrationMeasurementUnitId?.CodeName
                End If

                .IsPackagePersonalized = True
                .Name = String.Format("{0} \ {1}", MixinStationPackageXpo.Name, ViewListDashboardConfirmationUnitDoseXpo.SourceName)
                .Description = GetPackageDescription(newListPackageDetail)
                .RiskLevelId = MixinStationPackageXpo.RiskLevelId?.Id
                .RiskLevelCodeName = MixinStationPackageXpo.RiskLevelId?.CodeName
                .CodeAlternative = MixinStationPackageXpo.CodeAlternative
                .PhotoProtection = MixinStationPackageXpo.PhotoProtection

                .StabilityHour = MixinStationPackageXpo.StabilityHour
                .EnvironmentalTemperatureTerm = MixinStationPackageXpo.EnvironmentalTemperatureTerm
                .Purge = MixinStationPackageXpo.Purge
                .Storage = MixinStationPackageXpo.Storage
                .PreparationInstructions = MixinStationPackageXpo.PreparationInstructions
                .SpecialConsiderations = MixinStationPackageXpo.SpecialConsiderations
                .UnitDoseTypeId = MixinStationPackageXpo.UnitDoseTypeId.Id
                .UnitDoseTypeCodeName = MixinStationPackageXpo.UnitDoseTypeId.CodeDescription
                .PersonalizedMasterPreparation = True
                .AssociatedPackageId = MixinStationPackageXpo.Id
                .StandardMix = True
                .LabelType = MixinStationPackageXpo.LabelType
                .MainDrugId = MixinStationPackageXpo.MainDrugId
                .ProductId = MixinStationPackageXpo.ProductId?.Id
            End With

            newListPackageDetail.ForEach(Sub(item)
                                             If Not item.MainMedicine OrElse ((item.Quantity.HasValue AndAlso item.Quantity.Value > 0) OrElse (item.Volume.HasValue AndAlso item.Volume.Value > 0)) Then
                                                 packageEntity.PackageDetail.Add(item)
                                             End If
                                         End Sub)

            Return New ActionResult(Of Package) With {.StateResult = True, .ObjectEmbbeded = packageEntity}
        Catch ex As Exception
            Return New ActionResult(Of Package) With {.StateResult = False, .Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
        End Try
    End Function

    Private Function GetListPackageDetailNoParenteralNutrition(listPackageDetail As List(Of MixinStationPackageDetailXpo)) As List(Of PackageDetail)
        Dim lockMe As New Object
        Dim newListPackageDetail As New List(Of PackageDetail)

        Parallel.ForEach(listPackageDetail.Where(Function(packageDetail)
                                                     Return packageDetail.PackageId.UnitDoseTypeId.MSClass <> EUnitDoseTypeClass.ParenteralNutrition
                                                 End Function).ToList(),
                             Sub(packageDetail)

                                 Try
                                     Dim EntityGeneral As Object = Nothing
                                     Select Case packageDetail.ComponentType
                                         Case 1
                                             EntityGeneral = packageDetail.AtcId
                                         Case 2
                                             EntityGeneral = packageDetail.SupplieId
                                         Case 3
                                             EntityGeneral = packageDetail.ProductId
                                     End Select

                                     Dim packageComponent = packageConfiguration.FirstOrDefault(Function(o) o.ServiceId = EntityGeneral.Id)

                                     Dim entityD As New PackageDetail
                                     With entityD

                                         If EntityGeneral IsNot Nothing Then

                                             .ComponentName = EntityGeneral.CodeName
                                             .ComponentType = packageDetail.ComponentType
                                             .ComponentTypeName = packageDetail.ComponentTypeName
                                             .SourceName = If(packageDetail.ComponentType = 1, EntityGeneral.Name, EntityGeneral.SupplieName)
                                             .ATC = If(packageDetail.ComponentType = 1, New ATC With {.FormulationType = packageDetail.AtcId?.FormulationType, .Id = packageDetail.AtcId?.Id,
                                                                    .Volume = packageDetail.AtcId?.Volume, .Weight = packageDetail.AtcId?.Weight}, Nothing)
                                             .AtcId = .ATC?.Id
                                             .SupplieId = packageDetail.SupplieId?.Id
                                             .SourceCodeName = EntityGeneral.CodeName
                                             .Quantity = If(packageComponent?.Dosage, 0D)
                                             .PreparationType = packageDetail.PreparationType
                                             .IsPrescribed = packageComponent IsNot Nothing
                                             .Thinner = False
                                             .Vehicle = False
                                             .MainMedicine = False

                                             If .ComponentType = 1 Then
                                                 .DCIName = packageDetail?.AtcId?.DCI?.Name
                                             End If

                                             Dim isNtp = If(packageComponent Is Nothing, False, packageComponent.NPT)

                                             If Not isNtp Then
                                                 If packageComponent Is Nothing Then
                                                     .Thinner = packageDetail.Thinner
                                                     .Vehicle = packageDetail.Vehicle
                                                     .MainMedicine = packageDetail.MainMedicine
                                                 Else
                                                     If listPackageDetail?.Any(Function(j) j.Id = packageDetail.Id AndAlso j.Thinner AndAlso j.AtcId?.Id = packageComponent.ServiceId) Then
                                                         .Thinner = True
                                                     End If
                                                     If listPackageDetail?.Any(Function(j) j.Id = packageDetail.Id AndAlso j.Vehicle AndAlso j.AtcId?.Id = packageComponent.ServiceId) Then
                                                         .Vehicle = True
                                                     End If
                                                     If listPackageDetail?.Any(Function(j) j.Id = packageDetail.Id AndAlso j.MainMedicine AndAlso j.AtcId?.Id = packageComponent.ServiceId) Then
                                                         .MainMedicine = True
                                                     End If
                                                 End If
                                             End If

                                             .Osmolarity = 0
                                             .Density = 0
                                             .WasOrdened = packageComponent IsNot Nothing

                                             If packageComponent IsNot Nothing Then

                                                 Select Case packageComponent.FormulationType
                                                     Case 1
                                                         .MeasurementUnitId = packageComponent.WeightMeasureUnit
                                                         .MeasureUnitDescription = packageComponent.MeasurementUnitDescriptionWeight
                                                     Case 2
                                                         .Quantity = packageDetail.Quantity
                                                         .MeasurementUnitId = packageComponent.VolumeMeasureUnit
                                                         .MeasureUnitDescription = packageComponent.MeasurementUnitDescriptionVolume
                                                     Case 3
                                                         If packageComponent.UnitType = 1 Then
                                                             .MeasurementUnitId = packageComponent.MeasurementUnitId
                                                             .MeasureUnitDescription = packageComponent.MeasurementUnitDescription
                                                             .Volume = ((Utils.MeasureUnitConvert(packageComponent.MeasurementUnitAbbreviation.ToLower(), packageComponent.WeightMeasureAbbreviation.ToLower()) _
                                                                            * packageComponent.Dosage) / packageComponent.Weight) * packageComponent.Volume
                                                             .VolumeMeasureUnit = packageComponent.VolumeMeasureUnit
                                                             .VolumeMeasureUnitDescription = packageComponent.MeasurementUnitDescriptionVolume

                                                         Else
                                                             .Quantity = ((Utils.MeasureUnitConvert(packageComponent.MeasurementUnitAbbreviation.ToLower(), packageComponent.VolumeMeasureAbbreviation.ToLower()) _
                                                                            * packageComponent.Dosage) / packageComponent.Volume) * packageComponent.Weight
                                                             .Volume = packageComponent.Dosage
                                                             .VolumeMeasureUnit = packageComponent.MeasurementUnitId
                                                             .VolumeMeasureUnitDescription = packageComponent.MeasurementUnitDescription
                                                         End If
                                                     Case Else '4
                                                         .Quantity = packageDetail.Quantity
                                                         If packageComponent.WeightMeasureUnit IsNot Nothing Then
                                                             .MeasurementUnitId = packageComponent.WeightMeasureUnit
                                                             .MeasureUnitDescription = packageComponent.MeasurementUnitDescriptionWeight
                                                         Else
                                                             .MeasurementUnitId = packageComponent.VolumeMeasureUnit
                                                             .MeasureUnitDescription = packageComponent.MeasurementUnitDescriptionVolume
                                                         End If

                                                 End Select

                                                 .MeasurementUnitAbbreviation = packageComponent.MeasurementUnitAbbreviation
                                             Else
                                                 'si el tipo de preparacion es estandar se toma la cantidad del detalle del paquete
                                                 If Not PersonalizedMasterPreparation Then
                                                     .Quantity = packageDetail.Quantity
                                                 Else
                                                     If .Thinner AndAlso {EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Antibiotics}.Contains(MixinStationPackageXpo.UnitDoseTypeId?.MSClass) Then
                                                         Dim MainComponent = listPackageDetail.FirstOrDefault(Function(x) .MainMedicine.HasValue AndAlso x.MainMedicine)
                                                         Dim Result = Presenter.GetDilutionFactorByAtc(MainComponent?.AtcId?.Id, packageDetail.AtcId?.Id)

                                                         If Result IsNot Nothing Then
                                                             .Quantity = Math.Round(CType(If(Result?.Volume, packageDetail.Quantity), Decimal), 2)
                                                             .Dilution = Math.Round(CType(If(Result?.Dilution, packageDetail.Dilution), Decimal), 4)
                                                             .Concentration = Math.Round(CType(If(Result?.Concentration, packageDetail.Concentration), Decimal), 4)
                                                             .ConcentrationName = $"{ .Concentration} {MainComponent?.MeasurementUnitId?.Abbreviation}/{Result?.VolumeMeasureUnit.Abbreviation}"
                                                             .TimeUnit = If(Result?.TimeUnit, 0)
                                                             .AmountTime = If(Result?.AmountTime, 0)
                                                         End If
                                                     End If

                                                     ' Manejo del Medicamento Principal para solicitudes externas
                                                     ' la cantidad se auto-pobla en preparaciones magistrales personalizadas
                                                     If .MainMedicine AndAlso ViewListDashboardConfirmationUnitDoseXpo IsNot Nothing Then
                                                         .Quantity = ViewListDashboardConfirmationUnitDoseXpo.Dosage
                                                     End If
                                                 End If

                                                 If packageDetail.AtcId?.FormulationType = 3 Then
                                                     If packageDetail.MeasurementUnitId.UnitType = 1 Then
                                                         .MeasurementUnitId = packageDetail.MeasurementUnitId.Id
                                                         .MeasureUnitDescription = packageDetail.MeasurementUnitId.CodeName
                                                         .VolumeMeasureUnit = packageDetail.VolumeMeasureUnit
                                                         .VolumeMeasureUnitDescription = packageDetail.VolumeMeasureUnitObj?.CodeName
                                                     Else
                                                         .Volume = packageDetail.Quantity
                                                         .VolumeMeasureUnit = packageDetail.VolumeMeasureUnit
                                                         .VolumeMeasureUnitDescription = packageDetail.VolumeMeasureUnitObj?.CodeName
                                                     End If
                                                 Else
                                                     .MeasurementUnitId = packageDetail.MeasurementUnitId?.Id
                                                     .MeasureUnitDescription = packageDetail.MeasurementUnitId?.CodeName
                                                 End If

                                                 .MeasurementUnitAbbreviation = If(packageDetail.MeasurementUnitId?.Abbreviation, packageDetail.VolumeMeasureUnitObj?.Abbreviation)
                                             End If

                                             If packageDetail.PackageId.UnitDoseTypeId.MSClass = EUnitDoseTypeClass.Cytostatic Then

                                                 If packageDetail.PreparationType = ePreparationType.None Then
                                                     .Quantity = If(packageDetail?.Quantity, 0D)
                                                     .MeasurementUnitId = packageDetail.MeasurementUnitId.Id
                                                     .MeasurementUnitAbbreviation = If(packageDetail.MeasurementUnitId?.Abbreviation, packageDetail.VolumeMeasureUnitObj?.Abbreviation)

                                                     .Volume = If(packageDetail?.Volume, 0D)
                                                     .VolumeMeasureUnit = packageDetail.VolumeMeasureUnitObj?.Id
                                                     .VolumeMeasureUnitDescription = packageDetail.VolumeMeasureUnitObj?.CodeName
                                                     .VolumeMeasureUnitAbbreviation = packageDetail.VolumeMeasureUnitObj?.Abbreviation
                                                 End If

                                                 If packageDetail.SupplieId IsNot Nothing Then
                                                     .Quantity = If(packageDetail?.Quantity, 0D)
                                                 End If
                                             End If

                                             .VolumeTotal = VolumenTotal
                                             .Dosis = String.Format("{0} {1}", .Quantity, .MeasureUnitDescription?.Split("-")(1).Trim())
                                             .QuantityMeasureunitname = $"{ .Quantity} { .MeasurementUnitAbbreviation}"
                                             .PreparationTypeName = .ComponentTypeName
                                             .IsDashboardConfirmationUnitDose = True
                                             .SourceType = True
                                             .UnitType = If(packageComponent?.UnitType, packageDetail.MeasurementUnitId?.UnitType)
                                             .TimeUnit = packageDetail.TimeUnit
                                             .AmountTime = packageDetail.AmountTime
                                         End If
                                     End With

                                     SyncLock lockMe
                                         newListPackageDetail.Add(entityD)
                                     End SyncLock
                                 Catch ex As Exception
                                     Throw
                                 End Try
                             End Sub)

        Return newListPackageDetail
    End Function

    ''' <summary>
    ''' Genera los detalles del paquete de dosis unitaria NPT
    ''' </summary>
    ''' <param name="listPackageDetail"></param>
    ''' <returns></returns>
    Private Function GetListPackageDetailParenteralNutritionParental(listPackageDetail As List(Of MixinStationPackageDetailXpo)) As List(Of PackageDetail)
        Dim lockMe As New Object
        Dim newListPackageDetail As New List(Of PackageDetail)

        If listPackageDetail.Exists(Function(x) {4, 5}.Contains(x.ComponentType)) Then
            For Each item In listPackageDetail.Where(Function(y) {4, 5}.Contains(y.ComponentType)).ToList()

                Dim result = NewPackageDetailParenteralNutrition(item)
                newListPackageDetail.Add(result)
            Next
        End If

        Parallel.ForEach(packageConfiguration,
                             Sub(packageComponent)

                                 Try

                                     Dim packageDetail = listPackageDetail.FirstOrDefault(Function(pd)
                                                                                              Return pd.PackageId.UnitDoseTypeId.MSClass = EUnitDoseTypeClass.ParenteralNutrition _
                                                                                              AndAlso ((pd.ComponentType = 1 AndAlso packageComponent.ServiceId = pd.AtcId.Id) _
                                                                                                OrElse (pd.ComponentType = 2 AndAlso packageComponent.ServiceId = pd.SupplieId.Id) _
                                                                                                OrElse (pd.ComponentType = 3 AndAlso packageComponent.ServiceId = pd.ProductId.Id))
                                                                                          End Function)
                                     If packageDetail Is Nothing Then
                                         Return
                                     End If

                                     Dim result = NewPackageDetailParenteralNutrition(packageDetail, packageComponent)

                                     SyncLock lockMe
                                         newListPackageDetail.Add(result)
                                     End SyncLock
                                 Catch ex As Exception
                                     Throw
                                 End Try
                             End Sub)

        Return newListPackageDetail
    End Function

    ''' <summary>
    ''' Genera el objeto 'PackageDetail' para el proceso de asignacion de paquete si es tipo de dosis 'Nutricion Parenteral'
    ''' </summary>
    ''' <returns></returns>
    Private Function NewPackageDetailParenteralNutrition(_PackageDetailTmp As MixinStationPackageDetailXpo, Optional _PackageComponentTmp As ViewListHCFARMEPDtoConfirmationUnitDoseXpo = Nothing) As PackageDetail

        Dim EntityGeneral As Object = Nothing
        Select Case _PackageDetailTmp.ComponentType
            Case 1, 4
                EntityGeneral = _PackageDetailTmp.AtcId
            Case 2
                EntityGeneral = _PackageDetailTmp.SupplieId
            Case 3, 5
                EntityGeneral = _PackageDetailTmp.ProductId
        End Select

        Dim entityD As New PackageDetail
        With entityD

            If EntityGeneral IsNot Nothing Then

                .ComponentName = EntityGeneral.CodeName
                .ComponentType = _PackageDetailTmp.ComponentType
                .ComponentTypeName = _PackageDetailTmp.ComponentTypeName
                .SourceName = If({1, 4, 5}.Contains(_PackageDetailTmp.ComponentType), EntityGeneral.Name, EntityGeneral.SupplieName)
                .ATC = If({1, 4}.Contains(_PackageDetailTmp.ComponentType), New ATC With {.FormulationType = _PackageDetailTmp.AtcId?.FormulationType, .Id = _PackageDetailTmp.AtcId?.Id,
                                       .Volume = _PackageDetailTmp.AtcId?.Volume, .Weight = _PackageDetailTmp.AtcId?.Weight}, Nothing)
                .AtcId = .ATC?.Id
                .SupplieId = _PackageDetailTmp.SupplieId?.Id
                .ProductId = _PackageDetailTmp.ProductId?.Id
                .SourceCodeName = EntityGeneral.CodeName
                .Quantity = If(_PackageComponentTmp?.Dosage, If(_PackageDetailTmp?.Quantity, 0D))
                .PreparationType = _PackageDetailTmp.PreparationType
                .IsPrescribed = _PackageComponentTmp IsNot Nothing
                .Thinner = False
                .Vehicle = False
                .MainMedicine = True

                If {1, 4}.Contains(.ComponentType) Then
                    .DCIName = _PackageDetailTmp?.AtcId?.DCI?.Name
                End If

                .Osmolarity = 0
                .Density = 0
                .WasOrdened = _PackageComponentTmp IsNot Nothing

                .MeasurementUnitId = _PackageDetailTmp.MeasurementUnitId.Id
                .MeasureUnitDescription = _PackageDetailTmp.MeasurementUnitId.CodeName
                .MeasurementUnitAbbreviation = If(_PackageDetailTmp.MeasurementUnitId?.Abbreviation, _PackageDetailTmp.VolumeMeasureUnitObj?.Abbreviation)

                .VolumeTotal = VolumenTotal
                .NPTItemOrder = _PackageDetailTmp.NPTItemOrder
                .Dosis = String.Format("{0} {1}", .Quantity, .MeasureUnitDescription?.Split("-")(1).Trim())
                .QuantityMeasureunitname = $"{ .Quantity} { .MeasurementUnitAbbreviation}"
                .PreparationTypeName = .ComponentTypeName
                .IsDashboardConfirmationUnitDose = True
                .SourceType = True
                .UnitType = If(_PackageComponentTmp?.UnitType, _PackageDetailTmp.MeasurementUnitId?.UnitType)
            End If
        End With

        Return entityD
    End Function


    ''' <summary>
    ''' Crea una copia limpia del Package sin ChangeTracker activo para evitar problemas de serialización
    ''' </summary>
    Private Function CreateCleanPackageForSave(original As Package) As Package
        If original Is Nothing Then Return Nothing

        ' Crear nuevo Package con ChangeTracking desactivado
        Dim cleanPkg As New Package()
        cleanPkg.ChangeTracker.ChangeTrackingEnabled = False

        ' Copiar propiedades del Package
        With cleanPkg
            .Id = original.Id
            .Code = original.Code
            .Name = original.Name
            .Description = original.Description
            .State = original.State
            .CodeAlternative = original.CodeAlternative
            .RiskLevelId = original.RiskLevelId
            .RiskLevelCodeName = original.RiskLevelCodeName
            .PhotoProtection = original.PhotoProtection
            .Storage = original.Storage
            .UnitDoseTypeId = original.UnitDoseTypeId
            .UnitDoseTypeCodeName = original.UnitDoseTypeCodeName
            .ProductId = original.ProductId
            .Concentration = original.Concentration
            .ConcentrationMeasurementUnitId = original.ConcentrationMeasurementUnitId
            .ConcentrationMeasurementUnitCodeName = original.ConcentrationMeasurementUnitCodeName
            .ConcentrationAntibiotic = original.ConcentrationAntibiotic
            .VolumeTotalPrepared = original.VolumeTotalPrepared
            .MeasurementPreparedId = original.MeasurementPreparedId
            .PreparationType = original.PreparationType
            .TypeStability = original.TypeStability
            .StabilityHour = original.StabilityHour
            .StabilityDays = original.StabilityDays
            .EnvironmentalTemperatureTerm = original.EnvironmentalTemperatureTerm
            .Purge = original.Purge
            .PreparationInstructions = original.PreparationInstructions
            .SpecialConsiderations = original.SpecialConsiderations
            .NptId = original.NptId
            .MainDrugId = original.MainDrugId
            .VehicleOptimization = original.VehicleOptimization
            .Readjustments = original.Readjustments
            .OsmolarityTotal = original.OsmolarityTotal
            .VolumeTotalOrder = original.VolumeTotalOrder
            .VolumeTotalOrderMeasurementUnitId = original.VolumeTotalOrderMeasurementUnitId
            .VolumeTotalOrderMeasurementUnitCodeName = original.VolumeTotalOrderMeasurementUnitCodeName
            .VolumeTotalOrderPurga = original.VolumeTotalOrderPurga
            .WeightTotalSolution = original.WeightTotalSolution
            .StandardMix = original.StandardMix
            .LabelType = original.LabelType
            .AssociatedPackageId = original.AssociatedPackageId
            .IsPackagePersonalized = original.IsPackagePersonalized
            .PersonalizedMasterPreparation = original.PersonalizedMasterPreparation
        End With

        ' Copiar los detalles
        If original.PackageDetail?.Any() Then
            For Each detail In original.PackageDetail.ToList()
                Dim cleanDetail = CreateCleanPackageDetailForSave(detail)
                cleanPkg.PackageDetail.Add(cleanDetail)
            Next
        End If

        Return cleanPkg
    End Function

    ''' <summary>
    ''' Crea una copia limpia de un PackageDetail sin ChangeTracker activo
    ''' </summary>
    Private Function CreateCleanPackageDetailForSave(original As PackageDetail) As PackageDetail
        If original Is Nothing Then Return Nothing

        Dim cleanDetail As New PackageDetail()
        cleanDetail.ChangeTracker.ChangeTrackingEnabled = False

        With cleanDetail
            .Id = original.Id
            .PackageId = original.PackageId
            .AtcId = original.AtcId
            .SupplieId = original.SupplieId
            .ProductId = original.ProductId
            .ComponentType = original.ComponentType
            .ComponentTypeName = original.ComponentTypeName
            .PreparationType = original.PreparationType
            .PreparationTypeName = original.PreparationTypeName
            .MainMedicine = original.MainMedicine
            .Thinner = original.Thinner
            .Vehicle = original.Vehicle
            .ComplementaryMedicine = original.ComplementaryMedicine
            .Quantity = original.Quantity
            .QuantityMeasureunitname = original.QuantityMeasureunitname
            .MeasurementUnitId = original.MeasurementUnitId
            .MeasurementUnitAbbreviation = original.MeasurementUnitAbbreviation
            .MeasureUnitDescription = original.MeasureUnitDescription
            .Volume = original.Volume
            .VolumeTotal = original.VolumeTotal
            .VolumeMeasureUnit = original.VolumeMeasureUnit
            .VolumeMeasureUnitDescription = original.VolumeMeasureUnitDescription
            .VolumeMeasureUnitAbbreviation = original.VolumeMeasureUnitAbbreviation
            .Concentration = original.Concentration
            .ConcentrationName = original.ConcentrationName
            .Dilution = original.Dilution
            .TimeUnit = original.TimeUnit
            .AmountTime = original.AmountTime
            .SourceName = original.SourceName
            .SourceCodeName = original.SourceCodeName
            .SourceCodeNameSub = original.SourceCodeNameSub
            .DCIName = original.DCIName
            .Dosis = original.Dosis
            .ComponentName = original.ComponentName
            .Osmolarity = original.Osmolarity
            .Density = original.Density
            .NPTItemOrder = original.NPTItemOrder
            .IsDashboardConfirmationUnitDose = original.IsDashboardConfirmationUnitDose
            .SourceType = original.SourceType
            .UnitType = original.UnitType
            .IsPrescribed = original.IsPrescribed
            .WasOrdened = original.WasOrdened
        End With

        Return cleanDetail
    End Function

    ''' <summary>
    ''' Se crea la descripcion del paquete teniendo en cuenta el orden
    ''' 1. MainMedicine, 2 vehicle y 3. Dilution
    ''' </summary>
    Private Function GetPackageDescription(ListPackageDetail As List(Of PackageDetail)) As String
        Try
            If ListPackageDetail IsNot Nothing AndAlso ListPackageDetail.Any() Then
                Dim description As String

                Dim getComponentOrder = Function(m As PackageDetail) As Integer
                                            If m.MainMedicine = True Then
                                                Return 1
                                            ElseIf m.Vehicle = True Then
                                                Return 2
                                            ElseIf m.Thinner = True Then
                                                Return 3
                                            Else
                                                Return 4
                                            End If
                                        End Function

                Dim orderedComponents = ListPackageDetail _
                .Where(Function(m) CBool(m.ComponentType = 1)) _
                .OrderBy(getComponentOrder) _
                .Select(Function(m) $"{m.DCIName} {Math.Round(m.Quantity.Value, 2)} {m.MeasurementUnitAbbreviation}")

                description = String.Join(" + ", orderedComponents)
                Return description
            End If
            Return ""
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Se calcula la cantidad del vehiculo siempre y cuando sea un paquete perzonalizado
    ''' </summary>
    Private Function CalculateQuantityVehicleForPackagePerzonalized(VolumenTotaltmp As Decimal, Optional ListPackageDetail As List(Of PackageDetail) = Nothing) As Decimal
        Try
            Dim result As Decimal = Decimal.Zero
            Dim ThinnerTmp, VehicleTmp, MainMedicineTmp As PackageDetail

            If VolumenTotaltmp > 0 Then

                If ListPackageDetail IsNot Nothing Then
                    ThinnerTmp = ListPackageDetail.FirstOrDefault(Function(x) x.Thinner)
                    VehicleTmp = ListPackageDetail.FirstOrDefault(Function(x) x.Vehicle)
                    MainMedicineTmp = ListPackageDetail.FirstOrDefault(Function(x) x.MainMedicine)
                End If

                If ThinnerTmp Is Nothing Then ThinnerTmp = Thinner
                If VehicleTmp Is Nothing Then VehicleTmp = Vehicle
                If MainMedicineTmp Is Nothing Then MainMedicineTmp = MainMedicine

                If MainMedicineTmp.ATC.FormulationType = 1 Then 'Peso
                    If (VolumenTotaltmp - ThinnerTmp?.Quantity) > 0 Then
                        result = VolumenTotaltmp - ThinnerTmp?.Quantity
                    End If

                ElseIf MainMedicineTmp.ATC.FormulationType = 3 Then 'Peso -Volumen
                    result = VolumenTotaltmp - ((ViewListDashboardConfirmationUnitDoseXpo.Dosage * MainMedicineTmp.ATC?.Volume) / MainMedicineTmp.ATC?.Weight)
                Else
                    result = VehicleTmp?.Quantity
                End If
            End If

            If result < 0 Then
                Return Decimal.Zero
            End If

            Return Math.Round(CType(result, Decimal), 2)
        Catch ex As Exception
            AsyncLoader(False)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Se calcula la cantidad del reconstituyente siempre y cuando sea un paquete perzonalizado
    ''' </summary>
    Private Function CalculateQuantityThinnerForPackagePerzonalized(_ThinnerTmp As PackageDetail, Dosis As Decimal, _MainMedicineTmp As PackageDetail) As Decimal
        Try
            Dim result, _VolumeTotalDilution As Decimal
            If _MainMedicineTmp Is Nothing Then _MainMedicineTmp = MainMedicine

            Dim _presenterD As New PDilutionFactors
            Dim DilutionFactorsTmp = _presenterD.GetAtcDilutionById(_MainMedicineTmp.AtcId)

            If DilutionFactorsTmp.DilutionFactorsDetailXpo.Any(Function(x) x.AtcId = _ThinnerTmp.AtcId) Then
                _VolumeTotalDilution = DilutionFactorsTmp.DilutionFactorsDetailXpo.FirstOrDefault(Function(x) x.AtcId = _ThinnerTmp.AtcId).Volume
                result = ((Dosis * _VolumeTotalDilution) / _MainMedicineTmp.ATC.Weight)

                Return Math.Round(CType(result, Decimal), 2)
            Else
                Mensaje(EeventViewerImages.Advertencia) = $"No se encontro el diluyente {_ThinnerTmp.ComponentName} parametrizado en los factores de dilucion para el medicamento principal {_MainMedicineTmp.ComponentName}"
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Funcion que se encarga de establecer la concentracion concatenada con las unidades de medida
    ''' </summary>
    Private Function CalculateConcentration(VolumeTotalTmp As Decimal, Optional ListpackageDetail As List(Of PackageDetail) = Nothing) As String
        Try
            If CInt(VolumeTotalTmp) > 0 Then
                Dim MainMedicineTmp, VehicleTmp, ThinnerTmp As PackageDetail
                Dim totalQuantity As Decimal = 0
                Dim measurementUnitAbbreviation As String = String.Empty

                If ListpackageDetail IsNot Nothing Then
                    VehicleTmp = ListpackageDetail.FirstOrDefault(Function(x) x.Vehicle)
                    ThinnerTmp = ListpackageDetail.FirstOrDefault(Function(x) x.Thinner)

                    Dim isNonePreparationType = ListpackageDetail.Any(Function(x) x.MainMedicine AndAlso x.PreparationType = ePreparationType.None)
                    Dim isCytostatic = MixinStationPackageXpo?.UnitDoseTypeId?.MSClass = EUnitDoseTypeClass.Cytostatic

                    If isNonePreparationType AndAlso isCytostatic Then 'Oncológicas intratecales
                        Dim mainMedicines = ListpackageDetail.Where(Function(x) x.MainMedicine).ToList()
                        If mainMedicines.Any() Then
                            totalQuantity = mainMedicines.Sum(Function(x) If(x.Quantity, 0D))
                            measurementUnitAbbreviation = mainMedicines.First()?.MeasurementUnitAbbreviation
                        End If
                    Else
                        MainMedicineTmp = ListpackageDetail.FirstOrDefault(Function(x) x.MainMedicine)
                        If MainMedicineTmp IsNot Nothing Then
                            totalQuantity = ViewListDashboardConfirmationUnitDoseXpo.Dosage
                            measurementUnitAbbreviation = MainMedicineTmp.MeasurementUnitAbbreviation
                        End If
                    End If
                End If

                If totalQuantity = 0 AndAlso MainMedicine IsNot Nothing Then
                    totalQuantity = ViewListDashboardConfirmationUnitDoseXpo.Dosage
                    measurementUnitAbbreviation = MainMedicine.MeasurementUnitAbbreviation
                End If

                If VehicleTmp Is Nothing AndAlso Vehicle IsNot Nothing Then
                    VehicleTmp = Vehicle
                End If

                If ThinnerTmp Is Nothing AndAlso Thinner IsNot Nothing Then
                    ThinnerTmp = Thinner
                End If

                Dim result As Decimal = totalQuantity / VolumeTotalTmp
                Concentration = Utils.SetPartDecimalToValue(result)

                If Concentration = 0 Then
                    Return String.Empty
                End If

                Dim volumeMeasurementUnit = If(VehicleTmp?.MeasurementUnitAbbreviation, ThinnerTmp?.MeasurementUnitAbbreviation)

                If String.IsNullOrEmpty(volumeMeasurementUnit) Then
                    If ListpackageDetail IsNot Nothing Then
                        Dim firstMainMedicine = ListpackageDetail.FirstOrDefault(Function(x) x.MainMedicine)
                        If firstMainMedicine?.PreparationType = ePreparationType.None AndAlso firstMainMedicine?.ATC?.FormulationType = 3 Then
                            volumeMeasurementUnit = firstMainMedicine?.VolumeMeasureUnitAbbreviation
                        End If
                    ElseIf MainMedicine?.PreparationType = ePreparationType.None AndAlso MainMedicine?.ATC?.FormulationType = 3 Then
                        volumeMeasurementUnit = MainMedicine?.VolumeMeasureUnitAbbreviation
                    End If
                End If

                Return $"{Concentration} {measurementUnitAbbreviation}/{volumeMeasurementUnit}"
            End If

            Return String.Empty
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Funcion que se encarga de calcular el volumen total del preparado
    ''' </summary>
    Private Function CalculateVolumenTotalPrepared(ListpackageDetail As List(Of MixinStationPackageDetailXpo)) As Decimal
        Try
            INDVolumenTotal.Enabled = PersonalizedMasterPreparation
            RepositoryItemSpinEdit1.ReadOnly = Not PersonalizedMasterPreparation

            If Not PersonalizedMasterPreparation Then
                Return MixinStationPackageXpo.VolumeTotalPrepared
            End If

            Dim thinnerItem = ListpackageDetail.FirstOrDefault(Function(x) x.PreparationType.HasValue AndAlso x.PreparationType = 1 AndAlso x.Thinner)

            If thinnerItem IsNot Nothing Then
                Return thinnerItem.Quantity
            End If

            Select Case ViewListDashboardConfirmationUnitDoseXpo.OriginOrder
                Case 1 ' Origen HCPRESCRA
                    Return Decimal.Zero
                Case 2 ' Origen HCINFLIQA
                    Return MixinStationPackageXpo.VolumeTotalPrepared
                Case Else
                    Return MixinStationPackageXpo.VolumeTotalPrepared
            End Select

        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub CustomDose(ListPackage As List(Of MixinStationPackageDetailXpo))
        'Se valida que hayan seleccionado un paquete asociado
        If PackageId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un paquete asociado"
            Exit Sub
        End If

        OpenFormPackage(True, ListPackage)
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignPackage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)

        Me.indigo = SessionValues.Instance
        Presenter = New PDashboardConfirmationUnitDose()
        InitializeTuples()

        If ItemsToAssign.Any(Function(x) x.MSClass = EUnitDoseTypeClass.ParenteralNutrition) Then
            INDlyItemRequestDoses.Text = "Volumen solicitado"
            PersonalizedMasterPreparation = True
            INDslePersonalizedMasterPreparation.ReadOnly = True
        Else
            PersonalizedMasterPreparation = False
        End If

        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

        setColumnActions()
        printCtrlData()
    End Sub

    Private Async Sub printCtrlData()
        Try
            Using m As New Billing.MVP.MLiquidation()
                Dim data = ViewListDashboardConfirmationUnitDoseXpo

                Dim patientData As DataTable = Nothing
                Select Case data.SourceType
                    Case 2 'Solicitud externa, Solicitud dosis unitaria centro atención externo
                        patientData = Await m.ExecuteCommandDt($"select top 1 
	                        ad.SIGLA TipoDoc
                            FROM MixingStation.PatientExternalCareCenter pec WITH(NOLOCK)
                            JOIN ADTIPOIDENTIFICA ad ON ad.ID = pec.IdentificationTypeId
                            WHERE IdentificationNumber = '{data.PatientCode}'", indigo.TransactionalContainer)

                        ctrTmp.RefreshData(New CtrDatosPacienteNPT.DatoPacienteModel With {
                        .Paciente = data.PatientCodeName.Split("-")(1).Trim(),
                        .Identificacion = $"{patientData.Rows(0)("TipoDoc")} {data.PatientCode.Trim()}"})
                    Case Else 'Orden Medica
                        If data.AdmissionCode IsNot Nothing Then
                            patientData = Await m.ExecuteCommandDt($"Select top 1 
	                          ing.NUMINGRES as AdmissionNumber
	                        , pac.IPFECNACI as BirthDate
	                        , fisic.PESOPACIE as [Weight]
	                        , DATEDIFF(hour, pac.IPFECNACI, Common.GETDATE()) / 8766.0 AS Age
	                        , concat(diag.CODDIAGNO, ' - ', diag.NOMDIAGNO) as Diagno
	                        , case pac.IPTIPODOC 
		                        when 1 then 'CC'
		                        when 2 then 'CE'
		                        when 3 then 'TI'
		                        when 4 then 'RC'
		                        when 5 then 'PA'
		                        when 6 then 'AS'
		                        when 7 then 'MS'
		                        when 8 then 'NU'
		                        when 9 then 'CN'
		                        when 10 then 'CD'
		                        when 11 then 'SC'
		                        when 12 then 'PE'
		                        when 13 then 'PT'
		                        when 14 then 'DE'
		                        when 15 then 'SI'
	                        end as TipoDoc
	                        , fisic.TALLAPACI as Talla
                            from ADINGRESO ing (nolock)
                            join INPACIENT pac (nolock) on ing.IPCODPACI = pac.IPCODPACI
                            outer apply (
	                            select top 1 indiag.CODDIAGNO from INDIAGNOH indiag (nolock) where NUMINGRES = ing.NUMINGRES and CODDIAPRI = 1 order by FECDIAGNO desc
                            ) idiag
                            LEFT join INDIAGNOS diag (nolock) on idiag.CODDIAGNO = diag.CODDIAGNO
                            outer apply (
	                            select top 1 TALLAPACI, exf.PESOPACIE from HCEXFISIC exf (nolock) where exf.IPCODPACI = pac.IPCODPACI order by exf.FECREGITE desc
                            ) fisic
                            where ing.NUMINGRES = '{data.AdmissionCode}'", indigo.TransactionalContainer)

                            ctrTmp.RefreshData(New CtrDatosPacienteNPT.DatoPacienteModel With {
                            .Paciente = data.PatientCodeName.Split("-")(1).Trim(),
                            .Identificacion = $"{patientData.Rows(0)("TipoDoc")} {data.PatientCodeName.Split(" - ")(0).Trim()}",
                            .Ingreso = patientData.Rows(0)("AdmissionNumber").ToString(),
                            .Diagnostico = patientData.Rows(0)("Diagno").ToString(),
                            .BirthDate = Date.Parse(patientData.Rows(0)("BirthDate").ToString()),
                            .Peso = If(patientData.Rows(0)("Weight") Is DBNull.Value, 0, Decimal.Parse(patientData.Rows(0)("Weight").ToString()) / 1000.0),
                            .Talla = Decimal.Parse(patientData.Rows(0)("Talla").ToString())})

                        Else
                            patientData = Await m.ExecuteCommandDt(
                            $"select top 1 
				            pac.IPFECNACI as BirthDate
	                        , case pac.IPTIPODOC 
		                        when 1 then 'CC'
		                        when 2 then 'CE'
		                        when 3 then 'TI'
		                        when 4 then 'RC'
		                        when 5 then 'PA'
		                        when 6 then 'AS'
		                        when 7 then 'MS'
		                        when 8 then 'NU'
		                        when 9 then 'CN'
		                        when 10 then 'CD'
		                        when 11 then 'SC'
		                        when 12 then 'PE'
		                        when 13 then 'PT'
		                        when 14 then 'DE'
		                        when 15 then 'SI'
	                        end as TipoDoc
                            from INPACIENT pac (nolock)
                            where pac.IPCODPACI = '{data.PatientCode}'", indigo.TransactionalContainer)

                            ctrTmp.RefreshData(New CtrDatosPacienteNPT.DatoPacienteModel With {
                            .Paciente = data.PatientCodeName.Split("-")(1).Trim(),
                            .Identificacion = $"{patientData.Rows(0)("TipoDoc")} {data.PatientCode.Trim()}",
                            .BirthDate = Date.Parse(patientData.Rows(0)("BirthDate").ToString())})
                        End If
                End Select
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub setColumnActions()
        IndigoGridView1.SetListAcction(INDviewPersonalized, {eAcciones.Remove}.ToList())

        Dim col = INDviewPersonalized.Columns.FirstOrDefault(Function(m) m.Name = "colActions")

        If col IsNot Nothing Then
            col.Width = 90
            col.OptionsColumn.ShowInCustomizationForm = False
            col.OptionsColumn.AllowShowHide = False
            col.Visible = False
        End If
    End Sub

    Private Sub showColumnActions(value As Boolean)
        Dim col = INDviewPersonalized.Columns.FirstOrDefault(Function(m) m.Name = "colActions")

        If col IsNot Nothing Then
            col.Visible = value
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private packageConfiguration As List(Of ViewListHCFARMEPDtoConfirmationUnitDoseXpo) = Nothing

    Private Sub LoadPackageConfiguration()
        packageConfiguration = Presenter.GetHCFARMEPDbyAGRUPAQUETE(ViewListDashboardConfirmationUnitDoseXpo.AGRUPAQUETE)
    End Sub

    ''' <summary>
    ''' Establecer el orden de las columnas visualmente
    ''' </summary>
    Private Sub SetColumnOrderForPackage()
        INDColCode.VisibleIndex = 0
        INDColName.VisibleIndex = 1
        INDColType.VisibleIndex = 2
        INDColClass.VisibleIndex = 3
        INDColIsStandard.VisibleIndex = 4
        INDColIsStandard.Width = 20
    End Sub

    ''' <summary>
    ''' Carga los paquetes de forma optimizada.
    ''' </summary>
    Private Async Function LoadPackageDatasource() As Task
        INDslePersonalizedMasterPreparation.Enabled = False
        SearchLookUpEdit2View.ShowLoadingPanel()
        LoadPackageConfiguration()

        If packageConfiguration Is Nothing OrElse Not packageConfiguration.Any() And ViewListDashboardConfirmationUnitDoseXpo.SourceType <> 2 Then
            SearchLookUpEdit2View.HideLoadingPanel()
            Mensaje(EeventViewerImages.Advertencia) = "No hay paquetes disponibles."
            INDslePersonalizedMasterPreparation.Enabled = False
            Return
        End If

        Dim itemConfirmation = ViewListDashboardConfirmationUnitDoseXpo
        Dim filter As String = $"State = 1 And PersonalizedMasterPreparation = 0 And UnitDoseTypeId.MSClass = {itemConfirmation.MSClass}"

        If itemConfirmation.MSClass <> EUnitDoseTypeClass.ParenteralNutrition Then
            filter &= $" And MixinStationPackageDetailXpo[AtcId.Id = {itemConfirmation.ServiceId} And MainMedicine = 1]"
        Else
            Dim Npt = packageConfiguration.FirstOrDefault()
            If Npt Is Nothing OrElse Npt.NPTId Is Nothing Then
                SearchLookUpEdit2View.HideLoadingPanel()
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró plantilla NPT ligada a la solicitud."
                INDslePersonalizedMasterPreparation.Enabled = False
                Return
            End If

            filter &= $" And MainDrugId = {itemConfirmation.ServiceId} And NptId = {Npt.NPTId}"
        End If

        Dim data = Await Task.Run(Function()
                                      Return XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService.GetCollectionAsList(Of MixinStationPackageXpo)(Nothing, filter)
                                  End Function)

        If data Is Nothing OrElse Not data.Any() Then
            SearchLookUpEdit2View.HideLoadingPanel()
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron paquetes disponibles para la solicitud"
            INDslePersonalizedMasterPreparation.Enabled = False
            Return
        End If

        Dim isMagistral As Boolean = (itemConfirmation.MSClass = EUnitDoseTypeClass.Magistral)
        For Each item In data

            Dim detailsPT = item.MixinStationPackageDetailXpo.Where(Function(x) x.ComponentType = 1).ToList()
            If isMagistral Then
                If PersonalizedMasterPreparation Then

                    Dim PackageDetailsPersonalized = detailsPT.
                            Select(Function(a) Tuple.Create(a.AtcId.Id, a.MeasurementUnitId.Id)).
                            OrderBy(Function(t) t.Item1).
                            ToList()

                    Dim packageConfigurationDetailsPersonalized = packageConfiguration.
                            Select(Function(b) Tuple.Create(b.ServiceId, b.MeasurementUnitId.Value)).
                            OrderBy(Function(t) t.Item1).
                            ToList()

                    item.IsStandard = PackageDetailsPersonalized.SequenceEqual(packageConfigurationDetailsPersonalized)
                Else
                    Dim PackageDetailsStandar = detailsPT.
                            Select(Function(a) Tuple.Create(a.Quantity, a.AtcId.Id, a.MeasurementUnitId.Id)).
                            OrderBy(Function(t) t.Item2).
                            ToList()

                    Dim packageConfigurationDetailsStandar = packageConfiguration.
                            Select(Function(b) Tuple.Create(b.Dosage, b.ServiceId, b.MeasurementUnitId.Value)).
                            OrderBy(Function(t) t.Item2).
                            ToList()

                    item.IsStandard = PackageDetailsStandar.SequenceEqual(packageConfigurationDetailsStandar)
                End If
            Else
                If PersonalizedMasterPreparation Then
                    ' Cada configuración debe tener al menos un detalle principal equivalente en ATC y unidad
                    item.IsStandard = packageConfiguration.All(
                                                Function(m) detailsPT.Any(
                                                Function(o) m.ServiceId = o.AtcId?.Id AndAlso
                                                            m.MeasurementUnitId = o.MeasurementUnitId.Id))

                ElseIf packageConfiguration.Count = 1 Then
                    ' Para receta simple: debe haber un único detalle con coincidencia exacta
                    Dim prescribed = packageConfiguration.First()
                    item.IsStandard = detailsPT.Any(
                                                Function(m) m.Quantity = prescribed.Dosage AndAlso
                                                m.MeasurementUnitId.Id = prescribed.MeasurementUnitId AndAlso
                                                m.AtcId?.Id = prescribed.ServiceId)
                Else
                    ' Cada detalle principal debe tener una configuración equivalente
                    item.IsStandard = detailsPT.All(
                                                Function(m) packageConfiguration.Any(
                                                Function(o) o.Dosage = m.Quantity AndAlso
                                                o.MeasurementUnitId = m.MeasurementUnitId.Id AndAlso
                                                o.ServiceId = m.AtcId?.Id))
                End If
            End If
        Next

        INDslePackage.Properties.DataSource = data

        Dim packageDefault = data
        If packageDefault?.Any() Then
            If packageDefault.All(Function(x) Not x.IsStandard) Then
                INDslePersonalizedMasterPreparation.SafeInvoke(Sub() PersonalizedMasterPreparation = True)
                Exit Function
            End If

            Dim standardPackages = packageDefault _
            .Where(Function(x) x.IsStandard AndAlso x.UnitDoseTypeId.MSClass = itemConfirmation.MSClass) _
            .ToList()

            If standardPackages.Count = 1 Then
                PackageId = standardPackages.First().Id
            End If
        End If

        SearchLookUpEdit2View.HideLoadingPanel()
        INDslePersonalizedMasterPreparation.Enabled = True
        SetColumnOrderForPackage()
    End Function

    ''' <summary>
    ''' Carga las lines de produccion
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadProductionLineDatasource() As Task
        INDsleProductionLine.Enabled = False
        INDsleProductionLine.Properties.DataSource = Await _
            Task.Factory.StartNew(Function()
                                      Dim data As List(Of ViewListProductionLineXpo) = Nothing

                                      If INDsleProductionLine.Properties.DataSource IsNot Nothing Then
                                          data = INDsleProductionLine.Properties.DataSource
                                      Else
                                          data = Presenter.InitializeProductionLine(ViewListDashboardConfirmationUnitDoseXpo.CMConfigurationId, ViewListDashboardConfirmationUnitDoseXpo.CareCenterCode, MixinStationPackageXpo.UnitDoseTypeId.Id, ViewListDashboardConfirmationUnitDoseXpo.SourceType)
                                      End If

                                      If data?.Any() Then
                                          Return data
                                      End If
                                  End Function)

        Dim ProductionLineDefault As List(Of ViewListProductionLineXpo) = INDsleProductionLine.Properties.DataSource
        If ProductionLineDefault?.Count = 1 Then
            ProductionLine = ProductionLineDefault.Select(Function(x) x.ProductionLineId).First
        End If

        INDsleProductionLine.Enabled = True
    End Function

    Private Sub ColumnsVisible(NPT As Boolean)
        If NPT Then
            INDColQuantity.HideColumn(-1)
            GridColumn14.HideColumn(-1)
        Else
            INDColQuantity.ShowColumn(2)
            GridColumn14.ShowColumn(3)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDslePersonalizedMasterPreparation_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePersonalizedMasterPreparation.EditValueChanged

        INDlyItemVolume.HideLayout()
        INDliConcentrationAntibiotic.HideLayout()
        VolumenTotal = Decimal.Zero
        ConcentrationWithMeasure = String.Empty

        INDlygPersonalized.HideControl(False)
        ShowHideDosisPersonalizada(ViewListDashboardConfirmationUnitDoseXpo.ProductNPT)

        If PersonalizedMasterPreparation Then 'Se muestra el grupo de personalización y se cambia el nombre del paquete
            INDlyItemPackage.Text = "Paquete Asociado"
            INDlyItemTextPersonalizedMasterPreparation.AllowHide = False

            showColumnActions(True)
        Else 'Se muestra el control de paquete sugerido y se oculta el grupo de personalización
            INDlyItemPackage.Text = "Paquete Sugerido"
            INDlyItemTextPersonalizedMasterPreparation.AllowHide = True

            showColumnActions(False)
        End If

        PackageId = Nothing
        Await LoadPackageDatasource()

        ColumnsVisible(ViewListDashboardConfirmationUnitDoseXpo.ProductNPT)
    End Sub

    Private Sub ShowHideDosisPersonalizada(isProductNPT As Boolean)
        ''Cuando es diferente a NPT se muestra la accion paquete personalizado, de lo contrario se oculta la accion
        If Not isProductNPT Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DosisPersonalizada) = Not (PersonalizedMasterPreparation AndAlso PackageId IsNot Nothing)
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DosisPersonalizada) = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDslePackage_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePackage.EditValueChanged
        If PackageId IsNot Nothing Then
            Dim row = INDslePackage.GetFocusedObject(Of MixinStationPackageXpo)()

            If row Is Nothing Then
                row = Presenter.GetPackageAssociatedById(PackageId)
            Else
                If Not PersonalizedMasterPreparation Then
                    If Not row.IsStandard Then
                        PackageId = Nothing
                        Mensaje(EeventViewerImages.Advertencia) = "Se debe seleccionar un paquete estándar"
                        Return
                    End If
                Else
                    If Not row.IsStandard Then
                        If row.UnitDoseTypeId.MSClass = EUnitDoseTypeClass.Magistral Then
                            Dim detailsPT = row.MixinStationPackageDetailXpo.Where(Function(x) x.ComponentType = 1).ToList()

                            Dim PackageDetailsPersonalized = detailsPT.
                            Select(Function(a) Tuple.Create(a.AtcId.CodeName, a.MeasurementUnitId.CodeName)).
                            OrderBy(Function(t) t.Item1).
                            ToList()

                            Dim packageConfigurationDetailsPersonalized = packageConfiguration.
                            Select(Function(b) Tuple.Create(b.ServiceDescription, b.MeasurementUnitDescription)).
                            OrderBy(Function(t) t.Item1).
                            ToList()

                            Dim concatenatedItems As String
                            Dim excessItems = PackageDetailsPersonalized.Except(packageConfigurationDetailsPersonalized).ToList()
                            If excessItems.Any() Then
                                concatenatedItems = String.Join(Environment.NewLine, excessItems.Select(Function(t) t.Item1.ToString() & ", Con unidad de medida " & t.Item2.ToString()))
                                PackageId = Nothing
                                Mensaje(EeventViewerImages.Advertencia) = $"Los items del paquete {concatenatedItems} no pertenecen al ordenamiento"
                                Return
                            End If

                            Dim missingItems = packageConfigurationDetailsPersonalized.Except(PackageDetailsPersonalized).ToList()
                            If missingItems.Any() Then
                                concatenatedItems = String.Join(Environment.NewLine, missingItems.Select(Function(t) t.Item1.ToString() & ", Con unidad de medida " & t.Item2.ToString()))
                                PackageId = Nothing
                                Mensaje(EeventViewerImages.Advertencia) = $"Los siguientes ítems no se encuentran dentro del paquete: {concatenatedItems}"
                                Return
                            End If
                        Else
                            Dim notIn = packageConfiguration _
                            .FindAll(Function(m) Not row.MixinStationPackageDetailXpo.Where(Function(o) o.ComponentType = 1) _
                            .Any(Function(o) m.ServiceId = o.AtcId?.Id)) _
                            .Select(Function(m) m.ServiceDescription).ToArray()

                            If notIn.Any() Then
                                PackageId = Nothing
                                Mensaje(EeventViewerImages.Advertencia) = $"Los siguientes ítems no se encuentran dentro del paquete: {vbCrLf}{String.Join(vbCrLf, notIn)}"
                                Return
                            End If
                        End If
                    End If
                End If
            End If

            If {EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Antibiotics}.Contains(row.UnitDoseTypeId?.MSClass) Then
                INDlyItemVolume.ShowLayout()
                INDliConcentrationAntibiotic.ShowLayout()
                INDtxtConcentration.ReadOnly = True
            End If

            ProductionLine = Nothing
            INDsleProductionLine.Properties.DataSource = Nothing
            MixinStationPackageXpo = row

            Await LoadProductionLineDatasource()
            Magistralpackage(MixinStationPackageXpo.MixinStationPackageDetailXpo.ToList())
        Else
            INDgcPersonalized.DataSource = Nothing
            INDsleProductionLine.Properties.DataSource = Nothing
            ConcentrationWithMeasure = String.Empty
            VolumenTotal = Decimal.Zero
        End If

        ShowHideDosisPersonalizada(ViewListDashboardConfirmationUnitDoseXpo.ProductNPT)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del volumen total del preparado
    ''' </summary>
    Private Sub INDVolumenTotal_EditValueChanged(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDVolumenTotal.EditValueChanging
        If e.NewValue > 0 And PersonalizedMasterPreparation And {EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Antibiotics}.Contains(MixinStationPackageXpo.UnitDoseTypeId?.MSClass) Then
            Dim NewVolume As Decimal = Convert.ToDecimal(e.NewValue, Globalization.CultureInfo.InvariantCulture)
            If Vehicle IsNot Nothing Then
                Vehicle.Quantity = CalculateQuantityVehicleForPackagePerzonalized(NewVolume)
                Vehicle.Dosis = $"{Vehicle.Quantity} {Vehicle.MeasureUnitDescription.Split("-")(1).Trim()}"
                Vehicle.QuantityMeasureunitname = $"{Vehicle.Quantity} {Vehicle.MeasurementUnitAbbreviation}"
            End If
            ConcentrationWithMeasure = CalculateConcentration(NewVolume)
            INDviewPersonalized.RefreshData()

            If packageToCreate IsNot Nothing Then
                packageToCreate.ConcentrationAntibiotic = ConcentrationWithMeasure
                packageToCreate.VolumeTotalPrepared = NewVolume
            End If
        End If
    End Sub


    Private Sub INDsleProductionLine_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProductionLine.EditValueChanged
        If ProductionLine IsNot Nothing Then
            Dim row = INDsleProductionLine.GetFocusedObject(Of ViewListProductionLineXpo)()

            If row Is Nothing Then
                row = Presenter.GetProductionLineById(ProductionLine)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Showing editor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewPersonalized_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDviewPersonalized.ShowingEditor
        Dim row = DirectCast(sender, GridView).GetFocusedObject(Of PackageDetail)()

        If DirectCast(sender, GridView).FocusedColumn.Name = Me.INDColQuantity.Name AndAlso row.IsPrescribed Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se encargar de deshabilitar el SpinEdit para los detalles
    ''' de tipo "Dilucion" o "Vehiculo" siempre y cuando la preparacion es personalizada
    ''' </summary>
    Private Sub INDviewPersonalized_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles INDviewPersonalized.CustomRowCellEdit
        If {EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Antibiotics}.Contains(If(MixinStationPackageXpo?.UnitDoseTypeId?.MSClass, 0)) Then
            Dim Detailpackage As PackageDetail = CType(INDviewPersonalized.GetRow(e.RowHandle), PackageDetail)
            If e.Column.FieldName = "Quantity" AndAlso (Detailpackage?.Thinner OrElse Detailpackage?.Vehicle) Then
                Dim spinEdit As New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
                spinEdit.ReadOnly = True
                e.RepositoryItem = spinEdit
            End If
        End If
    End Sub


#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignPackage_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se ejecuta al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignPackage_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se ejecuta al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignPackage_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDtxtMedicament.EditValue = ViewListDashboardConfirmationUnitDoseXpo.ServiceDescription
        INDtxtRequestDoses.EditValue = ViewListDashboardConfirmationUnitDoseXpo.Dosage.ToString() + " " + ViewListDashboardConfirmationUnitDoseXpo.MeasurementUnitName
        INDslePersonalizedMasterPreparation.Focus()
        showColumnActions(False)
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se ejecuta para abrir el form de paquetes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePackage_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePackage.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormPackage(False)
        End If
    End Sub

#End Region

#Region "Click"

#End Region

#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_Click_DosisPersonalizada() Handles BarraBotones.Click_DosisPersonalizada
        CustomDose(Nothing)
    End Sub

    Private Sub RepositoryItemSpinEdit1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemSpinEdit1.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim row = INDviewPersonalized.GetFocusedObject(Of PackageDetail)()
            row.Dosis = $"{e.NewValue} {row.MeasureUnitDescription.Split("-")(1).Trim()}"
            row.QuantityMeasureunitname = $"{e.NewValue} {row.MeasurementUnitAbbreviation}"
        End If
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        RemoveDetailItem()
    End Sub

    Private Sub RemoveDetailItem()
        Dim item = INDviewPersonalized.GetFocusedObject(Of PackageDetail)()

        If item.WasOrdened Then
            Mensaje(EeventViewerImages.Advertencia) = "Este ítem no puede ser eliminado debido a que hace parte de una prescripción"
            Exit Sub
        End If

        If MessageIndigo.Show("¿Está seguro que desea eliminar el item seleccionado?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        item.MarkAsDeleted()
        INDgcPersonalized.DataSource = packageToCreate.PackageDetail.ToList()
        INDgcPersonalized.RefreshDataSource()
    End Sub
#End Region

End Class