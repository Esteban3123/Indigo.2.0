'***********************************************************************
' Assembly         : Presentacion.Seguridad
' Author           : Hector Rodriguez Rubiano
' Created          : 20-04-2021
'
' Last Modified By :
' Last Modified On :
' Description      : Formulario para asignar permisos por tenant
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.Threading.Tasks
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Domain.Security.Entities
Imports Presentation.Security.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region


Public Class PopupOperatingUnitsPermits

#Region "Constructor"
    ''' <summary>
    ''' Inicia una instancia del formulario
    ''' </summary>
    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        IndigoGridControl1.SetHoldSize(INDGcUserOperatingUnit, True)
    End Sub
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    Private _OriginalUser As User
    Public Property OriginalUser() As User
        Get
            Return _OriginalUser
        End Get
        Set(ByVal value As User)
            _OriginalUser = value
            _User = value.Clone()
        End Set
    End Property

    Private _User As User
    Public Property PUser() As User
        Get
            Return _User
        End Get
        Set(ByVal value As User)
            _User = value
        End Set
    End Property

    Private _CompanyId As Integer
    Public Property CompanyId() As Integer
        Get
            Return _CompanyId
        End Get
        Set(ByVal value As Integer)
            _CompanyId = value
        End Set
    End Property

    Private _GenesisPermissionCompanies As GenesisPermissionCompanies
    Public Property PPermissionCompany() As GenesisPermissionCompanies
        Get
            Return _GenesisPermissionCompanies
        End Get
        Set(ByVal value As GenesisPermissionCompanies)
            _GenesisPermissionCompanies = value
            _CompanyId = value.CompanyId
        End Set
    End Property

    Private _reportPathType As Byte
    Public Property ReportPathType() As Byte
        Get
            Return _reportPathType
        End Get
        Set(ByVal value As Byte)
            _reportPathType = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el tipo para ruta de reportes por empresa
    ''' </summary>
    ''' <returns></returns>
    Public Property ReportPath As String
        Get
            Return INDBeReportPath.EditValue
        End Get
        Set(value As String)
            INDBeReportPath.EditValue = value
        End Set
    End Property

#End Region

#Region "Eventos"
    ''' <summary>
    ''' Evento cuando se carga el control
    ''' </summary>
    Private Sub PopupOperatingUnitsPermits_Load() Handles Me.Load
        LoadOperatingUnits()
        INDGvUserOperatingUnit.OptionsView.ShowAutoFilterRow = False
        INDGvUserOperatingUnit.ShowLoadingPanel()

        If ReportPathType = 1 Then
            INDLciReportPath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDColPath.Visible = False
            ReportPath = Nothing
        End If

        Task.Factory.StartNew(Sub()
                                  INDGcUserOperatingUnit.SafeInvoke(Sub()
                                                                        INDGcUserOperatingUnit.DataSource = PUser.UserOperatingUnit.Where(Function(uou) uou.IdContainer = CompanyId AndAlso uou.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted)
                                                                        INDGvUserOperatingUnit.HideLoadingPanel()
                                                                    End Sub)
                              End Sub)
    End Sub
#End Region

