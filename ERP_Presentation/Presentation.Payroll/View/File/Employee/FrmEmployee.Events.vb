'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jose Luis Rojas
' Created          : 20-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo.CommonRepository

#End Region

Partial Public Class FrmEmployee

#Region "Variables De Rentas Exentas"

    Dim ListExemptIncomeL As New List(Of Domain.Entities.ExemptIncome)
    Dim ExemptIncomeTable As New Domain.Entities.ExemptIncome

#End Region

#Region "Events"
    ''' <summary>
    ''' Establece a  los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        culture.NumberFormat = numberFormat
        changeNumericFormatByCurrency(numberFormat, INDpccStudy.Controls)
        changeNumericFormatByCurrency(numberFormat, INDpceExemptIncome.Controls)
        changeNumericFormatByCurrency(numberFormat, INDpccContactInfo.Controls)
        changeNumericFormatByCurrency(numberFormat, INDctrContractViewer.Controls)
        INDtxtValueExemptIncome.Properties.Mask.Culture = culture
        INDtxtDependsValueRTF.Properties.Mask.Culture = culture
        INDgrcRelationshipDependsValue = Window.Utils.FormatGrid(INDgrcRelationshipDependsValue, _currencyAbbreviation)
        GridColumn1088 = Window.Utils.FormatGrid(GridColumn1088, _currencyAbbreviation)
        GridColumn45 = Window.Utils.FormatGrid(GridColumn45, _currencyAbbreviation)
        INDctrContractViewer.CurrencyAbbreviation = _currencyAbbreviation
        changeNumericFormatByCurrency(numberFormat)
    End Sub
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmHumanTalent_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        indigo = SessionValues.Instance

        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayroll", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Me.KeyPreview = True
        Presenter = New PEmployee(Me)
        SetCurrencyFormat(Presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        Deshacer()
        Presenter.Initializes()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Employee IsNot Nothing AndAlso Me.Employee.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), Botones.SiNo, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), MessageType.Question) = System.Windows.Forms.DialogResult.Yes Then
                Me.INDbteIdNumber.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteIdNumber.Text = Me.IdEntity.Trim()
            Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Evento que ubica el foco en el control habilitado e inicial para el funcionamiento del modulo
    ''' </summary>
    Private Sub FrmHumanTalent_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Initializes()
        INDbteIdNumber.Focus()
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteIdNumber.KeyDown
        If Not String.IsNullOrEmpty(INDbteIdNumber.Text.ToString) Then
            If Not ValidatingFlag Then
                If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                    LoadControls()
                    If INDbteIdNumber.Enabled = False Then
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
                    End If
                    INDbteIdNumber.Enabled = False
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteIdNumber.ButtonClick
        AbrirBusqueda()
    End Sub


#Region "Popup Contacto"

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al cerrar el popup
    ''' </summary>
    Private Sub INDpccContactInfo_CloseUp(sender As Object, e As EventArgs) Handles INDpccContactInfo.CloseUp
        INDbtnNationality.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre el boton
    ''' </summary>
    Private Sub INDbtnContactInfo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnContactInfo.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDbtnNationality.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento para eliminar las direcciones agregadas al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarDireccion() Handles CtrContacts.EliminarDireccion
        If Employee.ThirdParty.Person.Address.Count > 0 Then
            ListadoEliminadosDireccion.Add(Employee.ThirdParty.Person.Address.Item(CtrContacts.ItemSelecionadoDireccion))
            Employee.ThirdParty.Person.Address.RemoveAt(CtrContacts.ItemSelecionadoDireccion)
            CtrContacts.EstablecerDataSourceDireccion = Employee.ThirdParty.Person.Address
        End If
    End Sub

    ''' <summary>
    ''' Evento para eliminar los telefono agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarTelefono() Handles CtrContacts.EliminarTelefono
        If Employee.ThirdParty.Person.Phone.Count > 0 Then
            ListadoEliminadosTelefono.Add(Employee.ThirdParty.Person.Phone.Item(CtrContacts.ItemSelecionadoTelefono))
            Employee.ThirdParty.Person.Phone.RemoveAt(CtrContacts.ItemSelecionadoTelefono)
            CtrContacts.EstablecerDataSourceTelefono = Employee.ThirdParty.Person.Phone
        End If
    End Sub

    ''' <summary>
    ''' Evento para eliminar los Email agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarEmail() Handles CtrContacts.EliminarEmail
        If Employee.ThirdParty.Person.Email.Count > 0 Then
            ListadoEliminadosEmail.Add(Employee.ThirdParty.Person.Email.Item(CtrContacts.ItemSelecionadoEmail))
            Employee.ThirdParty.Person.Email.RemoveAt(CtrContacts.ItemSelecionadoEmail)
            CtrContacts.EstablecerDataSourceEmail = Employee.ThirdParty.Person.Email
        End If
    End Sub

    ''' <summary>
    ''' Evento para cambia el tipo Email
    ''' </summary>
    Private Sub CtrContactos1_ChangeEmailType(Type As Byte) Handles CtrContacts.ChangeEmailType
        If Employee.ThirdParty.Person.Email.Count > 0 Then
            Dim email = Employee.ThirdParty.Person.Email.ElementAt(CtrContacts.ItemSelecionadoEmail)
            email.Type = Type
            If email.Id > 0 Then
                email.ChangeTracker.State = ObjectState.Modified
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de direcciones  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevaDireccion() Handles CtrContacts.InsertoNuevaDireccion
        If Employee.ThirdParty.Person.Address Is Nothing Then
            Employee.ThirdParty.Person.Address = New Domain.Base.Entities.TrackableCollection(Of Address)
        End If
        If Employee.ThirdParty.Person.Address.Where(Function(e) e.Addresss = CtrContacts.Direccion).Count = 0 Then
            Employee.ThirdParty.Person.Address.Add(New Address With
                                                      {
                                                        .DepartmentId = CtrContacts.DepartmentId,
                                                        .DepartmentName = CtrContacts.DepartmentName,
                                                        .CityId = CtrContacts.CityId,
                                                        .CityName = CtrContacts.CityName,
                                                        .Addresss = CtrContacts.Direccion,
                                                        .Synchronized = "1",
                                                        .State = True
                                                      }
                                                   )
        End If
        CtrContacts.EstablecerDataSourceDireccion = Nothing
        CtrContacts.EstablecerDataSourceDireccion = Employee.ThirdParty.Person.Address
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de correos  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoEmail() Handles CtrContacts.InsertoNuevoEmail
        If Employee.ThirdParty.Person.Email Is Nothing Then
            Employee.ThirdParty.Person.Email = New Domain.Base.Entities.TrackableCollection(Of Email)
        End If
        If Employee.ThirdParty.Person.Email.Where(Function(e) e.Email1 = CtrContacts.Email).Count = 0 Then
            Employee.ThirdParty.Person.Email.Add(New Email With {.Email1 = CtrContacts.Email, .Synchronized = "1", .Type = CtrContacts.EmailType, .State = True})
        End If
        CtrContacts.EstablecerDataSourceEmail = Nothing
        CtrContacts.EstablecerDataSourceEmail = Employee.ThirdParty.Person.Email
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de Telefonos  del control de datos de contacto.
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoTelefono() Handles CtrContacts.InsertoNuevoTelefono
        If Employee.ThirdParty.Person.Phone Is Nothing Then
            Employee.ThirdParty.Person.Phone = New Domain.Base.Entities.TrackableCollection(Of Phone)
        End If
        If Employee.ThirdParty.Person.Phone.Where(Function(e) e.Phone1 = CtrContacts.Telefono).Count = 0 Then
            Employee.ThirdParty.Person.Phone.Add(New Phone With {.Phone1 = CtrContacts.Telefono, .IdPhoneType = CShort(CtrContacts.TipoTelefono), .Synchronized = "1", .State = True})
        End If
        CtrContacts.EstablecerDataSourceTelefono = Nothing
        CtrContacts.EstablecerDataSourceTelefono = Employee.ThirdParty.Person.Phone
    End Sub

#End Region

#Region "Popup sindicato"

    ''' <summary>
    ''' Metodo que añade una sindicato a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbAddNewTradeUnion_Click(sender As Object, e As EventArgs) Handles INDsbAddNewTradeUnion.Click
        If String.IsNullOrEmpty(INDsleTradeUnion.Text.Trim) OrElse INDsleTradeUnion.EditValue = -1 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciTradeUnion.Text)
            Exit Sub
        End If

        If TradeUnionDatasource.Where(Function(x) x.TradeUnionId = INDsleTradeUnion.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ItemYaAgregado)
            Exit Sub
        End If

        Dim codeName = INDsleTradeUnion.Text.Split(" - ")
        TradeUnionDatasource.Add(New TradeUnionEmployee() With {
            .TradeUnionId = INDsleTradeUnion.EditValue,
            .TradeUnion = New TradeUnion With {.Id = INDsleTradeUnion.EditValue, .Code = codeName(0), .Name = codeName(2)}
            })

        GridControl1.RefreshDataSource()

        INDsleTradeUnion.EditValue = -1
        INDsleTradeUnion.Focus()
    End Sub

    ''' <summary>
    ''' Eliminar nacionalidad de la rejilla de sindicatos
    ''' </summary>
    Private Sub INDrepTradeUnionDeleteAction2_Click(sender As Object, e As EventArgs) Handles INDrepTradeUnionDeleteAction2.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim ItemTradeUnionDetail = DirectCast(GridView1.GetFocusedRow(), TradeUnionEmployee)
            If ItemTradeUnionDetail.Id > 0 Then
                If listTradeUnionDetail Is Nothing Then
                    listTradeUnionDetail = New List(Of TradeUnionEmployee)
                End If
                ItemTradeUnionDetail.MarkAsDeleted()
                listTradeUnionDetail.Add(ItemTradeUnionDetail)
            End If
            TradeUnionDatasource.Remove(ItemTradeUnionDetail)
            GridControl1.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que muestra de acuerdo a el idioma el texto de eliminar el registro
    ''' </summary>
    Private Sub INDrepTradeUnionDeleteAction2_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles INDrepTradeUnionDeleteAction2.CustomDisplayText
        e.DisplayText = obtenerRecurso(Eresources.EliminarRegistro)
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al cerrar el popup
    ''' </summary>
    Private Sub INDpccNewTradeUnion_CloseUp(sender As Object, e As EventArgs) Handles INDpccNewTradeUnion.CloseUp
        INDbtnNationality.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre el boton
    ''' </summary>
    Private Sub INDbtnNewTradeUnion_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnNewTradeUnion.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDbtnNationality.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre la rejilla
    ''' </summary>
    Private Sub INDgvTradeUnion_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDbtnNationality.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna el foco al control despues de abierto el popup
    ''' </summary>
    Private Sub INDpccNewTradeUnion_GotFocus(sender As Object, e As EventArgs) Handles INDpccNewTradeUnion.GotFocus
        INDsleTradeUnion.Focus()
    End Sub

