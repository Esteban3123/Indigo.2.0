'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/01/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraSpreadsheet
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmMedicinesProduction

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PMedicinesProduction

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myStream As String = Nothing

    Private Medicineproduction As MedicinesProduction

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Retorno del modal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddEventArgs(sender As Object, e As EventArgs)
        BeginReloadDatasource()
    End Sub

    ''' <summary>
    ''' Carga los medicamentos para producción
    ''' </summary>
    Private Sub LoadInformation()
        If INDgcMedicinesProduction.DataSource IsNot Nothing OrElse INDsleCM.EditValue Is Nothing Then
            Exit Sub
        End If
        INDviewMedicinesProduction.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewListMedicinesProductionXpo) = Nothing
                                  Try
                                      result = Presenter.ListMedicinesProductionByCMId(INDsleCM.EditValue)
                                      INDgcMedicinesProduction.BeginInvoke(Sub()
                                                                               INDgcMedicinesProduction.DataSource = result
                                                                               INDviewMedicinesProduction.HideLoadingPanel()
                                                                           End Sub)
                                  Catch ex As Exception
                                      INDgcMedicinesProduction.BeginInvoke(Sub()
                                                                               INDviewMedicinesProduction.HideLoadingPanel()
                                                                               Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                           End Sub)
                                  End Try
                              End Sub)
    End Sub

    ''' <summary>
    ''' Vuelve a cargar el datasource de la rejilla activa
    ''' </summary>
    Private Sub BeginReloadDatasource()
        INDgcMedicinesProduction.DataSource = Nothing
        LoadInformation()
    End Sub

    ''' <summary>
    ''' Metodo que importa los items del excel a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ImportFile() As Task
        If INDsleCM.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una central de mezclas"
            Exit Function
        End If

        'Configuramos el cuadro de dialogo para importar el archivo
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)

        'Si el ususario cancela la operación
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            AsyncLoader(False)
            Exit Function
        End If

        Try
            'Obtengo la ruta del archivo
            _myStream = openFileDialog1.FileName
            If (_myStream Is Nothing OrElse _myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Function
            End If

            'Validamos los datos de excel y armamos el listado que se envia para poder pegar en la rejilla de facturas
            Dim result = LoadImportFile(_myStream)
            If result.StateResult = False Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                Exit Function
            End If

            'Se llama el metodo que utiliza el copyPaste
            Await PasteToGrid(INDgcMedicinesProduction, result.ObjectEmbbeded)

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Function

    ''' <summary>
    ''' Metodo que se encarga de validar el archivo de excel
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Private Function LoadImportFile(ByVal fileName As String) As ActionResult(Of List(Of List(Of String)))
        Dim ssc = New SpreadsheetControl()
        ssc.AllowDrop = False
        ssc.LoadDocument(_myStream)
        Dim workBook As IWorkbook = ssc.Document

        Dim rows As RowCollection = workBook.Worksheets(0).Rows
        If rows.LastUsedIndex = 0 Then
            Return New ActionResult(Of List(Of List(Of String))) With {.StateResult = False, .Message = "No se encontraron registros en el archivo"}
        End If

        Dim ListFile As New List(Of List(Of String))
        For i As Integer = 1 To rows.LastUsedIndex Step 1

            Dim item = rows.Item(i).SpreadsheetRowToList(4)
            If item.Take(4).All(Function(cell) cell IsNot Nothing AndAlso cell.ToString() <> "") Then
                Dim listItem As List(Of String) = item.Take(4).Select(Function(cell) cell.ToString()).ToList()
                ListFile.Add(listItem)
            End If
        Next

        Return New ActionResult(Of List(Of List(Of String))) With {.StateResult = True, .ObjectEmbbeded = ListFile}
    End Function

    ''' <summary>
    ''' Metodo que copia y pega los items a la rejilla de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function PasteToGrid(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
        Try
            If sender.Name = INDgcMedicinesProduction.Name AndAlso ListInfo IsNot Nothing AndAlso ListInfo.Count > 0 Then
                Dim itemsSend As Integer = 100
                Dim indexSend As Integer = 0
                Dim listMessages As New List(Of Tuple(Of String, Integer))

                AsyncLoader(True)

                While ListInfo.Count > 0
                    Dim listToSend = ListInfo.Take(itemsSend).ToList()

                    Using model As New MMedicinesProduction(Me.Tag)
                        Dim result = Await model.ImportMedicinesProduction(listToSend, INDsleCM.EditValue)
                        If result.StateResult = False Then
                            If ListInfo.Count < itemsSend Then
                                listMessages.Add(New Tuple(Of String, Integer)("Los items del " + (indexSend + 1).ToString() + " hasta " + (ListInfo.Count).ToString() + " no se pudieron guardar porque: " + result.Message, 2))
                            Else
                                listMessages.Add(New Tuple(Of String, Integer)("Los items del " + (indexSend + 1).ToString() + " hasta " + (indexSend + itemsSend).ToString() + " no se pudieron guardar porque: " + result.Message, 2))
                            End If
                        Else
                            result.ObjectEmbbeded.ForEach(Sub(x) listMessages.Add(New Tuple(Of String, Integer)(x.MessageField, If(x.StatusField = 1, 1, 2))))
                        End If
                    End Using

                    If ListInfo.Count < itemsSend Then
                        indexSend += ListInfo.Count - 1
                        ListInfo.RemoveRange(0, ListInfo.Count)
                    Else
                        indexSend += itemsSend
                        ListInfo.RemoveRange(0, itemsSend)
                    End If
                End While

                AsyncLoader(False)
                BeginReloadDatasource()

                If listMessages IsNot Nothing AndAlso listMessages.Count > 0 Then
                    Using formulario As New FrmListErrors(listMessages)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Function

    ''' <summary>
    ''' Elimina los registros
    ''' </summary>
    Private Async Sub DeleteMedicinesProduction()
        Try
            If MessageIndigo.Show("Desea eliminar los registros?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            AsyncLoader(True)
            Dim listIds = (From x In INDviewMedicinesProduction.GetSelectedRows() Select DirectCast(INDviewMedicinesProduction.GetRow(x), ViewListMedicinesProductionXpo).Id).ToList()
            If listIds IsNot Nothing AndAlso listIds.Count > 0 Then
                Using model As New MMedicinesProduction(Me.Tag)
                    Dim result = Await model.DeleteMedicinesProduction(listIds)
                    If result.StateResult = False Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        AsyncLoader(False)
                        Exit Sub
                    End If

                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Informacion) = "Registros eliminados correctamente"
                    BeginReloadDatasource()
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Private Async Sub EditMedicinesProduction()
        Try
            AsyncLoader(True)
            Dim SelectItem = INDviewMedicinesProduction.GetFocusedObject(Of ViewListMedicinesProductionXpo)
            If SelectItem IsNot Nothing Then
                Using model As New MMedicinesProduction(Me.Tag)
                    Medicineproduction = Await model.GetMedicineProductionByID(SelectItem.Id)
                End Using

                If Medicineproduction IsNot Nothing Then
                    Using formulario As New FrmAddMedicinesProduction()
                        AddHandler formulario.AddMedicinesProductionArgs, AddressOf ReturnAddEventArgs

                        formulario.CMConfigurationId = INDsleCM.EditValue
                        formulario.INDsleATC.Properties.NullText = SelectItem.ATCCodeName
                        formulario.INDsleUnitDoseType.Properties.NullText = SelectItem.UnitDoseTypeCodeName
                        formulario.INDsleCenterAttention.Properties.NullText = SelectItem.CenterAttentionCodeName
                        formulario._MedicinesProduction = Medicineproduction
                        formulario.EditMode = True
                        formulario.Size = New System.Drawing.Size(700, 600)
                        formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                        Dim transparent = New Base.FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If

                AsyncLoader(False)
                End If
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmMedicinesProduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        Presenter = New PMedicinesProduction()

        IndigoGridView1.SetListAcction(INDviewMedicinesProduction, ({eAcciones.Remove, eAcciones.Edit}).ToList)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewMedicinesProduction.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        INDEsbExport.AddExcelSheets({New ExcelSheet With {.Columns = {
                                    New ExcelColumn With {.Name = "Código Medicamento", .Type = ExcelColumnType.Text},
                                    New ExcelColumn With {.Name = "Código Tipo Dosis Unitaria", .Type = ExcelColumnType.Text},
                                    New ExcelColumn With {.Name = "Código Centro de Atención", .Type = ExcelColumnType.Text},
                                    New ExcelColumn With {.Name = "Permite remanente", .Type = ExcelColumnType.Text}}.ToList()}}.ToList())
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmMedicinesProduction_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCM.Properties.PopupFormMinSize = New System.Drawing.Size(INDsleCM.Size.Width - 11, 0)
        INDsleCM.Properties.PopupFormSize = New System.Drawing.Size(INDsleCM.Size.Width - 11, 0)
        INDsleCM.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCM_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCM.QueryPopUp
        If INDsleCM.Properties.DataSource Is Nothing Then
            INDsleCM.Properties.DataSource = Presenter.ListCMByUser()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Se dispara la cambiar el valor del control de central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCM_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCM.EditValueChanged
        If INDsleCM.EditValue IsNot Nothing Then
            INDgcMedicinesProduction.DataSource = Nothing
            LoadInformation()
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case button.Tag.ToString
            Case "Edit"
                EditMedicinesProduction()
            Case "Remove"
                DeleteMedicinesProduction()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditMedicinesProduction()
            Case "Remove"
                DeleteMedicinesProduction()
        End Select
    End Sub

#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Evento que se dispara al presionar agregar item
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBarAddItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarAddItem.ItemClick
        If INDsleCM.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una central de mezclas"
            Exit Sub
        End If
        Using formulario As New FrmAddMedicinesProduction()
            AddHandler formulario.AddMedicinesProductionArgs, AddressOf ReturnAddEventArgs
            formulario.CMConfigurationId = INDsleCM.EditValue
            formulario.Size = New System.Drawing.Size(700, 600)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar refrescar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBarRefreshDatasource_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarRefreshDatasource.ItemClick
        BeginReloadDatasource()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al exportar la estructura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBarExportStructure_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarExportStructure.ItemClick
        INDEsbExport.ExecuteOnClick()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al importar el archivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBarImportFile_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarImportFile.ItemClick
        Await ImportFile()
    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If INDsleCM.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una central de mezclas"
            Exit Sub
        End If
        Await PasteToGrid(sender, e.Rows)
    End Sub

#End Region

#End Region

End Class