#Region "Procesos"
    ''' <summary>
    ''' Consulta unidades operativas de la empresa
    ''' </summary>
    Private Sub LoadOperatingUnits()
        Using modelUsuario As New MUsuario
            Dim _ListOperatingUnit As List(Of OperatingUnit) = modelUsuario.ListAllOperatingUnitSimple(PPermissionCompany.CompanyTransactionalContainer)
            INDSleOperativeUnit.Properties.DataSource = _ListOperatingUnit
            'Completa el nombre de la unidad operativa
            Dim _UserOperatingUnits = From _UserOperatingUnit In PUser.UserOperatingUnit.Where(Function(uou) uou.IdContainer = CompanyId)
                                      Join _OperatingUnit In _ListOperatingUnit On _OperatingUnit.Id Equals _UserOperatingUnit.IdOperatingUnit
                                      Select _UserOperatingUnit, _OperatingUnit
            For Each _UserOperatingUnitAux In _UserOperatingUnits
                _UserOperatingUnitAux._UserOperatingUnit.OperatingUnitName = _UserOperatingUnitAux._OperatingUnit.UnitName
            Next
            'Asigna la unidad operativa por defecto
            If PPermissionCompany.IdOperatingUnitDefault > 0 Then
                Dim _UserOperatingUnit = PUser.UserOperatingUnit.Where(Function(uou) uou.IdContainer = CompanyId AndAlso uou.IdOperatingUnit = PPermissionCompany.IdOperatingUnitDefault).FirstOrDefault
                If _UserOperatingUnit IsNot Nothing Then
                    _UserOperatingUnit.ByDefault = True
                End If
            End If
        End Using
    End Sub
    ''' <summary>
    ''' Refresca la informacion de la rejilla UserOperatingUnit
    ''' </summary>
    Private Sub RefreshGridUserOperatingUnit()
        If PUser.UserOperatingUnit Is Nothing Then
            PUser.UserOperatingUnit = New Domain.Base.Entities.TrackableCollection(Of UserOperatingUnit)
        End If
        INDGcUserOperatingUnit.Invalidate()
        INDGcUserOperatingUnit.DataSource = PUser.UserOperatingUnit.Where(Function(uou) uou.IdContainer = CompanyId AndAlso uou.ChangeTracker.State <> ObjectState.Deleted)
        INDGcUserOperatingUnit.RefreshDataSource()
    End Sub