#End Region

#Region "Popup Nacionalidades"

    ''' <summary>
    ''' Metodo que añade una nacionalidad a la rejilla de nacionalidades
    ''' </summary>
    Private Sub INDbtnAddNewNationality_Click(sender As Object, e As EventArgs) Handles INDbtnAddNewNationality.Click

        If String.IsNullOrEmpty(INDsleNationality.Text.Trim) OrElse INDsleNationality.EditValue = -1 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciNationality.Text)
            Exit Sub
        End If

        If NationalityDatasource.Where(Function(i) i.CountryId = INDsleNationality.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ItemYaAgregado)
            Exit Sub
        End If

        NationalityDatasource.Add(New PersonNationality() With {
            .CountryId = INDsleNationality.EditValue,
            .Country = New Country With {.Id = INDsleNationality.EditValue, .Nationality = INDsleNationality.Text}
            })

        INDgrdNationalities.RefreshDataSource()

        INDsleNationality.EditValue = -1
        INDsleNationality.Focus()
    End Sub

    ''' <summary>
    ''' Eliminar nacionalidad de la rejilla de nacionalidades
    ''' </summary>
    Private Sub INDrepNationalityDeleteAction_Click(sender As Object, e As EventArgs) Handles INDrepNationalityDeleteAction.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim ToDelete = CType(INDgrdNationalities.DefaultView.GetRow(CType(INDgrdNationalities.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), PersonNationality)
            NationalityDatasource.Remove(ToDelete)
            ListadoEliminadosNacionalidades.Add(ToDelete)
            INDgrdNationalities.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que muestra de acuerdo a el idioma el texto de eliminar el registro
    ''' </summary>
    Private Sub INDrepNationalityDeleteAction_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles INDrepNationalityDeleteAction.CustomDisplayText
        e.DisplayText = obtenerRecurso(Eresources.EliminarRegistro)
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al cerrar el popup
    ''' </summary>
    Private Sub INDpccNationality_CloseUp(sender As Object, e As EventArgs) Handles INDpccNationality.CloseUp
        INDglePensionary.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre el boton
    ''' </summary>
    Private Sub INDbtnNationality_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnNationality.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDglePensionary.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre la rejilla
    ''' </summary>
    Private Sub INDgrvNationalities_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDgrvNationalities.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDglePensionary.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna el foco al control despues de abierto el popup
    ''' </summary>
    Private Sub INDpccNationality_GotFocus(sender As Object, e As EventArgs) Handles INDpccNationality.GotFocus
        INDsleNationality.Focus()
    End Sub

#End Region


#Region "Popup Actividades en Tiempo Libre"

    ''' <summary>
    ''' Metodo que añade una actividad en tiempo libre a la rejilla de uso del tiempo libre
    ''' </summary>
    Private Sub INDbtnAddNewFreeTimeUse_Click(sender As Object, e As EventArgs) Handles INDbtnNewFreeTimeUse.Click

        If String.IsNullOrEmpty(INDsleFreeTimeUse.Text.Trim) OrElse INDsleFreeTimeUse.EditValue = -1 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciFreeTime.Text)
            Exit Sub
        End If

        If FreeTimeUseDatasource.Where(Function(i) i.FreeTimeUseId = INDsleFreeTimeUse.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ItemYaAgregado)
            Exit Sub
        End If


        FreeTimeUseDatasource.Add(New PersonFreeTimeUse() With {
            .FreeTimeUseId = INDsleFreeTimeUse.EditValue,
            .FreeTimeUse = New FreeTimeUse With {.Id = INDsleFreeTimeUse.EditValue, .Name = INDsleFreeTimeUse.Text}
            })

        INDgrdFreeTimeUses.RefreshDataSource()

        INDsleFreeTimeUse.EditValue = -1
        INDsleFreeTimeUse.Focus()
    End Sub

    ''' <summary>
    ''' Eliminar actividades en tiempo libre de la rejilla de uso del tiempo libre
    ''' </summary>
    Private Sub INDrepFreeTimeUseDeleteAction_Click(sender As Object, e As EventArgs) Handles INDrepFreeTimeUseDeleteAction.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim ToDelete = CType(INDgrdFreeTimeUses.DefaultView.GetRow(CType(INDgrdFreeTimeUses.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), PersonFreeTimeUse)
            FreeTimeUseDatasource.Remove(ToDelete)
            ListadoEliminadosActividadesTiempoLibre.Add(ToDelete)
            INDgrdFreeTimeUses.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que muestra de acuerdo a el idioma el texto de eliminar el registro
    ''' </summary>
    Private Sub INDrepFreeTimeUseDeleteAction_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles INDrepFreeTimeUseDeleteAction.CustomDisplayText
        e.DisplayText = obtenerRecurso(Eresources.EliminarRegistro)
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al cerrar el popup
    ''' </summary>
    Private Sub INDpccFreeTimeUse_CloseUp(sender As Object, e As EventArgs) Handles INDpccFreeTimeUse.CloseUp
        INDsleDiagnosedDisease.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre el boton
    ''' </summary>
    Private Sub INDbtnFreeTimeUse_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnFreeTimeUse.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDsleDiagnosedDisease.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre la rejilla
    ''' </summary>
    Private Sub INDgrvFreeTimeUse_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDgrvFreeTimeUse.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDsleDiagnosedDisease.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna el foco al control despues de abierto el popup
    ''' </summary>
    Private Sub INDpccFreeTimeUse_GotFocus(sender As Object, e As EventArgs) Handles INDpccFreeTimeUse.GotFocus
        INDsleFreeTimeUse.Focus()
    End Sub

#End Region

