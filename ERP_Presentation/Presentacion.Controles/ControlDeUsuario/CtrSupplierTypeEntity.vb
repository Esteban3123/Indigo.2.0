Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.MaintenanceRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.Controls.MVP
Imports Domain.Base.Entities

Public Class CtrSupplierTypeEntity
    Inherits DevExpress.XtraEditors.TreeListLookUpEdit
    Implements ISupportInitialize

#Region "Constructor"

    Public Sub New()
        InitializeComponent()
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
    End Sub

#End Region

#Region "Friend WithEvents"

    Friend WithEvents TreeListLookUpEdit1TreeList As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn

#End Region

#Region "Init"

    Public Sub BeginInit() Implements ISupportInitialize.BeginInit

    End Sub

    Public Sub EndInit() Implements ISupportInitialize.EndInit

        TreeListLookUpEdit1TreeList = Me.Properties.TreeList

        'Properties
        Me.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Properties.Appearance.Options.UseBackColor = True
        Me.Properties.Appearance.Options.UseFont = True
        Me.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.Properties.AppearanceFocused.Options.UseFont = True
        'Me.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)})
        Me.Properties.DisplayMember = "CodeName"
        Me.Properties.TreeList = Me.TreeListLookUpEdit1TreeList
        Me.Properties.ValueMember = "Id"

        'TreeListLookUpEdit1TreeList
        Me.TreeListLookUpEdit1TreeList.Appearance.GroupButton.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TreeListLookUpEdit1TreeList.Appearance.GroupButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TreeListLookUpEdit1TreeList.Appearance.GroupButton.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.GroupButton.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!)
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn1})
        Me.TreeListLookUpEdit1TreeList.KeyFieldName = "Id"
        Me.TreeListLookUpEdit1TreeList.Name = "TreeListLookUpEdit1TreeList"
        Me.TreeListLookUpEdit1TreeList.OptionsBehavior.EnableFiltering = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = True
        Me.TreeListLookUpEdit1TreeList.ParentFieldName = "ParentId"

        Me.Properties.TreeList = TreeListLookUpEdit1TreeList

        'TreeListColumn1

        Me.TreeListColumn1.FieldName = "CodeName"
        Me.TreeListColumn1.Caption = "Descripción"
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.AppearanceHeader.Options.UseTextOptions = True
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    ''' Valida que el el tipo de proveedor sea el ultimo item
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ValidateSupplierType()
        If Me.EditValue IsNot Nothing AndAlso Me.Properties.DataSource IsNot Nothing Then
            Dim ListSupplierType As List(Of Domain.Entities.SupplierType) = Me.Properties.DataSource
            Dim supplierType As Domain.Entities.SupplierType = (From fu In ListSupplierType Where fu.Id = Me.EditValue Select fu).FirstOrDefault
            If supplierType IsNot Nothing Then
                If supplierType.IsSon = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedItemLastLevel")
                    Me.Properties.NullText = String.Empty
                    Me.EditValue = Nothing
                End If
            End If
        End If
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el proveedor
    ''' </summary>
    Private _supplierId As Integer?
    Public WriteOnly Property SupplierId() As Integer?
        Set(ByVal value As Integer?)
            Me._supplierId = value
            Me.Properties.DataSource = Nothing
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el texto a mostrar cuando el valor seleccionado es nulo
    ''' </summary>
    ''' <value>Texto a mostrar</value>
    Public WriteOnly Property DisplayNullText As String
        Set(value As String)
            Me.Properties.NullText = value
            Me.Properties.NullValuePrompt = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el texto a mostrar cuando el valor seleccionado es nulo
    ''' </summary>
    ''' <value>Texto a mostrar</value>
    Public WriteOnly Property SetDefault As SupplierType
        Set(value As SupplierType)
            Me.Properties.DataSource = New List(Of SupplierType) From {value}
        End Set
    End Property

#End Region

#Region "Events"

    Public Async Sub Me_QueryPopUp(sender As Object, e As CancelEventArgs) Handles Me.QueryPopUp
        If Me.Properties.DataSource Is Nothing Then
            If Not _supplierId > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un proveedor"
                Exit Sub
            End If

            Using model As New MCtrSupplierTypeEntity(String.Empty)
                Dim x As ActionResult(Of List(Of SupplierType)) = Await model.GetSupplierTypeBySupplierId(Me._supplierId)
                If x.StateResult Then
                    Me.Properties.DataSource = x.ObjectEmbbeded
                    Me.ShowPopup()
                End If
            End Using
        End If
    End Sub

#End Region

End Class
