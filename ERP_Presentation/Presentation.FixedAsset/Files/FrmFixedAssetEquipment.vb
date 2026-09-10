'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Daniel Eduardo Arevalo
' Created          : 03-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP
Imports System.Windows.Forms
Imports System.Globalization

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista en el frontal de torres
''' </summary>
Public Class FrmFixedAssetEquipment
    Implements IFixedAssetEquipment, ICustomizableForm

#Region "Builder"
    Public Sub New()
        InitializeComponent()
        ctrLastCost = New CtrLastCost()
        ctrLastCost.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrLastCost)
    End Sub
#End Region

#Region "Properties"

    ''' <summary>
    ''' Permite saber si deprecia por tiempo de uso de la ubicacion
    ''' </summary>
    ''' <returns></returns>
    Public Property DepreciateByTimeUse As Boolean Implements IFixedAssetEquipment.DepreciateByTimeUse
        Get
            Return INDsleDepreciateByTimeUse.EditValue
        End Get
        Set(value As Boolean)
            INDsleDepreciateByTimeUse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor razonable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FairValue As Decimal Implements IFixedAssetEquipment.FairValue
        Get
            Return INDtxtFairValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtFairValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si catalogo de equipos amortiza o no  
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Amortizes As Boolean? Implements IFixedAssetEquipment.Amortizes
        Get
            Return INDGleAmortizes.EditValue
        End Get
        Set(value As Boolean?)
            INDGleAmortizes.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Permite saber si se deprecia o no el articulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllowDepreciate As Boolean? Implements IFixedAssetEquipment.AllowDepreciate
        Get
            If INDsleAllowDepreciate.EditValue IsNot Nothing Then
                Return INDsleAllowDepreciate.EditValue
            Else
                Return False
            End If
        End Get
        Set(value As Boolean?)
            INDsleAllowDepreciate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de depreciación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DepreciationTypeId As Integer? Implements IFixedAssetEquipment.DepreciationTypeId
        Get
            Return INDsleDepreciationType.EditValue
        End Get
        Set(value As Integer?)
            INDsleDepreciationType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje de salvamento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PercentageRescue As Decimal Implements IFixedAssetEquipment.PercentageRescue
        Get
            Return INDsePercentageRescue.EditValue
        End Get
        Set(value As Decimal)
            INDsePercentageRescue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el total de unidades producidas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalProductionUnit As Long Implements IFixedAssetEquipment.TotalProductionUnit
        Get
            Return INDseTotalProductionUnit.EditValue
        End Get
        Set(value As Long)
            INDseTotalProductionUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la vida util
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LifeUtil As Integer Implements IFixedAssetEquipment.LifeUtil
        Get
            Return INDseLifeUtil.EditValue
        End Get
        Set(value As Integer)
            INDseLifeUtil.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de la vida util
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UnitLifeUtilId As Integer? Implements IFixedAssetEquipment.UnitLifeUtilId
        Get
            Return INDsleUnitLifeUtil.EditValue
        End Get
        Set(value As Integer?)
            INDsleUnitLifeUtil.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el ultimo costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LastCost As Decimal Implements IFixedAssetEquipment.LastCost
        Get
            Return 0
        End Get
        Set(value As Decimal)

        End Set
    End Property

    ''' <summary>
    ''' Obtiene el layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetEquipment.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IFixedAssetEquipment.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del catlogo de equipos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EquipmentCatalogId As Integer? Implements IFixedAssetEquipment.EquipmentCatalogId
        Get
            Return INDSlEquipmentCatalog.EditValue
        End Get
        Set(value As Integer?)
            INDSlEquipmentCatalog.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del catlogo bienes y servicios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CatalogOfPropertyandServicesId As Integer? Implements IFixedAssetEquipment.CatalogOfPropertyandServicesId
        Get
            Return INDSleCatalogOfPropertyandServices.EditValue
        End Get
        Set(value As Integer?)
            INDSleCatalogOfPropertyandServices.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAId As Integer? Implements IFixedAssetEquipment.IVAId
        Get
            Return INDSlIVA.EditValue
        End Get
        Set(value As Integer?)
            INDSlIVA.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVAXPO As XPInstantFeedbackSource Implements IFixedAssetEquipment.IVAXPO
        Get
            Return CType(INDSlIVA.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlIVA.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del catalogo de equipos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EquipmentCatalogXPO As XPInstantFeedbackSource Implements IFixedAssetEquipment.EquipmentCatalogXPO
        Get
            Return CType(INDSlEquipmentCatalog.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlEquipmentCatalog.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de tipo de equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EquipmentTypeXpo As XPCollection Implements IFixedAssetEquipment.EquipmentTypeXpo
        Get
            Return INDglEquipmentType.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDglEquipmentType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de catalogo de bienes y servicios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CatalogOfPropertyandServicesXpo As XPInstantFeedbackSource Implements IFixedAssetEquipment.CatalogOfPropertyandServicesXpo
        Get
            Return INDSleCatalogOfPropertyandServices.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCatalogOfPropertyandServices.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el codigo del tipo de equipo
    ''' </summary>
    Public Property IdEquipmentType As Integer? Implements IFixedAssetEquipment.IdEquipmentType
        Get
            Return INDglEquipmentType.EditValue
        End Get
        Set(value As Integer?)
            INDglEquipmentType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene las observaciones del equipo
    ''' </summary>
    Public Property Comments As String Implements IFixedAssetEquipment.Comments
        Get
            Return INDmeObjeto.EditValue
        End Get
        Set(value As String)
            INDmeObjeto.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el nombre del equipo
    ''' </summary>
    Public Property NameEquipment As String Implements IFixedAssetEquipment.NameEquipment
        Get
            Return INDTxtName.EditValue
        End Get
        Set(value As String)
            INDTxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalBookId As Integer? Implements IFixedAssetEquipment.LegalBookId
        Get
            Return INDsleLegalBook.EditValue
        End Get
        Set(value As Integer?)
            INDsleLegalBook.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalBookXpo As XPInstantFeedbackSource Implements IFixedAssetEquipment.LegalBookXpo
        Get
            Return INDsleLegalBook.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleLegalBook.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As FixedAssetSequence Implements IFixedAssetEquipment.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As FixedAssetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.FixedAssetSequenceDetail In Me._sequence.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo de un concepto de notas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IFixedAssetEquipment.Code
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de los registros de los conceptos de notas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IFixedAssetEquipment.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Dim Equipment As FixedAssetItem

    ''' <summary>
    ''' Variable que obtiene el controlador de ultimo costo
    ''' </summary>
    Dim ctrLastCost As CtrLastCost

    Dim ListActiveCurrencies As List(Of CommonCurrencyXpo)


    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListUnitLifeUtil As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListDepreciationType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PFixedAssetEquipment

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordFixedAsset

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Listado de detalles de equipo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListEquipmentDetail As List(Of FixedAssetItemDetail)

    ''' <summary>
    ''' Listado de eliminados de detalles de equipo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteEquipmentDetail As List(Of FixedAssetItemDetail)

    ''' <summary>
    ''' Permite saber si esta en modo de edición para el popup
    ''' (True=Edita, False=Guarda)
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagEditMode As Boolean

    ''' <summary>
    ''' Variable que representa la cultura
    ''' </summary>
    Dim _culture As CultureInfo

    ''' <summary>
    ''' Representa a la entidad del detalle de equipo cuando se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim EquipmentDetail As FixedAssetItemDetail

    ''' <summary>
    ''' Permite saber si al cambiar el search de tipo de depreciación se limpia los controles o no
    ''' (False=NoLimpia, True=Limpia)
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagPopup As Boolean

    ''' <summary>
    ''' Parametros de Activos fijos
    ''' </summary>
    Private _parameterFixedAsset As SettingFixedAsset

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        If Me.Equipment IsNot Nothing AndAlso Me.Equipment.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MFixedAssetEquipment
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteEquipment(Me.Equipment)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If

        If INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ListEquipmentDetail Is Nothing OrElse ListEquipmentDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un detalle."
                Exit Sub
            End If
        End If

        AssigningValues()

        Try
            Using Model As New MFixedAssetEquipment
                AsyncLoader(True)
                Dim result As ActionResult(Of FixedAssetItem) = Await Model.SaveEquipmentAsync(Me.Equipment, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If Equipment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.Equipment = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            NewEquipment()
        End If
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    '''Habilita o Desabilita el segmento ocho "Detalles"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidationAllowDepreciate()
        If AllowDepreciate IsNot Nothing Then
            If AllowDepreciate Then
                INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub
    ''' <summary>
    '''Habilita o Desabilita el segmento ocho "Detalles"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DepreciateFieldsEnabled(bool As Boolean)
        INDsleAllowDepreciate.Enabled = bool
        INDsleDepreciateByTimeUse.Enabled = bool
    End Sub
    ''' <summary>
    '''Metodo Tupla validacion Tipo de amortizacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitilizeTupleAmortizable()
        Dim ListDepreciation = New List(Of Tuple(Of Integer, String))

        ListDepreciation.Add(New Tuple(Of Integer, String)(1, "Línea Recta"))
        INDsleDepreciationType.Properties.DataSource = ListDepreciation.ToList
    End Sub
    ''' <summary>
    ''' Cargar catálogo de elementos de activos fijos
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadFixedAssetItemCatalog() As Task
        Dim itemCatalogId As Integer? = INDSlEquipmentCatalog.EditValue

        If itemCatalogId.HasValue Then
            Dim catalog = Await Task.Factory.StartNew(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.GetXPOObject(Of FixedAssetItemCatalogXpo)($"Id={itemCatalogId}"))
            INDLyCtrEquipment.BeginUpdate()
            If catalog IsNot Nothing Then

                If catalog.Classification = CByte(2) Then
                    INDlyItemAllowDepreciate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemAmortizes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemLifeUtil.Text = "Periodo Amortización"
                    INDlyItemUnitLifeUtil.Text = "Unidad"
                    INDlyItemDepreciationType.Text = "Amortizacion"
                    INDcolDepreciationType.Caption = "Tipo Amortizacion"
                    INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    InitilizeTupleAmortizable()
                    DepreciateFieldsEnabled(True)

                ElseIf catalog.Classification = CByte(3) Then 'Validación para Bienes Controlables
                    INDlyItemAllowDepreciate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemDepreciateByTimeUse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAmortizes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.AllowDepreciate = False
                    Me.DepreciateByTimeUse = False
                    DepreciateFieldsEnabled(False)

                Else
                    INDlyItemAllowDepreciate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAmortizes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemLifeUtil.Text = "Vida Util"
                    INDlyItemUnitLifeUtil.Text = "Tipo"
                    INDlyItemDepreciationType.Text = "Depreciación"
                    INDcolDepreciationType.Caption = "Tipo Depreciación"
                    INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    InitializeTuple()
                    ValidationAllowDepreciate()
                    DepreciateFieldsEnabled(True)
                End If
            End If

            INDLyCtrEquipment.EndUpdate()

        End If

    End Function
    ''' <summary>
    ''' Metodo que se utiliza para redimensionar el popup dependiendo del tipo de depreciación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RezizablePopup()
        Dim size As System.Drawing.Size
        If DepreciationTypeId IsNot Nothing Then
            If DepreciationTypeId = 4 Then 'Unidades producidas
                INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                size.Width = 432
                size.Height = 245
            ElseIf DepreciationTypeId = 3 Then 'Reducción de saldos
                INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                size.Width = 432
                size.Height = 245
            Else 'Línea recta o suma de dígitos
                INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                size.Width = 432
                size.Height = 205
            End If
        Else
            INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            size.Width = 432
            size.Height = 205
        End If
        INDpceDetail.Properties.PopupSizeable = True
        INDpopupDetail.Size = size
        INDpceDetail.Properties.PopupSizeable = False
        If DepreciationTypeId IsNot Nothing Then
            INDpceDetail.ShowPopup()
            INDsleDepreciationType.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Valida que el el tipo de proveedor sea el ultimo item
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ValidateEquipmentType()
        If INDglEquipmentType.EditValue IsNot Nothing AndAlso INDglEquipmentType.Properties.DataSource IsNot Nothing Then
            Dim item As FixedAssetEquipmentTypeXpo = (From fu In EquipmentTypeXpo Where fu.Id = INDglEquipmentType.EditValue Select fu).FirstOrDefault
            If item IsNot Nothing Then
                Dim count = (From fu As FixedAssetEquipmentTypeXpo In EquipmentTypeXpo Where fu.ParentId IsNot Nothing AndAlso fu.ParentId.Id = INDglEquipmentType.EditValue Select fu).Count
                If count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedItemLastLevel")
                    INDglEquipmentType.Properties.NullText = String.Empty
                    IdEquipmentType = Nothing
                    Exit Sub
                End If
            End If
        End If
        ValidateControlsDetails()
    End Sub

    Private Sub ValidateControlsDetails()
        If INDglEquipmentType.EditValue IsNot Nothing Then
            INDSleAccesory.Properties.DataSource = Nothing
            INDSleConsumible.Properties.DataSource = Nothing
            INDSlePart.Properties.DataSource = Nothing
            INDSleProtocolMaintenance.Properties.DataSource = Nothing
            INDSleTechnicalRegister.Properties.DataSource = Nothing


            INDSleAccesory.Enabled = True
            INDSleConsumible.Enabled = True
            INDSlePart.Enabled = True
            INDSleProtocolMaintenance.Enabled = True
            INDSleTechnicalRegister.Enabled = True
        Else
            INDSleAccesory.EditValue = Nothing
            INDSleConsumible.EditValue = Nothing
            INDSlePart.EditValue = Nothing
            INDSleProtocolMaintenance.EditValue = Nothing
            INDSleTechnicalRegister.EditValue = Nothing

            INDSleAccesory.Enabled = False
            INDSleConsumible.Enabled = False
            INDSlePart.Enabled = False
            INDSleProtocolMaintenance.Enabled = False
            INDSleTechnicalRegister.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Edita el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        EquipmentDetail = CType(ViewDetail.GetFocusedRow, FixedAssetItemDetail)
        FlagEditMode = True
        FlagPopup = True
        With EquipmentDetail
            LegalBookId = .LegalBookId
            INDsleLegalBook.Properties.NullText = .CodeNameLegalBook
            LifeUtil = .LifeTime
            UnitLifeUtilId = .UnitLifeTime
            TotalProductionUnit = .TotalProductionUnit
            PercentageRescue = .PercentageRescue

            INDsleLegalBook.Properties.ReadOnly = True
            INDseLifeUtil.Focus()

            DepreciationTypeId = .DepreciationType
        End With
        INDsleLegalBook.Focus()
    End Sub

    ''' <summary>
    ''' Elimina el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        EquipmentDetail = CType(ViewDetail.GetFocusedRow, FixedAssetItemDetail)
        ListEquipmentDetail.Remove(EquipmentDetail)

        If EquipmentDetail.Id > 0 Then
            If ListDeleteEquipmentDetail Is Nothing Then
                ListDeleteEquipmentDetail = New List(Of FixedAssetItemDetail)
            End If
            EquipmentDetail.MarkAsDeleted()
            ListDeleteEquipmentDetail.Add(EquipmentDetail)
        End If

        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListEquipmentDetail
    End Sub

    ''' <summary>
    ''' Metodo que agrega un detalle a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDetail()
        'Se valida que los controles esten diligenciados
        Dim errors As String = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If FlagEditMode = False Then 'Si se esta guardando
            If ListEquipmentDetail Is Nothing Then 'Si no hay registros en la rejilla
                ListEquipmentDetail = New List(Of FixedAssetItemDetail)
            Else 'Si ya hay registros en la rejilla
                'Se valida que el libro seleccionado en el search no exista en la rejilla
                If (From l In ListEquipmentDetail Where l.LegalBookId = LegalBookId Select l).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El libro " + INDsleLegalBook.Text + " ya existe en la lista."
                    INDsleLegalBook.Focus()
                    Exit Sub
                End If
            End If

            Dim _equipmentDetail As New FixedAssetItemDetail
            'Se crea la nueva entidad para agregarlo al listado
            With _equipmentDetail
                .LegalBookId = LegalBookId
                .CodeNameLegalBook = INDsleLegalBook.Text
                .LifeTime = LifeUtil
                .UnitLifeTime = UnitLifeUtilId
                .DepreciationType = DepreciationTypeId
                If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .TotalProductionUnit = TotalProductionUnit
                Else
                    .TotalProductionUnit = 0
                End If
                If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .PercentageRescue = PercentageRescue
                Else
                    .PercentageRescue = 0
                End If
            End With
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
            ListEquipmentDetail.Add(_equipmentDetail)
        Else 'Si se esta editando
            'Se edita el objeto que se obiene cuando se edita
            With EquipmentDetail
                .LegalBookId = LegalBookId
                .CodeNameLegalBook = INDsleLegalBook.Text
                .LifeTime = LifeUtil
                .UnitLifeTime = UnitLifeUtilId
                .DepreciationType = DepreciationTypeId
                If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .TotalProductionUnit = TotalProductionUnit
                Else
                    .TotalProductionUnit = 0
                End If
                If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .PercentageRescue = PercentageRescue
                Else
                    .PercentageRescue = 0
                End If
            End With
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If

        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListEquipmentDetail
        CleanControlsPopup()
        FlagEditMode = False
        INDpceDetail.ShowPopup()
        INDsleLegalBook.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If LegalBookId Is Nothing Then
            listErrors.AppendLine("Ingrese un Libro Oficial.")
        End If
        If LifeUtil = Nothing OrElse LifeUtil = 0 Then
            listErrors.AppendLine("Ingrese Vida Util.")
        End If
        If UnitLifeUtilId Is Nothing Then
            listErrors.AppendLine("Ingrese una Unidad Vida Util.")
        End If
        If DepreciationTypeId Is Nothing Then
            listErrors.AppendLine("Ingrese Tipo Depreciación.")
        End If
        If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If TotalProductionUnit = 0 Then
                listErrors.AppendLine("Ingrese Total Unidades.")
            End If
        End If
        If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If PercentageRescue = 0 Then
                listErrors.AppendLine("Ingrese % Salvamento.")
            End If
        End If
        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo que inicializa la tupla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        'Unidad vida util
        ListUnitLifeUtil = New List(Of Tuple(Of Integer, String))
        ListUnitLifeUtil.Add(New Tuple(Of Integer, String)(1, "Año"))
        ListUnitLifeUtil.Add(New Tuple(Of Integer, String)(2, "Mes"))
        ListUnitLifeUtil.Add(New Tuple(Of Integer, String)(3, "Día"))
        INDsleUnitLifeUtil.Properties.DataSource = ListUnitLifeUtil.ToList
        If INDsleUnitLifeUtil.Properties.Buttons.Count > 1 Then
            INDsleUnitLifeUtil.Properties.Buttons(1).Visible = False
        End If

        'Tipo de depreciación
        ListDepreciationType = New List(Of Tuple(Of Integer, String))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(1, "Línea Recta"))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(2, "Suma de Dígitos"))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(3, "Reducción de Saldos"))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(4, "Unidades de Producción"))
        INDsleDepreciationType.Properties.DataSource = ListDepreciationType.ToList

        Dim ListYesNo = New List(Of Tuple(Of Boolean, String))
        ListYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDGleAmortizes.Properties.DataSource = ListYesNo.ToList

    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.Equipment IsNot Nothing AndAlso Me.Equipment.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetEquipment.ActionsOnControls
        Set(value As Boolean)
            INDLyCtrEquipment.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDglEquipmentType.Enabled = value
            INDSleCatalogOfPropertyandServices.Enabled = value
            INDsleDepreciateByTimeUse.Enabled = value
            INDSlEquipmentCatalog.Enabled = value
            INDSlIVA.Enabled = value
            INDtxtFairValue.Enabled = value
            INDGleAmortizes.Enabled = value
            INDsleAllowDepreciate.Enabled = value
            INDmeObjeto.Enabled = value
            INDpceDetail.Enabled = value
            INDgcDetail.Enabled = value

            INDGcAccesory.Enabled = value
            INDGcConsumible.Enabled = value
            INDGcPart.Enabled = value
            INDGcProtocolMaintenance.Enabled = value
            INDGcTechnicalRegister.Enabled = value

            INDBtnAddAccesory.Enabled = value
            INDBtnConsumible.Enabled = value
            INDBtnAddPart.Enabled = value
            INDBtnAddProtocol.Enabled = value
            INDBtnAddTechnicalRegister.Enabled = value

            INDLyCtrEquipment.EndUpdate()
            If value Then
                INDTxtName.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Último Costo", .FieldName = "LastCostItem", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25), .ColumnFormat = "C0", .FormatCulture = _culture},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Equipment.Code, Me.Equipment.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.Equipment.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Equipment.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Equipment.Code, Me.Equipment.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Equipment.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    Private _isCleaning As Boolean = False

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        _isCleaning = True
        INDLyCtrEquipment.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me._doc = Nothing
        Status = True
        Code = String.Empty
        NameEquipment = String.Empty

        INDSleAccesory.Enabled = False
        INDSleConsumible.Enabled = False
        INDSlePart.Enabled = False
        INDSleProtocolMaintenance.Enabled = False
        INDSleTechnicalRegister.Enabled = False

        'listas
        INDSleAccesory.EditValue = Nothing
        INDSleConsumible.EditValue = Nothing
        INDSlePart.EditValue = Nothing
        INDSleProtocolMaintenance.EditValue = Nothing
        INDSleTechnicalRegister.EditValue = Nothing

        INDGcAccesory.DataSource = Nothing
        INDGcConsumible.DataSource = Nothing
        INDGcPart.DataSource = Nothing
        INDGcProtocolMaintenance.DataSource = Nothing
        INDGcTechnicalRegister.DataSource = Nothing

        IdEquipmentType = Nothing
        INDglEquipmentType.Properties.NullText = String.Empty

        DepreciateByTimeUse = Nothing

        EquipmentCatalogId = Nothing
        INDSlEquipmentCatalog.Properties.NullText = String.Empty

        CatalogOfPropertyandServicesId = Nothing
        INDSleCatalogOfPropertyandServices.Properties.NullText = String.Empty

        IVAId = Nothing
        INDSlIVA.Properties.NullText = String.Empty

        LastCost = 0.0
        ctrLastCost.INDgcLastCost.DataSource = Nothing
        ctrLastCost.LastCostItemValue = Nothing

        FairValue = Nothing
        Comments = Nothing
        Amortizes = Nothing
        AllowDepreciate = Nothing
        INDgcDetail.DataSource = Nothing
        ListEquipmentDetail = Nothing
        ListDeleteEquipmentDetail = Nothing

        _technicalLogSelected = Nothing
        _accesorioSelected = Nothing
        _consumibleSelected = Nothing
        _partSelected = Nothing
        _protocolSelected = Nothing

        CleanControlsPopup()
        'Limpiar controles
        INDlygDetail.HideControl()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLyCtrEquipment.EndUpdate()
        _isCleaning = False
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        LegalBookId = Nothing
        INDsleLegalBook.Properties.NullText = String.Empty
        LifeUtil = Nothing
        UnitLifeUtilId = Nothing
        INDsleLegalBook.Properties.ReadOnly = False
        DepreciationTypeId = Nothing
        TotalProductionUnit = Nothing
        PercentageRescue = Nothing
        FlagEditMode = False
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With Equipment
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Description = NameEquipment
            .ItemTypeId = IdEquipmentType
            .DepreciateByTimeUse = DepreciateByTimeUse
            .ItemCatalogId = EquipmentCatalogId
            .IVAId = IVAId
            .FairValue = FairValue
            .AllowDepreciate = AllowDepreciate
            .Amortizes = If(Amortizes IsNot Nothing, Amortizes, False)
            .Observations = Comments

            If INDlyItemCatalogOfPropertyandServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CatalogOfPropertyandServicesId = CatalogOfPropertyandServicesId
            Else
                .CatalogOfPropertyandServicesId = Nothing
            End If

            If INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then 'Si esta activo el grupo de detalles
                If ListEquipmentDetail IsNot Nothing AndAlso ListEquipmentDetail.Count > 0 Then
                    ListEquipmentDetail.ForEach(Sub(item)
                                                    .FixedAssetItemDetail.Add(item)
                                                End Sub)
                End If

                If ListDeleteEquipmentDetail IsNot Nothing AndAlso ListDeleteEquipmentDetail.Count > 0 Then
                    ListDeleteEquipmentDetail.ForEach(Sub(item)
                                                          .FixedAssetItemDetail.Add(item)
                                                      End Sub)
                End If
            End If

            If .Id > 0 Then
                .MarkAsModified()
                .LastCostItem = .LastCostItem
            Else
                .LastCostItem = LastCost
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Try
                Using Model As New MFixedAssetEquipment
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetEquipment(INDBteCode.Text.Trim)
                    INDLyCtrEquipment.BeginUpdate()
                    Equipment = resultOperation.ObjectEmbbeded
                    If Equipment IsNot Nothing AndAlso Equipment.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(Equipment.Id))
                            With Equipment
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad

                                Code = .Code
                                NameEquipment = .Description
                                IdEquipmentType = .ItemTypeId

                                EquipmentCatalogId = .ItemCatalogId
                                INDSlEquipmentCatalog.Properties.NullText = .CodeNameEquipmentCatalog

                                CatalogOfPropertyandServicesId = .CatalogOfPropertyandServicesId
                                INDSleCatalogOfPropertyandServices.Properties.NullText = .CodeNameCatalogOfPropertyandServices

                                DepreciateByTimeUse = .DepreciateByTimeUse

                                IVAId = .IVAId
                                INDSlIVA.Properties.NullText = .CodeNameIVA

                                Await ctrLastCost.PrintCostsPerCurrencies(.Id)

                                LastCost = .LastCostItem
                                FairValue = .FairValue
                                Comments = .Observations
                                Status = .Status
                                AllowDepreciate = .AllowDepreciate
                                Amortizes = .Amortizes
                                ListEquipmentDetail = .FixedAssetItemDetail.ToList
                                INDgcDetail.DataSource = Nothing
                                INDgcDetail.DataSource = ListEquipmentDetail

                                INDGcAccesory.DataSource = .FixedAssetItemAccesory.ToList()
                                INDGcConsumible.DataSource = .FixedAssetItemConsumible.ToList()
                                INDGcPart.DataSource = .FixedAssetItemPart.ToList()
                                INDGcProtocolMaintenance.DataSource = .FixedAssetItemProtocol.ToList()
                                INDGcTechnicalRegister.DataSource = .FixedAssetItemTechnicalLog.ToList()

                                INDGcAccesory.RefreshDataSource()
                                INDGcConsumible.RefreshDataSource()
                                INDGcPart.RefreshDataSource()
                                INDGcProtocolMaintenance.RefreshDataSource()
                                INDGcTechnicalRegister.RefreshDataSource()

                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Equipment.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Equipment.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(Equipment.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetItem).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewEquipment()
                            Amortizes = False
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLyCtrEquipment.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewEquipment() As Task
        Equipment = New FixedAssetItem() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        'If Not String.IsNullOrEmpty(Code) Then
        '    Dim state As Boolean
        '    Select Case Status
        '        Case CBool(eActionsStatusRecords.Active)
        '            state = True
        '        Case CBool(eActionsStatusRecords.Inactive)
        '            state = False
        '    End Select
        '    Using model As New MFixedAssetEquipment()
        '        AsyncLoader(True)
        '        Dim Result = Await model.ChangeState(Code, state)
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        '        Else
        '            If Result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '            End If
        '        End If
        '    End Using
        'Else
        '    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        'End If


        If Not String.IsNullOrEmpty(Me.Equipment.Code) Then
            Try
                Using model As New MFixedAssetEquipment
                    AsyncLoader(True)
                    Dim state As Boolean = Not Equipment.Status
                    Dim result As ActionResult(Of FixedAssetItem) = Await model.ChangeState(Me.Equipment.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.Equipment = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDBteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Equipment = Nothing
        ListUnitLifeUtil = Nothing
        ListDepreciationType = Nothing
        SearchMode = Nothing
        Presenter = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        ListEquipmentDetail = Nothing
        ListDeleteEquipmentDetail = Nothing
        FlagEditMode = Nothing
        EquipmentDetail = Nothing
        FlagPopup = Nothing
    End Sub

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmEquipment_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetEquipment(Me)
        IndigoGridControl1.RefreshGrid(INDgcDetail)

        Deshacer()
        INDBteCode.Enabled = False
        Await Me.LoadParameters()
        ListActiveCurrencies = Presenter.InitializeCurrency()
        Presenter.GetSequense()
        InitializeTuple()
        InitColumnActions()
        Presenter.InitializeEquipmentType()
        Presenter.InitializeCatalogOfPropertyandServices()
        Presenter.InitializeIVA()
        INDBteCode.Enabled = True
        LoadStatus()
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de cargar las acciones en las rejillas 
    ''' </summary>
    Private Sub InitColumnActions()
        IndigoGridView1.SetListAcction(ViewDetail, {eAcciones.Remove, eAcciones.Edit}.ToList())
        IndigoGridView2.SetListAcction(INDGvAccesory, {eAcciones.Remove}.ToList())
        IndigoGridView3.SetListAcction(INDGvConsumible, {eAcciones.Remove}.ToList())
        IndigoGridView4.SetListAcction(INDGvPart, {eAcciones.Remove}.ToList())
        IndigoGridView5.SetListAcction(INDGvProtocolMaintenance, {eAcciones.Remove}.ToList())
        IndigoGridView6.SetListAcction(INDGvTechnicalRegister, {eAcciones.Remove}.ToList())

        INDGvAccesory.Columns.FirstOrDefault(Function(o) o.Name = "colActions").Width = 100
        INDGvConsumible.Columns.FirstOrDefault(Function(o) o.Name = "colActions").Width = 100
        INDGvPart.Columns.FirstOrDefault(Function(o) o.Name = "colActions").Width = 100
        INDGvProtocolMaintenance.Columns.FirstOrDefault(Function(o) o.Name = "colActions").Width = 100
        INDGvTechnicalRegister.Columns.FirstOrDefault(Function(o) o.Name = "colActions").Width = 100
    End Sub

    Private Async Function LoadParameters() As Task
        Using model As New MSettingFixedAsset(MyTag)
            Me._parameterFixedAsset = Await model.GetSettingFixedAssetByOperatingUnitId(Me._idOperativeUnit)
            If Me._parameterFixedAsset Is Nothing OrElse Me._parameterFixedAsset.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Activos fijos para la unidad operativa seleccionada"
                Exit Function
            End If

            SetCurrencyUI(_parameterFixedAsset.Currency?.Abbreviation)

            If _parameterFixedAsset.AppliesCatalogPropertyandServices IsNot Nothing Then
                INDlyItemCatalogOfPropertyandServices.AllowHide = Not _parameterFixedAsset.AppliesCatalogPropertyandServices
                INDlyItemCatalogOfPropertyandServices.ShowInCustomizationForm = Not _parameterFixedAsset.AppliesCatalogPropertyandServices
                INDlyItemCatalogOfPropertyandServices.Visibility = If(_parameterFixedAsset.AppliesCatalogPropertyandServices, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            Else
                INDlyItemCatalogOfPropertyandServices.HideLayout
            End If

        End Using
    End Function

    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        _culture = New Globalization.CultureInfo(_currencyAbbreviation.GetCultureId())
        ctrLastCost.CodeISO4217 = _parameterFixedAsset.Currency.Abbreviation
        ctrLastCost.PrintSingleCost(LastCost)
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetEquipment_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewEquipment()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de popupContainerEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceDetail.ShowPopup()
            INDsleLegalBook.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que abre el formulario de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDglEquipmentType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDglEquipmentType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then

        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de libro oficial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLegalBook_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLegalBook.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)
            Presenter.InitializeLegalBook()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de catalogo de equipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlEquipmentCatalog_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlEquipmentCatalog.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1697, Nothing, True)
            Presenter.InitializeEquipmentCatalog()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de iva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlIVA_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlIVA.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1509, Nothing, True)
            Presenter.InitializeIVA()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de libro oficial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLegalBook_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleLegalBook.QueryPopUp
        If LegalBookXpo Is Nothing Then
            Presenter.InitializeLegalBook()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de catalogo de equipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlEquipmentCatalog_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlEquipmentCatalog.QueryPopUp
        If EquipmentCatalogXPO Is Nothing Then
            Presenter.InitializeEquipmentCatalog()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetEquipment_Shown(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de popupContainerEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceDetail_Popup(sender As Object, e As EventArgs) Handles INDpceDetail.Popup
        'INDsleLegalBook.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        AddDetail()
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ViewDetail_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles ViewDetail.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDcolUnitLifeUtil.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Año"
                Case 2
                    e.DisplayText = "Mes"
                Case 3
                    e.DisplayText = "Día"
                Case Else

            End Select
        End If
        If e.Column.Name = INDcolDepreciationType.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Línea Recta"
                Case 2
                    e.DisplayText = "Suma de Dígitos"
                Case 3
                    e.DisplayText = "Reducción de Saldos"
                Case 4
                    e.DisplayText = "Unidades de Producción"
                Case Else

            End Select
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Despliega los botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Accion de click derecho del mouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceDetail_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceDetail.CloseUp
        If FlagEditMode = True AndAlso FlagPopup = True Then
            CleanControlsPopup()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del campo amortiza
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub INDSleCatalogOfPropertyandServices_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCatalogOfPropertyandServices.EditValueChanged
        If CatalogOfPropertyandServicesId IsNot Nothing And _parameterFixedAsset.AppliesCatalogPropertyandServices Then
            Dim CatalogOfPropertyandServices = Await Task.Factory.StartNew(Function() As FixedAssetCatalogOfPropertyandServicesXpo
                                                                               Return XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.GetXPOObject(Of FixedAssetCatalogOfPropertyandServicesXpo)($"Id ='{CatalogOfPropertyandServicesId}'")
                                                                           End Function)

            If CatalogOfPropertyandServices IsNot Nothing Then
                IVAId = CatalogOfPropertyandServices.IVAId
                INDSlIVA.ReadOnly = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del campo amortiza
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDGleAmortizes_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAmortizes.EditValueChanged
        If Amortizes IsNot Nothing Then
            If Amortizes Then
                INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del Catálogo de Equipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSlEquipmentCatalog_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlEquipmentCatalog.EditValueChanged
        Await LoadFixedAssetItemCatalog()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del tipo de equipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDglEquipmentType_EditValueChanged(sender As Object, e As EventArgs) Handles INDglEquipmentType.EditValueChanged
        If IdEquipmentType IsNot Nothing Then
            ValidateEquipmentType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del tipo de depreciación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDepreciationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDepreciationType.EditValueChanged
        FlagPopup = False
        RezizablePopup()
        FlagPopup = True
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de si permite depreciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAllowDepreciate_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAllowDepreciate.EditValueChanged
        ValidationAllowDepreciate()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.FixedAssetSequenceDetail IsNot Nothing Then
                If Not Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

#Region "QueryPopup"
    Private Sub INDSleAccesory_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleAccesory.QueryPopUp
        If INDSleAccesory.Properties.DataSource Is Nothing Then
            Using model As New Presentation.Maintenance.MVP.MAccessories()
                INDSleAccesory.Properties.DataSource = model.ListAccesoriesByEquipmentType(INDglEquipmentType.EditValue)
            End Using
        End If
    End Sub

    Private Sub INDSleConsumible_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleConsumible.QueryPopUp
        If INDSleConsumible.Properties.DataSource Is Nothing Then
            Using model As New Presentation.Maintenance.MVP.MConsumables()
                INDSleConsumible.Properties.DataSource = model.ListAllConsumableByEquipmentType(INDglEquipmentType.EditValue)
            End Using
        End If
    End Sub

    Private Sub INDSlePart_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlePart.QueryPopUp
        If INDSlePart.Properties.DataSource Is Nothing Then
            Using model As New Presentation.FixedAsset.MVP.MFixedAssetPartsAccesoriesConsumables(Me.Tag)
                INDSlePart.Properties.DataSource = model.ListPartsByEquipmentType(INDglEquipmentType.EditValue)
            End Using
        End If
    End Sub

    Private Sub INDSleProtocolMaintenance_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProtocolMaintenance.QueryPopUp
        If INDSleProtocolMaintenance.Properties.DataSource Is Nothing AndAlso Equipment IsNot Nothing AndAlso Equipment.Id > 0 Then
            Using model As New Presentation.Maintenance.MVP.MProtocolMaintenance(Me.Tag)
                INDSleProtocolMaintenance.Properties.DataSource = model.ListMaintenanceProtocolByEquipment(Equipment.Id)
            End Using
        End If
    End Sub

    Private Sub INDSleTechnicalRegister_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleTechnicalRegister.QueryPopUp
        If INDSleTechnicalRegister.Properties.DataSource Is Nothing Then
            Using model As New Presentation.Maintenance.MVP.MTechnicalLog()
                INDSleTechnicalRegister.Properties.DataSource = model.ListTechnicalLog()
            End Using
        End If
    End Sub
#End Region

#Region "Click"

    Private _accesorioSelected As Infrastructure.Data.Xpo.MaintenanceRepository.Maintenance_Accessory
    Private Sub INDSlvAccesorio_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDSlvAccesorio.RowClick
        Dim obj = DirectCast(INDSlvAccesorio.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
            _accesorioSelected = obj.OriginalRow
        End If
    End Sub

    Private Sub INDBtnAddAccesory_Click(sender As Object, e As EventArgs) Handles INDBtnAddAccesory.Click
        If INDSleAccesory.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un accesorio"
            INDSleAccesory.Focus()
            Return
        End If
        If Equipment.FixedAssetItemAccesory.Any(Function(o) o.AccesoryId = CInt(INDSleAccesory.EditValue)) Then
            Mensaje(EeventViewerImages.Advertencia) = "El Accesorio seleccionado ya se encuentra en el listado"
            INDSleAccesory.Focus()
            Return
        End If
        If _accesorioSelected IsNot Nothing Then
            Equipment.FixedAssetItemAccesory.Add(New FixedAssetItemAccesory() With {.AccesoryId = _accesorioSelected.Id, .AccesoryCode = _accesorioSelected.Code, .AccesoryName = _accesorioSelected.Name})
            INDGcAccesory.DataSource = Equipment.FixedAssetItemAccesory.ToList()
            INDGcAccesory.RefreshDataSource()

            INDSleAccesory.EditValue = Nothing
            _accesorioSelected = Nothing
            INDSleAccesory.Focus()
        End If
    End Sub

    Private _consumibleSelected As Infrastructure.Data.Xpo.MaintenanceRepository.Maintenance_Consumable
    Private Sub INDSlvConsumible_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDSlvConsumible.RowClick
        Dim obj = DirectCast(INDSlvConsumible.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
            _consumibleSelected = obj.OriginalRow
        End If
    End Sub

    Private Sub INDBtnConsumible_Click(sender As Object, e As EventArgs) Handles INDBtnConsumible.Click
        If INDSleConsumible.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un accesorio"
            INDSleConsumible.Focus()
            Return
        End If
        If Equipment.FixedAssetItemConsumible.Any(Function(o) o.ConsumibleId = CInt(INDSleConsumible.EditValue)) Then
            Mensaje(EeventViewerImages.Advertencia) = "El Consumible seleccionado ya se encuentra en el listado"
            INDSleConsumible.Focus()
            Return
        End If
        If _consumibleSelected IsNot Nothing Then
            Equipment.FixedAssetItemConsumible.Add(New FixedAssetItemConsumible() With {.ConsumibleId = _consumibleSelected.Id, .ConsumibleCode = _consumibleSelected.Code, .ConsumibleName = _consumibleSelected.Name})
            INDGcConsumible.DataSource = Equipment.FixedAssetItemConsumible.ToList()
            INDGcConsumible.RefreshDataSource()

            INDSleConsumible.EditValue = Nothing
            _consumibleSelected = Nothing
            INDSleConsumible.Focus()
        End If
    End Sub

    Private _partSelected As Infrastructure.Data.Xpo.MaintenanceRepository.FixedAsset_FixedAssetPartsAccesoriesConsumables
    Private Sub INDSlvPartes_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDSlvPartes.RowClick
        Dim obj = DirectCast(INDSlvPartes.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
            _partSelected = obj.OriginalRow
        End If
    End Sub

    Private Sub INDBtnAddPart_Click(sender As Object, e As EventArgs) Handles INDBtnAddPart.Click
        If INDSlePart.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un accesorio"
            INDSlePart.Focus()
            Return
        End If
        If Equipment.FixedAssetItemPart.Any(Function(o) o.PartId = CInt(INDSlePart.EditValue)) Then
            Mensaje(EeventViewerImages.Advertencia) = "La Parte seleccionado ya se encuentra en el listado"
            INDSlePart.Focus()
            Return
        End If
        If _partSelected IsNot Nothing Then
            Equipment.FixedAssetItemPart.Add(New FixedAssetItemPart() With {.PartId = _partSelected.Id, .PartCode = _partSelected.Code, .PartName = _partSelected.Name})
            INDGcPart.DataSource = Equipment.FixedAssetItemPart.ToList()
            INDGcPart.RefreshDataSource()

            INDSlePart.EditValue = Nothing
            _partSelected = Nothing
            INDSlePart.Focus()
        End If
    End Sub

    Private _protocolSelected As Infrastructure.Data.Xpo.MaintenanceRepository.Maintenance_MaintenanceProtocol
    Private Sub INDSlvProtocol_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDSlvProtocol.RowClick
        Dim obj = DirectCast(INDSlvProtocol.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
            _protocolSelected = obj.OriginalRow
        End If
    End Sub

    Private Sub INDBtnAddProtocol_Click(sender As Object, e As EventArgs) Handles INDBtnAddProtocol.Click
        If INDSleProtocolMaintenance.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Protocolo"
            INDSleProtocolMaintenance.Focus()
            Return
        End If
        If Equipment.FixedAssetItemProtocol.Any(Function(o) o.MaintenanceProtocolId = CInt(INDSleProtocolMaintenance.EditValue)) Then
            Mensaje(EeventViewerImages.Advertencia) = "El Protocolo seleccionado ya se encuentra en el listado"
            INDSleProtocolMaintenance.Focus()
            Return
        End If
        If _protocolSelected IsNot Nothing Then
            Equipment.FixedAssetItemProtocol.Add(New FixedAssetItemProtocol() With {.MaintenanceProtocolId = _protocolSelected.Id, .ProtocolCode = _protocolSelected.Code, .ProtocolName = _protocolSelected.Name})
            INDGcProtocolMaintenance.DataSource = Equipment.FixedAssetItemProtocol.ToList()
            INDGcProtocolMaintenance.RefreshDataSource()

            INDSleProtocolMaintenance.EditValue = Nothing
            _protocolSelected = Nothing
            INDSleProtocolMaintenance.Focus()
        End If
    End Sub

    Private _technicalLogSelected As Infrastructure.Data.Xpo.MaintenanceRepository.Maintenance_TechnicalLog
    Private Sub INDSlvRegistroTecnico_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDSlvRegistroTecnico.RowClick
        Dim obj = DirectCast(INDSlvRegistroTecnico.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
            _technicalLogSelected = obj.OriginalRow
        End If
    End Sub

    Private Sub INDBtnAddTechnicalRegister_Click(sender As Object, e As EventArgs) Handles INDBtnAddTechnicalRegister.Click
        If INDSleTechnicalRegister.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Registro Técnico"
            INDSleTechnicalRegister.Focus()
            Return
        End If
        If Equipment.FixedAssetItemTechnicalLog.Any(Function(o) o.TechnicalLogId = CInt(INDSleTechnicalRegister.EditValue)) Then
            Mensaje(EeventViewerImages.Advertencia) = "El Registro Técnico seleccionado ya se encuentra en el listado"
            INDSleTechnicalRegister.Focus()
            Return
        End If
        If _technicalLogSelected IsNot Nothing Then
            Equipment.FixedAssetItemTechnicalLog.Add(New FixedAssetItemTechnicalLog() With {.TechnicalLogId = _technicalLogSelected.Id, .TechnicalLogCode = _technicalLogSelected.Code, .TechnicalLogName = _technicalLogSelected.Name})
            INDGcTechnicalRegister.DataSource = Equipment.FixedAssetItemTechnicalLog.ToList()
            INDGcTechnicalRegister.RefreshDataSource()

            INDSleTechnicalRegister.EditValue = Nothing
            _technicalLogSelected = Nothing
            INDSleTechnicalRegister.Focus()
        End If
    End Sub

    Private Sub INDGcAccesory_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcAccesory.DataSourceChanged, INDGcConsumible.DataSourceChanged, INDGcPart.DataSourceChanged
        If Not _isCleaning Then
            INDglEquipmentType.Enabled = Not (INDGcAccesory.DataSource IsNot Nothing AndAlso INDGcConsumible.DataSource IsNot Nothing AndAlso INDGcPart.DataSource IsNot Nothing)
        End If
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            CType(INDGvAccesory.GetFocusedRow(), FixedAssetItemAccesory).MarkAsDeleted()
            INDGcAccesory.DataSource = Equipment.FixedAssetItemAccesory.ToList()
            INDGcAccesory.RefreshDataSource()
            If Equipment.Id > 0 Then
                Equipment.MarkAsModified()
            End If
        End If
    End Sub

    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction, IndigoGridView3.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            CType(INDGvConsumible.GetFocusedRow(), FixedAssetItemConsumible).MarkAsDeleted()
            INDGcConsumible.DataSource = Equipment.FixedAssetItemConsumible.ToList()
            INDGcConsumible.RefreshDataSource()
            If Equipment.Id > 0 Then
                Equipment.MarkAsModified()
            End If
        End If
    End Sub

    Private Sub IndigoGridView4_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView4.Click_ButtonAction, IndigoGridView4.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            CType(INDGvPart.GetFocusedRow(), FixedAssetItemPart).MarkAsDeleted()
            INDGcPart.DataSource = Equipment.FixedAssetItemPart.ToList()
            INDGcPart.RefreshDataSource()
            If Equipment.Id > 0 Then
                Equipment.MarkAsModified()
            End If
        End If
    End Sub

    Private Sub IndigoGridView5_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView5.Click_ButtonAction, IndigoGridView5.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            CType(INDGvProtocolMaintenance.GetFocusedRow(), FixedAssetItemProtocol).MarkAsDeleted()
            INDGcProtocolMaintenance.DataSource = Equipment.FixedAssetItemProtocol.ToList()
            INDGcProtocolMaintenance.RefreshDataSource()
            If Equipment.Id > 0 Then
                Equipment.MarkAsModified()
            End If
        End If
    End Sub

    Private Sub IndigoGridView6_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView6.Click_ButtonAction, IndigoGridView6.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            CType(INDGvTechnicalRegister.GetFocusedRow(), FixedAssetItemTechnicalLog).MarkAsDeleted()
            INDGcTechnicalRegister.DataSource = Equipment.FixedAssetItemTechnicalLog.ToList()
            INDGcTechnicalRegister.RefreshDataSource()
            If Equipment.Id > 0 Then
                Equipment.MarkAsModified()
            End If
        End If
    End Sub

#End Region

End Class