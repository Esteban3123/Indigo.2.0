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
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Controls
Imports Domain.Base.Entities
#End Region

Partial Public Class FrmEmployee
#Region "ICRUD"

    ''' <summary>
    ''' Abre el control de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllEmployee
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Cedula", .FieldName = "Nit"}, New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name"}}.ToList()
            BarraBotones.PrepareToolbar(eAction.New)
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
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbteIdNumber.Text = ReturnValue
        If INDbteIdNumber.Text <> String.Empty Then

            LoadControls()
            If INDbteIdNumber.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteIdNumber.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: item buscar del control de usuario
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: item deshacer del control de usuario
    ''' </summary>
    Public Async Sub Deshacer() Implements Base.IcrudBase.Deshacer
        Await CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Employee IsNot Nothing Then
            If Employee.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MEmployee(MEmployee.TAG)
                        AsyncLoader(True)
                        Dim resultDelete = Await Model.DeleteEmployeeAsync(Employee)
                        AsyncLoader(False)
                        If resultDelete.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            Deshacer()
                        ElseIf resultDelete.MessageResult.Count > 0 Then
                            Dim message As String = obtenerRecurso(EmpleadoNoSeElimino, Empleado) & vbCrLf
                            For Each messageItem As MessageResult In resultDelete.MessageResult
                                Select Case messageItem.CodeMessage
                                    Case "E001"
                                        message &= obtenerRecurso(EmpleadoTieneLiquidaciones, Empleado) & vbCrLf
                                    Case "E002"
                                        message &= obtenerRecurso(EmpleadoTieneLiquidacionContrato, Empleado) & vbCrLf
                                    Case "E003"
                                        message &= obtenerRecurso(EmpleadoTieneNovedades, Empleado) & vbCrLf
                                    Case "E004"
                                        message &= obtenerRecurso(EmpleadoTieneTurno, Empleado) & vbCrLf
                                    Case "E005"
                                        message &= obtenerRecurso(EmpleadoTieneConvenios, Empleado) & vbCrLf
                                End Select
                            Next
                            Mensaje(EeventViewerImages.Advertencia) = message
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                        End If
                    End Using
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control del usuario
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Try
            Dim errors = ValidateGridControls()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe agregar información a: " & vbCrLf & errors
                Exit Sub
            End If
            If Not ValidateControls() Then
                Exit Sub
            End If


            AssigningValues()
            Using Model As New MEmployee(MEmployee.TAG)
                AsyncLoader(True)
                Dim FilterGlobalList = listToSaveExemptIncome.Where(Function(x) x.IsNew = True).ToList()

                Dim EmployeeResult = Await Model.SaveEmployeeAsync(Employee, FilterGlobalList, listToDeleteExemptIncome)

                AsyncLoader(False)
                If EmployeeResult.StateResult Then
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    Nuevo()
                Else

                    Dim ListString As New List(Of String)
                    For Each ObjListContractLiquidation As MessageResult In EmployeeResult.MessageResult
                        ListString.Add(ObjListContractLiquidation.Parameters(0))
                    Next

                    Using formulario As New FrmListErrors(ListString)
                        formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End Using

            listToDeleteExemptIncome.Clear()
        Catch ex As Exception
            AsyncLoader(False)
            ActionsOnControls = False
        End Try
    End Sub

    ''' <summary>
    '''  este permite establecer la logica para los permisos de Guardar y Actualizar 
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' METODO: Item nuevo del control de usuario
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        CleanControls()
    End Sub

#End Region

End Class
