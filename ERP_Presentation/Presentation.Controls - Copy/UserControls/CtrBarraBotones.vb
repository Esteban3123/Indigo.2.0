Imports Domain.DocumentalSystem.Entities
Imports System.Runtime.Serialization
Imports Domain.Entities
Imports Presentation.Controls.MVP
Imports Presentation.Base

Public Class CtrBarraBotones
    Implements IBarraBotones

#Region "Eventos Publicos de la Barra de Botones"

    ''' <summary>
    ''' Se Declara el evento [clic restablecer layout].
    ''' </summary>
    Public Event ClicRestablecerLayout()
    ''' <summary>
    ''' Se Declara el evento [click_ nuevo].
    ''' </summary>
    Public Event ClickNuevo()
    ''' <summary>
    ''' Se Declara el evento [click_ guardar].
    ''' </summary>
    Public Event ClickGuardar()
    ''' <summary>
    ''' Se Declara el evento [click_ liquidar].
    ''' </summary>
    Public Event ClickLiquidar()
    ''' <summary>
    ''' Se Declara el evento [click_ consultliquidar].
    ''' </summary>
    Public Event ClickConsultLiquidar()
    ''' <summary>
    ''' Se Declara el evento [click_ ConfirmLiquidation].
    ''' </summary>
    Public Event ClickConfirmLiquidation()
    ''' <summary>
    ''' Se Declara el evento [click_ Actualizar].
    ''' </summary>
    Public Event ClickActualizar()
    ''' <summary>
    ''' Se Declara el evento [click_ eliminar].
    ''' </summary>
    Public Event ClickEliminar()
    ''' <summary>
    ''' Se Declara el evento [click_ adicionar].
    ''' </summary>
    ''' 
    Public Event ClickAdicionarRejilla()
    ''' <summary>
    ''' Variable que almacena si se ha dado clic en el boton actualizar permisos
    ''' </summary>
    Property ClicBotonActualizar As Boolean Implements IBarraBotones.ClicBotonActualizar
    ''' <summary>
    ''' Se Declara el evento [click_ modificar].
    ''' </summary>
    Public Event ClickModificarRejilla()
    ''' <summary>
    ''' Se Declara el evento [click_ eliminar grilla].
    ''' </summary>
    Public Event ClickEliminarRejilla()
    ''' <summary>
    ''' Se Declara el evento [click_ confirmar].
    ''' </summary>
    Public Event ClickConfirmar()
    ''' <summary>
    ''' Se Declara el evento [click_ confirmar].
    ''' </summary>
    Public Event ClickConfirmarTodos()
    ''' <summary>
    ''' Se Declara el evento [click_ anular].
    ''' </summary>
    Public Event ClickAnular()
    ''' <summary>
    ''' Se Declara el evento [click_ Suspender]
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ClickSuspender()
    ''' <summary>
    ''' Se Declara el evento [click_ Reactivar]
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ClickReactivar()
    ''' <summary>
    ''' Se Declara el evento [click_ imprimir].
    ''' </summary>
    Public Event ClickImprimir()
    ''' <summary>
    ''' Se Declara el evento [click_ buscar].
    ''' </summary>
    Public Event ClickBuscar()
    ''' <summary>
    ''' Se Declara el evento [click_ deshacer].
    ''' </summary>
    Public Event ClickDeshacer()
    ''' <summary>
    ''' Se Declara el evento [click_ favoritos].
    ''' </summary>
    Public Event ClickFavoritos()
    ''' <summary>
    ''' Se Declara el evento [click_ pegar].
    ''' </summary>
    Public Event ClickPegar()
    ''' <summary>
    ''' Se Declara el evento [click_ ClickOcultarBarra].
    ''' </summary>
    Public Event ClickOcultarBarra()
    ''' <summary>
    ''' Se Declara el evento [click_ ClickCustomizar].
    ''' </summary>
    Public Event ClickCustomizar()
    ''' <summary>
    ''' Occurs when [verifica permiso customizar].
    ''' </summary>
    Public Event VerificaPermisoCustomizar()
    ''' <summary>
    ''' Occurs when [edita flujo pacientes].
    ''' </summary>
    Public Event Click_EditarFlujoPacientes()
    ''' <summary>
    ''' Se lanza cuando se ha minimizado la barra
    ''' </summary>
    Public Event ToolBarsMinimized(ByVal sender As Object, ByVal e As EventArgs)
    ''' <summary>
    ''' Se lanza cuando se ha maximizado la barra
    ''' </summary>
    Public Event ToolBarsMaximized(ByVal sender As Object, ByVal e As EventArgs)
    ''' <summary>
    ''' Evento que se dispara al dar clic en los botones activar e inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_ActiveInactive()
    ''' <summary>
    ''' Evento que se dispara al dar clic sobre el boton refrescar rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_RefreshGrid()
    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton generar archivo
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_GenerateFile()

    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton generar archivo
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ChangueOperatingUnit(operatingUnit As OperatingUnit)

    ''' <summary>
    ''' se dispara al dar click sobre guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_GuardarConfirmar()

    ''' <summary>
    ''' Se ejecuta cuando se da click sobre desconfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_Desconfirmar()

    ''' <summary>
    ''' Se ejecuta al dar sobre el boton terminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_Terminar()

    ''' <summary>
    ''' Evento que se dispara al dar click sobre jerarquia
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_Jerarquia()

    ''' <summary>
    ''' Evento que se dispara al dar click sobre importar informacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_ImportarInformacion()

    ''' <summary>
    ''' Evento que se dispara cuando dan Click en Actualizar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_ActualizarConfirmar()

    ''' <summary>
    ''' Evento que se dispara cuando dan Click en Entrega Manual
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_EntregaManual()

    ''' <summary>
    ''' Evento click boton validar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_Validar()

    ''' <summary>
    ''' Evento click boton Deshacer Todo
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Click_DeshacerTodo()

    ''' <summary>
    ''' Evento que se dispara cuando se cambia de registro en la navegacion de la barra
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Public Event RecordNavigationChangeEvent(ByVal Record As Object)

#End Region


    ''' <summary>
    ''' Propiedad que obtiene o establece si el control es visible.
    ''' </summary>
    Public Property StatusRecordVisible As Boolean
        Get
            Return True
        End Get
        Set(value As Boolean)
            
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el estado seleccionado.
    ''' </summary>
    ''' <value>
    ''' valor de estado que tiene el registro.
    ''' </value>
    Public Property StatusRecord As Object
        Get
            Return Nothing
        End Get
        Set(value As Object)
            
        End Set
    End Property

    Private _StatesWhitActions As eActionsStatusRecords()
    ''' <summary>
    ''' Propiedad que obtiene y establece un array de acciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatesWhitActions As eActionsStatusRecords()
        Get
            Return _StatesWhitActions
        End Get
        Set(value As eActionsStatusRecords())
            'Dim ListStates As New List(Of StatusRecord)
            'If value IsNot Nothing Then
            '    For Each State As eActionsStatusRecords In value
            '        Dim NewState As StatusRecord
            '        Dim Color As Color
            '        Select Case State
            '            Case eActionsStatusRecords.Active
            '                Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))
            '            Case eActionsStatusRecords.Confirmed
            '                Color = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
            '            Case eActionsStatusRecords.Finalized
            '                Color = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
            '            Case eActionsStatusRecords.Inactive
            '                Color = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))
            '            Case eActionsStatusRecords.Invalidate
            '                Color = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
            '            Case eActionsStatusRecords.Liquidated
            '                Color = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
            '            Case eActionsStatusRecords.Replaced
            '                Color = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
            '            Case eActionsStatusRecords.Suspended
            '                Color = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
            '        End Select
            '        NewState = New StatusRecord With {.StatusColor = Color, .StatusName = Infrastructure.CrossCutting.Resources.ResourceManager.GetString(State.ToString, Me.GetType()), .StatusValue = State}
            '        ListStates.Add(NewState)
            '    Next
            '    'CType(INDgleStatusRecord, GridLookUpEdit).Properties.DataSource = ListStates
            'End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece los estados que puede tener el registro.
    ''' </summary>
    ''' <value>
    ''' El listado de objetos.
    ''' </value>
    <Obsolete("Use la propiedad StatesWhitActions para asignar los estados")>
    Public Property States As List(Of StatusRecord)
        Get
            Return Nothing 'CType(INDgleStatusRecord, GridLookUpEdit).Properties.DataSource
        End Get
        Set(value As List(Of StatusRecord))
            'CType(INDgleStatusRecord, GridLookUpEdit).Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Actualiza los permisos de la barra.
    ''' </summary>
    Public Sub ActualizarPermisosBarra(ByVal codigoFormulario As String)
        
    End Sub

    Public Sub AddAuditBasic(name As String, value As Object)

    End Sub

    ''' <summary>
    ''' Limpiar los controles de auditoria basica
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanAuditBasic()

    End Sub


    ''' <summary>
    ''' Deshabilita la barra de digitalización
    ''' </summary>
    Public Sub DisableBarDocument()

    End Sub

    ''' <summary>
    ''' Habilitar los botones de acciones correspondientes al formulario
    ''' </summary>
    Sub EnableBarItems()

    End Sub
    ''' <summary>
    ''' Propiedad para controlar si el usuario puede consultar
    ''' </summary>
    Property PermiteConsultar As Boolean
        Get
            Return True
        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property


    ''' <summary>
    ''' Metodo encargado de habilitar el sistema documental según registro
    ''' </summary>
    Public Async Sub SetDocuments(idEntity As Integer, Optional tagForm As String = "", Optional formAux As FormBase = Nothing, Optional entityName As String = Nothing)

    End Sub

    ''' <summary>
    ''' Establecer mensaje en barra superior
    ''' </summary>
    ''' <param name="message"></param>
    ''' <param name="type"></param>
    Sub ShowXtraMessage(ByVal message As String, ByVal type As ImagesXtraLabel, ByVal user As String)

    End Sub

    ''' <summary>
    ''' Variable para la lista de documentos
    ''' </summary>
    Public _listDocuments As List(Of DocumentsStore)

    ''' <summary>
    ''' Prepara la barra dependiendo de la acción seleccionada
    ''' </summary>
    ''' <param name="action">Acción seleccionada para preparar la barra</param>
    Public Sub PrepareToolbar(ByVal action As eAction)

    End Sub

    Dim _ColumnInfo As List(Of ColumnInfo) = Nothing
    ''' <summary>
    ''' Obtiene o asigna la lista de columnas que se motraran
    ''' y enlazaran en el datasource de la rejilla
    ''' </summary>
    ''' <value>Lista de columnas a enlazar</value>
    ''' <returns>Lista de columnas enlazadas</returns>
    Public Property ColumnInfo As List(Of ColumnInfo)
        Get
            Return Me._ColumnInfo
        End Get
        Set(value As List(Of ColumnInfo))
            'Me._ColumnInfo = value
            'INDgcvRecords.Columns.Clear()
            'If Me._ColumnInfo IsNot Nothing Then
            '    For Each ci As ColumnInfo In Me._ColumnInfo
            '        INDgcvRecords.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn())
            '        INDgcvRecords.Columns(INDgcvRecords.Columns.Count - 1).Name = "Col" & INDgcvRecords.Columns.Count - 1
            '        INDgcvRecords.Columns(INDgcvRecords.Columns.Count - 1).Caption = ci.Caption.Trim()
            '        INDgcvRecords.Columns(INDgcvRecords.Columns.Count - 1).FieldName = ci.FieldName.Trim()
            '        INDgcvRecords.Columns(INDgcvRecords.Columns.Count - 1).Visible = ci.Visible
            '    Next
            'End If
        End Set
    End Property

    Private _FilterDataSource As IEnumerable(Of Object)
    ''' <summary>
    ''' Propiedad que obtiene o establece el datasource para poder navegar a traves de registros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilterDataSource As IEnumerable(Of Object)
        Get
            Return _FilterDataSource
        End Get
        Set(value As IEnumerable(Of Object))
            'RecordPosition = 0
            '_FilterDataSource = value
            'INDgcRecords.DataSource = value
            'If value IsNot Nothing AndAlso value.Count > 1 Then
            '    BarBtnFilter.Caption = String.Concat(RecordPosition + 1, " de ", FilterDataSource.Count)
            '    RibbonPageNavigationRecords.Visible = True
            '    LoadControlsNavigation()
            '    RaiseEvent RecordNavigationChangeEvent(value(RecordPosition))
            'Else
            '    RibbonPageNavigationRecords.Visible = False
            'End If
        End Set
    End Property

    Public WriteOnly Property Boton(bBotonTag As Integer) As Boolean Implements IBarraBotones.Boton
        Set(value As Boolean)

        End Set
    End Property

    Public Property PermissionsForm As Dictionary(Of Integer, String) Implements IBarraBotones.PermissionsForm

    Public Property PermitirCustomizarFuncional As Boolean Implements IBarraBotones.PermitirCustomizarFuncional

    ''' <summary>
    ''' Propiedad que me permite habilitar los botones sin permisos que quiero ocultar.
    ''' </summary>
    Public WriteOnly Property OcultarBotonesSinPermisos(ByVal Boton As EbuttonsWithoutPermission) As Boolean
        Set(value As Boolean)

        End Set
    End Property