#End Region
    ''' <summary>
    ''' Evento para quitar la relacion de un usuario con unidades operativas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRibeDeleteUserOperatingUnit_Click(sender As Object, e As EventArgs) Handles INDRibeDeleteUserOperatingUnit.Click
        Dim _UserOperatingUnit = TryCast(INDGvUserOperatingUnit.GetFocusedRow, UserOperatingUnit)
        If _UserOperatingUnit IsNot Nothing Then
            If _UserOperatingUnit.Id > 0 Then
                _UserOperatingUnit.ChangeTracker.State = ObjectState.Deleted
                PUser.UserOperatingUnit.Add(_UserOperatingUnit)
            Else
                PUser.UserOperatingUnit.Remove(_UserOperatingUnit)
            End If
            RefreshGridUserOperatingUnit()
        End If
    End Sub

    ''' <summary>
    ''' Adiciona o actualiza tenantuser a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbAddUserOperatingUnit_Click(sender As Object, e As EventArgs) Handles INDSmbAddUserOperatingUnit.Click
        Dim _Mensaje = "Seleccione: "
        If INDSleOperativeUnit.EditValue Is Nothing OrElse INDSleOperativeUnit.EditValue = 0 Then
            _Mensaje = String.Format("{0} {1} {2}", _Mensaje, Environment.NewLine, "Unidad operativa")
        End If
        If String.IsNullOrEmpty(ReportPath) And INDLciReportPath.Visible Then
            _Mensaje = String.Format("{0} {1} {2}", _Mensaje, Environment.NewLine, "Ruta reporte")
        End If

        If Not _Mensaje.Equals("Seleccione: ") Then
            Mensaje(EeventViewerImages.MensajeError) = _Mensaje
        Else
            Dim _UserOperatingUnit As UserOperatingUnit = PUser.UserOperatingUnit.Where(Function(uou) uou.IdContainer = CompanyId AndAlso uou.IdOperatingUnit = CInt(INDSleOperativeUnit.EditValue)).FirstOrDefault
            If _UserOperatingUnit Is Nothing Then
                _UserOperatingUnit = New UserOperatingUnit() With {.IdContainer = CompanyId, .IdOperatingUnit = INDSleOperativeUnit.EditValue, .IdUser = PUser.Id}
                Dim _OperatingUnit = TryCast(INDSleOperativeUnit.GetSelectedDataRow(), OperatingUnit)
                If _OperatingUnit IsNot Nothing Then
                    _UserOperatingUnit.OperatingUnitName = _OperatingUnit.UnitName
                End If
                PUser.UserOperatingUnit.Add(_UserOperatingUnit)
            ElseIf _UserOperatingUnit.ChangeTracker.State = ObjectState.Deleted Then
                _UserOperatingUnit.ChangeTracker.State = ObjectState.Modified
            ElseIf _UserOperatingUnit.ChangeTracker.State = ObjectState.Unchanged Then
                _UserOperatingUnit.ChangeTracker.State = ObjectState.Modified
            End If
            If INDRgDefaultOperatingUnit.EditValue Then
                For Each _UserOperatingUnitAux In PUser.UserOperatingUnit.Where(Function(uou) uou.IdContainer = CompanyId AndAlso uou.ByDefault)
                    _UserOperatingUnitAux.ByDefault = False
                    If _UserOperatingUnitAux.ChangeTracker.State = ObjectState.Unchanged Then
                        _UserOperatingUnitAux.ChangeTracker.State = ObjectState.Modified
                    End If
                Next
            End If
            _UserOperatingUnit.ByDefault = INDRgDefaultOperatingUnit.EditValue
            _UserOperatingUnit.Status = True
            _UserOperatingUnit.ReportPath = ReportPath
            RefreshGridUserOperatingUnit()
        End If
    End Sub

    ''' <summary>
    ''' Actualiza los permisos modificados en el objeto usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbAccept_Click(sender As Object, e As EventArgs) Handles INDSmbAccept.Click
        Dim _UserOperatingUnits = From _UserOperatingUnitMod In PUser.UserOperatingUnit.Where(Function(uoum) uoum.IdContainer = CompanyId AndAlso uoum.ChangeTracker.State <> ObjectState.Unchanged)
                                  Group Join _UserOperatingUnit In OriginalUser.UserOperatingUnit.Where(Function(uou) uou.IdContainer = CompanyId) On _UserOperatingUnit.IdOperatingUnit Equals _UserOperatingUnitMod.IdOperatingUnit Into Group
                                  From _UserOperatingUnit In Group.DefaultIfEmpty()
                                  Select _UserOperatingUnitMod, _UserOperatingUnit

        'Modifica permisos existentes
        If _UserOperatingUnits.Count > 0 AndAlso _UserOperatingUnits.Any(Function(_ExistingUserOperatingUnit) _ExistingUserOperatingUnit._UserOperatingUnit IsNot Nothing) Then
            For Each _ExistingUserOperatingUnitAux In _UserOperatingUnits.Where(Function(_ExistingPermissionUser) _ExistingPermissionUser._UserOperatingUnit IsNot Nothing).ToList
                _ExistingUserOperatingUnitAux._UserOperatingUnit.Status = _ExistingUserOperatingUnitAux._UserOperatingUnitMod.Status
                _ExistingUserOperatingUnitAux._UserOperatingUnit.ReportPath = _ExistingUserOperatingUnitAux._UserOperatingUnitMod.ReportPath
                _ExistingUserOperatingUnitAux._UserOperatingUnit.ByDefault = _ExistingUserOperatingUnitAux._UserOperatingUnitMod.ByDefault
                _ExistingUserOperatingUnitAux._UserOperatingUnit.ChangeTracker.State = _ExistingUserOperatingUnitAux._UserOperatingUnitMod.ChangeTracker.State
                If _ExistingUserOperatingUnitAux._UserOperatingUnitMod.ChangeTracker.State = ObjectState.Deleted Then
                    OriginalUser.UserOperatingUnit.Add(_ExistingUserOperatingUnitAux._UserOperatingUnit)
                End If
            Next
        End If

        'Agrega permisos nuevos
        If _UserOperatingUnits.Count > 0 AndAlso _UserOperatingUnits.Any(Function(_NewUserOperatingUnit) _NewUserOperatingUnit._UserOperatingUnit Is Nothing) Then
            For Each _NewUserOperatingUnitAux In _UserOperatingUnits.Where(Function(_NewUserOperatingUnit) _NewUserOperatingUnit._UserOperatingUnit Is Nothing).ToList
                OriginalUser.UserOperatingUnit.Add(New UserOperatingUnit() With {
                                                .IdUser = _NewUserOperatingUnitAux._UserOperatingUnitMod.IdUser,
                                                .IdContainer = CompanyId,
                                                .IdOperatingUnit = _NewUserOperatingUnitAux._UserOperatingUnitMod.IdOperatingUnit,
                                                .Status = _NewUserOperatingUnitAux._UserOperatingUnitMod.Status,
                                                .OperatingUnitName = _NewUserOperatingUnitAux._UserOperatingUnitMod.OperatingUnitName,
                                                .ByDefault = _NewUserOperatingUnitAux._UserOperatingUnitMod.ByDefault,
                                                .ReportPath = _NewUserOperatingUnitAux._UserOperatingUnitMod.ReportPath
                                                })
            Next
        End If
        'Permisos eliminados que no estan guardados en base de datos
        Dim _DeleteUserOperatingUnits = From _UserOperatingUnit In OriginalUser.UserOperatingUnit.Where(Function(uou) uou.IdContainer = CompanyId)
                                        Group Join _UserOperatingUnitMod In PUser.UserOperatingUnit.Where(Function(uoum) uoum.IdContainer = CompanyId) On _UserOperatingUnitMod.IdOperatingUnit Equals _UserOperatingUnit.IdOperatingUnit Into Group
                                        From _UserOperatingUnitMod In Group.DefaultIfEmpty()
                                        Where _UserOperatingUnitMod Is Nothing
                                        Select _UserOperatingUnit
        If _DeleteUserOperatingUnits.Count > 0 AndAlso _DeleteUserOperatingUnits.Any() Then
            For Each _DeleteOperatingUnit In _DeleteUserOperatingUnits.ToList
                OriginalUser.UserOperatingUnit.Remove(_DeleteOperatingUnit)
            Next
        End If

        'Actualiza la unidad operativa por defecto en permisos compañia
        Dim _UserOperatingUnitAux = OriginalUser.UserOperatingUnit.Where(Function(uou) uou.IdContainer = CompanyId AndAlso uou.ByDefault).FirstOrDefault
        Dim _PermissionCompany = OriginalUser.PermissionCompany.Where(Function(uou) uou.IdContainer = CompanyId).FirstOrDefault
        If _UserOperatingUnitAux IsNot Nothing Then
            PPermissionCompany.IdOperatingUnitDefault = _UserOperatingUnitAux.IdOperatingUnit
            If _PermissionCompany IsNot Nothing Then
                _PermissionCompany.IdOperatingUnitDefault = _UserOperatingUnitAux.IdOperatingUnit
            End If
        Else
            PPermissionCompany.IdOperatingUnitDefault = 0
            If _PermissionCompany IsNot Nothing Then
                _PermissionCompany.IdOperatingUnitDefault = 0
            End If
        End If

        PUser = Nothing
        Me.Close()
    End Sub

    ''' <summary>
    ''' Cancela la edicion de permisos y cierra el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbCancel_Click(sender As Object, e As EventArgs) Handles INDSmbCancel.Click
        PUser = Nothing
        Me.Close()
    End Sub

    Private Sub INDBeReportPath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBeReportPath.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
            INDXfbdReportPath.ShowDialog()
            If Not String.IsNullOrEmpty(INDXfbdReportPath.SelectedPath) Then
                ReportPath = INDXfbdReportPath.SelectedPath
            Else
                Mensaje(EeventViewerImages.MensajeError) = "La ruta seleccionada no es válida."
            End If
        End If
    End Sub
End Class