'***********************************************************************
' Assembly         : Presentacion.Common
' Author           : Jose Luis Rojas 
' Created          : 10-04-2011
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Common.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Accounting.MVP

#End Region

''' <summary>
''' Contiene el comportamiento de la vista del frontal Paises
''' </summary>
Public Class FrmCountry
    Implements ICountry, ICustomizableForm

#Region "Globals & Properties"

    Public Const NAME_MODULE As String = "Commons"

    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    Private Property ModoBusqueda As Boolean

    Dim TStandardCode As New List(Of Tuple(Of String, String))()

    ''' <summary>
    ''' Propiedad que contiene el estado de lo controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICountry.ActionsOnControls
        Set(value As Boolean)
            INDlyCountry.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDtxtNationality.Enabled = value
            INDSleStandardCode.Enabled = value

            INDlyCountry.EndUpdate()

            If value = True Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Entidad de paises
    ''' </summary>
    Dim Country As Country

    ''' <summary>
    ''' Variable para acceder al presentador
    ''' </summary>
    Dim Presenter As PCountry

    Private _idCurrentSequence As Long

    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Variable de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable para controlar el registro bloqueado en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim record As BlockRecord

    ''' <summary>
    ''' Propiedad que establece el codigo del pais
    ''' </summary>
    Public Property Code As String Implements ICountry.Code
        Get
            If (INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el nombre del pais
    ''' </summary>
    Public Property CountryName As String Implements ICountry.CountryName
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el nombre del pais
    ''' </summary>
    Public Property Nationality As String Implements ICountry.Nationality
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtNationality.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el estado del pais
    ''' </summary>
    Public Property Status As Boolean Implements ICountry.Status
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property

    Private _sequence As GeneralLedgerSequence
    Public Property Sequence As GeneralLedgerSequence Implements ICountry.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As GeneralLedgerSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.GeneralLedgerSequenceDetail In Me._sequence.GeneralLedgerSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

#End Region

#Region "Events"
    ''' <summary>
    ''' Esta función se ejecuta cuando el formulario (MyBase) se está cerrando y está siendo liberado de la memoria.
    ''' Al igual que en la respuesta anterior, su propósito es realizar la limpieza y liberación de recursos asociados
    ''' con el formulario antes de que se elimine por completo.
    ''' </summary>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        Country = Nothing
        Presenter = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
    End Sub


    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmCountry_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyCountry, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        'Me.Model = New MCountry(Me.Tag)
        Me.Indigo = SessionValues.Instance
        '******************************'
        Me._funct = AddressOf GenerateDoc
        'PathFunctionalDefinitions = String.Concat(Indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloCommonCountry.", Me.Name, ".xml")
        'LoadhronousDefinitions = New BackgroundWorker
        'If LoadhronousDefinitions.IsBusy = False Then
        '    LoadhronousDefinitions.RunWorkerAsync()
        'End If
        LoadStatus()
        Presenter = New PCountry(Me)
        Presenter.GetSequense()
        'Presenter.LoadDefinitionLayout()
        Deshacer()
        INDSleStandardCode.Properties.DataSource = DataSourceStandardCode()
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Su propósito es establecer el enfoque en un control específico del formulario
    ''' en caso de que el contenido de INDbteCode.Text (probablemente un cuadro de texto) esté vacío. 
    ''' </summary>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewCountry()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Country IsNot Nothing AndAlso Me.Country.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Evento para capturar el cierre del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDepartaments_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "ICRUD"

    ''' <summary>
    ''' Abre el formulario de busquedas
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Codigo", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripcion", .FieldName = "Descripcion"}}.ToList()
            .ValorSolicitado = "Codigo"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Country
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text IsNot String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta el metodo de abrirbusqueda
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Deshace los cambios hechos en el formulario
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Elimina el pais seleccionado
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        If Me.Country IsNot Nothing AndAlso Me.Country.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Using model As New MCountry(MCountry.TAG)
                        Dim result = Await model.DeleteCountryAsync(Me.Country)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Guarda el pais
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Using model As New MCountry(MCountry.TAG)
                Dim result As ActionResult(Of Country) = Await model.SaveCountryAsync(Me.Country, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If Country.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.Country = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Indica que el dato ya existe y se va a actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Muestre y escribe el mensaje de retorno por las operaciones
    ''' </summary>
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
    ''' La propiedad MyTag permite
    ''' acceder al valor de la propiedad Tag del formulario o del objeto que
    ''' implementa la interfaz ICountry.
    ''' </summary>
    Public ReadOnly Property MyTag As String Implements ICountry.MyTag
        Get
            Return Me.Tag.ToString()
        End Get
    End Property

    ''' <summary>
    ''' La propiedad MyLayoutControl permite acceder a un objeto de tipo
    ''' IndigoLayoutControl que probablemente se utiliza para administrar
    ''' el diseño y los controles en un formulario.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICountry.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Código estándar numérico
    ''' </summary>
    ''' <returns></returns>
    Public Property StandardCodeNumeric As String Implements ICountry.StandardCodeNumeric
        Get
            Return INDSleStandardCode.EditValue
        End Get
        Set(value As String)
            INDSleStandardCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Código estándar
    ''' </summary>
    Private _standardCode As String
    Private Property StandardCode As String
        Get
            For i As Integer = 0 To TStandardCode?.Count - 1
                If StandardCodeNumeric = TStandardCode(i)?.Item1 Then
                    _standardCode = TStandardCode(i)?.Item2
                    Exit For
                End If
            Next
            Return _standardCode
        End Get
        Set(value As String)
            For i As Integer = 0 To TStandardCode?.Count - 1
                If StandardCodeNumeric = TStandardCode(i)?.Item1 Then
                    value = TStandardCode(i)?.Item2
                    Exit For
                End If
            Next
        End Set
    End Property

    ''' <summary>
    ''' Limpia el formulario para iniciar 
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewCountry()
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmCountryMetaData, Eform.InfoMetaData), Me.Country.Code, Me.Country.Name, Me.Country.Nationality),
                .CreationDate = dateServer, .CreationUser = Me.Indigo.UserIndigo & "-" & Me.Indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.Country.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmCountryMetaDataTitle, Eform.InfoMetaData), Me.Country.Code),
                .Update = dateServer, .UpdateUser = Me.Indigo.UserIndigo & "-" & Me.Indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.Indigo.UserIndigo & "-" & Me.Indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmCountryMetaData, Eform.InfoMetaData), Me.Country.Code, Me.Country.Name, Me.Country.Nationality)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmCountryMetaDataTitle, Eform.InfoMetaData), Me.Country.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga el datasource del control código estándar
    ''' </summary>
    ''' <returns></returns>
    Private Function DataSourceStandardCode()
        TStandardCode = New List(Of Tuple(Of String, String))()
        TStandardCode.Add(New Tuple(Of String, String)("020", "AD"))
        TStandardCode.Add(New Tuple(Of String, String)("784", "AE"))
        TStandardCode.Add(New Tuple(Of String, String)("004", "AF"))
        TStandardCode.Add(New Tuple(Of String, String)("028", "AG"))
        TStandardCode.Add(New Tuple(Of String, String)("660", "AI"))
        TStandardCode.Add(New Tuple(Of String, String)("008", "AL"))
        TStandardCode.Add(New Tuple(Of String, String)("051", "AM"))
        TStandardCode.Add(New Tuple(Of String, String)("530", "AN"))
        TStandardCode.Add(New Tuple(Of String, String)("024", "AO"))
        TStandardCode.Add(New Tuple(Of String, String)("010", "AQ"))
        TStandardCode.Add(New Tuple(Of String, String)("032", "AR"))
        TStandardCode.Add(New Tuple(Of String, String)("016", "AS"))
        TStandardCode.Add(New Tuple(Of String, String)("040", "AT"))
        TStandardCode.Add(New Tuple(Of String, String)("036", "AU"))
        TStandardCode.Add(New Tuple(Of String, String)("533", "AW"))
        TStandardCode.Add(New Tuple(Of String, String)("248", "AX"))
        TStandardCode.Add(New Tuple(Of String, String)("031", "AZ"))
        TStandardCode.Add(New Tuple(Of String, String)("070", "BA"))
        TStandardCode.Add(New Tuple(Of String, String)("052", "BB"))
        TStandardCode.Add(New Tuple(Of String, String)("050", "BD"))
        TStandardCode.Add(New Tuple(Of String, String)("056", "BE"))
        TStandardCode.Add(New Tuple(Of String, String)("854", "BF"))
        TStandardCode.Add(New Tuple(Of String, String)("100", "BG"))
        TStandardCode.Add(New Tuple(Of String, String)("048", "BH"))
        TStandardCode.Add(New Tuple(Of String, String)("108", "BI"))
        TStandardCode.Add(New Tuple(Of String, String)("204", "BJ"))
        TStandardCode.Add(New Tuple(Of String, String)("652", "BL"))
        TStandardCode.Add(New Tuple(Of String, String)("060", "BM"))
        TStandardCode.Add(New Tuple(Of String, String)("096", "BN"))
        TStandardCode.Add(New Tuple(Of String, String)("068", "BO"))
        TStandardCode.Add(New Tuple(Of String, String)("076", "BR"))
        TStandardCode.Add(New Tuple(Of String, String)("044", "BS"))
        TStandardCode.Add(New Tuple(Of String, String)("064", "BT"))
        TStandardCode.Add(New Tuple(Of String, String)("074", "BV"))
        TStandardCode.Add(New Tuple(Of String, String)("072", "BW"))
        TStandardCode.Add(New Tuple(Of String, String)("112", "BY"))
        TStandardCode.Add(New Tuple(Of String, String)("084", "BZ"))
        TStandardCode.Add(New Tuple(Of String, String)("124", "CA"))
        TStandardCode.Add(New Tuple(Of String, String)("166", "CC"))
        TStandardCode.Add(New Tuple(Of String, String)("140", "CF"))
        TStandardCode.Add(New Tuple(Of String, String)("178", "CG"))
        TStandardCode.Add(New Tuple(Of String, String)("756", "CH"))
        TStandardCode.Add(New Tuple(Of String, String)("384", "CI"))
        TStandardCode.Add(New Tuple(Of String, String)("184", "CK"))
        TStandardCode.Add(New Tuple(Of String, String)("152", "CL"))
        TStandardCode.Add(New Tuple(Of String, String)("120", "CM"))
        TStandardCode.Add(New Tuple(Of String, String)("156", "CN"))
        TStandardCode.Add(New Tuple(Of String, String)("170", "CO"))
        TStandardCode.Add(New Tuple(Of String, String)("188", "CR"))
        TStandardCode.Add(New Tuple(Of String, String)("192", "CU"))
        TStandardCode.Add(New Tuple(Of String, String)("132", "CV"))
        TStandardCode.Add(New Tuple(Of String, String)("162", "CX"))
        TStandardCode.Add(New Tuple(Of String, String)("196", "CY"))
        TStandardCode.Add(New Tuple(Of String, String)("203", "CZ"))
        TStandardCode.Add(New Tuple(Of String, String)("276", "DE"))
        TStandardCode.Add(New Tuple(Of String, String)("262", "DJ"))
        TStandardCode.Add(New Tuple(Of String, String)("208", "DK"))
        TStandardCode.Add(New Tuple(Of String, String)("212", "DM"))
        TStandardCode.Add(New Tuple(Of String, String)("214", "DO"))
        TStandardCode.Add(New Tuple(Of String, String)("012", "DZ"))
        TStandardCode.Add(New Tuple(Of String, String)("218", "EC"))
        TStandardCode.Add(New Tuple(Of String, String)("233", "EE"))
        TStandardCode.Add(New Tuple(Of String, String)("818", "EG"))
        TStandardCode.Add(New Tuple(Of String, String)("732", "EH"))
        TStandardCode.Add(New Tuple(Of String, String)("232", "ER"))
        TStandardCode.Add(New Tuple(Of String, String)("724", "ES"))
        TStandardCode.Add(New Tuple(Of String, String)("231", "ET"))
        TStandardCode.Add(New Tuple(Of String, String)("246", "FI"))
        TStandardCode.Add(New Tuple(Of String, String)("242", "FJ"))
        TStandardCode.Add(New Tuple(Of String, String)("238", "FK"))
        TStandardCode.Add(New Tuple(Of String, String)("583", "FM"))
        TStandardCode.Add(New Tuple(Of String, String)("234", "FO"))
        TStandardCode.Add(New Tuple(Of String, String)("250", "FR"))
        TStandardCode.Add(New Tuple(Of String, String)("266", "GA"))
        TStandardCode.Add(New Tuple(Of String, String)("826", "GB"))
        TStandardCode.Add(New Tuple(Of String, String)("308", "GD"))
        TStandardCode.Add(New Tuple(Of String, String)("268", "GE"))
        TStandardCode.Add(New Tuple(Of String, String)("254", "GF"))
        TStandardCode.Add(New Tuple(Of String, String)("831", "GG"))
        TStandardCode.Add(New Tuple(Of String, String)("288", "GH"))
        TStandardCode.Add(New Tuple(Of String, String)("292", "GI"))
        TStandardCode.Add(New Tuple(Of String, String)("304", "GL"))
        TStandardCode.Add(New Tuple(Of String, String)("270", "GM"))
        TStandardCode.Add(New Tuple(Of String, String)("324", "GN"))
        TStandardCode.Add(New Tuple(Of String, String)("312", "GP"))
        TStandardCode.Add(New Tuple(Of String, String)("226", "GQ"))
        TStandardCode.Add(New Tuple(Of String, String)("300", "GR"))
        TStandardCode.Add(New Tuple(Of String, String)("239", "GS"))
        TStandardCode.Add(New Tuple(Of String, String)("320", "GT"))
        TStandardCode.Add(New Tuple(Of String, String)("316", "GU"))
        TStandardCode.Add(New Tuple(Of String, String)("624", "GW"))
        TStandardCode.Add(New Tuple(Of String, String)("328", "GY"))
        TStandardCode.Add(New Tuple(Of String, String)("344", "HK"))
        TStandardCode.Add(New Tuple(Of String, String)("334", "HM"))
        TStandardCode.Add(New Tuple(Of String, String)("340", "HN"))
        TStandardCode.Add(New Tuple(Of String, String)("191", "HR"))
        TStandardCode.Add(New Tuple(Of String, String)("332", "HT"))
        TStandardCode.Add(New Tuple(Of String, String)("348", "HU"))
        TStandardCode.Add(New Tuple(Of String, String)("360", "ID"))
        TStandardCode.Add(New Tuple(Of String, String)("372", "IE"))
        TStandardCode.Add(New Tuple(Of String, String)("376", "IL"))
        TStandardCode.Add(New Tuple(Of String, String)("833", "IM"))
        TStandardCode.Add(New Tuple(Of String, String)("356", "IN"))
        TStandardCode.Add(New Tuple(Of String, String)("086", "IO"))
        TStandardCode.Add(New Tuple(Of String, String)("368", "IQ"))
        TStandardCode.Add(New Tuple(Of String, String)("364", "IR"))
        TStandardCode.Add(New Tuple(Of String, String)("352", "IS"))
        TStandardCode.Add(New Tuple(Of String, String)("380", "IT"))
        TStandardCode.Add(New Tuple(Of String, String)("832", "JE"))
        TStandardCode.Add(New Tuple(Of String, String)("388", "JM"))
        TStandardCode.Add(New Tuple(Of String, String)("400", "JO"))
        TStandardCode.Add(New Tuple(Of String, String)("392", "JP"))
        TStandardCode.Add(New Tuple(Of String, String)("404", "KE"))
        TStandardCode.Add(New Tuple(Of String, String)("417", "KG"))
        TStandardCode.Add(New Tuple(Of String, String)("116", "KH"))
        TStandardCode.Add(New Tuple(Of String, String)("296", "KI"))
        TStandardCode.Add(New Tuple(Of String, String)("174", "KM"))
        TStandardCode.Add(New Tuple(Of String, String)("659", "KN"))
        TStandardCode.Add(New Tuple(Of String, String)("408", "KP"))
        TStandardCode.Add(New Tuple(Of String, String)("410", "KR"))
        TStandardCode.Add(New Tuple(Of String, String)("414", "KW"))
        TStandardCode.Add(New Tuple(Of String, String)("136", "KY"))
        TStandardCode.Add(New Tuple(Of String, String)("398", "KZ"))
        TStandardCode.Add(New Tuple(Of String, String)("418", "LA"))
        TStandardCode.Add(New Tuple(Of String, String)("422", "LB"))
        TStandardCode.Add(New Tuple(Of String, String)("662", "LC"))
        TStandardCode.Add(New Tuple(Of String, String)("438", "LI"))
        TStandardCode.Add(New Tuple(Of String, String)("144", "LK"))
        TStandardCode.Add(New Tuple(Of String, String)("430", "LR"))
        TStandardCode.Add(New Tuple(Of String, String)("426", "LS"))
        TStandardCode.Add(New Tuple(Of String, String)("440", "LT"))
        TStandardCode.Add(New Tuple(Of String, String)("442", "LU"))
        TStandardCode.Add(New Tuple(Of String, String)("428", "LV"))
        TStandardCode.Add(New Tuple(Of String, String)("434", "LY"))
        TStandardCode.Add(New Tuple(Of String, String)("504", "MA"))
        TStandardCode.Add(New Tuple(Of String, String)("492", "MC"))
        TStandardCode.Add(New Tuple(Of String, String)("498", "MD"))
        TStandardCode.Add(New Tuple(Of String, String)("499", "ME"))
        TStandardCode.Add(New Tuple(Of String, String)("450", "MG"))
        TStandardCode.Add(New Tuple(Of String, String)("584", "MH"))
        TStandardCode.Add(New Tuple(Of String, String)("807", "MK"))
        TStandardCode.Add(New Tuple(Of String, String)("466", "ML"))
        TStandardCode.Add(New Tuple(Of String, String)("104", "MM"))
        TStandardCode.Add(New Tuple(Of String, String)("496", "MN"))
        TStandardCode.Add(New Tuple(Of String, String)("446", "MO"))
        TStandardCode.Add(New Tuple(Of String, String)("474", "MQ"))
        TStandardCode.Add(New Tuple(Of String, String)("478", "MR"))
        TStandardCode.Add(New Tuple(Of String, String)("500", "MS"))
        TStandardCode.Add(New Tuple(Of String, String)("470", "MT"))
        TStandardCode.Add(New Tuple(Of String, String)("480", "MU"))
        TStandardCode.Add(New Tuple(Of String, String)("462", "MV"))
        TStandardCode.Add(New Tuple(Of String, String)("454", "MW"))
        TStandardCode.Add(New Tuple(Of String, String)("484", "MX"))
        TStandardCode.Add(New Tuple(Of String, String)("458", "MY"))
        TStandardCode.Add(New Tuple(Of String, String)("508", "MZ"))
        TStandardCode.Add(New Tuple(Of String, String)("516", "NA"))
        TStandardCode.Add(New Tuple(Of String, String)("540", "NC"))
        TStandardCode.Add(New Tuple(Of String, String)("562", "NE"))
        TStandardCode.Add(New Tuple(Of String, String)("574", "NF"))
        TStandardCode.Add(New Tuple(Of String, String)("566", "NG"))
        TStandardCode.Add(New Tuple(Of String, String)("558", "NI"))
        TStandardCode.Add(New Tuple(Of String, String)("528", "NL"))
        TStandardCode.Add(New Tuple(Of String, String)("578", "NO"))
        TStandardCode.Add(New Tuple(Of String, String)("524", "NP"))
        TStandardCode.Add(New Tuple(Of String, String)("520", "NR"))
        TStandardCode.Add(New Tuple(Of String, String)("570", "NU"))
        TStandardCode.Add(New Tuple(Of String, String)("554", "NZ"))
        TStandardCode.Add(New Tuple(Of String, String)("512", "OM"))
        TStandardCode.Add(New Tuple(Of String, String)("591", "PA"))
        TStandardCode.Add(New Tuple(Of String, String)("604", "PE"))
        TStandardCode.Add(New Tuple(Of String, String)("258", "PF"))
        TStandardCode.Add(New Tuple(Of String, String)("598", "PG"))
        TStandardCode.Add(New Tuple(Of String, String)("608", "PH"))
        TStandardCode.Add(New Tuple(Of String, String)("586", "PK"))
        TStandardCode.Add(New Tuple(Of String, String)("616", "PL"))
        TStandardCode.Add(New Tuple(Of String, String)("666", "PM"))
        TStandardCode.Add(New Tuple(Of String, String)("612", "PN"))
        TStandardCode.Add(New Tuple(Of String, String)("630", "PR"))
        TStandardCode.Add(New Tuple(Of String, String)("275", "PS"))
        TStandardCode.Add(New Tuple(Of String, String)("620", "PT"))
        TStandardCode.Add(New Tuple(Of String, String)("585", "PW"))
        TStandardCode.Add(New Tuple(Of String, String)("600", "PY"))
        TStandardCode.Add(New Tuple(Of String, String)("634", "QA"))
        TStandardCode.Add(New Tuple(Of String, String)("638", "RE"))
        TStandardCode.Add(New Tuple(Of String, String)("642", "RO"))
        TStandardCode.Add(New Tuple(Of String, String)("688", "RS"))
        TStandardCode.Add(New Tuple(Of String, String)("643", "RU"))
        TStandardCode.Add(New Tuple(Of String, String)("646", "RW"))
        TStandardCode.Add(New Tuple(Of String, String)("682", "SA"))
        TStandardCode.Add(New Tuple(Of String, String)("090", "SB"))
        TStandardCode.Add(New Tuple(Of String, String)("690", "SC"))
        TStandardCode.Add(New Tuple(Of String, String)("736", "SD"))
        TStandardCode.Add(New Tuple(Of String, String)("752", "SE"))
        TStandardCode.Add(New Tuple(Of String, String)("702", "SG"))
        TStandardCode.Add(New Tuple(Of String, String)("654", "SH"))
        TStandardCode.Add(New Tuple(Of String, String)("705", "SI"))
        TStandardCode.Add(New Tuple(Of String, String)("744", "SJ"))
        TStandardCode.Add(New Tuple(Of String, String)("703", "SK"))
        TStandardCode.Add(New Tuple(Of String, String)("694", "SL"))
        TStandardCode.Add(New Tuple(Of String, String)("674", "SM"))
        TStandardCode.Add(New Tuple(Of String, String)("686", "SN"))
        TStandardCode.Add(New Tuple(Of String, String)("706", "SO"))
        TStandardCode.Add(New Tuple(Of String, String)("740", "SR"))
        TStandardCode.Add(New Tuple(Of String, String)("678", "ST"))
        TStandardCode.Add(New Tuple(Of String, String)("222", "SV"))
        TStandardCode.Add(New Tuple(Of String, String)("760", "SY"))
        TStandardCode.Add(New Tuple(Of String, String)("748", "SZ"))
        TStandardCode.Add(New Tuple(Of String, String)("796", "TC"))
        TStandardCode.Add(New Tuple(Of String, String)("148", "TD"))
        TStandardCode.Add(New Tuple(Of String, String)("260", "TF"))
        TStandardCode.Add(New Tuple(Of String, String)("768", "TG"))
        TStandardCode.Add(New Tuple(Of String, String)("764", "TH"))
        TStandardCode.Add(New Tuple(Of String, String)("834", "TH"))
        TStandardCode.Add(New Tuple(Of String, String)("762", "TJ"))
        TStandardCode.Add(New Tuple(Of String, String)("772", "TK"))
        TStandardCode.Add(New Tuple(Of String, String)("626", "TL"))
        TStandardCode.Add(New Tuple(Of String, String)("795", "TM"))
        TStandardCode.Add(New Tuple(Of String, String)("788", "TN"))
        TStandardCode.Add(New Tuple(Of String, String)("776", "TO"))
        TStandardCode.Add(New Tuple(Of String, String)("792", "TR"))
        TStandardCode.Add(New Tuple(Of String, String)("780", "TT"))
        TStandardCode.Add(New Tuple(Of String, String)("798", "TV"))
        TStandardCode.Add(New Tuple(Of String, String)("158", "TW"))
        TStandardCode.Add(New Tuple(Of String, String)("804", "UA"))
        TStandardCode.Add(New Tuple(Of String, String)("800", "UG"))
        TStandardCode.Add(New Tuple(Of String, String)("840", "US"))
        TStandardCode.Add(New Tuple(Of String, String)("858", "UY"))
        TStandardCode.Add(New Tuple(Of String, String)("860", "UZ"))
        TStandardCode.Add(New Tuple(Of String, String)("336", "VA"))
        TStandardCode.Add(New Tuple(Of String, String)("670", "VC"))
        TStandardCode.Add(New Tuple(Of String, String)("862", "VE"))
        TStandardCode.Add(New Tuple(Of String, String)("092", "VG"))
        TStandardCode.Add(New Tuple(Of String, String)("850", "VI"))
        TStandardCode.Add(New Tuple(Of String, String)("704", "VN"))
        TStandardCode.Add(New Tuple(Of String, String)("548", "VU"))
        TStandardCode.Add(New Tuple(Of String, String)("876", "WF"))
        TStandardCode.Add(New Tuple(Of String, String)("882", "WS"))
        TStandardCode.Add(New Tuple(Of String, String)("887", "YE"))
        TStandardCode.Add(New Tuple(Of String, String)("175", "YT"))
        TStandardCode.Add(New Tuple(Of String, String)("710", "ZA"))
        Return TStandardCode
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.Indigo.UserIndigo) Then
            Using model As New MCountry(MCountry.TAG)
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlyCountry.BeginUpdate()
        ActionsOnControls = False

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        Status = True
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDtxtNationality.Text = String.Empty
        Country = Nothing
        StandardCodeNumeric = Nothing

        If Indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyCountry.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Country
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = INDtxtName.Text
            .Nationality = INDtxtNationality.Text
            .StandardCode = StandardCode
            .StandardCodeNumeric = StandardCodeNumeric
            .State = Status
        End With
    End Sub

    ''' <summary>
    ''' Esta función asincrónica (Async Function) realiza la creación de un nuevo país (objeto Country) 
    ''' </summary>
    Private Async Function NewCountry() As Task
        Country = New Country()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.GeneralLedgerSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.GeneralLedgerSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MStatementFolio(CStr(Me.Tag))
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
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Using Model As New MCountry(CStr(Me.Tag))
                AsyncLoader(True)
                Country = Await Model.GetCountryAsync(INDbteCode.Text.Trim)
                INDlyCountry.BeginUpdate()
                If Country IsNot Nothing AndAlso Country.Id > 0 Then
                    Me.BarraBotones.StatusRecordVisible = True
                    record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(Country.Id))
                    With Country
                        LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                        Code = .Code
                        CountryName = .Name
                        Nationality = .Nationality
                        StandardCodeNumeric = .StandardCodeNumeric
                        StandardCode = .StandardCode
                        Status = .State
                    End With

                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Country.Code)
                    If record.Id = 0 Then
                        record = (Await Model.SaveBlockRecord(
                            New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                .NameUser = Me.Indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.Indigo.UserIndigo, .IdRecord = Country.Id})
                            ).ObjectEmbbeded
                    Else
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(Country.Id, Me.Tag.ToString(), Nothing, GetType(Country).Name)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    AsyncLoader(False)
                    ActionsOnControls = True
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Await Me.NewCountry()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Code = String.Empty
                        INDbteCode.Focus()
                    End If
                End If
                INDlyCountry.EndUpdate()
            End Using
        End If

        'BarraBotones.StatusRecordVisible = True
        'Status = True
        'AsyncLoader(False)
        'Using Model As New MCountry(Me.Tag)
        '    Country = Await Model.GetCountryAsync(INDbteCode.Text)
        '    If Not Country Is Nothing Then
        '        If Country.Id > 0 Then
        '            Dim result = Await Model.GetBlockRecord(Me.Tag, Country.Id)
        '            With Country
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Country.CreationUser)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Country.CreationDate)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Country.ModificationUser)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), IIf(Country.ModificationDate Is Nothing, Nothing, Country.ModificationDate))
        '                Code = .Code
        '                CountryName = .Name
        '                Nationality = .Nationality
        '                Status = .State
        '            End With
        '            AsyncLoader(False)
        '            Me.GetDocumentIndexed(Me.Tag & "_" & Me.Country.Code)
        '            If result.Id = 0 Then
        '                Me.BarraBotones.SetDocuments(Country.Id)
        '                Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                state.State = Domain.Base.Entities.ObjectState.Added
        '                record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.Indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.Indigo.UserIndigo, .IdRecord = Country.Id}
        '                Dim operation = Await Model.SaveBlockRecord(record)
        '                record = operation.ObjectEmbbeded
        '            Else
        '                record = result
        '                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '            End If
        '        Else
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        End If
        '    Else
        '        Country = New Country
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    End If

        'End Using
        'ActionsOnControls = True
    End Function

#End Region

#Region "Eventos Barra Botones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        ModoBusqueda = False
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
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

    ''' <summary>
    ''' Esta función maneja el evento BarraBotones.ChangueOperatingUnit,
    ''' que ocurre cuando se cambia la unidad operativa en la barra de botones (o en otro componente). 
    ''' </summary>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.GeneralLedgerSequenceDetail IsNot Nothing Then
                If Not Me._sequence.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Customizar"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyCountry.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
            ExistDefinitionFront = True
        End If
    End Sub

    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
        If ExistDefinitionFront = True Then
            INDlyCountry.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCountry.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MCountry(Me.Tag)
                Dim dsFields As DataSet = Await model.GetFieldsNULLAsync()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyCountry.Items.Count - 1
                        INDlyCountry.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyCountry.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyCountry.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyCountry.Items.Item(j).AllowHide = True
                                End If
                            End If
                        Next
                    Next
                End If
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCountry.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyCountry.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(Indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyCountry.SaveLayoutToXml(PathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(Indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(Indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlyCountry.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

End Class