End Class

''' <summary>
''' Encapsula el estado de un registro
''' </summary>
<Serializable()>
Public Class StatusRecord
    Implements ISerializable

    ''' <summary>
    ''' Obtiene o asigna el nombre del estado
    ''' </summary>
    ''' <value>Nombre del estado</value>
    ''' <returns>El nombre del estado</returns>
    Public Property StatusName As String
    ''' <summary>
    ''' Obtiene o asigna el valor del estado
    ''' </summary>
    ''' <value>Valor del estado</value>
    ''' <returns>El valor del estado</returns>
    Public Property StatusValue As Object
    ''' <summary>
    ''' Obtiene o asigna el color del estado
    ''' </summary>
    ''' <value>Color del estado</value>
    ''' <returns>El color del estado</returns>
    Public Property StatusColor As Color

    Public Sub New()
        Me.StatusName = ""
        Me.StatusValue = ""
        StatusColor = Color.White
    End Sub

    Public Sub New(info As SerializationInfo, context As StreamingContext)
        If info Is Nothing Then
            Throw New ArgumentNullException("information")
        End If
        Me.StatusName = info.GetString("StatusName")
        Me.StatusValue = info.GetValue("StatusValue", GetType(Object))
        Me.StatusColor = Convert.ChangeType(info.GetValue("StatusColor", GetType(Color)), GetType(Color))
    End Sub

    Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        If info Is Nothing Then
            Throw New ArgumentNullException("info")
        End If
        info.AddValue("StatusName", Me.StatusName.Trim())
        info.AddValue("StatusValue", Me.StatusValue)
        info.AddValue("StatusColor", Me.StatusColor.ToString())
    End Sub

End Class
