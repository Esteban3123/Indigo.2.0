'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 14-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Payments.MVP

#End Region

''' <summary>
''' Formulario de Cajas
''' </summary>
Public Class FrmCash
    Implements ICash, ICustomizableForm

#Region "Properties and Variables"

    ''' <summary>
    ''' Constante que contiene el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements ICash.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICash.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' variable que contiene la secuencia de cabecera
    ''' </summary>
    Dim _sequence As TreasurySequence

    ''' <summary>
    ''' Contiene el id de la secuencia de detalle
    ''' </summary>
    Dim _idCurrentSequence As Int64

    ''' <summary>
    '''  Ultimo valor de la revalorizacion
    ''' </summary>
    Dim _periodLastRevaluation As Integer

    ''' <summary>
    ''' Obtiene o establece el datasource para el campo moneda
    ''' </summary>
    Dim _balanceLastRevaluation As Decimal

    ''' <summary>
    ''' Gets or sets the user code.
    ''' </summary>
    ''' <value>
    ''' The user code.
    ''' </value>
    Private Property UserId As Integer
        Get
            Return CType(INDsleUser.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleUser.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequence.
    ''' </value>
    Public Property Sequence As TreasurySequence Implements ICash.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As TreasurySequence)
            _sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo de la caja
    ''' </summary>
    ''' <value>
    ''' The code cash.
    ''' </value>
    Public Property Code As String Implements ICash.CodeCash
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
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
    ''' Obtiene o establece el nombre de la caja
    ''' </summary>
    ''' <value>
    ''' The name cash.
    ''' </value>
    Public Property NameCash As String Implements ICash.NameCash
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de la caja
    ''' </summary>
    ''' <value>
    ''' The type.
    ''' </value>
    Public Property Type As Integer Implements ICash.Type
        Get
            Return INDsleType.EditValue
        End Get
        Set(value As Integer)
            INDsleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el prefijo de la caja
    ''' </summary>
    Public Property Prefix As String Implements ICash.Prefix
        Get
            Return INDtePrefix.Text
        End Get
        Set(value As String)
            INDtePrefix.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el saldo inicial
    ''' </summary>
    ''' <value>
    ''' The opening balance.
    ''' </value>
    Public Property InitialBalance As Decimal Implements ICash.InitialBalance
        Get
            Return INDtxtOpeningBalance.EditValue
        End Get
        Set(value As Decimal)
            INDtxtOpeningBalance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha inicial
    ''' </summary>
    ''' <value>
    ''' The starting date.
    ''' </value>
    Public Property InitialDate As Date Implements ICash.InitialDate
        Get
            Return INDdeStartingDate.EditValue
        End Get
        Set(value As Date)
            INDdeStartingDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cuenta contable
    ''' </summary>
    ''' <value>
    ''' The account accounting.
    ''' </value>
    Public Property IdMainAccount As Integer Implements ICash.IdMainAccount
        Get
            Return INDsleAccountAccounting.EditValue
        End Get
        Set(value As Integer)
            INDsleAccountAccounting.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cuantia disponible
    ''' </summary>
    ''' <value>
    ''' The amount available.
    ''' </value>
    Public Property CurrentBalance As Decimal Implements ICash.CurrentBalance
        Get
            Return INDtxtCurrentBalance.EditValue
        End Get
        Set(value As Decimal)
            INDtxtCurrentBalance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el centro de costo
    ''' </summary>
    ''' <value>
    ''' The cost center.
    ''' </value>
    Public Property IdCostCenter As Nullable(Of Integer) Implements ICash.IdCostCenter
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Nullable(Of Integer))
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cuantia maxima
    ''' </summary>
    ''' <value>
    ''' The maximum amount.
    ''' </value>
    Public Property AmountMax As Decimal Implements ICash.AmountMax
        Get
            Return INDtxtMaximumAmount.EditValue
        End Get
        Set(value As Decimal)
            INDtxtMaximumAmount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cuantia minima
    ''' </summary>
    ''' <value>
    ''' The minimun amount.
    ''' </value>
    Public Property AmountMin As Decimal Implements ICash.AmountMin
        Get
            Return INDtxtMinimunAmount.EditValue
        End Get
        Set(value As Decimal)
            INDtxtMinimunAmount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el reembolso
    ''' </summary>
    ''' <value>
    ''' The refound.
    ''' </value>
    Public Property RefundDate As DateTime? Implements ICash.RefoundDate
        Get
            Return INDdeRefound.EditValue
        End Get
        Set(value As DateTime?)
            INDdeRefound.EditValue = value
        End Set
    End Property

    '' <summary>
    '' Obtiene o establece el estado del registro
    '' </summary>
    '' <value>
    ''   <c>true</c> if [state]; otherwise, <c>false</c>.
    '' </value>
    Public Property State As Boolean Implements ICash.State
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
    ''' Gets or sets the third party datasource.
    ''' </summary>
    Public Property ThirdPartyDatasource As XPInstantFeedbackSource Implements ICash.ThirdPartyDatasource
        Get
            Return CType(INDsleThirdParty.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the third party identifier.
    ''' </summary>
    Public Property ThirdPartyId As Integer? Implements ICash.ThirdPartyId
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer?)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas contables
    ''' </summary>
    ''' <value>
    ''' The account accounting datasource.
    ''' </value>
    Public Property AccountAccountingDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements ICash.AccountAccountingDatasource
        Get
            Return CType(INDsleAccountAccounting.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountAccounting.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasorce de centro costo
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Public Property CostCenterDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements ICash.CostCenterDatasource
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de los usuarios
    ''' </summary>
    ''' <value>
    ''' The user datasource.
    ''' </value>
    Public Property UserDatasource As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements ICash.UserDatasource
        Get
            Return CType(INDsleUser.Datasource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleUser.Datasource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la moneda de la caja
    ''' </summary>
    ''' <value>
    ''' The account accounting.
    ''' </value>
    Public Property CurrencyId As Integer Implements ICash.CurrencyId
        Get
            Return INDdeCurrency.EditValue
        End Get
        Set(value As Integer)
            INDdeCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para el campo de moneda
    ''' </summary>
    ''' <value>
    ''' The account accounting datasource.
    ''' </value>
    Public Property CurrencyXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICash.CurrencyXpo
        Get
            Return CType(INDdeCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDdeCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Variable para almacenar las monedas
    ''' </summary>
    Dim CurrencyAbbreviation As String

    ''' <summary>
    ''' Presentador de Cajas
    ''' </summary>
    Dim Presenter As PCash

    ''' <summary>
    ''' The lista usuarios
    ''' </summary>
    Dim ListaUsuarios As New List(Of User)

    ''' <summary>
    ''' variable que almacena el registro de bloqueo
    ''' </summary>
    Dim record As BlockRecordTreasury

    ''' <summary>
    ''' Variable que contiene la entidad de caja
    ''' </summary>
    Dim CashRegister As CashRegisters

    ''' <summary>
    ''' variable para almacenar los datos de tipo
    ''' </summary>
    Dim TypeFile As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property PeriodLastRevaluation As Integer Implements ICash.PeriodLastRevaluation
        Get
            Return _periodLastRevaluation
        End Get
        Set(value As Integer)
            _periodLastRevaluation = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property BalanceLastRevaluation As Decimal Implements ICash.BalanceLastRevaluation
        Get
            Return _balanceLastRevaluation
        End Get
        Set(value As Decimal)
            _balanceLastRevaluation = value
        End Set
    End Property
#End Region

#Region "Icrud"

    ''' <summary>
    ''' Esta función es parte de la implementación de la interfaz Base.ICrudBase 
    ''' y se encarga de iniciar una búsqueda de registros. Para hacerlo, simplemente 
    ''' llama a la función OpenSearch(), que a su vez se encarga de mostrar una ventana
    ''' de búsqueda donde el usuario puede buscar y seleccionar registros específicos. 
    ''' En esencia, esta función proporciona un punto de entrada para iniciar el proceso 
    ''' de búsqueda de registros en el contexto de la operación CRUD (Crear, Leer, Actualizar, Eliminar).
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Esta función implementa la interfaz ICrudBase y se encarga de restablecer los controles y campos
    ''' del formulario a su estado inicial o vacío, como si no se hubiera realizado ninguna acción de
    ''' edición o entrada de datos. Para lograrlo, simplemente llama a la función CleanControls(),
    ''' que se encarga de limpiar y restablecer todos los controles y valores del formulario a su estado
    ''' predeterminado. En resumen, esta función proporciona un método para deshacer o cancelar cualquier 
    ''' acción de edición o cambios realizados en los controles del formulario.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Esta función implementa la interfaz ICrudBase y se encarga de manejar la operación de eliminación
    ''' de un registro. En este caso, parece estar relacionada con el manejo de registros de cajas registradoras (CashRegister).
    ''' AsyncLoader => Carga Asyncrona
    ''' DeleteCashRegister => Metodo que se invoca de (MCashRegister) - pasando como parametro del objeto (CashRegister) que desea eliminar}
    ''' Deshacer => Funcion para establecer los controles de los formularios
    ''' DeleteDocumentIndexed => Metodo para eliminar documento indexado asociado al registro
    ''' ShowMessage => posiblemente una respuesta o mensaje de éxito o error
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        If Me.CashRegister IsNot Nothing AndAlso Me.CashRegister.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MCashRegister(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteCashRegister(Me.CashRegister)
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
    ''' - Esta función implementa la interfaz ICrudBase y se encarga de manejar la operación de guardar
    '''     (crear o actualizar) un registro. Parece estar relacionada con el manejo de registros de cajas 
    '''     registradoras (CashRegisters)
    ''' - ValidateControls() => Funcion para validar los controles de la interfaz  de usuario
    ''' - Si la validación es exitosa, se asignan los valores de los controles a las propiedades del 
    '''     objeto CashRegister utilizando la función AssigningValues().
    ''' - Si la validación es exitosa, se asignan los valores de los controles a las propiedades del 
    '''     objeto CashRegister utilizando la función AssigningValues().
    ''' - Si la validación es exitosa, se asignan los valores de los controles a las propiedades del 
    '''     objeto CashRegister utilizando la función AssigningValues().
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MCashRegister(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of CashRegisters) = Await Model.SaveCashRegister(Me.CashRegister, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If CashRegister.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.CashRegister = result.ObjectEmbbeded
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
    ''' Esta función implementa la interfaz ICrudBase y se encarga de realizar la operación de crear
    ''' un nuevo registro (en este caso, aparentemente un registro de caja registradora).
    ''' - Verifica si _sequence (que parece ser un objeto relacionado con secuencias) es nulo. Si es nulo, 
    '''     muestra un mensaje de advertencia indicando que la secuencia aún no se ha cargado y no puede 
    '''     proceder con la creación del registro.
    ''' - Si _sequence no es nulo pero su propiedad IsManual es verdadera, entonces llama a la función Deshacer()
    '''     para restablecer los controles del formulario en su estado inicial. Esto se asume como una forma de "deshacer"
    '''     cualquier entrada de datos que se haya realizado en los controles.
    ''' - Si _sequence no es nulo y su propiedad IsManual es falsa, invoca la función NewCash() de forma asincrónica.
    '''     Esta función aparentemente se encarga de iniciar el proceso de creación de un nuevo registro de caja registradora.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha cargado la secuencia todavia"
        ElseIf Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewCash()
        End If
    End Sub

#End Region

#Region "Functions and Methods"

    ''' <summary>
    ''' La función CreateTypeFile() se encarga de inicializar y llenar un origen de datos para un control de 
    ''' selección (INDsleType) con valores relacionados con el tipo de archivo. 
    ''' </summary>
    Private Sub CreateTypeFile()
        TypeFile.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("CashTypeMinor", NAME_MODULE)))
        TypeFile.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("CashTypeMajor", NAME_MODULE)))

        INDsleType.Properties.DataSource = TypeFile
    End Sub

    ''' <summary>
    ''' La función AssigningValues() se encarga de asignar valores a las propiedades de un objeto de tipo CashRegisters
    ''' (presumiblemente, una clase que representa información de una caja registradora) utilizando los valores actuales
    ''' de diferentes controles y campos. 
    ''' </summary>
    Private Sub AssigningValues()
        With CashRegister
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameCash
            .Type = Type
            .InitialBalance = InitialBalance
            .InitialDate = InitialDate
            .RefundDate = RefundDate
            .FilingUnitId = INDSleFilingUnit.EditValue
            .AmountMax = AmountMax
            .AmountMin = AmountMin
            .CurrentBalance = CurrentBalance
            .IdMainAccount = IdMainAccount
            .IdCostCenter = IdCostCenter
            .ThirdPartyId = ThirdPartyId
            .CurrencyId = CurrencyId
            .Prefix = Prefix
            .PeriodLastRevaluation = Me.PeriodLastRevaluation
            .BalanceLastRevaluation = Me.BalanceLastRevaluation
            If CashRegister.Id = 0 Then
                .IsMovement = False
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad Mensaje es una implementación de la interfaz Base.ICrudBase que permite mostrar mensajes de diferentes tipos en la interfaz
    ''' de usuario de una aplicación. La propiedad toma un parámetro Icono de tipo Base.EeventViewerImages que especifica el tipo de icono a mostrar
    ''' junto con el mensaje. Luego, según el valor de Icono, se muestra un mensaje usando la clase MessageIndigo que parece ser parte del sistema 
    ''' de mensajes de la aplicación.
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
    ''' Esta función OpenSearch es una implementación de la interfaz Base.ICrudBase y se utiliza para abrir una ventana
    ''' de búsqueda en la interfaz de usuario de una aplicación.
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4},
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "Type", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25},
                              New ColumnInfo() With {.Caption = "Moneda", .FieldName = "CurrencyAbbreviation", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllCashRegister
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Esta función ReturnValue es un manejador de eventos que se invoca cuando se obtiene un valor
    ''' de retorno de la ventana de búsqueda, que probablemente se utiliza para seleccionar un registro específico.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad ActionsOnControls controla la habilitación o deshabilitación de varios
    ''' controles en la interfaz de usuario. Su propósito es cambiar el estado de los controles 
    ''' según una bandera booleana proporcionada.
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICash.ActionsOnControls
        Set(value As Boolean)
            INDlycCash.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleType.Enabled = value
            INDtxtOpeningBalance.Enabled = value
            INDdeStartingDate.Enabled = value
            INDdeRefound.Enabled = value
            INDtxtMaximumAmount.Enabled = value
            INDtxtMinimunAmount.Enabled = value
            INDtxtCurrentBalance.Enabled = value
            INDsleThirdParty.Enabled = value
            INDtePrefix.Enabled = value
            INDdeCurrency.Enabled = value

            INDsleAccountAccounting.Enabled = value
            INDsleCostCenter.Enabled = value
            INDsleUser.Enabled = value
            INDgcAutorization.Enabled = value
            INDsbAdd.Enabled = value
            INDSleFilingUnit.Enabled = value
            INDlycCash.EndUpdate()

            Me.BarraBotones.StatusRecordVisible = value
            If value Then
                If CashRegister IsNot Nothing AndAlso CashRegister.Id > 0 Then
                    If CashRegister.IsMovement Then
                        INDtxtOpeningBalance.Enabled = False
                        INDdeStartingDate.Enabled = False
                        INDtxtCurrentBalance.Enabled = False
                        INDdeRefound.Enabled = False
                    End If
                End If
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta función DeleteBlockedRecord elimina un registro bloqueado específico en el contexto de un módulo
    ''' de tesorería (aparentemente relacionado con una entidad llamada "CashRegister").
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonTreasury As New MCashRegister(Me.Tag)
                Await ModelCommonTreasury.DeleteBlockRecordTreasury(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Esta función llamada LoadControls realiza varias tareas relacionadas con la carga y visualización
    ''' de controles y datos en una interfaz de usuario asociada a registros de tesorería ("CashRegister").
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MCashRegister(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetcashRegister(Me.Code)
                    CashRegister = resultOperation.ObjectEmbbeded
                    INDlycCash.BeginUpdate()
                    If CashRegister IsNot Nothing AndAlso CashRegister.Id > 0 Then
                        Dim _hasMovements As Boolean = False
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelRecord As New MCashRegisterUser(CStr(Me.Tag))
                            record = Await Model.GetBlockRecordTreasury(CStr(Me.Tag), CStr(CashRegister.Id))
                            Using ModelCashRegisterUser As New MCashRegisterUser(Me.Tag)
                                Dim resultUser = Await ModelCashRegisterUser.ListUsersByCashRegisterId(CashRegister.Id)
                                If resultUser.StateResult Then
                                    ListaUsuarios = resultUser.ObjectEmbbeded
                                End If
                            End Using
                            With CashRegister
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Type = .Type
                                If CashRegister.Type = 2 Then
                                    CleanControlsRefound()
                                Else
                                    If .RefundDate Is Nothing Then
                                        INDliRefound.HideControl(True)
                                    Else
                                        ControlsRefoundInVisible = False
                                    End If
                                End If
                                Code = .Code
                                NameCash = .Name

                                Prefix = .Prefix
                                InitialBalance = .InitialBalance
                                InitialDate = .InitialDate
                                RefundDate = .RefundDate
                                AmountMax = .AmountMax
                                AmountMin = .AmountMin
                                CurrentBalance = .CurrentBalance
                                IdMainAccount = .IdMainAccount
                                ThirdPartyId = .ThirdPartyId
                                IdCostCenter = .IdCostCenter
                                INDSleFilingUnit.EditValue = .FilingUnitId
                                _hasMovements = .hasMovements
                                If .CurrencyId IsNot Nothing Then
                                    CurrencyId = .CurrencyId
                                    Me.CurrencyAbbreviation = .CurrencyName
                                    INDdeCurrency.Properties.NullText = .CurrencyName
                                End If
                                State = .Status
                                Me.PeriodLastRevaluation = .PeriodLastRevaluation
                                Me.BalanceLastRevaluation = .BalanceLastRevaluation
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.CashRegister.Code)
                            If record.Id = 0 Then
                                record = (Await Model.SaveBlockRecordTreasury(
                                    New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = CashRegister.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(CashRegister.Id, Me.Tag.ToString(), Nothing, GetType(CashRegisters).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            If _hasMovements Then
                                INDdeCurrency.Enabled = False
                            End If
                            INDgcAutorization.DataSource = ListaUsuarios
                            If CashRegister.CurrentBalance < CashRegister.AmountMin Then
                                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrentAmountLessAmountMin", NAME_MODULE)
                            End If
                        End Using
                        Me.SetFormatsControls(Me.CurrencyAbbreviation)
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewCash()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlycCash.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' La función SetFormatsControls se encarga de establecer los formatos numéricos adecuados en los controles
    ''' de la interfaz de usuario relacionados con las cantidades de dinero (por ejemplo, montos, balances) en una moneda específica.
    ''' </summary>
    ''' <param name="SetFormatsControls"></param>
    Private Sub SetFormatsControls(Abbreviation As String)
        If String.IsNullOrEmpty(Abbreviation) Then
            Abbreviation = indigo.CurrencyISO4217
        End If
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = Abbreviation.GetNumberFormat()
        Me.INDdeCurrency.Properties.NullText = $"{Abbreviation}"
        INDtxtMaximumAmount.Properties.Mask.Culture = _culture
        INDtxtMinimunAmount.Properties.Mask.Culture = _culture
        INDtxtOpeningBalance.Properties.Mask.Culture = _culture
        INDtxtCurrentBalance.Properties.Mask.Culture = _culture
    End Sub

    ''' <summary>
    ''' La función LoadStatus se utiliza para cargar y configurar los estados posibles de los registros en la barra de botones
    ''' de la interfaz de usuario. 
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' La función GenerateDoc se utiliza para generar y actualizar un objeto de tipo IndexedDocument2, que parece estar relacionado
    ''' con la indexación de documentos en la aplicación.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.CashRegister.Code, Me.CashRegister.Name, Me.CashRegister.InitialDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.CashRegister.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.CashRegister.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.CashRegister.Code, Me.CashRegister.Name, Me.CashRegister.InitialDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.CashRegister.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' La función CleanControls se encarga de restablecer y limpiar los controles en el formulario relacionado con el registro de efectivo.
    ''' </summary>
    Private Sub CleanControls()
        INDlycCash.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        State = True
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDsleType.EditValue = Nothing
        INDtxtOpeningBalance.EditValue = 0
        INDdeStartingDate.EditValue = Nothing
        INDdeRefound.EditValue = Nothing
        INDtxtMaximumAmount.EditValue = 0
        INDtxtMinimunAmount.EditValue = 0
        INDtxtCurrentBalance.EditValue = 0
        INDsleThirdParty.EditValue = Nothing
        INDsleAccountAccounting.EditValue = Nothing
        INDsleCostCenter.EditValue = Nothing
        INDtePrefix.Text = String.Empty
        INDLciFilingUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciFilingUnit.ShowInCustomizationForm = True
        INDLciFilingUnit.AllowHide = True
        INDSleFilingUnit.EditValue = Nothing
        INDliThirdParty.HideControl(True)
        INDsleUser.EditValue = Nothing
        INDgcAutorization.DataSource = Nothing
        CleanControlsRefound()
        INDliCostCenter.HideControl(True)
        ListaUsuarios.Clear()
        CurrencyId = indigo.OfficialCurrencyId
        INDdeCurrency.Properties.NullText = indigo.CurrencyISO4217
        Me.CurrencyAbbreviation = String.Empty
        Me.SetFormatsControls(indigo.CurrencyISO4217)
        Me.PeriodLastRevaluation = 0
        Me.BalanceLastRevaluation = 0
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycCash.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' La función NewCash parece ser una función que se utiliza para crear un nuevo registro de efectivo en el formulario.
    ''' </summary>
    Private Async Function NewCash() As Task
        CashRegister = New CashRegisters() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MCashRegister(CStr(Me.Tag))
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
    ''' La función UpdateState parece ser una función que se utiliza para actualizar el estado (activo o inactivo)
    ''' de un registro de efectivo en el formulario.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me.CashRegister.Code) Then
            Try
                Using model As New MCashRegister(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me.CashRegister.Status
                    Dim result As ActionResult(Of CashRegisters) = Await model.UpdateStateCashRegister(Me.CashRegister.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.CashRegister = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' La función CleanControlsRefound se utiliza para limpiar y restablecer los controles relacionados
    ''' con la devolución (refund) en el formulario.
    ''' </summary>
    Public Sub CleanControlsRefound()
        INDdeRefound.EditValue = Nothing
        INDtxtMaximumAmount.EditValue = 0
        INDtxtMinimunAmount.EditValue = 0
        ControlsRefoundInVisible = True
    End Sub

    ''' <summary>
    ''' La propiedad ControlsRefoundInVisible es una propiedad de escritura (write-only property) que se utiliza
    ''' para controlar la visibilidad de ciertos controles relacionados con la devolución en un formulario.
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [block controls refound]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ControlsRefoundInVisible As Boolean
        Set(value As Boolean)
            If value Then
                AmountMax = 0
                AmountMin = 0
                RefundDate = Nothing
            End If
            INDliRefound.HideControl(value)
            INDliMaximumAmount.HideControl(value)
            INDliMinimunAmount.HideControl(value)
        End Set
    End Property

    ''' <summary>
    ''' La función ValidateControlsValue se utiliza para realizar validaciones en los valores de diferentes propiedades
    ''' relacionadas con los montos y balances en un formulario.
    ''' </summary>
    Public Sub ValidateControlsValue()
        If AmountMax < AmountMin Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessAmountMin", NAME_MODULE)
            AmountMax = 0
            Exit Sub
        ElseIf AmountMax < InitialBalance Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessInitialBalance", NAME_MODULE)
            AmountMax = 0
            Exit Sub
        ElseIf AmountMax < CurrentBalance Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessCurrentAmount", NAME_MODULE)
            AmountMax = 0
            Exit Sub
        ElseIf InitialBalance < AmountMin Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InitialBalanceLessAmountMin", NAME_MODULE)
            InitialBalance = 0
            Exit Sub
        ElseIf CurrentBalance < AmountMin Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrentAmountLessAmountMin", NAME_MODULE)
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' La función Frm_Disposed es un controlador de eventos que se ejecuta cuando el 
    ''' actual (representado por MyBase) se elimina o se cierra. En este caso, el código
    ''' en esta función realiza una serie de acciones para liberar recursos y limpiar las
    ''' variables utilizadas en el formulario antes de que se elimine
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        Presenter = Nothing
        ListaUsuarios = Nothing
        record = Nothing
        CashRegister = Nothing
        TypeFile = Nothing
    End Sub

    ''' <summary>
    ''' La función FrmCash_Load es un controlador de eventos que se ejecuta cuando el formulario "FrmCash" se carga. En esta función,
    ''' se realizan una serie de tareas de configuración y carga de datos para preparar el formulario para su uso. 
    ''' </summary>
    Private Sub FrmCash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycCash, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PCash(Me)
        Presenter.LoadDefinitionLayout()
        Presenter.InitializeAccountAccounting()
        Presenter.InitializeCostCenter()
        Presenter.InitializeThirdParty()
        Presenter.InitializeUser()
        Presenter.GetSequence()
        Using model As New MEntityAccount(Me.Tag)
            Me.INDsleUser.FuncQueryOnKeyEnterPressed = AddressOf model.GetUserByCode
        End Using
        Dim listAction As New List(Of eAcciones)
        listAction.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvUser, listAction)
        IndigoGridControl1.RefreshGrid(INDgcAutorization)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvUser.Columns
            If col.Name = "colActions" Then
                col.Width = "400"
            End If
        Next
        CurrencyId = indigo.OfficialCurrencyId
        INDdeCurrency.Properties.NullText = indigo.CurrencyISO4217
        LoadStatus()
        Deshacer()
        CreateTypeFile()
    End Sub

    ''' <summary>
    ''' La función FrmCash_Shown es un controlador de eventos que se ejecuta cuando el formulario "FrmCash" se muestra o se activa.
    ''' </summary>
    Private Async Sub FrmCash_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown, MyBase.Activated
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
        Using model As New MFilingUnit(Me.Tag)
            If indigo Is Nothing Then
                Me.indigo = SessionValues.Instance
            End If
            Dim x As ActionResult(Of List(Of FilingUnit)) = Await model.GetFilingUnitByUser(indigo.UserIndigo)
            Dim listFilingUnit As List(Of FilingUnit) = x.ObjectEmbbeded
            FilingUnitXpo = listFilingUnit
        End Using
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' La función INDbteCode_KeyDown es un controlador de eventos que se ejecuta cuando se presiona una tecla
    ''' en el control de edición "INDbteCode" y luego se suelta la tecla.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewCash()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' La función INDtxtMinimunAmount_KeyDown es un controlador de eventos que se ejecuta cuando se presiona
    ''' una tecla en el control de edición "INDtxtMinimunAmount" y luego se suelta la tecla.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtMinimunAmount_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtMinimunAmount.KeyDown
        If e.KeyCode = Keys.Enter Then
            'If AmountMax < AmountMin Then
            '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessAmountMin", NAME_MODULE)
            '    Exit Sub
            'ElseIf InitialBalance < AmountMin Then
            '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InitialBalanceLessAmountMin", NAME_MODULE)
            '    Exit Sub
            'ElseIf CurrentAmount < AmountMin Then
            '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrentAmountLessAmountMin", NAME_MODULE)
            'End If
        End If
    End Sub

    ''' <summary>
    ''' La función INDtxtAmountAvailable_KeyDown es un controlador de eventos que se ejecuta cuando
    ''' se presiona una tecla en el control de edición "INDtxtCurrentBalance" y luego se suelta la tecla.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtAmountAvailable_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtCurrentBalance.KeyDown
        If e.KeyCode = Keys.Enter Then
            'If Type = 2 Then
            '    If AmountMax < CurrentAmount Then
            '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessCurrentAmount", NAME_MODULE)
            '        Exit Sub
            '    ElseIf CurrentAmount < AmountMin Then
            '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrentAmountLessAmountMin", NAME_MODULE)
            '    End If
            'End If
        End If
    End Sub

    ''' <summary>
    ''' El fragmento de código que has proporcionado es un controlador de eventos que se ejecuta cuando se presiona
    ''' una tecla en el control de edición "INDtxtOpeningBalance" y luego se suelta la tecla.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtOpeningBalance_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtOpeningBalance.KeyDown
        If e.KeyCode = Keys.Enter Then
            'If Type = 1 Then
            '    If AmountMax < InitialBalance Then
            '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessInitialBalance", NAME_MODULE)
            '        Exit Sub
            '    ElseIf InitialBalance < AmountMin Then
            '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InitialBalanceLessAmountMin", NAME_MODULE)
            '        Exit Sub
            '    End If
            'End If
            'CurrentAmount = InitialBalance
        End If
    End Sub

    ''' <summary>
    ''' La función INDtxtMaximumAmount_KeyDown es un controlador de eventos que se ejecuta cuando se presiona
    ''' una tecla en el control de edición "INDtxtMaximumAmount" y luego se suelta la tecla.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtMaximumAmount_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtMaximumAmount.KeyDown
        If e.KeyCode = Keys.Enter Then
            'If AmountMax < AmountMin Then
            '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessAmountMin", NAME_MODULE)
            '    Exit Sub
            'ElseIf AmountMax < InitialBalance Then
            '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessInitialBalance", NAME_MODULE)
            '    Exit Sub
            'ElseIf AmountMax < CurrentAmount Then
            '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessCurrentAmount", NAME_MODULE)
            '    Exit Sub
            'End If
        End If
    End Sub
#End Region

#Region "ButtonCLick"
    ''' <summary>
    ''' La función INDsleAccountAccounting_ButtonClick es un controlador de eventos que se ejecuta
    ''' cuando se hace clic en el botón asociado al control INDsleAccountAccounting. 
    ''' La funcionalidad de esta función es abrir un formulario emergente (FrmPopupPUC) para administrar cuentas de contabilidad.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAccountAccounting_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountAccounting.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPopupPUC
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeAccountAccounting()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' La función INDsleCostCenter_ButtonClick es un controlador de eventos que se ejecuta cuando se hace clic en el botón asociado al control INDsleCostCenter.
    ''' La funcionalidad de esta función es abrir un formulario emergente (FrmCostCenter) para administrar centros de costos.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCostCenter
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeCostCenter()
            End Using
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' La función INDsbAdd_Click es un controlador de eventos que se ejecuta cuando se hace clic en el botón "Agregar" (INDsbAdd).
    ''' La funcionalidad de esta función es agregar un usuario autorizado a la lista de usuarios autorizados para una caja registradora.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If INDsleUser.EditValue IsNot Nothing Then
            If ListaUsuarios.FindAll(Function(x) x.Id = UserId).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("UserSelectExist", NAME_MODULE)
                Exit Sub
            End If
            Dim responsible As New CashRegisterUser()
            responsible.IdUser = UserId

            Dim userXpo As Object = Nothing
            If INDgvUserList.GetFocusedRow() IsNot Nothing AndAlso Not INDgvUserList.GetFocusedRow().GetType().Name.Equals("NotLoadedObject") Then
                userXpo = DirectCast(DirectCast(INDgvUserList.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.SecurityRepository.UserXpo)
            Else
                Using model As New MEntityAccount(Me.Tag)
                    userXpo = Await model.GetUserById(INDsleUser.EditValue)
                End Using
            End If
            Dim user As New User()
            With user
                .Id = userXpo.Id
                .UserCode = userXpo.UserCode
                .Person = New Domain.Security.Entities.Person()
                .Person.Fullname = userXpo.PersonFullName
            End With
            ListaUsuarios.Add(user)

            CashRegister.CashRegisterUser.Add(responsible)
            If CashRegister.ChangeTracker.State <> ObjectState.Added Then
                CashRegister.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
            INDsleUser.EditValue = Nothing
            INDgcAutorization.DataSource = Nothing
            INDgcAutorization.DataSource = ListaUsuarios
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("UserSelect", NAME_MODULE)
        End If
    End Sub
#End Region

#Region "LostFocus"
    ''' <summary>
    ''' La función INDtxtMinimunAmount_LostFocus es un controlador de eventos que se ejecuta cuando el control INDtxtMinimunAmount
    ''' pierde el foco, es decir, cuando el usuario deja de interactuar con ese control. Sin embargo, en el código proporcionado,
    ''' el cuerpo de la función está comentado, por lo que no se ejecuta ninguna acción específica en este momento.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtMinimunAmount_LostFocus(sender As Object, e As EventArgs) Handles INDtxtMinimunAmount.LostFocus
        'If AmountMax < AmountMin Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessAmountMin", NAME_MODULE)
        'ElseIf InitialBalance < AmountMin Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InitialBalanceLessAmountMin", NAME_MODULE)
        'ElseIf CurrentAmount < AmountMin Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrentAmountLessAmountMin", NAME_MODULE)
        'End If
    End Sub

    ''' <summary>
    ''' La función INDtxtAmountAvailable_LostFocus es un controlador de eventos que se ejecuta cuando el control INDtxtCurrentBalance
    ''' pierde el foco, es decir, cuando el usuario deja de interactuar con ese control. Sin embargo, en el código proporcionado,
    ''' el cuerpo de la función está comentado, por lo que no se ejecuta ninguna acción específica en este momento.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtAmountAvailable_LostFocus(sender As Object, e As EventArgs) Handles INDtxtCurrentBalance.LostFocus
        'If AmountMax < CurrentAmount Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessCurrentAmount", NAME_MODULE)
        'ElseIf CurrentAmount < AmountMin Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrentAmountLessAmountMin", NAME_MODULE)
        'End If
    End Sub

    ''' <summary>
    ''' La función INDtxtOpeningBalance_LostFocus es un controlador de eventos que se ejecuta cuando el control INDtxtOpeningBalance
    ''' pierde el foco, es decir, cuando el usuario deja de interactuar con ese control. Sin embargo, en el código proporcionado,
    ''' el cuerpo de la función está comentado, por lo que no se ejecuta ninguna acción específica en este momento.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtOpeningBalance_LostFocus(sender As Object, e As EventArgs) Handles INDtxtOpeningBalance.LostFocus
        'If AmountMax < InitialBalance Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessInitialBalance", NAME_MODULE)
        'ElseIf InitialBalance < AmountMin Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InitialBalanceLessAmountMin", NAME_MODULE)
        'End If
    End Sub

    ''' <summary>
    ''' La función INDtxtMaximumAmount_LostFocus es un controlador de eventos que se ejecuta cuando el control INDtxtMaximumAmount
    ''' pierde el foco, es decir, cuando el usuario deja de interactuar con ese control. Sin embargo, en el código proporcionado,
    ''' el cuerpo de la función está comentado, por lo que no se ejecuta ninguna acción específica en este momento.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtMaximumAmount_LostFocus(sender As Object, e As EventArgs) Handles INDtxtMaximumAmount.LostFocus
        'If AmountMax < AmountMin Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessAmountMin", NAME_MODULE)
        'ElseIf AmountMax < InitialBalance Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessInitialBalance", NAME_MODULE)
        'ElseIf AmountMax < CurrentAmount Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AmountMaxLessCurrentAmount", NAME_MODULE)
        'End If
    End Sub
#End Region

#Region "Actions"
    ''' <summary>
    ''' Esta función es un controlador de eventos que se ejecuta cuando se hace clic en un botón de acción en una celda específica
    ''' en una columna de la cuadrícula IndigoGridView1 (que parece ser un control GridView de DevExpress).
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim ObjUser = CType(INDGvUser.GetRow(INDGvUser.FocusedRowHandle), User)
        If ObjUser IsNot Nothing Then
            For Each usr As CashRegisterUser In CashRegister.CashRegisterUser
                If usr.IdUser.Equals(ObjUser.Id) Then
                    usr.ChangeTracker.State = ObjectState.Deleted
                    CashRegister.ChangeTracker.State = ObjectState.Modified
                    Exit For
                End If
            Next
            ListaUsuarios.Remove(ObjUser)
            INDgcAutorization.DataSource = Nothing
            INDgcAutorization.DataSource = ListaUsuarios
        End If
    End Sub
#End Region

#Region "Closing"
    ''' <summary>
    ''' Esta función se ejecuta cuando el formulario FrmCash se está cerrando. La función tiene la responsabilidad
    ''' de llamar a la función DeleteBlockedRecord() antes de que el formulario se cierre completamente.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmCash_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Esta función se ejecuta cuando se cambia el valor seleccionado en el control INDsleType
    ''' (que parece ser un elemento de selección, probablemente un ComboBox). La función maneja
    ''' el evento EditValueChanged del control INDsleType.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If INDsleType.EditValue = 1 Then
            ControlsRefoundInVisible = False
            INDLciFilingUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciFilingUnit.ShowInCustomizationForm = False
            INDLciFilingUnit.AllowHide = False
        Else
            CleanControlsRefound()
            INDLciFilingUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciFilingUnit.ShowInCustomizationForm = True
            INDLciFilingUnit.AllowHide = True
            INDSleFilingUnit.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleAccountAccounting control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleAccountAccounting_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountAccounting.EditValueChanged

        If INDsleAccountAccounting.EditValue Is Nothing Then
            Return
        End If

        Using Model As New MCashRegister(Me.Tag)
            Dim puc As MainAccounts = Await Model.GetAccountById(INDsleAccountAccounting.EditValue)
            If puc.HandlesCostCenter Then
                INDliCostCenter.HideControl(False)
            Else
                INDliCostCenter.HideControl(True)
                INDsleCostCenter.EditValue = Nothing
            End If
            If puc.HandlesThirdParty Then
                INDliThirdParty.HideControl(False)
            Else
                INDliThirdParty.HideControl(True)
                ThirdPartyId = Nothing
            End If
        End Using
        'Using Model As New MCashRegister(Me.Tag)
        '    Dim ListCash As List(Of CashRegisters) = Model.ListCashRegister()
        '    For Each cash As CashRegisters In ListCash
        '        If Not cash.Id.Equals(CashRegister.Id) Then
        '            If cash.IdMainAccount.Equals(INDsleAccountAccounting.EditValue) Then
        '                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("AccountVinculateCash", NAME_MODULE), cash.Code)
        '                INDsleAccountAccounting.EditValue = Nothing
        '            End If
        '        End If
        '    Next
        'End Using
    End Sub

    Private Sub INDdeCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDdeCurrency.EditValueChanged
        Me.SetFormatsControls(INDdeCurrency.Properties.NullText)
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleThirdParty control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmThirdParty
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeThirdParty()
            End Using
        End If
    End Sub
#End Region

#Region "IdEntity"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.CashRegister IsNot Nothing AndAlso Me.CashRegister.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Establece el datasource del campo  moneda
    ''' </summary>
    Private Sub INDdeCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDdeCurrency.QueryPopUp
        If INDdeCurrency.Properties.DataSource Is Nothing Then
            INDdeCurrency.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub
#End Region
#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
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
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.TreasurySequenceDetail IsNot Nothing Then
                If Not Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Xpo"
    ''' <summary>
    ''' Establece el datasource de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitXpo As List(Of FilingUnit)
        Get
            Return INDSleFilingUnit.Properties.DataSource
        End Get
        Set(value As List(Of FilingUnit))
            INDSleFilingUnit.Properties.DataSource = value
        End Set
    End Property
#End Region

End Class