'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/10/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports Presentation.Contract.MVP
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports Presentation.Accounting.MVP
Imports System.Globalization
Imports DevExpress.XtraLayout
Imports DevExpress.Data
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.XpoServiceEx
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmRateManual
    Implements IRateManual, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Obtiene el objecto del servicio que tiene el focus.
    ''' </summary>
    Private ReadOnly Property ServiceIPSSelected As ContractIPSServiceXPO
        Get
            If CInt(INDsleIPSServiceId.EditValue) = 0 Then
                Return Nothing
            End If

            Dim ips = XpoServiceEx.Instance(indigo.TransactionalContainer).ContractService.GetXPOObject(Of ContractIPSServiceXPO)($"Id={INDsleIPSServiceId.EditValue}")

            Return ips
        End Get
    End Property
    Private _companySettings As Task(Of CompanySettings)

    ''' <summary>
    ''' Obtiene o establece el valor del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValue As Decimal Implements IRateManual.SalesValue
        Get
            Return INDtxtSalesValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del recargo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValueWithSurcharge As Decimal Implements IRateManual.SalesValueWithSurcharge
        Get
            Return INDtxtSalesValueWithSurcharge.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValueWithSurcharge.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IRateManual.Status
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

    ''' <summary>
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As ContractSequence Implements IRateManual.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As ContractSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.ContractSequenceDetail In Me._sequence.ContractSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRateManual.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements IRateManual.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IRateManual.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre del subgrupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameRM As String Implements IRateManual.NameRM
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el modo de redondeo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RoundService As Integer? Implements IRateManual.RoundService
        Get
            Return INDsleRoundService.EditValue
        End Get
        Set(value As Integer?)
            INDsleRoundService.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Type As Integer? Implements IRateManual.Type
        Get
            Return INDsleType.EditValue
        End Get
        Set(value As Integer?)
            INDsleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del contrato minimo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractMinimumWageId As Integer? Implements IRateManual.ContractMinimumWageId
        Get
            Return INDsleContractMinimumWageId.EditValue
        End Get
        Set(value As Integer?)
            INDsleContractMinimumWageId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de contrato minimo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractMinimumWageXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IRateManual.ContractMinimumWageXpo
        Get
            Return INDsleContractMinimumWageId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleContractMinimumWageId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSServiceId As Integer Implements IRateManual.IPSServiceId
        Get
            Return INDsleIPSServiceId.EditValue
        End Get
        Set(value As Integer)
            INDsleIPSServiceId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de servicios ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSServiceXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IRateManual.IPSServiceXpo
        Get
            Return INDsleIPSServiceId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIPSServiceId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSServiceIdSurgical As Integer Implements IRateManual.IPSServiceIdSurgical
        Get
            Return INDsleIPSServiceIdSurgicalServices.EditValue
        End Get
        Set(value As Integer)
            INDsleIPSServiceIdSurgicalServices.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSServiceXpoSurgical As DevExpress.Xpo.XPInstantFeedbackSource Implements IRateManual.IPSServiceXpoSurgical
        Get
            Return INDsleIPSServiceIdSurgicalServices.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIPSServiceIdSurgicalServices.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValueSurgical As Decimal Implements IRateManual.SalesValueSurgical
        Get
            Return INDtxtSalesValueSurgicalServices.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValueSurgicalServices.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del recargo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValueWithSurchargeSurgical As Decimal Implements IRateManual.SalesValueWithSurchargeSurgical
        Get
            Return INDtxtSalesValueWithSurchargeSurgicalServices.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValueWithSurchargeSurgicalServices.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del grupo quirurgico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SurgicalGroupId As Integer? Implements IRateManual.SurgicalGroupId
        Get
            Return INDsleSurgicalGroupId.EditValue
        End Get
        Set(value As Integer?)
            INDsleSurgicalGroupId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del grupo quirurgico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SurgicalGroupXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IRateManual.SurgicalGroupXpo
        Get
            Return INDsleSurgicalGroupId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSurgicalGroupId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del rango UVR
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UVRRangeId As Integer? Implements IRateManual.UVRRangeId
        Get
            Return INDsleUVRRangeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleUVRRangeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del rango UVR
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UVRRangeXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IRateManual.UVRRangeXpo
        Get
            Return INDsleUVRRangeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleUVRRangeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del servicio ips de material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaterialNoBloodyIPSServiceId As Integer? Implements IRateManual.MaterialNoBloodyIPSServiceId
        Get
            Return INDsleMaterialNoBloodyIPSServiceId.EditValue
        End Get
        Set(value As Integer?)
            INDsleMaterialNoBloodyIPSServiceId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del servicio ips de material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaterialNoBloodyIPSServiceIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IRateManual.MaterialNoBloodyIPSServiceIdXpo
        Get
            Return INDsleMaterialNoBloodyIPSServiceId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleMaterialNoBloodyIPSServiceId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje de no cruentos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PercentageNoBloodyRoom As Decimal? Implements IRateManual.PercentageNoBloodyRoom
        Get
            Return INDsePercentageNoBloodyRoom.EditValue
        End Get
        Set(value As Decimal?)
            INDsePercentageNoBloodyRoom.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si se puede Liquidar todas las cirugías MIVIE.
    ''' </summary>
    ''' <returns></returns>
    Public Property LiquidateAllMIVIE As Boolean? Implements IRateManual.LiquidateAllMIVIE
        Get
            Return INDCELiquidateAllMIVIE.EditValue
        End Get
        Set(value As Boolean?)
            INDCELiquidateAllMIVIE.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"
    ''' <summary>
    ''' Representa el presentador de manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PRateManual

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.ContractSequence

    ''' <summary>
    ''' Representa la entidad de manual tarifario 
    ''' </summary>
    ''' <remarks></remarks>
    Dim rateManual As RateManual

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordContract

    ''' <summary>
    ''' Listado de porcentajes de cirugia
    ''' </summary>
    ''' <remarks></remarks>
    Private ListSurgeryPercentage As List(Of SurgeriesPercentageManual)

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListRound As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListTypeIntervention As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable para saber si es un registro nuevo o se va a modificar para los listados de cirugias
    ''' </summary>
    ''' <remarks></remarks>
    Dim modifiedType As Boolean = False

    ''' <summary>
    ''' Variable para saber si se guarda = true o se modifica = false
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSave As Boolean

    ''' <summary>
    ''' Listado de detalles del manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListRateManualDetail As List(Of RateManualDetail)

    ''' <summary>
    ''' Listado de los detalles del manual tarifario pero quirurgicos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListRateManualDetailSurgical As List(Of RateManualDetailSurgical)

    ''' <summary>
    ''' Listado de eliminados de detalles del manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteRateManualDetail As List(Of RateManualDetail)

    ''' <summary>
    ''' Listado de eliminados de detalles del manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteRateManualDetailSurgical As List(Of RateManualDetailSurgical)

    ''' <summary>
    ''' Listado utilizado para comparar cuando se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listCompareRateManualDetail As List(Of Integer)

    ''' <summary>
    ''' Listado utilizado para comparar cuando se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listCompareRateManualDetailSurgical As List(Of RateManualDetailSurgical)

    ''' <summary>
    ''' True = modificar y False = guardar en las rejillas
    ''' </summary>
    ''' <remarks></remarks>
    Dim modeModify As Boolean = False

    ''' <summary>
    ''' True = modificar y False = guardar en las rejillas
    ''' </summary>
    ''' <remarks></remarks>
    Dim modeModifySurgical As Boolean = False

    ''' <summary>
    ''' Representa la entidad de detalles del manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _rateManualDetail As RateManualDetail

    ''' <summary>
    ''' Representa la entidad de detalles del manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _rateManualDetailSurgical As RateManualDetailSurgical

    ''' <summary>
    ''' Variable para  la informacion del formato numerico
    ''' </summary>
    Private FormatNumber As Globalization.NumberFormatInfo


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

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If rateManual IsNot Nothing AndAlso rateManual.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MRateManual(Me.Tag.ToString())
                        AsyncLoader(True)
                        'rateManual.MarkAsDeleted()
                        Dim result = Await Model.DeleteRateManual(rateManual)
                        If result.StateResult = True Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
                            If result.MessageResult(0) = "-999" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                            ElseIf result.MessageResult(0) = "-000" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using model As New MRateManual(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveRateManual(rateManual, _idCurrentSequence)
                If Result.StateResult = True Then
                    If banSave = True Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf banSave = False Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.rateManual = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    ElseIf Result.MessageResult(0) = "-111" Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence?.IsManual Then
            Deshacer()
        Else
            Await NewRateManual()
        End If
    End Sub

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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "TypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Ajuste", .FieldName = "RoundServiceName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListRateManual
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que agrega un servicio a la rejilla de servicios quirurgicos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddServiceSurgical()
        Dim errors = ValidateControlsPopupSurgical()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            INDsleIPSServiceIdSurgicalServices.Focus()
            Exit Sub
        End If

        If modeModifySurgical = False Then
            If ListRateManualDetailSurgical Is Nothing Then
                ListRateManualDetailSurgical = New List(Of RateManualDetailSurgical)
            Else
                If INDlyItemSurgicalGroupId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    Dim cont As Integer = ListRateManualDetailSurgical.FindAll(Function(item) item.IPSServiceId = IPSServiceIdSurgical AndAlso item.SurgicalGroupId = SurgicalGroupId).ToList().Count
                    If cont > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ServiceAdded", NAME_MODULE)
                        INDsleIPSServiceIdSurgicalServices.Focus()
                        Exit Sub
                    End If
                Else
                    Dim cont As Integer = ListRateManualDetailSurgical.FindAll(Function(item) item.IPSServiceId = IPSServiceIdSurgical AndAlso item.UVRRangeId = UVRRangeId).ToList().Count
                    If cont > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ServiceAdded", NAME_MODULE)
                        INDsleIPSServiceIdSurgicalServices.Focus()
                        Exit Sub
                    End If
                End If
            End If

            Dim rateManualDetailSurgical As New RateManualDetailSurgical
            With rateManualDetailSurgical
                .IPSServiceId = IPSServiceIdSurgical
                .IPSServiceDescription = INDsleIPSServiceIdSurgicalServices.Text
                .SalesValue = SalesValueSurgical
                .SalesValueWithSurcharge = SalesValueWithSurchargeSurgical

                If INDlyItemSurgicalGroupId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .SurgicalGroupId = SurgicalGroupId
                    .SurgicalGroupDescription = INDsleSurgicalGroupId.Text
                Else
                    .SurgicalGroupId = Nothing
                    .SurgicalGroupDescription = String.Empty
                End If

                If INDlyItemUVRRangeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .UVRRangeId = UVRRangeId
                    .UVRRangeDescription = INDsleUVRRangeId.Text
                Else
                    .UVRRangeId = Nothing
                    .UVRRangeDescription = String.Empty
                End If

            End With

            ListRateManualDetailSurgical.Add(rateManualDetailSurgical)
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ServiceAddedSactisfactory", NAME_MODULE)
        Else
            If INDlyItemSurgicalGroupId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If _listCompareRateManualDetailSurgical.Count > 0 Then
                    Dim cont As Integer = _listCompareRateManualDetailSurgical.FindAll(Function(item) item.IPSServiceId = IPSServiceIdSurgical AndAlso (Not item.SurgicalGroupId.HasValue OrElse item.SurgicalGroupId = SurgicalGroupId)).ToList().Count
                    If cont > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ServiceAdded", NAME_MODULE)
                        INDsleIPSServiceIdSurgicalServices.Focus()
                        Exit Sub
                    End If
                End If
            Else
                If _listCompareRateManualDetailSurgical.Count > 0 Then
                    Dim cont As Integer = _listCompareRateManualDetailSurgical.FindAll(Function(item) item.Id <> _rateManualDetailSurgical.Id AndAlso item.IPSServiceId = IPSServiceIdSurgical AndAlso (Not item.UVRRangeId.HasValue OrElse item.UVRRangeId = UVRRangeId)).ToList().Count
                    If cont > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ServiceAdded", NAME_MODULE)
                        INDsleIPSServiceIdSurgicalServices.Focus()
                        Exit Sub
                    End If
                End If
            End If

            With _rateManualDetailSurgical
                .IPSServiceId = IPSServiceIdSurgical
                .IPSServiceDescription = INDsleIPSServiceIdSurgicalServices.Text
                .SalesValue = SalesValueSurgical
                .SalesValueWithSurcharge = SalesValueWithSurchargeSurgical

                If INDlyItemSurgicalGroupId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .SurgicalGroupId = SurgicalGroupId
                    .SurgicalGroupDescription = INDsleSurgicalGroupId.Text
                Else
                    .SurgicalGroupId = Nothing
                    .SurgicalGroupDescription = String.Empty
                End If

                If INDlyItemUVRRangeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .UVRRangeId = UVRRangeId
                    .UVRRangeDescription = INDsleUVRRangeId.Text
                Else
                    .UVRRangeId = Nothing
                    .UVRRangeDescription = String.Empty
                End If

            End With
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ServiceModified", NAME_MODULE)
        End If


        INDgcSurgicalServices.DataSource = Nothing
        INDgcSurgicalServices.DataSource = ListRateManualDetailSurgical
        INDsleType.Properties.ReadOnly = True
        modeModifySurgical = False
        CleanControlsPopupSurgical()
        INDsleIPSServiceIdSurgicalServices.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que agrega un servicio a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddService()
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            INDsleIPSServiceId.Focus()
            Exit Sub
        End If

        If Not modeModify Then
            If ListRateManualDetail Is Nothing Then
                ListRateManualDetail = New List(Of RateManualDetail)
            Else
                Dim cont As Integer = ListRateManualDetail.FindAll(Function(item) item.IPSServiceId = IPSServiceId).ToList().Count
                If cont > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ServiceAdded", NAME_MODULE)
                    INDsleIPSServiceId.Focus()
                    Exit Sub
                End If
            End If

            Dim rateManualDetail As New RateManualDetail
            With rateManualDetail
                .IPSServiceId = IPSServiceId
                .IPSServiceDescription = INDsleIPSServiceId.Text
                .SalesValue = SalesValue
                .SalesValueWithSurcharge = SalesValueWithSurcharge
            End With

            ListRateManualDetail.Add(rateManualDetail)
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ServiceAddedSactisfactory", NAME_MODULE)
        Else
            If _listCompareRateManualDetail.Count > 0 Then
                Dim cont As Integer = _listCompareRateManualDetail.FindAll(Function(item) item = IPSServiceId).ToList().Count
                If cont > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ServiceAdded", NAME_MODULE)
                    INDsleIPSServiceId.Focus()
                    Exit Sub
                End If
            End If

            With _rateManualDetail
                .IPSServiceId = IPSServiceId
                .IPSServiceDescription = INDsleIPSServiceId.Text
                .SalesValue = SalesValue
                .SalesValueWithSurcharge = SalesValueWithSurcharge
            End With
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ServiceModified", NAME_MODULE)
        End If


        INDgcServices.DataSource = Nothing
        INDgcServices.DataSource = ListRateManualDetail
        modeModify = False
        CleanControlsPopup()
        INDsleIPSServiceId.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que edita un servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub EditService()
        modeModify = True
        _rateManualDetail = viewGridServices.GetFocusedRow()
        Dim companySetting = Await _companySettings

        With _rateManualDetail
            IPSServiceId = .IPSServiceId
            INDsleIPSServiceId.Properties.NullText = .IPSServiceDescription
            SalesValue = .SalesValue
            SalesValueWithSurcharge = .SalesValueWithSurcharge
        End With

        _listCompareRateManualDetail = (From e In ListRateManualDetail Where e.IPSServiceId <> _rateManualDetail.IPSServiceId Select e.IPSServiceId).ToList()
        INDpceServices.ShowPopup()
        INDsleIPSServiceId.Focus()
    End Sub
    ''' <summary>
    ''' Metodo que elimina un servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteService()
        _rateManualDetail = viewGridServices.GetFocusedRow
        ListRateManualDetail.Remove(_rateManualDetail)

        If _rateManualDetail.Id > 0 Then
            If ListDeleteRateManualDetail Is Nothing Then
                ListDeleteRateManualDetail = New List(Of RateManualDetail)
            End If
            _rateManualDetail.MarkAsDeleted()
            ListDeleteRateManualDetail.Add(_rateManualDetail)
        End If

        INDgcServices.DataSource = Nothing
        INDgcServices.DataSource = ListRateManualDetail
    End Sub

    ''' <summary>
    ''' Metodo que elimina un servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteServiceSurgical()
        _rateManualDetailSurgical = ViewGridSurgicalServices.GetFocusedRow
        ListRateManualDetailSurgical.Remove(_rateManualDetailSurgical)

        If _rateManualDetailSurgical.Id > 0 Then
            If ListDeleteRateManualDetailSurgical Is Nothing Then
                ListDeleteRateManualDetailSurgical = New List(Of RateManualDetailSurgical)
            End If
            _rateManualDetailSurgical.MarkAsDeleted()
            ListDeleteRateManualDetailSurgical.Add(_rateManualDetailSurgical)
        End If

        INDgcSurgicalServices.DataSource = Nothing
        INDgcSurgicalServices.DataSource = ListRateManualDetailSurgical

        If ListRateManualDetailSurgical.Count = 0 Then
            INDsleType.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que edita un servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditServiceSurgical()
        modeModifySurgical = True
        _rateManualDetailSurgical = ViewGridSurgicalServices.GetFocusedRow
        With _rateManualDetailSurgical
            IPSServiceIdSurgical = .IPSServiceId
            INDsleIPSServiceIdSurgicalServices.Properties.NullText = .IPSServiceDescription
            'DiscountPercentageSurgical = .DiscountPercentage
            'OutPatientRecoveryFeeTypeSurgical = .OutPatientRecoveryFeeType
            'InPatientRecoveryFeeTypeSurgical = .InPatientRecoveryFeeType
            SalesValueSurgical = .SalesValue
            SalesValueWithSurchargeSurgical = .SalesValueWithSurcharge

            If INDlyItemSurgicalGroupId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                SurgicalGroupId = .SurgicalGroupId
                INDsleSurgicalGroupId.Properties.NullText = .SurgicalGroupDescription

                UVRRangeId = Nothing
                INDsleUVRRangeId.Properties.NullText = String.Empty

                _listCompareRateManualDetailSurgical = ListRateManualDetailSurgical.FindAll(Function(item) item.IPSServiceId = _rateManualDetailSurgical.IPSServiceId AndAlso item.SurgicalGroupId <> _rateManualDetailSurgical.SurgicalGroupId).ToList
            Else
                SurgicalGroupId = Nothing
                INDsleSurgicalGroupId.Properties.NullText = String.Empty

                UVRRangeId = .UVRRangeId
                INDsleUVRRangeId.Properties.NullText = .UVRRangeDescription

                _listCompareRateManualDetailSurgical = ListRateManualDetailSurgical.FindAll(Function(item) item.IPSServiceId = _rateManualDetailSurgical.IPSServiceId AndAlso (Type = 4 OrElse item.UVRRangeId <> _rateManualDetailSurgical.UVRRangeId)).ToList
            End If

        End With
        INDpceSurgicalServices.ShowPopup()
        INDsleIPSServiceIdSurgicalServices.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        INDsleIPSServiceId.Properties.NullText = String.Empty
        INDsleIPSServiceId.EditValue = Nothing
        GridView3.FocusedRowHandle = 0
        SalesValue = 0
        SalesValueWithSurcharge = 0
        INDtxtSalesSubtotal.EditValue = 0
        INDtxtSalesValueIVA.EditValue = 0
        INDlyItemSubtotal.HideLayout()
        INDlyItemValueIVA.HideLayout()
        modeModify = False
        _rateManualDetail = Nothing
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupSurgical()
        IPSServiceIdSurgical = Nothing
        INDsleIPSServiceIdSurgicalServices.Properties.NullText = String.Empty
        INDsleSurgicalGroupId.Properties.NullText = String.Empty
        INDsleUVRRangeId.Properties.NullText = String.Empty
        SalesValueSurgical = 0
        SalesValueWithSurchargeSurgical = 0
        SurgicalGroupId = Nothing
        UVRRangeId = Nothing
        _listCompareRateManualDetailSurgical = Nothing
    End Sub

    ''' <summary>
    ''' Metodo que valida los valores de los controles del popup de servicios
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If IPSServiceId = Nothing Then
            errors.AppendLine(ResourceManager.GetString("SelectedIPSService", NAME_MODULE))
        End If
        'If SalesValue = 0 Then
        '    errors.AppendLine(ResourceManager.GetString("SelectSalesValue", NAME_MODULE))
        'End If
        'If SalesValueWithSurcharge = 0 Then
        '    errors.AppendLine(ResourceManager.GetString("SelectSalesValueWithSurcharge", NAME_MODULE))
        'End If
        Return errors.ToString
    End Function

    ''' <summary>
    ''' Metodo que valida los valores de los controles del popup de servicios
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopupSurgical() As String
        Dim errors As New StringBuilder
        If IPSServiceIdSurgical = Nothing Then
            errors.AppendLine(ResourceManager.GetString("SelectedIPSService", NAME_MODULE))
        End If
        If SalesValueSurgical = 0 Then
            errors.AppendLine(ResourceManager.GetString("SelectSalesValue", NAME_MODULE))
        End If
        If SalesValueWithSurchargeSurgical = 0 Then
            errors.AppendLine(ResourceManager.GetString("SelectSalesValueWithSurcharge", NAME_MODULE))
        End If
        If INDlyItemSurgicalGroupId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If SurgicalGroupId Is Nothing Then
                errors.AppendLine(ResourceManager.GetString("SelectSurgicalGroup", NAME_MODULE))
            End If
        End If
        If INDlyItemUVRRangeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If UVRRangeId Is Nothing Then
                errors.AppendLine(ResourceManager.GetString("SelectUVRRange", NAME_MODULE))
            End If
        End If
        Return errors.ToString
    End Function

    ''' <summary>
    ''' Llena el control de tipo de entidad
    ''' </summary>
    Private Sub InitializeSearch()
        ListType = New List(Of Tuple(Of Integer, String))
        ListType.Add(New Tuple(Of Integer, String)(1, "ISS 2001"))
        ListType.Add(New Tuple(Of Integer, String)(2, "ISS 2004"))
        ListType.Add(New Tuple(Of Integer, String)(3, "SOAT"))
        ListType.Add(New Tuple(Of Integer, String)(4, "Institucional"))
        INDsleType.Properties.DataSource = ListType.ToList

        ListRound = New List(Of Tuple(Of Integer, String))
        ListRound.Add(New Tuple(Of Integer, String)(0, "Sin redondeo"))
        ListRound.Add(New Tuple(Of Integer, String)(1, "Peso"))
        ListRound.Add(New Tuple(Of Integer, String)(10, "Décima"))
        ListRound.Add(New Tuple(Of Integer, String)(100, "Centésima"))
        ListRound.Add(New Tuple(Of Integer, String)(1000, "Unidades de Mil"))
        INDsleRoundService.Properties.DataSource = ListRound.ToList

        ListTypeIntervention = New List(Of Tuple(Of Integer, String))
        ListTypeIntervention.Add(New Tuple(Of Integer, String)(2, "Bilateral"))
        ListTypeIntervention.Add(New Tuple(Of Integer, String)(3, "MIVIE (Multiple Igual Via Igual Especialista)"))
        ListTypeIntervention.Add(New Tuple(Of Integer, String)(4, "MDVIE (Multiple Diferente Via Igual Especialista)"))
        ListTypeIntervention.Add(New Tuple(Of Integer, String)(5, "MIVDE (Multiple Igual Via Diferente Especialista)"))
        ListTypeIntervention.Add(New Tuple(Of Integer, String)(6, "MDVDE (Multiple Diferente Via Diferente Especialista)"))
        ListTypeIntervention.Add(New Tuple(Of Integer, String)(7, "PolitraumaIV (Politrauma Igual Via)"))
        ListTypeIntervention.Add(New Tuple(Of Integer, String)(8, "PolitraumaDV (Politrauma Diferente Via)"))
        INDsleRepTypeIntervention.DataSource = ListTypeIntervention.ToList

    End Sub

    ''' <summary>
    ''' Establece el datasource de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeGrid()
        If ListSurgeryPercentage Is Nothing Then
            ListSurgeryPercentage = New List(Of SurgeriesPercentageManual)
            For i = 2 To 8
                Dim surgeryPercentage As New SurgeriesPercentageManual
                surgeryPercentage.InterventionType = i
                ListSurgeryPercentage.Add(surgeryPercentage)
            Next
        End If
        INDgcSurgeryPercentage.DataSource = Nothing
        INDgcSurgeryPercentage.DataSource = ListSurgeryPercentage
    End Sub

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRateManual.ActionsOnControls
        Set(value As Boolean)
            INDlyRateManual.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleType.Enabled = value
            INDsePercentageNoBloodyRoom.Enabled = value
            'INDsleMaterialNoBloodyIPSServiceId.Enabled = value
            INDsleRoundService.Enabled = value
            INDsleContractMinimumWageId.Enabled = value
            INDgcSurgeryPercentage.Enabled = value
            'INDpceServices.Enabled = value
            INDgcServices.Enabled = value
            'INDpceSurgicalServices.Enabled = value
            INDgcSurgicalServices.Enabled = value
            INDlyRateManual.EndUpdate()
            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.rateManual IsNot Nothing AndAlso Me.rateManual.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
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
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.rateManual.Code, Me.rateManual.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.rateManual.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.rateManual.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.rateManual.Code, Me.rateManual.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.rateManual.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyRateManual.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        NameRM = String.Empty
        Type = Nothing
        PercentageNoBloodyRoom = Nothing
        MaterialNoBloodyIPSServiceId = Nothing
        INDsleMaterialNoBloodyIPSServiceId.Properties.NullText = String.Empty
        MaterialNoBloodyIPSServiceIdXpo = Nothing
        INDpceServices.Enabled = False
        INDpceSurgicalServices.Enabled = False
        INDsleMaterialNoBloodyIPSServiceId.Enabled = False
        RoundService = Nothing
        ContractMinimumWageId = Nothing
        INDsleContractMinimumWageId.Properties.NullText = String.Empty
        ListSurgeryPercentage = Nothing
        INDgcSurgeryPercentage.DataSource = Nothing
        ListRateManualDetail = Nothing
        ListDeleteRateManualDetail = Nothing
        INDgcServices.DataSource = Nothing
        ListRateManualDetailSurgical = Nothing
        ListDeleteRateManualDetailSurgical = Nothing
        INDgcSurgicalServices.DataSource = Nothing
        _listCompareRateManualDetail = Nothing
        _listCompareRateManualDetailSurgical = Nothing
        CleanControlsPopup()
        CleanControlsPopupSurgical()
        Status = True
        modeModify = False
        INDlyItemContractMinimumWageId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemContractMinimumWageId.AllowHide = True
        INDlygBloodyInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemMaterialNoBloodyIPSServiceId.AllowHide = True
        INDlyItemPercentageNoBloodyRoom.AllowHide = True
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyRateManual.EndUpdate()
        LiquidateAllMIVIE = Nothing
        INDLycLiquidateAllMIVIE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        rateManual = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDsleType.Properties.ReadOnly = False
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With rateManual
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameRM
            .Type = Type
            .LiquidateAllMIVIE = LiquidateAllMIVIE

            If INDlygBloodyInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .PercentageNoBloodyRoom = PercentageNoBloodyRoom
                .MaterialNoBloodyIPSServiceId = MaterialNoBloodyIPSServiceId
            Else
                .PercentageNoBloodyRoom = Nothing
                .MaterialNoBloodyIPSServiceId = Nothing
            End If

            .RoundService = RoundService
            If INDlyItemContractMinimumWageId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ContractMinimumWageId = ContractMinimumWageId
            Else
                .ContractMinimumWageId = Nothing
            End If
            If ListSurgeryPercentage IsNot Nothing Then
                For Each item As SurgeriesPercentageManual In ListSurgeryPercentage
                    .SurgeriesPercentageManual.Add(item)
                Next
            End If

            If ListRateManualDetail IsNot Nothing AndAlso ListRateManualDetail.Count > 0 Then
                For Each item As RateManualDetail In ListRateManualDetail
                    .RateManualDetail.Add(item)
                Next
            End If

            If ListDeleteRateManualDetail IsNot Nothing Then
                For Each itemDelete As RateManualDetail In ListDeleteRateManualDetail
                    .RateManualDetail.Add(itemDelete)
                Next
            End If

            If ListRateManualDetailSurgical IsNot Nothing AndAlso ListRateManualDetailSurgical.Count > 0 Then
                For Each item As RateManualDetailSurgical In ListRateManualDetailSurgical
                    .RateManualDetailSurgical.Add(item)
                Next
            End If

            If ListDeleteRateManualDetailSurgical IsNot Nothing Then
                For Each itemDelete As RateManualDetailSurgical In ListDeleteRateManualDetailSurgical
                    .RateManualDetailSurgical.Add(itemDelete)
                Next
            End If
        End With
        If rateManual.Id > 0 Then
            rateManual.MarkAsModified()
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
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
                Using Model As New MRateManual(CStr(Me.Tag))
                    AsyncLoader(True)
                    rateManual = (Await Model.GetRateManual(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    INDlyRateManual.BeginUpdate()
                    If rateManual IsNot Nothing AndAlso rateManual.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(rateManual.Id))
                            With rateManual
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                NameRM = .Name
                                modifiedType = True
                                Type = .Type
                                modifiedType = False
                                LiquidateAllMIVIE = .LiquidateAllMIVIE

                                INDsleMaterialNoBloodyIPSServiceId.Properties.NullText = .MaterialNoBloodyIPSServiceDescription
                                MaterialNoBloodyIPSServiceId = .MaterialNoBloodyIPSServiceId
                                PercentageNoBloodyRoom = .PercentageNoBloodyRoom

                                RoundService = .RoundService
                                INDsleContractMinimumWageId.Properties.NullText = .ContractMinimumWageDescription
                                ContractMinimumWageId = .ContractMinimumWageId
                                Status = .Status

                                INDgcSurgeryPercentage.DataSource = Nothing
                                If .SurgeriesPercentageManual IsNot Nothing AndAlso .SurgeriesPercentageManual.Count > 0 Then 'Si ya tiene registros creados se llena el listado directamente
                                    ListSurgeryPercentage = .SurgeriesPercentageManual.ToList
                                Else 'Sino se llena el listado para que guarde de cero
                                    InitializeGrid()
                                End If
                                INDgcSurgeryPercentage.DataSource = ListSurgeryPercentage

                                ListRateManualDetail = .RateManualDetail.ToList
                                INDgcServices.DataSource = Nothing
                                INDgcServices.DataSource = ListRateManualDetail

                                ListRateManualDetailSurgical = .RateManualDetailSurgical.ToList
                                INDgcSurgicalServices.DataSource = Nothing
                                INDgcSurgicalServices.DataSource = ListRateManualDetailSurgical
                                If ListRateManualDetailSurgical.Count > 0 Then
                                    INDsleType.Properties.ReadOnly = True
                                End If
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.rateManual.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = rateManual.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(rateManual.Id, Me.Tag.ToString(), Nothing, GetType(RateManual).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewRateManual()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyRateManual.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If

    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewRateManual() As Task
        rateManual = New RateManual() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.ContractSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.ContractSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
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
            InitializeGrid()
        End If


        'rateManual = New RateManual()
        'If Me._sequence.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequence = Me._sequence.ContractSequenceDetail(0).Id
        '    ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me.Sequense.ContractSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequence = Me._sequence.ContractSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Function
        '        End If
        '    End If
        '    If Not Me._sequence.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                    Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    Else
        '        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        '    InitializeGrid()
        'End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MRateManual(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not rateManual.Status
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")

                    Me.rateManual = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If

                    INDbtnCode.Enabled = False
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Metodo que oculta los campos de la rejilla de servicios quirurgicos segun corresponda
    ''' </summary>
    ''' <param name="optionSurgical"></param>
    ''' <param name="optionUVR"></param>
    ''' <remarks></remarks>
    Private Sub HideColumnsGridSurgicalServices(ByVal optionSurgical As Boolean, ByVal optionUVR As Boolean)
        ViewGridSurgicalServices.Columns.ColumnByFieldName("UVRRangeDescription").Visible = optionUVR
        ViewGridSurgicalServices.Columns.ColumnByFieldName("SurgicalGroupDescription").Visible = optionSurgical
        If optionSurgical = True OrElse optionUVR = True Then
            Dim cont As Integer = 0
            For i = 0 To ViewGridSurgicalServices.Columns.Count - 1
                If ViewGridSurgicalServices.Columns.Item(i).Visible = True AndAlso ViewGridSurgicalServices.Columns.Item(i).GroupIndex < 0 Then
                    ViewGridSurgicalServices.Columns.Item(i).VisibleIndex = cont
                    cont += 1
                End If
            Next
        End If
    End Sub

    Private Sub getCompanySettings()
        Using model As New MCompanySettings(Tag)
            _companySettings = model.GetCompanySettings()
        End Using
    End Sub
    ''' <summary>
    ''' Me calcula el valor del IVA del servico si tiene
    ''' </summary>
    ''' <param name="ContractIPS"></param>
    ''' <remarks></remarks>
    Private Async Sub CalculatePercentageIVA(ContractIPS As ContractIPSServiceXPO)
        Dim companySetting = Await _companySettings
        Dim salesValue As Decimal = INDtxtSalesValue.EditValue

        If companySetting.SalePriceIncludeTax Then
            INDlyItemSubtotal.Text = "Subtotal"
            INDlyItemValueIVA.Text = "Valor IVA"
            Dim subTotal As Decimal = 0

            If ContractIPS IsNot Nothing Then
                subTotal = salesValue / (1 + (ContractIPS.IVA.Percentage / 100))
            End If

            INDtxtSalesSubtotal.EditValue = subTotal
            INDtxtSalesValueIVA.EditValue = salesValue - subTotal
        Else
            INDlyItemSubtotal.Text = "Valor IVA"
            INDlyItemValueIVA.Text = "Valor Total"

            Dim ivaValue As Decimal = 0

            If ContractIPS IsNot Nothing Then
                ivaValue = salesValue * (ContractIPS.IVA.Percentage / 100)
            End If

            INDtxtSalesSubtotal.EditValue = ivaValue
            INDtxtSalesValueIVA.EditValue = salesValue + ivaValue
        End If

        If ContractIPS.IVA.Percentage = 0 Then
            INDtxtSalesSubtotal.EditValue = 0
            INDtxtSalesValueIVA.EditValue = 0

            INDlyItemSubtotal.HideLayout()
            INDlyItemValueIVA.HideLayout()
        Else
            INDlyItemSubtotal.ShowLayout()
            INDlyItemValueIVA.ShowLayout()
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _sequence = Nothing
        rateManual = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
        ListSurgeryPercentage = Nothing
        ListType = Nothing
        ListRound = Nothing
        ListTypeIntervention = Nothing
        modifiedType = Nothing
        banSave = Nothing
        ListRateManualDetail = Nothing
        ListRateManualDetailSurgical = Nothing
        ListDeleteRateManualDetail = Nothing
        ListDeleteRateManualDetailSurgical = Nothing
        _listCompareRateManualDetail = Nothing
        _listCompareRateManualDetailSurgical = Nothing
        modeModify = Nothing
        modeModifySurgical = Nothing
        _rateManualDetail = Nothing
        _rateManualDetailSurgical = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRateManual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRateManual, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PRateManual(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()
        InitializeSearch()
        getCompanySettings()

        INDsleType.Properties.Buttons.Item(1).Visible = False
        INDsleRoundService.Properties.Buttons.Item(1).Visible = False

        IndigoGridControl1.RefreshGrid(INDgcSurgeryPercentage)
        IndigoGridControl1.RefreshGrid(INDgcServices)
        IndigoGridControl1.RefreshGrid(INDgcSurgicalServices)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(viewGridServices, ListActions)
        IndigoGridView2.SetListAcction(ViewGridSurgicalServices, ListActions)

        ''Localizacion de elementos-PopupCntainers del formulario segun la moneda
        FormatNumber = Me.indigo.CurrencyNumbertFormat
        changeNumericFormatByCurrency(FormatNumber)
        changeNumericFormatByCurrency(FormatNumber, INDlyPopup.Controls)
        changeNumericFormatByCurrency(FormatNumber, INDlyPopupSurgicalServices.Controls)
    End Sub


#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRateManual_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewRateManual()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 al popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceServices_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceServices.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceServices.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 sobre el control de popup de servicios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceSurgicalServices_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceSurgicalServices.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceSurgicalServices.ShowPopup()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de salario minimo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContractMinimumWageId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleContractMinimumWageId.QueryPopUp
        If INDsleContractMinimumWageId.Properties.DataSource Is Nothing Then
            Presenter.InitializeContractMinimumWage()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se disparas al desplegar el control de servicio ips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIPSServiceId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIPSServiceId.QueryPopUp
        If INDsleIPSServiceId.Properties.DataSource Is Nothing Then
            'Presenter.InitializeIPSServices()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de servicio ips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIPSServiceIdSurgicalServices_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIPSServiceIdSurgicalServices.QueryPopUp
        If INDsleIPSServiceIdSurgicalServices.Properties.DataSource Is Nothing Then
            'Presenter.InitializeIPSServicesSurgical()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de servicio ips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMaterialNoBloodyIPSServiceId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMaterialNoBloodyIPSServiceId.QueryPopUp
        If MaterialNoBloodyIPSServiceIdXpo Is Nothing Then
            'Presenter.InitializeMaterialNoBloodyIPSService(Type)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de grupo quirurgico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSurgicalGroupId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSurgicalGroupId.QueryPopUp
        If INDsleSurgicalGroupId.Properties.DataSource Is Nothing Then
            Presenter.InitializeSurgicalGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de rango uvr
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleUVRRangeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUVRRangeId.QueryPopUp
        If INDsleUVRRangeId.Properties.DataSource Is Nothing Then
            Presenter.InitializeUVRRange()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del control de salario minimo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContractMinimumWageId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleContractMinimumWageId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmContractMinimumWage With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeContractMinimumWage()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del control de servicio ips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIPSServiceId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIPSServiceId.ButtonClick, INDsleIPSServiceIdSurgicalServices.ButtonClick, INDsleMaterialNoBloodyIPSServiceId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmIPSService With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.Initialize(Type)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del control de grupo quirurgico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSurgicalGroupId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSurgicalGroupId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmSurgicalGroup With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeSurgicalGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del control de rango uvr
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleUVRRangeId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleUVRRangeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmUVRRange With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeUVRRange()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If Type IsNot Nothing Then
            INDEsbBills.AddRangeColumns("Código Servicio IPS", "Valor Servicio", "Valor Recargo")

            If Type = 1 OrElse Type = 2 Then
                INDlyItemSurgicalGroupId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemUVRRangeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemContractMinimumWageId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemContractMinimumWageId.AllowHide = True
                HideColumnsGridSurgicalServices(False, True)
                INDLycLiquidateAllMIVIE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                INDlygBloodyInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemMaterialNoBloodyIPSServiceId.AllowHide = True
                INDlyItemPercentageNoBloodyRoom.AllowHide = True
                INDEsbBillsSurgical.AddRangeColumns("Código Servicio IPS", "Valor Servicio", "Valor Recargo", "Código Rango UVR")
            ElseIf Type = 4 Then
                INDlyItemUVRRangeId.HideLayout()
                INDlygBloodyInformation.HideControl()
                INDlyItemContractMinimumWageId.HideLayout()
                INDLycLiquidateAllMIVIE.ShowLayout()

                INDlyItemSurgicalGroupId.HideLayout()
                INDlyItemUVRRangeId.HideLayout()
                INDEsbBillsSurgical.AddRangeColumns("Código Servicio IPS", "Valor Servicio", "Valor Recargo")
            Else
                INDlyItemSurgicalGroupId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemUVRRangeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemContractMinimumWageId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemContractMinimumWageId.AllowHide = False
                HideColumnsGridSurgicalServices(True, False)
                INDLycLiquidateAllMIVIE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDlygBloodyInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemMaterialNoBloodyIPSServiceId.AllowHide = False
                INDlyItemPercentageNoBloodyRoom.AllowHide = False
                INDEsbBillsSurgical.AddRangeColumns("Código Servicio IPS", "Valor Servicio", "Valor Recargo", "Código Grupo Quirúrgico")
            End If

            Presenter.Initialize(Type)
            INDpceSurgicalServices.Enabled = True
            INDpceServices.Enabled = True
            INDEsbBills.Enabled = True
            INDEsbBillsSurgical.Enabled = True
            INDsleMaterialNoBloodyIPSServiceId.Enabled = True
        Else
            INDlyItemSurgicalGroupId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemUVRRangeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemContractMinimumWageId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemContractMinimumWageId.AllowHide = True
            HideColumnsGridSurgicalServices(False, False)
            INDpceSurgicalServices.Enabled = False
            INDpceServices.Enabled = False
            INDsleMaterialNoBloodyIPSServiceId.Enabled = False
            INDEsbBills.Enabled = False
            INDEsbBillsSurgical.Enabled = False

            INDlygBloodyInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemMaterialNoBloodyIPSServiceId.AllowHide = True
            INDlyItemPercentageNoBloodyRoom.AllowHide = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de ipsService del popup de surgical
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIPSServiceIdSurgicalServices_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleIPSServiceIdSurgicalServices.EditValueChanged
        If IPSServiceIdSurgical <> Nothing Then
            If _listCompareRateManualDetailSurgical IsNot Nothing Then
                If IPSServiceIdSurgical <> _rateManualDetailSurgical.IPSServiceId Then
                    If INDlyItemSurgicalGroupId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        _listCompareRateManualDetailSurgical = ListRateManualDetailSurgical.FindAll(Function(item) item.IPSServiceId <> _rateManualDetailSurgical.IPSServiceId AndAlso (Not item.SurgicalGroupId.HasValue OrElse item.SurgicalGroupId = _rateManualDetailSurgical.SurgicalGroupId)).ToList
                    Else
                        _listCompareRateManualDetailSurgical = ListRateManualDetailSurgical.FindAll(Function(item) item.IPSServiceId <> _rateManualDetailSurgical.IPSServiceId AndAlso (Not item.UVRRangeId.HasValue OrElse item.UVRRangeId = _rateManualDetailSurgical.UVRRangeId)).ToList
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de ipsService del popup de Servicios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIPSServiceId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleIPSServiceId.EditValueChanged
        Dim ips = ServiceIPSSelected
        If ips IsNot Nothing Then
            If ips.IVA?.Percentage > 0 AndAlso ips.TaxedProduct Then
                CalculatePercentageIVA(ips)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de ipsService del popup de servicios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtSalesValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtSalesValue.EditValueChanged
        Dim ips = ServiceIPSSelected

        If ips IsNot Nothing Then
            If ips.IVA?.Percentage > 0 AndAlso ips.TaxedProduct Then
                CalculatePercentageIVA(ips)
            End If
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar servicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddService_Click(sender As Object, e As EventArgs) Handles INDbtnAddService.Click
        AddService()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar servicio pero del grupo quirurgico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddSurgicalServices_Click(sender As Object, e As EventArgs) Handles INDbtnAddSurgicalServices.Click
        AddServiceSurgical()
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceServices_Popup(sender As Object, e As EventArgs) Handles INDpceServices.Popup
        INDsleIPSServiceId.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el popup de servicios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceSurgicalServices_Popup(sender As Object, e As EventArgs) Handles INDpceSurgicalServices.Popup
        INDsleIPSServiceIdSurgicalServices.Focus()
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup de servicios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceServices_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceServices.CloseUp
        If e.CloseMode = PopupCloseMode.Cancel Then
            INDpceSurgicalServices.Focus()
        End If
        CleanControlsPopup()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup de servicios del grupo quirurgico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceSurgicalServices_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceSurgicalServices.CloseUp
        If e.CloseMode = PopupCloseMode.Cancel Then
            INDgcSurgeryPercentage.Focus()
        End If
    End Sub

#End Region

#Region "MenuContextual"

#Region "Services"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre los botones de la columna de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case sender.Tag
            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                DeleteService()
            Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                EditService()
        End Select
    End Sub

#End Region

#Region "ServicesSurgical"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre los botones de la columna de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        Select Case sender.Tag.ToString()
            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                DeleteServiceSurgical()
            Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                EditServiceSurgical()
        End Select
    End Sub

#End Region

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If Type Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe elegir un tipo manual para poder copiar y pegar"
            Exit Sub
        End If
        Await PasteToGrid(sender, e.Rows)
    End Sub

    ''' <summary>
    ''' Metodo que copia y pega los items a la rejilla de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function PasteToGrid(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
        If sender.Name = INDgcServices.Name Then
            viewGridServices.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MRateManual(MyTag)
                Dim result = Await model.CopyAndPasteRateManual(ListInfo, Type)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    viewGridServices.HideLoadingPanel()
                    Exit Function
                End If
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If ListRateManualDetail IsNot Nothing AndAlso ListRateManualDetail.Count > 0 Then
                        Dim listBillsTmp = (From s In result.ObjectEmbbeded Select s.IPSServiceId).ToList.Distinct.ToList()
                        Dim listBillsNotExist As New List(Of String)
                        For i As Integer = 0 To listBillsTmp.Count - 1 Step 1
                            Dim share = ListRateManualDetail.Where(Function(x) x.IPSServiceId = listBillsTmp.Item(i)).FirstOrDefault()
                            If share IsNot Nothing Then
                                If result.ObjectEmbbededAux Is Nothing Then
                                    result.ObjectEmbbededAux = New List(Of Tuple(Of String, Integer))
                                End If
                                result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("El servicio IPS " + share.IPSServiceDescription + " ya existe en la lista", 2))
                            Else
                                listBillsNotExist.Add(listBillsTmp.Item((i)))
                            End If
                        Next
                        For i As Integer = 0 To listBillsNotExist.Count - 1 Step 1
                            ListRateManualDetail.AddRange(result.ObjectEmbbeded.FindAll(Function(x) x.IPSServiceId = listBillsNotExist.Item(i)))
                        Next

                    Else
                        ListRateManualDetail = result.ObjectEmbbeded
                    End If
                End If
                If result.ObjectEmbbededAux IsNot Nothing AndAlso result.ObjectEmbbededAux.Count > 0 Then
                    Using formulario As New FrmListErrors(result.ObjectEmbbededAux)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End Using
            INDgcServices.DataSource = Nothing
            INDgcServices.DataSource = ListRateManualDetail
            Me.Cursor = System.Windows.Forms.Cursors.Default
            viewGridServices.HideLoadingPanel()
        ElseIf sender.Name = INDgcSurgicalServices.Name Then
            ViewGridSurgicalServices.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MRateManual(MyTag)
                Dim result = Await model.CopyAndPasteRateManualSurgical(ListInfo, Type)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    ViewGridSurgicalServices.HideLoadingPanel()
                    Exit Function
                End If
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If ListRateManualDetailSurgical IsNot Nothing AndAlso ListRateManualDetailSurgical.Count > 0 Then
                        Dim listBillsTmp = (From s In result.ObjectEmbbeded Select s.IPSServiceId).ToList.Distinct.ToList()
                        Dim listBillsNotExist As New List(Of String)
                        For i As Integer = 0 To listBillsTmp.Count - 1 Step 1
                            Dim share = ListRateManualDetailSurgical.Where(Function(x) x.IPSServiceId = listBillsTmp.Item(i)).FirstOrDefault()
                            If share IsNot Nothing Then
                                If result.ObjectEmbbededAux Is Nothing Then
                                    result.ObjectEmbbededAux = New List(Of Tuple(Of String, Integer))
                                End If
                                result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("El servicio IPS " + share.IPSServiceDescription + " ya existe en la lista", 2))
                            Else
                                listBillsNotExist.Add(listBillsTmp.Item((i)))
                            End If
                        Next
                        For i As Integer = 0 To listBillsNotExist.Count - 1 Step 1
                            ListRateManualDetailSurgical.AddRange(result.ObjectEmbbeded.FindAll(Function(x) x.IPSServiceId = listBillsNotExist.Item(i)))
                        Next

                    Else
                        ListRateManualDetailSurgical = result.ObjectEmbbeded
                    End If
                End If
                If result.ObjectEmbbededAux IsNot Nothing AndAlso result.ObjectEmbbededAux.Count > 0 Then
                    Using formulario As New FrmListErrors(result.ObjectEmbbededAux)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End Using
            INDgcSurgicalServices.DataSource = Nothing
            INDgcSurgicalServices.DataSource = ListRateManualDetailSurgical
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ViewGridSurgicalServices.HideLoadingPanel()
        End If
    End Function

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
        banSave = False
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
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
        banSave = True
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.ContractSequenceDetail IsNot Nothing Then
                If Not Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class