#Region "Popup Enfermedades Diagnosticadas"

    ''' <summary>
    ''' Metodo que añade una enfermedad diagnosticada a la rejilla de enfermedades diagnosticadas
    ''' </summary>
    Private Sub INDbtnAddNewDiagnosedDisease_Click(sender As Object, e As EventArgs) Handles INDbtnNewDiagnosedDisease.Click

        If String.IsNullOrEmpty(INDsleDiagnosedDisease.Text.Trim) OrElse INDsleDiagnosedDisease.EditValue = -1 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciDiagnosedDisease.Text)
            Exit Sub
        End If

        If DiagnosedDiseaseDatasource.Where(Function(i) i.DiagnosedDiseaseId = INDsleDiagnosedDisease.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ItemYaAgregado)
            Exit Sub
        End If


        DiagnosedDiseaseDatasource.Add(New PersonDiagnosedDisease() With {
            .DiagnosedDiseaseId = INDsleDiagnosedDisease.EditValue,
            .DiagnosedDisease = New DiagnosedDisease With {.Id = INDsleDiagnosedDisease.EditValue, .Name = INDsleDiagnosedDisease.Text}
            })

        INDgrdDiagnosedDiseases.RefreshDataSource()

        INDsleDiagnosedDisease.EditValue = -1
        INDsleDiagnosedDisease.Focus()
    End Sub

    ''' <summary>
    ''' Eliminar enfermedades diagnosticadas de la rejilla de uso del tiempo libre
    ''' </summary>
    Private Sub INDrepDiagnosedDiseaseDeleteAction_Click(sender As Object, e As EventArgs) Handles INDrepDiagnosedDiseaseDeleteAction.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim ToDelete = CType(INDgrdDiagnosedDiseases.DefaultView.GetRow(CType(INDgrdDiagnosedDiseases.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), PersonDiagnosedDisease)
            DiagnosedDiseaseDatasource.Remove(ToDelete)
            ListadoEliminadosEnfermedadesDiagnosticadas.Add(ToDelete)
            INDgrdDiagnosedDiseases.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que muestra de acuerdo a el idioma el texto de eliminar el registro
    ''' </summary>
    Private Sub INDrepDiagnosedDiseaseDeleteAction_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles INDrepDiagnosedDiseaseDeleteAction.CustomDisplayText
        e.DisplayText = obtenerRecurso(Eresources.EliminarRegistro)
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al cerrar el popup
    ''' </summary>
    Private Sub INDpccDiagnosedDisease_CloseUp(sender As Object, e As EventArgs) Handles INDpccDiagnosedDisease.CloseUp
        INDgleTradeUnion.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre el boton
    ''' </summary>
    Private Sub INDbtnDiagnosedDisease_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnDiagnosedDisease.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDgleTradeUnion.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre la rejilla
    ''' </summary>
    Private Sub INDgrvDiagnosedDisease_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDgrvDiagnosedDisease.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDgleTradeUnion.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna el foco al control despues de abierto el popup
    ''' </summary>
    Private Sub INDpccDiagnosedDisease_GotFocus(sender As Object, e As EventArgs) Handles INDpccDiagnosedDisease.GotFocus
        INDsleDiagnosedDisease.Focus()
    End Sub

#End Region

#Region "PopUp Rentas Exentas"
    ''' <summary>
    ''' Metodo que añade el detalle de una renta exenta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddExemptIncome_Click(sender As Object, e As EventArgs) Handles INDbtnAddExemptIncome.Click
        Dim HeaderList = Presenter.ListExemptIncome1(Employee.ThirdPartyId)

        If ValidateExemptIncome() = True Then

            If exemptIncomeModification = False Then
                Dim ListAuxAddItem = New CommonExemptIncomeDetailXpo With {
                                                          .Id = "0",
                                                          .ExemptIncomeValue = ExemptIncomeValue,
                                                          .DateLiquidation = DateExemptIcome,
                                                          .ThirdPartyId = Employee.ThirdPartyId,
                                                          .YearLiquidated = .DateLiquidation.Year,
                                                          .Comments = ExemptIncomeComment,
                                                          .IsNew = 1
                                    }

                Dim ListAuxAddItem2 = New Domain.Entities.ExemptIncome() With {
                                              .ExemptIncomeValue = ExemptIncomeValue,
                                              .DateLiquidation = DateExemptIcome,
                                              .ThirdPartyId = Employee.ThirdPartyId,
                                              .YearLiquidated = .DateLiquidation.Year,
                                              .Comments = ExemptIncomeComment,
                                              .MonthlyIncome = 0,
                                              .VoucherCode = "",
                                              .VoucherType = "",
                                              .RegisterStatus = "",
                                              .IsNew = 1
                        }

                Dim Filter = HeaderList.Any(Function(t) t.Year = ListAuxAddItem.YearLiquidated)
                If Filter Then

                    listExemptIncomeGlobal.Add(ListAuxAddItem)
                    listToSaveExemptIncome.Add(ListAuxAddItem2)
                    Mensaje(EeventViewerImages.Advertencia) = "Se agrego el detalle para el año seleccionado"
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "El año seleccionado no tiene rentas Exentas"
                End If
            Else 'Si se va a modificar el registro
                Dim ReviewList = childView.DataSource(childView.FocusedRowHandle)
                ReviewListObject = CType(ReviewList, Domain.Entities.ExemptIncome)

                For Each item In listExemptIncomeGlobal
                    Dim idParts() As String = item.Id.Split("-"c)
                    Dim Filter = HeaderList.Any(Function(t) t.Year = Year(DateExemptIcome))
                    If Filter Then

                        If CType(idParts(0), Integer) = ReviewListObject.Id Then 'comparamos los id
                            item.ExemptIncomeValue = If(ExemptIncomeValue = ReviewListObject.ExemptIncomeValue, ReviewListObject.ExemptIncomeValue, ExemptIncomeValue)
                            item.DateLiquidation = If(DateExemptIcome = ReviewListObject.DateLiquidation, ReviewListObject.DateLiquidation, DateExemptIcome)
                            item.Comments = If(ExemptIncomeComment = Nothing, item.Comments, ExemptIncomeComment)
                            Exit For
                        End If

                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "El año seleccionado no tiene rentas Exentas"
                        CleanExemptIncomePopUp()
                        Exit Sub
                    End If
                Next

                If listToSaveExemptIncome.Count > 0 Then
                    For Each item In listToSaveExemptIncome

                        If item.Id = ReviewListObject.Id Then
                            item.DateLiquidation = If(DateExemptIcome = ReviewListObject.DateLiquidation, ReviewListObject.DateLiquidation, DateExemptIcome)
                            item.ExemptIncomeValue = If(ExemptIncomeValue = ReviewListObject.ExemptIncomeValue, ReviewListObject.ExemptIncomeValue, ExemptIncomeValue)
                            item.Comments = If(ExemptIncomeComment = Nothing, "No hay comentarios", ExemptIncomeComment)

                            Exit For
                        End If



                    Next
                Else
                    Dim ListAuxAddItem2 = New Domain.Entities.ExemptIncome() With {
                                          .Id = CType(ReviewListObject.Id, Integer),
                                          .ExemptIncomeValue = If(ExemptIncomeValue = ReviewListObject.ExemptIncomeValue, ReviewListObject.ExemptIncomeValue, ExemptIncomeValue),
                                          .DateLiquidation = If(DateExemptIcome = ReviewListObject.DateLiquidation, ReviewListObject.DateLiquidation, DateExemptIcome),
                                          .ThirdPartyId = Employee.ThirdPartyId,
                                          .YearLiquidated = If(DateExemptIcome = Nothing, Year(ReviewListObject.DateLiquidation), Year(DateExemptIcome)),
                                          .Comments = If(ExemptIncomeComment = Nothing, "No hay comentarios", ExemptIncomeComment),
                                          .MonthlyIncome = 0,
                                          .VoucherCode = "",
                                          .VoucherType = "",
                                          .RegisterStatus = "",
                                          .IsNew = 1
                    }

                    ListAuxAddItem2.MarkAsModified()

                    listToSaveExemptIncome.Add(ListAuxAddItem2)
                    Mensaje(EeventViewerImages.Advertencia) = "Se modifico el detalle para el año seleccionado"
                End If
            End If
        End If

        exemptIncomeModification = False
        INDpceExemptIncome.ClosePopup()
        CleanExemptIncomePopUp()
        viewExemptIncome.CollapseAllDetails()
    End Sub

#End Region

#Region "PopUp Discapacidades"

    ''' <summary>
    ''' Metodo que añade una discapacidad al listado de discapacidades
    ''' </summary>
    Private Sub INDbtnAddNewDisability_Click(sender As Object, e As EventArgs) Handles INDbtnAddNewDisability.Click

        If ValidateDisabilityPopupControls() = False Then
            Exit Sub
        End If

        If DisabilitiesDatasource.Where(Function(i) i.DisabilityId = INDsleNewDisability.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ItemYaAgregado)
            Exit Sub
        End If

        Dim ctrAdd = 0

        If DisabilityToEdit Is Nothing Then
            DisabilityToEdit = New PersonDisability()
            ctrAdd = 1
        End If

        With DisabilityToEdit
            .DisabilityId = INDsleNewDisability.EditValue
            .Percentage = INDspeNewDisabilityPercentage.EditValue
            .Disability = New Domain.Payroll.Entities.Disability With {.Id = INDsleNewDisability.EditValue, .Description = INDsleNewDisability.Text}
        End With

        If ctrAdd = 1 Then
            DisabilitiesDatasource.Add(DisabilityToEdit)
        End If

        INDspeNewDisabilityPercentage.EditValue = String.Empty
        INDsleNewDisability.EditValue = -1

        INDgrdDisabilities.RefreshDataSource()

        DisabilityToEdit = Nothing
        INDsleNewDisability.Focus()
    End Sub

    ''' <summary>
    ''' Eliminar discapacidad
    ''' </summary>
    Private Sub INDbtnDelete_Click(sender As Object, e As EventArgs) Handles INDrepDisabilityDeleteAction.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim DisabilityToDelete = CType(INDgrdDisabilities.DefaultView.GetRow(CType(INDgrdDisabilities.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), PersonDisability)
            DisabilitiesDatasource.Remove(DisabilityToDelete)
            ListadoEliminadosDisability.Add(DisabilityToDelete)
            INDgrdDisabilities.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que limpia el popup de discapacidades si se cierra cuando se esta editando una discapacidad
    ''' </summary>
    Private Sub INDpccDisability_CloseUp(sender As Object, e As System.EventArgs) Handles INDpccDisability.CloseUp
        If DisabilityToEdit IsNot Nothing Then
            INDspeNewDisabilityPercentage.Text = String.Empty
            INDsleNewDisability.EditValue = Nothing
            DisabilityToEdit = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que ubica el foco en el primer campo a diligenciar en el popup de discapacidades
    ''' </summary>
    Private Sub INDpccDisability_Popup(sender As Object, e As EventArgs) Handles INDpccDisability.Popup
        INDsleNewDisability.Focus()
    End Sub

    Private Sub INDrepDisabilityDeleteAction_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles INDrepDisabilityDeleteAction.CustomDisplayText, INDrepLanguageDeleteAction.CustomDisplayText, INDrepProfessionDeleteAction.CustomDisplayText
        e.DisplayText = obtenerRecurso(Eresources.EliminarRegistro)
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al cerrar el popup
    ''' </summary>
    Private Sub DisabilityPopup_CloseUp(sender As Object, e As EventArgs) Handles INDpccDisability.CloseUp
        INDbtnAddLanguage.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre el boton
    ''' </summary>
    Private Sub INDbtnAddDisability_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnAddDisability.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDbtnAddLanguage.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre la rejilla
    ''' </summary>
    Private Sub INDgrvDisabilities_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDgrvDisabilities.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDbtnAddLanguage.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna el foco al control despues de abierto el popup
    ''' </summary>
    Private Sub INDpccDisability_GotFocus(sender As Object, e As EventArgs) Handles INDpccDisability.GotFocus
        INDsleNewDisability.Focus()
    End Sub

#End Region

#Region "PopUp Idiomas"

    ''' <summary>
    ''' Metodo que añade un idioma al listado de idiomas
    ''' </summary>
    Private Sub INDbtnAddNewLanguage_Click(sender As Object, e As EventArgs) Handles INDbtnAddNewLanguage.Click

        If Not ValidateLanguagePopupControl() Then
            Exit Sub
        End If

        If LanguagesDatasource.Where(Function(i) i.LanguageId = INDsleLanguageId.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ItemYaAgregado)
            Exit Sub
        End If

        Dim ctrAdd = 0

        If LanguageToEdit Is Nothing Then
            LanguageToEdit = New PersonLanguage()
            ctrAdd = 1
        End If

        With LanguageToEdit
            .LanguageId = INDsleLanguageId.EditValue
            .LanguageLevel = INDspeLanguageLevel.Text
            .Language = New Language With {.Id = INDsleLanguageId.EditValue, .Name = INDsleLanguageId.Text}
        End With

        If ctrAdd = 1 Then
            LanguagesDatasource.Add(LanguageToEdit)
        End If

        INDspeLanguageLevel.Text = String.Empty
        INDsleLanguageId.EditValue = -1

        INDgrdLanguages.RefreshDataSource()

        LanguageToEdit = Nothing
        INDsleLanguageId.Focus()
    End Sub

    ''' <summary>
    ''' Eliminar idioma
    ''' </summary>
    Private Sub INDbtnDeleteLanguage_Click(sender As Object, e As EventArgs) Handles INDrepLanguageDeleteAction.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim LanguageToDelete = CType(INDgrdLanguages.DefaultView.GetRow(CType(INDgrdLanguages.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), PersonLanguage)
            LanguagesDatasource.Remove(LanguageToDelete)
            ListadoEliminadosLanguage.Add(LanguageToDelete)
            INDgrdLanguages.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que limpia el popup de idiomas si se cierra cuando se esta editando un idioma
    ''' </summary>
    Private Sub INDpccLanguage_Closed(sender As Object, e As EventArgs) Handles INDpccLanguage.CloseUp
        If LanguageToEdit IsNot Nothing Then
            INDspeLanguageLevel.Text = String.Empty
            INDsleLanguageId.EditValue = -1
            LanguageToEdit = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que ubica el foco en el primer campo a diligenciar en el popup de idiomas
    ''' </summary>
    Private Sub INDpccLanguage_Popup(sender As Object, e As EventArgs) Handles INDpccLanguage.Popup
        INDsleLanguageId.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al cerrar el popup
    ''' </summary>
    Private Sub LanguagePopup_CloseUp(sender As Object, e As EventArgs) Handles INDpccLanguage.CloseUp
        INDbtnAddStudy.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre el boton
    ''' </summary>
    Private Sub INDbtnAddLanguage_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnAddLanguage.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDbtnAddStudy.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre la rejilla
    ''' </summary>
    Private Sub INDgrvLanguages_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDgrvLanguages.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDbtnAddStudy.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna el foco al control despues de abierto el popup
    ''' </summary>
    Private Sub INDpccLanguage_GotFocus(sender As Object, e As EventArgs) Handles INDpccLanguage.GotFocus
        INDsleLanguageId.Focus()
    End Sub

#End Region

#Region "PopUp Profesiones"

    ''' <summary>
    ''' Metodo que añade una profesion al listado de profesiones
    ''' </summary>
    Private Sub INDbtnAddNewProfession_Click(sender As Object, e As EventArgs) Handles INDbtnAddNewProfession.Click

        If Not ValidateProfessionsPopupControl() Then
            Exit Sub
        End If

        If ProfessionsDatasource.Where(Function(i) i.IdProfession = INDsleProfessionId.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ItemYaAgregado)
            Exit Sub
        End If

        Dim ctrAdd = 0

        If ProfessionToEdit Is Nothing Then
            ProfessionToEdit = New PersonProfession()
            ctrAdd = 1
        End If

        With ProfessionToEdit
            .IdProfession = INDsleProfessionId.EditValue
            .IsSupported = INDgleIsSupported.EditValue
            .PersonStudyId = INDgleListOfStudies.EditValue
            .Profession = New Profession With {.Id = INDsleProfessionId.EditValue, .Name = INDsleProfessionId.Text}
            .State = True
        End With

        If ctrAdd = 1 Then
            ProfessionsDatasource.Add(ProfessionToEdit)
        End If

        CleanProfessionsPopUp()

        INDgrdProfessions.RefreshDataSource()
        ProfessionToEdit = Nothing
        INDsleProfessionId.Focus()
    End Sub

    ''' <summary>
    ''' Limpia el popup de profesiones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanProfessionsPopUp()
        INDsleProfessionId.EditValue = -1
        INDgleIsSupported.EditValue = Nothing
        INDgleListOfStudies.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Limpia el pop up de rentas exentas
    ''' </summary>
    Private Sub CleanExemptIncomePopUp()
        ExemptIncomeValue = Nothing
        DateExemptIcome = Date.Today()
        ExemptIncomeComment = ""
    End Sub

    ''' <summary>
    ''' Funcion que valida los campos vacios
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateExemptIncome() As Boolean

        If ExemptIncomeValue = 0 Or DateExemptIcome = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Los campos deben ser llenados"
            Return False
        ElseIf ExemptIncomeValue < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor de la renta no puede ser Negativo"
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Eliminar profesion
    ''' </summary>
    Private Sub INDbtnDeleteProfession_Click(sender As Object, e As EventArgs) Handles INDrepProfessionDeleteAction.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim ToDelete = CType(INDgrdProfessions.DefaultView.GetRow(CType(INDgrdProfessions.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), PersonProfession)
            ToDelete.MarkAsDeleted()
            ProfessionsDatasource.Remove(ToDelete)
            ListadoEliminadosProfession.Add(ToDelete)
            INDgrdProfessions.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que limpia el popup de profesiones si se cierra cuando se esta editando una profesion
    ''' </summary>
    Private Sub INDpccProfession_Closed(sender As Object, e As EventArgs) Handles INDpccProfession.CloseUp
        If ProfessionToEdit IsNot Nothing Then
            CleanProfessionsPopUp()
            ProfessionToEdit = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que ubica el foco en el primer campo a diligenciar en el popup de idiomas
    ''' </summary>
    Private Sub INDpccProfession_Popup(sender As Object, e As EventArgs) Handles INDpccProfession.Popup
        INDsleProfessionId.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que activa o desactiva el control de lista de estudio en el popup del formulario para seleccionar un estudio que soporte la profesion
    ''' </summary>
    Private Sub INDgleIsSupported_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleIsSupported.EditValueChanged
        If INDgleIsSupported.EditValue = True Then
            INDlciListOfStudies.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlciListOfStudies.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDgleListOfStudies.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al cerrar el popup
    ''' </summary>
    Private Sub INDpccProfession_CloseUp(sender As Object, e As EventArgs) Handles INDpccProfession.CloseUp
        INDtxtDependents.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre el boton
    ''' </summary>
    Private Sub INDbtnAddProfession_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnAddProfession.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDtxtDependents.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre la rejilla
    ''' </summary>
    Private Sub INDgrvProfessions_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDgrvProfessions.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDtxtDependents.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna el foco al control despues de abierto el popup
    ''' </summary>
    Private Sub INDpccProfession_GotFocus(sender As Object, e As EventArgs) Handles INDpccProfession.GotFocus
        INDsleProfessionId.Focus()
    End Sub

#End Region

#Region "PopUp Relaciones"

    ''' <summary>
    ''' Metodo que añade una relacion al listado de grupo familiar
    ''' </summary>
    Private Sub INDbtnAddRelationship_Click(sender As Object, e As EventArgs) Handles INDbtnAddNewRelationship.Click

        If Not ValidateRelationshipsPopupControl() Then
            Exit Sub
        End If

        If RelationshipsDatasource.Where(Function(i) i.Name = INDtxtPersonName.Text.Trim AndAlso i.KinshipId = INDsleKinshipId.EditValue AndAlso i.BirthDate = INDdtePersonBirthdate.EditValue).Count > 0 AndAlso RelationshipToEdit Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ItemYaAgregado)
            Exit Sub
        End If

        Dim ctrAdd = 0

        If RelationshipToEdit Is Nothing Then
            RelationshipToEdit = New Relationship()
            ctrAdd = 1
        End If

        With RelationshipToEdit
            .Name = INDtxtPersonName.Text.Trim
            .KinshipId = INDsleKinshipId.EditValue
            .Kinship = New Kinship With {.Id = INDsleKinshipId.EditValue, .Name = INDsleKinshipId.Text}
            .IsEmergencyContact = INDgleIsEmergencyContact.EditValue
            .PhoneNumber = INDtxtPhoneNumber.Text
            .ProvidesUPC = INDgleProvidesUpc.EditValue
            .Dependent = INDgleDependent.EditValue
            .BirthDate = INDdtePersonBirthdate.EditValue
            .UPCValue = If(INDtxtUpcValue.Text.Trim.Equals(String.Empty), 0, Decimal.Parse(INDtxtUpcValue.Text))
            .Gender = CType(INDSlGenderParents.EditValue, Byte)
            .IdentificationNumber = IdentificationNumberRelationship
            .IdentificationType = IdentificationTypeRelationship

            If INDSlDependsTypeValue.EditValue IsNot Nothing Then
                .DependsType = Integer.Parse(INDSlDependsTypeValue.EditValue)
                If INDSlDependsTypeValue.EditValue = 1 Then
                    .DependsValue = If(INDtxtDependsValueRTF.Text.Trim.Equals(String.Empty), 0, Decimal.Parse(INDtxtDependsValueRTF.EditValue))
                    .DependsPercentage = 0
                Else
                    .DependsValue = 0
                    .DependsPercentage = If(INDSpDependsPercentage.Text.Trim.Equals(String.Empty), 0, Decimal.Parse(INDSpDependsPercentage.EditValue))
                End If
            Else
                .DependsType = Nothing
                .DependsValue = 0
                .DependsPercentage = 0
            End If

            .State = 1
        End With

        If ctrAdd = 1 Then
            RelationshipsDatasource.Add(RelationshipToEdit)
        End If

        CleanRelationshipsPopUp()

        INDgrdRelationships.RefreshDataSource()

        INDtxtDependents.EditValue = RelationshipsDatasource.Where(Function(i) i.Dependent = True).Count

        RelationshipToEdit = Nothing
        INDsleKinshipId.Focus()
    End Sub

    ''' <summary>
    ''' Limpia el popup de relaciones
    ''' </summary>
    Public Sub CleanRelationshipsPopUp()
        INDtxtPersonName.Text = String.Empty
        INDsleKinshipId.Properties.NullText = String.Empty
        INDsleKinshipId.EditValue = -1
        INDdtePersonBirthdate.EditValue = Nothing
        INDgleIsEmergencyContact.EditValue = Nothing
        INDtxtPhoneNumber.Text = String.Empty
        INDgleProvidesUpc.EditValue = Nothing
        INDgleDependent.EditValue = Nothing
        INDtxtUpcValue.Text = String.Empty
        INDtxtDependsValueRTF.EditValue = Nothing
        INDSlDependsTypeValue.EditValue = 0
        INDSpDependsPercentage.EditValue = 0
        IdentificationNumberRelationship = String.Empty
        IdentificationTypeRelationship = Nothing
    End Sub

    ''' <summary>
    ''' Editar relaciones
    ''' </summary>
    Private Sub INDbtnEditRelationship_Click(sender As Object, e As EventArgs) Handles INDbtnEditRelationship.Click
        RelationshipToEdit = CType(INDgrdRelationships.DefaultView.GetRow(CType(INDgrdRelationships.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), Relationship)
        INDtxtPersonName.Text = RelationshipToEdit.Name
        INDsleKinshipId.Properties.NullText = RelationshipToEdit.Kinship.Name
        INDsleKinshipId.EditValue = RelationshipToEdit.KinshipId
        INDgleIsEmergencyContact.EditValue = RelationshipToEdit.IsEmergencyContact
        INDtxtPhoneNumber.Text = RelationshipToEdit.PhoneNumber
        INDgleProvidesUpc.EditValue = RelationshipToEdit.ProvidesUPC
        INDgleDependent.EditValue = RelationshipToEdit.Dependent
        INDdtePersonBirthdate.EditValue = RelationshipToEdit.BirthDate
        INDtxtUpcValue.Text = RelationshipToEdit.UPCValue
        INDSlGenderParents.EditValue = RelationshipToEdit.Gender
        IdentificationNumberRelationship = RelationshipToEdit.IdentificationNumber
        IdentificationTypeRelationship = RelationshipToEdit.IdentificationType


        INDSlDependsTypeValue.EditValue = RelationshipToEdit.DependsType

        If INDSlDependsTypeValue.EditValue = 1 Then
            INDtxtDependsValueRTF.EditValue = RelationshipToEdit.DependsValue
            INDSpDependsPercentage.EditValue = 0
        Else
            INDtxtDependsValueRTF.EditValue = 0
            INDSpDependsPercentage.EditValue = RelationshipToEdit.DependsPercentage
        End If

        INDbtnAddRelationship.ShowDropDown()
    End Sub

    ''' <summary>
    ''' Eliminar relacion
    ''' </summary>
    Private Sub INDbtnDeleteRelationship_Click(sender As Object, e As EventArgs) Handles INDbtnDeleteRelationship.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim ToDelete = CType(INDgrdRelationships.DefaultView.GetRow(CType(INDgrdRelationships.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), Relationship)
            RelationshipsDatasource.Remove(ToDelete)
            ListadoEliminadosRelationship.Add(ToDelete)
            INDgrdRelationships.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que limpia el popup de relaciones si se cierra cuando se esta editando una relacion
    ''' </summary>
    Private Sub INDpccRelationship_Closed(sender As Object, e As EventArgs) Handles INDpccRelationship.CloseUp
        If RelationshipToEdit IsNot Nothing Then
            CleanRelationshipsPopUp()
            RelationshipToEdit = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que ubica el foco en el primer campo a diligenciar en el popup de relaciones
    ''' </summary>
    Private Sub INDpccRelationship_Popup(sender As Object, e As EventArgs) Handles INDpccRelationship.Popup
        INDsleKinshipId.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que muestre el campo para que el valor del PhoneNumber se digite
    ''' </summary>
    Private Sub INDgleIsEmergencyContact_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleIsEmergencyContact.EditValueChanged
        If INDgleIsEmergencyContact.EditValue IsNot Nothing AndAlso INDgleIsEmergencyContact.EditValue = True Then
            INDlciPhoneNumber.ShowLayout()
        Else
            INDlciPhoneNumber.HideLayout()
            INDtxtPhoneNumber.Text = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Metodo que muestre el campo para que el valor del UPC se digite
    ''' </summary>
    Private Sub INDgleProvidesUpc_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleProvidesUpc.EditValueChanged
        If INDgleProvidesUpc.EditValue IsNot Nothing AndAlso INDgleProvidesUpc.EditValue = True Then
            INDlciUpcValue.ShowLayout()
        Else
            INDlciUpcValue.HideLayout()
            INDtxtUpcValue.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al cerrar el popup
    ''' </summary>
    Private Sub INDpccRelationship_CloseUp(sender As Object, e As EventArgs) Handles INDpccRelationship.CloseUp
        INDctrContractViewer.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre el boton
    ''' </summary>
    Private Sub INDbtnAddRelationship_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnAddRelationship.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDctrContractViewer.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre la rejilla
    ''' </summary>
    Private Sub INDgrvRelationships_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDgrvRelationships.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDctrContractViewer.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna el foco al control despues de abierto el popup
    ''' </summary>
    Private Sub INDpccRelationship_GotFocus(sender As Object, e As EventArgs) Handles INDpccRelationship.GotFocus
        INDsleKinshipId.Focus()
    End Sub

#End Region

#Region "PopUp Estudios"

    ''' <summary>
    ''' Metodo que añade un estudio al listado de estudios
    ''' </summary>
    Private Sub INDbtnAddStudy_Click(sender As Object, e As EventArgs) Handles INDbtnAddNewStudy.Click

        If Not ValidateStudiesPopupControl() Then
            Exit Sub
        End If

        If StudyToEdit Is Nothing AndAlso StudyDatasource.Where(Function(i) i.StudyCenterId = INDsleStudyCenterId.EditValue AndAlso i.Name = INDtxtStudyName.Text AndAlso i.StudyCenterId = INDsleStudyCenterId.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ItemYaAgregado)
            Exit Sub
        End If

        Dim ctrAdd = 0

        If StudyToEdit Is Nothing Then
            StudyToEdit = New PersonStudy()
            ctrAdd = 1
        End If

        With StudyToEdit
            .AverageGrade = If(INDtxtAverageGrade.Text.Trim.Equals(String.Empty), Nothing, INDtxtAverageGrade.Text)
            .CityId = INDsleStudyCityId.EditValue
            .EndingDate = If(INDdteStudyEndingDate.EditValue IsNot Nothing AndAlso INDdteStudyEndingDate.EditValue.Equals(String.Empty), Nothing, INDdteStudyEndingDate.EditValue)
            .GraduationDate = If(INDdteStudyGraduationDate.EditValue IsNot Nothing AndAlso INDdteStudyGraduationDate.EditValue.Equals(String.Empty), Nothing, INDdteStudyGraduationDate.EditValue)
            .IsFormal = INDgleIsFormal.EditValue
            .IsInternal = If(INDgleIsInternal.EditValue Is Nothing, False, INDgleIsInternal.EditValue)
            .Length = If(Not String.IsNullOrEmpty(INDtxtStudyLength.Text.Trim) AndAlso CType(INDtxtStudyLength.Text.Trim, Integer) > 0, INDtxtStudyLength.Text.Trim, 0)
            .Name = INDtxtStudyName.Text
            .ProfessionalCardExpeditionDate = If(INDdteProfessionalCardExpeditionDate.EditValue IsNot Nothing AndAlso INDdteProfessionalCardExpeditionDate.EditValue.Equals(String.Empty), Nothing, INDdteProfessionalCardExpeditionDate.EditValue)
            .ProfessionalCardNumber = If(INDtxtProfessionalCardNumber.Text.Trim.Equals(String.Empty), Nothing, INDtxtProfessionalCardNumber.Text)
            .ProfessionalCardOnProcess = If(INDgleProfessionalCardOnProcess.EditValue Is Nothing, False, INDgleProfessionalCardOnProcess.EditValue)
            .StartDate = INDdteStudyStartDate.EditValue
            .Status = CType(INDgleStudyStatus.EditValue, Byte)
            .StudyCenterId = INDsleStudyCenterId.EditValue
            .StudyTypeId = INDsleStudyTypeID.EditValue
            .StudyType = New StudyType With {.Id = INDsleStudyTypeID.EditValue, .Name = INDsleStudyTypeID.Text, .StudyLevel = CByte(StudyLevel.Item2)}
            Dim parsedValue As Integer
            If Integer.TryParse(Convert.ToString(INDtxtStudyValue.EditValue), parsedValue) Then
                .StudyValue = parsedValue
            Else
                .StudyValue = 0
            End If

            Dim parseValuePercentage As Integer
            If Integer.TryParse(Convert.ToString(INDtxtStudyValueCompanyPercentage.EditValue), parseValuePercentage) Then
                .StudyValueCompanyPercentage = parseValuePercentage
            Else
                .StudyValueCompanyPercentage = 0
            End If
            .TimeUnitId = INDsleTimeUnitId.EditValue
        End With

        If ctrAdd = 1 Then
            StudyDatasource.Add(StudyToEdit)
        End If

        CleanStudyPopUp()

        INDgrdStudies.RefreshDataSource()

        StudyToEdit = Nothing
        INDsleStudyTypeID.Focus()
    End Sub

    ''' <summary>
    ''' Limpia el popup de estudios
    ''' </summary>
    Public Sub CleanStudyPopUp()
        INDtxtAverageGrade.Text = String.Empty
        INDsleStudyCityId.EditValue = -1
        INDdteStudyEndingDate.EditValue = Nothing
        INDdteStudyGraduationDate.EditValue = Nothing
        INDgleIsFormal.EditValue = Nothing
        INDgleIsInternal.EditValue = Nothing
        INDtxtStudyLength.Text = String.Empty
        INDtxtStudyName.Text = String.Empty
        INDdteProfessionalCardExpeditionDate.EditValue = Nothing
        INDtxtProfessionalCardNumber.Text = String.Empty
        INDgleProfessionalCardOnProcess.EditValue = Nothing
        INDdteStudyStartDate.EditValue = Nothing
        INDgleStudyStatus.EditValue = Nothing
        INDsleStudyCenterId.EditValue = -1
        INDsleStudyTypeID.EditValue = -1
        INDtxtStudyValue.Text = String.Empty
        INDtxtStudyValueCompanyPercentage.Text = String.Empty
        INDsleTimeUnitId.EditValue = -1
    End Sub

    ''' <summary>
    ''' Editar Estudio
    ''' </summary>
    Private Sub INDbtnEditStudy_Click(sender As Object, e As EventArgs) Handles INDbtnEditStudy.Click
        StudyToEdit = CType(INDgrdStudies.DefaultView.GetRow(CType(INDgrdStudies.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), PersonStudy)

        INDtxtAverageGrade.Text = StudyToEdit.AverageGrade
        INDsleStudyCityId.EditValue = StudyToEdit.CityId
        INDdteStudyEndingDate.EditValue = StudyToEdit.EndingDate
        INDdteStudyGraduationDate.EditValue = StudyToEdit.GraduationDate
        INDgleIsFormal.EditValue = StudyToEdit.IsFormal
        INDgleIsInternal.EditValue = StudyToEdit.IsInternal
        INDtxtStudyLength.Text = StudyToEdit.Length
        INDtxtStudyName.Text = StudyToEdit.Name
        INDdteProfessionalCardExpeditionDate.EditValue = StudyToEdit.ProfessionalCardExpeditionDate
        INDtxtProfessionalCardNumber.Text = StudyToEdit.ProfessionalCardNumber
        INDgleProfessionalCardOnProcess.EditValue = StudyToEdit.ProfessionalCardOnProcess
        INDdteStudyStartDate.EditValue = StudyToEdit.StartDate
        INDgleStudyStatus.EditValue = StudyToEdit.Status
        INDsleStudyCenterId.EditValue = StudyToEdit.StudyCenterId
        INDsleStudyTypeID.EditValue = StudyToEdit.StudyTypeId
        StudyLevel = New Tuple(Of Integer, Integer, String)(EmployeeHelper.StudyHierarchy.Where(Function(i) i.Item2 = StudyToEdit.StudyType.StudyLevel).FirstOrDefault.Item1, StudyToEdit.StudyType.StudyLevel, StudyToEdit.StudyType.Name)
        If StudyToEdit.StudyType.StudyLevel = EmployeeHelper.StudyHierarchy.Where(Function(i) i.Item1 = Universitario).FirstOrDefault.Item2 Then
            INDlcgProfessionalCardInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlcgProfessionalCardInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        INDtxtStudyValue.Text = StudyToEdit.StudyValue
        INDtxtStudyValueCompanyPercentage.Text = StudyToEdit.StudyValueCompanyPercentage
        INDsleTimeUnitId.EditValue = StudyToEdit.TimeUnitId

        INDbtnAddStudy.ShowDropDown()
    End Sub

    ''' <summary>
    ''' Eliminar relacion
    ''' </summary>
    Private Sub INDbtnDeleteStudy_Click(sender As Object, e As EventArgs) Handles INDbtnDeleteStudy.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim ToDelete = CType(INDgrdStudies.DefaultView.GetRow(CType(INDgrdStudies.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), PersonStudy)
            StudyDatasource.Remove(ToDelete)
            ListadoEliminadosStudy.Add(ToDelete)
            INDgrdStudies.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que limpia el popup de relaciones si se cierra cuando se esta editando una relacion
    ''' </summary>
    Private Sub INDpccStudy_Closed(sender As Object, e As EventArgs) Handles INDpccStudy.CloseUp
        If StudyToEdit IsNot Nothing Then
            CleanStudyPopUp()
            StudyToEdit = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que ubica el foco en el primer campo a diligenciar en el popup de relaciones
    ''' </summary>
    Private Sub INDpccStudy_Popup(sender As Object, e As EventArgs) Handles INDpccStudy.Popup
        INDsleStudyTypeID.Focus()
        'muestra el grupo de tarjeta profesional si no hay estudio en edicion
        If StudyToEdit Is Nothing Then
            INDlcgProfessionalCardInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

    End Sub

    ''' <summary>
    ''' Metodo que muestre el campo para que el la fecha de expedicion de la tarjeta profesional se digite
    ''' </summary>
    Private Sub INDgleProfessionalCardOnProcess_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleProfessionalCardOnProcess.EditValueChanged
        If INDgleProfessionalCardOnProcess.EditValue IsNot Nothing AndAlso INDgleProfessionalCardOnProcess.EditValue = False Then
            INDlciProfessionalCardExpeditionDate.ShowLayout()
            INDlciProfessionalCardNumber.ShowLayout()
        Else
            INDlciProfessionalCardExpeditionDate.HideLayout()
            INDlciProfessionalCardNumber.HideLayout()
            INDdteProfessionalCardExpeditionDate.EditValue = Nothing
            INDtxtProfessionalCardNumber.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que cambia la visualizacion de las fechas de acuerdo al estado seleccionado
    ''' </summary>
    Private Sub INDgleStudyStatus_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleStudyStatus.EditValueChanged

        'Oculta el grupo de tarjeta profesional siempre
        INDlcgProfessionalCardInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If INDgleStudyStatus.EditValue = 1 OrElse INDgleStudyStatus.EditValue = 2 Then
            INDlciEndingDate.HideLayout()
            INDlciGraduationDate.HideLayout()
            INDdteStudyEndingDate.EditValue = Nothing
            INDdteStudyGraduationDate.EditValue = Nothing
        ElseIf INDgleStudyStatus.EditValue = 3 Then
            INDlciEndingDate.ShowLayout()
            INDlciGraduationDate.HideLayout()
            INDdteStudyGraduationDate.EditValue = Nothing
        ElseIf INDgleStudyStatus.EditValue = 4 Then
            INDlciEndingDate.ShowLayout()
            INDlciGraduationDate.ShowLayout()

            If StudyLevel IsNot Nothing AndAlso StudyLevel.Item1 = Universitario Then
                INDlcgProfessionalCardInfo.HideControl(False)
            End If
        Else
            INDlciEndingDate.ShowLayout()
            INDlciGraduationDate.ShowLayout()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que cambia la visualizacion de campos de acuerdo a si el estudio es proveido por la empresa
    ''' </summary>
    Private Sub INDgleIsInternal_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleIsInternal.EditValueChanged
        If INDgleIsInternal.EditValue IsNot Nothing AndAlso INDgleIsInternal.EditValue = True Then
            INDtxtStudyValue.BackColor = Drawing.Color.MistyRose
            INDlciStudyValueCompanyPercentage.ShowLayout()
        ElseIf INDgleIsInternal.EditValue IsNot Nothing AndAlso INDgleIsInternal.EditValue = False Then
            INDtxtStudyValue.BackColor = Drawing.Color.White
            INDlciStudyValueCompanyPercentage.HideLayout()
            INDtxtStudyValueCompanyPercentage.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al cerrar el popup
    ''' </summary>
    Private Sub INDbtnAddStudy_CloseUp(sender As Object, e As EventArgs) Handles INDpccStudy.CloseUp
        INDbtnAddProfession.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre el boton
    ''' </summary>
    Private Sub INDbtnAddStudy_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnAddStudy.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDbtnAddProfession.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre la rejilla
    ''' </summary>
    Private Sub INDgrvStudies_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDgrvStudies.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDbtnAddProfession.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna el foco al control despues de abierto el popup
    ''' </summary>
    Private Sub INDpccStudy_GotFocus(sender As Object, e As EventArgs) Handles INDpccStudy.GotFocus
        INDgleIsFormal.Focus()
    End Sub

    ''' <summary>
    ''' Metodo para habilitar el grupo de tarjeta profesional dependiendo del tipo de estudio
    ''' </summary>
    Private Sub INDsleStudyTypeID_EditValueChanged() Handles INDsleStudyTypeID.EditValueChanged
        Dim s = EmployeeHelper.ConvertFromSelectedRowInGridViewToXpoEntity(Of PayrollStudyTypeXpo)(INDgrvSleStudyTypeId)

        If s IsNot Nothing Then

            StudyLevel = EmployeeHelper.StudyHierarchy.Where(Function(i) i.Item2 = s.StudyLevel).FirstOrDefault

            If s.StudyLevel = EmployeeHelper.StudyHierarchy.Where(Function(i) i.Item1 = Universitario).FirstOrDefault.Item2 AndAlso INDgleStudyStatus.EditValue = EmployeeHelper.StudyStatus.Where(Function(i) i.Item1 = Graduado).FirstOrDefault.Item2 Then
                INDlcgProfessionalCardInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDlcgProfessionalCardInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If

    End Sub

#End Region

#Region "Contrato"

    ''' <summary>
    ''' Metodo que se encarga que asignar los valores basicos al empleado antes de enviarlos al contrato
    ''' </summary>
    Public Sub NewContract(sender As Object, e As System.EventArgs) Handles INDctrContractViewer.OnNewContractInitialized
        If ValidateControls() = True Then
            BasicAssigningValues()
            INDctrContractViewer.Valid = True
        Else
            INDctrContractViewer.Valid = False
        End If
    End Sub

#End Region

#End Region

#Region "Eventos Generales"

#Region "Validacion de fechas"
    ''' <summary>
    ''' Metodo que valida que las fecha de nacimiento cumplan con el orden logico
    ''' </summary>
    Private Sub INDdteBirthDate_EditValueChanging(sender As Object, e As EventArgs) Handles INDdteBirthDate.Leave

        Dim f = If(INDdteBirthDate.EditValue Is Nothing, "", CType(INDdteBirthDate.EditValue, Date))

        If f IsNot Nothing AndAlso Not f.ToString.Trim.Equals(String.Empty) Then

            Dim edad = Math.Abs(DateDiff(DateInterval.Year, Date.Today, INDdteBirthDate.EditValue))

            INDlblAge.Text = edad

            If edad < 15 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(MinimoQuinceLaborar, Empleado)
                INDdteBirthDate.EditValue = Nothing
            End If

            If INDdteDeathDate.EditValue IsNot Nothing AndAlso Not INDdteDeathDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(f, Date).CompareTo(CType(INDdteDeathDate.EditValue, Date)) >= 0 Then 'Si la fecha de nacimiento es mayor o igual a la fecha de muerte
                INDdteDeathDate.EditValue = Nothing
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciDeathDate.Text, INDlciBirthDate.Text)
            ElseIf INDdteIdDate.EditValue IsNot Nothing AndAlso Not INDdteIdDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(f, Date).CompareTo(CType(INDdteIdDate.EditValue, Date)) >= 0 Then 'Si la fecha de nacimiento es mayor o igual a la fecha de expedicion del documento de identidad
                INDdteIdDate.EditValue = Nothing
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciIdDate.Text, INDlciBirthDate.Text)
            ElseIf INDdteStudyEndingDate.EditValue IsNot Nothing AndAlso Not INDdteStudyEndingDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(f, Date).CompareTo(CType(INDdteStudyEndingDate.EditValue, Date)) >= 0 Then 'Si la fecha de nacimiento es mayor o igual a la fecha de finalizacion del estudio en edicion
                INDdteStudyEndingDate.EditValue = Nothing
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciEndingDate.Text, INDlciBirthDate.Text)
            ElseIf INDdteStudyGraduationDate.EditValue IsNot Nothing AndAlso Not INDdteStudyGraduationDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(f, Date).CompareTo(CType(INDdteStudyGraduationDate.EditValue, Date)) >= 0 Then 'Si la fecha de nacimiento es mayor o igual a la fecha de graduacion del estudio en edicion
                INDdteStudyGraduationDate.EditValue = Nothing
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciGraduationDate.Text, INDlciBirthDate.Text)
            ElseIf INDdteStudyStartDate.EditValue IsNot Nothing AndAlso Not INDdteStudyStartDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(f, Date).CompareTo(CType(INDdteStudyStartDate.EditValue, Date)) >= 0 Then 'Si la fecha de nacimiento es mayor o igual a la fecha de inicio del estudio en edicion
                INDdteStudyStartDate.EditValue = Nothing
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciStartDate.Text, INDlciBirthDate.Text)
            ElseIf INDdteProfessionalCardExpeditionDate.EditValue IsNot Nothing AndAlso Not INDdteProfessionalCardExpeditionDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(f, Date).CompareTo(CType(INDdteProfessionalCardExpeditionDate.EditValue, Date)) >= 0 Then 'Si la fecha de nacimiento es mayor o igual a la fecha de tarjeta profesional
                INDdteProfessionalCardExpeditionDate.EditValue = Nothing
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciProfessionalCardExpeditionDate.Text, INDlciBirthDate.Text)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida que las fecha de fallecimiento cumplan con el orden logico
    ''' </summary>
    Private Sub INDdteDeathDate_EditValueChanging(sender As Object, e As EventArgs) Handles INDdteDeathDate.Leave

        Dim d As Date = If(INDdteDeathDate.EditValue IsNot Nothing AndAlso Not INDdteDeathDate.EditValue.ToString.Trim.Equals(String.Empty), CDate(INDdteDeathDate.EditValue), Nothing)

        If d <> Nothing Then
            If INDdteBirthDate.EditValue IsNot Nothing AndAlso Not INDdteBirthDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso d.CompareTo(CType(INDdteBirthDate.EditValue, Date)) <= 0 Then 'Si la fecha de nacimiento es menor o igual a la fecha de fallecimiento
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciBirthDate.Text, INDlciDeathDate.Text)
            ElseIf INDdteIdDate.EditValue IsNot Nothing AndAlso Not INDdteIdDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso d.CompareTo(CType(INDdteIdDate.EditValue, Date)) <= 0 Then 'Si la fecha de expedicion del documento es menor o igual a la fecha de fallecimiento
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciIdDate.Text, INDlciDeathDate.Text)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida que las fecha de expedicion del documento de identidad cumplan con el orden logico
    ''' </summary>
    Private Sub INDdteIdDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDdteIdDate.EditValueChanging

        If e.NewValue IsNot Nothing AndAlso Not e.NewValue.ToString.Trim.Equals(String.Empty) Then
            If INDdteBirthDate.EditValue IsNot Nothing AndAlso Not INDdteBirthDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteBirthDate.EditValue, Date)) <= 0 Then 'Si la fecha de fecha de nacimiento es menor o igual a la fecha de expedicion del documento de identidad
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciIdDate.Text, INDlciBirthDate.Text)
            ElseIf INDdteDeathDate.EditValue IsNot Nothing AndAlso Not INDdteDeathDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteDeathDate.EditValue, Date)) >= 0 Then 'Si la fecha de nacimiento es menor o igual a la fecha de expedicion del documento de identidad
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciDeathDate.Text, INDlciIdDate.Text)
            End If
        End If

    End Sub

    ''' <summary>
    ''' Metodo que valida que las fecha de inicio del estudio cumplan con el orden logico
    ''' </summary>
    Private Sub INDdteStudyStartDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDdteStudyStartDate.EditValueChanging

        If e.NewValue IsNot Nothing AndAlso Not e.NewValue.ToString.Trim.Equals(String.Empty) Then
            If INDdteBirthDate.EditValue IsNot Nothing AndAlso Not INDdteBirthDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteBirthDate.EditValue, Date)) <= 0 Then 'Si la fecha de fecha de nacimiento es mayor o igual a la fecha de inicio del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciStartDate.Text, INDlciBirthDate.Text)
            ElseIf INDdteStudyEndingDate.EditValue IsNot Nothing AndAlso Not INDdteStudyEndingDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteStudyEndingDate.EditValue, Date)) >= 0 Then 'Si la fecha de inicio del estudio es mayor o igual a la fecha de terminacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciEndingDate.Text, INDlciStartDate.Text)
            ElseIf INDdteStudyGraduationDate.EditValue IsNot Nothing AndAlso Not INDdteStudyGraduationDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteStudyGraduationDate.EditValue, Date)) >= 0 Then 'Si la fecha de inicio del estudio es mayor o igual a la fecha de graduacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciGraduationDate.Text, INDlciStartDate.Text)
            ElseIf INDdteProfessionalCardExpeditionDate.EditValue IsNot Nothing AndAlso Not INDdteProfessionalCardExpeditionDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteProfessionalCardExpeditionDate.EditValue, Date)) >= 0 Then 'Si la fecha de inicio del estudio es mayor o igual a la fecha de graduacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciProfessionalCardExpeditionDate.Text, INDlciStartDate.Text)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida que las fecha de terminacion del estudio cumplan con el orden logico
    ''' </summary>
    Private Sub INDdteStudyEndingDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDdteStudyEndingDate.EditValueChanging

        If e.NewValue IsNot Nothing AndAlso Not e.NewValue.ToString.Trim.Equals(String.Empty) Then
            If INDdteBirthDate.EditValue IsNot Nothing AndAlso Not INDdteBirthDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteBirthDate.EditValue, Date)) <= 0 Then 'Si la fecha de fecha de nacimiento es mayor o igual a la fecha de terminacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciEndingDate.Text, INDlciBirthDate.Text)
            ElseIf INDdteStudyStartDate.EditValue IsNot Nothing AndAlso Not INDdteStudyStartDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteStudyStartDate.EditValue, Date)) <= 0 Then 'Si la fecha de inicio del estudio es menor o igual a la fecha de terminacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciEndingDate.Text, INDlciStartDate.Text)
            ElseIf INDdteStudyGraduationDate.EditValue IsNot Nothing AndAlso Not INDdteStudyGraduationDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteStudyGraduationDate.EditValue, Date)) <= 0 Then 'Si la fecha de inicio del estudio es mayor o igual a la fecha de graduacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciEndingDate.Text, INDlciGraduationDate.Text)
            ElseIf INDdteProfessionalCardExpeditionDate.EditValue IsNot Nothing AndAlso Not INDdteProfessionalCardExpeditionDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteProfessionalCardExpeditionDate.EditValue, Date)) >= 0 Then 'Si la fecha de inicio del estudio es mayor o igual a la fecha de graduacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciProfessionalCardExpeditionDate.Text, INDlciEndingDate.Text)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida que las fecha de graduacion del estudio cumplan con el orden logico
    ''' </summary>
    Private Sub INDdteStudyGraduationDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDdteStudyGraduationDate.EditValueChanging

        If e.NewValue IsNot Nothing AndAlso Not e.NewValue.ToString.Trim.Equals(String.Empty) Then
            If INDdteBirthDate.EditValue IsNot Nothing AndAlso Not INDdteBirthDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteBirthDate.EditValue, Date)) <= 0 Then 'Si la fecha de fecha de nacimiento es mayor o igual a la fecha de terminacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciGraduationDate.Text, INDlciBirthDate.Text)
            ElseIf INDdteStudyStartDate.EditValue IsNot Nothing AndAlso Not INDdteStudyStartDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteStudyStartDate.EditValue, Date)) <= 0 Then 'Si la fecha de inicio del estudio es menor o igual a la fecha de terminacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciGraduationDate.Text, INDlciStartDate.Text)
            ElseIf INDdteStudyEndingDate.EditValue IsNot Nothing AndAlso Not INDdteStudyEndingDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteStudyEndingDate.EditValue, Date)) <= 0 Then 'Si la fecha de inicio del estudio es mayor o igual a la fecha de graduacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciGraduationDate.Text, INDlciEndingDate.Text)
            ElseIf INDdteProfessionalCardExpeditionDate.EditValue IsNot Nothing AndAlso Not INDdteProfessionalCardExpeditionDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteProfessionalCardExpeditionDate.EditValue, Date)) >= 0 Then 'Si la fecha de inicio del estudio es mayor o igual a la fecha de graduacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciProfessionalCardExpeditionDate.Text, INDlciGraduationDate.Text)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida que las fecha de expedicion de la tarjeta profesional cumplan con el orden logico
    ''' </summary>
    Private Sub INDdteProfessionalCardExpeditionDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDdteProfessionalCardExpeditionDate.EditValueChanging

        If e.NewValue IsNot Nothing AndAlso Not e.NewValue.ToString.Trim.Equals(String.Empty) Then
            If INDdteBirthDate.EditValue IsNot Nothing AndAlso Not INDdteBirthDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteBirthDate.EditValue, Date)) <= 0 Then 'Si la fecha de fecha de nacimiento es mayor o igual a la fecha de terminacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciProfessionalCardExpeditionDate.Text, INDlciBirthDate.Text)
            ElseIf INDdteStudyStartDate.EditValue IsNot Nothing AndAlso Not INDdteStudyStartDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteStudyStartDate.EditValue, Date)) <= 0 Then 'Si la fecha de inicio del estudio es menor o igual a la fecha de terminacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciProfessionalCardExpeditionDate.Text, INDlciStartDate.Text)
            ElseIf INDdteStudyEndingDate.EditValue IsNot Nothing AndAlso Not INDdteStudyEndingDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteStudyEndingDate.EditValue, Date)) <= 0 Then 'Si la fecha de inicio del estudio es mayor o igual a la fecha de graduacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciProfessionalCardExpeditionDate.Text, INDlciEndingDate.Text)
            ElseIf INDdteStudyGraduationDate.EditValue IsNot Nothing AndAlso Not INDdteStudyGraduationDate.EditValue.ToString.Trim.Equals(String.Empty) AndAlso CType(e.NewValue, Date).CompareTo(CType(INDdteStudyGraduationDate.EditValue, Date)) <= 0 Then 'Si la fecha de inicio del estudio es mayor o igual a la fecha de graduacion del estudio
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FechaXMenorQueY, Empleado), INDlciProfessionalCardExpeditionDate.Text, INDlciGraduationDate.Text)
            End If
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Metodo que muestre los campos libreta militar para que se digiten
    ''' </summary>
    Private Sub INDgleGender_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleGender.EditValueChanged
        If INDgleGender.EditValue IsNot Nothing Then
            If INDgleGender.EditValue = CType(1, Byte) AndAlso IdentificationTypeXpo?.SIGLA <> "TI" Then

                IndigoTextEdit1.SetCampoObligatorio(INDtxtMilitaryCardNumber, True)
                    IndigoTextEdit1.SetTamañoMinimoString(INDtxtMilitaryCardNumber, 1)
                    IndigoTextEdit1.SetCampoObligatorio(INDgleMilitaryCardType, True)
                    IndigoTextEdit1.SetTamañoMinimoString(INDgleMilitaryCardType, 1)

            ElseIf INDgleGender.EditValue = CType(3, Byte) Or (INDgleGender.EditValue = CType(1, Byte) AndAlso IdentificationTypeXpo?.SIGLA <> "TI") Then


                IndigoTextEdit1.SetCampoObligatorio(INDtxtMilitaryCardNumber, False)
                IndigoTextEdit1.SetTamañoMinimoString(INDtxtMilitaryCardNumber, 0)
                IndigoTextEdit1.SetCampoObligatorio(INDgleMilitaryCardType, False)
                IndigoTextEdit1.SetTamañoMinimoString(INDgleMilitaryCardType, 0)
            Else

                INDlciMilitaryCardNumber.HideLayout()
                INDlciMilitaryCardType.HideLayout()
                INDtxtMilitaryCardNumber.EditValue = Nothing
                INDgleMilitaryCardType.EditValue = Nothing
                IndigoTextEdit1.SetCampoObligatorio(INDtxtMilitaryCardNumber, False)
                IndigoTextEdit1.SetTamañoMinimoString(INDtxtMilitaryCardNumber, 0)
                IndigoTextEdit1.SetCampoObligatorio(INDgleMilitaryCardType, False)
                IndigoTextEdit1.SetTamañoMinimoString(INDgleMilitaryCardType, 0)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que muestra los campos informacion de pensionado para que se digiten
    ''' </summary>
    Private Sub INDglePensionary_EditValueChanged(sender As Object, e As EventArgs) Handles INDglePensionary.EditValueChanged
        If INDglePensionary.EditValue IsNot Nothing AndAlso INDglePensionary.EditValue = True Then
            INDlciPensionaryStatus.ShowLayout()
            INDlciPensionaryTypeId.ShowLayout()
            INDlciRetiredForeign.ShowLayout()
        Else
            INDlciPensionaryStatus.HideLayout()
            INDglePensionaryStatus.EditValue = Nothing
            INDlciPensionaryTypeId.HideLayout()
            INDslePensionaryTypeId.EditValue = -1
            INDlciRetiredForeign.HideLayout()
            INDgleRetiredForeign.EditValue = Nothing
        End If
    End Sub


    ''' <summary>
    ''' Metodo que muestra los campos informacion de incapacidad permanente para que se digiten
    ''' </summary>
    Private Sub INDGlePermanentInability_EditValueChanged(sender As Object, e As EventArgs) Handles INDGlePermanentInability.EditValueChanged
        If INDGlePermanentInability.EditValue IsNot Nothing AndAlso INDGlePermanentInability.EditValue = True Then
            INDLcInitialDateInability.ShowLayout()
            INDLcFinalDateInability.ShowLayout()
        Else
            INDDeInitialDateInability.EditValue = Nothing
            INDDeFinalDateInability.EditValue = Nothing
            INDLcInitialDateInability.HideLayout()
            INDLcFinalDateInability.HideLayout()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para limpiar el contenido del DateEdit al dar click en el boton de cerrar
    ''' </summary>
    Private Sub CleanDateEdit_ButtonClick(s As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDdteBirthDate.ButtonClick,
        INDdteIdDate.ButtonClick, INDdteDeathDate.ButtonClick, INDdteStudyStartDate.ButtonClick, INDdteStudyEndingDate.ButtonClick,
        INDdteStudyGraduationDate.ButtonClick, INDdteProfessionalCardExpeditionDate.ButtonClick

        If s.GetType() = GetType(DevExpress.XtraEditors.DateEdit) AndAlso e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Close Then
            CType(s, DevExpress.XtraEditors.DateEdit).EditValue = Nothing
        End If
    End Sub

    'Recorro el xpo y se le asigna a la lista
    ''' <summary>viewExemptIncomeDetail
    ''' Evento que despliega y asigna valores a la grilla hija
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub viewExemptIncome_MasterRowGetChildList(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs) Handles viewExemptIncome.MasterRowGetChildList

        Dim ExemptIncomeDetail = viewExemptIncome.GetFocusedObject(Of CommonExemptIncomeXpo) 'Selecionamos la cabecera para pasarle los hijos

        viewExemptIncome.OptionsDetail.AllowExpandEmptyDetails = True

        If e.ChildList Is Nothing Then
            viewExemptIncomeDetail.ShowLoadingPanel()

            Dim childListExpand As New BindingList(Of Domain.Entities.ExemptIncome)
            e.ChildList = childListExpand
            Dim loadingItem As New Domain.Entities.ExemptIncome With {
                .Id = 0,
                .VoucherCode = "",
                .VoucherType = String.Format("Obteniendo datos del año {0}...", ExemptIncomeDetail.Year),
                .MonthlyIncome = 0,
                .YearLiquidated = ExemptIncomeDetail.Year
            }
            childListExpand.Add(loadingItem)

            ' Cargar datos de forma asíncrona
            Dim yearToLoad = ExemptIncomeDetail.Year
            Dim thirdPartyIdToLoad = Employee.ThirdPartyId


            Dim detailList = Await Presenter.ListExemptIncomeDetailByYearAsync(thirdPartyIdToLoad, yearToLoad)

            ' Validar que detailList no sea Nothing
            If detailList IsNot Nothing Then
                ' Combinar con los datos en memoria (si hay ediciones locales)
                Dim FilterListFromMemory = listExemptIncomeGlobal.Where(Function(x) x.YearLiquidated = yearToLoad).ToList()

                ' Crear un diccionario para combinar datos del servicio con ediciones locales
                Dim combinedDetails = New Dictionary(Of String, CommonExemptIncomeDetailXpo)()

                ' Primero agregar los datos del servicio
                For Each item In detailList
                    combinedDetails(item.Id) = item
                Next

                ' Luego sobrescribir/agregar los datos en memoria (ediciones locales)
                For Each item In FilterListFromMemory
                    combinedDetails(item.Id) = item
                Next

                ' Convertir a lista de Domain.Entities.ExemptIncome usando combinedDetails (incluye ediciones locales)
                For Each ListExemptIncomeAux In combinedDetails.Values
                    Dim ListExemptIncome As New Domain.Entities.ExemptIncome 'Se crea el objeto donde se insertaron los datos
                    With ListExemptIncome
                        If ListExemptIncomeAux.IsNew = False Then 'validamos si es un registro nuevo o viene desde servicios
                            Dim idParts() As String = ListExemptIncomeAux.Id.Split("-"c)

                            Integer.TryParse(idParts(0), .Id) 'Se convierte el id a integer
                            .ThirdPartyId = ListExemptIncomeAux.ThirdPartyId
                            .DateLiquidation = ListExemptIncomeAux.DateLiquidation
                            .VoucherType = ListExemptIncomeAux.VoucherType
                            .VoucherCode = ListExemptIncomeAux.VoucherCode
                            .MonthlyIncome = ListExemptIncomeAux.MonthlyIncome
                            .ExemptIncomeValue = ListExemptIncomeAux.ExemptIncomeValue
                            .YearLiquidated = ListExemptIncomeAux.YearLiquidated
                            .RegisterStatus = ListExemptIncomeAux.RegisterStatus
                            .Comments = ListExemptIncomeAux.Comments

                        Else
                            .Id = CType(ListExemptIncomeAux.Id, Integer)
                            .ThirdPartyId = ListExemptIncomeAux.ThirdPartyId
                            .DateLiquidation = ListExemptIncomeAux.DateLiquidation
                            .VoucherType = ListExemptIncomeAux.VoucherType
                            .VoucherCode = ListExemptIncomeAux.VoucherCode
                            .MonthlyIncome = ListExemptIncomeAux.MonthlyIncome
                            .ExemptIncomeValue = ListExemptIncomeAux.ExemptIncomeValue
                            .YearLiquidated = ListExemptIncomeAux.YearLiquidated
                            .RegisterStatus = ListExemptIncomeAux.RegisterStatus
                            .Comments = ListExemptIncomeAux.Comments
                        End If

                    End With
                    childListExpand.Add(ListExemptIncome)
                Next
            End If
            childListExpand.Remove(loadingItem)
            viewExemptIncomeDetail.HideLoadingPanel()
        End If
    End Sub


    ''' <summary>
    ''' Evento que establece la relacion entre la rejilla padre e hija
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewExemptIncome_MasterRowGetRelationName(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs) Handles viewExemptIncome.MasterRowGetRelationName
        e.RelationName = "viewExemptIncome"
    End Sub
    ''' <summary>
    ''' evento que establece el tipo de relacion 1 a 1 entre padre e hija
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewExemptIncome_MasterRowGetRelationCount(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs) Handles viewExemptIncome.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub

#End Region

End Class
